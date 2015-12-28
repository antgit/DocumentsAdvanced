namespace BusinessObjects.Windows.Controls
{
    partial class ControlLogin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbUsername = new DevExpress.XtraEditors.LabelControl();
            this.lbUsepsw = new DevExpress.XtraEditors.LabelControl();
            this.txtUserId = new DevExpress.XtraEditors.TextEdit();
            this.txtPsw = new DevExpress.XtraEditors.TextEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lbHeader = new DevExpress.XtraEditors.LabelControl();
            this.chkIntegrated = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPsw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIntegrated.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUsername
            // 
            this.lbUsername.Location = new System.Drawing.Point(4, 40);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(76, 13);
            this.lbUsername.TabIndex = 0;
            this.lbUsername.Text = "Пользователь:";
            // 
            // lbUsepsw
            // 
            this.lbUsepsw.Location = new System.Drawing.Point(4, 69);
            this.lbUsepsw.Name = "lbUsepsw";
            this.lbUsepsw.Size = new System.Drawing.Size(41, 13);
            this.lbUsepsw.TabIndex = 1;
            this.lbUsepsw.Text = "Пароль:";
            // 
            // txtUserId
            // 
            this.txtUserId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserId.Location = new System.Drawing.Point(86, 40);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(241, 20);
            this.txtUserId.TabIndex = 2;
            // 
            // txtPsw
            // 
            this.txtPsw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPsw.Location = new System.Drawing.Point(86, 66);
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.Properties.PasswordChar = '*';
            this.txtPsw.Size = new System.Drawing.Size(241, 20);
            this.txtPsw.TabIndex = 2;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(3, 3);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Size = new System.Drawing.Size(33, 33);
            this.pictureEdit1.TabIndex = 3;
            // 
            // lbHeader
            // 
            this.lbHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbHeader.Appearance.Options.UseFont = true;
            this.lbHeader.Location = new System.Drawing.Point(86, 14);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(85, 13);
            this.lbHeader.TabIndex = 4;
            this.lbHeader.Text = "Авторизация...";
            // 
            // chkIntegrated
            // 
            this.chkIntegrated.Location = new System.Drawing.Point(3, 89);
            this.chkIntegrated.Name = "chkIntegrated";
            this.chkIntegrated.Properties.Caption = "Интегрированная аутентификация";
            this.chkIntegrated.Size = new System.Drawing.Size(226, 18);
            this.chkIntegrated.TabIndex = 5;
            // 
            // ControlLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkIntegrated);
            this.Controls.Add(this.lbHeader);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.txtPsw);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.lbUsepsw);
            this.Controls.Add(this.lbUsername);
            this.MinimumSize = new System.Drawing.Size(330, 115);
            this.Name = "ControlLogin";
            this.Size = new System.Drawing.Size(330, 115);
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPsw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIntegrated.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txtUserId;
        public DevExpress.XtraEditors.TextEdit txtPsw;
        public DevExpress.XtraEditors.PictureEdit pictureEdit1;
        public DevExpress.XtraEditors.LabelControl lbHeader;
        public DevExpress.XtraEditors.LabelControl lbUsepsw;
        public DevExpress.XtraEditors.LabelControl lbUsername;
        public DevExpress.XtraEditors.CheckEdit chkIntegrated;
    }
}
