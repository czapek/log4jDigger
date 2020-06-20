using log4jDigger.Controls;
using SingleInstancing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace log4jDigger
{
    public partial class MainForm : Form, ISingleInstanceEnforcer
    {
        private BackgroundWorker workerIndex;
        private BackgroundWorker workerSearch;
        private TabPage infoTabPage;
        private LoglineInfoControl infoControl;
        private StreamingFactory streamingFactory;
        private delegate void OnMessageReceivedInvoker(MessageEventArgs e);
        private delegate void SafeAfterInConsistent();
        private System.Windows.Forms.Timer timerNewLogFilesAdded;
        private LogListControl selectedLogListControl;
        private static MainForm currentMainForm;
        private delegate void SafeFlashTrayIcon();
        private const int BasketStateCol = 3;

        public String[] Args;

        public MainForm()
        {
            InitializeComponent();

            selectedLogListControl = logListControlMain;
            this.Size = new Size(1600, 800);
            streamingFactory = new StreamingFactory();
            streamingFactory.IsInConsistent += StreamingFactory_IsInConsistent;
            currentMainForm = this;
            workerIndex = new BackgroundWorker();
            workerIndex.WorkerReportsProgress = true;
            workerIndex.WorkerSupportsCancellation = true;
            workerIndex.ProgressChanged += WorkerIndex_ProgressChanged;
            workerIndex.DoWork += WorkerIndex_DoWork;
            workerIndex.RunWorkerCompleted += WorkerIndex_RunWorkerCompleted;

            workerSearch = new BackgroundWorker();
            workerSearch.WorkerReportsProgress = true;
            workerSearch.WorkerSupportsCancellation = true;
            workerSearch.ProgressChanged += WorkerSearch_ProgressChanged;
            workerSearch.DoWork += WorkerSearch_DoWork;
            workerSearch.RunWorkerCompleted += WorkerSearch_RunWorkerCompleted;

            timerNewLogFilesAdded = new System.Windows.Forms.Timer();
            timerNewLogFilesAdded.Interval = 500;
            timerNewLogFilesAdded.Enabled = false;
            timerNewLogFilesAdded.Tick += TimerNewLogFilesAdded_Tick;

            String mainLogDir = LogUtils.FindLatestLogDir();
            if (mainLogDir != null)
                openFileDialogBasket.InitialDirectory = mainLogDir;
        }

        private void TimerNewLogFilesAdded_Tick(object sender, EventArgs e)
        {
            timerNewLogFilesAdded.Enabled = false;
            if (workerIndex.IsBusy)
                return;

            WorkaroundForInitialInstance();
            CreateIndex();
        }

        /// <summary>
        /// TODO: wenn über Shell Extension initial mehrere Dateien geöffnet werden klappt das mit dem Mutex nicht 
        /// </summary>
        private void WorkaroundForInitialInstance()
        {
            Boolean wait = true;
            while (wait)
            {
                try
                {
                    if (File.Exists(Program.WorkaroundForInitialInstancePath))
                        foreach (String line in File.ReadAllLines(Program.WorkaroundForInitialInstancePath))
                        {
                            AddToBasket(line);
                        }
                    File.Delete(Program.WorkaroundForInitialInstancePath);
                    wait = false;
                }
                catch (Exception)
                {

                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Args.Length > 0)
            {
                ParseArgs(Args);
            }
            else
            {
                foreach (FileInfo info in LogUtils.FindLatesLogfiles())
                    AddToBasket(info.FullName, false);
            }
        }

        public void OnMessageReceived(MessageEventArgs e)
        {
            OnMessageReceivedInvoker invoker = delegate (MessageEventArgs eventArgs)
            {
                string[] args = eventArgs.Message as string[];

                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;

                bool top = TopMost;
                TopMost = true;
                TopMost = top;
                ParseArgs(args);
            };

            if (InvokeRequired)
                Invoke(invoker, e);
            else
                invoker(e);
        }

        private void ParseArgs(string[] args)
        {
            if (args.Length == 2 && args[0] == "W")
            {
                if (AddToBasket(args[1]))
                {
                    tabControlMain.SelectedTab = tabPageBasket;
                    timerNewLogFilesAdded.Enabled = true;
                }
            }
            else if (args.Length == 1 && AddToBasket(args[0]))
                CreateIndex();
        }

        public void OnNewInstanceCreated(EventArgs e)
        {

        }

        private bool AddToBasket(String file)
        {
            return AddToBasket(file, true);
        }

        private bool AddToBasket(String file, bool isChecked)
        {
            if (String.IsNullOrWhiteSpace(file))
                return false;

            if (listViewBasket.Items.Cast<ListViewItem>().Any(x => x.Text == file))
                return false;

            FileInfo fi = new FileInfo(file);
            if (fi.Exists && fi.Length > 0)
            {
                ListViewItem item = new ListViewItem(file);
                item.Tag = file;
                item.Checked = true;
                ListViewItem.ListViewSubItem lvsSize = new ListViewItem.ListViewSubItem();
                lvsSize.Text = $"{fi.Length / 1024:n0} KB";
                item.SubItems.Add(lvsSize);

                ListViewItem.ListViewSubItem lvsLastChanged = new ListViewItem.ListViewSubItem();
                lvsLastChanged.Text = fi.LastWriteTime.ToString();
                item.SubItems.Add(lvsLastChanged);

                ListViewItem.ListViewSubItem lvsState = new ListViewItem.ListViewSubItem();
                lvsState.Text = "added";
                item.SubItems.Add(lvsState);

                item.Checked = isChecked;
                listViewBasket.Items.Add(item);
                return true;
            }
            return false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (workerIndex.IsBusy)
                workerIndex.CancelAsync();
            streamingFactory.Dispose();
        }

        private void buttonCreateIndex_Click(object sender, EventArgs e)
        {
            if (workerIndex.IsBusy)
            {
                workerIndex.CancelAsync();
                buttonCreateIndex.Text = "Create index";
            }
            else
            {
                Clear();
                CreateIndex();
            }
        }

        private void DisableForIndex()
        {
            buttonCreateIndex.Text = "Abort";

            this.Cursor = Cursors.WaitCursor;
            buttonAddFiles.Enabled = false;
            buttonClear.Enabled = false;
            logListControlMain.Enabled = false;
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name != "tabPageBasket")
                    tp.Controls[0].Enabled = false;
        }

        private void EnableForIndex()
        {
            buttonCreateIndex.Text = "Create index";

            this.Cursor = Cursors.Default;
            buttonAddFiles.Enabled = true;
            buttonClear.Enabled = true;
            logListControlMain.Enabled = true;
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name != "tabPageBasket")
                    tp.Controls[0].Enabled = true;
        }

        private void Clear()
        {
            selectedLogListControl.Clear();
            if (workerIndex.IsBusy)
                workerIndex.CancelAsync();

            logListControlMain.VirtualListSize = 0;
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name == "tabPageSearchResult")
                    ((LogListControl)tp.Controls[0]).VirtualListSize = 0;

            streamingFactory.IsInConsistent -= StreamingFactory_IsInConsistent;
            streamingFactory.Dispose();
            streamingFactory = new StreamingFactory();
            streamingFactory.IsInConsistent += StreamingFactory_IsInConsistent;

            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(tabPageBasket);
            tabControlMain.TabPages.Add(tabPageSearch);
        }

        private void StreamingFactory_IsInConsistent(object sender, EventArgs e)
        {
            var d = new SafeAfterInConsistent(AfterInConsistent);
            this.Invoke(d);
        }

        private void AfterInConsistent()
        {
            logListControlMain.Follow = false;
            Clear();
            CreateIndex();
        }

        private void CreateIndex()
        {
            if (listViewBasket.CheckedItems.Count == 0)
                return;

            if (workerIndex.IsBusy)
                return;

            logListControlMain.Follow = false;

            List<String> fileList = new List<String>();
            foreach (ListViewItem item in listViewBasket.Items)
            {
                if (item.Checked)
                {
                    item.SubItems[BasketStateCol].Text = "pending";
                    fileList.Add(item.Text);
                }
                else
                {
                    item.SubItems[BasketStateCol].Text = "ignored";
                }
            }
            workerIndex.RunWorkerAsync(fileList);
            DisableForIndex();
        }

        private void WorkerIndex_DoWork(object sender, DoWorkEventArgs e)
        {
            List<String> fileList = (List<String>)e.Argument;
            int progress = 0;
            foreach (String file in fileList)
            {
                workerIndex.ReportProgress(progress);
                if (workerIndex.CancellationPending)
                    break;

                streamingFactory.AddNewFile(file, workerIndex, progress);
                progress += 100;
            }
            workerIndex.ReportProgress(progress);
            streamingFactory.PositionList = streamingFactory.PositionList.OrderBy(x => x.TimeStamp).ThenBy(x => x.Order).ToList();
        }

        private void WorkerIndex_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            selectedLogListControl.ShortInfo = $"{streamingFactory.PositionList.Count - 1:n0} lines";
            searchControlMain.StreamingFactory = streamingFactory;
            selectedLogListControl.SetStreamingFactory(streamingFactory);

            EnableForIndex();
            AddInfoTabPage(-1);
        }

        private void WorkerIndex_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int index = (int)(e.ProgressPercentage / 100.0);
            int rel = e.ProgressPercentage - (index * 100);
            if (index < listViewBasket.CheckedItems.Count)
            {
                listViewBasket.CheckedItems[index].SubItems[BasketStateCol].Text = "indexing ... " + rel + "%";
                listViewBasket.CheckedItems[index].EnsureVisible();
            }

            if (index > 0 && listViewBasket.CheckedItems.Count >= index)
                listViewBasket.CheckedItems[index - 1].SubItems[BasketStateCol].Text = "done";
        }

        bool noSearch = false;
        private void searchControlMain_SearchEvent(object sender, SearchEventArgs e)
        {
            if (noSearch)
                return;

            if (workerSearch.IsBusy)
            {
                workerSearch.CancelAsync();
            }
            else
            {
                noSearch = true;
                foreach (TabPage tp in tabControlMain.TabPages)
                    if (tp.Name == "tabPageSearchResult"
                        && ((LogListControl)tp.Controls[0]).LongInfo == e.ToString())
                    {
                        searchControlMain.SetProgress(100);
                        tabControlMain.SelectedTab = tp;
                        noSearch = false;
                        return;
                    }

                this.Cursor = Cursors.WaitCursor;
                logListControlMain.Enabled = false;
                foreach (TabPage tp in tabControlMain.TabPages)
                    if (tp.Name != "tabPageSearch")
                        tp.Controls[0].Enabled = false;

                Application.DoEvents();
                workerSearch.RunWorkerAsync(e);
                noSearch = false;
            }
        }

        private void WorkerSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            SearchEventArgs sea = (SearchEventArgs)e.Argument;
            streamingFactory.Search(sea, workerSearch);
            e.Result = sea;
        }

        private void WorkerSearch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            searchControlMain.SetProgress(e.ProgressPercentage);
        }

        private void WorkerSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchEventArgs sea = (SearchEventArgs)e.Result;
            List<LogPos> searchResult = streamingFactory.GetSearchResult(sea);
            if (searchResult != null && searchResult.Count > 0)
            {
                TabPage tp = new TabPage();
                LogListControl llc = new LogListControl();
                tp.Controls.Add(llc);
                llc.Dock = DockStyle.Fill;
                llc.DoubleClickListView += Llc_DoubleClickListView;
                llc.Follow = logListControlMain.Follow;
                tp.Name = "tabPageSearchResult";
                tp.Padding = new System.Windows.Forms.Padding(3);
                tp.Text = "Search Result";
                tp.UseVisualStyleBackColor = true;
                tabControlMain.TabPages.Add(tp);
                tabControlMain.SelectedTab = tp;
                llc.SetStreamingFactory(streamingFactory, sea);
            }

            logListControlMain.Enabled = true;
            foreach (TabPage tp in tabControlMain.TabPages)
                tp.Controls[0].Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void Llc_SelectedIndexChangedListView(object sender, EventArgs e)
        {
            LogListControl llc = sender as LogListControl;
            if (llc.SelectedIndex >= 0)
            {
                int index = llc.SelectedIndex;
                llc.ShortInfo = $"Line {index:n0} / {llc.VirtualListSize - 1:n0} ({llc.SelectedLogPos.Order:n0})";
            }
        }

        private void AddInfoTabPage(int pos)
        {
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name == "tabPageInfo" && ((LoglineInfoControl)tp.Controls[0]).SelectedLine == pos)
                    return;

            infoTabPage = new TabPage();
            infoControl = new LoglineInfoControl();
            infoControl.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            infoTabPage.Controls.Add(infoControl);
            infoControl.Dock = DockStyle.Fill;
            infoControl.SearchEvent += InfoControl_SearchEvent;
            infoTabPage.Name = "tabPageInfo";
            infoTabPage.Padding = new System.Windows.Forms.Padding(3);
            infoTabPage.Text = "Details";
            infoTabPage.UseVisualStyleBackColor = true;
            tabControlMain.TabPages.Add(infoTabPage);
            tabControlMain.SelectedTab = infoTabPage;
            SelectedIndexChanged(selectedLogListControl);
        }

        private void InfoControl_SearchEvent(object sender, SearchEventArgs e)
        {
            tabControlMain.SelectedTab = tabPageSearch;
            searchControlMain.InvokeSearch(e);
        }

        private void InfoControl_DoubleClickTextBox(object sender, EventArgs e)
        {
            LoglineInfoControl lic = sender as LoglineInfoControl;
            selectedLogListControl.SelectIndexVisible(lic.SelectedLine);
        }

        private void listViewLog_DoubleClick(object sender, EventArgs e)
        {
            LogListControl llc = sender as LogListControl;
            TabPage oldPage = infoTabPage;
            AddInfoTabPage(llc.SelectedIndex);

            if (oldPage != infoTabPage && oldPage != null)
            {
                oldPage.Text = $"Line {llc.SelectedIndex:n0}";
                LoglineInfoControl llic = (LoglineInfoControl)oldPage.Controls[0];
                llic.SelectedLine = llc.SelectedIndex;
                llic.DoubleClickTextBox += InfoControl_DoubleClickTextBox;
            }
        }

        private void listViewLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged(((LogListControl)sender));
        }

        private void SelectedIndexChanged(LogListControl listViewLog)
        {
            if (listViewLog.SelectedIndex >= 0)
            {
                int index = listViewLog.SelectedIndex;
                if (infoControl != null)
                {
                    long selectedLine = LoglineObject.InfoTextFromLine(streamingFactory, index, infoControl.InfoTextBox);
                    infoControl.Tag = selectedLine;
                    selectedLogListControl.ShortInfo = $"Line {selectedLine:n0} / {streamingFactory.PositionList.Count - 1:n0}";

                    if (!isFromSearchControl)
                    {
                        this.searchControlMain.SelectedLine = (int)selectedLine;
                        this.searchControlMain.SelectedTimestamp = streamingFactory.PositionList[index].TimeStamp;
                    }
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listViewBasket.Items.Clear();
            Clear();
        }

        private void buttonAddFiles_Click(object sender, EventArgs e)
        {
            AddFiles();
        }

        private void AddFiles()
        {
            DialogResult result = openFileDialogBasket.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (String file in openFileDialogBasket.FileNames)
                {
                    AddToBasket(file);
                }

                if (checkBoxIndexAfterAdd.Checked)
                    CreateIndex();
            }
        }

        bool isFromSearchControl = false;
        private void searchControlMain_JumpToLine(object sender, EventArgs e)
        {
            isFromSearchControl = true;
            int index = (int)sender;
            if (index <= 0)
                index = 0;

            JumpToLine(index);
        }

        private void JumpToLine(int index)
        {
            searchControlMain.SelectedTimestamp = streamingFactory.PositionList[index].TimeStamp;
            selectedLogListControl.SelectIndexVisible(index);
            isFromSearchControl = false;
        }

        private void searchControlMain_JumpToTimeStamp(object sender, EventArgs e)
        {
            isFromSearchControl = true;
            DateTime timestamp = ((SearchControl)sender).SelectedTimestamp;
            int index = streamingFactory.PositionList.FindIndex(x => x.TimeStamp > timestamp);
            if (index <= 0)
                index = 0;

            searchControlMain.SelectedLine = index;
            selectedLogListControl.SelectIndexVisible(index);

            isFromSearchControl = false;
        }

        private void Llc_DoubleClickListView(object sender, EventArgs e)
        {
            LogListControl lc = sender as LogListControl;
            LogPos lp = lc.SelectedLogPos;
            if (lp != null)
                JumpToLine((int)lp.Order);
        }

        private void tabControlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                for (int i = 0; i < tabControlMain.TabPages.Count; ++i)
                {
                    Rectangle r = tabControlMain.GetTabRect(i);
                    if (r.Contains(e.Location))
                    {
                        ContextMenu cm = new ContextMenu();
                        if (tabControlMain.TabPages[i].Name == "tabPageInfo" && tabControlMain.TabPages[i] != infoTabPage)
                        {
                            MenuItem item = new MenuItem("Clear all Info Tabs");
                            item.Tag = "tabPageInfo";
                            item.Click += tabControlClearTabs_Click;
                            cm.MenuItems.Add(item);
                        }
                        else if (tabControlMain.TabPages[i].Name == "tabPageSearchResult")
                        {
                            MenuItem item = new MenuItem("Clear all Search Result Tabs");
                            item.Tag = "tabPageSearchResult";
                            item.Click += tabControlClearTabs_Click;
                            cm.MenuItems.Add(item);
                        }
                        cm.Show(tabControlMain, e.Location);
                    }
                }
            }
        }

        private void tabControlClearTabs_Click(object sender, EventArgs e)
        {
            String clearString = ((MenuItem)sender).Tag as String;
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name == clearString && tp != infoTabPage)
                {
                    tabControlMain.TabPages.Remove(tp);
                    tp.Dispose();
                }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F)
            {
                if (e.Control)
                {
                    tabControlMain.SelectedTab = tabPageSearch;
                    searchControlMain.FocusSearch();
                }
                else
                {
                    logListControlMain.Follow = !logListControlMain.Follow;
                    foreach (TabPage tp in tabControlMain.TabPages)
                        if (tp.Name == "tabPageSearchResult" && tp != infoTabPage)
                            ((LogListControl)tp.Controls[0]).Follow = logListControlMain.Follow;
                }
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (e.Control)
                {
                    Clear();
                    CreateIndex();
                }
                else if (!logListControlMain.Follow)
                {
                    logListControlMain.Reload();
                    foreach (TabPage tp in tabControlMain.TabPages)
                        if (tp.Name == "tabPageSearchResult" && tp != infoTabPage)
                            ((LogListControl)tp.Controls[0]).Reload();
                }
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (streamingFactory != null)
                    streamingFactory.ReleaseFile();
            }
        }

        public static void FlashTrayIcon()
        {
            var sft = new SafeFlashTrayIcon(currentMainForm.FlashTray);
            currentMainForm.Invoke(sft);
        }

        private void FlashTray()
        {
            FlashWindow.Tray(this, 1);
        }

        private void logListControlMain_SelectedIndexChangedListView(object sender, EventArgs e)
        {
            SelectedIndexChanged((LogListControl)sender);
        }

        private void listViewBasket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control)
                    AddFilesFromSelectedFolder();
                else
                    CreateIndex();
            }
        }

        private void createIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateIndex();
        }

        private void addFilesFromThisFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFilesFromSelectedFolder();
        }

        private void AddFilesFromSelectedFolder()
        {
            if (listViewBasket.SelectedItems.Count == 0)
                return;

            openFileDialogBasket.InitialDirectory = Path.GetDirectoryName(((String)listViewBasket.SelectedItems[0].Tag));
            AddFiles();
        }

        private void listViewBasket_DoubleClick(object sender, EventArgs e)
        {
            if (listViewBasket.SelectedItems.Count == 1 && listViewBasket.SelectedItems[0].Checked)
                CreateIndex();
        }
    }
}
