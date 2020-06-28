using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace log4jDigger.Controls
{
    public partial class SearchControl : UserControl
    {
        public event EventHandler JumpToLine;
        public event EventHandler JumpToTimeStamp;
        public event EventHandler<SearchEventArgs> SearchEvent;
        private bool isSetValues = false;
        private Timer timerTimeValueChanged;
        private Timer timerNumberValueChanged;
        private delegate void SafeSetControls(List<LogPos> pl);
        public SearchControl()
        {
            InitializeComponent();
            timerTimeValueChanged = new Timer();
            timerTimeValueChanged.Interval = 500;
            timerTimeValueChanged.Enabled = false;
            timerTimeValueChanged.Tick += timerTimeValueChanged_Tick;

            timerNumberValueChanged = new Timer();
            timerNumberValueChanged.Interval = 500;
            timerNumberValueChanged.Enabled = false;
            timerNumberValueChanged.Tick += timerNumberValueChanged_Tick;

            ResetSearch();
        }

        public void FocusSearch()
        {
            textBoxSearch.Focus();
        }

        public StreamingFactory StreamingFactory
        {
            set
            {
                if (value.PositionList.Count == 0)
                    return;

                SetControls(value.PositionList);
                value.NewPositions += Value_NewPositions;

                comboBoxLogSource.Items.Clear();
                comboBoxLogSource.Items.Add(new LogSource() { Servername = "All" });
                comboBoxLogSource.Items.AddRange(value.PositionList.Select(x => x.LogSource).Distinct().OrderBy(x => x.ToString()).ToArray());
                comboBoxLogSource.SelectedIndex = 0;
            }
        }

        private void Value_NewPositions(object sender, EventArgs e)
        {
            var d = new SafeSetControls(SetControls);
            this.Invoke(d, new object[] { ((StreamingFactory)sender).PositionList });
        }

        private void SetControls(List<LogPos> pl)
        {
            System.Diagnostics.Debug.WriteLine("set controls in SearchControl");
            isSetValues = true;
            numericUpDownLineNumber.Maximum = pl.Count - 1;
            numericUpDownLineNumber.Value = 0;
            numericUpDownLineNumber.Increment = (int)Math.Pow(10, (int)Math.Log10(pl.Count)) / 100;

            timeControlJump.MinDate = pl[0].TimeStamp;
            timeControlJump.MaxDate = pl[pl.Count - 1].TimeStamp;
            timeControlJump.Value = pl[0].TimeStamp;
            isSetValues = false;
        }

        public DateTime SelectedTimestamp
        {
            set
            {
                if (value >= timeControlJump.MinDate && value <= timeControlJump.MaxDate)
                {
                    isSetValues = true;
                    timeControlJump.Value = value;
                    isSetValues = false;
                }
            }

            get
            {
                return timeControlJump.Value;
            }
        }

        public int SelectedLine
        {
            set
            {
                if (value >= numericUpDownLineNumber.Minimum && value <= numericUpDownLineNumber.Maximum)
                {
                    isSetValues = true;
                    numericUpDownLineNumber.Value = value;
                    isSetValues = false;
                }
            }
        }

        public void SetProgress(int percent)
        {
            progressBarSearch.Value = percent;

            if (percent == 100)
            {
                buttonSearch.Text = "Search";
                foreach (Control c in this.Controls)
                    c.Enabled = true;
            }
        }

        private void numericUpDownLineNumber_ValueChanged(object sender, EventArgs e)
        {
            if (isSetValues)
                return;

            timerNumberValueChanged.Enabled = true;
        }

        private void timerNumberValueChanged_Tick(object sender, EventArgs e)
        {
            if (JumpToLine != null && !timerTimeValueChanged.Enabled)
                JumpToLine.Invoke((int)numericUpDownLineNumber.Value, EventArgs.Empty);

            timerNumberValueChanged.Enabled = false;
        }

        private void timeControlJump_ValueChanged(object sender, EventArgs e)
        {
            if (isSetValues)
                return;

            timerTimeValueChanged.Enabled = true;
        }

        private void timerTimeValueChanged_Tick(object sender, EventArgs e)
        {
            if (JumpToTimeStamp != null && !timerNumberValueChanged.Enabled)
                JumpToTimeStamp.Invoke(this, EventArgs.Empty);

            timerTimeValueChanged.Enabled = false;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (SearchEvent != null && (textBoxSearch.Text.Trim().Length > 0
                            || (numericUpDownDurationFrom.Value <= numericUpDownDurationTo.Value && numericUpDownDurationTo.Value > 0)
                            || (numericUpDownDurationFrom.Value > 0 && numericUpDownDurationTo.Value == 0)
                            || checkBoxStackTrace.Checked
                            || comboBoxLogSource.SelectedIndex > 0
                            || !checkBoxTrace.Checked
                            || !checkBoxDebug.Checked
                            || !checkBoxInfo.Checked
                            || !checkBoxWarn.Checked
                            || !checkBoxError.Checked
                            || !checkBoxFatal.Checked))
            {
                SearchEventArgs args = new SearchEventArgs()
                {
                    SearchText = textBoxSearch.Text.Trim(),
                    DurationFrom = (int)numericUpDownDurationFrom.Value,
                    DurationTo = (int)numericUpDownDurationTo.Value,
                    IgnoreCase = checkBoxIgnoreCase.Checked,
                    UseRegex = checkBoxRegex.Checked,
                    OnlyLinesWithStackTrace = checkBoxStackTrace.Checked,
                    LevelTrace = checkBoxTrace.Checked,
                    LevelDebug = checkBoxDebug.Checked,
                    LevelInfo = checkBoxInfo.Checked,
                    LevelWarn = checkBoxWarn.Checked,
                    LevelError = checkBoxError.Checked,
                    LevelFatal = checkBoxFatal.Checked,
                    LogSource = comboBoxLogSource.SelectedIndex > 0 ? (LogSource)comboBoxLogSource.SelectedItem : null
                };

                InvokeSearch(args);
            }
        }

        public void ResetSearch()
        {
            SetFromSearchArgs(new SearchEventArgs());
        }

        public void InvokeSearch(SearchEventArgs args)
        {
            SetFromSearchArgs(args);

            buttonSearch.Text = "Abort";
            foreach (Control c in this.Controls)
                if (c != buttonSearch && c != progressBarSearch)
                    c.Enabled = false;

            SearchEvent.Invoke(this, args);
        }

        private void SetFromSearchArgs(SearchEventArgs args)
        {
            numericUpDownDurationFrom.Value = args.DurationFrom;
            numericUpDownDurationTo.Value = args.DurationTo;
            textBoxSearch.Text = args.SearchText;
            checkBoxIgnoreCase.Checked = args.IgnoreCase;
            checkBoxRegex.Checked = args.UseRegex;
            checkBoxStackTrace.Checked = args.OnlyLinesWithStackTrace;
            checkBoxTrace.Checked = args.LevelTrace;
            checkBoxDebug.Checked = args.LevelDebug;
            checkBoxInfo.Checked = args.LevelInfo;
            checkBoxWarn.Checked = args.LevelWarn;
            checkBoxError.Checked = args.LevelError;
            checkBoxFatal.Checked = args.LevelFatal;
            if (args.LogSource == null && comboBoxLogSource.Items.Count > 0)
                comboBoxLogSource.SelectedIndex = 0;
            else
                comboBoxLogSource.SelectedItem = args.LogSource;
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                Search();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }
    }
}
