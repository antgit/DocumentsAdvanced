using System;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlCodeValue<T> : BasePropertyControlICore<CodeValue<T>> where T : class, IBase, new()
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlCodeValue()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }

        public Predicate<T> Filter;
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Value = _common.txtValue.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.OrderNo = (int)_common.edOrderNo.Value;
            InternalSave();
        }

        ControlCodeValue _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlCodeValue
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.CodeName.Name},
                                  txtCode = { Text = SelectedItem.CodeName.Code },
                                  txtValue = { Text = SelectedItem.Value },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  edOrderNo = {Value = SelectedItem.OrderNo},
                                  Workarea = SelectedItem.Workarea
                              };

                
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