namespace BusinessObjects.Windows.Controls
{
    partial class ControlDepatment
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
            this.layoutControlItemMyCompany = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbMyCompanyId = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewMyCompanyId = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemHead = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbHead = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewHeadId = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemSubHead = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbSubHead = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewSubHeadId = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMyCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMyCompanyId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewMyCompanyId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHead.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewHeadId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSubHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSubHead.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewSubHeadId)).BeginInit();
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
            // txtNameFull2
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbMyCompanyId);
            this.LayoutControl.Controls.Add(this.cmbHead);
            this.LayoutControl.Controls.Add(this.cmbSubHead);
            this.LayoutControl.Size = new System.Drawing.Size(400, 310);
            this.LayoutControl.Controls.SetChildIndex(this.cmbSubHead, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbHead, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbMyCompanyId, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemMyCompany,
            this.layoutControlItemHead,
            this.layoutControlItemSubHead});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 310);
            // 
            // layoutControlItemMyCompany
            // 
            this.layoutControlItemMyCompany.Control = this.cmbMyCompanyId;
            this.layoutControlItemMyCompany.CustomizationFormText = "Предприятие";
            this.layoutControlItemMyCompany.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemMyCompany.Name = "layoutControlItemMyCompany";
            this.layoutControlItemMyCompany.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemMyCompany.Text = "Предприятие:";
            this.layoutControlItemMyCompany.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbMyCompanyId
            // 
            this.cmbMyCompanyId.Location = new System.Drawing.Point(132, 154);
            this.cmbMyCompanyId.Name = "cmbMyCompanyId";
            this.cmbMyCompanyId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMyCompanyId.Properties.View = this.ViewMyCompanyId;
            this.cmbMyCompanyId.Size = new System.Drawing.Size(256, 20);
            this.cmbMyCompanyId.StyleController = this.LayoutControl;
            this.cmbMyCompanyId.TabIndex = 9;
            // 
            // ViewMyCompanyId
            // 
            this.ViewMyCompanyId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewMyCompanyId.Name = "ViewMyCompanyId";
            this.ViewMyCompanyId.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewMyCompanyId.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItemHead
            // 
            this.layoutControlItemHead.Control = this.cmbHead;
            this.layoutControlItemHead.CustomizationFormText = "Руководитель";
            this.layoutControlItemHead.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemHead.Name = "layoutControlItemHead";
            this.layoutControlItemHead.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemHead.Text = "Руководитель:";
            this.layoutControlItemHead.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbHead
            // 
            this.cmbHead.Location = new System.Drawing.Point(132, 178);
            this.cmbHead.Name = "cmbHead";
            this.cmbHead.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbHead.Properties.View = this.ViewHeadId;
            this.cmbHead.Size = new System.Drawing.Size(256, 20);
            this.cmbHead.StyleController = this.LayoutControl;
            this.cmbHead.TabIndex = 10;
            // 
            // ViewHeadId
            // 
            this.ViewHeadId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewHeadId.Name = "ViewHeadId";
            this.ViewHeadId.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewHeadId.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItemSubHead
            // 
            this.layoutControlItemSubHead.Control = this.cmbSubHead;
            this.layoutControlItemSubHead.CustomizationFormText = "Заместитель";
            this.layoutControlItemSubHead.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemSubHead.Name = "layoutControlItemSubHead";
            this.layoutControlItemSubHead.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemSubHead.Text = "Заместитель:";
            this.layoutControlItemSubHead.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbSubHead
            // 
            this.cmbSubHead.Location = new System.Drawing.Point(132, 202);
            this.cmbSubHead.Name = "cmbSubHead";
            this.cmbSubHead.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSubHead.Properties.View = this.ViewSubHeadId;
            this.cmbSubHead.Size = new System.Drawing.Size(256, 20);
            this.cmbSubHead.StyleController = this.LayoutControl;
            this.cmbSubHead.TabIndex = 11;
            // 
            // ViewSubHeadId
            // 
            this.ViewSubHeadId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewSubHeadId.Name = "ViewSubHeadId";
            this.ViewSubHeadId.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewSubHeadId.OptionsView.ShowGroupPanel = false;
            // 
            // ControlDepatment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 310);
            this.Name = "ControlDepatment";
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMyCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMyCompanyId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewMyCompanyId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHead.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewHeadId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSubHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSubHead.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewSubHeadId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemMyCompany;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemHead;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemSubHead;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbMyCompanyId;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbHead;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbSubHead;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewMyCompanyId;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewHeadId;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewSubHeadId;
    }
}
