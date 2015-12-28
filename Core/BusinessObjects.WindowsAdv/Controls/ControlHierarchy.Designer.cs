namespace BusinessObjects.Windows.Controls
{
    partial class ControlHierarchy
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
            this.cmbView = new DevExpress.XtraEditors.LookUpEdit();
            this.ltItemView = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbViewDocs = new DevExpress.XtraEditors.LookUpEdit();
            this.ltItemDocView = new DevExpress.XtraLayout.LayoutControlItem();
            this.numSortOrder = new DevExpress.XtraEditors.SpinEdit();
            this.ltItemOrder = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtParent = new DevExpress.XtraEditors.TextEdit();
            this.ltItemParent = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbVirtualBuild = new DevExpress.XtraEditors.LookUpEdit();
            this.ltItemVirtualBuild = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridSubKind = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItemContentKinds = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbViewDocs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemDocView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSortOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbVirtualBuild.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemVirtualBuild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContentKinds)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 398);
            this.txtMemo.Size = new System.Drawing.Size(376, 70);
            // 
            // txtCode
            // 
            // 
            // txtName
            // 
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 370);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 90);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.GridSubKind);
            this.LayoutControl.Controls.Add(this.cmbVirtualBuild);
            this.LayoutControl.Controls.Add(this.txtParent);
            this.LayoutControl.Controls.Add(this.numSortOrder);
            this.LayoutControl.Controls.Add(this.cmbViewDocs);
            this.LayoutControl.Controls.Add(this.cmbView);
            this.LayoutControl.Size = new System.Drawing.Size(400, 480);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbView, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbViewDocs, 0);
            this.LayoutControl.Controls.SetChildIndex(this.numSortOrder, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtParent, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbVirtualBuild, 0);
            this.LayoutControl.Controls.SetChildIndex(this.GridSubKind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ltItemView,
            this.layoutControlItemContentKinds,
            this.ltItemOrder,
            this.ltItemParent,
            this.ltItemVirtualBuild,
            this.ltItemDocView});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 480);
            // 
            // cmbView
            // 
            this.cmbView.Location = new System.Drawing.Point(132, 154);
            this.cmbView.Name = "cmbView";
            this.cmbView.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbView.Size = new System.Drawing.Size(256, 20);
            this.cmbView.StyleController = this.LayoutControl;
            this.cmbView.TabIndex = 7;
            // 
            // ltItemView
            // 
            this.ltItemView.Control = this.cmbView;
            this.ltItemView.CustomizationFormText = "Представление";
            this.ltItemView.Location = new System.Drawing.Point(0, 142);
            this.ltItemView.Name = "ltItemView";
            this.ltItemView.Size = new System.Drawing.Size(380, 24);
            this.ltItemView.Text = "Представление:";
            this.ltItemView.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbViewDocs
            // 
            this.cmbViewDocs.Location = new System.Drawing.Point(132, 202);
            this.cmbViewDocs.Name = "cmbViewDocs";
            this.cmbViewDocs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbViewDocs.Size = new System.Drawing.Size(256, 20);
            this.cmbViewDocs.StyleController = this.LayoutControl;
            this.cmbViewDocs.TabIndex = 8;
            // 
            // ltItemDocView
            // 
            this.ltItemDocView.Control = this.cmbViewDocs;
            this.ltItemDocView.CustomizationFormText = "Документы";
            this.ltItemDocView.Location = new System.Drawing.Point(0, 190);
            this.ltItemDocView.Name = "ltItemDocView";
            this.ltItemDocView.Size = new System.Drawing.Size(380, 24);
            this.ltItemDocView.Text = "Документы:";
            this.ltItemDocView.TextSize = new System.Drawing.Size(116, 13);
            // 
            // numSortOrder
            // 
            this.numSortOrder.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSortOrder.Location = new System.Drawing.Point(132, 226);
            this.numSortOrder.Name = "numSortOrder";
            this.numSortOrder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numSortOrder.Properties.IsFloatValue = false;
            this.numSortOrder.Properties.Mask.EditMask = "n0";
            this.numSortOrder.Size = new System.Drawing.Size(256, 20);
            this.numSortOrder.StyleController = this.LayoutControl;
            this.numSortOrder.TabIndex = 10;
            // 
            // ltItemOrder
            // 
            this.ltItemOrder.Control = this.numSortOrder;
            this.ltItemOrder.CustomizationFormText = "Сортировка";
            this.ltItemOrder.Location = new System.Drawing.Point(0, 214);
            this.ltItemOrder.Name = "ltItemOrder";
            this.ltItemOrder.Size = new System.Drawing.Size(380, 24);
            this.ltItemOrder.Text = "Сортировка:";
            this.ltItemOrder.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtParent
            // 
            this.txtParent.Location = new System.Drawing.Point(132, 250);
            this.txtParent.Name = "txtParent";
            this.txtParent.Properties.ReadOnly = true;
            this.txtParent.Size = new System.Drawing.Size(256, 20);
            this.txtParent.StyleController = this.LayoutControl;
            this.txtParent.TabIndex = 12;
            // 
            // ltItemParent
            // 
            this.ltItemParent.Control = this.txtParent;
            this.ltItemParent.CustomizationFormText = "Родитель";
            this.ltItemParent.Location = new System.Drawing.Point(0, 238);
            this.ltItemParent.Name = "ltItemParent";
            this.ltItemParent.Size = new System.Drawing.Size(380, 24);
            this.ltItemParent.Text = "Родитель:";
            this.ltItemParent.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbVirtualBuild
            // 
            this.cmbVirtualBuild.Location = new System.Drawing.Point(132, 178);
            this.cmbVirtualBuild.Name = "cmbVirtualBuild";
            this.cmbVirtualBuild.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbVirtualBuild.Size = new System.Drawing.Size(256, 20);
            this.cmbVirtualBuild.StyleController = this.LayoutControl;
            this.cmbVirtualBuild.TabIndex = 13;
            // 
            // ltItemVirtualBuild
            // 
            this.ltItemVirtualBuild.Control = this.cmbVirtualBuild;
            this.ltItemVirtualBuild.CustomizationFormText = "Построитель групп";
            this.ltItemVirtualBuild.Location = new System.Drawing.Point(0, 166);
            this.ltItemVirtualBuild.Name = "ltItemVirtualBuild";
            this.ltItemVirtualBuild.Size = new System.Drawing.Size(380, 24);
            this.ltItemVirtualBuild.Text = "Построитель групп:";
            this.ltItemVirtualBuild.TextSize = new System.Drawing.Size(116, 13);
            // 
            // GridSubKind
            // 
            this.GridSubKind.Location = new System.Drawing.Point(12, 290);
            this.GridSubKind.Name = "GridSubKind";
            this.GridSubKind.Size = new System.Drawing.Size(376, 88);
            this.GridSubKind.TabIndex = 14;
            // 
            // layoutControlItemContentKinds
            // 
            this.layoutControlItemContentKinds.Control = this.GridSubKind;
            this.layoutControlItemContentKinds.CustomizationFormText = "Содержание";
            this.layoutControlItemContentKinds.Location = new System.Drawing.Point(0, 262);
            this.layoutControlItemContentKinds.Name = "layoutControlItemContentKinds";
            this.layoutControlItemContentKinds.Size = new System.Drawing.Size(380, 108);
            this.layoutControlItemContentKinds.Text = "Содержание:";
            this.layoutControlItemContentKinds.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemContentKinds.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlHierarchy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 480);
            this.Name = "ControlHierarchy";
            this.Size = new System.Drawing.Size(400, 480);
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbViewDocs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemDocView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSortOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbVirtualBuild.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ltItemVirtualBuild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemContentKinds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SpinEdit numSortOrder;
        public DevExpress.XtraEditors.LookUpEdit cmbViewDocs;
        public DevExpress.XtraEditors.TextEdit txtParent;
        public DevExpress.XtraEditors.LookUpEdit cmbView;
        public DevExpress.XtraEditors.LookUpEdit cmbVirtualBuild;
        public DevExpress.XtraLayout.LayoutControlItem ltItemParent;
        public DevExpress.XtraLayout.LayoutControlItem ltItemVirtualBuild;
        public DevExpress.XtraLayout.LayoutControlItem ltItemOrder;
        public DevExpress.XtraLayout.LayoutControlItem ltItemDocView;
        public DevExpress.XtraLayout.LayoutControlItem ltItemView;
        public ControlList GridSubKind;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemContentKinds;
    }
}
