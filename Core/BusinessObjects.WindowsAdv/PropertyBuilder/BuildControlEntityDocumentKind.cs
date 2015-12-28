using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlEntityDocumentKind : BasePropertyControlICore<EntityDocumentKind>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlEntityDocumentKind()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.SubKind = (short)_common.numIntCode.Value;
            SelectedItem.AgentFirstFilterId = _common.cmbAgentFirstFilterId.SelectedIndex;
            SelectedItem.AgentSecondFilterId = _common.cmbAgentSecondFilterId.SelectedIndex;
            SelectedItem.AgentThirdFilterId = _common.cmbAgentThirdFilterId.SelectedIndex;
            SelectedItem.AgentFourthFilterId = _common.cmbAgentFourthFilterId.SelectedIndex;
            SelectedItem.UseCustomFilter = _common.chkUseCustomFilter.Checked;
            SelectedItem.CorrespondenceId = _common.cmbCorrespondenceId.SelectedIndex;

            InternalSave();
        }

        ControlDocumentKind _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlDocumentKind
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  numIntCode = {Value = SelectedItem.SubKind},
                                  Workarea = SelectedItem.Workarea
                              };
                _common.cmbCorrespondenceId.SelectedIndex = SelectedItem.CorrespondenceId;
                _common.chkUseCustomFilter.Checked = SelectedItem.UseCustomFilter;
                _common.cmbAgentFirstFilterId.SelectedIndex = SelectedItem.AgentFirstFilterId;
                _common.cmbAgentSecondFilterId.SelectedIndex = SelectedItem.AgentSecondFilterId;
                _common.cmbAgentThirdFilterId.SelectedIndex = SelectedItem.AgentThirdFilterId;
                _common.cmbAgentFourthFilterId.SelectedIndex = SelectedItem.AgentFourthFilterId;

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}