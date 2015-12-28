using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using BusinessObjects.Workflows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.DocumentLibrary
{
    /// <summary>
    /// Абстракный класс внешненго представления документа в разделе "Маршруты"
    /// </summary>
    public abstract class BaseDocumentViewRoute<T> : BaseDocumentView<T> where T : class, IDocument, new()
    {
        /// <summary>
        /// Коллекция участников маршрута
        /// </summary>
        protected List<RouteMember> CollectionRouteMember;
        /// <summary>
        /// Коллекция устройств GPS
        /// </summary>
        protected List<Device> CollectionDevice;
        /// <summary>
        /// Источники для связывания участника маршрута
        /// </summary>
        public BindingSource BindSourceRouteMember;
        /// <summary>
        /// Источники для связывания устройств
        /// </summary>
        public BindingSource BindSourceDevice;
        /// <summary>
        /// Коллекция аналитики "Статус"
        /// </summary>
        protected List<Analitic> CollectionStatus;
        /// <summary>
        /// Источники для связывания статуса
        /// </summary>
        public BindingSource BindSourceStatus;

        /// <summary>
        /// Коллекция адрессов
        /// </summary>
        protected List<AgentAddress> CollectionAddress;
        /// <summary>
        /// Источники для связывания адреса
        /// </summary>
        public BindingSource BindSourceAddress;
        
        /// <summary>
        /// Элемент отображения главной страницы
        /// </summary>
        internal ControlRouteDocument ControlMain { get; set; }
        internal override ControlMainDocument ControlMainDocument { get { return ControlMain; } }

        // Установка минимальных размеров формы документа
        protected override void SetMinsize()
        {
            base.SetMinsize();
            int maxWith = (Form.clientPanel.Width.CompareTo(ControlMain.MinimumSize.Width) > 0) ? Form.clientPanel.Width : ControlMain.MinimumSize.Width;
            int maxHeight = (Form.clientPanel.Height.CompareTo(ControlMain.MinimumSize.Height) > 0) ? Form.clientPanel.Height : ControlMain.MinimumSize.Height;
            Size mix = (Form.Size - Form.clientPanel.Size) + new Size(maxWith, maxHeight);
            Form.MinimumSize = mix;
        }

        public override void InvokeRowDelete()
        {
            try
            {
                int index = ControlMain.GridView.FocusedRowHandle;
                IDocumentDetail docRow = ControlMain.GridView.GetRow(index) as IDocumentDetail;
                if (docRow != null)
                {
                    if (docRow.Id == 0)
                        ControlMain.GridView.DeleteRow(index);
                    else
                    {
                        docRow.StateId = State.STATEDELETED;
                        ControlMain.GridView.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void InvokeProductInfo()
        {
            try
            {
                int index = ControlMain.GridView.FocusedRowHandle;
                DocumentDetailSale docRow = ControlMain.GridView.GetRow(index) as DocumentDetailSale;
                if (docRow != null)
                {
                    if (docRow.ProductId != 0)
                            docRow.Product.ShowProperty();
                        //InvokeWorkflowAction(WfCore.WFA_ActivityShowProductInfo, docRow.Product);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void OnOpenNotAllowEdit()
        {
            ControlMain.layoutControlGroupMain.Enabled = false;
            //Form.Ribbon.Enabled = false;
            foreach (RibbonPage page in Form.Ribbon.Pages)
            {
                foreach (RibbonPageGroup group in page.Groups)
                {
                    foreach (BarItemLink itemLink in group.ItemLinks)
                    {
                        itemLink.Item.Enabled = false;
                    }
                }
            }
            ControlMain.navBarGroupActions.Visible = false;
            Form.Ribbon.SelectedPageChanging += new RibbonPageChangingEventHandler(SetNoEditSelectedPageChanging);
            Form.btnClose.Enabled = true;
            Form.Ribbon.Refresh();
        }

        void SetNoEditSelectedPageChanging(object sender, RibbonPageChangingEventArgs e)
        {
            foreach (RibbonPageGroup group in e.Page.Groups)
            {
                foreach (BarItemLink itemLink in group.ItemLinks)
                {
                    itemLink.Item.Enabled = false;
                }
            }
            Form.btnClose.Enabled = true;
            Form.clientPanel.Enabled = false;
            Form.Ribbon.Refresh();
        }
        protected override void OnSetReadOnly()
        {
            base.OnSetReadOnly();
            int currentValue = SourceDocument.FlagsValue;
            if ((currentValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
            {
                SourceDocument.FlagsValue = (currentValue + FlagValue.FLAGREADONLY);
                InvokeSave();
                SetViewStateReadonly();
            }
            else
            {
                if (Workarea.Access.RightCommon.Admin)
                {
                    SourceDocument.FlagsValue = (currentValue - FlagValue.FLAGREADONLY);
                    InvokeSave();
                    ButtonSetReadOnly.Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_READONLY, 1049);
                    SetViewStateReadWrite();
                    List<Product> collProducts = Workarea.GetCollection<Product>();
                    ControlMain.editNom.DataSource = collProducts;
                    ControlMain.editName.DataSource = collProducts;
                }
                else
                    XtraMessageBox.Show("Только администратор может разрешить изменения в документе!",
                                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RefreshButtontAndUi();
        }
        protected virtual void RefreshEditEnabled()
        {
            if (SourceDocument.Document.AgentFromId == SourceDocument.Document.AgentDepartmentFromId)
                ControlMain.cmbAgentDepatmentFrom.Enabled = false;
            if (SourceDocument.Document.AgentToId == SourceDocument.Document.AgentDepartmentToId)
                ControlMain.cmbAgentDepatmentTo.Enabled = false;
        }
        // Обновление кнопок и внешнего вида документа
        protected virtual void RefreshButtontAndUi(bool end = true)
        {
            if (!AllowEdit)
            {
                OnOpenNotAllowEdit();
                return;
            }
            if ((SourceDocument.Document.FlagsValue & FlagValue.FLAGREADONLY) == FlagValue.FLAGREADONLY)
            {
                ButtonSetReadOnly.Enabled = (Workarea.Access.RightCommon.Admin || Workarea.Access.RightCommon.AdminEnterprize);
                ButtonSetReadOnly.Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_NOTREADONLY, 1049);
                ButtonSetStateDone.Enabled = false;
                ButtonSetStateNotDone.Enabled = false;

                if (ButtonNewProduct != null) ButtonNewProduct.Enabled = false;
                if (ButtonDelete != null) ButtonDelete.Enabled = false;
                Form.btnSave.Enabled = false;
                Form.btnSaveClose.Enabled = false;
                ControlMain.GridView.OptionsBehavior.Editable = false;
                ControlMain.GridView.OptionsView.NewItemRowPosition =
                    DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                ControlMain.layoutControlGroupMain.Enabled = false;
            }
            else
            {
                ControlMain.Enabled = true;
                ControlMain.GridView.OptionsBehavior.Editable = true;
                ControlMain.GridView.OptionsView.NewItemRowPosition =
                    DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                Form.btnSave.Enabled = true;
                Form.btnSaveClose.Enabled = true;
                if (ButtonNewProduct != null) ButtonNewProduct.Enabled = true;
                if (ButtonDelete != null) ButtonDelete.Enabled = true;

                ControlMain.layoutControlGroupMain.Enabled = true;

                if (SourceDocument.Document.StateId == State.STATEACTIVE)
                {
                    ButtonSetStateNotDone.Enabled = true;
                    ButtonSetStateDone.Enabled = false;
                    ButtonSetReadOnly.Enabled = true;
                }
                else
                {
                    ButtonSetStateNotDone.Enabled = false;
                    ButtonSetStateDone.Enabled = true;
                    ButtonSetReadOnly.Enabled = false;
                }
            }
            RefreshEditEnabled();
            if (end)
                Form.Ribbon.Refresh();
        }

        protected virtual void CreateFillCustomActions()
        {

        }
        // Заполнение панели действий
        protected virtual void CreateActionLinks()
        {
            List<DocChain> coll = DocChain.Get(Workarea, SourceDocument.Document.TemplateId);
            foreach (DocChain document in coll)
            {
                DevExpress.XtraNavBar.NavBarItem navBarItem = new DevExpress.XtraNavBar.NavBarItem
                                                                  {
                                                                      Tag = document,
                                                                      Caption = document.Name,
                                                                      SmallImage = ExtentionsImage.GetImageDocument(Workarea, State.STATEACTIVE)
                                                                  };

                ControlMain.navBarControl.Items.Add(navBarItem);
                ControlMain.navBarGroupActions.ItemLinks.Add(new DevExpress.XtraNavBar.NavBarItemLink(navBarItem));
                navBarItem.LinkClicked += NavBarItemLinkClicked;
            }
        }
        void NavBarItemLinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (e.Link.Item.Tag == null) return;
            DocChain v = (DocChain)e.Link.Item.Tag;
            CreateDocuments(v, this);
        }
        /// <summary>
        /// Проверка бизнес правил
        /// </summary>
        protected bool IsValidRuleSet()
        {
            if (!SourceDocument.ValidateRuleSet())
            {
                SourceDocument.ShowDialogValidationErrors();
                return false;
            }
            return true;
        }
    }
}