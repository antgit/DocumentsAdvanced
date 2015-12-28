using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using BusinessObjects.Windows.Controls;
using System.Drawing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlRegion : BasePropertyControlIBase<Territory>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlRegion()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_TOWNS, ExtentionString.CONTROL_TOWNS);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKFILES, ExtentionString.CONTROL_LINKFILES);
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

            SelectedItem.CountryId = _common.cmbCountry.EditValue == null ? 0 : (int)_common.cmbCountry.EditValue;
            SelectedItem.TownId = _common.cmbTown.EditValue == null ? 0 : (int)_common.cmbTown.EditValue;
            SelectedItem.X = _ccl.seLocationX.Value;
            SelectedItem.Y = _ccl.seLocationY.Value;

            SelectedItem.NameInternational = _common.txtNameInternational.Text;
            SelectedItem.NameNational = _common.txtNameNational.Text;

            SaveStateData();

            InternalSave();
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_TOWNS)
                BuildPageLinkTowns();
            if (value == ExtentionString.CONTROL_LINKFILES)
                BuildPageLinkedFiles();
        }

        #region Связи городами
        private ControlList _controlLinkTowns;
        private BindingSource _sourceBindLinkTowns;
        private List<ChainValueView> _collLinkTownView;
        private void BuildPageLinkTowns()
        {
            if ((SelectedItem as IChainsAdvancedList<Territory, Town>) == null)
                return;
            if (_controlLinkTowns == null)
            {
                //Territory t = SelectedItem.Workarea.GetObject<Territory>(7);
                //Territory t2 = SelectedItem.Workarea.GetObject<Territory>(600);// GetAllTownsInRegion
                //List<Town> cTw = t2.GetAllTownsInRegion(t, t2);
                _controlLinkTowns = new ControlList { Name = ExtentionString.CONTROL_TOWNS };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlLinkTowns.View,
                                                       "DEFAULT_LISTVIEWCHAINVALUETOWN");
                _controlLinkTowns.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlLinkTowns);
                _sourceBindLinkTowns = new BindingSource();

                _collLinkTownView = ChainValueView.GetView<Territory, Town>(SelectedItem);
                _sourceBindLinkTowns.DataSource = _collLinkTownView;
                _controlLinkTowns.Grid.DataSource = _sourceBindLinkTowns;
                _controlLinkTowns.View.ExpandAllGroups();
                _controlLinkTowns.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
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
                        ChainValueView rowValue = _sourceBindLinkTowns.Current as ChainValueView;
                        if (rowValue == null) return;
                    }
                };
                _controlLinkTowns.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindLinkTowns.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindLinkTowns[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImage(imageItem.Workarea.Empty<Town>());
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindLinkTowns.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindLinkTowns[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_TOWNS)];
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

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Town );

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
                        List<Town> newAgent = SelectedItem.Workarea.Empty<Town>().BrowseListType(s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Town>());
                        if (newAgent != null)
                        {
                            foreach (Town selItem in newAgent)
                            {
                                ChainAdvanced<Territory, Town> link = new ChainAdvanced<Territory, Town>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };

                                try
                                {
                                    link.Save();
                                    ChainValueView view = ChainValueView.ConvertToView<Territory, Town>(link);
                                    _sourceBindLinkTowns.Add(view);
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
                btnFastNew.SuperTip = UIHelper.CreateSuperToolTip(btnFastNew.Glyph, btnFastNew.Caption, "Создать населенный пункт");
                groupLinksAction.ItemLinks.Add(btnFastNew);
                btnFastNew.ItemClick += delegate
                {
                    ChainKind objectTml = collectionTemplates.First();
                    Town tmlMsg = SelectedItem.Workarea.GetTemplates<Town>().First(
                        s => s.KindValue == Town.KINDVALUE_TOWN);
                    Town selItem = SelectedItem.Workarea.CreateNewObject<Town>(tmlMsg);

                    selItem.Saved += delegate
                    {
                        Hierarchy h =
                            SelectedItem.Workarea.Cashe.GetCasheData
                                <Hierarchy>().ItemCode<Hierarchy>(
                                    Hierarchy.SYSTEM_EVENT_TASK);
                        h.ContentAdd<Town>(selItem);
                        ChainAdvanced<Territory, Town> link = new ChainAdvanced<Territory, Town>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };

                        try
                        {
                            link.Save();
                            ChainValueView view = ChainValueView.ConvertToView<Territory, Town>(link);
                            _sourceBindLinkTowns.Add(view);
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

                    selItem.ShowProperty(true);
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
                    ChainValueView rowValue = _sourceBindLinkTowns.Current as ChainValueView;
                    if (rowValue == null) return;

                    ChainAdvanced<Territory, Town> value = ChainValueView.ConvertToValue<Territory, Town>(SelectedItem, rowValue);
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

                    ChainValueView currentObject = _sourceBindLinkTowns.Current as ChainValueView;

                    if (currentObject != null)
                    {
                        ChainAdvanced<Territory, Town> value = ChainValueView.ConvertToValue<Territory, Town>(SelectedItem, currentObject);
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
                    ChainValueView currentObject = _sourceBindLinkTowns.Current as ChainValueView;
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
                                ChainAdvanced<Territory, Town> value = ChainValueView.ConvertToValue<Territory, Town>(SelectedItem, currentObject);
                                value.Remove();
                                _sourceBindLinkTowns.Remove(currentObject);
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
                                ChainAdvanced<Territory, Town> value = ChainValueView.ConvertToValue<Territory, Town>(SelectedItem, currentObject);
                                value.Delete();
                                _sourceBindLinkTowns.Remove(currentObject);
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
                _controlLinkTowns.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlLinkTowns.Grid;
            HidePageControls(ExtentionString.CONTROL_TOWNS);
        }
        #endregion

        private BindingSource _bindCountry;
        private List<Country> _collCountry;

        private BindingSource _bindTowns;
        private List<Town> _collTowns;

        ControlGeoLocation _ccl;
        ControlTerritory _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlTerritory
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  editLocation = {Text = SelectedItem.X + ";" + SelectedItem.Y},
                                  Workarea = SelectedItem.Workarea
                              };
                _common.txtNameInternational.Text = SelectedItem.NameInternational;
                _common.txtNameNational.Text = SelectedItem.NameNational;

                PopupContainerControl pcc = new PopupContainerControl();
                _ccl = new ControlGeoLocation();
                pcc.Controls.Add(_ccl);
                pcc.Size = new Size(_common.editLocation.Size.Width, _ccl.Size.Height);
                _ccl.Dock = DockStyle.Fill;
                _ccl.seLocationX.Value = SelectedItem.X;
                _ccl.seLocationY.Value = SelectedItem.Y;
                _common.editLocation.Closed += delegate
                {
                    SelectedItem.X = _ccl.seLocationX.Value;
                    SelectedItem.Y = _ccl.seLocationY.Value;
                    _common.editLocation.Text = SelectedItem.X + ";" + SelectedItem.Y;
                };
                _common.editLocation.Text = SelectedItem.X + ";" + SelectedItem.Y;
                _common.editLocation.Properties.PopupControl = pcc;
                _common.editLocation.ButtonClick += PpcLocationButtonClick;

                #region Данные для списка "Страна"
                _common.cmbCountry.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCountry.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindCountry = new BindingSource();
                _collCountry = new List<Country>();
                _bindCountry.DataSource = _collCountry;
                _common.cmbCountry.Properties.DataSource = _bindCountry;
                if (SelectedItem.CountryId > 0)
                {
                    Country c = new Country { Workarea = SelectedItem.Workarea };
                    c.Load(SelectedItem.CountryId);
                    _bindCountry.Add(c);
                }
                _common.cmbCountry.EditValue = SelectedItem.CountryId;

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.gridViewCountry, "DEFAULT_LOOKUP");
                _common.cmbCountry.Properties.View.BestFitColumns();
                _common.gridViewCountry.CustomUnboundColumnData += ViewCurrencyCustomUnboundColumnData;
                _common.cmbCountry.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbCountry.ButtonClick += CmbCurrencyButtonClick;
                #endregion

                #region Данные для списка "Город"
                _common.cmbTown.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbTown.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindTowns = new BindingSource();
                _collTowns = new List<Town>();
                _bindTowns.DataSource = _collTowns;
                _common.cmbTown.Properties.DataSource = _bindTowns;
                if (SelectedItem.TownId > 0)
                {
                    Town t = new Town { Workarea = SelectedItem.Workarea };
                    t.Load(SelectedItem.TownId);
                    _bindTowns.Add(t);
                }
                _common.cmbTown.EditValue = SelectedItem.TownId;

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.gridViewControlCenter, "DEFAULT_LOOKUP_TOWN");
                _common.cmbTown.Properties.View.BestFitColumns();
                _common.gridViewControlCenter.CustomUnboundColumnData += ViewTownCustomUnboundColumnData;
                _common.cmbTown.QueryPopUp += CmbSearchGridLookUpEditQueryPopUp;
                _common.cmbTown.ButtonClick += CmbTownButtonClick;
                _common.cmbTown.Properties.Buttons[2].Image = ResourceImage.GetByCode(SelectedItem.Workarea, "SEARCH_X8");
                #endregion

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

        #region Страница "Файлы"
        private List<IChainAdvanced<Territory, FileData>> _collectionFiles;
        private BindingSource _bindFiles;
        private DevExpress.XtraGrid.GridControl _gridFiles;
        public GridView ViewFiles;

        protected void BuildPageLinkedFiles()
        {

            if (_gridFiles == null)
            {
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKFILES)];
                RibbonPageGroup groupLinksActionFiles = new RibbonPageGroup();
                //RibbonPageGroup groupLinksActionFiles = page.GetGroupByName(page.Name + "_ACTIONLIST");

                groupLinksActionFiles = new RibbonPageGroup { Name = page.Name + "_ACTIONLIST", Text = "Действия с файлами" };

                #region Добавить новый файл
                BarButtonItem btnFileCreate = new BarButtonItem
                {
                    Name = "btnFileCreate",
                    ButtonStyle = BarButtonStyle.Default,
                    Caption = "Добавить новый файл",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.NEW_X32), "Добавить новый файл",
                                                  "Добавление файла и связывание его с текущим документом из файловой системы. Не забудьте выбрать правильный фильтр отображаемых файлов!")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileCreate);
                btnFileCreate.ItemClick += BtnFileCreateItemClick;
                #endregion

                #region Связать с файлом
                BarButtonItem btnFileLink = new BarButtonItem
                {
                    Name = "btnFileLink",
                    Caption = "Связать с файлом",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINKNEW_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "LINKNEW32"), "Связать с файлом",
                                                  "Связать с файлом уже зарегестрированных в базе данных на текущего корреспондента."),
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileLink);
                btnFileLink.ItemClick += BtnFileLinkItemClick;

                #endregion

                #region Экспорт файла
                BarButtonItem btnFileExport = new BarButtonItem
                {
                    Name = "btnFileExport",
                    Caption = "Экспорт файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32), "Экспорт файла",
                                                  "Экспор файла из базы данных для просмотра или изменений. После изменений необходимо повторно импортировать файл в базу данных")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileExport);
                btnFileExport.ItemClick += BtnFileExportItemClick;
                #endregion

                #region Просмотр файла
                BarButtonItem btnFilePreview = new BarButtonItem
                {
                    Name = "btnFilePreview",
                    Caption = "Просмотр файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = BusinessObjects.Windows.Properties.Resources.PREVIEW_X32,
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "PREVIEW32"), "Просмотр файла",
                                                  "Просмотр файла приложением соответствующим для данного файла, программа должна быть установлена на Вашем компьютере. После просмотра в окне сообщения нажмите кнопку <b>Ок</b> для удаления временно созданного файла <br><i>(не нажимайте кнопку <b>Ок</b> до закрытия файла)</i>")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFilePreview);
                btnFilePreview.ItemClick += BtnFilePreviewItemClick;
                #endregion

                #region Удаление файла
                BarButtonItem btnFileDelete = new BarButtonItem
                {
                    Name = "btnFileDelete",
                    Caption = "Удалить файл",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32), "Удалить файл",
                                                  "Удаление файла связанного с документом. Возможно полное удаление - удаление связи и файла и удаление только связи.")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileDelete);
                btnFileDelete.ItemClick += BtnFileDeleteItemClick;

                #endregion
                page.Groups.Add(groupLinksActionFiles);

                _gridFiles = new DevExpress.XtraGrid.GridControl();
                ViewFiles = new GridView();
                _gridFiles.Dock = DockStyle.Fill;
                _gridFiles.ViewCollection.Add(ViewFiles);
                _gridFiles.MainView = ViewFiles;
                ViewFiles.GridControl = _gridFiles;

                ViewFiles.OptionsBehavior.AllowIncrementalSearch = true;
                ViewFiles.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled;
                ViewFiles.OptionsBehavior.Editable = false;
                ViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
                ViewFiles.OptionsView.ShowGroupPanel = false;
                ViewFiles.OptionsView.ShowIndicator = false;
                _gridFiles.ShowOnlyPredefinedDetails = true;
                Control.Controls.Add(_gridFiles);
                _gridFiles.Dock = DockStyle.Fill;
                //Form.clientPanel.Controls.Add(_gridFiles);

                _gridFiles.Name = ExtentionString.CONTROL_LINKFILES;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, ViewFiles, "DEFAULT_LISTVIEWCONTRACTFILES");
                // TODO: Заменить 13 на правильное представление типа связи
                _collectionFiles = SelectedItem.GetLinkedFiles().Where(s => s.StateId == State.STATEACTIVE).ToList();
                _bindFiles = new BindingSource { DataSource = _collectionFiles };
                _gridFiles.DataSource = _bindFiles;
                ViewFiles.CustomUnboundColumnData += ViewCustomUnboundColumnDataFiles;
                _gridFiles.DoubleClick += GridFilesDoubleClick;

            }
            HidePageControls(ExtentionString.CONTROL_LINKFILES);
        }
        // Обработка отрисовки изображения файлов в списке
        void ViewCustomUnboundColumnDataFiles(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Territory, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Territory, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Territory, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Territory, FileData>;
                if (link != null)
                {
                    e.Value = link.State.GetImage();
                }
            }
        }
        private void BtnFileDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileDelete();
        }

        protected void InvokeFileDelete()
        {
            if (_bindFiles.Current == null) return;
            ChainAdvanced<Territory, FileData> link = _bindFiles.Current as ChainAdvanced<Territory, FileData>;
            if (link == null) return;
            // TODO: Использовать строку ресурсов
            int res = Extentions.ShowMessageChoice(SelectedItem.Workarea, "Удаление файла", "Удаление файла", "Удаление данных о файлах связанных с данным документом. Удаление связи удаляет только связь с данным файлом, удаление связи и файла удаляет все данные включая файл.", "Удалить только связь|Удалить связь и файл");
            switch (res)
            {
                case 0:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: Использовать строку ресурсов
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 1:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        link.Right.StateId = State.STATEDELETED;
                        link.Right.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: Использовать строку ресурсов
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                                               SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                   SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void BtnFilePreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFilePreview();
        }

        private void BtnFileExportItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileExport();
        }

        protected void InvokeFileExport()
        {
            try
            {
                if (_bindFiles.Current == null) return;
                ChainAdvanced<Territory, FileData> link = _bindFiles.Current as ChainAdvanced<Territory, FileData>;
                if (link == null) return;
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = link.Right.Name,
                    DefaultExt = link.Right.FileExtention,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    link.Right.ExportStreamDataToFile(dlg.FileName);
                    System.Diagnostics.Process.Start(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFileLinkItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileLink();
        }

        protected void InvokeFileLink()
        {
            int AgentIdRelatedFiles = 0;
            int currentAgId = AgentIdRelatedFiles;
            List<FileData> collFilesToBrowse = FileData.GetCollectionClientFiles(SelectedItem.Workarea, currentAgId);
            List<FileData> retColl = SelectedItem.Workarea.Empty<FileData>().BrowseList(null, collFilesToBrowse.Count == 0 ? null : collFilesToBrowse);
            if (retColl == null || retColl.Count == 0) return;
            ChainKind ckind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(
                    f =>
                    f.Code == ChainKind.FILE & f.FromEntityId == SelectedItem.EntityId &
                    f.ToEntityId == (int)WhellKnownDbEntity.FileData);
            foreach (ChainAdvanced<Territory, FileData> link in
                retColl.Select(item => new ChainAdvanced<Territory, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
            {
                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
                _bindFiles.ResetBindings(false);
            }
        }

        private void BtnFileCreateItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileCreate();
        }

        protected void InvokeFileCreate()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter =
                    "Adobe PDF|*.pdf|Microsoft Excel 2007|*.xlsx|Microsoft Excel|*.xls|Microsoft Word 2007|*.docx|Microsoft Word|*.doc|PNG|*.png|JPG|*.jpg|XPS|*.xps|Все файлы|*.*"
            };
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            foreach (string fileName in dlg.FileNames)
            {
                FileData fileData = new FileData { Workarea = SelectedItem.Workarea, Name = Path.GetFileName(fileName) };
                fileData.KindId = FileData.KINDID_FILEDATA;
                fileData.SetStreamFromFile(fileName);
                fileData.Save();
                ChainKind ckind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(
                    f =>
                    f.Code == ChainKind.FILE & f.FromEntityId == SelectedItem.EntityId &
                    f.ToEntityId == (int)WhellKnownDbEntity.FileData);

                ChainAdvanced<Territory, FileData> link =
                    new ChainAdvanced<Territory, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
            }
            _bindFiles.ResetBindings(false);
        }

        private void GridFilesDoubleClick(object sender, EventArgs e)
        {
            Point p = _gridFiles.PointToClient(Control.MousePosition);
            GridHitInfo hit = ViewFiles.CalcHitInfo(p.X, p.Y);
            if (hit.InRow)
            {
                InvokeFilePreview();
            }
        }
        List<FilePreviewThread<Territory>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<Territory>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<Territory> dpt = new FilePreviewThread<Territory>(_bindFiles.Current as ChainAdvanced<Territory, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        void PpcLocationButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                SelectedItem.ShowOnGoogleMap();
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
                if (cmb.Name == "cmbCountry" && _bindCountry.Count < 2)
                {
                    _collCountry = SelectedItem.Workarea.GetCollection<Country>();
                    _bindCountry.DataSource = _collCountry;
                }
                else
                    if (cmb.Name == "cmbTown" && _bindTowns.Count < 2)
                    {
                        _collTowns = SelectedItem.Workarea.GetCollection<Town>();
                        _bindTowns.DataSource = _collTowns;
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
        void CmbSearchGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, cmb.Properties.PopupFormSize.Height);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbCountry" && _bindCountry.Count < 2)
                {
                    _collCountry = SelectedItem.Workarea.GetCollection<Country>();
                    _bindCountry.DataSource = _collCountry;
                }
                else if (cmb.Name == "cmbTown" && _bindTowns.Count < 2)
                {
                    if(SelectedItem.KindValue== Territory.KINDID_REGION)
                        _collTowns = SelectedItem.Workarea.GetCollection<Town>().Where(s=>s.TerritoryId==SelectedItem.Id).ToList();
                    else
                        _collTowns = SelectedItem.Workarea.GetCollection<Town>();
                    _bindTowns.DataSource = _collTowns;
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

        void ViewCurrencyCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindCountry.Count > 0)
            {
                Country imageItem = _bindCountry[e.ListSourceRowIndex] as Country;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
        }

        void ViewTownCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindCountry.Count > 0)
            {
                Town imageItem = _bindTowns[e.ListSourceRowIndex] as Town;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
        }

        void CmbCurrencyButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Country> browseDialog = new TreeListBrowser<Country> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindCountry.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindCountry.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            // _common.cmbCurrency.EditValue = browseDialog.SelectedValue.Id;
        }

        void CmbTownButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) 
                return;
            if (e.Button.Index == 1)
            {
                TreeListBrowser<Town> browseDialog =
                    new TreeListBrowser<Town> {Workarea = SelectedItem.Workarea}.ShowDialog();
                if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) ||
                    (browseDialog.DialogResult != DialogResult.OK)) return;
                if (!_bindTowns.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                    _bindTowns.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                _common.cmbTown.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
            }
            else if(e.Button.Index==2)
            {
                if (_common.cmbTown.EditValue == null) return;
                int selTownId = (int)_common.cmbTown.EditValue;
                SelectedItem.Workarea.GetObject<Town>(selTownId).ShowProperty(true);
            }
            // _common.cmbCurrency.EditValue = browseDialog.SelectedValue.Id;
        }
    }
}
