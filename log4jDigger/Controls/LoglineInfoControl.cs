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
    public partial class LoglineInfoControl : UserControl
    {
        public event EventHandler DoubleClickTextBox;
        public event EventHandler<SearchEventArgs> SearchEvent;

        public LoglineInfoControl()
        {
            InitializeComponent();
        }

        public int SelectedLine { get; set; }

        public RichTextBox InfoTextBox
        {
            get
            {
                return this.richTextBoxInfo;
            }
        }

        private void richTextBoxInfo_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClickTextBox != null)
                DoubleClickTextBox.Invoke(this, e);
        }

        private void searchForSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SearchEvent != null && !String.IsNullOrWhiteSpace(richTextBoxInfo.SelectedText.Trim()))
            {
                SearchEvent.Invoke(this, new SearchEventArgs()
                {
                    SearchText = richTextBoxInfo.SelectedText.Trim(),
                    DurationFrom = 0,
                    DurationTo = 0,
                    IgnoreCase = false,
                    UseRegex = false
                });
            }
        }

        private void copySelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(richTextBoxInfo.SelectedText.Trim()))
            {
                Clipboard.SetText(richTextBoxInfo.SelectedText.Trim());
            }
        }
    }
}
