namespace log4jDigger.Controls
{
    partial class OptionsControl
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
            this.buttonRegister = new System.Windows.Forms.Button();
            this.checkBoxAllowRollower = new System.Windows.Forms.CheckBox();
            this.checkBoxBringToFrontafterReload = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(13, 12);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(175, 23);
            this.buttonRegister.TabIndex = 0;
            this.buttonRegister.Text = "Register ShellExtensions";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // checkBoxAllowRollower
            // 
            this.checkBoxAllowRollower.AutoSize = true;
            this.checkBoxAllowRollower.Checked = true;
            this.checkBoxAllowRollower.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowRollower.Location = new System.Drawing.Point(13, 72);
            this.checkBoxAllowRollower.Name = "checkBoxAllowRollower";
            this.checkBoxAllowRollower.Size = new System.Drawing.Size(290, 17);
            this.checkBoxAllowRollower.TabIndex = 1;
            this.checkBoxAllowRollower.Text = "Unlock Logfiles for 5 Seconds to enable hourly Rollower";
            this.checkBoxAllowRollower.UseVisualStyleBackColor = true;
            this.checkBoxAllowRollower.CheckedChanged += new System.EventHandler(this.checkBoxAllowRollower_CheckedChanged);
            // 
            // checkBoxBringToFrontafterReload
            // 
            this.checkBoxBringToFrontafterReload.AutoSize = true;
            this.checkBoxBringToFrontafterReload.Location = new System.Drawing.Point(13, 95);
            this.checkBoxBringToFrontafterReload.Name = "checkBoxBringToFrontafterReload";
            this.checkBoxBringToFrontafterReload.Size = new System.Drawing.Size(192, 17);
            this.checkBoxBringToFrontafterReload.TabIndex = 2;
            this.checkBoxBringToFrontafterReload.Text = "Bring to Front after Logfile changed";
            this.checkBoxBringToFrontafterReload.UseVisualStyleBackColor = true;
            // 
            // OptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxBringToFrontafterReload);
            this.Controls.Add(this.checkBoxAllowRollower);
            this.Controls.Add(this.buttonRegister);
            this.Name = "OptionsControl";
            this.Size = new System.Drawing.Size(631, 340);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.CheckBox checkBoxAllowRollower;
        private System.Windows.Forms.CheckBox checkBoxBringToFrontafterReload;
    }
}
