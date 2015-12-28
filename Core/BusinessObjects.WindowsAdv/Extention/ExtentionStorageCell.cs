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
        /// Свойства ячейки хранения
        /// </summary>
        /// <param name="item">Ячейка хранения</param>
        /// <returns></returns>
        public static Form ShowProperty(this StorageCell item)
        {
            InternalShowPropertyBase<StorageCell> showPropertyBase = new InternalShowPropertyBase<StorageCell>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlStorageCell { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список ячеек хранения
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static StorageCell BrowseList(this StorageCell item)
        {
            List<StorageCell> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список ячеек хранения
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<StorageCell> BrowseList(this StorageCell item, Predicate<StorageCell> filter, List<StorageCell> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<StorageCell> BrowseMultyList(this StorageCell item, Workarea wa, Predicate<StorageCell> filter, List<StorageCell> sourceCollection, bool allowMultySelect)
        {
            List<StorageCell> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<StorageCell>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<StorageCell> browserBaseObjects = new ListBrowserBaseObjects<StorageCell>(wa, sourceCollection, filter, item, true, false, false, true);
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
            browserBaseObjects.ShowProperty += delegate(StorageCell obj)
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

        public static List<StorageCell> BrowseContent(this StorageCell item, Workarea wa = null)
        {
            ContentModuleStorageCell module = new ContentModuleStorageCell();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
   }
}