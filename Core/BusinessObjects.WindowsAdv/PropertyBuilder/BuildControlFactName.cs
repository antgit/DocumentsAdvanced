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
    internal sealed class BuildControlFactName : BasePropertyControlIBase<FactName>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlFactName()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACTCOLUMN, ExtentionString.CONTROL_FACTCOLUMN);
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
            if (SelectedItem.IsNew)
            {
                SelectedItem.ToEntityId = System.Convert.ToInt16(_common.cmbToEntityId.EditValue);
            }
            SaveStateData();

            InternalSave();
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_FACTCOLUMN)
                BuildPageFactColumn();
        }

        private ControlList _controlContactValues;
        private void BuildPageFactColumn()
        {
            if (_controlContactValues == null)
            {
                List<FactColumn> list = SelectedItem.GetCollectionFactColumns();
                ListBrowserBaseObjects<FactColumn> browserBaseObjects = new ListBrowserBaseObjects<FactColumn>(SelectedItem.Workarea, list, null, null, true, false, false, true);
                browserBaseObjects.Build();
                browserBaseObjects.ListControl.Name = ExtentionString.CONTROL_FACTCOLUMN;
                _controlContactValues = browserBaseObjects.ListControl;

                browserBaseObjects.CreateNew += delegate(FactColumn obj, FactColumn objTemplate)
                {
                    obj.FactName = SelectedItem;
                };
                browserBaseObjects.ShowProperty += delegate(FactColumn obj)
                {
                    Form frmProperties = obj.ShowProperty();
                    frmProperties.FormClosed += delegate
                    {
                        if(obj.IsNew) return;
                        if (browserBaseObjects.BindingSource.Contains(obj)) return;
                        int position = browserBaseObjects.BindingSource.Add(obj);
                        browserBaseObjects.BindingSource.Position = position;
                    };
                };
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_FACTCOLUMN)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

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


                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                btnProp.ItemClick += delegate
                {
                    browserBaseObjects.InvokeProperties();
                };

                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                btnDelete.ItemClick += delegate
                {
                    browserBaseObjects.InvokeDelete();
                };
                page.Groups.Add(groupLinksAction);

                Control.Controls.Add(_controlContactValues);
                _controlContactValues.Dock = DockStyle.Fill;
            }
            HidePageControls(ExtentionString.CONTROL_FACTCOLUMN);
        }

        ControlFactName _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlFactName
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = { Text = SelectedItem.Code },
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  Workarea = SelectedItem.Workarea
                              };
                
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbToEntityId, "DEFAULT_LOOKUP_NAME");
                _common.cmbToEntityId.Properties.DataSource = SelectedItem.Workarea.CollectionEntities;
                _common.cmbToEntityId.Properties.DisplayMember = "Name";
                _common.cmbToEntityId.Properties.ValueMember = GlobalPropertyNames.Id;

                if(!SelectedItem.IsNew)
                {
                    _common.cmbToEntityId.Properties.ReadOnly = true;
                    _common.cmbToEntityId.EditValue = SelectedItem.ToEntityId;
                }

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }
    }
}