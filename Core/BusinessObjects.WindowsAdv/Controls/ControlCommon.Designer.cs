namespace BusinessObjects.Windows.Controls
{
    partial class ControlCommon
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
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMemo = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCodeFind = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemCodeFind = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtNameFull2 = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItemNameFull2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.txtNameFull2);
            this.LayoutControl.Controls.Add(this.txtCodeFind);
            this.LayoutControl.Controls.Add(this.txtMemo);
            this.LayoutControl.Controls.Add(this.txtCode);
            this.LayoutControl.Controls.Add(this.txtName);
            this.LayoutControl.Size = new System.Drawing.Size(400, 255);
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 170);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(376, 73);
            this.txtMemo.StyleController = this.LayoutControl;
            this.txtMemo.TabIndex = 6;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(132, 36);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(256, 20);
            this.txtCode.StyleController = this.LayoutControl;
            this.txtCode.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(132, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(256, 20);
            this.txtName.StyleController = this.LayoutControl;
            this.txtName.TabIndex = 4;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemName,
            this.layoutControlItemCode,
            this.layoutControlItemMemo,
            this.layoutControlItemCodeFind,
            this.layoutControlItemNameFull2});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 255);
            this.layoutControlGroup.Text = "Основные";
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Control = this.txtName;
            this.layoutControlItemName.CustomizationFormText = "Наименование";
            this.layoutControlItemName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemName.Name = "layoutControlItemName";
            this.layoutControlItemName.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemName.Text = "Наименование:";
            this.layoutControlItemName.TextSize = new System.Drawing.Size(116, 13);
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.Control = this.txtCode;
            this.layoutControlItemCode.CustomizationFormText = "Признак";
            this.layoutControlItemCode.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemCode.Name = "layoutControlItemCode";
            this.layoutControlItemCode.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemCode.Text = "Признак:";
            this.layoutControlItemCode.TextSize = new System.Drawing.Size(116, 13);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Control = this.txtMemo;
            this.layoutControlItemMemo.CustomizationFormText = "Примечание";
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemMemo.Name = "layoutControlItemMemo";
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 93);
            this.layoutControlItemMemo.Text = "Примечание:";
            this.layoutControlItemMemo.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemMemo.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtCodeFind
            // 
            this.txtCodeFind.Location = new System.Drawing.Point(132, 60);
            this.txtCodeFind.Name = "txtCodeFind";
            this.txtCodeFind.Size = new System.Drawing.Size(256, 20);
            this.txtCodeFind.StyleController = this.LayoutControl;
            this.txtCodeFind.TabIndex = 7;
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.Control = this.txtCodeFind;
            this.layoutControlItemCodeFind.CustomizationFormText = "Код поиска";
            this.layoutControlItemCodeFind.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemCodeFind.Name = "layoutControlItemCodeFind";
            this.layoutControlItemCodeFind.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemCodeFind.Text = "Код поиска:";
            this.layoutControlItemCodeFind.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 100);
            this.txtNameFull2.Name = "txtNameFull2";
            this.txtNameFull2.Size = new System.Drawing.Size(376, 50);
            this.txtNameFull2.StyleController = this.LayoutControl;
            this.txtNameFull2.TabIndex = 8;
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Control = this.txtNameFull2;
            this.layoutControlItemNameFull2.CustomizationFormText = "Полное наименование";
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemNameFull2.MaxSize = new System.Drawing.Size(0, 70);
            this.layoutControlItemNameFull2.MinSize = new System.Drawing.Size(120, 70);
            this.layoutControlItemNameFull2.Name = "layoutControlItemNameFull2";
            this.layoutControlItemNameFull2.Size = new System.Drawing.Size(380, 70);
            this.layoutControlItemNameFull2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemNameFull2.Text = "Полное наименование:";
            this.layoutControlItemNameFull2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemNameFull2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlCommon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(400, 255);
            this.Name = "ControlCommon";
            this.Size = new System.Drawing.Size(400, 255);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.MemoEdit txtMemo;
        public DevExpress.XtraEditors.TextEdit txtCode;
        public DevExpress.XtraEditors.TextEdit txtName;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemName;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemCode;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemMemo;
        public DevExpress.XtraEditors.TextEdit txtCodeFind;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemCodeFind;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemNameFull2;
        public DevExpress.XtraEditors.MemoEdit txtNameFull2;
    }
}
