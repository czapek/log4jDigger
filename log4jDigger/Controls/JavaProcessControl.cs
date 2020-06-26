using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.IO;
using static System.Windows.Forms.ListViewItem;
using System.Text.RegularExpressions;

namespace log4jDigger.Controls
{
    public partial class JavaProcessControl : UserControl
    {
        private const int COL_ARGS = 6;

        public JavaProcessControl()
        {
            InitializeComponent();
        }

        public void ScanProcesses()
        {
            listViewJavaProcesses.Items.Clear();
            foreach (Process p in Process.GetProcessesByName("java").Union(Process.GetProcessesByName("javaw")))
            {
                JavaProcess jp = new JavaProcess(p);
                ListViewItem item = new ListViewItem();
                item.Tag = jp;
                item.Text = jp.Process.Id.ToString();

                ListViewSubItem sItemName = new ListViewSubItem();
                sItemName.Text = jp.Process.ProcessName;
                item.SubItems.Add(sItemName);

                ListViewSubItem sItemStartTime = new ListViewSubItem();
                try
                {
                    sItemStartTime.Text = jp.Process.StartTime.ToString("dd.MM.yy HH:mm:ss");
                }
                catch (Exception ex)
                {
                    sItemStartTime.Text = ex.Message;
                }
                item.SubItems.Add(sItemStartTime);

                ListViewSubItem sItemBytes = new ListViewSubItem();
                sItemBytes.Text = $"Bytes";
                item.SubItems.Add(sItemBytes);

                ListViewSubItem sItemThreads = new ListViewSubItem();
                sItemThreads.Text = $"{jp.Process.Threads.Count:n0}";
                item.SubItems.Add(sItemThreads);

                ListViewSubItem sItemCpu = new ListViewSubItem();
                sItemCpu.Text = $"CPU";
                item.SubItems.Add(sItemCpu);

                ListViewSubItem sItemArgs = new ListViewSubItem();
                sItemArgs.Text = jp.CommandLine;
                item.SubItems.Add(sItemArgs);

                listViewJavaProcesses.Items.Add(item);
            }
        }       

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanProcesses();
        }

        private void contextMenuStripProcess_Opening(object sender, CancelEventArgs e)
        {
            if (listViewJavaProcesses.SelectedItems.Count > 0)
            {
                openFolderToolStripMenuItem.DropDownItems.Clear();
                JavaProcess jp = (JavaProcess)listViewJavaProcesses.SelectedItems[0].Tag;
                if (jp.Paths != null)
                {
                    foreach (String path in jp.Paths)
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem();
                        menuItem.Text = path;
                        menuItem.Click += MenuItem_Click;
                        openFolderToolStripMenuItem.DropDownItems.Add(menuItem);
                    }
                }
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            String path = menuItem.Text;
            if (File.Exists(path))
                path = Path.GetDirectoryName(path);

            if (Directory.Exists(path))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = path,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
        }

        private void startArgumentsToClipbardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewJavaProcesses.SelectedItems.Count > 0)
            {
                String args = listViewJavaProcesses.SelectedItems[0].SubItems[COL_ARGS].Text;
                if (!String.IsNullOrWhiteSpace(args))
                    Clipboard.SetText(listViewJavaProcesses.SelectedItems[0].SubItems[COL_ARGS].Text);
            }
        }


        private class JavaProcess
        {
            public Process Process { get; private set; }
            public String CommandLine { get; private set; }
            public List<String> Paths { get; private set; }

            public JavaProcess(Process process)
            {
                Process = process;
                CommandLine = GetCommandLine(Process) ?? "";
                ScanProcess();
            }

            private static string GetCommandLine(Process process)
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
                using (ManagementObjectCollection objects = searcher.Get())
                {
                    return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
                }
            }

            private void ScanProcess()
            {
                List<String> quotedParts = new List<string>();
                String cmdLine = CommandLine;
                foreach (Match match in Regex.Matches(cmdLine, "\"([^\"]*)\""))
                {
                    quotedParts.Add(match.ToString().Trim('"'));
                    cmdLine = cmdLine.Replace(match.ToString(), "");
                }

                quotedParts.Add(cmdLine.Replace(' ', '='));

                List<String> pathNames = new List<string>();
                List<Char> splitChars = new List<char>() { '<', '>', '"', '|', '?', '*', ';', ',', '=' };

                foreach (String qpart in quotedParts)
                {
                    foreach (String part in qpart.Split(splitChars.ToArray()))
                    {
                        if (part.Trim().Length > 0)
                        {
                            String testFolder = part.Replace('/', '\\').Trim();

                            if (!testFolder.Contains('\\'))
                                continue;

                            String[] testFolderParts = testFolder.Split(':');
                            if (testFolderParts.Length > 1)
                            {
                                testFolder = testFolderParts[testFolderParts.Length - 2] + ":" + testFolderParts[testFolderParts.Length - 1];
                            }

                            FileInfo fileInfo = new FileInfo(testFolder);
                            if (fileInfo.Exists)
                            {
                                pathNames.Add(fileInfo.FullName);
                            }
                            else
                            {
                                DirectoryInfo dirInfo = new DirectoryInfo(testFolder);
                                if (dirInfo.Exists)
                                {
                                    pathNames.Add(dirInfo.FullName);

                                    foreach (String subDirs in Directory.GetDirectories(dirInfo.FullName))
                                    {
                                        if (subDirs.EndsWith("WEB-INF"))
                                        {
                                            pathNames.Add(subDirs);
                                            break;
                                        }
                                        else if (Directory.Exists(Path.Combine(subDirs, "WEB-INF")))
                                        {
                                            pathNames.Add(Path.Combine(subDirs, "WEB-INF"));
                                            break;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                Paths = pathNames.Distinct().OrderBy(x => x).ToList();
            }
        }
    }
}
