namespace BusinessObjects.Windows.Controls
{
    partial class ControlDataCatalog
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
            this.editDirectory = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlItemDirectoty = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbUserGroupId = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewUserGroupId = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemUserGroupId = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.editDirectory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDirectoty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserGroupId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUserGroupId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserGroupId)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 218);
            this.txtMemo.Size = new System.Drawing.Size(376, 25);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(136, 36);
            this.txtCode.Size = new System.Drawing.Size(252, 20);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(136, 12);
            this.txtName.Size = new System.Drawing.Size(252, 20);
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.TextSize = new System.Drawing.Size(120, 13);
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.TextSize = new System.Drawing.Size(120, 13);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 45);
            this.layoutControlItemMemo.TextSize = new System.Drawing.Size(120, 13);
            // 
            // txtCodeFind
            // 
            this.txtCodeFind.Location = new System.Drawing.Point(136, 60);
            this.txtCodeFind.Size = new System.Drawing.Size(252, 20);
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.TextSize = new System.Drawing.Size(120, 13);
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemNameFull2.TextSize = new System.Drawing.Size(120, 13);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 148);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbUserGroupId);
            this.LayoutControl.Controls.Add(this.editDirectory);
            this.LayoutControl.Controls.SetChildIndex(this.editDirectory, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbUserGroupId, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDirectoty,
            this.layoutControlItemUserGroupId});
            // 
            // editDirectory
            // 
            this.editDirectory.Location = new System.Drawing.Point(136, 84);
            this.editDirectory.Name = "editDirectory";
            this.editDirectory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.editDirectory.Size = new System.Drawing.Size(252, 20);
            this.editDirectory.StyleController = this.LayoutControl;
            this.editDirectory.TabIndex = 9;
            // 
            // layoutControlItemDirectoty
            // 
            this.layoutControlItemDirectoty.Control = this.editDirectory;
            this.layoutControlItemDirectoty.CustomizationFormText = "Каталог";
            this.layoutControlItemDirectoty.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemDirectoty.Name = "layoutControlItemDirectoty";
            this.layoutControlItemDirectoty.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDirectoty.Text = "Каталог:";
            this.layoutControlItemDirectoty.TextSize = new System.Drawing.Size(120, 13);
            // 
            // cmbUserGroupId
            // 
            this.cmbUserGroupId.Location = new System.Drawing.Point(136, 108);
            this.cmbUserGroupId.Name = "cmbUserGroupId";
            this.cmbUserGroupId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUserGroupId.Properties.View = this.ViewUserGroupId;
            this.cmbUserGroupId.Size = new System.Drawing.Size(252, 20);
            this.cmbUserGroupId.StyleController = this.LayoutControl;
            this.cmbUserGroupId.TabIndex = 10;
            // 
            // ViewUserGroupId
            // 
            this.ViewUserGroupId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewUserGroupId.Name = "ViewUserGroupId";
            this.ViewUserGroupId.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewUserGroupId.OptionsView.ShowGroupPanel = false;
            this.ViewUserGroupId.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItemUserGroupId
            // 
            this.layoutControlItemUserGroupId.Control = this.cmbUserGroupId;
            this.layoutControlItemUserGroupId.CustomizationFormText = "Группа пользователей:";
            this.layoutControlItemUserGroupId.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemUserGroupId.Name = "layoutControlItemUserGroupId";
            this.layoutControlItemUserGroupId.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemUserGroupId.Text = "Группа пользователей:";
            this.layoutControlItemUserGroupId.TextSize = new System.Drawing.Size(120, 13);
            // 
            // ControlDataCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlDataCatalog";
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
            ((System.ComponentModel.ISupportInitialize)(this.editDirectory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDirectoty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserGroupId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewUserGroupId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUserGroupId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDirectoty;
        public DevExpress.XtraEditors.ButtonEdit editDirectory;
        public DevExpress.XtraEditors.GridLookUpEdit cmbUserGroupId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUserGroupId;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewUserGroupId;
    }
}
