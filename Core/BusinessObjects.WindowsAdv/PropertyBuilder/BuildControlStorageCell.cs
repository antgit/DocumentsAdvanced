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
    internal sealed class BuildControlStorageCell : BasePropertyControlIBase<StorageCell>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlStorageCell()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
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
            SelectedItem.Qty = (decimal)_common.calcQty.EditValue;
            SelectedItem.StoreId = (int)_common.cmbStore.EditValue;
            SelectedItem.UnitId = (int)_common.cmbUnits.EditValue;

            SaveStateData();

            InternalSave();
        }

        ControlStorageCell _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlStorageCell
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };
                BindingSource bindingSourceUnit = new BindingSource();
                List<Unit> collUnits = SelectedItem.Workarea.GetCollection<Unit>();
                bindingSourceUnit.DataSource = collUnits;
                _common.cmbUnits.Properties.DataSource = bindingSourceUnit;
                _common.cmbUnits.Properties.DisplayMember = "Name";
                _common.cmbUnits.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbUnits.EditValue = SelectedItem.UnitId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbUnits, "DEFAULT_LOOKUPUNIT");

                BindingSource bindingSourceStore = new BindingSource();
                List<Agent> collManufacture = SelectedItem.Workarea.GetCollection<Agent>();
                bindingSourceStore.DataSource = collManufacture;
                _common.cmbStore.Properties.DataSource = bindingSourceStore;
                _common.cmbStore.Properties.DisplayMember = "Name";
                _common.cmbStore.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbStore.EditValue = SelectedItem.StoreId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbStore, "DEFAULT_LOOKUPAGENT");

                _common.calcQty.EditValue = SelectedItem.Qty;


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