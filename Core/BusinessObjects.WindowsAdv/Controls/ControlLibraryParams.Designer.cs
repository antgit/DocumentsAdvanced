namespace BusinessObjects.Windows.Controls
{
    partial class ControlLibraryParams
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
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.Grid = new DevExpress.XtraGrid.GridControl();
            this.View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtAfter = new DevExpress.XtraEditors.TextEdit();
            this.txtBefore = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroupMain = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlBefore = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlAfter = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGrid = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAfter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBefore.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.Grid);
            this.layoutControlMain.Controls.Add(this.txtAfter);
            this.layoutControlMain.Controls.Add(this.txtBefore);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroupMain;
            this.layoutControlMain.Size = new System.Drawing.Size(565, 454);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControlMain";
            // 
            // Grid
            // 
            this.Grid.Location = new System.Drawing.Point(12, 60);
            this.Grid.MainView = this.View;
            this.Grid.Name = "Grid";
            this.Grid.ShowOnlyPredefinedDetails = true;
            this.Grid.Size = new System.Drawing.Size(541, 382);
            this.Grid.TabIndex = 6;
            this.Grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View});
            // 
            // View
            // 
            this.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.View.GridControl = this.Grid;
            this.View.Name = "View";
            this.View.OptionsBehavior.Editable = false;
            this.View.OptionsDetail.EnableMasterViewMode = false;
            this.View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.View.OptionsView.ShowGroupPanel = false;
            this.View.OptionsView.ShowIndicator = false;
            // 
            // txtAfter
            // 
            this.txtAfter.Location = new System.Drawing.Point(89, 36);
            this.txtAfter.Name = "txtAfter";
            this.txtAfter.Size = new System.Drawing.Size(464, 20);
            this.txtAfter.StyleController = this.layoutControlMain;
            this.txtAfter.TabIndex = 5;
            // 
            // txtBefore
            // 
            this.txtBefore.Location = new System.Drawing.Point(89, 12);
            this.txtBefore.Name = "txtBefore";
            this.txtBefore.Size = new System.Drawing.Size(464, 20);
            this.txtBefore.StyleController = this.layoutControlMain;
            this.txtBefore.TabIndex = 4;
            // 
            // layoutControlGroupMain
            // 
            this.layoutControlGroupMain.CustomizationFormText = "layoutControlGroupMain";
            this.layoutControlGroupMain.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupMain.GroupBordersVisible = false;
            this.layoutControlGroupMain.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlBefore,
            this.layoutControlAfter,
            this.layoutControlGrid});
            this.layoutControlGroupMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupMain.Name = "layoutControlGroupMain";
            this.layoutControlGroupMain.Size = new System.Drawing.Size(565, 454);
            this.layoutControlGroupMain.Text = "layoutControlGroupMain";
            this.layoutControlGroupMain.TextVisible = false;
            // 
            // layoutControlBefore
            // 
            this.layoutControlBefore.Control = this.txtBefore;
            this.layoutControlBefore.CustomizationFormText = "Строка до";
            this.layoutControlBefore.Location = new System.Drawing.Point(0, 0);
            this.layoutControlBefore.Name = "layoutControlBefore";
            this.layoutControlBefore.Size = new System.Drawing.Size(545, 24);
            this.layoutControlBefore.Text = "Строка до:";
            this.layoutControlBefore.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlAfter
            // 
            this.layoutControlAfter.Control = this.txtAfter;
            this.layoutControlAfter.CustomizationFormText = "Строка после";
            this.layoutControlAfter.Location = new System.Drawing.Point(0, 24);
            this.layoutControlAfter.Name = "layoutControlAfter";
            this.layoutControlAfter.Size = new System.Drawing.Size(545, 24);
            this.layoutControlAfter.Text = "Строка после:";
            this.layoutControlAfter.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlGrid
            // 
            this.layoutControlGrid.Control = this.Grid;
            this.layoutControlGrid.CustomizationFormText = "layoutControlGrid";
            this.layoutControlGrid.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGrid.Name = "layoutControlGrid";
            this.layoutControlGrid.Size = new System.Drawing.Size(545, 386);
            this.layoutControlGrid.Text = "layoutControlGrid";
            this.layoutControlGrid.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlGrid.TextToControlDistance = 0;
            this.layoutControlGrid.TextVisible = false;
            // 
            // ControlLibraryParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlMain);
            this.MinimumSize = new System.Drawing.Size(565, 454);
            this.Name = "ControlLibraryParams";
            this.Size = new System.Drawing.Size(565, 454);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAfter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBefore.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMain;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlBefore;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlAfter;
        public DevExpress.XtraEditors.TextEdit txtBefore;
        public DevExpress.XtraEditors.TextEdit txtAfter;
        public DevExpress.XtraGrid.GridControl Grid;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView View;
    }
}
