using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.DocumentLibrary
{
    /// <summary>
    /// Базовый класс кадровых документов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDocumentViewPerson<T> : BaseDocumentView<T> where T : class, IDocument, new()
    {
        /// <summary>
        /// Коллекция "Отдел"
        /// </summary>
        protected List<Depatment> CollectionDepatmentTo;
        /// <summary>
        /// Источники для связывания "Отдел"
        /// </summary>
        public BindingSource BindSourceDepatmentTo;
        /// <summary>
        /// Коллекция "Отдел"
        /// </summary>
        protected List<Depatment> CollectionDepatmentFrom;
        /// <summary>
        /// Источники для связывания "Отдел"
        /// </summary>
        public BindingSource BindSourceDepatmentFrom;
        /// <summary>
        /// Коллекция корреспондентов "Сотрудник"
        /// </summary>
        protected List<Agent> CollectionEmployer;

        /// <summary>
        /// Коллекция аналитики "Должность"
        /// </summary>
        protected List<Analitic> CollectionWorkPost;
        /// <summary>
        /// Источники для связывания аналитики "Должность"
        /// </summary>
        public BindingSource BindSourceWorkPost;

        /// <summary>
        /// Источники для связывания аналитики "Тип платежа"
        /// </summary>
        public BindingSource BindSourceEmployer;
        /// <summary>
        /// Коллекция расчетных счетов коррекпондента "Кто"
        /// </summary>
        protected List<AgentBankAccount> CollectionBankAccountFrom;
        /// <summary>
        /// Коллекция расчетных счетов коррекпондента "Кому"
        /// </summary>
        protected List<AgentBankAccount> CollectionBankAccountTo;
        /// <summary>
        /// Источники для связывания расчетного счета коррекпондента "Кто"
        /// </summary>
        public BindingSource BindSourceBankAccountFrom;
        /// <summary>
        /// Источники для связывания расчетного счета коррекпондента "Кому"
        /// </summary>
        public BindingSource BindSourceBankAccountTo;

        internal ControlPerson ControlMain;
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
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                IDocumentDetail docRow = ControlMain.ViewDetail.GetRow(index) as IDocumentDetail;
                if (docRow != null)
                {
                    if (docRow.Id == 0)
                        ControlMain.ViewDetail.DeleteRow(index);
                    else
                    {
                        docRow.StateId = State.STATEDELETED;
                        ControlMain.ViewDetail.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Информация о товаре
        public override void InvokeProductInfo()
        {
            try
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                DocumentDetailFinance docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance;
                if (docRow != null)
                {
                    if (docRow.ProductId != 0)
                        docRow.Product.ShowProperty();
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
            int currentValue = SourceDocument.Document.FlagsValue;
            if ((currentValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
            {
                SourceDocument.Document.FlagsValue = (currentValue + FlagValue.FLAGREADONLY);
                InvokeSave();
                SetViewStateReadonly();
            }
            else
            {
                if (Workarea.Access.RightCommon.Admin)
                {
                    SourceDocument.Document.FlagsValue = (currentValue - FlagValue.FLAGREADONLY);
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
        protected void RefreshEditEnabled()
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
                ButtonSetReadOnly.Enabled = (Workarea.Access.RightCommon.Admin ||
                                             Workarea.Access.RightCommon.AdminEnterprize);
                ButtonSetReadOnly.Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_NOTREADONLY, 1049);
                ButtonSetStateDone.Enabled = false;
                ButtonSetStateNotDone.Enabled = false;

                if (ButtonNewProduct != null) ButtonNewProduct.Enabled = false;
                if (ButtonDelete != null) ButtonDelete.Enabled = false;
                Form.btnSave.Enabled = false;
                Form.btnSaveClose.Enabled = false;
                ControlMain.ViewDetail.OptionsBehavior.Editable = false;
                ControlMain.ViewDetail.OptionsView.NewItemRowPosition =
                    DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                ControlMain.dtDate.Properties.ReadOnly = true;
                ControlMain.txtMemo.Properties.ReadOnly = true;
                ControlMain.txtName.Properties.ReadOnly = true;
                ControlMain.txtNumber.Properties.ReadOnly = true;
                ControlMain.cmbAgentFrom.Enabled = false;
                ControlMain.cmbAgentTo.Enabled = false;
                ControlMain.cmbAgentDepatmentFrom.Enabled = false;
                ControlMain.cmbAgentDepatmentTo.Enabled = false;
            }
            else
            {
                ControlMain.Enabled = true;
                ControlMain.ViewDetail.OptionsBehavior.Editable = true;
                ControlMain.ViewDetail.OptionsView.NewItemRowPosition =
                    DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                Form.btnSave.Enabled = true;
                Form.btnSaveClose.Enabled = true;
                if (ButtonNewProduct != null) ButtonNewProduct.Enabled = true;
                if (ButtonDelete != null) ButtonDelete.Enabled = true;

                ControlMain.dtDate.Properties.ReadOnly = false;
                ControlMain.txtMemo.Properties.ReadOnly = false;
                ControlMain.txtName.Properties.ReadOnly = false;
                ControlMain.txtNumber.Properties.ReadOnly = false;
                ControlMain.cmbAgentFrom.Enabled = true;
                ControlMain.cmbAgentTo.Enabled = true;
                ControlMain.cmbAgentDepatmentFrom.Enabled = true;
                ControlMain.cmbAgentDepatmentTo.Enabled = true;

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
        //protected void PrepareChainsDocumentGrid()
        //{
        //    DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.GridViewChainDocs, "DEFAULT_LISTVIEWDOCUMENTSHORT");
        //    DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.GridViewReports, "DEFAULT_LOOKUP_NAME");
        //    BindingDocumentChains = new BindingSource
        //    {
        //        DataSource = Document.GetChainSourceList(Workarea, SourceDocument.Id, 20).Where(f => !f.IsStateDeleted).ToList()
        //    };
        //    ControlMain.GridChainDocs.DataSource = BindingDocumentChains;
        //    ControlMain.GridViewChainDocs.RowClick += GridViewChainDocsRowClick;
        //}

        //void GridViewChainDocsRowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    if (e.Clicks < 2) return;

        //    InvokeShowDocument(BindingDocumentChains);
        //}

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
            CreateDocuments(v);
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