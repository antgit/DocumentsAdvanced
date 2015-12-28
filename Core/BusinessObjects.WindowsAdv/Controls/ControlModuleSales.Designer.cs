namespace BusinessObjects.Windows.Controls
{
    partial class ControlModuleSales
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
            this.components = new System.ComponentModel.Container();
            this.Layout = new DevExpress.XtraLayout.LayoutControl();
            this.GridDocuments = new DevExpress.XtraGrid.GridControl();
            this.ViewDocuments = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GridGroups = new DevExpress.XtraGrid.GridControl();
            this.ViewGroups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.NavBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupActions = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.GridReports = new DevExpress.XtraGrid.GridControl();
            this.GridViewReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.navBarGroupReports = new DevExpress.XtraNavBar.NavBarGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.navBarItemProcess = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.Layout)).BeginInit();
            this.Layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavBarControl)).BeginInit();
            this.NavBarControl.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // Layout
            // 
            this.Layout.Controls.Add(this.GridDocuments);
            this.Layout.Controls.Add(this.GridGroups);
            this.Layout.Controls.Add(this.NavBarControl);
            this.Layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layout.Location = new System.Drawing.Point(0, 0);
            this.Layout.Name = "Layout";
            this.Layout.Root = this.layoutControlGroup1;
            this.Layout.Size = new System.Drawing.Size(1100, 728);
            this.Layout.TabIndex = 0;
            this.Layout.Text = "layoutControl1";
            // 
            // GridDocuments
            // 
            this.GridDocuments.Location = new System.Drawing.Point(283, 2);
            this.GridDocuments.MainView = this.ViewDocuments;
            this.GridDocuments.Name = "GridDocuments";
            this.GridDocuments.Size = new System.Drawing.Size(565, 724);
            this.GridDocuments.TabIndex = 6;
            this.GridDocuments.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewDocuments});
            // 
            // ViewDocuments
            // 
            this.ViewDocuments.GridControl = this.GridDocuments;
            this.ViewDocuments.Name = "ViewDocuments";
            this.ViewDocuments.OptionsBehavior.Editable = false;
            this.ViewDocuments.OptionsDetail.EnableMasterViewMode = false;
            this.ViewDocuments.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewDocuments.OptionsSelection.MultiSelect = true;
            this.ViewDocuments.OptionsView.ColumnAutoWidth = false;
            this.ViewDocuments.OptionsView.ShowAutoFilterRow = true;
            this.ViewDocuments.OptionsView.ShowGroupedColumns = true;
            this.ViewDocuments.OptionsView.ShowGroupPanel = false;
            this.ViewDocuments.OptionsView.ShowIndicator = false;
            // 
            // GridGroups
            // 
            this.GridGroups.Location = new System.Drawing.Point(2, 2);
            this.GridGroups.MainView = this.ViewGroups;
            this.GridGroups.Name = "GridGroups";
            this.GridGroups.Size = new System.Drawing.Size(271, 724);
            this.GridGroups.TabIndex = 5;
            this.GridGroups.ToolTipController = this.toolTipController;
            this.GridGroups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewGroups});
            // 
            // ViewGroups
            // 
            this.ViewGroups.GridControl = this.GridGroups;
            this.ViewGroups.Name = "ViewGroups";
            this.ViewGroups.OptionsBehavior.Editable = false;
            this.ViewGroups.OptionsFilter.UseNewCustomFilterDialog = true;
            this.ViewGroups.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewGroups.OptionsView.AutoCalcPreviewLineCount = true;
            this.ViewGroups.OptionsView.ShowAutoFilterRow = true;
            this.ViewGroups.OptionsView.ShowGroupPanel = false;
            this.ViewGroups.OptionsView.ShowIndicator = false;
            this.ViewGroups.OptionsView.ShowPreview = true;
            this.ViewGroups.PreviewFieldName = "Memo";
            // 
            // toolTipController
            // 
            this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // NavBarControl
            // 
            this.NavBarControl.ActiveGroup = this.navBarGroupActions;
            this.NavBarControl.Controls.Add(this.navBarGroupControlContainer1);
            this.NavBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupActions,
            this.navBarGroupReports});
            this.NavBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItemProcess});
            this.NavBarControl.Location = new System.Drawing.Point(858, 2);
            this.NavBarControl.Name = "NavBarControl";
            this.NavBarControl.OptionsNavPane.ExpandedWidth = 241;
            this.NavBarControl.Size = new System.Drawing.Size(240, 724);
            this.NavBarControl.TabIndex = 4;
            this.NavBarControl.Text = "navBarControl1";
            // 
            // navBarGroupActions
            // 
            this.navBarGroupActions.Caption = "Действия";
            this.navBarGroupActions.Expanded = true;
            this.navBarGroupActions.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;
            this.navBarGroupActions.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemProcess)});
            this.navBarGroupActions.Name = "navBarGroupActions";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.GridReports);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(238, 79);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // GridReports
            // 
            this.GridReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridReports.Location = new System.Drawing.Point(0, 0);
            this.GridReports.MainView = this.GridViewReports;
            this.GridReports.Name = "GridReports";
            this.GridReports.Size = new System.Drawing.Size(238, 79);
            this.GridReports.TabIndex = 3;
            this.GridReports.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewReports});
            // 
            // GridViewReports
            // 
            this.GridViewReports.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.GridViewReports.GridControl = this.GridReports;
            this.GridViewReports.Name = "GridViewReports";
            this.GridViewReports.OptionsBehavior.Editable = false;
            this.GridViewReports.OptionsDetail.EnableMasterViewMode = false;
            this.GridViewReports.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridViewReports.OptionsView.ShowGroupPanel = false;
            this.GridViewReports.OptionsView.ShowIndicator = false;
            // 
            // navBarGroupReports
            // 
            this.navBarGroupReports.Caption = "Отчеты";
            this.navBarGroupReports.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroupReports.Expanded = true;
            this.navBarGroupReports.GroupClientHeight = 80;
            this.navBarGroupReports.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroupReports.Name = "navBarGroupReports";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.splitterItem1,
            this.splitterItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1100, 728);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GridGroups;
            this.layoutControlItem2.CustomizationFormText = "Раздел";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(275, 728);
            this.layoutControlItem2.Text = "Раздел";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.NavBarControl;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(856, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(244, 728);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.GridDocuments;
            this.layoutControlItem3.CustomizationFormText = "Документы";
            this.layoutControlItem3.Location = new System.Drawing.Point(281, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(569, 728);
            this.layoutControlItem3.Text = "Документы";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(850, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(6, 728);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(275, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(6, 728);
            // 
            // navBarItemProcess
            // 
            this.navBarItemProcess.Caption = "Процессы...";
            this.navBarItemProcess.Name = "navBarItemProcess";
            // 
            // ControlModuleSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Layout);
            this.Name = "ControlModuleSales";
            this.Size = new System.Drawing.Size(1100, 728);
            ((System.ComponentModel.ISupportInitialize)(this.Layout)).EndInit();
            this.Layout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavBarControl)).EndInit();
            this.NavBarControl.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        public DevExpress.XtraLayout.LayoutControl Layout;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraGrid.GridControl GridDocuments;
        public DevExpress.XtraGrid.GridControl GridGroups;
        public DevExpress.XtraNavBar.NavBarControl NavBarControl;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewGroups;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewDocuments;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        public DevExpress.XtraGrid.GridControl GridReports;
        public DevExpress.XtraGrid.Views.Grid.GridView GridViewReports;
        public DevExpress.XtraNavBar.NavBarGroup navBarGroupReports;
        public DevExpress.XtraNavBar.NavBarGroup navBarGroupActions;
        public DevExpress.Utils.ToolTipController toolTipController;
        public DevExpress.XtraNavBar.NavBarItem navBarItemProcess;
    }
}
