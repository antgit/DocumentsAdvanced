namespace BusinessObjects.Windows.Controls
{
    partial class ControlFileData
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
            this.txtFileExtention = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.chkAllowVersion = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItemAllowVersion = new DevExpress.XtraLayout.LayoutControlItem();
            this.edVersionCode = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItemVersionCode = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtFileExtention.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllowVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edVersionCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVersionCode)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 207);
            this.txtMemo.Size = new System.Drawing.Size(376, 51);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 179);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 71);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.MaxSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemNameFull2.MinSize = new System.Drawing.Size(120, 36);
            this.layoutControlItemNameFull2.Size = new System.Drawing.Size(380, 36);
            this.layoutControlItemNameFull2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Default;
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Properties.ReadOnly = true;
            this.txtNameFull2.Size = new System.Drawing.Size(376, 16);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.edVersionCode);
            this.LayoutControl.Controls.Add(this.chkAllowVersion);
            this.LayoutControl.Controls.Add(this.txtFileExtention);
            this.LayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(442, 59, 250, 350);
            this.LayoutControl.Size = new System.Drawing.Size(400, 270);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtFileExtention, 0);
            this.LayoutControl.Controls.SetChildIndex(this.chkAllowVersion, 0);
            this.LayoutControl.Controls.SetChildIndex(this.edVersionCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItemAllowVersion,
            this.layoutControlItemVersionCode});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 270);
            // 
            // txtFileExtention
            // 
            this.txtFileExtention.Location = new System.Drawing.Point(132, 120);
            this.txtFileExtention.Name = "txtFileExtention";
            this.txtFileExtention.Size = new System.Drawing.Size(256, 20);
            this.txtFileExtention.StyleController = this.LayoutControl;
            this.txtFileExtention.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtFileExtention;
            this.layoutControlItem1.CustomizationFormText = "Расширение";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem1.Text = "Расширение:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(116, 13);
            // 
            // chkAllowVersion
            // 
            this.chkAllowVersion.Location = new System.Drawing.Point(12, 144);
            this.chkAllowVersion.Name = "chkAllowVersion";
            this.chkAllowVersion.Properties.Caption = "Сохранять версии файла";
            this.chkAllowVersion.Size = new System.Drawing.Size(376, 19);
            this.chkAllowVersion.StyleController = this.LayoutControl;
            this.chkAllowVersion.TabIndex = 9;
            // 
            // layoutControlItemAllowVersion
            // 
            this.layoutControlItemAllowVersion.Control = this.chkAllowVersion;
            this.layoutControlItemAllowVersion.CustomizationFormText = "Сохранять версии файлов";
            this.layoutControlItemAllowVersion.Location = new System.Drawing.Point(0, 132);
            this.layoutControlItemAllowVersion.Name = "layoutControlItemAllowVersion";
            this.layoutControlItemAllowVersion.Size = new System.Drawing.Size(380, 23);
            this.layoutControlItemAllowVersion.Text = "Сохранять версии файлов";
            this.layoutControlItemAllowVersion.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemAllowVersion.TextToControlDistance = 0;
            this.layoutControlItemAllowVersion.TextVisible = false;
            // 
            // edVersionCode
            // 
            this.edVersionCode.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.edVersionCode.Location = new System.Drawing.Point(132, 167);
            this.edVersionCode.Name = "edVersionCode";
            this.edVersionCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edVersionCode.Properties.Mask.EditMask = "n0";
            this.edVersionCode.Size = new System.Drawing.Size(256, 20);
            this.edVersionCode.StyleController = this.LayoutControl;
            this.edVersionCode.TabIndex = 10;
            // 
            // layoutControlItemVersionCode
            // 
            this.layoutControlItemVersionCode.Control = this.edVersionCode;
            this.layoutControlItemVersionCode.CustomizationFormText = "Версия";
            this.layoutControlItemVersionCode.Location = new System.Drawing.Point(0, 155);
            this.layoutControlItemVersionCode.Name = "layoutControlItemVersionCode";
            this.layoutControlItemVersionCode.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemVersionCode.Text = "Версия:";
            this.layoutControlItemVersionCode.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlFileData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 270);
            this.Name = "ControlFileData";
            this.Size = new System.Drawing.Size(400, 270);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtFileExtention.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAllowVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edVersionCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemVersionCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txtFileExtention;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraEditors.CheckEdit chkAllowVersion;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAllowVersion;
        public DevExpress.XtraEditors.SpinEdit edVersionCode;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemVersionCode;
    }
}
