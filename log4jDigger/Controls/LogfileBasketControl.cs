using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace log4jDigger.Controls
{
    public partial class LogfileBasketControl : UserControl
    {
        public event EventHandler CreateIndexEvent;
        public event EventHandler ClearEvent;
        private const int BasketStateCol = 3;

        public LogfileBasketControl()
        {
            InitializeComponent();

            textBoxTimestamp.Tag = DateTime.Now.AddHours(-1);
            textBoxTimestamp.Text = $"{DateTime.Now.AddHours(-1):yyyy-MM-dd_HH}";

            String mainLogDir = LogUtils.FindLatestLogDir();
            if (mainLogDir != null)
                openFileDialogBasket.InitialDirectory = mainLogDir;
        }

        public bool IsIndexing
        {
            set
            {
                if (value)
                    buttonCreateIndex.Text = "Abort";
                else
                    buttonCreateIndex.Text = "Create index";
            }

            get
            {
                return buttonCreateIndex.Text == "Create index";
            }
        }

        public void AddFiles()
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

        private void AddFilesFromSelectedFolder()
        {
            if (listViewBasket.SelectedItems.Count == 0)
                return;

            openFileDialogBasket.InitialDirectory = Path.GetDirectoryName(((String)listViewBasket.SelectedItems[0].Tag));
            AddFiles();
        }

        public List<String> GetFilelistForIndexing()
        {    
            if (listViewBasket.CheckedItems.Count == 0)
                return null;

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

            return fileList;
        }

        public void DisableForIndex()
        {
            buttonCreateIndex.Text = "Abort";
            buttonAddFiles.Enabled = false;
            buttonClear.Enabled = false;
        }

        public void EnableForIndex()
        {
            buttonCreateIndex.Text = "Create index";
            buttonAddFiles.Enabled = true;
            buttonClear.Enabled = true;
        }

        public bool AddToBasket(String file)
        {
            return AddToBasket(file, true, false);
        }

        public bool AddToBasket(String file, bool isChecked, bool force)
        {
            if (String.IsNullOrWhiteSpace(file))
                return false;

            FileInfo fi = new FileInfo(file);
            if (force && fi.Exists && fi.Length > 0)
            {
                foreach (ListViewItem item in listViewBasket.Items)
                    item.Checked = false;
            }

            ListViewItem existingItem = listViewBasket.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == file);
            if (existingItem != null)
            {
                if (force)
                {
                    existingItem.Checked = true;
                    return true;
                }
                return false;
            }


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

                item.Checked = isChecked || force;
                listViewBasket.Items.Add(item);
                return true;
            }
            return false;
        }

        public void ProgressChanged(int percentage)
        {
            int index = (int)(percentage / 100.0);
            int rel = percentage - (index * 100);
            if (index < listViewBasket.CheckedItems.Count)
            {
                listViewBasket.CheckedItems[index].SubItems[BasketStateCol].Text = "indexing ... " + rel + "%";
                listViewBasket.CheckedItems[index].EnsureVisible();
            }

            if (index > 0 && listViewBasket.CheckedItems.Count >= index)
                listViewBasket.CheckedItems[index - 1].SubItems[BasketStateCol].Text = "done";
        }

        private void CreateIndex()
        {
            if (CreateIndexEvent != null)
                CreateIndexEvent.Invoke(this, EventArgs.Empty);
        }

        private void Clear()
        {
            if (ClearEvent != null)
                ClearEvent.Invoke(this, EventArgs.Empty);
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

        private void buttonAddTimestampPrevDay_Click(object sender, EventArgs e)
        {
            AddTimstampLookFor(-24);
        }

        private void buttonAddTimestampPrevHour_Click(object sender, EventArgs e)
        {
            AddTimstampLookFor(-1);
        }

        private void buttonAddTimestampNextHour_Click(object sender, EventArgs e)
        {
            AddTimstampLookFor(1);
        }

        private void buttonAddTimestampNextDay_Click(object sender, EventArgs e)
        {
            AddTimstampLookFor(24);
        }

        private void AddTimstampLookFor(int hours)
        {
            DateTime date = ((DateTime)textBoxTimestamp.Tag).AddHours(hours);
            textBoxTimestamp.Tag = date;
            textBoxTimestamp.Text = $"{date:yyyy-MM-dd_HH}";
        }

        private void buttonAddTimestamp_Click(object sender, EventArgs e)
        {
            List<FileInfo> logfiles = LogUtils.FindRolloverLogfiles((DateTime)textBoxTimestamp.Tag);

            foreach (FileInfo file in logfiles)
            {
                AddToBasket(file.FullName);
            }

            if (checkBoxIndexAfterAdd.Checked)
                CreateIndex();
        }

        private void buttonCreateIndex_Click(object sender, EventArgs e)
        {
            CreateIndex();
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

        private void listViewBasket_DoubleClick(object sender, EventArgs e)
        {
            if (listViewBasket.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in listViewBasket.CheckedItems)
                    item.Checked = false;
                listViewBasket.SelectedItems[0].Checked = true;
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
    }
}
