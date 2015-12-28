using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Developer;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlColumnInfo : BasePropertyControlIBase<DbObjectChild>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlColumnInfo()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Name = _common.txtColumnName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.OrderNo = (Int16)_common.numOrderNo.Value;
            SelectedItem.GroupNo = (Int16)_common.numGroup.Value;
            SelectedItem.IsFormula = _common.chkFormula.Checked;
            SelectedItem.TypeNameSql = _common.cmbTypeNameSql.Text;
            SelectedItem.TypeNameNet = _common.cmbTypeNameNet.Text;
            SelectedItem.TypeLen = _common.txtTypeLen.Text;
            SelectedItem.AllowNull = _common.chkAllowNull.Checked;

            SaveStateData();
            InternalSave();
        }
        ControlTableColumn _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlTableColumn
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Owner.Schema},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  Workarea = SelectedItem.Workarea
                              };
                //#region Настройка расположения элементов управления
                //// Поиск данных о настройке
                //string controlName = string.Empty;
                //string entityKind = string.Empty;
                //string keyValue = _common.Tag != null ? _common.Tag.ToString() : _common.GetType().Name;

                //if (!string.IsNullOrWhiteSpace((Owner as IWorkareaForm).Key))
                //    controlName = (Owner as IWorkareaForm).Key;

                //entityKind = SelectedItem.KindId.ToString();

                //// Общие поисковые данные
                //List<XmlStorage> collSet = SelectedItem.Workarea.Empty<XmlStorage>().FindBy(kindId: 2359299,
                //                                                                            name: controlName,
                //                                                                            code: keyValue,
                //                                                                            flagString: entityKind);
                //if (collSet.Count > 0)
                //{
                //    // Уточняющий подзапрос
                //    XmlStorage setiings = collSet.FirstOrDefault(f => f.KindId == 2359299
                //                                                                && f.Name == controlName
                //                                                                && f.Code == keyValue
                //                                                                && f.FlagString == entityKind);
                //    if (setiings != null && !string.IsNullOrWhiteSpace(setiings.XmlData))
                //    {
                //        MemoryStream s = new MemoryStream();
                //        StreamWriter w = new StreamWriter(s) { AutoFlush = true };
                //        w.Write(setiings.XmlData);
                //        s.Position = 0;
                //        try
                //        {
                //            _common.LayoutControl.RestoreLayoutFromStream(s);
                //        }
                //        catch (Exception)
                //        {

                //        }
                //    }
                //}
                ////
                //if (SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = true;
                //    _common.LayoutControl.RegisterUserCustomizatonForm(typeof(FormCustomLayout));
                //}
                //else
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = false;
                //}
                //#endregion
                
                _common.txtName.Properties.ReadOnly = true;
                _common.txtCode.Text = SelectedItem.Owner.Name;
                _common.txtCode.Properties.ReadOnly = true;
                _common.txtMemo.Text = SelectedItem.Memo;
                _common.txtColumnName.Text = SelectedItem.Name;
                _common.numOrderNo.Value = SelectedItem.OrderNo;
                _common.numGroup.Value = SelectedItem.GroupNo;
                _common.chkFormula.Checked = SelectedItem.IsFormula;
                _common.cmbTypeNameSql.Text = SelectedItem.TypeNameSql;
                _common.cmbTypeNameNet.Text = SelectedItem.TypeNameNet;
                _common.txtTypeLen.Text = SelectedItem.TypeLen;
                _common.chkAllowNull.Checked = SelectedItem.AllowNull;
                
                foreach (string t in Enum.GetNames(typeof(SqlDbType)))
                    _common.cmbTypeNameSql.Properties.Items.Add(t);
                foreach (string t in Enum.GetNames(typeof(TypeCode)))
                    _common.cmbTypeNameNet.Properties.Items.Add("System." + t);
                _common.cmbTypeNameNet.Properties.Items.Add("System.Guid");
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