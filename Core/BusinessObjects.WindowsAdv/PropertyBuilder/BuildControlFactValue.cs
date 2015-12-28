using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlFactValue : BasePropertyControlICore<FactValue>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlFactValue()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            //TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.FactDate.ActualDate = _common.dtActualDate.DateTime;
            if (SelectedItem.FactDate.FactColumn.AllowValueDate)
                SelectedItem.ValueDate = (DateTime?)_common.dtValueDate.EditValue;
            else
                SelectedItem.ValueDate = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueDecimal)
                SelectedItem.ValueDecimal = (float?)_common.txtValueDecimal.EditValue;
            else
                SelectedItem.ValueDecimal = null;
            
            if (SelectedItem.FactDate.FactColumn.AllowValueGuid)
                SelectedItem.ValueGuid = (Guid?)_common.txtValueGuid.EditValue;
            else
                SelectedItem.ValueGuid = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueInt)
                SelectedItem.ValueInt = (int?)_common.txtValueInt.EditValue;
            else
                SelectedItem.ValueInt = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueMoney)
                SelectedItem.ValueMoney = (decimal?)_common.txtValueMoney.EditValue;
            else
                SelectedItem.ValueMoney = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueXml)
                SelectedItem.ValueXml = (string)_common.txtValueXml.EditValue;
            else
                SelectedItem.ValueXml = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueString)
                SelectedItem.ValueString = (string)_common.txtValueString.EditValue;
            else
                SelectedItem.ValueString = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueBit)
                SelectedItem.ValueBit = _common.chkValueBit.Checked;
            else
                SelectedItem.ValueBit = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueRef1)
                SelectedItem.ValueRef1 = (int?)_common.lookUpEditRef1.EditValue;
            else
                SelectedItem.ValueRef1 = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueRef2)
                SelectedItem.ValueRef2 = (int?)_common.lookUpEditRef2.EditValue;
            else
                SelectedItem.ValueRef2 = null;

            if (SelectedItem.FactDate.FactColumn.AllowValueRef3)
                SelectedItem.ValueRef3 = (int?)_common.lookUpEditRef3.EditValue;
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

        ControlFactPropertyValue _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlFactPropertyValue
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  dtActualDate = {DateTime = SelectedItem.ActualDate},
                                  dtValueDate = {EditValue = SelectedItem.ValueDate},
                                  txtValueDecimal = {EditValue = SelectedItem.ValueDecimal},
                                  txtValueGuid = {EditValue = SelectedItem.ValueGuid},
                                  txtValueInt = {EditValue = SelectedItem.ValueInt},
                                  txtValueMoney = {EditValue = SelectedItem.ValueMoney},
                                  txtValueXml = {EditValue = SelectedItem.ValueXml},
                                  txtValueString = {EditValue = SelectedItem.ValueString},
                                  chkValueBit =
                                      {Checked = SelectedItem.ValueBit.HasValue && SelectedItem.ValueBit.Value},
                                  lookUpEditRef1 = {EditValue = SelectedItem.ValueRef1},
                                  lookUpEditRef2 = {EditValue = SelectedItem.ValueRef2},
                                  lookUpEditRef3 = {EditValue = SelectedItem.ValueRef3},
                                  Workarea = SelectedItem.Workarea
                                  /*,
                                  Enabled = SelectedItem.FactDate.HasValue*/
                              };

                /*if (!SelectedItem.FactDate.HasValue)
                {
                    frmProp.btnSave.Enabled = false;
                    frmProp.btnSaveClose.Enabled = false;
                }*/

                
                _common.dtValueDate.KeyDown += DtValueDateKeyDown;
                _common.txtValueDecimal.KeyDown += TxtValueDecimalKeyDown;
                _common.txtValueGuid.KeyDown += TxtValueGuidKeyDown;
                _common.txtValueInt.KeyDown +=TxtValueIntKeyDown;
                _common.txtValueMoney.KeyDown +=TxtValueMoneyKeyDown;
                _common.txtValueXml.KeyDown +=TxtValueXmlKeyDown;
                _common.txtValueString.KeyDown += TxtVlaueStringKeyDown;

                #region Ссылки
                if (SelectedItem.FactDate.FactColumn.AllowValueRef1)
                {
                    _common.lookUpEditRef1.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.lookUpEditRef1.Properties.ValueMember = GlobalPropertyNames.Id;
                    List<object> coll = new List<object>();
                    coll.Add(SelectedItem.Workarea.Cashe.GetCasheItem(SelectedItem.FactDate.FactColumn.ReferenceType ?? 0, SelectedItem.ValueRef1 ?? 0));
                    _common.lookUpEditRef1.Properties.DataSource = coll;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.lookUpEditRef1, "DEFAULT_LOOKUP");
                    _common.lookUpEditRef1.KeyDown += delegate(object sender, KeyEventArgs e)
                                                            {
                                                                if (e.KeyCode == Keys.Delete)
                                                                    ((LookUpEdit) sender).EditValue= 0;
                                                            };
                    _common.lookUpEditRef1.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                            {
                                                                LookUpEdit cmb = sender as LookUpEdit;
                                                                _common.Cursor = Cursors.WaitCursor;
                                                                cmb.Properties.DataSource =SelectedItem.Workarea.GetCollection(SelectedItem.FactDate.FactColumn.ReferenceType ??0);
                                                                _common.Cursor = Cursors.Default;
                                                            };
                }

                if (SelectedItem.FactDate.FactColumn.AllowValueRef2)
                {
                    _common.lookUpEditRef2.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.lookUpEditRef2.Properties.ValueMember = GlobalPropertyNames.Id;
                    List<object> coll = new List<object>();
                    coll.Add(SelectedItem.Workarea.Cashe.GetCasheItem(SelectedItem.FactDate.FactColumn.ReferenceType2 ?? 0, SelectedItem.ValueRef2 ?? 0));
                    _common.lookUpEditRef2.Properties.DataSource = coll;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.lookUpEditRef2, "DEFAULT_LOOKUP");
                    _common.lookUpEditRef2.KeyDown += delegate(object sender, KeyEventArgs e)
                                                            {
                                                                if (e.KeyCode == Keys.Delete)
                                                                    ((LookUpEdit)sender).EditValue = 0;
                                                            };
                    _common.lookUpEditRef1.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                            {
                                                                LookUpEdit cmb = sender as LookUpEdit;
                                                                _common.Cursor = Cursors.WaitCursor;
                                                                cmb.Properties.DataSource = SelectedItem.Workarea.GetCollection(SelectedItem.FactDate.FactColumn.ReferenceType2 ?? 0);
                                                                _common.Cursor = Cursors.Default;
                                                            };
                }

                if (SelectedItem.FactDate.FactColumn.AllowValueRef3)
                {
                    _common.lookUpEditRef3.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.lookUpEditRef3.Properties.ValueMember = GlobalPropertyNames.Id;
                    List<object> coll = new List<object>();
                    coll.Add(SelectedItem.Workarea.Cashe.GetCasheItem(SelectedItem.FactDate.FactColumn.ReferenceType3 ?? 0, SelectedItem.ValueRef3 ?? 0));
                    _common.lookUpEditRef3.Properties.DataSource = coll;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.lookUpEditRef3, "DEFAULT_LOOKUP");
                    _common.lookUpEditRef3.KeyDown += delegate(object sender, KeyEventArgs e)
                                                            {
                                                                if (e.KeyCode == Keys.Delete)
                                                                    ((LookUpEdit)sender).EditValue = 0;
                                                            };
                    _common.lookUpEditRef3.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                            {
                                                                LookUpEdit cmb = sender as LookUpEdit;
                                                                _common.Cursor = Cursors.WaitCursor;
                                                                cmb.Properties.DataSource = SelectedItem.Workarea.GetCollection(SelectedItem.FactDate.FactColumn.ReferenceType3 ?? 0);
                                                                _common.Cursor = Cursors.Default;
                                                            };
                }
                #endregion

                if (!SelectedItem.FactDate.FactColumn.AllowValueBit)
                    _common.layoutControlItemValueBit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueDecimal)
                    _common.layoutControlItemValueFloat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueGuid)
                    _common.layoutControlItemValueGuid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueInt)
                    _common.layoutControlItemValueInt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueMoney)
                    _common.layoutControlItemValueMoney.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueXml)
                    _common.layoutControlItemValueXml.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueString)
                    _common.layoutControlItemValueString.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueBinary)
                    _common.layoutControlItemValueBinary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueDate)
                    _common.layoutControlItemValueDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueRef1)
                    _common.layoutControlItemValueRef1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueRef2)
                    _common.layoutControlItemValueRef2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (!SelectedItem.FactDate.FactColumn.AllowValueRef3)
                    _common.layoutControlItemValueRef3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                //DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbToEntityId, "DEFAULT_LOOKUP_NAME");
                //_common.cmbToEntityId.Properties.DataSource = SelectedItem.Workarea.CollectionDbEntities;
                //_common.cmbToEntityId.Properties.DisplayMember = "Name";
                //_common.cmbToEntityId.Properties.ValueMember = GlobalPropertyNames.Id;

                //if (!SelectedItem.IsNew)
                //{
                //    _common.cmbToEntityId.Properties.ReadOnly = true;
                //    _common.cmbToEntityId.EditValue = SelectedItem.ToEntityId;
                //}

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }

        void TxtVlaueStringKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.txtValueString.EditValue = null;
        }

        void TxtValueXmlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.txtValueXml.EditValue = null;
        }

        void TxtValueMoneyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.txtValueMoney.EditValue = null;
        }

        void TxtValueIntKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.txtValueInt.EditValue = null;
        }

        void TxtValueGuidKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.txtValueGuid.EditValue = null;
        }

        void TxtValueDecimalKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D0)
                _common.txtValueDecimal.EditValue = null;
        }

        void DtValueDateKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Modifiers== Keys.Control && e.KeyCode== Keys.D0)
                _common.dtValueDate.EditValue = null;
        }
    }
}