using System;
using System.IO;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using System.Collections.Generic;
using BusinessObjects.Security;
using System.Linq;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlXmlStorage : BasePropertyControlIBase<XmlStorage>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlXmlStorage()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.XmlData = _common.txtXmlData.Text;
            SelectedItem.UserId = _common.cmbUsers.EditValue == null ? 0 : (int)_common.cmbUsers.EditValue;

            SaveStateData();

            InternalSave();
        }

        ControlXmlStorage _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlXmlStorage
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  Workarea = SelectedItem.Workarea
                              };
                
                _common.txtXmlData.Popup += delegate(object sender, EventArgs e)
                {
                    Control c = ((DevExpress.Utils.Win.IPopupControl)sender).PopupWindow;
                    c.Width = _common.txtXmlData.Size.Width;
                    c.Height = _common.Size.Height - _common.txtXmlData.Top;
                };
                _common.txtName.Text = SelectedItem.Name;
                _common.txtNameFull2.Text = SelectedItem.NameFull;
                _common.txtCode.Text = SelectedItem.Code;
                _common.txtCodeFind.Text = SelectedItem.CodeFind;
                _common.txtMemo.Text = SelectedItem.Memo;
                _common.txtXmlData.Text = SelectedItem.XmlData;

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewUsers, "DEFAULT_LOOKUP_NAME");
                _common.cmbUsers.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbUsers.Properties.ValueMember = GlobalPropertyNames.Id;
                List<Uid> users = SelectedItem.Workarea.GetCollection<Uid>().Where(u => u.KindValue == 1).ToList();
                _common.cmbUsers.Properties.DataSource = users;
                _common.cmbUsers.EditValue = SelectedItem.UserId;

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}