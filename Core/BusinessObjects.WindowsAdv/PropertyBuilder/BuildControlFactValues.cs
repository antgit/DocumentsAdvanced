using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlFactValues : BasePropertyControlICore<FactValue>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlFactValues()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            //TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem = _currentFactValue;
            SelectedItem.Memo = _common.FactPropertyValue.txtMemo.Text;
            SelectedItem.Code = _common.FactPropertyValue.txtCode.Text;
            SelectedItem.FactDate.ActualDate = _common.FactPropertyValue.dtActualDate.DateTime;
            if (SelectedItem.FactDate.FactColumn.AllowValueDate)
                SelectedItem.ValueDate = (DateTime?)_common.FactPropertyValue.dtValueDate.EditValue;
            else
                SelectedItem.ValueDate = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueDecimal)
                SelectedItem.ValueDecimal = (float?)_common.FactPropertyValue.txtValueDecimal.EditValue;
            else
                SelectedItem.ValueDecimal = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueGuid)
                SelectedItem.ValueGuid = (Guid?)_common.FactPropertyValue.txtValueGuid.EditValue;
            else
                SelectedItem.ValueGuid = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueInt)
                SelectedItem.ValueInt = (int?)_common.FactPropertyValue.txtValueInt.EditValue;
            else
                SelectedItem.ValueInt = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueMoney)
                SelectedItem.ValueMoney = (decimal?)_common.FactPropertyValue.txtValueMoney.EditValue;
            else
                SelectedItem.ValueMoney = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueXml)
                SelectedItem.ValueXml = (string)_common.FactPropertyValue.txtValueXml.EditValue;
            else
                SelectedItem.ValueXml = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueString)
                SelectedItem.ValueString = (string)_common.FactPropertyValue.txtValueString.EditValue;
            else
                SelectedItem.ValueString = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueBit)
                SelectedItem.ValueBit = _common.FactPropertyValue.chkValueBit.Checked;
            else
                SelectedItem.ValueBit = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueRef1)
                SelectedItem.ValueRef1 = (int?)_common.FactPropertyValue.lookUpEditRef1.EditValue;
            else
                SelectedItem.ValueRef1 = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueRef2)
                SelectedItem.ValueRef2 = (int?)_common.FactPropertyValue.lookUpEditRef2.EditValue;
            else
                SelectedItem.ValueRef2 = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueRef3)
                SelectedItem.ValueRef3 = (int?)_common.FactPropertyValue.lookUpEditRef3.EditValue;
            else
                SelectedItem.ValueRef3 = null;

            InternalSave();

        }
        public override void InternalSave()
        {
            try
            {
                CanClose = true;
                if (SelectedItem.ValidateRuleSet())
                {
                    SelectedItem.FactDate.Save();
                    if (SelectedItem.ValidateRuleSet())
                        SelectedItem.Save();
                    else
                        SelectedItem.ShowDialogValidationErrors();
                }
                else
                    SelectedItem.ShowDialogValidationErrors();

            }
            catch (DatabaseException dbe)
            {
                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                                       SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                       SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                CanClose = false;
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
                                                           SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                CanClose = false;
            }
        }

        ControlFactValues _common;
        private FactValue _currentFactValue;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlFactValues
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  FactPropertyValue =
                                      {
                                          txtCode = { Text = SelectedItem.Code },
                                          txtMemo = { Text = SelectedItem.Memo },
                                          dtActualDate = { DateTime = SelectedItem.ActualDate },
                                          dtValueDate = { EditValue = SelectedItem.ValueDate },
                                          txtValueDecimal = { EditValue = SelectedItem.ValueDecimal },
                                          txtValueGuid = { EditValue = SelectedItem.ValueGuid },
                                          txtValueInt = { EditValue = SelectedItem.ValueInt },
                                          txtValueMoney = { EditValue = SelectedItem.ValueMoney },
                                          txtValueXml = { EditValue = SelectedItem.ValueXml },
                                          txtValueString = { EditValue = SelectedItem.ValueString },
                                          chkValueBit = { Checked = SelectedItem.ValueBit.HasValue && SelectedItem.ValueBit.Value },
                                          lookUpEditRef1 = { EditValue = SelectedItem.ValueRef1 },
                                          lookUpEditRef2 = { EditValue = SelectedItem.ValueRef2 },
                                          lookUpEditRef3 = { EditValue = SelectedItem.ValueRef3 },
                                          Workarea = SelectedItem.Workarea
                                      }
                              };
                _currentFactValue = SelectedItem;

                #region Ribbon buttons
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();
                BarButtonItem btnNew = new BarButtonItem()
                                           {
                                               Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                               RibbonStyle = RibbonItemStyles.Large,
                                               Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.NEW_X32)
                                           };
                btnNew.ItemClick += delegate
                                        {
                                            FactDate factDate = SelectedItem.Workarea.Empty<FactDate>();
                                            factDate.Workarea = SelectedItem.Workarea;
                                            factDate.ActualDate = DateTime.Now;
                                            factDate.ColumnId = SelectedItem.FactDate.ColumnId;
                                            factDate.ElementId = SelectedItem.FactDate.ElementId;
                                            factDate.ToEntityId = SelectedItem.FactDate.ToEntityId;
                                            factDate.Save();
                                            //Обновление списка дат
                                            _common.ControlDateList.Grid.DataSource = SelectedItem.FactDate.FactColumn.FactName.GetCollectionFactDates(SelectedItem.FactDate.ElementId, SelectedItem.FactDate.ToEntityId, SelectedItem.FactDate.FactColumn.Id, (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue, (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                                        };
                groupLinksAction.ItemLinks.Add(btnNew);
                frmProp.ribbon.Pages[0].Groups.Add(groupLinksAction);
                #endregion

                if (SelectedItem.FactDateId!=0)
                    _common.ControlDateList.Grid.DataSource = SelectedItem.FactDate.FactColumn.FactName.GetCollectionFactDates(SelectedItem.FactDate.ElementId, SelectedItem.FactDate.ToEntityId, SelectedItem.FactDate.FactColumn.Id, (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue, (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                else
                {
                    _common.ControlDateList.Grid.DataSource = SelectedItem.FactColumn.FactName.GetCollectionFactDates(SelectedItem.ElementId, SelectedItem.ToEntityId, SelectedItem.ColumnId, (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue, (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue);
                }
                _common.ControlDateList.View.Columns.Clear();
                _common.ControlDateList.View.Columns.Add(new GridColumn()
                                                             {
                                                                 Name = "colDate",
                                                                 Caption = "Дата",
                                                                 FieldName = "ActualDate",
                                                                 Width = 17,
                                                                 Visible = true,
                                                                 VisibleIndex = 1
                                                             });
                _common.ControlDateList.View.FocusedRowChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
                                                                      {
                                                                          var currentDate = (FactDate)_common.ControlDateList.View.GetRow(e.FocusedRowHandle);
                                                                          List<FactValue> factValues = currentDate.GetFactValues(currentDate.ColumnId, currentDate.ElementId, currentDate.ToEntityId);
                                                                          if (factValues.Count > 0)
                                                                          {
                                                                              _common.FactPropertyValue.Enabled = true;
                                                                              _currentFactValue = factValues[0];

                                                                              _common.FactPropertyValue.txtCode.Text = _currentFactValue.Code;
                                                                              _common.FactPropertyValue.txtMemo.Text = _currentFactValue.Memo;
                                                                              _common.FactPropertyValue.dtActualDate.DateTime = _currentFactValue.ActualDate;
                                                                              _common.FactPropertyValue.txtValueDecimal.EditValue = _currentFactValue.ValueDecimal;
                                                                              _common.FactPropertyValue.txtValueGuid.EditValue = _currentFactValue.ValueGuid;
                                                                              _common.FactPropertyValue.txtValueInt.EditValue = _currentFactValue.ValueInt;
                                                                              _common.FactPropertyValue.txtValueMoney.EditValue = _currentFactValue.ValueMoney;
                                                                              _common.FactPropertyValue.txtValueXml.EditValue = _currentFactValue.ValueXml;
                                                                              _common.FactPropertyValue.txtValueString.EditValue = _currentFactValue.ValueString;
                                                                              _common.FactPropertyValue.chkValueBit.EditValue = _currentFactValue.ValueBit.HasValue && _currentFactValue.ValueBit.Value;
                                                                              _common.FactPropertyValue.lookUpEditRef1.EditValue = _currentFactValue.ValueRef1;
                                                                              _common.FactPropertyValue.lookUpEditRef2.EditValue = _currentFactValue.ValueRef2;
                                                                              _common.FactPropertyValue.lookUpEditRef3.EditValue = _currentFactValue.ValueRef3;

                                                                              SetVisibility();
                                                                          }
                                                                          else
                                                                          {
                                                                              _common.FactPropertyValue.Enabled = false;
                                                                          }
                                                                      };
                _common.ControlDateList.View.DoubleClick += delegate (object sender, EventArgs e)
                                                                {
                                                                    var currentDate = (FactDate)_common.ControlDateList.View.GetRow(_common.ControlDateList.View.FocusedRowHandle);
                                                                    XtraMessageBox.Show(currentDate.ActualDate.ToString());
                                                                };

                _common.FactPropertyValue.dtValueDate.KeyDown += DtValueDateKeyDown;
                _common.FactPropertyValue.txtValueDecimal.KeyDown += TxtValueDecimalKeyDown;
                _common.FactPropertyValue.txtValueGuid.KeyDown += TxtValueGuidKeyDown;
                _common.FactPropertyValue.txtValueInt.KeyDown += TxtValueIntKeyDown;
                _common.FactPropertyValue.txtValueMoney.KeyDown += TxtValueMoneyKeyDown;
                _common.FactPropertyValue.txtValueXml.KeyDown += TxtValueXmlKeyDown;
                _common.FactPropertyValue.txtValueString.KeyDown += TxtVlaueStringKeyDown;

                #region Ссылки
                if (SelectedItem.FactColumn.AllowValueRef1)
                {
                    _common.FactPropertyValue.lookUpEditRef1.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.FactPropertyValue.lookUpEditRef1.Properties.ValueMember = GlobalPropertyNames.Id;
                    List<object> coll = new List<object>();
                    coll.Add(SelectedItem.Workarea.Cashe.GetCasheItem(SelectedItem.FactColumn.ReferenceType ?? 0, SelectedItem.ValueRef1 ?? 0));
                    _common.FactPropertyValue.lookUpEditRef1.Properties.DataSource = coll;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.FactPropertyValue.lookUpEditRef1, "DEFAULT_LOOKUP");
                    _common.FactPropertyValue.lookUpEditRef1.KeyDown += delegate(object sender, KeyEventArgs e)
                                                                            {
                                                                                if (e.KeyCode == Keys.Delete)
                                                                                    ((LookUpEdit)sender).EditValue = 0;
                                                                            };
                    _common.FactPropertyValue.lookUpEditRef1.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                                               {
                                                                                   LookUpEdit cmb = sender as LookUpEdit;
                                                                                   _common.Cursor = Cursors.WaitCursor;
                                                                                   cmb.Properties.DataSource = SelectedItem.Workarea.GetCollection(SelectedItem.FactDate.FactColumn.ReferenceType ?? 0);
                                                                                   _common.Cursor = Cursors.Default;
                                                                               };
                }

                if (SelectedItem.FactColumn.AllowValueRef2)
                {
                    _common.FactPropertyValue.lookUpEditRef2.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.FactPropertyValue.lookUpEditRef2.Properties.ValueMember = GlobalPropertyNames.Id;
                    List<object> coll = new List<object>();
                    coll.Add(SelectedItem.Workarea.Cashe.GetCasheItem(SelectedItem.FactColumn.ReferenceType2 ?? 0, SelectedItem.ValueRef2 ?? 0));
                    _common.FactPropertyValue.lookUpEditRef2.Properties.DataSource = coll;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.FactPropertyValue.lookUpEditRef2, "DEFAULT_LOOKUP");
                    _common.FactPropertyValue.lookUpEditRef2.KeyDown += delegate(object sender, KeyEventArgs e)
                                                                            {
                                                                                if (e.KeyCode == Keys.Delete)
                                                                                    ((LookUpEdit)sender).EditValue = 0;
                                                                            };
                    _common.FactPropertyValue.lookUpEditRef1.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                                               {
                                                                                   LookUpEdit cmb = sender as LookUpEdit;
                                                                                   _common.Cursor = Cursors.WaitCursor;
                                                                                   cmb.Properties.DataSource = SelectedItem.Workarea.GetCollection(SelectedItem.FactDate.FactColumn.ReferenceType2 ?? 0);
                                                                                   _common.Cursor = Cursors.Default;
                                                                               };
                }

                if (SelectedItem.FactColumn.AllowValueRef3)
                {
                    _common.FactPropertyValue.lookUpEditRef3.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.FactPropertyValue.lookUpEditRef3.Properties.ValueMember = GlobalPropertyNames.Id;
                    List<object> coll = new List<object>();
                    coll.Add(SelectedItem.Workarea.Cashe.GetCasheItem(SelectedItem.FactDate.FactColumn.ReferenceType3 ?? 0, SelectedItem.ValueRef3 ?? 0));
                    _common.FactPropertyValue.lookUpEditRef3.Properties.DataSource = coll;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.FactPropertyValue.lookUpEditRef3, "DEFAULT_LOOKUP");
                    _common.FactPropertyValue.lookUpEditRef3.KeyDown += delegate(object sender, KeyEventArgs e)
                                                                            {
                                                                                if (e.KeyCode == Keys.Delete)
                                                                                    ((LookUpEdit)sender).EditValue = 0;
                                                                            };
                    _common.FactPropertyValue.lookUpEditRef3.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                                               {
                                                                                   LookUpEdit cmb = sender as LookUpEdit;
                                                                                   _common.Cursor = Cursors.WaitCursor;
                                                                                   cmb.Properties.DataSource = SelectedItem.Workarea.GetCollection(SelectedItem.FactColumn.ReferenceType3 ?? 0);
                                                                                   _common.Cursor = Cursors.Default;
                                                                               };
                }
                #endregion


                SetVisibility();

                //DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbToEntityId, "DEFAULT_LOOKUP_NAME");
                //_common.cmbToEntityId.Properties.DataSource = SelectedItem.Workarea.CollectionDbEntities;
                //_common.cmbToEntityId.Properties.DisplayMember = "Name";
                //_common.cmbToEntityId.Properties.ValueMember = GlobalPropertyNames.Id;

                //if (!SelectedItem.IsNew)
                //{
                //    _common.cmbToEntityId.Properties.ReadOnly = true;
                //    _common.cmbToEntityId.EditValue = SelectedItem.ToEntityId;
                //}
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }

        /// <summary>
        /// Естановка значений видимости элементов формы в зависимости от значений флагов текущего объекта класса FactValue
        /// </summary>
        private void SetVisibility()
        {
            if (!_currentFactValue.FactColumn.AllowValueBit)
                _common.FactPropertyValue.layoutControlItemValueBit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueDecimal)
                _common.FactPropertyValue.layoutControlItemValueFloat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueGuid)
                _common.FactPropertyValue.layoutControlItemValueGuid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueInt)
                _common.FactPropertyValue.layoutControlItemValueInt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueMoney)
                _common.FactPropertyValue.layoutControlItemValueMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueXml)
                _common.FactPropertyValue.layoutControlItemValueXml.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueString)
                _common.FactPropertyValue.layoutControlItemValueString.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueBinary)
                _common.FactPropertyValue.layoutControlItemValueBinary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueDate)
                _common.FactPropertyValue.layoutControlItemValueDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueRef1)
                _common.FactPropertyValue.layoutControlItemValueRef1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueRef2)
                _common.FactPropertyValue.layoutControlItemValueRef2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (!_currentFactValue.FactColumn.AllowValueRef3)
                _common.FactPropertyValue.layoutControlItemValueRef3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        void TxtVlaueStringKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.FactPropertyValue.txtValueString.EditValue = null;
        }

        void TxtValueXmlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.FactPropertyValue.txtValueXml.EditValue = null;
        }

        void TxtValueMoneyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.FactPropertyValue.txtValueMoney.EditValue = null;
        }

        void TxtValueIntKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.FactPropertyValue.txtValueInt.EditValue = null;
        }

        void TxtValueGuidKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.FactPropertyValue.txtValueGuid.EditValue = null;
        }

        void TxtValueDecimalKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.FactPropertyValue.txtValueDecimal.EditValue = null;
        }

        void DtValueDateKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.FactPropertyValue.dtValueDate.EditValue = null;
        }
    }
}