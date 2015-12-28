namespace BusinessObjects.Windows.Controls
{
    partial class ControlSqlServerConnection
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
            this.LayoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.cmbDataBase = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.cmbServer = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.chkSql = new DevExpress.XtraEditors.CheckEdit();
            this.chkWindows = new DevExpress.XtraEditors.CheckEdit();
            this.txtUserId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemServer = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemUserId = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemPass = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDataBase = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataBase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSql.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWindows.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDataBase)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.AllowCustomizationMenu = false;
            this.LayoutControl.Controls.Add(this.cmbDataBase);
            this.LayoutControl.Controls.Add(this.txtPassword);
            this.LayoutControl.Controls.Add(this.cmbServer);
            this.LayoutControl.Controls.Add(this.txtName);
            this.LayoutControl.Controls.Add(this.chkSql);
            this.LayoutControl.Controls.Add(this.chkWindows);
            this.LayoutControl.Controls.Add(this.txtUserId);
            this.LayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutControl.Location = new System.Drawing.Point(0, 0);
            this.LayoutControl.Name = "LayoutControl";
            this.LayoutControl.Root = this.layoutControlGroup1;
            this.LayoutControl.Size = new System.Drawing.Size(350, 235);
            this.LayoutControl.TabIndex = 0;
            this.LayoutControl.Text = "layoutControl1";
            // 
            // cmbDataBase
            // 
            this.cmbDataBase.Location = new System.Drawing.Point(93, 196);
            this.cmbDataBase.Name = "cmbDataBase";
            this.cmbDataBase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDataBase.Size = new System.Drawing.Size(245, 20);
            this.cmbDataBase.StyleController = this.LayoutControl;
            this.cmbDataBase.TabIndex = 9;
            this.cmbDataBase.Popup += new System.EventHandler(this.cmbDataBase_Popup);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(93, 172);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(245, 20);
            this.txtPassword.StyleController = this.LayoutControl;
            this.txtPassword.TabIndex = 8;
            // 
            // cmbServer
            // 
            this.cmbServer.Location = new System.Drawing.Point(93, 36);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbServer.Size = new System.Drawing.Size(245, 20);
            this.cmbServer.StyleController = this.LayoutControl;
            this.cmbServer.TabIndex = 5;
            this.cmbServer.Popup += new System.EventHandler(this.cmbServer_Popup);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(93, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(245, 20);
            this.txtName.StyleController = this.LayoutControl;
            this.txtName.TabIndex = 4;
            // 
            // chkSql
            // 
            this.chkSql.Location = new System.Drawing.Point(24, 92);
            this.chkSql.Name = "chkSql";
            this.chkSql.Properties.Caption = "Sql Server";
            this.chkSql.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkSql.Size = new System.Drawing.Size(302, 18);
            this.chkSql.StyleController = this.LayoutControl;
            this.chkSql.TabIndex = 6;
            this.chkSql.CheckedChanged += new System.EventHandler(this.chkSql_CheckedChanged);
            // 
            // chkWindows
            // 
            this.chkWindows.Location = new System.Drawing.Point(24, 114);
            this.chkWindows.Name = "chkWindows";
            this.chkWindows.Properties.Caption = "Windows";
            this.chkWindows.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkWindows.Size = new System.Drawing.Size(302, 18);
            this.chkWindows.StyleController = this.LayoutControl;
            this.chkWindows.TabIndex = 6;
            this.chkWindows.CheckedChanged += new System.EventHandler(this.chkWindows_CheckedChanged);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(93, 148);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(245, 20);
            this.txtUserId.StyleController = this.LayoutControl;
            this.txtUserId.TabIndex = 7;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Основные";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.layoutControlItemServer,
            this.layoutControlGroup2,
            this.layoutControlItemUserId,
            this.layoutControlItemPass,
            this.layoutControlItemDataBase});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(350, 235);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Основные";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.CustomizationFormText = "Наименование";
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItemServer
            // 
            this.layoutControlItemServer.Control = this.cmbServer;
            this.layoutControlItemServer.CustomizationFormText = "Сервер";
            this.layoutControlItemServer.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemServer.Name = "layoutControlItemServer";
            this.layoutControlItemServer.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemServer.Text = "Сервер:";
            this.layoutControlItemServer.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Аутентификация";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(330, 88);
            this.layoutControlGroup2.Text = "Аутентификация";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkSql;
            this.layoutControlItem3.CustomizationFormText = "SQL Server авторизация";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(306, 22);
            this.layoutControlItem3.Text = "SQL Server авторизация";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkWindows;
            this.layoutControlItem4.CustomizationFormText = "Windows авторизация";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 22);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(306, 22);
            this.layoutControlItem4.Text = "Windows авторизация";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItemUserId
            // 
            this.layoutControlItemUserId.Control = this.txtUserId;
            this.layoutControlItemUserId.CustomizationFormText = "Пользователь";
            this.layoutControlItemUserId.Location = new System.Drawing.Point(0, 136);
            this.layoutControlItemUserId.Name = "layoutControlItemUserId";
            this.layoutControlItemUserId.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemUserId.Text = "Пользователь:";
            this.layoutControlItemUserId.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItemPass
            // 
            this.layoutControlItemPass.Control = this.txtPassword;
            this.layoutControlItemPass.CustomizationFormText = "Пароль";
            this.layoutControlItemPass.Location = new System.Drawing.Point(0, 160);
            this.layoutControlItemPass.Name = "layoutControlItemPass";
            this.layoutControlItemPass.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemPass.Text = "Пароль:";
            this.layoutControlItemPass.TextSize = new System.Drawing.Size(77, 13);
            // 
            // layoutControlItemDataBase
            // 
            this.layoutControlItemDataBase.Control = this.cmbDataBase;
            this.layoutControlItemDataBase.CustomizationFormText = "База данных";
            this.layoutControlItemDataBase.Location = new System.Drawing.Point(0, 184);
            this.layoutControlItemDataBase.Name = "layoutControlItemDataBase";
            this.layoutControlItemDataBase.Size = new System.Drawing.Size(330, 31);
            this.layoutControlItemDataBase.Text = "База данных:";
            this.layoutControlItemDataBase.TextSize = new System.Drawing.Size(77, 13);
            // 
            // ControlSqlServerConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LayoutControl);
            this.MinimumSize = new System.Drawing.Size(350, 235);
            this.Name = "ControlSqlServerConnection";
            this.Size = new System.Drawing.Size(350, 235);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbDataBase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSql.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWindows.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDataBase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbServer;
        private DevExpress.XtraEditors.CheckEdit chkSql;
        private DevExpress.XtraEditors.CheckEdit chkWindows;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemServer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDataBase;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUserId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUserId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDataBase;
        public DevExpress.XtraEditors.TextEdit txtName;
        public DevExpress.XtraLayout.LayoutControl LayoutControl;
    }
}
