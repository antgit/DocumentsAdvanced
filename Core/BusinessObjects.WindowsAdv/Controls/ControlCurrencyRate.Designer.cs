namespace BusinessObjects.Windows.Controls
{
    partial class ControlCurrencyRate
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
            this.dtDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbBank = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbCurrencyFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbCurrencyTo = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.numDivider = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.numValue = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDivider.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 314);
            this.txtMemo.Size = new System.Drawing.Size(376, 49);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.numValue);
            this.LayoutControl.Controls.Add(this.numDivider);
            this.LayoutControl.Controls.Add(this.cmbCurrencyTo);
            this.LayoutControl.Controls.Add(this.cmbCurrencyFrom);
            this.LayoutControl.Controls.Add(this.cmbBank);
            this.LayoutControl.Controls.Add(this.dtDate);
            this.LayoutControl.Size = new System.Drawing.Size(400, 375);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.dtDate, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbBank, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbCurrencyFrom, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbCurrencyTo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.numDivider, 0);
            this.LayoutControl.Controls.SetChildIndex(this.numValue, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem7});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 377);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 286);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 71);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // dtDate
            // 
            this.dtDate.EditValue = null;
            this.dtDate.Location = new System.Drawing.Point(132, 154);
            this.dtDate.Name = "dtDate";
            this.dtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtDate.Size = new System.Drawing.Size(256, 20);
            this.dtDate.StyleController = this.LayoutControl;
            this.dtDate.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dtDate;
            this.layoutControlItem1.CustomizationFormText = "Дата";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem1.Text = "Дата:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbBank
            // 
            this.cmbBank.Location = new System.Drawing.Point(132, 178);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBank.Size = new System.Drawing.Size(256, 20);
            this.cmbBank.StyleController = this.LayoutControl;
            this.cmbBank.TabIndex = 8;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbBank;
            this.layoutControlItem2.CustomizationFormText = "Банк";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem2.Text = "Банк:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbCurrencyFrom
            // 
            this.cmbCurrencyFrom.Location = new System.Drawing.Point(132, 202);
            this.cmbCurrencyFrom.Name = "cmbCurrencyFrom";
            this.cmbCurrencyFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrencyFrom.Size = new System.Drawing.Size(256, 20);
            this.cmbCurrencyFrom.StyleController = this.LayoutControl;
            this.cmbCurrencyFrom.TabIndex = 9;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbCurrencyFrom;
            this.layoutControlItem3.CustomizationFormText = "Первая валюта";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem3.Text = "Первая валюта:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbCurrencyTo
            // 
            this.cmbCurrencyTo.Location = new System.Drawing.Point(132, 226);
            this.cmbCurrencyTo.Name = "cmbCurrencyTo";
            this.cmbCurrencyTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrencyTo.Size = new System.Drawing.Size(256, 20);
            this.cmbCurrencyTo.StyleController = this.LayoutControl;
            this.cmbCurrencyTo.TabIndex = 10;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbCurrencyTo;
            this.layoutControlItem4.CustomizationFormText = "Вторая валюта";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem4.Text = "Вторая валюта:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(116, 13);
            // 
            // numDivider
            // 
            this.numDivider.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numDivider.Location = new System.Drawing.Point(132, 250);
            this.numDivider.Name = "numDivider";
            this.numDivider.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numDivider.Size = new System.Drawing.Size(256, 20);
            this.numDivider.StyleController = this.LayoutControl;
            this.numDivider.TabIndex = 11;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.numDivider;
            this.layoutControlItem5.CustomizationFormText = "Делитель";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 238);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem5.Text = "Делитель:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(116, 13);
            // 
            // numValue
            // 
            this.numValue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numValue.Location = new System.Drawing.Point(132, 274);
            this.numValue.Name = "numValue";
            this.numValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numValue.Size = new System.Drawing.Size(256, 20);
            this.numValue.StyleController = this.LayoutControl;
            this.numValue.TabIndex = 13;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.numValue;
            this.layoutControlItem7.CustomizationFormText = "Курс";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 262);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem7.Text = "Курс:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlCurrencyRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 375);
            this.Name = "ControlCurrencyRate";
            this.Size = new System.Drawing.Size(400, 375);
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
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrencyTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDivider.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        public DevExpress.XtraEditors.LookUpEdit cmbBank;
        public DevExpress.XtraEditors.LookUpEdit cmbCurrencyFrom;
        public DevExpress.XtraEditors.LookUpEdit cmbCurrencyTo;
        public DevExpress.XtraEditors.SpinEdit numValue;
        public DevExpress.XtraEditors.SpinEdit numDivider;
        public DevExpress.XtraEditors.DateEdit dtDate;
    }
}
