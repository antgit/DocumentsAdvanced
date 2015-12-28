namespace BusinessObjects.Windows.Controls
{
	partial class ControlTreeListProp
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
            this.SplitActionPanel = new DevExpress.XtraEditors.SplitContainerControl();
            this.SplitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.SplitProperyListControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.ActionsBar = new DevExpress.XtraNavBar.NavBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.SplitActionPanel)).BeginInit();
            this.SplitActionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerControl)).BeginInit();
            this.SplitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitProperyListControl)).BeginInit();
            this.SplitProperyListControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActionsBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitActionPanel
            // 
            this.SplitActionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitActionPanel.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.SplitActionPanel.Location = new System.Drawing.Point(0, 0);
            this.SplitActionPanel.Name = "SplitActionPanel";
            this.SplitActionPanel.Panel1.Controls.Add(this.SplitContainerControl);
            this.SplitActionPanel.Panel2.Controls.Add(this.ActionsBar);
            this.SplitActionPanel.Size = new System.Drawing.Size(600, 318);
            this.SplitActionPanel.SplitterPosition = 210;
            this.SplitActionPanel.TabIndex = 0;
            // 
            // SplitContainerControl
            // 
            this.SplitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerControl.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerControl.Name = "SplitContainerControl";
            this.SplitContainerControl.Panel2.Controls.Add(this.SplitProperyListControl);
            this.SplitContainerControl.Size = new System.Drawing.Size(384, 318);
            this.SplitContainerControl.SplitterPosition = 219;
            this.SplitContainerControl.TabIndex = 0;
            // 
            // SplitProperyListControl
            // 
            this.SplitProperyListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitProperyListControl.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.SplitProperyListControl.Horizontal = false;
            this.SplitProperyListControl.Location = new System.Drawing.Point(0, 0);
            this.SplitProperyListControl.Name = "SplitProperyListControl";
            this.SplitProperyListControl.Size = new System.Drawing.Size(159, 318);
            this.SplitProperyListControl.SplitterPosition = 220;
            this.SplitProperyListControl.TabIndex = 0;
            // 
            // ActionsBar
            // 
            this.ActionsBar.ActiveGroup = null;
            this.ActionsBar.ContentButtonHint = null;
            this.ActionsBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionsBar.Location = new System.Drawing.Point(0, 0);
            this.ActionsBar.Name = "ActionsBar";
            this.ActionsBar.OptionsNavPane.ExpandedWidth = 220;
            this.ActionsBar.Size = new System.Drawing.Size(210, 318);
            this.ActionsBar.TabIndex = 0;
            // 
            // ControlTreeListProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitActionPanel);
            this.Name = "ControlTreeListProp";
            this.Size = new System.Drawing.Size(600, 318);
            ((System.ComponentModel.ISupportInitialize)(this.SplitActionPanel)).EndInit();
            this.SplitActionPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerControl)).EndInit();
            this.SplitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitProperyListControl)).EndInit();
            this.SplitProperyListControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ActionsBar)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        public DevExpress.XtraNavBar.NavBarControl ActionsBar;
        public DevExpress.XtraEditors.SplitContainerControl SplitProperyListControl;
        public DevExpress.XtraEditors.SplitContainerControl SplitActionPanel;
        public DevExpress.XtraEditors.SplitContainerControl SplitContainerControl;
    }
}
