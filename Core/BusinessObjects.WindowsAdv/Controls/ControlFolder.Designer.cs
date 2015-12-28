namespace BusinessObjects.Windows.Controls
{
    partial class ControlFolder
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
            this.cmbForm = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbOperationTemplate = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbViewDocs = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbForm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbViewDocs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 242);
            this.txtMemo.Size = new System.Drawing.Size(376, 61);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbViewDocs);
            this.LayoutControl.Controls.Add(this.cmbOperationTemplate);
            this.LayoutControl.Controls.Add(this.cmbForm);
            this.LayoutControl.Size = new System.Drawing.Size(400, 315);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbForm, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbOperationTemplate, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbViewDocs, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 315);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 81);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // cmbForm
            // 
            this.cmbForm.Location = new System.Drawing.Point(132, 154);
            this.cmbForm.Name = "cmbForm";
            this.cmbForm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbForm.Size = new System.Drawing.Size(256, 20);
            this.cmbForm.StyleController = this.LayoutControl;
            this.cmbForm.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbForm;
            this.layoutControlItem1.CustomizationFormText = "Форма";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem1.Text = "Форма:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbOperationTemplate
            // 
            this.cmbOperationTemplate.Location = new System.Drawing.Point(132, 178);
            this.cmbOperationTemplate.Name = "cmbOperationTemplate";
            this.cmbOperationTemplate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOperationTemplate.Size = new System.Drawing.Size(256, 20);
            this.cmbOperationTemplate.StyleController = this.LayoutControl;
            this.cmbOperationTemplate.TabIndex = 8;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbOperationTemplate;
            this.layoutControlItem2.CustomizationFormText = "Шаблон операции";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem2.Text = "Шаблон операции:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbViewDocs
            // 
            this.cmbViewDocs.Location = new System.Drawing.Point(132, 202);
            this.cmbViewDocs.Name = "cmbViewDocs";
            this.cmbViewDocs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbViewDocs.Size = new System.Drawing.Size(256, 20);
            this.cmbViewDocs.StyleController = this.LayoutControl;
            this.cmbViewDocs.TabIndex = 9;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbViewDocs;
            this.layoutControlItem3.CustomizationFormText = "Список документов";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem3.Text = "Список документов:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 315);
            this.Name = "ControlFolder";
            this.Size = new System.Drawing.Size(400, 315);
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbForm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbViewDocs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        public DevExpress.XtraEditors.LookUpEdit cmbForm;
        public DevExpress.XtraEditors.LookUpEdit cmbOperationTemplate;
        public DevExpress.XtraEditors.LookUpEdit cmbViewDocs;
    }
}
