namespace BusinessObjects.Windows.Controls
{
    partial class ControlStorageCell
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
            this.calcQty = new DevExpress.XtraEditors.CalcEdit();
            this.layoutControlItemQty = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbUnits = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemUnit = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbStore = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemcmbStoreId = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.calcQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnits.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemcmbStoreId)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 242);
            this.txtMemo.Size = new System.Drawing.Size(376, 66);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbStore);
            this.LayoutControl.Controls.Add(this.cmbUnits);
            this.LayoutControl.Controls.Add(this.calcQty);
            this.LayoutControl.Size = new System.Drawing.Size(400, 320);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.calcQty, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbUnits, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbStore, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemQty,
            this.layoutControlItemUnit,
            this.layoutControlItemcmbStoreId});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 320);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 86);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // calcQty
            // 
            this.calcQty.Location = new System.Drawing.Point(132, 202);
            this.calcQty.Name = "calcQty";
            this.calcQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcQty.Size = new System.Drawing.Size(256, 20);
            this.calcQty.StyleController = this.LayoutControl;
            this.calcQty.TabIndex = 9;
            // 
            // layoutControlItemQty
            // 
            this.layoutControlItemQty.Control = this.calcQty;
            this.layoutControlItemQty.CustomizationFormText = "Вместимость";
            this.layoutControlItemQty.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemQty.Name = "layoutControlItemQty";
            this.layoutControlItemQty.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemQty.Text = "Вместимость:";
            this.layoutControlItemQty.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbUnits
            // 
            this.cmbUnits.Location = new System.Drawing.Point(132, 154);
            this.cmbUnits.Name = "cmbUnits";
            this.cmbUnits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUnits.Size = new System.Drawing.Size(256, 20);
            this.cmbUnits.StyleController = this.LayoutControl;
            this.cmbUnits.TabIndex = 10;
            // 
            // layoutControlItemUnit
            // 
            this.layoutControlItemUnit.Control = this.cmbUnits;
            this.layoutControlItemUnit.CustomizationFormText = "Единица";
            this.layoutControlItemUnit.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemUnit.Name = "layoutControlItemUnit";
            this.layoutControlItemUnit.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemUnit.Text = "Единица:";
            this.layoutControlItemUnit.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbStore
            // 
            this.cmbStore.Location = new System.Drawing.Point(132, 178);
            this.cmbStore.Name = "cmbStore";
            this.cmbStore.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbStore.Size = new System.Drawing.Size(256, 20);
            this.cmbStore.StyleController = this.LayoutControl;
            this.cmbStore.TabIndex = 11;
            // 
            // layoutControlItemcmbStoreId
            // 
            this.layoutControlItemcmbStoreId.Control = this.cmbStore;
            this.layoutControlItemcmbStoreId.CustomizationFormText = "Склад";
            this.layoutControlItemcmbStoreId.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemcmbStoreId.Name = "layoutControlItemcmbStoreId";
            this.layoutControlItemcmbStoreId.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemcmbStoreId.Text = "Склад:";
            this.layoutControlItemcmbStoreId.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlStorageCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 320);
            this.Name = "ControlStorageCell";
            this.Size = new System.Drawing.Size(400, 320);
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
            ((System.ComponentModel.ISupportInitialize)(this.calcQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnits.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemcmbStoreId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemQty;
        public DevExpress.XtraEditors.CalcEdit calcQty;
        public DevExpress.XtraEditors.LookUpEdit cmbUnits;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemUnit;
        public DevExpress.XtraEditors.LookUpEdit cmbStore;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemcmbStoreId;
    }
}
