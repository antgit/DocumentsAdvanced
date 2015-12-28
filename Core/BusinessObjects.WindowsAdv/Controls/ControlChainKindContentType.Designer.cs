namespace BusinessObjects.Windows.Controls
{
    partial class ControlChainKindContentType
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
            this.cmbSourceKinds = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewSourceKinds = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbDestinationKinds = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewDestinationKinds = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkAnySource = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSourceKinds.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewSourceKinds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDestinationKinds.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDestinationKinds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAnySource.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSourceKinds
            // 
            this.cmbSourceKinds.Location = new System.Drawing.Point(114, 30);
            this.cmbSourceKinds.Name = "cmbSourceKinds";
            this.cmbSourceKinds.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSourceKinds.Properties.View = this.ViewSourceKinds;
            this.cmbSourceKinds.Size = new System.Drawing.Size(246, 20);
            this.cmbSourceKinds.TabIndex = 0;
            // 
            // ViewSourceKinds
            // 
            this.ViewSourceKinds.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewSourceKinds.Name = "ViewSourceKinds";
            this.ViewSourceKinds.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewSourceKinds.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Подтип источника:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(4, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(104, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Подтип назначения:";
            // 
            // cmbDestinationKinds
            // 
            this.cmbDestinationKinds.Location = new System.Drawing.Point(114, 56);
            this.cmbDestinationKinds.Name = "cmbDestinationKinds";
            this.cmbDestinationKinds.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDestinationKinds.Properties.View = this.ViewDestinationKinds;
            this.cmbDestinationKinds.Size = new System.Drawing.Size(246, 20);
            this.cmbDestinationKinds.TabIndex = 2;
            // 
            // ViewDestinationKinds
            // 
            this.ViewDestinationKinds.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewDestinationKinds.Name = "ViewDestinationKinds";
            this.ViewDestinationKinds.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewDestinationKinds.OptionsView.ShowGroupPanel = false;
            // 
            // chkAnySource
            // 
            this.chkAnySource.Location = new System.Drawing.Point(4, 8);
            this.chkAnySource.Name = "chkAnySource";
            this.chkAnySource.Properties.Caption = "Подтип источника может быть любым";
            this.chkAnySource.Size = new System.Drawing.Size(230, 19);
            this.chkAnySource.TabIndex = 4;
            // 
            // ControlChainKindContentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkAnySource);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cmbDestinationKinds);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbSourceKinds);
            this.Name = "ControlChainKindContentType";
            this.Size = new System.Drawing.Size(363, 91);
            ((System.ComponentModel.ISupportInitialize)(this.cmbSourceKinds.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewSourceKinds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDestinationKinds.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDestinationKinds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAnySource.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.CheckEdit chkAnySource;
        public DevExpress.XtraEditors.GridLookUpEdit cmbSourceKinds;
        public DevExpress.XtraEditors.GridLookUpEdit cmbDestinationKinds;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewDestinationKinds;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewSourceKinds;
    }
}
