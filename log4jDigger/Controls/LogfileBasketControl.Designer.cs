namespace log4jDigger.Controls
{
    partial class LogfileBasketControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxTimestamp = new System.Windows.Forms.TextBox();
            this.buttonAddTimestamp = new System.Windows.Forms.Button();
            this.buttonAddTimestampNextHour = new System.Windows.Forms.Button();
            this.buttonAddTimestampNextDay = new System.Windows.Forms.Button();
            this.buttonAddTimestampPrevHour = new System.Windows.Forms.Button();
            this.buttonAddTimestampPrevDay = new System.Windows.Forms.Button();
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
            this.openFileDialogBasket = new System.Windows.Forms.OpenFileDialog();
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unCheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripBasket.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxTimestamp
            // 
            this.textBoxTimestamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTimestamp.Location = new System.Drawing.Point(902, 206);
            this.textBoxTimestamp.Name = "textBoxTimestamp";
            this.textBoxTimestamp.ReadOnly = true;
            this.textBoxTimestamp.Size = new System.Drawing.Size(85, 20);
            this.textBoxTimestamp.TabIndex = 17;
            this.textBoxTimestamp.Text = "2020-08-12_10";
            // 
            // buttonAddTimestamp
            // 
            this.buttonAddTimestamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTimestamp.Location = new System.Drawing.Point(1067, 204);
            this.buttonAddTimestamp.Name = "buttonAddTimestamp";
            this.buttonAddTimestamp.Size = new System.Drawing.Size(103, 23);
            this.buttonAddTimestamp.TabIndex = 12;
            this.buttonAddTimestamp.Text = "Look for Rollover";
            this.buttonAddTimestamp.UseVisualStyleBackColor = true;
            this.buttonAddTimestamp.Click += new System.EventHandler(this.buttonAddTimestamp_Click);
            // 
            // buttonAddTimestampNextHour
            // 
            this.buttonAddTimestampNextHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTimestampNextHour.Location = new System.Drawing.Point(988, 204);
            this.buttonAddTimestampNextHour.Name = "buttonAddTimestampNextHour";
            this.buttonAddTimestampNextHour.Size = new System.Drawing.Size(37, 23);
            this.buttonAddTimestampNextHour.TabIndex = 13;
            this.buttonAddTimestampNextHour.Text = ">";
            this.buttonAddTimestampNextHour.UseVisualStyleBackColor = true;
            this.buttonAddTimestampNextHour.Click += new System.EventHandler(this.buttonAddTimestampNextHour_Click);
            // 
            // buttonAddTimestampNextDay
            // 
            this.buttonAddTimestampNextDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTimestampNextDay.Location = new System.Drawing.Point(1025, 204);
            this.buttonAddTimestampNextDay.Name = "buttonAddTimestampNextDay";
            this.buttonAddTimestampNextDay.Size = new System.Drawing.Size(37, 23);
            this.buttonAddTimestampNextDay.TabIndex = 14;
            this.buttonAddTimestampNextDay.Text = ">>";
            this.buttonAddTimestampNextDay.UseVisualStyleBackColor = true;
            this.buttonAddTimestampNextDay.Click += new System.EventHandler(this.buttonAddTimestampNextDay_Click);
            // 
            // buttonAddTimestampPrevHour
            // 
            this.buttonAddTimestampPrevHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTimestampPrevHour.Location = new System.Drawing.Point(864, 204);
            this.buttonAddTimestampPrevHour.Name = "buttonAddTimestampPrevHour";
            this.buttonAddTimestampPrevHour.Size = new System.Drawing.Size(37, 23);
            this.buttonAddTimestampPrevHour.TabIndex = 15;
            this.buttonAddTimestampPrevHour.Text = "<";
            this.buttonAddTimestampPrevHour.UseVisualStyleBackColor = true;
            this.buttonAddTimestampPrevHour.Click += new System.EventHandler(this.buttonAddTimestampPrevHour_Click);
            // 
            // buttonAddTimestampPrevDay
            // 
            this.buttonAddTimestampPrevDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTimestampPrevDay.Location = new System.Drawing.Point(827, 204);
            this.buttonAddTimestampPrevDay.Name = "buttonAddTimestampPrevDay";
            this.buttonAddTimestampPrevDay.Size = new System.Drawing.Size(37, 23);
            this.buttonAddTimestampPrevDay.TabIndex = 16;
            this.buttonAddTimestampPrevDay.Text = "<<<";
            this.buttonAddTimestampPrevDay.UseVisualStyleBackColor = true;
            this.buttonAddTimestampPrevDay.Click += new System.EventHandler(this.buttonAddTimestampPrevDay_Click);
            // 
            // checkBoxIndexAfterAdd
            // 
            this.checkBoxIndexAfterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxIndexAfterAdd.AutoSize = true;
            this.checkBoxIndexAfterAdd.Checked = true;
            this.checkBoxIndexAfterAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIndexAfterAdd.Location = new System.Drawing.Point(274, 209);
            this.checkBoxIndexAfterAdd.Name = "checkBoxIndexAfterAdd";
            this.checkBoxIndexAfterAdd.Size = new System.Drawing.Size(96, 17);
            this.checkBoxIndexAfterAdd.TabIndex = 10;
            this.checkBoxIndexAfterAdd.Text = "index after add";
            this.checkBoxIndexAfterAdd.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Location = new System.Drawing.Point(90, 204);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(88, 23);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Clear List";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonAddFiles
            // 
            this.buttonAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddFiles.Location = new System.Drawing.Point(181, 204);
            this.buttonAddFiles.Name = "buttonAddFiles";
            this.buttonAddFiles.Size = new System.Drawing.Size(88, 23);
            this.buttonAddFiles.TabIndex = 8;
            this.buttonAddFiles.Text = "Add Files";
            this.buttonAddFiles.UseVisualStyleBackColor = true;
            this.buttonAddFiles.Click += new System.EventHandler(this.buttonAddFiles_Click);
            // 
            // buttonCreateIndex
            // 
            this.buttonCreateIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateIndex.Location = new System.Drawing.Point(-1, 204);
            this.buttonCreateIndex.Name = "buttonCreateIndex";
            this.buttonCreateIndex.Size = new System.Drawing.Size(88, 23);
            this.buttonCreateIndex.TabIndex = 9;
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
            this.listViewBasket.FullRowSelect = true;
            this.listViewBasket.GridLines = true;
            this.listViewBasket.HideSelection = false;
            this.listViewBasket.Location = new System.Drawing.Point(0, 0);
            this.listViewBasket.Name = "listViewBasket";
            this.listViewBasket.Size = new System.Drawing.Size(1169, 200);
            this.listViewBasket.TabIndex = 7;
            this.listViewBasket.UseCompatibleStateImageBehavior = false;
            this.listViewBasket.View = System.Windows.Forms.View.Details;
            this.listViewBasket.DoubleClick += new System.EventHandler(this.listViewBasket_DoubleClick);
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
            this.addFilesFromThisFolderToolStripMenuItem,
            this.checkAllToolStripMenuItem,
            this.unCheckAllToolStripMenuItem});
            this.contextMenuStripBasket.Name = "contextMenuStripBasket";
            this.contextMenuStripBasket.Size = new System.Drawing.Size(280, 92);
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
            // openFileDialogBasket
            // 
            this.openFileDialogBasket.AddExtension = false;
            this.openFileDialogBasket.Multiselect = true;
            this.openFileDialogBasket.Title = "Select Logfiles";
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.checkAllToolStripMenuItem.Text = "Check all and Index";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // unCheckAllToolStripMenuItem
            // 
            this.unCheckAllToolStripMenuItem.Name = "unCheckAllToolStripMenuItem";
            this.unCheckAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.unCheckAllToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.unCheckAllToolStripMenuItem.Text = "Uncheck all";
            this.unCheckAllToolStripMenuItem.Click += new System.EventHandler(this.unCheckAllToolStripMenuItem_Click);
            // 
            // LogfileBasketControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxTimestamp);
            this.Controls.Add(this.buttonAddTimestamp);
            this.Controls.Add(this.buttonAddTimestampNextHour);
            this.Controls.Add(this.buttonAddTimestampNextDay);
            this.Controls.Add(this.buttonAddTimestampPrevHour);
            this.Controls.Add(this.buttonAddTimestampPrevDay);
            this.Controls.Add(this.checkBoxIndexAfterAdd);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonAddFiles);
            this.Controls.Add(this.buttonCreateIndex);
            this.Controls.Add(this.listViewBasket);
            this.Name = "LogfileBasketControl";
            this.Size = new System.Drawing.Size(1169, 226);
            this.contextMenuStripBasket.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTimestamp;
        private System.Windows.Forms.Button buttonAddTimestamp;
        private System.Windows.Forms.Button buttonAddTimestampNextHour;
        private System.Windows.Forms.Button buttonAddTimestampNextDay;
        private System.Windows.Forms.Button buttonAddTimestampPrevHour;
        private System.Windows.Forms.Button buttonAddTimestampPrevDay;
        private System.Windows.Forms.CheckBox checkBoxIndexAfterAdd;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonAddFiles;
        private System.Windows.Forms.Button buttonCreateIndex;
        private System.Windows.Forms.ListView listViewBasket;
        private System.Windows.Forms.ColumnHeader columnHeaderLogFile;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderLastWriteTime;
        private System.Windows.Forms.ColumnHeader columnHeaderState;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBasket;
        private System.Windows.Forms.ToolStripMenuItem createIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFilesFromThisFolderToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogBasket;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unCheckAllToolStripMenuItem;
    }
}
