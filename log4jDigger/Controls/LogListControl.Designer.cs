namespace log4jDigger.Controls
{
    partial class LogListControl
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
            if (this.streamingFactory != null)
                this.streamingFactory.RemoveSearchResult(searchEventArgs);
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
            this.contextMenuStripListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.jumpToPreviousLineInOfThreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jumpToNextLineInOfThreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelShortLeftInfo = new System.Windows.Forms.Label();
            this.labelShortRightInfo = new System.Windows.Forms.Label();
            this.timerRepaint = new System.Windows.Forms.Timer(this.components);
            this.labelLongCenterInfo = new System.Windows.Forms.Label();
            this.listViewLog = new log4jDigger.Controls.FlickerFreeListView();
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderThread = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLogSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripListView
            // 
            this.contextMenuStripListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jumpToPreviousLineInOfThreadToolStripMenuItem,
            this.jumpToNextLineInOfThreadToolStripMenuItem});
            this.contextMenuStripListView.Name = "contextMenuStripListView";
            this.contextMenuStripListView.Size = new System.Drawing.Size(305, 48);
            // 
            // jumpToPreviousLineInOfThreadToolStripMenuItem
            // 
            this.jumpToPreviousLineInOfThreadToolStripMenuItem.Name = "jumpToPreviousLineInOfThreadToolStripMenuItem";
            this.jumpToPreviousLineInOfThreadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.jumpToPreviousLineInOfThreadToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.jumpToPreviousLineInOfThreadToolStripMenuItem.Text = "Jump to previous line in of Thread";
            this.jumpToPreviousLineInOfThreadToolStripMenuItem.Click += new System.EventHandler(this.jumpToPreviousLineInOfThreadToolStripMenuItem_Click);
            // 
            // jumpToNextLineInOfThreadToolStripMenuItem
            // 
            this.jumpToNextLineInOfThreadToolStripMenuItem.Name = "jumpToNextLineInOfThreadToolStripMenuItem";
            this.jumpToNextLineInOfThreadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.jumpToNextLineInOfThreadToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.jumpToNextLineInOfThreadToolStripMenuItem.Text = "Jump to next line in of Thread";
            this.jumpToNextLineInOfThreadToolStripMenuItem.Click += new System.EventHandler(this.jumpToNextLineInOfThreadToolStripMenuItem_Click);
            // 
            // labelShortLeftInfo
            // 
            this.labelShortLeftInfo.AutoSize = true;
            this.labelShortLeftInfo.Location = new System.Drawing.Point(3, 0);
            this.labelShortLeftInfo.Name = "labelShortLeftInfo";
            this.labelShortLeftInfo.Size = new System.Drawing.Size(68, 13);
            this.labelShortLeftInfo.TabIndex = 2;
            this.labelShortLeftInfo.Text = "ShortLeftInfo";
            // 
            // labelShortRightInfo
            // 
            this.labelShortRightInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelShortRightInfo.AutoSize = true;
            this.labelShortRightInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShortRightInfo.Location = new System.Drawing.Point(1038, 1);
            this.labelShortRightInfo.Name = "labelShortRightInfo";
            this.labelShortRightInfo.Size = new System.Drawing.Size(89, 13);
            this.labelShortRightInfo.TabIndex = 2;
            this.labelShortRightInfo.Text = "ShortRightInfo";
            // 
            // timerRepaint
            // 
            this.timerRepaint.Interval = 60000;
            this.timerRepaint.Tick += new System.EventHandler(this.timerRepaint_Tick);
            // 
            // labelLongCenterInfo
            // 
            this.labelLongCenterInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLongCenterInfo.AutoEllipsis = true;
            this.labelLongCenterInfo.Location = new System.Drawing.Point(236, 0);
            this.labelLongCenterInfo.Name = "labelLongCenterInfo";
            this.labelLongCenterInfo.Size = new System.Drawing.Size(780, 13);
            this.labelLongCenterInfo.TabIndex = 3;
            this.labelLongCenterInfo.Text = "LongCenterInfo";
            // 
            // listViewLog
            // 
            this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderLevel,
            this.columnHeaderDuration,
            this.columnHeaderClass,
            this.columnHeaderMessage,
            this.columnHeaderThread,
            this.columnHeaderLogSource});
            this.listViewLog.ContextMenuStrip = this.contextMenuStripListView;
            this.listViewLog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewLog.FullRowSelect = true;
            this.listViewLog.HideSelection = false;
            this.listViewLog.Location = new System.Drawing.Point(0, 16);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(1176, 554);
            this.listViewLog.TabIndex = 1;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            this.listViewLog.VirtualMode = true;
            this.listViewLog.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewLog_RetrieveVirtualItem);
            this.listViewLog.SelectedIndexChanged += new System.EventHandler(this.listViewLog_SelectedIndexChanged);
            this.listViewLog.DoubleClick += new System.EventHandler(this.listViewLog_DoubleClick);
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Timestamp";
            this.columnHeaderTime.Width = 200;
            // 
            // columnHeaderLevel
            // 
            this.columnHeaderLevel.Text = "Level";
            // 
            // columnHeaderDuration
            // 
            this.columnHeaderDuration.Text = "Duration";
            this.columnHeaderDuration.Width = 100;
            // 
            // columnHeaderClass
            // 
            this.columnHeaderClass.Text = "Classname";
            this.columnHeaderClass.Width = 280;
            // 
            // columnHeaderMessage
            // 
            this.columnHeaderMessage.Text = "Logmessage";
            this.columnHeaderMessage.Width = 800;
            // 
            // columnHeaderThread
            // 
            this.columnHeaderThread.Text = "Threadname";
            this.columnHeaderThread.Width = 230;
            // 
            // columnHeaderLogSource
            // 
            this.columnHeaderLogSource.Text = "Logsource";
            this.columnHeaderLogSource.Width = 200;
            // 
            // LogListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelLongCenterInfo);
            this.Controls.Add(this.labelShortRightInfo);
            this.Controls.Add(this.labelShortLeftInfo);
            this.Controls.Add(this.listViewLog);
            this.Name = "LogListControl";
            this.Size = new System.Drawing.Size(1176, 570);
            this.contextMenuStripListView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlickerFreeListView listViewLog;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.ColumnHeader columnHeaderLevel;
        private System.Windows.Forms.ColumnHeader columnHeaderDuration;
        private System.Windows.Forms.ColumnHeader columnHeaderClass;
        private System.Windows.Forms.ColumnHeader columnHeaderMessage;
        private System.Windows.Forms.ColumnHeader columnHeaderThread;
        private System.Windows.Forms.ColumnHeader columnHeaderLogSource;
        private System.Windows.Forms.Label labelShortLeftInfo;
        private System.Windows.Forms.Label labelShortRightInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListView;
        private System.Windows.Forms.ToolStripMenuItem jumpToPreviousLineInOfThreadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpToNextLineInOfThreadToolStripMenuItem;
        private System.Windows.Forms.Timer timerRepaint;
        private System.Windows.Forms.Label labelLongCenterInfo;
    }
}
