namespace BusinessObjects.UpdateServices
{
    partial class ControlSystemUpdate
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
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridNews = new DevExpress.XtraGrid.GridControl();
            this.ViewLogs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnMemo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LbInfo = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbServiceList = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.ViewService = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridNews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServiceList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbServiceList);
            this.LayoutControl.Controls.Add(this.GridNews);
            this.LayoutControl.Controls.Add(this.LbInfo);
            this.LayoutControl.Controls.Add(this.textEdit1);
            this.LayoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(545, 265, 250, 350);
            this.LayoutControl.Size = new System.Drawing.Size(753, 476);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlGroup1,
            this.layoutControlItem2});
            this.layoutControlGroup.Size = new System.Drawing.Size(753, 476);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(122, 36);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(619, 20);
            this.textEdit1.StyleController = this.LayoutControl;
            this.textEdit1.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit1;
            this.layoutControlItem1.CustomizationFormText = "Текущая версия:";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(733, 24);
            this.layoutControlItem1.Text = "Текущая версия:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(106, 13);
            // 
            // GridNews
            // 
            this.GridNews.Location = new System.Drawing.Point(15, 100);
            this.GridNews.MainView = this.ViewLogs;
            this.GridNews.Name = "GridNews";
            this.GridNews.Size = new System.Drawing.Size(723, 361);
            this.GridNews.TabIndex = 7;
            this.GridNews.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewLogs});
            // 
            // ViewLogs
            // 
            this.ViewLogs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnAction,
            this.gridColumnDate,
            this.gridColumnMemo,
            this.gridColumnStatus});
            this.ViewLogs.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewLogs.GridControl = this.GridNews;
            this.ViewLogs.GroupCount = 1;
            this.ViewLogs.Name = "ViewLogs";
            this.ViewLogs.OptionsBehavior.Editable = false;
            this.ViewLogs.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewLogs.OptionsView.ShowGroupPanel = false;
            this.ViewLogs.OptionsView.ShowIndicator = false;
            this.ViewLogs.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnAction, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnAction
            // 
            this.gridColumnAction.Caption = "Раздел";
            this.gridColumnAction.FieldName = "Action";
            this.gridColumnAction.Name = "gridColumnAction";
            this.gridColumnAction.Visible = true;
            this.gridColumnAction.VisibleIndex = 0;
            this.gridColumnAction.Width = 130;
            // 
            // gridColumnDate
            // 
            this.gridColumnDate.Caption = "Дата";
            this.gridColumnDate.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            this.gridColumnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumnDate.FieldName = "Date";
            this.gridColumnDate.Name = "gridColumnDate";
            this.gridColumnDate.Visible = true;
            this.gridColumnDate.VisibleIndex = 0;
            // 
            // gridColumnMemo
            // 
            this.gridColumnMemo.Caption = "Описание";
            this.gridColumnMemo.FieldName = "Memo";
            this.gridColumnMemo.Name = "gridColumnMemo";
            this.gridColumnMemo.Visible = true;
            this.gridColumnMemo.VisibleIndex = 1;
            this.gridColumnMemo.Width = 554;
            // 
            // gridColumnStatus
            // 
            this.gridColumnStatus.Caption = "Статус";
            this.gridColumnStatus.FieldName = "Status";
            this.gridColumnStatus.Name = "gridColumnStatus";
            this.gridColumnStatus.Visible = true;
            this.gridColumnStatus.VisibleIndex = 2;
            // 
            // LbInfo
            // 
            this.LbInfo.Location = new System.Drawing.Point(12, 60);
            this.LbInfo.Name = "LbInfo";
            this.LbInfo.Size = new System.Drawing.Size(144, 13);
            this.LbInfo.StyleController = this.LayoutControl;
            this.LbInfo.TabIndex = 6;
            this.LbInfo.Text = "Обновления отсутствуют...";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.LbInfo;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(733, 17);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Протокол, сообщения";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 65);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(733, 391);
            this.layoutControlGroup1.Text = "Протокол, сообщения";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.GridNews;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(727, 365);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // cmbServiceList
            // 
            this.cmbServiceList.Location = new System.Drawing.Point(122, 12);
            this.cmbServiceList.Name = "cmbServiceList";
            this.cmbServiceList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbServiceList.Properties.View = this.ViewService;
            this.cmbServiceList.Size = new System.Drawing.Size(619, 20);
            this.cmbServiceList.StyleController = this.LayoutControl;
            this.cmbServiceList.TabIndex = 8;
            // 
            // ViewService
            // 
            this.ViewService.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewService.Name = "ViewService";
            this.ViewService.OptionsBehavior.Editable = false;
            this.ViewService.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewService.OptionsView.ShowGroupPanel = false;
            this.ViewService.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbServiceList;
            this.layoutControlItem2.CustomizationFormText = "Служба обновления";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(733, 24);
            this.layoutControlItem2.Text = "Служба обновления:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(106, 13);
            // 
            // ControlSystemUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlSystemUpdate";
            this.Size = new System.Drawing.Size(753, 476);
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridNews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbServiceList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAction;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnMemo;
        public DevExpress.XtraEditors.LabelControl LbInfo;
        public DevExpress.XtraGrid.GridControl GridNews;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public DevExpress.XtraEditors.SearchLookUpEdit cmbServiceList;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewService;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDate;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewLogs;
    }
}
