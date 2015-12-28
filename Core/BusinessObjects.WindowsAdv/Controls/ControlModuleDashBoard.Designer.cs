namespace BusinessObjects.Windows.Controls
{
    partial class ControlModuleDashBoard
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
            this.controlPresenterSales = new BusinessObjects.Windows.Controls.ControlPresenterSales();
            this.layoutControlItemSale = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlPresenterFinance = new BusinessObjects.Windows.Controls.ControlPresenterFinance();
            this.layoutControlItemFinance = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlPresenterStore = new BusinessObjects.Windows.Controls.ControlPresenterStore();
            this.layoutControlItemStore = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlPresenterBookKeep = new BusinessObjects.Windows.Controls.ControlPresenterBookKeep();
            this.layoutControlItemBookKeep = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlPresenterService = new BusinessObjects.Windows.Controls.ControlPresenterService();
            this.layoutControlItemService = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlPresenterDogovor = new BusinessObjects.Windows.Controls.ControlPresenterDogovor();
            this.layoutControlItemPresenterDogovor = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFinance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBookKeep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPresenterDogovor)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.controlPresenterDogovor);
            this.LayoutControl.Controls.Add(this.controlPresenterService);
            this.LayoutControl.Controls.Add(this.controlPresenterBookKeep);
            this.LayoutControl.Controls.Add(this.controlPresenterStore);
            this.LayoutControl.Controls.Add(this.controlPresenterFinance);
            this.LayoutControl.Controls.Add(this.controlPresenterSales);
            this.LayoutControl.Size = new System.Drawing.Size(893, 690);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSale,
            this.layoutControlItemFinance,
            this.layoutControlItemService,
            this.layoutControlItemStore,
            this.layoutControlItemBookKeep,
            this.layoutControlItemPresenterDogovor});
            this.layoutControlGroup.Size = new System.Drawing.Size(893, 690);
            // 
            // controlPresenterSales
            // 
            this.controlPresenterSales.Location = new System.Drawing.Point(12, 12);
            this.controlPresenterSales.MaximumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterSales.MinimumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterSales.Name = "controlPresenterSales";
            this.controlPresenterSales.Size = new System.Drawing.Size(400, 186);
            this.controlPresenterSales.TabIndex = 4;
            // 
            // layoutControlItemSale
            // 
            this.layoutControlItemSale.Control = this.controlPresenterSales;
            this.layoutControlItemSale.CustomizationFormText = "Управление продпжами";
            this.layoutControlItemSale.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemSale.Name = "layoutControlItemSale";
            this.layoutControlItemSale.Size = new System.Drawing.Size(404, 190);
            this.layoutControlItemSale.Text = "Управление продпжами";
            this.layoutControlItemSale.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemSale.TextToControlDistance = 0;
            this.layoutControlItemSale.TextVisible = false;
            // 
            // controlPresenterFinance
            // 
            this.controlPresenterFinance.Location = new System.Drawing.Point(416, 12);
            this.controlPresenterFinance.MaximumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterFinance.MinimumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterFinance.Name = "controlPresenterFinance";
            this.controlPresenterFinance.Size = new System.Drawing.Size(400, 186);
            this.controlPresenterFinance.TabIndex = 5;
            // 
            // layoutControlItemFinance
            // 
            this.layoutControlItemFinance.Control = this.controlPresenterFinance;
            this.layoutControlItemFinance.CustomizationFormText = "Управление финансами";
            this.layoutControlItemFinance.Location = new System.Drawing.Point(404, 0);
            this.layoutControlItemFinance.Name = "layoutControlItemFinance";
            this.layoutControlItemFinance.Size = new System.Drawing.Size(469, 190);
            this.layoutControlItemFinance.Text = "Управление финансами";
            this.layoutControlItemFinance.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemFinance.TextToControlDistance = 0;
            this.layoutControlItemFinance.TextVisible = false;
            // 
            // controlPresenterStore
            // 
            this.controlPresenterStore.Location = new System.Drawing.Point(416, 202);
            this.controlPresenterStore.MaximumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterStore.MinimumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterStore.Name = "controlPresenterStore";
            this.controlPresenterStore.Size = new System.Drawing.Size(400, 186);
            this.controlPresenterStore.TabIndex = 6;
            // 
            // layoutControlItemStore
            // 
            this.layoutControlItemStore.Control = this.controlPresenterStore;
            this.layoutControlItemStore.CustomizationFormText = "Управление товарными запасами";
            this.layoutControlItemStore.Location = new System.Drawing.Point(404, 190);
            this.layoutControlItemStore.Name = "layoutControlItemStore";
            this.layoutControlItemStore.Size = new System.Drawing.Size(469, 190);
            this.layoutControlItemStore.Text = "Управление товарными запасами";
            this.layoutControlItemStore.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemStore.TextToControlDistance = 0;
            this.layoutControlItemStore.TextVisible = false;
            // 
            // controlPresenterBookKeep
            // 
            this.controlPresenterBookKeep.Location = new System.Drawing.Point(12, 392);
            this.controlPresenterBookKeep.MaximumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterBookKeep.MinimumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterBookKeep.Name = "controlPresenterBookKeep";
            this.controlPresenterBookKeep.Size = new System.Drawing.Size(400, 186);
            this.controlPresenterBookKeep.TabIndex = 7;
            // 
            // layoutControlItemBookKeep
            // 
            this.layoutControlItemBookKeep.Control = this.controlPresenterBookKeep;
            this.layoutControlItemBookKeep.CustomizationFormText = "Бухгалтерский учет";
            this.layoutControlItemBookKeep.Location = new System.Drawing.Point(0, 380);
            this.layoutControlItemBookKeep.Name = "layoutControlItemBookKeep";
            this.layoutControlItemBookKeep.Size = new System.Drawing.Size(404, 290);
            this.layoutControlItemBookKeep.Text = "Бухгалтерский учет";
            this.layoutControlItemBookKeep.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemBookKeep.TextToControlDistance = 0;
            this.layoutControlItemBookKeep.TextVisible = false;
            // 
            // controlPresenterService
            // 
            this.controlPresenterService.Location = new System.Drawing.Point(12, 202);
            this.controlPresenterService.MaximumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterService.MinimumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterService.Name = "controlPresenterService";
            this.controlPresenterService.Size = new System.Drawing.Size(400, 186);
            this.controlPresenterService.TabIndex = 8;
            // 
            // layoutControlItemService
            // 
            this.layoutControlItemService.Control = this.controlPresenterService;
            this.layoutControlItemService.CustomizationFormText = "Услуги";
            this.layoutControlItemService.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemService.Name = "layoutControlItemService";
            this.layoutControlItemService.Size = new System.Drawing.Size(404, 190);
            this.layoutControlItemService.Text = "Услуги";
            this.layoutControlItemService.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemService.TextToControlDistance = 0;
            this.layoutControlItemService.TextVisible = false;
            // 
            // controlPresenterDogovor
            // 
            this.controlPresenterDogovor.Location = new System.Drawing.Point(416, 392);
            this.controlPresenterDogovor.MaximumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterDogovor.MinimumSize = new System.Drawing.Size(400, 186);
            this.controlPresenterDogovor.Name = "controlPresenterDogovor";
            this.controlPresenterDogovor.Size = new System.Drawing.Size(400, 186);
            this.controlPresenterDogovor.TabIndex = 9;
            // 
            // layoutControlItemPresenterDogovor
            // 
            this.layoutControlItemPresenterDogovor.Control = this.controlPresenterDogovor;
            this.layoutControlItemPresenterDogovor.CustomizationFormText = "Учет договоров";
            this.layoutControlItemPresenterDogovor.Location = new System.Drawing.Point(404, 380);
            this.layoutControlItemPresenterDogovor.Name = "layoutControlItemPresenterDogovor";
            this.layoutControlItemPresenterDogovor.Size = new System.Drawing.Size(469, 290);
            this.layoutControlItemPresenterDogovor.Text = "Учет договоров";
            this.layoutControlItemPresenterDogovor.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemPresenterDogovor.TextToControlDistance = 0;
            this.layoutControlItemPresenterDogovor.TextVisible = false;
            // 
            // ControlModuleDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlModuleDashBoard";
            this.Size = new System.Drawing.Size(893, 690);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFinance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBookKeep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPresenterDogovor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public ControlPresenterSales controlPresenterSales;
        public ControlPresenterFinance controlPresenterFinance;
        public ControlPresenterStore controlPresenterStore;
        public ControlPresenterBookKeep controlPresenterBookKeep;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemService;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemBookKeep;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemStore;
        public ControlPresenterService controlPresenterService;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemSale;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemFinance;
        public ControlPresenterDogovor controlPresenterDogovor;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemPresenterDogovor;
    }
}
