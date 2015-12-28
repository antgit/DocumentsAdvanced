using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Models;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.Utils.Zip;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Export.Html;
using DevExpress.XtraSpellChecker;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств задачи
    /// </summary>
    internal sealed class BuildControlTask : BasePropertyControlIBase<Task>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlTask()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKFILES, ExtentionString.CONTROL_LINKFILES);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
            TotalPages.Add(ExtentionString.CONTROL_MESSAGES, ExtentionString.CONTROL_MESSAGES);
            TotalPages.Add(ExtentionString.CONTROL_LINKDOCUMENTS, ExtentionString.CONTROL_LINKDOCUMENTS);
            TotalPages.Add(ExtentionString.CONTROL_LINKEVENTS, ExtentionString.CONTROL_LINKEVENTS);
            
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
            if (value == ExtentionString.CONTROL_MESSAGES)
                BuildPageMessages();
            if (value == ExtentionString.CONTROL_LINKDOCUMENTS)
                BuildPageLinkDocuments();
            if (value == ExtentionString.CONTROL_LINKEVENTS)
                BuildPageEvents();
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            //SelectedItem.Memo = _common.txtMemo.Text;
            HtmlDocumentExporterOptions options = new HtmlDocumentExporterOptions();
            options.ExportRootTag = ExportRootTag.Body;
            options.CssPropertiesExportType = CssPropertiesExportType.Inline; //cssExportType;
            HtmlExporter exporter = new HtmlExporter(_common.txtMemoAdv.Model, options);
            string stringHtml = exporter.Export();
            stringHtml = stringHtml.Replace("<body>", "").Replace("</body>", "");

            SelectedItem.Memo = stringHtml;//_common.txtMemoAdv.HtmlText;
            SelectedItem.MemoTxt = _common.txtMemoAdv.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;

            if ((SelectedItem.IsNew && _common.txtMemoAdv.Text.Length > 0) || _common.txtName.Text.Length==0)
            {
                if (_common.txtMemoAdv.Text.Length>150)
                    SelectedItem.Name = _common.txtMemoAdv.Text.Substring(0, 147) + "...";
                else
                    SelectedItem.Name = _common.txtMemoAdv.Text;
                _common.txtName.Text = SelectedItem.Name;
            }

            if (string.IsNullOrEmpty(SelectedItem.CodeFind) && !SelectedItem.IsTemplate)
            {
                SelectedItem.CodeFind = (SelectedItem.Name + Transliteration.Front(SelectedItem.Name)).Replace(" ", "");
                _common.txtCodeFind.Text = SelectedItem.CodeFind;
            }

            SelectedItem.DateStartPlan = (DateTime?)_common.dtDateStartPlan.EditValue;
            if (_common.tmDateStartPlanTime.EditValue != null)
                SelectedItem.DateStartPlanTime = _common.tmDateStartPlanTime.Time.TimeOfDay;
            else
                SelectedItem.DateStartPlanTime = null;

            SelectedItem.DateEndPlan = (DateTime?)_common.dtDateEndPlan.EditValue;
            if (_common.tmDateEndPlanTime.EditValue != null)
            {
                SelectedItem.DateEndPlanTime = _common.tmDateEndPlanTime.Time.TimeOfDay;
            }
            else
                SelectedItem.DateEndPlanTime = null;

            SelectedItem.DateStart = (DateTime?)_common.dtDateStart.EditValue;
            if (_common.tmDateStartTime.EditValue != null)
                SelectedItem.DateStartTime = _common.tmDateStartTime.Time.TimeOfDay;
            else
                SelectedItem.DateStartTime = null;

            SelectedItem.DateEnd = (DateTime?)_common.dtDateEnd.EditValue;
            if (_common.tmDateEndTime.EditValue != null)
                SelectedItem.DateEndTime = (TimeSpan?)_common.tmDateEndTime.Time.TimeOfDay;
            else
                SelectedItem.DateEndTime = null;

            SelectedItem.TaskNumber = (int)_common.edTaskNumber.Value;
            int doneP = 0;
            Int32.TryParse(_common.cmbDonePersent.Text, out doneP);
            SelectedItem.DonePersent = doneP;
            SelectedItem.InPrivate = _common.chkInPrivate.Checked;

            SelectedItem.PriorityId = (int)_common.cmbPriorityId.EditValue;
            SelectedItem.TaskStateId = (int)_common.cmbTaskStateId.EditValue;
            SelectedItem.UserOwnerId = (int)_common.cmbUserOwnerId.EditValue;
            SelectedItem.UserToId = (int)_common.cmbUserToId.EditValue;

            SaveStateData();

            InternalSave();
        }

        ControlTask _common;
        BindingSource _bindingSourceTaskStateId;
        List<Analitic> _collTaskStateId;

        BindingSource _bindingSourcePriorityId;
        List<Analitic> _collPriorityId;

        BindingSource _bindingSourceUserOwnerId;
        List<Uid> _collUserOwnerId;

        BindingSource _bindingSourceUserToId;
        List<Uid> _collUserToId;
        
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlTask
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  Workarea = SelectedItem.Workarea
                              };
                _common.txtMemo.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
                _common.txtMemo.Properties.Appearance.Options.UseTextOptions = true;
                _common.txtMemoAdv.HtmlText = SelectedItem.Memo;
                _common.dtDateStartPlan.EditValue = SelectedItem.DateStartPlan;
                _common.tmDateStartPlanTime.EditValue = SelectedItem.DateStartPlanTime;

                _common.dtDateEndPlan.EditValue = SelectedItem.DateEndPlan;
                _common.tmDateEndPlanTime.EditValue = SelectedItem.DateEndPlanTime;

                _common.dtDateStart.EditValue = SelectedItem.DateStart;
                _common.tmDateStartTime.EditValue = SelectedItem.DateStartTime;

                _common.dtDateEnd.EditValue = SelectedItem.DateEnd;
                _common.tmDateEndTime.EditValue = SelectedItem.DateEndTime;

                _common.edTaskNumber.Value = SelectedItem.TaskNumber;
                _common.cmbDonePersent.Text = SelectedItem.DonePersent.ToString();
                _common.chkInPrivate.Checked = SelectedItem.InPrivate;
                
                #region Данные для списка "Состояние задачи"
                _common.cmbTaskStateId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbTaskStateId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceTaskStateId = new BindingSource();
                _collTaskStateId = new List<Analitic>();
                if (SelectedItem.TaskStateId != 0)
                    _collTaskStateId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.TaskStateId));
                _bindingSourceTaskStateId.DataSource = _collTaskStateId;
                _common.cmbTaskStateId.Properties.DataSource = _bindingSourceTaskStateId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewTaskState, "DEFAULT_LOOKUP_ANALITIC");
                _common.cmbTaskStateId.EditValue = SelectedItem.TaskStateId;
                _common.cmbTaskStateId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.ViewTaskState.CustomUnboundColumnData += ViewTaskStateCustomUnboundColumnData;
                _common.cmbTaskStateId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbTaskStateId.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Приоритет"
                _common.cmbPriorityId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbPriorityId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourcePriorityId = new BindingSource();
                _collPriorityId = new List<Analitic>();
                if (SelectedItem.PriorityId != 0)
                    _collPriorityId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.PriorityId));
                _bindingSourcePriorityId.DataSource = _collPriorityId;
                _common.cmbPriorityId.Properties.DataSource = _bindingSourcePriorityId;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewPriority, "DEFAULT_LOOKUP_ANALITIC");
                _common.cmbPriorityId.EditValue = SelectedItem.PriorityId;
                _common.cmbPriorityId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.ViewPriority.CustomUnboundColumnData += ViewPriorityCustomUnboundColumnData;
                _common.cmbPriorityId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbPriorityId.EditValue = 0;
                };
                #endregion

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

                #region Проверка орфографии
                BarButtonItem btnSpellCheck = new BarButtonItem
        {
            Caption = "Орфография",
            RibbonStyle = RibbonItemStyles.Large,
            Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.SPELLCHECK_X32)
        };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnSpellCheck);
                btnSpellCheck.ItemClick += delegate
                {
                    _common.spellChecker1.Culture = CultureInfo.InvariantCulture;
                    _common.spellChecker1.UseSharedDictionaries = false;
                    //_common.spellChecker1.SpellingFormType = SpellingFormType.Word;
                    LoadDictionary("DICTIONARY_RU", new CultureInfo("ru-RU"));
                    LoadDictionary("DICTIONARY_EN", new CultureInfo("en-US"));
                    _common.spellChecker1.Check(_common.txtMemoAdv);
                }; 
                #endregion

                #region Плановое завершение
                BarButtonItem btnChainCreate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.DropDown,
                    ActAsDropDown = true,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString("BTN_CAPTION_TASKENDPLAN", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.PERIOD_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnChainCreate);

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == SelectedItem.EntityId);
                PopupMenu mnuTemplates = new PopupMenu { Ribbon = frmProp.ribbon };
                BarButtonItem btnPlanEndMonth = new BarButtonItem { Caption = "Конец месяца" };
                mnuTemplates.AddItem(btnPlanEndMonth);
                btnPlanEndMonth.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.LastDayOfCurrentMonth();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };

                BarButtonItem btnPlanEndNextMonth = new BarButtonItem { Caption = "Конец следующего месяца" };
                mnuTemplates.AddItem(btnPlanEndNextMonth);
                btnPlanEndNextMonth.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.LastDayOfNextMonth();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };
                

                BarButtonItem btnPlanEndWeek = new BarButtonItem { Caption = "Конец недели" };
                mnuTemplates.AddItem(btnPlanEndWeek).BeginGroup = true;
                btnPlanEndWeek.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.EndOfCurrentWeek();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };

                BarButtonItem btnPlanNextWeek = new BarButtonItem { Caption = "Конец следующей недели" };
                mnuTemplates.AddItem(btnPlanNextWeek);
                btnPlanNextWeek.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.EndOfNextWeek();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };

                BarButtonItem btnPlanNextTwoWeek = new BarButtonItem { Caption = "Конец в конце двух недель" };
                mnuTemplates.AddItem(btnPlanNextTwoWeek);
                btnPlanNextTwoWeek.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.EndOfTwoNextWeek();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };

                BarButtonItem btnPlanNextThreeWeek = new BarButtonItem { Caption = "Конец в конце трех недель" };
                mnuTemplates.AddItem(btnPlanNextThreeWeek);
                btnPlanNextThreeWeek.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.EndOfThreeNextWeek();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };

                BarButtonItem btnPlanToday = new BarButtonItem { Caption = "Сегодня" };
                mnuTemplates.AddItem(btnPlanToday).BeginGroup = true;
                btnPlanToday.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.EndOfToday();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };

                BarButtonItem btnPlanTomorow = new BarButtonItem { Caption = "Завтра" };
                mnuTemplates.AddItem(btnPlanTomorow);
                btnPlanTomorow.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.EndOfTomorow();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };

                BarButtonItem btnPlanThedDay = new BarButtonItem { Caption = "Послезавтра" };
                mnuTemplates.AddItem(btnPlanThedDay);
                btnPlanThedDay.ItemClick += delegate
                {
                    _common.dtDateEndPlan.DateTime = Period.EndOfThedDay();
                    _common.tmDateEndPlanTime.Time = _common.dtDateEndPlan.DateTime;
                };
                
                btnChainCreate.DropDownControl = mnuTemplates;
                #endregion

                #region Передача задачи  
                BarButtonItem btnReasign = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = SelectedItem.Workarea.Cashe.ResourceString("BTN_TASKREASIGN_CAPTION"),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.FORWARDGREEN_X32)
                    
                };
                btnReasign.SuperTip = UIHelper.CreateSuperToolTip(btnReasign.Glyph, btnReasign.Caption,
                                            SelectedItem.Workarea.Cashe.ResourceString("BTN_TASKREASIGN_TOOLTIP"));
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnReasign);

                btnReasign.ItemClick += delegate
                                            {
                                                InternalSave();
                                                if (CanClose)
                                                {
                                                    Task newTask = SelectedItem.Reasign();

                                                    newTask.ShowProperty();
                                                    if (CanClose)
                                                        this.frmProp.Close();
                                                }
                                            };

                
                #endregion

                UIHelper.GenerateTooltips<Task>(SelectedItem, _common);
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

        protected override void Print(int printFormId)
        {
            base.Print(printFormId);
            try
            {
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == printFormId);
                string fileName = printLibrary.AssemblyDll.NameFull;
                Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                TaskModel model = new TaskModel();
                model.GetData(SelectedItem);
                report.RegData("Document", model);
                report.Render();
                report.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORPRINT, 1049) + Environment.NewLine + ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDictionary(string dictionaryRu, CultureInfo culture)
        {
            //DICTIONARY_EN
            //DICTIONARY_RU
            FileData fileDictionary = SelectedItem.Workarea.Cashe.GetCasheData<FileData>().ItemCode<FileData>(dictionaryRu);
            if (fileDictionary != null)
            {
                HunspellDictionary result = new HunspellDictionary();
                Stream zipFileStream = new MemoryStream(fileDictionary.StreamData);
                ZipFileCollection files = ZipArchive.Open(zipFileStream);
                try
                {
                    result.LoadFromStream(GetFileStream(files, ".dic"), GetFileStream(files, ".aff"));
                }
                catch
                {
                }
                finally
                {
                    zipFileStream.Dispose();
                    DisposeZipFileStreams(files);
                }
                result.Culture = culture;
                _common.spellChecker1.Dictionaries.Add(result);
            }
        }

        private Stream GetFileStream(ZipFileCollection files, string name)
        {
            foreach (ZipFile file in files)
            {
                if (file.FileName.IndexOf(name) >= 0)
                    return file.FileDataStream;
            }
            return null;
        }

        private void DisposeZipFileStreams(ZipFileCollection files)
        {
            foreach (ZipFile file in files)
                file.FileDataStream.Dispose();
        }
        void cmbUserOwnerId_EditValueChanged(object sender, EventArgs e)
        {
            if(SelectedItem.IsNew && (int)_common.cmbUserToId.EditValue == 0)
            {
                if (_bindingSourceUserToId.Count < 2)
                {
                    _collUserToId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER && f.AgentId != 0).ToList();
                    _bindingSourceUserToId.DataSource = _collUserToId;
                }
                _common.cmbUserToId.EditValue = _common.cmbUserOwnerId.EditValue;
            }
        }
        #region Страница "Файлы"
        private List<IChainAdvanced<Task, FileData>> _collectionFiles;
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
                ChainAdvanced<Task, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Task, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Task, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Task, FileData>;
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
            ChainAdvanced<Task, FileData> link = _bindFiles.Current as ChainAdvanced<Task, FileData>;
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
                ChainAdvanced<Task, FileData> link = _bindFiles.Current as ChainAdvanced<Task, FileData>;
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
            foreach (ChainAdvanced<Task, FileData> link in
                retColl.Select(item => new ChainAdvanced<Task, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
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

                ChainAdvanced<Task, FileData> link =
                    new ChainAdvanced<Task, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

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
        List<FilePreviewThread<Task>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {
                
                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<Task>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<Task> dpt = new FilePreviewThread<Task>(_bindFiles.Current as ChainAdvanced<Task, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Связи с пользовательскими примечаниями
        BrowseChainObject<Message> OnBrowseMessage { get; set; }
        private ControlList _controlMessageView;
        private BindingSource _sourceBindMessages;
        private List<ChainValueView> _collMessageView;
        private void BuildPageMessages()
        {
            if ((SelectedItem as IChainsAdvancedList<Task, Message>) == null)
                return;
            if (_controlMessageView == null)
            {
                _controlMessageView = new ControlList { Name = ExtentionString.CONTROL_MESSAGES };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlMessageView.View,
                                                       "DEFAULT_LISTVIEWCHAINVALUE");
                _controlMessageView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlMessageView);
                _sourceBindMessages = new BindingSource();

                _collMessageView = ChainValueView.GetView<Task, Message>(SelectedItem);
                _sourceBindMessages.DataSource = _collMessageView;
                _controlMessageView.Grid.DataSource = _sourceBindMessages;
                _controlMessageView.View.ExpandAllGroups();
                _controlMessageView.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
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
                        ChainValueView rowValue = _sourceBindMessages.Current as ChainValueView;
                        if (rowValue == null) return;
                    }
                };
                _controlMessageView.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindMessages.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindMessages[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageMessage(imageItem.Workarea, imageItem.RightKind);
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindMessages.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindMessages[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_MESSAGES)];
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

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Message && f.Code == ChainKind.MESSAGE);

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
                        List<Message> newAgent = SelectedItem.Workarea.Empty<Message>().BrowseListType(s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Message>());
                        if (newAgent != null)
                        {
                            foreach (Message selItem in newAgent)
                            {
                                ChainAdvanced<Task, Message> link = new ChainAdvanced<Task, Message>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };
                                
                                try
                                {
                                    link.Save();
                                    ChainValueView view = ChainValueView.ConvertToView<Task, Message>(link);
                                    _sourceBindMessages.Add(view);
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
                btnFastNew.SuperTip = UIHelper.CreateSuperToolTip(btnFastNew.Glyph, btnFastNew.Caption, "Создать быстрое сообщение");
                groupLinksAction.ItemLinks.Add(btnFastNew);
                btnFastNew.ItemClick += delegate
                                            {
                                                ChainKind objectTml = collectionTemplates.First();
                                                Message tmlMsg = SelectedItem.Workarea.GetTemplates<Message>().First(
                                                    s => s.KindValue == Message.KINDVALUE_USER);
                                                Message selItem = SelectedItem.Workarea.CreateNewObject<Message>(tmlMsg);
                                                
                                                selItem.Saved += delegate
                                                                     {
                                                                         Hierarchy h =
                                                                             SelectedItem.Workarea.Cashe.GetCasheData
                                                                                 <Hierarchy>().ItemCode<Hierarchy>(
                                                                                     Hierarchy.SYSTEM_MESSAGE_USERS);
                                                                         h.ContentAdd<Message>(selItem);
                                                                         ChainAdvanced<Task, Message> link = new ChainAdvanced<Task, Message>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };

                                                                         try
                                                                         {
                                                                             link.Save();
                                                                             ChainValueView view = ChainValueView.ConvertToView<Task, Message>(link);
                                                                             _sourceBindMessages.Add(view);
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
                    ChainValueView rowValue = _sourceBindMessages.Current as ChainValueView;
                    if (rowValue == null) return;

                    ChainAdvanced<Task, Message> value = ChainValueView.ConvertToValue<Task, Message>(SelectedItem, rowValue);
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

                    ChainValueView currentObject = _sourceBindMessages.Current as ChainValueView;

                    if (currentObject != null)
                    {
                        ChainAdvanced<Task, Message> value = ChainValueView.ConvertToValue<Task, Message>(SelectedItem, currentObject);
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
                    ChainValueView currentObject = _sourceBindMessages.Current as ChainValueView;
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
                                ChainAdvanced<Task, Message> value = ChainValueView.ConvertToValue<Task, Message>(SelectedItem, currentObject);
                                value.Remove();
                                _sourceBindMessages.Remove(currentObject);
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
                                ChainAdvanced<Task, Message> value = ChainValueView.ConvertToValue<Task, Message>(SelectedItem, currentObject);
                                value.Delete();
                                _sourceBindMessages.Remove(currentObject);
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
                _controlMessageView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlMessageView.Grid;
            HidePageControls(ExtentionString.CONTROL_MESSAGES);
        }
        #endregion

        #region Связи с документами
        private ControlList _controlDocumentView;
        private BindingSource _sourceBindDocuments;
        private List<DocumentValueView> _collDocumentView;
        private void BuildPageLinkDocuments()
        {
            if ((SelectedItem as IChainsAdvancedList<Task, Document>) == null)
                return;
            if (_controlDocumentView == null)
            {
                _controlDocumentView = new ControlList { Name = ExtentionString.CONTROL_LINKDOCUMENTS };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlDocumentView.View,
                                                       "DEFAULT_DOCUMENTVALUEVIEW");
                _controlDocumentView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlDocumentView);
                _sourceBindDocuments = new BindingSource();

                _collDocumentView = DocumentValueView.GetView<Task>(SelectedItem);
                _sourceBindDocuments.DataSource = _collDocumentView;
                _controlDocumentView.Grid.DataSource = _sourceBindDocuments;
                _controlDocumentView.View.ExpandAllGroups();
                FormProperties frmProp = Owner as FormProperties;
                if (frmProp!=null)
                {
                    frmProp.btnRefresh.ItemClick += delegate
                                                        {
                                                            _collDocumentView = DocumentValueView.GetView<Task>(SelectedItem);
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
                                                                    ChainAdvanced<Task, Document> link =
                                                                        new ChainAdvanced<Task, Document>(SelectedItem)
                                                                            {
                                                                                RightId = selItem.Id,
                                                                                KindId = objectTml.Id,
                                                                                StateId = State.STATEACTIVE
                                                                            };

                                                                    try
                                                                    {
                                                                        link.Save();
                                                                        DocumentValueView view =
                                                                            DocumentValueView.ConvertToView<Task, Document>
                                                                                (link);
                                                                        _sourceBindDocuments.Add(view);
                                                                    }
                                                                    catch (DatabaseException dbe)
                                                                    {
                                                                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR),"Создание новой связи невозможно!",dbe.Message, dbe.Id);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        XtraMessageBox.Show(ex.Message,SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR),
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

                    ChainAdvanced<Task, Document> value = DocumentValueView.ConvertToValue<Task, Document>(SelectedItem, rowValue);
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
                        ChainAdvanced<Task, Document> value = DocumentValueView.ConvertToValue<Task, Document>(SelectedItem, currentObject);
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
                                ChainAdvanced<Task, Document> value = DocumentValueView.ConvertToValue<Task, Document>(SelectedItem, currentObject);
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
                                ChainAdvanced<Task, Document> value = DocumentValueView.ConvertToValue<Task, Document>(SelectedItem, currentObject);
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

        #region Связи с событиями
        private ControlList _controlEventView;
        private BindingSource _sourceBindEvents;
        private List<ChainValueView> _collEventView;
        private void BuildPageEvents()
        {
            if ((SelectedItem as IChainsAdvancedList<Task, Event>) == null)
                return;
            if (_controlEventView == null)
            {
                _controlEventView = new ControlList { Name = ExtentionString.CONTROL_LINKEVENTS };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlEventView.View,
                                                       "DEFAULT_LISTVIEWCHAINVALUE");
                _controlEventView.View.GroupFormat = " [#image]{1} {2}";
                Control.Controls.Add(_controlEventView);
                _sourceBindEvents = new BindingSource();

                _collEventView = ChainValueView.GetView<Task, Event>(SelectedItem);
                _sourceBindEvents.DataSource = _collEventView;
                _controlEventView.Grid.DataSource = _sourceBindEvents;
                _controlEventView.View.ExpandAllGroups();
                _controlEventView.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
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
                        ChainValueView rowValue = _sourceBindEvents.Current as ChainValueView;
                        if (rowValue == null) return;
                    }
                };
                _controlEventView.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _sourceBindEvents.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindEvents[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageMessage(imageItem.Workarea, imageItem.RightKind);
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _sourceBindEvents.Count > 0)
                    {
                        ChainValueView imageItem = _sourceBindEvents[e.ListSourceRowIndex] as ChainValueView;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };

                // Построение группы управления дополнительными кодами
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKEVENTS)];
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

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Event && f.Code == ChainKind.EVENT);

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
                        List<Event> newAgent = SelectedItem.Workarea.Empty<Event>().BrowseListType(s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Event>());
                        if (newAgent != null)
                        {
                            foreach (Event selItem in newAgent)
                            {
                                ChainAdvanced<Task, Event> link = new ChainAdvanced<Task, Event>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };

                                try
                                {
                                    link.Save();
                                    ChainValueView view = ChainValueView.ConvertToView<Task, Event>(link);
                                    _sourceBindEvents.Add(view);
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
                btnFastNew.SuperTip = UIHelper.CreateSuperToolTip(btnFastNew.Glyph, btnFastNew.Caption, "Добавить событие сообщения");
                groupLinksAction.ItemLinks.Add(btnFastNew);
                btnFastNew.ItemClick += delegate
                {
                    ChainKind objectTml = collectionTemplates.First();
                    Event tmlMsg = SelectedItem.Workarea.GetTemplates<Event>().First(
                        s => s.KindValue == Event.KINDVALUE_MESSAGE);
                    Event selItem = SelectedItem.Workarea.CreateNewObject<Event>(tmlMsg);
                    selItem.UserOwnerId = SelectedItem.Workarea.CurrentUser.Id;
                    selItem.UserToId = SelectedItem.Workarea.CurrentUser.Id;
                    selItem.StartPlanDate = DateTime.Today;
                    selItem.StartPlanTime= DateTime.Now.TimeOfDay;

                    selItem.Saved += delegate
                    {
                        Hierarchy h =
                            SelectedItem.Workarea.Cashe.GetCasheData
                                <Hierarchy>().ItemCode<Hierarchy>(
                                    Hierarchy.SYSTEM_EVENT_TASK);
                        h.ContentAdd<Event>(selItem);
                        ChainAdvanced<Task, Event> link = new ChainAdvanced<Task, Event>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id, StateId = State.STATEACTIVE };

                        try
                        {
                            link.Save();
                            ChainValueView view = ChainValueView.ConvertToView<Task, Event>(link);
                            _sourceBindEvents.Add(view);
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
                    ChainValueView rowValue = _sourceBindEvents.Current as ChainValueView;
                    if (rowValue == null) return;

                    ChainAdvanced<Task, Event> value = ChainValueView.ConvertToValue<Task, Event>(SelectedItem, rowValue);
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

                    ChainValueView currentObject = _sourceBindEvents.Current as ChainValueView;

                    if (currentObject != null)
                    {
                        ChainAdvanced<Task, Event> value = ChainValueView.ConvertToValue<Task, Event>(SelectedItem, currentObject);
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
                    ChainValueView currentObject = _sourceBindEvents.Current as ChainValueView;
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
                                ChainAdvanced<Task, Event> value = ChainValueView.ConvertToValue<Task, Event>(SelectedItem, currentObject);
                                value.Remove();
                                _sourceBindEvents.Remove(currentObject);
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
                                ChainAdvanced<Task, Event> value = ChainValueView.ConvertToValue<Task, Event>(SelectedItem, currentObject);
                                value.Delete();
                                _sourceBindEvents.Remove(currentObject);
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
                _controlEventView.Dock = DockStyle.Fill;
            }
            CurrentPrintControl = _controlEventView.Grid;
            HidePageControls(ExtentionString.CONTROL_LINKEVENTS);
        }
        #endregion
        void ViewUserToIdCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayUidImagesLookupGrid(e, _bindingSourceUserToId);
        }
        void ViewUserOwnerIdCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayUidImagesLookupGrid(e, _bindingSourceUserOwnerId);
        }
        void ViewTaskStateCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayAnaliticImagesLookupGrid(e, _bindingSourceTaskStateId);
        }
        void ViewPriorityCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            UIHelper.DisplayAnaliticImagesLookupGrid(e, _bindingSourcePriorityId);
        }
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbTaskStateId" && _bindingSourceTaskStateId.Count < 2)
                {
                    Hierarchy rootTaskState = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_TASKSTATE);
                    if (rootTaskState != null)
                        _collTaskStateId = rootTaskState.GetTypeContents<Analitic>();
                    else
                        _collTaskStateId = SelectedItem.Workarea.GetCollection<Analitic>(Analitic.KINDVALUE_TASKSTATE).Where(f => f.KindValue == Analitic.KINDVALUE_TASKSTATE).ToList();

                    _bindingSourceTaskStateId.DataSource = _collTaskStateId;
                }
                else if (cmb.Name == "cmbPriorityId" && _bindingSourcePriorityId.Count < 2)
                {
                    Hierarchy rootPriority = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("CONTRACT_PRIORITY");
                    if (rootPriority != null)
                        _collPriorityId = rootPriority.GetTypeContents<Analitic>();
                    else
                        _collPriorityId = SelectedItem.Workarea.GetCollection<Analitic>(Analitic.KINDVALUE_IMPORTANCE).Where(f => f.KindValue == Analitic.KINDVALUE_IMPORTANCE).ToList();

                    _bindingSourcePriorityId.DataSource = _collPriorityId;
                }
            }
            catch (Exception z)
            { }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
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
                    _collUserOwnerId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER && f.AgentId!=0).ToList();
                    _bindingSourceUserOwnerId.DataSource = _collUserOwnerId;
                }
                else if (cmb.Name == "cmbUserToId" && _bindingSourceUserToId.Count < 2)
                {
                    _collUserToId = SelectedItem.Workarea.GetCollection<Uid>(Uid.KINDVALUE_USER).Where(f => f.KindValue == Uid.KINDVALUE_USER && f.AgentId != 0).ToList();
                    _bindingSourceUserToId.DataSource = _collUserToId;
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