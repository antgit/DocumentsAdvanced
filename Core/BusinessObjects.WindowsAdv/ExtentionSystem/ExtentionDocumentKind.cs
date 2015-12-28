﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства вида документа
        /// </summary>
        /// <param name="item">Состояние</param>
        /// <returns></returns>
        public static Form ShowProperty(this EntityDocumentKind item)
        {
            InternalShowPropertyCore<EntityDocumentKind> showProperty = new InternalShowPropertyCore<EntityDocumentKind>
                                                                            {
                                                                                SelectedItem = item,
                                                                                ControlBuilder =
                                                                                    new BuildControlEntityDocumentKind
                                                                                        {
                                                                                            SelectedItem = item
                                                                                        }
                                                                            };
            return showProperty.ShowDialog();
        }
        #endregion

        internal static List<EntityDocumentKind> BrowseMultyList(this EntityDocumentKind item, Workarea wa, Predicate<EntityDocumentKind> filter, List<EntityDocumentKind> sourceCollection, bool allowMultySelect)
        {
            List<EntityDocumentKind> returnValue = null;
            FormProperties frm = new FormProperties
            {
                ribbon = { ApplicationIcon = ExtentionsImage.GetImageState(wa, 0) }
            };
            ListBrowserCore<EntityDocumentKind> browser = new ListBrowserCore<EntityDocumentKind>(wa, sourceCollection, filter, item, true, false, false, true) { Owner = frm };
            browser.Build();

            new FormStateMaintainer(frm, String.Format("Browse{0}", item.GetType().Name));
            frm.clientPanel.Controls.Add(browser.ListControl);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Always;
            frm.btnCreate.Visibility = BarItemVisibility.Always;
            frm.btnDelete.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;

            frm.btnDelete.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32);
            frm.btnSelect.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32);

            frm.btnProp.ItemClick += delegate
            {
                browser.InvokeProperties();
            };
            frm.btnDelete.ItemClick += delegate
            {
                browser.InvokeDelete();
            };
            browser.ListControl.Dock = DockStyle.Fill;
            browser.ShowProperty += delegate(EntityDocumentKind obj)
            {
                obj.ShowProperty();
                if (obj.IsNew)
                {
                    // TODO: 
                    //obj.Created += delegate
                    //{
                    //    int position = browserBaseObjects.BindingSource.Add(obj);
                    //    browserBaseObjects.BindingSource.Position = position;
                    //};
                }
            };

            browser.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Alt)
                {
                    returnValue = browser.SelectedValues;
                    frm.Close();
                }
            };
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browser.SelectedValues;
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
