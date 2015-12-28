namespace BusinessObjects.Windows.Controls
{
    partial class ControlModuleSaleLeave
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
            this.Grid = new DevExpress.XtraGrid.GridControl();
            this.View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnUnitCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnExist = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItemGrid = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.Grid);
            this.LayoutControl.Size = new System.Drawing.Size(778, 572);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemGrid});
            this.layoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup.Size = new System.Drawing.Size(778, 572);
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(2, 2);
            this.Grid.MainView = this.View;
            this.Grid.Name = "Grid";
            this.Grid.ShowOnlyPredefinedDetails = true;
            this.Grid.Size = new System.Drawing.Size(774, 568);
            this.Grid.TabIndex = 4;
            this.Grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View});
            // 
            // View
            // 
            this.View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNom,
            this.gridColumnName,
            this.gridColumnUnitCode,
            this.gridColumnPrice,
            this.gridColumnExist});
            this.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.View.GridControl = this.Grid;
            this.View.Name = "View";
            this.View.OptionsBehavior.Editable = false;
            this.View.OptionsDetail.EnableMasterViewMode = false;
            this.View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.View.OptionsView.ShowAutoFilterRow = true;
            this.View.OptionsView.ShowFooter = true;
            this.View.OptionsView.ShowIndicator = false;
            // 
            // gridColumnNom
            // 
            this.gridColumnNom.Caption = "Номенклатура";
            this.gridColumnNom.FieldName = "Nomenclature";
            this.gridColumnNom.Name = "gridColumnNom";
            this.gridColumnNom.Visible = true;
            this.gridColumnNom.VisibleIndex = 0;
            this.gridColumnNom.Width = 311;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Наименование";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 826;
            // 
            // gridColumnUnitCode
            // 
            this.gridColumnUnitCode.Caption = "Ед изм";
            this.gridColumnUnitCode.FieldName = "UnitCode";
            this.gridColumnUnitCode.Name = "gridColumnUnitCode";
            this.gridColumnUnitCode.Visible = true;
            this.gridColumnUnitCode.VisibleIndex = 2;
            this.gridColumnUnitCode.Width = 81;
            // 
            // gridColumnPrice
            // 
            this.gridColumnPrice.Caption = "Цена";
            this.gridColumnPrice.DisplayFormat.FormatString = "N2";
            this.gridColumnPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnPrice.FieldName = "Price";
            this.gridColumnPrice.Name = "gridColumnPrice";
            this.gridColumnPrice.Visible = true;
            this.gridColumnPrice.VisibleIndex = 3;
            this.gridColumnPrice.Width = 140;
            // 
            // gridColumnExist
            // 
            this.gridColumnExist.Caption = "Наличие";
            this.gridColumnExist.FieldName = "Leave";
            this.gridColumnExist.Name = "gridColumnExist";
            this.gridColumnExist.Visible = true;
            this.gridColumnExist.VisibleIndex = 4;
            this.gridColumnExist.Width = 150;
            // 
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Control = this.Grid;
            this.layoutControlItemGrid.CustomizationFormText = "Товары";
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemGrid.Name = "layoutControlItemGrid";
            this.layoutControlItemGrid.Size = new System.Drawing.Size(778, 572);
            this.layoutControlItemGrid.Text = "Товары";
            this.layoutControlItemGrid.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemGrid.TextToControlDistance = 0;
            this.layoutControlItemGrid.TextVisible = false;
            // 
            // ControlModuleSaleLeave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlModuleSaleLeave";
            this.Size = new System.Drawing.Size(778, 572);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemGrid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnExist;
        public DevExpress.XtraGrid.Views.Grid.GridView View;
        public DevExpress.XtraGrid.GridControl Grid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnUnitCode;
    }
}
