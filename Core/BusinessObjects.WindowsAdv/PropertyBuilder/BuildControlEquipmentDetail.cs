using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств детали оборудования
    /// </summary>
    internal sealed class BuildControlEquipmentDetail : BasePropertyControlIBase<EquipmentDetail>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlEquipmentDetail()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_KNOWLEDGES, ExtentionString.CONTROL_KNOWLEDGES);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
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

            SelectedItem.Nomenclature = _common.txtNomenclature.Text;
            SelectedItem.DrawingAssemblyNumber = _common.txtDrawingAssemblyNumber.Text;
            SelectedItem.DrawingNumber = _common.txtDrawingNumber.Text;
            SelectedItem.Weight = (decimal)_common.edWeight.Value;

            if (_common.dtDateBuild.EditValue != null)
            {
                SelectedItem.DateBuild = _common.dtDateBuild.DateTime;
            }
            else
                SelectedItem.DateBuild = (DateTime?)null;

            if (_common.cmbUnitId.EditValue != null)
                SelectedItem.UnitId = (int)_common.cmbUnitId.EditValue;
            else
                SelectedItem.UnitId = 0;

            if (string.IsNullOrEmpty(SelectedItem.CodeFind))
            {
                SelectedItem.CodeFind = (SelectedItem.Name + Transliteration.Front(SelectedItem.Name)).Replace(" ", "");
            }

            SaveStateData();

            InternalSave();
        }

        BindingSource _bindingSourceUnit;
        List<Unit> _collUnits;
        ControlEquipmentDetail _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlEquipmentDetail
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  txtNomenclature = { Text = SelectedItem.Nomenclature },
                                  txtDrawingAssemblyNumber = { Text = SelectedItem.DrawingAssemblyNumber },
                                  txtDrawingNumber = { Text = SelectedItem.DrawingNumber },
                                  edWeight = { Value = SelectedItem.Weight },
                                  Workarea = SelectedItem.Workarea
                              };

                if(SelectedItem.DateBuild.HasValue)
                {
                    _common.dtDateBuild.EditValue = SelectedItem.DateBuild;
                }
                #region Единицы измерения

                _bindingSourceUnit = new BindingSource();
                _collUnits = new List<Unit>();
                if (SelectedItem.UnitId != 0)
                    _collUnits.Add(SelectedItem.Workarea.Cashe.GetCasheData<Unit>().Item(SelectedItem.UnitId));
                _bindingSourceUnit.DataSource = _collUnits;
                _common.cmbUnitId.Properties.DataSource = _bindingSourceUnit;
                _common.cmbUnitId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbUnitId.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbUnitId.EditValue = SelectedItem.UnitId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbUnitId, "DEFAULT_LOOKUPUNIT");
                _common.cmbUnitId.ButtonClick += cmbUnits_ButtonClick;
                _common.cmbUnitId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbUnitId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUnitId.EditValue = 0;
                };
                #endregion

                UIHelper.GenerateTooltips<EquipmentDetail>(SelectedItem, _common);
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
        void cmbUnits_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Unit> browseDialog = new TreeListBrowser<Unit> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindingSourceUnit.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindingSourceUnit.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbUnitId.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width < cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, cmb.Properties.PopupFormSize.Height);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                
                if (cmb.Name == "cmbUnitId" && _bindingSourceUnit.Count < 2)
                {
                    _collUnits = SelectedItem.Workarea.GetCollection<Unit>();
                    _bindingSourceUnit.DataSource = _collUnits;
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
        #region Страница "Файлы"
        private List<IChainAdvanced<EquipmentDetail, FileData>> _collectionFiles;
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
                    Glyph = Properties.Resources.PREVIEW_X32,
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
                ChainAdvanced<EquipmentDetail, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<EquipmentDetail, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<EquipmentDetail, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<EquipmentDetail, FileData>;
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
            ChainAdvanced<EquipmentDetail, FileData> link = _bindFiles.Current as ChainAdvanced<EquipmentDetail, FileData>;
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
                        XtraMessageBox.Show(ex.Message,
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
                ChainAdvanced<EquipmentDetail, FileData> link = _bindFiles.Current as ChainAdvanced<EquipmentDetail, FileData>;
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
                XtraMessageBox.Show(ex.Message, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            foreach (ChainAdvanced<EquipmentDetail, FileData> link in
                retColl.Select(item => new ChainAdvanced<EquipmentDetail, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
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

                ChainAdvanced<EquipmentDetail, FileData> link =
                    new ChainAdvanced<EquipmentDetail, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

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
        List<FilePreviewThread<EquipmentDetail>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<EquipmentDetail>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<EquipmentDetail> dpt = new FilePreviewThread<EquipmentDetail>(_bindFiles.Current as ChainAdvanced<EquipmentDetail, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}