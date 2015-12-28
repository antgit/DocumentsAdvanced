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
        /// Свойства наименования дополнительных кодов
        /// </summary>
        /// <param name="item">Наименование дополнительных кодов</param>
        /// <returns></returns>
        public static Form ShowProperty(this CodeName item)
        {
            InternalShowPropertyBase<CodeName> showPropertyBase = new InternalShowPropertyBase<CodeName>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlCodeName { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Свойства кода
        /// </summary>
        /// <param name="item">Код</param>
        /// <returns></returns>
        public static Form ShowProperty<T>(this CodeValue<T> item, bool modal=true) where T : class, IBase, new()
        {
            InternalShowPropertyCore<CodeValue<T>> showProperty = new InternalShowPropertyCore<CodeValue<T>>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlCodeValue<T> { SelectedItem = item };
            return showProperty.ShowDialog(modal);
        }

        /// <summary>
        /// Список наименований кодов
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static CodeName BrowseList(this CodeName item)
        {
            List<CodeName> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список наименований кодов
        /// </summary>
        /// <param name="item">Наименование кода</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<CodeName> BrowseList(this CodeName item, Predicate<CodeName> filter, List<CodeName> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<CodeName> BrowseMultyList(this CodeName item, Workarea wa, Predicate<CodeName> filter, List<CodeName> sourceCollection, bool allowMultySelect)
        {
            List<CodeName> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<CodeName>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            if (img != null) frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<CodeName> browserBaseObjects = new ListBrowserBaseObjects<CodeName>(wa, sourceCollection, filter, item, true, false, false, true);
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
            browserBaseObjects.ShowProperty += delegate(CodeName obj)
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

        public static List<CodeName> BrowseContent(this CodeName item, Workarea wa = null)
        {
            ContentModuleCodeName module = new ContentModuleCodeName();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}