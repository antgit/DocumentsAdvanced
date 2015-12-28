namespace BusinessObjects.Windows.Controls
{
    partial class ControlDocumentSecure
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
            this.cmbRightId = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewRights = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemRightId = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbUserId = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemUserToId = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkAllow = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItemAllow = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItemMemo = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRightId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewRights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRightId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserToId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.dateEnd);
            this.LayoutControl.Controls.Add(this.dateStart);
            this.LayoutControl.Controls.Add(this.txtMemo);
            this.LayoutControl.Controls.Add(this.chkAllow);
            this.LayoutControl.Controls.Add(this.cmbUserId);
            this.LayoutControl.Controls.Add(this.cmbRightId);
            this.LayoutControl.Size = new System.Drawing.Size(380, 180);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemRightId,
            this.layoutControlItemUserToId,
            this.layoutControlItemAllow,
            this.layoutControlItemMemo,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup.Size = new System.Drawing.Size(380, 180);
            // 
            // cmbRightId
            // 
            this.cmbRightId.Location = new System.Drawing.Point(92, 12);
            this.cmbRightId.Name = "cmbRightId";
            this.cmbRightId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbRightId.Properties.View = this.ViewRights;
            this.cmbRightId.Size = new System.Drawing.Size(276, 20);
            this.cmbRightId.StyleController = this.LayoutControl;
            this.cmbRightId.TabIndex = 4;
            // 
            // ViewRights
            // 
            this.ViewRights.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewRights.Name = "ViewRights";
            this.ViewRights.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewRights.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItemRightId
            // 
            this.layoutControlItemRightId.Control = this.cmbRightId;
            this.layoutControlItemRightId.CustomizationFormText = "Разрешение";
            this.layoutControlItemRightId.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemRightId.Name = "layoutControlItemRightId";
            this.layoutControlItemRightId.Size = new System.Drawing.Size(360, 24);
            this.layoutControlItemRightId.Text = "Разрешение:";
            this.layoutControlItemRightId.TextSize = new System.Drawing.Size(76, 13);
            // 
            // cmbUserId
            // 
            this.cmbUserId.Location = new System.Drawing.Point(92, 36);
            this.cmbUserId.Name = "cmbUserId";
            this.cmbUserId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUserId.Properties.View = this.ViewUsers;
            this.cmbUserId.Size = new System.Drawing.Size(276, 20);
            this.cmbUserId.StyleController = this.LayoutControl;
            this.cmbUserId.TabIndex = 5;
            // 
            // ViewUsers
            // 
            this.ViewUsers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewUsers.Name = "ViewUsers";
            this.ViewUsers.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewUsers.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItemUserToId
            // 
            this.layoutControlItemUserToId.Control = this.cmbUserId;
            this.layoutControlItemUserToId.CustomizationFormText = "Пользователь";
            this.layoutControlItemUserToId.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemUserToId.Name = "layoutControlItemUserToId";
            this.layoutControlItemUserToId.Size = new System.Drawing.Size(360, 24);
            this.layoutControlItemUserToId.Text = "Пользователь:";
            this.layoutControlItemUserToId.TextSize = new System.Drawing.Size(76, 13);
            // 
            // chkAllow
            // 
            this.chkAllow.Location = new System.Drawing.Point(12, 108);
            this.chkAllow.Name = "chkAllow";
            this.chkAllow.Properties.Caption = "Разрешено/Запрешено";
            this.chkAllow.Size = new System.Drawing.Size(356, 19);
            this.chkAllow.StyleController = this.LayoutControl;
            this.chkAllow.TabIndex = 6;
            // 
            // layoutControlItemAllow
            // 
            this.layoutControlItemAllow.Control = this.chkAllow;
            this.layoutControlItemAllow.CustomizationFormText = "layoutControlItemAllow";
            this.layoutControlItemAllow.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemAllow.Name = "layoutControlItemAllow";
            this.layoutControlItemAllow.Size = new System.Drawing.Size(360, 23);
            this.layoutControlItemAllow.Text = "layoutControlItemAllow";
            this.layoutControlItemAllow.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAllow.TextToControlDistance = 0;
            this.layoutControlItemAllow.TextVisible = false;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 147);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(356, 21);
            this.txtMemo.StyleController = this.LayoutControl;
            this.txtMemo.TabIndex = 7;
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Control = this.txtMemo;
            this.layoutControlItemMemo.CustomizationFormText = "Примечание";
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 119);
            this.layoutControlItemMemo.Name = "layoutControlItemMemo";
            this.layoutControlItemMemo.Size = new System.Drawing.Size(360, 41);
            this.layoutControlItemMemo.Text = "Примечание:";
            this.layoutControlItemMemo.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemMemo.TextSize = new System.Drawing.Size(76, 13);
            // 
            // dateStart
            // 
            this.dateStart.EditValue = null;
            this.dateStart.Location = new System.Drawing.Point(92, 60);
            this.dateStart.Name = "dateStart";
            this.dateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateStart.Size = new System.Drawing.Size(276, 20);
            this.dateStart.StyleController = this.LayoutControl;
            this.dateStart.TabIndex = 8;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateStart;
            this.layoutControlItem1.CustomizationFormText = "Дествует с";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(360, 24);
            this.layoutControlItem1.Text = "Дествует с:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 13);
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(92, 84);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEnd.Size = new System.Drawing.Size(276, 20);
            this.dateEnd.StyleController = this.LayoutControl;
            this.dateEnd.TabIndex = 9;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEnd;
            this.layoutControlItem2.CustomizationFormText = "Дествует по";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(360, 24);
            this.layoutControlItem2.Text = "Дествует по:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 13);
            // 
            // ControlDocumentSecure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MaximumSize = new System.Drawing.Size(380, 180);
            this.Name = "ControlDocumentSecure";
            this.Size = new System.Drawing.Size(380, 180);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRightId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewRights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRightId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserToId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemMemo;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemRightId;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemUserToId;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemAllow;
        public DevExpress.XtraEditors.CheckEdit chkAllow;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbRightId;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbUserId;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewRights;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewUsers;
        public DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private DevExpress.XtraEditors.DateEdit dateStart;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
