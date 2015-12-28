using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Файлы"
    /// </summary>
    public sealed class ContentModuleFileData : ContentModuleBase<FileData>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Файлы".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleFileData()
        {
            Caption = "Файлы";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModule_CreateTreeList;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.FILEDATA_X32); 
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<FileData> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<FileData> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<FileData> _saveAnalitic;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(FileData value)
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
        void OnShowPropTreeList(FileData value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                 {
                     /*int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                     treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;*/
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
        void OnSaveObject(FileData value)
        {
            value.Save();
        }
        /// <summary>
        /// Обработчик события отображения модуля
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<FileData>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<FileData>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<FileData>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
            }
        }

        void ContentModule_CreateTreeList(object sender, EventArgs e)
        {
            #region Обновление файлов
            if (SecureLibrary.IsAllow(UserRightElement.UIFILEDATAUPDATE, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
            {
                BarButtonItem btnUpdateLibrarires = new BarButtonItem
                {
                    Caption = "Обновление файлов",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32)
                };
                btnUpdateLibrarires.SuperTip = UIHelper.CreateSuperToolTip(btnUpdateLibrarires.Glyph, "Обновление файлов",
                    "Обновление существующих библиотек в базе данных. Обновляются только библиотеки имеющие расширение файла dll. Сервисная функция для обновления программы.");
                groupLinksActionTreeList.ItemLinks.Insert(btnCommon.Links[0], btnUpdateLibrarires);
                btnUpdateLibrarires.ItemClick += delegate
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "Наиболее используемые|*.exe;*.dll;*.mrt|Библиотеки|*.dll|Печатные формы документов|*.mrt|Приложения|*.exe|Файлы конфигурации|*.config|SQL файлы|*.sql";
                    //|Печатные формы документов|*.mrt|Файлы конфигурации|*.config
                    dlg.Multiselect = true;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        //List<FileData> coll = TreeBrowser.SelectedHierarchy.GetTypeContents<FileData>();


                        foreach (string v in dlg.FileNames)
                        {

                            string fileName = Path.GetFileNameWithoutExtension(v);
                            string fileExtention = Path.GetExtension(v).Substring(1);
                            bool IsDone = false;
                            FileData findObj = null;
                            List<FileData> coll = Workarea.Empty<FileData>().FindByFullName(fileName, fileExtention, TreeListBrowser.TreeBrowser.SelectedHierarchyId);
                            if (coll.Count == 1)
                                findObj = coll[0];

                            if (findObj != null)
                            {
                                findObj.SetStreamFromFile(v);
                                findObj.Save();
                                IsDone = true;

                                if (!IsDone)
                                {
                                    XtraMessageBox.Show(string.Format("Библиотека {0} не обновлена!", fileName),
                                                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(string.Format("Библиотека {0} не зарегестрирована в системе!", fileName),
                                                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        XtraMessageBox.Show("Обновление выполнено! Перезапустите программу если вы обновляли системные файлы",
                                                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
            }
            #endregion

            #region Импорт файла
            if (SecureLibrary.IsAllow(UserRightElement.UIFILEDATAIMPORT, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
            {
                BarButtonItem btnImport = new BarButtonItem
                {
                    Caption = "Импорт файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DATAINTO_X32)
                };
                groupLinksActionTreeList.ItemLinks.Insert(btnCommon.Links[0], btnImport);
                btnImport.ItemClick += delegate
                {
                    try
                    {
                        OpenFileDialog dlgImport = new OpenFileDialog
                        {
                            //FileName = NameFull,
                            Filter = "Все файлы|*.*|Библиотеки|*.dll|Приложения|*.exe|Печатные формы|*.mrt|SQL файлы|*.sql",
                            //InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            Multiselect = true
                        };

                        if (dlgImport.ShowDialog() == DialogResult.OK)
                        {
                            foreach (string v in dlgImport.FileNames)
                            {

                                string fileName = System.IO.Path.GetFileNameWithoutExtension(v);
                                string fileExtention = System.IO.Path.GetExtension(v).Substring(1);

                                FileData findObj = null;
                                List<FileData> coll = Workarea.Empty<FileData>().FindByFullName(fileName, fileExtention, TreeListBrowser.TreeBrowser.SelectedHierarchyId);

                                if (coll.Count == 1)
                                    findObj = coll[0];

                                if (findObj != null)
                                {
                                    findObj.SetStreamFromFile(v);
                                    findObj.Save();
                                }
                                else
                                {
                                    FileData tml = Workarea.GetTemplates<FileData>()[0];
                                    findObj = Workarea.CreateNewObject<FileData>(tml);
                                    findObj.SetStreamFromFile(v);
                                    findObj.Save();
                                    TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd<FileData>(findObj);
                                }
                                TreeListBrowser.TreeBrowser.InvokeRefresh();
                                TreeListBrowser.RequestResreshOnNodeChange = true;
                                var eV = new FocusedNodeChangedEventArgs(null, TreeListBrowser.TreeBrowser.ControlTree.Tree.FocusedNode);
                                TreeListBrowser.TreeFocusedNodeChanged(TreeListBrowser.TreeBrowser.ControlTree.Tree, eV);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
            }
            #endregion
        }
    }
}