using System;
using System.Activities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using BusinessObjects.Windows.Workflows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств события
    /// </summary>
    internal sealed class BuildControlEvent : BasePropertyControlIBase<Event>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlEvent()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            //TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
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

            SelectedItem.StartPlanDate = _common.dtStartPlanDate.DateTime;
            SelectedItem.StartPlanTime = _common.tmStartPlanTime.Time.TimeOfDay;

            SelectedItem.StartOn =  (DateTime?)_common.dtStartOn.EditValue;

            if (_common.tmStartOnTime.EditValue != null)
                SelectedItem.StartOnTime = _common.tmStartOnTime.Time.TimeOfDay;
            else
                SelectedItem.StartOnTime = null;


            SelectedItem.EndOn = (DateTime?)_common.dtEndOn.EditValue;
            if (_common.tmEndOnTime.EditValue != null)
                SelectedItem.EndOnTime = _common.tmEndOnTime.Time.TimeOfDay;
            else
                SelectedItem.EndOnTime = null;
            
            SelectedItem.IsRecurcive = _common.chkEveryDay.Checked;
            SelectedItem.UserOwnerId = (int)_common.cmbUserOwnerId.EditValue;
            SelectedItem.UserToId = (int)_common.cmbUserToId.EditValue;
            SelectedItem.StatusId = (int)_common.cmbStatusId.EditValue;
            SelectedItem.WfToStartId = (int)_common.cmbWfToStartId.EditValue;
            SelectedItem.RecursiveId = (int) _common.cmbRecursiveId.EditValue;
            SaveStateData();

            InternalSave();
        }

        ControlEvent _common;
        BindingSource _bindingSourceUserOwnerId;
        List<Uid> _collUserOwnerId;

        BindingSource _bindingSourceUserToId;
        List<Uid> _collUserToId;

        BindingSource _bindingSourceStatusId;
        List<Analitic> _collStatusId;

        BindingSource _bindingSourceRecursiveId;
        List<Analitic> _collRecursiveId;

        BindingSource _bindingSourceWfToStart;
        List<Ruleset> _collWfTostart;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlEvent
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };

                _common.dtStartPlanDate.EditValue = SelectedItem.StartPlanDate;
                _common.tmStartPlanTime.EditValue = SelectedItem.StartPlanTime;

                _common.dtStartOn.EditValue = SelectedItem.StartOn;
                _common.tmStartOnTime.EditValue = SelectedItem.StartOnTime;

                _common.dtEndOn.EditValue = SelectedItem.EndOn;
                _common.tmEndOnTime.EditValue = SelectedItem.EndOnTime;

                if (SelectedItem.WfOwnerId != 0)
                    _common.txtWfOwnerId.Text = SelectedItem.WfOwner.Name;

                _common.chkEveryDay.Checked = SelectedItem.IsRecurcive;

                #region Данные для списка "Автор"
                _common.cmbUserOwnerId.Properties.DisplayMember = GlobalPropertyNames.Agent;
                _common.cmbUserOwnerId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceUserOwnerId = new BindingSource();
                _collUserOwnerId = new List<Uid>();
                if (SelectedItem.UserOwnerId != 0)
                    _collUserOwnerId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Uid>().Item(SelectedItem.UserOwnerId));
                _bindingSourceUserOwnerId.DataSource = _collUserOwnerId;
                _common.cmbUserOwnerId.Properties.DataSource = _bindingSourceUserOwnerId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewUserOwner, "DEFAULT_LOOKUP_UID");
                _common.cmbUserOwnerId.EditValue = SelectedItem.UserOwnerId;
                _common.cmbUserOwnerId.QueryPopUp += CmbGridSearchLookUpEditQueryPopUp;
                _common.ViewUserOwner.CustomUnboundColumnData += ViewUserOwnerIdCustomUnboundColumnData;
                _common.cmbUserOwnerId.EditValueChanged += new EventHandler(cmbUserOwnerId_EditValueChanged);
                _common.cmbUserOwnerId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUserOwnerId.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Исполнитель"
                _common.cmbUserToId.Properties.DisplayMember = GlobalPropertyNames.Agent;
                _common.cmbUserToId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceUserToId = new BindingSource();
                _collUserToId = new List<Uid>();
                if (SelectedItem.UserToId != 0)
                    _collUserToId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Uid>().Item(SelectedItem.UserToId));
                _bindingSourceUserToId.DataSource = _collUserToId;
                _common.cmbUserToId.Properties.DataSource = _bindingSourceUserToId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewUserTo, "DEFAULT_LOOKUP_UID");
                _common.cmbUserToId.EditValue = SelectedItem.UserToId;
                _common.cmbUserToId.QueryPopUp += CmbGridSearchLookUpEditQueryPopUp;
                _common.ViewUserTo.CustomUnboundColumnData += ViewUserToIdCustomUnboundColumnData;
                _common.cmbUserToId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUserToId.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Статус"
                _common.cmbStatusId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbStatusId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceStatusId = new BindingSource();
                _collStatusId = new List<Analitic>();
                if (SelectedItem.StatusId != 0)
                    _collStatusId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.StatusId));
                _bindingSourceStatusId.DataSource = _collStatusId;
                _common.cmbStatusId.Properties.DataSource = _bindingSourceStatusId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewStatus, "DEFAULT_LOOKUP_ANALITIC");
                _common.cmbStatusId.EditValue = SelectedItem.StatusId;
                _common.cmbStatusId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.ViewStatus.CustomUnboundColumnData += ViewStatusCustomUnboundColumnData;
                _common.cmbStatusId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbStatusId.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Тип рекурсии"
                _common.cmbRecursiveId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbRecursiveId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceRecursiveId = new BindingSource();
                _collRecursiveId = new List<Analitic>();
                if (SelectedItem.RecursiveId != 0)
                    _collRecursiveId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.RecursiveId));
                _bindingSourceRecursiveId.DataSource = _collRecursiveId;
                _common.cmbRecursiveId.Properties.DataSource = _bindingSourceRecursiveId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewRecursiveId, "DEFAULT_LOOKUP_ANALITIC");
                _common.cmbRecursiveId.EditValue = SelectedItem.RecursiveId;
                _common.cmbRecursiveId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.ViewRecursiveId.CustomUnboundColumnData += ViewRecursiveCustomUnboundColumnData;
                _common.cmbRecursiveId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbRecursiveId.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Процесс"
                _common.cmbWfToStartId.Properties.DisplayMember = GlobalPropertyNames.Agent;
                _common.cmbWfToStartId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceWfToStart= new BindingSource();
                _collWfTostart = new List<Ruleset>();
                if (SelectedItem.WfToStartId != 0)
                    _collWfTostart.Add(SelectedItem.Workarea.Cashe.GetCasheData<Ruleset>().Item(SelectedItem.WfToStartId));
                _bindingSourceWfToStart.DataSource = _collWfTostart;
                _common.cmbWfToStartId.Properties.DataSource = _bindingSourceWfToStart;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewWfToStartId, "DEFAULT_LOOKUP_NAME");
                _common.cmbWfToStartId.EditValue = SelectedItem.WfToStartId;
                _common.cmbWfToStartId.QueryPopUp += CmbGridSearchLookUpEditQueryPopUp;
                _common.ViewWfToStartId.CustomUnboundColumnData += ViewWfToStartIdCustomUnboundColumnData;
                _common.cmbWfToStartId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbWfToStartId.EditValue = 0;
                };
                #endregion
                UIHelper.GenerateTooltips<Event>(SelectedItem, _common);
                //
                BarButtonItem btnProcessStart = new BarButtonItem
                {
                    Caption = "Запуск процесса",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.RUNROUNDGREEN_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnProcessStart);
                btnProcessStart.ItemClick += delegate
                                                 {
                                                     int processToRunId = (int) _common.cmbWfToStartId.EditValue;
                                                     if (processToRunId == 0)
                                                         return;
                                                     Ruleset process = SelectedItem.Workarea.GetObject<Ruleset>(processToRunId);
                                                     Activity value = null;
                                                    value = WfCore.FindByCodeInternal(process.Code);
                                                    if(value!=null)
                                                    {
                                                        IDictionary<string, object> outputs = WorkflowInvoker.Invoke(value, new Dictionary<string, object>
                                                                                        {
                                                                                            {"CurrentObject", SelectedItem},
                                                                                            {"XmlData", SelectedItem.XmlData}
                                                                                        });
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
        void cmbUserOwnerId_EditValueChanged(object sender, EventArgs e)
        {
            if (SelectedItem.IsNew && (int)_common.cmbUserToId.EditValue == 0)
            {
                if (_bindingSourceUserToId.Count < 2)
                {
                    _collUserToId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER && f.AgentId != 0).ToList();
                    _bindingSourceUserToId.DataSource = _collUserToId;
                }
                _common.cmbUserToId.EditValue = _common.cmbUserOwnerId.EditValue;
            }
        }
        void ViewWfToStartIdCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayRulesetImagesLookupGrid(e, _bindingSourceWfToStart);
        }
        
        void ViewUserToIdCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayUidImagesLookupGrid(e, _bindingSourceUserToId);
        }

        void ViewUserOwnerIdCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayUidImagesLookupGrid(e, _bindingSourceUserOwnerId);
        }
        void ViewStatusCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayAnaliticImagesLookupGrid(e, _bindingSourceStatusId);
        }
        void ViewRecursiveCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayAnaliticImagesLookupGrid(e, _bindingSourceRecursiveId);
        }
        void CmbGridSearchLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, cmb.Properties.PopupFormSize.Height);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbUserOwnerId" && _bindingSourceUserOwnerId.Count < 2)
                {
                    _collUserOwnerId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER && f.AgentId != 0).ToList();
                    _bindingSourceUserOwnerId.DataSource = _collUserOwnerId;
                }
                else if (cmb.Name == "cmbUserToId" && _bindingSourceUserToId.Count < 2)
                {
                    _collUserToId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER && f.AgentId != 0).ToList();
                    _bindingSourceUserToId.DataSource = _collUserToId;
                }
                else if (cmb.Name == "cmbWfToStartId" && _bindingSourceWfToStart.Count < 2)
                {
                    _collWfTostart = SelectedItem.Workarea.GetCollection<Ruleset>(Ruleset.KINDVALUE_DOCPROCCESS_FONE).Where(f => f.KindValue == Ruleset.KINDVALUE_DOCPROCCESS_FONE).ToList();
                    _bindingSourceWfToStart.DataSource = _collWfTostart;
                }
                
            }
            catch (Exception z)
            { }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbStatusId" && _bindingSourceStatusId.Count < 2)
                {
                    Hierarchy rootTaskState = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_EVENT);
                    if (rootTaskState != null)
                        _collStatusId = rootTaskState.GetTypeContents<Analitic>();
                    else
                        _collStatusId = SelectedItem.Workarea.GetCollection<Analitic>(Analitic.KINDVALUE_EVENT).Where(f => f.KindValue == Analitic.KINDVALUE_EVENT).ToList();

                    _bindingSourceStatusId.DataSource = _collStatusId;
                }
                if (cmb.Name == "cmbRecursiveId" && _bindingSourceRecursiveId.Count < 2)
                {
                    Hierarchy rootRecursiveId = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITITIC_RECURCIVE);
                    if (rootRecursiveId != null)
                        _collRecursiveId = rootRecursiveId.GetTypeContents<Analitic>();
                    else
                        _collRecursiveId = SelectedItem.Workarea.GetCollection<Analitic>(Analitic.KINDVALUE_RECURCIVE).Where(f => f.KindValue == Analitic.KINDVALUE_RECURCIVE).ToList();

                    _bindingSourceRecursiveId.DataSource = _collRecursiveId;
                }
                
            }
            catch (Exception z)
            { }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
    }
}