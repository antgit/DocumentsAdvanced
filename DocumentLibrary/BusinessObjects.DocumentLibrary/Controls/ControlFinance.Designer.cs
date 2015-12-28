namespace BusinessObjects.DocumentLibrary.Controls
{
    partial class ControlFinance
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
            this.cmbPaymentType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewPaymentType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemPaymentType = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbAgentToBankAcc = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewBankAccountTo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlIMyBankAcc = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbAgentFromBankAcc = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewBankAccountFrom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemClientBankAcc = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentToBankAcc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBankAccountTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlIMyBankAcc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentFromBankAcc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBankAccountFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClientBankAcc)).BeginInit();
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
            this.layoutControlItemPaymentType,
            this.layoutControlIMyBankAcc,
            this.layoutControlItemClientBankAcc});
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
            this.gridColumnSumm.VisibleIndex = 3;
            // 
            // gridColumnMemo
            // 
            this.gridColumnMemo.VisibleIndex = 4;
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbAgentFromBankAcc);
            this.LayoutControl.Controls.Add(this.cmbAgentToBankAcc);
            this.LayoutControl.Controls.Add(this.cmbPaymentType);
            this.LayoutControl.Controls.SetChildIndex(this.cmbPaymentType, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentToBankAcc, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAgentFromBankAcc, 0);
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
            // cmbPaymentType
            // 
            this.cmbPaymentType.Location = new System.Drawing.Point(138, 218);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPaymentType.Properties.View = this.ViewPaymentType;
            this.cmbPaymentType.Size = new System.Drawing.Size(458, 20);
            this.cmbPaymentType.StyleController = this.LayoutControl;
            this.cmbPaymentType.TabIndex = 14;
            // 
            // ViewPaymentType
            // 
            this.ViewPaymentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewPaymentType.Name = "ViewPaymentType";
            this.ViewPaymentType.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewPaymentType.OptionsView.ShowGroupPanel = false;
            this.ViewPaymentType.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItemPaymentType
            // 
            this.layoutControlItemPaymentType.Control = this.cmbPaymentType;
            this.layoutControlItemPaymentType.CustomizationFormText = "Тип платежа";
            this.layoutControlItemPaymentType.Location = new System.Drawing.Point(0, 192);
            this.layoutControlItemPaymentType.Name = "layoutControlItemPaymentType";
            this.layoutControlItemPaymentType.Size = new System.Drawing.Size(595, 24);
            this.layoutControlItemPaymentType.Text = "Тип платежа:";
            this.layoutControlItemPaymentType.TextSize = new System.Drawing.Size(129, 13);
            // 
            // cmbAgentToBankAcc
            // 
            this.cmbAgentToBankAcc.Location = new System.Drawing.Point(138, 194);
            this.cmbAgentToBankAcc.Name = "cmbAgentToBankAcc";
            this.cmbAgentToBankAcc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAgentToBankAcc.Properties.View = this.ViewBankAccountTo;
            this.cmbAgentToBankAcc.Size = new System.Drawing.Size(458, 20);
            this.cmbAgentToBankAcc.StyleController = this.LayoutControl;
            this.cmbAgentToBankAcc.TabIndex = 15;
            // 
            // ViewBankAccountTo
            // 
            this.ViewBankAccountTo.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewBankAccountTo.Name = "ViewBankAccountTo";
            this.ViewBankAccountTo.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewBankAccountTo.OptionsView.ShowGroupPanel = false;
            this.ViewBankAccountTo.OptionsView.ShowIndicator = false;
            // 
            // layoutControlIMyBankAcc
            // 
            this.layoutControlIMyBankAcc.Control = this.cmbAgentToBankAcc;
            this.layoutControlIMyBankAcc.CustomizationFormText = "Счет";
            this.layoutControlIMyBankAcc.Location = new System.Drawing.Point(0, 168);
            this.layoutControlIMyBankAcc.Name = "layoutControlIMyBankAcc";
            this.layoutControlIMyBankAcc.Size = new System.Drawing.Size(595, 24);
            this.layoutControlIMyBankAcc.Text = "Счет:";
            this.layoutControlIMyBankAcc.TextSize = new System.Drawing.Size(129, 13);
            // 
            // cmbAgentFromBankAcc
            // 
            this.cmbAgentFromBankAcc.Location = new System.Drawing.Point(138, 122);
            this.cmbAgentFromBankAcc.Name = "cmbAgentFromBankAcc";
            this.cmbAgentFromBankAcc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAgentFromBankAcc.Properties.View = this.ViewBankAccountFrom;
            this.cmbAgentFromBankAcc.Size = new System.Drawing.Size(458, 20);
            this.cmbAgentFromBankAcc.StyleController = this.LayoutControl;
            this.cmbAgentFromBankAcc.TabIndex = 16;
            // 
            // ViewBankAccountFrom
            // 
            this.ViewBankAccountFrom.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewBankAccountFrom.Name = "ViewBankAccountFrom";
            this.ViewBankAccountFrom.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewBankAccountFrom.OptionsView.ShowGroupPanel = false;
            this.ViewBankAccountFrom.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItemClientBankAcc
            // 
            this.layoutControlItemClientBankAcc.Control = this.cmbAgentFromBankAcc;
            this.layoutControlItemClientBankAcc.CustomizationFormText = "Счет";
            this.layoutControlItemClientBankAcc.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemClientBankAcc.Name = "layoutControlItemClientBankAcc";
            this.layoutControlItemClientBankAcc.Size = new System.Drawing.Size(595, 24);
            this.layoutControlItemClientBankAcc.Text = "Счет:";
            this.layoutControlItemClientBankAcc.TextSize = new System.Drawing.Size(129, 13);
            // 
            // ControlFinance
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlFinance";
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPaymentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentToBankAcc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBankAccountTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlIMyBankAcc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgentFromBankAcc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBankAccountFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemClientBankAcc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemPaymentType;
        public DevExpress.XtraEditors.GridLookUpEdit cmbPaymentType;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewPaymentType;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlIMyBankAcc;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemClientBankAcc;
        public DevExpress.XtraEditors.GridLookUpEdit cmbAgentFromBankAcc;
        public DevExpress.XtraEditors.GridLookUpEdit cmbAgentToBankAcc;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewBankAccountTo;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewBankAccountFrom;
    }
}
