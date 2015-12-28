namespace BusinessObjects.Windows
{
    partial class FormGridViewOption
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
            this.ControlGridViewOption = new BusinessObjects.Windows.Controls.ControlGridViewOption();
            this.SuspendLayout();
            // 
            // ControlGridViewOption
            // 
            this.ControlGridViewOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlGridViewOption.Key = null;
            this.ControlGridViewOption.KeyName = null;
            this.ControlGridViewOption.Location = new System.Drawing.Point(0, 0);
            this.ControlGridViewOption.Name = "ControlGridViewOption";
            this.ControlGridViewOption.Size = new System.Drawing.Size(465, 266);
            this.ControlGridViewOption.TabIndex = 0;
            this.ControlGridViewOption.Workarea = null;
            // 
            // FormGridViewOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 266);
            this.Controls.Add(this.ControlGridViewOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormGridViewOption";
            this.Text = "Настройки...";
            this.ResumeLayout(false);

        }

        #endregion

        public Controls.ControlGridViewOption ControlGridViewOption;

    }
}