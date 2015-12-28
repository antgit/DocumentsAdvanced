namespace BusinessObjects.Samples
{
    partial class Form1
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
            this.btnShowProperty = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowProperty
            // 
            this.btnShowProperty.Location = new System.Drawing.Point(13, 13);
            this.btnShowProperty.Name = "btnShowProperty";
            this.btnShowProperty.Size = new System.Drawing.Size(165, 23);
            this.btnShowProperty.TabIndex = 0;
            this.btnShowProperty.Text = "Показать окно свойтсв";
            this.btnShowProperty.UseVisualStyleBackColor = true;
            this.btnShowProperty.Click += new System.EventHandler(this.btnShowProperty_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 172);
            this.Controls.Add(this.btnShowProperty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Разработка собственного окна свойств...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowProperty;
    }
}

