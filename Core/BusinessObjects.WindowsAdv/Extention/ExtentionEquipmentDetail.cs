﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
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
        public static Form ShowProperty(this EquipmentDetail item)
        {
            InternalShowPropertyBase<EquipmentDetail> showPropertyBase = new InternalShowPropertyBase<EquipmentDetail>();
            showPropertyBase.SelectedItem = item;
            showPropertyBase.ControlBuilder = new BuildControlEquipmentDetail { SelectedItem = item };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список аналитики
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static EquipmentDetail BrowseList(this EquipmentDetail item)
        {
            List<EquipmentDetail> col = BrowseMultyList(item, item.Workarea, null, null, false);
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
        public static List<EquipmentDetail> BrowseList(this EquipmentDetail item, Predicate<EquipmentDetail> filter, List<EquipmentDetail> sourceCollection, string rootCode = null)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true, rootCode);
        }
        internal static List<EquipmentDetail> BrowseMultyList(this EquipmentDetail item, Workarea wa, Predicate<EquipmentDetail> filter, List<EquipmentDetail> sourceCollection, bool allowMultySelect, string rootCode = null)
        {
            List<EquipmentDetail> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<EquipmentDetail>().Entity);
            if (img!=null)
            {
                frm.Ribbon.ApplicationIcon = img;
                frm.Icon = Icon.FromHandle(img.GetHicon());
            }
            ListBrowserBaseObjects<EquipmentDetail> browserBaseObjects = new ListBrowserBaseObjects<EquipmentDetail>(wa, sourceCollection, filter, item, true, false, false, true) { Owner = frm, RootCode = rootCode };
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
            browserBaseObjects.ShowProperty += delegate(EquipmentDetail obj)
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

        public static List<EquipmentDetail> BrowseContent(this EquipmentDetail item, Workarea wa = null)
        {
            ContentModuleEquipmentDetail module = new ContentModuleEquipmentDetail();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}
