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
        /// Свойства аналитики
        /// </summary>
        /// <param name="item">Аналитика</param>
        /// <returns></returns>
        public static Form ShowProperty(this Note item)
        {
            InternalShowPropertyBase<Note> showPropertyBase = new InternalShowPropertyBase<Note>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlNote { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список аналитики
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Note BrowseList(this Note item)
        {
            List<Note> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }

        /// <summary>
        /// Показать спикок аналитики
        /// </summary>
        /// <param name="item">Аналитика</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <param name="rootCode">Корневая иерархия</param>
        /// <returns></returns>
        public static List<Note> BrowseList(this Note item, Predicate<Note> filter, List<Note> sourceCollection, string rootCode = null)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true, rootCode);
        }
        internal static List<Note> BrowseMultyList(this Note item, Workarea wa, Predicate<Note> filter, List<Note> sourceCollection, bool allowMultySelect, string rootCode = null)
        {
            List<Note> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<Note>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            if (img!=null)
                frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<Note> browserBaseObjects = new ListBrowserBaseObjects<Note>(wa, sourceCollection, filter, item, true, false, false, true) { Owner = frm, RootCode = rootCode };
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
            browserBaseObjects.ShowProperty += delegate(Note obj)
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

        public static List<Note> BrowseContent(this Note item, Workarea wa = null)
        {
            ContentModuleNote module = new ContentModuleNote();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
        /*
        internal static List<Note> BrowseContent(this Note item, Workarea wa = null)
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
