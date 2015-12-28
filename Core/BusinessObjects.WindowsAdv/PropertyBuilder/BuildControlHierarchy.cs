using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlHierarchy : BasePropertyControlIBase<Hierarchy>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlHierarchy()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
            TotalPages.Add(ExtentionString.CONTROL_LINKUIDAGENT, ExtentionString.CONTROL_LINKUIDAGENT);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
            
        }
        public override void Build()
        {
            if (!SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_LINKUIDAGENT))
                    TotalPages.Remove(ExtentionString.CONTROL_LINKUIDAGENT);
            }
            base.Build();
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            
            if (value == ExtentionString.CONTROL_LINKUIDAGENT)
                BuildPageScopeAgent();
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.OrderNo = (short)_common.numSortOrder.Value;
            SelectedItem.ViewListDocumentsId = (int)_common.cmbViewDocs.EditValue;
            SelectedItem.ViewListId = (int)_common.cmbView.EditValue;
            SelectedItem.VirtualBuildId = (int)_common.cmbVirtualBuild.EditValue;
            int contentFlag = 0;
            if (SelectedItem.ViewListId == 0)
            {
                if (SelectedItem.GetContentType().Count > 0)
                    contentFlag = 1;
            }
            else if(SelectedItem.ViewList.KindId== CustomViewList.KINDID_LIST)
            {
                if (SelectedItem.GetContentType().Count > 0)
                    contentFlag = 1;
            }
            SelectedItem.ContentFlags = contentFlag;

            SaveStateData();

            InternalSave();
        }

        ControlHierarchy _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlHierarchy
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  numSortOrder = {Value = SelectedItem.OrderNo},
                                  txtParent =
                                      {Text = SelectedItem.Parent == null ? string.Empty : SelectedItem.Parent.Name},
                                  Workarea = SelectedItem.Workarea
                              };

                //SelectedItem.VirtualBuildId = (int)_common.cmbVirtualBuild.EditValue;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbVirtualBuild, "DEFAULT_LOOKUP");
                BindingSource bindingSourceVirtualBuild = new BindingSource
                                                              {
                                                                  DataSource =
                                                                      SelectedItem.Workarea.GetCollection<Library>().
                                                                      Where(s => s.KindValue == 17)
                                                              };
                _common.cmbVirtualBuild.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbVirtualBuild.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbVirtualBuild.Properties.DataSource = bindingSourceVirtualBuild;
                _common.cmbVirtualBuild.EditValue = SelectedItem.VirtualBuildId;
                _common.cmbVirtualBuild.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbVirtualBuild.EditValue = 0;
                };

                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbView, "DEFAULT_LOOKUP");
                BindingSource bindingSourceView = new BindingSource
                                                      {
                                                          DataSource =
                                                              SelectedItem.Workarea.GetCollection<CustomViewList>().
                                                              Where(
                                                                  s =>
                                                                  s.SourceEntityTypeId == SelectedItem.ContentEntityId)
                                                      };
                _common.cmbView.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbView.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbView.Properties.DataSource = bindingSourceView;
                _common.cmbView.EditValue = SelectedItem.ViewListId;
                _common.cmbView.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbView.EditValue = 0;
                };

                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbViewDocs, "DEFAULT_LOOKUP");
                BindingSource bindingSourceViewDocs = new BindingSource
                                                          {
                                                              DataSource =
                                                                  SelectedItem.Workarea.GetCollection<CustomViewList>().
                                                                  Where(s => s.SourceEntityTypeId == 20)
                                                          };
                _common.cmbViewDocs.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbViewDocs.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbViewDocs.Properties.DataSource = bindingSourceViewDocs;
                _common.cmbViewDocs.EditValue = SelectedItem.ViewListDocumentsId;
                _common.cmbViewDocs.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbViewDocs.EditValue = 0;
                };
                //if (SelectedItem.ViewListId == 0)
                //{
                //    List<EntityKind> collectionKinds = SelectedItem.Workarea.GetCollectionEntityKind(SelectedItem.ContentEntityId);
                //    foreach (EntityKind itemKind in collectionKinds)
                //    {
                //        if(SelectedItem.ContentEntityType.IsSubKindAsFlag)
                //        {
                //            CheckState state = ((SelectedItem.ContentFlags & itemKind.SubKind) == itemKind.SubKind) ? CheckState.Checked : CheckState.Unchecked;
                //            _common.GridSubKinds.Items.Add(itemKind.SubKind, itemKind.Name, state, true);
                //        }
                //        else
                //        {
                //            CheckState state = (SelectedItem.ContentFlags == itemKind.SubKind ) ? CheckState.Checked : CheckState.Unchecked;
                //            _common.GridSubKinds.Items.Add(itemKind.SubKind, itemKind.Name, state, true);
                //        }
                        
                //       // _common.GridSubKinds.Rows.Add((SelectedItem.ContentFlags & itemKind.SubKind) == itemKind.SubKind, itemKind.Name, itemKind.SubKind);
                //    }
                //}

                if ((SelectedItem.ViewListId == 0) || (SelectedItem.ViewListId!=0 && SelectedItem.ViewList!=null && SelectedItem.ViewList.KindValue== CustomViewList.KINDVALUE_LIST) )
                {
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.GridSubKind.View, "DEFAULT_LOOKUP_NAME");
                    BindingSource bindEntKinds = new BindingSource();
                    List<HierarchyContentType> collValues = SelectedItem.GetContentType();//  HierarchyContentType.GetCollection(SelectedItem.Workarea, SelectedItem.Id);
                    bindEntKinds.DataSource = collValues;
                    _common.GridSubKind.Grid.DataSource = bindEntKinds;
                    _common.GridSubKind.View.OptionsView.ShowColumnHeaders = false;

                    
                    BarButtonItem btnAdd = new BarButtonItem
                    {
                        Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                        RibbonStyle = RibbonItemStyles.Large,
                        Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                    };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnAdd);
                    btnAdd.ItemClick += delegate
                    {
                        if (SelectedItem.ContentEntityType.EntityKinds.Count == 0)
                            return;
                        List<EntityKind> col = Extentions.BrowseMultyList(SelectedItem.ContentEntityType.EntityKinds[0], SelectedItem.Workarea, null, SelectedItem.ContentEntityType.EntityKinds, false);
                        if (col != null && col.Count > 0)
                        {
                            HierarchyContentType valueAdd = collValues.Find(s => s.EntityKindId == col[0].Id);
                            if (valueAdd == null)
                            {
                                valueAdd = new HierarchyContentType { Workarea = SelectedItem.Workarea };
                                valueAdd.ElementId = SelectedItem.Id;
                                valueAdd.EntityKindId = col[0].Id;
                                valueAdd.Save();
                                collValues.Add(valueAdd);
                                if (!bindEntKinds.Contains(valueAdd))
                                    bindEntKinds.Add(valueAdd);
                                _common.GridSubKind.Grid.RefreshDataSource();
                            }
                        }
                    };


                    BarButtonItem btnRemove = new BarButtonItem
                    {
                        Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                        RibbonStyle = RibbonItemStyles.Large,
                        Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                    };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnRemove);
                    btnRemove.ItemClick += delegate
                    {
                        if (bindEntKinds.Current == null) return;
                        HierarchyContentType val = bindEntKinds.Current as HierarchyContentType;
                        val.Delete();
                        bindEntKinds.RemoveCurrent();
                    };
                    
                }

                frmProp.btnRefresh.ItemClick += delegate
                {
                    bindingSourceViewDocs.DataSource =
                        SelectedItem.Workarea.GetCollection<CustomViewList>(true).
                            Where(s => s.SourceEntityTypeId == 20);

                    bindingSourceVirtualBuild.DataSource =
                        SelectedItem.Workarea.GetCollection<Library>(true).
                            Where(s => s.KindValue == 17);

                    bindingSourceView.DataSource =
                        SelectedItem.Workarea.GetCollection<CustomViewList>(true).
                            Where(s =>
                                  s.SourceEntityTypeId == SelectedItem.ContentEntityId);

                };
                //_collOperation = SelectedItem.Workarea.GetTemplates<Document>(true);
                //_bindingSourceOperation.DataSource = _collOperation;

                //_collForm = SelectedItem.Workarea.GetCollection<Library>(true).Where(s => s.KindValue == 16).ToList();
                //_bindingSourceForm.DataSource = _collForm;

                //_collViewDocs = SelectedItem.Workarea.GetCollection<CustomViewList>(true).Where(s => s.SourceEntityTypeId == 20).ToList();
                //_bindingSourceViewDocs.DataSource = _collViewDocs;
            
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

        ControlList _controlLinksAgent;
        private List<IChainAdvanced<Hierarchy, Agent>> _collectionRuleset;
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
                _collectionRuleset = SelectedItem.GetLinkedHierarchy();
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
                                ChainAdvanced<Hierarchy, Agent> link = new ChainAdvanced<Hierarchy, Agent>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId= State.STATEACTIVE };
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
                    ((_bindRuleset.Current) as ChainAdvanced<Hierarchy, Agent>).ShowProperty();
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
                        IChainAdvanced<Hierarchy, Agent> currentItem = (IChainAdvanced<Hierarchy, Agent>)_bindRuleset.Current;
                        if (_bindRuleset.Position - 1 > -1)
                        {
                            IChainAdvanced<Hierarchy, Agent> prevItem = (IChainAdvanced<Hierarchy, Agent>)_bindRuleset[_bindRuleset.Position - 1];
                            IWorkarea wa = ((IChainAdvanced<Library, Ruleset>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Hierarchy, Agent>)currentItem, (ChainAdvanced<Hierarchy, Agent>)prevItem);
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
                        IChainAdvanced<Hierarchy, Agent> currentItem = (IChainAdvanced<Hierarchy, Agent>)_bindRuleset.Current;
                        if (_bindRuleset.Position + 1 < _bindRuleset.Count)
                        {
                            IChainAdvanced<Hierarchy, Agent> nextItem = (IChainAdvanced<Hierarchy, Agent>)_bindRuleset[_bindRuleset.Position + 1];
                            IWorkarea wa = ((IChainAdvanced<Hierarchy, Agent>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Hierarchy, Agent>)nextItem, (ChainAdvanced<Hierarchy, Agent>)currentItem);
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
                    ChainAdvanced<Hierarchy, Agent> currentObject = _bindRuleset.Current as ChainAdvanced<Hierarchy, Agent>;
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

        /// <summary>
        /// Подготовка страницы "Связи"
        /// </summary>
        /// <returns></returns>
        protected override void BuildPageLink()
        {
            if (_controlLinks == null)
            {
                _controlLinks = new ControlList { Name = ExtentionString.CONTROL_LINK_NAME };
                // Данные для отображения в списке связей
                BindingSource collectionBind = new BindingSource();
                List<IChain<Hierarchy>> collection = (SelectedItem as IChains<Hierarchy>).GetLinks();
                collectionBind.DataSource = collection;
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINK_NAME)];
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

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == SelectedItem.EntityId);

                for (int i = collectionTemplates.Count - 1; i > -1; i--)
                {
                    List<int> typeFrom = collectionTemplates[i].GetContentTypeKindIdFrom();
                    if (typeFrom.Count > 0 && !typeFrom.Contains(0))
                    {
                        if (!typeFrom.Contains(SelectedItem.KindId))
                        {
                            collectionTemplates.RemoveAt(i);
                        }
                    }
                }

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
                            //OnBrowseChain = Extentions.BrowseListType<T>(SelectedItem, s => (s.KindValue & objectTml.EntityContent) == objectTml.EntityContent,
                            //                                             SelectedItem.Workarea.GetCollection<T>());
                            //OnBrowse = Extentions.BrowseListType<T>;
                            //OnBrowseChain = Extentions.BrowseListType;
                        }
                        List<ChainKindContentType> allTypes = objectTml.GetContentType();
                        List<int> types = objectTml.GetContentTypeKindId();

                        
                        //List<Hierarchy> newAgent = OnBrowseChain.Invoke(SelectedItem, s => allTypes.Exists(a => (a.EntityKindIdFrom == SelectedItem.KindId || a.EntityKindIdFrom == 0) && s.KindId == a.EntityKindId), SelectedItem.Workarea.GetCollection<Hierarchy>(true));

                        Hierarchy rootHierarchy = SelectedItem.Workarea.GetCollection<Hierarchy>().FirstOrDefault(
                                                   s => s.ParentId == 0 && s.ContentEntityId == SelectedItem.ContentEntityId);

                        Hierarchy selectedHierarchy = null;
                        if(SelectedItem.ContentEntityId== (short) WhellKnownDbEntity.Agent)
                        {
                            selectedHierarchy = rootHierarchy.BrowseTree(SelectedItem.Workarea.Empty<Agent>());
                        }

                        List<Hierarchy> newAgent = null;
                        if (selectedHierarchy != null)
                        {
                            newAgent = new List<Hierarchy> { selectedHierarchy };
                        }
                        if (newAgent != null)
                        {
                            foreach (Hierarchy selItem in newAgent)
                            {
                                Chain<Hierarchy> link = new Chain<Hierarchy>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };
                                try
                                {
                                    link.Save();
                                    collectionBind.Add(link);
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
                    ((collectionBind.Current) as Chain<Hierarchy>).ShowProperty();
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
                    if (collectionBind.Current != null)
                    {
                        IChain<Hierarchy> currentItem = (IChain<Hierarchy>)collectionBind.Current;
                        (currentItem as Chain<Hierarchy>).OrderNo--;
                        try
                        {
                            (currentItem as Chain<Hierarchy>).Save();
                            _controlLinks.View.UpdateCurrentRow();
                            int indexNext = _controlLinks.View.GetPrevVisibleRow(_controlLinks.View.FocusedRowHandle);
                            _controlLinks.View.RefreshRow(indexNext);
                        }
                        catch (Exception e)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(e.Message,
                                   SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (collectionBind.Current != null)
                    {
                        IChain<Hierarchy> currentItem = (IChain<Hierarchy>)collectionBind.Current;
                        (currentItem as Chain<Hierarchy>).OrderNo++;
                        try
                        {
                            (currentItem as Chain<Hierarchy>).Save();
                            _controlLinks.View.UpdateCurrentRow();
                            int indexNext = _controlLinks.View.GetPrevVisibleRow(_controlLinks.View.FocusedRowHandle);
                            _controlLinks.View.RefreshRow(indexNext);
                        }
                        catch (Exception e)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(e.Message,
                                   SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    Chain<Hierarchy> currentObject = collectionBind.Current as Chain<Hierarchy>;
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
                                collectionBind.Remove(currentObject);
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
                                collectionBind.Remove(currentObject);
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
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlLinks.View, "DEFAULT_LISTVIEWCHAIN");
                _controlLinks.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                };
                _controlLinks.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && collectionBind.Count > 0)
                    {
                        IChain<Hierarchy> imageItem = collectionBind[e.ListSourceRowIndex] as IChain<Hierarchy>;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.Right.GetImage();
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && collectionBind.Count > 0)
                    {
                        IChain<Hierarchy> imageItem = collectionBind[e.ListSourceRowIndex] as IChain<Hierarchy>;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };
                Control.Controls.Add(_controlLinks);
                _controlLinks.Dock = DockStyle.Fill;

                _controlLinks.Grid.DataSource = collectionBind;
                _controlLinks.View.ExpandAllGroups();
            }
            CurrentPrintControl = _controlLinks.Grid;
            HidePageControls(ExtentionString.CONTROL_LINK_NAME);
        }
    }
}