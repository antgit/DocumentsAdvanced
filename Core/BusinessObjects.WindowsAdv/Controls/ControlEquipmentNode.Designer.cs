namespace BusinessObjects.Windows.Controls
{
    partial class ControlEquipmentNode
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
            this.txtDrawingNumber = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemDrawingNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtDrawingAssemblyNumber = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemDrawingAssemblyNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.edWeight = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItemWeight = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtDrawingNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDrawingNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDrawingAssemblyNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDrawingAssemblyNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 242);
            this.txtMemo.Size = new System.Drawing.Size(376, 56);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(162, 36);
            this.txtCode.Size = new System.Drawing.Size(226, 20);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(162, 12);
            this.txtName.Size = new System.Drawing.Size(226, 20);
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Size = new System.Drawing.Size(363, 24);
            this.layoutControlItemName.TextSize = new System.Drawing.Size(146, 13);
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.Size = new System.Drawing.Size(363, 24);
            this.layoutControlItemCode.TextSize = new System.Drawing.Size(146, 13);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(363, 36);
            this.layoutControlItemMemo.TextSize = new System.Drawing.Size(146, 13);
            // 
            // txtCodeFind
            // 
            this.txtCodeFind.Location = new System.Drawing.Point(162, 60);
            this.txtCodeFind.Size = new System.Drawing.Size(226, 20);
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.Size = new System.Drawing.Size(363, 24);
            this.layoutControlItemCodeFind.TextSize = new System.Drawing.Size(146, 13);
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Size = new System.Drawing.Size(363, 70);
            this.layoutControlItemNameFull2.TextSize = new System.Drawing.Size(146, 13);
            // 
            // txtNameFull2
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.edWeight);
            this.LayoutControl.Controls.Add(this.txtDrawingAssemblyNumber);
            this.LayoutControl.Controls.Add(this.txtDrawingNumber);
            this.LayoutControl.Size = new System.Drawing.Size(400, 310);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtDrawingNumber, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtDrawingAssemblyNumber, 0);
            this.LayoutControl.Controls.SetChildIndex(this.edWeight, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDrawingNumber,
            this.layoutControlItemDrawingAssemblyNumber,
            this.layoutControlItemWeight});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 310);
            // 
            // txtDrawingNumber
            // 
            this.txtDrawingNumber.Location = new System.Drawing.Point(162, 154);
            this.txtDrawingNumber.Name = "txtDrawingNumber";
            this.txtDrawingNumber.Size = new System.Drawing.Size(226, 20);
            this.txtDrawingNumber.StyleController = this.LayoutControl;
            this.txtDrawingNumber.TabIndex = 12;
            // 
            // layoutControlItemDrawingNumber
            // 
            this.layoutControlItemDrawingNumber.Control = this.txtDrawingNumber;
            this.layoutControlItemDrawingNumber.CustomizationFormText = " Номер чертежа";
            this.layoutControlItemDrawingNumber.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemDrawingNumber.Name = "layoutControlItemDrawingNumber";
            this.layoutControlItemDrawingNumber.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDrawingNumber.Text = " Номер чертежа:";
            this.layoutControlItemDrawingNumber.TextSize = new System.Drawing.Size(146, 13);
            // 
            // txtDrawingAssemblyNumber
            // 
            this.txtDrawingAssemblyNumber.Location = new System.Drawing.Point(162, 178);
            this.txtDrawingAssemblyNumber.Name = "txtDrawingAssemblyNumber";
            this.txtDrawingAssemblyNumber.Size = new System.Drawing.Size(226, 20);
            this.txtDrawingAssemblyNumber.StyleController = this.LayoutControl;
            this.txtDrawingAssemblyNumber.TabIndex = 13;
            // 
            // layoutControlItemDrawingAssemblyNumber
            // 
            this.layoutControlItemDrawingAssemblyNumber.Control = this.txtDrawingAssemblyNumber;
            this.layoutControlItemDrawingAssemblyNumber.CustomizationFormText = " Номер сборочного чертежа";
            this.layoutControlItemDrawingAssemblyNumber.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemDrawingAssemblyNumber.Name = "layoutControlItemDrawingAssemblyNumber";
            this.layoutControlItemDrawingAssemblyNumber.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDrawingAssemblyNumber.Text = " Номер сборочного чертежа:";
            this.layoutControlItemDrawingAssemblyNumber.TextSize = new System.Drawing.Size(146, 13);
            // 
            // edWeight
            // 
            this.edWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edWeight.Location = new System.Drawing.Point(162, 202);
            this.edWeight.Name = "edWeight";
            this.edWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edWeight.Size = new System.Drawing.Size(226, 20);
            this.edWeight.StyleController = this.LayoutControl;
            this.edWeight.TabIndex = 14;
            // 
            // layoutControlItemWeight
            // 
            this.layoutControlItemWeight.Control = this.edWeight;
            this.layoutControlItemWeight.CustomizationFormText = "Вес";
            this.layoutControlItemWeight.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemWeight.Name = "layoutControlItemWeight";
            this.layoutControlItemWeight.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemWeight.Text = "Вес:";
            this.layoutControlItemWeight.TextSize = new System.Drawing.Size(146, 13);
            // 
            // ControlEquipmentNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 310);
            this.Name = "ControlEquipmentNode";
            this.Size = new System.Drawing.Size(400, 310);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtDrawingNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDrawingNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDrawingAssemblyNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDrawingAssemblyNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemWeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txtDrawingNumber;
        public DevExpress.XtraEditors.TextEdit txtDrawingAssemblyNumber;
        public DevExpress.XtraEditors.SpinEdit edWeight;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDrawingNumber;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDrawingAssemblyNumber;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemWeight;
    }
}
