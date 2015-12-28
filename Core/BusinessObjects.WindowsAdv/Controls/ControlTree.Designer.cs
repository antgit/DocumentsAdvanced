namespace BusinessObjects.Windows.Controls
{
    partial class ControlTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlTree));
            this.Tree = new DevExpress.XtraTreeList.TreeList();
            this.ImageCollection = new DevExpress.Utils.ImageCollection();
            this.ToolTipController = new DevExpress.Utils.ToolTipController();
            ((System.ComponentModel.ISupportInitialize)(this.Tree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // Tree
            // 
            this.Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tree.Location = new System.Drawing.Point(0, 0);
            this.Tree.Name = "Tree";
            this.Tree.OptionsBehavior.AutoPopulateColumns = false;
            this.Tree.OptionsBehavior.Editable = false;
            this.Tree.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Tree.OptionsView.ShowIndicator = false;
            this.Tree.OptionsView.ShowRoot = false;
            this.Tree.SelectImageList = this.ImageCollection;
            this.Tree.Size = new System.Drawing.Size(250, 250);
            this.Tree.TabIndex = 0;
            this.Tree.ToolTipController = this.ToolTipController;
            // 
            // ImageCollection
            // 
            this.ImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ImageCollection.ImageStream")));
            // 
            // ToolTipController
            // 
            this.ToolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // ControlTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Tree);
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "ControlTree";
            this.Size = new System.Drawing.Size(250, 250);
            ((System.ComponentModel.ISupportInitialize)(this.Tree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraTreeList.TreeList Tree;
        public DevExpress.Utils.ImageCollection ImageCollection;
        public DevExpress.Utils.ToolTipController ToolTipController;

    }
}
