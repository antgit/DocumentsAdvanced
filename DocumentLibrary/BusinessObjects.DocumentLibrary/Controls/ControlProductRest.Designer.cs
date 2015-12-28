namespace BusinessObjects.DocumentLibrary.Controls
{
    partial class ControlProductRest
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.TreeList = new DevExpress.XtraTreeList.TreeList();
            this.GridSelected = new DevExpress.XtraGrid.GridControl();
            this.ViewSelected = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.GridProducts = new DevExpress.XtraGrid.GridControl();
            this.ViewProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.TreeList);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.GridSelected);
            this.splitContainerControl1.Panel2.Controls.Add(this.splitterControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.GridProducts);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(793, 432);
            this.splitContainerControl1.SplitterPosition = 179;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // TreeList
            // 
            this.TreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeList.Location = new System.Drawing.Point(0, 0);
            this.TreeList.Name = "TreeList";
            this.TreeList.Size = new System.Drawing.Size(179, 432);
            this.TreeList.TabIndex = 0;
            // 
            // GridSelected
            // 
            this.GridSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridSelected.Location = new System.Drawing.Point(0, 291);
            this.GridSelected.MainView = this.ViewSelected;
            this.GridSelected.Name = "GridSelected";
            this.GridSelected.Size = new System.Drawing.Size(608, 141);
            this.GridSelected.TabIndex = 2;
            this.GridSelected.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewSelected});
            // 
            // ViewSelected
            // 
            this.ViewSelected.GridControl = this.GridSelected;
            this.ViewSelected.Name = "ViewSelected";
            this.ViewSelected.OptionsBehavior.Editable = false;
            this.ViewSelected.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewSelected.OptionsView.ShowGroupPanel = false;
            this.ViewSelected.OptionsView.ShowIndicator = false;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 285);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(608, 6);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // GridProducts
            // 
            this.GridProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.GridProducts.Location = new System.Drawing.Point(0, 0);
            this.GridProducts.MainView = this.ViewProducts;
            this.GridProducts.Name = "GridProducts";
            this.GridProducts.Size = new System.Drawing.Size(608, 285);
            this.GridProducts.TabIndex = 0;
            this.GridProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewProducts});
            // 
            // ViewProducs
            // 
            this.ViewProducts.GridControl = this.GridProducts;
            this.ViewProducts.Name = "ViewProducs";
            this.ViewProducts.OptionsView.ShowGroupPanel = false;
            this.ViewProducts.OptionsView.ShowIndicator = false;
            // 
            // ControlProductRest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ControlProductRest";
            this.Size = new System.Drawing.Size(793, 432);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        public DevExpress.XtraGrid.GridControl GridSelected;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewSelected;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        public DevExpress.XtraGrid.GridControl GridProducts;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewProducts;
        public DevExpress.XtraTreeList.TreeList TreeList;


    }
}
