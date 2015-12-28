namespace BusinessObjects.DocumentLibrary.Controls
{
    partial class ControlStoresLeaves
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
            this.GridStoresLeaves = new DevExpress.XtraGrid.GridControl();
            this.ViewStoresLeaves = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.GridStoresLeaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewStoresLeaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // GridStoresLeaves
            // 
            this.GridStoresLeaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridStoresLeaves.Location = new System.Drawing.Point(3, 32);
            this.GridStoresLeaves.MainView = this.ViewStoresLeaves;
            this.GridStoresLeaves.Name = "GridStoresLeaves";
            this.GridStoresLeaves.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.GridStoresLeaves.Size = new System.Drawing.Size(358, 200);
            this.GridStoresLeaves.TabIndex = 1;
            this.GridStoresLeaves.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewStoresLeaves});
            // 
            // ViewStoresLeaves
            // 
            this.ViewStoresLeaves.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colImage,
            this.colName,
            this.colCount});
            this.ViewStoresLeaves.GridControl = this.GridStoresLeaves;
            this.ViewStoresLeaves.Name = "ViewStoresLeaves";
            this.ViewStoresLeaves.OptionsBehavior.Editable = false;
            this.ViewStoresLeaves.OptionsDetail.EnableMasterViewMode = false;
            this.ViewStoresLeaves.OptionsDetail.ShowDetailTabs = false;
            this.ViewStoresLeaves.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewStoresLeaves.OptionsSelection.MultiSelect = true;
            this.ViewStoresLeaves.OptionsSelection.UseIndicatorForSelection = false;
            this.ViewStoresLeaves.OptionsView.ShowGroupPanel = false;
            this.ViewStoresLeaves.OptionsView.ShowIndicator = false;
            // 
            // colImage
            // 
            this.colImage.Caption = "Image";
            this.colImage.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colImage.FieldName = "Image";
            this.colImage.Name = "colImage";
            this.colImage.OptionsColumn.AllowEdit = false;
            this.colImage.OptionsColumn.AllowFocus = false;
            this.colImage.OptionsColumn.AllowSize = false;
            this.colImage.OptionsColumn.FixedWidth = true;
            this.colImage.OptionsColumn.ReadOnly = true;
            this.colImage.OptionsColumn.ShowCaption = false;
            this.colImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colImage.Visible = true;
            this.colImage.VisibleIndex = 0;
            this.colImage.Width = 20;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // colName
            // 
            this.colName.Caption = "Наименование";
            this.colName.FieldName = "Store.Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 259;
            // 
            // colCount
            // 
            this.colCount.Caption = "Остаток";
            this.colCount.DisplayFormat.FormatString = "n2";
            this.colCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCount.FieldName = "Leave";
            this.colCount.Name = "colCount";
            this.colCount.OptionsColumn.AllowEdit = false;
            this.colCount.OptionsColumn.AllowFocus = false;
            this.colCount.OptionsColumn.FixedWidth = true;
            this.colCount.OptionsColumn.ReadOnly = true;
            this.colCount.Visible = true;
            this.colCount.VisibleIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 23);
            this.btnAdd.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(89, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(281, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 23);
            this.btnRefresh.TabIndex = 4;
            // 
            // ControlStoresLeaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.GridStoresLeaves);
            this.MinimumSize = new System.Drawing.Size(364, 235);
            this.Name = "ControlStoresLeaves";
            this.Size = new System.Drawing.Size(364, 235);
            ((System.ComponentModel.ISupportInitialize)(this.GridStoresLeaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewStoresLeaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl GridStoresLeaves;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewStoresLeaves;
        public DevExpress.XtraEditors.SimpleButton btnAdd;
        public DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        public DevExpress.XtraEditors.SimpleButton btnRefresh;

    }
}
