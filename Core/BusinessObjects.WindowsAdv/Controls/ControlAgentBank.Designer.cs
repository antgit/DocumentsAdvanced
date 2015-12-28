namespace BusinessObjects.Windows.Controls
{
    partial class ControlAgentBank
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
            this.txtSertificateNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemSertificateNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtLicenseNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemLicenseNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtSertificateDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemSertificateDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtLicenseDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemLicenseDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtSwift = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemSwift = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCorrBankAccount = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemCorrBankAccount = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSertificateNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSertificateNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLicenseNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLicenseNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSertificateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSertificateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSertificateDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLicenseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLicenseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLicenseDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSwift.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSwift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrBankAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCorrBankAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.txtCorrBankAccount);
            this.LayoutControl.Controls.Add(this.txtSwift);
            this.LayoutControl.Controls.Add(this.dtLicenseDate);
            this.LayoutControl.Controls.Add(this.dtSertificateDate);
            this.LayoutControl.Controls.Add(this.txtLicenseNo);
            this.LayoutControl.Controls.Add(this.txtSertificateNo);
            this.LayoutControl.Size = new System.Drawing.Size(700, 320);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSertificateNo,
            this.layoutControlItemLicenseNo,
            this.layoutControlItemSwift,
            this.layoutControlItemCorrBankAccount,
            this.layoutControlItemSertificateDate,
            this.layoutControlItemLicenseDate});
            this.layoutControlGroup.Size = new System.Drawing.Size(700, 320);
            // 
            // txtSertificateNo
            // 
            this.txtSertificateNo.Location = new System.Drawing.Point(556, 12);
            this.txtSertificateNo.Name = "txtSertificateNo";
            this.txtSertificateNo.Size = new System.Drawing.Size(132, 20);
            this.txtSertificateNo.StyleController = this.LayoutControl;
            this.txtSertificateNo.TabIndex = 4;
            // 
            // layoutControlItemSertificateNo
            // 
            this.layoutControlItemSertificateNo.Control = this.txtSertificateNo;
            this.layoutControlItemSertificateNo.CustomizationFormText = "Номер свидетельства регистрации НБУ";
            this.layoutControlItemSertificateNo.Location = new System.Drawing.Point(335, 0);
            this.layoutControlItemSertificateNo.Name = "layoutControlItemSertificateNo";
            this.layoutControlItemSertificateNo.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItemSertificateNo.Text = "Номер свидетельства регистрации НБУ:";
            this.layoutControlItemSertificateNo.TextSize = new System.Drawing.Size(205, 13);
            // 
            // txtLicenseNo
            // 
            this.txtLicenseNo.Location = new System.Drawing.Point(556, 36);
            this.txtLicenseNo.Name = "txtLicenseNo";
            this.txtLicenseNo.Size = new System.Drawing.Size(132, 20);
            this.txtLicenseNo.StyleController = this.LayoutControl;
            this.txtLicenseNo.TabIndex = 5;
            // 
            // layoutControlItemLicenseNo
            // 
            this.layoutControlItemLicenseNo.Control = this.txtLicenseNo;
            this.layoutControlItemLicenseNo.CustomizationFormText = "Номер банковской лицензии";
            this.layoutControlItemLicenseNo.Location = new System.Drawing.Point(335, 24);
            this.layoutControlItemLicenseNo.Name = "layoutControlItemLicenseNo";
            this.layoutControlItemLicenseNo.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItemLicenseNo.Text = "Номер банковской лицензии:";
            this.layoutControlItemLicenseNo.TextSize = new System.Drawing.Size(205, 13);
            // 
            // dtSertificateDate
            // 
            this.dtSertificateDate.EditValue = null;
            this.dtSertificateDate.Location = new System.Drawing.Point(221, 12);
            this.dtSertificateDate.Name = "dtSertificateDate";
            this.dtSertificateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtSertificateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtSertificateDate.Size = new System.Drawing.Size(122, 20);
            this.dtSertificateDate.StyleController = this.LayoutControl;
            this.dtSertificateDate.TabIndex = 6;
            // 
            // layoutControlItemSertificateDate
            // 
            this.layoutControlItemSertificateDate.Control = this.dtSertificateDate;
            this.layoutControlItemSertificateDate.CustomizationFormText = "Дата свидетельства регистрации НБУ";
            this.layoutControlItemSertificateDate.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemSertificateDate.MaxSize = new System.Drawing.Size(335, 24);
            this.layoutControlItemSertificateDate.MinSize = new System.Drawing.Size(335, 24);
            this.layoutControlItemSertificateDate.Name = "layoutControlItemSertificateDate";
            this.layoutControlItemSertificateDate.Size = new System.Drawing.Size(335, 24);
            this.layoutControlItemSertificateDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemSertificateDate.Text = "Дата свидетельства регистрации НБУ:";
            this.layoutControlItemSertificateDate.TextSize = new System.Drawing.Size(205, 13);
            // 
            // dtLicenseDate
            // 
            this.dtLicenseDate.EditValue = null;
            this.dtLicenseDate.Location = new System.Drawing.Point(221, 36);
            this.dtLicenseDate.Name = "dtLicenseDate";
            this.dtLicenseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtLicenseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtLicenseDate.Size = new System.Drawing.Size(122, 20);
            this.dtLicenseDate.StyleController = this.LayoutControl;
            this.dtLicenseDate.TabIndex = 7;
            // 
            // layoutControlItemLicenseDate
            // 
            this.layoutControlItemLicenseDate.Control = this.dtLicenseDate;
            this.layoutControlItemLicenseDate.CustomizationFormText = "Дата банковской лицензии";
            this.layoutControlItemLicenseDate.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemLicenseDate.MaxSize = new System.Drawing.Size(335, 24);
            this.layoutControlItemLicenseDate.MinSize = new System.Drawing.Size(335, 24);
            this.layoutControlItemLicenseDate.Name = "layoutControlItemLicenseDate";
            this.layoutControlItemLicenseDate.Size = new System.Drawing.Size(335, 24);
            this.layoutControlItemLicenseDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemLicenseDate.Text = "Дата банковской лицензии:";
            this.layoutControlItemLicenseDate.TextSize = new System.Drawing.Size(205, 13);
            // 
            // txtSwift
            // 
            this.txtSwift.Location = new System.Drawing.Point(221, 60);
            this.txtSwift.Name = "txtSwift";
            this.txtSwift.Size = new System.Drawing.Size(467, 20);
            this.txtSwift.StyleController = this.LayoutControl;
            this.txtSwift.TabIndex = 8;
            // 
            // layoutControlItemSwift
            // 
            this.layoutControlItemSwift.Control = this.txtSwift;
            this.layoutControlItemSwift.CustomizationFormText = "SWIFT";
            this.layoutControlItemSwift.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemSwift.Name = "layoutControlItemSwift";
            this.layoutControlItemSwift.Size = new System.Drawing.Size(680, 24);
            this.layoutControlItemSwift.Text = "S.W.I.F.T.:";
            this.layoutControlItemSwift.TextSize = new System.Drawing.Size(205, 13);
            // 
            // txtCorrBankAccount
            // 
            this.txtCorrBankAccount.Location = new System.Drawing.Point(221, 84);
            this.txtCorrBankAccount.Name = "txtCorrBankAccount";
            this.txtCorrBankAccount.Size = new System.Drawing.Size(467, 20);
            this.txtCorrBankAccount.StyleController = this.LayoutControl;
            this.txtCorrBankAccount.TabIndex = 9;
            // 
            // layoutControlItemCorrBankAccount
            // 
            this.layoutControlItemCorrBankAccount.Control = this.txtCorrBankAccount;
            this.layoutControlItemCorrBankAccount.CustomizationFormText = "Кореспондентский счет";
            this.layoutControlItemCorrBankAccount.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemCorrBankAccount.Name = "layoutControlItemCorrBankAccount";
            this.layoutControlItemCorrBankAccount.Size = new System.Drawing.Size(680, 228);
            this.layoutControlItemCorrBankAccount.Text = "Кореспондентский счет:";
            this.layoutControlItemCorrBankAccount.TextSize = new System.Drawing.Size(205, 13);
            // 
            // ControlAgentBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(700, 320);
            this.Name = "ControlAgentBank";
            this.Size = new System.Drawing.Size(700, 320);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSertificateNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSertificateNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLicenseNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLicenseNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSertificateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSertificateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSertificateDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLicenseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLicenseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLicenseDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSwift.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSwift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrBankAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCorrBankAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemCorrBankAccount;
        public DevExpress.XtraEditors.TextEdit txtCorrBankAccount;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemSwift;
        public DevExpress.XtraEditors.TextEdit txtSwift;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemLicenseDate;
        public DevExpress.XtraEditors.DateEdit dtLicenseDate;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemLicenseNo;
        public DevExpress.XtraEditors.TextEdit txtLicenseNo;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemSertificateDate;
        public DevExpress.XtraEditors.DateEdit dtSertificateDate;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemSertificateNo;
        public DevExpress.XtraEditors.TextEdit txtSertificateNo;
    }
}
