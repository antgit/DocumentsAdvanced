using System;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    internal sealed class BuildControlEntityDocument : BasePropertyControlIBase<EntityDocument>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildControlEntityDocument()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_DBENTITYKIND_NAME, ExtentionString.CONTROL_DBENTITYKIND_NAME);
            TotalPages.Add(ExtentionString.CONTROL_DBENTITYMETHOD_NAME, ExtentionString.CONTROL_DBENTITYMETHOD_NAME);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);
        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_DBENTITYKIND_NAME)
                BuildPageDbEntityKind();
            if (value == ExtentionString.CONTROL_DBENTITYMETHOD_NAME)
                BuildPageEntityMethod();
        }


        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            //SelectedItem.MaxKind = (short)_common.numMaxKind.Value;
            SelectedItem.Id = (Int32)_common.numId.Value;
            SelectedItem.CodeClass = _common.txtCodeClass.Text;
            SelectedItem.NameSchema = _common.cmbShemaName.Text;
            SaveStateData();

            InternalSave();
        }

        ControlEntity _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlEntity
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtCode = {Text = SelectedItem.Code},
                                  txtMemo = {Text = SelectedItem.Memo},
                                  numId = {Value = SelectedItem.Id},
                                  Workarea = SelectedItem.Workarea
                              };

                _common.txtCodeClass.Text = SelectedItem.CodeClass;
                _common.cmbShemaName.Text = SelectedItem.NameSchema;
                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
        }


        private ListBrowserCore<ProcedureMap> _browserProcedureMap;
        private void BuildPageEntityMethod()
        {
            if (_browserProcedureMap == null)
            {
                _browserProcedureMap = new ListBrowserCore<ProcedureMap>(SelectedItem.Workarea, SelectedItem.Methods, null, null, true, false, true, true);
                _browserProcedureMap.Build();
                _browserProcedureMap.ListControl.Name = ExtentionString.CONTROL_DBENTITYMETHOD_NAME;
                _browserProcedureMap.GridView.CustomUnboundColumnData += EntityMethodCustomUnboundColumnData;
                _browserProcedureMap.ShowProperty += browserProcedureMap_ShowProperty;
                _browserProcedureMap.Delete += browserProcedureMap_Delete;
                Control.Controls.Add(_browserProcedureMap.ListControl);
                _browserProcedureMap.ListControl.Dock = DockStyle.Fill;

                // Построение группы управления
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_DBENTITYMETHOD_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();
                #region Создание
                BarButtonItem btnCreate = new BarButtonItem
                                              {
                                                  ButtonStyle = BarButtonStyle.Default,
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnCreate);

                btnCreate.ItemClick += delegate
                                           {
                                               ProcedureMap newObject = new ProcedureMap
                                                                            {
                                                                                TypeId = 1,
                                                                                Workarea = SelectedItem.Workarea,
                                                                                EntityId = ((Int16)SelectedItem.Id)
                                                                            };
                                               Form frmProperties = newObject.ShowProperty();
                                               frmProperties.FormClosed += delegate
                                                                               {
                                                                                   if (!newObject.IsNew)
                                                                                   {
                                                                                       int position = _browserProcedureMap.BindingSource.Add(newObject);
                                                                                       _browserProcedureMap.BindingSource.Position = position;
                                                                                   }
                                                                               };
                                           };
                #endregion

                #region Свойства
                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                btnProp.ItemClick += delegate
                                         {
                                             _browserProcedureMap.InvokeProperties();
                                         };
                #endregion

                #region Удаление
                BarButtonItem btnChainDelete = new BarButtonItem
                                                   {
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainDelete);
                btnChainDelete.ItemClick += delegate
                                                {
                                                    _browserProcedureMap.InvokeDelete();
                                                };
                #endregion

                page.Groups.Add(groupLinksAction);
            }
            HidePageControls(ExtentionString.CONTROL_DBENTITYMETHOD_NAME);
        }

        private void browserProcedureMap_Delete(ProcedureMap obj)
        {
            obj.Delete();
        }

        private void browserProcedureMap_ShowProperty(ProcedureMap obj)
        {
            obj.ShowProperty();
        }

        private void EntityMethodCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                e.Value = ((ProcedureMap)_browserProcedureMap.BindingSource[e.ListSourceRowIndex]).GetImage();
            }
        }

        private ListBrowserCore<EntityDocumentKind> _browserEntityKind;
        private void BuildPageDbEntityKind()
        {
            if (_browserEntityKind == null)
            {
                _browserEntityKind = new ListBrowserCore<EntityDocumentKind>(SelectedItem.Workarea, SelectedItem.Kinds, null, null, true, false, true, true);
                _browserEntityKind.Build();
                _browserEntityKind.ListControl.Name = ExtentionString.CONTROL_DBENTITYKIND_NAME;
                _browserEntityKind.GridView.CustomUnboundColumnData += EntityKindCustomUnboundColumnData;
                _browserEntityKind.ShowProperty += browserEntityKind_ShowProperty;
                _browserEntityKind.Delete += browserEntityKind_Delete;
                Control.Controls.Add(_browserEntityKind.ListControl);
                _browserEntityKind.ListControl.Dock = DockStyle.Fill;

                // Построение группы управления
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_DBENTITYKIND_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();
                #region Создание
                BarButtonItem btnCreate = new BarButtonItem
                                              {
                                                  ButtonStyle = BarButtonStyle.Default,
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnCreate);

                btnCreate.ItemClick += delegate
                                           {
                                               EntityDocumentKind newObject = new EntityDocumentKind
                                                                          {
                                                                              Workarea = SelectedItem.Workarea,
                                                                              EntityId = ((Int16)SelectedItem.Id)
                                                                          };
                                               Form frmProperties = newObject.ShowProperty();
                                               frmProperties.FormClosed += delegate
                                                                               {
                                                                                   if (!newObject.IsNew)
                                                                                   {
                                                                                       int position = _browserEntityKind.BindingSource.Add(newObject);
                                                                                       _browserEntityKind.BindingSource.Position = position;
                                                                                   }
                                                                               };
                                           };
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
                                             _browserEntityKind.InvokeProperties();
                                         };
                #endregion

                #region Удаление
                BarButtonItem btnChainDelete = new BarButtonItem
                                                   {
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainDelete);
                btnChainDelete.ItemClick += delegate
                                                {
                                                    _browserEntityKind.InvokeDelete();
                                                };
                #endregion

                page.Groups.Add(groupLinksAction);
            }
            HidePageControls(ExtentionString.CONTROL_DBENTITYKIND_NAME);
        }

        void browserEntityKind_Delete(EntityDocumentKind obj)
        {
            obj.Delete();
        }

        void browserEntityKind_ShowProperty(EntityDocumentKind obj)
        {
            obj.ShowProperty();
        }

        void EntityKindCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                e.Value = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DOCUMENTDONE_X16); 
            }
        }
    }
}