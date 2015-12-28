using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств аналитики
    /// </summary>
    internal sealed class BuildControlWebService : BasePropertyControlIBase<WebService>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlWebService()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
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
            SelectedItem.UrlAddress = _common.txtUrlAddress.Text;
            SelectedItem.Uid = _common.txtUid.Text;
            SelectedItem.Password = _common.txtPassword.Text;
            SelectedItem.AuthKind = _common.cmbAuthKind.SelectedIndex;
            SelectedItem.StorePassword = _common.chkStorePassword.Checked;
            
            SaveStateData();

            InternalSave();
        }

        ControlWebService _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {

                _common = new ControlWebService
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtUrlAddress = {Text = SelectedItem.UrlAddress},
                                  txtUid = {Text = SelectedItem.Uid},
                                  txtPassword = {Text = SelectedItem.Password},
                                  Workarea = SelectedItem.Workarea,
                                  chkStorePassword = {Checked = SelectedItem.StorePassword}
                              };
                _common.cmbAuthKind.Properties.Items.Add("Нет авторизации");
                _common.cmbAuthKind.Properties.Items.Add("Windows авторизация");
                _common.cmbAuthKind.Properties.Items.Add("На основе форм");

                _common.cmbAuthKind.SelectedIndex = SelectedItem.AuthKind;

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);

                

                //frmProp.btnActions.Visibility = BarItemVisibility.Always;
                //BarItemLink lnk = frmProp.ActionMenu.AddItem(new BarButtonItem() { Caption = "Пользовательские флаги" });
                //lnk.Item.ItemClick += delegate
                //                          {
                //                              SelectedItem.ShowFlagString();
                //                          };
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }

    //private sealed class
}