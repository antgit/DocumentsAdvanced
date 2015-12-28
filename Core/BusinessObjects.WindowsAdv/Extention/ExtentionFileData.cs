using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства файловых данных
        /// </summary>
        /// <param name="item">Файловые данные</param>
        /// <returns></returns>
        public static Form ShowProperty(this FileData item)
        {
            InternalShowPropertyBase<FileData> showPropertyBase = new InternalShowPropertyBase<FileData>
                                                                      {
                                                                          SelectedItem = item,
                                                                          ControlBuilder =
                                                                              new BuildControlFileData
                                                                                  {
                                                                                      SelectedItem = item
                                                                                  }
                                                                      };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список файлоывх данных
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static FileData BrowseList(this FileData item)
        {
            List<FileData> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список файлов
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<FileData> BrowseList(this FileData item, Predicate<FileData> filter, List<FileData> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<FileData> BrowseMultyList(this FileData item, Workarea wa, Predicate<FileData> filter, List<FileData> sourceCollection, bool allowMultySelect)
        {
            List<FileData> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<FileData>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<FileData> browserBaseObjects = new ListBrowserBaseObjects<FileData>(wa, sourceCollection, filter, item, true, false, false, true)
                                                                      {Owner = frm};
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
            browserBaseObjects.ShowProperty += delegate(FileData obj)
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

        public static List<FileData> BrowseContent(this FileData item, Workarea wa = null)
        {
            ContentModuleFileData module = new ContentModuleFileData();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}
