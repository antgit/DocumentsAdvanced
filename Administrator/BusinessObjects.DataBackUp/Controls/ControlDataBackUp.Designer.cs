namespace BusinessObjects.DataBackUp.Controls
{
    partial class ControlDataBackUp
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
            this.btnEditFolder = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlItemPath = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridData = new DevExpress.XtraGrid.GridControl();
            this.View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtFileName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.LayoutControl.AllowCustomizationMenu = false;
            this.LayoutControl.Controls.Add(this.txtFileName);
            this.LayoutControl.Controls.Add(this.GridData);
            this.LayoutControl.Controls.Add(this.btnEditFolder);
            this.LayoutControl.Size = new System.Drawing.Size(861, 600);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup1});
            this.layoutControlGroup.Size = new System.Drawing.Size(861, 600);
            // 
            // btnEditFolder
            // 
            this.btnEditFolder.Location = new System.Drawing.Point(198, 44);
            this.btnEditFolder.Name = "btnEditFolder";
            this.btnEditFolder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEditFolder.Size = new System.Drawing.Size(639, 20);
            this.btnEditFolder.StyleController = this.LayoutControl;
            this.btnEditFolder.TabIndex = 4;
            // 
            // layoutControlItemPath
            // 
            this.layoutControlItemPath.Control = this.btnEditFolder;
            this.layoutControlItemPath.CustomizationFormText = "Папка на сервере";
            this.layoutControlItemPath.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemPath.Name = "layoutControlItemPath";
            this.layoutControlItemPath.Size = new System.Drawing.Size(817, 24);
            this.layoutControlItemPath.Text = "Папка на сервере:";
            this.layoutControlItemPath.TextSize = new System.Drawing.Size(170, 13);
            // 
            // GridData
            // 
            this.GridData.Location = new System.Drawing.Point(12, 120);
            this.GridData.MainView = this.View;
            this.GridData.Name = "GridData";
            this.GridData.Size = new System.Drawing.Size(837, 468);
            this.GridData.TabIndex = 5;
            this.GridData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.View});
            // 
            // View
            // 
            this.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.View.GridControl = this.GridData;
            this.View.Name = "View";
            this.View.OptionsBehavior.Editable = false;
            this.View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.View.OptionsView.ShowGroupPanel = false;
            this.View.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GridData;
            this.layoutControlItem1.CustomizationFormText = "Существующие резерные копии";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 92);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(841, 488);
            this.layoutControlItem1.Text = "Существующие резерные копии:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(170, 13);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(198, 68);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(639, 20);
            this.txtFileName.StyleController = this.LayoutControl;
            this.txtFileName.TabIndex = 6;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtFileName;
            this.layoutControlItem2.CustomizationFormText = "Имя файла резервной копии";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(817, 24);
            this.layoutControlItem2.Text = "Имя файла резервной копии:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(170, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Создание быстрой резервной копии";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItemPath});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(841, 92);
            this.layoutControlGroup1.Text = "Создание быстрой резервной копии";
            // 
            // ControlDataBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlDataBackUp";
            this.Size = new System.Drawing.Size(861, 600);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPath;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraEditors.ButtonEdit btnEditFolder;
        public DevExpress.XtraGrid.GridControl GridData;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraGrid.Views.Grid.GridView View;
        public DevExpress.XtraEditors.TextEdit txtFileName;
    }
}
