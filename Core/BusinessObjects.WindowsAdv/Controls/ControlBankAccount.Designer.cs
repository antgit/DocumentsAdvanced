namespace BusinessObjects.Windows.Controls
{
    partial class ControlBankAccount
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
            this.txtAgent = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemAgent = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbCurrency = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemCurrency = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbBank = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.layoutControlItemBank2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ViewBank = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBank2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBank)).BeginInit();
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
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 76);
            // 
            // txtCodeFind
            // 
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 144);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 172);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbBank);
            this.LayoutControl.Controls.Add(this.cmbCurrency);
            this.LayoutControl.Controls.Add(this.txtAgent);
            this.LayoutControl.Size = new System.Drawing.Size(400, 310);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtAgent, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbCurrency, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbBank, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemAgent,
            this.layoutControlItemCurrency,
            this.layoutControlItemBank2});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 310);
            // 
            // txtAgent
            // 
            this.txtAgent.Location = new System.Drawing.Point(132, 84);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(256, 20);
            this.txtAgent.StyleController = this.LayoutControl;
            this.txtAgent.TabIndex = 9;
            // 
            // layoutControlItemAgent
            // 
            this.layoutControlItemAgent.Control = this.txtAgent;
            this.layoutControlItemAgent.CustomizationFormText = "Корреспондент";
            this.layoutControlItemAgent.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemAgent.Name = "layoutControlItemAgent";
            this.layoutControlItemAgent.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemAgent.Text = "Корреспондент:";
            this.layoutControlItemAgent.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(132, 132);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(256, 20);
            this.cmbCurrency.StyleController = this.LayoutControl;
            this.cmbCurrency.TabIndex = 10;
            // 
            // layoutControlItemCurrency
            // 
            this.layoutControlItemCurrency.Control = this.cmbCurrency;
            this.layoutControlItemCurrency.CustomizationFormText = "Валюта";
            this.layoutControlItemCurrency.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemCurrency.Name = "layoutControlItemCurrency";
            this.layoutControlItemCurrency.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemCurrency.Text = "Валюта:";
            this.layoutControlItemCurrency.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbBank
            // 
            this.cmbBank.Location = new System.Drawing.Point(132, 108);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBank.Properties.View = this.ViewBank;
            this.cmbBank.Size = new System.Drawing.Size(256, 20);
            this.cmbBank.StyleController = this.LayoutControl;
            this.cmbBank.TabIndex = 11;
            // 
            // layoutControlItemBank2
            // 
            this.layoutControlItemBank2.Control = this.cmbBank;
            this.layoutControlItemBank2.CustomizationFormText = "Банк";
            this.layoutControlItemBank2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemBank2.Name = "layoutControlItemBank2";
            this.layoutControlItemBank2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemBank2.Text = "Банк:";
            this.layoutControlItemBank2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ViewBank
            // 
            this.ViewBank.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewBank.Name = "ViewBank";
            this.ViewBank.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewBank.OptionsView.ShowGroupPanel = false;
            // 
            // ControlBankAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 310);
            this.Name = "ControlBankAccount";
            this.Size = new System.Drawing.Size(400, 310);
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
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAgent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBank2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewBank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txtAgent;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAgent;
        public DevExpress.XtraEditors.LookUpEdit cmbCurrency;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemCurrency;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbBank;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewBank;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBank2;
    }
}
