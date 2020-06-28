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
        bool wasFollowing = false;
        bool reloadSearch = false;

        public String[] Args;

        public MainForm()
        {
            InitializeComponent();

            selectedLogListControl = logListControlMain;
            this.Size = new Size(1600, 800);
            streamingFactory = new StreamingFactory();
            selectedLogListControl.SetStreamingFactory(streamingFactory);
            streamingFactory.IsInconsistent += StreamingFactory_IsInConsistent;
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
                        logfileBasketControl.AddToBasket(File.ReadAllLines(Program.WorkaroundForInitialInstancePath));

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
            Screen screen = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));
            this.Location = new Point(screen.Bounds.X + 30, screen.Bounds.Y + 30);
            this.Size = new Size(Math.Min(screen.Bounds.Width - 100, 1800), Math.Min(screen.Bounds.Height - 100, 1000));
            if (Args.Length > 0)
            {
                ParseArgs(Args);
            }
            else
            {
                foreach (FileInfo info in LogUtils.FindLatesLogfiles())
                    logfileBasketControl.AddToBasket(info.FullName, false, false);
            }
        }

        public void OnMessageReceived(MessageEventArgs e)
        {
            OnMessageReceivedInvoker invoker = delegate (MessageEventArgs eventArgs)
            {
                string[] args = eventArgs.Message as string[];
                BringApplicationToFront();
                ParseArgs(args);
            };

            if (InvokeRequired)
                Invoke(invoker, e);
            else
                invoker(e);
        }

        private void BringApplicationToFront()
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;

            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }

        private void ParseArgs(string[] args)
        {
            if (args.Length == 2 && args[0] == "W")
            {
                if (logfileBasketControl.AddToBasket(args[1]))
                {
                    tabControlMain.SelectedTab = tabPageBasket;
                    timerNewLogFilesAdded.Enabled = true;
                }
            }
            else if (args.Length == 1 && logfileBasketControl.AddToBasket(args[0], true, true))
            {
                Clear();
                RemoveTabs();
                CreateIndex();
            }
        }

        public void OnNewInstanceCreated(EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (workerIndex.IsBusy)
                workerIndex.CancelAsync();
            streamingFactory.Dispose();
        }

        private void DisableForIndex()
        {

            this.Cursor = Cursors.WaitCursor;
            logfileBasketControl.DisableForIndex();
            logListControlMain.Clear();
            logListControlMain.Enabled = false;
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name != "tabPageBasket")
                    tp.Controls[0].Enabled = false;
        }

        private void EnableForIndex()
        {
            this.Cursor = Cursors.Default;
            logfileBasketControl.EnableForIndex();
            logListControlMain.Enabled = true;
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name != "tabPageBasket")
                    tp.Controls[0].Enabled = true;
        }

        private void Clear()
        {
            logListControlMain.Follow = false;
            selectedLogListControl.Clear();
            if (workerIndex.IsBusy)
                workerIndex.CancelAsync();

            logListControlMain.VirtualListSize = 0;
            streamingFactory.Clear();
        }

        private void RemoveTabs()
        {
            tabControlMain.TabPages.Clear();
            tabControlMain.TabPages.Add(tabPageBasket);
            tabControlMain.TabPages.Add(tabPageSearch);
            tabControlMain.TabPages.Add(tabPageJavaProcess);
            tabControlMain.TabPages.Add(tabPageOptions);
        }

        private void StreamingFactory_IsInConsistent(object sender, EventArgs e)
        {
            var d = new SafeAfterInConsistent(AfterInConsistent);
            this.Invoke(d);
        }

        private void AfterInConsistent()
        {
            wasFollowing = logListControlMain.Follow;
            reloadSearch = true;
            Clear();
            CreateIndex();
        }

        private void CreateIndex()
        {
            if (workerIndex.IsBusy)
                return;

            logListControlMain.Follow = false;

            List<String> fileList = logfileBasketControl.GetFilelistForIndexing();

            if (fileList != null && fileList.Count > 0)
            {
                DisableForIndex();
                RemoveTabs();
                workerIndex.RunWorkerAsync(fileList);                
            }
        }

        private void WorkerIndex_DoWork(object sender, DoWorkEventArgs e)
        {
            List<String> fileList = (List<String>)e.Argument;
            int progress = 0;
            streamingFactory.Clear(fileList);
            foreach (String file in fileList)
            {
                workerIndex.ReportProgress(progress);
                if (workerIndex.CancellationPending)
                    break;

                streamingFactory.AddNewFile(file, workerIndex, progress);
                progress += 100;
            }
            workerIndex.ReportProgress(progress);
            streamingFactory.OrderPositionList();
        }

        private void WorkerIndex_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            selectedLogListControl.ShortLeftInfo = $"{streamingFactory.PositionList.Count - 1:n0} lines";
            searchControlMain.StreamingFactory = streamingFactory;
            selectedLogListControl.ResetSetStreamingFactory();

            EnableForIndex();
            AddInfoTabPage(-1);

            logListControlMain.Follow = wasFollowing;
            wasFollowing = false;

            if (reloadSearch)
                foreach (TabPage tp in tabControlMain.TabPages)
                    if (tp.Name == "tabPageSearchResult")
                        ((LogListControl)tp.Controls[0]).Reload();

            reloadSearch = false;
        }

        private void WorkerIndex_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            logfileBasketControl.ProgressChanged(e.ProgressPercentage);
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
                        && ((LogListControl)tp.Controls[0]).LongCenterInfo == e.ToString())
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
            streamingFactory.RemoveSearchResult(sea);
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
                llc.SetStreamingFactory(streamingFactory);
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
                llc.SetSearchResult(sea);
            }

            logListControlMain.Enabled = true;
            foreach (TabPage tp in tabControlMain.TabPages)
                tp.Controls[0].Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void AddInfoTabPage(int pos)
        {
            foreach (TabPage tp in tabControlMain.TabPages)
                if (tp.Name == "tabPageInfo" && (pos == -1 || ((LoglineInfoControl)tp.Controls[0]).SelectedLine == pos))
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


        private void SelectedIndexChanged(LogListControl listViewLog)
        {
            if (listViewLog.SelectedIndex >= 0)
            {
                int index = listViewLog.SelectedIndex;
                if (infoControl != null)
                {
                    long selectedLine = LoglineObject.InfoTextFromLine(streamingFactory, index, infoControl.InfoTextBox);
                    infoControl.Tag = selectedLine;

                    if (!isFromSearchControl)
                    {
                        this.searchControlMain.SelectedLine = (int)selectedLine;
                        this.searchControlMain.SelectedTimestamp = streamingFactory.PositionList[index].TimeStamp;
                    }
                }
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
            searchControlMain.ResetSearch();
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
                        if (tp.Name == "tabPageSearchResult")
                            ((LogListControl)tp.Controls[0]).Follow = logListControlMain.Follow;
                }
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (e.Control)
                {
                    wasFollowing = logListControlMain.Follow;
                    reloadSearch = true;
                    Clear();
                    CreateIndex();
                }
                else if (!logListControlMain.Follow)
                {
                    logListControlMain.Reload();

                    foreach (TabPage tp in tabControlMain.TabPages)
                        if (tp.Name == "tabPageSearchResult")
                            ((LogListControl)tp.Controls[0]).Reload();
                }
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
            if (optionsControl.IsBringToFront)
                BringApplicationToFront();
        }

        private void logListControlMain_SelectedIndexChangedListView(object sender, EventArgs e)
        {
            SelectedIndexChanged((LogListControl)sender);
        }

        private void optionsControl_AllowRollowerCheckedChanged(object sender, EventArgs e)
        {
            if (streamingFactory != null)
            {
                streamingFactory.EnableHourlyUnlock = optionsControl.IsAllowRollower;
            }
        }

        private void logfileBasketControl_CreateIndexEvent(object sender, EventArgs e)
        {
            if (workerIndex.IsBusy)
            {
                workerIndex.CancelAsync();
                logfileBasketControl.IsIndexing = false;
            }
            else
            {                
                CreateIndex();
            }
        }

        private void logfileBasketControl_ClearEvent(object sender, EventArgs e)
        {
            Clear();
            RemoveTabs();
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabPageJavaProcess)
            {
                javaProcessControl.ScanProcesses();
            }
            else
            {
                javaProcessControl.Disable();
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            logfileBasketControl.AddToBasket(files);
        }
    }
}
