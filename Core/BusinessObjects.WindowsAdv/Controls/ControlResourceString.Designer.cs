namespace BusinessObjects.Windows.Controls
{
    partial class ControlResourceString
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
            DevExpress.XtraSpellChecker.OptionsSpelling optionsSpelling1 = new DevExpress.XtraSpellChecker.OptionsSpelling();
            DevExpress.XtraSpellChecker.OptionsSpelling optionsSpelling2 = new DevExpress.XtraSpellChecker.OptionsSpelling();
            DevExpress.XtraSpellChecker.OptionsSpelling optionsSpelling3 = new DevExpress.XtraSpellChecker.OptionsSpelling();
            DevExpress.XtraSpellChecker.OptionsSpelling optionsSpelling4 = new DevExpress.XtraSpellChecker.OptionsSpelling();
            DevExpress.XtraSpellChecker.OptionsSpelling optionsSpelling5 = new DevExpress.XtraSpellChecker.OptionsSpelling();
            this.cmbCulture = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItemCulture = new DevExpress.XtraLayout.LayoutControlItem();
            this.spellChecker1 = new DevExpress.XtraSpellChecker.SpellChecker();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).BeginInit();
            this.LayoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCulture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCulture)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(12, 124);
            this.spellChecker1.SetShowSpellCheckMenu(this.txtMemo, true);
            this.txtMemo.Size = new System.Drawing.Size(376, 49);
            this.spellChecker1.SetSpellCheckerOptions(this.txtMemo, optionsSpelling1);
            // 
            // txtCode
            // 
            this.spellChecker1.SetShowSpellCheckMenu(this.txtCode, true);
            this.spellChecker1.SetSpellCheckerOptions(this.txtCode, optionsSpelling2);
            // 
            // txtName
            // 
            this.spellChecker1.SetShowSpellCheckMenu(this.txtName, true);
            this.spellChecker1.SetSpellCheckerOptions(this.txtName, optionsSpelling3);
            // 
            // layoutControlItemMemo
            // 
            this.layoutControlItemMemo.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItemMemo.Size = new System.Drawing.Size(380, 69);
            // 
            // txtCodeFind
            // 
            this.spellChecker1.SetShowSpellCheckMenu(this.txtCodeFind, true);
            this.spellChecker1.SetSpellCheckerOptions(this.txtCodeFind, optionsSpelling4);
            // 
            // layoutControlItemCodeFind
            // 
            this.layoutControlItemCodeFind.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemNameFull2
            // 
            this.layoutControlItemNameFull2.Location = new System.Drawing.Point(0, 165);
            this.layoutControlItemNameFull2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // txtNameFull2
            // 
            this.txtNameFull2.Location = new System.Drawing.Point(12, 193);
            this.spellChecker1.SetShowSpellCheckMenu(this.txtNameFull2, true);
            this.spellChecker1.SetSpellCheckerOptions(this.txtNameFull2, optionsSpelling5);
            // 
            // LayoutControl
            // 
            this.LayoutControl.Controls.Add(this.cmbCulture);
            this.LayoutControl.Controls.SetChildIndex(this.txtCodeFind, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtNameFull2, 0);
            this.LayoutControl.Controls.SetChildIndex(this.cmbCulture, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtName, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtCode, 0);
            this.LayoutControl.Controls.SetChildIndex(this.txtMemo, 0);
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemCulture});
            // 
            // cmbCulture
            // 
            this.cmbCulture.Location = new System.Drawing.Point(132, 84);
            this.cmbCulture.Name = "cmbCulture";
            this.cmbCulture.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCulture.Size = new System.Drawing.Size(256, 20);
            this.cmbCulture.StyleController = this.LayoutControl;
            this.cmbCulture.TabIndex = 7;
            // 
            // layoutControlItemCulture
            // 
            this.layoutControlItemCulture.Control = this.cmbCulture;
            this.layoutControlItemCulture.CustomizationFormText = "Язык";
            this.layoutControlItemCulture.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemCulture.Name = "layoutControlItemCulture";
            this.layoutControlItemCulture.Size = new System.Drawing.Size(380, 24);
            this.layoutControlItemCulture.Text = "Язык:";
            this.layoutControlItemCulture.TextSize = new System.Drawing.Size(116, 13);
            // 
            // spellChecker1
            // 
            this.spellChecker1.Culture = new System.Globalization.CultureInfo("ru-RU");
            this.spellChecker1.ParentContainer = this.txtMemo;
            // 
            // ControlResourceString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlResourceString";
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodeFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCodeFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemNameFull2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNameFull2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayoutControl)).EndInit();
            this.LayoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCulture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCulture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.LookUpEdit cmbCulture;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCulture;
        public DevExpress.XtraSpellChecker.SpellChecker spellChecker1;

    }
}
