namespace BusinessObjects.DocumentLibrary.Controls
{
    partial class ControlCalcPrices
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridContent = new DevExpress.XtraGrid.GridControl();
            this.ViewContent = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNomenclature = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOldPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.seFactor = new DevExpress.XtraEditors.SpinEdit();
            this.cbFormula = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbCPDest = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewCPDest = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cbCPSource = new DevExpress.XtraEditors.GridLookUpEdit();
            this.ViewCPSource = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCalc = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seFactor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFormula.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCPDest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCPDest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCPSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCPSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridContent);
            this.layoutControl1.Controls.Add(this.seFactor);
            this.layoutControl1.Controls.Add(this.cbFormula);
            this.layoutControl1.Controls.Add(this.cbCPDest);
            this.layoutControl1.Controls.Add(this.cbCPSource);
            this.layoutControl1.Controls.Add(this.btnCalc);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(502, 384);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridContent
            // 
            this.gridContent.Location = new System.Drawing.Point(12, 84);
            this.gridContent.MainView = this.ViewContent;
            this.gridContent.Name = "gridContent";
            this.gridContent.Size = new System.Drawing.Size(478, 262);
            this.gridContent.TabIndex = 1;
            this.gridContent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewContent,
            this.gridView1});
            // 
            // ViewContent
            // 
            this.ViewContent.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNomenclature,
            this.colName,
            this.colUnit,
            this.colOldPrice,
            this.colPrice,
            this.colDiscount});
            this.ViewContent.GridControl = this.gridContent;
            this.ViewContent.Name = "ViewContent";
            this.ViewContent.OptionsCustomization.AllowGroup = false;
            this.ViewContent.OptionsDetail.EnableMasterViewMode = false;
            this.ViewContent.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewContent.OptionsView.ShowGroupPanel = false;
            this.ViewContent.OptionsView.ShowIndicator = false;
            // 
            // colNomenclature
            // 
            this.colNomenclature.Caption = "Номенклатура";
            this.colNomenclature.FieldName = "Product.Nomenclature";
            this.colNomenclature.Name = "colNomenclature";
            this.colNomenclature.OptionsColumn.AllowEdit = false;
            this.colNomenclature.Visible = true;
            this.colNomenclature.VisibleIndex = 0;
            // 
            // colName
            // 
            this.colName.Caption = "Наименование";
            this.colName.FieldName = "Product.Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            // 
            // colUnit
            // 
            this.colUnit.Caption = "Ед. изм.";
            this.colUnit.FieldName = "Product.Unit.Code";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 2;
            // 
            // colOldPrice
            // 
            this.colOldPrice.Caption = "Старая цена";
            this.colOldPrice.DisplayFormat.FormatString = "N2";
            this.colOldPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOldPrice.FieldName = "ValueOld";
            this.colOldPrice.Name = "colOldPrice";
            this.colOldPrice.OptionsColumn.AllowEdit = false;
            this.colOldPrice.Visible = true;
            this.colOldPrice.VisibleIndex = 3;
            // 
            // colPrice
            // 
            this.colPrice.Caption = "Новая цена";
            this.colPrice.DisplayFormat.FormatString = "N2";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Value";
            this.colPrice.Name = "colPrice";
            this.colPrice.OptionsColumn.AllowEdit = false;
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 4;
            // 
            // colDiscount
            // 
            this.colDiscount.Caption = "Дисконт";
            this.colDiscount.FieldName = "Discount";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.OptionsColumn.AllowEdit = false;
            this.colDiscount.Visible = true;
            this.colDiscount.VisibleIndex = 5;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridContent;
            this.gridView1.Name = "gridView1";
            // 
            // seFactor
            // 
            this.seFactor.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seFactor.Location = new System.Drawing.Point(440, 60);
            this.seFactor.Name = "seFactor";
            this.seFactor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seFactor.Size = new System.Drawing.Size(50, 20);
            this.seFactor.StyleController = this.layoutControl1;
            this.seFactor.TabIndex = 7;
            // 
            // cbFormula
            // 
            this.cbFormula.EditValue = "Умножить";
            this.cbFormula.Location = new System.Drawing.Point(185, 60);
            this.cbFormula.Name = "cbFormula";
            this.cbFormula.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbFormula.Properties.Items.AddRange(new object[] {
            "Умножить",
            "Разделить",
            "Прибавить",
            "Отнять"});
            this.cbFormula.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbFormula.Size = new System.Drawing.Size(78, 20);
            this.cbFormula.StyleController = this.layoutControl1;
            this.cbFormula.TabIndex = 6;
            // 
            // cbCPDest
            // 
            this.cbCPDest.Location = new System.Drawing.Point(185, 36);
            this.cbCPDest.Name = "cbCPDest";
            this.cbCPDest.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCPDest.Properties.View = this.ViewCPDest;
            this.cbCPDest.Size = new System.Drawing.Size(305, 20);
            this.cbCPDest.StyleController = this.layoutControl1;
            this.cbCPDest.TabIndex = 5;
            // 
            // ViewCPDest
            // 
            this.ViewCPDest.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewCPDest.Name = "ViewCPDest";
            this.ViewCPDest.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewCPDest.OptionsView.ShowGroupPanel = false;
            // 
            // cbCPSource
            // 
            this.cbCPSource.Location = new System.Drawing.Point(185, 12);
            this.cbCPSource.Name = "cbCPSource";
            this.cbCPSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCPSource.Properties.View = this.ViewCPSource;
            this.cbCPSource.Size = new System.Drawing.Size(305, 20);
            this.cbCPSource.StyleController = this.layoutControl1;
            this.cbCPSource.TabIndex = 4;
            // 
            // ViewCPSource
            // 
            this.ViewCPSource.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ViewCPSource.Name = "ViewCPSource";
            this.ViewCPSource.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ViewCPSource.OptionsView.ShowGroupPanel = false;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(358, 350);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(132, 22);
            this.btnCalc.StyleController = this.layoutControl1;
            this.btnCalc.TabIndex = 8;
            this.btnCalc.Text = "Расчитать";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(502, 384);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbCPSource;
            this.layoutControlItem1.CustomizationFormText = "Ценовая политика (источник):";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(482, 24);
            this.layoutControlItem1.Text = "Ценовая политика (назначение):";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(169, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cbCPDest;
            this.layoutControlItem2.CustomizationFormText = "Ценовая политика (назначение):";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(482, 24);
            this.layoutControlItem2.Text = "Ценовая политика (источник):";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(169, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cbFormula;
            this.layoutControlItem3.CustomizationFormText = "Формула:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(255, 24);
            this.layoutControlItem3.Text = "Формула:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(169, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.seFactor;
            this.layoutControlItem4.CustomizationFormText = "Коэффициент:";
            this.layoutControlItem4.Location = new System.Drawing.Point(255, 48);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(227, 24);
            this.layoutControlItem4.Text = "Коэффициент:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(169, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCalc;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(346, 338);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(136, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 338);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(346, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridContent;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(482, 266);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // ControlCalcPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ControlCalcPrices";
            this.Size = new System.Drawing.Size(502, 384);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seFactor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFormula.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCPDest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCPDest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbCPSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCPSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewContent;
        public DevExpress.XtraEditors.ComboBoxEdit cbFormula;
        public DevExpress.XtraEditors.GridLookUpEdit cbCPDest;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewCPDest;
        public DevExpress.XtraEditors.GridLookUpEdit cbCPSource;
        public DevExpress.XtraGrid.Views.Grid.GridView ViewCPSource;
        public DevExpress.XtraEditors.SimpleButton btnCalc;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colOldPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colNomenclature;
        public DevExpress.XtraGrid.GridControl gridContent;
        public DevExpress.XtraEditors.SpinEdit seFactor;


    }
}
