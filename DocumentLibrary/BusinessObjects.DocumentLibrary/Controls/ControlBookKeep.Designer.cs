namespace BusinessObjects.DocumentLibrary.Controls
{
    partial class ControlBookKeep
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.gridColumnAccDb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAccCr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.editDb = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.editCr = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentDepatmentFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentDepatmentTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDepatmentFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDepatmentTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValidationProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentDepatmentFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentDepatmentTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridChainDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewChainDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editNom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCalc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editDb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl
            // 
            this.navBarControl.OptionsNavPane.ExpandedWidth = 176;
            // 
            // txtMemo
            // 
            // 
            // cmbAgentTo
            // 
            // 
            // cmbAgentDepatmentFrom
            // 
            // 
            // cmbAgentDepatmentTo
            // 
            // 
            // cmbAgentFrom
            // 
            // 
            // txtNumber
            // 
            // 
            // dtDate
            // 
            this.dtDate.EditValue = null;
            this.dtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            this.ValidationProvider.SetValidationRule(this.dtDate, conditionValidationRule2);
            // 
            // txtName
            // 
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            this.ValidationProvider.SetValidationRule(this.txtName, conditionValidationRule3);
            // 
            // ViewDepatmentFrom
            // 
            this.ViewDepatmentFrom.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewDepatmentFrom.OptionsView.ShowGroupPanel = false;
            this.ViewDepatmentFrom.OptionsView.ShowIndicator = false;
            // 
            // ViewDepatmentTo
            // 
            this.ViewDepatmentTo.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewDepatmentTo.OptionsView.ShowGroupPanel = false;
            this.ViewDepatmentTo.OptionsView.ShowIndicator = false;
            // 
            // ViewAgentTo
            // 
            this.ViewAgentTo.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewAgentTo.OptionsView.ShowGroupPanel = false;
            this.ViewAgentTo.OptionsView.ShowIndicator = false;
            // 
            // ViewAgentFrom
            // 
            this.ViewAgentFrom.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewAgentFrom.OptionsView.ShowGroupPanel = false;
            this.ViewAgentFrom.OptionsView.ShowIndicator = false;
            // 
            // GridViewChainDocs
            // 
            this.GridViewChainDocs.OptionsBehavior.Editable = false;
            this.GridViewChainDocs.OptionsDetail.EnableMasterViewMode = false;
            this.GridViewChainDocs.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridViewChainDocs.OptionsView.ShowGroupPanel = false;
            this.GridViewChainDocs.OptionsView.ShowIndicator = false;
            // 
            // GridViewReports
            // 
            this.GridViewReports.OptionsBehavior.Editable = false;
            this.GridViewReports.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridViewReports.OptionsView.ShowGroupPanel = false;
            this.GridViewReports.OptionsView.ShowIndicator = false;
            // 
            // GridDetail
            // 
            this.GridDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.editNom,
            this.editName,
            this.editCalc,
            this.editDb,
            this.editCr});
            // 
            // ViewDetail
            // 
            this.ViewDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnAccDb,
            this.gridColumnAccCr});
            this.ViewDetail.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.ViewDetail.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.ViewDetail.OptionsBehavior.AutoPopulateColumns = false;
            this.ViewDetail.OptionsCustomization.AllowColumnMoving = false;
            this.ViewDetail.OptionsSelection.UseIndicatorForSelection = false;
            this.ViewDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.ViewDetail.OptionsView.ShowFooter = true;
            this.ViewDetail.OptionsView.ShowGroupPanel = false;
            this.ViewDetail.OptionsView.ShowIndicator = false;
            // 
            // editCalc
            // 
            this.editCalc.DisplayFormat.FormatString = "N2";
            this.editCalc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // gridColumnNom
            // 
            this.gridColumnNom.Visible = false;
            this.gridColumnNom.VisibleIndex = -1;
            this.gridColumnNom.Width = 68;
            // 
            // gridColumnName
            // 
            this.gridColumnName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gridColumnName.Visible = false;
            this.gridColumnName.VisibleIndex = -1;
            // 
            // gridColumnUnitName
            // 
            this.gridColumnUnitName.OptionsColumn.AllowEdit = false;
            this.gridColumnUnitName.OptionsColumn.AllowFocus = false;
            this.gridColumnUnitName.OptionsColumn.ReadOnly = true;
            this.gridColumnUnitName.Visible = false;
            this.gridColumnUnitName.VisibleIndex = -1;
            // 
            // gridColumnQty
            // 
            this.gridColumnQty.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumnQty.DisplayFormat.FormatString = "n2";
            this.gridColumnQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnQty.Visible = false;
            this.gridColumnQty.VisibleIndex = -1;
            // 
            // gridColumnPrice
            // 
            this.gridColumnPrice.DisplayFormat.FormatString = "n2";
            this.gridColumnPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnPrice.Visible = false;
            this.gridColumnPrice.VisibleIndex = -1;
            // 
            // gridColumnSumm
            // 
            this.gridColumnSumm.DisplayFormat.FormatString = "n2";
            this.gridColumnSumm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnSumm.SummaryItem.DisplayFormat = "{0:#,##0.00}";
            this.gridColumnSumm.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumnSumm.VisibleIndex = 2;
            // 
            // gridColumnMemo
            // 
            this.gridColumnMemo.VisibleIndex = 3;
            this.LayoutControl.Controls.SetChildIndex(this.dtDate, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNumber, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentFrom, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentDepatmentFrom, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentTo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentDepatmentTo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.navBarControl, 0);
            this.LayoutControl.Controls.SetChildIndex(this.GridDetail, 0);
            // 
            // gridColumnAccDb
            // 
            this.gridColumnAccDb.Caption = "Дебет";
            this.gridColumnAccDb.ColumnEdit = this.editDb;
            this.gridColumnAccDb.FieldName = "AccountDbId";
            this.gridColumnAccDb.Name = "gridColumnAccDb";
            this.gridColumnAccDb.OptionsColumn.AllowSize = false;
            this.gridColumnAccDb.OptionsColumn.FixedWidth = true;
            this.gridColumnAccDb.Visible = true;
            this.gridColumnAccDb.VisibleIndex = 0;
            this.gridColumnAccDb.Width = 65;
            // 
            // gridColumnAccCr
            // 
            this.gridColumnAccCr.Caption = "Кредит";
            this.gridColumnAccCr.ColumnEdit = this.editCr;
            this.gridColumnAccCr.FieldName = "AccountCrId";
            this.gridColumnAccCr.Name = "gridColumnAccCr";
            this.gridColumnAccCr.OptionsColumn.AllowSize = false;
            this.gridColumnAccCr.OptionsColumn.FixedWidth = true;
            this.gridColumnAccCr.Visible = true;
            this.gridColumnAccCr.VisibleIndex = 1;
            this.gridColumnAccCr.Width = 65;
            // 
            // editDb
            // 
            this.editDb.AutoHeight = false;
            this.editDb.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editDb.DisplayMember = "Code";
            this.editDb.Name = "editDb";
            this.editDb.ValueMember = "Id";
            this.editDb.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // editCr
            // 
            this.editCr.AutoHeight = false;
            this.editCr.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.editCr.DisplayMember = "Code";
            this.editCr.Name = "editCr";
            this.editCr.ValueMember = "Id";
            this.editCr.View = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // ControlBookKeep
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlBookKeep";
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentDepatmentFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentDepatmentTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDepatmentFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDepatmentTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAgentFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValidationProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentDepatmentFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentDepatmentTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridChainDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewChainDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editNom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCalc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editDb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.Columns.GridColumn gridColumnAccDb;
        public DevExpress.XtraGrid.Columns.GridColumn gridColumnAccCr;
        public DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit editDb;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        public DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit editCr;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;

    }
}
