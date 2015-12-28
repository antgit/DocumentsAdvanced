using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using BusinessObjects.Windows.Controls;
using BusinessObjects.Security;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid;
using Stimulsoft.Base.Drawing;

namespace BusinessObjects.DocumentLibrary
{
    /// <summary>
    /// ���������� ����� ��������� ������������� ���������
    /// </summary>
    public abstract class BaseDocumentView<T> : IDocumentView where T : class, IDocument, new() //IDataErrorInfo,
    {
        protected BaseDocumentView()
        {
            TotalPages = new HashSet<string>
                             {
                                 ExtentionString.CONTROL_COMMON_NAME,
                                 ExtentionString.CONTROL_LINK_NAME,
                                 ExtentionString.CONTROL_LINKFILES,
                                 ExtentionString.CONTROL_LOGACTION,
                                 ExtentionString.CONTROL_ID_NAME,
                                 ExtentionString.CONTROL_SETUP
                             };

            ActivePage = ExtentionString.CONTROL_COMMON_NAME;
            CanClose = true;
        }

        public bool AllowEdit
        {
            get { return Workarea.Access.IsDocumentAllowEdit(SourceDocument.Document.MyCompanyId); } 
        }
        protected virtual void OnOpenNotAllowEdit()
        {
            
        }
        /// <summary>
        /// ������������� (�����) ������������� ���������
        /// </summary>
        public IDocumentView SourceView { get; set; }
        /// <summary>
        /// ������ � �������� ����������� ������� ����������� ���������
        /// </summary>
        public object OwnerObject { get; set; }
        /// <summary>�������������</summary>
        protected Autonum Autonum;

        private Workarea _workarea;
        /// <summary>����� ����������� ���������</summary>
        internal FormProperties Form { get; set; }
        /// <summary>����� ����������� ���������</summary>
        public object GetForm()
        {
            return Form;
        }
        public object GetMainControl()
        {
            return ControlMainDocument;
        }
        /// <summary>������������� ���������</summary>
        public int Id { get; set; }
        /// <summary>
        /// ������������� ������������� ��������� � ������� ���������� ������� ����� ����� ���������� ���������
        /// </summary>
        internal int ParentId { get; set; }
        /// <summary>������������� ������� ���������</summary>
        public int DocumentTemplateId { get; set; }
        /// <summary>������� �������</summary>
        public Workarea Workarea
        {
            get { return _workarea; }
            set
            {
                _workarea = value;
                if (!DocumentViewConfig.IsInit)
                    DocumentViewConfig.Init(_workarea);
            }
        }

        internal abstract ControlMainDocument ControlMainDocument { get; }
        
        #region ������ ��������
        protected void PrepareChainsDocumentGrid()
        {
            //DataGridViewHelper.GenerateGridColumns(Workarea, ControlMainDocument.GridViewChainDocs, "DEFAULT_LISTVIEWDOCUMENTSHORT");
            DataGridViewHelper.GenerateGridColumns(Workarea, ControlMainDocument.GridViewReports, "DEFAULT_DOCSREPORTS");
            BindingDocumentChains = new BindingSource
            {
                DataSource = Document.GetChainSourceList(Workarea, SourceDocument.Id, 20).Where(f => !f.IsStateDeleted).ToList()
            };
            ControlMainDocument.GridChainDocs.DataSource = BindingDocumentChains;
            ControlMainDocument.GridViewChainDocs.RowClick += GridViewChainDocsRowClick;
            ControlMainDocument.GridViewChainDocs.CustomUnboundColumnData += GridViewChainDocsCustomUnboundColumnData;

            BindingDocumentReports = new BindingSource
            {
                DataSource = Workarea.GetReports<EntityDocument,Document> (MainDocument).Where(f => f.IsStateAllow)
            };
            ControlMainDocument.GridReports.DataSource = BindingDocumentReports;
            ControlMainDocument.GridViewReports.RowClick += GridViewReportsRowClick;
            ControlMainDocument.GridViewReports.CustomUnboundColumnData += GridViewReportsCustomUnboundColumnData;

            ControlMainDocument.navBarGroupReports.CalcGroupClientHeight += NavBarGroupReportCalcGroupClientHeight;
            ControlMainDocument.navBarGroupChainDocuments.CalcGroupClientHeight += NavBarGroupChainDocumentsCalcGroupClientHeight;
            
            //����������� ����
            PopupMenu popupMenuReports = new PopupMenu();
            popupMenuReports.Ribbon = Form.ribbon;

            #region ��������
            BarButtonItem btnCreate = new BarButtonItem
            {
                Caption = "��������",
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
            };
            btnCreate.ItemClick += delegate
                                       {
                                           List<Library> coll = Workarea.Empty<Library>().BrowseReports(Workarea);
                                           if (coll == null) return;

                                           foreach (Library item in coll)
                                           {
                                               ReportChain<EntityDocument,Document> newChain = new ReportChain<EntityDocument,Document>
                                                                          {
                                                                              Workarea = Workarea,
                                                                              ToEntityId = MainDocument.KindId,
                                                                              ReportId =item.Id,
                                                                              ElementId = MainDocument.TemplateId,
                                                                              StateId = State.STATEACTIVE
                                                                          };
                                               newChain.Save();
                                           }
                                           BindingDocumentReports.DataSource = Workarea.GetReports<EntityDocument, Document>(MainDocument).Where(f => f.IsStateAllow);
                                       };
            popupMenuReports.AddItem(btnCreate);
            #endregion

            #region ���������
            BarButtonItem btnBuild = new BarButtonItem
            {
                Caption = "���������",
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REPORT_X16)
            };
            btnBuild.ItemClick += delegate
                                      {
                                          if (BindingDocumentReports.Current != null)
                                          {
                                              ReportChain<EntityDocument,Document> repChain = BindingDocumentReports.Current as ReportChain<EntityDocument,Document>;
                                              if (repChain.Library != null)
                                              {
                                                  SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>("REPORTSERVER");
                                                  if (prm != null)
                                                  {
                                                      repChain.Library.ShowReport(SourceDocument, prm.ValueString);
                                                  }
                                              }
                                          }
                                      };
            popupMenuReports.AddItem(btnBuild);
            #endregion

            #region �������
            BarButtonItem btnDelete = new BarButtonItem
            {
                Caption = "�������",
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X16)
            };
            btnDelete.ItemClick += delegate
                                       {
                                           if (BindingDocumentReports.Current == null) return;
                                           var rptChain = BindingDocumentReports.Current as ReportChain<EntityDocument,Document>;
                                           if (rptChain == null) return;
                                           rptChain.ChangeState(State.STATEDELETED);
                                           BindingDocumentReports.DataSource = Workarea.GetReports<EntityDocument, Document>(MainDocument).Where(f => f.IsStateAllow);
                                       };
            popupMenuReports.AddItem(btnDelete);
            #endregion

