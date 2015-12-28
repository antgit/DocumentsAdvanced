using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlCustomViewList : BasePropertyControlIBase<CustomViewList>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlCustomViewList()
            : base()
        {
            // Состав возможных закладок в окне свойст
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_COLUMNS_NAME, ExtentionString.CONTROL_COLUMNS_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            // сохранение данных закладки "Общая"
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.IsCollectionBased = _common.chkIsCollectionBased.Checked;
            SelectedItem.SystemName = _common.cmbCustomView.Text;
            SelectedItem.SystemNameRefresh = _common.cmbSystemNameRefresh.Text;
            SelectedItem.SourceEntityTypeId = (int)_common.cmbDbEntityKind.EditValue;
            SelectedItem.GroupPanelVisible = _common.chkGroupPanelVisible.Checked;
            SelectedItem.AutoFilterVisible = _common.chkAutoFilter.Checked;
            SelectedItem.UseLayout = _common.chkUseLayout.Checked;
            SelectedItem.LayoutId = (int) _common.cmbLayoutId.EditValue;
            SelectedItem.ShowIndicator = _common.chkShowIndicator.Checked;
            // Сосхранение состоянй
            SaveStateData();
            try
            {
                // Очистка текущих настроек списков для типа
                if(SelectedItem.SourceEntityTypeId!=0)
                {
                    
                    string name = SelectedItem.Workarea.Empty(SelectedItem.SourceEntityType.Id).GetType().Name;
                    using (SqlCommand cmd = new SqlCommand(string.Format("DELETE FROM Core.XmlStorages WHERE Code LIKE '%{0}_FOLDER_VIEW_%'", name), SelectedItem.Workarea.GetDatabaseConnection()))
                    {
                        cmd.CommandType = CommandType.Text;
                        int count = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
            InternalSave();
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_COLUMNS_NAME)
                BuildPageColumns();
        }
        ControlCustomList _common;
        private BindingSource _bindingSourceLayout;
        private List<XmlStorage> _collLayout;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlCustomList
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = {Text = SelectedItem.NameFull},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = {Text = SelectedItem.CodeFind},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  chkIsCollectionBased = {Checked = SelectedItem.IsCollectionBased},
                                  chkGroupPanelVisible = {Checked = SelectedItem.GroupPanelVisible},
                                  chkAutoFilter = {Checked = SelectedItem.AutoFilterVisible},
                                  cmbCustomView = {Text = SelectedItem.SystemName},
                                  chkUseLayout = {Checked = SelectedItem.UseLayout},
                                  Workarea = SelectedItem.Workarea,
                                  chkShowIndicator = {Checked = SelectedItem.ShowIndicator}
                              };

                #region Данные для списка "Настройки списка"
                _common.cmbLayoutId.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbLayoutId.Properties.ValueMember = GlobalPropertyNames.Id;
                _bindingSourceLayout = new BindingSource();
                _collLayout = new List<XmlStorage>();
                if (SelectedItem.LayoutId != 0)
                    _collLayout.Add(SelectedItem.Workarea.Cashe.GetCasheData<XmlStorage>().Item(SelectedItem.LayoutId));
                _bindingSourceLayout.DataSource = _collLayout;
                _common.cmbLayoutId.Properties.DataSource = _bindingSourceLayout;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbLayoutId, "DEFAULT_LOOKUP_NAME");
                _common.cmbLayoutId.EditValue = SelectedItem.LayoutId;
                _common.cmbLayoutId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbLayoutId.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbLayoutId.EditValue = 0;
                };
                #endregion

                List<string> proc = SelectedItem.Workarea.GetCollectionProcedures("Extention", true);
                _common.cmbCustomView.Properties.Items.AddRange(proc);
                _common.cmbSystemNameRefresh.Text = SelectedItem.SystemNameRefresh;
                _common.cmbSystemNameRefresh.Properties.Items.AddRange(proc);
                BindingSource bindingSourceEntity = new BindingSource
                                                        {
                                                            DataSource = SelectedItem.Workarea.CollectionEntities
                                                        };
                _common.cmbDbEntityKind.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbDbEntityKind.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbDbEntityKind.Properties.DataSource = bindingSourceEntity;
                _common.cmbDbEntityKind.EditValue = SelectedItem.SourceEntityTypeId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbDbEntityKind, "DEFAULT_LOOKUP_NAME");

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
        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LookUpEdit cmb = sender as LookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbLayoutId" && _bindingSourceLayout.Count < 2)
                {
                    _collLayout = SelectedItem.Workarea.GetCollection<XmlStorage>().Where(s => s.KindValue == 5).ToList();
                    _bindingSourceLayout.DataSource = _collLayout;
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
        private ControlList _controlColumnList;
        /// <summary>
        /// Закладка "Колонки"
        /// </summary>
        private void BuildPageColumns()
        {
            if (_controlColumnList == null)
            {
                ListBrowserBaseObjects<CustomViewColumn> browserBaseObjects = new ListBrowserBaseObjects<CustomViewColumn>(SelectedItem.Workarea, SelectedItem.Columns, null, null, true, false, false, true);
                browserBaseObjects.Build();
                browserBaseObjects.ListControl.Name = ExtentionString.CONTROL_COLUMNS_NAME;
                _controlColumnList = browserBaseObjects.ListControl;

                browserBaseObjects.CreateNew += delegate(CustomViewColumn obj, CustomViewColumn objTemplate)
                                                    {
                                                        obj.DataType = "System.String";
                                                        obj.With = 75;
                                                        obj.Visible = true;
                                                        obj.DisplayHeader = true;
                    obj.CustomViewListId = SelectedItem.Id;
                    obj.OrderNo =(short)(SelectedItem.Columns.Count + 1);
                };
                browserBaseObjects.ShowProperty += delegate(CustomViewColumn obj)
                {
                    Form frmProperties = obj.ShowProperty();
                    frmProperties.FormClosed += delegate
                    {
                        if (!obj.IsNew)
                        {
                            if (!browserBaseObjects.BindingSource.Contains(obj))
                            {
                                int position = browserBaseObjects.BindingSource.Add(obj);
                                browserBaseObjects.BindingSource.Position = position;
                            }
                        }
                    };
                };
                // Построение группы упраления связями
                RibbonPage page =
                    frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_COLUMNS_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                #region Создать
                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       ButtonStyle = BarButtonStyle.DropDown,
                                                       ActAsDropDown = true,
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                browserBaseObjects.ListControl.CreateMenu.Ribbon = frmProp.ribbon;
                btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu; 
                #endregion
                #region Свойства
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
                                             browserBaseObjects.InvokeProperties();
                                         };

                #endregion
                #region Удаление
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph =
                                                      ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                              ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                btnDelete.ItemClick += delegate
                {
                    browserBaseObjects.InvokeDelete();
                };
                #endregion
                page.Groups.Add(groupLinksAction);

                Control.Controls.Add(_controlColumnList);
                _controlColumnList.Dock = DockStyle.Fill;
            }
            HidePageControls(ExtentionString.CONTROL_COLUMNS_NAME);
        }
    }
}