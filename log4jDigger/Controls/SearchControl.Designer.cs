namespace log4jDigger.Controls
{
    partial class SearchControl
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
            this.numericUpDownLineNumber = new System.Windows.Forms.NumericUpDown();
            this.labelJump = new System.Windows.Forms.Label();
            this.labelTimestamp = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.numericUpDownDurationFrom = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDurationTo = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timeControlJump = new log4jDigger.Controls.TimeControl();
            this.checkBoxRegex = new System.Windows.Forms.CheckBox();
            this.checkBoxIgnoreCase = new System.Windows.Forms.CheckBox();
            this.progressBarSearch = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDurationFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDurationTo)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownLineNumber
            // 
            this.numericUpDownLineNumber.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLineNumber.Location = new System.Drawing.Point(112, 5);
            this.numericUpDownLineNumber.Name = "numericUpDownLineNumber";
            this.numericUpDownLineNumber.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownLineNumber.TabIndex = 1;
            this.numericUpDownLineNumber.ThousandsSeparator = true;
            this.numericUpDownLineNumber.ValueChanged += new System.EventHandler(this.numericUpDownLineNumber_ValueChanged);
            // 
            // labelJump
            // 
            this.labelJump.AutoSize = true;
            this.labelJump.Location = new System.Drawing.Point(8, 5);
            this.labelJump.Name = "labelJump";
            this.labelJump.Size = new System.Drawing.Size(67, 13);
            this.labelJump.TabIndex = 4;
            this.labelJump.Text = "Jump to Line";
            // 
            // labelTimestamp
            // 
            this.labelTimestamp.AutoSize = true;
            this.labelTimestamp.Location = new System.Drawing.Point(8, 32);
            this.labelTimestamp.Name = "labelTimestamp";
            this.labelTimestamp.Size = new System.Drawing.Size(98, 13);
            this.labelTimestamp.TabIndex = 4;
            this.labelTimestamp.Text = "Jump to Timestamp";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(982, 6);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(95, 23);
            this.buttonSearch.TabIndex = 500;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(698, 8);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(278, 20);
            this.textBoxSearch.TabIndex = 100;
            this.textBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // numericUpDownDurationFrom
            // 
            this.numericUpDownDurationFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDurationFrom.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDurationFrom.Location = new System.Drawing.Point(768, 32);
            this.numericUpDownDurationFrom.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownDurationFrom.Name = "numericUpDownDurationFrom";
            this.numericUpDownDurationFrom.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownDurationFrom.TabIndex = 110;
            this.numericUpDownDurationFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            // 
            // numericUpDownDurationTo
            // 
            this.numericUpDownDurationTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDurationTo.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDurationTo.Location = new System.Drawing.Point(879, 32);
            this.numericUpDownDurationTo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownDurationTo.Name = "numericUpDownDurationTo";
            this.numericUpDownDurationTo.Size = new System.Drawing.Size(74, 20);
            this.numericUpDownDurationTo.TabIndex = 120;
            this.numericUpDownDurationTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(695, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Duration from";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(956, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(845, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "ms to";
            // 
            // timeControlJump
            // 
            this.timeControlJump.Location = new System.Drawing.Point(112, 29);
            this.timeControlJump.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.timeControlJump.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.timeControlJump.Name = "timeControlJump";
            this.timeControlJump.Size = new System.Drawing.Size(358, 20);
            this.timeControlJump.TabIndex = 2;
            this.timeControlJump.Value = new System.DateTime(2020, 5, 25, 16, 53, 22, 0);
            this.timeControlJump.ValueChanged += new System.EventHandler(this.timeControlJump_ValueChanged);
            // 
            // checkBoxRegex
            // 
            this.checkBoxRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxRegex.AutoSize = true;
            this.checkBoxRegex.Location = new System.Drawing.Point(698, 82);
            this.checkBoxRegex.Name = "checkBoxRegex";
            this.checkBoxRegex.Size = new System.Drawing.Size(79, 17);
            this.checkBoxRegex.TabIndex = 140;
            this.checkBoxRegex.Text = "Use Regex";
            this.checkBoxRegex.UseVisualStyleBackColor = true;
            // 
            // checkBoxIgnoreCase
            // 
            this.checkBoxIgnoreCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIgnoreCase.AutoSize = true;
            this.checkBoxIgnoreCase.Checked = true;
            this.checkBoxIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIgnoreCase.Location = new System.Drawing.Point(698, 105);
            this.checkBoxIgnoreCase.Name = "checkBoxIgnoreCase";
            this.checkBoxIgnoreCase.Size = new System.Drawing.Size(83, 17);
            this.checkBoxIgnoreCase.TabIndex = 141;
            this.checkBoxIgnoreCase.Text = "Ignore Case";
            this.checkBoxIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // progressBarSearch
            // 
            this.progressBarSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSearch.Location = new System.Drawing.Point(0, 173);
            this.progressBarSearch.Name = "progressBarSearch";
            this.progressBarSearch.Size = new System.Drawing.Size(1181, 14);
            this.progressBarSearch.TabIndex = 501;
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBarSearch);
            this.Controls.Add(this.checkBoxIgnoreCase);
            this.Controls.Add(this.checkBoxRegex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownDurationTo);
            this.Controls.Add(this.numericUpDownDurationFrom);
            this.Controls.Add(this.timeControlJump);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelTimestamp);
            this.Controls.Add(this.labelJump);
            this.Controls.Add(this.numericUpDownLineNumber);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(1181, 187);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDurationFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDurationTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown numericUpDownLineNumber;
        private System.Windows.Forms.Label labelJump;
        private System.Windows.Forms.Label labelTimestamp;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private TimeControl timeControlJump;
        private System.Windows.Forms.NumericUpDown numericUpDownDurationFrom;
        private System.Windows.Forms.NumericUpDown numericUpDownDurationTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxRegex;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCase;
        private System.Windows.Forms.ProgressBar progressBarSearch;
    }
}
