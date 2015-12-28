using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств партии товара
    /// </summary>
    internal sealed class BuildControlSeries : BasePropertyControlIBase<Series>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlSeries()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
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
            if (_common.dtOut.EditValue != null)
                SelectedItem.DateOut = _common.dtOut.DateTime;
            else
                SelectedItem.DateOut = null;

            if (_common.dtOn.EditValue != null)
                SelectedItem.DateOn = _common.dtOn.DateTime;
            else
                SelectedItem.DateOn = null;

            SaveStateData();

            InternalSave();
        }

        ControlSeries _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlSeries
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtNumber = {Text = SelectedItem.Number},
                                  txtProduct = {Text = SelectedItem.Product.Name},
                                  btnDoc = {Text = SelectedItem.Document.ToString()},
                                  Workarea = SelectedItem.Workarea
                              };
                
                if(SelectedItem.DateOut.HasValue)
                {
                    _common.dtOut.EditValue = SelectedItem.DateOut;
                }
                if (SelectedItem.DateOn.HasValue)
                {
                    _common.dtOn.EditValue = SelectedItem.DateOn;
                }

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