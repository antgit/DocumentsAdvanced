using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlState : BasePropertyControlICore<State>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlState()
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
            // TODO: 
            //SelectedItem.Code = _common.txtCode.Text;

            InternalSave();
        }

        ControlCommon _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlCommon
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea
                              };
                
                // TODO: 
                //_common.txtCode.Text = SelectedItem.Code;

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