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
    internal sealed class BuildControlBranche : BasePropertyControlIBase<Branche>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlBranche()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
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
            SelectedItem.DatabaseCode = Convert.ToInt32(_common.numDbCode.Value);
            SelectedItem.DatabaseName = _common.txtDbName.Text;
            SelectedItem.ServerName = _common.txtServerName.Text;
            SelectedItem.SortOrder = Convert.ToInt32(_common.numSortOrder.Value);
            SelectedItem.IpAddress = _common.txtIP.Text;
            SelectedItem.Authentication = _common.cmbAuthentication.SelectedIndex;
            SelectedItem.Uid = _common.txtUid.Text;
            SelectedItem.Domain = _common.txtDomain.Text;
            SelectedItem.Password = _common.txtPassword.Text;
            if (_common.dtDateStart.EditValue != null)
                SelectedItem.DateStart = _common.dtDateStart.DateTime;
            else
                SelectedItem.DateStart = (DateTime?) null;

            if (_common.dtDateEnd.EditValue != null)
                SelectedItem.DateEnd = _common.dtDateEnd.DateTime;
            else
                SelectedItem.DateEnd= (DateTime?)null;
            

            SaveStateData();

            InternalSave();
        }

        ControlBranche _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlBranche
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  numDbCode = {Value = SelectedItem.DatabaseCode},
                                  txtDbName = {Text = SelectedItem.DatabaseName},
                                  txtServerName = {Text = SelectedItem.ServerName},
                                  numSortOrder = {Value = SelectedItem.SortOrder},
                                  txtIP = {Text = SelectedItem.IpAddress},
                                  cmbAuthentication = {SelectedIndex = SelectedItem.Authentication},
                                  txtUid = {Text = SelectedItem.Uid},
                                  txtDomain = {Text = SelectedItem.Domain},
                                  txtPassword = {Text = SelectedItem.Password},
                                  Workarea = SelectedItem.Workarea
                              };

                _common.dtDateStart.EditValue = SelectedItem.DateStart;
                _common.dtDateEnd.EditValue = SelectedItem.DateEnd;
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
                UIHelper.GenerateTooltips<Branche>(SelectedItem, _common);
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