using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Windows.Wizard;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {

        #region Свойства
        /// <summary>
        /// Свойства товаров
        /// </summary>
        /// <param name="item">Товар</param>
        /// <returns></returns>
        public static Form ShowProperty(this Product item)
        {
            InternalShowPropertyBase<Product> showPropertyBase = new InternalShowPropertyBase<Product>
                                                                     {
                                                                         SelectedItem = item,
                                                                         ControlBuilder =
                                                                             new BuildProductControl
                                                                                 {
                                                                                     SelectedItem = item
                                                                                     
                                                                                 }
                                                                     };
            return showPropertyBase.ShowDialog();
        }
        /// <summary>
        /// Свойства партии товара
        /// </summary>
        /// <param name="item">Партия товара</param>
        /// <returns></returns>
        public static Form ShowProperty(this Series item)
        {
            InternalShowPropertyBase<Series> showPropertyBase = new InternalShowPropertyBase<Series>
            {
                SelectedItem = item,
                ControlBuilder =
                    new BuildControlSeries
                    {
                        SelectedItem = item
                    }
            };
            return showPropertyBase.ShowDialog();
        }
        #endregion
        /// <summary>
        /// Список товаров
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный товар</returns>
        public static Product BrowseList(this Product item)
        {
            List<Product> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать окно списка товаров
        /// </summary>
        /// <param name="item">Товар</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<Product> BrowseList(this Product item, Predicate<Product> filter, List<Product> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }

        internal static List<Product> BrowseMultyList(this Product item, Workarea wa, Predicate<Product> filter, List<Product> sourceCollection, bool allowMultySelect)
        {
            List<Product> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<Product>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<Product> browserBaseObjects = new ListBrowserBaseObjects<Product>(wa, sourceCollection, filter, item, true, false, false, true)
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
            #region Поиск
            browserBaseObjects.Find += delegate
            {
                //BusinessObjects.Windows.Finder finder = new BusinessObjects.Windows.Finder
                //{
                //    FindString = browserBaseObjects.FirstSelectedValue.Name,
                //    CriteriaNames =
                //        "Наименование|Признак|Примечание|Идентификатор|Артикульный №|Каталожный №|Номенклатурный №|Штрих код",
                //    ShowCriteria = true,
                //    AllowCriteria = true
                //};

                //System.Drawing.Point fstart = frm.PointToClient(frm.Location);
                //fstart.Offset(frm.Width - 432, 0);
                //finder.StartPosition = frm.PointToScreen(fstart);

                //finder.Restart += delegate
                //{
                //    browserBaseObjects.BindingSource.Position = -1;
                //};
                //finder.Find += delegate
                //{
                //    int value = -1;
                //    int start = browserBaseObjects.BindingSource.Position + 1;
                //    if (start >= browserBaseObjects.BindingSource.Count)
                //        start = 1;
                //    if (finder.SelectedCriteria == 0)
                //        value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => !string.IsNullOrEmpty(f.Name) && f.Name.ToUpper().Contains(finder.FindString.ToUpper()));
                //    else if (finder.SelectedCriteria == 1)
                //        value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => !string.IsNullOrEmpty(f.Code) && f.Code.ToUpper().Contains(finder.FindString.ToUpper()));
                //    else if (finder.SelectedCriteria == 2)
                //        value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => !string.IsNullOrEmpty(f.Memo) && f.Memo.ToUpper().Contains(finder.FindString.ToUpper()));
                //    else if (finder.SelectedCriteria == 3)
                //    {
                //        int res;
                //        if (Int32.TryParse(finder.FindString, out res))
                //            value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => f.Id != 0 && f.Id == res);
                //    }
                //    else if (finder.SelectedCriteria == 4)
                //        value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => !string.IsNullOrEmpty(f.Articul) && f.Articul.ToUpper().Contains(finder.FindString.ToUpper()));
                //    else if (finder.SelectedCriteria == 5)
                //        value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => !string.IsNullOrEmpty(f.Cataloque) && f.Cataloque.ToUpper().Contains(finder.FindString.ToUpper()));
                //    else if (finder.SelectedCriteria == 6)
                //        value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => !string.IsNullOrEmpty(f.Nomenclature) && f.Nomenclature.ToUpper().Contains(finder.FindString.ToUpper()));
                //    else if (finder.SelectedCriteria == 7)
                //        value = ((List<Product>)browserBaseObjects.BindingSource.DataSource).FindIndex(start, f => !string.IsNullOrEmpty(f.Barcode) && f.Barcode.ToUpper().Contains(finder.FindString.ToUpper()));
                //    if (value != -1)
                //        browserBaseObjects.BindingSource.Position = value;
                //};
                //finder.ShowDialog();
            };
            #endregion
            browserBaseObjects.ShowProperty += delegate(Product obj)
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
        /// Список товаров
        /// </summary>
        /// <param name="value">Стартовый объект</param>
        /// <param name="wa">Рабочая область</param>
        /// <param name="showContent">Отображать состав иерархии в виде списка</param>
        /// <param name="showContentTree">Отображать состав иерархии в дереве</param>
        /// <param name="filter">Фильтр</param>
        /// <returns>Выбранный товар</returns>
        public static Product ShowTreeList(this Product value, Workarea wa, bool showContent, bool showContentTree, Predicate<Product> filter) //where T : class, IBase, new()//BaseCore<T>
        {
            FormProperties frm = new FormProperties();
            new FormStateMaintainer(frm, String.Format("ShowTreeList{0}", value.GetType().Name));
            //frm.Text = string.Format("Список: {0}", wa.Empty<Product>().Entity.ToString());
            #region Не отображать содержимое
            if (!showContent)
            {
                TreeBrowser<Product> browser = new TreeBrowser<Product>(wa)
                {
                    StartValue = value,
                    ShowContentTree = showContentTree,
                };
                browser.Build();
                frm.clientPanel.Controls.Add(browser.ControlTree);
                browser.ControlTree.Dock = DockStyle.Fill;
            }
            #endregion
            #region Отображать содержимое
            //else
            //{
            //    ListTreeBrowser<BusinessObjects.Product> browserBaseObjects = new ListTreeBrowser<Product>(wa)
            //    {
            //        StartValue = value,
            //        ShowContent = true,
            //        TreeBrowser =
            //        {
            //            ShowTreeToolBar = showTreeToolBar,
            //            ShowContentTree = showContentTree,
            //            StartValue = value
            //        }
            //    };
            //    browserBaseObjects.Build();

            //    frm.splitContainer.Panel1.Controls.Add(browserBaseObjects.TreeBrowser.ControlTree);
            //    browserBaseObjects.TreeBrowser.ControlTree.Dock = DockStyle.Fill;
            //    frm.splitContainer.Panel2.Controls.Add(browserBaseObjects.ListControl);
            //    browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            //    browserBaseObjects.ListAddExists += delegate
            //    {

            //        Extentions.ListBrowseEnd += delegate
            //        {
            //            frm.Cursor = Cursors.Default;
            //        };
            //        browserBaseObjects.ListControl.Invoke((MethodInvoker)delegate
            //        {

            //        }
            //        );
            //        browserBaseObjects.ListControl.Refresh();
            //        frm.Cursor = Cursors.WaitCursor;

            //        int flagContent = browserBaseObjects.TreeBrowser.SelectedHierarchy.ContentFlags;
            //        List<Product> addCollection = value.BrowseMultyList(wa, s => (flagContent & s.KindValue) == s.KindValue, null, true);
            //        if (addCollection != null)
            //        {
            //            foreach (Product v in addCollection)
            //            {
            //                try
            //                {
            //                    browserBaseObjects.TreeBrowser.SelectedHierarchy.ContentAdd(v);
            //                    browserBaseObjects.BindingSource.Position = browserBaseObjects.BindingSource.Add(v);
            //                }
            //                catch (DatabaseException dbe)
            //                {
            //                    int idx = BusinessObjects.Controls.cTaskDialog.ShowCommandBox(Properties.Resources.MSG_CAPERROR,
            //                    Properties.Resources.MSG_EX_ACTION,
            //                    dbe.Message, string.Empty, string.Empty, string.Empty, Properties.Resources.INFO_VIEW_DETAIL,
            //                     true, BusinessObjects.Controls.eSysIcons.Error, BusinessObjects.Controls.eSysIcons.Information);
            //                    if (idx == 0)
            //                    {
            //                        ErrorLog err = wa.GetErrorLog(dbe.Id);
            //                        BusinessObjects.Controls.cTaskDialog.MessageBox(Properties.Resources.MSG_CAPERROR,
            //                    Properties.Resources.MSG_EX_ACTION,
            //                    string.Format(Properties.Resources.STR_ERR_DBE_FORMAT, Environment.NewLine,
            //                    err.Id, err.Number, err.Severity, err.State, err.Procedure, err.Message),
            //                     BusinessObjects.Controls.eTaskDialogButtons.Close, BusinessObjects.Controls.eSysIcons.Error);
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    BusinessObjects.Controls.cTaskDialog.MessageBox(Properties.Resources.MSG_CAPERROR, Properties.Resources.MSG_EX_ACTION, ex.Message, BusinessObjects.Controls.eTaskDialogButtons.OK, BusinessObjects.Controls.eSysIcons.Error);
            //                }
            //            }
            //        }

            //    };
            //    browserBaseObjects.ListObjectSave += delegate(object savedValue, EventArgs e)
            //    {
            //        if (savedValue is HierarchyContent)
            //            (savedValue as HierarchyContent).Save();
            //        else if (savedValue is Product)
            //            (savedValue as Product).Save();
            //    };
            //    browserBaseObjects.ListShowProperty += delegate(Hierarchy currentHierarchy, Product newObject)
            //    {
            //        Form frmProp = newObject.ShowProperty();
            //        frmProp.FormClosed += delegate
            //        {
            //            if (!newObject.IsNew)
            //            {
            //                HierarchyContent newContent = ShowTreeListCreateNewHierarchyContent(newObject, currentHierarchy);
            //                try
            //                {
            //                    newContent.Save();
            //                    if (browserBaseObjects.BindingSource.DataSource is List<Product>)
            //                        browserBaseObjects.BindingSource.Add(newObject);
            //                    else
            //                        browserBaseObjects.BindingSource.Add(newContent);

            //                }
            //                catch (Exception ex)
            //                {
            //                    BusinessObjects.Controls.cTaskDialog.MessageBox(
            //                        BusinessObjects.Windows.Properties.Resources.MSG_CAPERROR, Properties.Resources.MSG_EX_ACTION,
            //                        ex.Message, BusinessObjects.Controls.eTaskDialogButtons.Close,
            //                        BusinessObjects.Controls.eSysIcons.Error);

            //                }
            //            }
            //        };
            //    };
            //}
            #endregion
            Product returnValue = default(Product);
            frm.ShowDialog();

            return returnValue;
        }

        public static List<Product> BrowseContent(this Product item, Workarea wa = null)
        {
            ProductContentModule module = new ProductContentModule();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
        /// <summary>
        /// Мастер свойств объекта учета
        /// </summary>
        /// <param name="value">Объект учета</param>
        /// <returns></returns>
        public static Product ShowWizard(this Product value)
        {
            //Hierarchy newItem = new Hierarchy { Workarea = value.Workarea, KindId = Hierarchy.KINDID_GROUP };
            FormWizardProduct wizard = new FormWizardProduct();
            wizard.ShowInTaskbar = false;
            wizard.Icon = Icon.FromHandle(ResourceImage.GetByCode(value.Workarea, ResourceImage.PRODUCT_X16).GetHicon());
            BindingSource bindingEntities = new BindingSource();
            bindingEntities.DataSource = value.Entity.EntityKinds;
            DataGridViewHelper.GenerateGridColumns(value.Workarea, wizard.View, "DEFAULT_LOOKUP_NAME");
            wizard.Grid.DataSource = bindingEntities;

            bindingEntities.CurrencyManager.Position = value.Entity.EntityKinds.FindIndex(s => (s.Id == value.KindId));

            #region Единица измерения
            wizard.cmbUnit.Properties.DisplayMember = GlobalPropertyNames.Name;
            wizard.cmbUnit.Properties.ValueMember = GlobalPropertyNames.Id;
            BindingSource _bindUnits = new BindingSource();
            List<Unit>  _collUnits = new List<Unit>();
            _bindUnits.DataSource = _collUnits;
            wizard.cmbUnit.Properties.DataSource = _bindUnits;
            if (value.UnitId > 0)
            {
                Unit unit = new Unit() { Workarea = value.Workarea };
                unit.Load(value.UnitId);
                _collUnits.Add(unit);
            }
            wizard.cmbUnit.EditValue = value.UnitId;

            DataGridViewHelper.GenerateGridColumns(value.Workarea, wizard.gridLookUpEdit1View, "DEFAULT_LOOKUP");
            wizard.cmbUnit.Properties.View.BestFitColumns();
            wizard.gridLookUpEdit1View.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                                                                    {
                                                                        if (e.Column.FieldName == "Image" && e.IsGetData && _bindUnits.Count > 0)
                                                                        {
                                                                            Unit imageItem = _bindUnits[e.ListSourceRowIndex] as Unit;
                                                                            if (imageItem != null)
                                                                            {
                                                                                e.Value = imageItem.GetImage();
                                                                            }
                                                                        }
                                                                    };
            wizard.cmbUnit.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                 {
                                                     GridLookUpEdit cmb = sender as GridLookUpEdit;
                                                     if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                                                         cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                                                     try
                                                     {
                                                         wizard.Cursor = Cursors.WaitCursor;
                                                         if (cmb.Name == "cmbUnit" && _bindUnits.Count < 2)
                                                         {
                                                             _collUnits = value.Workarea.GetCollection<Unit>();
                                                             _bindUnits.DataSource = _collUnits;
                                                         }
                                                     }
                                                     catch (Exception)
                                                     {
                                                     }
                                                     finally
                                                     {
                                                         wizard.Cursor = Cursors.Default;
                                                     }
                                                 };
            wizard.cmbUnit.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                                            {
                                                if (e.Button.Index == 0) return;
                                                TreeListBrowser<Unit> browseDialog = new TreeListBrowser<Unit> { Workarea = value.Workarea }.ShowDialog();
                                                if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                                                if (!_bindUnits.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                                                    _bindUnits.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
                                                wizard.cmbUnit.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                                            };
            wizard.cmbUnit.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Delete)
                    wizard.cmbUnit.EditValue = 0;
            };
            #endregion

            wizard.txtName.Text = value.Name;
            wizard.txtNomenclature.Text = value.Nomenclature;
            wizard.txtMemo.Text = value.Memo;
            wizard.txtBarCode.Text = value.Barcode;
            wizard.wizardPage1.PageValidating +=
                delegate(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
                {
                    if (string.IsNullOrEmpty(wizard.txtName.Text))
                    {
                        if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
                            e.Valid = false;
                    }
                };
            wizard.wizardControl.FinishClick += delegate(object sender, System.ComponentModel.CancelEventArgs ef)
            {
                if (bindingEntities.Current != null)
                {
                    value.KindValue=(short)(bindingEntities.Current as EntityKind).Id;
                    value.Name = wizard.txtName.Text;
                    value.Nomenclature = wizard.txtNomenclature.Text;
                    value.Barcode = wizard.txtBarCode.Text;
                    value.Memo = wizard.txtMemo.Text;
                    value.UnitId = (int) wizard.cmbUnit.EditValue;
                    value.StateId = State.STATEACTIVE;
                    try
                    {
                        value.Save();
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(value.Workarea,
                            value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                            value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                        ef.Cancel = true;
                    }
                    catch (Exception ex)
                    {
                        Extentions.ShowMessagesExeption(value.Workarea,
                                                        value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                        value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                        ex);
                        ef.Cancel = true;
                    }
                }
            };
            if(!value.IsNew && value.IsReadOnly)
            {
                foreach (Control ctl in wizard.wizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.welcomeWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.completionWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                wizard.wizardControl.CustomizeCommandButtons +=
                    delegate(object sender, DevExpress.XtraWizard.CustomizeCommandButtonsEventArgs e)
                        {
                            e.FinishButton.Visible = false;
                        };
            }
            wizard.ShowDialog();
            return value;
        }
    }
}
