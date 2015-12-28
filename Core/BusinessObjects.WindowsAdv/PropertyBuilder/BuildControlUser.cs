using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlUser : BasePropertyControlIBase<Uid>
    {
        List<Uid> _newUserGroup;
        List<Uid> _delUserGroup;
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlUser()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_USERGROUPS_NAME, ExtentionString.CONTROL_USERGROUPS_NAME);
            TotalPages.Add(ExtentionString.CONTROL_RIGHTDBENTITY_NAME, ExtentionString.CONTROL_RIGHTDBENTITY_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKUIDAGENT, ExtentionString.CONTROL_LINKUIDAGENT);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        public override void Build()
        {
            if(SelectedItem.KindValue!=1)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_USERGROUPS_NAME))
                    TotalPages.Remove(ExtentionString.CONTROL_USERGROUPS_NAME);
            }
            if(!SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_LINKUIDAGENT))
                    TotalPages.Remove(ExtentionString.CONTROL_LINKUIDAGENT);
            }
            base.Build();
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_USERGROUPS_NAME)
                BuildPageUserGroup();
            if (value == ExtentionString.CONTROL_RIGHTDBENTITY_NAME)
                BuildPageCommonRights();
            if (value == ExtentionString.CONTROL_LINKUIDAGENT)
                BuildPageScopeAgent();
        }

        private ListBrowserBaseObjects<Uid> _browserUid;
        private void BuildPageUserGroup()
        {
            if(_browserUid==null)
            {
                _newUserGroup = new List<Uid>();
                _delUserGroup = new List<Uid>();
                _browserUid = new ListBrowserBaseObjects<Uid>(SelectedItem.Workarea, SelectedItem.Groups, null, null, true, false, true, true);
                _browserUid.Build();
                _browserUid.ListControl.Name = ExtentionString.CONTROL_USERGROUPS_NAME;
                _browserUid.ShowProperty += browserUid_ShowProperty;
                Control.Controls.Add(_browserUid.ListControl);
                _browserUid.ListControl.Dock = DockStyle.Fill;

                // Построение группы управления
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_USERGROUPS_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();
                #region Создание
                BarButtonItem btnCreate = new BarButtonItem
                                              {
                                                  ButtonStyle = BarButtonStyle.Default,
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADDS, 1049),//"Добавить...",
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnCreate);

                btnCreate.ItemClick += delegate
                {
                    List<Uid> newGroup = SelectedItem.BrowseMultyList(s => s.KindValue == Uid.KINDVALUE_GROUP, null);
                    if (newGroup != null)
                    {
                        if (_newUserGroup == null) _newUserGroup = new List<Uid>();
                        foreach (Uid v in newGroup)
                        {
                            _browserUid.BindingSource.Add(v);
                            _newUserGroup.Add(v);    
                        }
                        
                    }
                };
                #endregion

                #region Свойства
                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                btnProp.ItemClick += delegate
                {
                    _browserUid.InvokeProperties();
                };
                #endregion

                #region Удаление
                BarButtonItem btnChainDelete = new BarButtonItem
                                                   {
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainDelete);
                btnChainDelete.ItemClick += delegate
                {
                    if (_delUserGroup == null)
                        _delUserGroup = new List<Uid>();
                    _delUserGroup.AddRange(_browserUid.SelectedValues);
                    foreach (Uid g in _browserUid.SelectedValues)
                    {
                        _browserUid.BindingSource.Remove(g);
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);
            }
            HidePageControls(ExtentionString.CONTROL_USERGROUPS_NAME);

        }

        private List<UserRightCommon> _collRights;
        private void BuildPageCommonRights()
        {
            if (_collRights == null)
            {
                _collRights = SelectedItem.Workarea.Access.GetCollectionUserRights(Right.KINDID_GENERALE, SelectedItem.Id);
                ControlList listRights = new ControlList();
                Control.Controls.Add(listRights);
                listRights.Dock = DockStyle.Fill;
                listRights.Name = ExtentionString.CONTROL_RIGHTDBENTITY_NAME;

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, listRights.View, "DEFAULT_LISTVIEWUSERRIGHTELEMENT");

                BindingSource bindRights = new BindingSource {DataSource = _collRights};
                listRights.Grid.DataSource = bindRights;
                // Построение группы управления
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_RIGHTDBENTITY_NAME)];
                RibbonPageGroup groupLinksRights = new RibbonPageGroup {Text = "Разрешения"};

                #region Добавить
                BarButtonItem btnCreateAcl = new BarButtonItem
                                                 {
                                                     ButtonStyle = BarButtonStyle.Default,
                                                     Caption =
                                                         SelectedItem.Workarea.Cashe.ResourceString(
                                                             ResourceString.BTN_CAPTON_ADD, 1049),
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                                                 };
                groupLinksRights.ItemLinks.Add(btnCreateAcl);
                btnCreateAcl.ItemClick += delegate
                {
                    List<Right> coll = SelectedItem.Workarea.GetCollection<Right>().Where(s => s.KindValue == 1).ToList();
                    var ccList = from c in coll
                                 join a in _collRights on c.Id equals a.RightId into j
                                 from x in j.DefaultIfEmpty()
                                 where x == null
                                 select c;
                    Right selectedRight = SelectedItem.Workarea.Empty<Right>().BrowseList(null, ccList.ToList());
                    if (selectedRight != null)
                    {
                        UserRightCommon right = new UserRightCommon
                                                    {
                                                        Workarea = SelectedItem.Workarea,
                                                        DbUidId = SelectedItem.Id,
                                                        RightId = selectedRight.Id,
                                                        Value = 1
                                                    };
                        right.Save();
                        bindRights.Add(right);
                    }

                };
                #endregion
                #region Изменить
                BarButtonItem btnPropAcl = new BarButtonItem
                                               {
                                                   Caption =
                                                       SelectedItem.Workarea.Cashe.ResourceString(
                                                           ResourceString.BTN_CAPTION_EDIT, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                               };
                groupLinksRights.ItemLinks.Add(btnPropAcl);
                btnPropAcl.ItemClick += delegate
                {
                    if (bindRights.Current == null) return;
                    UserRightCommon right = bindRights.Current as UserRightCommon;
                    right.ShowProperty();
                };
                #endregion
                #region Удаление
                BarButtonItem btnDeleteAcl = new BarButtonItem
                                                 {
                                                     Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph =
                                                         ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                                 ResourceImage.DELETE_X32)
                                                 };
                groupLinksRights.ItemLinks.Add(btnDeleteAcl);
                btnDeleteAcl.ItemClick += delegate
                {
                    if (bindRights.Current == null) return;
                    UserRightCommon right = bindRights.Current as UserRightCommon;
                    right.Delete();
                    bindRights.RemoveCurrent();
                };
                #endregion
                listRights.View.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "Image" && e.IsGetData)
                    {
                        e.Value = ResourceImage.GetSystemImage(ResourceImage.KEY_X16);
                    }
                };
                page.Groups.Add(groupLinksRights);

            }
            HidePageControls(ExtentionString.CONTROL_RIGHTDBENTITY_NAME);

        }
        void browserUid_ShowProperty(Uid obj)
        {
            obj.ShowProperty();
        }

        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.Password = _common.txtPassword.Text;
            SelectedItem.AgentId = (int)_common.cmbAgent.EditValue;
            SelectedItem.AuthenticateKind = _common.cmbAutentificate.SelectedIndex;
            SelectedItem.Email = _common.txtEmail.Text;
            if (_common.cmbMyCompanyId.EditValue != null) 
                SelectedItem.MyCompanyId = (int)_common.cmbMyCompanyId.EditValue;

            SelectedItem.AllowChangePassword = _common.chkAllowChangePassword.Checked;
            SelectedItem.AutogenerateNextPassword = _common.chkAutogenerateNextPassword.Checked;
            SelectedItem.RecommendedDateChangePassword = (DateTime?)_common.dtRecommendedDateChangePassword.EditValue;
            if (_common.cmbTimePeriodId.EditValue == null)
                SelectedItem.TimePeriodId = 0;
            else
                SelectedItem.TimePeriodId = (int)_common.cmbTimePeriodId.EditValue;

            if (!SelectedItem.IsNew)
            {
                if (_delUserGroup != null)
                {
                    foreach (Uid g in _delUserGroup)
                    {
                        SelectedItem.ExcludeFromGroup(g);
                    }
                }

                if (_newUserGroup != null)
                {
                    foreach (Uid g in _newUserGroup)
                    {
                        SelectedItem.IncludeInGroup(g);
                    }
                }
            }
            
            
            SaveStateData();

            InternalSave();
            Uid grp = SelectedItem.Workarea.GetCollection<Uid>().FirstOrDefault(s => s.Name == "Пользователи");
            if (!SelectedItem.Workarea.Access.IsUserExistsInGroup(SelectedItem.Name, "Пользователи") && SelectedItem.Name != "Пользователи")
                SelectedItem.IncludeInGroup(grp);
        }

        ControlUser _common;
        private List<Agent> _collMyCompany;
        private BindingSource bindingSourceMyCompanyId;

        private List<TimePeriod> _collTimePeriod;
        private BindingSource bindingSourceTimePeriodId;
        
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlUser
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtPassword = {Text = SelectedItem.Password},
                                  txtEmail = { Text = SelectedItem.Email },
                                  Workarea = SelectedItem.Workarea
                              };

                _common.txtPassword.ButtonClick += TxtPasswordButtonClick;
                #region Данные для списка "Корреспондент"
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbAgent, "DEFAULT_LOOKUPAGENT");
                BindingSource bindingSourceForm = new BindingSource
                                                      {
                                                          DataSource =
                                                              SelectedItem.Workarea.GetCollection<Agent>().Where(
                                                                  s => s.KindValue == 2).ToList()
                                                      };
                _common.cmbAgent.Properties.DataSource = bindingSourceForm;
                _common.cmbAgent.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbAgent.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbAgent.EditValue = SelectedItem.AgentId;
                #endregion

                #region Данные для списка "Предприятие"
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbMyCompanyId, "DEFAULT_LOOKUPAGENT");

                
                _collMyCompany = new List<Agent>();
                bindingSourceMyCompanyId = new BindingSource(); //{ DataSource = hierarchy.GetTypeContents<Agent>() };
                if (SelectedItem.MyCompanyId != 0)
                    _collMyCompany.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.MyCompanyId));

                bindingSourceMyCompanyId.DataSource = _collMyCompany;

                _common.ViewMyCompanyId.CustomUnboundColumnData += (sender, e) => DisplayAgentImagesLookupGrid(e, bindingSourceMyCompanyId);
                _common.cmbMyCompanyId.Properties.DataSource = bindingSourceMyCompanyId;
                _common.cmbMyCompanyId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbMyCompanyId.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbMyCompanyId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbMyCompanyId.EditValue = SelectedItem.MyCompanyId;
                #endregion

                _common.chkAllowChangePassword.Checked = SelectedItem.AllowChangePassword;
                _common.chkAutogenerateNextPassword.Checked = SelectedItem.AutogenerateNextPassword;
                _common.dtRecommendedDateChangePassword.EditValue = SelectedItem.RecommendedDateChangePassword;

                _common.cmbAutentificate.Properties.Items.AddRange("Sql Server|Windows authentication|Windows authentication without login".Split('|'));
                _common.cmbAutentificate.SelectedIndex = SelectedItem.AuthenticateKind;
                if(SelectedItem.KindValue == 2)
                {
                    _common.LayoutControl.HideItem(_common.layoutControlItemAgent);
                    _common.LayoutControl.HideItem(_common.layoutControlItemPassword);
                    _common.LayoutControl.HideItem(_common.layoutControlItemAutentificate);
                    _common.LayoutControl.HideItem(_common.layoutControlItemRecommendedDateChangePassword);
                    _common.LayoutControl.HideItem(_common.layoutControlItemAllowChangePassword);
                    _common.LayoutControl.HideItem(_common.layoutControlItemAutogenerateNextPassword);
                    _common.LayoutControl.HideItem(_common.layoutControlItemTimePeriodId);
                    //_common.LayoutControl.HideItem(_common.layoutControlItemCode);
                }
                else
                {
                    _common.layoutControlItemCode.Text = "Логин:";
                    if (!SelectedItem.IsNew) //SelectedItem.AuthenticateKind==2
                    {
                        //_common.LayoutControl.HideItem(_common.layoutControlItemPassword);
                        //_common.layoutControlItemAutentificate.
                        //_common.LayoutControl.HideItem(_common.layoutControlItemAutentificate);
                        _common.cmbAutentificate.Properties.ReadOnly = true;

                    }
                    #region Данные для списка "Разрешенный вход"
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbTimePeriodId, "DEFAULT_LOOKUP_TIMEPRIOD");


                    _collTimePeriod = new List<TimePeriod>();
                    bindingSourceTimePeriodId = new BindingSource();
                    if (SelectedItem.TimePeriodId != 0)
                        _collTimePeriod.Add(SelectedItem.Workarea.Cashe.GetCasheData<TimePeriod>().Item(SelectedItem.TimePeriodId));

                    bindingSourceTimePeriodId.DataSource = _collTimePeriod;

                    _common.ViewTimePeriodId.CustomUnboundColumnData += (sender, e) => UIHelper.DisplayTimePriodImagesLookupGrid(e, bindingSourceTimePeriodId);
                    _common.cmbTimePeriodId.Properties.DataSource = bindingSourceTimePeriodId;
                    _common.cmbTimePeriodId.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbTimePeriodId.Properties.ValueMember = GlobalPropertyNames.Id;
                    _common.cmbTimePeriodId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbTimePeriodId.EditValue = SelectedItem.TimePeriodId;

                    _common.cmbTimePeriodId.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbTimePeriodId.EditValue = 0;
                    };
                    #endregion
                }
                if(!SelectedItem.IsNew)
                {
                    _common.cmbAutentificate.Properties.ReadOnly = true;
                }
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

        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            if (cmb != null && cmb.Properties.PopupFormSize.Height <200)
               cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 200);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbMyCompanyId" && bindingSourceMyCompanyId.Count < 2)
                {
                    //Hierarchy hierarchy = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYDEPATMENTS);
                    _collMyCompany = SelectedItem.Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY); //hierarchy.GetTypeContents<Agent>();
                    bindingSourceMyCompanyId.DataSource = _collMyCompany;
                }
                if (cmb.Name == "cmbTimePeriodId" && bindingSourceTimePeriodId.Count < 2)
                {
                    Hierarchy hierarchy = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_TIMEPERIOD_USERLOGON);
                    _collTimePeriod = hierarchy.GetTypeContents<TimePeriod>();
                    bindingSourceTimePeriodId.DataSource = _collTimePeriod;
                }
                
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }

        ControlList _controlLinksAgent;
        private List<IChainAdvanced<Uid, Agent>> _collectionRuleset;
        private BindingSource _bindRuleset;
        BrowseChainObject<Agent> OnBrowseAgent { get; set; }
        /// <summary>
        /// Закладка "Область видимости"
        /// </summary>
        private void BuildPageScopeAgent()
        {
            if (_controlLinksAgent == null)
            {
                _controlLinksAgent = new ControlList { Name = ExtentionString.CONTROL_LINKUIDAGENT };
                // Данные для отображения в списке связей
                _bindRuleset = new BindingSource();
                _collectionRuleset = SelectedItem.GetLinkedAgents();
                _bindRuleset.DataSource = _collectionRuleset;
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKUIDAGENT)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новая связь
                BarButtonItem btnChainCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.DropDown,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnChainCreate);

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Agent);
                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                foreach (ChainKind itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                    {
                        ChainKind objectTml = (ChainKind)btn.Tag;
                        if (OnBrowse == null)
                        {
                            OnBrowseAgent = Extentions.BrowseListType;
                        }
                        List<int> types = objectTml.GetContentTypeKindId();
                        List<Agent> newAgent = OnBrowseAgent.Invoke(SelectedItem.Workarea.Empty<Agent>(), s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Agent>());
                        if (newAgent != null)
                        {
                            foreach (Agent selItem in newAgent)
                            {
                                ChainAdvanced<Uid, Agent> link = new ChainAdvanced<Uid, Agent>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id };
                                try
                                {
                                    link.StateId = State.STATEACTIVE;
                                    link.Save();
                                    _bindRuleset.Add(link);
                                }
                                catch (DatabaseException dbe)
                                {
                                    Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                        SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                             "Создание новой связи невозможно!", dbe.Message, dbe.Id);
                                }
                                catch (Exception ex)
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                        SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    };
                }
                btnChainCreate.DropDownControl = mnuTemplates;
                #endregion

                #region Изменить
                BarButtonItem btnProp = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                groupLinksAction.ItemLinks.Add(btnProp);

                btnProp.ItemClick += delegate
                {
                    ((_bindRuleset.Current) as ChainAdvanced<Uid, Agent>).ShowProperty();
                };
                #endregion

                #region Выше
                BarButtonItem btnChainMoveUp = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_UP, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.ARROWUPBLUE_X32)
                };
                groupLinksAction.ItemLinks.Add(btnChainMoveUp);

                btnChainMoveUp.ItemClick += delegate
                {
                    if (_bindRuleset.Current != null)
                    {
                        IChainAdvanced<Uid, Agent> currentItem = (IChainAdvanced<Uid, Agent>)_bindRuleset.Current;
                        if (_bindRuleset.Position - 1 > -1)
                        {
                            IChainAdvanced<Uid, Agent> prevItem = (IChainAdvanced<Uid, Agent>)_bindRuleset[_bindRuleset.Position - 1];
                            IWorkarea wa = ((IChainAdvanced<Library, Ruleset>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Uid, Agent>)currentItem, (ChainAdvanced<Uid, Agent>)prevItem);
                                _controlLinksAgent.View.UpdateCurrentRow();
                                int indexNext = _controlLinksAgent.View.GetPrevVisibleRow(_controlLinksAgent.View.FocusedRowHandle);
                                _controlLinksAgent.View.RefreshRow(indexNext);
                            }
                            catch (Exception e)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(e.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                #region Ниже
                BarButtonItem btnChainMoveDown = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DOWN, 1049),//"Ниже", 
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.ARROWDOWNBLUE_X32)
                };
                groupLinksAction.ItemLinks.Add(btnChainMoveDown);

                btnChainMoveDown.ItemClick += delegate
                {
                    if (_bindRuleset.Current != null)
                    {
                        IChainAdvanced<Uid, Agent> currentItem = (IChainAdvanced<Uid, Agent>)_bindRuleset.Current;
                        if (_bindRuleset.Position + 1 < _bindRuleset.Count)
                        {
                            IChainAdvanced<Uid, Agent> nextItem = (IChainAdvanced<Uid, Agent>)_bindRuleset[_bindRuleset.Position + 1];
                            IWorkarea wa = ((IChainAdvanced<Uid, Agent>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Uid, Agent>)nextItem, (ChainAdvanced<Uid, Agent>)currentItem);
                                _controlLinksAgent.View.UpdateCurrentRow();
                                int indexNext = _controlLinksAgent.View.GetNextVisibleRow(_controlLinksAgent.View.FocusedRowHandle);
                                _controlLinksAgent.View.RefreshRow(indexNext);
                            }
                            catch (Exception e)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(e.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                #region Удаление
                BarButtonItem btnChainDelete = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                };
                groupLinksAction.ItemLinks.Add(btnChainDelete);


                btnChainDelete.ItemClick += delegate
                {
                    ChainAdvanced<Uid, Agent> currentObject = _bindRuleset.Current as ChainAdvanced<Uid, Agent>;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea,
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), "Удаление связей",
                                                         string.Empty,
                                                         Properties.Resources.STR_CHOICE_DEL);
                        if (res == 0)
                        {
                            try
                            {
                                // TODO: Поддержка удаления связей в корзину
                                //currentObject.Remove();
                                _bindRuleset.Remove(currentObject);
                            }
                            catch (Exception ex)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                currentObject.Delete();
                                _bindRuleset.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                         "Ошибка удаления связи!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlLinksAgent.View, "DEFAULT_LISTVIEWCHAIN");
                _controlLinksAgent.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        System.Drawing.Rectangle r = e.Bounds;
                        System.Drawing.Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                };
                Control.Controls.Add(_controlLinksAgent);
                _controlLinksAgent.Dock = DockStyle.Fill;

                _controlLinksAgent.Grid.DataSource = _bindRuleset;
            }
            CurrentPrintControl = _controlLinksAgent.Grid;
            HidePageControls(ExtentionString.CONTROL_LINKUIDAGENT);
        }

        void TxtPasswordButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            _common.txtPassword.Properties.PasswordChar = _common.txtPassword.Properties.PasswordChar == '*' ? '\0' : '*';
        }
    }
}