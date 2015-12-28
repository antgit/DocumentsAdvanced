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
    internal sealed class BuildControlAccount : BasePropertyControlIBase<Account>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlAccount()
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
            SelectedItem.Turn = _common.chkTurn.Checked;
            SelectedItem.KindValue = (short)_common.cmbSaldo.SelectedIndex;
            SelectedItem.TurnKind = (short)_common.cmbTurnKind.SelectedIndex;
            SaveStateData();

            InternalSave();
        }

        ControlAccount _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlAccount
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea
                              };

                _common.cmbSaldo.Properties.Items.AddRange(Properties.Resources.STR_ACCOUNTSALDO.Split('|'));
                _common.cmbTurnKind.Properties.Items.AddRange(Properties.Resources.STR_ACCOUNTSUBTYPE.Split('|'));
                _common.cmbSaldo.SelectedIndex = SelectedItem.KindValue-1;
                _common.cmbTurnKind.SelectedIndex = SelectedItem.TurnKind;
                _common.chkTurn.Checked = SelectedItem.Turn;

                UIHelper.GenerateTooltips<Account>(SelectedItem, _common);
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