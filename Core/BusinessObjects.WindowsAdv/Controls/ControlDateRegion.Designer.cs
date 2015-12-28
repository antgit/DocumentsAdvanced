namespace BusinessObjects.Windows.Controls
{
    partial class ControlDateRegion
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
            this.dtDateStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemDateStart = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtDateEnd = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemDateEnd = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.dtDateStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 218);
            this.txtMemo.Size = new System.Drawing.Size(376, 55);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 75);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 120);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 148);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.dtDateEnd);
            this.LayoutControl.Controls.Add(this.dtDateStart);
            this.LayoutControl.Size = new System.Drawing.Size(400, 285);
            this.LayoutControl.Controls.SetChildIndex(this.dtDateStart, 0);
            this.LayoutControl.Controls.SetChildIndex(this.dtDateEnd, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDateStart,
            this.layoutControlItemDateEnd});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 285);
            // 
            // dtDateStart
            // 
            this.dtDateStart.EditValue = null;
            this.dtDateStart.Location = new System.Drawing.Point(132, 84);
            this.dtDateStart.Name = "dtDateStart";
            this.dtDateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtDateStart.Size = new System.Drawing.Size(256, 20);
            this.dtDateStart.StyleController = this.LayoutControl;
            this.dtDateStart.TabIndex = 9;
            // 
            // layoutControlItemDateStart
            // 
            this.layoutControlItemDateStart.Control = this.dtDateStart;
            this.layoutControlItemDateStart.CustomizationFormText = "Дата начала";
            this.layoutControlItemDateStart.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemDateStart.Name = "layoutControlItemDateStart";
            this.layoutControlItemDateStart.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDateStart.Text = "Дата начала:";
            this.layoutControlItemDateStart.TextSize = new System.Drawing.Size(116, 13);
            // 
            // dtDateEnd
            // 
            this.dtDateEnd.EditValue = null;
            this.dtDateEnd.Location = new System.Drawing.Point(132, 108);
            this.dtDateEnd.Name = "dtDateEnd";
            this.dtDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDateEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtDateEnd.Size = new System.Drawing.Size(256, 20);
            this.dtDateEnd.StyleController = this.LayoutControl;
            this.dtDateEnd.TabIndex = 10;
            // 
            // layoutControlItemDateEnd
            // 
            this.layoutControlItemDateEnd.Control = this.dtDateEnd;
            this.layoutControlItemDateEnd.CustomizationFormText = "Дата окончания";
            this.layoutControlItemDateEnd.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemDateEnd.Name = "layoutControlItemDateEnd";
            this.layoutControlItemDateEnd.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemDateEnd.Text = "Дата окончания:";
            this.layoutControlItemDateEnd.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlDateRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 285);
            this.Name = "ControlDateRegion";
            this.Size = new System.Drawing.Size(400, 285);
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
            ((System.ComponentModel.ISupportInitialize)(this.dtDateStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDateEnd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateEnd;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemDateStart;
        public DevExpress.XtraEditors.DateEdit dtDateStart;
        public DevExpress.XtraEditors.DateEdit dtDateEnd;
    }
}
