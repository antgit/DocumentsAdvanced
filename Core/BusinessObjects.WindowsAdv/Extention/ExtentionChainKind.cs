using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства вида связи
        /// </summary>
        /// <param name="item">Вид связи</param>
        /// <returns></returns>
        public static Form ShowProperty(this ChainKind item)
        {
            InternalShowPropertyCore<ChainKind> showProperty = new InternalShowPropertyCore<ChainKind>();
            showProperty.SelectedItem = item;
            showProperty.ControlBuilder = new BuildControlChainKind { SelectedItem = item };
            return showProperty.ShowDialog();
        }
        #endregion
        /// <summary>
        /// Список видов связей
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static ChainKind BrowseList(this ChainKind item)
        {
            List<ChainKind> col = item.BrowseMultyList(item.Workarea, null, item.Workarea.CollectionChainKinds, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        internal static List<ChainKind> BrowseMultyList(this ChainKind item, Workarea wa, Predicate<ChainKind> filter, List<ChainKind> sourceCollection, bool allowMultySelect)
        {
            List<ChainKind> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ResourceImage.GetByCode(wa, ResourceImage.LINK_X16); 
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserCore<ChainKind> browser = new ListBrowserCore<ChainKind>(wa, sourceCollection, filter, item, true, false, false, true);
            browser.Owner = frm;
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
            browser.ShowProperty += delegate(ChainKind obj)
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

        public static List<ChainKind> BrowseContent(this ChainKind item, Workarea wa = null)
        {
            ContentModuleChainKind module = new ContentModuleChainKind();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }

        public static ChainKindContentType ShowPropertyChainKindContentType(this ChainKind source, ChainKindContentType item)
        {
            FormProperties frm = new FormProperties();

            if (item == null)
                item = new ChainKindContentType { Workarea = source.Workarea, StateId = State.STATEACTIVE, ElementId = source.Id };

            ControlChainKindContentType ctl = new ControlChainKindContentType();
            DataGridViewHelper.GenerateGridColumns(source.Workarea, ctl.ViewSourceKinds, "DEFAULT_LOOKUP_NAME");
            BindingSource bindingSourceEntityFrom = new BindingSource
            {
                DataSource = source.FromEntity.EntityKinds
            };
            ctl.cmbSourceKinds.Properties.DisplayMember = "Name";
            ctl.cmbSourceKinds.Properties.ValueMember = GlobalPropertyNames.Id;
            ctl.cmbSourceKinds.Properties.DataSource = bindingSourceEntityFrom;
            ctl.cmbSourceKinds.EditValue = item.EntityKindIdFrom;

            ctl.chkAnySource.Checked = item.EntityKindIdFrom == 0;
            if (item.EntityKindIdFrom == 0)
            {
                ctl.cmbSourceKinds.Enabled = false;
            }
            ctl.chkAnySource.CheckedChanged += delegate
                                                   {
                                                       ctl.cmbSourceKinds.Enabled = !ctl.chkAnySource.Checked;
                                                   };

            DataGridViewHelper.GenerateGridColumns(source.Workarea, ctl.ViewDestinationKinds, "DEFAULT_LOOKUP_NAME");
            BindingSource bindingSourceEntityTo = new BindingSource
            {
                DataSource = source.ToEntity.EntityKinds
            };
            ctl.cmbDestinationKinds.Properties.DisplayMember = "Name";
            ctl.cmbDestinationKinds.Properties.ValueMember = GlobalPropertyNames.Id;
            ctl.cmbDestinationKinds.Properties.DataSource = bindingSourceEntityTo;
            ctl.cmbDestinationKinds.EditValue = item.EntityKindId;
            
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            if(frm.ShowDialog()== DialogResult.OK)
            {
                if (ctl.chkAnySource.Checked)
                    item.EntityKindIdFrom = 0;
                else
                    item.EntityKindIdFrom = (int)ctl.cmbSourceKinds.EditValue;

                item.EntityKindId = (int)ctl.cmbDestinationKinds.EditValue;
                item.Save();
                return item;
            }
            return null;
        }
    }
}

