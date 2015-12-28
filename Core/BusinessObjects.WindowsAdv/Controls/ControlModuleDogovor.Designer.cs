namespace BusinessObjects.Windows.Controls
{
    partial class ControlModuleDogovor
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
            this.TreeFolders = new BusinessObjects.Windows.Controls.ControlTree();
            this.tabbedControlGroup = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutGroupFolder = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemFolderGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemDocuments = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridDocumentsByFolder = new DevExpress.XtraGrid.GridControl();
            this.ViewFolderDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutGroupAgent = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemAgentGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.TreeAgents = new BusinessObjects.Windows.Controls.ControlTree();
            this.layoutControlItemAgents = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlListAgents = new BusinessObjects.Windows.Controls.ControlList();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItemDocumentByAgent = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridDocumentsByAgent = new DevExpress.XtraGrid.GridControl();
            this.ViewAgentDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterItem4 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutGroupProduct = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemProducGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.TreeProducts = new BusinessObjects.Windows.Controls.ControlTree();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.controlListProducts = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridDocumentsByProduct = new DevExpress.XtraGrid.GridControl();
            this.ViewProductDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterItem5 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem6 = new DevExpress.XtraLayout.SplitterItem();
            this.GridReports = new DevExpress.XtraGrid.GridControl();
            this.ViewReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFolderGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewFolderDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocumentByAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProducGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewProductDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.AllowCustomizationMenu = false;
            this.LayoutControl.Controls.Add(this.GridDocumentsByProduct);
            this.LayoutControl.Controls.Add(this.controlListProducts);
            this.LayoutControl.Controls.Add(this.GridReports);
            this.LayoutControl.Controls.Add(this.GridDocumentsByFolder);
            this.LayoutControl.Controls.Add(this.GridDocumentsByAgent);
            this.LayoutControl.Controls.Add(this.controlListAgents);
            this.LayoutControl.Controls.Add(this.TreeAgents);
            this.LayoutControl.Controls.Add(this.TreeFolders);
            this.LayoutControl.Controls.Add(this.TreeProducts);
            this.LayoutControl.Size = new System.Drawing.Size(1004, 699);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.tabbedControlGroup,
            this.layoutControlItem5,
            this.splitterItem3});
            this.layoutControlGroup.Name = "Root";
            this.layoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup.Size = new System.Drawing.Size(1004, 699);
            // 
            // TreeFolders
            // 
            this.TreeFolders.Location = new System.Drawing.Point(5, 42);
            this.TreeFolders.MinimumSize = new System.Drawing.Size(250, 250);
            this.TreeFolders.Name = "TreeFolders";
            this.TreeFolders.Size = new System.Drawing.Size(251, 652);
            this.TreeFolders.TabIndex = 4;
            // 
            // tabbedControlGroup
            // 
            this.tabbedControlGroup.CustomizationFormText = "tabbedControlGroup1";
            this.tabbedControlGroup.Location = new System.Drawing.Point(0, 0);
            this.tabbedControlGroup.Name = "tabbedControlGroup";
            this.tabbedControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.tabbedControlGroup.SelectedTabPage = this.layoutGroupFolder;
            this.tabbedControlGroup.SelectedTabPageIndex = 0;
            this.tabbedControlGroup.Size = new System.Drawing.Size(780, 699);
            this.tabbedControlGroup.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutGroupFolder,
            this.layoutGroupAgent,
            this.layoutGroupProduct});
            this.tabbedControlGroup.Text = "tabbedControlGroup";
            // 
            // layoutGroupFolder
            // 
            this.layoutGroupFolder.CustomizationFormText = "Договора";
            this.layoutGroupFolder.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemFolderGroup,
            this.splitterItem1,
            this.layoutControlItemDocuments});
            this.layoutGroupFolder.Location = new System.Drawing.Point(0, 0);
            this.layoutGroupFolder.Name = "layoutGroupFolder";
            this.layoutGroupFolder.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutGroupFolder.Size = new System.Drawing.Size(774, 672);
            this.layoutGroupFolder.Text = "Договора";
            // 
            // layoutControlItemFolderGroup
            // 
            this.layoutControlItemFolderGroup.Control = this.TreeFolders;
            this.layoutControlItemFolderGroup.CustomizationFormText = "Папки";
            this.layoutControlItemFolderGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemFolderGroup.Name = "layoutControlItemFolderGroup";
            this.layoutControlItemFolderGroup.Size = new System.Drawing.Size(255, 672);
            this.layoutControlItemFolderGroup.Text = "Папки";
            this.layoutControlItemFolderGroup.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemFolderGroup.TextSize = new System.Drawing.Size(131, 13);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(255, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(6, 672);
            // 
            // layoutControlItemDocuments
            // 
            this.layoutControlItemDocuments.Control = this.GridDocumentsByFolder;
            this.layoutControlItemDocuments.CustomizationFormText = "Документы";
            this.layoutControlItemDocuments.Location = new System.Drawing.Point(261, 0);
            this.layoutControlItemDocuments.Name = "layoutControlItemDocuments";
            this.layoutControlItemDocuments.Size = new System.Drawing.Size(513, 672);
            this.layoutControlItemDocuments.Text = "Документы";
            this.layoutControlItemDocuments.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDocuments.TextSize = new System.Drawing.Size(131, 13);
            // 
            // GridDocumentsByFolder
            // 
            this.GridDocumentsByFolder.Location = new System.Drawing.Point(266, 42);
            this.GridDocumentsByFolder.MainView = this.ViewFolderDocuments;
            this.GridDocumentsByFolder.Name = "GridDocumentsByFolder";
            this.GridDocumentsByFolder.Size = new System.Drawing.Size(509, 652);
            this.GridDocumentsByFolder.TabIndex = 6;
            this.GridDocumentsByFolder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewFolderDocuments});
            // 
            // ViewFolderDocuments
            // 
            this.ViewFolderDocuments.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewFolderDocuments.GridControl = this.GridDocumentsByFolder;
            this.ViewFolderDocuments.Name = "ViewFolderDocuments";
            this.ViewFolderDocuments.OptionsBehavior.AutoPopulateColumns = false;
            this.ViewFolderDocuments.OptionsBehavior.Editable = false;
            this.ViewFolderDocuments.OptionsBehavior.ReadOnly = true;
            this.ViewFolderDocuments.OptionsDetail.EnableMasterViewMode = false;
            this.ViewFolderDocuments.OptionsNavigation.UseTabKey = false;
            this.ViewFolderDocuments.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewFolderDocuments.OptionsSelection.MultiSelect = true;
            this.ViewFolderDocuments.OptionsView.ShowAutoFilterRow = true;
            this.ViewFolderDocuments.OptionsView.ShowGroupPanel = false;
            this.ViewFolderDocuments.OptionsView.ShowIndicator = false;
            // 
            // layoutGroupAgent
            // 
            this.layoutGroupAgent.CustomizationFormText = "По корреспондентам";
            this.layoutGroupAgent.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemAgentGroup,
            this.layoutControlItemAgents,
            this.splitterItem2,
            this.layoutControlItemDocumentByAgent,
            this.splitterItem4});
            this.layoutGroupAgent.Location = new System.Drawing.Point(0, 0);
            this.layoutGroupAgent.Name = "layoutGroupAgent";
            this.layoutGroupAgent.Size = new System.Drawing.Size(774, 672);
            this.layoutGroupAgent.Text = "По корреспондентам";
            // 
            // layoutControlItemAgentGroup
            // 
            this.layoutControlItemAgentGroup.Control = this.TreeAgents;
            this.layoutControlItemAgentGroup.CustomizationFormText = "Группы корреспондентов";
            this.layoutControlItemAgentGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemAgentGroup.Name = "layoutControlItemAgentGroup";
            this.layoutControlItemAgentGroup.Size = new System.Drawing.Size(255, 672);
            this.layoutControlItemAgentGroup.Text = "Группы корреспондентов";
            this.layoutControlItemAgentGroup.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemAgentGroup.TextSize = new System.Drawing.Size(131, 13);
            // 
            // TreeAgents
            // 
            this.TreeAgents.Location = new System.Drawing.Point(5, 42);
            this.TreeAgents.MinimumSize = new System.Drawing.Size(250, 250);
            this.TreeAgents.Name = "TreeAgents";
            this.TreeAgents.Size = new System.Drawing.Size(251, 652);
            this.TreeAgents.TabIndex = 6;
            // 
            // layoutControlItemAgents
            // 
            this.layoutControlItemAgents.Control = this.controlListAgents;
            this.layoutControlItemAgents.CustomizationFormText = "Корреспонденты";
            this.layoutControlItemAgents.Location = new System.Drawing.Point(261, 0);
            this.layoutControlItemAgents.Name = "layoutControlItemAgents";
            this.layoutControlItemAgents.Size = new System.Drawing.Size(513, 334);
            this.layoutControlItemAgents.Text = "Корреспонденты";
            this.layoutControlItemAgents.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemAgents.TextSize = new System.Drawing.Size(131, 13);
            // 
            // controlListAgents
            // 
            this.controlListAgents.Location = new System.Drawing.Point(266, 42);
            this.controlListAgents.Name = "controlListAgents";
            this.controlListAgents.Size = new System.Drawing.Size(509, 314);
            this.controlListAgents.TabIndex = 7;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(255, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(6, 672);
            // 
            // layoutControlItemDocumentByAgent
            // 
            this.layoutControlItemDocumentByAgent.Control = this.GridDocumentsByAgent;
            this.layoutControlItemDocumentByAgent.CustomizationFormText = "Документы";
            this.layoutControlItemDocumentByAgent.Location = new System.Drawing.Point(261, 340);
            this.layoutControlItemDocumentByAgent.Name = "layoutControlItemDocumentByAgent";
            this.layoutControlItemDocumentByAgent.Size = new System.Drawing.Size(513, 332);
            this.layoutControlItemDocumentByAgent.Text = "Документы";
            this.layoutControlItemDocumentByAgent.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemDocumentByAgent.TextSize = new System.Drawing.Size(131, 13);
            // 
            // GridDocumentsByAgent
            // 
            this.GridDocumentsByAgent.Location = new System.Drawing.Point(266, 382);
            this.GridDocumentsByAgent.MainView = this.ViewAgentDocuments;
            this.GridDocumentsByAgent.Name = "GridDocumentsByAgent";
            this.GridDocumentsByAgent.Size = new System.Drawing.Size(509, 312);
            this.GridDocumentsByAgent.TabIndex = 8;
            this.GridDocumentsByAgent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewAgentDocuments});
            // 
            // ViewAgentDocuments
            // 
            this.ViewAgentDocuments.GridControl = this.GridDocumentsByAgent;
            this.ViewAgentDocuments.Name = "ViewAgentDocuments";
            this.ViewAgentDocuments.OptionsView.ShowGroupPanel = false;
            // 
            // splitterItem4
            // 
            this.splitterItem4.AllowHotTrack = true;
            this.splitterItem4.CustomizationFormText = "splitterItem4";
            this.splitterItem4.Location = new System.Drawing.Point(261, 334);
            this.splitterItem4.Name = "splitterItem4";
            this.splitterItem4.Size = new System.Drawing.Size(513, 6);
            // 
            // layoutGroupProduct
            // 
            this.layoutGroupProduct.CustomizationFormText = "По товарам";
            this.layoutGroupProduct.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemProducGroup,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.splitterItem5,
            this.splitterItem6});
            this.layoutGroupProduct.Location = new System.Drawing.Point(0, 0);
            this.layoutGroupProduct.Name = "layoutGroupProduct";
            this.layoutGroupProduct.Size = new System.Drawing.Size(774, 672);
            this.layoutGroupProduct.Text = "По товарам";
            // 
            // layoutControlItemProducGroup
            // 
            this.layoutControlItemProducGroup.Control = this.TreeProducts;
            this.layoutControlItemProducGroup.CustomizationFormText = "Группы товаров";
            this.layoutControlItemProducGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemProducGroup.Name = "layoutControlItemProducGroup";
            this.layoutControlItemProducGroup.Size = new System.Drawing.Size(255, 672);
            this.layoutControlItemProducGroup.Text = "Группы товаров";
            this.layoutControlItemProducGroup.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemProducGroup.TextSize = new System.Drawing.Size(131, 13);
            // 
            // TreeProducts
            // 
            this.TreeProducts.Location = new System.Drawing.Point(5, 42);
            this.TreeProducts.MinimumSize = new System.Drawing.Size(250, 250);
            this.TreeProducts.Name = "TreeProducts";
            this.TreeProducts.Size = new System.Drawing.Size(251, 652);
            this.TreeProducts.TabIndex = 14;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.controlListProducts;
            this.layoutControlItem2.CustomizationFormText = "Товары";
            this.layoutControlItem2.Location = new System.Drawing.Point(261, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(513, 334);
            this.layoutControlItem2.Text = "Товары";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(131, 13);
            // 
            // controlListProducts
            // 
            this.controlListProducts.Location = new System.Drawing.Point(266, 42);
            this.controlListProducts.Name = "controlListProducts";
            this.controlListProducts.Size = new System.Drawing.Size(509, 314);
            this.controlListProducts.TabIndex = 15;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.GridDocumentsByProduct;
            this.layoutControlItem3.CustomizationFormText = "Документы";
            this.layoutControlItem3.Location = new System.Drawing.Point(261, 340);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(513, 332);
            this.layoutControlItem3.Text = "Документы";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(131, 13);
            // 
            // GridDocumentsByProduct
            // 
            this.GridDocumentsByProduct.Location = new System.Drawing.Point(266, 382);
            this.GridDocumentsByProduct.MainView = this.ViewProductDocuments;
            this.GridDocumentsByProduct.Name = "GridDocumentsByProduct";
            this.GridDocumentsByProduct.Size = new System.Drawing.Size(509, 312);
            this.GridDocumentsByProduct.TabIndex = 16;
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
            // splitterItem5
            // 
            this.splitterItem5.AllowHotTrack = true;
            this.splitterItem5.CustomizationFormText = "splitterItem5";
            this.splitterItem5.Location = new System.Drawing.Point(255, 0);
            this.splitterItem5.Name = "splitterItem5";
            this.splitterItem5.Size = new System.Drawing.Size(6, 672);
            // 
            // splitterItem6
            // 
            this.splitterItem6.AllowHotTrack = true;
            this.splitterItem6.CustomizationFormText = "splitterItem6";
            this.splitterItem6.Location = new System.Drawing.Point(261, 334);
            this.splitterItem6.Name = "splitterItem6";
            this.splitterItem6.Size = new System.Drawing.Size(513, 6);
            // 
            // GridReports
            // 
            this.GridReports.Location = new System.Drawing.Point(788, 18);
            this.GridReports.MainView = this.ViewReports;
            this.GridReports.Name = "GridReports";
            this.GridReports.Size = new System.Drawing.Size(214, 679);
            this.GridReports.TabIndex = 11;
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
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.GridReports;
            this.layoutControlItem5.CustomizationFormText = "Отчеты";
            this.layoutControlItem5.Location = new System.Drawing.Point(786, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(218, 699);
            this.layoutControlItem5.Text = "Отчеты";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(131, 13);
            // 
            // splitterItem3
            // 
            this.splitterItem3.AllowHotTrack = true;
            this.splitterItem3.CustomizationFormText = "splitterItem3";
            this.splitterItem3.Location = new System.Drawing.Point(780, 0);
            this.splitterItem3.Name = "splitterItem3";
            this.splitterItem3.Size = new System.Drawing.Size(6, 699);
            // 
            // ControlModuleDogovor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlModuleDogovor";
            this.Size = new System.Drawing.Size(1004, 699);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFolderGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewFolderDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocumentByAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroupProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProducGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocumentsByProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewProductDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl GridDocumentsByFolder;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewFolderDocuments;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDocuments;
        public DevExpress.XtraGrid.GridControl GridReports;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewReports;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        public ControlList controlListAgents;
        public ControlTree TreeAgents;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAgentGroup;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAgents;
        public DevExpress.XtraGrid.GridControl GridDocumentsByAgent;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDocumentByAgent;
        public DevExpress.XtraLayout.LayoutControlGroup layoutGroupProduct;
        public DevExpress.XtraLayout.LayoutControlGroup layoutGroupAgent;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        internal ControlList controlListProducts;
        public DevExpress.XtraGrid.GridControl GridDocumentsByProduct;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewProductDocuments;
        public ControlTree TreeProducts;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemProducGroup;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup;
        private DevExpress.XtraLayout.SplitterItem splitterItem5;
        private DevExpress.XtraLayout.SplitterItem splitterItem6;
        private DevExpress.XtraLayout.SplitterItem splitterItem4;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewAgentDocuments;
        public DevExpress.XtraLayout.LayoutControlGroup layoutGroupFolder;
        public ControlTree TreeFolders;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemFolderGroup;
    }
}
