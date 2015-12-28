namespace BusinessObjects.AccentExchange.Controls
{
    partial class ControlAccent7Exchange
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
            this.splitContainer = new DevExpress.XtraEditors.SplitContainerControl();
            this.Grid = new DevExpress.XtraGrid.GridControl();
            this.gridViewActions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActions)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.Grid);
            this.splitContainer.Panel1.Text = "Panel1";
            this.splitContainer.Panel2.Text = "Panel2";
            this.splitContainer.Size = new System.Drawing.Size(720, 496);
            this.splitContainer.SplitterPosition = 229;
            this.splitContainer.TabIndex = 0;
            this.splitContainer.Text = "splitContainerControl1";
            // 
            // Grid
            // 
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(0, 0);
            this.Grid.MainView = this.gridViewActions;
            this.Grid.Name = "Grid";
            this.Grid.ShowOnlyPredefinedDetails = true;
            this.Grid.Size = new System.Drawing.Size(229, 496);
            this.Grid.TabIndex = 0;
            this.Grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewActions});
            // 
            // gridViewActions
            // 
            this.gridViewActions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnValue});
            this.gridViewActions.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewActions.GridControl = this.Grid;
            this.gridViewActions.Name = "gridViewActions";
            this.gridViewActions.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewActions.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled;
            this.gridViewActions.OptionsBehavior.Editable = false;
            this.gridViewActions.OptionsBehavior.ReadOnly = true;
            this.gridViewActions.OptionsCustomization.AllowGroup = false;
            this.gridViewActions.OptionsDetail.EnableMasterViewMode = false;
            this.gridViewActions.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewActions.OptionsSelection.UseIndicatorForSelection = false;
            this.gridViewActions.OptionsView.ShowGroupPanel = false;
            this.gridViewActions.OptionsView.ShowIndicator = false;
            // 
            // gridColumnValue
            // 
            this.gridColumnValue.Caption = "Наименование";
            this.gridColumnValue.FieldName = "Value";
            this.gridColumnValue.Name = "gridColumnValue";
            this.gridColumnValue.Visible = true;
            this.gridColumnValue.VisibleIndex = 0;
            // 
            // ControlAccent7Exchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "ControlAccent7Exchange";
            this.Size = new System.Drawing.Size(720, 496);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewActions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl Grid;
        public DevExpress.XtraEditors.SplitContainerControl splitContainer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnValue;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewActions;
    }
}
