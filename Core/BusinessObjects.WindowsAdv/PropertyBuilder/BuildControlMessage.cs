using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Models;
using BusinessObjects.Security;
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
    /// Формирование контрола для отображения свойств сообщения
    /// </summary>
    internal sealed class BuildControlMessage : BasePropertyControlIBase<Message>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlMessage()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKFILES, ExtentionString.CONTROL_LINKFILES);
            TotalPages.Add(ExtentionString.CONTROL_LINKDOCUMENTS, ExtentionString.CONTROL_LINKDOCUMENTS);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);

            if (value == ExtentionString.CONTROL_LINKFILES)
                BuildPageLinkedFiles();
            if (value == ExtentionString.CONTROL_LINKDOCUMENTS)
                BuildPageLinkDocuments();
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.PriorityId = (int?) _common.cmbPriority.EditValue ?? 0;
            SelectedItem.UserOwnerId = (int?)_common.cmbUserOwnerId.EditValue ?? 0;
            SelectedItem.UserId = (int?)_common.cmbUserId.EditValue ?? 0;
            SelectedItem.HasRead = _common.chkHasRead.Checked;
            SelectedItem.ReadDone = _common.chkReadDone.Checked;

            if ((SelectedItem.IsNew && _common.txtMemo.Text.Length > 0) || _common.txtName.Text.Length == 0)
            {
                if (_common.txtMemo.Text.Length > 150)
                    SelectedItem.Name = _common.txtMemo.Text.Substring(0, 147) + "...";
                else
                    SelectedItem.Name = _common.txtMemo.Text;
                _common.txtName.Text = SelectedItem.Name;
            }

            SelectedItem.IsSend = _common.chkIsSend.Checked;

            SelectedItem.ReadDate = (DateTime?)_common.dtReadDate.EditValue;
            if (_common.tmReadTime.EditValue != null)
                SelectedItem.ReadTime= (TimeSpan?)_common.tmReadTime.Time.TimeOfDay;
            else
                SelectedItem.ReadTime = null;

            SelectedItem.SendDate = (DateTime?)_common.dtSendDate.EditValue;
            if (_common.tmSendTime.EditValue != null)
                SelectedItem.SendTime = (TimeSpan?)_common.tmSendTime.Time.TimeOfDay;
            else
                SelectedItem.SendTime = null;

            SaveStateData();

            InternalSave();
        }

        ControlMessage _common;
        BindingSource _bindingSourcePriority;
        List<Analitic> _collPriority;

        private BindingSource bindUserOwnerId;
        private List<Uid> collUserOwnerId;

        private BindingSource bindUserId;
        private List<Uid> collUserId;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlMessage
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  cmbUserOwnerId = {EditValue = SelectedItem.UserOwnerId},
                                  Workarea = SelectedItem.Workarea
                              };

                _common.chkHasRead.Checked = SelectedItem.HasRead;
                _common.chkReadDone.Checked = SelectedItem.ReadDone;

                _common.chkIsSend.Checked = SelectedItem.IsSend;

                _common.dtSendDate.EditValue = SelectedItem.SendDate;
                _common.tmSendTime.EditValue = SelectedItem.SendTime;

                _common.chkIsSend.CheckedChanged += delegate
                                                        {
                                                            if (_common.chkIsSend.Checked)
                                                            {
                                                                _common.dtSendDate.EditValue = DateTime.Today;
                                                                _common.tmSendTime.EditValue = DateTime.Now.TimeOfDay;
                                                            }
                                                            else
                                                            {
                                                                _common.dtSendDate.EditValue = null;
                                                                _common.tmSendTime.EditValue = null;
                                                            }
                                                        };

                _common.dtReadDate.EditValue = SelectedItem.ReadDate;
                _common.tmReadTime.EditValue = SelectedItem.ReadTime;

                _common.chkReadDone.CheckedChanged += delegate
                                                          {
                                                              if (_common.chkReadDone.Checked)
                                                              {
                                                                  _common.dtReadDate.EditValue = DateTime.Today;
                                                                  _common.tmReadTime.EditValue = DateTime.Now.TimeOfDay;
                                                              }
                                                              else
                                                              {
                                                                  _common.dtReadDate.EditValue = null;
                                                                  _common.tmReadTime.EditValue = null;
                                                              }
                                                          };

                #region Данные для списка "Приоритет"
                _common.cmbPriority.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbPriority.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourcePriority = new BindingSource();
                _collPriority = new List<Analitic>();
                if (SelectedItem.PriorityId != 0)
                    _collPriority.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.PriorityId));
                _bindingSourcePriority.DataSource = _collPriority;
                _common.cmbPriority.Properties.DataSource = _bindingSourcePriority;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbPriority, "DEFAULT_LOOKUP_NAME");
                _common.cmbPriority.EditValue = SelectedItem.PriorityId;
                _common.cmbPriority.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbPriority.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbPriority.EditValue = 0;
                };
                
                #endregion

                #region Данные для списка "Владелец"
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewUserOwnerId, "DEFAULT_LOOKUP_UID");
                _common.cmbUserOwnerId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbUserOwnerId.Properties.ValueMember = GlobalPropertyNames.Id;
                bindUserOwnerId = new BindingSource();
                collUserOwnerId = new List<Uid>();
                if (SelectedItem.UserOwnerId != 0)
                    collUserOwnerId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Uid>().Item(SelectedItem.UserOwnerId));
                bindUserOwnerId.DataSource = collUserOwnerId;
                _common.cmbUserOwnerId.Properties.DataSource = bindUserOwnerId;
                _common.cmbUserOwnerId.EditValue = SelectedItem.UserOwnerId;
                _common.cmbUserOwnerId.QueryPopUp += cmbUserOwnerId_QueryPopUp;
                _common.ViewUserOwnerId.CustomUnboundColumnData += ViewUserOwnerIdCustomUnboundColumnData;
                _common.cmbUserOwnerId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUserOwnerId.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Получатель"
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewUserId, "DEFAULT_LOOKUP_UID");
                _common.cmbUserId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbUserId.Properties.ValueMember = GlobalPropertyNames.Id;
                bindUserId = new BindingSource();
                collUserId = new List<Uid>();
                if (SelectedItem.UserId != 0)
                    collUserId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Uid>().Item(SelectedItem.UserId));
                bindUserId.DataSource = collUserId;
                _common.cmbUserId.Properties.DataSource = bindUserId;
                _common.cmbUserId.EditValue = SelectedItem.UserId;
                _common.cmbUserId.QueryPopUp += cmbUserOwnerId_QueryPopUp;
                _common.ViewUserId.CustomUnboundColumnData += ViewUserToIdCustomUnboundColumnData;
                _common.cmbUserId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbUserId.EditValue = 0;
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

                RegisterPrintForms();
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }

        #region Страница "Файлы"
        private List<IChainAdvanced<Message, FileData>> _collectionFiles;
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
                ChainAdvanced<Message, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Message, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Message, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Message, FileData>;
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
            ChainAdvanced<Message, FileData> link = _bindFiles.Current as ChainAdvanced<Message, FileData>;
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
                ChainAdvanced<Message, FileData> link = _bindFiles.Current as ChainAdvanced<Message, FileData>;
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
            foreach (ChainAdvanced<Message, FileData> link in
                retColl.Select(item => new ChainAdvanced<Message, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
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

                ChainAdvanced<Message, FileData> link =
                    new ChainAdvanced<Message, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

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
        List<FilePreviewThread<Message>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<Message>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<Message> dpt = new FilePreviewThread<Message>(_bindFiles.Current as ChainAdvanced<Message, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Связи с документами
        private ControlList _controlDocumentView;
        private BindingSource _sourceBindDocuments;
        private List<DocumentValueView> _collDocumentView;
        private void BuildPageLinkDocuments()
        {
            if ((SelectedItem as IChainsAdvancedList<Message, Document>) == null)
                return;
            if (_controlDocumentView == null)
            {
                _controlDocumentView = new ControlList { Name = ExtentionString.CONTROL_LINKDOCUMENTS };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlDocumentView.View,
                                                       "DEFAULT_DOCUMENTVALUEVIEW");
                _controlDocumentView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlDocumentView);
                _sourceBindDocuments = new BindingSource();

                _collDocumentView = DocumentValueView.GetView<Message>(SelectedItem);
                _sourceBindDocuments.DataSource = _collDocumentView;
                _controlDocumentView.Grid.DataSource = _sourceBindDocuments;
                _controlDocumentView.View.ExpandAllGroups();
                FormProperties frmProp = Owner as FormProperties;
                if (frmProp != null)
                {
                    frmProp.btnRefresh.ItemClick += delegate
                    {
                        _collDocumentView = DocumentValueView.GetView<Message>(SelectedItem);
                        _sourceBindDocuments.DataSource = _collDocumentView;
                        _controlDocumentView.Grid.DataSource = _sourceBindDocuments;
                        _controlDocumentView.View.ExpandAllGroups();
                    };
                }
                _controlDocumentView.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
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
                        ChainValueView rowValue = _sourceBindDocuments.Current as ChainValueView;
                        if (rowValue == null) return;
                    }
                };
                _controlDocumentView.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindDocuments.Count > 0)
                    {
                        DocumentValueView imageItem = _sourceBindDocuments[e.ListSourceRowIndex] as DocumentValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageDocument(imageItem.Workarea, State.STATEACTIVE);
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindDocuments.Count > 0)
                    {
                        DocumentValueView imageItem = _sourceBindDocuments[e.ListSourceRowIndex] as DocumentValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKDOCUMENTS)];
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

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Document && f.Code == ChainKind.DOCS);

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
                        //List<Document> newAgent = SelectedItem.Workarea.Empty<Document>().BrowseListType(s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Document>());
                        FormProperties frm = new FormProperties
                        {
                            Width = 800,
                            Height = 480
                        };
                        Bitmap img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        frm.Ribbon.ApplicationIcon = img;
                        frm.Icon = Icon.FromHandle(img.GetHicon());
                        ContentNavigator Navigator = new ContentNavigator { MainForm = frm, Workarea = SelectedItem.Workarea };
                        ContentModuleDocuments DocsModule = new ContentModuleDocuments() { Workarea = SelectedItem.Workarea };
                        Navigator.SafeAddModule("DOCUMENTS", DocsModule);
                        Navigator.ActiveKey = "DOCUMENTS";
                        frm.btnSave.Visibility = BarItemVisibility.Never;
                        frm.btnSelect.Visibility = BarItemVisibility.Always;

                        frm.btnSelect.Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.SELECT_X32);
                        frm.btnSelect.ItemClick += delegate
                        {
                            foreach (Document selItem in DocsModule.SelectedDocuments)
                            {
                                if (!_collDocumentView.Exists(d => d.RightId == selItem.Id))
                                {
                                    ChainAdvanced<Message, Document> link =
                                        new ChainAdvanced<Message, Document>(SelectedItem)
                                        {
                                            RightId = selItem.Id,
                                            KindId = objectTml.Id,
                                            StateId = State.STATEACTIVE
                                        };

                                    try
                                    {
                                        link.Save();
                                        DocumentValueView view =
                                            DocumentValueView.ConvertToView<Message, Document>
                                                (link);
                                        _sourceBindDocuments.Add(view);
                                    }
                                    catch (DatabaseException dbe)
                                    {
                                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR), "Создание новой связи невозможно!", dbe.Message, dbe.Id);
                                    }
                                    catch (Exception ex)
                                    {
                                        XtraMessageBox.Show(ex.Message, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR),
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Error);
                                    }
                                }
                            }
                        };
                        frm.ShowDialog();

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
                    DocumentValueView rowValue = _sourceBindDocuments.Current as DocumentValueView;
                    if (rowValue == null) return;

                    ChainAdvanced<Message, Document> value = DocumentValueView.ConvertToValue<Message, Document>(SelectedItem, rowValue);
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

                    DocumentValueView currentObject = _sourceBindDocuments.Current as DocumentValueView;

                    if (currentObject != null)
                    {
                        ChainAdvanced<Message, Document> value = DocumentValueView.ConvertToValue<Message, Document>(SelectedItem, currentObject);
                        if (value != null)
                        {
                            InvokeShowDocument(value.Right);
                            //value.Right.ShowProperty();
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
                    DocumentValueView currentObject = _sourceBindDocuments.Current as DocumentValueView;
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
                                ChainAdvanced<Message, Document> value = DocumentValueView.ConvertToValue<Message, Document>(SelectedItem, currentObject);
                                value.Remove();
                                _sourceBindDocuments.Remove(currentObject);
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
                                ChainAdvanced<Message, Document> value = DocumentValueView.ConvertToValue<Message, Document>(SelectedItem, currentObject);
                                value.Delete();
                                _sourceBindDocuments.Remove(currentObject);
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
                _controlDocumentView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlDocumentView.Grid;
            HidePageControls(ExtentionString.CONTROL_LINKDOCUMENTS);
        }

        protected override void Print(int printFormId)
        {
            base.Print(printFormId);
            try
            {
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == printFormId);
                string fileName = printLibrary.AssemblyDll.NameFull;
                Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                MessageModel model = new MessageModel();
                model.GetData(SelectedItem);
                report.RegData("Document", model);
                //report.RegData("DocumentDetail", collection);
                report.Render();
                    //if (!SourceDocument.IsNew)
                    //    LogUserAction.CreateActionPreview(SelectedItem.Workarea, SourceDocument.Id, printLibrary.Name);
                report.Show();
                
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORPRINT, 1049) + Environment.NewLine + ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void InvokeShowDocument(Document op)
        {
            if (op == null) return;
            if (op.ProjectItemId == 0) return;
            Library lib = op.ProjectItem;
            int referenceLibId = Library.GetLibraryIdByContent(op.Workarea, lib.LibraryTypeId);
            Library referenceLib = op.Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
            LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

            Assembly ass = Library.GetAssemblyFromGac(referenceLib);
            if (ass == null)
            {
                string assFile = Path.Combine(Application.StartupPath,
                                              referenceLib.AssemblyDll.NameFull);
                if (!File.Exists(assFile))
                {
                    using (
                        FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                    {
                        stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                     referenceLib.AssemblyDll.StreamData.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                }
                ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                      Assembly.LoadFile(assFile);
            }
            Type type = ass.GetType(cnt.FullTypeName);
            if (type == null) return;
            object objectContentModule = Activator.CreateInstance(type);
            IDocumentView formModule = objectContentModule as IDocumentView;
            //if (currentElement.Kind == 28)
            //    formModule.OwnerObject = currentElement.Hierarchy;
            //else if (currentElement.Kind == 7)
            //    formModule.OwnerObject = currentElement.Folder;
            formModule.Show(op.Workarea, null, op.Id, 0);
        }
        #endregion

        void cmbUserOwnerId_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SearchLookUpEdit cmb = sender as SearchLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, cmb.Properties.PopupFormSize.Height);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbUserOwnerId" && bindUserOwnerId.Count < 2)
                {
                    collUserOwnerId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER).ToList();
                    bindUserOwnerId.DataSource = collUserOwnerId;
                }
                else if (cmb.Name == "cmbUserId" && bindUserId.Count < 2)
                {
                    collUserId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER).ToList();
                    bindUserId.DataSource = collUserId;
                }
            }
            catch (Exception z)
            { }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        void ViewUserToIdCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayUidImagesLookupGrid(e, bindUserId);
        }
        void ViewUserOwnerIdCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayUidImagesLookupGrid(e, bindUserOwnerId);
        }
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LookUpEdit cmb = sender as LookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbPriority" && _bindingSourcePriority.Count < 2)
                {
                    Hierarchy rootImpatance = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("CONTRACT_PRIORITY");
                    _collPriority = rootImpatance.GetTypeContents<Analitic>();
                    _bindingSourcePriority.DataSource = _collPriority;
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
    }
}