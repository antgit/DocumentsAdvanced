namespace BusinessObjects.Windows.Controls
{
    partial class ControlTerritory
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
            this.cmbCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridViewCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemCountry = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbTown = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridViewControlCenter = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.editLocation = new DevExpress.XtraEditors.PopupContainerEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtNameNational = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemNameNational = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtNameInternational = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemNameInternational = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewControlCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameNational.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameNational)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameInternational.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameInternational)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 290);
            this.txtMemo.Size = new System.Drawing.Size(376, 38);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(132, 84);
            // 
            // txtName
            // 
            // 
            // layoutControlItemCode
            // 
            this.layoutControlItemCode.Location = new System.Drawing.Point(0, 72);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 262);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 58);
            // 
            // txtCodeFind
            // 
            this.txtCodeFind.Location = new System.Drawing.Point(132, 108);
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.Location = new System.Drawing.Point(0, 96);
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
            this.LayoutControl.Controls.Add(this.txtNameInternational);
            this.LayoutControl.Controls.Add(this.txtNameNational);
            this.LayoutControl.Controls.Add(this.editLocation);
            this.LayoutControl.Controls.Add(this.cmbCountry);
            this.LayoutControl.Controls.Add(this.cmbTown);
            this.LayoutControl.Size = new System.Drawing.Size(400, 340);
            this.LayoutControl.Controls.SetChildIndex(this.cmbTown, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbCountry, 0);
            this.LayoutControl.Controls.SetChildIndex(this.editLocation, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameNational, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameInternational, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCountry,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItemNameNational,
            this.layoutControlItemNameInternational});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 340);
            // 
            // cmbCountry
            // 
            this.cmbCountry.Location = new System.Drawing.Point(132, 202);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCountry.Properties.View = this.gridViewCountry;
            this.cmbCountry.Size = new System.Drawing.Size(256, 20);
            this.cmbCountry.StyleController = this.LayoutControl;
            this.cmbCountry.TabIndex = 7;
            // 
            // gridViewCountry
            // 
            this.gridViewCountry.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewCountry.Name = "gridViewCountry";
            this.gridViewCountry.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewCountry.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItemCountry
            // 
            this.layoutControlItemCountry.Control = this.cmbCountry;
            this.layoutControlItemCountry.CustomizationFormText = "Страна";
            this.layoutControlItemCountry.Location = new System.Drawing.Point(0, 190);
            this.layoutControlItemCountry.Name = "layoutControlItemCountry";
            this.layoutControlItemCountry.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemCountry.Text = "Страна:";
            this.layoutControlItemCountry.TextSize = new System.Drawing.Size(116, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbTown;
            this.layoutControlItem1.CustomizationFormText = "Цент управления";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem1.Text = "Цент управления:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbTown
            // 
            this.cmbTown.Location = new System.Drawing.Point(132, 226);
            this.cmbTown.Name = "cmbTown";
            this.cmbTown.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.cmbTown.Properties.View = this.gridViewControlCenter;
            this.cmbTown.Size = new System.Drawing.Size(256, 20);
            this.cmbTown.StyleController = this.LayoutControl;
            this.cmbTown.TabIndex = 9;
            // 
            // gridViewControlCenter
            // 
            this.gridViewControlCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewControlCenter.Name = "gridViewControlCenter";
            this.gridViewControlCenter.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewControlCenter.OptionsView.ShowGroupPanel = false;
            // 
            // editLocation
            // 
            this.editLocation.Location = new System.Drawing.Point(132, 250);
            this.editLocation.Name = "editLocation";
            this.editLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.editLocation.Size = new System.Drawing.Size(256, 20);
            this.editLocation.StyleController = this.LayoutControl;
            this.editLocation.TabIndex = 10;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.editLocation;
            this.layoutControlItem2.CustomizationFormText = "Расположение";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 238);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItem2.Text = "Расположение:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtNameNational
            // 
            this.txtNameNational.Location = new System.Drawing.Point(132, 60);
            this.txtNameNational.Name = "txtNameNational";
            this.txtNameNational.Size = new System.Drawing.Size(256, 20);
            this.txtNameNational.StyleController = this.LayoutControl;
            this.txtNameNational.TabIndex = 11;
            // 
            // layoutControlItemNameNational
            // 
            this.layoutControlItemNameNational.Control = this.txtNameNational;
            this.layoutControlItemNameNational.CustomizationFormText = "Национальное";
            this.layoutControlItemNameNational.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemNameNational.Name = "layoutControlItemNameNational";
            this.layoutControlItemNameNational.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemNameNational.Text = "Национальное:";
            this.layoutControlItemNameNational.TextSize = new System.Drawing.Size(116, 13);
            // 
            // txtNameInternational
            // 
            this.txtNameInternational.Location = new System.Drawing.Point(132, 36);
            this.txtNameInternational.Name = "txtNameInternational";
            this.txtNameInternational.Size = new System.Drawing.Size(256, 20);
            this.txtNameInternational.StyleController = this.LayoutControl;
            this.txtNameInternational.TabIndex = 12;
            // 
            // layoutControlItemNameInternational
            // 
            this.layoutControlItemNameInternational.Control = this.txtNameInternational;
            this.layoutControlItemNameInternational.CustomizationFormText = "Международное";
            this.layoutControlItemNameInternational.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemNameInternational.Name = "layoutControlItemNameInternational";
            this.layoutControlItemNameInternational.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemNameInternational.Text = "Международное:";
            this.layoutControlItemNameInternational.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlTerritory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "ControlTerritory";
            this.Size = new System.Drawing.Size(400, 340);
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewControlCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameNational.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameNational)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameInternational.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameInternational)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemCountry;
        public DevExpress.XtraEditors.GridLookUpEdit cmbCountry;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraEditors.PopupContainerEdit editLocation;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewCountry;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbTown;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewControlCenter;
        public DevExpress.XtraEditors.TextEdit txtNameNational;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemNameNational;
        public DevExpress.XtraEditors.TextEdit txtNameInternational;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemNameInternational;
    }
}
