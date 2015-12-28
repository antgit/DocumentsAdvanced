namespace BusinessObjects.Exchange.Controls
{
    partial class ControlDataExportImport
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.GridTables = new DevExpress.XtraGrid.GridControl();
            this.ViewTables = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnSchema = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnTableName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProcedureExport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProcedureImport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridData = new DevExpress.XtraGrid.GridControl();
            this.ViewData = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewData)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.GridTables);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.GridData);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(732, 455);
            this.splitContainerControl1.SplitterPosition = 220;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // GridTables
            // 
            this.GridTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridTables.Location = new System.Drawing.Point(0, 0);
            this.GridTables.MainView = this.ViewTables;
            this.GridTables.Name = "GridTables";
            this.GridTables.Size = new System.Drawing.Size(220, 455);
            this.GridTables.TabIndex = 0;
            this.GridTables.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewTables});
            // 
            // ViewTables
            // 
            this.ViewTables.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSchema,
            this.gridColumnTableName,
            this.gridColumnProcedureExport,
            this.gridColumnProcedureImport});
            this.ViewTables.GridControl = this.GridTables;
            this.ViewTables.GroupCount = 1;
            this.ViewTables.Name = "ViewTables";
            this.ViewTables.OptionsBehavior.Editable = false;
            this.ViewTables.OptionsDetail.EnableMasterViewMode = false;
            this.ViewTables.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewTables.OptionsView.ShowGroupedColumns = true;
            this.ViewTables.OptionsView.ShowGroupPanel = false;
            this.ViewTables.OptionsView.ShowIndicator = false;
            this.ViewTables.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnSchema, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnSchema
            // 
            this.gridColumnSchema.Caption = "Схема";
            this.gridColumnSchema.FieldName = "Schema";
            this.gridColumnSchema.Name = "gridColumnSchema";
            this.gridColumnSchema.Visible = true;
            this.gridColumnSchema.VisibleIndex = 0;
            // 
            // gridColumnTableName
            // 
            this.gridColumnTableName.Caption = "Наименование";
            this.gridColumnTableName.FieldName = "Name";
            this.gridColumnTableName.Name = "gridColumnTableName";
            this.gridColumnTableName.Visible = true;
            this.gridColumnTableName.VisibleIndex = 1;
            // 
            // gridColumnProcedureExport
            // 
            this.gridColumnProcedureExport.Caption = "ХП экспорта";
            this.gridColumnProcedureExport.FieldName = "ProcedureExport";
            this.gridColumnProcedureExport.Name = "gridColumnProcedureExport";
            this.gridColumnProcedureExport.Visible = true;
            this.gridColumnProcedureExport.VisibleIndex = 2;
            // 
            // gridColumnProcedureImport
            // 
            this.gridColumnProcedureImport.Caption = "ХП импорта";
            this.gridColumnProcedureImport.FieldName = "ProcedureImport";
            this.gridColumnProcedureImport.Name = "gridColumnProcedureImport";
            this.gridColumnProcedureImport.Visible = true;
            this.gridColumnProcedureImport.VisibleIndex = 3;
            // 
            // GridData
            // 
            this.GridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridData.Location = new System.Drawing.Point(0, 0);
            this.GridData.MainView = this.ViewData;
            this.GridData.Name = "GridData";
            this.GridData.ShowOnlyPredefinedDetails = true;
            this.GridData.Size = new System.Drawing.Size(506, 455);
            this.GridData.TabIndex = 0;
            this.GridData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewData});
            // 
            // ViewData
            // 
            this.ViewData.BestFitMaxRowCount = 100;
            this.ViewData.GridControl = this.GridData;
            this.ViewData.Name = "ViewData";
            this.ViewData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.ViewData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.ViewData.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled;
            this.ViewData.OptionsBehavior.Editable = false;
            this.ViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewData.OptionsSelection.MultiSelect = true;
            this.ViewData.OptionsView.ColumnAutoWidth = false;
            this.ViewData.OptionsView.ShowFooter = true;
            this.ViewData.OptionsView.ShowGroupPanel = false;
            this.ViewData.OptionsView.ShowIndicator = false;
            // 
            // ControlDataExportImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ControlDataExportImport";
            this.Size = new System.Drawing.Size(732, 455);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        public DevExpress.XtraGrid.GridControl GridTables;
        public DevExpress.XtraGrid.GridControl GridData;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchema;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTableName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProcedureExport;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewTables;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProcedureImport;
    }
}
