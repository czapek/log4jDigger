namespace log4jDigger.Controls
{
    partial class JavaProcessControl
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
            this.listViewJavaProcesses = new System.Windows.Forms.ListView();
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPrivateBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderThreads = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCpu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderArgs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripProcess = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startArgumentsToClipbardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCpu = new System.Windows.Forms.Timer(this.components);
            this.enviromentToClipoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWithJConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWithVisualVMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewJavaProcesses
            // 
            this.listViewJavaProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderId,
            this.columnHeaderName,
            this.columnHeaderStartTime,
            this.columnHeaderPrivateBytes,
            this.columnHeaderThreads,
            this.columnHeaderCpu,
            this.columnHeaderArgs});
            this.listViewJavaProcesses.ContextMenuStrip = this.contextMenuStripProcess;
            this.listViewJavaProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewJavaProcesses.FullRowSelect = true;
            this.listViewJavaProcesses.GridLines = true;
            this.listViewJavaProcesses.HideSelection = false;
            this.listViewJavaProcesses.Location = new System.Drawing.Point(0, 0);
            this.listViewJavaProcesses.Name = "listViewJavaProcesses";
            this.listViewJavaProcesses.ShowItemToolTips = true;
            this.listViewJavaProcesses.Size = new System.Drawing.Size(1111, 303);
            this.listViewJavaProcesses.TabIndex = 0;
            this.listViewJavaProcesses.UseCompatibleStateImageBehavior = false;
            this.listViewJavaProcesses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "Id";
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 100;
            // 
            // columnHeaderStartTime
            // 
            this.columnHeaderStartTime.Text = "Start Time";
            this.columnHeaderStartTime.Width = 100;
            // 
            // columnHeaderPrivateBytes
            // 
            this.columnHeaderPrivateBytes.Text = "Private Bytes";
            this.columnHeaderPrivateBytes.Width = 100;
            // 
            // columnHeaderThreads
            // 
            this.columnHeaderThreads.Text = "Threads";
            // 
            // columnHeaderCpu
            // 
            this.columnHeaderCpu.Text = "CPU";
            // 
            // columnHeaderArgs
            // 
            this.columnHeaderArgs.Text = "Start Arguments";
            this.columnHeaderArgs.Width = 600;
            // 
            // contextMenuStripProcess
            // 
            this.contextMenuStripProcess.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startArgumentsToClipbardToolStripMenuItem,
            this.enviromentToClipoardToolStripMenuItem,
            this.openWithJConsoleToolStripMenuItem,
            this.openWithVisualVMToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStripProcess.Name = "contextMenuStripProcess";
            this.contextMenuStripProcess.Size = new System.Drawing.Size(198, 158);
            this.contextMenuStripProcess.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripProcess_Opening);
            // 
            // startArgumentsToClipbardToolStripMenuItem
            // 
            this.startArgumentsToClipbardToolStripMenuItem.Name = "startArgumentsToClipbardToolStripMenuItem";
            this.startArgumentsToClipbardToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.startArgumentsToClipbardToolStripMenuItem.Text = "Arguments to Clipbard";
            this.startArgumentsToClipbardToolStripMenuItem.Click += new System.EventHandler(this.startArgumentsToClipbardToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // timerCpu
            // 
            this.timerCpu.Interval = 1000;
            this.timerCpu.Tick += new System.EventHandler(this.timerCpu_Tick);
            // 
            // enviromentToClipoardToolStripMenuItem
            // 
            this.enviromentToClipoardToolStripMenuItem.Name = "enviromentToClipoardToolStripMenuItem";
            this.enviromentToClipoardToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.enviromentToClipoardToolStripMenuItem.Text = "Enviroment to Clipoard";
            this.enviromentToClipoardToolStripMenuItem.Click += new System.EventHandler(this.enviromentToClipoardToolStripMenuItem_Click);
            // 
            // openWithJConsoleToolStripMenuItem
            // 
            this.openWithJConsoleToolStripMenuItem.Name = "openWithJConsoleToolStripMenuItem";
            this.openWithJConsoleToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openWithJConsoleToolStripMenuItem.Text = "Open with JConsole";
            this.openWithJConsoleToolStripMenuItem.Click += new System.EventHandler(this.openWithJConsoleToolStripMenuItem_Click);
            // 
            // openWithVisualVMToolStripMenuItem
            // 
            this.openWithVisualVMToolStripMenuItem.Name = "openWithVisualVMToolStripMenuItem";
            this.openWithVisualVMToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openWithVisualVMToolStripMenuItem.Text = "Run VisualVM";
            this.openWithVisualVMToolStripMenuItem.Click += new System.EventHandler(this.openWithVisualVMToolStripMenuItem_Click);
            // 
            // JavaProcessControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewJavaProcesses);
            this.Name = "JavaProcessControl";
            this.Size = new System.Drawing.Size(1111, 303);
            this.contextMenuStripProcess.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewJavaProcesses;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderArgs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripProcess;
        private System.Windows.Forms.ToolStripMenuItem startArgumentsToClipbardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderStartTime;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderPrivateBytes;
        private System.Windows.Forms.ColumnHeader columnHeaderThreads;
        private System.Windows.Forms.ColumnHeader columnHeaderCpu;
        private System.Windows.Forms.Timer timerCpu;
        private System.Windows.Forms.ToolStripMenuItem enviromentToClipoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWithJConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWithVisualVMToolStripMenuItem;
    }
}
