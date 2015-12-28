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
            this.btnShowListAgent = new System.Windows.Forms.Button();
            this.btnShowListProduct = new System.Windows.Forms.Button();
            this.btnShowListGroupAgent = new System.Windows.Forms.Button();
            this.btnShowListGroupProduct = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowListAgent
            // 
            this.btnShowListAgent.Location = new System.Drawing.Point(13, 13);
            this.btnShowListAgent.Name = "btnShowListAgent";
            this.btnShowListAgent.Size = new System.Drawing.Size(150, 23);
            this.btnShowListAgent.TabIndex = 0;
            this.btnShowListAgent.Text = "Список корреспондентов";
            this.btnShowListAgent.UseVisualStyleBackColor = true;
            this.btnShowListAgent.Click += new System.EventHandler(this.btnShowListAgent_Click);
            // 
            // btnShowListProduct
            // 
            this.btnShowListProduct.Location = new System.Drawing.Point(13, 43);
            this.btnShowListProduct.Name = "btnShowListProduct";
            this.btnShowListProduct.Size = new System.Drawing.Size(150, 23);
            this.btnShowListProduct.TabIndex = 1;
            this.btnShowListProduct.Text = "Список товаров";
            this.btnShowListProduct.UseVisualStyleBackColor = true;
            this.btnShowListProduct.Click += new System.EventHandler(this.btnShowListProduct_Click);
            // 
            // btnShowListGroupAgent
            // 
            this.btnShowListGroupAgent.Location = new System.Drawing.Point(169, 13);
            this.btnShowListGroupAgent.Name = "btnShowListGroupAgent";
            this.btnShowListGroupAgent.Size = new System.Drawing.Size(210, 23);
            this.btnShowListGroupAgent.TabIndex = 0;
            this.btnShowListGroupAgent.Text = "Список корреспондентов по группам";
            this.btnShowListGroupAgent.UseVisualStyleBackColor = true;
            this.btnShowListGroupAgent.Click += new System.EventHandler(this.btnShowListGroupAgent_Click);
            // 
            // btnShowListGroupProduct
            // 
            this.btnShowListGroupProduct.Location = new System.Drawing.Point(169, 43);
            this.btnShowListGroupProduct.Name = "btnShowListGroupProduct";
            this.btnShowListGroupProduct.Size = new System.Drawing.Size(210, 23);
            this.btnShowListGroupProduct.TabIndex = 1;
            this.btnShowListGroupProduct.Text = "Список товаров по группам";
            this.btnShowListGroupProduct.UseVisualStyleBackColor = true;
            this.btnShowListGroupProduct.Click += new System.EventHandler(this.btnShowListGroupProduct_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 182);
            this.Controls.Add(this.btnShowListGroupProduct);
            this.Controls.Add(this.btnShowListProduct);
            this.Controls.Add(this.btnShowListGroupAgent);
            this.Controls.Add(this.btnShowListAgent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список...";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowListAgent;
        private System.Windows.Forms.Button btnShowListProduct;
        private System.Windows.Forms.Button btnShowListGroupAgent;
        private System.Windows.Forms.Button btnShowListGroupProduct;
    }
}

