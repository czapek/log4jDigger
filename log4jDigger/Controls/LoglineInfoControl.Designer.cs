namespace log4jDigger.Controls
{
    partial class LoglineInfoControl
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
            this.richTextBoxInfo = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripInfo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchForSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxInfo
            // 
            this.richTextBoxInfo.BackColor = System.Drawing.Color.White;
            this.richTextBoxInfo.ContextMenuStrip = this.contextMenuStripInfo;
            this.richTextBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxInfo.Name = "richTextBoxInfo";
            this.richTextBoxInfo.ReadOnly = true;
            this.richTextBoxInfo.Size = new System.Drawing.Size(701, 358);
            this.richTextBoxInfo.TabIndex = 0;
            this.richTextBoxInfo.Text = "";
            this.richTextBoxInfo.DoubleClick += new System.EventHandler(this.richTextBoxInfo_DoubleClick);
            // 
            // contextMenuStripInfo
            // 
            this.contextMenuStripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchForSelectionToolStripMenuItem,
            this.copySelectionToolStripMenuItem});
            this.contextMenuStripInfo.Name = "contextMenuStripInfo";
            this.contextMenuStripInfo.Size = new System.Drawing.Size(221, 70);
            // 
            // searchForSelectionToolStripMenuItem
            // 
            this.searchForSelectionToolStripMenuItem.Name = "searchForSelectionToolStripMenuItem";
            this.searchForSelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.searchForSelectionToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.searchForSelectionToolStripMenuItem.Text = "Search for Selection";
            this.searchForSelectionToolStripMenuItem.Click += new System.EventHandler(this.searchForSelectionToolStripMenuItem_Click);
            // 
            // copySelectionToolStripMenuItem
            // 
            this.copySelectionToolStripMenuItem.Name = "copySelectionToolStripMenuItem";
            this.copySelectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copySelectionToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.copySelectionToolStripMenuItem.Text = "Copy Selection";
            this.copySelectionToolStripMenuItem.Click += new System.EventHandler(this.copySelectionToolStripMenuItem_Click);
            // 
            // LoglineInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBoxInfo);
            this.Name = "LoglineInfoControl";
            this.Size = new System.Drawing.Size(701, 358);
            this.contextMenuStripInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInfo;
        private System.Windows.Forms.ToolStripMenuItem searchForSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectionToolStripMenuItem;
    }
}
