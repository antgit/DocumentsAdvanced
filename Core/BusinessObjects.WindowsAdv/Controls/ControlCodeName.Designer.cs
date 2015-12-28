namespace BusinessObjects.Windows.Controls
{
    partial class ControlCodeName
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
            this.cmbToEntity = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemToEntity = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbApp = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemApp = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridSubKind = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbDocType = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemDocType = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbToEntity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbApp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocType)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 194);
            this.txtMemo.Size = new System.Drawing.Size(376, 72);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 92);
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
            this.LayoutControl.Controls.Add(this.cmbDocType);
            this.LayoutControl.Controls.Add(this.GridSubKind);
            this.LayoutControl.Controls.Add(this.cmbApp);
            this.LayoutControl.Controls.Add(this.cmbToEntity);
            this.LayoutControl.Size = new System.Drawing.Size(400, 506);
            this.LayoutControl.Controls.SetChildIndex(this.cmbToEntity, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbApp, 0);
            this.LayoutControl.Controls.SetChildIndex(this.GridSubKind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbDocType, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemToEntity,
            this.layoutControlItemApp,
            this.layoutControlItem1,
            this.layoutControlItemDocType});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 506);
            // 
            // cmbToEntity
            // 
            this.cmbToEntity.Location = new System.Drawing.Point(132, 270);
            this.cmbToEntity.Name = "cmbToEntity";
            this.cmbToEntity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToEntity.Size = new System.Drawing.Size(256, 20);
            this.cmbToEntity.StyleController = this.LayoutControl;
            this.cmbToEntity.TabIndex = 9;
            // 
            // layoutControlItemToEntity
            // 
            this.layoutControlItemToEntity.Control = this.cmbToEntity;
            this.layoutControlItemToEntity.CustomizationFormText = "Тип";
            this.layoutControlItemToEntity.Location = new System.Drawing.Point(0, 258);
            this.layoutControlItemToEntity.Name = "layoutControlItemToEntity";
            this.layoutControlItemToEntity.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemToEntity.Text = "Тип:";
            this.layoutControlItemToEntity.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbApp
            // 
            this.cmbApp.Location = new System.Drawing.Point(132, 84);
            this.cmbApp.Name = "cmbApp";
            this.cmbApp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbApp.Size = new System.Drawing.Size(256, 20);
            this.cmbApp.StyleController = this.LayoutControl;
            this.cmbApp.TabIndex = 10;
            // 
            // layoutControlItemApp
            // 
            this.layoutControlItemApp.Control = this.cmbApp;
            this.layoutControlItemApp.CustomizationFormText = "Приложение";
            this.layoutControlItemApp.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemApp.Name = "layoutControlItemApp";
            this.layoutControlItemApp.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemApp.Text = "Приложение:";
            this.layoutControlItemApp.TextSize = new System.Drawing.Size(116, 13);
            // 
            // GridSubKind
            // 
            this.GridSubKind.Location = new System.Drawing.Point(12, 334);
            this.GridSubKind.Name = "GridSubKind";
            this.GridSubKind.Size = new System.Drawing.Size(376, 160);
            this.GridSubKind.TabIndex = 11;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GridSubKind;
            this.layoutControlItem1.CustomizationFormText = "Допустимые типы";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 306);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 180);
            this.layoutControlItem1.Text = "Допустимые типы:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbDocType
            // 
            this.cmbDocType.Location = new System.Drawing.Point(132, 294);
            this.cmbDocType.Name = "cmbDocType";
            this.cmbDocType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDocType.Size = new System.Drawing.Size(256, 20);
            this.cmbDocType.StyleController = this.LayoutControl;
            this.cmbDocType.TabIndex = 12;
            // 
            // layoutControlItemDocType
            // 
            this.layoutControlItemDocType.Control = this.cmbDocType;
            this.layoutControlItemDocType.CustomizationFormText = "Тип документов";
            this.layoutControlItemDocType.Location = new System.Drawing.Point(0, 282);
            this.layoutControlItemDocType.Name = "layoutControlItemDocType";
            this.layoutControlItemDocType.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDocType.Text = "Тип документов:";
            this.layoutControlItemDocType.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlCodeName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 285);
            this.Name = "ControlCodeName";
            this.Size = new System.Drawing.Size(400, 506);
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbToEntity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbApp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDocType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDocType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.ComboBoxEdit cmbApp;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemApp;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemToEntity;
        public DevExpress.XtraEditors.LookUpEdit cmbToEntity;
        public ControlList GridSubKind;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraEditors.LookUpEdit cmbDocType;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDocType;
    }
}
