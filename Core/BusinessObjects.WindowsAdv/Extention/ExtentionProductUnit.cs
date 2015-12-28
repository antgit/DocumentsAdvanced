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
        /// Свойства производной единицы измерения
        /// </summary>
        /// <param name="item">Производная единица измерения</param>
        /// <returns></returns>
        public static Form ShowProperty(this ProductUnit item)
        {
            InternalShowPropertyBase<ProductUnit> showPropertyBase = new InternalShowPropertyBase<ProductUnit>
                                                                         {
                                                                             SelectedItem = item,
                                                                             ControlBuilder =
                                                                                 new BuildControlProductUnit
                                                                                     {
                                                                                         SelectedItem = item
                                                                                     }
                                                                         };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список производных единиц измерения
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static ProductUnit BrowseList(this ProductUnit item)
        {
            List<ProductUnit> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список производных единиц измерения
        /// </summary>
        /// <param name="item">Статовый объект</param>
        /// <param name="fiClass1.cslter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<ProductUnit> BrowseList(this ProductUnit item, Predicate<ProductUnit> filter, List<ProductUnit> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<ProductUnit> BrowseMultyList(this ProductUnit item, Workarea wa, Predicate<ProductUnit> filter, List<ProductUnit> sourceCollection, bool allowMultySelect)
        {
            List<ProductUnit> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<ProductUnit>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ListBrowserBaseObjects<ProductUnit> browserBaseObjects = new ListBrowserBaseObjects<ProductUnit>(wa, sourceCollection, filter, item, true, false, false, true)
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
            browserBaseObjects.ShowProperty += delegate(ProductUnit obj)
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
