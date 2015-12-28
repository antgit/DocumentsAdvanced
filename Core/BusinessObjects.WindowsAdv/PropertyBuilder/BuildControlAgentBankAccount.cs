using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildAgentBankAccountControl : BasePropertyControlIBase<AgentBankAccount>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildAgentBankAccountControl()
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
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.BankId = (int)_common.cmbBank.EditValue;
            SelectedItem.CurrencyId = (int)_common.cmbCurrency.EditValue;
            SaveStateData();

            InternalSave();
        }

        ControlBankAccount _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlBankAccount
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtAgent = {Text = SelectedItem.Agent.Name},
                                  Workarea = SelectedItem.Workarea
                              };
                SelectedItem.CodeFind = _common.txtCodeFind.Text;
                BindingSource bankBinding = new BindingSource();
                List<Agent> collBank = SelectedItem.Workarea.GetCollection<Agent>().Where(f => (f.IsBank)).ToList();
                bankBinding.DataSource = collBank;
                _common.cmbBank.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbBank.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbBank.Properties.DataSource = bankBinding;
                _common.cmbBank.EditValue = SelectedItem.BankId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewBank, "DEFAULT_AGENTBANKVIEW");


                BindingSource currencyBinding = new BindingSource();
                List<Currency> collCurrency = SelectedItem.Workarea.GetCollection<Currency>().Where(f => (f.IsStateAllow)).ToList();
                currencyBinding.DataSource = collCurrency;
                _common.cmbCurrency.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbCurrency.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCurrency.Properties.DataSource = currencyBinding;
                _common.cmbCurrency.EditValue = SelectedItem.CurrencyId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbCurrency, "DEFAULT_LOOKUPCURRENCY");

                UIHelper.GenerateTooltips<AgentBankAccount>(SelectedItem, _common);
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
