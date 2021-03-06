﻿using System;
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
        /// Свойства адреса корреспондента
        /// </summary>
        /// <param name="item">Адрес корреспондента</param>
        /// <returns></returns>
        public static Form ShowProperty(this AgentAddress item)
        {
            InternalShowPropertyBase<AgentAddress> showPropertyBase = new InternalShowPropertyBase<AgentAddress>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlAgentAddress { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список адрессов
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static AgentAddress BrowseList(this AgentAddress item)
        {
            List<AgentAddress> col = BrowseMultyList(item, item.Workarea, null, null, false);
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
        public static List<AgentAddress> BrowseList(this AgentAddress item, Predicate<AgentAddress> filter, List<AgentAddress> sourceCollection, string rootCode = null)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true, rootCode);
        }
        internal static List<AgentAddress> BrowseMultyList(this AgentAddress item, Workarea wa, Predicate<AgentAddress> filter, List<AgentAddress> sourceCollection, bool allowMultySelect, string rootCode = null)
        {
            List<AgentAddress> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<AgentAddress>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<AgentAddress> browserBaseObjects = new ListBrowserBaseObjects<AgentAddress>(wa, sourceCollection, filter, item, true, false, false, true) { Owner = frm, RootCode = rootCode };
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
            browserBaseObjects.ShowProperty += delegate(AgentAddress obj)
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

        public static List<AgentAddress> BrowseContent(this AgentAddress item, Workarea wa = null)
        {
            // TODO: 
            //ContentModuleAgentAddress module = new ContentModuleAgentAddress();
            //module.Workarea = item != null ? item.Workarea : wa;
            //return module.ShowDialog(true);
            return null;
        }
        /*
        internal static List<Analitic> BrowseContent(this Analitic item, Workarea wa = null)
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
