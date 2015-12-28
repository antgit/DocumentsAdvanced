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
            this.btnOpenDataBase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenDataBase
            // 
            this.btnOpenDataBase.Location = new System.Drawing.Point(13, 13);
            this.btnOpenDataBase.Name = "btnOpenDataBase";
            this.btnOpenDataBase.Size = new System.Drawing.Size(153, 23);
            this.btnOpenDataBase.TabIndex = 0;
            this.btnOpenDataBase.Text = "Открыть базу данных";
            this.btnOpenDataBase.UseVisualStyleBackColor = true;
            this.btnOpenDataBase.Click += new System.EventHandler(this.btnOpenDataBase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 192);
            this.Controls.Add(this.btnOpenDataBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пример открытия базы данных...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenDataBase;
    }
}

