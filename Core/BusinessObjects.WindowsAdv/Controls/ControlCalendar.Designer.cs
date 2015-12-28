namespace BusinessObjects.Windows.Controls
{
    partial class ControlCalendar
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
            this.layoutControlItemStartTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.edStartTime = new DevExpress.XtraEditors.TimeEdit();
            this.edStartDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlItemStartDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbPriorityId = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewPriority = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItemPriorityId = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPriorityId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPriorityId)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 218);
            this.txtMemo.Size = new System.Drawing.Size(376, 85);
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
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 65);
            // 
            // txtCodeFind
            // 
            // 
            // txtNameFull2
            // 
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbPriorityId);
            this.LayoutControl.Controls.Add(this.edStartDate);
            this.LayoutControl.Controls.Add(this.edStartTime);
            this.LayoutControl.Size = new System.Drawing.Size(400, 315);
            this.LayoutControl.Controls.SetChildIndex(this.edStartTime, 0);
            this.LayoutControl.Controls.SetChildIndex(this.edStartDate, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbPriorityId, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemStartTime,
            this.layoutControlItemStartDate,
            this.layoutControlItemPriorityId});
            this.layoutControlGroup.Size = new System.Drawing.Size(400, 275);
            // 
            // layoutControlItemStartTime
            // 
            this.layoutControlItemStartTime.Control = this.edStartTime;
            this.layoutControlItemStartTime.CustomizationFormText = "Время";
            this.layoutControlItemStartTime.Location = new System.Drawing.Point(231, 166);
            this.layoutControlItemStartTime.MaxSize = new System.Drawing.Size(149, 24);
            this.layoutControlItemStartTime.MinSize = new System.Drawing.Size(149, 24);
            this.layoutControlItemStartTime.Name = "layoutControlItemStartTime";
            this.layoutControlItemStartTime.Size = new System.Drawing.Size(149, 24);
            this.layoutControlItemStartTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemStartTime.Text = "Время:";
            this.layoutControlItemStartTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItemStartTime.TextSize = new System.Drawing.Size(34, 13);
            this.layoutControlItemStartTime.TextToControlDistance = 5;
            // 
            // edStartTime
            // 
            this.edStartTime.EditValue = null;
            this.edStartTime.Location = new System.Drawing.Point(282, 178);
            this.edStartTime.Name = "edStartTime";
            this.edStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edStartTime.Properties.DisplayFormat.FormatString = "T";
            this.edStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.edStartTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.edStartTime.Size = new System.Drawing.Size(106, 20);
            this.edStartTime.StyleController = this.LayoutControl;
            this.edStartTime.TabIndex = 9;
            // 
            // edStartDate
            // 
            this.edStartDate.EditValue = null;
            this.edStartDate.Location = new System.Drawing.Point(132, 178);
            this.edStartDate.Name = "edStartDate";
            this.edStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.edStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edStartDate.Size = new System.Drawing.Size(107, 20);
            this.edStartDate.StyleController = this.LayoutControl;
            this.edStartDate.TabIndex = 10;
            // 
            // layoutControlItemStartDate
            // 
            this.layoutControlItemStartDate.Control = this.edStartDate;
            this.layoutControlItemStartDate.CustomizationFormText = "Дата";
            this.layoutControlItemStartDate.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItemStartDate.MaxSize = new System.Drawing.Size(231, 24);
            this.layoutControlItemStartDate.MinSize = new System.Drawing.Size(231, 24);
            this.layoutControlItemStartDate.Name = "layoutControlItemStartDate";
            this.layoutControlItemStartDate.Size = new System.Drawing.Size(231, 24);
            this.layoutControlItemStartDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemStartDate.Text = "Дата:";
            this.layoutControlItemStartDate.TextSize = new System.Drawing.Size(116, 13);
            // 
            // cmbPriorityId
            // 
            this.cmbPriorityId.Location = new System.Drawing.Point(132, 154);
            this.cmbPriorityId.Name = "cmbPriorityId";
            this.cmbPriorityId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPriorityId.Properties.View = this.ViewPriority;
            this.cmbPriorityId.Size = new System.Drawing.Size(256, 20);
            this.cmbPriorityId.StyleController = this.LayoutControl;
            this.cmbPriorityId.TabIndex = 19;
            // 
            // ViewPriority
            // 
            this.ViewPriority.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewPriority.Name = "ViewPriority";
            this.ViewPriority.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewPriority.OptionsView.ShowGroupPanel = false;
            this.ViewPriority.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItemPriorityId
            // 
            this.layoutControlItemPriorityId.Control = this.cmbPriorityId;
            this.layoutControlItemPriorityId.CustomizationFormText = "Приоритет";
            this.layoutControlItemPriorityId.Location = new System.Drawing.Point(0, 142);
            this.layoutControlItemPriorityId.Name = "layoutControlItemPriorityId";
            this.layoutControlItemPriorityId.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemPriorityId.Text = "Приоритет:";
            this.layoutControlItemPriorityId.TextSize = new System.Drawing.Size(116, 13);
            // 
            // ControlCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.MinimumSize = new System.Drawing.Size(400, 315);
            this.Name = "ControlCalendar";
            this.Size = new System.Drawing.Size(400, 315);
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
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPriorityId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPriorityId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemStartTime;
        public DevExpress.XtraEditors.DateEdit edStartDate;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemStartDate;
        public DevExpress.XtraEditors.TimeEdit edStartTime;
        public DevExpress.XtraEditors.GridLookUpEdit cmbPriorityId;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewPriority;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemPriorityId;

    }
}
