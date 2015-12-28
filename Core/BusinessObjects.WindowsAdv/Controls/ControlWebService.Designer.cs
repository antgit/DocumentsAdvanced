namespace BusinessObjects.Windows.Controls
{
    partial class ControlWebService
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
            this.txtUrlAddress = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemUrlAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtUid = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemUid = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbAuthKind = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemAuthKind = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkStorePassword = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItemStorePassword = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrlAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUrlAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAuthKind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAuthKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStorePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStorePassword)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 289);
            this.txtMemo.Size = new System.Drawing.Size(376, 59);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 261);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 54);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 96);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 124);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.chkStorePassword);
            this.LayoutControl.Controls.Add(this.cmbAuthKind);
            this.LayoutControl.Controls.Add(this.txtPassword);
            this.LayoutControl.Controls.Add(this.txtUid);
            this.LayoutControl.Controls.Add(this.txtUrlAddress);
            this.LayoutControl.Size = new System.Drawing.Size(400, 360);
            this.LayoutControl.Controls.SetChildIndex(this.txtUrlAddress, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtUid, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtPassword, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbAuthKind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.chkStorePassword, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemUrlAddress,
            this.layoutControlItemAuthKind,
            this.layoutControlItemUid,
            this.layoutControlItemStorePassword,
            this.layoutControlItemPassword});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 335);
            // 
            // txtUrlAddress
            // 
            this.txtUrlAddress.Location = new System.Drawing.Point(132, 84);
            this.txtUrlAddress.Name = "txtUrlAddress";
            this.txtUrlAddress.Size = new System.Drawing.Size(256, 20);
            this.txtUrlAddress.StyleController = this.LayoutControl;
            this.txtUrlAddress.TabIndex = 9;
            // 
            // layoutControlItemUrlAddress
            // 
            this.layoutControlItemUrlAddress.Control = this.txtUrlAddress;
            this.layoutControlItemUrlAddress.CustomizationFormText = "Адрес службы:";
            this.layoutControlItemUrlAddress.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemUrlAddress.Name = "layoutControlItemUrlAddress";
            this.layoutControlItemUrlAddress.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemUrlAddress.Text = "Адрес службы:";
            this.layoutControlItemUrlAddress.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(132, 202);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(256, 20);
            this.txtUid.StyleController = this.LayoutControl;
            this.txtUid.TabIndex = 10;
            // 
            // layoutControlItemUid
            // 
            this.layoutControlItemUid.Control = this.txtUid;
            this.layoutControlItemUid.CustomizationFormText = "Пользователь:";
            this.layoutControlItemUid.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemUid.Name = "layoutControlItemUid";
            this.layoutControlItemUid.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemUid.Text = "Пользователь:";
            this.layoutControlItemUid.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(132, 226);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(256, 20);
            this.txtPassword.StyleController = this.LayoutControl;
            this.txtPassword.TabIndex = 11;
            // 
            // layoutControlItemPassword
            // 
            this.layoutControlItemPassword.Control = this.txtPassword;
            this.layoutControlItemPassword.CustomizationFormText = "Пароль:";
            this.layoutControlItemPassword.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemPassword.Name = "layoutControlItemPassword";
            this.layoutControlItemPassword.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemPassword.Text = "Пароль:";
            this.layoutControlItemPassword.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbAuthKind
            // 
            this.cmbAuthKind.Location = new System.Drawing.Point(132, 178);
            this.cmbAuthKind.Name = "cmbAuthKind";
            this.cmbAuthKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAuthKind.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAuthKind.Size = new System.Drawing.Size(256, 20);
            this.cmbAuthKind.StyleController = this.LayoutControl;
            this.cmbAuthKind.TabIndex = 12;
            // 
            // layoutControlItemAuthKind
            // 
            this.layoutControlItemAuthKind.Control = this.cmbAuthKind;
            this.layoutControlItemAuthKind.CustomizationFormText = "Авторизация:";
            this.layoutControlItemAuthKind.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemAuthKind.Name = "layoutControlItemAuthKind";
            this.layoutControlItemAuthKind.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemAuthKind.Text = "Авторизация:";
            this.layoutControlItemAuthKind.TextSize = new System.Drawing.Size(116, 13);
            // 
            // chkStorePassword
            // 
            this.chkStorePassword.Location = new System.Drawing.Point(12, 250);
            this.chkStorePassword.Name = "chkStorePassword";
            this.chkStorePassword.Properties.Caption = "Сохранять пароль";
            this.chkStorePassword.Size = new System.Drawing.Size(376, 19);
            this.chkStorePassword.StyleController = this.LayoutControl;
            this.chkStorePassword.TabIndex = 13;
            // 
            // layoutControlItemStorePassword
            // 
            this.layoutControlItemStorePassword.Control = this.chkStorePassword;
            this.layoutControlItemStorePassword.CustomizationFormText = "Сохранять пароль";
            this.layoutControlItemStorePassword.Location = new System.Drawing.Point(0, 238);
            this.layoutControlItemStorePassword.Name = "layoutControlItemStorePassword";
            this.layoutControlItemStorePassword.Size = new System.Drawing.Size(380, 23);
            this.layoutControlItemStorePassword.Text = "Сохранять пароль";
            this.layoutControlItemStorePassword.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemStorePassword.TextToControlDistance = 0;
            this.layoutControlItemStorePassword.TextVisible = false;
            // 
            // ControlWebService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 360);
            this.Name = "ControlWebService";
            this.Size = new System.Drawing.Size(400, 360);
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrlAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUrlAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAuthKind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAuthKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStorePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStorePassword)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.ComboBoxEdit cmbAuthKind;
        public DevExpress.XtraEditors.TextEdit txtPassword;
        public DevExpress.XtraEditors.TextEdit txtUrlAddress;
        public DevExpress.XtraEditors.TextEdit txtUid;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemPassword;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAuthKind;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemUrlAddress;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemUid;
        public DevExpress.XtraEditors.CheckEdit chkStorePassword;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemStorePassword;
    }
}
