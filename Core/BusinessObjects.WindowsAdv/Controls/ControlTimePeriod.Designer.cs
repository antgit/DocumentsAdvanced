namespace BusinessObjects.Windows.Controls
{
    partial class ControlTimePeriod
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
            this.controlTimePeriodValues = new BusinessObjects.Windows.Controls.ControlTimePeriodValues();
            this.layoutControlItemValues = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValues)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 404);
            this.txtMemo.Size = new System.Drawing.Size(376, 44);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 376);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 64);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.controlTimePeriodValues);
            this.LayoutControl.Size = new System.Drawing.Size(400, 460);
            this.LayoutControl.Controls.SetChildIndex(this.controlTimePeriodValues, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemValues});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 460);
            // 
            // controlTimePeriodValues
            // 
            this.controlTimePeriodValues.Location = new System.Drawing.Point(12, 170);
            this.controlTimePeriodValues.MinimumSize = new System.Drawing.Size(338, 214);
            this.controlTimePeriodValues.Name = "controlTimePeriodValues";
            this.controlTimePeriodValues.Size = new System.Drawing.Size(376, 214);
            this.controlTimePeriodValues.TabIndex = 9;
            // 
            // layoutControlItemValues
            // 
            this.layoutControlItemValues.Control = this.controlTimePeriodValues;
            this.layoutControlItemValues.CustomizationFormText = "График работы";
            this.layoutControlItemValues.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemValues.Name = "layoutControlItemValues";
            this.layoutControlItemValues.Size = new System.Drawing.Size(380, 234);
            this.layoutControlItemValues.Text = "График работы";
            this.layoutControlItemValues.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemValues.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlTimePeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 460);
            this.Name = "ControlTimePeriod";
            this.Size = new System.Drawing.Size(400, 460);
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public ControlTimePeriodValues controlTimePeriodValues;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemValues;
    }
}
