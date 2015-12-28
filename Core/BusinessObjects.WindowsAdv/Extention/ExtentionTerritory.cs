using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Drawing;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Показать на карте и свойства
        /// <summary>
        /// Показать расположение на карте Google
        /// </summary>
        /// <param name="item">Страна</param>
        public static void ShowOnGoogleMap(this AgentAddress item)
        {
            if (item == null) return;
            string value = string.Format("http://maps.google.com/maps?ll={0},{1}&z=18&t=m&hl=ru", item.X, item.Y);
            System.Diagnostics.Process.Start(value);
        }
        /// <summary>
        /// Показать расположение на карте Google
        /// </summary>
        /// <param name="item">Страна</param>
        public static void ShowOnGoogleMap(this Country item)
        {
            if (item == null) return;
            string value = string.Format("http://maps.google.com/maps?ll={0},{1}&z=5&t=m&hl=ru", item.X, item.Y);
            System.Diagnostics.Process.Start(value);
        }
        /// <summary>
        /// Показать расположение на карте Google
        /// </summary>
        /// <param name="item">Область</param>
        public static void ShowOnGoogleMap(this Territory item)
        {
            if (item == null) return;
            string value = string.Format("http://maps.google.com/maps?ll={0},{1}&z=10&t=m&hl=ru", item.X, item.Y);
            System.Diagnostics.Process.Start(value);
        }
        /// <summary>
        /// Показать расположение на карте Google
        /// </summary>
        /// <param name="item">Город</param>
        public static void ShowOnGoogleMap(this Town item)
        {
            if (item == null) return;
            string value = string.Format("http://maps.google.com/maps?ll={0},{1}&z=12&t=m&hl=ru&q={0},{1}", item.X, item.Y);
            //binq
            //http://maps.bing.com/default.aspx?v=2&cp=47.274167~34.71&style=r&lvl=12&sp=Point.47.274167_34.71_Велика Білозерка___&setlang=uk-UA
            //google
            //http://maps.google.com/maps?ll=47.274167,34.71&spn=0.1,0.1&q=47.274167,34.71&hl=uk
            // yandex
            //http://maps.yandex.ru/?ll=34.71,47.274167&spn=0.1,0.1
            System.Diagnostics.Process.Start(value);
        }
        /// <summary>
        /// Показать расположение на карте Google
        /// </summary>
        /// <param name="item">Город</param>
        public static void ShowOnMapBinq(this Town item)
        {
            if (item == null) return;
            string value = string.Format("http://maps.bing.com/default.aspx?v=2&cp={0}~{1}&style=r&lvl=12&sp=Point.{0}_{1}", item.X, item.Y);
            System.Diagnostics.Process.Start(value);
        }
        /// <summary>
        /// Свойства страны
        /// </summary>
        /// <param name="item">Страна</param>
        /// <returns></returns>
        public static Form ShowProperty(this Country item)
        {
            InternalShowPropertyBase<Country> showPropertyBase = new InternalShowPropertyBase<Country>
                                                                     {
                                                                         SelectedItem = item,
                                                                         ControlBuilder =
                                                                             new BuildControlCountry
                                                                                 {
                                                                                     SelectedItem = item
                                                                                 }
                                                                     };
            return showPropertyBase.ShowDialog();
        }
        /// <summary>
        /// Список стран
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Country BrowseList(this Country item)
        {
            List<Country> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список стран
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<Country> BrowseList(this Country item, Predicate<Country> filter, List<Country> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<Country> BrowseMultyList(this Country item, Workarea wa, Predicate<Country> filter, List<Country> sourceCollection, bool allowMultySelect)
        {
            List<Country> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<Country>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ListBrowserBaseObjects<Country> browserBaseObjects = new ListBrowserBaseObjects<Country>(wa, sourceCollection, filter, item, true, false, false, true)
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
            browserBaseObjects.ShowProperty += delegate(Country obj)
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

        /// <summary>
        /// Свойства области
        /// </summary>
        /// <param name="item">Область</param>
        /// <returns></returns>
        public static Form ShowPropertyTerritory(this Territory item)
        {
            InternalShowPropertyBase<Territory> showPropertyBase = new InternalShowPropertyBase<Territory>
                                                                       {
                                                                           SelectedItem = item,
                                                                           ControlBuilder =
                                                                               new BuildControlTerritory
                                                                                   {
                                                                                       SelectedItem = item
                                                                                   }
                                                                       };
            return showPropertyBase.ShowDialog();
        }

        /// <summary>
        /// Свойства региона
        /// </summary>
        /// <param name="item">Регион</param>
        /// <returns></returns>
        public static Form ShowPropertyRegion(this Territory item)
        {
            InternalShowPropertyBase<Territory> showPropertyBase = new InternalShowPropertyBase<Territory>
                                                                       {
                                                                           SelectedItem = item,
                                                                           ControlBuilder =
                                                                               new BuildControlRegion
                                                                                   {
                                                                                       SelectedItem = item,
                                                                                   }
                                                                       };
            return showPropertyBase.ShowDialog();
        }

        /// <summary>
        /// Свойства города
        /// </summary>
        /// <param name="item">Город</param>
        /// <returns></returns>
        public static Form ShowProperty(this Town item, bool modal=false )
        {
            InternalShowPropertyBase<Town> showPropertyBase = new InternalShowPropertyBase<Town>
                                                                  {
                                                                      SelectedItem = item,
                                                                      ControlBuilder =
                                                                          new BuildControlTown
                                                                              {
                                                                                  SelectedItem = item
                                                                              }
                                                                  };
            return showPropertyBase.ShowDialog(modal);
        }

        /// <summary>
        /// Список городов
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Town BrowseList(this Town item)
        {
            List<Town> col = BrowseMultyList(item, item.Workarea, null, null, false);
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
        public static List<Town> BrowseList(this Town item, Predicate<Town> filter, List<Town> sourceCollection, string rootCode = null)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true, rootCode);
        }
        internal static List<Town> BrowseMultyList(this Town item, Workarea wa, Predicate<Town> filter, List<Town> sourceCollection, bool allowMultySelect, string rootCode = null)
        {
            List<Town> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<Town>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<Town> browserBaseObjects = new ListBrowserBaseObjects<Town>(wa, sourceCollection, filter, item, true, false, false, true) { Owner = frm, RootCode = rootCode };
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
            browserBaseObjects.ShowProperty += delegate(Town obj)
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


        /// <summary>
        /// Список задач
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Territory BrowseList(this Territory item)
        {
            List<Territory> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }

        /// <summary>
        /// Показать спикок задач
        /// </summary>
        /// <param name="item">Задача</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <param name="rootCode">Корневая иерархия</param>
        /// <returns></returns>
        public static List<Territory> BrowseList(this Territory item, Predicate<Territory> filter, List<Territory> sourceCollection, string rootCode = null)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true, rootCode);
        }
        internal static List<Territory> BrowseMultyList(this Territory item, Workarea wa, Predicate<Territory> filter, List<Territory> sourceCollection, bool allowMultySelect, string rootCode = null)
        {
            List<Territory> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<Territory>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<Territory> browserBaseObjects = new ListBrowserBaseObjects<Territory>(wa, sourceCollection, filter, item, true, false, false, true) { Owner = frm, RootCode = rootCode };
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
            browserBaseObjects.ShowProperty += delegate(Territory obj)
            {
                if (obj.KindValue== Territory.KINDVALUE_REGIONALDISTRICT)
                    obj.ShowPropertyRegion();
                if (obj.KindValue == Territory.KINDVALUE_REGION)
                    obj.ShowPropertyTerritory();
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

        
        #endregion
    }
}