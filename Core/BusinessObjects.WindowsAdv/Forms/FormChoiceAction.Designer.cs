namespace BusinessObjects.Windows
{
    partial class FormChoiceAction
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
            this.labelHeader = new DevExpress.XtraEditors.LabelControl();
            this.labelMessage = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            // 
            // 
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Size = new System.Drawing.Size(340, 147);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // clientPanel
            // 
            this.clientPanel.Appearance.Options.UseBackColor = true;
            this.clientPanel.Controls.Add(this.labelMessage);
            this.clientPanel.Controls.Add(this.radioGroup);
            this.clientPanel.Controls.Add(this.labelHeader);
            this.clientPanel.Size = new System.Drawing.Size(340, 195);
            // 
            // labelHeader
            // 
            this.labelHeader.AllowHtmlString = true;
            this.labelHeader.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHeader.Location = new System.Drawing.Point(0, 0);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(340, 27);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "labelControl1";
            // 
            // labelMessage
            // 
            this.labelMessage.AllowHtmlString = true;
            this.labelMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessage.Location = new System.Drawing.Point(0, 27);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(340, 82);
            this.labelMessage.TabIndex = 1;
            // 
            // radioGroup
            // 
            this.radioGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radioGroup.Location = new System.Drawing.Point(0, 109);
            this.radioGroup.MenuManager = this.ribbon;
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Size = new System.Drawing.Size(340, 86);
            this.radioGroup.TabIndex = 2;
            // 
            // FormChoiceAction
            // 
            this.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(340, 365);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChoiceAction";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl labelHeader;
        public DevExpress.XtraEditors.LabelControl labelMessage;
        public DevExpress.XtraEditors.RadioGroup radioGroup;
    }
}
