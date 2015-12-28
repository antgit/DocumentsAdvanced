using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств пользовательских аккаунтов
    /// </summary>
    internal sealed class BuildControlUserAccount : BasePropertyControlIBase<UserAccount>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlUserAccount()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            // TODO: 
            //TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
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

            SelectedItem.Email = _common.txtEmail.Text;
            SelectedItem.Password = _common.txtPassword.Text;
            SelectedItem.UserId = (int)_common.cmbUserId.EditValue;
            
            if (string.IsNullOrEmpty(SelectedItem.CodeFind))
            {
                SelectedItem.CodeFind = (SelectedItem.Name + Transliteration.Front(SelectedItem.Name)).Replace(" ", "");
            }

            SaveStateData();

            InternalSave();
        }

        ControlUserAccount _common;
        BindingSource _bindingSourceUserId;
        List<Uid> _collUserId;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlUserAccount
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea,
                                  txtPassword = {Text = SelectedItem.Password},
                                  txtEmail = {Text = SelectedItem.Email}
                              };

                #region Данные для списка "Автор"
                _common.cmbUserId.Properties.DisplayMember = GlobalPropertyNames.Agent;
                _common.cmbUserId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceUserId = new BindingSource();
                _collUserId = new List<Uid>();
                if (SelectedItem.UserId != 0)
                    _collUserId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Uid>().Item(SelectedItem.UserId));
                _bindingSourceUserId.DataSource = _collUserId;
                _common.cmbUserId.Properties.DataSource = _bindingSourceUserId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewUserOwner, "DEFAULT_LOOKUP_UID");
                _common.cmbUserId.EditValue = SelectedItem.UserId;
                _common.cmbUserId.QueryPopUp += CmbGridSearchLookUpEditQueryPopUp;
                _common.ViewUserOwner.CustomUnboundColumnData += ViewUserOwnerIdCustomUnboundColumnData;
                _common.cmbUserId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUserId.EditValue = 0;
                };
                #endregion

                _common.txtPassword.ButtonClick += delegate
                                                     {
                                                         if (SelectedItem.Workarea.Access.RightCommon.Admin || SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                                                             _common.txtPassword.Properties.PasswordChar = _common.txtPassword.Properties.PasswordChar == '*' ? '\0' : '*';
                                                     };
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
        void ViewUserOwnerIdCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayUidImagesLookupGrid(e, _bindingSourceUserId);
        }
        void CmbGridSearchLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, cmb.Properties.PopupFormSize.Height);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbUserId" && _bindingSourceUserId.Count < 2)
                {
                    _collUserId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER).ToList();
                    _bindingSourceUserId.DataSource = _collUserId;
                }
                
            }
            catch (Exception z)
            { }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
    }
}