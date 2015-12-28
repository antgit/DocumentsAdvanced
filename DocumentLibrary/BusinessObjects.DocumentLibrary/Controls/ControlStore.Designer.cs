namespace BusinessObjects.DocumentLibrary.Controls
{
    partial class ControlStore
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.cmbAgentStoreFrom = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewStoreFrom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemAgentStoreFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbAgentStoreTo = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewStoreTo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemAgentStoreTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbReturnReason = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemReturnReason = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentStoreFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewStoreFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentStoreFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentStoreTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewStoreTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentStoreTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturnReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReturnReason)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl
            // 
            this.navBarControl.OptionsNavPane.ExpandedWidth = 176;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(5, 258);
            // 
            // cmbAgentTo
            // 
            this.cmbAgentTo.Location = new System.Drawing.Point(138, 146);
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.NotEquals;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.Value1 = "0";
            this.ValidationProvider.SetValidationRule(this.cmbAgentTo, conditionValidationRule1);
            // 
            // cmbAgentDepatmentFrom
            // 
            this.cmbAgentDepatmentFrom.Location = new System.Drawing.Point(138, 122);
            // 
            // cmbAgentDepatmentTo
            // 
            this.cmbAgentDepatmentTo.Location = new System.Drawing.Point(138, 170);
            // 
            // cmbAgentFrom
            // 
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.NotEquals;
            conditionValidationRule2.ErrorText = "This value is not valid";
            conditionValidationRule2.Value1 = "0";
            this.ValidationProvider.SetValidationRule(this.cmbAgentFrom, conditionValidationRule2);
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
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            this.ValidationProvider.SetValidationRule(this.dtDate, conditionValidationRule3);
            // 
            // txtName
            // 
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "This value is not valid";
            this.ValidationProvider.SetValidationRule(this.txtName, conditionValidationRule4);
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
            // layoutControlItemAgentDepatmentFrom
            // 
            this.layoutControlItemAgentDepatmentFrom.Location = new System.Drawing.Point(0, 96);
            // 
            // layoutControlItemAgentTo
            // 
            this.layoutControlItemAgentTo.Location = new System.Drawing.Point(0, 120);
            // 
            // layoutControlItemAgentDepatmentTo
            // 
            this.layoutControlItemAgentDepatmentTo.Location = new System.Drawing.Point(0, 144);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 216);
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
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemAgentStoreFrom,
            this.layoutControlItemAgentStoreTo,
            this.layoutControlItemReturnReason});
            this.layoutControlGroupMain.Size = new System.Drawing.Size(601, 303);
            // 
            // GridDetail
            // 
            this.GridDetail.Location = new System.Drawing.Point(2, 305);
            this.GridDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.editNom,
            this.editName,
            this.editCalc});
            this.GridDetail.Size = new System.Drawing.Size(597, 293);
            // 
            // ViewDetail
            // 
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
            // layoutControlItemGrid
            // 
            this.layoutControlItemGrid.Location = new System.Drawing.Point(0, 303);
            this.layoutControlItemGrid.Size = new System.Drawing.Size(601, 297);
            // 
            // gridColumnName
            // 
            this.gridColumnName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            // 
            // gridColumnUnitName
            // 
            this.gridColumnUnitName.OptionsColumn.AllowEdit = false;
            this.gridColumnUnitName.OptionsColumn.AllowFocus = false;
            this.gridColumnUnitName.OptionsColumn.ReadOnly = true;
            // 
            // gridColumnQty
            // 
            this.gridColumnQty.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnQty.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumnQty.DisplayFormat.FormatString = "n2";
            this.gridColumnQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // gridColumnPrice
            // 
            this.gridColumnPrice.DisplayFormat.FormatString = "n2";
            this.gridColumnPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // gridColumnSumm
            // 
            this.gridColumnSumm.DisplayFormat.FormatString = "n2";
            this.gridColumnSumm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumnSumm.SummaryItem.DisplayFormat = "{0:#,##0.00}";
            this.gridColumnSumm.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            // 
            // layoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbReturnReason);
            this.LayoutControl.Controls.Add(this.cmbAgentStoreTo);
            this.LayoutControl.Controls.Add(this.cmbAgentStoreFrom);
            this.LayoutControl.Controls.SetChildIndex(this.dtDate, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentStoreFrom, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentStoreTo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbReturnReason, 0);
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
            // cmbAgentStoreFrom
            // 
            this.cmbAgentStoreFrom.Location = new System.Drawing.Point(138, 98);
            this.cmbAgentStoreFrom.Name = "cmbAgentStoreFrom";
            this.cmbAgentStoreFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAgentStoreFrom.Properties.View = this.ViewStoreFrom;
            this.cmbAgentStoreFrom.Size = new System.Drawing.Size(458, 20);
            this.cmbAgentStoreFrom.StyleController = this.LayoutControl;
            this.cmbAgentStoreFrom.TabIndex = 9;
            // 
            // ViewStoreFrom
            // 
            this.ViewStoreFrom.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewStoreFrom.Name = "ViewStoreFrom";
            this.ViewStoreFrom.OptionsBehavior.Editable = false;
            this.ViewStoreFrom.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewStoreFrom.OptionsView.ShowGroupPanel = false;
            this.ViewStoreFrom.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItemAgentStoreFrom
            // 
            this.layoutControlItemAgentStoreFrom.Control = this.cmbAgentStoreFrom;
            this.layoutControlItemAgentStoreFrom.CustomizationFormText = "Склад";
            this.layoutControlItemAgentStoreFrom.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemAgentStoreFrom.Name = "layoutControlItemAgentStoreFrom";
            this.layoutControlItemAgentStoreFrom.Size = new System.Drawing.Size(595, 24);
            this.layoutControlItemAgentStoreFrom.Text = "Склад:";
            this.layoutControlItemAgentStoreFrom.TextSize = new System.Drawing.Size(129, 13);
            // 
            // cmbAgentStoreTo
            // 
            this.cmbAgentStoreTo.Location = new System.Drawing.Point(138, 194);
            this.cmbAgentStoreTo.Name = "cmbAgentStoreTo";
            this.cmbAgentStoreTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAgentStoreTo.Properties.View = this.ViewStoreTo;
            this.cmbAgentStoreTo.Size = new System.Drawing.Size(458, 20);
            this.cmbAgentStoreTo.StyleController = this.LayoutControl;
            this.cmbAgentStoreTo.TabIndex = 11;
            // 
            // ViewStoreTo
            // 
            this.ViewStoreTo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewStoreTo.Name = "ViewStoreTo";
            this.ViewStoreTo.OptionsBehavior.Editable = false;
            this.ViewStoreTo.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewStoreTo.OptionsView.ShowGroupPanel = false;
            this.ViewStoreTo.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItemAgentStoreTo
            // 
            this.layoutControlItemAgentStoreTo.Control = this.cmbAgentStoreTo;
            this.layoutControlItemAgentStoreTo.CustomizationFormText = "Склад клиента";
            this.layoutControlItemAgentStoreTo.Location = new System.Drawing.Point(0, 168);
            this.layoutControlItemAgentStoreTo.Name = "layoutControlItemAgentStoreTo";
            this.layoutControlItemAgentStoreTo.Size = new System.Drawing.Size(595, 24);
            this.layoutControlItemAgentStoreTo.Text = "Склад клиента:";
            this.layoutControlItemAgentStoreTo.TextSize = new System.Drawing.Size(129, 13);
            // 
            // cmbReturnReason
            // 
            this.cmbReturnReason.Location = new System.Drawing.Point(138, 218);
            this.cmbReturnReason.Name = "cmbReturnReason";
            this.cmbReturnReason.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReturnReason.Size = new System.Drawing.Size(458, 20);
            this.cmbReturnReason.StyleController = this.LayoutControl;
            this.cmbReturnReason.TabIndex = 17;
            // 
            // layoutControlItemReturnReason
            // 
            this.layoutControlItemReturnReason.Control = this.cmbReturnReason;
            this.layoutControlItemReturnReason.CustomizationFormText = "Причина возврата";
            this.layoutControlItemReturnReason.Location = new System.Drawing.Point(0, 192);
            this.layoutControlItemReturnReason.Name = "layoutControlItemReturnReason";
            this.layoutControlItemReturnReason.Size = new System.Drawing.Size(595, 24);
            this.layoutControlItemReturnReason.Text = "Причина возврата:";
            this.layoutControlItemReturnReason.TextSize = new System.Drawing.Size(129, 13);
            this.layoutControlItemReturnReason.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // ControlStore
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlStore";
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentStoreFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewStoreFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentStoreFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentStoreTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewStoreTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgentStoreTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturnReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemReturnReason)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.GridLookUpEdit cmbAgentStoreFrom;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewStoreFrom;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAgentStoreFrom;
        public DevExpress.XtraEditors.GridLookUpEdit cmbAgentStoreTo;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewStoreTo;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAgentStoreTo;
        public DevExpress.XtraEditors.LookUpEdit cmbReturnReason;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemReturnReason;
    }
}
