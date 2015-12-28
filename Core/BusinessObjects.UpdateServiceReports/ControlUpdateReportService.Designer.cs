namespace BusinessObjects.UpdateServiceReports
{
    partial class ControlUpdateReportService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlUpdateReportService));
            this.cmbAdress = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtUserId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtPsw = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbServer = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewReports = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.treeListNew = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnDescription = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnTypeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnPath = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumnId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.imageCollection = new DevExpress.Utils.ImageCollection();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAdress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPsw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.dateEdit1);
            this.LayoutControl.Controls.Add(this.txtLog);
            this.LayoutControl.Controls.Add(this.cmbServer);
            this.LayoutControl.Controls.Add(this.txtPsw);
            this.LayoutControl.Controls.Add(this.txtUserId);
            this.LayoutControl.Controls.Add(this.cmbAdress);
            this.LayoutControl.Controls.Add(this.treeListNew);
            this.LayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(620, 382, 250, 350);
            this.LayoutControl.Size = new System.Drawing.Size(588, 445);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlItem6,
            this.layoutControlItem5});
            this.layoutControlGroup.Name = "Root";
            this.layoutControlGroup.Size = new System.Drawing.Size(588, 445);
            // 
            // cmbAdress
            // 
            this.cmbAdress.Location = new System.Drawing.Point(213, 45);
            this.cmbAdress.Name = "cmbAdress";
            this.cmbAdress.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAdress.Size = new System.Drawing.Size(351, 20);
            this.cmbAdress.StyleController = this.LayoutControl;
            this.cmbAdress.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbAdress;
            this.layoutControlItem1.CustomizationFormText = "Адрес службы обновления отчетов";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(544, 24);
            this.layoutControlItem1.Text = "Адрес службы обновления отчетов:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(185, 13);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Настройки";
            this.layoutControlGroup1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroup1.ExpandButtonVisible = true;
            this.layoutControlGroup1.ExpandOnDoubleClick = true;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(568, 165);
            this.layoutControlGroup1.Text = "Настройки";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtUserId;
            this.layoutControlItem2.CustomizationFormText = "Пользователь";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(544, 24);
            this.layoutControlItem2.Text = "Пользователь:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(185, 13);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(213, 69);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(351, 20);
            this.txtUserId.StyleController = this.LayoutControl;
            this.txtUserId.TabIndex = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtPsw;
            this.layoutControlItem3.CustomizationFormText = "Пароль";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(544, 24);
            this.layoutControlItem3.Text = "Пароль:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(185, 13);
            // 
            // txtPsw
            // 
            this.txtPsw.Location = new System.Drawing.Point(213, 93);
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.Properties.UseSystemPasswordChar = true;
            this.txtPsw.Size = new System.Drawing.Size(351, 20);
            this.txtPsw.StyleController = this.LayoutControl;
            this.txtPsw.TabIndex = 6;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbServer;
            this.layoutControlItem4.CustomizationFormText = "Мой сервер отчетов";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(544, 24);
            this.layoutControlItem4.Text = "Мой сервер отчетов:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(185, 13);
            // 
            // cmbServer
            // 
            this.cmbServer.Location = new System.Drawing.Point(213, 117);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbServer.Properties.View = this.ViewReports;
            this.cmbServer.Size = new System.Drawing.Size(351, 20);
            this.cmbServer.StyleController = this.LayoutControl;
            this.cmbServer.TabIndex = 7;
            // 
            // ViewReports
            // 
            this.ViewReports.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewReports.Name = "ViewReports";
            this.ViewReports.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewReports.OptionsView.ShowGroupPanel = false;
            this.ViewReports.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.dateEdit1;
            this.layoutControlItem7.CustomizationFormText = "Дата поиска новых отчетов:";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(329, 24);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(329, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(329, 24);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "Дата поиска новых отчетов:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(185, 13);
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(213, 141);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.Size = new System.Drawing.Size(136, 20);
            this.dateEdit1.StyleController = this.LayoutControl;
            this.dateEdit1.TabIndex = 10;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(329, 96);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(215, 24);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // treeListNew
            // 
            this.treeListNew.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName,
            this.treeListColumnDescription,
            this.treeListColumnDate,
            this.treeListColumnTypeName,
            this.treeListColumnPath,
            this.treeListColumnId});
            this.treeListNew.Location = new System.Drawing.Point(12, 193);
            this.treeListNew.Name = "treeListNew";
            this.treeListNew.OptionsBehavior.Editable = false;
            this.treeListNew.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeListNew.OptionsSelection.UseIndicatorForSelection = true;
            this.treeListNew.OptionsView.AutoCalcPreviewLineCount = true;
            this.treeListNew.OptionsView.ShowCheckBoxes = true;
            this.treeListNew.OptionsView.ShowIndicator = false;
            this.treeListNew.ParentFieldName = "Parent";
            this.treeListNew.PreviewFieldName = "Description";
            this.treeListNew.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.treeListNew.SelectImageList = this.imageCollection;
            this.treeListNew.Size = new System.Drawing.Size(564, 142);
            this.treeListNew.TabIndex = 8;
            // 
            // treeListColumnName
            // 
            this.treeListColumnName.Caption = "Наименование";
            this.treeListColumnName.FieldName = "Name";
            this.treeListColumnName.MinWidth = 48;
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.OptionsColumn.ReadOnly = true;
            this.treeListColumnName.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.treeListColumnName.Visible = true;
            this.treeListColumnName.VisibleIndex = 0;
            this.treeListColumnName.Width = 187;
            // 
            // treeListColumnDescription
            // 
            this.treeListColumnDescription.Caption = "Описание";
            this.treeListColumnDescription.FieldName = "Description";
            this.treeListColumnDescription.Name = "treeListColumnDescription";
            this.treeListColumnDescription.OptionsColumn.ReadOnly = true;
            this.treeListColumnDescription.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.treeListColumnDescription.Visible = true;
            this.treeListColumnDescription.VisibleIndex = 1;
            this.treeListColumnDescription.Width = 273;
            // 
            // treeListColumnDate
            // 
            this.treeListColumnDate.Caption = "Дата изменения";
            this.treeListColumnDate.FieldName = "Дата изменения";
            this.treeListColumnDate.Name = "treeListColumnDate";
            this.treeListColumnDate.OptionsColumn.FixedWidth = true;
            this.treeListColumnDate.OptionsColumn.ReadOnly = true;
            this.treeListColumnDate.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.DateTime;
            this.treeListColumnDate.Visible = true;
            this.treeListColumnDate.VisibleIndex = 2;
            this.treeListColumnDate.Width = 100;
            // 
            // treeListColumnTypeName
            // 
            this.treeListColumnTypeName.Caption = "Тип";
            this.treeListColumnTypeName.FieldName = "TypeName";
            this.treeListColumnTypeName.Name = "treeListColumnTypeName";
            this.treeListColumnTypeName.OptionsColumn.FixedWidth = true;
            this.treeListColumnTypeName.OptionsColumn.ReadOnly = true;
            this.treeListColumnTypeName.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            this.treeListColumnTypeName.Width = 70;
            // 
            // treeListColumnPath
            // 
            this.treeListColumnPath.Caption = "Путь";
            this.treeListColumnPath.FieldName = "Path";
            this.treeListColumnPath.Name = "treeListColumnPath";
            this.treeListColumnPath.OptionsColumn.ReadOnly = true;
            this.treeListColumnPath.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            // 
            // treeListColumnId
            // 
            this.treeListColumnId.Caption = "Ид";
            this.treeListColumnId.FieldName = "Id";
            this.treeListColumnId.Name = "treeListColumnId";
            this.treeListColumnId.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // imageCollection
            // 
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 355);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(564, 78);
            this.txtLog.StyleController = this.LayoutControl;
            this.txtLog.TabIndex = 9;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtLog;
            this.layoutControlItem6.CustomizationFormText = "Протокол";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 327);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(568, 98);
            this.layoutControlItem6.Text = "Протокол";
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(185, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.treeListNew;
            this.layoutControlItem5.CustomizationFormText = "Отчеты";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 165);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(568, 162);
            this.layoutControlItem5.Text = "Отчеты:";
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(185, 13);
            // 
            // ControlUpdateReportService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlUpdateReportService";
            this.Size = new System.Drawing.Size(588, 445);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAdress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPsw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        public DevExpress.XtraEditors.ComboBoxEdit cmbAdress;
        public DevExpress.XtraEditors.TextEdit txtUserId;
        public DevExpress.XtraEditors.TextEdit txtPsw;
        public DevExpress.XtraEditors.GridLookUpEdit cmbServer;
        public DevExpress.XtraTreeList.TreeList treeListNew;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewReports;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnPath;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnTypeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnDescription;
        public DevExpress.Utils.ImageCollection imageCollection;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        public DevExpress.XtraEditors.MemoEdit txtLog;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnId;
    }
}
