using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Drawing;
using DevExpress.XtraEditors.Popup;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlCountry : BasePropertyControlIBase<Country>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlCountry()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_COUNTRYREGIONS, ExtentionString.CONTROL_COUNTRYREGIONS);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;

            SelectedItem.Iso = _common.txtISO.Text;
            SelectedItem.Iso3 = _common.txtISO3.Text;
            SelectedItem.Stanag = _common.txtStanag.Text;
            SelectedItem.IsoNum = (int)_common.numISO.Value;
            SelectedItem.Iana = _common.txtIANA.Text;
            SelectedItem.Fips = _common.txtFIPS.Text;
            SelectedItem.X = _ccl.seLocationX.Value;
            SelectedItem.Y = _ccl.seLocationY.Value;
            SelectedItem.CurrencyId = _common.cmbCurrency.EditValue == null ? 0 : (int)_common.cmbCurrency.EditValue;
            SelectedItem.Continent = _common.cmbContinent.Text;
            SaveStateData();

            InternalSave();
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_COUNTRYREGIONS)
                BuildPageTerritories();
        }
        ControlList _controlTerritory;
        private void BuildPageTerritories()
        {
            if (_controlTerritory == null)
            {
                _controlTerritory = new ControlList {Name = ExtentionString.CONTROL_COUNTRYREGIONS};
                // Данные для отображения в списке связей
                BindingSource collectionBind = new BindingSource();
                List<Territory> collection = SelectedItem.Workarea.GetCollection<Territory>().Where(s=>s.CountryId == SelectedItem.Id && s.KindValue==Territory.KINDVALUE_REGION).ToList();
                collectionBind.DataSource = collection;
                _controlTerritory.Grid.DoubleClick += delegate
                {
                    if (collectionBind.Current != null)
                        (collectionBind.Current as Territory).ShowPropertyTerritory();
                };
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_COUNTRYREGIONS)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Новая связь
                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       ButtonStyle = BarButtonStyle.DropDown,
                                                       ActAsDropDown = true,
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);

                PopupMenu mnuTemplates = new PopupMenu {Ribbon = frmProp.ribbon};
                BarButtonItem btn = new BarButtonItem {Caption = "Область"};
                mnuTemplates.AddItem(btn);
                btn.ItemClick += delegate
                {
                    Territory newTerritory = new Territory { Workarea = SelectedItem.Workarea, CountryId = SelectedItem.Id };
                    newTerritory.ShowPropertyTerritory().FormClosed += delegate(object s, FormClosedEventArgs ev)
                    {
                        if (s != null)
                        {
                            Form f = s as Form;
                            if (f.DialogResult == DialogResult.OK)
                            {
                                int index = collectionBind.Add(newTerritory);
                                collectionBind.Position = index;
                            }
                        }
                    };
                };
                btnChainCreate.DropDownControl = mnuTemplates;
                #endregion

                #region Изменить
                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption =
                                                    SelectedItem.Workarea.Cashe.ResourceString(
                                                        ResourceString.BTN_CAPTION_EDIT, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);

                btnProp.ItemClick += delegate
                {
                    ((collectionBind.Current) as Territory).ShowPropertyTerritory();
                };
                #endregion

                #region Удаление
                BarButtonItem btnChainDelete = new BarButtonItem
                                                   {
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph =
                                                           ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                                   ResourceImage.DELETE_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainDelete);
                btnChainDelete.ItemClick += delegate
                {
                    Territory currentTerritory = collectionBind.Current as Territory;
                    if (currentTerritory != null)
                    {
                        if (MessageBox.Show("Вы уверенны, что хотите удалить указанный объект?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                currentTerritory.Delete();
                                collectionBind.Remove(currentTerritory);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                         "Ошибка удаления связи!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlTerritory.View, "DEFAULT_LISTVIEW");
                _controlTerritory.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        Rectangle r = e.Bounds;
                        Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINK_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                };
                Control.Controls.Add(_controlTerritory);
                _controlTerritory.Dock = DockStyle.Fill;

                _controlTerritory.Grid.DataSource = collectionBind;
            }
            HidePageControls(ExtentionString.CONTROL_COUNTRYREGIONS);
        }

        private BindingSource _bindCurrency;
        private List<Currency> _collCurrency;

        private PopupMenu imageEditPopupMenu;

        ControlCountry _common;
        ControlGeoLocation _ccl;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlCountry
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtISO = {Text = SelectedItem.Iso},
                                  txtISO3 = {Text = SelectedItem.Iso3},
                                  txtStanag = {Text = SelectedItem.Stanag},
                                  numISO = {Value = SelectedItem.IsoNum},
                                  txtIANA = {Text = SelectedItem.Iana},
                                  txtFIPS = {Text = SelectedItem.Fips},
                                  cmbContinent = { Text = SelectedItem.Continent },
                                  //cmbContinent = {Text = SelectedItem.X.ToString() + ";" + SelectedItem.Y.ToString()},
                                  Workarea = SelectedItem.Workarea
                              };
                //#region Настройка расположения элементов управления
                //// Поиск данных о настройке
                //string controlName = string.Empty;
                //string entityKind = string.Empty;
                //string keyValue = _common.Tag != null ? _common.Tag.ToString() : _common.GetType().Name;

                //if (!string.IsNullOrWhiteSpace((Owner as IWorkareaForm).Key))
                //    controlName = (Owner as IWorkareaForm).Key;

                //entityKind = SelectedItem.KindId.ToString();

                //// Общие поисковые данные
                //List<XmlStorage> collSet = SelectedItem.Workarea.Empty<XmlStorage>().FindBy(kindId: 2359299,
                //                                                                            name: controlName,
                //                                                                            code: keyValue,
                //                                                                            flagString: entityKind);
                //if (collSet.Count > 0)
                //{
                //    // Уточняющий подзапрос
                //    XmlStorage setiings = collSet.FirstOrDefault(f => f.KindId == 2359299
                //                                                                && f.Name == controlName
                //                                                                && f.Code == keyValue
                //                                                                && f.FlagString == entityKind);
                //    if (setiings != null && !string.IsNullOrWhiteSpace(setiings.XmlData))
                //    {
                //        MemoryStream s = new MemoryStream();
                //        StreamWriter w = new StreamWriter(s) { AutoFlush = true };
                //        w.Write(setiings.XmlData);
                //        s.Position = 0;
                //        try
                //        {
                //            _common.LayoutControl.RestoreLayoutFromStream(s);
                //        }
                //        catch (Exception)
                //        {

                //        }
                //    }
                //}
                ////
                //if (SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = true;
                //    _common.LayoutControl.RegisterUserCustomizatonForm(typeof(FormCustomLayout));
                //}
                //else
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = false;
                //}
                //#endregion
                PopupContainerControl pcc = new PopupContainerControl();
                _ccl = new ControlGeoLocation();
                pcc.Controls.Add(_ccl);
                pcc.Size = new Size(_common.ppcLocation.Size.Width, _ccl.Size.Height);
                _ccl.Dock = DockStyle.Fill;
                _ccl.seLocationX.Value = SelectedItem.X;
                _ccl.seLocationY.Value = SelectedItem.Y;
                _common.ppcLocation.Closed += delegate
                {
                    SelectedItem.X = _ccl.seLocationX.Value;
                    SelectedItem.Y = _ccl.seLocationY.Value;
                    _common.ppcLocation.Text = SelectedItem.X + ";" + SelectedItem.Y;
                };
                _common.ppcLocation.Text = SelectedItem.X + ";" + SelectedItem.Y;
                _common.ppcLocation.Properties.PopupControl = pcc;
                _common.ppcLocation.ButtonClick += PpcLocationButtonClick;

                #region Данные для списка "Валюты"
                _common.cmbCurrency.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbCurrency.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindCurrency = new BindingSource();
                _collCurrency = new List<Currency>();
                _bindCurrency.DataSource = _collCurrency;
                _common.cmbCurrency.Properties.DataSource = _bindCurrency;
                if (SelectedItem.CurrencyId > 0)
                {
                    Currency curr = new Currency() { Workarea = SelectedItem.Workarea };
                    curr.Load(SelectedItem.CurrencyId);
                    _collCurrency.Add(curr);
                }
                _common.cmbCurrency.EditValue = SelectedItem.CurrencyId;

                // TODO: Сделать View для списка валют
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.gridLookUpEdit1View, "DEFAULT_LOOKUP");
                _common.cmbCurrency.Properties.View.BestFitColumns();
                _common.gridLookUpEdit1View.CustomUnboundColumnData += ViewCurrencyCustomUnboundColumnData;
                _common.cmbCurrency.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbCurrency.ButtonClick += CmbCurrencyButtonClick;
                _common.cmbCurrency.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbCurrency.EditValue = 0;
                };
                #endregion

                #region Данные для списка континентов

                _common.cmbContinent.Properties.Items.AddRange(SelectedItem.GetDistinctContinents());
                #endregion

                #region Герб

                _common.edEmblem.Properties.QueryPopUp += delegate
                                                              {
                                                                  if (_common.edEmblem.Properties.PopupFormSize.Width != _common.edEmblem.Width)
                                                                      _common.edEmblem.Properties.PopupFormSize = new Size(_common.edEmblem.Width, 150);

                                                                  if((SelectedItem.EmblemId!=0)&&(_common.edEmblem.Image==null))
                                                                  {
                                                                      FileData file = new FileData() { Workarea = SelectedItem.Workarea };
                                                                      file.Load(SelectedItem.EmblemId);
                                                                      _common.edEmblem.Image = Image.FromStream(new MemoryStream(file.StreamData));
                                                                  }
                                                              };
                _common.edEmblem.Properties.Popup += delegate(object sender, EventArgs e)
                                                         {
                                                             ImagePopupForm f = (_common.edEmblem as DevExpress.Utils.Win.IPopupControl).PopupWindow as ImagePopupForm;
                                                             PropertyInfo pi = typeof (ImagePopupForm).GetProperty("Picture", BindingFlags.NonPublic | BindingFlags.Instance);
                                                             PictureEdit pe = pi.GetValue(f, null) as PictureEdit;
                                                             pe.MouseClick -= new MouseEventHandler(PictureEdit_MouseClick);
                                                             pe.MouseClick += new MouseEventHandler(PictureEdit_MouseClick);
                                                         };
               
                _common.edEmblem.Properties.ShowMenu = false;
                #endregion

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if (!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }

        void PictureEdit_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (imageEditPopupMenu == null)
                {
                    imageEditPopupMenu = new PopupMenu { Ribbon = frmProp.Ribbon };

                    BarButtonItem mnuAdd=null;
                    BarButtonItem mnuOpen = null;
                    BarButtonItem mnuSave = null;
                    BarButtonItem mnuDelete = null;

                    #region Выбрать
                    mnuAdd = new BarButtonItem { Caption = "Выбрать", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.ADD_X16)};
                    mnuAdd.ItemClick += delegate
                                            {
                                                TreeListBrowser<FileData> browseDialog = new TreeListBrowser<FileData> { Workarea = SelectedItem.Workarea, RootCode = Hierarchy.SYSTEM_FILEDATA_COUNTRYEMBLEMS}.ShowDialog();
                                                if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
                                                SelectedItem.EmblemId = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
                                                FileData file = new FileData { Workarea = SelectedItem.Workarea };
                                                file.Load(SelectedItem.EmblemId);
                                                _common.edEmblem.Image = Image.FromStream(new MemoryStream(file.StreamData));
                                                mnuSave.Enabled = true;
                                                mnuDelete.Enabled = true;
                                            };
                    imageEditPopupMenu.AddItem(mnuAdd);
                    #endregion

                    #region Открыть
                    mnuOpen = new BarButtonItem { Caption = "Открыть", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.OPEN_X16) };
                    mnuOpen.ItemClick += delegate
                                             {
                                                 OpenFileDialog dialog = new OpenFileDialog { Filter = "Графические файлы|*.bmp;*.jpg;*.jpeg;*.png|Все файлы|*.*" };
                                                if(dialog.ShowDialog()==DialogResult.OK)
                                                {
                                                    _common.edEmblem.Image = Image.FromFile(dialog.FileName);

                                                    FileData fileData = new FileData
                                                    {
                                                        Workarea = SelectedItem.Workarea,
                                                        Name = Path.GetFileNameWithoutExtension(dialog.FileName),
                                                        FileExtention = Path.GetExtension(dialog.FileName).Substring(1),
                                                        StreamData = File.ReadAllBytes(dialog.FileName),
                                                        KindId = FileData.KINDID_FILEDATA,
                                                        StateId = State.STATEACTIVE
                                                    };

                                                    fileData.Save();
                                                    SelectedItem.EmblemId = fileData.Id;

                                                    Hierarchy emblemsRoot = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_FILEDATA_COUNTRYEMBLEMS);
                                                    if(emblemsRoot==null)
                                                        throw new Exception("Не найдена иерархия для хранения файлов гербов стран");
                                                    emblemsRoot.ContentAdd(fileData);

                                                    mnuSave.Enabled = true;
                                                    mnuDelete.Enabled = true;
                                                }
                                            };
                    imageEditPopupMenu.AddItem(mnuOpen);
                    #endregion

                    #region Сохранить
                    mnuSave = new BarButtonItem { Caption = "Сохранить", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.SAVE_X16), Enabled = SelectedItem.EmblemId > 0 };
                    mnuSave.ItemClick += delegate
                    {
                        if(SelectedItem.EmblemId==0)
                            return;

                        FileData file = new FileData() { Workarea = SelectedItem.Workarea };
                        file.Load(SelectedItem.EmblemId);

                        SaveFileDialog dialog = new SaveFileDialog()
                                                    {
                                                        FileName = file.Name,
                                                        Filter = string.Format("{0}|*.{0}|Все файлы|*.*", file.FileExtention),
                                                        AddExtension = true
                                                    };
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            file.ExportStreamDataToFile(dialog.FileName);
                        }
                    };
                    imageEditPopupMenu.AddItem(mnuSave);
                    #endregion

                    #region Удалить
                    mnuDelete = new BarButtonItem { Caption = "Удалить", Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X16), Enabled = SelectedItem.EmblemId > 0 };
                    mnuDelete.ItemClick += delegate
                                               {
                                                   //_common.edEmblem.Image = null;
                                                   ImagePopupForm f = (_common.edEmblem as DevExpress.Utils.Win.IPopupControl).PopupWindow as ImagePopupForm;
                                                   PropertyInfo pi = typeof(ImagePopupForm).GetProperty("Picture", BindingFlags.NonPublic | BindingFlags.Instance);
                                                   PictureEdit pe = pi.GetValue(f, null) as PictureEdit;
                                                   pe.Image = null;
                                                   SelectedItem.EmblemId = 0;
                                                   mnuSave.Enabled = false;
                                                   mnuDelete.Enabled = false;
                                               };
                    imageEditPopupMenu.AddItem(mnuDelete);
                    #endregion
                }
                imageEditPopupMenu.ShowPopup(Cursor.Position);
            }
                
        }

        void PpcLocationButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                SelectedItem.ShowOnGoogleMap();
            }
        }

        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbCurrency" && _bindCurrency.Count < 2)
                {
                    _collCurrency = SelectedItem.Workarea.GetCollection<Currency>();
                    _bindCurrency.DataSource = _collCurrency;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        
        void ViewCurrencyCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindCurrency.Count > 0)
            {
                Currency imageItem = _bindCurrency[e.ListSourceRowIndex] as Currency;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
        }

        void CmbCurrencyButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Currency> browseDialog = new TreeListBrowser<Currency> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindCurrency.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindCurrency.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbCurrency.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
    }
}
