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
    public partial class TimeControl : UserControl
    {
        public event EventHandler ValueChanged;
        private bool setValues = false;

        public TimeControl()
        {
            InitializeComponent();
        }

        public DateTime MaxDate { set { dateTimePickerControl.MaxDate = value; } get { return dateTimePickerControl.MaxDate; } }
        public DateTime MinDate { set { dateTimePickerControl.MinDate = value; } get { return dateTimePickerControl.MinDate; } }

        public DateTime Value
        {
            set
            {
                if (value == DateTime.MinValue)
                    return;

                setValues = true;
                dateTimePickerControl.Value = value;
                TimeSpan time = value - value.Date;
                numericUpDownHour.Value = time.Hours;
                numericUpDownMinute.Value = time.Minutes;
                numericUpDownSecond.Value = time.Seconds;
                setValues = false;
            }

            get
            {
                return dateTimePickerControl.Value.Date + new TimeSpan(0, (int)numericUpDownHour.Value, (int)numericUpDownMinute.Value, (int)numericUpDownSecond.Value);
            }
        }

        bool dontSetHour = false;
        private void numericUpDownHour_ValueChanged(object sender, EventArgs e)
        {
            if (dontSetHour || setValues)
                return;

            if (numericUpDownHour.Value < 0)
            {
                dontSetHour = true;
                numericUpDownHour.Value = 23;
                dontSetHour = false;
                dateTimePickerControl.Value = dateTimePickerControl.Value.AddDays(-1);
            }
            else if (numericUpDownHour.Value > 23)
            {
                dontSetHour = true;
                numericUpDownHour.Value = 0;
                dontSetHour = false;
                dateTimePickerControl.Value = dateTimePickerControl.Value.AddDays(1);
            }
            LimitDate();
        }

        bool dontSetMinute = false;
        private void numericUpDownMinute_ValueChanged(object sender, EventArgs e)
        {
            if (dontSetMinute || setValues)
                return;

            if (numericUpDownMinute.Value < 0)
            {
                dontSetMinute = true;
                dontSetHour = true;
                numericUpDownMinute.Value = 59;
                numericUpDownHour.Value--;
                dontSetMinute = false;
                dontSetHour = false;
            }
            else if (numericUpDownMinute.Value > 59)
            {
                dontSetMinute = true;
                dontSetHour = true;
                numericUpDownMinute.Value = 0;
                numericUpDownHour.Value++;
                dontSetMinute = false;
                dontSetHour = false;
            }
            LimitDate();
        }

        bool dontSetSecond = false;
        private void numericUpDownSecond_ValueChanged(object sender, EventArgs e)
        {
            if (dontSetSecond || setValues)
                return;

            if (numericUpDownSecond.Value < 0)
            {
                dontSetMinute = true;
                dontSetSecond = true;
                numericUpDownSecond.Value = 59;
                numericUpDownMinute.Value--;
                dontSetMinute = false;
                dontSetSecond = false;
            }
            else if (numericUpDownSecond.Value > 59)
            {
                dontSetMinute = true;
                dontSetSecond = true;
                numericUpDownSecond.Value = 0;
                numericUpDownMinute.Value++;
                dontSetMinute = false;
                dontSetSecond = false;
            }
            LimitDate();
        }

        private void dateTimePickerControl_ValueChanged(object sender, EventArgs e)
        {
            if (setValues)
                return;

            LimitDate();
        }

        private void LimitDate()
        {
            if (setValues)
                return;
            TimeSpan ts = new TimeSpan(0, (int)numericUpDownHour.Value, (int)numericUpDownMinute.Value, (int)numericUpDownSecond.Value);
            DateTime d = dateTimePickerControl.Value.Date + ts;

            if (d < dateTimePickerControl.MinDate)
                Value = dateTimePickerControl.MinDate;
            else if (d > dateTimePickerControl.MaxDate)
                Value = dateTimePickerControl.MaxDate;

            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
