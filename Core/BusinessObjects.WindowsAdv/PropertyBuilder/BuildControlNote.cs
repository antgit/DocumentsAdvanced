using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств примечания
    /// </summary>
    internal sealed class BuildControlNote : BasePropertyControlIBase<Note>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlNote()
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
            SelectedItem.UserOwnerId = (int)_common.cmbUserUwnerId.EditValue;
            SaveStateData();

            InternalSave();
        }

        ControlNote _common;
        BindingSource _bindingSourceUsers;
        List<Uid> _collUsers;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlNote
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };

                #region Данные для списка "Автор"
                _common.cmbUserUwnerId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbUserUwnerId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceUsers = new BindingSource();
                _collUsers = new List<Uid>();
                if (SelectedItem.UserOwnerId != 0)
                    _collUsers.Add(SelectedItem.Workarea.Cashe.GetCasheData<Uid>().Item(SelectedItem.UserOwnerId));
                _bindingSourceUsers.DataSource = _collUsers;
                _common.cmbUserUwnerId.Properties.DataSource = _bindingSourceUsers;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbUserUwnerId, "DEFAULT_LISTVIEWUID");
                _common.cmbUserUwnerId.EditValue = SelectedItem.UserOwnerId;
                _common.cmbUserUwnerId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbUserUwnerId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUserUwnerId.EditValue = 0;
                };
                _common.cmbUserUwnerId.Properties.View.CustomUnboundColumnData += ViewWorkersCustomUnboundColumnData;
                //DisplayAgentImagesLookupGrid(e, _bindWorkers);
                #endregion
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
        void ViewWorkersCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayUidImagesLookupGrid(e, _bindingSourceUsers);
        }
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;

                if (cmb.Name == "cmbUserUwnerId" && _bindingSourceUsers.Count < 2)
                {
                    _collUsers = SelectedItem.Workarea.GetCollection<Uid>().Where(s => s.KindValue == Uid.KINDVALUE_USER).ToList();
                    _bindingSourceUsers.DataSource = _collUsers;
                }
                
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
    }
}