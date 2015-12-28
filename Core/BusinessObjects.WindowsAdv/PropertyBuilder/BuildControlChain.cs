using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlChain<T> : BasePropertyControlICore<Chain<T>> where T : class, IBase, new()
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlChain()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }

        public Predicate<T> Filter;
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            //SelectedItem.Name = _common.txtName.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.StateId = _common.chkEnabled.Checked ? State.STATEACTIVE : State.STATENOTDONE;
            SelectedItem.OrderNo = (int)_common.numSortorder.Value;
            SelectedItem.RightId = (int)_common.cmbRight.EditValue;
            SelectedItem.Code = _common.txtCode.Text;

            InternalSave();
        }

        ControlChain _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlChain
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Left.Name},
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };
                _common.txtName.Properties.ReadOnly = true;
                _common.txtCode.Text = SelectedItem.Code;
                _common.txtChainKind.Text = SelectedItem.Kind.Name;
                _common.numSortorder.Value = SelectedItem.OrderNo;
                
                _common.chkEnabled.Checked = SelectedItem.StateId== State.STATEACTIVE;

                // TODO: 
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbRight, "DEFAULT_LOOKUP");
                BindingSource bindingSourceRight = new BindingSource
                                                       {
                                                           DataSource =
                                                               Filter != null
                                                                   ? SelectedItem.Workarea.GetCollection<T>().FindAll(
                                                                       Filter).ToList()
                                                                   : SelectedItem.Workarea.GetCollection<T>()
                                                       };

                List<T> coll = (bindingSourceRight.DataSource as List<T>);
                if (!coll.Exists(f => f.Id == SelectedItem.RightId))
                {
                    T newVal = SelectedItem.Workarea.Cashe.GetCasheData<T>().Item(SelectedItem.RightId);
                    bindingSourceRight.Add(newVal);
                }
                _common.cmbRight.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbRight.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbRight.Properties.DataSource = bindingSourceRight;
                _common.cmbRight.EditValue = SelectedItem.RightId;
                _common.cmbRight.ButtonClick +=
                    delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                        {
                            if(e.Button.Index==1)
                            {
                                int selId = (int)_common.cmbRight.EditValue;
                                if(selId!=0)
                                {
                                    T val = SelectedItem.Workarea.Cashe.GetCasheData<T>().Item(selId);
                                    val.ShowPropertyType();
                                }
                            }
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

    internal sealed class BuildControlChainAdvanced<TSource, TDestination> : BasePropertyControlICore<ChainAdvanced<TSource, TDestination>>
        where TSource : class, IBase, new()
        where TDestination : class, IBase, new()
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlChainAdvanced()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }

        public Predicate<TDestination> Filter;
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            //SelectedItem.Name = _common.txtName.Text;
            //SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.StateId = _common.chkEnabled.Checked ? State.STATEACTIVE : State.STATENOTDONE;
            SelectedItem.OrderNo = (int)_common.numSortorder.Value;
            SelectedItem.RightId = (int)_common.cmbRight.EditValue;
            SelectedItem.Code = _common.txtCode.Text;

            InternalSave();
        }

        ControlChain _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlChain
                {
                    Name = ExtentionString.CONTROL_COMMON_NAME,
                    txtName = { Text = SelectedItem.Left.Name },
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
                _common.txtName.Properties.ReadOnly = true;
                _common.txtCode.Text = SelectedItem.Code;
                _common.txtChainKind.Text = SelectedItem.Kind.Name;
                //_common.txtCode.Text = SelectedItem.Kind.Name;
                //_common.txtCode.Properties.ReadOnly = true;
                _common.numSortorder.Value = SelectedItem.OrderNo;

                _common.chkEnabled.Checked = SelectedItem.StateId == State.STATEACTIVE;

                // TODO: 
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbRight, "DEFAULT_LOOKUP");
                BindingSource bindingSourceRight = new BindingSource
                {
                    DataSource =
                        Filter != null
                            ? SelectedItem.Workarea.GetCollection<TDestination>().FindAll(
                                Filter).ToList()
                            : SelectedItem.Workarea.GetCollection<TDestination>()
                };

                List<TDestination> coll = (bindingSourceRight.DataSource as List<TDestination>);
                if (!coll.Exists(f => f.Id == SelectedItem.RightId))
                {
                    TDestination newVal = SelectedItem.Workarea.Cashe.GetCasheData<TDestination>().Item(SelectedItem.RightId);
                    bindingSourceRight.Add(newVal);
                }
                _common.cmbRight.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbRight.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbRight.Properties.DataSource = bindingSourceRight;
                _common.cmbRight.EditValue = SelectedItem.RightId;

                // TODO: 
                //_common.txtCode.Text = SelectedItem.Code;
                //_common.txtMemo.Text = SelectedItem.Memo;

                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}