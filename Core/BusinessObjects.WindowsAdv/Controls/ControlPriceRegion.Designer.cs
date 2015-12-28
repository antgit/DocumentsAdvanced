namespace BusinessObjects.Windows.Controls
{
    partial class ControlPriceRegion
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
            this.dtStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemDateStart = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtEnd = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemDateEnd = new DevExpress.XtraLayout.LayoutControlItem();
            this.edValue = new DevExpress.XtraEditors.CalcEdit();
            this.layoutControlItemValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.edValueMin = new DevExpress.XtraEditors.CalcEdit();
            this.layoutControlItemValueMin = new DevExpress.XtraLayout.LayoutControlItem();
            this.edValueMax = new DevExpress.XtraEditors.CalcEdit();
            this.layoutControlItemValueMax = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbPriceName = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemPriceName = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbProduct = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemProduct = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edValueMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValueMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edValueMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValueMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPriceName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPriceName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 338);
            this.txtMemo.Size = new System.Drawing.Size(376, 92);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 310);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 112);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 240);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 268);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbProduct);
            this.LayoutControl.Controls.Add(this.cmbPriceName);
            this.LayoutControl.Controls.Add(this.edValueMax);
            this.LayoutControl.Controls.Add(this.edValueMin);
            this.LayoutControl.Controls.Add(this.edValue);
            this.LayoutControl.Controls.Add(this.dtEnd);
            this.LayoutControl.Controls.Add(this.dtStart);
            this.LayoutControl.Size = new System.Drawing.Size(400, 442);
            this.LayoutControl.Controls.SetChildIndex(this.dtStart, 0);
            this.LayoutControl.Controls.SetChildIndex(this.dtEnd, 0);
            this.LayoutControl.Controls.SetChildIndex(this.edValue, 0);
            this.LayoutControl.Controls.SetChildIndex(this.edValueMin, 0);
            this.LayoutControl.Controls.SetChildIndex(this.edValueMax, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbPriceName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbProduct, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDateStart,
            this.layoutControlItemDateEnd,
            this.layoutControlItemValue,
            this.layoutControlItemValueMin,
            this.layoutControlItemValueMax,
            this.layoutControlItemPriceName,
            this.layoutControlItemProduct});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 442);
            // 
            // dtStart
            // 
            this.dtStart.EditValue = null;
            this.dtStart.Location = new System.Drawing.Point(132, 132);
            this.dtStart.Name = "dtStart";
            this.dtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtStart.Size = new System.Drawing.Size(256, 20);
            this.dtStart.StyleController = this.LayoutControl;
            this.dtStart.TabIndex = 9;
            // 
            // layoutControlItemDateStart
            // 
            this.layoutControlItemDateStart.Control = this.dtStart;
            this.layoutControlItemDateStart.CustomizationFormText = "Начало";
            this.layoutControlItemDateStart.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemDateStart.Name = "layoutControlItemDateStart";
            this.layoutControlItemDateStart.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDateStart.Text = "Начало:";
            this.layoutControlItemDateStart.TextSize = new System.Drawing.Size(116, 13);
            // 
            // dtEnd
            // 
            this.dtEnd.EditValue = null;
            this.dtEnd.Location = new System.Drawing.Point(132, 156);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEnd.Size = new System.Drawing.Size(256, 20);
            this.dtEnd.StyleController = this.LayoutControl;
            this.dtEnd.TabIndex = 10;
            // 
            // layoutControlItemDateEnd
            // 
            this.layoutControlItemDateEnd.Control = this.dtEnd;
            this.layoutControlItemDateEnd.CustomizationFormText = "Конец";
            this.layoutControlItemDateEnd.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItemDateEnd.Name = "layoutControlItemDateEnd";
            this.layoutControlItemDateEnd.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDateEnd.Text = "Конец:";
            this.layoutControlItemDateEnd.TextSize = new System.Drawing.Size(116, 13);
            // 
            // edValue
            // 
            this.edValue.Location = new System.Drawing.Point(132, 180);
            this.edValue.Name = "edValue";
            this.edValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edValue.Size = new System.Drawing.Size(256, 20);
            this.edValue.StyleController = this.LayoutControl;
            this.edValue.TabIndex = 11;
            // 
            // layoutControlItemValue
            // 
            this.layoutControlItemValue.Control = this.edValue;
            this.layoutControlItemValue.CustomizationFormText = "Цена";
            this.layoutControlItemValue.Location = new System.Drawing.Point(0, 168);
            this.layoutControlItemValue.Name = "layoutControlItemValue";
            this.layoutControlItemValue.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemValue.Text = "Цена:";
            this.layoutControlItemValue.TextSize = new System.Drawing.Size(116, 13);
            // 
            // edValueMin
            // 
            this.edValueMin.Location = new System.Drawing.Point(132, 204);
            this.edValueMin.Name = "edValueMin";
            this.edValueMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edValueMin.Size = new System.Drawing.Size(256, 20);
            this.edValueMin.StyleController = this.LayoutControl;
            this.edValueMin.TabIndex = 12;
            // 
            // layoutControlItemValueMin
            // 
            this.layoutControlItemValueMin.Control = this.edValueMin;
            this.layoutControlItemValueMin.CustomizationFormText = "Минимальная цена";
            this.layoutControlItemValueMin.Location = new System.Drawing.Point(0, 192);
            this.layoutControlItemValueMin.Name = "layoutControlItemValueMin";
            this.layoutControlItemValueMin.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemValueMin.Text = "Минимальная цена:";
            this.layoutControlItemValueMin.TextSize = new System.Drawing.Size(116, 13);
            // 
            // edValueMax
            // 
            this.edValueMax.Location = new System.Drawing.Point(132, 228);
            this.edValueMax.Name = "edValueMax";
            this.edValueMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edValueMax.Size = new System.Drawing.Size(256, 20);
            this.edValueMax.StyleController = this.LayoutControl;
            this.edValueMax.TabIndex = 13;
            // 
            // layoutControlItemValueMax
            // 
            this.layoutControlItemValueMax.Control = this.edValueMax;
            this.layoutControlItemValueMax.CustomizationFormText = "Максимальная цена";
            this.layoutControlItemValueMax.Location = new System.Drawing.Point(0, 216);
            this.layoutControlItemValueMax.Name = "layoutControlItemValueMax";
            this.layoutControlItemValueMax.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemValueMax.Text = "Максимальная цена:";
            this.layoutControlItemValueMax.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbPriceName
            // 
            this.cmbPriceName.Location = new System.Drawing.Point(132, 84);
            this.cmbPriceName.Name = "cmbPriceName";
            this.cmbPriceName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPriceName.Size = new System.Drawing.Size(256, 20);
            this.cmbPriceName.StyleController = this.LayoutControl;
            this.cmbPriceName.TabIndex = 14;
            // 
            // layoutControlItemPriceName
            // 
            this.layoutControlItemPriceName.Control = this.cmbPriceName;
            this.layoutControlItemPriceName.CustomizationFormText = "Вид цены";
            this.layoutControlItemPriceName.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemPriceName.Name = "layoutControlItemPriceName";
            this.layoutControlItemPriceName.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemPriceName.Text = "Вид цены:";
            this.layoutControlItemPriceName.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbProduct
            // 
            this.cmbProduct.Location = new System.Drawing.Point(132, 108);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProduct.Size = new System.Drawing.Size(256, 20);
            this.cmbProduct.StyleController = this.LayoutControl;
            this.cmbProduct.TabIndex = 15;
            // 
            // layoutControlItemProduct
            // 
            this.layoutControlItemProduct.Control = this.cmbProduct;
            this.layoutControlItemProduct.CustomizationFormText = "Товар";
            this.layoutControlItemProduct.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemProduct.Name = "layoutControlItemProduct";
            this.layoutControlItemProduct.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemProduct.Text = "Товар:";
            this.layoutControlItemProduct.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlPriceRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlPriceRegion";
            this.Size = new System.Drawing.Size(400, 442);
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
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edValueMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValueMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edValueMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValueMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPriceName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPriceName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.DateEdit dtStart;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateStart;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateEnd;
        public DevExpress.XtraEditors.DateEdit dtEnd;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemValue;
        public DevExpress.XtraEditors.CalcEdit edValue;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemValueMin;
        public DevExpress.XtraEditors.CalcEdit edValueMin;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemValueMax;
        public DevExpress.XtraEditors.CalcEdit edValueMax;
        public DevExpress.XtraEditors.LookUpEdit cmbProduct;
        public DevExpress.XtraEditors.LookUpEdit cmbPriceName;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemPriceName;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemProduct;
    }
}
