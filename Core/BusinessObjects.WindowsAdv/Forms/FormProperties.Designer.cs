namespace BusinessObjects.Windows
{
    partial class FormProperties
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
            this.components = new System.ComponentModel.Container();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelect = new DevExpress.XtraBars.BarButtonItem();
            this.btnProp = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnCreate = new DevExpress.XtraBars.BarButtonItem();
            this.CreateMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnSaveClose = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItemProgress = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.btnRun = new DevExpress.XtraBars.BarButtonItem();
            this.btnActions = new DevExpress.XtraBars.BarButtonItem();
            this.ActionMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.PAGE_PAGECOMMON = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.AllowMinimizeRibbon = false;
            this.ribbon.ApplicationButtonText = null;
            // 
            // 
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnClose,
            this.btnSave,
            this.btnSelect,
            this.btnProp,
            this.btnDelete,
            this.btnCreate,
            this.btnSaveClose,
            this.btnRefresh,
            this.barEditItemProgress,
            this.btnPrint,
            this.btnRun,
            this.btnActions});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 19;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.PAGE_PAGECOMMON});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMarqueeProgressBar});
            this.ribbon.SelectedPage = this.PAGE_PAGECOMMON;
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(596, 149);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            this.ribbon.ApplicationButtonClick += new System.EventHandler(this.ribbon_ApplicationButtonClick);
            // 
            // btnClose
            // 
            this.btnClose.Caption = "Закрыть";
            this.btnClose.Id = 4;
            this.btnClose.Name = "btnClose";
            this.btnClose.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Сохранить";
            this.btnSave.Id = 5;
            this.btnSave.Name = "btnSave";
            this.btnSave.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Caption = "Выбрать";
            this.btnSelect.Id = 6;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelect_ItemClick);
            // 
            // btnProp
            // 
            this.btnProp.Caption = "Свойства";
            this.btnProp.Id = 7;
            this.btnProp.Name = "btnProp";
            this.btnProp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnProp.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "Удалить";
            this.btnDelete.Id = 8;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnCreate
            // 
            this.btnCreate.ActAsDropDown = true;
            this.btnCreate.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.btnCreate.Caption = "Создать...";
            this.btnCreate.DropDownControl = this.CreateMenu;
            this.btnCreate.Id = 9;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnCreate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // CreateMenu
            // 
            this.CreateMenu.Name = "CreateMenu";
            this.CreateMenu.Ribbon = this.ribbon;
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Caption = "Сохранить и закрыть";
            this.btnSaveClose.Id = 10;
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSaveClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnSaveClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveClose_ItemClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Обновить";
            this.btnRefresh.Id = 11;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barEditItemProgress
            // 
            this.barEditItemProgress.Caption = "Прогресс:";
            this.barEditItemProgress.Edit = this.repositoryItemMarqueeProgressBar;
            this.barEditItemProgress.Id = 13;
            this.barEditItemProgress.Name = "barEditItemProgress";
            this.barEditItemProgress.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barEditItemProgress.Width = 150;
            // 
            // repositoryItemMarqueeProgressBar
            // 
            this.repositoryItemMarqueeProgressBar.Name = "repositoryItemMarqueeProgressBar";
            // 
            // btnPrint
            // 
            this.btnPrint.Caption = "Печать";
            this.btnPrint.Id = 14;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnRun
            // 
            this.btnRun.Caption = "Выполнить";
            this.btnRun.Id = 15;
            this.btnRun.Name = "btnRun";
            this.btnRun.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnRun.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnActions
            // 
            this.btnActions.ActAsDropDown = true;
            this.btnActions.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.btnActions.Caption = "Действия";
            this.btnActions.DropDownControl = this.ActionMenu;
            this.btnActions.Id = 16;
            this.btnActions.Name = "btnActions";
            this.btnActions.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnActions.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // ActionMenu
            // 
            this.ActionMenu.Name = "ActionMenu";
            this.ActionMenu.Ribbon = this.ribbon;
            // 
            // PAGE_PAGECOMMON
            // 
            this.PAGE_PAGECOMMON.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION});
            this.PAGE_PAGECOMMON.Name = "PAGE_PAGECOMMON";
            this.PAGE_PAGECOMMON.Tag = "PAGECOMMON";
            this.PAGE_PAGECOMMON.Text = "Главная";
            // 
            // PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION
            // 
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnClose);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnSelect);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnRun);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnSaveClose);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnSave);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnPrint);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnCreate);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnProp);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnRefresh);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnDelete);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(this.btnActions);
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.Name = "PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION";
            this.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.Text = "Действия";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barEditItemProgress);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 260);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(596, 23);
            // 
            // clientPanel
            // 
            this.clientPanel.Appearance.Options.UseBackColor = true;
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 149);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(596, 111);
            this.clientPanel.TabIndex = 2;
            // 
            // FormProperties
            // 
            this.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoHideRibbon = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 283);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "FormProperties";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        public DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        public DevExpress.XtraEditors.PanelControl clientPanel;
        public DevExpress.XtraBars.BarButtonItem btnClose;
        public DevExpress.XtraBars.BarButtonItem btnSave;
        public DevExpress.XtraBars.BarButtonItem btnSelect;
        public DevExpress.XtraBars.BarButtonItem btnProp;
        public DevExpress.XtraBars.BarButtonItem btnDelete;
        public DevExpress.XtraBars.BarButtonItem btnCreate;
        public DevExpress.XtraBars.BarButtonItem btnSaveClose;
        private DevExpress.XtraBars.Ribbon.RibbonPage PAGE_PAGECOMMON;
        public DevExpress.XtraBars.Ribbon.RibbonPageGroup PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION;
        public DevExpress.XtraBars.BarButtonItem btnRefresh;
        public DevExpress.XtraBars.BarEditItem barEditItemProgress;
        public DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar;
        public DevExpress.XtraBars.BarButtonItem btnPrint;
        public DevExpress.XtraBars.BarButtonItem btnRun;
        public DevExpress.XtraBars.PopupMenu CreateMenu;
        public DevExpress.XtraBars.PopupMenu ActionMenu;
        public DevExpress.XtraBars.BarButtonItem btnActions;
    }
}