using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств подразделения/отдела
    /// </summary>
    internal sealed class BuildControlDepatment : BasePropertyControlIBase<Depatment>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlDepatment()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKWORKER, ExtentionString.CONTROL_LINKWORKER);
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

            if (string.IsNullOrEmpty(SelectedItem.CodeFind))
            {
                SelectedItem.CodeFind = (SelectedItem.Name + Transliteration.Front(SelectedItem.Name)).Replace(" ", "");
            }

            if (_common.cmbMyCompanyId.EditValue != null)
                SelectedItem.MyCompanyId = (int)_common.cmbMyCompanyId.EditValue;
            
            if (_common.cmbHead.EditValue != null)
                SelectedItem.DepatmentHeadId = (int)_common.cmbHead.EditValue;

            if (_common.cmbSubHead.EditValue != null)
                SelectedItem.DepatmentSubHeadId = (int)_common.cmbSubHead.EditValue;

            SaveStateData();

            InternalSave();
        }

        ControlDepatment _common;
        private List<Agent> _collMyCompany;
        private BindingSource bindingSourceMyCompanyId;

        private List<Agent> _collHead;
        private BindingSource bindingSourceHead;

        private List<Agent> _collSubHead;
        private BindingSource bindingSourceSubHead;

        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlDepatment
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };

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

                #region Данные для списка "Корреспондент"
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbHead, "DEFAULT_LOOKUPAGENT");
                _collHead = new List<Agent>();
                if (SelectedItem.DepatmentHeadId != 0)
                    _collHead.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.DepatmentHeadId));
                bindingSourceHead = new BindingSource();
                bindingSourceHead.DataSource = _collHead;
                _common.cmbHead.Properties.DataSource = bindingSourceHead;
                _common.cmbHead.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbHead.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbHead.EditValue = SelectedItem.DepatmentHeadId;
                _common.cmbHead.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.ViewHeadId.CustomUnboundColumnData += (sender, e) => DisplayAgentImagesLookupGrid(e, bindingSourceHead);
                
                #endregion

                #region Данные для списка "Корреспондент"
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbSubHead, "DEFAULT_LOOKUPAGENT");
                _collSubHead = new List<Agent>();
                if (SelectedItem.DepatmentSubHeadId != 0)
                    _collSubHead.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.DepatmentSubHeadId));
                bindingSourceSubHead = new BindingSource();
                bindingSourceSubHead.DataSource = _collSubHead;
                _common.cmbSubHead.Properties.DataSource = bindingSourceSubHead;
                _common.cmbSubHead.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbSubHead.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbSubHead.EditValue = SelectedItem.DepatmentSubHeadId;
                _common.cmbSubHead.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.ViewSubHeadId.CustomUnboundColumnData += (sender, e) => DisplayAgentImagesLookupGrid(e, bindingSourceSubHead);

                #endregion
                UIHelper.GenerateTooltips<Depatment>(SelectedItem, _common);
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
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 400);
            if (cmb != null && cmb.Properties.PopupFormSize.Height < 200)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 400);
            try
            {
                Int32 curMyCompany = 0;
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbMyCompanyId" && bindingSourceMyCompanyId.Count < 2)
                {
                    //Hierarchy hierarchy = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYDEPATMENTS);
                    _collMyCompany = SelectedItem.Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY); //hierarchy.GetTypeContents<Agent>();
                    bindingSourceMyCompanyId.DataSource = _collMyCompany;
                }
                if (cmb.Name == "cmbHead")
                {
                    curMyCompany = (int)_common.cmbMyCompanyId.EditValue;
                    if (curMyCompany != 0)
                    {
                        _collHead = Chain<Agent>.GetChainSourceList(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(curMyCompany), SelectedItem.Workarea.WorkresChainId());
                        bindingSourceHead.DataSource = _collHead;
                    }
                    else
                        bindingSourceHead.DataSource = null;
                }
                if (cmb.Name == "cmbSubHead")
                {
                    curMyCompany = (int)_common.cmbMyCompanyId.EditValue;
                    if (curMyCompany != 0)
                    {
                        _collSubHead = Chain<Agent>.GetChainSourceList(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(curMyCompany), SelectedItem.Workarea.WorkresChainId());
                        bindingSourceSubHead.DataSource = _collSubHead;
                    }
                    else
                        bindingSourceSubHead.DataSource = null;
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

        protected override void BuildPage(string value)
        {
            base.BuildPage(value);

            if (value == ExtentionString.CONTROL_LINKWORKER)
                BuildPageAgents();
        }

        #region Связи с корреспондентами
        private ControlList _controlAgentView;
        private BindingSource _sourceBindAgents;
        private List<ChainValueView> _collAgentView;
        private void BuildPageAgents()
        {
            if ((SelectedItem as IChainsAdvancedList<Depatment, Agent>) == null)
                return;
            if (_controlAgentView == null)
            {
                _controlAgentView = new ControlList { Name = ExtentionString.CONTROL_LINKWORKER };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlAgentView.View,
                                                       "DEFAULT_LISTVIEWCHAINVALUE");
                _controlAgentView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlAgentView);
                _sourceBindAgents = new BindingSource();

                _collAgentView = ChainValueView.GetView<Depatment, Agent>(SelectedItem);
                _sourceBindAgents.DataSource = _collAgentView;
                _controlAgentView.Grid.DataSource = _sourceBindAgents;
                _controlAgentView.View.ExpandAllGroups();
                _controlAgentView.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                    else if (e.Column.Name == "colStateImage")
                    {
                        ChainValueView rowValue = _sourceBindAgents.Current as ChainValueView;
                        if (rowValue == null) return;
                    }
                };
                _controlAgentView.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindAgents.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindAgents[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageMessage(imageItem.Workarea, imageItem.RightKind);
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindAgents.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindAgents[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKWORKER)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnValueCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.DropDown,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_LINK, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINKNEW_X32)
                };
                //
                btnValueCreate.SuperTip = UIHelper.CreateSuperToolTip(btnValueCreate.Glyph, btnValueCreate.Caption, "Связать текущую задачу с сообщениями");

                groupLinksAction.ItemLinks.Add(btnValueCreate);

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Agent && f.Code == ChainKind.WORKERS);

                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                foreach (ChainKind itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;

                    btn.SuperTip = UIHelper.CreateSuperToolTip(btnValueCreate.Glyph, btn.Caption, itemTml.Memo);
                    //SelectedItem.Workarea.Cashe.ResourceString("BTN_TASKREASIGN_TOOLTIP")
                    btn.ItemClick += delegate
                    {
                        ChainKind objectTml = (ChainKind)btn.Tag;
                        //OnBrowseMessage = Extentions.BrowseListType;

                        List<int> types = objectTml.GetContentTypeKindId();
                        //types.Contains(s.KindId)
                        //List<T> newAgent = OnBrowseChain.Invoke(SelectedItem, s => (s.KindValue & objectTml.EntityContent) == objectTml.EntityContent, SelectedItem.Workarea.GetCollection<T>());
                        List<Agent> newAgent = SelectedItem.Workarea.Empty<Agent>().BrowseListType(s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Agent>());
                        if (newAgent != null)
                        {
                            foreach (Agent selItem in newAgent)
                            {
                                ChainAdvanced<Depatment, Agent> link = new ChainAdvanced<Depatment, Agent>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };

                                try
                                {
                                    link.Save();
                                    ChainValueView view = ChainValueView.ConvertToView<Depatment, Agent>(link);
                                    _sourceBindAgents.Add(view);
                                }
                                catch (DatabaseException dbe)
                                {
                                    Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                        SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                             "Создание новой связи невозможно!", dbe.Message, dbe.Id);
                                }
                                catch (Exception ex)
                                {
                                    XtraMessageBox.Show(ex.Message,
                                        SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    };
                }
                btnValueCreate.DropDownControl = mnuTemplates;

                #endregion
                #region Быстрое сообщение
                BarButtonItem btnFastNew = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                btnFastNew.SuperTip = UIHelper.CreateSuperToolTip(btnFastNew.Glyph, btnFastNew.Caption, "Добавить сотрудника");
                groupLinksAction.ItemLinks.Add(btnFastNew);
                btnFastNew.ItemClick += delegate
                {
                    ChainKind objectTml = collectionTemplates.First();
                    Agent tmlMsg = SelectedItem.Workarea.GetTemplates<Agent>().First(
                        s => s.KindValue == Agent.KINDVALUE_PEOPLE);
                    Agent selItem = SelectedItem.Workarea.CreateNewObject<Agent>(tmlMsg);
                    //selItem.UserOwnerId = SelectedItem.Workarea.CurrentUser.Id;
                    //selItem.UserToId = SelectedItem.Workarea.CurrentUser.Id;
                    //selItem.StartPlanDate = DateTime.Today;
                    //selItem.StartPlanTime = DateTime.Now.TimeOfDay;

                    selItem.Saved += delegate
                    {
                        Hierarchy h =
                            SelectedItem.Workarea.Cashe.GetCasheData
                                <Hierarchy>().ItemCode<Hierarchy>(
                                    Hierarchy.SYSTEM_AGENT_MYWORKERS);
                        h.ContentAdd<Agent>(selItem);
                        ChainAdvanced<Depatment, Agent> link = new ChainAdvanced<Depatment, Agent>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };

                        try
                        {
                            link.Save();
                            ChainValueView view = ChainValueView.ConvertToView<Depatment, Agent>(link);
                            _sourceBindAgents.Add(view);
                        }
                        catch (DatabaseException dbe)
                        {
                            Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                     "Создание новой связи невозможно!", dbe.Message, dbe.Id);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    selItem.ShowProperty();
                };
                #endregion
                #region Изменить
                BarButtonItem btnValueEdit = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueEdit);
                btnValueEdit.ItemClick += delegate
                {
                    ChainValueView rowValue = _sourceBindAgents.Current as ChainValueView;
                    if (rowValue == null) return;

                    ChainAdvanced<Depatment, Agent> value = ChainValueView.ConvertToValue<Depatment, Agent>(SelectedItem, rowValue);
                    //new CodeValue<Product> {Workarea = SelectedItem.Workarea, Element = SelectedItem};
                    value.Load(rowValue.Id);
                    value.ShowProperty();

                };
                #endregion
                #region Навигация
                BarButtonItem btnNavigate = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PREVIEW, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PREVIEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnNavigate);
                btnNavigate.ItemClick += delegate
                {

                    ChainValueView currentObject = _sourceBindAgents.Current as ChainValueView;

                    if (currentObject != null)
                    {
                        ChainAdvanced<Depatment, Agent> value = ChainValueView.ConvertToValue<Depatment, Agent>(SelectedItem, currentObject);
                        if (value != null)
                        {
                            value.Right.ShowProperty();
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
                    ChainValueView currentObject = _sourceBindAgents.Current as ChainValueView;
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
                                ChainAdvanced<Depatment, Agent> value = ChainValueView.ConvertToValue<Depatment, Agent>(SelectedItem, currentObject);
                                value.Remove();
                                _sourceBindAgents.Remove(currentObject);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                ChainAdvanced<Depatment, Agent> value = ChainValueView.ConvertToValue<Depatment, Agent>(SelectedItem, currentObject);
                                value.Delete();
                                _sourceBindAgents.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                         "Ошибка удаления связи!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion
                page.Groups.Add(groupLinksAction);
                _controlAgentView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlAgentView.Grid;
            HidePageControls(ExtentionString.CONTROL_LINKWORKER);
        }
        #endregion
    }
}