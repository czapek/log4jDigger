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
            this.columnHeaderArgs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripProcess = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startArgumentsToClipbardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeaderThreads = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPrivateBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCpu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            // columnHeaderArgs
            // 
            this.columnHeaderArgs.Text = "Start Arguments";
            this.columnHeaderArgs.Width = 600;
            // 
            // contextMenuStripProcess
            // 
            this.contextMenuStripProcess.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startArgumentsToClipbardToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStripProcess.Name = "contextMenuStripProcess";
            this.contextMenuStripProcess.Size = new System.Drawing.Size(196, 70);
            this.contextMenuStripProcess.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripProcess_Opening);
            // 
            // startArgumentsToClipbardToolStripMenuItem
            // 
            this.startArgumentsToClipbardToolStripMenuItem.Name = "startArgumentsToClipbardToolStripMenuItem";
            this.startArgumentsToClipbardToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.startArgumentsToClipbardToolStripMenuItem.Text = "Arguments to Clipbard";
            this.startArgumentsToClipbardToolStripMenuItem.Click += new System.EventHandler(this.startArgumentsToClipbardToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // columnHeaderThreads
            // 
            this.columnHeaderThreads.Text = "Threads";
            // 
            // columnHeaderPrivateBytes
            // 
            this.columnHeaderPrivateBytes.Text = "Private Bytes";
            this.columnHeaderPrivateBytes.Width = 100;
            // 
            // columnHeaderCpu
            // 
            this.columnHeaderCpu.Text = "CPU";
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
    }
}
