namespace BusinessObjects.Windows.Controls
{
    partial class ControlPresenterService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPresenterService));
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel
            // 
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "\r\nУслуги\r\n\r\nДокументы: акт выполненых работ, акт приема работ, входящие и исходящ" +
                "ие счета, заказ услуг клиентами, заказ услуг  поставщика";
            // 
            // pictureEdit
            // 
            this.pictureEdit.Image = ((System.Drawing.Image)(resources.GetObject("pictureEdit.Image")));
            // 
            // ControlPresenterService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlPresenterService";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
