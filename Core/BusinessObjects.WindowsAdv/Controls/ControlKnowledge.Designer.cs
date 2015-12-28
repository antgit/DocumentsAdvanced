namespace BusinessObjects.Windows.Controls
{
    partial class ControlKnowledge
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
            this.btnEditFile = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlItemFileData = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFileData)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 194);
            this.txtMemo.Size = new System.Drawing.Size(376, 49);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 69);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 96);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 124);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.btnEditFile);
            this.LayoutControl.Controls.SetChildIndex(this.btnEditFile, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemFileData});
            // 
            // btnEditFile
            // 
            this.btnEditFile.Location = new System.Drawing.Point(132, 84);
            this.btnEditFile.Name = "btnEditFile";
            this.btnEditFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditFile.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnEditFile.Size = new System.Drawing.Size(256, 20);
            this.btnEditFile.StyleController = this.LayoutControl;
            this.btnEditFile.TabIndex = 9;
            // 
            // layoutControlItemFileData
            // 
            this.layoutControlItemFileData.Control = this.btnEditFile;
            this.layoutControlItemFileData.CustomizationFormText = "Файл";
            this.layoutControlItemFileData.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemFileData.Name = "layoutControlItemFileData";
            this.layoutControlItemFileData.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemFileData.Text = "Файл:";
            this.layoutControlItemFileData.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlKnowledge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlKnowledge";
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
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFileData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFileData;
        public DevExpress.XtraEditors.ButtonEdit btnEditFile;
    }
}
