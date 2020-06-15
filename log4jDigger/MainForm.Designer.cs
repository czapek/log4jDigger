﻿using log4jDigger.Controls;

namespace log4jDigger
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logListControlMain = new log4jDigger.Controls.LogListControl();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageBasket = new System.Windows.Forms.TabPage();
            this.checkBoxIndexAfterAdd = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonAddFiles = new System.Windows.Forms.Button();
            this.buttonCreateIndex = new System.Windows.Forms.Button();
            this.listViewBasket = new System.Windows.Forms.ListView();
            this.columnHeaderLogFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastWriteTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripBasket = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilesFromThisFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.searchControlMain = new log4jDigger.Controls.SearchControl();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.optionsControl = new log4jDigger.Controls.OptionsControl();
            this.openFileDialogBasket = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageBasket.SuspendLayout();
            this.contextMenuStripBasket.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.logListControlMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainer1.Panel2MinSize = 180;
            this.splitContainer1.Size = new System.Drawing.Size(1233, 576);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 1;
            // 
            // logListControlMain
            // 
            this.logListControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logListControlMain.Follow = false;
            this.logListControlMain.Location = new System.Drawing.Point(0, 0);
            this.logListControlMain.LongInfo = "";
            this.logListControlMain.Name = "logListControlMain";
            this.logListControlMain.ShortInfo = "";
            this.logListControlMain.Size = new System.Drawing.Size(1233, 392);
            this.logListControlMain.TabIndex = 1;
            this.logListControlMain.VirtualListSize = ((long)(0));
            this.logListControlMain.DoubleClickListView += new System.EventHandler(this.listViewLog_DoubleClick);
            this.logListControlMain.SelectedIndexChangedListView += new System.EventHandler(this.logListControlMain_SelectedIndexChangedListView);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageBasket);
            this.tabControlMain.Controls.Add(this.tabPageSearch);
            this.tabControlMain.Controls.Add(this.tabPageOptions);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1233, 180);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControlMain_MouseUp);
            // 
            // tabPageBasket
            // 
            this.tabPageBasket.Controls.Add(this.checkBoxIndexAfterAdd);
            this.tabPageBasket.Controls.Add(this.buttonClear);
            this.tabPageBasket.Controls.Add(this.buttonAddFiles);
            this.tabPageBasket.Controls.Add(this.buttonCreateIndex);
            this.tabPageBasket.Controls.Add(this.listViewBasket);
            this.tabPageBasket.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasket.Name = "tabPageBasket";
            this.tabPageBasket.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasket.Size = new System.Drawing.Size(1225, 154);
            this.tabPageBasket.TabIndex = 0;
            this.tabPageBasket.Text = "Basket";
            this.tabPageBasket.UseVisualStyleBackColor = true;
            // 
            // checkBoxIndexAfterAdd
            // 
            this.checkBoxIndexAfterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIndexAfterAdd.AutoSize = true;
            this.checkBoxIndexAfterAdd.Checked = true;
            this.checkBoxIndexAfterAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIndexAfterAdd.Location = new System.Drawing.Point(1153, 61);
            this.checkBoxIndexAfterAdd.Name = "checkBoxIndexAfterAdd";
            this.checkBoxIndexAfterAdd.Size = new System.Drawing.Size(68, 17);
            this.checkBoxIndexAfterAdd.TabIndex = 3;
            this.checkBoxIndexAfterAdd.Text = "after add";
            this.checkBoxIndexAfterAdd.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(1131, 125);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(88, 23);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "Clear List";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddFiles.Location = new System.Drawing.Point(1131, 6);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(88, 23);
            this.buttonAddFiles.TabIndex = 1;
            this.buttonAddFiles.Text = "Add Files";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // buttonCreateIndex
            // 
            this.buttonCreateIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateIndex.Location = new System.Drawing.Point(1131, 35);
            this.buttonCreateIndex.Name = "buttonCreateIndex";
            this.buttonCreateIndex.Size = new System.Drawing.Size(88, 23);
            this.buttonCreateIndex.TabIndex = 2;
            this.buttonCreateIndex.Text = "Create Index";
            this.buttonCreateIndex.UseVisualStyleBackColor = true;
            this.buttonCreateIndex.Click += new System.EventHandler(this.buttonCreateIndex_Click);
            // 
            // listViewBasket
            // 
            this.listViewBasket.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBasket.CheckBoxes = true;
            this.listViewBasket.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLogFile,
            this.columnHeaderSize,
            this.columnHeaderLastWriteTime,
            this.columnHeaderState});
            this.listViewBasket.ContextMenuStrip = this.contextMenuStripBasket;
            this.listViewBasket.GridLines = true;
            this.listViewBasket.HideSelection = false;
            this.listViewBasket.Location = new System.Drawing.Point(0, 0);
            this.listViewBasket.Name = "listViewBasket";
            this.listViewBasket.Size = new System.Drawing.Size(1125, 154);
            this.listViewBasket.TabIndex = 0;
            this.listViewBasket.UseCompatibleStateImageBehavior = false;
            this.listViewBasket.View = System.Windows.Forms.View.Details;
            this.listViewBasket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewBasket_KeyDown);
            // 
            // columnHeaderLogFile
            // 
            this.columnHeaderLogFile.Text = "Logfiles";
            this.columnHeaderLogFile.Width = 600;
            // 
            // columnHeaderSize
            // 
            this.columnHeaderSize.Text = "Size";
            this.columnHeaderSize.Width = 150;
            // 
            // columnHeaderLastWriteTime
            // 
            this.columnHeaderLastWriteTime.Text = "Last WriteTime";
            this.columnHeaderLastWriteTime.Width = 120;
            // 
            // columnHeaderState
            // 
            this.columnHeaderState.Text = "State";
            this.columnHeaderState.Width = 120;
            // 
            // contextMenuStripBasket
            // 
            this.contextMenuStripBasket.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createIndexToolStripMenuItem,
            this.addFilesFromThisFolderToolStripMenuItem});
            this.contextMenuStripBasket.Name = "contextMenuStripBasket";
            this.contextMenuStripBasket.Size = new System.Drawing.Size(280, 70);
            // 
            // createIndexToolStripMenuItem
            // 
            this.createIndexToolStripMenuItem.Name = "createIndexToolStripMenuItem";
            this.createIndexToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.createIndexToolStripMenuItem.Text = "Create Index         Enter";
            this.createIndexToolStripMenuItem.Click += new System.EventHandler(this.createIndexToolStripMenuItem_Click);
            // 
            // addFilesFromThisFolderToolStripMenuItem
            // 
            this.addFilesFromThisFolderToolStripMenuItem.Name = "addFilesFromThisFolderToolStripMenuItem";
            this.addFilesFromThisFolderToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.addFilesFromThisFolderToolStripMenuItem.Text = "Add Files from this Folder    ctrl + Enter";
            this.addFilesFromThisFolderToolStripMenuItem.Click += new System.EventHandler(this.addFilesFromThisFolderToolStripMenuItem_Click);
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.searchControlMain);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Size = new System.Drawing.Size(1225, 154);
            this.tabPageSearch.TabIndex = 1;
            this.tabPageSearch.Text = "Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // searchControlMain
            // 
            this.searchControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchControlMain.Location = new System.Drawing.Point(0, 0);
            this.searchControlMain.Name = "searchControlMain";
            this.searchControlMain.SelectedTimestamp = new System.DateTime(2020, 5, 25, 16, 53, 22, 0);
            this.searchControlMain.Size = new System.Drawing.Size(1225, 154);
            this.searchControlMain.TabIndex = 0;
            this.searchControlMain.JumpToLine += new System.EventHandler(this.searchControlMain_JumpToLine);
            this.searchControlMain.JumpToTimeStamp += new System.EventHandler(this.searchControlMain_JumpToTimeStamp);
            this.searchControlMain.SearchEvent += new System.EventHandler<log4jDigger.SearchEventArgs>(this.searchControlMain_SearchEvent);
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.optionsControl);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Size = new System.Drawing.Size(1225, 154);
            this.tabPageOptions.TabIndex = 2;
            this.tabPageOptions.Text = "Options";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // optionsControl
            // 
            this.optionsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsControl.Location = new System.Drawing.Point(0, 0);
            this.optionsControl.Name = "optionsControl";
            this.optionsControl.Size = new System.Drawing.Size(1225, 154);
            this.optionsControl.TabIndex = 0;
            // 
            // openFileDialogBasket
            // 
            this.openFileDialogBasket.AddExtension = false;
            this.openFileDialogBasket.Multiselect = true;
            this.openFileDialogBasket.Title = "Select Logfiles";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 576);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1200, 550);
            this.Name = "MainForm";
            this.Text = "log4jDigger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageBasket.ResumeLayout(false);
            this.tabPageBasket.PerformLayout();
            this.contextMenuStripBasket.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageBasket;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.Button buttonCreateIndex;
        private System.Windows.Forms.ListView listViewBasket;
        private System.Windows.Forms.ColumnHeader columnHeaderLogFile;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderState;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.OpenFileDialog openFileDialogBasket;
        private System.Windows.Forms.TabPage tabPageSearch;
        private SearchControl searchControlMain;
        private System.Windows.Forms.TabPage tabPageOptions;
        private OptionsControl optionsControl;
        private LogListControl logListControlMain;
        private System.Windows.Forms.CheckBox checkBoxIndexAfterAdd;
        private System.Windows.Forms.ColumnHeader columnHeaderLastWriteTime;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBasket;
        private System.Windows.Forms.ToolStripMenuItem createIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesFromThisFolderToolStripMenuItem;
    }
}
