namespace BusinessObjects.Windows.Controls
{
    partial class ControlProductUnit
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
            this.numSort = new DevExpress.XtraEditors.SpinEdit();
            this.numDivider = new DevExpress.XtraEditors.SpinEdit();
            this.numMultiply = new DevExpress.XtraEditors.SpinEdit();
            this.cmbUnit = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbProduct = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemProduct = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemUnit = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSupply = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemDivider = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemOrderNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDivider.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMultiply.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSupply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDivider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOrderNo)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.numSort);
            this.LayoutControl.Controls.Add(this.numDivider);
            this.LayoutControl.Controls.Add(this.numMultiply);
            this.LayoutControl.Controls.Add(this.cmbUnit);
            this.LayoutControl.Controls.Add(this.cmbProduct);
            this.LayoutControl.Size = new System.Drawing.Size(350, 140);
            // 
            // numSort
            // 
            this.numSort.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSort.Location = new System.Drawing.Point(119, 108);
            this.numSort.Name = "numSort";
            this.numSort.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numSort.Size = new System.Drawing.Size(219, 20);
            this.numSort.StyleController = this.LayoutControl;
            this.numSort.TabIndex = 8;
            // 
            // numDivider
            // 
            this.numDivider.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDivider.Location = new System.Drawing.Point(119, 84);
            this.numDivider.Name = "numDivider";
            this.numDivider.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numDivider.Size = new System.Drawing.Size(219, 20);
            this.numDivider.StyleController = this.LayoutControl;
            this.numDivider.TabIndex = 7;
            // 
            // numMultiply
            // 
            this.numMultiply.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMultiply.Location = new System.Drawing.Point(119, 60);
            this.numMultiply.Name = "numMultiply";
            this.numMultiply.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMultiply.Size = new System.Drawing.Size(219, 20);
            this.numMultiply.StyleController = this.LayoutControl;
            this.numMultiply.TabIndex = 6;
            // 
            // cmbUnit
            // 
            this.cmbUnit.Location = new System.Drawing.Point(119, 36);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUnit.Size = new System.Drawing.Size(219, 20);
            this.cmbUnit.StyleController = this.LayoutControl;
            this.cmbUnit.TabIndex = 5;
            // 
            // cmbProduct
            // 
            this.cmbProduct.Location = new System.Drawing.Point(119, 12);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProduct.Size = new System.Drawing.Size(219, 20);
            this.cmbProduct.StyleController = this.LayoutControl;
            this.cmbProduct.TabIndex = 4;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemProduct,
            this.layoutControlItemUnit,
            this.layoutControlItemSupply,
            this.layoutControlItemDivider,
            this.layoutControlItemOrderNo});
            this.layoutControlGroup.Size = new System.Drawing.Size(350, 140);
            this.layoutControlGroup.Text = "Основные";
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlItemProduct
            // 
            this.layoutControlItemProduct.Control = this.cmbProduct;
            this.layoutControlItemProduct.CustomizationFormText = "Товар";
            this.layoutControlItemProduct.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemProduct.Name = "layoutControlItemProduct";
            this.layoutControlItemProduct.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemProduct.Text = "Товар:";
            this.layoutControlItemProduct.TextSize = new System.Drawing.Size(103, 13);
            // 
            // layoutControlItemUnit
            // 
            this.layoutControlItemUnit.Control = this.cmbUnit;
            this.layoutControlItemUnit.CustomizationFormText = "Единица измерения";
            this.layoutControlItemUnit.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemUnit.Name = "layoutControlItemUnit";
            this.layoutControlItemUnit.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemUnit.Text = "Единица измерения:";
            this.layoutControlItemUnit.TextSize = new System.Drawing.Size(103, 13);
            // 
            // layoutControlItemSupply
            // 
            this.layoutControlItemSupply.Control = this.numMultiply;
            this.layoutControlItemSupply.CustomizationFormText = "Множитель";
            this.layoutControlItemSupply.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemSupply.Name = "layoutControlItemSupply";
            this.layoutControlItemSupply.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemSupply.Text = "Множитель:";
            this.layoutControlItemSupply.TextSize = new System.Drawing.Size(103, 13);
            // 
            // layoutControlItemDivider
            // 
            this.layoutControlItemDivider.Control = this.numDivider;
            this.layoutControlItemDivider.CustomizationFormText = "Делитель";
            this.layoutControlItemDivider.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemDivider.Name = "layoutControlItemDivider";
            this.layoutControlItemDivider.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemDivider.Text = "Делитель:";
            this.layoutControlItemDivider.TextSize = new System.Drawing.Size(103, 13);
            // 
            // layoutControlItemOrderNo
            // 
            this.layoutControlItemOrderNo.Control = this.numSort;
            this.layoutControlItemOrderNo.CustomizationFormText = "Сортировка";
            this.layoutControlItemOrderNo.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemOrderNo.Name = "layoutControlItemOrderNo";
            this.layoutControlItemOrderNo.Size = new System.Drawing.Size(330, 24);
            this.layoutControlItemOrderNo.Text = "Сортировка:";
            this.layoutControlItemOrderNo.TextSize = new System.Drawing.Size(103, 13);
            // 
            // ControlProductUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(350, 140);
            this.Name = "ControlProductUnit";
            this.Size = new System.Drawing.Size(350, 140);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDivider.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMultiply.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSupply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDivider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOrderNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemProduct;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUnit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSupply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDivider;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemOrderNo;
        public DevExpress.XtraEditors.SpinEdit numSort;
        public DevExpress.XtraEditors.SpinEdit numDivider;
        public DevExpress.XtraEditors.SpinEdit numMultiply;
        public DevExpress.XtraEditors.LookUpEdit cmbUnit;
        public DevExpress.XtraEditors.LookUpEdit cmbProduct;
    }
}
