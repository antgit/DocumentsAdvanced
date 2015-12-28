using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства статьи базы знаний
        /// </summary>
        /// <param name="item">Статья базы знаний</param>
        /// <returns></returns>
        public static Form ShowProperty(this Knowledge item)
        {
            InternalShowPropertyBase<Knowledge> showPropertyBase = new InternalShowPropertyBase<Knowledge>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlKnowledge { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }

        /// <summary>
        /// Просмотр статьи базы знаний
        /// </summary>
        /// <param name="item">Объект "Статья базы знаний"</param>
        /// <param name="external">Использовать внешний просмотр, для интернет статьи соответствует открытие в интернет браузере</param>
        public static void ShowKnowledge(this Knowledge item, bool external=true)
        {
            if (item == null)
                return;

            switch (item.KindId)
            {
                case Knowledge.KINDID_ONLINE:
                    if(external)
                    {
                        System.Diagnostics.Process.Start(item.CodeFind);
                    }
                    else
                    {
                        FormWebViewer frm = new FormWebViewer();
                        Bitmap img = ResourceImage.GetByCode(item.Workarea, ResourceImage.REPORT_X16);
                        frm.Icon = Icon.FromHandle(img.GetHicon());
                        CreateOpenWindowsButton(frm.Ribbon, item.Workarea);
                        frm.Text = item.Name;
                        frm.btnClose.Glyph = ResourceImage.GetSystemImage(ResourceImage.EXIT_X32);
                        frm.btnNext.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.FORWARDGREEN_X32);
                        frm.btnBack.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.BACKGREEN_X32);
                        frm.Ribbon.ApplicationIcon = img;
                        //string navstring = System.Web.HttpUtility.UrlEncode("/Документы2010/Управление продажами/Продажи за период");

                        frm.Shown += delegate
                        {
                            frm.Browser.Navigate(item.CodeFind);
                        };
                        frm.Show();
                    }
                    break;
                case Knowledge.KINDID_LOCAL:
                    string filename = Path.GetTempFileName() + "." + item.File.FileExtention;

                    using(BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                    {
                        writer.Write(item.File.StreamData);
                    }

                    //System.Diagnostics.Process.Start(filename);
                    System.Diagnostics.Process prc = new System.Diagnostics.Process();
                            prc.StartInfo.FileName = filename;
                            prc.StartInfo.Verb = "Open";
                            prc.EnableRaisingEvents = true;
                            prc.StartInfo.CreateNoWindow = true;

                            prc.Exited += delegate
                                              {

                                                  try
                                                  {
                                                      File.Delete(filename);
                                                  }
                                                  catch (Exception)
                                                  {
                                                      
                                                  }
                                              };
                            
                            prc.Start();
                    break;
                case Knowledge.KINDID_FILELINK:
                    System.Diagnostics.Process.Start(item.CodeFind);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Список статей базы знаний
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Knowledge BrowseList(this Knowledge item)
        {
            List<Knowledge> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список статей базы знаний
        /// </summary>
        /// <param name="item">Статья базы знаний</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<Knowledge> BrowseList(this Knowledge item, Predicate<Knowledge> filter, List<Knowledge> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }

        internal static List<Knowledge> BrowseMultyList(this Knowledge item, Workarea wa, Predicate<Knowledge> filter, List<Knowledge> sourceCollection, bool allowMultySelect)
        {
            List<Knowledge> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<Knowledge>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<Knowledge> browserBaseObjects = new ListBrowserBaseObjects<Knowledge>(wa, sourceCollection, filter, item, true, false, false, true);
            browserBaseObjects.Owner = frm;
            browserBaseObjects.Build();

            new FormStateMaintainer(frm, String.Format("Browse{0}", item.GetType().Name));
            frm.clientPanel.Controls.Add(browserBaseObjects.ListControl);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Always;
            frm.btnCreate.Visibility = BarItemVisibility.Always;
            frm.btnDelete.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;

            frm.btnDelete.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32);
            frm.btnSelect.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32);

            frm.btnProp.ItemClick += delegate
            {
                browserBaseObjects.InvokeProperties();
            };
            frm.btnDelete.ItemClick += delegate
            {
                browserBaseObjects.InvokeDelete();
            };
            browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            browserBaseObjects.ShowProperty += delegate(Knowledge obj)
            {
                obj.ShowProperty();
                if (obj.IsNew)
                {
                    obj.Created += delegate
                    {
                        int position = browserBaseObjects.BindingSource.Add(obj);
                        browserBaseObjects.BindingSource.Position = position;
                    };
                }
            };

            browserBaseObjects.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Alt)
                {
                    returnValue = browserBaseObjects.SelectedValues;
                    frm.Close();
                }
            };
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browserBaseObjects.SelectedValues;
                frm.Close();
            };
            frm.btnClose.ItemClick += delegate
            {
                returnValue = null;
                frm.Close();

            };
            frm.ShowDialog();

            return returnValue;
        }

        public static List<Knowledge> BrowseContent(this Knowledge item, Workarea wa = null)
        {
            ContentModuleKnowledge module = new ContentModuleKnowledge();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
        /*
        internal static List<Knowledge> BrowseContent(this Knowledge item, Workarea wa = null)
        {
            ContentModuleAnalitic module = new ContentModuleAnalitic();
            
            module.Workarea = item != null ? item.Workarea : wa;
            return item.BrowseContent(module);
        }
        */
        /*
        internal static List<T> BrowseContent<T>(this T item, IContentModule<T> module) where T: IBase
        {
            return module.ShowDialog(true);
        }
        */
    }
}