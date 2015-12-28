using System;
using DevExpress.XtraBars;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства
        /// <summary>
        /// Свойства дополнительного параметра библиотеки
        /// </summary>
        /// <param name="item">Дополнительный параметр библиотеки</param>
        /// <returns></returns>
        public static Form ShowProperty(this LibraryReportParam item)
        {
            FormProperties frm = new FormProperties
            {
                //ribbon = { ApplicationIcon = item.GetImage() },
                btnSaveClose = { Visibility = BarItemVisibility.Always },
            };

            new FormStateMaintainer(frm, string.Format("Property{0}", item.GetType().Name));
            frm.Text = string.Format("Свойства: {0}", item.Name);
            frm.Key = "Окно свойств - дополнительный параметр библиотеки";

            ControlLibraryParam _control = new ControlLibraryParam();

            foreach (string t in Enum.GetNames(typeof(TypeCode)))
                _control.cmbTypeName.Properties.Items.Add("System." + t);
            _control.cmbTypeName.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Delete)
                    _control.cmbTypeName.Text = "";
            };

            _control.cmbTypeEditor.SelectedValueChanged += delegate
            {
                if (_control.cmbTypeEditor.Text == "")
                    _control.cmbCurrentValue.Enabled = false;
                else
                    _control.cmbCurrentValue.Enabled = true;
                if (_control.cmbTypeEditor.Text == "TextEditor")
                {
                    _control.cmbCurrentValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    _control.cmbCurrentValue.Properties.Buttons[0].Visible = false;
                }
                else if (_control.cmbTypeEditor.Text.StartsWith("Browse"))
                {
                    _control.cmbCurrentValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                    _control.cmbCurrentValue.Properties.Buttons[0].Visible = true;
                }
            };
            _control.cmbTypeEditor.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Delete)
                    _control.cmbTypeEditor.Text = "";
            };

            _control.cmbTypeEditor.Properties.Items.Add("TextEditor");
            _control.cmbTypeEditor.Properties.Items.Add("BrowseProduct");
            _control.cmbTypeEditor.Properties.Items.Add("BrowseAnalitic");
            _control.cmbTypeEditor.Properties.Items.Add("BrowseAgent");

            if (_control.cmbTypeEditor.Text == "")
                _control.cmbCurrentValue.Enabled = false;
            _control.cmbCurrentValue.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
            {
                string typeEntity = _control.cmbTypeEditor.Text.Remove(0, 6);
                IBase obj = item.Workarea.Empty((WhellKnownDbEntity)Enum.Parse(typeof(WhellKnownDbEntity), typeEntity));
                _control.cmbCurrentValue.Text = obj.BrowseListType().Id.ToString();
            };

            _control.txtName.Text = item.Name;
            _control.txtAlies.Text = item.Alias;
            _control.txtDefault.Text = item.Default;
            _control.chkAllowNull.Checked = item.AllowNull;
            _control.cmbTypeName.Text = item.TypeName;
            _control.cmbTypeEditor.Text = item.TypeEditor;
            _control.cmbCurrentValue.Text = item.CurrentValue;

            frm.clientPanel.Controls.Add(_control);
            _control.Dock = DockStyle.Fill;
            frm.MinimumSize = new System.Drawing.Size(_control.MinimumSize.Width + 100, _control.MinimumSize.Height + 200);

            #region OK
            frm.btnSaveClose.ItemClick += delegate
            {
                item.Name = _control.txtName.Text;
                item.Alias = _control.txtAlies.Text;
                item.Default = _control.txtDefault.Text;
                item.AllowNull = _control.chkAllowNull.Checked;
                item.TypeName = _control.cmbTypeName.Text;
                item.TypeEditor = _control.cmbTypeEditor.Text;
                item.CurrentValue = _control.cmbCurrentValue.Text;

                frm.Close();
            };
            frm.btnSave.ItemClick += delegate
            {
                item.Name = _control.txtName.Text;
                item.Alias = _control.txtAlies.Text;
                item.Default = _control.txtDefault.Text;
                item.AllowNull = _control.chkAllowNull.Checked;
                item.TypeName = _control.cmbTypeName.Text;
                item.TypeEditor = _control.cmbTypeEditor.Text;
                item.CurrentValue = _control.cmbCurrentValue.Text;
            };
            #endregion
            frm.ShowDialog();
            return frm;
        }
        #endregion
    }
}
