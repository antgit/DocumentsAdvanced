using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Windows;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.News
{
    public class ContentModuleNews : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        /// <summary>
        /// Настройки модуля
        /// </summary>
        [Serializable]
        public class Options
        {
            /// <summary>
            /// Конструктор
            /// </summary>
            public Options()
            {

            }
            //public string BackupDir { get; set; }
            /// <summary>
            /// Сохранить текщие настройки модуля
            /// </summary>
            public void Save(Workarea wa)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Options));
                StringBuilder sb = new StringBuilder();
                StringWriter writer = new StringWriter(sb);
                serializer.Serialize(writer, this);
                //return sb.ToString();

                List<XmlStorage> storage = wa.GetCollection<XmlStorage>();
                const string key = TYPENAME + "_OPTIONS";

                XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key) ??
                                      new XmlStorage { Workarea = wa, Code = key, KindId = 2359300 };

                keyValue.XmlData = sb.ToString();
                if (string.IsNullOrEmpty(keyValue.Name))
                    keyValue.Name = keyValue.Code;

                keyValue.Save();

            }

            public static Options Load(Workarea wa)
            {

                const string key = TYPENAME + "_OPTIONS";
                List<XmlStorage> storage = wa.Empty<XmlStorage>().FindBy(code: key);
                XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key);
                if (keyValue == null) return null;

                XmlSerializer serializer = new XmlSerializer(typeof(Options));
                StringReader reader = new StringReader(keyValue.XmlData);
                return (Options)serializer.Deserialize(reader);
            }
        }

        private Options _options;
// ReSharper disable InconsistentNaming
        private const string TYPENAME = "MODULEWHATSNEWS";
