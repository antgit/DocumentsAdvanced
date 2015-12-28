namespace BusinessObjects.Windows.Controls
{
    partial class ControlSystemParameterUser : DevExpress.XtraEditors.XtraUserControl
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
            this.txtString = new DevExpress.XtraEditors.TextEdit();
            this.LayoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.numInt = new DevExpress.XtraEditors.SpinEdit();
            this.numMoney = new DevExpress.XtraEditors.SpinEdit();
            this.txtGuid = new DevExpress.XtraEditors.TextEdit();
            this.numFloat = new DevExpress.XtraEditors.SpinEdit();
            this.cmbReference = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbReferenceKind = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbUser = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlUser = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemString = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemInt = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemMoney = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemGuid = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFloat = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemView = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtString.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGuid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReference.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReferenceKind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGuid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtString
            // 
            this.txtString.Location = new System.Drawing.Point(94, 84);
            this.txtString.Name = "txtString";
            this.txtString.Size = new System.Drawing.Size(309, 20);
            this.txtString.StyleController = this.LayoutControl;
            this.txtString.TabIndex = 7;
            // 
            // LayoutControl
            // 
            this.LayoutControl.AllowCustomizationMenu = false;
            this.LayoutControl.Controls.Add(this.txtString);
            this.LayoutControl.Controls.Add(this.numInt);
            this.LayoutControl.Controls.Add(this.numMoney);
            this.LayoutControl.Controls.Add(this.txtGuid);
            this.LayoutControl.Controls.Add(this.numFloat);
            this.LayoutControl.Controls.Add(this.cmbReference);
            this.LayoutControl.Controls.Add(this.cmbReferenceKind);
            this.LayoutControl.Controls.Add(this.cmbUser);
            this.LayoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutControl.Location = new System.Drawing.Point(0, 0);
            this.LayoutControl.Name = "LayoutControl";
            this.LayoutControl.Root = this.layoutControlGroup;
            this.LayoutControl.Size = new System.Drawing.Size(415, 310);
            this.LayoutControl.TabIndex = 0;
            this.LayoutControl.Text = "layoutControl1";
            // 
            // numInt
            // 
            this.numInt.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numInt.Location = new System.Drawing.Point(94, 108);
            this.numInt.Name = "numInt";
            this.numInt.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.numInt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numInt.Properties.Mask.EditMask = "n0";
            this.numInt.Size = new System.Drawing.Size(309, 20);
            this.numInt.StyleController = this.LayoutControl;
            this.numInt.TabIndex = 8;
            // 
            // numMoney
            // 
            this.numMoney.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numMoney.Location = new System.Drawing.Point(94, 132);
            this.numMoney.Name = "numMoney";
            this.numMoney.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.numMoney.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMoney.Properties.Mask.EditMask = "n4";
            this.numMoney.Size = new System.Drawing.Size(309, 20);
            this.numMoney.StyleController = this.LayoutControl;
            this.numMoney.TabIndex = 9;
            // 
            // txtGuid
            // 
            this.txtGuid.Location = new System.Drawing.Point(94, 180);
            this.txtGuid.Name = "txtGuid";
            this.txtGuid.Size = new System.Drawing.Size(309, 20);
            this.txtGuid.StyleController = this.LayoutControl;
            this.txtGuid.TabIndex = 10;
            // 
            // numFloat
            // 
            this.numFloat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numFloat.Location = new System.Drawing.Point(94, 156);
            this.numFloat.Name = "numFloat";
            this.numFloat.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.numFloat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numFloat.Properties.Mask.EditMask = "d";
            this.numFloat.Size = new System.Drawing.Size(309, 20);
            this.numFloat.StyleController = this.LayoutControl;
            this.numFloat.TabIndex = 12;
            // 
            // cmbReference
            // 
            this.cmbReference.Location = new System.Drawing.Point(94, 60);
            this.cmbReference.Name = "cmbReference";
            this.cmbReference.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReference.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbReference.Size = new System.Drawing.Size(309, 20);
            this.cmbReference.StyleController = this.LayoutControl;
            this.cmbReference.TabIndex = 13;
            // 
            // cmbReferenceKind
            // 
            this.cmbReferenceKind.Location = new System.Drawing.Point(94, 36);
            this.cmbReferenceKind.Name = "cmbReferenceKind";
            this.cmbReferenceKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReferenceKind.Size = new System.Drawing.Size(309, 20);
            this.cmbReferenceKind.StyleController = this.LayoutControl;
            this.cmbReferenceKind.TabIndex = 14;
            // 
            // cmbUser
            // 
            this.cmbUser.Location = new System.Drawing.Point(94, 12);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUser.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbUser.Size = new System.Drawing.Size(309, 20);
            this.cmbUser.StyleController = this.LayoutControl;
            this.cmbUser.TabIndex = 14;
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.CustomizationFormText = "Основные";
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlUser,
            this.layoutControlItemString,
            this.layoutControlItemInt,
            this.layoutControlItemMoney,
            this.layoutControlItemGuid,
            this.layoutControlItemFloat,
            this.layoutControlItemView,
            this.layoutControlItem1});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Size = new System.Drawing.Size(415, 310);
            this.layoutControlGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup.Text = "Основные";
            this.layoutControlGroup.TextVisible = false;
            // 
            // layoutControlUser
            // 
            this.layoutControlUser.Control = this.cmbUser;
            this.layoutControlUser.CustomizationFormText = "Пользователь";
            this.layoutControlUser.Location = new System.Drawing.Point(0, 0);
            this.layoutControlUser.Name = "layoutControlUser";
            this.layoutControlUser.Size = new System.Drawing.Size(395, 24);
            this.layoutControlUser.Text = "Пользователь:";
            this.layoutControlUser.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItemString
            // 
            this.layoutControlItemString.Control = this.txtString;
            this.layoutControlItemString.CustomizationFormText = "Строка";
            this.layoutControlItemString.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemString.Name = "layoutControlItemString";
            this.layoutControlItemString.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItemString.Text = "Строка:";
            this.layoutControlItemString.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItemInt
            // 
            this.layoutControlItemInt.Control = this.numInt;
            this.layoutControlItemInt.CustomizationFormText = "Число";
            this.layoutControlItemInt.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemInt.Name = "layoutControlItemInt";
            this.layoutControlItemInt.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItemInt.Text = "Число:";
            this.layoutControlItemInt.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItemMoney
            // 
            this.layoutControlItemMoney.Control = this.numMoney;
            this.layoutControlItemMoney.CustomizationFormText = "Денежное";
            this.layoutControlItemMoney.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItemMoney.Name = "layoutControlItemMoney";
            this.layoutControlItemMoney.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItemMoney.Text = "Денежное:";
            this.layoutControlItemMoney.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItemGuid
            // 
            this.layoutControlItemGuid.Control = this.txtGuid;
            this.layoutControlItemGuid.CustomizationFormText = "Гуид";
            this.layoutControlItemGuid.Location = new System.Drawing.Point(0, 168);
            this.layoutControlItemGuid.Name = "layoutControlItemGuid";
            this.layoutControlItemGuid.Size = new System.Drawing.Size(395, 122);
            this.layoutControlItemGuid.Text = "Гуид:";
            this.layoutControlItemGuid.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItemFloat
            // 
            this.layoutControlItemFloat.Control = this.numFloat;
            this.layoutControlItemFloat.CustomizationFormText = "Вещественное";
            this.layoutControlItemFloat.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItemFloat.Name = "layoutControlItemFloat";
            this.layoutControlItemFloat.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItemFloat.Text = "Вещественное:";
            this.layoutControlItemFloat.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItemView
            // 
            this.layoutControlItemView.Control = this.cmbReference;
            this.layoutControlItemView.CustomizationFormText = "Значение";
            this.layoutControlItemView.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemView.Name = "layoutControlItemView";
            this.layoutControlItemView.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItemView.Text = "Значение:";
            this.layoutControlItemView.TextSize = new System.Drawing.Size(78, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbReferenceKind;
            this.layoutControlItem1.CustomizationFormText = "Тип ссылки";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(395, 24);
            this.layoutControlItem1.Text = "Тип ссылки:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(78, 13);
            // 
            // ControlSystemParameterUser
            // 
            this.Controls.Add(this.LayoutControl);
            this.MinimumSize = new System.Drawing.Size(415, 310);
            this.Name = "ControlSystemParameterUser";
            this.Size = new System.Drawing.Size(415, 310);
            ((System.ComponentModel.ISupportInitialize)(this.txtString.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numInt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGuid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFloat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReference.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReferenceKind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemGuid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraLayout.LayoutControl LayoutControl;
        public DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemString;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemInt;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemMoney;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemGuid;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemFloat;
        public DevExpress.XtraEditors.LookUpEdit cmbReference;
        public DevExpress.XtraEditors.LookUpEdit cmbUser;
        public DevExpress.XtraLayout.LayoutControlItem layoutControlItemView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlUser;
        public DevExpress.XtraEditors.LookUpEdit cmbReferenceKind;
        public DevExpress.XtraEditors.TextEdit txtString;
        public DevExpress.XtraEditors.SpinEdit numFloat;
        public DevExpress.XtraEditors.SpinEdit numMoney;
        public DevExpress.XtraEditors.SpinEdit numInt;
        public DevExpress.XtraEditors.TextEdit txtGuid;
    }
}
