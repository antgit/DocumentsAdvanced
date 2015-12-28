namespace SaleManager
{
    partial class FormDocumentView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDocumentView));
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnDone = new DevExpress.XtraBars.BarButtonItem();
            this.btnNotDone = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ControlMain = new SaleManager.ControlMainDocument();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 537);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(879, 23);
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            // 
            // 
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnClose,
            this.btnSave,
            this.btnDone,
            this.btnNotDone,
            this.btnPrint});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 7;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageMain});
            this.ribbon.SelectedPage = this.ribbonPageMain;
            this.ribbon.Size = new System.Drawing.Size(879, 149);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnClose
            // 
            this.btnClose.Caption = "Закрыть";
            this.btnClose.Id = 1;
            this.btnClose.Name = "btnClose";
            this.btnClose.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Сохранить";
            this.btnSave.Id = 2;
            this.btnSave.Name = "btnSave";
            this.btnSave.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnDone
            // 
            this.btnDone.Caption = "Провести";
            this.btnDone.Id = 3;
            this.btnDone.Name = "btnDone";
            this.btnDone.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnNotDone
            // 
            this.btnNotDone.Caption = "Снять с учета";
            this.btnNotDone.Id = 4;
            this.btnNotDone.Name = "btnNotDone";
            this.btnNotDone.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnPrint
            // 
            this.btnPrint.Caption = "Печать";
            this.btnPrint.Id = 5;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPageMain
            // 
            this.ribbonPageMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPageMain.Name = "ribbonPageMain";
            this.ribbonPageMain.Text = "Главная";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnClose);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDone);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNotDone);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnPrint);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Стандарные действия";
            // 
            // ControlMain
            // 
            this.ControlMain.CollectionAgentFrom = ((System.Collections.Generic.List<BusinessObjects.Agent>)(resources.GetObject("ControlMain.CollectionAgentFrom")));
            this.ControlMain.CollectionAgentTo = ((System.Collections.Generic.List<BusinessObjects.Agent>)(resources.GetObject("ControlMain.CollectionAgentTo")));
            this.ControlMain.CollectionBankFrom = ((System.Collections.Generic.List<BusinessObjects.AgentBankAccount>)(resources.GetObject("ControlMain.CollectionBankFrom")));
            this.ControlMain.CollectionBankTo = ((System.Collections.Generic.List<BusinessObjects.AgentBankAccount>)(resources.GetObject("ControlMain.CollectionBankTo")));
            this.ControlMain.CollectionPrice = ((System.Collections.Generic.List<BusinessObjects.PriceName>)(resources.GetObject("ControlMain.CollectionPrice")));
            this.ControlMain.CollectionSuperviser = ((System.Collections.Generic.List<BusinessObjects.Agent>)(resources.GetObject("ControlMain.CollectionSuperviser")));
            this.ControlMain.CollectionTrader = ((System.Collections.Generic.List<BusinessObjects.Agent>)(resources.GetObject("ControlMain.CollectionTrader")));
            this.ControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlMain.Location = new System.Drawing.Point(0, 149);
            this.ControlMain.Name = "ControlMain";
            this.ControlMain.Size = new System.Drawing.Size(879, 388);
            this.ControlMain.TabIndex = 2;
            // 
            // FormDocumentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 560);
            this.Controls.Add(this.ControlMain);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "FormDocumentView";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Документ...";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        public DevExpress.XtraBars.BarButtonItem btnClose;
        public DevExpress.XtraBars.BarButtonItem btnSave;
        public DevExpress.XtraBars.BarButtonItem btnDone;
        public DevExpress.XtraBars.BarButtonItem btnNotDone;
        public DevExpress.XtraBars.BarButtonItem btnPrint;
        public DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMain;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        public ControlMainDocument ControlMain;
    }
}