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
        /// Свойства каталога данных
        /// </summary>
        /// <param name="item">Каталог данных</param>
        /// <returns></returns>
        public static Form ShowProperty(this DataCatalog item)
        {
            InternalShowPropertyBase<DataCatalog> showPropertyBase = new InternalShowPropertyBase<DataCatalog>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlDataCatalog { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список каталогов данных
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static DataCatalog BrowseList(this DataCatalog item)
        {
            List<DataCatalog> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список каталогов данных
        /// </summary>
        /// <param name="item">Каталог данных</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<DataCatalog> BrowseList(this DataCatalog item, Predicate<DataCatalog> filter, List<DataCatalog> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<DataCatalog> BrowseMultyList(this DataCatalog item, Workarea wa, Predicate<DataCatalog> filter, List<DataCatalog> sourceCollection, bool allowMultySelect)
        {
            List<DataCatalog> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<DataCatalog>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<DataCatalog> browserBaseObjects = new ListBrowserBaseObjects<DataCatalog>(wa, sourceCollection, filter, item, true, false, false, true);
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
            browserBaseObjects.ShowProperty += delegate(DataCatalog obj)
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

        public static List<DataCatalog> BrowseContent(this DataCatalog item, Workarea wa = null)
        {
            ContentModuleDataCatalog module = new ContentModuleDataCatalog();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
        /*
        internal static List<DataCatalog> BrowseContent(this DataCatalog item, Workarea wa = null)
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
