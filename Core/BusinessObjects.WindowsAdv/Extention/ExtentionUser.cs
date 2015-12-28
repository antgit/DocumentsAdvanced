using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Security;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства пользователя или группы
        /// </summary>
        /// <param name="item">Пользователь или группа</param>
        /// <returns></returns>
        public static Form ShowProperty(this Uid item)
        {
            InternalShowPropertyBase<Uid> showPropertyBase = new InternalShowPropertyBase<Uid>
                                                                 {
                                                                     SelectedItem = item,
                                                                     ControlBuilder =
                                                                         new BuildControlUser
                                                                             {
                                                                                 SelectedItem = item
                                                                             }
                                                                 };

            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список пользователей или групп
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Uid BrowseList(this Uid item)
        {
            List<Uid> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список пользователей или групп
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static Uid BrowseList(this Uid item, Predicate<Uid> filter, List<Uid> sourceCollection)
        {
            List<Uid> col = BrowseMultyList(item, item.Workarea, filter, sourceCollection, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        public static List<Uid> BrowseMultyList(this Uid item, Predicate<Uid> filter, List<Uid> sourceCollection)
        {
            List<Uid> col = BrowseMultyList(item, item.Workarea, filter, sourceCollection, false);
            if (col != null && col.Count > 0)
                return col;
            return null;
        }
        internal static List<Uid> BrowseMultyList(this Uid item, Workarea wa, Predicate<Uid> filter, List<Uid> sourceCollection, bool allowMultySelect)
        {
            List<Uid> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<Uid>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ListBrowserBaseObjects<Uid> browserBaseObjects = new ListBrowserBaseObjects<Uid>(wa, sourceCollection, filter, item, true, false, false, true)
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
            browserBaseObjects.ShowProperty += delegate(Uid obj)
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
    }
}

