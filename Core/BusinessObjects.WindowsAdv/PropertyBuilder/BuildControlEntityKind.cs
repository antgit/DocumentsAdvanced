using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlEntityKind : BasePropertyControlICore<EntityKind>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlEntityKind()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_COMMON_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.SubKind = (short)_common.numIntCode.Value;
            //SaveStateData();

            InternalSave();
        }

        ControlFlagValue _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlFlagValue
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  numIntCode = {Value = SelectedItem.SubKind},
                                  Workarea = SelectedItem.Workarea
                              };

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}