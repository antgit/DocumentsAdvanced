using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraLayout.Utils;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlColumn : BasePropertyControlIBase<CustomViewColumn>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlColumn()
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
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.Format = _common.txtFormat.Text;
            SelectedItem.DataProperty = _common.txtDataProperty.Text;
            SelectedItem.DataType = _common.cmbDataType.Text;
            SelectedItem.With = (int)_common.numWith.Value;
            SelectedItem.Visible = _common.chkVisible.Checked;
            SelectedItem.OrderNo = (Int16)_common.numOrderNo.Value;
            SelectedItem.Frozen = _common.chkFrozen.Checked;
            SelectedItem.EditAble = _common.chkEditAble.Checked;
            SelectedItem.AutoSizeMode = _common.cmbAutosize.SelectedIndex;
            SelectedItem.DisplayHeader = _common.chkDisplayHeader.Checked;
            SelectedItem.GroupIndex = (int)_common.numGroupIndex.Value;
            SelectedItem.Formula = _common.txtFormula.Text;
            if (_common.cmbAlignment.SelectedIndex == 1)
                SelectedItem.Alignment = 16;
            else if (_common.cmbAlignment.SelectedIndex == 2)
                SelectedItem.Alignment = 32;
            else if (_common.cmbAlignment.SelectedIndex == 3)
                SelectedItem.Alignment = 64;
            else if (_common.cmbAlignment.SelectedIndex == 0)
                SelectedItem.Alignment = 0;

            SaveStateData();

            InternalSave();
        }

        ControlColumn _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlColumn
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
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
                //if (SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = true;
                //    _common.LayoutControl.RegisterUserCustomizatonForm(typeof(FormCustomLayout));
                //}
                //else
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = false;
                //}
                _common.cmbAutosize.Properties.Items.AddRange(new object[] {
                                                                               "Не установлен",
                                                                               "Без авторазмера",
                                                                               "По ширине заголовка",
                                                                               "По ширине ячеек без учета ширины заголовков",
                                                                               "По ширине ячеек с учетом заголовков",
                                                                               "Видимые ячейки без учета заголовков",
                                                                               "Видимые ячейки",
                                                                               "Заполнение"});
                _common.cmbAlignment.Properties.Items.AddRange(new[] { "По умолчанию", "Влево", "По центру", "Вправо" });
                _common.txtName.Text = SelectedItem.Name;
                _common.txtNameFull2.Text = SelectedItem.NameFull;
                _common.txtCode.Text = SelectedItem.Code;
                _common.txtCodeFind.Text = SelectedItem.CodeFind;
                _common.txtMemo.Text = SelectedItem.Memo;

                _common.txtFormat.Text = SelectedItem.Format;
                _common.txtDataProperty.Text = SelectedItem.DataProperty;
                _common.cmbDataType.Text = SelectedItem.DataType;
                _common.numWith.Value = SelectedItem.With;
                _common.chkVisible.Checked = SelectedItem.Visible;
                _common.numOrderNo.Value = SelectedItem.OrderNo;
                _common.chkFrozen.Checked = SelectedItem.Frozen;
                _common.chkEditAble.Checked = SelectedItem.EditAble;
                _common.cmbAutosize.SelectedIndex = SelectedItem.AutoSizeMode;
                _common.chkDisplayHeader.Checked = SelectedItem.DisplayHeader;
                _common.numGroupIndex.Value = SelectedItem.GroupIndex;
                ///// <remarks>"По умолчанию" = 0, "Влево" = 16, "По центру" = 32, "Вправо" = 64</remarks>
                if (SelectedItem.Alignment == 16)
                    _common.cmbAlignment.SelectedIndex = 1;
                else if (SelectedItem.Alignment == 32)
                    _common.cmbAlignment.SelectedIndex = 2;
                else if (SelectedItem.Alignment == 64)
                    _common.cmbAlignment.SelectedIndex = 3;
                foreach (string t in Enum.GetNames(typeof(TypeCode)))
                {
                    _common.cmbDataType.Properties.Items.Add("System." + t);
                }

                if(SelectedItem.KindValue==1)
                {
                    _common.layoutControlItemFormula.Visibility = LayoutVisibility.Never;
                }
                _common.txtFormula.Text = SelectedItem.Formula;

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