namespace BusinessObjects.Windows.Controls
{
    partial class ControlChainKind
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
            this.cmbEntityFrom = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.numId = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtNameRight = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbEntityTo = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridSubKind = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntityFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameRight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntityTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 194);
            this.txtMemo.Size = new System.Drawing.Size(376, 94);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(145, 36);
            this.txtCode.Size = new System.Drawing.Size(243, 20);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(145, 12);
            this.txtName.Size = new System.Drawing.Size(243, 20);
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.TextSize = new System.Drawing.Size(129, 13);
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.TextSize = new System.Drawing.Size(129, 13);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 114);
            this.layoutControlItemMemo.TextSize = new System.Drawing.Size(129, 13);
            // 
            // txtCodeFind
            // 
            this.txtCodeFind.Location = new System.Drawing.Point(145, 60);
            this.txtCodeFind.Size = new System.Drawing.Size(243, 20);
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.TextSize = new System.Drawing.Size(129, 13);
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemNameFull2.TextSize = new System.Drawing.Size(129, 13);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 124);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.GridSubKind);
            this.LayoutControl.Controls.Add(this.cmbEntityTo);
            this.LayoutControl.Controls.Add(this.txtNameRight);
            this.LayoutControl.Controls.Add(this.numId);
            this.LayoutControl.Controls.Add(this.cmbEntityFrom);
            this.LayoutControl.Size = new System.Drawing.Size(400, 465);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbEntityFrom, 0);
            this.LayoutControl.Controls.SetChildIndex(this.numId, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameRight, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbEntityTo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.GridSubKind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem2});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 465);
            // 
            // cmbEntityFrom
            // 
            this.cmbEntityFrom.Location = new System.Drawing.Point(145, 316);
            this.cmbEntityFrom.Name = "cmbEntityFrom";
            this.cmbEntityFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEntityFrom.Size = new System.Drawing.Size(243, 20);
            this.cmbEntityFrom.StyleController = this.LayoutControl;
            this.cmbEntityFrom.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbEntityFrom;
            this.layoutControlItem1.CustomizationFormText = "Тип";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 304);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem1.Text = "Тип:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(129, 13);
            // 
            // numId
            // 
            this.numId.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numId.Location = new System.Drawing.Point(145, 292);
            this.numId.Name = "numId";
            this.numId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numId.Properties.Mask.EditMask = "n0";
            this.numId.Size = new System.Drawing.Size(243, 20);
            this.numId.StyleController = this.LayoutControl;
            this.numId.TabIndex = 8;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.numId;
            this.layoutControlItem2.CustomizationFormText = "Код";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 280);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem2.Text = "Код:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(129, 13);
            // 
            // txtNameRight
            // 
            this.txtNameRight.Location = new System.Drawing.Point(145, 84);
            this.txtNameRight.Name = "txtNameRight";
            this.txtNameRight.Size = new System.Drawing.Size(243, 20);
            this.txtNameRight.StyleController = this.LayoutControl;
            this.txtNameRight.TabIndex = 9;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtNameRight;
            this.layoutControlItem3.CustomizationFormText = "Обратное наименование:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem3.Text = "Обратное наименование:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(129, 13);
            // 
            // cmbEntityTo
            // 
            this.cmbEntityTo.Location = new System.Drawing.Point(145, 340);
            this.cmbEntityTo.Name = "cmbEntityTo";
            this.cmbEntityTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbEntityTo.Size = new System.Drawing.Size(243, 20);
            this.cmbEntityTo.StyleController = this.LayoutControl;
            this.cmbEntityTo.TabIndex = 11;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cmbEntityTo;
            this.layoutControlItem5.CustomizationFormText = "Тип назначения";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 328);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem5.Text = "Тип назначения:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(129, 13);
            // 
            // GridSubKind
            // 
            this.GridSubKind.Location = new System.Drawing.Point(12, 380);
            this.GridSubKind.Name = "GridSubKind";
            this.GridSubKind.Size = new System.Drawing.Size(376, 73);
            this.GridSubKind.TabIndex = 12;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.GridSubKind;
            this.layoutControlItem6.CustomizationFormText = "Значение подтипов";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 352);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(380, 93);
            this.layoutControlItem6.Text = "Значение подтипов:";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(129, 13);
            // 
            // ControlChainKind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 465);
            this.Name = "ControlChainKind";
            this.Size = new System.Drawing.Size(400, 465);
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntityFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameRight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbEntityTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraEditors.LookUpEdit cmbEntityFrom;
        public DevExpress.XtraEditors.SpinEdit numId;
        public DevExpress.XtraEditors.TextEdit txtNameRight;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        public DevExpress.XtraEditors.LookUpEdit cmbEntityTo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        public ControlList GridSubKind;
    }
}
