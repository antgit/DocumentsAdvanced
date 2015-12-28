using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlFactColumn : BasePropertyControlIBase<FactColumn>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlFactColumn()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACTCOLUMNENTITYKIND, ExtentionString.CONTROL_FACTCOLUMNENTITYKIND);
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_FACTCOLUMNENTITYKIND)
                BuildPageEntityKinds();
        }

        private ControlList _controlSubKinds;
        private void BuildPageEntityKinds()
        {
            if(_controlSubKinds==null)
            {
                _controlSubKinds = new ControlList {Name = ExtentionString.CONTROL_FACTCOLUMNENTITYKIND};
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlSubKinds.View, "DEFAULT_LOOKUP_NAME");
                Control.Controls.Add(_controlSubKinds);
                BindingSource bindEntKinds = new BindingSource();
                List<FactColumnEntityKind> collValues = FactColumnEntityKind.GetCollection(SelectedItem.Workarea, SelectedItem.Id);
                bindEntKinds.DataSource = collValues;
                _controlSubKinds.Grid.DataSource = bindEntKinds;
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_FACTCOLUMNENTITYKIND)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();


                BarButtonItem btnAdd = new BarButtonItem
                {
                    Caption = "Добавить",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                };
                groupLinksAction.ItemLinks.Add(btnAdd);
                btnAdd.ItemClick+=delegate
                                      {
                                          if(SelectedItem.FactName.ToEntity.EntityKinds.Count==0)
                                              return;
                                          List<EntityKind> col = Extentions.BrowseMultyList(SelectedItem.FactName.ToEntity.EntityKinds[0], SelectedItem.Workarea, null, SelectedItem.FactName.ToEntity.EntityKinds, false);
                                          if (col != null && col.Count > 0)
                                          {
                                              FactColumnEntityKind valueAdd = collValues.Find(s => s.EntityKindId == col[0].Id);
                                              if(valueAdd==null)
                                              {
                                                  valueAdd = new FactColumnEntityKind {Workarea = SelectedItem.Workarea};
                                                  valueAdd.ColumnId = SelectedItem.Id;
                                                  valueAdd.EntityKindId = col[0].Id;
                                                  valueAdd.Save();
                                                  collValues.Add(valueAdd);
                                                  if (!bindEntKinds.Contains(valueAdd))
                                                      bindEntKinds.Add(valueAdd);
                                                  _controlSubKinds.Grid.RefreshDataSource();
                                              }
                                          }
                                      };

                BarButtonItem btnRemove = new BarButtonItem
                {
                    Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.DELETE_X32)
                };
                groupLinksAction.ItemLinks.Add(btnRemove);
                btnRemove.ItemClick += delegate
                                           {
                                               if (bindEntKinds.Current == null) return;
                                               FactColumnEntityKind val = bindEntKinds.Current as FactColumnEntityKind;
                                               val.Delete();
                                               bindEntKinds.RemoveCurrent();
                                           };
                page.Groups.Add(groupLinksAction);

                page.Groups.Add(groupLinksAction);
                _controlSubKinds.Dock = DockStyle.Fill;
            }
            HidePageControls(ExtentionString.CONTROL_FACTCOLUMNENTITYKIND);
        }

        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;
            SelectedItem.OrderNo = (int)_common.editNumber.Value;
            SelectedItem.ReferenceType = (int?)_common.cmbRef1.EditValue;
            SelectedItem.ReferenceType2 = (int?)_common.cmbRef2.EditValue;
            SelectedItem.ReferenceType3 = (int?)_common.cmbRef3.EditValue;
            //if (_common.cmbRef1.EditValue == null)
            //    SelectedItem.ReferenceType = null;
            //else
            //    SelectedItem.ReferenceType = (int) _common.cmbRef1.EditValue;

            //if (_common.cmbRef2.EditValue == null)
            //    SelectedItem.ReferenceType2 = null;
            //else
            //    SelectedItem.ReferenceType2 = (int)_common.cmbRef2.EditValue;

            //if (_common.cmbRef3.EditValue == null)
            //    SelectedItem.ReferenceType3 = null;
            //else
            //    SelectedItem.ReferenceType3 = (int)_common.cmbRef3.EditValue;


            SaveStateData();

            InternalSave();
        }

        ControlFactColumn _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlFactColumn
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  editNumber = {Value = SelectedItem.OrderNo},
                                  Workarea = SelectedItem.Workarea
                              };
                
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbRef1, "DEFAULT_LOOKUP_NAME");
                _common.cmbRef1.Properties.DataSource = SelectedItem.Workarea.CollectionEntities;
                _common.cmbRef1.Properties.DisplayMember = "Name";
                _common.cmbRef1.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbRef1.EditValue = SelectedItem.ReferenceType;

                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbRef2, "DEFAULT_LOOKUP_NAME");
                _common.cmbRef2.BindingContext = new BindingContext();
                _common.cmbRef2.Properties.DataSource = SelectedItem.Workarea.CollectionEntities;
                _common.cmbRef2.Properties.DisplayMember = "Name";
                _common.cmbRef2.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbRef2.EditValue = SelectedItem.ReferenceType2;


                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbRef3, "DEFAULT_LOOKUP_NAME");
                _common.cmbRef3.BindingContext = new BindingContext();
                _common.cmbRef3.Properties.DataSource = SelectedItem.Workarea.CollectionEntities;
                _common.cmbRef3.Properties.DisplayMember = "Name";
                _common.cmbRef3.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbRef3.EditValue = SelectedItem.ReferenceType3;
                _common.cmbRef1.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbRef1.EditValue = null;
                };
                _common.cmbRef2.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbRef2.EditValue = null;
                };
                _common.cmbRef3.KeyDown += delegate(object sender, KeyEventArgs e)
                                               {
                                                   if (e.KeyCode == Keys.Delete)
                                                       _common.cmbRef3.EditValue = null;
                                               };



                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }

        
    }
}