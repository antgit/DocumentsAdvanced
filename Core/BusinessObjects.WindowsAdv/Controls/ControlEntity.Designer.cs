namespace BusinessObjects.Windows.Controls
{
    partial class ControlEntity
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
            this.numId = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.numMaxKind = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtCodeClass = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemCodeClass = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbShemaName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemSchemaName = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.numId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxKind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShemaName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSchemaName)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 266);
            this.txtMemo.Size = new System.Drawing.Size(376, 39);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Size = new System.Drawing.Size(363, 24);
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.Size = new System.Drawing.Size(363, 24);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 238);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(363, 36);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.Size = new System.Drawing.Size(363, 24);
            this.layoutControlItemCodeFind.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItemNameFull2.Size = new System.Drawing.Size(363, 70);
            this.layoutControlItemNameFull2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 172);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbShemaName);
            this.LayoutControl.Controls.Add(this.txtCodeClass);
            this.LayoutControl.Controls.Add(this.numMaxKind);
            this.LayoutControl.Controls.Add(this.numId);
            this.LayoutControl.Size = new System.Drawing.Size(400, 317);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.numId, 0);
            this.LayoutControl.Controls.SetChildIndex(this.numMaxKind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeClass, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbShemaName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItemCodeClass,
            this.layoutControlItemSchemaName});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 317);
            // 
            // numId
            // 
            this.numId.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numId.Location = new System.Drawing.Point(132, 84);
            this.numId.Name = "numId";
            this.numId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numId.Properties.Mask.EditMask = "n0";
            this.numId.Size = new System.Drawing.Size(256, 20);
            this.numId.StyleController = this.LayoutControl;
            this.numId.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.numId;
            this.layoutControlItem1.CustomizationFormText = "Код типа";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem1.Text = "Код типа:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(116, 13);
            // 
            // numMaxKind
            // 
            this.numMaxKind.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMaxKind.Location = new System.Drawing.Point(132, 226);
            this.numMaxKind.Name = "numMaxKind";
            this.numMaxKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMaxKind.Properties.Mask.EditMask = "n0";
            this.numMaxKind.Size = new System.Drawing.Size(256, 20);
            this.numMaxKind.StyleController = this.LayoutControl;
            this.numMaxKind.TabIndex = 8;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.numMaxKind;
            this.layoutControlItem2.CustomizationFormText = "Максимальный вид";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem2.Text = "Максимальный вид:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtCodeClass
            // 
            this.txtCodeClass.Location = new System.Drawing.Point(132, 108);
            this.txtCodeClass.Name = "txtCodeClass";
            this.txtCodeClass.Size = new System.Drawing.Size(256, 20);
            this.txtCodeClass.StyleController = this.LayoutControl;
            this.txtCodeClass.TabIndex = 9;
            // 
            // layoutControlItemCodeClass
            // 
            this.layoutControlItemCodeClass.Control = this.txtCodeClass;
            this.layoutControlItemCodeClass.CustomizationFormText = "Код класса";
            this.layoutControlItemCodeClass.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemCodeClass.Name = "layoutControlItemCodeClass";
            this.layoutControlItemCodeClass.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemCodeClass.Text = "Код класса:";
            this.layoutControlItemCodeClass.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbShemaName
            // 
            this.cmbShemaName.Location = new System.Drawing.Point(132, 132);
            this.cmbShemaName.Name = "cmbShemaName";
            this.cmbShemaName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbShemaName.Size = new System.Drawing.Size(256, 20);
            this.cmbShemaName.StyleController = this.LayoutControl;
            this.cmbShemaName.TabIndex = 10;
            // 
            // layoutControlItemSchemaName
            // 
            this.layoutControlItemSchemaName.Control = this.cmbShemaName;
            this.layoutControlItemSchemaName.CustomizationFormText = "Схема данных";
            this.layoutControlItemSchemaName.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemSchemaName.Name = "layoutControlItemSchemaName";
            this.layoutControlItemSchemaName.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemSchemaName.Text = "Схема данных:";
            this.layoutControlItemSchemaName.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 317);
            this.Name = "ControlEntity";
            this.Size = new System.Drawing.Size(400, 317);
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
            ((System.ComponentModel.ISupportInitialize)(this.numId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxKind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShemaName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSchemaName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraEditors.SpinEdit numMaxKind;
        public DevExpress.XtraEditors.SpinEdit numId;
        public DevExpress.XtraEditors.ComboBoxEdit cmbShemaName;
        public DevExpress.XtraEditors.TextEdit txtCodeClass;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemCodeClass;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemSchemaName;
    }
}
