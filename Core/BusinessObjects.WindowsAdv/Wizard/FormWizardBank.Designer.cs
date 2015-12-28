namespace BusinessObjects.Windows.Wizard
{
    partial class FormWizardBank
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
            this.wizardControl = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.edTaxValue = new DevExpress.XtraEditors.SpinEdit();
            this.chkTax = new DevExpress.XtraEditors.CheckEdit();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.txtRegNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPhone = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtOkpo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtInn = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.txtNameFull = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddressLegal = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddressLocal = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.txtMfo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardControl.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edTaxValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOkpo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLegal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLocal.Properties)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMfo.Properties)).BeginInit();
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
            this.wizardControl.Text = "Мастер банка...";
            this.wizardControl.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.edTaxValue);
            this.welcomeWizardPage1.Controls.Add(this.chkTax);
            this.welcomeWizardPage1.Controls.Add(this.txtMemo);
            this.welcomeWizardPage1.Controls.Add(this.txtRegNumber);
            this.welcomeWizardPage1.Controls.Add(this.labelControl4);
            this.welcomeWizardPage1.Controls.Add(this.txtPhone);
            this.welcomeWizardPage1.Controls.Add(this.labelControl5);
            this.welcomeWizardPage1.Controls.Add(this.txtMfo);
            this.welcomeWizardPage1.Controls.Add(this.labelControl11);
            this.welcomeWizardPage1.Controls.Add(this.txtOkpo);
            this.welcomeWizardPage1.Controls.Add(this.labelControl3);
            this.welcomeWizardPage1.Controls.Add(this.txtInn);
            this.welcomeWizardPage1.Controls.Add(this.labelControl2);
            this.welcomeWizardPage1.Controls.Add(this.labelControl6);
            this.welcomeWizardPage1.Controls.Add(this.txtName);
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(634, 221);
            this.welcomeWizardPage1.Text = "Основные атрибуты";
            // 
            // edTaxValue
            // 
            this.edTaxValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edTaxValue.Location = new System.Drawing.Point(101, 80);
            this.edTaxValue.Name = "edTaxValue";
            this.edTaxValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edTaxValue.Size = new System.Drawing.Size(134, 20);
            this.edTaxValue.TabIndex = 13;
            // 
            // chkTax
            // 
            this.chkTax.Location = new System.Drawing.Point(4, 54);
            this.chkTax.Name = "chkTax";
            this.chkTax.Properties.Caption = "Плательщик НДС";
            this.chkTax.Size = new System.Drawing.Size(122, 19);
            this.chkTax.TabIndex = 12;
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
            // txtRegNumber
            // 
            this.txtRegNumber.Location = new System.Drawing.Point(281, 54);
            this.txtRegNumber.Name = "txtRegNumber";
            this.txtRegNumber.Properties.NullValuePrompt = "Номер свидетельства плательщика НДС";
            this.txtRegNumber.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtRegNumber.Size = new System.Drawing.Size(348, 20);
            this.txtRegNumber.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(160, 57);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(115, 13);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Номер свидетельства:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(476, 28);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Properties.NullValuePrompt = "Рабочий телефон";
            this.txtPhone.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPhone.Size = new System.Drawing.Size(153, 20);
            this.txtPhone.TabIndex = 10;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(422, 31);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "Телефон:";
            // 
            // txtOkpo
            // 
            this.txtOkpo.Location = new System.Drawing.Point(281, 28);
            this.txtOkpo.Name = "txtOkpo";
            this.txtOkpo.Properties.NullValuePrompt = "Окпо предприятия";
            this.txtOkpo.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtOkpo.Size = new System.Drawing.Size(134, 20);
            this.txtOkpo.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(241, 31);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(34, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "ОКПО:";
            // 
            // txtInn
            // 
            this.txtInn.Location = new System.Drawing.Point(101, 28);
            this.txtInn.Name = "txtInn";
            this.txtInn.Properties.NullValuePrompt = "ИНН предприятия";
            this.txtInn.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtInn.Size = new System.Drawing.Size(134, 20);
            this.txtInn.TabIndex = 10;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 31);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "ИНН:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(6, 83);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(66, 13);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "Ставка НДС:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 2);
            this.txtName.Name = "txtName";
            this.txtName.Properties.NullValuePrompt = "Отображемое наименование";
            this.txtName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtName.Size = new System.Drawing.Size(528, 20);
            this.txtName.TabIndex = 10;
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
            this.wizardPage1.Controls.Add(this.txtAddressLegal);
            this.wizardPage1.Controls.Add(this.txtAddressLocal);
            this.wizardPage1.Controls.Add(this.labelControl9);
            this.wizardPage1.Controls.Add(this.labelControl8);
            this.wizardPage1.Controls.Add(this.labelControl7);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(634, 221);
            this.wizardPage1.Text = "Печать...";
            // 
            // txtNameFull
            // 
            this.txtNameFull.Location = new System.Drawing.Point(0, 22);
            this.txtNameFull.Name = "txtNameFull";
            this.txtNameFull.Properties.NullValuePrompt = "Полное наименование предприятия используемое для печати";
            this.txtNameFull.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtNameFull.Size = new System.Drawing.Size(630, 50);
            this.txtNameFull.TabIndex = 14;
            // 
            // txtAddressLegal
            // 
            this.txtAddressLegal.Location = new System.Drawing.Point(0, 93);
            this.txtAddressLegal.Name = "txtAddressLegal";
            this.txtAddressLegal.Properties.NullValuePrompt = "Юридический адрес предприятия";
            this.txtAddressLegal.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtAddressLegal.Size = new System.Drawing.Size(630, 50);
            this.txtAddressLegal.TabIndex = 14;
            // 
            // txtAddressLocal
            // 
            this.txtAddressLocal.Location = new System.Drawing.Point(0, 168);
            this.txtAddressLocal.Name = "txtAddressLocal";
            this.txtAddressLocal.Properties.NullValuePrompt = "Юридический адрес предприятия";
            this.txtAddressLocal.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtAddressLocal.Size = new System.Drawing.Size(630, 50);
            this.txtAddressLocal.TabIndex = 13;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(3, 149);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(91, 13);
            this.labelControl9.TabIndex = 11;
            this.labelControl9.Text = "Физческий адрес:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(3, 74);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(107, 13);
            this.labelControl8.TabIndex = 11;
            this.labelControl8.Text = "Юридический адрес:";
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
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(241, 83);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(28, 13);
            this.labelControl11.TabIndex = 9;
            this.labelControl11.Text = "МФО:";
            // 
            // txtMfo
            // 
            this.txtMfo.Location = new System.Drawing.Point(281, 80);
            this.txtMfo.Name = "txtMfo";
            this.txtMfo.Properties.NullValuePrompt = "Окпо предприятия";
            this.txtMfo.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMfo.Size = new System.Drawing.Size(134, 20);
            this.txtMfo.TabIndex = 10;
            // 
            // FormWizardBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 383);
            this.Controls.Add(this.wizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWizardBank";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Мастер банка...";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardControl.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            this.welcomeWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edTaxValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOkpo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLegal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLocal.Properties)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            this.completionWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMfo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraWizard.WizardControl wizardControl;
        public DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        public DevExpress.XtraEditors.SpinEdit edTaxValue;
        public DevExpress.XtraEditors.CheckEdit chkTax;
        public DevExpress.XtraEditors.MemoEdit txtMemo;
        public DevExpress.XtraEditors.TextEdit txtRegNumber;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtOkpo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtInn;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraWizard.WizardPage wizardPage1;
        public DevExpress.XtraEditors.MemoEdit txtNameFull;
        public DevExpress.XtraEditors.MemoEdit txtAddressLegal;
        public DevExpress.XtraEditors.MemoEdit txtAddressLocal;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        public DevExpress.XtraEditors.TextEdit txtMfo;
        private DevExpress.XtraEditors.LabelControl labelControl11;
    }
}