// ReSharper restore InconsistentNaming

        public ContentModuleNews()
        {
            Caption = "Что нового";
            Key = TYPENAME + "_MODULE";
        }
        #region IContentModule Members
        private Library _selfLib;
        public Library SelfLibrary
        {
            get
            {
                if (_selfLib == null && Workarea != null)
                    _selfLib = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(Key);
                return _selfLib;
            }
        }
        private string _parentKey;
        public string ParentKey
        {
            get
            {
                if (_parentKey == null && Workarea != null)
                {
                    if (SelfLibrary != null)
                    {
                        int? fHierarchyId = Hierarchy.FirstHierarchy<Library>(SelfLibrary);
                        if (fHierarchyId.HasValue && fHierarchyId.Value != 0)
                        {
                            Hierarchy h = Workarea.Cashe.GetCasheData<Hierarchy>().Item(fHierarchyId.Value);
                            _parentKey = UIHelper.FindParentHierarchy(h);

                        }

                    }

                }
                return _parentKey;
            }
            set { _parentKey = value; }
        }
        public void InvokeHelp()
        {
            Library lib = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(Key);
            List<FactView> prop = lib.GetCollectionFactView();
            FactView viewHelpLocation = prop.FirstOrDefault(f => f.FactNameCode == "HELPDOC" & f.ColumnCode == "HELPLINKINET");
            if (viewHelpLocation == null || string.IsNullOrWhiteSpace(viewHelpLocation.ValueString))
                XtraMessageBox.Show("Справочная информация отсутствует!", Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                System.Diagnostics.Process.Start(viewHelpLocation.ValueString);
            }
        }
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
            set
            {
                _workarea = value;
                SetImage();
            }
        }
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected virtual void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.NOTE_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlModuleNews _control;
        public Control Control
        {
            get
            {
                return _control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private BindingSource _bindingSourceNews;
        private List<string> categories;
        private List<string> authors;
        private List<string> currentstates;
        public void PerformShow()
        {
            if (_control == null)
            {
                _control = new ControlModuleNews();
                _control.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                _options = Options.Load(Workarea);
                if (_options == null)
                    _options = new Options();


                _bindingSourceNews = new BindingSource();
                _control.Grid.DataSource = _bindingSourceNews;
                //_control.txtMemo.DataBindings.Add("Text", _bindingSourceNews.Current, "Memo");
                _control.View.FocusedRowChanged += delegate
                                                       {
                                                           if(_bindingSourceNews.Current==null)
                                                               _control.txtMemo.Text = string.Empty;
                                                           else
                                                               _control.txtMemo.Text = (_bindingSourceNews.Current as WhatNew).Memo;
                                                       };
                RegisterPageAction();
                InvokeRefresh();
            }

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;
        }


        private void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksActionList = page.GetGroupByName(Key + "_ACTIONLIST");
            if (_groupLinksActionList == null)
            {
                _groupLinksActionList = new RibbonPageGroup { Name = Key + "_ACTIONLIST", Text = Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049) };

                #region Обновить

                BarButtonItem btnRefresh = new BarButtonItem
                {
                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksActionList.ItemLinks.Add(btnRefresh);
                btnRefresh.ItemClick += delegate
                {
                    InvokeRefresh();
                };

                #endregion

                #region Создать

                BarButtonItem _btnNewDocument = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.NEW_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnNewDocument);
                _btnNewDocument.ItemClick += delegate
                                                 {
                                                     InvokeShowEdit(true);
                                                 };
                #endregion

                #region Изменить
                
                BarButtonItem _btnEdit = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.EDIT_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnEdit);
                _btnEdit.ItemClick += delegate
                {
                    InvokeShowEdit(false);   
                };
                #endregion

                #region Удалить

                BarButtonItem _btnDelete = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnDelete);
                _btnDelete.ItemClick += delegate
                                            {
                                                InvokeDelete();
                                            };
                #endregion
                page.Groups.Add(_groupLinksActionList);
            }
        }

        private void InvokeDelete()
        {
            if (_bindingSourceNews.Current == null)
                return;
            WhatNew obj = _bindingSourceNews.Current as WhatNew;
            if (obj != null)
            {
                obj.Delete();
                _bindingSourceNews.RemoveCurrent();
            }
        }
        /// <summary>
        /// Показать свойства
        /// </summary>
        /// <param name="asNew">Для новой записи</param>
        private void InvokeShowEdit(bool asNew)
        {
            WhatNew item = null;
            if (!asNew && _bindingSourceNews.Current != null)
            {
                item = _bindingSourceNews.Current as WhatNew;
            }
            else
                item = new WhatNew {Workarea = Workarea, Date = DateTime.Today, DateEnd = DateTime.Today};

            if (item == null)
                return;
            FormProperties frm = new FormProperties();
            frm.btnSave.Visibility = BarItemVisibility.Never;
            ControlWhatsNew ctl = new ControlWhatsNew();
            ctl.Dock = DockStyle.Fill;
            frm.clientPanel.Controls.Add(ctl);
            frm.btnSaveClose.Visibility = BarItemVisibility.Always;
            ctl.cmbCategory.Text = item.Name;
            ctl.cmbAuthor.Text = item.Author;
            ctl.cmbState.Text = item.CurrentState;
            ctl.edDateEnd.EditValue = item.DateEnd;
            ctl.txtMemo.Text = item.Memo;
            ctl.edDate.DateTime = item.Date;

            ctl.cmbCategory.Properties.Items.AddRange(categories);
            ctl.cmbAuthor.Properties.Items.AddRange(authors);
            ctl.cmbState.Properties.Items.AddRange(currentstates);
            frm.MinimumSize = new Size(600, 500);
            frm.btnSaveClose.ItemClick += delegate
                                              {
                                                  item.Name = ctl.cmbCategory.Text;
                                                  item.Memo = ctl.txtMemo.Text;
                                                  item.Date = ctl.edDate.DateTime;
                                                  item.Author = ctl.cmbAuthor.Text;
                                                  item.CurrentState = ctl.cmbState.Text;
                                                  if (ctl.edDateEnd.EditValue == null)
                                                      item.DateEnd = null;
                                                  else
                                                      item.DateEnd = ctl.edDateEnd.DateTime;
                                                  item.Save();
                                                  if(!_bindingSourceNews.Contains(item))
                                                      _bindingSourceNews.Add(item);
                                              };
            frm.Shown += delegate
                             {
                                 ctl.ActiveControl = ctl.cmbCategory;
                             };
            frm.ShowDialog();
        }

        private void InvokeRefresh()
        {
            //options.BackupDir = control.btnEditFolder.Text;
            _options.Save(Workarea);

            try
            {
                _bindingSourceNews.DataSource = WhatNew.GetCollection(Workarea);
                _control.Grid.DataSource = _bindingSourceNews;
                categories = WhatNew.GetCategories(Workarea);
                authors = WhatNew.GetAuthors(Workarea);
                currentstates = WhatNew.GetCurrentStates(Workarea);
            }
            finally
            {
                _control.Cursor = Cursors.Default;
            }
        }

        

        public void PerformHide()
        {
            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = false;
        }
        public Form Owner { get; set; }
        public void ShowNewWindows()
        {

            FormProperties frm = new FormProperties
            {
                Width = 1000,
                Height = 600
            };
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.NOTE_X16);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


            IContentModule module = new ContentModuleNews();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion

    }
}
