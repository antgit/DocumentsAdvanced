namespace BusinessObjects.Windows.Controls
{
    partial class ControlState
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
            this.LayoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.chkKinds = new DevExpress.XtraEditors.RadioGroup();
            this.chlFlags = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.cmbState = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemState = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFlag = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemKinds = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkKinds.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chlFlags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKinds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.AllowCustomizationMenu = false;
            this.LayoutControl.Controls.Add(this.chkKinds);
            this.LayoutControl.Controls.Add(this.chlFlags);
            this.LayoutControl.Controls.Add(this.cmbState);
            this.LayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutControl.Location = new System.Drawing.Point(0, 0);
            this.LayoutControl.Name = "LayoutControl";
            this.LayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(442, 39, 250, 350);
            this.LayoutControl.Root = this.layoutControlGroup1;
            this.LayoutControl.Size = new System.Drawing.Size(400, 250);
            this.LayoutControl.TabIndex = 0;
            this.LayoutControl.Text = "layoutControl";
            // 
            // chkKinds
            // 
            this.chkKinds.AutoSizeInLayoutControl = true;
            this.chkKinds.Location = new System.Drawing.Point(12, 177);
            this.chkKinds.Name = "chkKinds";
            this.chkKinds.Size = new System.Drawing.Size(376, 51);
            this.chkKinds.StyleController = this.LayoutControl;
            this.chkKinds.TabIndex = 7;
            // 
            // chlFlags
            // 
            this.chlFlags.Location = new System.Drawing.Point(12, 52);
            this.chlFlags.Name = "chlFlags";
            this.chlFlags.Size = new System.Drawing.Size(376, 105);
            this.chlFlags.StyleController = this.LayoutControl;
            this.chlFlags.TabIndex = 5;
            // 
            // cmbState
            // 
            this.cmbState.Location = new System.Drawing.Point(74, 12);
            this.cmbState.Name = "cmbState";
            this.cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbState.Size = new System.Drawing.Size(314, 20);
            this.cmbState.StyleController = this.LayoutControl;
            this.cmbState.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Основные";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemState,
            this.layoutControlItemFlag,
            this.layoutControlItemKinds,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(400, 250);
            this.layoutControlGroup1.Text = "Основные";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItemState
            // 
            this.layoutControlItemState.Control = this.cmbState;
            this.layoutControlItemState.CustomizationFormText = "Состояние";
            this.layoutControlItemState.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemState.Name = "layoutControlItemState";
            this.layoutControlItemState.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemState.Text = "Состояние:";
            this.layoutControlItemState.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItemFlag
            // 
            this.layoutControlItemFlag.Control = this.chlFlags;
            this.layoutControlItemFlag.CustomizationFormText = "Флаг";
            this.layoutControlItemFlag.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemFlag.MaxSize = new System.Drawing.Size(0, 125);
            this.layoutControlItemFlag.MinSize = new System.Drawing.Size(62, 125);
            this.layoutControlItemFlag.Name = "layoutControlItemFlag";
            this.layoutControlItemFlag.Size = new System.Drawing.Size(380, 125);
            this.layoutControlItemFlag.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemFlag.Text = "Флаг:";
            this.layoutControlItemFlag.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemFlag.TextSize = new System.Drawing.Size(58, 13);
            // 
            // layoutControlItemKinds
            // 
            this.layoutControlItemKinds.Control = this.chkKinds;
            this.layoutControlItemKinds.CustomizationFormText = "Вид";
            this.layoutControlItemKinds.Location = new System.Drawing.Point(0, 149);
            this.layoutControlItemKinds.Name = "layoutControlItemKinds";
            this.layoutControlItemKinds.Size = new System.Drawing.Size(380, 71);
            this.layoutControlItemKinds.Text = "Вид";
            this.layoutControlItemKinds.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemKinds.TextSize = new System.Drawing.Size(58, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 220);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(380, 10);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ControlState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LayoutControl);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "ControlState";
            this.Size = new System.Drawing.Size(400, 250);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkKinds.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chlFlags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKinds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        public DevExpress.XtraEditors.CheckedListBoxControl chlFlags;
        public DevExpress.XtraEditors.LookUpEdit cmbState;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemState;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemFlag;
        public DevExpress.XtraLayout.LayoutControl LayoutControl;
        public DevExpress.XtraEditors.RadioGroup chkKinds;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemKinds;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
