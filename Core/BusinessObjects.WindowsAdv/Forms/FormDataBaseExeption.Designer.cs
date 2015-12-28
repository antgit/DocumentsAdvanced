namespace BusinessObjects.Windows
{
    partial class FormDataBaseExeption
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
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtInnerEx = new DevExpress.XtraEditors.MemoEdit();
            this.dbeMessage = new DevExpress.XtraEditors.MemoEdit();
            this.dbeProcedure = new DevExpress.XtraEditors.TextEdit();
            this.dbeState = new DevExpress.XtraEditors.TextEdit();
            this.dbeSeverity = new DevExpress.XtraEditors.TextEdit();
            this.dbeNumber = new DevExpress.XtraEditors.TextEdit();
            this.dbeId = new DevExpress.XtraEditors.TextEdit();
            this.txtMessage = new DevExpress.XtraEditors.MemoEdit();
            this.txtAction = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupDbe = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupInnerEx = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInnerEx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeProcedure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeSeverity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDbe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInnerEx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.Size = new System.Drawing.Size(592, 148);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Toolbar.ShowCustomizeItem = false;
            // 
            // clientPanel
            // 
            this.clientPanel.Appearance.Options.UseBackColor = true;
            this.clientPanel.Controls.Add(this.layoutControl1);
            this.clientPanel.Location = new System.Drawing.Point(0, 148);
            this.clientPanel.Size = new System.Drawing.Size(592, 509);
            // 
            // btnSave
            // 
            this.btnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.barEditItemProgress);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 576);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(592, 23);
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.txtInnerEx);
            this.layoutControl1.Controls.Add(this.dbeMessage);
            this.layoutControl1.Controls.Add(this.dbeProcedure);
            this.layoutControl1.Controls.Add(this.dbeState);
            this.layoutControl1.Controls.Add(this.dbeSeverity);
            this.layoutControl1.Controls.Add(this.dbeNumber);
            this.layoutControl1.Controls.Add(this.dbeId);
            this.layoutControl1.Controls.Add(this.txtMessage);
            this.layoutControl1.Controls.Add(this.txtAction);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(592, 509);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtInnerEx
            // 
            this.txtInnerEx.Location = new System.Drawing.Point(24, 390);
            this.txtInnerEx.MenuManager = this.ribbon;
            this.txtInnerEx.Name = "txtInnerEx";
            this.txtInnerEx.Size = new System.Drawing.Size(544, 95);
            this.txtInnerEx.StyleController = this.layoutControl1;
            this.txtInnerEx.TabIndex = 12;
            // 
            // dbeMessage
            // 
            this.dbeMessage.Location = new System.Drawing.Point(24, 325);
            this.dbeMessage.MenuManager = this.ribbon;
            this.dbeMessage.Name = "dbeMessage";
            this.dbeMessage.Size = new System.Drawing.Size(544, 16);
            this.dbeMessage.StyleController = this.layoutControl1;
            this.dbeMessage.TabIndex = 11;
            // 
            // dbeProcedure
            // 
            this.dbeProcedure.Location = new System.Drawing.Point(90, 285);
            this.dbeProcedure.MenuManager = this.ribbon;
            this.dbeProcedure.Name = "dbeProcedure";
            this.dbeProcedure.Size = new System.Drawing.Size(478, 20);
            this.dbeProcedure.StyleController = this.layoutControl1;
            this.dbeProcedure.TabIndex = 10;
            // 
            // dbeState
            // 
            this.dbeState.Location = new System.Drawing.Point(90, 261);
            this.dbeState.MenuManager = this.ribbon;
            this.dbeState.Name = "dbeState";
            this.dbeState.Size = new System.Drawing.Size(478, 20);
            this.dbeState.StyleController = this.layoutControl1;
            this.dbeState.TabIndex = 9;
            // 
            // dbeSeverity
            // 
            this.dbeSeverity.Location = new System.Drawing.Point(90, 237);
            this.dbeSeverity.MenuManager = this.ribbon;
            this.dbeSeverity.Name = "dbeSeverity";
            this.dbeSeverity.Size = new System.Drawing.Size(478, 20);
            this.dbeSeverity.StyleController = this.layoutControl1;
            this.dbeSeverity.TabIndex = 8;
            // 
            // dbeNumber
            // 
            this.dbeNumber.Location = new System.Drawing.Point(90, 213);
            this.dbeNumber.MenuManager = this.ribbon;
            this.dbeNumber.Name = "dbeNumber";
            this.dbeNumber.Size = new System.Drawing.Size(478, 20);
            this.dbeNumber.StyleController = this.layoutControl1;
            this.dbeNumber.TabIndex = 7;
            // 
            // dbeId
            // 
            this.dbeId.Location = new System.Drawing.Point(90, 189);
            this.dbeId.MenuManager = this.ribbon;
            this.dbeId.Name = "dbeId";
            this.dbeId.Size = new System.Drawing.Size(478, 20);
            this.dbeId.StyleController = this.layoutControl1;
            this.dbeId.TabIndex = 6;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(24, 84);
            this.txtMessage.MenuManager = this.ribbon;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(544, 56);
            this.txtMessage.StyleController = this.layoutControl1;
            this.txtMessage.TabIndex = 5;
            // 
            // txtAction
            // 
            this.txtAction.Location = new System.Drawing.Point(90, 44);
            this.txtAction.MenuManager = this.ribbon;
            this.txtAction.Name = "txtAction";
            this.txtAction.Size = new System.Drawing.Size(478, 20);
            this.txtAction.StyleController = this.layoutControl1;
            this.txtAction.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroupDbe,
            this.layoutControlGroupInnerEx});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(592, 509);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Сообщение:";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(572, 144);
            this.layoutControlGroup2.Text = "Сообщение:";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtAction;
            this.layoutControlItem1.CustomizationFormText = "Действие";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(548, 24);
            this.layoutControlItem1.Text = "Действие:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtMessage;
            this.layoutControlItem2.CustomizationFormText = "Сообщение:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 76);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(66, 76);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(548, 76);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Сообщение:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlGroupDbe
            // 
            this.layoutControlGroupDbe.CustomizationFormText = "Протокол ошибки:";
            this.layoutControlGroupDbe.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroupDbe.ExpandButtonVisible = true;
            this.layoutControlGroupDbe.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroupDbe.Location = new System.Drawing.Point(0, 144);
            this.layoutControlGroupDbe.Name = "layoutControlGroupDbe";
            this.layoutControlGroupDbe.Size = new System.Drawing.Size(572, 201);
            this.layoutControlGroupDbe.Text = "Протокол ошибки:";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.dbeId;
            this.layoutControlItem3.CustomizationFormText = "Ид:";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(548, 24);
            this.layoutControlItem3.Text = "Ид:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dbeNumber;
            this.layoutControlItem4.CustomizationFormText = "Номер:";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(548, 24);
            this.layoutControlItem4.Text = "Номер:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dbeSeverity;
            this.layoutControlItem5.CustomizationFormText = "Уровень:";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(548, 24);
            this.layoutControlItem5.Text = "Уровень:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.dbeState;
            this.layoutControlItem6.CustomizationFormText = "Состояние:";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(548, 24);
            this.layoutControlItem6.Text = "Состояние:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.dbeProcedure;
            this.layoutControlItem7.CustomizationFormText = "Процедура:";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(548, 24);
            this.layoutControlItem7.Text = "Процедура:";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.dbeMessage;
            this.layoutControlItem8.CustomizationFormText = "Сообщение:";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(548, 36);
            this.layoutControlItem8.Text = "Сообщение:";
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlGroupInnerEx
            // 
            this.layoutControlGroupInnerEx.CustomizationFormText = "Внутренняя информация";
            this.layoutControlGroupInnerEx.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutControlGroupInnerEx.ExpandButtonVisible = true;
            this.layoutControlGroupInnerEx.ExpandOnDoubleClick = true;
            this.layoutControlGroupInnerEx.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9});
            this.layoutControlGroupInnerEx.Location = new System.Drawing.Point(0, 345);
            this.layoutControlGroupInnerEx.Name = "layoutControlGroupInnerEx";
            this.layoutControlGroupInnerEx.Size = new System.Drawing.Size(572, 144);
            this.layoutControlGroupInnerEx.Text = "Внутренняя информация";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtInnerEx;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(548, 99);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // FormDataBaseExeption
            // 
            this.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(592, 680);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "FormDataBaseExeption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CreateMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtInnerEx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeProcedure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeSeverity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbeId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDbe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupInnerEx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        public DevExpress.XtraEditors.TextEdit txtAction;
        public DevExpress.XtraEditors.MemoEdit txtMessage;
        public DevExpress.XtraEditors.TextEdit dbeId;
        public DevExpress.XtraEditors.TextEdit dbeNumber;
        public DevExpress.XtraEditors.TextEdit dbeSeverity;
        public DevExpress.XtraEditors.TextEdit dbeState;
        public DevExpress.XtraEditors.TextEdit dbeProcedure;
        public DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupDbe;
        public DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupInnerEx;
        public DevExpress.XtraEditors.MemoEdit dbeMessage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        public DevExpress.XtraEditors.MemoEdit txtInnerEx;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;


    }
}
