namespace BusinessObjects.Windows.Controls
{
    partial class ControlRuleset
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
            this.cmbLibrary = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemLibrary = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemType = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLibrary.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLibrary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemType)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 218);
            this.txtMemo.Size = new System.Drawing.Size(376, 55);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbType);
            this.LayoutControl.Controls.Add(this.cmbLibrary);
            this.LayoutControl.Size = new System.Drawing.Size(400, 285);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbLibrary, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbType, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemLibrary,
            this.layoutControlItemType});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 285);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 75);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // cmbLibrary
            // 
            this.cmbLibrary.Location = new System.Drawing.Point(132, 154);
            this.cmbLibrary.Name = "cmbLibrary";
            this.cmbLibrary.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLibrary.Size = new System.Drawing.Size(256, 20);
            this.cmbLibrary.StyleController = this.LayoutControl;
            this.cmbLibrary.TabIndex = 7;
            // 
            // layoutControlItemLibrary
            // 
            this.layoutControlItemLibrary.Control = this.cmbLibrary;
            this.layoutControlItemLibrary.CustomizationFormText = "Библиотека";
            this.layoutControlItemLibrary.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemLibrary.Name = "layoutControlItemLibrary";
            this.layoutControlItemLibrary.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemLibrary.Text = "Библиотека:";
            this.layoutControlItemLibrary.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(132, 178);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(256, 20);
            this.cmbType.StyleController = this.LayoutControl;
            this.cmbType.TabIndex = 8;
            // 
            // layoutControlItemType
            // 
            this.layoutControlItemType.Control = this.cmbType;
            this.layoutControlItemType.CustomizationFormText = "Тип";
            this.layoutControlItemType.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemType.Name = "layoutControlItemType";
            this.layoutControlItemType.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemType.Text = "Тип:";
            this.layoutControlItemType.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlRuleset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 285);
            this.Name = "ControlRuleset";
            this.Size = new System.Drawing.Size(400, 285);
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLibrary.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemLibrary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.LookUpEdit cmbLibrary;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemLibrary;
        public DevExpress.XtraEditors.ComboBoxEdit cmbType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemType;
    }
}
