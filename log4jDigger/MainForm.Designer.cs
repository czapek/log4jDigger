using log4jDigger.Controls;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logListControlMain = new log4jDigger.Controls.LogListControl();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageBasket = new System.Windows.Forms.TabPage();
            this.logfileBasketControl = new log4jDigger.Controls.LogfileBasketControl();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.searchControlMain = new log4jDigger.Controls.SearchControl();
            this.tabPageJavaProcess = new System.Windows.Forms.TabPage();
            this.javaProcessControl = new log4jDigger.Controls.JavaProcessControl();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.optionsControl = new log4jDigger.Controls.OptionsControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageBasket.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageJavaProcess.SuspendLayout();
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
            this.logListControlMain.LongCenterInfo = "";
            this.logListControlMain.Name = "logListControlMain";
            this.logListControlMain.ShortLeftInfo = "";
            this.logListControlMain.ShortRightInfo = "";
            this.logListControlMain.Size = new System.Drawing.Size(1233, 392);
            this.logListControlMain.TabIndex = 1;
            this.logListControlMain.VirtualListSize = ((long)(0));
            this.logListControlMain.DoubleClickListView += LogListControlMain_DoubleClickListView;
            this.logListControlMain.SelectedIndexChangedListView += new System.EventHandler(this.logListControlMain_SelectedIndexChangedListView);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageBasket);
            this.tabControlMain.Controls.Add(this.tabPageSearch);
            this.tabControlMain.Controls.Add(this.tabPageJavaProcess);
            this.tabControlMain.Controls.Add(this.tabPageOptions);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1233, 180);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControlMain_SelectedIndexChanged);
            this.tabControlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControlMain_MouseUp);
            // 
            // tabPageBasket
            // 
            this.tabPageBasket.Controls.Add(this.logfileBasketControl);
            this.tabPageBasket.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasket.Name = "tabPageBasket";
            this.tabPageBasket.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBasket.Size = new System.Drawing.Size(1225, 154);
            this.tabPageBasket.TabIndex = 0;
            this.tabPageBasket.Text = "Basket";
            this.tabPageBasket.UseVisualStyleBackColor = true;
            // 
            // logfileBasketControl
            // 
            this.logfileBasketControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logfileBasketControl.IsIndexing = false;
            this.logfileBasketControl.Location = new System.Drawing.Point(3, 3);
            this.logfileBasketControl.Name = "logfileBasketControl";
            this.logfileBasketControl.Size = new System.Drawing.Size(1219, 148);
            this.logfileBasketControl.TabIndex = 7;
            this.logfileBasketControl.CreateIndexEvent += new System.EventHandler(this.logfileBasketControl_CreateIndexEvent);
            this.logfileBasketControl.ClearEvent += new System.EventHandler(this.logfileBasketControl_ClearEvent);
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
            // tabPageJavaProcess
            // 
            this.tabPageJavaProcess.Controls.Add(this.javaProcessControl);
            this.tabPageJavaProcess.Location = new System.Drawing.Point(4, 22);
            this.tabPageJavaProcess.Name = "tabPageJavaProcess";
            this.tabPageJavaProcess.Size = new System.Drawing.Size(1225, 154);
            this.tabPageJavaProcess.TabIndex = 3;
            this.tabPageJavaProcess.Text = "Java Process";
            this.tabPageJavaProcess.UseVisualStyleBackColor = true;
            // 
            // javaProcessControl
            // 
            this.javaProcessControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.javaProcessControl.Location = new System.Drawing.Point(0, 0);
            this.javaProcessControl.Name = "javaProcessControl";
            this.javaProcessControl.Size = new System.Drawing.Size(1225, 154);
            this.javaProcessControl.TabIndex = 0;
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
            this.optionsControl.AllowRollowerCheckedChanged += new System.EventHandler(this.optionsControl_AllowRollowerCheckedChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
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
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageBasket.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageJavaProcess.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.ResumeLayout(false);
        }


    

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageBasket;
        private System.Windows.Forms.TabPage tabPageSearch;
        private SearchControl searchControlMain;
        private System.Windows.Forms.TabPage tabPageOptions;
        private OptionsControl optionsControl;
        private LogListControl logListControlMain;
        private LogfileBasketControl logfileBasketControl;
        private System.Windows.Forms.TabPage tabPageJavaProcess;
        private JavaProcessControl javaProcessControl;
    }
}

