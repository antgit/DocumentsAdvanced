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
        /// Свойства службы
        /// </summary>
        /// <param name="item">Служба</param>
        /// <returns></returns>
        public static Form ShowProperty(this WebService item)
        {
            InternalShowPropertyBase<WebService> showPropertyBase = new InternalShowPropertyBase<WebService>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlWebService { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список служб
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static WebService BrowseList(this WebService item)
        {
            List<WebService> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список вебслужб
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<WebService> BrowseList(this WebService item, Predicate<WebService> filter, List<WebService> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<WebService> BrowseMultyList(this WebService item, Workarea wa, Predicate<WebService> filter, List<WebService> sourceCollection, bool allowMultySelect)
        {
            List<WebService> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<WebService>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<WebService> browserBaseObjects = new ListBrowserBaseObjects<WebService>(wa, sourceCollection, filter, item, true, false, false, true);
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
            browserBaseObjects.ShowProperty += delegate(WebService obj)
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

        public static List<WebService> BrowseContent(this WebService item, Workarea wa = null)
        {
            ContentModuleWebService module = new ContentModuleWebService();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }

    }
}
