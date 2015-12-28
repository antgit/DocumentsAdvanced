using System;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlUserRightCommon : BasePropertyControlICore<Security.UserRightCommon>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlUserRightCommon()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            if (_common.cmbValue.SelectedIndex == 2)
                SelectedItem.Value = null;
            else
                SelectedItem.Value = (Int16)_common.cmbValue.SelectedIndex;
            SelectedItem.StateId = State.STATEACTIVE;

            InternalSave();
        }

        ControlUserRightElement _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlUserRightElement
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  //Workarea = SelectedItem.Workarea
                              };
                if (!SelectedItem.Value.HasValue)
                    _common.cmbValue.SelectedIndex = 2;
                else if (SelectedItem.Value.Value == 0)
                    _common.cmbValue.SelectedIndex = 0;
                else if (SelectedItem.Value.Value == 1)
                    _common.cmbValue.SelectedIndex = 1;

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