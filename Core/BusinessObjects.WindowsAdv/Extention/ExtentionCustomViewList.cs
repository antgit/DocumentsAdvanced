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
        /// Свойства списка
        /// </summary>
        /// <param name="item">Список</param>
        /// <returns></returns>
        public static Form ShowProperty(this CustomViewList item)
        {
            InternalShowPropertyBase<CustomViewList> showPropertyBase = new InternalShowPropertyBase<CustomViewList>
                                                                            {
                                                                                SelectedItem = item,
                                                                                ControlBuilder =
                                                                                    new BuildControlCustomViewList
                                                                                        {
                                                                                            SelectedItem = item
                                                                                        }
                                                                            };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список данных
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static CustomViewList BrowseList(this CustomViewList item)
        {
            List<CustomViewList> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать спикок списков
        /// </summary>
        /// <param name="item">Список</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<CustomViewList> BrowseList(this CustomViewList item, Predicate<CustomViewList> filter, List<CustomViewList> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<CustomViewList> BrowseMultyList(this CustomViewList item, Workarea wa, Predicate<CustomViewList> filter, List<CustomViewList> sourceCollection, bool allowMultySelect)
        {
            List<CustomViewList> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<CustomViewList>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ListBrowserBaseObjects<CustomViewList> browserBaseObjects = new ListBrowserBaseObjects<CustomViewList>(wa, sourceCollection, filter, item, true, false, false, true)
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
            browserBaseObjects.ShowProperty += delegate(CustomViewList obj)
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

        public static List<CustomViewList> BrowseContent(this CustomViewList item, Workarea wa = null)
        {
            ContentModuleCustomViewList module = new ContentModuleCustomViewList();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}
