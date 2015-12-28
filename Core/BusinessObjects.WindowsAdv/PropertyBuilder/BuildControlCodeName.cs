using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств
    /// </summary>
    internal sealed class BuildControlCodeName : BasePropertyControlIBase<CodeName>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlCodeName()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
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
            SelectedItem.App = _common.cmbApp.Text;
            SelectedItem.ToEntityId = (int)_common.cmbToEntity.EditValue;
            if (SelectedItem.KindValue == CodeName.KINDVALUE_DOCUMENT)
            {
                SelectedItem.DocTypeId = (int)_common.cmbDocType.EditValue;
            }
            else
                SelectedItem.DocTypeId = 0;
            SaveStateData();

            InternalSave();
        }

        ControlCodeName _common;
        private BindingSource bindingSourceEntity;
        private BindingSource bindingSourceDocEntity;
        
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlCodeName
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = { Text = SelectedItem.Name },
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = { Text = SelectedItem.Memo },
                                  cmbApp = {Text = SelectedItem.App},
                                  Workarea = SelectedItem.Workarea
                              };
                bindingSourceEntity = new BindingSource
                {
                    DataSource = SelectedItem.Workarea.CollectionEntities
                };
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbToEntity, "DEFAULT_LOOKUP_NAME");
                _common.cmbToEntity.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbToEntity.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbToEntity.Properties.DataSource = bindingSourceEntity;
                _common.cmbToEntity.EditValue = SelectedItem.ToEntityId;

                _common.cmbApp.QueryPopUp += cmbApp_QueryPopUp;

                if (SelectedItem.KindValue == CodeName.KINDVALUE_DOCUMENT)
                {
                    bindingSourceDocEntity = new BindingSource
                                                 {
                                                     DataSource = SelectedItem.Workarea.CollectionDocumentTypes()
                                                 };
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbDocType,
                                                             "DEFAULT_LOOKUP_NAME");
                    _common.cmbDocType.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbDocType.Properties.ValueMember = GlobalPropertyNames.Id;
                    _common.cmbDocType.Properties.DataSource = bindingSourceDocEntity;
                    _common.cmbDocType.EditValue = SelectedItem.DocTypeId;
                }
                else
                {
                    _common.layoutControlItemDocType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                if (SelectedItem.KindValue == CodeName.KINDVALUE_DEFAULT)
                {
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.GridSubKind.View,
                                                           "DEFAULT_LOOKUP_NAME");
                    BindingSource bindEntKinds = new BindingSource();
                    List<CodeNameEntityKind> collValues = CodeNameEntityKind.GetCollection(SelectedItem.Workarea,
                                                                                           SelectedItem.Id);
                    bindEntKinds.DataSource = collValues;
                    _common.GridSubKind.Grid.DataSource = bindEntKinds;
                    _common.GridSubKind.View.OptionsView.ShowColumnHeaders = false;


                    BarButtonItem btnAdd = new BarButtonItem
                                               {
                                                   Caption =
                                                       SelectedItem.Workarea.Cashe.ResourceString(
                                                           ResourceString.BTN_CAPTON_ADD, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                                               };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnAdd);
                    btnAdd.ItemClick += delegate
                                            {
                                                if (SelectedItem.ToEntity.EntityKinds.Count == 0)
                                                    return;
                                                List<EntityKind> col =
                                                    Extentions.BrowseMultyList(SelectedItem.ToEntity.EntityKinds[0],
                                                                               SelectedItem.Workarea, null,
                                                                               SelectedItem.ToEntity.EntityKinds, false);
                                                if (col != null && col.Count > 0)
                                                {
                                                    CodeNameEntityKind valueAdd =
                                                        collValues.Find(s => s.EntityKindId == col[0].Id);
                                                    if (valueAdd == null)
                                                    {
                                                        valueAdd = new CodeNameEntityKind
                                                                       {Workarea = SelectedItem.Workarea};
                                                        valueAdd.ElementId = SelectedItem.Id;
                                                        valueAdd.EntityKindId = col[0].Id;
                                                        valueAdd.Save();
                                                        collValues.Add(valueAdd);
                                                        if (!bindEntKinds.Contains(valueAdd))
                                                            bindEntKinds.Add(valueAdd);
                                                        _common.GridSubKind.Grid.RefreshDataSource();
                                                    }
                                                }
                                            };


                    BarButtonItem btnRemove = new BarButtonItem
                                                  {
                                                      Caption =
                                                          SelectedItem.Workarea.Cashe.ResourceString(
                                                              ResourceString.BTN_CAPTION_DELETE, 1049),
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph =
                                                          ResourceImage.GetByCode(SelectedItem.Workarea,
                                                                                  ResourceImage.DELETE_X32)
                                                  };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnRemove);
                    btnRemove.ItemClick += delegate
                                               {
                                                   if (bindEntKinds.Current == null) return;
                                                   CodeNameEntityKind val = bindEntKinds.Current as CodeNameEntityKind;
                                                   val.Delete();
                                                   bindEntKinds.RemoveCurrent();
                                               };

                }

                else if (SelectedItem.KindValue == CodeName.KINDVALUE_DOCUMENT)
                {
                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.GridSubKind.View,
                                                           "DEFAULT_LOOKUP_NAME");
                    BindingSource bindEntKinds = new BindingSource();
                    List<CodeNameDocumentKind> collValues = CodeNameDocumentKind.GetCollection(SelectedItem.Workarea,
                                                                                           SelectedItem.Id);
                    bindEntKinds.DataSource = collValues;
                    _common.GridSubKind.Grid.DataSource = bindEntKinds;
                    _common.GridSubKind.View.OptionsView.ShowColumnHeaders = false;


                    BarButtonItem btnAdd = new BarButtonItem
                    {
                        Caption =
                            SelectedItem.Workarea.Cashe.ResourceString(
                                ResourceString.BTN_CAPTON_ADD, 1049),
                        RibbonStyle = RibbonItemStyles.Large,
                        Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                    };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnAdd);
                    btnAdd.ItemClick += delegate
                    {
                        
                        if (SelectedItem.DocType.Kinds.Count == 0)
                            return;
                        List<EntityDocumentKind> col =
                            Extentions.BrowseMultyList(SelectedItem.DocType.Kinds[0],
                                                       SelectedItem.Workarea, null,
                                                       SelectedItem.DocType.Kinds, false);
                        if (col != null && col.Count > 0)
                        {
                            CodeNameDocumentKind valueAdd =
                                collValues.Find(s => s.EntityKindId == col[0].Id);
                            if (valueAdd == null)
                            {
                                valueAdd = new CodeNameDocumentKind { Workarea = SelectedItem.Workarea };
                                valueAdd.ElementId = SelectedItem.Id;
                                valueAdd.EntityKindId = col[0].Id;
                                valueAdd.Save();
                                collValues.Add(valueAdd);
                                if (!bindEntKinds.Contains(valueAdd))
                                    bindEntKinds.Add(valueAdd);
                                _common.GridSubKind.Grid.RefreshDataSource();
                            }
                        }
                    };


                    BarButtonItem btnRemove = new BarButtonItem
                    {
                        Caption =
                            SelectedItem.Workarea.Cashe.ResourceString(
                                ResourceString.BTN_CAPTION_DELETE, 1049),
                        RibbonStyle = RibbonItemStyles.Large,
                        Glyph =
                            ResourceImage.GetByCode(SelectedItem.Workarea,
                                                    ResourceImage.DELETE_X32)
                    };
                    frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnRemove);
                    btnRemove.ItemClick += delegate
                    {
                        if (bindEntKinds.Current == null) return;
                        CodeNameDocumentKind val = bindEntKinds.Current as CodeNameDocumentKind;
                        val.Delete();
                        bindEntKinds.RemoveCurrent();
                    };

                }
                UIHelper.GenerateTooltips<CodeName>(SelectedItem, _common);
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

        void cmbApp_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ComboBoxEdit cmb = sender as ComboBoxEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;

                if (cmb.Name == "cmbApp" && _common.cmbApp.Properties.Items.Count ==0)
                {
                    _common.cmbApp.Properties.Items.AddRange(SelectedItem.GetAppNames());
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
    }
}