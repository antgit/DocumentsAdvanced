using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль управления корневыми иерархиями
    /// </summary>
    public class ContentModuleHierarchyRoot : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        // ReSharper disable InconsistentNaming
        private const string TYPENAME = "HIERARHYROOT";
        // ReSharper restore InconsistentNaming

        public ContentModuleHierarchyRoot()
        {
            Caption = "Управление корневыми группами";
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

        /// <summary>Изображение</summary>
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;

        /// <summary>Рабочая область</summary>
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.FOLDER_X32);
        }

        /// <summary>Ключ</summary>
        public string Key { get; set; }

        /// <summary>Название</summary>
        public string Caption { get; set; }

        private ControlModuleHierarchyRoot _control;

        /// <summary>Основной контрол для отображения</summary>
        public Control Control
        {
            get
            {
                return _control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private BindingSource _bindingSourceMain;

        /// <summary>Показать</summary>
        public void PerformShow()
        {
            if (_control == null)
            {
                _control = new ControlModuleHierarchyRoot();
                _control.HelpRequested += delegate
                {
                    InvokeHelp();
                };

                _bindingSourceMain = new BindingSource();
                _control.Grid.DataSource = _bindingSourceMain;
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
            if (_bindingSourceMain.Current == null)
                return;
            Hierarchy obj = _bindingSourceMain.Current as Hierarchy;
            if (obj != null)
            {
                try
                {
                    obj.Delete();
                    _bindingSourceMain.RemoveCurrent();
                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(Workarea,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    Extentions.ShowMessagesExeption(Workarea,
                                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                    Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                    ex);
                }
            }
        }
        /// <summary>
        /// Показать свойства
        /// </summary>
        /// <param name="asNew">Для новой записи</param>
        private void InvokeShowEdit(bool asNew)
        {
            Hierarchy item = null;
            if (!asNew && _bindingSourceMain.Current != null)
            {
                item = _bindingSourceMain.Current as Hierarchy;
                item.ShowProperty();
            }
            else
            {
                item = Workarea.Empty<Hierarchy>().ShowCreateWizard();
                if(item!=null && !item.IsNew)
                {
                    
                    Hierarchy newHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = item.Id,
                        KindId = item.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Избранное",
                        Code = Hierarchy.GetSystemFavoriteCodeValue((WhellKnownDbEntity)item.ContentEntityId)
                    };
                    newHierarchy.Save();
                    
                    newHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = item.Id,
                        KindId = item.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Поиск",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.FindRoot)
                    };
                    newHierarchy.Save();

                    Hierarchy newFindHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = newHierarchy.Id,
                        KindId = newHierarchy.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Все данные",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.All)
                    };
                    newFindHierarchy.Save();

                    newFindHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = newHierarchy.Id,
                        KindId = newHierarchy.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Запрещенные",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.Deny)
                    };
                    newFindHierarchy.Save();

                    newFindHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = newHierarchy.Id,
                        KindId = newHierarchy.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Не входящие в группы",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.NotInGroup)
                    };
                    newFindHierarchy.Save();
                    
                    newFindHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = newHierarchy.Id,
                        KindId = newHierarchy.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Системные",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.System)
                    };
                    newFindHierarchy.Save();

                    newFindHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = newHierarchy.Id,
                        KindId = newHierarchy.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Требуют корректировки",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.NotDone)
                    };
                    newFindHierarchy.Save();

                    newFindHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = newHierarchy.Id,
                        KindId = newHierarchy.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Удаленные",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.Trash)
                    };
                    newFindHierarchy.Save();

                    newFindHierarchy = new Hierarchy
                    {
                        Workarea = Workarea,
                        ContentEntityId = item.ContentEntityId,
                        ParentId = newHierarchy.Id,
                        KindId = newHierarchy.KindId,
                        StateId = State.STATEACTIVE,
                        Name = "Шаблоны",
                        Code = Hierarchy.GetSystemFindCodeValue((WhellKnownDbEntity)item.ContentEntityId, HierarchyCodeKind.Template)
                    };
                    newFindHierarchy.Save();


                    _bindingSourceMain.Add(item);
                }
            }
        }

        private void InvokeRefresh()
        {
            try
            {

                _bindingSourceMain.DataSource = Workarea.Empty<Hierarchy>().GetCollectionHierarchy();
                _control.Grid.DataSource = _bindingSourceMain;
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

        /// <summary>Форма - владелец на которой необходимо отображать модуль</summary>
        public Form Owner { get; set; }

        /// <summary>Показать в новом окне</summary>
        public void ShowNewWindows()
        {

            FormProperties frm = new FormProperties
            {
                Width = 1000,
                Height = 600
            };
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.FOLDER_X32);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


            IContentModule module = new ContentModuleHierarchyRoot();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion

    }
}
