using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using DevExpress.Data.Linq;
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
    internal sealed class BuildControlTown : BasePropertyControlIBase<Town>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlTown()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKFILES, ExtentionString.CONTROL_LINKFILES);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            
            if (value == ExtentionString.CONTROL_LINKFILES)
                BuildPageLinkedFiles();
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;

            SelectedItem.TerritoryId = _common.cmbTerritory.EditValue == null ? 0 : (int)_common.cmbTerritory.EditValue;
            SelectedItem.X = _ccl.seLocationX.Value;
            SelectedItem.Y = _ccl.seLocationY.Value;

            SelectedItem.NameInternational = _common.txtNameInternational.Text;
            SelectedItem.NameNational = _common.txtNameNational.Text;
            SelectedItem.Amts = _common.txtAmts.Text;

            if (_common.dtDateFoundation.EditValue == null)
                SelectedItem.DateFoundation = null;
            else
                SelectedItem.DateFoundation = _common.dtDateFoundation.DateTime;
            
            SelectedItem.NameOld = _common.txtNameOld.Text;
            SelectedItem.PostIndex = _common.txtPostIndex.Text;
            
            if (_common.edTerritoryKvKm.EditValue == null || _common.edTerritoryKvKm.Value == 0)
                SelectedItem.TerritoryKvKm = null;
            else
                SelectedItem.TerritoryKvKm = (decimal?)_common.edTerritoryKvKm.Value;

            if (_common.edPopulation.EditValue == null || _common.edPopulation.Value == 0)
                SelectedItem.Population = (int?)null;
            else
                SelectedItem.Population = (int?)_common.edPopulation.Value;

            if (_common.edPopulationDensity.EditValue == null || _common.edPopulationDensity.Value == 0)
                SelectedItem.PopulationDensity = null;
            else
                SelectedItem.PopulationDensity = (decimal?)_common.edPopulationDensity.Value;

            SelectedItem.AgentId = (int) _common.cmbAgentId.EditValue;
            SaveStateData();

            InternalSave();
        }

        private BindingSource _bindTerritory;
        private List<Territory> _collTerritory;

        BindingSource _bindingSourceAgentId;
        List<Agent> _collAgentId;


        ControlGeoLocation _ccl;
        ControlTown _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlTown
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
                _common.dtDateFoundation.EditValue = SelectedItem.DateFoundation;
                _common.txtNameOld.Text = SelectedItem.NameOld;
                _common.txtPostIndex.Text = SelectedItem.PostIndex;
                _common.edTerritoryKvKm.EditValue = SelectedItem.TerritoryKvKm;
                _common.edPopulation.EditValue = SelectedItem.Population;
                _common.edPopulationDensity.EditValue = SelectedItem.PopulationDensity;
                _common.txtAmts.Text = SelectedItem.Amts;
                //_common.cmbAgentId
                #region Данные для списка "Исполнитель"

                //LinqInstantFeedbackSource lingSource = new LinqInstantFeedbackSource();
                //lingSource.KeyExpression = "Id";
                //lingSource.GetQueryable += new EventHandler<GetQueryableEventArgs>(lingSource_GetQueryable);
                //lingSource.DismissQueryable += new EventHandler<GetQueryableEventArgs>(lingSource_DismissQueryable);
                /*
                 linqInstantFeedbackSource1.KeyExpression = "PurchaseOrderID";
        // Handle the GetQueryable event, to create a DataContext and assign a queryable source
        linqInstantFeedbackSource1.GetQueryable += linqInstantFeedbackSource1_GetQueryable;
        // Handle the DismissQueryable event, to dispose of the DataContext
        linqInstantFeedbackSource1.DismissQueryable += linqInstantFeedbackSource1_DismissQueryable;
        // Assign the created data source to an XtraGrid
        gridControl1.DataSource = linqInstantFeedbackSource1;

                 */

                //_common.cmbAgentId.Properties.DisplayMember = GlobalPropertyNames.Name;
                //_common.cmbAgentId.Properties.ValueMember = GlobalPropertyNames.Id;
                
                //_bindingSourceAgentId = new BindingSource();
                //_collAgentId = new List<Agent>();
                //if (SelectedItem.AgentId != 0)
                //    _collAgentId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.AgentId));
                //_bindingSourceAgentId.DataSource = _collAgentId;
                //_common.cmbAgentId.Properties.DataSource = _bindingSourceAgentId;
                //DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewAgentId, "DEFAULT_LOOKUPAGENT");
                //_common.cmbAgentId.EditValue = SelectedItem.AgentId;
                //_common.cmbAgentId.QueryPopUp += CmbGridSearchLookUpEditQueryPopUp;
                //_common.ViewAgentId.CustomUnboundColumnData += ViewAgentIdCustomUnboundColumnData;
                //_common.cmbAgentId.KeyDown += delegate(object sender, KeyEventArgs e)
                //{
                //    if (e.KeyCode == Keys.Delete)
                //        _common.cmbAgentId.EditValue = 0;
                //};


                _common.cmbAgentId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbAgentId.Properties.ValueMember = GlobalPropertyNames.Id;

                _bindingSourceAgentId = new BindingSource();
                _collAgentId = new List<Agent>();
                if (SelectedItem.AgentId != 0)
                    _collAgentId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.AgentId));
                _bindingSourceAgentId.DataSource = _collAgentId;
                _common.cmbAgentId.Properties.DataSource = _bindingSourceAgentId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewAgentId, "DEFAULT_LOOKUPAGENT");
                _common.cmbAgentId.EditValue = SelectedItem.AgentId;
                _common.cmbAgentId.BeforFilterChange += new Action<string>(cmbAgentId2_BeforFilterChange);
                _common.ViewAgentId.CustomUnboundColumnData += ViewAgentIdCustomUnboundColumnData;
                _common.cmbAgentId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbAgentId.EditValue = 0;
                };
                #endregion
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

                #region Данные для списка "Область"
                _common.cmbTerritory.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbTerritory.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindTerritory = new BindingSource();
                _collTerritory = new List<Territory>();
                _bindTerritory.DataSource = _collTerritory;
                _common.cmbTerritory.Properties.DataSource = _bindTerritory;
                if (SelectedItem.TerritoryId > 0)
                {
                    Territory t = new Territory { Workarea = SelectedItem.Workarea };
                    t.Load(SelectedItem.TerritoryId);
                    _bindTerritory.Add(t);
                }
                _common.cmbTerritory.EditValue = SelectedItem.TerritoryId;

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.gridViewTerritories, "DEFAULT_LOOKUP");
                _common.cmbTerritory.Properties.View.BestFitColumns();
                _common.gridViewTerritories.CustomUnboundColumnData += ViewTerritoryCustomUnboundColumnData;
                _common.cmbTerritory.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbTerritory.ButtonClick += CmbTerritoryButtonClick;
                #endregion


                #region Показать на карте Google
                BarButtonItem btnNavMapGoogle = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString("BTN_TOWNGOOGLEMAP_CAPTION"),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PREVIEW_X32)

                };
                btnNavMapGoogle.SuperTip = UIHelper.CreateSuperToolTip(btnNavMapGoogle.Glyph, btnNavMapGoogle.Caption,
                                            SelectedItem.Workarea.Cashe.ResourceString("BTN_TOWNGOOGLEMAP_TOOLTIP"));
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnNavMapGoogle);

                btnNavMapGoogle.ItemClick += delegate
                                            {
                                                SelectedItem.ShowOnGoogleMap();
                                            };


                #endregion

                #region Показать на карте Bing
                BarButtonItem btnNavMapBinq = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString("BTN_TOWNBINGMAP_CAPTION"),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PREVIEW_X32)

                };
                btnNavMapBinq.SuperTip = UIHelper.CreateSuperToolTip(btnNavMapBinq.Glyph, btnNavMapBinq.Caption,
                                            SelectedItem.Workarea.Cashe.ResourceString("BTN_TOWNBINQMAP_TOOLTIP"));
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnNavMapBinq);

                btnNavMapBinq.ItemClick += delegate
                {
                    SelectedItem.ShowOnMapBinq();
                };


                #endregion

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

        void cmbAgentId2_BeforFilterChange(string obj)
        {
            if (obj!=null && obj.Length>2)
            {
                _collAgentId = SelectedItem.Workarea.Empty<Agent>().FindBy(name: string.Format("%{0}%", obj), useAndFilter: true);
            }
            else
            {
                _collAgentId = new List<Agent>();
            }
            _bindingSourceAgentId.DataSource = _collAgentId;
        }

        #region Страница "Файлы"
        private List<IChainAdvanced<Town, FileData>> _collectionFiles;
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
                ChainAdvanced<Town, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Town, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Town, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Town, FileData>;
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
            ChainAdvanced<Town, FileData> link = _bindFiles.Current as ChainAdvanced<Town, FileData>;
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
                ChainAdvanced<Town, FileData> link = _bindFiles.Current as ChainAdvanced<Town, FileData>;
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
            foreach (ChainAdvanced<Town, FileData> link in
                retColl.Select(item => new ChainAdvanced<Town, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
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

                ChainAdvanced<Town, FileData> link =
                    new ChainAdvanced<Town, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

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
        List<FilePreviewThread<Town>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<Town>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<Town> dpt = new FilePreviewThread<Town>(_bindFiles.Current as ChainAdvanced<Town, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
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

        //void CmbGridSearchLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    SearchLookUpEdit cmb = sender as SearchLookUpEdit;
        //    if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
        //        cmb.Properties.PopupFormSize = new Size(cmb.Width, cmb.Properties.PopupFormSize.Height);
        //    try
        //    {
        //        _common.Cursor = Cursors.WaitCursor;
        //        if (cmb.Name == "cmbAgentId" && _bindingSourceAgentId.Count < 2)
        //        {
        //            _collAgentId = SelectedItem.Workarea.GetCollection<Agent>(Agent.KINDVALUE_COMPANY);
        //            _bindingSourceAgentId.DataSource = _collAgentId;
        //        }
                
        //    }
        //    catch (Exception z)
        //    { }
        //    finally
        //    {
        //        _common.Cursor = Cursors.Default;
        //    }
        //}
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbTerritory" && _bindTerritory.Count < 2)
                {
                    _collTerritory = SelectedItem.Workarea.GetCollection<Territory>();
                    _bindTerritory.DataSource = _collTerritory;
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

        void ViewAgentIdCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayAgentImagesLookupGrid(e, _bindingSourceAgentId);
        }
        void ViewTerritoryCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindTerritory.Count > 0)
            {
                Territory imageItem = _bindTerritory[e.ListSourceRowIndex] as Territory;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
        }

        void CmbTerritoryButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Territory> browseDialog = new TreeListBrowser<Territory> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if (browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) return;
            if (!_bindTerritory.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindTerritory.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            // _common.cmbCurrency.EditValue = browseDialog.SelectedValue.Id;
        }
    }
}
