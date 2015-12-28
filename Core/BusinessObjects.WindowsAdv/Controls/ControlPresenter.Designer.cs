namespace BusinessObjects.Windows.Controls
{
    partial class ControlPresenter
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
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.pictureEdit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel
            // 
            this.linkLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel.Location = new System.Drawing.Point(135, 0);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(265, 186);
            this.linkLabel.TabIndex = 2;
            // 
            // pictureEdit
            // 
            this.pictureEdit.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Size = new System.Drawing.Size(135, 186);
            this.pictureEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureEdit.TabIndex = 3;
            this.pictureEdit.TabStop = false;
            // 
            // ControlPresenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.pictureEdit);
            this.MaximumSize = new System.Drawing.Size(400, 186);
            this.MinimumSize = new System.Drawing.Size(400, 186);
            this.Name = "ControlPresenter";
            this.Size = new System.Drawing.Size(400, 186);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.LinkLabel linkLabel;
        public System.Windows.Forms.PictureBox pictureEdit;


    }
}
