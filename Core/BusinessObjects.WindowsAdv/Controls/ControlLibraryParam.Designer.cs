namespace BusinessObjects.Windows.Controls
{
    partial class ControlLibraryParam
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
            this.layoutControlMain = new DevExpress.XtraLayout.LayoutControl();
            this.cmbCurrentValue = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbTypeEditor = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbTypeName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtDefault = new DevExpress.XtraEditors.TextEdit();
            this.chkAllowNull = new DevExpress.XtraEditors.CheckEdit();
            this.txtAlies = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlAlies = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlDefault = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlTypeName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlTypeEditor = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlCurrentValue = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).BeginInit();
            this.layoutControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrentValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeEditor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowNull.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAlies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTypeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTypeEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCurrentValue)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlMain
            // 
            this.layoutControlMain.Controls.Add(this.cmbCurrentValue);
            this.layoutControlMain.Controls.Add(this.cmbTypeEditor);
            this.layoutControlMain.Controls.Add(this.cmbTypeName);
            this.layoutControlMain.Controls.Add(this.txtDefault);
            this.layoutControlMain.Controls.Add(this.chkAllowNull);
            this.layoutControlMain.Controls.Add(this.txtAlies);
            this.layoutControlMain.Controls.Add(this.txtName);
            this.layoutControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMain.Location = new System.Drawing.Point(0, 0);
            this.layoutControlMain.Name = "layoutControlMain";
            this.layoutControlMain.Root = this.layoutControlGroup1;
            this.layoutControlMain.Size = new System.Drawing.Size(425, 187);
            this.layoutControlMain.TabIndex = 0;
            this.layoutControlMain.Text = "layoutControlMain";
            // 
            // cmbCurrentValue
            // 
            this.cmbCurrentValue.Location = new System.Drawing.Point(150, 154);
            this.cmbCurrentValue.Name = "cmbCurrentValue";
            this.cmbCurrentValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbCurrentValue.Size = new System.Drawing.Size(263, 20);
            this.cmbCurrentValue.StyleController = this.layoutControlMain;
            this.cmbCurrentValue.TabIndex = 10;
            // 
            // cmbTypeEditor
            // 
            this.cmbTypeEditor.Location = new System.Drawing.Point(150, 130);
            this.cmbTypeEditor.Name = "cmbTypeEditor";
            this.cmbTypeEditor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTypeEditor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbTypeEditor.Size = new System.Drawing.Size(263, 20);
            this.cmbTypeEditor.StyleController = this.layoutControlMain;
            this.cmbTypeEditor.TabIndex = 9;
            // 
            // cmbTypeName
            // 
            this.cmbTypeName.Location = new System.Drawing.Point(150, 106);
            this.cmbTypeName.Name = "cmbTypeName";
            this.cmbTypeName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTypeName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbTypeName.Size = new System.Drawing.Size(263, 20);
            this.cmbTypeName.StyleController = this.layoutControlMain;
            this.cmbTypeName.TabIndex = 8;
            // 
            // txtDefault
            // 
            this.txtDefault.Location = new System.Drawing.Point(150, 82);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Size = new System.Drawing.Size(263, 20);
            this.txtDefault.StyleController = this.layoutControlMain;
            this.txtDefault.TabIndex = 7;
            // 
            // chkAllowNull
            // 
            this.chkAllowNull.Location = new System.Drawing.Point(12, 60);
            this.chkAllowNull.Name = "chkAllowNull";
            this.chkAllowNull.Properties.Caption = "Разрешить пустое значение";
            this.chkAllowNull.Size = new System.Drawing.Size(401, 18);
            this.chkAllowNull.StyleController = this.layoutControlMain;
            this.chkAllowNull.TabIndex = 6;
            // 
            // txtAlies
            // 
            this.txtAlies.Location = new System.Drawing.Point(150, 36);
            this.txtAlies.Name = "txtAlies";
            this.txtAlies.Size = new System.Drawing.Size(263, 20);
            this.txtAlies.StyleController = this.layoutControlMain;
            this.txtAlies.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(150, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(263, 20);
            this.txtName.StyleController = this.layoutControlMain;
            this.txtName.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlName,
            this.layoutControlAlies,
            this.layoutControlItem3,
            this.layoutControlDefault,
            this.layoutControlTypeName,
            this.layoutControlTypeEditor,
            this.layoutControlCurrentValue});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(425, 187);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlName
            // 
            this.layoutControlName.Control = this.txtName;
            this.layoutControlName.CustomizationFormText = "Наименование";
            this.layoutControlName.Location = new System.Drawing.Point(0, 0);
            this.layoutControlName.Name = "layoutControlName";
            this.layoutControlName.Size = new System.Drawing.Size(405, 24);
            this.layoutControlName.Text = "Наименование:";
            this.layoutControlName.TextSize = new System.Drawing.Size(134, 13);
            // 
            // layoutControlAlies
            // 
            this.layoutControlAlies.Control = this.txtAlies;
            this.layoutControlAlies.CustomizationFormText = "Наименование параметра";
            this.layoutControlAlies.Location = new System.Drawing.Point(0, 24);
            this.layoutControlAlies.Name = "layoutControlAlies";
            this.layoutControlAlies.Size = new System.Drawing.Size(405, 24);
            this.layoutControlAlies.Text = "Наименование параметра:";
            this.layoutControlAlies.TextSize = new System.Drawing.Size(134, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkAllowNull;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(405, 22);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlDefault
            // 
            this.layoutControlDefault.Control = this.txtDefault;
            this.layoutControlDefault.CustomizationFormText = "Значение по умолчанию";
            this.layoutControlDefault.Location = new System.Drawing.Point(0, 70);
            this.layoutControlDefault.Name = "layoutControlDefault";
            this.layoutControlDefault.Size = new System.Drawing.Size(405, 24);
            this.layoutControlDefault.Text = "Значение по умолчанию:";
            this.layoutControlDefault.TextSize = new System.Drawing.Size(134, 13);
            // 
            // layoutControlTypeName
            // 
            this.layoutControlTypeName.Control = this.cmbTypeName;
            this.layoutControlTypeName.CustomizationFormText = "Тип";
            this.layoutControlTypeName.Location = new System.Drawing.Point(0, 94);
            this.layoutControlTypeName.Name = "layoutControlTypeName";
            this.layoutControlTypeName.Size = new System.Drawing.Size(405, 24);
            this.layoutControlTypeName.Text = "Тип:";
            this.layoutControlTypeName.TextSize = new System.Drawing.Size(134, 13);
            // 
            // layoutControlTypeEditor
            // 
            this.layoutControlTypeEditor.Control = this.cmbTypeEditor;
            this.layoutControlTypeEditor.CustomizationFormText = "Тип редактора";
            this.layoutControlTypeEditor.Location = new System.Drawing.Point(0, 118);
            this.layoutControlTypeEditor.Name = "layoutControlTypeEditor";
            this.layoutControlTypeEditor.Size = new System.Drawing.Size(405, 24);
            this.layoutControlTypeEditor.Text = "Тип редактора:";
            this.layoutControlTypeEditor.TextSize = new System.Drawing.Size(134, 13);
            // 
            // layoutControlCurrentValue
            // 
            this.layoutControlCurrentValue.Control = this.cmbCurrentValue;
            this.layoutControlCurrentValue.CustomizationFormText = "Текущее значение";
            this.layoutControlCurrentValue.Location = new System.Drawing.Point(0, 142);
            this.layoutControlCurrentValue.Name = "layoutControlCurrentValue";
            this.layoutControlCurrentValue.Size = new System.Drawing.Size(405, 25);
            this.layoutControlCurrentValue.Text = "Текущее значение:";
            this.layoutControlCurrentValue.TextSize = new System.Drawing.Size(134, 13);
            // 
            // ControlLibraryParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControlMain);
            this.MinimumSize = new System.Drawing.Size(425, 187);
            this.Name = "ControlLibraryParam";
            this.Size = new System.Drawing.Size(425, 187);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMain)).EndInit();
            this.layoutControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrentValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeEditor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllowNull.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAlies.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlAlies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTypeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlTypeEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlCurrentValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControlMain;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlAlies;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlDefault;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlTypeName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlTypeEditor;
        public DevExpress.XtraEditors.TextEdit txtName;
        public DevExpress.XtraEditors.CheckEdit chkAllowNull;
        public DevExpress.XtraEditors.TextEdit txtDefault;
        public DevExpress.XtraEditors.ComboBoxEdit cmbTypeName;
        public DevExpress.XtraEditors.ComboBoxEdit cmbTypeEditor;
        public DevExpress.XtraEditors.TextEdit txtAlies;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlCurrentValue;
        public DevExpress.XtraEditors.ComboBoxEdit cmbCurrentValue;
    }
}
