namespace BusinessObjects.Windows.Controls
{
    partial class ControlNote
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
            this.cmbUserUwnerId = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemuUserOwnerId = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserUwnerId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemuUserOwnerId)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 194);
            this.txtMemo.Size = new System.Drawing.Size(476, 144);
            // 
            // txtCode
            // 
            this.txtCode.Size = new System.Drawing.Size(356, 20);
            // 
            // txtName
            // 
            this.txtName.Size = new System.Drawing.Size(356, 20);
            // 
            // layoutControlItemName
            // 
            this.layoutControlItemName.Size = new System.Drawing.Size(480, 24);
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.Size = new System.Drawing.Size(480, 24);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(480, 164);
            // 
            // txtCodeFind
            // 
            this.txtCodeFind.Size = new System.Drawing.Size(356, 20);
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.Size = new System.Drawing.Size(480, 24);
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemNameFull2.Size = new System.Drawing.Size(480, 70);
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 124);
            this.txtNameFull2.Size = new System.Drawing.Size(476, 50);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbUserUwnerId);
            this.LayoutControl.Size = new System.Drawing.Size(500, 350);
            this.LayoutControl.Controls.SetChildIndex(this.cmbUserUwnerId, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemuUserOwnerId});
            this.layoutControlGroup.Size = new System.Drawing.Size(500, 350);
            // 
            // cmbUserUwnerId
            // 
            this.cmbUserUwnerId.Location = new System.Drawing.Point(132, 84);
            this.cmbUserUwnerId.Name = "cmbUserUwnerId";
            this.cmbUserUwnerId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUserUwnerId.Properties.PopupFormMinSize = new System.Drawing.Size(0, 300);
            this.cmbUserUwnerId.Properties.PopupFormSize = new System.Drawing.Size(0, 300);
            this.cmbUserUwnerId.Properties.View = this.searchLookUpEdit1View;
            this.cmbUserUwnerId.Size = new System.Drawing.Size(356, 20);
            this.cmbUserUwnerId.StyleController = this.LayoutControl;
            this.cmbUserUwnerId.TabIndex = 9;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsSelection.UseIndicatorForSelection = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItemuUserOwnerId
            // 
            this.layoutControlItemuUserOwnerId.Control = this.cmbUserUwnerId;
            this.layoutControlItemuUserOwnerId.CustomizationFormText = "Владелец";
            this.layoutControlItemuUserOwnerId.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemuUserOwnerId.Name = "layoutControlItemuUserOwnerId";
            this.layoutControlItemuUserOwnerId.Size = new System.Drawing.Size(480, 24);
            this.layoutControlItemuUserOwnerId.Text = "Владелец:";
            this.layoutControlItemuUserOwnerId.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(500, 350);
            this.Name = "ControlNote";
            this.Size = new System.Drawing.Size(500, 350);
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserUwnerId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemuUserOwnerId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbUserUwnerId;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemuUserOwnerId;
    }
}
