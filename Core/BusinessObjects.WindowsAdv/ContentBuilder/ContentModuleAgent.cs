using System;
using BusinessObjects.Security;
using DevExpress.XtraLayout;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using System.Collections.Generic;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Drawing;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Корреспонденты"
    /// </summary>
    /// <remarks>
    /// </remarks>
    public sealed class ContentModuleAgent : ContentModuleBase<Agent>
    {
        private TabbedControlGroup _tabbedProperties;

        private ControlAgent _cAgGeneral;
        private ControlList _cBankAccount;
        private ControlList _cContacts;
        private ControlList _cLinks;
        private ControlList _cActLinks;
        private ControlList _cHierarchy;
        private ControlList _cFacts;
        private ControlState _cStates;
        private ControlId _cIds;

        private NavBarGroup _structureGroup;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Корреспонденты".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleAgent()
        {
            Caption = "Корреспонденты";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModuleCreateTreeList;
        }
        /// <summary>
        /// Установка изображения размером в 32 pix
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.AGENT_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Agent> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Agent> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Agent> _saveAgent;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Agent value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                {
                    int position = BrowserBaseObjects.BindingSource.Add(value);
                    BrowserBaseObjects.BindingSource.Position = position;
                };
            }
        }
        /// <summary>
        /// Реализация метода отображения свойств объекта при просмотре в виде групп
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowPropTreeList(Agent value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                {
                    TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);
                    if (!TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                    {
                        int position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                    }
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position =
                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(value);

                };
            }
        }
        /// <summary>
        /// Реализация метода сохранения объекта
        /// </summary>
        /// <param name="value">Объект для сохранения</param>
        void OnSaveObject(Agent value)
        {
            //value.Save();
        }

        void ContentModuleCreateTreeList(object sender, EventArgs e)
        {
            if (btnCommon != null) btnCommon.Visibility = BarItemVisibility.Always;
            TreeListBrowser.ListBrowserBaseObjects.GridView.SelectionChanged += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue == null) return;
                if (btnCommon != null && btnCommon.Checked)
                        TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                UpdateProperties();
            };

            TreeListBrowser.ListBrowserBaseObjects.GridView.DataSourceChanged += delegate
            {
                TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                TreeListBrowser.ListBrowserBaseObjects.SelectedValues.Clear();
            };

            #region Показать структуру ... (Ribbon)
            BarButtonItem btnStructure = new BarButtonItem
                                             {
                                                 Caption = "Структура",
                                                 RibbonStyle = RibbonItemStyles.Large,
                                                 Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.STRUCTUREVIEW_X32)
                                             };
            btnStructure.SuperTip = UIHelper.CreateSuperToolTip(btnStructure.Glyph, "Структура",
                "Вызывает окно настройки структуры текущего корреспондента. К примеру для предприятия можно указать его подразделения и сотрудников");
            if (btnCommon != null) groupLinksActionTreeList.ItemLinks.Insert(btnCommon.Links[0], btnStructure);
            btnStructure.ItemClick += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
                    TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue.BrowseStructure();
            };
            #endregion

            #region Поиск по иерархиям ... (Ribbon)
            if (SecureLibrary.IsAllow(UserRightElement.UIFIND, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
            {
                BindingSource bindResult = new BindingSource();
                BarButtonItem btnFind = new BarButtonItem
                                            {
                                                Caption = "Поиск\nкорреспондентов",
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SEARCHAGENT_X32)
                                            };
                groupLinksActionTreeList.ItemLinks.Add(btnFind);
                btnFind.ButtonStyle = BarButtonStyle.DropDown;
                btnFind.SuperTip = UIHelper.CreateSuperToolTip(btnFind.Glyph, "Поиск по корреспондентам",
                        "Осуществляет быстрый поиск по корреспондентам.");
                btnFind.ActAsDropDown = true;
                PopupControlContainer containerFind = new PopupControlContainer
                                                          {
                                                              CloseOnLostFocus = false,
                                                              ShowCloseButton = true,
                                                              ShowSizeGrip = true,
                                                              Ribbon = groupLinksActionTreeList.Ribbon
                                                          };
                ControlFindHierarchys ctlFindA = new ControlFindHierarchys();
                //ctlFindA.cbSearchInCurrHierarchy.Enabled = false;
                containerFind.Controls.Add(ctlFindA);
                ctlFindA.Dock = DockStyle.Fill;
                containerFind.FormMinimumSize = new Size(ctlFindA.MinimumSize.Width, ctlFindA.MinimumSize.Height + 20);
                containerFind.MinimumSize = new Size(ctlFindA.MinimumSize.Width, ctlFindA.MinimumSize.Height);
                btnFind.DropDownControl = containerFind;
                ctlFindA.btnFind.Click += delegate
                {
                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    string name = ctlFindA.txtName.Text.ToUpper();
                    string code = ctlFindA.txtCode.Text.ToUpper();
                    string memo = ctlFindA.txtMemo.Text.ToUpper();
                    bindResult.Clear();
                    List<Agent> collection = null;
                    if (ctlFindA.cbSearchInCurrHierarchy.Checked && TreeListBrowser.TreeBrowser.SelectedHierarchy != null && TreeListBrowser.TreeBrowser.SelectedHierarchy.ParentId != 0)
                    {
                        collection = new List<Agent>();

                        foreach (HierarchyContent c in TreeListBrowser.TreeBrowser.SelectedHierarchy.Contents)
                        {
                            Agent ag = new Agent { Workarea = Workarea };
                            ag.Load(c.ElementId);
                            collection.Add(ag);
                        }
                    }
                    else
                        collection = Workarea.GetCollection<Agent>();
                    foreach (Agent ag in collection)
                    {
                        if (ctlFindA.radioAndOr.SelectedIndex == 0)
                        {
                            if ((name.Length > 0 && ag.Name != null && ag.Name.ToUpper().Contains(name)) ||
                                (code.Length > 0 && ag.Code != null && ag.Code.ToUpper().Contains(code)) ||
                                (memo.Length > 0 && ag.Memo != null && ag.Memo.ToUpper().Contains(memo)))
                            {
                                bindResult.Add(ag);
                            }
                        }
                        else
                        {
                            if (((name.Length > 0 && ag.Name != null && ag.Name.ToUpper().Contains(name)) || name.Length == 0) &&
                                ((code.Length > 0 && ag.Code != null && ag.Code.ToUpper().Contains(code)) || code.Length == 0) &&
                                ((memo.Length > 0 && ag.Memo != null && ag.Memo.ToUpper().Contains(memo)) || memo.Length == 0))
                            {
                                bindResult.Add(ag);
                            }
                        }
                    }
                    ctlFindA.gridFindResult.DataSource = bindResult;
                    Cursor.Current = currentCursor;
                };
                ctlFindA.gridViewFindResult.CustomUnboundColumnData += delegate(object _sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs _e)
                {
                    if (_e.Column.FieldName != "Image" || !_e.IsGetData || bindResult.Count <= 0) return;
                    Agent imageItem = bindResult[_e.ListSourceRowIndex] as Agent;
                    if (imageItem != null)
                    {
                        _e.Value = imageItem.GetImage();
                    }
                };
                ctlFindA.gridFindResult.DoubleClick += delegate
                {
                    if (ctlFindA.gridViewFindResult.GetSelectedRows().Length <= 0) return;
                    Agent selA = bindResult[ctlFindA.gridViewFindResult.GetSelectedRows()[0]] as Agent;
                    TreeListBrowser.JumpOnObject(selA);
                };
                ctlFindA.txtName.KeyPress += delegate(object _sender, KeyPressEventArgs _e)
                {
                    if (_e.KeyChar == Convert.ToChar(13))
                        ctlFindA.btnFind.PerformClick();
                };
                ctlFindA.txtMemo.KeyPress += delegate(object _sender, KeyPressEventArgs _e)
                {
                    if (_e.KeyChar == Convert.ToChar(13))
                        ctlFindA.btnFind.PerformClick();
                };
                ctlFindA.txtCode.KeyPress += delegate(object _sender, KeyPressEventArgs _e)
                {
                    if (_e.KeyChar == Convert.ToChar(13))
                        ctlFindA.btnFind.PerformClick();
                };
            }
            #endregion

            #region Показать структуру ... (NavBar)
            NavBarGroup ActionGroup = TreeListBrowser._control.ActionsBar.Groups["ActionGroup"];
            NavBarItem ActionStructShow = new NavBarItem
                                              {
                                                  SmallImage = ResourceImage.GetByCode(Workarea, ResourceImage.STRUCTUREVIEW_X16),
                                                  Caption = "Показать структуру ..."
                                              };
            ActionGroup.ItemLinks.Add(ActionStructShow);
            ActionStructShow.LinkClicked += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
                    TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue.BrowseStructure();
            };
            
            #endregion

            #region Обзор структуры
            _structureGroup = new NavBarGroup()
            {
                GroupStyle = NavBarGroupStyle.ControlContainer,
                GroupClientHeight = 200,
                Expanded = false
            };
            TreeListBrowser._control.ActionsBar.Groups.Insert(1, _structureGroup);
            _structureGroup.Caption = "Структура";
            _structureGroup.ItemChanged += delegate
            {
                UpdateProperties();
            };
            _cActLinks = new ControlList();
            _structureGroup.ControlContainer = new NavBarGroupControlContainer();
            _structureGroup.ControlContainer.Controls.Add(_cActLinks);
            _cActLinks.Dock = DockStyle.Fill;
            #endregion

            #region Формирование вкладок
            LayoutControlGroup GroupGeneral = new LayoutControlGroup {Text = "Общие", Name = "GroupGeneral"};

            LayoutControlGroup GroupAccoutingCounts = new LayoutControlGroup {Text = "Расчетные счета", Name = "GroupAccoutingCounts"};

            LayoutControlGroup GroupContacts = new LayoutControlGroup {Text = "Контакты", Name = "GroupContacts"};

            LayoutControlGroup GroupRelations = new LayoutControlGroup {Text = "Связи", Name = "GroupRelations"};

            LayoutControlGroup GroupGroups = new LayoutControlGroup {Text = "Группы", Name = "GroupGroups"};

            LayoutControlGroup GroupFacts = new LayoutControlGroup {Text = "Факты", Name = "GroupFacts"};

            LayoutControlGroup GroupConditions = new LayoutControlGroup {Text = "Состояния", Name = "GroupConditions"};

            LayoutControlGroup GroupIds = new LayoutControlGroup {Text = "Идентификаторы", Name = "GroupIds"};

            _tabbedProperties = new TabbedControlGroup();
            _tabbedProperties.AddTabPage(GroupGeneral);
            _tabbedProperties.AddTabPage(GroupAccoutingCounts);
            _tabbedProperties.AddTabPage(GroupContacts);
            _tabbedProperties.AddTabPage(GroupRelations);
            _tabbedProperties.AddTabPage(GroupGroups);
            _tabbedProperties.AddTabPage(GroupFacts);
            _tabbedProperties.AddTabPage(GroupConditions);
            _tabbedProperties.AddTabPage(GroupIds);
            _tabbedProperties.SelectedTabPage = GroupGeneral;
            _tabbedProperties.SelectedPageChanged += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
                    UpdateProperties();
            };

            LayoutControlGroup groupLayout = new LayoutControlGroup();
            groupLayout.Add(_tabbedProperties);
            groupLayout.TextVisible = false;
            groupLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(0);

            LayoutControl baseLayout = new LayoutControl();
            baseLayout.Dock = DockStyle.Fill;
            baseLayout.Root = groupLayout;
            TreeListBrowser._control.SplitProperyListControl.Panel2.Controls.Add(baseLayout);
            #endregion

            #region Подготовка общих свойств
            LayoutControlItem layGeneral = new LayoutControlItem();
            _cAgGeneral = new ControlAgent();
            _cAgGeneral.Dock = DockStyle.Fill;
            _cAgGeneral.Enabled = false;
            layGeneral.Control = _cAgGeneral;
            layGeneral.Text = " ";
            GroupGeneral.Add(layGeneral);
            #endregion

            #region Подготовка расчетных счетов
            LayoutControlItem layBankAcount = new LayoutControlItem();
            _cBankAccount = new ControlList();
            _cBankAccount.Dock = DockStyle.Fill;
            _cBankAccount.Grid.DoubleClick += delegate
            {
                BindingSource values = (BindingSource)_cBankAccount.Grid.DataSource;
                AgentBankAccount aba = (AgentBankAccount)values.Current;
                if (aba != null)
                {
                    aba.ShowProperty();
                }
            };
            layBankAcount.Control = _cBankAccount;
            layBankAcount.Text = " ";
            GroupAccoutingCounts.Add(layBankAcount);
            #endregion

            #region Подготовка контактов
            LayoutControlItem layContacts = new LayoutControlItem();
            _cContacts = new ControlList();
            _cContacts.Dock = DockStyle.Fill;
            layContacts.Control = _cContacts;
            layContacts.Text = " ";
            GroupContacts.Add(layContacts);
            #endregion

            #region Подготовка связей
            LayoutControlItem layLinks = new LayoutControlItem();
            _cLinks = new ControlList();
            _cLinks.Dock = DockStyle.Fill;
            _cLinks.Grid.DoubleClick += delegate
            {
                BindingSource values = (BindingSource)_cLinks.Grid.DataSource;
                if (values.Current != null)
                    (values.Current as Chain<Agent>).ShowProperty();
            };
            layLinks.Control = _cLinks;
            layLinks.Text = " ";
            GroupRelations.Add(layLinks);
            #endregion

            #region Подготовка групп
            LayoutControlItem layHierarchy = new LayoutControlItem();
            layHierarchy.Name = Guid.NewGuid().ToString();
            _cHierarchy = new ControlList();
            _cHierarchy.Name = Guid.NewGuid().ToString();
            _cHierarchy.Dock = DockStyle.Fill;
            layHierarchy.Control = _cHierarchy;
            layHierarchy.Text = " ";
            GroupGroups.Add(layHierarchy);
            #endregion

            #region Подготовка фактов
            LayoutControlItem layFacts = new LayoutControlItem();
            _cFacts = new ControlList();
            _cFacts.Dock = DockStyle.Fill;
            // Колонки отображаемые в фактах:
            // Наименование факта
            DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Name = "colName",
                Caption = "Наименование",
                Width = 150,
                VisibleIndex = 1,
                Visible = true,
                FieldName = "FactName"

            };
            col.GroupIndex = 0;
            _cFacts.View.Columns.Add(col);
            // Наименование колонки
            col = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Name = "colColumnName",
                Caption = "Колонка",
                Width = 150,
                VisibleIndex = 2,
                Visible = true,
                FieldName = "ColumnName"

            };
            col.GroupIndex = 1;
            _cFacts.View.Columns.Add(col);
            // Дата
            col = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Name = "colDate",
                Caption = "Дата",
                Width = 150,
                VisibleIndex = 3,
                Visible = true,
                FieldName = "ActualDate"
            };
            _cFacts.View.Columns.Add(col);
            // Значение
            col = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Name = "colValue",
                Caption = "Значение",
                Width = 150,
                VisibleIndex = 4,
                Visible = true,
                FieldName = "Value"
            };
            _cFacts.View.Columns.Add(col);
            layFacts.Control = _cFacts;
            layFacts.Text = " ";
            GroupFacts.Add(layFacts);
            #endregion

            #region Подготовка состояний
            LayoutControlItem layStates = new LayoutControlItem();
            _cStates = new ControlState();
            _cStates.Dock = DockStyle.Fill;
            _cStates.Enabled = false;
            layStates.Control = _cStates;
            layStates.Text = " ";
            GroupConditions.Add(layStates);
            #endregion

            #region Подготовка идентификаторов
            LayoutControlItem layIds = new LayoutControlItem();
            _cIds = new ControlId();
            _cIds.Dock = DockStyle.Fill;
            _cIds.Enabled = false;
            layIds.Control = _cIds;
            layIds.Text = " ";
            GroupIds.Add(layIds);
            #endregion
        }
        /// <summary>
        /// Обработчик события отображения модуля
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            //this.groupLinksView.Visible = false;
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = OnShowProp;
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser == null) return;
            if (_showPropTreeList == null)
            {
                _showPropTreeList = OnShowPropTreeList;
                TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
            }
            if (_saveAgent != null) return;
            _saveAgent = OnSaveObject;
            TreeListBrowser.ListBrowserBaseObjects.Save += _saveAgent;
        }

        private void UpdateProperties()
        {
            Agent ag = TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;

            if (ag == null) return;
            if (_tabbedProperties == null) return;
            switch (_tabbedProperties.SelectedTabPage.Name)
            {
                case "GroupGeneral":
                    #region Заполнение общих свойств
                    Company cp = ag.Company;
                    _cAgGeneral.txtCode.Text = ag.Code;
                    _cAgGeneral.txtCodeFind.Text = ag.CodeFind;
                    _cAgGeneral.txtCodeTax.Text = ag.CodeTax;
                    _cAgGeneral.txtMemo.Text = ag.Memo;
                    _cAgGeneral.txtName.Text = ag.Name;
                    _cAgGeneral.txtOkpo.Text = cp.Okpo;
                    #endregion
                    break;
                case "GroupAccoutingCounts":
                    #region Заполнение расчетных считов
                    BindingSource valuesCollectinBind = new BindingSource {DataSource = ag.BankAccounts};
                    _cBankAccount.Grid.DataSource = valuesCollectinBind;
                    DataGridViewHelper.GenerateGridColumns(ag.Workarea, _cBankAccount.View, "DEFAULT_GRID_BANKACCOUNTS");
                    _cBankAccount.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                                                            {
                                                                if (e.Column.Name == "colImage")
                                                                {
                                                                    Rectangle r = e.Bounds;
                                                                    Image img = ResourceImage.GetByCode(Workarea, ResourceImage.MONEY_X16);
                                                                    e.Graphics.DrawImageUnscaled(img, r);
                                                                    e.Handled = true;
                                                                }
                                                                if (e.Column.Name != "colStateImage") return;
                                                                if (valuesCollectinBind.Count - 1 >= _cBankAccount.View.GetDataSourceRowIndex(e.RowHandle))
                                                                {
                                                                    Rectangle r = e.Bounds;
                                                                    int index = _cBankAccount.View.GetDataSourceRowIndex(e.RowHandle);
                                                                    AgentBankAccount v = (AgentBankAccount)valuesCollectinBind[index];
                                                                    Image img = v.State.GetImage();
                                                                    e.Graphics.DrawImageUnscaled(img, r);
                                                                    e.Handled = true;
                                                                }
                                                            };
                    #endregion
                    break;
                case "GroupContacts":
                    #region Заполнение контактов
                    BindingSource contactsBind = new BindingSource();
                    RelationHelper<Agent, Contact> relation = new RelationHelper<Agent, Contact>();
                    List<Contact> list = relation.GetListObject(ag);
                    DataGridViewHelper.GenerateGridColumns(Workarea, _cContacts.View, "DEFAULT_LISTVIEWAGENTCONTACT");
                    _cContacts.Grid.DataSource = contactsBind;
                    contactsBind.DataSource = list;
                    #endregion
                    break;
                case "GroupRelations":
                    #region Заполнение связей
                    _cLinks.Grid.DataSource = null;
                    BindingSource collectionBind = new BindingSource();
                    List<IChain<Agent>> collection = (ag as IChains<Agent>).GetLinks();
                    collectionBind.DataSource = collection;
                    DataGridViewHelper.GenerateGridColumns(ag.Workarea, _cLinks.View, "DEFAULT_LISTVIEWCHAIN");
                    _cLinks.Grid.DataSource = collectionBind;
                    _cLinks.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                                                      {
                                                          if (e.Column.Name == "colImage")
                                                          {
                                                              Rectangle r = e.Bounds;
                                                              Image img = ResourceImage.GetByCode(Workarea, ResourceImage.LINK_X16);
                                                              e.Graphics.DrawImageUnscaledAndClipped(img, r);
                                                              e.Handled = true;
                                                          }
                                                      };
                    TreeListBrowser.Control.Refresh();
                    #endregion
                    break;
                case "GroupGroups":
                    #region Заполнение групп
                    BindingSource hierarchySource = new BindingSource();
                    Hierarchy h = new Hierarchy { Workarea = ag.Workarea };
                    List<Hierarchy> h_list = h.Hierarchies(ag);
                    hierarchySource.DataSource = h_list;
                    // TODO: Исправить представление для групп
                    DataGridViewHelper.GenerateGridColumns(ag.Workarea, _cHierarchy.View, "DEFAULT_LOOKUP_NAME");
                    _cHierarchy.Grid.DataSource = hierarchySource;
                    #endregion
                    break;
                case "GroupFacts":
                    #region Заполнение фактов
                    BindingSource sourceBindFacts = new BindingSource();
                    List<FactView> collFactView = ag.GetCollectionFactView();
                        //ag.Workarea.GetCollectionFactView(ag.Id,
                                                  //                                  ag.EntityId);
                    sourceBindFacts.DataSource = collFactView;
                    _cFacts.Grid.DataSource = sourceBindFacts;
                    _cFacts.View.ExpandAllGroups();
                    #endregion
                    break;
                case "GroupConditions":
                    #region Заполнение состояний
                    BindingSource stateBindings = new BindingSource { DataSource = ag.Workarea.CollectionStates };
                    _cStates.cmbState.Properties.DataSource = stateBindings;
                    _cStates.cmbState.EditValue = ag.State;

                    #region Заполнение списка флагов
                    _cStates.chlFlags.Items.Clear();
                    foreach (FlagValue flagItem in ag.Entity.FlagValues)
                    {
                        int index = _cStates.chlFlags.Items.Add(flagItem.Name);
                        _cStates.chlFlags.Items[index].Description = flagItem.Name;
                        _cStates.chlFlags.Items[index].Value = flagItem.Value;
                        CheckState status = CheckState.Unchecked;
                        if ((flagItem.Value & ag.FlagsValue) == flagItem.Value)
                            status = CheckState.Checked;
                        _cStates.chlFlags.Items[index].CheckState = status;
                    }
                    #endregion

                    #region Заполнение списка типов
                    // TODO: Заполнение списка типов
                    //_cStates.chkKinds.Items.Clear();
                    //foreach (EntityKind kindItem in ag.Entity.EntityKinds.Where(s => s.SubKind < ag.Entity.MaxKind + 1))
                    //{
                    //    int index = _cStates.chkKinds.Items.Add(kindItem.ToString());
                    //    _cStates.chkKinds.Items[index].Description = kindItem.ToString();
                    //    _cStates.chkKinds.Items[index].Value = kindItem.SubKind;
                    //    CheckState status = CheckState.Unchecked;
                    //    if ((kindItem.SubKind & ag.KindValue) == kindItem.SubKind)
                    //        status = CheckState.Checked;
                    //    _cStates.chkKinds.Items[index].CheckState = status;
                    //}

                    #endregion

                    #endregion
                    break;
                case "GroupIds":
                    #region Заполнение идентификаторов
                    _cIds.txtId.Text = ag.Id.ToString();
                    _cIds.txtGuid.Text = ((IBase)ag).Guid.ToString();
                    _cIds.txtBranchId.Text = ((IBase)ag).DatabaseId.ToString();
                    _cIds.txtSourceId.Text = ((IBase)ag).DbSourceId.ToString();
                    _cIds.txtKind.Text = ag.KindValue.ToString();
                    #endregion
                    break;
            }
            #region Заполнение структуры в NavBar
            if (_structureGroup.Expanded)
            {
                _cActLinks.Grid.DataSource = null;
                BindingSource collectionBind = new BindingSource();
                List<IChain<Agent>> collection = (ag as IChains<Agent>).GetLinks();
                collectionBind.DataSource = collection;
                DataGridViewHelper.GenerateGridColumns(ag.Workarea, _cActLinks.View, "DEFAULT_LISTVIEWCHAIN");
                _cActLinks.Grid.DataSource = collectionBind;
                _cActLinks.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                                                     {
                                                         if (e.Column.Name == "colImage")
                                                         {
                                                             Rectangle r = e.Bounds;
                                                             Image img = ResourceImage.GetByCode(Workarea, ResourceImage.LINK_X16);
                                                             e.Graphics.DrawImageUnscaledAndClipped(img, r);
                                                             e.Handled = true;
                                                         }
                                                     };
                _cActLinks.View.ExpandAllGroups();
                TreeListBrowser.Control.Refresh();
            }
            #endregion
        }
    }
}