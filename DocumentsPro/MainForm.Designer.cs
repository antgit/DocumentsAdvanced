namespace Documents2012
{
    partial class MainForm
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
            this.applicationMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.btnOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnHelp = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.iPaintStyle = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItemPeriod = new DevExpress.XtraBars.BarButtonItem();
            this.btnPeriodQuick = new DevExpress.XtraBars.BarButtonItem();
            this.btnWindowsList = new DevExpress.XtraBars.BarButtonItem();
            this.btnNavigator = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.btnViewDocuments = new DevExpress.XtraBars.BarButtonItem();
            this.defaultToolTipController1 = new DevExpress.Utils.DefaultToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonDropDownControl = this.applicationMenu;
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("Стандартные действия", new System.Guid("4478ad7d-8b98-4127-aebf-55f2bca83264")),
            new DevExpress.XtraBars.BarManagerCategory("Главное меню", new System.Guid("21d1ae0a-f617-4e28-8d9c-6da9ffe4ec57")),
            new DevExpress.XtraBars.BarManagerCategory("Вид", new System.Guid("cc1be85e-1cbe-4de1-8866-30c91c24459f"))});
            // 
            // 
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnExit,
            this.iPaintStyle,
            this.barButtonItemPeriod,
            this.btnOpen,
            this.btnPeriodQuick,
            this.btnWindowsList,
            this.btnNavigator,
            this.btnHelp});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 25;
            this.ribbon.Name = "ribbon";
            this.ribbon.PageHeaderItemLinks.Add(this.btnNavigator);
            this.ribbon.PageHeaderItemLinks.Add(this.btnWindowsList);
            this.ribbon.PageHeaderItemLinks.Add(this.btnPeriodQuick);
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageMain});
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.Size = new System.Drawing.Size(916, 147);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Toolbar.ItemLinks.Add(this.iPaintStyle);
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            this.ribbon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RibbonMouseClick);
            // 
            // applicationMenu
            // 
            this.applicationMenu.ItemLinks.Add(this.btnOpen);
            this.applicationMenu.ItemLinks.Add(this.btnHelp);
            this.applicationMenu.ItemLinks.Add(this.btnExit);
            this.applicationMenu.Name = "applicationMenu";
            this.applicationMenu.Ribbon = this.ribbon;
            // 
            // btnOpen
            // 
            this.btnOpen.Caption = "Открыть...";
            this.btnOpen.Description = "Запустить новое приложение не закрывая текущее";
            this.btnOpen.Id = 20;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOpen_ItemClick);
            // 
            // btnHelp
            // 
            this.btnHelp.Caption = "О программе";
            this.btnHelp.Id = 24;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnExit
            // 
            this.btnExit.Caption = "Выход";
            this.btnExit.CategoryGuid = new System.Guid("21d1ae0a-f617-4e28-8d9c-6da9ffe4ec57");
            this.btnExit.Description = "Закрыть все открытые окна и выйти из программы";
            this.btnExit.Id = 0;
            this.btnExit.Name = "btnExit";
            this.btnExit.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnExitItemClick);
            // 
            // iPaintStyle
            // 
            this.iPaintStyle.Caption = "Стиль";
            this.iPaintStyle.Glyph = global::Documents2012.Properties.Resources.StyleChoice;
            this.iPaintStyle.Hint = "Выбор темы отображения";
            this.iPaintStyle.Id = 14;
            this.iPaintStyle.Name = "iPaintStyle";
            this.iPaintStyle.Popup += new System.EventHandler(this.PaintStylePopup);
            // 
            // barButtonItemPeriod
            // 
            this.barButtonItemPeriod.ActAsDropDown = true;
            this.barButtonItemPeriod.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItemPeriod.Caption = "Рабочий период";
            this.barButtonItemPeriod.Id = 18;
            this.barButtonItemPeriod.Name = "barButtonItemPeriod";
            this.barButtonItemPeriod.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnPeriodQuick
            // 
            this.btnPeriodQuick.ActAsDropDown = true;
            this.btnPeriodQuick.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.btnPeriodQuick.Caption = "Рабочий период";
            this.btnPeriodQuick.Id = 21;
            this.btnPeriodQuick.Name = "btnPeriodQuick";
            this.btnPeriodQuick.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnPeriodQuick.SmallWithTextWidth = 100;
            // 
            // btnWindowsList
            // 
            this.btnWindowsList.ActAsDropDown = true;
            this.btnWindowsList.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.btnWindowsList.Caption = "Список окон";
            this.btnWindowsList.Id = 22;
            this.btnWindowsList.Name = "btnWindowsList";
            this.btnWindowsList.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnWindowsList.SmallWithTextWidth = 150;
            // 
            // btnNavigator
            // 
            this.btnNavigator.Caption = "Навигатор";
            this.btnNavigator.Id = 23;
            this.btnNavigator.Name = "btnNavigator";
            // 
            // ribbonPageMain
            // 
            this.ribbonPageMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.ribbonPageMain.Name = "ribbonPageMain";
            this.ribbonPageMain.Tag = "MAIN_MODULES";
            this.ribbonPageMain.Text = "Главная";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemPeriod);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Выбор...";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 584);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(916, 23);
            // 
            // clientPanel
            // 
            this.defaultToolTipController1.SetAllowHtmlText(this.clientPanel, DevExpress.Utils.DefaultBoolean.Default);
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 147);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(916, 437);
            this.clientPanel.TabIndex = 2;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Blue";
            // 
            // btnViewDocuments
            // 
            this.btnViewDocuments.Caption = "Список документов";
            this.btnViewDocuments.CategoryGuid = new System.Guid("cc1be85e-1cbe-4de1-8866-30c91c24459f");
            this.btnViewDocuments.Id = 9;
            this.btnViewDocuments.Name = "btnViewDocuments";
            // 
            // defaultToolTipController1
            // 
            // 
            // 
            // 
            this.defaultToolTipController1.DefaultController.AutoPopDelay = 15000;
            // 
            // MainForm
            // 
            this.defaultToolTipController1.SetAllowHtmlText(this, DevExpress.Utils.DefaultBoolean.Default);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 607);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMain;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        public DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraBars.BarButtonItem btnViewDocuments;
        private DevExpress.XtraBars.BarSubItem iPaintStyle;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemPeriod;
        //private DevExpress.XtraBars.PopupMenu popupMenuPeriod;
        private DevExpress.XtraBars.BarButtonItem btnOpen;
        private DevExpress.XtraBars.BarButtonItem btnPeriodQuick;
        private DevExpress.XtraBars.BarButtonItem btnWindowsList;
        private DevExpress.Utils.DefaultToolTipController defaultToolTipController1;
        private DevExpress.XtraBars.BarButtonItem btnNavigator;
        public DevExpress.XtraBars.BarButtonItem btnHelp;
    }
}