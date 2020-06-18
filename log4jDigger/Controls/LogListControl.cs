using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace log4jDigger.Controls
{
    public partial class LogListControl : UserControl
    {
        public event EventHandler DoubleClickListView;
        public event EventHandler SelectedIndexChangedListView;
        private SearchEventArgs searchEventArgs;
        private StreamingFactory streamingFactory;
        private List<LogPos> positionList;
        private delegate void SafeSetPositionList(List<LogPos> pl);

        public void SetStreamingFactory(StreamingFactory sf)
        {
            streamingFactory = sf;
            SetPositionList(streamingFactory.PositionList);
            streamingFactory.NewPositions += StreamingFactory_NewPositions;

            contextMenuStripListView.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem item = new ToolStripMenuItem("Toggle Follow       F");
            item.Click += Item_Click;
            contextMenuStripListView.Items.Add(item);

            ToolStripMenuItem itemF5 = new ToolStripMenuItem("Reload       F5");
            itemF5.Click += ItemF5_Click;
            contextMenuStripListView.Items.Add(itemF5);
        }

        public void SetStreamingFactory(StreamingFactory sf, SearchEventArgs sea)
        {
            LongInfo = sea.ToString();
            searchEventArgs = sea;
            streamingFactory = sf;
            SetPositionList(streamingFactory.GetSearchResult(sea));
            streamingFactory.NewPositions += StreamingFactory_NewPositions;
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
            System.Diagnostics.Debug.WriteLine("SetPositionList in LogListControl");
            positionList = pl;
            if (positionList != null)
            {
                VirtualListSize = positionList.Count();
                if (follow)
                    Follow = true;
                else if (positionList.Count() > 0)
                    listViewLog.Items[0].Selected = true;
            }

        }

        public String ShortInfo
        {
            set
            {
                labelShortInfo.Text = value;
            }

            get
            {
                return labelShortInfo.Text;
            }
        }

        public String LongInfo
        {
            set
            {
                labelLongInfo.Text = value;
            }

            get
            {
                return labelLongInfo.Text;
            }
        }

        public LogPos SelectedLogPos
        {
            get
            {
                if (listViewLog.SelectedIndices.Count > 0)
                {
                    int index = listViewLog.SelectedIndices[0];
                    if (positionList != null && positionList.Count > index)
                        return positionList[index];
                }
                return null;
            }
        }

        bool follow = false;
        public bool Follow
        {
            set
            {
                if (streamingFactory != null)
                    streamingFactory.EnablePolling = value;

                follow = value;
                if (follow && listViewLog.VirtualListSize > 0)
                    SelectIndexVisible((int)(listViewLog.VirtualListSize - 1));

                if (searchEventArgs == null)
                    this.LongInfo = follow ? "Follow on" : "Follow off";
            }

            get
            {
                return follow;
            }
        }

        public void Clear()
        {
            VirtualListSize = 0;
            LongInfo = "";
            ShortInfo = "";
        }

        public long VirtualListSize
        {
            set
            {
                if (listViewLog.VirtualListSize != value)
                    listViewLog.SetVirtualListSize((int)value);
            }

            get
            {
                return (int)listViewLog.VirtualListSize;
            }
        }

        public void Reload()
        {
            if (streamingFactory != null)
            {
                streamingFactory.Poll();
                SetPositionList(streamingFactory.PositionList);
                SelectIndexVisible((int)(VirtualListSize - 1));
            }
        }

        public void SelectIndexVisible(int index)
        {
            this.listViewLog.SelectedIndices.Clear();
            this.listViewLog.Items[index].Selected = true;
            this.listViewLog.Items[index].Focused = true;
            this.listViewLog.Items[index].EnsureVisible();
        }

        public int SelectedIndex
        {
            get
            {
                if (this.listViewLog.SelectedIndices.Count > 0)
                    return this.listViewLog.SelectedIndices[0];
                else
                    return -1;
            }
        }

        public LogListControl()
        {
            InitializeComponent();
        }

        private void listViewLog_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClickListView != null)
            {
                DoubleClickListView.Invoke(this, EventArgs.Empty);
            }
        }

        private void listViewLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndex >= 0)
            {
                if (searchEventArgs != null)
                    ShortInfo = $"Line {SelectedIndex:n0} / {VirtualListSize - 1:n0} ({SelectedLogPos.Order:n0})";
                else
                    ShortInfo = $"Line {SelectedIndex:n0} / {VirtualListSize - 1:n0}";

                if (SelectedIndexChangedListView != null)
                {
                    SelectedIndexChangedListView.Invoke(this, EventArgs.Empty);
                }
            }
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

        private void jumpToPreviousLineInOfThreadToolStripMenuItem_Click(object sender, EventArgs e)
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

        private List<Color> last100Colors = new List<Color>();
        private void timerRepaint_Tick(object sender, EventArgs e)
        {
            if (positionList == null || positionList.Count == 0)
                return;

            if (positionList[positionList.Count - 1].TimeStamp < DateTime.Now.AddDays(-1))
                return;

            int refreshFrom = positionList.Count > 100 ? positionList.Count - 100 : 0;
            if (positionList[refreshFrom].TimeStamp < DateTime.Now.AddDays(-1))
                return;

            if (last100Colors.Count != positionList.Count - refreshFrom)
                UpdateAgeColors(refreshFrom);

            for (int i = refreshFrom; i < positionList.Count; i++)
            {
                if (last100Colors[i - refreshFrom] != GetAgeColor(positionList[i].TimeStamp))
                {
                    listViewLog.RedrawItems(refreshFrom, positionList.Count - 1, false);
                    System.Diagnostics.Debug.WriteLine("refresh age");
                    UpdateAgeColors(refreshFrom);
                    return;
                }
            }

        }

        private void UpdateAgeColors(int refreshFrom)
        {
            if (last100Colors.Count == 100 && positionList.Count - refreshFrom == 100)
            {
                for (int i = refreshFrom; i < positionList.Count; i++)
                    last100Colors[i - refreshFrom] = GetAgeColor(positionList[i].TimeStamp);
            }
            else
            {
                last100Colors.Clear();
                for (int i = refreshFrom; i < positionList.Count; i++)
                    last100Colors.Add(GetAgeColor(positionList[i].TimeStamp));
            }
        }
    }
}
