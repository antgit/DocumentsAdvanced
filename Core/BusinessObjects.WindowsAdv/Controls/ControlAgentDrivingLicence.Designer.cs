namespace BusinessObjects.Windows.Controls
{
    partial class ControlAgentDrivingLicence
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbAuthority = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewAuthority = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtRestriction = new DevExpress.XtraEditors.MemoEdit();
            this.dtCategoryExpire = new DevExpress.XtraEditors.DateEdit();
            this.dtCategoryDate = new DevExpress.XtraEditors.DateEdit();
            this.txtCategory = new DevExpress.XtraEditors.TextEdit();
            this.dtExpireDate = new DevExpress.XtraEditors.DateEdit();
            this.txtNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtSeriesNo = new DevExpress.XtraEditors.TextEdit();
            this.dtDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlSeriesNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlExpireDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlCategory = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlCategoryDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlCategoryExpire = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlRestriction = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlAuthority = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAuthority.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAuthority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRestriction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryExpire.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryExpire.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpireDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpireDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeriesNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSeriesNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlExpireDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCategoryDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCategoryExpire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRestriction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAuthority)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbAuthority);
            this.LayoutControl.Controls.Add(this.txtRestriction);
            this.LayoutControl.Controls.Add(this.dtCategoryExpire);
            this.LayoutControl.Controls.Add(this.dtCategoryDate);
            this.LayoutControl.Controls.Add(this.txtCategory);
            this.LayoutControl.Controls.Add(this.dtExpireDate);
            this.LayoutControl.Controls.Add(this.txtNumber);
            this.LayoutControl.Controls.Add(this.txtSeriesNo);
            this.LayoutControl.Controls.Add(this.dtDate);
            this.LayoutControl.Size = new System.Drawing.Size(442, 250);
            // 
            // cmbAuthority
            // 
            this.cmbAuthority.Location = new System.Drawing.Point(155, 84);
            this.cmbAuthority.Name = "cmbAuthority";
            this.cmbAuthority.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbAuthority.Properties.View = this.ViewAuthority;
            this.cmbAuthority.Size = new System.Drawing.Size(275, 20);
            this.cmbAuthority.StyleController = this.LayoutControl;
            this.cmbAuthority.TabIndex = 13;
            // 
            // ViewAuthority
            // 
            this.ViewAuthority.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewAuthority.Name = "ViewAuthority";
            this.ViewAuthority.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewAuthority.OptionsView.ShowGroupPanel = false;
            this.ViewAuthority.OptionsView.ShowIndicator = false;
            // 
            // txtRestriction
            // 
            this.txtRestriction.Location = new System.Drawing.Point(12, 196);
            this.txtRestriction.Name = "txtRestriction";
            this.txtRestriction.Size = new System.Drawing.Size(418, 42);
            this.txtRestriction.StyleController = this.LayoutControl;
            this.txtRestriction.TabIndex = 12;
            // 
            // dtCategoryExpire
            // 
            this.dtCategoryExpire.EditValue = null;
            this.dtCategoryExpire.Location = new System.Drawing.Point(155, 156);
            this.dtCategoryExpire.Name = "dtCategoryExpire";
            this.dtCategoryExpire.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtCategoryExpire.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtCategoryExpire.Size = new System.Drawing.Size(275, 20);
            this.dtCategoryExpire.StyleController = this.LayoutControl;
            this.dtCategoryExpire.TabIndex = 11;
            // 
            // dtCategoryDate
            // 
            this.dtCategoryDate.EditValue = null;
            this.dtCategoryDate.Location = new System.Drawing.Point(155, 132);
            this.dtCategoryDate.Name = "dtCategoryDate";
            this.dtCategoryDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtCategoryDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtCategoryDate.Size = new System.Drawing.Size(275, 20);
            this.dtCategoryDate.StyleController = this.LayoutControl;
            this.dtCategoryDate.TabIndex = 10;
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(155, 108);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(275, 20);
            this.txtCategory.StyleController = this.LayoutControl;
            this.txtCategory.TabIndex = 9;
            // 
            // dtExpireDate
            // 
            this.dtExpireDate.EditValue = null;
            this.dtExpireDate.Location = new System.Drawing.Point(155, 60);
            this.dtExpireDate.Name = "dtExpireDate";
            this.dtExpireDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtExpireDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtExpireDate.Size = new System.Drawing.Size(275, 20);
            this.dtExpireDate.StyleController = this.LayoutControl;
            this.dtExpireDate.TabIndex = 7;
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(266, 36);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(164, 20);
            this.txtNumber.StyleController = this.LayoutControl;
            this.txtNumber.TabIndex = 6;
            // 
            // txtSeriesNo
            // 
            this.txtSeriesNo.Location = new System.Drawing.Point(155, 36);
            this.txtSeriesNo.Name = "txtSeriesNo";
            this.txtSeriesNo.Size = new System.Drawing.Size(67, 20);
            this.txtSeriesNo.StyleController = this.LayoutControl;
            this.txtSeriesNo.TabIndex = 5;
            // 
            // dtDate
            // 
            this.dtDate.EditValue = null;
            this.dtDate.Location = new System.Drawing.Point(155, 12);
            this.dtDate.Name = "dtDate";
            this.dtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtDate.Size = new System.Drawing.Size(275, 20);
            this.dtDate.StyleController = this.LayoutControl;
            this.dtDate.TabIndex = 4;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.CustomizationFormText = "Водительское удостоверение";
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlDate,
            this.layoutControlSeriesNo,
            this.layoutControlExpireDate,
            this.layoutControlNumber,
            this.layoutControlCategory,
            this.layoutControlCategoryDate,
            this.layoutControlCategoryExpire,
            this.layoutControlRestriction,
            this.layoutControlAuthority});
            this.layoutControlGroup.Size = new System.Drawing.Size(442, 250);
            this.layoutControlGroup.Text = "Водительское удостоверение";
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlDate
            // 
            this.layoutControlDate.Control = this.dtDate;
            this.layoutControlDate.CustomizationFormText = "Дата выдачи";
            this.layoutControlDate.Location = new System.Drawing.Point(0, 0);
            this.layoutControlDate.Name = "layoutControlDate";
            this.layoutControlDate.Size = new System.Drawing.Size(422, 24);
            this.layoutControlDate.Text = "Дата выдачи:";
            this.layoutControlDate.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlSeriesNo
            // 
            this.layoutControlSeriesNo.Control = this.txtSeriesNo;
            this.layoutControlSeriesNo.CustomizationFormText = "Серия";
            this.layoutControlSeriesNo.Location = new System.Drawing.Point(0, 24);
            this.layoutControlSeriesNo.MaxSize = new System.Drawing.Size(214, 24);
            this.layoutControlSeriesNo.MinSize = new System.Drawing.Size(214, 24);
            this.layoutControlSeriesNo.Name = "layoutControlSeriesNo";
            this.layoutControlSeriesNo.Size = new System.Drawing.Size(214, 24);
            this.layoutControlSeriesNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlSeriesNo.Text = "Серия:";
            this.layoutControlSeriesNo.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlExpireDate
            // 
            this.layoutControlExpireDate.Control = this.dtExpireDate;
            this.layoutControlExpireDate.CustomizationFormText = "Дата окончания действия";
            this.layoutControlExpireDate.Location = new System.Drawing.Point(0, 48);
            this.layoutControlExpireDate.Name = "layoutControlExpireDate";
            this.layoutControlExpireDate.Size = new System.Drawing.Size(422, 24);
            this.layoutControlExpireDate.Text = "Дата окончания действия:";
            this.layoutControlExpireDate.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlNumber
            // 
            this.layoutControlNumber.Control = this.txtNumber;
            this.layoutControlNumber.CustomizationFormText = "Номер";
            this.layoutControlNumber.Location = new System.Drawing.Point(214, 24);
            this.layoutControlNumber.Name = "layoutControlNumber";
            this.layoutControlNumber.Size = new System.Drawing.Size(208, 24);
            this.layoutControlNumber.Text = "Номер:";
            this.layoutControlNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlNumber.TextSize = new System.Drawing.Size(35, 13);
            this.layoutControlNumber.TextToControlDistance = 5;
            // 
            // layoutControlCategory
            // 
            this.layoutControlCategory.Control = this.txtCategory;
            this.layoutControlCategory.CustomizationFormText = "Категория";
            this.layoutControlCategory.Location = new System.Drawing.Point(0, 96);
            this.layoutControlCategory.Name = "layoutControlCategory";
            this.layoutControlCategory.Size = new System.Drawing.Size(422, 24);
            this.layoutControlCategory.Text = "Категория:";
            this.layoutControlCategory.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlCategoryDate
            // 
            this.layoutControlCategoryDate.Control = this.dtCategoryDate;
            this.layoutControlCategoryDate.CustomizationFormText = "Дата открытия категории";
            this.layoutControlCategoryDate.Location = new System.Drawing.Point(0, 120);
            this.layoutControlCategoryDate.Name = "layoutControlCategoryDate";
            this.layoutControlCategoryDate.Size = new System.Drawing.Size(422, 24);
            this.layoutControlCategoryDate.Text = "Дата открытия категории:";
            this.layoutControlCategoryDate.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlCategoryExpire
            // 
            this.layoutControlCategoryExpire.Control = this.dtCategoryExpire;
            this.layoutControlCategoryExpire.CustomizationFormText = "Дата закрытия категории";
            this.layoutControlCategoryExpire.Location = new System.Drawing.Point(0, 144);
            this.layoutControlCategoryExpire.Name = "layoutControlCategoryExpire";
            this.layoutControlCategoryExpire.Size = new System.Drawing.Size(422, 24);
            this.layoutControlCategoryExpire.Text = "Дата закрытия категории:";
            this.layoutControlCategoryExpire.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlRestriction
            // 
            this.layoutControlRestriction.Control = this.txtRestriction;
            this.layoutControlRestriction.CustomizationFormText = "Ограничения";
            this.layoutControlRestriction.Location = new System.Drawing.Point(0, 168);
            this.layoutControlRestriction.Name = "layoutControlRestriction";
            this.layoutControlRestriction.Size = new System.Drawing.Size(422, 62);
            this.layoutControlRestriction.Text = "Ограничения:";
            this.layoutControlRestriction.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlRestriction.TextSize = new System.Drawing.Size(139, 13);
            // 
            // layoutControlAuthority
            // 
            this.layoutControlAuthority.Control = this.cmbAuthority;
            this.layoutControlAuthority.CustomizationFormText = "Кем выдан";
            this.layoutControlAuthority.Location = new System.Drawing.Point(0, 72);
            this.layoutControlAuthority.Name = "layoutControlAuthority";
            this.layoutControlAuthority.Size = new System.Drawing.Size(422, 24);
            this.layoutControlAuthority.Text = "Кем выдан:";
            this.layoutControlAuthority.TextSize = new System.Drawing.Size(139, 13);
            // 
            // ControlAgentDrivingLicence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LayoutControl);
            this.MinimumSize = new System.Drawing.Size(440, 250);
            this.Name = "ControlAgentDrivingLicence";
            this.Size = new System.Drawing.Size(442, 250);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAuthority.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewAuthority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRestriction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryExpire.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryExpire.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCategoryDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpireDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtExpireDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeriesNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlSeriesNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlExpireDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCategoryDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCategoryExpire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRestriction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAuthority)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlSeriesNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlExpireDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlNumber;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlCategoryDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlCategoryExpire;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlRestriction;
        public DevExpress.XtraEditors.DateEdit dtDate;
        public DevExpress.XtraEditors.TextEdit txtSeriesNo;
        public DevExpress.XtraEditors.TextEdit txtNumber;
        public DevExpress.XtraEditors.GridLookUpEdit cmbAuthority;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewAuthority;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlAuthority;
        public DevExpress.XtraEditors.TextEdit txtCategory;
        public DevExpress.XtraEditors.DateEdit dtCategoryDate;
        public DevExpress.XtraEditors.DateEdit dtCategoryExpire;
        public DevExpress.XtraEditors.MemoEdit txtRestriction;
        public DevExpress.XtraEditors.DateEdit dtExpireDate;
    }
}
