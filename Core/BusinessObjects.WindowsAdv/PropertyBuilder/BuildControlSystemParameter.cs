using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlSystemParameter : BasePropertyControlIBase<SystemParameter>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlSystemParameter()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_USERS_PARAMETERS, ExtentionString.CONTROL_USERS_PARAMETERS);
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
            SelectedItem.ValueString = _common.txtString.Text;
            if (_common.numInt.EditValue == null)
                SelectedItem.ValueInt = null;
            else
                SelectedItem.ValueInt = (int)_common.numInt.Value;
            if (_common.numFloat.EditValue == null)
                SelectedItem.ValueFloat = null;
            else
                SelectedItem.ValueFloat = (float)_common.numFloat.Value;

            if (_common.numMoney.EditValue == null)
                SelectedItem.ValueMoney = null;
            else
                SelectedItem.ValueMoney = _common.numMoney.Value;
            if (_common.txtGuid.EditValue == null || string.IsNullOrEmpty(_common.txtGuid.Text))
                SelectedItem.ValueGuid = null;
            else
            {
                SelectedItem.ValueGuid = new Guid(_common.txtGuid.Text);
            }
            SelectedItem.EntityReferenceId = (int)_common.cmbReferenceKind.EditValue;
            SelectedItem.ReferenceId = (int) _common.cmbReference.EditValue;

            SaveStateData();

            InternalSave();
        }

        ControlSystemParameter _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlSystemParameter
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtString = {Text = SelectedItem.ValueString},
                                  numInt = {EditValue = SelectedItem.ValueInt},
                                  numFloat = {EditValue = SelectedItem.ValueFloat},
                                  numMoney = {EditValue = SelectedItem.ValueMoney},
                                  txtGuid =
                                      {
                                          Text =
                                              SelectedItem.ValueGuid.HasValue
                                                  ? SelectedItem.ValueGuid.Value.ToString()
                                                  : string.Empty
                                      },
                                  Workarea = SelectedItem.Workarea
                              };
               
                BindingSource bindingSourceReferenceKind = new BindingSource
                                                               {
                                                                   DataSource = SelectedItem.Workarea.CollectionEntities
                                                               };

                _common.cmbReferenceKind.Properties.DataSource = bindingSourceReferenceKind;
                _common.cmbReferenceKind.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbReferenceKind.Properties.ValueMember = GlobalPropertyNames.Id;
                _common.cmbReferenceKind.EditValue = SelectedItem.EntityReferenceId;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbReferenceKind,
                                                         "DEFAULT_LOOKUPENTITYTYPE");
                _common.cmbReference.Properties.DisplayMember = GlobalPropertyNames.Name;
                _common.cmbReference.Properties.ValueMember = GlobalPropertyNames.Id;
                
                InitReferenceData(SelectedItem.EntityReferenceId);
                _common.cmbReference.EditValue = SelectedItem.ReferenceId;
                _common.cmbReference.EditValueChanged += delegate
                                                             {
                                                                 InitReferenceData((int) _common.cmbReference.EditValue);
                                                             };

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

        ControlList _usersParameters;
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_USERS_PARAMETERS)
            {
                if (_usersParameters == null)
                {
                    BindingSource bindingProperties = new BindingSource();
                    _usersParameters = new ControlList {Name = ExtentionString.CONTROL_USERS_PARAMETERS};

                    DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _usersParameters.View, 
                                                            "DEFAULT_LISTVIEWSYSTEMPARAMETER");
                    _usersParameters.Grid.DataSource = bindingProperties;
                    _usersParameters.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                    {
                        if (e.Column.Name == "colImage")
                        {
                            System.Drawing.Rectangle r = e.Bounds;
                            System.Drawing.Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.AGENTWORKER_X16);
                            e.Graphics.DrawImageUnscaled(img, r);
                            e.Handled = true;
                        }
                        if (e.Column.Name == "colStateImage")
                        {
                            if (bindingProperties.Count - 1 >= _usersParameters.View.GetDataSourceRowIndex(e.RowHandle))
                            {
                                System.Drawing.Rectangle r = e.Bounds;
                                int index = _usersParameters.View.GetDataSourceRowIndex(e.RowHandle);
                                SystemParameterUser v = (SystemParameterUser)bindingProperties[index];
                                System.Drawing.Image img = v.State.GetImage();
                                e.Graphics.DrawImageUnscaled(img, r);
                                e.Handled = true;
                            }
                        }
                    };

                    RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_USERS_PARAMETERS)];
                    RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                    #region Добавить
                    BarButtonItem btnCreate = new BarButtonItem
                                                  {
                                                      ButtonStyle = BarButtonStyle.Default,
                                                      Caption =
                                                          SelectedItem.Workarea.Cashe.ResourceString(
                                                              ResourceString.BTN_CAPTON_ADD, 1049),
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = Properties.Resources.CREATE_X32
                                                  };
                    groupLinksAction.ItemLinks.Add(btnCreate);
                    btnCreate.ItemClick += delegate
                    {
                        SystemParameterUser spu = new SystemParameterUser(SelectedItem)
                                                      {Workarea = SelectedItem.Workarea};
                        Form propForm = spu.BrowseProperties();
                        propForm.FormClosed += delegate
                        {
                            bindingProperties.DataSource = SelectedItem.GetUserParams();
                        };
                    };
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
                        if (_usersParameters.View.SelectedRowsCount > 0)
                        {
                            SystemParameterUser spu = (SystemParameterUser)bindingProperties[_usersParameters.View.GetSelectedRows()[0]];
                            Form propForm = spu.BrowseProperties();
                            propForm.FormClosed += delegate
                            {
                                bindingProperties.DataSource = SelectedItem.GetUserParams();
                            };
                        }
                    };

                    #endregion

                    #region Удаление
                    BarButtonItem btnDelete = new BarButtonItem
                                                  {
                                                      Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                                  };
                    groupLinksAction.ItemLinks.Add(btnDelete);
                    btnDelete.ItemClick += delegate
                    {
                        if (_usersParameters.View.SelectedRowsCount > 0)
                        {
                            if (MessageBox.Show("Вы уверенны, что хотите удалить этот параметр?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SystemParameterUser spu = (SystemParameterUser)bindingProperties[_usersParameters.View.GetSelectedRows()[0]];
                                spu.Delete();
                                bindingProperties.DataSource = SelectedItem.GetUserParams();
                            }
                        }   
                    };
                    #endregion

                    page.Groups.Add(groupLinksAction);
                    _usersParameters.Grid.DoubleClick += delegate
                    {
                        if (_usersParameters.View.SelectedRowsCount > 0)
                        {
                            SystemParameterUser spu = (SystemParameterUser)bindingProperties[_usersParameters.View.GetSelectedRows()[0]];
                            Form propForm = spu.BrowseProperties();
                            propForm.FormClosed += delegate
                            {
                                bindingProperties.DataSource = SelectedItem.GetUserParams();
                            };
                        }
                    };

                    Control.Controls.Add(_usersParameters);
                    _usersParameters.Dock = DockStyle.Fill;
                    bindingProperties.DataSource = SelectedItem.GetUserParams();
                }
                HidePageControls(ExtentionString.CONTROL_USERS_PARAMETERS);
            }
        }

        private void InitReferenceData(int referenceKind)
        {
            BindingSource bindingSourceReference = new BindingSource();
            if(referenceKind==0)
            {
                _common.cmbReference.Properties.DataSource = bindingSourceReference;
            }
            if(referenceKind==21)
            {
                _common.cmbReference.Properties.Columns.Clear();
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbReference,
                                                         "DEFAULT_LOOKUPCUSTOMVIEWLIST");
                bindingSourceReference.DataSource = SelectedItem.Workarea.GetCollection<CustomViewList>();
                _common.cmbReference.Properties.DataSource = bindingSourceReference;
            }
        }
    }
}