namespace BusinessObjects.Windows.Wizard
{
    partial class FormWizardBankAccount
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.wizardControl = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.cmbCurrency = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewCurrency = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmbBank = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewBank = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.txtNameFull = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardControl.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull.Properties)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            this.wizardControl.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl.Controls.Add(this.wizardPage1);
            this.wizardControl.Controls.Add(this.completionWizardPage1);
            this.wizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl.Location = new System.Drawing.Point(0, 0);
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPage1,
            this.completionWizardPage1});
            this.wizardControl.Size = new System.Drawing.Size(694, 383);
            this.wizardControl.Text = "Мастер банковского счета...";
            this.wizardControl.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.cmbCurrency);
            this.welcomeWizardPage1.Controls.Add(this.cmbBank);
            this.welcomeWizardPage1.Controls.Add(this.labelControl4);
            this.welcomeWizardPage1.Controls.Add(this.txtMemo);
            this.welcomeWizardPage1.Controls.Add(this.labelControl5);
            this.welcomeWizardPage1.Controls.Add(this.txtCode);
            this.welcomeWizardPage1.Controls.Add(this.labelControl2);
            this.welcomeWizardPage1.Controls.Add(this.txtName);
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(634, 221);
            this.welcomeWizardPage1.Text = "Основные атрибуты";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(416, 28);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Properties.View = this.ViewCurrency;
            this.cmbCurrency.Size = new System.Drawing.Size(213, 20);
            this.cmbCurrency.TabIndex = 15;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.NotEquals;
            conditionValidationRule1.ErrorText = "Валюта обязательна";
            conditionValidationRule1.Value1 = 0;
            this.dxValidationProvider1.SetValidationRule(this.cmbCurrency, conditionValidationRule1);
            // 
            // ViewCurrency
            // 
            this.ViewCurrency.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewCurrency.Name = "ViewCurrency";
            this.ViewCurrency.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewCurrency.OptionsView.ShowGroupPanel = false;
            this.ViewCurrency.OptionsView.ShowIndicator = false;
            // 
            // cmbBank
            // 
            this.cmbBank.Location = new System.Drawing.Point(101, 54);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBank.Properties.View = this.ViewBank;
            this.cmbBank.Size = new System.Drawing.Size(260, 20);
            this.cmbBank.TabIndex = 15;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.NotEquals;
            conditionValidationRule2.ErrorText = "Банк обязателен";
            conditionValidationRule2.Value1 = 0;
            this.dxValidationProvider1.SetValidationRule(this.cmbBank, conditionValidationRule2);
            // 
            // ViewBank
            // 
            this.ViewBank.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewBank.Name = "ViewBank";
            this.ViewBank.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewBank.OptionsView.ShowGroupPanel = false;
            this.ViewBank.OptionsView.ShowIndicator = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(367, 31);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(43, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Валюта:";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(2, 108);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Properties.NullValuePrompt = "Введите примечание, описание";
            this.txtMemo.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMemo.Size = new System.Drawing.Size(630, 111);
            this.txtMemo.TabIndex = 11;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 57);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Банк:";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(101, 28);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.NullValuePrompt = "Номер счета";
            this.txtCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCode.Size = new System.Drawing.Size(260, 20);
            this.txtCode.TabIndex = 10;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "Номер счета обязателен";
            this.dxValidationProvider1.SetValidationRule(this.txtCode, conditionValidationRule3);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "№ счета:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 2);
            this.txtName.Name = "txtName";
            this.txtName.Properties.NullValuePrompt = "Отображемое наименование";
            this.txtName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtName.Size = new System.Drawing.Size(528, 20);
            this.txtName.TabIndex = 10;
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "Наименование обязательно";
            this.dxValidationProvider1.SetValidationRule(this.txtName, conditionValidationRule4);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Наименование:";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.txtNameFull);
            this.wizardPage1.Controls.Add(this.labelControl7);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(634, 221);
            this.wizardPage1.Text = "Печать...";
            // 
            // txtNameFull
            // 
            this.txtNameFull.Location = new System.Drawing.Point(0, 22);
            this.txtNameFull.Name = "txtNameFull";
            this.txtNameFull.Properties.NullValuePrompt = "Полное наименование банковского счета используемое для печати";
            this.txtNameFull.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtNameFull.Size = new System.Drawing.Size(630, 50);
            this.txtNameFull.TabIndex = 14;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(3, 3);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(138, 13);
            this.labelControl7.TabIndex = 11;
            this.labelControl7.Text = "Наименование для печати:";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.labelControl10);
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(634, 221);
            this.completionWizardPage1.Text = "Завершение...";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(3, 3);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(535, 13);
            this.labelControl10.TabIndex = 2;
            this.labelControl10.Text = "Данные для создания нового предприятия введены. Нажмите кнопку \"Финиш\" для заверш" +
    "ения работы.";
            // 
            // dxValidationProvider1
            // 
            this.dxValidationProvider1.ValidationMode = DevExpress.XtraEditors.DXErrorProvider.ValidationMode.Auto;
            // 
            // FormWizardBankAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 383);
            this.Controls.Add(this.wizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWizardBankAccount";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Мастер банковского счета...";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardControl.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            this.welcomeWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull.Properties)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            this.completionWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraWizard.WizardControl wizardControl;
        public DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        public DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraWizard.WizardPage wizardPage1;
        public DevExpress.XtraEditors.MemoEdit txtNameFull;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbCurrency;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbBank;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewBank;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewCurrency;
    }
}