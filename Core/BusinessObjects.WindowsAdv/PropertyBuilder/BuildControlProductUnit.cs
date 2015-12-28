using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlProductUnit : BasePropertyControlIBase<ProductUnit>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlProductUnit()
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
            SelectedItem.Divider = _common.numDivider.Value;
            SelectedItem.Multiplier = _common.numMultiply.Value;
            SelectedItem.SortOrder = (Int32)_common.numSort.Value;
            if (_common.cmbProduct.EditValue != null)
                SelectedItem.ProductId = (int)_common.cmbProduct.EditValue;
            else
                SelectedItem.ProductId = 0;
            if (_common.cmbUnit.EditValue != null)
                SelectedItem.UnitId= (int)_common.cmbUnit.EditValue;
            else
                SelectedItem.UnitId = 0;
            
            SaveStateData();

            InternalSave();
        }

        ControlProductUnit _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlProductUnit
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  numDivider = {Value = SelectedItem.Divider},
                                  numMultiply = {Value = SelectedItem.Multiplier},
                                  numSort = {Value = SelectedItem.SortOrder},
                                  Workarea = SelectedItem.Workarea
                              };
               
                BindingSource bindingSourceProduct = new BindingSource();
                List<Product> colProd = SelectedItem.Workarea.GetCollection<Product>();
                bindingSourceProduct.DataSource = colProd;
                _common.cmbProduct.Properties.DisplayMember = "Name";
                _common.cmbProduct.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbProduct.Properties.DataSource = bindingSourceProduct;
                _common.cmbProduct.EditValue = SelectedItem.ProductId;

                BindingSource bindingSourceUnit = new BindingSource();
                List<Unit> colUnit = SelectedItem.Workarea.GetCollection<Unit>();
                bindingSourceUnit.DataSource = colUnit;
                _common.cmbUnit.Properties.DisplayMember = "Name";
                _common.cmbUnit.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbUnit.Properties.DataSource = bindingSourceUnit;
                _common.cmbUnit.EditValue = SelectedItem.UnitId;

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}
