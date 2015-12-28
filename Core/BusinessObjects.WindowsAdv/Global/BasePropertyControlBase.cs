using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xaml;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using BusinessObjects.Windows.Workflows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    public class TextMeasurer 
    {
        private readonly Image _fakeImage;
        private readonly Graphics _graphics;

        public TextMeasurer()
        {
            _fakeImage = new Bitmap(1, 1);
            _graphics = Graphics.FromImage(_fakeImage);
        }

        public SizeF MeasureString(string text, Font font)
        {
            return _graphics.MeasureString(text, font);
        }
    }

    internal interface IBasePropertyControlICore<T> where T : class, ICoreObject, new()
    {
        Browse<T> OnBrowse { get; set; }

        /// <summary>Активная страница</summary>
        string ActivePage { get; set; }

        /// <summary>Временный контрол свойств</summary>
        Control Control { get; set; }

        /// <summary>Текущий элемент</summary>
        T SelectedItem { get; set; }

        Control Owner { get; set; }

        void Save();

        void Build();
        bool CanClose { get; set; }

        Dictionary<string, string> TotalPages { get; set; }
    }

    internal abstract class BasePropertyControlIBase<T> : BasePropertyControlICore<T> where T : class, IBase,  new()
    {
        protected BasePropertyControlIBase()
            : base()
        {

        }
        public override void Build()
        {
            base.Build();
            frmProp.btnActions.Visibility = BarItemVisibility.Always;

            frmProp.btnActions.Visibility = BarItemVisibility.Always;
            BarItemLink lnk = frmProp.ActionMenu.AddItem(new BarButtonItem { Caption = "Пользовательские флаги" });
            lnk.Item.ItemClick += delegate
            {
                IFlagString v = (SelectedItem as IFlagString);
                v.ShowFlagString();
            };
            Library libWindow = UIHelper.FindWindow<T>(SelectedItem);
            if(libWindow!=null)
            {
                // Определяем текущие процессы
                List<ChainValueView> list = ((IChainsAdvancedList<Library, Ruleset>) libWindow).GetChainView();
                foreach (ChainValueView view in list)
                {
                    BarItemLink lnkWF = frmProp.ActionMenu.AddItem(new BarButtonItem { Caption = view.RightName });
                    lnkWF.Item.Tag = view;
                    lnkWF.Item.ItemClick += delegate
                                                {
                                                    ChainValueView clickedView = lnkWF.Item.Tag as ChainValueView;
                                                    Ruleset process = SelectedItem.Workarea.Cashe.GetCasheData<Ruleset>().Item(
                                                        clickedView.RightId);
                                                    Activity value = null;
                                                    value = WfCore.FindByCodeInternal(process.Code);
                                                    if(value==null)
                                                        value = WfCore.FindByCodeInternalIBase<T>(process.Code);
                                                    if(value==null)
                                                        value = ActivityXamlServices.Load(ActivityXamlServices.CreateReader(new XamlXmlReader(process.ValueToStream(), new XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() })));

                                                    IDictionary<string, object> outputs = WorkflowInvoker.Invoke(value, new Dictionary<string, object>
                                                                                        {
                                                                                            {"CurrentObject", SelectedItem}
                                                                                        });
                                                };
                }
            }

        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if (SelectedItem == null) return;
            bool res = SelectedItem.Workarea.Empty<FactName>().HasFactNames(SelectedItem.EntityId);
            if (!res)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_FACT_NAME))
                    TotalPages.Remove(ExtentionString.CONTROL_FACT_NAME);
            }

            res = SelectedItem.Workarea.CollectionChainKinds.Exists(f => f.FromEntityId == SelectedItem.EntityId);
            if(!res)
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_LINK_NAME))
                    TotalPages.Remove(ExtentionString.CONTROL_LINK_NAME);

        }
        protected BrowseChainObject<T> OnBrowseChain { get; set; }
        //List<T> BrowseListType<T>(this T value, Predicate<T> filter, List<T> sourceCollection)
        /// <summary>
        /// Метод построения страницы свойств по коду страницы
        /// </summary>
        /// <param name="value">Код страницы</param>
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            switch (value)
            {
                case ExtentionString.CONTROL_FACT_NAME:
                    BuildPageFact();
                    break;
                case ExtentionString.CONTROL_LINK_NAME:
                    BuildPageLink();
                    break;
                case ExtentionString.CONTROL_HIERARCHIES_NAME:
                    BuildPageHierarchy();
                    break;
                case ExtentionString.CONTROL_CODES:
                    BuildPageCodes();
                    break;
                case ExtentionString.CONTROL_KNOWLEDGES:
                    BuildPageKnowledges();
                    break;
                case ExtentionString.CONTROL_NOTES:
                    BuildPageNotes();
                    break;
            }
        }

        Controls.ControlList _controlHierarchy;
        /// <summary>
        /// Подготовка страницы "Иерархии"
        /// </summary>
        /// <returns></returns>
        protected virtual void BuildPageHierarchy()
        {
            if(_controlHierarchy==null)
            {
                Hierarchy h = new Hierarchy { Workarea = SelectedItem.Workarea };
                ListBrowserBaseObjects<Hierarchy> browserBaseObjects = new ListBrowserBaseObjects<Hierarchy>(SelectedItem.Workarea,
                                                                            h.Hierarchies(SelectedItem),
                                                                            null, null, true, false,
                                                                            false, true);
                browserBaseObjects.Build();
                _controlHierarchy = browserBaseObjects.ListControl;
                _controlHierarchy.Name = ExtentionString.CONTROL_HIERARCHIES_NAME;

                // Построение группы упраления связями
                RibbonPage page =
                    frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_HIERARCHIES_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Добавить
                BarButtonItem btnCreate = new BarButtonItem
                                              {
                                                  ButtonStyle = BarButtonStyle.Default,
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = Properties.Resources.CREATE_X32
                                              };
                groupLinksAction.ItemLinks.Add(btnCreate);
                btnCreate.ItemClick += delegate
                                           {
                                               Hierarchy rootHierarchy = SelectedItem.Workarea.GetCollection<Hierarchy>().FirstOrDefault(
                                                   s => s.ParentId == 0 && s.ContentEntityId == SelectedItem.EntityId);
                                               Hierarchy selectedHierarchy = rootHierarchy.BrowseTree(SelectedItem);
                                               if (selectedHierarchy != null)
                                               {
                                                   selectedHierarchy.ContentAdd(SelectedItem);
                                                   browserBaseObjects.BindingSource.Add(selectedHierarchy);
                                               }
                                           };
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
                    browserBaseObjects.InvokeProperties();
                };

                #endregion
                #region Удаление
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EXCLUDE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                btnDelete.ItemClick += delegate
                {
                    if (browserBaseObjects.FirstSelectedValue == null) return;
                    browserBaseObjects.FirstSelectedValue.ContentRemove(SelectedItem);
                };
                #endregion
                page.Groups.Add(groupLinksAction);

                Control.Controls.Add(_controlHierarchy);
                _controlHierarchy.Dock = DockStyle.Fill;


            }
            CurrentPrintControl = _controlHierarchy.Grid;
            HidePageControls(ExtentionString.CONTROL_HIERARCHIES_NAME);
        }
        /// <summary>
        /// Подготовка страницы "Состояния"
        /// </summary>
        /// <returns></returns>
        protected override void BuildPageState()
        {
            if(ControlState==null)
            {
                ControlState = new ControlState {Name = ExtentionString.CONTROL_STATES_NAME};
                Control.Controls.Add(ControlState);
                ControlState.Dock = DockStyle.Fill;
                BindingSource stateBindings = new BindingSource { DataSource = SelectedItem.Workarea.CollectionStates };
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, ControlState.cmbState, "DEFAULT_LOOKUP_NAME");
                
                ControlState.cmbState.Properties.DataSource = stateBindings;
                ControlState.cmbState.EditValue = SelectedItem.State;
                //controlState.cmbState.SelectedIndexChanged += delegate
                //{
                //    SelectedItem.State = controlState.cmbState.SelectedValue as State;
                //};
                #region Заполнение списка флагов
                foreach (FlagValue flagItem in SelectedItem.Entity.FlagValues)
                {
                    int index = ControlState.chlFlags.Items.Add(flagItem.Name);
                    ControlState.chlFlags.Items[index].Description = flagItem.Name;
                    ControlState.chlFlags.Items[index].Value = flagItem.Value;
                    CheckState status = CheckState.Unchecked;
                    if ((flagItem.Value & SelectedItem.FlagsValue) == flagItem.Value)
                        status = CheckState.Checked;
                    ControlState.chlFlags.Items[index].CheckState = status;
                }
                #endregion
                #region Заполнение списка типов

                bool IsMultyKind = ((SelectedItem.Entity.FlagsValue & 16) == 16);
                foreach (EntityKind kindItem in SelectedItem.Entity.EntityKinds.Where(s => s.SubKind < SelectedItem.Entity.MaxKind+1))
                {
                    RadioGroupItem rdItem = new RadioGroupItem(kindItem.SubKind, kindItem.ToString());
                    ControlState.chkKinds.Properties.Items.Add(rdItem);
                    //ControlState.chkKinds.Properties.Items[index].Description = kindItem.ToString();
                    //ControlState.chkKinds.Properties.Items[index].Value = kindItem.SubKind;
                    CheckState status = CheckState.Unchecked;
                    if (IsMultyKind)
                    {
                        if ((kindItem.SubKind == SelectedItem.KindValue))
                            status = CheckState.Checked;
                        
                        //rdItem .CheckState = status;
                    }
                    else
                    {
                        if(kindItem.SubKind == SelectedItem.KindValue)
                            status = CheckState.Checked;
                        //ControlState.chkKinds.Properties.Items[index].CheckState = status;
                    }
                }
                ControlState.chkKinds.EditValue = SelectedItem.KindValue;
                TextMeasurer tx = new TextMeasurer();
                SizeF v = tx.MeasureString("Н", ControlState.chkKinds.Font);
                int h = (int)Math.Floor((v.Height +4) * ControlState.chkKinds.Properties.Items.Count +ControlState.chkKinds.Properties.Items.Count *4);
                //Graphics graphics = ControlState.CreateGraphics();
                //SizeF textSize = graphics.MeasureString("H", ControlState.chkKinds.Font);
                //int h = (int)Math.Floor(textSize.Height * ControlState.chkKinds.Properties.Items.Count + +2);
                ControlState.chkKinds.MaximumSize = new Size(0, h );

                #endregion
            }
            CurrentPrintControl = ControlState.LayoutControl;
            HidePageControls(ExtentionString.CONTROL_STATES_NAME);
        }

        protected override void SaveStateData()
        {
            #region Сохранение состояний объекта
            
                if (ControlState!=null)
                {
                    SelectedItem.StateId = ((State)ControlState.cmbState.EditValue).Id;
                    int flagValue = (from CheckedListBoxItem i in ControlState.chlFlags.Items where i.CheckState == CheckState.Checked select (int) i.Value).Sum();
                    SelectedItem.FlagsValue = flagValue;
                    short kind = 0;
                    //foreach (CheckedListBoxItem i in ControlState.chkKinds.Items)
                    //{
                    //    if (i.CheckState == CheckState.Checked)
                    //    {
                    //        kind += (short)i.Value;
                    //    }
                    //}
                    kind = (short)ControlState.chkKinds.EditValue;
                    SelectedItem.KindValue = kind;
                    
                }
            
            #endregion
        }

        private ControlList _controlactValues;
        private BindingSource _sourceBindFacts;
        private List<FactView> _collFactView;
        private IFacts<T> _factItems;
        /// <summary>
        /// Подготовка страницы "Факты"
        /// </summary>
        /// <returns></returns>
        protected virtual void BuildPageFact()
        {
            _factItems = SelectedItem as IFacts<T>;
            if (_factItems == null) return;
            if (_controlactValues == null)
            {
                _controlactValues = new ControlList {Name = ExtentionString.CONTROL_FACT_NAME};
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlactValues.View, "DEFAULT_LISTVIEWFACTVIEW");
                _controlactValues.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlactValues);
                _sourceBindFacts = new BindingSource();
                _collFactView = _factItems.GetCollectionFactView();
                    //SelectedItem.GetCollectionFactView(SelectedItem.Id,SelectedItem.EntityId, SelectedItem.KindId);
                _sourceBindFacts.DataSource = _collFactView;
                _controlactValues.Grid.DataSource = _sourceBindFacts;
                _controlactValues.View.ExpandAllGroups();
                _controlactValues.View.RowClick += ViewRowClick;

                // Построение группы управления фактами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_FACT_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnFactValueCreate = new BarButtonItem
                                                       {
                                                           Caption = "Новое значение",
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                       };
                groupLinksAction.ItemLinks.Add(btnFactValueCreate);
                btnFactValueCreate.ItemClick += delegate
                                                    {
                                                        //FactView factView = new FactView();
                                                        //_sourceBindFacts.Add(factView);
                                                        //_factItems.
                                                        InvokeShowFactProperty(true);
                                                        //FactValue newFactValue = SelectedItem.Workarea.Empty<FactValue>();
                                                        //newFactValue.Workarea = SelectedItem.Workarea;
                                                        //newFactValue.Save();
                                                    };
                #endregion

                #region Изменить значения
                BarButtonItem btnFactValueProperty = new BarButtonItem
                                                         {
                                                             Caption = "Изменить значения",
                                                             RibbonStyle = RibbonItemStyles.Large,
                                                             Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                                         };
                groupLinksAction.ItemLinks.Add(btnFactValueProperty);
                btnFactValueProperty.ItemClick += delegate
                                                      {
                                                          InvokeShowFactProperty(); 
                                                      };
                #endregion

                #region Удалить значение
                BarButtonItem btnFactValueDelete = new BarButtonItem
                                                       {
                                                           Caption = "Удалить значение",
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                                       };
                groupLinksAction.ItemLinks.Add(btnFactValueDelete);
                btnFactValueDelete.ItemClick += delegate
                                                    {
                                                        FactView rowValue = _sourceBindFacts.Current as FactView;
                                                        if (rowValue == null) return;
                                                        if(rowValue.DateId.HasValue)
                                                        {
                                                            FactDate dt = new FactDate { Workarea = SelectedItem.Workarea };
                                                            dt.Load(rowValue.DateId.Value);
                                                            dt.Delete();
                                                            _factItems.RefreshFaсtView();
                                                            _collFactView = _factItems.GetCollectionFactView();
                                                            //collFactView = SelectedItem.Workarea.GetCollectionFactView(SelectedItem.Id,
                                                            //                              SelectedItem.EntityId, SelectedItem.KindId);
                                                            _sourceBindFacts.DataSource = _collFactView;
                                                            _controlactValues.Grid.Refresh();
                                                            _controlactValues.View.ExpandAllGroups();
                                                        }
                                                    };
                #endregion

                #region Показать историю
                BarButtonItem btnFactValueShowHistory = new BarButtonItem
                {
                    Caption = "Показать историю",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PERIOD_X32)
                };
                groupLinksAction.ItemLinks.Add(btnFactValueShowHistory);
                btnFactValueShowHistory.ItemClick += delegate
                {
                    FactView rowValue = _sourceBindFacts.Current as FactView;
                    if (rowValue == null) return;
                    FactValue fv = new FactValue {Workarea = SelectedItem.Workarea};
                    if (rowValue.ValueId == 0)
                    {
                        fv.ColumnId = rowValue.ColumnId;
                        fv.ElementId = SelectedItem.Id;
                        fv.ToEntityId = SelectedItem.EntityId;
                    }
                    fv.Load(rowValue.ValueId);
                    fv.ShowPropertyFactValues();
                };
                #endregion

                page.Groups.Add(groupLinksAction);
                _controlactValues.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlactValues.Grid;
            HidePageControls(ExtentionString.CONTROL_FACT_NAME);
        }

        void ViewRowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks < 2) return;
            if (e.RowHandle < 0) return;
            InvokeShowFactProperty();
        }

        private void InvokeShowFactProperty(bool newValue=false)
        {
            FactView rowValue = _sourceBindFacts.Current as FactView;
            if (rowValue == null) return;
            // если это свойство и rowValue.ValueId=0 - создаем новое
            // если это свойство и rowValue.ValueId<>0 - редактируем текущее
            // если это факт и newValue=true - создаем новое
            // если это факт и (rowValue.ValueId<>0 или newValue=false) - редактируем текущее
            if (((rowValue.FactNameKindValue == 1)&&(newValue))||
                ((rowValue.FactNameKindValue==2)&&(rowValue.ValueId==0)))
            //if (rowValue.ValueId == 0)
            {
                FactValue fv = new FactValue { Workarea = SelectedItem.Workarea };
                FactDate dt = new FactDate
                                  {
                                      Workarea = SelectedItem.Workarea,
                                      ColumnId = rowValue.ColumnId,
                                      StateId = State.STATEACTIVE,
                                      ToEntityId = SelectedItem.EntityId,
                                      ElementId = SelectedItem.Id,
                                      IsNew = true
                                  };
                fv.FactDate = dt;
                dt.ActualDate = fv.FactDate.FactColumn.FactName.KindValue == 2 ? SqlDateTime.MinValue.Value : DateTime.Today;
                Form frm = fv.ShowProperty();
                frm.FormClosed += delegate
                                      {
                                          _factItems.RefreshFaсtView();
                                          _collFactView = _factItems.GetCollectionFactView();
                                          //sourceBindFacts.SuspendBinding();
                                          //collFactView = SelectedItem.Workarea.GetCollectionFactView(SelectedItem.Id,
                                          //                                                           SelectedItem.EntityId, SelectedItem.KindId);
                                          _sourceBindFacts.DataSource = _collFactView;
                                          //_controlactValues.Grid.Refresh();
                                          //sourceBindFacts.ResumeBinding();
                                          _controlactValues.View.ExpandAllGroups();
                                      };
            }
            else
            {
                FactValue fv = null;
                if (rowValue.ValueId != 0)
                {
                    fv = new FactValue {Workarea = SelectedItem.Workarea};
                    fv.Load(rowValue.ValueId);
                }
                else if (rowValue.DateId.HasValue)
                {
                    fv = new FactValue {Workarea = SelectedItem.Workarea};
                    FactDate dt = new FactDate {Workarea = SelectedItem.Workarea};
                    dt.Load(rowValue.DateId.Value);
                    fv.FactDate = dt;
                }
                
                if (fv == null) return;
                Form frm = fv.ShowProperty();
                frm.FormClosed += delegate
                                      {
                                          _factItems.RefreshFaсtView();
                                          _collFactView = _factItems.GetCollectionFactView();
                                          //sourceBindFacts.SuspendBinding();
                                          //collFactView = SelectedItem.Workarea.GetCollectionFactView(SelectedItem.Id,
                                          //                                                           SelectedItem.EntityId,SelectedItem.KindId);
                                          _sourceBindFacts.DataSource = _collFactView;
                                          //sourceBindFacts.ResumeBinding();
                                          _controlactValues.View.ExpandAllGroups();
                                          
                                      };
            }
        }


        internal ControlList _controlLinks;
        /// <summary>
        /// Подготовка страницы "Связи"
        /// </summary>
        /// <returns></returns>
        protected virtual void BuildPageLink()
        {
            if ((SelectedItem as IChains<T>) == null)
                    return;
            if(_controlLinks==null)
            {
                _controlLinks = new ControlList {Name = ExtentionString.CONTROL_LINK_NAME};
                // Данные для отображения в списке связей
                BindingSource collectionBind = new BindingSource();
                List<IChain<T>> collection = (SelectedItem as IChains<T>).GetLinks();
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

                for (int i = collectionTemplates.Count-1; i >-1 ; i--)
                {
                    List<int> typeFrom = collectionTemplates[i].GetContentTypeKindIdFrom();
                    if(typeFrom.Count>0 && !typeFrom.Contains(0))
                    {
                        if (!typeFrom.Contains(SelectedItem.KindId))
                        {
                            collectionTemplates.RemoveAt(i);
                        }
                    }
                }

                PopupMenu mnuTemplates = new PopupMenu {Ribbon = frmProp.ribbon};
                foreach (ChainKind itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem {Caption = itemTml.Name};
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                    {
                        ChainKind objectTml = (ChainKind)btn.Tag;
                        if(OnBrowse==null)
                        {
                            //OnBrowseChain = Extentions.BrowseListType<T>(SelectedItem, s => (s.KindValue & objectTml.EntityContent) == objectTml.EntityContent,
                            //                                             SelectedItem.Workarea.GetCollection<T>());
                            //OnBrowse = Extentions.BrowseListType<T>;
                            OnBrowseChain = Extentions.BrowseListType;
                        }
                        List<ChainKindContentType> allTypes = objectTml.GetContentType();
                        List<int> types = objectTml.GetContentTypeKindId();
                        
                        //types.Contains(s.KindId)
                        
                        //List<T> newAgent = OnBrowseChain.Invoke(SelectedItem, s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<T>(true));
                        List<T> newAgent = OnBrowseChain.Invoke(SelectedItem, s => allTypes.Exists(a => (a.EntityKindIdFrom == SelectedItem.KindId || a.EntityKindIdFrom==0) && s.KindId == a.EntityKindId), SelectedItem.Workarea.GetCollection<T>(true));
                        if (newAgent != null)
                        {
                            foreach (T selItem in newAgent)
                            {
                                Chain<T> link = new Chain<T>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };
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
                                             ((collectionBind.Current) as Chain<T>).ShowProperty();
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
                        IChain<T> currentItem = (IChain<T>)collectionBind.Current;
                        (currentItem as Chain<T>).OrderNo--;
                        try
                        {
                            (currentItem as Chain<T>).Save();
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
                        
                        //IChain<T> currentItem = (IChain<T>)collectionBind.Current;
                        //if (collectionBind.Position - 1 > -1)
                        //{
                        //    IChain<T> prevItem = (IChain<T>)collectionBind[collectionBind.Position - 1];
                        //    IWorkarea wa = ((Chain<T>)currentItem).Workarea;
                        //    try
                        //    {
                        //        wa.Swap((Chain<T>)currentItem, (Chain<T>)prevItem);
                        //        _controlLinks.View.UpdateCurrentRow();
                        //        int indexNext = _controlLinks.View.GetPrevVisibleRow(_controlLinks.View.FocusedRowHandle);
                        //        _controlLinks.View.RefreshRow(indexNext);
                        //    }
                        //    catch (Exception e)
                        //    {
                        //        DevExpress.XtraEditors.XtraMessageBox.Show(e.Message,
                        //            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    }
                        //}
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
                        IChain<T> currentItem = (IChain<T>)collectionBind.Current;
                        (currentItem as Chain<T>).OrderNo++;
                        try
                        {
                            (currentItem as Chain<T>).Save();
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
                        //IChain<T> currentItem = (IChain<T>)collectionBind.Current;
                        //if (collectionBind.Position + 1 < collectionBind.Count)
                        //{
                        //    IChain<T> nextItem = (IChain<T>)collectionBind[collectionBind.Position + 1];
                        //    IWorkarea wa = ((Chain<T>)currentItem).Workarea;
                        //    try
                        //    {
                        //        wa.Swap((Chain<T>)nextItem, (Chain<T>)currentItem);
                        //        _controlLinks.View.UpdateCurrentRow();
                        //        int indexNext = _controlLinks.View.GetNextVisibleRow(_controlLinks.View.FocusedRowHandle);
                        //        _controlLinks.View.RefreshRow(indexNext);
                        //    }
                        //    catch (Exception e)
                        //    {
                        //        DevExpress.XtraEditors.XtraMessageBox.Show(e.Message,
                        //            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    }
                        //}
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
                    Chain<T> currentObject = collectionBind.Current as Chain<T>;
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
                _controlLinks.View.CustomDrawCell +=delegate(object sender, RowCellCustomDrawEventArgs e)
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
                        IChain<T> imageItem = collectionBind[e.ListSourceRowIndex] as IChain<T>;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.Right.GetImage();
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && collectionBind.Count > 0)
                    {
                        IChain<T> imageItem = collectionBind[e.ListSourceRowIndex] as IChain<T>;
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

        #region Дополнительные коды
        private ControlList _controlCodeView;
        private BindingSource _sourceBindCodes;
        private List<CodeValueView> _collCodeView;
        private void BuildPageCodes()
        {
            if ((SelectedItem as ICodes<T>) == null)
                return;
            if (_controlCodeView == null)
            {
                _controlCodeView = new ControlList { Name = ExtentionString.CONTROL_CODES };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlCodeView.View,
                                                       "DEFAULT_LISTVIEWCODEVALUEVIEW");
                _controlCodeView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlCodeView);
                _sourceBindCodes = new BindingSource();
                _collCodeView = (SelectedItem as ICodes<T>).GetView(false);
                _sourceBindCodes.DataSource = _collCodeView;
                _controlCodeView.Grid.DataSource = _sourceBindCodes;
                _controlCodeView.View.ExpandAllGroups();

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_CODES)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnValueCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.DropDown,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueCreate);

                List<CodeName> collectionTemplates = SelectedItem.Workarea.GetCollection<CodeName>().FindAll(f => f.ToEntityId == SelectedItem.EntityId);
                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                foreach (CodeName itemTml in collectionTemplates)
                {
                    List<CodeNameEntityKind> collValues = CodeNameEntityKind.GetCollection(SelectedItem.Workarea, itemTml.Id);
                    if (collValues.Exists(s => s.EntityKindId == SelectedItem.KindId))
                    {
                        BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                        mnuTemplates.AddItem(btn);
                        btn.Tag = itemTml;
                        btn.ItemClick += delegate
                                             {
                                                 if(SelectedItem.Id==0)
                                                 {
                                                     DevExpress.XtraEditors.XtraMessageBox.Show("Требуется сохранить элемент!",
                                                         SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                     return;
                                                 }
                                                 CodeName objectTml = (CodeName)btn.Tag;
                                                 CodeValue<T> newCodeValue = new CodeValue<T>
                                                                                 {
                                                                                     Workarea = SelectedItem.Workarea,
                                                                                     CodeNameId = objectTml.Id,
                                                                                     ElementId = SelectedItem.Id,
                                                                                     Element = SelectedItem
                                                                                 };
                                                 newCodeValue.Saved += delegate
                                                                           {
                                                                               _collCodeView = (SelectedItem as ICodes<T>).GetView(false);
                                                                               _sourceBindCodes.DataSource = _collCodeView;
                                                                               _controlCodeView.Grid.DataSource = _sourceBindCodes;
                                                                               _controlCodeView.View.ExpandAllGroups();   
                                                                           };
                                                 newCodeValue.ShowProperty();
                                                 
                                                                               
                                                 
                                             };
                    }
                }
                btnValueCreate.DropDownControl = mnuTemplates;

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
                    CodeValueView rowValue = _sourceBindCodes.Current as CodeValueView;
                    if (rowValue == null) return;

                    CodeValue<T> value = rowValue.ConvertToCodeValue<T>(SelectedItem);
                    //new CodeValue<Product> {Workarea = SelectedItem.Workarea, Element = SelectedItem};
                    value.Load(rowValue.Id);
                    value.ShowProperty();

                };
                #endregion

                page.Groups.Add(groupLinksAction);
                _controlCodeView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlCodeView.Grid;
            HidePageControls(ExtentionString.CONTROL_CODES);
        } 
        #endregion

        #region Связи со статьями базу зананий
        private ControlList _controlKnowledgeView;
        private BindingSource _sourceBindKnowledges;
        private List<ChainValueView> _collKnowledgeView;
        private void BuildPageKnowledges()
        {
            if ((SelectedItem as IChainsAdvancedList<T, Knowledge>) == null)
                return;
            if (_controlKnowledgeView == null)
            {
                _controlKnowledgeView = new ControlList { Name = ExtentionString.CONTROL_KNOWLEDGES };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlKnowledgeView.View,
                                                       "DEFAULT_LISTVIEWCHAINVALUE");
                _controlKnowledgeView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlKnowledgeView);
                _sourceBindKnowledges = new BindingSource();
                _collKnowledgeView = (SelectedItem as IChainsAdvancedList<T, Knowledge>).GetChainView();
                _sourceBindKnowledges.DataSource = _collKnowledgeView;
                _controlKnowledgeView.Grid.DataSource = _sourceBindKnowledges;
                _controlKnowledgeView.View.ExpandAllGroups();
                _controlKnowledgeView.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                    else if (e.Column.Name== "colStateImage")
                    {
                        ChainValueView rowValue = _sourceBindKnowledges.Current as ChainValueView;
                        if (rowValue == null) return;
                    }
                };
                _controlKnowledgeView.View.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindKnowledges.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindKnowledges[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageKnowledge(imageItem.Workarea, imageItem.RightKind);
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindKnowledges.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindKnowledges[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_KNOWLEDGES)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnValueCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.DropDown,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueCreate);

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Knowledge && f.Code== ChainKind.KNOWLEDGES);
                
                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                foreach (ChainKind itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                    {
                        ChainKind objectTml = (ChainKind)btn.Tag;
                        OnBrowseChain = Extentions.BrowseListType;
                        
                        List<int> types = objectTml.GetContentTypeKindId();
                        //types.Contains(s.KindId)
                        //List<T> newAgent = OnBrowseChain.Invoke(SelectedItem, s => (s.KindValue & objectTml.EntityContent) == objectTml.EntityContent, SelectedItem.Workarea.GetCollection<T>());
                        List<Knowledge> newAgent = Extentions.BrowseListType<Knowledge>(SelectedItem.Workarea.Empty<Knowledge>(),
                                                             s => types.Contains(s.KindId),
                                                             SelectedItem.Workarea.GetCollection<Knowledge>());
                        if (newAgent != null)
                        {
                            foreach (Knowledge selItem in newAgent)
                            {
                                ChainAdvanced<T, Knowledge> link = new ChainAdvanced<T, Knowledge>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };
                                try
                                {
                                    link.Save();
                                    ChainValueView view = ChainValueView.ConvertToView<T, Knowledge>(link);
                                    _sourceBindKnowledges.Add(view);
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
                    //List<CodeNameEntityKind> collValues = CodeNameEntityKind.GetCollection(SelectedItem.Workarea, itemTml.Id);
                    //if (collValues.Exists(s => s.EntityKindId == SelectedItem.KindId))
                    //{
                    //    BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                    //    mnuTemplates.AddItem(btn);
                    //    btn.Tag = itemTml;
                    //    btn.ItemClick += delegate
                    //    {
                    //        CodeName objectTml = (CodeName)btn.Tag;
                    //        CodeValue<T> newCodeValue = new CodeValue<T>
                    //        {
                    //            Workarea = SelectedItem.Workarea,
                    //            CodeNameId = objectTml.Id,
                    //            ElementId = SelectedItem.Id,
                    //            Element = SelectedItem
                    //        };
                    //        newCodeValue.ShowProperty();
                    //    };
                    //}
                }
                btnValueCreate.DropDownControl = mnuTemplates;

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
                    ChainValueView rowValue = _sourceBindKnowledges.Current as ChainValueView;
                    if (rowValue == null) return;

                    ChainAdvanced<T, Knowledge> value = ChainValueView.ConvertToValue<T, Knowledge>(SelectedItem, null, rowValue);
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

                    ChainValueView currentObject = _sourceBindKnowledges.Current as ChainValueView;

                    if (currentObject != null)
                    {
                        ChainAdvanced<T, Knowledge> value = ChainValueView.ConvertToValue<T, Knowledge>(SelectedItem, null, currentObject);
                        if (value != null)
                        {
                            value.Right.ShowKnowledge();
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
                    ChainValueView currentObject = _sourceBindKnowledges.Current as ChainValueView;
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
                                ChainAdvanced<T, Knowledge> value = ChainValueView.ConvertToValue<T, Knowledge>(SelectedItem, null, currentObject);
                                value.Remove();
                                _sourceBindKnowledges.Remove(currentObject);
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
                                ChainAdvanced<T, Knowledge> value = ChainValueView.ConvertToValue<T, Knowledge>(SelectedItem, null, currentObject);
                                value.Delete();
                                _sourceBindKnowledges.Remove(currentObject);
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
                _controlKnowledgeView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlKnowledgeView.Grid;
            HidePageControls(ExtentionString.CONTROL_KNOWLEDGES);
        }
        #endregion

        #region Связи с пользовательскими примечаниями
        private ControlList _controlNoteView;
        private BindingSource _sourceBindNotes;
        private List<NoteValueView> _collNoteView;
        private void BuildPageNotes()
        {
            if ((SelectedItem as IChainsAdvancedList<T, Note>) == null)
                return;
            if (_controlNoteView == null)
            {
                _controlNoteView = new ControlList { Name = ExtentionString.CONTROL_NOTES };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlNoteView.View,
                                                       "NOTE_VIEW");
                _controlNoteView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlNoteView);
                _sourceBindNotes = new BindingSource();

                _collNoteView = NoteValueView.GetView(SelectedItem, true);
                _sourceBindNotes.DataSource = _collNoteView;
                _controlNoteView.Grid.DataSource = _sourceBindNotes;
                _controlNoteView.View.ExpandAllGroups();
                _controlNoteView.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
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
                        NoteValueView rowValue = _sourceBindNotes.Current as NoteValueView;
                        if (rowValue == null) return;
                    }
                };
                _controlNoteView.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindNotes.Count > 0)
                    {
                        NoteValueView imageItem = _sourceBindNotes[e.ListSourceRowIndex] as NoteValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageKnowledge(imageItem.Workarea, imageItem.RightKind);
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindNotes.Count > 0)
                    {
                        NoteValueView imageItem = _sourceBindNotes[e.ListSourceRowIndex] as NoteValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_NOTES)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новое значение
                BarButtonItem btnValueCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.DropDown,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnValueCreate);

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Note && f.Code == ChainKind.NOTES);

                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                foreach (ChainKind itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem { Caption = itemTml.Name };
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                    {
                        ChainKind objectTml = (ChainKind)btn.Tag;
                        OnBrowseChain = Extentions.BrowseListType;

                        List<int> types = objectTml.GetContentTypeKindId();
                        //types.Contains(s.KindId)
                        //List<T> newAgent = OnBrowseChain.Invoke(SelectedItem, s => (s.KindValue & objectTml.EntityContent) == objectTml.EntityContent, SelectedItem.Workarea.GetCollection<T>());
                        List<Note> newAgent = SelectedItem.Workarea.Empty<Note>().BrowseListType(s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Note>());
                        if (newAgent != null)
                        {
                            foreach (Note selItem in newAgent)
                            {
                                ChainNotes<T> link = new ChainNotes<T>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };
                                link.UserOwnerId = SelectedItem.Workarea.CurrentUser.Id;
                                try
                                {
                                    link.Save();
                                    NoteValueView view = NoteValueView.ConvertToView<T>(link);
                                    _sourceBindNotes.Add(view);
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
                btnValueCreate.DropDownControl = mnuTemplates;

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
                    NoteValueView rowValue = _sourceBindNotes.Current as NoteValueView;
                    if (rowValue == null) return;

                    ChainNotes<T> value = NoteValueView.ConvertToValue<T>(SelectedItem, rowValue);
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

                    NoteValueView currentObject = _sourceBindNotes.Current as NoteValueView;

                    if (currentObject != null)
                    {
                        ChainNotes<T> value = NoteValueView.ConvertToValue<T>(SelectedItem, currentObject);
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
                    NoteValueView currentObject = _sourceBindNotes.Current as NoteValueView;
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
                                ChainNotes<T> value = NoteValueView.ConvertToValue<T>(SelectedItem, currentObject);
                                value.Remove();
                                _sourceBindNotes.Remove(currentObject);
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
                                ChainNotes<T> value = NoteValueView.ConvertToValue<T>(SelectedItem, currentObject);
                                value.Delete();
                                _sourceBindNotes.Remove(currentObject);
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
                _controlNoteView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlNoteView.Grid;
            HidePageControls(ExtentionString.CONTROL_NOTES);
        }
        #endregion


        internal static void DisplayAgentImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }

        internal static void DisplayUidImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceUids)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceUids.Count > 0)
            {
                Uid imageItem = bindSourceUids[e.ListSourceRowIndex] as Uid;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceUids.Count > 0)
            {
                Uid imageItem = bindSourceUids[e.ListSourceRowIndex] as Uid;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }

    }
}
