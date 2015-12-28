namespace BusinessObjects.ReportServerBackUp
{
    partial class ControlReportServerBackUp
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
            this.btnEditFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Grid = new DevExpress.XtraGrid.GridControl();
            this.View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbServer = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbServer);
            this.LayoutControl.Controls.Add(this.txtLog);
            this.LayoutControl.Controls.Add(this.Grid);
            this.LayoutControl.Controls.Add(this.btnEditFolder);
            this.LayoutControl.Size = new System.Drawing.Size(718, 482);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup.Size = new System.Drawing.Size(718, 482);
            // 
            // btnEditFolder
            // 
            this.btnEditFolder.Location = new System.Drawing.Point(181, 36);
            this.btnEditFolder.Name = "btnEditFolder";
            this.btnEditFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditFolder.Size = new System.Drawing.Size(525, 20);
            this.btnEditFolder.StyleController = this.LayoutControl;
            this.btnEditFolder.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnEditFolder;
            this.layoutControlItem1.CustomizationFormText = "Папка резервного копирования:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(698, 24);
            this.layoutControlItem1.Text = "Папка резервного копирования:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(165, 13);
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(12, 76);
            this.Grid.MainView = this.View;
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(694, 272);
            this.Grid.TabIndex = 5;
            this.Grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View});
            // 
            // View
            // 
            this.View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn3});
            this.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.View.GridControl = this.Grid;
            this.View.GroupCount = 1;
            this.View.Name = "View";
            this.View.OptionsBehavior.Editable = false;
            this.View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.View.OptionsView.AutoCalcPreviewLineCount = true;
            this.View.OptionsView.ShowGroupPanel = false;
            this.View.OptionsView.ShowIndicator = false;
            this.View.OptionsView.ShowPreview = true;
            this.View.PreviewFieldName = "Description";
            this.View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Скрытый";
            this.gridColumn2.FieldName = "Hidden";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 35;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Наименование";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 638;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Путь";
            this.gridColumn3.FieldName = "Path";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Grid;
            this.layoutControlItem2.CustomizationFormText = "Отчеты";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(698, 292);
            this.layoutControlItem2.Text = "Отчеты";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(165, 13);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 368);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(694, 102);
            this.txtLog.StyleController = this.LayoutControl;
            this.txtLog.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtLog;
            this.layoutControlItem3.CustomizationFormText = "Протокол";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 340);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(698, 122);
            this.layoutControlItem3.Text = "Протокол";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(165, 13);
            // 
            // cmbServer
            // 
            this.cmbServer.Location = new System.Drawing.Point(181, 12);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbServer.Properties.View = this.ViewReports;
            this.cmbServer.Size = new System.Drawing.Size(525, 20);
            this.cmbServer.StyleController = this.LayoutControl;
            this.cmbServer.TabIndex = 7;
            // 
            // ViewReports
            // 
            this.ViewReports.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewReports.Name = "ViewReports";
            this.ViewReports.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewReports.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbServer;
            this.layoutControlItem4.CustomizationFormText = "Сервер отчетов";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(698, 24);
            this.layoutControlItem4.Text = "Сервер отчетов:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(165, 13);
            // 
            // ControlReportServerBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlReportServerBackUp";
            this.Size = new System.Drawing.Size(718, 482);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraGrid.Views.Grid.GridView View;
        public DevExpress.XtraGrid.GridControl Grid;
        public DevExpress.XtraEditors.ButtonEdit btnEditFolder;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraEditors.MemoEdit txtLog;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewReports;
        public DevExpress.XtraEditors.GridLookUpEdit cmbServer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}
