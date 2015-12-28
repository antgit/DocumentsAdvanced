using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using BusinessObjects.Documents;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlDocument : BasePropertyControlIBase<Document>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDocument()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SaveStateData();
        }

        ControlDocument _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlDocument
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  Enabled = false,
                                  deDocDate = {EditValue = SelectedItem.Date},
                                  txtNumber = {Text = SelectedItem.Number},
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  seSumm = {Value = SelectedItem.Summa},
                                  Workarea = SelectedItem.Workarea
                              };

                BindingSource bindingAgentFrom = new BindingSource {SelectedItem.AgentFrom};
                _common.cmbAgentFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbAgentFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbAgentFrom.Properties.DataSource = bindingAgentFrom;
                _common.cmbAgentFrom.EditValue = SelectedItem.AgentFromId;

                BindingSource bindingAgentTo = new BindingSource {SelectedItem.AgentTo};
                _common.cmbAgentTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbAgentTo.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbAgentTo.Properties.DataSource = bindingAgentTo;
                _common.cmbAgentTo.EditValue = SelectedItem.AgentToId;

                BindingSource bindingDepartmentFrom = new BindingSource {SelectedItem.AgentDepartmentFrom};
                _common.cmbDepartmentFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbDepartmentFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbDepartmentFrom.Properties.DataSource = bindingDepartmentFrom;
                _common.cmbDepartmentFrom.EditValue = SelectedItem.AgentDepartmentFromId;

                BindingSource bindingDepartmentTo = new BindingSource {SelectedItem.AgentDepartmentTo};
                _common.cmbDepartmentTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbDepartmentTo.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbDepartmentTo.Properties.DataSource = bindingDepartmentTo;
                _common.cmbDepartmentTo.EditValue = SelectedItem.AgentDepartmentToId;

                BindingSource bindingForms = new BindingSource {SelectedItem.ProjectItem};
                _common.cmbForm.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbForm.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbForm.Properties.DataSource = bindingForms;
                _common.cmbForm.EditValue = SelectedItem.ProjectItemId;

                BindingSource bindingFolders = new BindingSource {SelectedItem.Folder};
                _common.cmbFolder.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbFolder.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbFolder.Properties.DataSource = bindingFolders;
                _common.cmbFolder.EditValue = SelectedItem.FolderId;

                BindingSource bindingCurrency = new BindingSource {SelectedItem.Currency};
                _common.cmbCurrency.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCurrency.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbCurrency.Properties.DataSource = bindingCurrency;
                _common.cmbCurrency.EditValue = SelectedItem.CurrencyId;

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}
