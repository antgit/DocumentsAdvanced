using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Developer;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlTableInfo : BasePropertyControlIBase<DbObject>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlTableInfo()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Schema = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Name = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.ProcedureImport = _common.cmbProcedureImport.Text;
            SelectedItem.ProcedureExport = _common.cmbProcedureExport.Text;

            SaveStateData();
            InternalSave();
        }
        ControlTableInfo _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlTableInfo
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Schema},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Name},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  cmbProcedureExport = {Text = SelectedItem.ProcedureExport},
                                  Workarea = SelectedItem.Workarea
                              };
                
                List<string> procExport = SelectedItem.Workarea.GetCollectionProcedures("Export", true);
                _common.cmbProcedureExport.Properties.Items.AddRange(procExport);
                _common.cmbProcedureImport.Text = SelectedItem.ProcedureImport;
                _common.cmbProcedureImport.Properties.Items.AddRange(procExport);

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