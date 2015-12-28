namespace BusinessObjects.Windows.Wizard
{
    partial class FormWizardStore
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
            this.cmbStoreKeep = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtPhone = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardControl.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStoreKeep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLegal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLocal.Properties)).BeginInit();
            this.completionWizardPage1.SuspendLayout();
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
            this.wizardControl.Text = "Мастер склада...";
            this.wizardControl.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.cmbStoreKeep);
            this.welcomeWizardPage1.Controls.Add(this.txtPhone);
            this.welcomeWizardPage1.Controls.Add(this.labelControl2);
            this.welcomeWizardPage1.Controls.Add(this.labelControl5);
            this.welcomeWizardPage1.Controls.Add(this.txtMemo);
            this.welcomeWizardPage1.Controls.Add(this.txtName);
            this.welcomeWizardPage1.Controls.Add(this.labelControl1);
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(634, 221);
            this.welcomeWizardPage1.Text = "Основные атрибуты";
            // 
            // cmbStoreKeep
            // 
            this.cmbStoreKeep.Location = new System.Drawing.Point(101, 55);
            this.cmbStoreKeep.Name = "cmbStoreKeep";
            this.cmbStoreKeep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStoreKeep.Properties.View = this.searchLookUpEdit1View;
            this.cmbStoreKeep.Size = new System.Drawing.Size(213, 20);
            this.cmbStoreKeep.TabIndex = 14;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(101, 28);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Properties.NullValuePrompt = "Рабочий телефон";
            this.txtPhone.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPhone.Size = new System.Drawing.Size(213, 20);
            this.txtPhone.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(67, 13);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "Зав складом:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 31);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 13);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "Телефон:";
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
            this.txtNameFull.Location = new System.Drawing.Point(2, 22);
            this.txtNameFull.Name = "txtNameFull";
            this.txtNameFull.Properties.NullValuePrompt = "Номенование склада используемое для печати";
            this.txtNameFull.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtNameFull.Size = new System.Drawing.Size(630, 50);
            this.txtNameFull.TabIndex = 19;
            // 
            // txtAddressLegal
            // 
            this.txtAddressLegal.Location = new System.Drawing.Point(2, 93);
            this.txtAddressLegal.Name = "txtAddressLegal";
            this.txtAddressLegal.Properties.NullValuePrompt = "Юридический адрес склада";
            this.txtAddressLegal.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtAddressLegal.Size = new System.Drawing.Size(630, 50);
            this.txtAddressLegal.TabIndex = 20;
            // 
            // txtAddressLocal
            // 
            this.txtAddressLocal.Location = new System.Drawing.Point(2, 168);
            this.txtAddressLocal.Name = "txtAddressLocal";
            this.txtAddressLocal.Properties.NullValuePrompt = "Фактический адрес склада";
            this.txtAddressLocal.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtAddressLocal.Size = new System.Drawing.Size(630, 50);
            this.txtAddressLocal.TabIndex = 18;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(5, 149);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(91, 13);
            this.labelControl9.TabIndex = 15;
            this.labelControl9.Text = "Физческий адрес:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 74);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(107, 13);
            this.labelControl8.TabIndex = 16;
            this.labelControl8.Text = "Юридический адрес:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 3);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(138, 13);
            this.labelControl7.TabIndex = 17;
            this.labelControl7.Text = "Наименование для печати:";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.labelControl4);
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(634, 221);
            this.completionWizardPage1.Text = "Завершение...";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(3, 3);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(504, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Данные для создания нового склада введены. Нажмите кнопку \"Финиш\" для завершения " +
    "работы.";
            // 
            // FormWizardStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 383);
            this.Controls.Add(this.wizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 415);
            this.Name = "FormWizardStore";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Склад...";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardControl.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            this.welcomeWizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStoreKeep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLegal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddressLocal.Properties)).EndInit();
            this.completionWizardPage1.ResumeLayout(false);
            this.completionWizardPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.MemoEdit txtMemo;
        public DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtPhone;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbStoreKeep;
        public DevExpress.XtraEditors.MemoEdit txtNameFull;
        public DevExpress.XtraEditors.MemoEdit txtAddressLegal;
        public DevExpress.XtraEditors.MemoEdit txtAddressLocal;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        public DevExpress.XtraWizard.WizardControl wizardControl;
        public DevExpress.XtraWizard.WizardPage wizardPage1;
        public DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
    }
}