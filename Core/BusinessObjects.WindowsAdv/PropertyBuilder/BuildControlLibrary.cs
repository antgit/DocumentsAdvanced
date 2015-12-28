using System;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlLibrary : BasePropertyControlIBase<Library>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlLibrary()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LIBCOMPOSITION, ExtentionString.CONTROL_LIBCOMPOSITION);
            TotalPages.Add(ExtentionString.CONTROL_LIBRARY_PARAMS, ExtentionString.CONTROL_LIBRARY_PARAMS);
            TotalPages.Add(ExtentionString.CONTROL_LINKFILES, ExtentionString.CONTROL_LINKFILES);
            TotalPages.Add(ExtentionString.CONTROL_LINKRULESET, ExtentionString.CONTROL_LINKRULESET);
            TotalPages.Add(ExtentionString.CONTROL_LINKXMLSTORAGE, ExtentionString.CONTROL_LINKXMLSTORAGE);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtFullName.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.AssemblyVersion = _common.txtVersion.Text;
            SelectedItem.AssemblyId = (int) _common.cmbAssemblyDll.EditValue;
            SelectedItem.AssemblySourceId = (int)_common.cmbSource.EditValue;
            SelectedItem.HelpUrl = _common.edHelpUrl.Text;
            //SelectedItem.FileName = _common.cmbAssemblyDll.Text;
            //SelectedItem.FileNameSource = _common.cmbSource.Text;
            SelectedItem.TypeUrl = _common.txtUrl.Text;
            if (SelectedItem.KindValue == Library.KINDVALUE_PRINTFORM
                || SelectedItem.KindValue == Library.KINDVALUE_WEBPRINTFORM
                || SelectedItem.KindValue == Library.KINDVALUE_DOCFORM 
                || SelectedItem.KindValue == Library.KINDVALUE_METHOD 
                || SelectedItem.KindValue == Library.KINDVALUE_UIMODULE)
            {
                if (_common.cmbLibraryContentType.EditValue!=null)
                    SelectedItem.LibraryTypeId = (int)_common.cmbLibraryContentType.EditValue;
            }
            if (SelectedItem.KindValue == Library.KINDID_REPTBL)
            {
                if (_common.cmbCustomViewList.EditValue!=null)
                    SelectedItem.ListId = (int)_common.cmbCustomViewList.EditValue;
            }
            if (libParams != null)
            {
                libParams.Befor = _controlLibraryParams.txtBefore.Text;
                libParams.After = _controlLibraryParams.txtAfter.Text;

                XmlSerializer serializer = new XmlSerializer(typeof(LibraryReportParams));
                TextWriter write = new StringWriter();
                serializer.Serialize(write, libParams);
                SelectedItem.Params = write.ToString();
                write.Close();
            }

            SaveStateData();

            InternalSave();
        }

        ControlLibrary _common;
        BindingSource _bindingSourceAssemblyDll;
        private List<FileData> _collFileDataAssemblyDll;
        BindingSource _bindingSourceAssemblySource;
        private List<FileData> _collFileDataAssemblySource;
        private List<Library> collLib;
        private BindingSource libBindings;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlLibrary
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtFullName = { Text = SelectedItem.NameFull },
                                  txtVersion = {Text = SelectedItem.AssemblyVersion},
                                  Workarea = SelectedItem.Workarea,
                                  Key = SelectedItem.KindName
                              };
                _common.edHelpUrl.Text = SelectedItem.HelpUrl;
                _common.cmbAssemblyDll.Properties.DisplayMember = GlobalPropertyNames.FullName;
                _common.cmbAssemblyDll.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceAssemblyDll = new BindingSource();
                _collFileDataAssemblyDll = new List<FileData>();
                if (SelectedItem.AssemblyId != 0)
                    _collFileDataAssemblyDll.Add(SelectedItem.Workarea.Cashe.GetCasheData<FileData>().Item(SelectedItem.AssemblyId));
                _bindingSourceAssemblyDll.DataSource = _collFileDataAssemblyDll;
                _common.cmbAssemblyDll.Properties.DataSource = _bindingSourceAssemblyDll;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbAssemblyDll, "DEFAULT_LOOKUP_NAMEFULL");
                _common.cmbAssemblyDll.EditValue = SelectedItem.AssemblyId;
                _common.cmbAssemblyDll.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbAssemblyDll.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbAssemblyDll.EditValue = 0;
                };

                _common.cmbSource.Properties.DisplayMember = GlobalPropertyNames.FullName;
                _common.cmbSource.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceAssemblySource = new BindingSource();
                _collFileDataAssemblySource = new List<FileData>();
                if (SelectedItem.AssemblySourceId != 0)
                    _collFileDataAssemblySource.Add(SelectedItem.Workarea.Cashe.GetCasheData<FileData>().Item(SelectedItem.AssemblySourceId));
                _bindingSourceAssemblySource.DataSource = _collFileDataAssemblySource;
                _common.cmbSource.Properties.DataSource = _bindingSourceAssemblySource;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbSource, "DEFAULT_LOOKUP_NAMEFULL");
                _common.cmbSource.EditValue = SelectedItem.AssemblySourceId;
                _common.cmbSource.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbSource.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbSource.EditValue = 0;
                };
                
                _common.txtUrl.Text = SelectedItem.TypeUrl;
                if (SelectedItem.KindValue == Library.KINDVALUE_PRINTFORM
                    || SelectedItem.KindValue == Library.KINDVALUE_WEBPRINTFORM
                    || SelectedItem.KindValue == Library.KINDVALUE_DOCFORM 
                    || SelectedItem.KindValue == Library.KINDVALUE_METHOD 
                    || SelectedItem.KindValue == Library.KINDVALUE_UIMODULE 
                    || SelectedItem.KindValue == Library.KINDVALUE_REPTBL)
                {

                    _common.cmbLibrary.Properties.DisplayMember = "Name";
                    _common.cmbLibrary.Properties.ValueMember = GlobalPropertyNames.Id;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewLibrary, "DEFAULT_LOOKUP_NAME");
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewContent, "DEFAULT_LISTVIEWLIBRARYCONTENT");
                    _common.cmbLibraryContentType.Properties.DisplayMember = "TypeName";
                    _common.cmbLibraryContentType.Properties.ValueMember = GlobalPropertyNames.Id;
                    _common.ViewLibrary.OptionsView.ShowIndicator = false;
                    _common.ViewContent.OptionsView.ShowIndicator = false;
                    _common.cmbLibrary.KeyDown += CmbLibraryKeyDown;
                    collLib = SelectedItem.Workarea.GetCollection<Library>().Where(s => s.KindValue == 1 || s.KindValue == 8).ToList();
                    libBindings = new BindingSource {DataSource = collLib};
                    _common.cmbLibrary.Properties.DataSource = libBindings;
                    _common.cmbLibrary.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbLibraryContentType.QueryPopUp += CmbGridLookUpEditQueryPopUp2;
                    if (SelectedItem.LibraryTypeId != 0)
                    {
                        int libId = Library.GetLibraryIdByContent(SelectedItem.Workarea,SelectedItem.LibraryTypeId);
                        Library lib = SelectedItem.Workarea.Cashe.GetCasheData<Library>().Item(libId);    
                        _common.cmbLibrary.EditValue = libId;

                        List<LibraryContent> contents = Library.GetLibraryContents(lib);
                        _common.cmbLibraryContentType.DataBindings.Add("EditValue", SelectedItem, "LibraryTypeId");
                        _common.cmbLibraryContentType.Properties.DataSource = contents;
                    }
                    else
                    {
                        _common.cmbLibraryContentType.DataBindings.Add("EditValue", SelectedItem, "LibraryTypeId");
                    }
                    libBindings.PositionChanged += delegate
                                                       {
                                                           Library lib = libBindings.Current as Library;
                                                           List<LibraryContent> contents = Library.GetLibraryContents(lib);
                                                           _common.cmbLibraryContentType.Properties.DataSource = contents;
                                                       };

                    _common.cmbCustomViewList.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbCustomViewList.Properties.ValueMember = GlobalPropertyNames.Id;
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.ViewCustomViewList, "DEFAULT_LOOKUP_NAME");
                    List<CustomViewList> collectionCustomViewList = new List<CustomViewList>();
                    if (SelectedItem.ListId != 0)
                    {
                        collectionCustomViewList.Add(SelectedItem.Workarea.Cashe.GetCasheData<CustomViewList>().Item(SelectedItem.ListId));
                    }
                    BindingSource customViewListBindings = new BindingSource {DataSource = collectionCustomViewList};
                    _common.cmbCustomViewList.Properties.DataSource = customViewListBindings;
                    _common.cmbCustomViewList.EditValue = SelectedItem.ListId;
                    _common.ViewCustomViewList.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                    {
                        if (e.Column.FieldName == "Image" && e.IsGetData && customViewListBindings.Count > 0)
                        {
                            CustomViewList imageItem = customViewListBindings[e.ListSourceRowIndex] as CustomViewList;
                            if (imageItem != null)
                            {
                                e.Value = imageItem.GetImage();
                            }
                        }
                        else if (e.Column.Name == "colStateImage" && e.IsGetData && customViewListBindings.Count > 0)
                        {
                            CustomViewList imageItem = customViewListBindings[e.ListSourceRowIndex] as CustomViewList;
                            if (imageItem != null)
                            {
                                e.Value = imageItem.State.GetImage();
                            }
                        }
                    };
                    _common.cmbCustomViewList.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                    {
                        GridLookUpEdit cmb = sender as GridLookUpEdit;
                        if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                            cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                        try
                        {
                            _common.Cursor = Cursors.WaitCursor;
                            collectionCustomViewList = SelectedItem.Workarea.GetCollection<CustomViewList>().Where(s=>(s.KindValue & 8)==8).ToList();
                            customViewListBindings.DataSource = collectionCustomViewList;
                        }
                        catch (Exception)
                        {


                        }
                        finally
                        {
                            _common.Cursor = Cursors.Default;
                        }
                    };
                    _common.cmbCustomViewList.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbCustomViewList.EditValue = 0;
                    };
                }
                if (SelectedItem.KindValue == Library.KINDVALUE_PRINTFORM
                    || SelectedItem.KindValue == Library.KINDVALUE_WEBPRINTFORM
                    || SelectedItem.KindValue == Library.KINDVALUE_DOCFORM 
                    || SelectedItem.KindValue == Library.KINDVALUE_METHOD 
                    || SelectedItem.KindValue == Library.KINDVALUE_UIMODULE 
                    || SelectedItem.KindValue == Library.KINDVALUE_REPTBL)
                {
                    _common.layoutControlItemLibrary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlItemLibraryContentType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    _common.layoutControlItemLibrary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlItemLibraryContentType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                _common.layoutControlItemCustomViewList.Visibility = SelectedItem.KindValue == Library.KINDVALUE_REPTBL ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                _common.layoutControlItemUrl.Visibility = (SelectedItem.KindValue == Library.KINDVALUE_REPSQL || SelectedItem.KindValue == Library.KINDVALUE_WEBREPORT || SelectedItem.KindValue == Library.KINDVALUE_WEBMODULE) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                BarButtonItem btnAddExists = new BarButtonItem
                                                 {
                                                     Caption = "Обновить содержимое",
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                                 };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnAddExists);
                btnAddExists.ItemClick += delegate
                                              {
                                                  RefreshContent();
                                                  
                                              };
                if (SelectedItem.Workarea.Access.RightCommon.AdminEnterprize && (SelectedItem.KindValue == Library.KINDVALUE_UIMODULE) 
                    || (SelectedItem.KindValue == Library.KINDVALUE_WEBDOCFORM) 
                    || (SelectedItem.KindValue == Library.KINDVALUE_WEBMODULE)
                    || (SelectedItem.KindValue == Library.KINDVALUE_WEBPRINTFORM)
                    || (SelectedItem.KindValue == Library.KINDVALUE_WEBREPORT) 
                    || (SelectedItem.KindValue == Library.KINDVALUE_REPSQL 
                    || SelectedItem.KindValue == Library.KINDVALUE_PRINTFORM))
                {
                    BarButtonItem btnUiAcl = new BarButtonItem
                    {
                        Caption = "Разрешения модуля",
                        RibbonStyle = RibbonItemStyles.Large,
                        Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PROTECTRED_X32)
                    };
                    btnUiAcl.SuperTip = UIHelper.CreateSuperToolTip(btnUiAcl.Glyph, "Разрешения модуля",
                        "Задает разрешения на управление текущим модулем для пользователей");
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnUiAcl);
                    btnUiAcl.ItemClick += delegate
                    {
                        SelectedItem.BrowseModuleRights();
                    };
                }
                if (SelectedItem.KindValue == Library.KINDVALUE_PRINTFORM
                    || SelectedItem.KindValue == Library.KINDVALUE_WEBPRINTFORM
                    || SelectedItem.KindValue == Library.KINDVALUE_REPPRINT 
                    || SelectedItem.KindId== Library.KINDID_WEBREPORT)
                {
                    BarButtonItem btnDesignerSti = new BarButtonItem
                                                       {
                                                           Caption = "Дизайнер отчета",
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph =
                                                               ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                                       ResourceImage.REPORT_X32)
                                                       };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnDesignerSti);
                    btnDesignerSti.ItemClick += delegate
                    {
                        try
                        {
                            if ((int)_common.cmbSource.EditValue == 0)
                                throw new ApplicationException("Отсутствуют данные о печатной форме документа. Выберите в списке файл отчета");
                            byte[] value = SelectedItem.GetSource();
                            Stimulsoft.Report.StiReport rep = new Stimulsoft.Report.StiReport();
                            rep.Load(value);
                            Stimulsoft.Report.Design.StiDesigner.SavingReport += delegate
                            {
                                SelectedItem.AssemblySource.StreamData = rep.SaveToByteArray();
                                MemoryStream stream = new MemoryStream();
                                rep.Compile(stream);
                                if(SelectedItem.AssemblyId!=0)
                                {
                                    SelectedItem.AssemblyDll.StreamData = stream.ToArray();
                                    SelectedItem.AssemblyDll.Save();
                                }
                                else
                                {
                                    FileData fd = new FileData {Name = SelectedItem.AssemblySource.Name, FileExtention = "dll", StateId = State.STATEACTIVE};
                                    fd.Workarea = SelectedItem.Workarea;
                                    fd.KindId = FileData.KINDID_FILEDATA;
                                    fd.StreamData = stream.ToArray();
                                    fd.Save();
                                    _bindingSourceAssemblyDll.Add(fd);
                                    SelectedItem.AssemblyId = fd.Id;
                                }
                                Save();
                            };
                            rep.Design(true);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };


                    BarButtonItem btnExportReportSource = new BarButtonItem
                                                              {
                                                                  Caption = "Экспорт описателя отчета",
                                                                  RibbonStyle = RibbonItemStyles.Large,
                                                                  Glyph =
                                                                      ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                                              ResourceImage.DATAOUT_X32)
                                                              };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnExportReportSource);
                    btnExportReportSource.ItemClick += delegate
                                                           {
                                                               try
                                                               {
                                                                   if (SelectedItem.AssemblySourceId != 0)
                                                                   {
                                                                       SaveFileDialog dlg = new SaveFileDialog
                                                                                                {
                                                                                                    DefaultExt = "mrt",
                                                                                                    InitialDirectory =
                                                                                                        Environment.GetFolderPath(
                                                                                                            Environment.SpecialFolder.MyDocuments)
                                                                                                };

                                                                       dlg.FileName = string.IsNullOrEmpty(SelectedItem.AssemblySource.NameFull)
                                                                                          ? SelectedItem.Name
                                                                                          : SelectedItem.AssemblySource.NameFull;
                                                                       if (dlg.ShowDialog() == DialogResult.OK)
                                                                       {
                                                                           SelectedItem.ExportSourceToFile(dlg.FileName);
                                                                       }
                                                                   }
                                                               }
                                                               catch (Exception ex)
                                                               {
                                                                   XtraMessageBox.Show(ex.Message,
                                                                       SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),                                       
                                                                       MessageBoxButtons.OK,MessageBoxIcon.Error);
                                                               }
                                                           };
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
            LookUpEdit cmb = sender as LookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbAssemblyDll" && _bindingSourceAssemblyDll.Count < 2)
                {
                    _collFileDataAssemblyDll= SelectedItem.Workarea.GetCollection<FileData>();
                    _bindingSourceAssemblyDll.DataSource = _collFileDataAssemblyDll;
                }
                else if (cmb.Name == "cmbSource" && _bindingSourceAssemblySource.Count < 2)
                {
                    _collFileDataAssemblySource = SelectedItem.Workarea.GetCollection<FileData>();
                    _bindingSourceAssemblySource.DataSource = _collFileDataAssemblySource;
                }
                else if (cmb.Name == "cmbLibraryContentType")
                {
                    Library lib = libBindings.Current as Library;
                    List<LibraryContent> contents = Library.GetLibraryContents(lib);
                    _common.cmbLibraryContentType.Properties.DataSource = contents;
                }
                
                //
                //
                //
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        void CmbGridLookUpEditQueryPopUp2(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbLibraryContentType")
                {
                    if(_common.cmbLibrary.EditValue!=null)
                    {
                        int selectedLib = (int) _common.cmbLibrary.EditValue;
                        Library lib = collLib.FirstOrDefault(s => s.Id == selectedLib);
                        List<LibraryContent> contents = Library.GetLibraryContents(lib);
                        _common.cmbLibraryContentType.Properties.DataSource = contents;
                    }
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
        void CmbLibraryKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                _common.cmbLibrary.EditValue = 0;
                _common.cmbLibraryContentType.EditValue = 0;
            }
        }

        ControlList _content;

        /// <summary>
        /// Закладка "Состав библиотеки"
        /// </summary>
        private void BuildPageLibComposition()
        {
            if (_content == null)
            {
                /*_lib_composition = new ControlList();
                _lib_composition.Name = ExtentionString.CONTROL_LIBCOMPOSITION;
                _lib_composition.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "Image" && e.IsGetData)
                    {
                        e.Value = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.CLASS16);
                    }
                };
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _lib_composition.View, "DEFAULT_LISTLIBCONTENT");
                BindingSource _lib_src = new BindingSource();
                _lib_composition.Grid.DataSource = _lib_src;
                _lib_src.DataSource = Library.GetLibraryContents(SelectedItem);
                Control.Controls.Add(_lib_composition);
                _lib_composition.Dock = DockStyle.Fill;*/

                RibbonPageGroup groupLinksAction = new RibbonPageGroup();
                BindingSource data = new BindingSource();
                RepositoryItemTextEdit contentEditor = new RepositoryItemTextEdit();
                RepositoryItemPictureEdit pictureEditor = new RepositoryItemPictureEdit();
                Library item = SelectedItem;
                contentEditor.Name = "ContentEditor";
                _content = new ControlList {Name = ExtentionString.CONTROL_LIBCOMPOSITION};
                _content.Grid.RepositoryItems.Add(pictureEditor);
                _content.Grid.RepositoryItems.Add(contentEditor);

                _content.View.OptionsSelection.MultiSelect = true;
                _content.View.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                _content.View.OptionsSelection.UseIndicatorForSelection = false;
                _content.View.OptionsSelection.EnableAppearanceFocusedCell = false;
                _content.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
                _content.View.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                _content.View.OptionsBehavior.Editable = true;
                //DataGridViewHelper.GenerateGridColumns(item.Workarea, _content.View, "DEFAULT_LISTLIBCONTENT");

                DevExpress.XtraGrid.Columns.GridColumn imageCol = _content.View.Columns.Add();
                imageCol.FieldName = "Image";
                imageCol.Name = "colImage";
                imageCol.OptionsColumn.AllowEdit = false;
                imageCol.OptionsColumn.AllowSize = false;
                imageCol.OptionsColumn.AllowMove = false;
                imageCol.OptionsColumn.FixedWidth = true;
                imageCol.Width = 20;
                imageCol.ColumnEdit = pictureEditor;
                imageCol.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                imageCol.Visible = true;

                DevExpress.XtraGrid.Columns.GridColumn typeNameCol = _content.View.Columns.Add();
                typeNameCol.FieldName = "TypeName";
                typeNameCol.Name = "colTypeName";
                typeNameCol.OptionsColumn.AllowEdit = true;
                typeNameCol.OptionsColumn.AllowSize = true;
                typeNameCol.OptionsColumn.AllowMove = false;
                typeNameCol.OptionsColumn.FixedWidth = false;
                typeNameCol.Width = 150;
                typeNameCol.ColumnEdit = contentEditor;
                typeNameCol.UnboundType = DevExpress.Data.UnboundColumnType.String;
                typeNameCol.Caption = "Имя типа";
                typeNameCol.Visible = true;

                DevExpress.XtraGrid.Columns.GridColumn kindCodeCol = _content.View.Columns.Add();
                kindCodeCol.FieldName = "KindCode";
                kindCodeCol.Name = "colKindCode";
                kindCodeCol.OptionsColumn.AllowEdit = true;
                kindCodeCol.OptionsColumn.AllowSize = true;
                kindCodeCol.OptionsColumn.AllowMove = false;
                kindCodeCol.OptionsColumn.FixedWidth = false;
                kindCodeCol.Width = 150;
                kindCodeCol.ColumnEdit = contentEditor;
                kindCodeCol.UnboundType = DevExpress.Data.UnboundColumnType.String;
                kindCodeCol.Caption = "Код типа";
                kindCodeCol.Visible = true;

                DevExpress.XtraGrid.Columns.GridColumn fullTypeNameCol = _content.View.Columns.Add();
                fullTypeNameCol.FieldName = "FullTypeName";
                fullTypeNameCol.Name = "colFullTypeName";
                fullTypeNameCol.OptionsColumn.AllowEdit = true;
                fullTypeNameCol.OptionsColumn.AllowSize = true;
                fullTypeNameCol.OptionsColumn.AllowMove = false;
                fullTypeNameCol.OptionsColumn.FixedWidth = false;
                fullTypeNameCol.Width = 150;
                fullTypeNameCol.ColumnEdit = contentEditor;
                fullTypeNameCol.UnboundType = DevExpress.Data.UnboundColumnType.String;
                fullTypeNameCol.Caption = "Полное имя типа";
                fullTypeNameCol.Visible = true;

                _content.Grid.DataSource = data;
                Control.Controls.Add(_content);
                _content.Dock = DockStyle.Fill;

                _content.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "Image" && e.IsGetData)
                    {
                        e.Value = ResourceImage.GetByCode(item.Workarea, ResourceImage.CLASS_X16);
                    }
                };
                _content.View.RowUpdated += delegate(object sender, RowObjectEventArgs e)
                {
                    LibraryContent lc = (LibraryContent)e.Row;
                    if (lc.LibraryId == 0)
                    {
                        lc.Workarea = item.Workarea;
                        lc.LibraryId = item.Id;
                    }
                    try
                    {
                        lc.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    frmProp.btnRefresh.PerformClick();
                    
                };

                #region Удаление
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32)
                                              };
                btnDelete.SuperTip = Extentions.CreateSuperToolTip(btnDelete.Glyph, btnDelete.Caption,
                        "Удаляет указанные элементы содержимого библиотеки");
                groupLinksAction.ItemLinks.Add(btnDelete);


                btnDelete.ItemClick += delegate
                {
                    int[] selRows = _content.View.GetSelectedRows();
                    if (selRows.Length > 0)
                    {
                        if (MessageBox.Show("Вы уверены, что хотите удалить указанные элементы?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (int i in selRows)
                            {
                                LibraryContent c = (LibraryContent)_content.View.GetRow(i);
                                if (c != null)
                                    c.Delete();
                            }
                        }
                    }
                    frmProp.btnRefresh.PerformClick();
                };
                #endregion

                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LIBCOMPOSITION)];
                page.Groups.Add(groupLinksAction);

                frmProp.btnRefresh.ItemClick += delegate
                {
                    data.DataSource = Library.GetLibraryContents(item);
                };

                frmProp.btnRefresh.PerformClick();
            }
            HidePageControls(ExtentionString.CONTROL_LIBCOMPOSITION);
        }

        LibraryReportParams libParams;
        ControlLibraryParams _controlLibraryParams;
        /// <summary>
        /// Вкладка дополнительные параметры
        /// </summary>
        private void BuildPageLibraryParams()
        {
            if (_controlLibraryParams == null)
            {
                _controlLibraryParams = new ControlLibraryParams
                {
                    Name = ExtentionString.CONTROL_LIBRARY_PARAMS
                };

                if (string.IsNullOrEmpty(SelectedItem.Params))
                    libParams = new LibraryReportParams();
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LibraryReportParams));
                    TextReader reader = new StringReader(SelectedItem.Params);
                    libParams = (LibraryReportParams)serializer.Deserialize(reader);
                    reader.Close();
                }

                _controlLibraryParams.txtBefore.Text = libParams.Befor;
                _controlLibraryParams.txtAfter.Text = libParams.After;

                // Данные для отображения в списке дополнительными параметрами библиотеки
                BindingSource valuesCollectinBind = new BindingSource { DataSource = libParams.Params };
                _controlLibraryParams.Grid.DataSource = valuesCollectinBind;

                // Построение группы упраления дополнительными параметрами библиотеки
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LIBRARY_PARAMS)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                #region Новая запись
                btnChainCreate.ItemClick += delegate
                {
                    LibraryReportParam newObject = new LibraryReportParam { Workarea = SelectedItem.Workarea };
                    Form frmProperties = newObject.ShowProperty();
                    if (frmProperties.DialogResult == DialogResult.OK)
                    {
                        if (!libParams.Params.Contains(newObject))
                        {
                            int position = valuesCollectinBind.Add(newObject);
                            valuesCollectinBind.Position = position;
                        }
                    };
                };
                #endregion

                BarButtonItem btnProp = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                groupLinksAction.ItemLinks.Add(btnProp);
                #region Свойства
                btnProp.ItemClick += delegate
                {
                    LibraryReportParam currentObject = (LibraryReportParam)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        currentObject.ShowProperty();
                    }
                };
                _controlLibraryParams.Grid.DoubleClick += delegate
                {
                    Point p = _controlLibraryParams.Grid.PointToClient(Control.MousePosition);
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlLibraryParams.View.CalcHitInfo(p.X, p.Y);
                    if (hit.InRow)
                    {
                        LibraryReportParam currentObject = (LibraryReportParam)valuesCollectinBind.Current;
                        if (currentObject != null)
                        {
                            currentObject.ShowProperty();
                        }
                    }
                };
                #endregion

                BarButtonItem btnDelete = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph =
                        ResourceImage.GetByCode(SelectedItem.Workarea,
                                                ResourceImage.DELETE_X32)
                };
                groupLinksAction.ItemLinks.Add(btnDelete);
                #region Удаление
                btnDelete.ItemClick += delegate
                {
                    LibraryReportParam currentObject = (LibraryReportParam)valuesCollectinBind.Current;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea,
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                            "Удаление дополнительного параметра",
                                 string.Empty,
                                 Properties.Resources.STR_CHOICE_DEL);

                        if (res == 0)
                        {
                            try
                            {
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления дополнительного параметра!", dbe.Message, dbe.Id);
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
                                valuesCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления дополнительного параметра!", dbe.Message, dbe.Id);
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

                // TODO: набор колонок для списка дополнительных параметров
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlLibraryParams.View, "DEFAULT_LOOKUP");
                _controlLibraryParams.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        // TODO: добавить иконку для LibraryParams
                        //System.Drawing.Rectangle r = e.Bounds;
                        //System.Drawing.Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.MONEY_X16);
                        //e.Graphics.DrawImageUnscaled(img, r);
                        //e.Handled = true;
                    }
                    if (e.Column.Name == "colStateImage")
                    {
                        //System.Drawing.Rectangle r = e.Bounds;
                        //int index = _controlLibraryParams.View.GetDataSourceRowIndex(e.RowHandle);
                        //LibraryReportParam v = (LibraryReportParam)valuesCollectinBind[index];
                        //System.Drawing.Image img = v.State.GetImage();
                        //e.Graphics.DrawImageUnscaled(img, r);
                        //e.Handled = true;
                    }
                };

                Control.Controls.Add(_controlLibraryParams);
                _controlLibraryParams.Dock = DockStyle.Fill;
            }
            HidePageControls(ExtentionString.CONTROL_LIBRARY_PARAMS);
        }
        #region Страница "Правила"
        ControlList _controlLinksRuleset;
        private List<IChainAdvanced<Library, Ruleset>> _collectionRuleset;
        private BindingSource _bindRuleset;
        BrowseChainObject<Ruleset> OnBrowseRuleset { get; set; }
        /// <summary>
        /// Закладка "Правила и процессы"
        /// </summary>
        private void BuildPageLibraryRuleset()
        {
            if (_controlLinksRuleset == null)
            {
                _controlLinksRuleset = new ControlList { Name = ExtentionString.CONTROL_LINKRULESET };
                // Данные для отображения в списке связей
                _bindRuleset = new BindingSource();
                _collectionRuleset = SelectedItem.GetLinkedRuleset();
                _bindRuleset.DataSource = _collectionRuleset;
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKRULESET)];
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

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.Ruleset);
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
                            OnBrowseRuleset = Extentions.BrowseListType;
                        }
                        List<int> types = objectTml.GetContentTypeKindId();
                        List<Ruleset> newAgent = OnBrowseRuleset.Invoke(SelectedItem.Workarea.Empty<Ruleset>(), s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<Ruleset>());
                        if (newAgent != null)
                        {
                            foreach (Ruleset selItem in newAgent)
                            {
                                ChainAdvanced<Library, Ruleset> link = new ChainAdvanced<Library, Ruleset>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id };
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
                    ((_bindRuleset.Current) as ChainAdvanced<Library, Ruleset>).ShowProperty();
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
                        IChainAdvanced<Library, Ruleset> currentItem = (IChainAdvanced<Library, Ruleset>)_bindRuleset.Current;
                        if (_bindRuleset.Position - 1 > -1)
                        {
                            IChainAdvanced<Library, Ruleset> prevItem = (IChainAdvanced<Library, Ruleset>)_bindRuleset[_bindRuleset.Position - 1];
                            IWorkarea wa = ((IChainAdvanced<Library, Ruleset>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Library, Ruleset>)currentItem, (ChainAdvanced<Library, Ruleset>)prevItem);
                                _controlLinksRuleset.View.UpdateCurrentRow();
                                int indexNext = _controlLinksRuleset.View.GetPrevVisibleRow(_controlLinksRuleset.View.FocusedRowHandle);
                                _controlLinksRuleset.View.RefreshRow(indexNext);
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
                        IChainAdvanced<Library, Ruleset> currentItem = (IChainAdvanced<Library, Ruleset>)_bindRuleset.Current;
                        if (_bindRuleset.Position + 1 < _bindRuleset.Count)
                        {
                            IChainAdvanced<Library, Ruleset> nextItem = (IChainAdvanced<Library, Ruleset>)_bindRuleset[_bindRuleset.Position + 1];
                            IWorkarea wa = ((IChainAdvanced<Library, Ruleset>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Library, Ruleset>)nextItem, (ChainAdvanced<Library, Ruleset>)currentItem);
                                _controlLinksRuleset.View.UpdateCurrentRow();
                                int indexNext = _controlLinksRuleset.View.GetNextVisibleRow(_controlLinksRuleset.View.FocusedRowHandle);
                                _controlLinksRuleset.View.RefreshRow(indexNext);
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
                    ChainAdvanced<Library, Ruleset> currentObject = _bindRuleset.Current as ChainAdvanced<Library, Ruleset>;
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
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlLinksRuleset.View, "DEFAULT_LISTVIEWCHAIN");
                _controlLinksRuleset.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                };
                _controlLinksRuleset.View.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "ImageRight" && e.IsGetData && _bindRuleset.Count > 0)
                    {
                        IChainAdvanced<Library, Ruleset> imageItem = _bindRuleset[e.ListSourceRowIndex] as IChainAdvanced<Library, Ruleset>;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.Right.GetImage();
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindRuleset.Count > 0)
                    {
                        IChainAdvanced<Library, Ruleset> imageItem = _bindRuleset[e.ListSourceRowIndex] as IChainAdvanced<Library, Ruleset>;
                        if (imageItem != null)
                        {
                            e.Value = ExtentionsImage.GetImageState(imageItem.Workarea, imageItem.StateId);
                        }
                    }
                };
                Control.Controls.Add(_controlLinksRuleset);
                _controlLinksRuleset.Dock = DockStyle.Fill;

                _controlLinksRuleset.Grid.DataSource = _bindRuleset;
            }
            CurrentPrintControl = _controlLinksRuleset.Grid;
            HidePageControls(ExtentionString.CONTROL_LINKRULESET);
        } 
        #endregion

        #region Страница "Настройки элемента"
        ControlList _controlLinksXmlStorage;
        private List<IChainAdvanced<Library, XmlStorage>> _collectionXmlStorage;
        private BindingSource _bindXmlStorage;
        BrowseChainObject<XmlStorage> OnBrowseXmlStorage { get; set; }
        /// <summary>
        /// Закладка "Настройки расположения элементов"
        /// </summary>
        private void BuildPageLibraryXmlStorage()
        {
            if (_controlLinksXmlStorage == null)
            {
                _controlLinksXmlStorage = new ControlList { Name = ExtentionString.CONTROL_LINKXMLSTORAGE };
                // Данные для отображения в списке связей
                _bindXmlStorage = new BindingSource();
                _collectionXmlStorage = SelectedItem.GetLinkedXmlStorage();
                _bindXmlStorage.DataSource = _collectionXmlStorage;
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKXMLSTORAGE)];
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

                List<ChainKind> collectionTemplates = SelectedItem.Workarea.CollectionChainKinds.FindAll(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == (int)WhellKnownDbEntity.XmlStorage);
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
                            OnBrowseXmlStorage = Extentions.BrowseListType;
                        }
                        List<int> types = objectTml.GetContentTypeKindId();
                        List<XmlStorage> newAgent = OnBrowseXmlStorage.Invoke(SelectedItem.Workarea.Empty<XmlStorage>(), s => types.Contains(s.KindId), SelectedItem.Workarea.GetCollection<XmlStorage>());
                        if (newAgent != null)
                        {
                            foreach (XmlStorage selItem in newAgent)
                            {
                                ChainAdvanced<Library, XmlStorage> link = new ChainAdvanced<Library, XmlStorage>(SelectedItem) { RightId = selItem.Id, KindId = objectTml.Id };
                                try
                                {
                                    link.Save();
                                    _bindXmlStorage.Add(link);
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
                    ((_bindXmlStorage.Current) as ChainAdvanced<Library, XmlStorage>).ShowProperty();
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
                        IChainAdvanced<Library, XmlStorage> currentItem = (IChainAdvanced<Library, XmlStorage>)_bindXmlStorage.Current;
                        if (_bindXmlStorage.Position - 1 > -1)
                        {
                            IChainAdvanced<Library, XmlStorage> prevItem = (IChainAdvanced<Library, XmlStorage>)_bindXmlStorage[_bindXmlStorage.Position - 1];
                            IWorkarea wa = ((IChainAdvanced<Library, XmlStorage>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Library, XmlStorage>)currentItem, (ChainAdvanced<Library, XmlStorage>)prevItem);
                                _controlLinksXmlStorage.View.UpdateCurrentRow();
                                int indexNext = _controlLinksXmlStorage.View.GetPrevVisibleRow(_controlLinksXmlStorage.View.FocusedRowHandle);
                                _controlLinksXmlStorage.View.RefreshRow(indexNext);
                            }
                            catch (Exception e)
                            {
                                XtraMessageBox.Show(e.Message,
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
                        IChainAdvanced<Library, XmlStorage> currentItem = (IChainAdvanced<Library, XmlStorage>)_bindXmlStorage.Current;
                        if (_bindRuleset.Position + 1 < _bindRuleset.Count)
                        {
                            IChainAdvanced<Library, XmlStorage> nextItem = (IChainAdvanced<Library, XmlStorage>)_bindXmlStorage[_bindXmlStorage.Position + 1];
                            IWorkarea wa = ((IChainAdvanced<Library, XmlStorage>)currentItem).Workarea;
                            try
                            {
                                wa.Swap((ChainAdvanced<Library, XmlStorage>)nextItem, (ChainAdvanced<Library, XmlStorage>)currentItem);
                                _controlLinksXmlStorage.View.UpdateCurrentRow();
                                int indexNext = _controlLinksXmlStorage.View.GetNextVisibleRow(_controlLinksXmlStorage.View.FocusedRowHandle);
                                _controlLinksXmlStorage.View.RefreshRow(indexNext);
                            }
                            catch (Exception e)
                            {
                                XtraMessageBox.Show(e.Message,
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
                    ChainAdvanced<Library, XmlStorage> currentObject = _bindXmlStorage.Current as ChainAdvanced<Library, XmlStorage>;
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
                                _bindXmlStorage.Remove(currentObject);
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
                                currentObject.Delete();
                                _bindXmlStorage.Remove(currentObject);
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
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlLinksXmlStorage.View, "DEFAULT_LISTVIEWCHAIN");
                _controlLinksXmlStorage.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                };
                Control.Controls.Add(_controlLinksXmlStorage);
                _controlLinksXmlStorage.Dock = DockStyle.Fill;

                _controlLinksXmlStorage.Grid.DataSource = _bindXmlStorage;
            }
            CurrentPrintControl = _controlLinksXmlStorage.Grid;
            HidePageControls(ExtentionString.CONTROL_LINKXMLSTORAGE);
        }
        #endregion

        #region Страница "Файлы"
        private List<IChainAdvanced<Library, FileData>> _collectionFiles;
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
                ChainAdvanced<Library, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Library, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Library, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Library, FileData>;
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
            ChainAdvanced<Library, FileData> link = _bindFiles.Current as ChainAdvanced<Library, FileData>;
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
                ChainAdvanced<Library, FileData> link = _bindFiles.Current as ChainAdvanced<Library, FileData>;
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
            foreach (ChainAdvanced<Library, FileData> link in
                retColl.Select(item => new ChainAdvanced<Library, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
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

                ChainAdvanced<Library, FileData> link =
                    new ChainAdvanced<Library, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

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
        List<FilePreviewThread<Library>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<Library>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<Library> dpt = new FilePreviewThread<Library>(_bindFiles.Current as ChainAdvanced<Library, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if (SelectedItem.KindValue == 5 && SelectedItem.KindValue == 4)
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_LIBRARY_PARAMS))
                    TotalPages.Remove(ExtentionString.CONTROL_LIBRARY_PARAMS);
            }
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_LIBCOMPOSITION)
                BuildPageLibComposition();
            if (value == ExtentionString.CONTROL_LIBRARY_PARAMS)
                BuildPageLibraryParams();
            if (value == ExtentionString.CONTROL_LINKRULESET)
                BuildPageLibraryRuleset();
            if (value == ExtentionString.CONTROL_LINKXMLSTORAGE)
                BuildPageLibraryXmlStorage();
            if (value == ExtentionString.CONTROL_LINKFILES)
                BuildPageLinkedFiles();
        }
        // TODO: Срочно!!!
        //void TxtAssemblyDllButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    if(e.Button.Index == 1)
        //    {
        //        OpenFileDialog dlg = new OpenFileDialog { Filter = "Библиотеки|*.dll|Приложения|*.exe" };
        //        dlg.FileName = _common.cmbAssemblyDll.Text;
        //        if (dlg.ShowDialog() == DialogResult.OK)
        //        {
        //            SelectedItem.SetLibrary(dlg.FileName);
        //            _common.cmbAssemblyDll.Text = Path.GetFileName(dlg.FileName);
        //            SelectedItem.FileName = _common.cmbAssemblyDll.Text;
        //            if (String.IsNullOrEmpty(_common.txtName.Text) || SelectedItem.IsNew)
        //            {
        //                _common.txtName.Text = SelectedItem.FileName;
        //            }
        //            SelectedItem.FullName = SelectedItem.GetAssembly().FullName;
        //            _common.txtFullName.Text = SelectedItem.FullName;

        //            object[] attributes = SelectedItem.GetAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
        //            if (attributes.Length > 0)
        //            {
        //                ((ICoreObject)SelectedItem).Guid = new Guid(((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value);
        //            }
        //        }
        //    }
        //    else if (e.Button.Index == 1)
        //    {
        //        if (XtraMessageBox.Show("Удалить данные о содержимом библиотеки?", 
        //            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),  
        //            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            SelectedItem.ClearLib();
        //            _common.cmbAssemblyDll.Text = string.Empty;
        //        }
        //    }
 
        //}

        //void TxtSourceButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    if (e.Button.Index == 1)
        //    {
        //        OpenFileDialog dlg = new OpenFileDialog
        //                                 {
        //                                     Filter = "Библиотеки|*.dll|Отчеты StimulReport|*.mrt|Все файлы|*.*",
        //                                     FilterIndex = 2,
        //                                     FileName = _common.cmbSource.Text
        //                                 };
        //        if (dlg.ShowDialog() == DialogResult.OK)
        //        {
        //            SelectedItem.SetSource(dlg.FileName);
        //            _common.cmbSource.Text = Path.GetFileName(dlg.FileName);
        //            if (SelectedItem.KindValue == 8)
        //            {
        //                if (!PrintService.IsInit)
        //                {
        //                    PrintService.Workarea = SelectedItem.Workarea;
        //                    PrintService.InitConfig();
        //                }
        //                byte[] value = SelectedItem.GetSource();
        //                Stimulsoft.Report.StiReport rep = new Stimulsoft.Report.StiReport();
        //                rep.Load(value);
        //                SelectedItem.SetSource(rep.SaveToByteArray());
        //                MemoryStream stream = new MemoryStream();
        //                //rep.Compile()
        //                rep.Compile(stream);
        //                SelectedItem.SetLibrary(stream.ToArray());
        //                Save();
        //            }
        //        }
        //    }
        //    else if(e.Button.Index == 1)
        //    {
        //        if(XtraMessageBox.Show("Удалить данные о исходном коде?",
        //            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            SelectedItem.ClearSource();
        //            _common.cmbSource.Text = string.Empty;
        //        }
        //    }
        //}

        void RefreshContent()
        {
            System.Reflection.Assembly asmCurrent = SelectedItem.GetAssembly();
            if (asmCurrent == null) return;
            Type[] theTypesCurrent = asmCurrent.GetTypes();
            for (int i = 0; i < theTypesCurrent.Length; i++)
            {
                Type t = theTypesCurrent[i].GetInterface("BusinessObjects.IDocumentView");

                if (t != null)
                {
                    if (!theTypesCurrent[i].IsAbstract)
                    {
                        int sValue = Library.LibraryContentFind(SelectedItem.Workarea, theTypesCurrent[i].Name, SelectedItem.Id,
                                                           "BusinessObjects.IDocumentView",
                                                           theTypesCurrent[i].FullName);
                        if (sValue == 0)
                        {
                            LibraryContent cont = new LibraryContent
                            {
                                Workarea = SelectedItem.Workarea,
                                FullTypeName = theTypesCurrent[i].FullName,
                                LibraryId = SelectedItem.Id,
                                KindCode = "BusinessObjects.IDocumentView",
                                TypeName = theTypesCurrent[i].Name
                            };
                            cont.Save();
                        }
                    }
                }
                //t = theTypesCurrent[i].GetInterface("BusinessObjects.Rules.IBusinessMethod");

                //if (t != null)
                //{
                //    if (!theTypesCurrent[i].IsAbstract)
                //    {
                //        int sValue = SelectedItem.Workarea.LibraryContentFind(theTypesCurrent[i].Name, SelectedItem.Id,
                //                                           "BusinessObjects.Rules.IBusinessMethod",
                //                                           theTypesCurrent[i].FullName);
                //        if (sValue == 0)
                //        {
                //            LibraryContent cont = new LibraryContent
                //            {
                //                Workarea = SelectedItem.Workarea,
                //                FullTypeName = theTypesCurrent[i].FullName,
                //                LibraryId = SelectedItem.Id,
                //                KindCode = "BusinessObjects.Rules.IBusinessMethod",
                //                TypeName = theTypesCurrent[i].Name
                //            };
                //            cont.Save();
                //        }
                //    }
                //}

                t = theTypesCurrent[i].GetInterface("BusinessObjects.Windows.IContentModule");

                if (t != null)
                {
                    if (!theTypesCurrent[i].IsAbstract)
                    {
                        int sValue = Library.LibraryContentFind(SelectedItem.Workarea, theTypesCurrent[i].Name, SelectedItem.Id,
                                                           "BusinessObjects.Windows.IContentModule",
                                                           theTypesCurrent[i].FullName);
                        if (sValue == 0)
                        {
                            LibraryContent cont = new LibraryContent
                            {
                                Workarea = SelectedItem.Workarea,
                                FullTypeName = theTypesCurrent[i].FullName,
                                LibraryId = SelectedItem.Id,
                                KindCode = "BusinessObjects.Windows.IContentModule",
                                TypeName = theTypesCurrent[i].Name
                            };
                            cont.Save();
                        }
                    }
                }
                t = theTypesCurrent[i].GetInterface("BusinessObjects.IVirtualGroupBuilderExtender");

                if (t != null)
                {
                    if (!theTypesCurrent[i].IsAbstract)
                    {
                        int sValue = Library.LibraryContentFind(SelectedItem.Workarea, theTypesCurrent[i].Name, SelectedItem.Id,
                                                           "BusinessObjects.IVirtualGroupBuilderExtender",
                                                           theTypesCurrent[i].FullName);
                        if (sValue == 0)
                        {
                            LibraryContent cont = new LibraryContent
                            {
                                Workarea = SelectedItem.Workarea,
                                FullTypeName = theTypesCurrent[i].FullName,
                                LibraryId = SelectedItem.Id,
                                KindCode = "BusinessObjects.IVirtualGroupBuilderExtender",
                                TypeName = theTypesCurrent[i].Name
                            };
                            cont.Save();
                        }
                    }
                }
                
            }
        }
    }
}