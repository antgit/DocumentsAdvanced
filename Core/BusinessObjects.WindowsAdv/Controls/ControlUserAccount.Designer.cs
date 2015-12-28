namespace BusinessObjects.Windows.Controls
{
    partial class ControlUserAccount
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
            this.layoutControlItemUserId = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbUserId = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewUserOwner = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtPassword = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlItemPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtEmail = new DevExpress.XtraEditors.HyperLinkEdit();
            this.layoutControlItemEmail = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUserOwner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 242);
            this.txtMemo.Size = new System.Drawing.Size(376, 71);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 91);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.txtEmail);
            this.LayoutControl.Controls.Add(this.txtPassword);
            this.LayoutControl.Controls.Add(this.cmbUserId);
            this.LayoutControl.Size = new System.Drawing.Size(400, 325);
            this.LayoutControl.Controls.SetChildIndex(this.cmbUserId, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtPassword, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtEmail, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemUserId,
            this.layoutControlItemEmail,
            this.layoutControlItemPassword});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 325);
            // 
            // layoutControlItemUserId
            // 
            this.layoutControlItemUserId.Control = this.cmbUserId;
            this.layoutControlItemUserId.CustomizationFormText = "Пользователь";
            this.layoutControlItemUserId.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemUserId.Name = "layoutControlItemUserId";
            this.layoutControlItemUserId.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemUserId.Text = "Пользователь:";
            this.layoutControlItemUserId.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbUserId
            // 
            this.cmbUserId.Location = new System.Drawing.Point(132, 154);
            this.cmbUserId.Name = "cmbUserId";
            this.cmbUserId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUserId.Properties.View = this.ViewUserOwner;
            this.cmbUserId.Size = new System.Drawing.Size(256, 20);
            this.cmbUserId.StyleController = this.LayoutControl;
            this.cmbUserId.TabIndex = 9;
            // 
            // ViewUserOwner
            // 
            this.ViewUserOwner.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewUserOwner.Name = "ViewUserOwner";
            this.ViewUserOwner.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewUserOwner.OptionsView.ShowGroupPanel = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(132, 202);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(256, 20);
            this.txtPassword.StyleController = this.LayoutControl;
            this.txtPassword.TabIndex = 10;
            // 
            // layoutControlItemPassword
            // 
            this.layoutControlItemPassword.Control = this.txtPassword;
            this.layoutControlItemPassword.CustomizationFormText = "Пароль";
            this.layoutControlItemPassword.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemPassword.Name = "layoutControlItemPassword";
            this.layoutControlItemPassword.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemPassword.Text = "Пароль:";
            this.layoutControlItemPassword.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(132, 178);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.txtEmail.Size = new System.Drawing.Size(256, 20);
            this.txtEmail.StyleController = this.LayoutControl;
            this.txtEmail.TabIndex = 11;
            // 
            // layoutControlItemEmail
            // 
            this.layoutControlItemEmail.Control = this.txtEmail;
            this.layoutControlItemEmail.CustomizationFormText = "Email";
            this.layoutControlItemEmail.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemEmail.Name = "layoutControlItemEmail";
            this.layoutControlItemEmail.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemEmail.Text = "Email:";
            this.layoutControlItemEmail.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlUserAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 325);
            this.Name = "ControlUserAccount";
            this.Size = new System.Drawing.Size(400, 325);
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUserOwner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEmail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemUserId;
        public DevExpress.XtraEditors.HyperLinkEdit txtEmail;
        public DevExpress.XtraEditors.ButtonEdit txtPassword;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemEmail;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemPassword;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbUserId;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewUserOwner;
    }
}
