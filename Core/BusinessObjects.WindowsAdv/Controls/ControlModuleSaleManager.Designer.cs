namespace BusinessObjects.Windows.Controls
{
    partial class ControlModuleSaleManager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GridDocumentsByProduct = new DevExpress.XtraGrid.GridControl();
            this.ViewProductDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.controlListProducts = new BusinessObjects.Windows.Controls.ControlList();
            this.TreeProducts = new BusinessObjects.Windows.Controls.ControlTree();
            this.controlListAgents = new BusinessObjects.Windows.Controls.ControlList();
            this.TreeAgents = new BusinessObjects.Windows.Controls.ControlTree();
            this.GridReports = new DevExpress.XtraGrid.GridControl();
            this.ViewReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GridDocumentsByAgent = new DevExpress.XtraGrid.GridControl();
            this.ViewAgentDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tabbedControlGroup = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutGroupAgent = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemDocumentByAgent = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem4 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemAgentGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAgents = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutGroupProduct = new DevExpress.XtraLayout.LayoutControlGroup();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemProducGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemProducts = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDocumentsByProduct = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemReports = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem5 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewProductDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocumentByAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProducGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocumentsByProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.GridDocumentsByProduct);
            this.layoutControl1.Controls.Add(this.controlListProducts);
            this.layoutControl1.Controls.Add(this.TreeProducts);
            this.layoutControl1.Controls.Add(this.controlListAgents);
            this.layoutControl1.Controls.Add(this.TreeAgents);
            this.layoutControl1.Controls.Add(this.GridReports);
            this.layoutControl1.Controls.Add(this.GridDocumentsByAgent);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1096, 702);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // GridDocumentsByProduct
            // 
            this.GridDocumentsByProduct.Location = new System.Drawing.Point(275, 331);
            this.GridDocumentsByProduct.MainView = this.ViewProductDocuments;
            this.GridDocumentsByProduct.Name = "GridDocumentsByProduct";
            this.GridDocumentsByProduct.Size = new System.Drawing.Size(618, 366);
            this.GridDocumentsByProduct.TabIndex = 15;
            this.GridDocumentsByProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewProductDocuments});
            // 
            // ViewProductDocuments
            // 
            this.ViewProductDocuments.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewProductDocuments.GridControl = this.GridDocumentsByProduct;
            this.ViewProductDocuments.Name = "ViewProductDocuments";
            this.ViewProductDocuments.OptionsBehavior.Editable = false;
            this.ViewProductDocuments.OptionsBehavior.ReadOnly = true;
            this.ViewProductDocuments.OptionsDetail.EnableMasterViewMode = false;
            this.ViewProductDocuments.OptionsNavigation.UseTabKey = false;
            this.ViewProductDocuments.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewProductDocuments.OptionsView.ShowAutoFilterRow = true;
            this.ViewProductDocuments.OptionsView.ShowGroupPanel = false;
            this.ViewProductDocuments.OptionsView.ShowIndicator = false;
            // 
            // controlListProducts
            // 
            this.controlListProducts.Location = new System.Drawing.Point(275, 42);
            this.controlListProducts.Name = "controlListProducts";
            this.controlListProducts.Size = new System.Drawing.Size(618, 263);
            this.controlListProducts.TabIndex = 14;
            // 
            // TreeProducts
            // 
            this.TreeProducts.Location = new System.Drawing.Point(5, 42);
            this.TreeProducts.MinimumSize = new System.Drawing.Size(250, 250);
            this.TreeProducts.Name = "TreeProducts";
            this.TreeProducts.Size = new System.Drawing.Size(260, 655);
            this.TreeProducts.TabIndex = 13;
            // 
            // controlListAgents
            // 
            this.controlListAgents.Location = new System.Drawing.Point(278, 50);
            this.controlListAgents.Name = "controlListAgents";
            this.controlListAgents.Size = new System.Drawing.Size(612, 252);
            this.controlListAgents.TabIndex = 12;
            // 
            // TreeAgents
            // 
            this.TreeAgents.Location = new System.Drawing.Point(5, 42);
            this.TreeAgents.MinimumSize = new System.Drawing.Size(250, 250);
            this.TreeAgents.Name = "TreeAgents";
            this.TreeAgents.Size = new System.Drawing.Size(260, 655);
            this.TreeAgents.TabIndex = 11;
            // 
            // GridReports
            // 
            this.GridReports.Location = new System.Drawing.Point(906, 18);
            this.GridReports.MainView = this.ViewReports;
            this.GridReports.Name = "GridReports";
            this.GridReports.Size = new System.Drawing.Size(188, 682);
            this.GridReports.TabIndex = 10;
            this.GridReports.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewReports});
            // 
            // ViewReports
            // 
            this.ViewReports.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewReports.GridControl = this.GridReports;
            this.ViewReports.Name = "ViewReports";
            this.ViewReports.OptionsBehavior.Editable = false;
            this.ViewReports.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewReports.OptionsView.ShowGroupPanel = false;
            this.ViewReports.OptionsView.ShowIndicator = false;
            // 
            // GridDocumentsByAgent
            // 
            this.GridDocumentsByAgent.Location = new System.Drawing.Point(275, 331);
            this.GridDocumentsByAgent.MainView = this.ViewAgentDocuments;
            this.GridDocumentsByAgent.Name = "GridDocumentsByAgent";
            this.GridDocumentsByAgent.Size = new System.Drawing.Size(618, 366);
            this.GridDocumentsByAgent.TabIndex = 5;
            this.GridDocumentsByAgent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewAgentDocuments});
            // 
            // ViewAgentDocuments
            // 
            this.ViewAgentDocuments.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewAgentDocuments.GridControl = this.GridDocumentsByAgent;
            this.ViewAgentDocuments.Name = "ViewAgentDocuments";
            this.ViewAgentDocuments.OptionsBehavior.AutoPopulateColumns = false;
            this.ViewAgentDocuments.OptionsBehavior.Editable = false;
            this.ViewAgentDocuments.OptionsBehavior.ReadOnly = true;
            this.ViewAgentDocuments.OptionsDetail.EnableMasterViewMode = false;
            this.ViewAgentDocuments.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.ViewAgentDocuments.OptionsNavigation.UseTabKey = false;
            this.ViewAgentDocuments.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewAgentDocuments.OptionsSelection.MultiSelect = true;
            this.ViewAgentDocuments.OptionsView.ShowAutoFilterRow = true;
            this.ViewAgentDocuments.OptionsView.ShowFooter = true;
            this.ViewAgentDocuments.OptionsView.ShowGroupPanel = false;
            this.ViewAgentDocuments.OptionsView.ShowIndicator = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroup,
            this.layoutControlItemReports,
            this.splitterItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1096, 702);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // tabbedControlGroup
            // 
            this.tabbedControlGroup.CustomizationFormText = "tabbedControlGroup1";
            this.tabbedControlGroup.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup.Name = "tabbedControlGroup";
            this.tabbedControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.tabbedControlGroup.SelectedTabPage = this.layoutGroupAgent;
            this.tabbedControlGroup.SelectedTabPageIndex = 0;
            this.tabbedControlGroup.Size = new System.Drawing.Size(898, 702);
            this.tabbedControlGroup.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutGroupAgent,
            this.layoutGroupProduct});
            this.tabbedControlGroup.Text = "tabbedControlGroup";
            // 
            // layoutGroupAgent
            // 
            this.layoutGroupAgent.CustomizationFormText = "По корреспондентам";
            this.layoutGroupAgent.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDocumentByAgent,
            this.splitterItem4,
            this.layoutControlItemAgentGroup,
            this.splitterItem3,
            this.layoutControlGroup2});
            this.layoutGroupAgent.Location = new System.Drawing.Point(0, 0);
            this.layoutGroupAgent.Name = "layoutGroupAgent";
            this.layoutGroupAgent.Size = new System.Drawing.Size(892, 675);
            this.layoutGroupAgent.Text = "По корреспондентам";
            // 
            // layoutControlItemDocumentByAgent
            // 
            this.layoutControlItemDocumentByAgent.Control = this.GridDocumentsByAgent;
            this.layoutControlItemDocumentByAgent.CustomizationFormText = "Документы";
            this.layoutControlItemDocumentByAgent.Location = new System.Drawing.Point(270, 289);
            this.layoutControlItemDocumentByAgent.Name = "layoutControlItemDocumentByAgent";
            this.layoutControlItemDocumentByAgent.Size = new System.Drawing.Size(622, 386);
            this.layoutControlItemDocumentByAgent.Text = "Документы";
            this.layoutControlItemDocumentByAgent.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDocumentByAgent.TextSize = new System.Drawing.Size(89, 13);
            // 
            // splitterItem4
            // 
            this.splitterItem4.AllowHotTrack = true;
            this.splitterItem4.CustomizationFormText = "splitterItem4";
            this.splitterItem4.Location = new System.Drawing.Point(270, 283);
            this.splitterItem4.Name = "splitterItem4";
            this.splitterItem4.Size = new System.Drawing.Size(622, 6);
            // 
            // layoutControlItemAgentGroup
            // 
            this.layoutControlItemAgentGroup.Control = this.TreeAgents;
            this.layoutControlItemAgentGroup.CustomizationFormText = "Группы клиентов";
            this.layoutControlItemAgentGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemAgentGroup.Name = "layoutControlItemAgentGroup";
            this.layoutControlItemAgentGroup.Size = new System.Drawing.Size(264, 675);
            this.layoutControlItemAgentGroup.Text = "Группы клиентов";
            this.layoutControlItemAgentGroup.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemAgentGroup.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItemAgents
            // 
            this.layoutControlItemAgents.Control = this.controlListAgents;
            this.layoutControlItemAgents.CustomizationFormText = "Корреспонденты";
            this.layoutControlItemAgents.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemAgents.Name = "layoutControlItemAgents";
            this.layoutControlItemAgents.Size = new System.Drawing.Size(616, 256);
            this.layoutControlItemAgents.Text = "Корреспонденты";
            this.layoutControlItemAgents.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemAgents.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAgents.TextToControlDistance = 0;
            this.layoutControlItemAgents.TextVisible = false;
            // 
            // splitterItem3
            // 
            this.splitterItem3.AllowHotTrack = true;
            this.splitterItem3.CustomizationFormText = "splitterItem3";
            this.splitterItem3.Location = new System.Drawing.Point(264, 0);
            this.splitterItem3.Name = "splitterItem3";
            this.splitterItem3.Size = new System.Drawing.Size(6, 675);
            // 
            // layoutGroupProduct
            // 
            this.layoutGroupProduct.CustomizationFormText = "По товарам";
            this.layoutGroupProduct.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.splitterItem1,
            this.splitterItem2,
            this.layoutControlItemProducGroup,
            this.layoutControlItemProducts,
            this.layoutControlItemDocumentsByProduct});
            this.layoutGroupProduct.Location = new System.Drawing.Point(0, 0);
            this.layoutGroupProduct.Name = "layoutGroupProduct";
            this.layoutGroupProduct.Size = new System.Drawing.Size(892, 675);
            this.layoutGroupProduct.Text = "По товарам";
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(264, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(6, 675);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(270, 283);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(622, 6);
            // 
            // layoutControlItemProducGroup
            // 
            this.layoutControlItemProducGroup.Control = this.TreeProducts;
            this.layoutControlItemProducGroup.CustomizationFormText = "Группы товара";
            this.layoutControlItemProducGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemProducGroup.Name = "layoutControlItemProducGroup";
            this.layoutControlItemProducGroup.Size = new System.Drawing.Size(264, 675);
            this.layoutControlItemProducGroup.Text = "Группы товара";
            this.layoutControlItemProducGroup.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemProducGroup.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItemProducts
            // 
            this.layoutControlItemProducts.Control = this.controlListProducts;
            this.layoutControlItemProducts.CustomizationFormText = "Товары";
            this.layoutControlItemProducts.Location = new System.Drawing.Point(270, 0);
            this.layoutControlItemProducts.Name = "layoutControlItemProducts";
            this.layoutControlItemProducts.Size = new System.Drawing.Size(622, 283);
            this.layoutControlItemProducts.Text = "Товары";
            this.layoutControlItemProducts.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemProducts.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItemDocumentsByProduct
            // 
            this.layoutControlItemDocumentsByProduct.Control = this.GridDocumentsByProduct;
            this.layoutControlItemDocumentsByProduct.CustomizationFormText = "Документы";
            this.layoutControlItemDocumentsByProduct.Location = new System.Drawing.Point(270, 289);
            this.layoutControlItemDocumentsByProduct.Name = "layoutControlItemDocumentsByProduct";
            this.layoutControlItemDocumentsByProduct.Size = new System.Drawing.Size(622, 386);
            this.layoutControlItemDocumentsByProduct.Text = "Документы";
            this.layoutControlItemDocumentsByProduct.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDocumentsByProduct.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItemReports
            // 
            this.layoutControlItemReports.Control = this.GridReports;
            this.layoutControlItemReports.CustomizationFormText = "Отчеты";
            this.layoutControlItemReports.Location = new System.Drawing.Point(904, 0);
            this.layoutControlItemReports.Name = "layoutControlItemReports";
            this.layoutControlItemReports.Size = new System.Drawing.Size(192, 702);
            this.layoutControlItemReports.Text = "Отчеты";
            this.layoutControlItemReports.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemReports.TextSize = new System.Drawing.Size(89, 13);
            // 
            // splitterItem5
            // 
            this.splitterItem5.AllowHotTrack = true;
            this.splitterItem5.CustomizationFormText = "splitterItem5";
            this.splitterItem5.Location = new System.Drawing.Point(898, 0);
            this.splitterItem5.Name = "splitterItem5";
            this.splitterItem5.Size = new System.Drawing.Size(6, 702);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Корреспонденты";
            this.layoutControlGroup2.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup2.ExpandButtonVisible = true;
            this.layoutControlGroup2.ExpandOnDoubleClick = true;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemAgents});
            this.layoutControlGroup2.Location = new System.Drawing.Point(270, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(622, 283);
            this.layoutControlGroup2.Text = "Корреспонденты";
            // 
            // ControlModuleSaleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ControlModuleSaleManager";
            this.Size = new System.Drawing.Size(1096, 702);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewProductDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocumentByAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProducGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocumentsByProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem4;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem5;
        public DevExpress.XtraGrid.GridControl GridDocumentsByAgent;
        public DevExpress.XtraGrid.GridControl GridReports;
        public DevExpress.XtraLayout.LayoutControlGroup layoutGroupAgent;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAgentGroup;
        internal ControlTree TreeAgents;
        internal ControlList controlListAgents;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewAgentDocuments;
        public DevExpress.XtraLayout.LayoutControlGroup layoutGroupProduct;
        public DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup;
        internal ControlTree TreeProducts;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemProducGroup;
        internal ControlList controlListProducts;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemProducts;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDocumentByAgent;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDocumentsByProduct;
        public DevExpress.XtraGrid.GridControl GridDocumentsByProduct;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewProductDocuments;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewReports;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAgents;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemReports;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
    }
}
