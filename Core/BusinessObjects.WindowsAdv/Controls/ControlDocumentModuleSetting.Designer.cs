namespace BusinessObjects.Windows.Controls
{
    internal partial class ControlDocumentModuleSetting
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbUser = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtMemo = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMemo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemUser = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.cmbUser);
            this.layoutControl1.Controls.Add(this.txtCode);
            this.layoutControl1.Controls.Add(this.txtMemo);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(380, 220);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbUser
            // 
            this.cmbUser.Location = new System.Drawing.Point(83, 74);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUser.Properties.View = this.ViewUser;
            this.cmbUser.Size = new System.Drawing.Size(295, 20);
            this.cmbUser.StyleController = this.layoutControl1;
            this.cmbUser.TabIndex = 12;
            // 
            // ViewUser
            // 
            this.ViewUser.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewUser.Name = "ViewUser";
            this.ViewUser.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewUser.OptionsView.ShowGroupPanel = false;
            this.ViewUser.OptionsView.ShowIndicator = false;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(83, 50);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(295, 20);
            this.txtCode.StyleController = this.layoutControl1;
            this.txtCode.TabIndex = 6;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(83, 26);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(295, 20);
            this.txtMemo.StyleController = this.layoutControl1;
            this.txtMemo.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(83, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(295, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.layoutControlItemMemo,
            this.layoutControlItemCode,
            this.layoutControlItemUser,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(380, 220);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.CustomizationFormText = "Наименование:";
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(77, 13);
            this.layoutControlItemName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Control = this.txtMemo;
            this.layoutControlItemMemo.CustomizationFormText = "Описание:";
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemMemo.Name = "layoutControlItemMemo";
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemMemo.Text = "Описание:";
            this.layoutControlItemMemo.TextSize = new System.Drawing.Size(77, 13);
            this.layoutControlItemMemo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.Control = this.txtCode;
            this.layoutControlItemCode.CustomizationFormText = "Код:";
            this.layoutControlItemCode.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemCode.Name = "layoutControlItemCode";
            this.layoutControlItemCode.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemCode.Text = "Код:";
            this.layoutControlItemCode.TextSize = new System.Drawing.Size(77, 13);
            this.layoutControlItemCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemUser
            // 
            this.layoutControlItemUser.Control = this.cmbUser;
            this.layoutControlItemUser.CustomizationFormText = "Пользователь:";
            this.layoutControlItemUser.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemUser.Name = "layoutControlItemUser";
            this.layoutControlItemUser.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemUser.Text = "Пользователь:";
            this.layoutControlItemUser.TextSize = new System.Drawing.Size(77, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 96);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(380, 124);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ControlDocumentModuleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ControlDocumentModuleSetting";
            this.Size = new System.Drawing.Size(380, 220);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMemo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCode;
        public DevExpress.XtraEditors.TextEdit txtMemo;
        public DevExpress.XtraEditors.TextEdit txtCode;
        public DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUser;
        public DevExpress.XtraEditors.GridLookUpEdit cmbUser;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewUser;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
