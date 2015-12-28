namespace BusinessObjects.ProcedureGenerator.Controls
{
    partial class ControlDbObjectsModule
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.ViewDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnOrderNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMemo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Grid = new DevExpress.XtraGrid.GridControl();
            this.View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnTableSchema = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTableName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTableMemo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewDetail
            // 
            this.ViewDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnOrderNo,
            this.gridColumnName,
            this.gridColumnMemo});
            this.ViewDetail.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewDetail.GridControl = this.Grid;
            this.ViewDetail.Name = "ViewDetail";
            this.ViewDetail.OptionsBehavior.Editable = false;
            this.ViewDetail.OptionsDetail.EnableMasterViewMode = false;
            this.ViewDetail.OptionsDetail.ShowDetailTabs = false;
            this.ViewDetail.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewDetail.OptionsView.ShowGroupPanel = false;
            this.ViewDetail.OptionsView.ShowIndicator = false;
            // 
            // gridColumnOrderNo
            // 
            this.gridColumnOrderNo.Caption = "№";
            this.gridColumnOrderNo.FieldName = "OrderNo";
            this.gridColumnOrderNo.MaxWidth = 35;
            this.gridColumnOrderNo.Name = "gridColumnOrderNo";
            this.gridColumnOrderNo.Visible = true;
            this.gridColumnOrderNo.VisibleIndex = 0;
            this.gridColumnOrderNo.Width = 25;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Колонка";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            // 
            // gridColumnMemo
            // 
            this.gridColumnMemo.Caption = "Описание";
            this.gridColumnMemo.FieldName = "Memo";
            this.gridColumnMemo.Name = "gridColumnMemo";
            this.gridColumnMemo.Visible = true;
            this.gridColumnMemo.VisibleIndex = 2;
            // 
            // Grid
            // 
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.ViewDetail;
            gridLevelNode1.RelationName = "ColumnInfos";
            this.Grid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.Grid.Location = new System.Drawing.Point(0, 0);
            this.Grid.MainView = this.View;
            this.Grid.Name = "Grid";
            this.Grid.ShowOnlyPredefinedDetails = true;
            this.Grid.Size = new System.Drawing.Size(803, 398);
            this.Grid.TabIndex = 5;
            this.Grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View,
            this.ViewDetail});
            // 
            // View
            // 
            this.View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnTableSchema,
            this.gridColumnTableName,
            this.gridColumnTableMemo});
            this.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.View.GridControl = this.Grid;
            this.View.GroupCount = 1;
            this.View.Name = "View";
            this.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.View.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.View.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled;
            this.View.OptionsBehavior.Editable = false;
            this.View.OptionsDetail.EnableMasterViewMode = false;
            this.View.OptionsDetail.ShowDetailTabs = false;
            this.View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.View.OptionsSelection.MultiSelect = true;
            this.View.OptionsSelection.UseIndicatorForSelection = false;
            this.View.OptionsView.ShowGroupedColumns = true;
            this.View.OptionsView.ShowIndicator = false;
            this.View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnTableSchema, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnTableSchema
            // 
            this.gridColumnTableSchema.Caption = "Схема";
            this.gridColumnTableSchema.FieldName = "Schema";
            this.gridColumnTableSchema.Name = "gridColumnTableSchema";
            this.gridColumnTableSchema.Visible = true;
            this.gridColumnTableSchema.VisibleIndex = 0;
            this.gridColumnTableSchema.Width = 266;
            // 
            // gridColumnTableName
            // 
            this.gridColumnTableName.Caption = "Таблица";
            this.gridColumnTableName.FieldName = "Name";
            this.gridColumnTableName.Name = "gridColumnTableName";
            this.gridColumnTableName.Visible = true;
            this.gridColumnTableName.VisibleIndex = 1;
            this.gridColumnTableName.Width = 266;
            // 
            // gridColumnTableMemo
            // 
            this.gridColumnTableMemo.Caption = "Описание";
            this.gridColumnTableMemo.FieldName = "Memo";
            this.gridColumnTableMemo.Name = "gridColumnTableMemo";
            this.gridColumnTableMemo.Visible = true;
            this.gridColumnTableMemo.VisibleIndex = 2;
            this.gridColumnTableMemo.Width = 150;
            // 
            // ControlGeneratorModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grid);
            this.DoubleBuffered = true;
            this.Name = "ControlGeneratorModule";
            this.Size = new System.Drawing.Size(803, 398);
            ((System.ComponentModel.ISupportInitialize)(this.ViewDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl Grid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTableSchema;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTableName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTableMemo;
        public DevExpress.XtraGrid.Views.Grid.GridView View;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMemo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnOrderNo;

    }
}
