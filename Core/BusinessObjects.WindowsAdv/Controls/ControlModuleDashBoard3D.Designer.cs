namespace BusinessObjects.Windows.Controls
{
    partial class ControlModuleDashBoard3D
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
            this.Grid = new DevExpress.XtraGrid.GridControl();
            this.layoutView1 = new DevExpress.XtraGrid.Views.Layout.LayoutView();
            this.colCaption = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colName = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.colPlotoutLine = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colPhoto = new DevExpress.XtraGrid.Columns.LayoutViewColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.layoutViewCard1 = new DevExpress.XtraGrid.Views.Layout.LayoutViewCard();
            this.layoutViewField_colPhoto = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField_colPlotoutLine = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.layoutViewField_colYear = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            this.item1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutViewField_colCaption = new DevExpress.XtraGrid.Views.Layout.LayoutViewField();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPlotoutLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colCaption)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.AllowCustomizationMenu = false;
            this.LayoutControl.Size = new System.Drawing.Size(832, 547);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Size = new System.Drawing.Size(832, 547);
            // 
            // Grid
            // 
            this.Grid.Cursor = System.Windows.Forms.Cursors.Default;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(0, 0);
            this.Grid.MainView = this.layoutView1;
            this.Grid.Name = "Grid";
            this.Grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemPictureEdit1,
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemMemoEdit2});
            this.Grid.Size = new System.Drawing.Size(832, 547);
            this.Grid.TabIndex = 1;
            this.Grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.layoutView1});
            // 
            // layoutView1
            // 
            this.layoutView1.Appearance.CardCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.layoutView1.Appearance.CardCaption.Options.UseFont = true;
            this.layoutView1.Appearance.CardCaption.Options.UseTextOptions = true;
            this.layoutView1.Appearance.CardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.CardCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.layoutView1.Appearance.FocusedCardCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.layoutView1.Appearance.FocusedCardCaption.Options.UseFont = true;
            this.layoutView1.Appearance.FocusedCardCaption.Options.UseTextOptions = true;
            this.layoutView1.Appearance.FocusedCardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.FocusedCardCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.layoutView1.Appearance.HideSelectionCardCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.layoutView1.Appearance.HideSelectionCardCaption.Options.UseFont = true;
            this.layoutView1.Appearance.HideSelectionCardCaption.Options.UseTextOptions = true;
            this.layoutView1.Appearance.HideSelectionCardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.HideSelectionCardCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.layoutView1.Appearance.SelectedCardCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.layoutView1.Appearance.SelectedCardCaption.Options.UseFont = true;
            this.layoutView1.Appearance.SelectedCardCaption.Options.UseTextOptions = true;
            this.layoutView1.Appearance.SelectedCardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutView1.Appearance.SelectedCardCaption.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.layoutView1.CardCaptionFormat = "{2}";
            this.layoutView1.CardMinSize = new System.Drawing.Size(390, 225);
            this.layoutView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.LayoutViewColumn[] {
            this.colCaption,
            this.colName,
            this.colPlotoutLine,
            this.colPhoto});
            this.layoutView1.GridControl = this.Grid;
            this.layoutView1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colCaption});
            this.layoutView1.Name = "layoutView1";
            this.layoutView1.OptionsBehavior.AllowRuntimeCustomization = false;
            this.layoutView1.OptionsBehavior.Editable = false;
            this.layoutView1.OptionsBehavior.ReadOnly = true;
            this.layoutView1.OptionsBehavior.ScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.layoutView1.OptionsBehavior.UseTabKey = false;
            this.layoutView1.OptionsCarouselMode.PitchAngle = 1.75F;
            this.layoutView1.OptionsItemText.AlignMode = DevExpress.XtraGrid.Views.Layout.FieldTextAlignMode.AutoSize;
            this.layoutView1.OptionsView.AllowHotTrackFields = false;
            this.layoutView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            this.layoutView1.OptionsView.ShowCardExpandButton = false;
            this.layoutView1.OptionsView.ShowCardLines = false;
            this.layoutView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.layoutView1.OptionsView.ShowHeaderPanel = false;
            this.layoutView1.OptionsView.ViewMode = DevExpress.XtraGrid.Views.Layout.LayoutViewMode.Carousel;
            this.layoutView1.TemplateCard = this.layoutViewCard1;
            // 
            // colCaption
            // 
            this.colCaption.Caption = "Caption";
            this.colCaption.ColumnEdit = this.repositoryItemTextEdit1;
            this.colCaption.CustomizationCaption = "Caption";
            this.colCaption.FieldName = "Caption";
            this.colCaption.LayoutViewField = this.layoutViewField_colCaption;
            this.colCaption.Name = "colCaption";
            this.colCaption.OptionsColumn.AllowEdit = false;
            this.colCaption.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCaption.OptionsColumn.ReadOnly = true;
            this.colCaption.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AllowFocused = false;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // colName
            // 
            this.colName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceCell.Options.UseFont = true;
            this.colName.Caption = "Наименование";
            this.colName.CustomizationCaption = "Наименование";
            this.colName.FieldName = "Name";
            this.colName.LayoutViewField = this.layoutViewField_colYear;
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colName.OptionsColumn.TabStop = false;
            this.colName.OptionsFilter.AllowFilter = false;
            // 
            // colPlotoutLine
            // 
            this.colPlotoutLine.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.colPlotoutLine.AppearanceCell.Options.UseFont = true;
            this.colPlotoutLine.AppearanceCell.Options.UseTextOptions = true;
            this.colPlotoutLine.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.colPlotoutLine.Caption = "Описание";
            this.colPlotoutLine.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colPlotoutLine.CustomizationCaption = "Описание";
            this.colPlotoutLine.FieldName = "Memo";
            this.colPlotoutLine.LayoutViewField = this.layoutViewField_colPlotoutLine;
            this.colPlotoutLine.Name = "colPlotoutLine";
            this.colPlotoutLine.OptionsColumn.AllowEdit = false;
            this.colPlotoutLine.OptionsColumn.AllowFocus = false;
            this.colPlotoutLine.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPlotoutLine.OptionsColumn.ReadOnly = true;
            this.colPlotoutLine.OptionsColumn.TabStop = false;
            this.colPlotoutLine.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.AllowFocused = false;
            this.repositoryItemMemoEdit2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // colPhoto
            // 
            this.colPhoto.Caption = "Изображение";
            this.colPhoto.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colPhoto.CustomizationCaption = "Изображение";
            this.colPhoto.FieldName = "Photo";
            this.colPhoto.LayoutViewField = this.layoutViewField_colPhoto;
            this.colPhoto.Name = "colPhoto";
            this.colPhoto.OptionsColumn.AllowFocus = false;
            this.colPhoto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPhoto.OptionsColumn.TabStop = false;
            this.colPhoto.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.AllowFocused = false;
            this.repositoryItemPictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.ReadOnly = true;
            this.repositoryItemPictureEdit1.ShowMenu = false;
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // layoutViewCard1
            // 
            this.layoutViewCard1.CustomizationFormText = "layoutViewTemplateCard";
            this.layoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutViewCard1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutViewField_colPhoto,
            this.layoutViewField_colPlotoutLine,
            this.layoutViewField_colYear,
            this.item1});
            this.layoutViewCard1.Name = "layoutViewCard1";
            this.layoutViewCard1.OptionsItemText.TextToControlDistance = 5;
            this.layoutViewCard1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewCard1.Text = "TemplateCard";
            // 
            // layoutViewField_colPhoto
            // 
            this.layoutViewField_colPhoto.EditorPreferredWidth = 133;
            this.layoutViewField_colPhoto.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colPhoto.MaxSize = new System.Drawing.Size(135, 192);
            this.layoutViewField_colPhoto.MinSize = new System.Drawing.Size(135, 192);
            this.layoutViewField_colPhoto.Name = "layoutViewField_colPhoto";
            this.layoutViewField_colPhoto.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField_colPhoto.Size = new System.Drawing.Size(135, 192);
            this.layoutViewField_colPhoto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutViewField_colPhoto.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colPhoto.TextToControlDistance = 0;
            this.layoutViewField_colPhoto.TextVisible = false;
            // 
            // layoutViewField_colPlotoutLine
            // 
            this.layoutViewField_colPlotoutLine.EditorPreferredWidth = 249;
            this.layoutViewField_colPlotoutLine.Location = new System.Drawing.Point(135, 18);
            this.layoutViewField_colPlotoutLine.Name = "layoutViewField_colPlotoutLine";
            this.layoutViewField_colPlotoutLine.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField_colPlotoutLine.Size = new System.Drawing.Size(251, 184);
            this.layoutViewField_colPlotoutLine.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colPlotoutLine.TextToControlDistance = 0;
            this.layoutViewField_colPlotoutLine.TextVisible = false;
            // 
            // layoutViewField_colYear
            // 
            this.layoutViewField_colYear.EditorPreferredWidth = 249;
            this.layoutViewField_colYear.Location = new System.Drawing.Point(135, 0);
            this.layoutViewField_colYear.Name = "layoutViewField_colYear";
            this.layoutViewField_colYear.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField_colYear.Size = new System.Drawing.Size(251, 18);
            this.layoutViewField_colYear.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colYear.TextToControlDistance = 0;
            this.layoutViewField_colYear.TextVisible = false;
            // 
            // item1
            // 
            this.item1.AllowHotTrack = false;
            this.item1.CustomizationFormText = "item1";
            this.item1.Location = new System.Drawing.Point(0, 192);
            this.item1.Name = "item1";
            this.item1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.item1.Size = new System.Drawing.Size(135, 10);
            this.item1.Text = "item1";
            this.item1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutViewField_colCaption
            // 
            this.layoutViewField_colCaption.EditorPreferredWidth = 25;
            this.layoutViewField_colCaption.Location = new System.Drawing.Point(0, 0);
            this.layoutViewField_colCaption.Name = "layoutViewField_colCaption";
            this.layoutViewField_colCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutViewField_colCaption.Size = new System.Drawing.Size(407, 199);
            this.layoutViewField_colCaption.TextSize = new System.Drawing.Size(0, 0);
            this.layoutViewField_colCaption.TextToControlDistance = 0;
            this.layoutViewField_colCaption.TextVisible = false;
            // 
            // ControlModuleDashBoard3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.Grid);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "ControlModuleDashBoard3D";
            this.Size = new System.Drawing.Size(832, 547);
            this.Controls.SetChildIndex(this.LayoutControl, 0);
            this.Controls.SetChildIndex(this.Grid, 0);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colPlotoutLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.item1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutViewField_colCaption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.LayoutViewColumn colCaption;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colName;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colPlotoutLine;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraGrid.Columns.LayoutViewColumn colPhoto;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        public DevExpress.XtraGrid.GridControl Grid;
        public DevExpress.XtraGrid.Views.Layout.LayoutView layoutView1;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colCaption;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colYear;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colPlotoutLine;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewField layoutViewField_colPhoto;
        private DevExpress.XtraGrid.Views.Layout.LayoutViewCard layoutViewCard1;
        private DevExpress.XtraLayout.EmptySpaceItem item1;
    }
}
