namespace BusinessObjects.Windows.Controls
{
    partial class ControlEntityMethod
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
            this.cmbSchema = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemShema = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbProcedure = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbSubKind = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbSchema.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemShema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProcedure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSubKind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 242);
            this.txtMemo.Size = new System.Drawing.Size(376, 56);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbSubKind);
            this.LayoutControl.Controls.Add(this.cmbProcedure);
            this.LayoutControl.Controls.Add(this.cmbSchema);
            this.LayoutControl.Size = new System.Drawing.Size(400, 310);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbSchema, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbProcedure, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbSubKind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemShema,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 310);
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.CustomizationFormText = "Метод";
            this.layoutControlItemCode.Text = "Метод:";
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 76);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // txtNameFull2
            // 
            // 
            // cmbSchema
            // 
            this.cmbSchema.Location = new System.Drawing.Point(132, 154);
            this.cmbSchema.Name = "cmbSchema";
            this.cmbSchema.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSchema.Size = new System.Drawing.Size(256, 20);
            this.cmbSchema.StyleController = this.LayoutControl;
            this.cmbSchema.TabIndex = 9;
            // 
            // layoutControlItemShema
            // 
            this.layoutControlItemShema.Control = this.cmbSchema;
            this.layoutControlItemShema.CustomizationFormText = "Схема";
            this.layoutControlItemShema.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemShema.Name = "layoutControlItemShema";
            this.layoutControlItemShema.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemShema.Text = "Схема:";
            this.layoutControlItemShema.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbProcedure
            // 
            this.cmbProcedure.Location = new System.Drawing.Point(132, 178);
            this.cmbProcedure.Name = "cmbProcedure";
            this.cmbProcedure.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProcedure.Size = new System.Drawing.Size(256, 20);
            this.cmbProcedure.StyleController = this.LayoutControl;
            this.cmbProcedure.TabIndex = 10;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbProcedure;
            this.layoutControlItem1.CustomizationFormText = "Процедура";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem1.Text = "Процедура:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbSubKind
            // 
            this.cmbSubKind.Location = new System.Drawing.Point(132, 202);
            this.cmbSubKind.Name = "cmbSubKind";
            this.cmbSubKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSubKind.Size = new System.Drawing.Size(256, 20);
            this.cmbSubKind.StyleController = this.LayoutControl;
            this.cmbSubKind.TabIndex = 11;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbSubKind;
            this.layoutControlItem2.CustomizationFormText = "Подтип";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem2.Text = "Подтип:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlEntityMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 310);
            this.Name = "ControlEntityMethod";
            this.Size = new System.Drawing.Size(400, 310);
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbSchema.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemShema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProcedure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSubKind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemShema;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraEditors.ComboBoxEdit cmbSchema;
        public DevExpress.XtraEditors.ComboBoxEdit cmbProcedure;
        public DevExpress.XtraEditors.LookUpEdit cmbSubKind;
    }
}
