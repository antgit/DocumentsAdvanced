namespace BusinessObjects.Windows.Controls
{
    partial class ControlCodeValue
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
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtValue = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.edOrderNo = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItemOrderNo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edOrderNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOrderNo)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.edOrderNo);
            this.LayoutControl.Controls.Add(this.txtMemo);
            this.LayoutControl.Controls.Add(this.txtName);
            this.LayoutControl.Controls.Add(this.txtValue);
            this.LayoutControl.Controls.Add(this.txtCode);
            this.LayoutControl.Size = new System.Drawing.Size(355, 200);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItemOrderNo});
            this.layoutControlGroup.Size = new System.Drawing.Size(355, 200);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(93, 36);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(250, 20);
            this.txtCode.StyleController = this.LayoutControl;
            this.txtCode.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtCode;
            this.layoutControlItem1.CustomizationFormText = "Код";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(335, 24);
            this.layoutControlItem1.Text = "Код:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(77, 13);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(93, 60);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(250, 20);
            this.txtValue.StyleController = this.LayoutControl;
            this.txtValue.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtValue;
            this.layoutControlItem2.CustomizationFormText = "Значение";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(335, 24);
            this.layoutControlItem2.Text = "Значение:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(77, 13);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(93, 12);
            this.txtName.Name = "txtName";
            this.txtName.Properties.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.StyleController = this.LayoutControl;
            this.txtName.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtName;
            this.layoutControlItem3.CustomizationFormText = "Наименование";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(335, 24);
            this.layoutControlItem3.Text = "Наименование:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(77, 13);
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 124);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(331, 64);
            this.txtMemo.StyleController = this.LayoutControl;
            this.txtMemo.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtMemo;
            this.layoutControlItem4.CustomizationFormText = "Примечание";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(335, 84);
            this.layoutControlItem4.Text = "Примечание";
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(77, 13);
            // 
            // edOrderNo
            // 
            this.edOrderNo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edOrderNo.Location = new System.Drawing.Point(93, 84);
            this.edOrderNo.Name = "edOrderNo";
            this.edOrderNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edOrderNo.Properties.Mask.EditMask = "n0";
            this.edOrderNo.Size = new System.Drawing.Size(250, 20);
            this.edOrderNo.StyleController = this.LayoutControl;
            this.edOrderNo.TabIndex = 8;
            // 
            // layoutControlItemOrderNo
            // 
            this.layoutControlItemOrderNo.Control = this.edOrderNo;
            this.layoutControlItemOrderNo.CustomizationFormText = "Номер";
            this.layoutControlItemOrderNo.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemOrderNo.Name = "layoutControlItemOrderNo";
            this.layoutControlItemOrderNo.Size = new System.Drawing.Size(335, 24);
            this.layoutControlItemOrderNo.Text = "Номер:";
            this.layoutControlItemOrderNo.TextSize = new System.Drawing.Size(77, 13);
            // 
            // ControlCodeValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(355, 200);
            this.Name = "ControlCodeValue";
            this.Size = new System.Drawing.Size(355, 200);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edOrderNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOrderNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraEditors.TextEdit txtName;
        public DevExpress.XtraEditors.TextEdit txtCode;
        public DevExpress.XtraEditors.TextEdit txtValue;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        public DevExpress.XtraEditors.MemoEdit txtMemo;
        public DevExpress.XtraEditors.SpinEdit edOrderNo;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemOrderNo;
    }
}
