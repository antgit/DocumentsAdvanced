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
        /// Свойства интервала времени
        /// </summary>
        /// <param name="item">Интервал времени</param>
        /// <returns></returns>
        public static Form ShowProperty(this DateRegion item)
        {
            InternalShowPropertyBase<DateRegion> showPropertyBase = new InternalShowPropertyBase<DateRegion>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlDateRegion { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список интервалов времени
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static DateRegion BrowseList(this DateRegion item)
        {
            List<DateRegion> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать спикок интервалов времени
        /// </summary>
        /// <param name="item">Интервал времени</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<DateRegion> BrowseList(this DateRegion item, Predicate<DateRegion> filter, List<DateRegion> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<DateRegion> BrowseMultyList(this DateRegion item, Workarea wa, Predicate<DateRegion> filter, List<DateRegion> sourceCollection, bool allowMultySelect)
        {
            List<DateRegion> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<DateRegion>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<DateRegion> browserBaseObjects = new ListBrowserBaseObjects<DateRegion>(wa, sourceCollection, filter, item, true, false, false, true);
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
            browserBaseObjects.ShowProperty += delegate(DateRegion obj)
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

        public static List<DateRegion> BrowseContent(this DateRegion item, Workarea wa = null)
        {
            ContentModuleDateRegion module = new ContentModuleDateRegion();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}
