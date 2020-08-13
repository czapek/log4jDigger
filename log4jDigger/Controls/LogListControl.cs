using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace log4jDigger.Controls
{
    public partial class LogListControl : UserControl
    {
        public event EventHandler<ListViewControlEventArgs> DoubleClickListView;
        public event EventHandler ClickListView;
        public event EventHandler SelectedIndexChangedListView;
        private SearchEventArgs searchEventArgs;
        private StreamingFactory streamingFactory;
        private List<LogPos> positionList;
        private delegate void SafeSetPositionList(List<LogPos> pl);
        private Object lockObject = new Object();
        List<LogPos> backupList;

        public LogListControl()
        {
            InitializeComponent();
        }


        public void SetStreamingFactory(StreamingFactory sf, SearchEventArgs sea)
        {
            streamingFactory = sf;
            streamingFactory.NewPositions += StreamingFactory_NewPositions;

            contextMenuStripListView.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem bookmarkMenuItem = new ToolStripMenuItem("Bookmark                               Ctrl+B");
            bookmarkMenuItem.Click += new System.EventHandler(this.bookmarkMenuItem_Click);
            contextMenuStripListView.Items.Add(bookmarkMenuItem);

            ToolStripMenuItem detailsDoubleClickToolStripMenuItem = new ToolStripMenuItem("Show Details                             DoubleClick");
            detailsDoubleClickToolStripMenuItem.Click += DetailsDoubleClickToolStripMenuItem_Click;
            contextMenuStripListView.Items.Add(detailsDoubleClickToolStripMenuItem);

            if (sea == null)
            {
                contextMenuStripListView.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem item = new ToolStripMenuItem("Toggle Follow       F");
                item.Click += Item_Click;
                contextMenuStripListView.Items.Add(item);

                ToolStripMenuItem itemF5 = new ToolStripMenuItem("Reload       F5");
                itemF5.Click += ItemF5_Click;
                contextMenuStripListView.Items.Add(itemF5);
            }
            else
            {
                LongCenterInfo = sea.ToString();
                searchEventArgs = sea;
                SetPositionList(streamingFactory.GetSearchResult(sea));
            }
        }

        public void ResetSetStreamingFactory()
        {
            LongCenterInfo = "Digging in: " + streamingFactory.ToString();
            SetPositionList(streamingFactory.PositionList);
        }

        private void ItemF5_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void Item_Click(object sender, EventArgs e)
        {
            Follow = !Follow;
        }

        private void StreamingFactory_NewPositions(object sender, EventArgs e)
        {
            if (Follow)
            {
                List<LogPos> positionList = null;
                var d = new SafeSetPositionList(SetPositionList);
                if (searchEventArgs != null)
                {
                    positionList = streamingFactory.GetSearchResult(searchEventArgs);
                }
                else
                {
                    positionList = streamingFactory.PositionList;
                }

                if (positionList.Count > VirtualListSize)
                    this.Invoke(d, new object[] { positionList });
            }
        }

        private void SetPositionList(List<LogPos> pl)
        {
            lock (lockObject)
            {
                System.Diagnostics.Debug.WriteLine("SetPositionList in LogListControl");
                positionList = pl;
                if (positionList != null)
                {
                    VirtualListSize = positionList.Count();

                    if (positionList.Count == 0)
                        return;

                    if (follow && listViewLog.VirtualListSize > 0)
                        SelectIndexVisible((int)(listViewLog.VirtualListSize - 1));
                    else if (positionList.Count() > 0)
                        listViewLog.Items[0].Selected = true;

                    timerRepaint.Enabled = true;
                }
            }
        }

        public String ShortLeftInfo
        {
            set
            {
                labelShortLeftInfo.Text = value;
            }

            get
            {
                return labelShortLeftInfo.Text;
            }
        }

        public String LongCenterInfo
        {
            set
            {
                labelLongCenterInfo.Text = value;
            }

            get
            {
                return labelLongCenterInfo.Text;
            }
        }

        public String ShortRightInfo
        {
            set
            {
                labelShortRightInfo.Text = value;
            }

            get
            {
                return labelShortRightInfo.Text;
            }
        }

        public LogPos SelectedLogPos
        {
            get
            {
                lock (listViewLog)
                {
                    if (listViewLog.SelectedIndices.Count > 0)
                    {
                        int index = listViewLog.SelectedIndices[0];
                        if (positionList != null && positionList.Count > index)
                            return positionList[index];
                    }
                }
                return null;
            }
        }

        bool follow = false;
        public bool Follow
        {
            set
            {
                lock (lockObject)
                {
                    if (value)
                        OrderByTimestamp();

                    if (streamingFactory != null)
                        streamingFactory.EnablePolling = value;

                    follow = value;
                    if (follow && listViewLog.VirtualListSize > 0)
                        SelectIndexVisible((int)(listViewLog.VirtualListSize - 1));

                    this.ShortRightInfo = follow ? "Follow on" : "Follow off";
                }
            }

            get
            {
                return follow;
            }
        }

        public void Clear()
        {
            if (streamingFactory == null)
                return;

            VirtualListSize = 0;
            ShortRightInfo = "";
            ShortLeftInfo = "";
        }

        public long VirtualListSize
        {
            set
            {
                lock (lockObject)
                {
                    if (listViewLog != null && listViewLog.VirtualListSize != value)
                        listViewLog.SetVirtualListSize((int)value);
                }
            }

            get
            {
                lock (lockObject)
                {
                    return (int)listViewLog.VirtualListSize;
                }
            }
        }

        public void Reload()
        {
            if (streamingFactory != null)
            {
                lock (lockObject)
                {
                    OrderByTimestamp();
                    List<LogPos> positionList = null;
                    streamingFactory.Poll();
                    if (searchEventArgs != null)
                    {
                        positionList = streamingFactory.GetSearchResult(searchEventArgs);
                    }
                    else
                    {
                        positionList = streamingFactory.PositionList;
                    }

                    SetPositionList(positionList);
                    SelectIndexVisible((int)(VirtualListSize - 1));
                }
            }
        }

        public void SelectIndexVisible(int index)
        {
            if (index < 0 || index >= VirtualListSize)
                return;
            lock (lockObject)
            {
                try
                {
                    this.listViewLog.SelectedIndices.Clear();
                    this.listViewLog.Items[index].Selected = true;
                    this.listViewLog.Items[index].Focused = true;
                    this.listViewLog.Items[index].EnsureVisible();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("SelectIndexVisible:" + ex.Message);
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                lock (lockObject)
                {
                    if (this.listViewLog.SelectedIndices.Count > 0)
                        return this.listViewLog.SelectedIndices[0];
                    else
                        return -1;
                }
            }
        }


        private void ListViewLog_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.B && e.Modifiers == Keys.Control)
            {
                DoubleClickListView.Invoke(this, new ListViewControlEventArgs() { Bookmark = true, SearchEventArgs = searchEventArgs });
            }
        }


        private void listViewLog_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClickListView != null && backupList == null)
            {
                DoubleClickListView.Invoke(this, new ListViewControlEventArgs() { Bookmark = false, SearchEventArgs = searchEventArgs });
            }
        }

        private void ListViewLog_Click(object sender, System.EventArgs e)
        {
            if (ClickListView != null)
            {
                ClickListView.Invoke(this, EventArgs.Empty);
            }
        }

        private void bookmarkMenuItem_Click(object sender, EventArgs e)
        {
            if (DoubleClickListView != null && backupList == null)
            {
                DoubleClickListView.Invoke(this, new ListViewControlEventArgs() { Bookmark = true, SearchEventArgs = searchEventArgs });
            }
        }

        private void DetailsDoubleClickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DoubleClickListView != null && backupList == null)
            {
                DoubleClickListView.Invoke(this, new ListViewControlEventArgs() { Bookmark = false, SearchEventArgs = searchEventArgs });
            }
        }

        private void listViewLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndex >= 0)
            {
                lock (lockObject)
                {
                    int logLinesPerMinute = CalculateLogLinesPerMinute();
                    String llpm = logLinesPerMinute > 0 ? $" - [{logLinesPerMinute:n0} LPM]" : "";

                    if (searchEventArgs != null)
                        ShortLeftInfo = $"Line {SelectedIndex:n0} / {VirtualListSize - 1:n0} ({SelectedLogPos.Order:n0})" + llpm;
                    else
                        ShortLeftInfo = $"Line {SelectedIndex:n0} / {VirtualListSize - 1:n0}" + llpm;


                    if (SelectedIndexChangedListView != null && backupList == null)
                    {
                        SelectedIndexChangedListView.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        private int CalculateLogLinesPerMinute()
        {
            DateTime end = positionList[SelectedIndex].TimeStamp;
            DateTime start = end;
            int step = 10;
            while (SelectedIndex - step > 0)
            {
                start = positionList[SelectedIndex - step].TimeStamp;
                if ((end - start).TotalMinutes > 1.0)
                    break;

                step *= 10;
            }

            int logLinesPerMinute = 0;
            double totalMinutes = (end - start).TotalMinutes;
            if (totalMinutes >= 1.0)
            {
                logLinesPerMinute = (int)((double)step / totalMinutes);
            }

            return logLinesPerMinute;
        }

        private void listViewLog_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            LogPos logPos = positionList[e.ItemIndex];
            String line = LoglineObject.ReadLine(logPos);

            ListViewItem lvi = new ListViewItem();
            lvi.BackColor = GetAgeColor(logPos.TimeStamp);
            lvi.UseItemStyleForSubItems = false;
            LoglineObject loglineObject = LoglineObject.CreateLoglineObject(line, logPos);
            lvi.Text = loglineObject.Timestamp;

            ListViewItem.ListViewSubItem lvsuLevel = new ListViewItem.ListViewSubItem();
            lvsuLevel.Text = loglineObject.Level;
            lvsuLevel.BackColor = loglineObject.GetLevelBackColor();
            lvsuLevel.ForeColor = loglineObject.GetLevelFrontColor();
            lvi.SubItems.Add(lvsuLevel);

            ListViewItem.ListViewSubItem lvsuDuration = new ListViewItem.ListViewSubItem();
            if (loglineObject.Duration.HasValue)
            {
                lvsuDuration.Text = $"{loglineObject.Duration.Value:n0} ms";
                if (loglineObject.Duration.Value > 1000)
                    lvsuDuration.BackColor = loglineObject.Duration.Value > 5000 ? Color.LightCoral : Color.LightYellow;
            }

            lvi.SubItems.Add(lvsuDuration);
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = loglineObject.ClassnameShort });
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = loglineObject.Message });
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = loglineObject.Threadname });
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = logPos.LogSource.ToString() });

            if (logPos.LoglineType == LoglineType.CHILD_LINE)
            {
                foreach (String kv in LogUtils.LineMarkerFont)
                {
                    if (line.Contains(kv))
                    {
                        lvi.SubItems[4].BackColor = Color.LightGreen;
                        lvi.SubItems[4].Font = new Font(listViewLog.Font, FontStyle.Bold);
                    }

                }
            }

            e.Item = lvi;
        }

        private LoglineObject GetLoglineObject(int index)
        {
            LogPos logPos = positionList[index];
            String line = LoglineObject.ReadLine(logPos);
            return LoglineObject.CreateLoglineObject(line, logPos);
        }

        LoglineObject refObject;
        private void jumpToNextLineInOfThreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                if (listViewLog.SelectedIndices.Count > 0)
                {
                    int index = listViewLog.SelectedIndices[0];

                    if (refObject.LogPos != null && refObject.LogPos.Order == index)
                        return;

                    this.Cursor = Cursors.WaitCursor;
                    refObject = GetLoglineObject(index);

                    while (index < positionList.Count - 1)
                    {
                        index++;
                        LoglineObject nextObject = GetLoglineObject(index);

                        if (nextObject.Threadname == refObject.Threadname)
                        {
                            SelectIndexVisible(index);
                            break;
                        }

                        if ((nextObject.LogPos.TimeStamp - refObject.LogPos.TimeStamp).TotalMinutes > 60)
                            break;
                    }
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void jumpToPreviousLineInOfThreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                if (listViewLog.SelectedIndices.Count > 0)
                {
                    int index = listViewLog.SelectedIndices[0];
                    if (refObject.LogPos != null && refObject.LogPos.Order == index)
                        return;

                    this.Cursor = Cursors.WaitCursor;
                    refObject = GetLoglineObject(index);

                    while (index > 0)
                    {
                        index--;
                        LoglineObject nextObject = GetLoglineObject(index);

                        if (nextObject.Threadname == refObject.Threadname)
                        {
                            SelectIndexVisible(index);
                            break;
                        }

                        if ((refObject.LogPos.TimeStamp - nextObject.LogPos.TimeStamp).TotalMinutes > 60)
                            break;
                    }
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private Color GetAgeColor(DateTime timestamp)
        {
            double ageInSeconds = (DateTime.Now - timestamp).TotalSeconds;
            if (ageInSeconds < 5)
                return Color.LimeGreen;
            else if (ageInSeconds < 60)
                return Color.Cyan;
            else if (ageInSeconds < 600)
                return Color.Lavender;
            else if (ageInSeconds < 3600)
                return Color.LightCyan;
            else if (ageInSeconds < 36000)
                return Color.WhiteSmoke;

            return Color.White;
        }

        private void timerRepaint_Tick(object sender, EventArgs e)
        {
            if (follow)
                return;

            lock (lockObject)
            {
                timerRepaint.Enabled = false;

                if (positionList == null || positionList.Count == 0)
                    return;

                if (positionList[positionList.Count - 1].TimeStamp < DateTime.Now.AddDays(-1))
                    return;

                int refreshFrom = positionList.Count > 100 ? positionList.Count - 100 : 0;
                if (positionList[refreshFrom].TimeStamp < DateTime.Now.AddDays(-1))
                    return;

                //eine Minute nach dem letzten Add zeichnen wir das Ende der liste neu, 
                //damit die Farben des für das Alter der Logline nicht ganz falsch sind
                listViewLog.RedrawItems(refreshFrom, positionList.Count - 1, false);
            }
        }

        private void durationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderByDuration();
        }

        private void timestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderByTimestamp();
        }

        private void OrderByDuration()
        {
            lock (lockObject)
            {
                if (listViewLog.VirtualListSize > 0)
                {
                    if (durationToolStripMenuItem.Checked == true)
                        return;

                    timestampToolStripMenuItem.Checked = false;
                    durationToolStripMenuItem.Checked = true;

                    this.Cursor = Cursors.WaitCursor;
                    Follow = false;
                    backupList = positionList;
                    positionList = positionList.OrderByDescending(x => LoglineObject.GetDurationFromLogPos(LoglineObject.ReadLine(x))).ThenBy(x => x.TimeStamp).ThenBy(x => x.Order).ToList();
                    SelectIndexVisible(0);
                    listViewLog.Refresh();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void OrderByTimestamp()
        {
            lock (lockObject)
            {
                if (timestampToolStripMenuItem.Checked == true)
                    return;

                timestampToolStripMenuItem.Checked = true;
                durationToolStripMenuItem.Checked = false;

                if (listViewLog.VirtualListSize > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    positionList = backupList;
                    backupList = null;
                    SelectIndexVisible(0);
                    listViewLog.Refresh();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void copyLoglinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lock (listViewLog)
            {
                StringBuilder sb = new StringBuilder();
                foreach (int index in listViewLog.SelectedIndices)
                {
                    if (positionList != null && positionList.Count > index)
                    {
                        sb.AppendLine(LoglineObject.ReadLine(positionList[index]));
                    }
                }

                if (sb.Length > 0)
                    Clipboard.SetText(sb.ToString());

            }
        }

        private void bookmarkDoubleClickAltToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
    }

    public class ListViewControlEventArgs : EventArgs
    {
        public bool Bookmark;
        public SearchEventArgs SearchEventArgs;
    }
}
