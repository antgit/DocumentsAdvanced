using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlChainKind : BasePropertyControlICore<ChainKind>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlChainKind()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.Id = (int)_common.numId.Value;
            SelectedItem.FromEntityId = (int) _common.cmbEntityFrom.EditValue;
            SelectedItem.ToEntityId = (int)_common.cmbEntityTo.EditValue;
            SelectedItem.NameRight = _common.txtNameRight.Text;
            InternalSave();
        }

        ControlChainKind _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlChainKind
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  numId = {Value = SelectedItem.Id},
                                  txtNameRight = {Text = SelectedItem.NameRight},
                                  Workarea = SelectedItem.Workarea
                              };
                //if (SelectedItem.Workarea.Access.RightCommon.AdminEnterprize)
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = true;
                //    _common.LayoutControl.RegisterUserCustomizatonForm(typeof(FormCustomLayout));
                //}
                //else
                //{
                //    _common.LayoutControl.AllowCustomizationMenu = false;
                //}
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbEntityFrom, "DEFAULT_LOOKUPENTITYTYPE");
                BindingSource bindingSourceEntityFrom = new BindingSource
                                                        {
                                                            DataSource = SelectedItem.Workarea.CollectionEntities
                                                        };
                _common.cmbEntityFrom.Properties.DisplayMember = "Name";
                _common.cmbEntityFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbEntityFrom.Properties.DataSource = bindingSourceEntityFrom;
                _common.cmbEntityFrom.EditValue = SelectedItem.FromEntityId;

                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbEntityTo, "DEFAULT_LOOKUPENTITYTYPE");
                BindingSource bindingSourceEntityTo = new BindingSource
                {
                    DataSource = SelectedItem.Workarea.CollectionEntities
                };
                _common.cmbEntityTo.Properties.DisplayMember = "Name";
                _common.cmbEntityTo.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbEntityTo.Properties.DataSource = bindingSourceEntityTo;
                _common.cmbEntityTo.EditValue = SelectedItem.ToEntityId;

                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _common.GridSubKind.View, "DEFAULT_CHAINKINDCONTENTTYPES");
                _common.GridSubKind.View.OptionsView.ShowColumnHeaders = true;
                BindingSource bindEntKinds = new BindingSource();
                List<ChainKindContentType> collValues = ChainKindContentType.GetCollection(SelectedItem.Workarea, SelectedItem.Id);
                bindEntKinds.DataSource = collValues;
                _common.GridSubKind.Grid.DataSource = bindEntKinds;

                    
                BarButtonItem btnAdd = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnAdd);
                btnAdd.ItemClick += delegate
                                        {
                                            if (SelectedItem.ToEntity.EntityKinds.Count == 0)
                                                return;
                                            ChainKindContentType valueAdd = SelectedItem.ShowPropertyChainKindContentType(null);
                                            if (valueAdd!=null)
                                            {
                                                if (!bindEntKinds.Contains(valueAdd))
                                                bindEntKinds.Add(valueAdd);
                                                _common.GridSubKind.Grid.RefreshDataSource();
                                            }
                                            
                                            //List<EntityKind> col =
                                            //    Extentions.BrowseMultyList(SelectedItem.ToEntity.EntityKinds[0],
                                            //                               SelectedItem.Workarea, null,
                                            //                               SelectedItem.ToEntity.EntityKinds, false);
                                            //if (col != null && col.Count > 0)
                                            //{
                                            //    ChainKindContentType valueAdd =
                                            //        collValues.Find(s => s.EntityKindId == col[0].Id);
                                            //    if (valueAdd == null)
                                            //    {
                                            //        valueAdd = new ChainKindContentType
                                            //                       {Workarea = SelectedItem.Workarea, StateId = State.STATEACTIVE};
                                            //        valueAdd.ElementId = SelectedItem.Id;
                                            //        valueAdd.EntityKindId = col[0].Id;
                                            //        valueAdd.Save();
                                            //        collValues.Add(valueAdd);
                                            //        if (!bindEntKinds.Contains(valueAdd))
                                            //            bindEntKinds.Add(valueAdd);
                                            //        _common.GridSubKind.Grid.RefreshDataSource();
                                            //    }
                                            //}
                                        };

                BarButtonItem btnEdit = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnEdit);
                btnEdit.ItemClick += delegate
                                         {
                                             if (bindEntKinds.Current == null) return;
                                             ChainKindContentType val = bindEntKinds.Current as ChainKindContentType;
                                             SelectedItem.ShowPropertyChainKindContentType(val);
                                             _common.GridSubKind.Grid.RefreshDataSource();
                                         };
                BarButtonItem btnRemove = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                };
                frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnRemove);
                btnRemove.ItemClick += delegate
                {
                    if (bindEntKinds.Current == null) return;
                    ChainKindContentType val = bindEntKinds.Current as ChainKindContentType;
                    val.Delete();
                    bindEntKinds.RemoveCurrent();
                };
                UIHelper.GenerateTooltips<ChainKind>(SelectedItem, _common);
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
    }
}