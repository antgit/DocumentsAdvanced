namespace BusinessObjects.Windows.Controls
{
    partial class ControlAccount
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
            this.chkTurn = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItemTurn = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbSaldo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemSaldo = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbTurnKind = new DevExpress.XtraEditors.ComboBoxEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.chkTurn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTurn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSaldo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSaldo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTurnKind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 240);
            this.txtMemo.Size = new System.Drawing.Size(376, 48);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbTurnKind);
            this.LayoutControl.Controls.Add(this.cmbSaldo);
            this.LayoutControl.Controls.Add(this.chkTurn);
            this.LayoutControl.Size = new System.Drawing.Size(400, 300);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.chkTurn, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbSaldo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbTurnKind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemTurn,
            this.layoutControlItemSaldo,
            this.layoutControlItem2});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 301);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 212);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 69);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // chkTurn
            // 
            this.chkTurn.Location = new System.Drawing.Point(12, 154);
            this.chkTurn.Name = "chkTurn";
            this.chkTurn.Properties.Caption = "Сворачивать субсчета";
            this.chkTurn.Size = new System.Drawing.Size(376, 18);
            this.chkTurn.StyleController = this.LayoutControl;
            this.chkTurn.TabIndex = 7;
            // 
            // layoutControlItemTurn
            // 
            this.layoutControlItemTurn.Control = this.chkTurn;
            this.layoutControlItemTurn.CustomizationFormText = "Сворачивать субсчета";
            this.layoutControlItemTurn.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemTurn.Name = "layoutControlItemTurn";
            this.layoutControlItemTurn.Size = new System.Drawing.Size(380, 22);
            this.layoutControlItemTurn.Text = "layoutControlItemTurn";
            this.layoutControlItemTurn.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItemTurn.TextToControlDistance = 0;
            this.layoutControlItemTurn.TextVisible = false;
            // 
            // cmbSaldo
            // 
            this.cmbSaldo.Location = new System.Drawing.Point(132, 176);
            this.cmbSaldo.Name = "cmbSaldo";
            this.cmbSaldo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSaldo.Size = new System.Drawing.Size(256, 20);
            this.cmbSaldo.StyleController = this.LayoutControl;
            this.cmbSaldo.TabIndex = 10;
            // 
            // layoutControlItemSaldo
            // 
            this.layoutControlItemSaldo.Control = this.cmbSaldo;
            this.layoutControlItemSaldo.CustomizationFormText = "Тип счета";
            this.layoutControlItemSaldo.Location = new System.Drawing.Point(0, 164);
            this.layoutControlItemSaldo.Name = "layoutControlItemSaldo";
            this.layoutControlItemSaldo.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemSaldo.Text = "Тип счета:";
            this.layoutControlItemSaldo.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbTurnKind
            // 
            this.cmbTurnKind.Location = new System.Drawing.Point(132, 200);
            this.cmbTurnKind.Name = "cmbTurnKind";
            this.cmbTurnKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTurnKind.Size = new System.Drawing.Size(256, 20);
            this.cmbTurnKind.StyleController = this.LayoutControl;
            this.cmbTurnKind.TabIndex = 11;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbTurnKind;
            this.layoutControlItem2.CustomizationFormText = "Тип свертки";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 188);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem2.Text = "Тип свертки:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "ControlAccount";
            this.Size = new System.Drawing.Size(400, 300);
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
            ((System.ComponentModel.ISupportInitialize)(this.chkTurn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTurn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSaldo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSaldo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTurnKind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTurn;
        public DevExpress.XtraEditors.CheckEdit chkTurn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSaldo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraEditors.ComboBoxEdit cmbTurnKind;
        public DevExpress.XtraEditors.ComboBoxEdit cmbSaldo;
    }
}