            ControlMainDocument.GridViewReports.ShowGridMenu += delegate(object sender, GridMenuEventArgs e)
                                                                    {
                                                                        //if (!e.HitInfo.InRowCell) return;
                                                                        //ControlMainDocument.GridViewChainDocs.FocusedRowHandle = e.HitInfo.RowHandle;
                                                                        //ControlMainDocument.GridViewChainDocs.FocusedColumn = e.HitInfo.Column;
                                                                        popupMenuReports.ShowPopup(Cursor.Position);
                                                                    };
        }

        void GridViewChainDocsCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                Document imageItem = BindingDocumentChains[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                Document imageItem = BindingDocumentChains[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }

        void GridViewReportsCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                ReportChain<EntityDocument, Document> imageItem = BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityDocument, Document>;
                if (imageItem != null && imageItem.Library!=null)
                {
                    e.Value = imageItem.Library.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                ReportChain<EntityDocument, Document> imageItem = BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityDocument, Document>;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }

        void GridViewReportsRowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks < 2) return;

            if (BindingDocumentReports.Current != null)
            {
                ReportChain<EntityDocument, Document> repChain = BindingDocumentReports.Current as ReportChain<EntityDocument, Document>;
                if (repChain!=null && repChain.Library != null)
                {
                    SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>("REPORTSERVER");
                    if (prm != null)
                    {
                        repChain.Library.ShowReport(SourceDocument, prm.ValueString);
                    }
                }
            }
        }

        void GridViewChainDocsRowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks < 2) return;

            InvokeShowDocument(BindingDocumentChains);
        }

        private void NavBarGroupReportCalcGroupClientHeight(object sender, NavBarCalcGroupClientHeightEventArgs e)
        {
            NavBarViewInfo vi = GetViewInfo(ControlMainDocument.navBarControl);
            NavGroupInfoArgs gi = vi.Groups[vi.Groups.Count - 1] as NavGroupInfoArgs;
            ExplorerBarNavGroupPainter groupPainter = GetGroupPainter(ControlMainDocument.navBarControl);
            groupPainter.CalcFooterBounds(gi, gi.Bounds);
            int delta = gi.Bounds.Top - vi.Client.Top;
            int ch = vi.Client.Height - delta - gi.Bounds.Height - gi.FooterBounds.Height;
            e.Height = ch;
        }

        private void NavBarGroupChainDocumentsCalcGroupClientHeight(object sender, NavBarCalcGroupClientHeightEventArgs e)
        {
            if (ControlMainDocument.GridViewChainDocs.RowCount > 2)
            {
                int rowHeight = (ControlMainDocument.GridChainDocs.Font.Height + 9);
                e.Height = rowHeight * 5 + 20;
            }
        }
        #endregion
        /// <summary>
        /// �������� ���������
        /// </summary>
        public List<DocumentPreviewThread<T>> OpennedDocs { get; private set; }
        /// <summary>����� �� ������� ��������</summary>
        public bool CanClose { get; set; }
        
        public void Load(IDocument value)
        {
            SourceDocument = value as T;
        }
        /// <summary>��������</summary>
        public T SourceDocument { get; set; }
        /// <summary>�������� ��������</summary>
        public virtual Document MainDocument
        {
            get { return SourceDocument.Document; }
        }

        #region ���������������� ���������
        /// <summary>
        /// ����� ������������ ������� � ������
        /// </summary>
        public HashSet<string> TotalPages { get; protected set; }
        /// <summary>�������� ��������</summary>
        public string ActivePage { get; set; }

        /// <summary>
        /// �������� ����������� �������� ������������
        /// </summary>
        /// <param name="value">��� ��������</param>
        /// <returns></returns>
        protected virtual bool IsPageAvailablePermission(string value)
        {
            // TODO: �������� ����������
            return true;
        }
        /// <summary>
        /// �������� ������� �������� �� ����
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsPageAvailable(string value)
        {
            return TotalPages.Contains(value) && IsPageAvailablePermission(value);
        }
        /// <summary>
        /// ���������� �������� ��������
        /// </summary>
        public virtual void SetActivePage(string value)
        {
            if (Form == null) return;
            if (TotalPages.Contains(value))
            {
                BuildPage(value);
            }
            RibbonPage page = GetPageByName("PAGE_" + value);
            Form.Ribbon.SelectedPage = page;
        }
        RibbonPage GetPageByName(string name)
        {
            return Form.ribbon.Pages.Cast<RibbonPage>().FirstOrDefault(s => s.Name == name);
        }
        protected void HidePageControls(string excludeValue)
        {
            foreach (Control ctrl in Form.clientPanel.Controls.Cast<Control>().Where(ctrl => ctrl.Name.StartsWith("PAGE") & ctrl.Name != excludeValue))
            {
                ctrl.Visible = false;
            }
            if (!Form.clientPanel.Controls.ContainsKey(excludeValue)) return;
            Form.clientPanel.Controls[excludeValue].Visible = true;
            Form.clientPanel.Controls[excludeValue].BringToFront();
        }
        protected virtual void BuildRibbonHeader()
        {
            if (Form == null) return;

            Form.ribbon.SelectedPageChanging += RibbonSelectedPageChanging;
            Form.FormClosed += FormFormClosed;
            foreach (RibbonPage page in from i in TotalPages
                                        where i != ExtentionString.CONTROL_COMMON_NAME
                                        select new RibbonPage { Name = "PAGE_" + i, Text = ExtentionString.GetPageNameByKey(Workarea, i), Tag = i })
            {
                    page.Groups.Add(Form.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION);
                    Form.ribbon.Pages.Add(page);
                //if (SourceDocument.Document.IsTemplate && page.Name==ExtentionString.CONTROL_SETUP)
                //{
                //    page.Groups.Add(Form.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION);
                //    Form.ribbon.Pages.Add(page);
                //}
                //else if(!SourceDocument.Document.IsTemplate && page.Name!=ExtentionString.CONTROL_SETUP)
                //{
                //    page.Groups.Add(Form.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION);
                //    Form.ribbon.Pages.Add(page);
                //}
            }
           
            
            /*
            foreach (PageSetting pageSettigs in Settings.Pages.Where(pageSettigs => page.Name == pageSettigs.Name))
            {
                Form.ribbon.Pages[page.PageIndex].Visible = pageSettigs.Visible;
            */
        }
        void RibbonSelectedPageChanging(object sender, RibbonPageChangingEventArgs e)
        {
            if (e.Page.Tag == null) return;
            ActivePage = e.Page.Tag.ToString();
            LogUserAction.CreateActionPageView(Workarea, MainDocument.Id, e.Page.Text);
            BuildPage(e.Page.Tag.ToString());
        }
        #endregion
        /// <summary>
        /// ���������� ��������(��������) � �����������
        /// </summary>
        /// <param name="value"></param>
        protected virtual void BuildPage(string value)
        {
            switch (value)
            {
                case ExtentionString.CONTROL_COMMON_NAME:
                    BuildPageCommon();
                    break;
                case ExtentionString.CONTROL_ID_NAME:
                    BuildPageId();
                    break;
                case ExtentionString.CONTROL_LINK_NAME:
                    BuildPageLinks();
                    break;
                case ExtentionString.CONTROL_LINKFILES:
                    BuildPageLinkedFiles();
                    break;
                case ExtentionString.CONTROL_LOGACTION:
                    BuildPageLogAction();
                    break;
                case ExtentionString.CONTROL_SETUP:
                    BuildPageSetup();
                    break;
            }
            if (Settings != null && !string.IsNullOrEmpty(Settings.Storage.Code))
                SetConfig(value);
        }
        public virtual void Build()
        {
            BuildRibbonHeader();
            if (IsPageAvailable(ExtentionString.CONTROL_COMMON_NAME))
            {
                BuildPage(ExtentionString.CONTROL_COMMON_NAME);
                if (ControlMainDocument!=null)
                    ControlMainDocument.HelpRequested += delegate
                    {
                        InvokeHelp();
                    };
            }
            SetMinsize();
            SetCaption();
            LogUserAction.CreateActionOpenDocument(Workarea, MainDocument.Id, null);
            SetActivePage(ActivePage);
            foreach (string pageKey in TotalPages)
            {
                PageSetting pageSetup = Settings.Pages.FirstOrDefault(f => f.Name == "PAGE_" +pageKey);
                if (pageSetup != null)
                {
                    RibbonPage page = GetPageByName("PAGE_" + pageKey);
                    Form.ribbon.Pages[page.PageIndex].Visible = pageSetup.Visible;
                }
            }
            OnBuildComplete();
        }

        /// <summary>
        /// �������������� ��������� ����� ���������� ���������� ����� ���������
        /// </summary>
        protected virtual void OnBuildComplete()
        {
            // ��������� �� ������� �������� "���������", ����  ��� �� ������ ���������
            if(SourceDocument!=null && TotalPages.Contains(ExtentionString.CONTROL_SETUP))
            {
                if(!SourceDocument.Document.IsTemplate)
                {
                    RibbonPage page = GetPageByName("PAGE_" + ExtentionString.CONTROL_SETUP);
                    Form.ribbon.Pages[page.PageIndex].Visible = false;
                }
                else
                {
                    RibbonPage page = GetPageByName("PAGE_" + ExtentionString.CONTROL_SETUP);
                    Form.ribbon.Pages[page.PageIndex].Visible = true;
                }
            }
        }
        /// <summary>
        /// ����� ���������� �� ���������/������ ��������� "������ ������"
        /// </summary>
        protected virtual void OnSetReadOnly()
        {

        }
        void FormFormClosed(object sender, FormClosedEventArgs e)
        {
            if (Settings.AutoSave)
                Settings.Save(Workarea);
        }

        #region ��������� ���������
        void SetConfig(string value)
        {
            RibbonPage page = GetPageByName("PAGE_" + value);
            foreach (PageSetting pageSettigs in Settings.Pages.Where(pageSettigs => page.Name == pageSettigs.Name))
            {
                Form.ribbon.Pages[page.PageIndex].Visible = pageSettigs.Visible;
                if (page.Visible)
                {
                    foreach (PageActionGroupSetting groupSettings in pageSettigs.Groups)
                    {
                        RibbonPageGroup group = page.GetGroupByName(groupSettings.Name);
                        group.Visible = groupSettings.Visible;
                        if (group.Visible)
                        {
                            foreach (ButtonSetting btnSettings in groupSettings.Buttons)
                            {
                                foreach (BarItemLink btn in group.ItemLinks.Cast<BarItemLink>().Where(btn => btn.Item.Name == btnSettings.Name))
                                {
                                    btn.Item.Visibility = btnSettings.Visible ? BarItemVisibility.Always : BarItemVisibility.Never;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private ControlDocumentViewSetting _controlViewSetting;
        protected void BuildPageSetup()
        {
            if (_controlViewSetting == null && Settings != null)
            {
                _controlViewSetting = new ControlDocumentViewSetting { Name = ExtentionString.CONTROL_SETUP };
                Form.clientPanel.Controls.Add(_controlViewSetting);
                _controlViewSetting.Dock = DockStyle.Fill;
                _controlViewSetting.txtName.Text = Settings.Name;
                _controlViewSetting.txtCode.Text = Settings.Code;
                _controlViewSetting.txtMemo.Text = Settings.Memo;
                _controlViewSetting.txtSummary.Text = Settings.FormatSummary;
                _controlViewSetting.chkAutoSave.Checked = Settings.AutoSave;

                #region ������ ��� ������ ������������
                List<Uid> collectionUser = new List<Uid>();
                _controlViewSetting.cmbUser.Properties.DisplayMember = GlobalPropertyNames.Name;
                _controlViewSetting.cmbUser.Properties.ValueMember = GlobalPropertyNames.Id;
                if (Settings.Storage.UserId != 0)
                {
                    collectionUser.Add(Workarea.Cashe.GetCasheData<Uid>().Item(Settings.Storage.UserId));
                }
                BindingSource bindSourceUser = new BindingSource { DataSource = collectionUser };
                _controlViewSetting.cmbUser.Properties.DataSource = bindSourceUser;
                DataGridViewHelper.GenerateGridColumns(Workarea, _controlViewSetting.ViewUser, "DEFAULT_LISTVIEWUID");
                _controlViewSetting.cmbUser.EditValue = Settings.Storage.UserId;
                _controlViewSetting.cmbUser.Properties.View.BestFitColumns();
                _controlViewSetting.cmbUser.Properties.PopupFormSize = new Size(_controlViewSetting.cmbUser.Width, 150);
                _controlViewSetting.ViewUser.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
                {
                    if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceUser.Count > 0)
                    {
                        Uid imageItem = bindSourceUser[e.ListSourceRowIndex] as Uid;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.GetImage();
                        }
                    }
                    else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceUser.Count > 0)
                    {
                        Uid imageItem = bindSourceUser[e.ListSourceRowIndex] as Uid;
                        if (imageItem != null)
                        {
                            e.Value = imageItem.State.GetImage();
                        }
                    }
                };
                _controlViewSetting.cmbUser.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                {
                    GridLookUpEdit cmb = sender as GridLookUpEdit;
                    if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                        cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                    try
                    {
                        _controlViewSetting.Cursor = Cursors.WaitCursor;
                        collectionUser = Workarea.GetCollection<Uid>().Where(s => (s.KindValue & 1) == 1).ToList();
                        bindSourceUser.DataSource = collectionUser;
                    }
                    catch (Exception)
                    {


                    }
                    finally
                    {
                        _controlViewSetting.Cursor = Cursors.Default;
                    }
                };
                _controlViewSetting.cmbUser.KeyUp += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _controlViewSetting.cmbUser.EditValue = 0;
                };
                _controlViewSetting.cmbUser.EditValueChanged += delegate
                                                                    {
                                                                        Settings.Storage.UserId = (int)_controlViewSetting.cmbUser.EditValue;
                                                                        Settings.Load(Workarea);
                                                                        if (Settings.Pages == null)
                                                                            Settings.Reset(Workarea, Form);
                                                                        _controlViewSetting.ListBoxPages.Items.Clear();
                                                                        foreach (PageSetting page in Settings.Pages)
                                                                            _controlViewSetting.ListBoxPages.Items.Add(page.Name, page.Caption, page.Visible ? CheckState.Checked : CheckState.Unchecked, true);
                                                                    };
                #endregion

                #region ������ ������ ��� ������ � �����������
                RibbonPageGroup Group = new RibbonPageGroup { Name = "SETTINGS_ACTIONLIST", Text = "�������� � �����������" };

                BarButtonItem btnSave = new BarButtonItem
                                            {
                                                Caption = "��������� ���������",
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SAVE_X32),
                                                SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.SAVE_X32), "��������� ���������",
                                                                              "��������� ������� ��������� ��������� ��� �������� ������������")
                                            };
                Group.ItemLinks.Add(btnSave);
                btnSave.ItemClick += delegate
                                         {
                                             Settings.Name = _controlViewSetting.txtName.Text;
                                             Settings.Code = _controlViewSetting.txtCode.Text;
                                             Settings.Memo = _controlViewSetting.txtMemo.Text;
                                             Settings.FormatSummary = _controlViewSetting.txtSummary.Text;
                                             Settings.AutoSave = _controlViewSetting.chkAutoSave.Checked;
                                             Settings.Storage.UserId = (int)_controlViewSetting.cmbUser.EditValue;

                                             Settings.Save(Workarea);
                                         };

                BarButtonItem btnDefault = new BarButtonItem
                                               {
                                                   Caption = "��������� �� ���������",
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32),
                                                   SuperTip =
                                                       CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32), "��������� �� ���������",
                                                                          "���������� ������� ��������� �������� ������������ �� ���������")
                                               };
                Group.ItemLinks.Add(btnDefault);
                btnDefault.ItemClick += delegate
                                            {
                                                Settings.Reset(Workarea, Form);
                                                _controlViewSetting.ListBoxPages.Items.Clear();
                                                foreach (PageSetting page in Settings.Pages)
                                                    _controlViewSetting.ListBoxPages.Items.Add(page.Name, page.Caption, page.Visible ? CheckState.Checked : CheckState.Unchecked, true);
                                            };

                BarButtonItem btnDefaultAll = new BarButtonItem
                                                  {
                                                      Caption = "��� ��������� �� ���������",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32),
                                                      SuperTip =
                                                          CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32),
                                                                             "��� ��������� �� ���������",
                                                                             "���������� ��������� ���� ������������� �� ���������")
                                                  };
                Group.ItemLinks.Add(btnDefaultAll);
                btnDefaultAll.ItemClick += delegate
                                               {
                                                   List<XmlStorage> xmlCollection = Workarea.GetCollection<XmlStorage>().Where(s => s.Code == Settings.Code && s.UserId != 0).ToList();
                                                   foreach (XmlStorage item in xmlCollection)
                                                       item.Delete();
                                                   _controlViewSetting.cmbUser.EditValue = 0;
                                               };

                // TODO: Form.ribbon.Pages.Count - 1 ????
                Form.ribbon.Pages[Form.ribbon.Pages.Count - 1].Groups.Add(Group);
                #endregion

                _controlViewSetting.ListBoxPages.SelectedValueChanged += delegate
                {
                    if (_controlViewSetting.ListBoxPages.SelectedValue == null) return;

                    _controlViewSetting.ListBoxActionGroups.Items.Clear();
                    string pageName = _controlViewSetting.ListBoxPages.SelectedValue.ToString();
                    foreach (PageSetting page in Settings.Pages.Where(page => page.Name == pageName))
                    {
                        foreach (PageActionGroupSetting group in page.Groups)
                            _controlViewSetting.ListBoxActionGroups.Items.Add(group.Name, group.Caption, group.Visible ? CheckState.Checked : CheckState.Unchecked, true);
                        break;
                    }
                };
                _controlViewSetting.ListBoxActionGroups.SelectedValueChanged += delegate
                {
                    if (_controlViewSetting.ListBoxActionGroups.SelectedValue == null) return;

                    _controlViewSetting.ListBoxButtons.Items.Clear();
                    string pageName = _controlViewSetting.ListBoxPages.SelectedValue.ToString();
                    string groupName = _controlViewSetting.ListBoxActionGroups.SelectedValue.ToString();
                    foreach (PageSetting page in Settings.Pages.Where(page => page.Name == pageName))
                    {
                        foreach (PageActionGroupSetting group in page.Groups)
                        {
                            if (group.Name != groupName) continue;
                            foreach (ButtonSetting btn in group.Buttons)
                                _controlViewSetting.ListBoxButtons.Items.Add(btn.Name, btn.Caption, btn.Visible ? CheckState.Checked : CheckState.Unchecked, true);
                            break;
                        }
                        break;
                    }
                };
                _controlViewSetting.ListBoxPages.ItemCheck += delegate
                {
                    foreach (PageSetting page in Settings.Pages.Where(page => page.Name == _controlViewSetting.ListBoxPages.SelectedValue.ToString()))
                    {
                        if (_controlViewSetting.ListBoxPages.Items[_controlViewSetting.ListBoxPages.SelectedIndex].CheckState == CheckState.Checked)
                        {
                            page.Visible = true;
                            Form.ribbon.Pages[GetPageByName(page.Name).PageIndex].Visible = true;
                        }
                        else
                        {
                            page.Visible = false;
                            Form.ribbon.Pages[GetPageByName(page.Name).PageIndex].Visible = false;
                        }
                        break;
                    }
                };
                _controlViewSetting.ListBoxActionGroups.ItemCheck += delegate
                {
                    foreach (PageSetting page in Settings.Pages.Where(page => page.Name == _controlViewSetting.ListBoxPages.SelectedValue.ToString()))
                    {
                        foreach (PageActionGroupSetting group in page.Groups)
                        {
                            if (group.Name != _controlViewSetting.ListBoxActionGroups.SelectedValue.ToString()) continue;
                            group.Visible = _controlViewSetting.ListBoxActionGroups.Items[_controlViewSetting.ListBoxActionGroups.SelectedIndex].CheckState == CheckState.Checked;
                            break;
                        }
                        break;
                    }
                };
                _controlViewSetting.ListBoxButtons.ItemCheck += delegate
                {
                    foreach (PageSetting page in Settings.Pages.Where(page => page.Name == _controlViewSetting.ListBoxPages.SelectedValue.ToString()))
                    {
                        foreach (PageActionGroupSetting group in page.Groups)
                        {
                            if (group.Name != _controlViewSetting.ListBoxActionGroups.SelectedValue.ToString()) continue;
                            foreach (ButtonSetting btn in
                                group.Buttons.Where(btn => btn.Name == _controlViewSetting.ListBoxButtons.SelectedValue.ToString()))
                            {
                                btn.Visible = _controlViewSetting.ListBoxButtons.Items[_controlViewSetting.ListBoxButtons.SelectedIndex].CheckState == CheckState.Checked;
                                break;
                            }
                            break;
                        }
                        break;
                    }
                };
                foreach (PageSetting page in Settings.Pages)
                    _controlViewSetting.ListBoxPages.Items.Add(page.Name, page.Caption, page.Visible ? CheckState.Checked : CheckState.Unchecked, true);
            }
            HidePageControls(ExtentionString.CONTROL_SETUP);
        }
        /// <summary>
        /// ������������� �������� ���������, �� ������� ������������ �������� � ������, ��� ����������� �� �����������
        /// </summary>
        protected void InitConfig()
        {
            Settings = new RibbonFormViewSetting
            {
                
                Name = string.Format("��������� ��������� \"{0}\"", SourceDocument.EntityDocument.Name)
            };
            Settings.Code = "DOCUMENTVIEW_CONFIG_" + GetType().Name.ToUpper();
            if(Settings.Code.Length>50)
            {
                Settings.Code = Settings.Code.Substring(0, 50);
            }
            if (Settings.Storage == null)
            {
                Settings.Load(Workarea);
                if (Settings.Pages == null)
                {
                    Settings.Reset(Workarea, Form);
                    Settings.Save(Workarea);
                }
                //int index = Form.Ribbon.SelectedPage.PageIndex;
                //for (int i = 0; i < Form.Ribbon.Pages.Count - 1; i++)
                //    Form.Ribbon.SelectedPage = Form.Ribbon.Pages[i];
                //Form.Ribbon.SelectedPage = Form.Ribbon.Pages[index];
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public IRibbonFormViewSetting Settings { get; set; } 
        #endregion

        #region �������� ���������������� ��������
        private ControlList _gridLogUserAction;
        private void BuildPageLogAction()
        {
            // _sourceDocument.Document.ShowChangesProtocol();
            RibbonPage page = GetPageByName("PAGE_" + ActivePage);
            RibbonPageGroup groupLinksActionLogActions = page.GetGroupByName(page.Name + "_ACTIONLIST");
            if (groupLinksActionLogActions == null)
            {
                groupLinksActionLogActions = new RibbonPageGroup { Name = page.Name + "_ACTIONLIST", Text = "�������� � ���������� ���������" };

                #region �������� ��������
                BarButtonItem buttonRefreshLogAction = new BarButtonItem
                                                           {
                                                               Name = "btnRefreshLogAction",
                                                               Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                               RibbonStyle = RibbonItemStyles.Large,
                                                               Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32),
                                                               SuperTip =
                                                                   CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32),
                                                                                      Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                                                      "�������� �������� ���������")
                                                           };
                groupLinksActionLogActions.ItemLinks.Add(buttonRefreshLogAction);
                buttonRefreshLogAction.ItemClick += ButtonRefreshLogActionItemClick;
                //ButtonNewProduct.ItemClick += ButtonNewProductItemClick;
                #endregion

                #region ��������� ��������
                BarButtonItem buttonDocLogReport = new BarButtonItem
                                                       {
                                                           Name = "btnDocLogReport",
                                                           Caption = "���������������� ��������",
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REPORT_X32),
                                                           SuperTip =
                                                               CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.REPORT_X32),
                                                                                  "���������������� ��������",
                                                                                  "���������������� �������� ��������� ���������")
                                                       };
                groupLinksActionLogActions.ItemLinks.Add(buttonDocLogReport);
                buttonDocLogReport.ItemClick += ButtonDocLogReportItemClick;
                #endregion

                page.Groups.Add(groupLinksActionLogActions);
            }
            if (_gridLogUserAction == null)
            {
                _gridLogUserAction = new ControlList {Name = ExtentionString.CONTROL_LOGACTION};
                Form.clientPanel.Controls.Add(_gridLogUserAction);
                _gridLogUserAction.Dock = DockStyle.Fill;
                BindingSource collectionBind = new BindingSource();
                List<LogUserAction> collection = LogUserAction.GetCollection(Workarea, MainDocument.Id);
                collectionBind.DataSource = collection;
                DataGridViewHelper.GenerateGridColumns(Workarea, _gridLogUserAction.View, "DEFAULT_LISTVIEWLOGACTION");
                _gridLogUserAction.Grid.DataSource = collectionBind;
                _gridLogUserAction.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
                
            }
            HidePageControls(ExtentionString.CONTROL_LOGACTION);
        }

        void ViewCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                e.Value = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTCHANGES_X16);
            }
        }

        void ButtonDocLogReportItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeDocumentLogReport();
        }
        /// <summary>�������� ����� ������� ���������������� ���������</summary>
        protected void InvokeDocumentLogReport()
        {
            throw new NotImplementedException();
        }

        void ButtonRefreshLogActionItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeRefreshLogAction();
        }

        protected void InvokeRefreshLogAction()
        {
            _gridLogUserAction.Grid.DataSource = null;
            BindingSource collectionBind = new BindingSource();
            List<LogUserAction> collection = LogUserAction.GetCollection(Workarea, MainDocument.Id);
            collectionBind.DataSource = collection;
            _gridLogUserAction.Grid.DataSource = collectionBind;
        } 
        #endregion
        #region ���������� ���������������� ��������
        /// <summary>��������� ������� ��������� ���������� ��� ��������� "��������"</summary>
        protected virtual void SetViewStateDone()
        {
            LogUserAction.CreateActionStateChanged(Workarea, MainDocument.Id, "��������");
        }

        /// <summary>��������� ������� ��������� ���������� ��� ��������� "������������"</summary>
        protected virtual void SetViewStateReadonly()
        {
            LogUserAction.CreateActionStateChanged(Workarea, MainDocument.Id, "������������");
        }

        /// <summary>��������� ������� ��������� ���������� ��� ��������� "�������������"</summary>
        protected virtual void SetViewStateReadWrite()
        {
            LogUserAction.CreateActionStateChanged(Workarea, MainDocument.Id, "�������������");
        }

        /// <summary>��������� ������� ��������� ���������� ��� ��������� "�� ��������� � �����"</summary>
        protected virtual void SetViewStateNotDone()
        {
            LogUserAction.CreateActionStateChanged(Workarea, MainDocument.Id, Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SETNOTDONE, 1049));
        }
        #endregion

        #region �������� "�����"
        private List<IChainAdvanced<Document, FileData>> _collectionFiles;
        private BindingSource _bindFiles;
        private DevExpress.XtraGrid.GridControl _gridFiles;
        public GridView ViewFiles;

        protected void BuildPageLinkedFiles()
        {
            RibbonPage page = GetPageByName("PAGE_" + ActivePage);
            RibbonPageGroup groupLinksActionFiles = page.GetGroupByName(page.Name + "_ACTIONLIST");
            if (groupLinksActionFiles == null)
            {
                groupLinksActionFiles = new RibbonPageGroup { Name = page.Name + "_ACTIONLIST", Text = "�������� � �������" };

                #region �������� ����� ����
                BarButtonItem btnFileCreate = new BarButtonItem
                {
                    Name = "btnFileCreate",
                    ButtonStyle = BarButtonStyle.Default,
                    Caption = "�������� ����� ����",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.NEW_X32), "�������� ����� ����",
                                                  "���������� ����� � ���������� ��� � ������� ���������� �� �������� �������. �� �������� ������� ���������� ������ ������������ ������!")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileCreate);
                btnFileCreate.ItemClick += BtnFileCreateItemClick;
                #endregion

                #region ������� � ������
                BarButtonItem btnFileLink = new BarButtonItem
                {
                    Name = "btnFileLink",
                    Caption = "������� � ������",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.LINKNEW_X32),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, "LINKNEW32"), "������� � ������",
                                                  "������� � ������ ��� ������������������ � ���� ������ �� �������� ��������������."),
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileLink);
                btnFileLink.ItemClick += BtnFileLinkItemClick;

                #endregion

                #region ������� �����
                BarButtonItem btnFileExport = new BarButtonItem
                {
                    Name = "btnFileExport",
                    Caption = "������� �����",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DATAOUT_X32),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.DATAOUT_X32), "������� �����",
                                                  "������ ����� �� ���� ������ ��� ��������� ��� ���������. ����� ��������� ���������� �������� ������������� ���� � ���� ������")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileExport);
                btnFileExport.ItemClick += BtnFileExportItemClick;
                #endregion

                #region �������� �����
                BarButtonItem btnFilePreview = new BarButtonItem
                {
                    Name = "btnFilePreview",
                    Caption = "�������� �����",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = BusinessObjects.Windows.Properties.Resources.PREVIEW_X32,
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, "PREVIEW32"), "�������� �����",
                                                  "�������� ����� ����������� ��������������� ��� ������� �����, ��������� ������ ���� ����������� �� ����� ����������. ����� ��������� � ���� ��������� ������� ������ <b>��</b> ��� �������� �������� ���������� ����� <br><i>(�� ��������� ������ <b>��</b> �� �������� �����)</i>")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFilePreview);
                btnFilePreview.ItemClick += BtnFilePreviewItemClick;
                #endregion

                #region �������� �����
                BarButtonItem btnFileDelete = new BarButtonItem
                {
                    Name = "btnFileDelete",
                    Caption = "������� ����",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32), "������� ����",
                                                  "�������� ����� ���������� � ����������. �������� ������ �������� - �������� ����� � ����� � �������� ������ �����.")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileDelete);
                btnFileDelete.ItemClick += BtnFileDeleteItemClick;

                #endregion
                page.Groups.Add(groupLinksActionFiles);
            }
            if (_gridFiles == null)
            {
                _gridFiles = new DevExpress.XtraGrid.GridControl();
                ViewFiles = new GridView();
                _gridFiles.Dock = DockStyle.Fill;
                _gridFiles.ViewCollection.Add(ViewFiles);
                _gridFiles.MainView = ViewFiles;
                ViewFiles.GridControl = _gridFiles;

                ViewFiles.OptionsBehavior.AllowIncrementalSearch = true;
                ViewFiles.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled;
                ViewFiles.OptionsBehavior.Editable = false;
                ViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
                ViewFiles.OptionsView.ShowGroupPanel = false;
                ViewFiles.OptionsView.ShowIndicator = false;
                _gridFiles.ShowOnlyPredefinedDetails = true;
                Form.clientPanel.Controls.Add(_gridFiles);

                _gridFiles.Name = ExtentionString.CONTROL_LINKFILES;
                DataGridViewHelper.GenerateGridColumns(Workarea, ViewFiles, "DEFAULT_LISTVIEWCONTRACTFILES");
                // TODO: �������� 13 �� ���������� ������������� ���� �����
                _collectionFiles = MainDocument.GetLinkedFiles().Where(s => s.StateId == State.STATEACTIVE).ToList();
                _bindFiles = new BindingSource { DataSource = _collectionFiles };
                _gridFiles.DataSource = _bindFiles;
                ViewFiles.CustomUnboundColumnData += ViewCustomUnboundColumnDataFiles;
                _gridFiles.DoubleClick += GridFilesDoubleClick;

            }
            HidePageControls(ExtentionString.CONTROL_LINKFILES);
        }
        // ��������� ��������� ����������� ������ � ������
        void ViewCustomUnboundColumnDataFiles(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Document, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Document, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Document, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Document, FileData>;
                if (link != null)
                {
                    e.Value = link.State.GetImage();
                }
            }
        }
        private void BtnFileDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileDelete();
        }

        protected void InvokeFileDelete()
        {
            if (_bindFiles.Current == null) return;
            ChainAdvanced<Document, FileData> link = _bindFiles.Current as ChainAdvanced<Document, FileData>;
            if (link == null) return;
            // TODO: ������������ ������ ��������
            int res = Extentions.ShowMessageChoice(Workarea, "�������� �����", "�������� �����", "�������� ������ � ������ ��������� � ������ ����������. �������� ����� ������� ������ ����� � ������ ������, �������� ����� � ����� ������� ��� ������ ������� ����.", "������� ������ �����|������� ����� � ����");
            switch (res)
            {
                case 0:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: ������������ ������ ��������
                        Extentions.ShowMessageDatabaseExeption(Workarea, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "������ ��������!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 1:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        link.Right.StateId = State.STATEDELETED;
                        link.Right.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: ������������ ������ ��������
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                                                               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "������ ��������!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                                                   Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void BtnFilePreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFilePreview();
        }

        private void BtnFileExportItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileExport();
        }

        protected void InvokeFileExport()
        {
            try
            {
                if (_bindFiles.Current == null) return;
                ChainAdvanced<Document, FileData> link = _bindFiles.Current as ChainAdvanced<Document, FileData>;
                if (link == null) return;
                SaveFileDialog dlg = new SaveFileDialog
                                         {
                                             FileName = link.Right.Name,
                                             DefaultExt = link.Right.FileExtention,
                                             InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                         };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    link.Right.ExportStreamDataToFile(dlg.FileName);
                    System.Diagnostics.Process.Start(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFileLinkItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileLink();
        }

        protected void InvokeFileLink()
        {
            int AgentIdRelatedFiles = 0;
            int currentAgId = AgentIdRelatedFiles;
            List<FileData> collFilesToBrowse = FileData.GetCollectionClientFiles(Workarea, currentAgId);
            List<FileData> retColl = Workarea.Empty<FileData>().BrowseList(null, collFilesToBrowse.Count == 0 ? null : collFilesToBrowse);
            if (retColl == null || retColl.Count == 0) return;
            foreach (ChainAdvanced<Document, FileData> link in
                retColl.Select(item => new ChainAdvanced<Document, FileData>(MainDocument) { Right = item, StateId = State.STATEACTIVE, KindId = 13 }))
            {
                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
                _bindFiles.ResetBindings(false);
            }
        }

        private void BtnFileCreateItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileCreate();
        }

        protected void InvokeFileCreate()
        {
            OpenFileDialog dlg = new OpenFileDialog
                                     {
                                         Multiselect = true,
                                         InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                         Filter =
                                             "Adobe PDF|*.pdf|Microsoft Excel 2007|*.xlsx|Microsoft Excel|*.xls|Microsoft Word 2007|*.docx|Microsoft Word|*.doc|PNG|*.png|JPG|*.jpg|XPS|*.xps|��� �����|*.*"
                                     };
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            foreach (string fileName in dlg.FileNames)
            {
                FileData fileData = new FileData { Workarea = Workarea, Name = Path.GetFileName(fileName) };
                fileData.KindId = FileData.KINDID_FILEDATA;
                fileData.SetStreamFromFile(fileName);
                fileData.Save();
                // TODO: �������� �� ���������� ����������� ���� �����
                ChainAdvanced<Document, FileData> link =
                    new ChainAdvanced<Document, FileData>(MainDocument) { Right = fileData, StateId = State.STATEACTIVE, KindId = 13 };

                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
            }
            _bindFiles.ResetBindings(false);
        }

        private void GridFilesDoubleClick(object sender, EventArgs e)
        {
            Point p = _gridFiles.PointToClient(Control.MousePosition);
            GridHitInfo hit = ViewFiles.CalcHitInfo(p.X, p.Y);
            if (hit.InRow)
            {
                InvokeFilePreview();
            }
        }

        protected void InvokeFilePreview()
        {
            try
            {
                /*if (_bindFiles.Current == null) return;
                ChainAdvanced<Document, FileData> link = _bindFiles.Current as ChainAdvanced<Document, FileData>;
                if (link == null) return;
                string pathToSave = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(), link.Right.Name + "." + link.Right.FileExtention);
                link.Right.ExportStreamDataToFile(pathToSave);
                System.Diagnostics.Process.Start(pathToSave);
                DevExpress.XtraEditors.XtraMessageBox.Show("������� �� ����� ���������� ��������� �����!",
                                                           _wa.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.IO.File.Delete(pathToSave);*/
                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<DocumentPreviewThread<T>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                DocumentPreviewThread<T> dpt = new DocumentPreviewThread<T>(_bindFiles.Current as ChainAdvanced<Document, FileData>) { DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion

        #region �������� "����� ���������"
        private BarButtonItem _buttonDocLinksView;
        private ControlTree _treeChains;
        protected virtual void BuildPageLinks()
        {
            RibbonPage page = GetPageByName("PAGE_" + ActivePage);
            RibbonPageGroup groupLinksActionDocLinks = page.GetGroupByName(page.Name + "_ACTIONLIST");
            if (groupLinksActionDocLinks == null)
            {
                groupLinksActionDocLinks = new RibbonPageGroup { Name = page.Name + "_ACTIONLIST", Text = "�������� �� ���������� �����������" };

                #region �������
                BarButtonItem btnLinkCreate = new BarButtonItem
                                                  {
                                                      Name = "btnLinkCreate",
                                                      ButtonStyle = BarButtonStyle.Default,
                                                      Caption = "�������",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                                                      SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.NEW_X32), "������� �",
                                                                                    "������� � ������� �������� � ������ ����������")
                                                  };
                groupLinksActionDocLinks.ItemLinks.Add(btnLinkCreate);
                btnLinkCreate.ItemClick += delegate
                {
                    if (MainDocument.IsTemplate)
                    {
                        //List<Document> branches = Workarea.Empty<Document>().BrowseList(f => f.IsTemplate, null);
                        List<Document> coll = Workarea.GetTemplates<Document>();
                        List<Document> documents = MainDocument.BrowseList(null, coll);

                        if((documents!=null)&&(documents.Count>0))
                        {
                            List<Document> collD = Document.GetChainSourceList(Workarea, MainDocument.Id, 20);
                            foreach (Document doc in documents)
                            {
                                if (!collD.Exists(d => d.Id == doc.Id) && MainDocument.Id != doc.Id)
                                {
                                    ChainDocument Chain = new ChainDocument()
                                    {
                                        Workarea = Workarea,
                                        LeftId = MainDocument.Id,
                                        RightId = doc.Id,
                                        DateValue = DateTime.Now
                                    };
                                    Chain.Save();
                                }
                            }
                        }
                    }
                    else
                    {
                        FormProperties frm = new FormProperties
                                                 {
                                                     Width = 800,
                                                     Height = 480
                                                 };
                        Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.LINK_X16);
                        frm.Ribbon.ApplicationIcon = img;
                        frm.Icon = Icon.FromHandle(img.GetHicon());
                        ContentNavigator Navigator = new ContentNavigator {MainForm = frm, Workarea = Workarea};
                        ContentModuleDocuments DocsModule = new ContentModuleDocuments() {Workarea = Workarea};
                        Navigator.SafeAddModule("DOCUMENTS", DocsModule);
                        Navigator.ActiveKey = "DOCUMENTS";
                        frm.btnSave.Visibility = BarItemVisibility.Never;
                        frm.btnSelect.Visibility = BarItemVisibility.Always;

                        frm.btnSelect.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32);
                        frm.btnSelect.ItemClick += delegate
                                                       {
                                                           List<Document> collD = Document.GetChainSourceList(Workarea,MainDocument.Id,20);
                                                           foreach (Document doc in DocsModule.SelectedDocuments)
                                                           {
                                                               if (!collD.Exists(d => d.Id == doc.Id) &&
                                                                   MainDocument.Id != doc.Id)
                                                               {
                                                                   ChainDocument Chain = new ChainDocument()
                                                                                             {
                                                                                                 Workarea = Workarea,
                                                                                                 LeftId = MainDocument.Id,
                                                                                                 RightId = doc.Id,
                                                                                                 DateValue =DateTime.Now,
                                                                                                 Summ = MainDocument.Summa > doc.Summa
                                                                                                         ? doc.Summa
                                                                                                         : MainDocument.Summa
                                                                                             };
                                                                   Chain.Save();
                                                               }
                                                           }
                                                       };
                        frm.ShowDialog();
                        InvokeLinksRefresh();
                    }
                };
                //BtnLinkCreate.ItemClick += BtnLinkCreateItemClick;
                #endregion

                #region ��������
                BarButtonItem btnProp = new BarButtonItem
                {
                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                btnProp.SuperTip = CreateSuperToolTip(btnProp.Glyph, btnProp.Caption,
                        "�������� ���� ��������� ����� ��� ������������ ���������");
                groupLinksActionDocLinks.ItemLinks.Add(btnProp);

                btnProp.ItemClick += delegate
                                         {
                                             LocalChainItem ci = _treeChains.Tree.FocusedNode.Tag as LocalChainItem;
                                             
                                             List<IChain<Document>> collC = ((IChains<Document>)MainDocument).GetLinks();
                                             IChain<Document> chain = collC.Find(f => f.Id == ci.ChainId);
                                             (chain as Chain<Document>).ShowProperty();
                                             
                                             //List<IChain<Document>> collC = ((IChains<Document>)MainDocument).GetLinks();
                                             //List<Document> collD = Document.GetChainSourceList(Workarea, (int)e.Node.GetValue(GlobalPropertyNames.Id), 20);
                                             //((collectionBind.Current) as Chain<T>).ShowProperty();
                                             //LocalChainItem item = (LocalChainItem)_treeChains.Tree.Selection[0].Tag;
                                             //Chain<Document> c = Workarea.GetCollection<Chain<Document>>().Where(s => (s.Id == item.Id)).ToList()[0];
                                             //c.ShowProperty();
                                             //((_bindFiles.Current) as Chain<Agent>).ShowProperty();
                                         };
                #endregion

                #region �������� �����
                BarButtonItem buttonRefreshLinks = new BarButtonItem
                                                       {
                                                           Name = "btnRefreshLinks",
                                                           Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32),
                                                           SuperTip =
                                                               CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32), Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                                                  "�������� ����� ���������")
                                                       };
                groupLinksActionDocLinks.ItemLinks.Add(buttonRefreshLinks);
                buttonRefreshLinks.ItemClick += ButtonRefreshLinks_ItemClick;
                //ButtonNewProduct.ItemClick += ButtonNewProductItemClick;
                #endregion

                #region �������� �����
                BarButtonItem buttonDocLinkDelete = new BarButtonItem
                                                        {
                                                            Name = "btnDocLinkDelete",
                                                            Caption = "������� �����",
                                                            RibbonStyle = RibbonItemStyles.Large,
                                                            Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32),
                                                            SuperTip =
                                                                CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32), "������� �����",
                                                                                   "������� ������� ����� ���������")
                                                        };
                groupLinksActionDocLinks.ItemLinks.Add(buttonDocLinkDelete);
                buttonDocLinkDelete.ItemClick += delegate
                {
                    if (_treeChains.Tree.Selection.Count > 0 && _treeChains.Tree.Selection[0].Tag != null)
                    {
                        if (MessageBox.Show("�� �������, ��� ������ ������� �����?", "��������", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            LocalChainItem item = (LocalChainItem)_treeChains.Tree.Selection[0].Tag;
                            IChain<Document> Chain = ((IChains<Document>)MainDocument).GetLinks().First(c => c.Id == item.ChainId);
                            Chain.Delete();
                            _treeChains.Tree.DeleteNode(_treeChains.Tree.Selection[0]);
                        }
                    }
                };
                //ButtonDelete.ItemClick += ButtonDeleteItemClick;
                #endregion

                #region ��������/����������
                _buttonDocLinksView = new BarButtonItem
                {
                    Name = "btnDocLinksView",
                    Caption = "���\n[��������]",
                    Tag = 0,
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ARROWDOWNBLUE_X32)
                };
                _buttonDocLinksView.SuperTip = CreateSuperToolTip(_buttonDocLinksView.Glyph, "��� ����������� ������",
                "������ ��� ����������� ������. ���������� ��� ���� ������ - �������� � ����������.\n�������� - ������ ���������� ��������� �� �������� ���������.\n��������� - ������ ���������� �� ������� ������� ������� ��������.");
                groupLinksActionDocLinks.ItemLinks.Add(_buttonDocLinksView);
                _buttonDocLinksView.ItemClick += delegate
                {
                    if ((int)_buttonDocLinksView.Tag == 0)
                    {
                        _buttonDocLinksView.Caption = "���\n[����������]";
                        _buttonDocLinksView.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ARROWUPBLUE_X32);
                        _buttonDocLinksView.Tag = 1;
                        btnLinkCreate.Enabled = false;
                    }
                    else
                    {
                        _buttonDocLinksView.Caption = "���\n[��������]";
                        _buttonDocLinksView.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ARROWDOWNBLUE_X32);
                        _buttonDocLinksView.Tag = 0;
                        btnLinkCreate.Enabled = true;
                    }
                    page.Ribbon.Refresh();
                    buttonRefreshLinks.PerformClick();
                };
                #endregion

                page.Groups.Add(groupLinksActionDocLinks);
            }
            if (_treeChains == null)
            {
                _treeChains = new ControlTree {Name = ExtentionString.CONTROL_LINK_NAME};
                Form.clientPanel.Controls.Add(_treeChains);
                _treeChains.Dock = DockStyle.Fill;
                Image img = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTDONE_X16);
                _treeChains.ImageCollection.AddImage(img);

                DataGridViewHelper.GenerateTreeGreedColumns(Workarea, _treeChains.Tree, "DEFAULT_LISTVIEWDOCUMENTSHORT");
                //DevExpress.XtraTreeList.Columns.TreeListColumn colId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                //colId.Caption = "��";
                //colId.FieldName = GlobalPropertyNames.Id;
                //colId.Visible = false;

                //DevExpress.XtraTreeList.Columns.TreeListColumn colDate = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                //colDate.Caption = "����";
                //colDate.FieldName = "Date";
                //colDate.Visible = true;
                //colDate.Width = 50;
                //colDate.VisibleIndex = 0;

                //DevExpress.XtraTreeList.Columns.TreeListColumn colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                //colName.Caption = "������������";
                //colName.FieldName = "Name";
                //colName.VisibleIndex = 1;
                //colName.Width = 150;

                //DevExpress.XtraTreeList.Columns.TreeListColumn colNumber = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                //colNumber.Caption = "�����";
                //colNumber.FieldName = "Number";
                //colNumber.VisibleIndex = 2;
                //colNumber.Width = 50;

                //TreeChains.Tree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] { colId, colDate, colName, colNumber });
                _treeChains.Tree.BeforeExpand += TreeBeforeExpand;
                InvokeLinksRefresh();
            }
            HidePageControls(ExtentionString.CONTROL_LINK_NAME);
        }

        private class LocalChainItem
        {
            public int ChainId;
            public int OrderNo;
            public int Id;
            public DateTime Date;
            public string Name;
            public string Number;
            public decimal Summa;
        }

        void TreeBeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            e.Node.Nodes.Clear();
            List<IChain<Document>> collC = ((IChains<Document>) MainDocument).GetLinks();
            List<Document> collD = Document.GetChainSourceList(Workarea, (int)e.Node.GetValue(GlobalPropertyNames.Id), 20);

            var query = from chain in collC
                        join doc in collD on chain.RightId equals doc.Id
                        select new LocalChainItem() {ChainId = chain.Id, OrderNo = chain.OrderNo, Name = doc.Name, Number = doc.Number, Summa = doc.Summa, Date = doc.Date, Id = doc.Id};
            
            //List<Document> list = (int)_buttonDocLinksView.Tag == 0 ? Document.GetChainSourceList(Workarea, (int)e.Node.GetValue(GlobalPropertyNames.Id), 20) : Document.GetChainDestinationList(Workarea, (int)e.Node.GetValue(GlobalPropertyNames.Id), 20);
            //foreach (TreeListNode node in query.Select(c => _treeChains.Tree.AppendNode(new object[] { c.Id, c.Date, c.Name, c.Number, c.Summa }, e.Node.Id)))
            foreach (LocalChainItem c in query)
            {
                TreeListNode node = _treeChains.Tree.AppendNode(new object[] { c.Id, c.Date, c.Name, c.Number, c.Summa }, e.Node.Id);
                node.Tag = c;
                node.ImageIndex = 0;
                node.StateImageIndex = 0;
                node.SelectImageIndex = 0;
                _treeChains.Tree.AppendNode(new object[] { 0, DateTime.Now, "Empty", "000" }, node.Id);
            }
        }

        void ButtonRefreshLinks_ItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeLinksRefresh();
        }

        protected void InvokeLinksRefresh()
        {
            _treeChains.Tree.ClearNodes();
            TreeListNode node = _treeChains.Tree.AppendNode(new object[] { MainDocument.Id, MainDocument.Date, MainDocument.Name, MainDocument.Number, MainDocument.Summa }, 0);
            node.Tag = null;
            node.ImageIndex = 0;
            node.StateImageIndex = 0;
            node.SelectImageIndex = 0;
            _treeChains.Tree.AppendNode(new object[] { 0, DateTime.Now, "Empty", "000" }, node.Id);
            node.Expanded = true;
        }
        #endregion

        #region �������� "��������������"
        private Control _controlIdGroup;
        ControlState _controlState;
        ControlId _controlId;
        /// <summary>
        /// ���������� �������� "��������������"
        /// </summary>
        /// <returns></returns>
        protected virtual void BuildPageId()
        {
            if (_controlIdGroup == null)
            {
                _controlIdGroup = new Control {Name = ExtentionString.CONTROL_ID_NAME};
                Form.clientPanel.Controls.Add(_controlIdGroup);
                _controlIdGroup.Dock = DockStyle.Fill;
                _controlId = new ControlId();
                //controlId.Name = 
                _controlIdGroup.Controls.Add(_controlId);
                _controlId.Dock = DockStyle.Top;
                _controlId.txtId.Text = MainDocument.Id.ToString();
                _controlId.txtGuid.Text = ((ICoreObject)MainDocument).Guid.ToString();
                _controlId.txtBranchId.Text = ((ICoreObject)MainDocument).DatabaseId.ToString();
                _controlId.txtSourceId.Text = ((ICoreObject)MainDocument).DbSourceId.ToString();
                _controlId.txtKind.Text = MainDocument.KindId.ToString();

                if (MainDocument is ICompanyOwner)
                {
                    _controlId.layoutControlItemMyCompanyId.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _controlId.layoutControlItemMyCompany.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    _controlId.txtMyCompanyId.Text = MainDocument.MyCompanyId.ToString();
                    if (MainDocument.MyCompanyId != 0)
                        _controlId.txtMyCompany.Text = MainDocument.MyCompany.Name;
                }

                _controlState = new ControlState {Name = ExtentionString.CONTROL_STATES_NAME};
                _controlIdGroup.Controls.Add(_controlState);
                _controlState.Dock = DockStyle.Fill;
                _controlState.BringToFront();
                BindingSource stateBindings = new BindingSource { DataSource = MainDocument.Workarea.CollectionStates };
                DataGridViewHelper.GenerateLookUpColumns(MainDocument.Workarea, _controlState.cmbState, "DEFAULT_LOOKUP_NAME");

                _controlState.cmbState.Properties.DataSource = stateBindings;
                _controlState.cmbState.EditValue = MainDocument.State;
                //controlState.cmbState.SelectedIndexChanged += delegate
                //{
                //    SelectedItem.State = controlState.cmbState.SelectedValue as State;
                //};
                #region ���������� ������ ������
                foreach (FlagValue flagItem in MainDocument.Entity.FlagValues)
                {
                    int index = _controlState.chlFlags.Items.Add(flagItem.Name);
                    _controlState.chlFlags.Items[index].Description = flagItem.Name;
                    _controlState.chlFlags.Items[index].Value = flagItem.Value;
                    CheckState status = CheckState.Unchecked;
                    if ((flagItem.Value & MainDocument.FlagsValue) == flagItem.Value)
                        status = CheckState.Checked;
                    _controlState.chlFlags.Items[index].CheckState = status;
                }
                #endregion
                #region ���������� ������ �����
                // TODO: ���������� ������ �����
                //foreach (EntityKind kindItem in MainDocument.Entity.EntityKinds.Where(s => s.SubKind < MainDocument.Entity.MaxKind + 1))
                //{
                //    int index = _controlState.chkKinds.Items.Add(kindItem.ToString());
                //    _controlState.chkKinds.Items[index].Description = kindItem.ToString();
                //    _controlState.chkKinds.Items[index].Value = kindItem.SubKind;
                //    CheckState status = CheckState.Unchecked;
                //    if ((kindItem.SubKind & MainDocument.KindValue) == kindItem.SubKind)
                //        status = CheckState.Checked;
                //    _controlState.chkKinds.Items[index].CheckState = status;
                //}

                #endregion
            }
            HidePageControls(ExtentionString.CONTROL_ID_NAME);
        } 
        #endregion

        /// <summary>
        /// ���������� �������� "�����"
        /// </summary>
        /// <returns></returns>
        public abstract void BuildPageCommon();

        #region ��������� ��������������� ��� ���������� �� ������� �������� �� ��������� ���������
        /// <summary>
        /// ��������� ���������������
        /// </summary>
        protected List<Agent> CollectionAgentFrom;
        /// <summary>
        /// ��������� ���������������
        /// </summary>
        protected List<Agent> CollectionAgentDepatmentFrom;
        /// <summary>
        /// ��������� ���������������
        /// </summary>
        protected List<Agent> CollectionAgentTo;
        /// <summary>
        /// ��������� ���������������
        /// </summary>
        protected List<Agent> CollectionAgentDepatmentTo;
        /// <summary>
        /// ��������� ���������������
        /// </summary>
        protected List<Agent> CollectionAgentManagers;
        /// <summary>
        /// ��������� ���������������
        /// </summary>
        protected List<Agent> CollectionAgentSupervisors;
        /// <summary>
        /// ��������� �������� ����
        /// </summary>
        protected List<Library> CollectionPrintableForms;
        #endregion
        #region ��������� ��� ����������
        /// <summary>
        /// ������ ��������� ������� � ����������
        /// </summary>
        protected BindingSource BindingDocumentReports;
        /// <summary>
        /// ������ ��������� ���������� ������� ������
        /// </summary>
        protected BindingSource BindingDocumentChains;
        /// <summary>
        /// ������-�������� � ������� ��������� �������� ��� � ������� �� ������ ���� ��������
        /// </summary>
        protected BindingSource OwnerList;
        /// <summary>
        /// ��������� ��� ���������� ��������������� "����������� ���"
        /// </summary>
        public BindingSource BindSourceAgentFrom;
        /// <summary>
        /// ��������� ��� ���������� ��������������� "������������� ���"
        /// </summary>
        public BindingSource BindSourceAgentDepatmentFrom;
        /// <summary>
        /// ��������� ��� ���������� ��������������� "����������� ����"
        /// </summary>
        public BindingSource BindSourceAgentTo;
        /// <summary>
        /// ��������� ��� ���������� ��������������� "�������������"
        /// </summary>
        public BindingSource BindSourceAgentDepatmentTo;
        /// <summary>
        /// ��������� ��� ���������� ��������������� "��������"
        /// </summary>
        public BindingSource BindSourceAgentManager;
        /// <summary>
        /// ��������� ��� ���������� ��������������� "����������"
        /// </summary>
        public BindingSource BindSourceAgentSupervisor;
        /// <summary>
        /// ��������� ��� ���������� ����� ���������
        /// </summary>
        public BindingSource BindSourceDetails;
        #endregion
        #region ������ ������ ������������
        /// <summary>
        /// ������ �������� �� ������ ������������ �������� "�������"
        /// </summary>
        protected RibbonPageGroup GroupLinksActionList;
        /// <summary>
        /// ������ "��������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonSetStateDone;
        /// <summary>
        /// ������ "�������������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonSetReadOnly;
        /// <summary>
        /// ������ "�������� ����������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonSetStateNotDone;
        /// <summary>
        /// ������ "��������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonPreview;
        /// <summary>
        /// ������ "������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonPrint;
        /// <summary>
        /// ������ "����� �����" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonNewProduct;
        /// <summary>
        /// ������ "������� �����" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonDelete;

        /// <summary>
        /// ������ "�������� �������� �������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonProductInfo;
        /// <summary>
        /// ������ "������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonHelp;
        /// <summary>
        /// ������ "��������" �� ������ ������������
        /// </summary>
        protected BarButtonItem ButtonUserActions;
        #endregion

        /// <summary>�������� ��� ������ ��������, ������������ ��� ����� �������, ���������� � ���� �������� HelpProvider. </summary>
        public string HelpNamespace { get; set; }

        /// <summary>������������� ����������� ��������� ��� ���������� ������� ��� ��������� ����������.</summary>
        public HelpProvider HelpProvider { get; set; }

        /// <summary>������������� ��������� �������</summary>
        protected virtual void InitReports()
        {

        }

        /// <summary>���������� ���������</summary>
        /// <returns></returns>
        public virtual bool InvokeSave()
        {
            InvokeSaveState();
            if (!this.ValidateRuleSetView())
            {
                MainDocument.ShowDialogValidationErrors();
                return false;
                CanClose = false;
            }
            return true;
        }

        public virtual bool InvokeSetState(int value)
        {
            SourceDocument.StateId = value;
            return InvokeSave();
        }
        public virtual void InvokeSaveState()
        {
            if (_controlState != null)
            {
                int flagValue = (from CheckedListBoxItem i in _controlState.chlFlags.Items where i.CheckState == CheckState.Checked select (int)i.Value).Sum();
                SourceDocument.FlagsValue = flagValue;
            } 
        }

        /// <summary>�������� ������ ���������</summary>
        public abstract void InvokeRowDelete();

        /// <summary>�������� �������� ������</summary>
        public abstract void InvokeProductInfo();
        /// <summary>�������� ������� �� ���������</summary>
        protected virtual void InvokeHelp()
        {
            List<FactView> prop = this.MainDocument.ProjectItem.GetCollectionFactView();
            FactView viewHelpLocation = prop.FirstOrDefault(f => f.FactNameCode == "HELPDOC" & f.ColumnCode == "HELPLINKINET");
            if (viewHelpLocation == null || string.IsNullOrWhiteSpace(viewHelpLocation.ValueString))
                XtraMessageBox.Show("���������� ���������� �����������!", Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                System.Diagnostics.Process.Start(viewHelpLocation.ValueString);
            }
        }
        /// <summary>����������� ������ ������ ����������</summary>
        protected abstract void RegisterActionToolBar();
        protected virtual void PostRegisterActionToolBar(RibbonPageGroup GroupLinksActionList)
        {
            ButtonUserActions = new BarButtonItem
            {
                Name = "btnUserActions",
                Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_ACTIONS, 1049),
                RibbonStyle = RibbonItemStyles.SmallWithText,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.INFO_X16),
                ButtonStyle = BarButtonStyle.DropDown,
                SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.INFO_X16), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_ACTIONS, 1049),
                                              Workarea.Cashe.ResourceString(ResourceString.STR_DOC_ACTIONS_TIP, 1049))
            };
            GroupLinksActionList.ItemLinks.Add(ButtonUserActions);
            PopupMenu menuActions = new PopupMenu { Ribbon = Form.Ribbon };
            //menuActions.AddItem(new BarButtonItem { Caption = "��������� ������� �� ����� ����������" });
            //menuActions.AddItem(new BarButtonItem { Caption = "������� ������ �� �����������" });
            BarItemLink lnkInfoRep = menuActions.AddItem(new BarButtonItem { Caption = "���������� � ���������" });
            lnkInfoRep.Item.ItemClick += delegate
            {
                MainDocument.ShowDocumentInfoReports();
            };
            BarItemLink lnkAdvProp = menuActions.AddItem(new BarButtonItem { Caption = "�������������� ��������" });
            lnkAdvProp.Item.ItemClick += delegate
            {
                MainDocument.ShowDocumentPropertiesAdvanced();
            };
            // �������� �������������� ���� ���������
            BarItemLink lnkCodeShow = menuActions.AddItem(new BarButtonItem { Caption = "���� ���������" });
            lnkCodeShow.Item.ItemClick += delegate
            {
                MainDocument.ShowCodes();
            };

            // �������� �������������� ���������� ���������
            BarItemLink lnkNoteShow = menuActions.AddItem(new BarButtonItem { Caption = "���������� ���������" });
            lnkNoteShow.Item.ItemClick += delegate
            {
                MainDocument.ShowNotes();
            };

            // �������� �������������� ���������� ���������
            BarItemLink lnkKnowledgeShow = menuActions.AddItem(new BarButtonItem { Caption = "���� ������ ���������" });
            lnkKnowledgeShow.Item.ItemClick += delegate
            {
                MainDocument.ShowKnowledges();
            };

            // �������� �������������� ���������� ���������
            BarItemLink lnkRightsShow = menuActions.AddItem(new BarButtonItem { Caption = "���������� ���������" });
            lnkRightsShow.Item.ItemClick += delegate
            {
                MainDocument.BrowseDocumentRights();
            };

            menuActions.AddItem(new BarButtonItem { Caption = "���������" }).BeginGroup = true;
            ButtonUserActions.DropDownControl = menuActions;   
        }
        #region ������ � ��������������� ��������
        /// <summary>������������� ��������� �������� ����</summary>
        protected virtual void InitPrintForms()
        {

        }
        /// <summary>��������������� �������� (�������� ����� �� ���������)</summary>
        // ��������� ��������
        public virtual void InvokePreview()
        {
            try
            {
                if (CollectionPrintableForms == null)
                    RegisterPrintForms(SourceDocument.Document.ProjectItemId);
                if (CollectionPrintableForms != null && CollectionPrintableForms.Count > 0)
                {
                    Print(CollectionPrintableForms[0].Id, true);
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>������ (�������� ����� �� ���������)</summary>
        public abstract void InvokePrint();

        protected virtual void Print(int id, bool withPrewiew)
        {
            if (!PrintService.IsInit)
            {
                PrintService.Workarea = Workarea;
                PrintService.InitConfig();
            }
            OnPrinting(EventArgs.Empty);
        }
        protected virtual void RegisterPrintForms(int id)
        {
            //CollectionPrintableForms = Library.GetChainSourceList(Workarea, id, DocumentViewConfig.PrintFormChainId).Where(s => s.StateId == State.STATEACTIVE).ToList();
            CollectionPrintableForms = Chain<Library>.GetChainSourceList(SourceDocument.Document.ProjectItem, DocumentViewConfig.PrintFormChainId, State.STATEACTIVE).Where(s => s.StateId == State.STATEACTIVE).ToList();
            PopupMenu popupMenuPrintForms = new PopupMenu { Ribbon = Form.Ribbon };
            popupMenuPrintForms.Manager.HighlightedLinkChanged += new HighlightedLinkChangedEventHandler(Manager_HighlightedLinkChanged);
            foreach (Library printableForm in CollectionPrintableForms)
            {
                BarButtonItem itemMnuPrint = new BarButtonItem { Caption = printableForm.Name };
                BarItemLink lnk = popupMenuPrintForms.AddItem(itemMnuPrint);
                itemMnuPrint.Tag = printableForm.Id;
                itemMnuPrint.ItemClick += ItemMnuPrintItemClick;

                //itemMnuPrint.Hint = printableForm.Memo;

                //��������� ��� ������
                itemMnuPrint.SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.PREVIEW_X32), printableForm.Name,
                                                                              printableForm.Memo);
            }
            ButtonPrint.DropDownControl = popupMenuPrintForms;
        }

        void Manager_HighlightedLinkChanged(object sender, HighlightedLinkChangedEventArgs e)
        {
            BarManager barManager1 = sender as BarManager;
            if (e.Link == null)
            {
                barManager1.GetToolTipController().HideHint();
                return;
            }
            if (!e.Link.IsLinkInMenu)
                return;
            if (e.Link.Item.SuperTip == null) return;
            ToolTipControllerShowEventArgs te = new ToolTipControllerShowEventArgs();
            te.ToolTipLocation = ToolTipLocation.Fixed;
            te.SuperTip = e.Link.Item.SuperTip;
            //te.SuperTip.Items.Add("hello");
            Point linkPoint = new Point(e.Link.Bounds.Right, e.Link.Bounds.Bottom);
            barManager1.GetToolTipController().ShowHint(te, e.Link.LinkPointToScreen(linkPoint));
            //barManager1.GetToolTipController().ShowHint(te, e.Link.LinkPointToScreen(linkPoint));
        }
        // ���������� ������� ������ "������"
        private void ItemMnuPrintItemClick(object sender, ItemClickEventArgs e)
        {
            int id = (int)e.Item.Tag;
            Print(id, true);
        }
        #endregion        
        #region �������� ����� � ������ ���������
        /// <summary>������� ����� ��������</summary>
        protected abstract void CreateNew();

        /// <summary>������� ����� ���������</summary>
        protected abstract void CreateCopy(); 
        #endregion
        #region �������� ��������

        /// <summary>�������� ��������</summary>
        /// <param name="workarea">������� �������</param>
        /// <param name="ownerList">������ ��������</param>
        /// <param name="id">������������� ���������</param>
        /// <param name="documentTemplateId">������������� �������</param>
        /// <param name="parentDocId">������������� ������������� ���������</param>
        /// <remarks>��� ������������� ��������� parentDocId ����� ������� ����� � ������������ ����������</remarks>
        ///
        public virtual void Show(Workarea workarea, object ownerList, int id, int documentTemplateId, int parentDocId = 0, bool hidden = false)
        {
            ParentId = parentDocId;
            _isRequestParentDocId = (ParentId != 0);
            OwnerList = ownerList as BindingSource;
            Workarea = workarea;
            Id = id;
            DocumentTemplateId = documentTemplateId;
            Form = new FormProperties
            {
                Ribbon = { ApplicationIcon = ResourceImage.GetByCode(workarea, ResourceImage.DOCUMENTDONE_X16) }
            };
            Form.Icon = Icon.FromHandle(Form.Ribbon.ApplicationIcon.GetHicon());

            Form.Ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Show;
            ButtonHelp = Extentions.CreateHelpButton(Form.Ribbon, Workarea);
            ButtonHelp.ItemClick += ButtonHelpItemClick;
            Extentions.CreateOpenWindowsButton(Form.Ribbon, Workarea);
            Build();
            OnShowing(EventArgs.Empty);
            if (!hidden) 
                Form.Show();
            Form.Disposed += delegate
            {
                if (OpennedDocs != null)
                    for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                        OpennedDocs[i].Kill();
            };
        }

        void ButtonHelpItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeHelp();
        } 
        #endregion

        #region �������
        [NonSerialized]
        private EventHandler _showingHandlers;
        /// <summary>������� ����������� ���������</summary>
        public event EventHandler Showing
        {
            add
            {
                _showingHandlers = (EventHandler)
                                   Delegate.Combine(_showingHandlers, value);
            }
            remove
            {
                _showingHandlers = (EventHandler)
                                   Delegate.Remove(_showingHandlers, value);
            }
        }

        protected virtual void OnShowing(EventArgs e)
        {
            if (_showingHandlers != null)
                _showingHandlers.Invoke(this, e);
        }

        [NonSerialized]
        private EventHandler _printingHandlers;
        /// <summary>������� ������ ���������</summary>
        public event EventHandler Printing
        {
            add
            {
                _printingHandlers = (EventHandler)
                                   Delegate.Combine(_printingHandlers, value);
            }
            remove
            {
                _printingHandlers = (EventHandler)
                                   Delegate.Remove(_printingHandlers, value);
            }
        }

        protected virtual void OnPrinting(EventArgs e)
        {
            if (_printingHandlers != null)
                _printingHandlers.Invoke(this, e);
        }

        [NonSerialized]
        private EventHandler _loadingHandlers;
        /// <summary>������� �������� ���������</summary>
        public event EventHandler Loading
        {
            add
            {
                _loadingHandlers = (EventHandler)
                                   Delegate.Combine(_loadingHandlers, value);
            }
            remove
            {
                _printingHandlers = (EventHandler)
                                   Delegate.Remove(_loadingHandlers, value);
            }
        }

        protected virtual void OnLoading(EventArgs e)
        {
            if (_loadingHandlers != null)
                _loadingHandlers.Invoke(this, e);
        }

        [NonSerialized]
        private EventHandler _validatingHandlers;
        /// <summary>������� �������� ���������</summary>
        public event EventHandler Validating
        {
            add
            {
                _validatingHandlers = (EventHandler)
                                   Delegate.Combine(_validatingHandlers, value);
            }
            remove
            {
                _validatingHandlers = (EventHandler)
                                   Delegate.Remove(_validatingHandlers, value);
            }
        }

        protected virtual void OnValidating(EventArgs e)
        {
            if (_validatingHandlers != null)
                _validatingHandlers.Invoke(this, e);
        }

        [NonSerialized]
        private EventHandler _closingHandlers;
        /// <summary>������� �������� ���������</summary>
        public event EventHandler Closing
        {
            add
            {
                _closingHandlers = (EventHandler)
                                   Delegate.Combine(_closingHandlers, value);
            }
            remove
            {
                _closingHandlers = (EventHandler)
                                   Delegate.Remove(_closingHandlers, value);
            }
        }

        protected virtual void OnClosing(EventArgs e)
        {
            if (_closingHandlers != null)
                _closingHandlers.Invoke(this, e);
        } 
        #endregion

        #region �������������� ������

        internal virtual void CreateDocuments(DocChain v, IDocumentView parentForm=null)
        {
            string code = v.Code.ToUpper();
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ApplicationException("�� ������ ������� ������� �������� ���������!");
            }
            // TODO: ������������ ����� ���������� ���������, � ����� ������� ���������� ����������
            /* 
             * 
            Ruleset obj = Workarea.Cashe.GetCasheData<Ruleset>().ItemCode<Ruleset>(code);
            if(obj==null)
                throw new ApplicationException("�� ������ ������� ������� �������� ���������!");
            Activity mathWF = ActivityXamlServices.Load(obj.ValueToStream());
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(mathWF, new Dictionary<string, object>
                    {
                        { "document", SourceDocument },
                        { "templateId", v.Id }
                    });
            if (outputs.ContainsKey("returnValue"))
            {
                object val = outputs["returnValue"];
                if (val != null)
                {
                    ((val as IDocument)).ShowDocument(Id);
                }
            }
             */
            Activity value = Workflows.WfCore.FindByCode(Workarea, code);
            if(value==null)
                value = Workflows.WfCore.FindByCodeInternal(code);
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(value, new Dictionary<string, object>
                    {
                        { "document", SourceDocument },
                        { "templateId", v.Id }
                    });
            if (outputs.ContainsKey("returnValue"))
            {
                object val = outputs["returnValue"];
                if (val != null)
                {
                    ((val as IDocument)).ShowDocument(Id, parentForm);
                    //parentForm
                }
            }
        }
        internal virtual void InvokeWorkflow(string code)
        {
            Activity value = Workflows.WfCore.FindByCodeInternal(code);
            if (value == null)
                 value = Workflows.WfCore.FindByCode(Workarea, code);
            //Activity value = Workflows.WfCore.FindByCode(Workarea, code);
            //if(value==null)
            //    value = Workflows.WfCore.FindByCodeInternal(code);
            if(value!=null)
            {
                // TODO: ������!!!
                IDictionary<string, object> outputs = WorkflowInvoker.Invoke(value, new Dictionary<string, object>
                                                                                        {
                                                                                            {"document", SourceDocument}
                                                                                        });
            }
        }
        /// <summary>
        /// ��������� �������������� �������� � ���������
        /// </summary>
        /// <param name="code"></param>
        /// <param name="currentObject"></param>
        internal virtual void InvokeWorkflowAction(string code, object currentObject)
        {
            Activity value = Workflows.WfCore.FindByCode(Workarea, code);
            if (value == null)
                value = Workflows.WfCore.FindByCodeInternal(code);
            if (value != null)
            {
                IDictionary<string, object> outputs = WorkflowInvoker.Invoke(value, new Dictionary<string, object>
                                                                                        {
                                                                                            {"DocumentView", this },
                                                                                            {"CurrentObject", currentObject}
                                                                                        });
            }
        }
        internal void InvokeShowDocument(BindingSource bindingDocument)
        {
            if (bindingDocument.Current == null) return;
            Document op = bindingDocument.Current as Document;
            if (op == null)
            {
                DataRowView rv = bindingDocument.Current as DataRowView;
                if (rv != null)
                {
                    int docid = (int)rv[GlobalPropertyNames.Id];
                    op = Workarea.Cashe.GetCasheData<Document>().Item(docid);
                }
            }
            if (op == null) return;
            if (op.ProjectItemId == 0) return;
            Library lib = op.ProjectItem;
            int referenceLibId = Library.GetLibraryIdByContent(Workarea, lib.LibraryTypeId);
            Library referenceLib = Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
            LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

            Assembly ass = Library.GetAssemblyFromGac(referenceLib);
            if (ass == null)
            {
                string assFile = Path.Combine(Application.StartupPath,
                                              referenceLib.AssemblyDll.NameFull);
                if (!File.Exists(assFile))
                {
                    using (
                        FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                    {
                        stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                     referenceLib.AssemblyDll.StreamData.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                }
                ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                      Assembly.LoadFile(assFile);
            }
            Type type = ass.GetType(cnt.FullTypeName);
            if (type == null) return;
            object objectContentModule = Activator.CreateInstance(type);
            IDocumentView formModule = objectContentModule as IDocumentView;
            formModule.Show(Workarea, bindingDocument, op.Id, 0);
        }
        private bool _isRequestParentDocId;
        protected void CreateChainToParentDoc()
        {
            if (ParentId == 0) return;

            if (_isRequestParentDocId)
            {
                Chain<Document> chain = new Chain<Document>()
                                            {
                                                Workarea = Workarea,
                                                LeftId = ParentId,
                                                Right = MainDocument,
                                                KindId = 20,
                                                StateId = State.STATEACTIVE
                                            };
                chain.Save();
                _isRequestParentDocId = false;
            }
        }
        /// <summary>���������� ��������� ����</summary>
        protected virtual void SetCaption()
        {
            Form.Text = string.Format("{0} ����� {1} �� {2}", MainDocument.Name, MainDocument.Number, MainDocument.Date.ToShortDateString());
        }
        /// <summary>��������� ����������� ��������</summary>
        protected virtual void SetMinsize()
        {
            new FormStateMaintainer(Form, string.Format("Property{0}", GetType().Name));
            //int maxWith = (Form.clientPanel.Width.CompareTo(Control.MinimumSize.Width) > 0) ? Form.clientPanel.Width : Control.MinimumSize.Width;
            //int maxHeight = (Form.clientPanel.Height.CompareTo(Control.MinimumSize.Height) > 0) ? Form.clientPanel.Height : Control.MinimumSize.Height;
            //Size mix = (Form.Size - Form.clientPanel.Size) + new Size(maxWith, maxHeight);
            //Form.MinimumSize = mix;
        }
        /// <summary>��������� ��� ������ � ��������� ����������</summary>
        /// <param name="image">�����������</param>
        /// <param name="caption">���������</param>
        /// <param name="text">�����</param>
        /// <returns></returns>
        protected SuperToolTip CreateSuperToolTip(Image image, string caption, string text)
        {
            SuperToolTip superToolTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };
            ToolTipTitleItem toolTipTitle = new ToolTipTitleItem { Text = caption };
            ToolTipItem toolTipItem = new ToolTipItem { LeftIndent = 6, Text = text };
            toolTipItem.Appearance.Image = image;
            toolTipItem.Appearance.Options.UseImage = true;
            superToolTip.Items.Add(toolTipTitle);
            superToolTip.Items.Add(toolTipItem);

            return superToolTip;
        }
        /// <summary>����������� ������ ��� ������� ���������</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAgents"></param>
        internal static void DisplayDeviceImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Device imageItem = bindSourceAgents[e.ListSourceRowIndex] as Device;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Device imageItem = bindSourceAgents[e.ListSourceRowIndex] as Device;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>����������� ������ ��� ������� ���������� ��������</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAgents"></param>
        internal static void DisplayRouteMemberImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                RouteMember imageItem = bindSourceAgents[e.ListSourceRowIndex] as RouteMember;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                RouteMember imageItem = bindSourceAgents[e.ListSourceRowIndex] as RouteMember;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        
        /// <summary>����������� ������ ��� ������� ���������������</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAgents"></param>
        internal static void DisplayAgentImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Agent imageItem = bindSourceAgents[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>����������� ������ ��� ������� �������</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAgents"></param>
        internal static void DisplayDepatmentImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAgents)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Depatment imageItem = bindSourceAgents[e.ListSourceRowIndex] as Depatment;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAgents.Count > 0)
            {
                Depatment imageItem = bindSourceAgents[e.ListSourceRowIndex] as Depatment;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>����������� ������ ��� ������� ��������� ������</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceBank"></param>
        internal static void DisplayBankAccountImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceBank)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceBank.Count > 0)
            {
                AgentBankAccount imageItem = bindSourceBank[e.ListSourceRowIndex] as AgentBankAccount;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceBank.Count > 0)
            {
                AgentBankAccount imageItem = bindSourceBank[e.ListSourceRowIndex] as AgentBankAccount;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        /// <summary>����������� ������ ��� ������� ���������������</summary>
        /// <param name="e"></param>
        /// <param name="bindSourceAnalitics"></param>
        internal static void DisplayAnaliticImagesLookupGrid(CustomColumnDataEventArgs e, BindingSource bindSourceAnalitics)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceAnalitics.Count > 0)
            {
                Analitic imageItem = bindSourceAnalitics[e.ListSourceRowIndex] as Analitic;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceAnalitics.Count > 0)
            {
                Analitic imageItem = bindSourceAnalitics[e.ListSourceRowIndex] as Analitic;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        internal NavBarViewInfo GetViewInfo(NavBarControl navBar)
        {
            FieldInfo fi = typeof(NavBarControl).GetField("viewInfo", BindingFlags.NonPublic |
        BindingFlags.Instance);
            return fi.GetValue(navBar) as NavBarViewInfo;
        }

        internal ExplorerBarNavGroupPainter GetGroupPainter(NavBarControl navBar)
        {
            FieldInfo fi = typeof(NavBarControl).GetField("groupPainter", BindingFlags.NonPublic |
        BindingFlags.Instance);
            return fi.GetValue(navBar) as ExplorerBarNavGroupPainter;
        }
        #endregion
        
    }
}
