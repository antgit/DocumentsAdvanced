using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства системного объекта
        /// </summary>
        /// <param name="item">Системный объект</param>
        /// <returns></returns>
        public static Form ShowProperty(this EntityType item)
        {
            InternalShowPropertyBase<EntityType> showPropertyBase = new InternalShowPropertyBase<EntityType>
                                                                        {
                                                                            SelectedItem = item,
                                                                            ControlBuilder =
                                                                                new BuildControlEntityType
                                                                                    {
                                                                                        SelectedItem = item
                                                                                    }
                                                                        };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Диалог выбора системного объекта
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static EntityType BrowseList(this EntityType item)
        {
            List<EntityType> col = BrowseMultyList(item, item.Workarea, null, item.Workarea.CollectionEntities, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        public static List<EntityType> BrowseList(this EntityType item, Predicate<EntityType> filter, List<EntityType> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<EntityType> BrowseMultyList(this EntityType item, Workarea wa, Predicate<EntityType> filter, List<EntityType> sourceCollection, bool allowMultySelect)
        {
            List<EntityType> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<EntityType>().GetImage();
            if (img != null)
            {
                frm.Ribbon.ApplicationIcon = img;
                frm.Icon = Icon.FromHandle(img.GetHicon());
            }
            ListBrowserCore<EntityType> browser = new ListBrowserCore<EntityType>(wa, sourceCollection, filter, item, true, false, false, true)
                                                      {Owner = frm};
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
            browser.ShowProperty += delegate(EntityType obj)
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

            browser.ListControl.View.CustomUnboundColumnData +=
                delegate(object sender, CustomColumnDataEventArgs e)
                    {
                        if (e.Column.FieldName == "Image" && e.IsGetData)
                        {
                            EntityType imageItem = browser.BindingSource[e.ListSourceRowIndex] as EntityType;
                            if (imageItem != null)
                            {
                                e.Value = imageItem.GetImage();
                            }
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
