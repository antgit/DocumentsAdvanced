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
            this.btnShowPropertyAgent = new System.Windows.Forms.Button();
            this.btnShowPropertyProduct = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowPropertyAgent
            // 
            this.btnShowPropertyAgent.Location = new System.Drawing.Point(12, 12);
            this.btnShowPropertyAgent.Name = "btnShowPropertyAgent";
            this.btnShowPropertyAgent.Size = new System.Drawing.Size(174, 23);
            this.btnShowPropertyAgent.TabIndex = 0;
            this.btnShowPropertyAgent.Text = "Свойства корреспондента";
            this.btnShowPropertyAgent.UseVisualStyleBackColor = true;
            this.btnShowPropertyAgent.Click += new System.EventHandler(this.btnShowPropertyAgent_Click);
            // 
            // btnShowPropertyProduct
            // 
            this.btnShowPropertyProduct.Location = new System.Drawing.Point(12, 41);
            this.btnShowPropertyProduct.Name = "btnShowPropertyProduct";
            this.btnShowPropertyProduct.Size = new System.Drawing.Size(174, 23);
            this.btnShowPropertyProduct.TabIndex = 0;
            this.btnShowPropertyProduct.Text = "Свойства товара";
            this.btnShowPropertyProduct.UseVisualStyleBackColor = true;
            this.btnShowPropertyProduct.Click += new System.EventHandler(this.btnShowPropertyProduct_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 182);
            this.Controls.Add(this.btnShowPropertyProduct);
            this.Controls.Add(this.btnShowPropertyAgent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Показать свойства...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowPropertyAgent;
        private System.Windows.Forms.Button btnShowPropertyProduct;
    }
}

