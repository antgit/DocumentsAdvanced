using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlRate : BasePropertyControlIBase<Rate>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlRate()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
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
            SelectedItem.Value = _common.numValue.Value;
            SelectedItem.Multiplier = _common.numDivider.Value;

            SelectedItem.BankId = (int)_common.cmbBank.EditValue;
            SelectedItem.CurrencyToId = (int)_common.cmbCurrencyTo.EditValue;
            SelectedItem.CurrencyFromId = (int)_common.cmbCurrencyFrom.EditValue;
            SelectedItem.Date = _common.dtDate.DateTime;
            SaveStateData();

            InternalSave();
        }

        ControlCurrencyRate _common;
        private List<Agent> _collBanks;
        private BindingSource bindingSourceBank;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlCurrencyRate
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  numValue = {Value = SelectedItem.Value},
                                  numDivider = {Value = SelectedItem.Multiplier},
                                  dtDate = {DateTime = SelectedItem.Date},
                                  Workarea = SelectedItem.Workarea
                              };

               
                BindingSource bindingSourceCurrencyFrom = new BindingSource
                                                              {
                                                                  DataSource =
                                                                      SelectedItem.Workarea.GetCollection<Currency>()
                                                              };
                _common.cmbCurrencyFrom.Properties.DataSource = bindingSourceCurrencyFrom;
                _common.cmbCurrencyFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCurrencyFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbCurrencyFrom.EditValue = SelectedItem.CurrencyFromId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbCurrencyFrom,
                                                         "DEFAULT_LOOKUPCURRENCY");


                BindingSource bindingSourceCurrencyTo = new BindingSource
                                                            {
                                                                DataSource =
                                                                    SelectedItem.Workarea.GetCollection<Currency>()
                                                            };
                _common.cmbCurrencyTo.Properties.DataSource = bindingSourceCurrencyTo;
                _common.cmbCurrencyTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCurrencyTo.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbCurrencyTo.EditValue = SelectedItem.CurrencyToId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbCurrencyTo,
                                                         "DEFAULT_LOOKUPCURRENCY");

                _collBanks = new List<Agent>();
                bindingSourceBank = new BindingSource();
                                                      //{
                                                      //    //BANKS
                                                      //    DataSource = SelectedItem.Workarea.GetCollection<Currency>()
                                                      //};
                if (SelectedItem.BankId != 0)
                    _collBanks.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.BankId));
                bindingSourceBank.DataSource = _collBanks;
                _common.cmbBank.Properties.DataSource = bindingSourceBank;
                _common.cmbBank.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbBank.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbBank.EditValue = SelectedItem.BankId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbBank,
                                                         "DEFAULT_LOOKUPAGENT");
                _common.cmbBank.QueryPopUp += CmbGridLookUpEditQueryPopUp;

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
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LookUpEdit cmb = sender as LookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbBank" && bindingSourceBank.Count < 2)
                {
                    Hierarchy rootBanks = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BANKS);
                    _collBanks = rootBanks.GetTypeContents<Agent>();
                    bindingSourceBank.DataSource = _collBanks;
                }
                //else if (cmb.Name == "cmbForm" && _bindingSourceForm.Count < 2)
                //{
                //    _collForm = SelectedItem.Workarea.GetCollection<Library>().Where(s => s.KindValue == 16).ToList();
                //    _bindingSourceForm.DataSource = _collForm;
                //}
                //else if (cmb.Name == "cmbViewDocs" && _bindingSourceViewDocs.Count < 2)
                //{
                //    _collViewDocs = SelectedItem.Workarea.GetCollection<CustomViewList>().Where(s => s.SourceEntityTypeId == 20).ToList();
                //    _bindingSourceViewDocs.DataSource = _collViewDocs;
                //}
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