namespace BusinessObjects.Windows
{
    partial class FormDataBaseConnection
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
            this.controlSqlServerConnection = new BusinessObjects.Windows.Controls.ControlSqlServerConnection();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.Size = new System.Drawing.Size(422, 148);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // clientPanel
            // 
            this.clientPanel.Controls.Add(this.controlSqlServerConnection);
            this.clientPanel.Location = new System.Drawing.Point(0, 148);
            this.clientPanel.Size = new System.Drawing.Size(422, 228);
            // 
            // controlSqlServerConnection
            // 
            this.controlSqlServerConnection.ConnectionString = "Data Source=;Initial Catalog=;Integrated Security=False;User ID=;Password=;Applic" +
                "ation Name=\"Documents System\";Current Language=Russian";
            this.controlSqlServerConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlSqlServerConnection.Location = new System.Drawing.Point(0, 0);
            this.controlSqlServerConnection.MinimumSize = new System.Drawing.Size(350, 235);
            this.controlSqlServerConnection.Name = "controlSqlServerConnection";
            this.controlSqlServerConnection.Size = new System.Drawing.Size(422, 235);
            this.controlSqlServerConnection.TabIndex = 0;
            // 
            // FormDataBaseConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(422, 399);
            this.MinimumSize = new System.Drawing.Size(430, 400);
            this.Name = "FormDataBaseConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal BusinessObjects.Windows.Controls.ControlSqlServerConnection controlSqlServerConnection;

    }
}
