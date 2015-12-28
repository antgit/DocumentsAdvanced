namespace BusinessObjects.Windows.Controls
{
    partial class ControlModuleTerritory
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
            this.controlListOblast = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItemOblast = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemOblast = new DevExpress.XtraLayout.SplitterItem();
            this.controlListRegion = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItemRegion = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemRegion = new DevExpress.XtraLayout.SplitterItem();
            this.controlListTown = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItemTown = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItemRegionTown = new DevExpress.XtraLayout.SplitterItem();
            this.controlListOblastTown = new BusinessObjects.Windows.Controls.ControlList();
            this.layoutControlItemOblTown = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOblast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemOblast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemRegion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemRegionTown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOblTown)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.controlListOblastTown);
            this.LayoutControl.Controls.Add(this.controlListTown);
            this.LayoutControl.Controls.Add(this.controlListRegion);
            this.LayoutControl.Controls.Add(this.controlListOblast);
            this.LayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1335, 137, 250, 350);
            this.LayoutControl.Size = new System.Drawing.Size(1041, 754);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemOblast,
            this.splitterItemOblast,
            this.layoutControlItemRegion,
            this.layoutControlItemTown,
            this.splitterItemRegionTown,
            this.layoutControlItemOblTown,
            this.splitterItemRegion});
            this.layoutControlGroup.Name = "Root";
            this.layoutControlGroup.Size = new System.Drawing.Size(1041, 754);
            // 
            // controlListOblast
            // 
            this.controlListOblast.Location = new System.Drawing.Point(12, 28);
            this.controlListOblast.Name = "controlListOblast";
            this.controlListOblast.Size = new System.Drawing.Size(246, 714);
            this.controlListOblast.TabIndex = 4;
            // 
            // layoutControlItemOblast
            // 
            this.layoutControlItemOblast.Control = this.controlListOblast;
            this.layoutControlItemOblast.CustomizationFormText = "Области";
            this.layoutControlItemOblast.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemOblast.Name = "layoutControlItemOblast";
            this.layoutControlItemOblast.Size = new System.Drawing.Size(250, 734);
            this.layoutControlItemOblast.Text = "Области";
            this.layoutControlItemOblast.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemOblast.TextSize = new System.Drawing.Size(100, 13);
            // 
            // splitterItemOblast
            // 
            this.splitterItemOblast.AllowHotTrack = true;
            this.splitterItemOblast.CustomizationFormText = "splitterItemOblast";
            this.splitterItemOblast.Location = new System.Drawing.Point(250, 0);
            this.splitterItemOblast.Name = "splitterItemOblast";
            this.splitterItemOblast.Size = new System.Drawing.Size(5, 734);
            // 
            // controlListRegion
            // 
            this.controlListRegion.Location = new System.Drawing.Point(267, 28);
            this.controlListRegion.Name = "controlListRegion";
            this.controlListRegion.Size = new System.Drawing.Size(500, 246);
            this.controlListRegion.TabIndex = 5;
            // 
            // layoutControlItemRegion
            // 
            this.layoutControlItemRegion.Control = this.controlListRegion;
            this.layoutControlItemRegion.CustomizationFormText = "Районы";
            this.layoutControlItemRegion.Location = new System.Drawing.Point(255, 0);
            this.layoutControlItemRegion.Name = "layoutControlItemRegion";
            this.layoutControlItemRegion.Size = new System.Drawing.Size(504, 266);
            this.layoutControlItemRegion.Text = "Районы";
            this.layoutControlItemRegion.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemRegion.TextSize = new System.Drawing.Size(100, 13);
            // 
            // splitterItemRegion
            // 
            this.splitterItemRegion.AllowHotTrack = true;
            this.splitterItemRegion.CustomizationFormText = "splitterItemRegion";
            this.splitterItemRegion.Location = new System.Drawing.Point(255, 266);
            this.splitterItemRegion.Name = "splitterItemRegion";
            this.splitterItemRegion.Size = new System.Drawing.Size(766, 5);
            // 
            // controlListTown
            // 
            this.controlListTown.Location = new System.Drawing.Point(267, 299);
            this.controlListTown.Name = "controlListTown";
            this.controlListTown.Size = new System.Drawing.Size(762, 443);
            this.controlListTown.TabIndex = 6;
            // 
            // layoutControlItemTown
            // 
            this.layoutControlItemTown.Control = this.controlListTown;
            this.layoutControlItemTown.CustomizationFormText = "Города";
            this.layoutControlItemTown.Location = new System.Drawing.Point(255, 271);
            this.layoutControlItemTown.Name = "layoutControlItemTown";
            this.layoutControlItemTown.Size = new System.Drawing.Size(766, 463);
            this.layoutControlItemTown.Text = "Города";
            this.layoutControlItemTown.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemTown.TextSize = new System.Drawing.Size(100, 13);
            // 
            // splitterItemRegionTown
            // 
            this.splitterItemRegionTown.AllowHotTrack = true;
            this.splitterItemRegionTown.CustomizationFormText = "splitterItemRegionTown";
            this.splitterItemRegionTown.Location = new System.Drawing.Point(759, 0);
            this.splitterItemRegionTown.Name = "splitterItemRegionTown";
            this.splitterItemRegionTown.Size = new System.Drawing.Size(5, 266);
            // 
            // controlListOblastTown
            // 
            this.controlListOblastTown.Location = new System.Drawing.Point(776, 28);
            this.controlListOblastTown.Name = "controlListOblastTown";
            this.controlListOblastTown.Size = new System.Drawing.Size(253, 246);
            this.controlListOblastTown.TabIndex = 7;
            // 
            // layoutControlItemOblTown
            // 
            this.layoutControlItemOblTown.Control = this.controlListOblastTown;
            this.layoutControlItemOblTown.CustomizationFormText = "Все города области";
            this.layoutControlItemOblTown.Location = new System.Drawing.Point(764, 0);
            this.layoutControlItemOblTown.Name = "layoutControlItemOblTown";
            this.layoutControlItemOblTown.Size = new System.Drawing.Size(257, 266);
            this.layoutControlItemOblTown.Text = "Все города области";
            this.layoutControlItemOblTown.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItemOblTown.TextSize = new System.Drawing.Size(100, 13);
            // 
            // ControlModuleTerritory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlModuleTerritory";
            this.Size = new System.Drawing.Size(1041, 754);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOblast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemOblast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemRegion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItemRegionTown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemOblTown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemOblast;
        private DevExpress.XtraLayout.SplitterItem splitterItemOblast;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRegion;
        private DevExpress.XtraLayout.SplitterItem splitterItemRegion;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemTown;
        private DevExpress.XtraLayout.SplitterItem splitterItemRegionTown;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemOblTown;
        public ControlList controlListOblast;
        public ControlList controlListRegion;
        public ControlList controlListTown;
        public ControlList controlListOblastTown;
    }
}
