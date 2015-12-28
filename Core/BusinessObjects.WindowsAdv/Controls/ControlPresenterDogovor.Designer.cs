namespace BusinessObjects.Windows.Controls
{
    partial class ControlPresenterDogovor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPresenterDogovor));
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel
            // 
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "\r\nУчет договоров\r\n\r\nУчет договора в которых организация является исполнителем или" +
                " заказчиком.\r\n\r\nОтслеживание состояний договоров. \r\n";
            // 
            // pictureEdit
            // 
            this.pictureEdit.Image = ((System.Drawing.Image)(resources.GetObject("pictureEdit.Image")));
            // 
            // ControlPresenterDogovor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlPresenterDogovor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
