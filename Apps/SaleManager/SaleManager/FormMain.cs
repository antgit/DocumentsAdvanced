using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace SaleManager
{
    public partial class FormMain : RibbonForm
    {
        bool IsLoading = true;
        public FormMain()
        {
            IsLoading = true;
            InitializeComponent();
            InitSkinGallery();
            InitGrid();
            this.Load += new EventHandler(FormMain_Load);
            this.Shown += new EventHandler(FormMainShown);
            InitWa();
            IsLoading = false;
        }

        void FormMain_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            string random_item = SaleManager.Helper.WebPages[rand.Next(SaleManager.Helper.WebPages.Count-1)];

            webBrowser.Navigate(random_item);
        }

        void FormMainShown(object sender, EventArgs e)
        {
            while (webBrowser.IsBusy || IsLoading)
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
            }
            //webBrowser.Document.ContextMenuShowing += new HtmlElementEventHandler(Document_ContextMenuShowing);
            Cursor = Cursors.Default;
        }
        void InitWa()
        {
            Workarea wa = SaleManager.Helper.WA;

        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }
        void InitGrid()
        {
            _documentsBindingSource = new BindingSource();
            splitContainerControl.Panel2.Controls.Clear();
            GridDocuments.DataSource = _documentsBindingSource;
            GridDocuments.Dock = DockStyle.Fill;
            DataGridViewHelper.GenerateGridColumns(SaleManager.Helper.WA, ViewDocuments,
                                                               "DEFAULT_LISTVIEWDOCUMENT");
            ViewDocuments.OptionsView.ShowAutoFilterRow = true;
            ViewDocuments.OptionsView.ShowGroupPanel = true;

            ViewDocuments.CustomUnboundColumnData += ViewCustomUnboundColumnData;
            ViewDocuments.DoubleClick += delegate
            {
                Point p = GridDocuments.PointToClient(Control.MousePosition);
                GridHitInfo hit = ViewDocuments.CalcHitInfo(p.X, p.Y);
                if (hit.InRow)
                {
                    InvokeShowDocument();
                }
            };

            webBrowser.Dock = DockStyle.Fill;
            webBrowser.IsWebBrowserContextMenuEnabled = false; 
            splitContainerControl.Panel2.Controls.Add(webBrowser);
        }

        

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private ListBrowserBaseObjects<Product> browserProducts;
        private ListBrowserBaseObjects<Agent> browserClients;
        private ListBrowserBaseObjects<Agent> browserBanks;
        private ListBrowserBaseObjects<Agent> browserSupplyers;
        private ListBrowserBaseObjects<Agent> browserMyCompanies;
        private ListBrowserBaseObjects<Agent> browserMyStores;
        private ListBrowserBaseObjects<Agent> browserMyWorker;

        private BindingSource _documentsBindingSource;
        private void NavItemProducts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (browserProducts ==null) 
            {
                browserProducts = new ListBrowserBaseObjects<Product>(SaleManager.Helper.WA, null,
                                                                                          null, null, true, false, false,
                                                                                          true);
                browserProducts.ListViewCode = "SALEMANAGER_PRODUCTVIEW";
                browserProducts.ShowProperiesOnDoudleClick = true;
                browserProducts.Build();
                browserProducts.ListControl.Dock = DockStyle.Fill;
                
                browserProducts.ShowProperty += delegate(Product obj)
                {
                    obj.ShowWizard();
                    if (obj.IsNew)
                    {
                        obj.Created += delegate
                        {
                            int position = browserProducts.BindingSource.Add(obj);
                            browserProducts.BindingSource.Position = position;
                        };
                    }
                };
            }
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(browserProducts.ListControl);
            SaleManager.Helper.CurrentSection = Section.Product;
            SetCurrentInfo();
        }
        private void navItemBanks_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (browserBanks == null)
            {
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BANKS);
                List<Agent> collagents = rootClients.GetTypeContents<Agent>(true);
                browserBanks = new ListBrowserBaseObjects<Agent>(SaleManager.Helper.WA, collagents,
                                                                                          null, null, true, false, false,
                                                                                          true);
                browserBanks.ListViewCode = "SALEMANAGER_BANKVIEW";
                browserBanks.ShowProperiesOnDoudleClick = true;
                browserBanks.Build();
                browserBanks.ListControl.Dock = DockStyle.Fill;

                browserBanks.ShowProperty += delegate(Agent obj)
                {
                    obj.ShowWizard();
                    if (obj.IsNew)
                    {
                        obj.Created += delegate
                        {
                            int position = browserBanks.BindingSource.Add(obj);
                            browserBanks.BindingSource.Position = position;
                        };
                    }
                };
            }
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(browserBanks.ListControl);

            SaleManager.Helper.CurrentSection = Section.Banks;
            SetCurrentInfo();
        }
        private void NavItemClients_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (browserClients == null)
            { 
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BUYERS);
                List<Agent> collagents = rootClients.GetTypeContents<Agent>(true);
                browserClients = new ListBrowserBaseObjects<Agent>(SaleManager.Helper.WA, collagents,
                                                                                          null, null, true, false, false,
                                                                                          true);
                browserClients.ListViewCode = "SALEMANAGER_AGENTVIEW";
                browserClients.ShowProperiesOnDoudleClick = true;
                browserClients.Build();
                browserClients.ListControl.Dock = DockStyle.Fill;

                browserClients.ShowProperty += delegate(Agent obj)
                {
                    obj.ShowWizard();
                    if (obj.IsNew)
                    {
                        obj.Created += delegate
                        {
                            int position = browserClients.BindingSource.Add(obj);
                            browserClients.BindingSource.Position = position;
                        };
                    }
                };
            }
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(browserClients.ListControl);

            SaleManager.Helper.CurrentSection = Section.Clients;
            SetCurrentInfo();
        }

        private void NavItemSupplyer_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (browserSupplyers == null)
            { 
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_SUPPLIERS);
                List<Agent> collagents = rootClients.GetTypeContents<Agent>(true);
                browserSupplyers = new ListBrowserBaseObjects<Agent>(SaleManager.Helper.WA, collagents,
                                                                                          null, null, true, false, false,
                                                                                          true);
                browserSupplyers.ListViewCode = "SALEMANAGER_AGENTVIEW";
                browserSupplyers.ShowProperiesOnDoudleClick = true;
                browserSupplyers.Build();
                browserSupplyers.ListControl.Dock = DockStyle.Fill;

                browserSupplyers.ShowProperty += delegate(Agent obj)
                {
                    obj.ShowWizard();
                    if (obj.IsNew)
                    {
                        obj.Created += delegate
                        {
                            int position = browserSupplyers.BindingSource.Add(obj);
                            browserSupplyers.BindingSource.Position = position;
                        };
                    }
                };
            }
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(browserSupplyers.ListControl);

            SaleManager.Helper.CurrentSection = Section.Supplyer;
            SetCurrentInfo();
        }

        private void NavItemMyCompanies_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (browserMyCompanies == null)
            {
                Hierarchy hRoot = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYCOMPANY);


                Agent agHolding = SaleManager.Helper.WA.Cashe.GetCasheData<Agent>().Item(hRoot.GetTypeContents<Agent>()[0].Id);
                int DepatmentChainId = SaleManager.Helper.WA.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TREE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;



                List<Agent> coll = Chain<Agent>.GetChainSourceList(agHolding, DepatmentChainId);
                browserMyCompanies = new ListBrowserBaseObjects<Agent>(SaleManager.Helper.WA, coll,
                                                                                          null, null, true, false, false,
                                                                                          true);
                browserMyCompanies.ListViewCode = "SALEMANAGER_AGENTVIEW";
                browserMyCompanies.ShowProperiesOnDoudleClick = true;
                browserMyCompanies.Build();
                browserMyCompanies.ListControl.Dock = DockStyle.Fill;

                browserMyCompanies.ShowProperty += delegate(Agent obj)
                {
                    obj.ShowWizard();
                    if (obj.IsNew)
                    {
                        obj.Created += delegate
                        {
                            int position = browserMyCompanies.BindingSource.Add(obj);
                            browserMyCompanies.BindingSource.Position = position;
                        };
                    }
                };
            }
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(browserMyCompanies.ListControl);

            SaleManager.Helper.CurrentSection = Section.MyCompany;
            SetCurrentInfo();
        }

        private void NavItemStores_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (browserMyStores == null)
            { 
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYSTORES);
                List<Agent> collagents = rootClients.GetTypeContents<Agent>(true);
                browserMyStores = new ListBrowserBaseObjects<Agent>(SaleManager.Helper.WA, collagents,
                                                                                          null, null, true, false, false,
                                                                                          true);
                browserMyStores.ListViewCode = "SALEMANAGER_AGENTVIEW";
                browserMyStores.ShowProperiesOnDoudleClick = true;
                browserMyStores.Build();
                browserMyStores.ListControl.Dock = DockStyle.Fill;

                browserMyStores.ShowProperty += delegate(Agent obj)
                {
                    obj.ShowWizard();
                    if (obj.IsNew)
                    {
                        obj.Created += delegate
                        {
                            int position = browserMyStores.BindingSource.Add(obj);
                            browserMyStores.BindingSource.Position = position;
                        };
                    }
                };
            }
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(browserMyStores.ListControl);

            SaleManager.Helper.CurrentSection = Section.Store;
            SetCurrentInfo();
        }

        private void navItemWorkers_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {//
            if (browserMyWorker == null)
            {
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYWORKERS);
                List<Agent> collagents = rootClients.GetTypeContents<Agent>(true);
                browserMyWorker = new ListBrowserBaseObjects<Agent>(SaleManager.Helper.WA, collagents,
                                                                                          null, null, true, false, false,
                                                                                          true);
                browserMyWorker.ListViewCode = "SALEMANAGER_WORKERVIEW";
                browserMyWorker.ShowProperiesOnDoudleClick = true;
                browserMyWorker.Build();
                browserMyWorker.ListControl.Dock = DockStyle.Fill;

                browserMyWorker.ShowProperty += delegate(Agent obj)
                {
                    obj.ShowWizard();
                    if (obj.IsNew)
                    {
                        obj.Created += delegate
                        {
                            int position = browserMyWorker.BindingSource.Add(obj);
                            browserMyWorker.BindingSource.Position = position;
                        };
                    }
                };
            }
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(browserMyWorker.ListControl);

            SaleManager.Helper.CurrentSection = Section.Worker;
            SetCurrentInfo();
        }

        private void NavItemDocOut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Folder f = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_SALES_OUT_NDS];
            RefreshDocumentList(f.Id);
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(GridDocuments);
            SaleManager.Helper.CurrentSection = Section.DocProductOut;
            SetCurrentInfo();
        }

        private void NavItemDocMoneyIn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Folder f = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_FINANCE_IN];
            RefreshDocumentList(f.Id);
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(GridDocuments);
            SaleManager.Helper.CurrentSection = Section.DocMoneyIn;
            SetCurrentInfo();
        }

        private void NavItemDocIn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Folder f = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_SALES_IN_NDS];
            RefreshDocumentList(f.Id);
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(GridDocuments);
            SaleManager.Helper.CurrentSection = Section.DocProductIn;
        }

        private void NavItemDocMoneyOut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Folder f = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_FINANCE_OUT];
            RefreshDocumentList(f.Id);
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(GridDocuments);
            SaleManager.Helper.CurrentSection = Section.DocMoneyOut;
            SetCurrentInfo();
        }

        private void NavItemDocPrice_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Folder f = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_PRICE];
            RefreshDocumentList(f.Id);
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(GridDocuments);
            SaleManager.Helper.CurrentSection = Section.DocPrice;
        }

        private void NavItemDocDrafts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            splitContainerControl.Panel2.Controls.Clear();
            RefreshDocumentListFind(HierarchyCodeKind.NotDone);
            splitContainerControl.Panel2.Controls.Add(GridDocuments);
            SaleManager.Helper.CurrentSection = Section.DocNotDone;
            SetCurrentInfo();
        }

        private void NavItemDocTrash_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            splitContainerControl.Panel2.Controls.Clear();
            RefreshDocumentListFind(HierarchyCodeKind.Trash);
            splitContainerControl.Panel2.Controls.Add(GridDocuments);
            SaleManager.Helper.CurrentSection = Section.DocTrash;
            SetCurrentInfo();
        }

        private void iAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            splitContainerControl.Panel2.Controls.Clear();
            splitContainerControl.Panel2.Controls.Add(webBrowser);
            SaleManager.Helper.CurrentSection = Section.Help;
            SetCurrentInfo();
        }
        private void iHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://atlan.com.ua/documentsmini.aspx");
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void iNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            // определение текущего расположения (навигации) 
            if(SaleManager.Helper.CurrentSection== Section.Product)
            {
                CreateNewProduct();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Clients)
            {
                CreateNewClient();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Banks)
            {
                CreateNewBank();
            }
            else if (SaleManager.Helper.CurrentSection == Section.MyCompany)
            {
                CreateNewMyCompany();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Store)
            {
                CreateNewStore();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Worker)
            {
                CreateNewWorker();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Supplyer)
            {
                CreateNewSupplyer();
            }
            else if (SaleManager.Helper.CurrentSection == Section.DocMoneyIn)
            {
                CreateNewDocMoneyIn();
            }
            else if (SaleManager.Helper.CurrentSection == Section.DocMoneyOut)
            {
                CreateNewDocMoneyOut();
            }
            else if (SaleManager.Helper.CurrentSection == Section.DocPrice)
            {
                CreateNewDocPrice();
            }
            else if (SaleManager.Helper.CurrentSection == Section.DocProductIn)
            {
                CreateNewDocProductIn();
            }
            else if (SaleManager.Helper.CurrentSection == Section.DocProductOut)
            {
                CreateNewDocProductOut();
            }
        }


        private void InvokeShowDocument()
        {
            if (_documentsBindingSource.Current == null) return;
            int[] selectedRows = ViewDocuments.GetSelectedRows();
            Document row = ViewDocuments.GetRow(selectedRows[0]) as Document;
            if (row==null)
            {
                DataRowView rv = ViewDocuments.GetRow(selectedRows[0]) as DataRowView;
                if (rv != null)
                {
                    int docid = (int)rv[GlobalPropertyNames.Id];
                    row = SaleManager.Helper.WA.GetObject<Document>(docid);
                }
            }
                if(row.FolderId== SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_SALES_OUT_NDS].Id)
                {
                    DocumentSales doc = new DocumentSales();
                    doc.Workarea = SaleManager.Helper.WA;
                    doc.Id = row.Id;
                    doc.Load(doc.Id);
                    SaleManager.Helper.ShowDocumentOut(doc,
                                                       SaleManager.Helper.FolderValue[
                                                           SaleManager.Helper.FLD_SALES_OUT_NDS]);
                }

                if (row.FolderId == SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_FINANCE_OUT].Id)
                {
                    DocumentFinance doc = new DocumentFinance();
                    doc.Workarea = SaleManager.Helper.WA;
                    doc.Id = row.Id;
                    doc.Load(doc.Id);
                    SaleManager.Helper.ShowDocumentMoneyOut(doc,
                                                       SaleManager.Helper.FolderValue[
                                                           SaleManager.Helper.FLD_FINANCE_OUT]);
                }
                
            
        }
        private void CreateNewDocProductOut()
        {
            DocumentSales doc = new DocumentSales(){ Workarea = SaleManager.Helper.WA, Date = DateTime.Today};

            Folder fld = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_SALES_OUT_NDS];
            
            SaleManager.Helper.ShowDocumentOut(doc, fld);
        }

        private void CreateNewDocProductIn()
        {
            DocumentSales doc = new DocumentSales() { Workarea = SaleManager.Helper.WA, Date = DateTime.Today };
            Folder fld = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_SALES_IN_NDS];

            SaleManager.Helper.ShowDocumentIn(doc, fld);
        }

        private void CreateNewDocPrice()
        {
            throw new NotImplementedException();
        }

        private void CreateNewDocMoneyOut()
        {
            DocumentFinance doc = new DocumentFinance() { Workarea = SaleManager.Helper.WA, Date = DateTime.Today };
            Folder fld = SaleManager.Helper.FolderValue[SaleManager.Helper.FLD_FINANCE_OUT];

            SaleManager.Helper.ShowDocumentMoneyOut(doc, fld);
        }

        private void CreateNewDocMoneyIn()
        {
            throw new NotImplementedException();
        }

        private void CreateNewStore()
        {
            Agent tmlObj =
                SaleManager.Helper.WA.GetTemplates<Agent>().First(f => f.KindValue == Agent.KINDVALUE_STORE);
            Agent newObj = SaleManager.Helper.WA.CreateNewObject<Agent>(tmlObj);
            newObj.ShowWizard();
            if (newObj.Id != 0)
            {
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYSTORES);
                rootClients.ContentAdd<Agent>(newObj);
                int position = browserMyStores.BindingSource.Add(newObj);
                browserMyStores.BindingSource.Position = position;
                
            }
        }
        private void CreateNewWorker()
        {
            Agent tmlObj =
                SaleManager.Helper.WA.GetTemplates<Agent>().First(f => f.KindValue == Agent.KINDVALUE_PEOPLE);
            Agent newObj = SaleManager.Helper.WA.CreateNewObject<Agent>(tmlObj);
            newObj.ShowWizard();
            if (newObj.Id != 0)
            {
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYWORKERS);
                rootClients.ContentAdd<Agent>(newObj);
                int position = browserMyStores.BindingSource.Add(newObj);
                browserMyStores.BindingSource.Position = position;

            }
        }

        private void CreateNewMyCompany()
        {
            Agent tmlObj =
                SaleManager.Helper.WA.GetTemplates<Agent>().First(f => f.KindValue == Agent.KINDVALUE_MYCOMPANY);
            Agent newObj = SaleManager.Helper.WA.CreateNewObject<Agent>(tmlObj);
            newObj.ShowWizard();
            if (newObj.Id != 0)
            {
                Hierarchy hRoot = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYCOMPANY);


                Agent agHolding = SaleManager.Helper.WA.Cashe.GetCasheData<Agent>().Item(hRoot.GetTypeContents<Agent>()[0].Id);
                int DepatmentChainId = SaleManager.Helper.WA.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TREE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                
                Chain<Agent> link = new Chain<Agent>(agHolding) { RightId = newObj.Id, KindId = DepatmentChainId, StateId = State.STATEACTIVE };
                link.Save();
                
                int position = browserMyCompanies.BindingSource.Add(newObj);
                browserMyCompanies.BindingSource.Position = position;
            }
        }
        private void CreateNewSupplyer()
        {
            Agent tmlObj =
                SaleManager.Helper.WA.GetTemplates<Agent>().First(f => f.KindValue == Agent.KINDVALUE_COMPANY);
            Agent newObj = SaleManager.Helper.WA.CreateNewObject<Agent>(tmlObj);
            newObj.ShowWizard();
            if (newObj.Id != 0)
            {
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_SUPPLIERS);
                rootClients.ContentAdd<Agent>(newObj);
                
                int position = browserSupplyers.BindingSource.Add(newObj);
                browserSupplyers.BindingSource.Position = position;
            }
        }

        private void CreateNewClient()
        {
            Agent tmlObj =
                SaleManager.Helper.WA.GetTemplates<Agent>().First(f => f.KindValue == Agent.KINDVALUE_COMPANY);
            Agent newObj = SaleManager.Helper.WA.CreateNewObject<Agent>(tmlObj);
            newObj.ShowWizard();
            if (newObj.Id != 0)
            {
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BUYERS);
                rootClients.ContentAdd<Agent>(newObj);
                int position = browserClients.BindingSource.Add(newObj);
                browserClients.BindingSource.Position = position;
            }
        }

        private void CreateNewBank()
        {
            Agent tmlObj =
                SaleManager.Helper.WA.GetTemplates<Agent>().First(f => f.KindValue == Agent.KINDVALUE_BANK);
            Agent newObj = SaleManager.Helper.WA.CreateNewObject<Agent>(tmlObj);
            newObj.ShowWizard();
            if (newObj.Id != 0)
            {
                Hierarchy rootClients = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_BANKS);
                rootClients.ContentAdd<Agent>(newObj);
                int position = browserBanks.BindingSource.Add(newObj);
                browserBanks.BindingSource.Position = position;
            }
        }

        private void CreateNewProduct()
        {
            Product tmlProduct =
                SaleManager.Helper.WA.GetTemplates<Product>().First(f => f.KindValue == Product.KINDVALUE_PRODUCT);
            Product newProduct = SaleManager.Helper.WA.CreateNewObject<Product>(tmlProduct);
            newProduct.ShowWizard();
            if (newProduct.Id != 0)
            {
                int position = browserProducts.BindingSource.Add(newProduct);
                browserProducts.BindingSource.Position = position;
            }
        }

        private void iDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            // определение текущего расположения (навигации) 
            if (SaleManager.Helper.CurrentSection == Section.Product)
            {
                browserProducts.InvokeDelete();
            }
            else if(SaleManager.Helper.CurrentSection == Section.Clients)
            {
                browserClients.InvokeDelete();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Supplyer)
            {
                browserSupplyers.InvokeDelete();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Store)
            {
                browserMyStores.InvokeDelete();
            }
            else if (SaleManager.Helper.CurrentSection == Section.MyCompany)
            {
                browserMyCompanies.InvokeDelete();
            }
            else if(SaleManager.Helper.CurrentSection== Section.DocMoneyIn ||
                SaleManager.Helper.CurrentSection== Section.DocMoneyOut ||
                SaleManager.Helper.CurrentSection== Section.DocPrice ||
                SaleManager.Helper.CurrentSection== Section.DocProductIn ||
                SaleManager.Helper.CurrentSection== Section.DocProductOut ||
                SaleManager.Helper.CurrentSection== Section.DocNotDone)
            {
                InvokeDocumentDelete();
            }
        }

        

        private void RefreshDocumentList(int folderId)
        {
            List<Document> collectionDocuments =
                        Document.GetCollectionDocumentByFolder(SaleManager.Helper.WA, folderId).Where(s => s.StateId != 5).ToList();
            _documentsBindingSource.DataSource = collectionDocuments;
        }

        private void RefreshDocumentListFind(HierarchyCodeKind kind)
        {
            string code = Hierarchy.GetSystemFindDocumentCodeValue(kind);
            Hierarchy element = SaleManager.Helper.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(code);
            DataTable tbl = SaleManager.Helper.WA.GetDocumetsListByListView(element.ViewListDocumentsId,
                                                                            (int) WhellKnownDbEntity.Folder, element.Id);
            string Filter = string.Empty;
            foreach (Folder value in SaleManager.Helper.FolderValue.Values)
            {
                if (Filter.Length > 0)
                    Filter = Filter + " or ";
                Filter = Filter + " FolderId = " + value.Id + " ";
                
            }
            tbl.DefaultView.RowFilter = Filter;
            _documentsBindingSource.DataSource = tbl;
        }

        protected void ViewCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                Document imageItem = _documentsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];

                        e.Value = ExtentionsImage.GetImageDocument(SaleManager.Helper.WA, stId);
                    }
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                Document imageItem = _documentsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        e.Value = ExtentionsImage.GetImageState(SaleManager.Helper.WA, stId);
                    }
                }
            }
        }

        private void iFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (SaleManager.Helper.CurrentSection == Section.MyCompany)
            {
                if (browserMyCompanies.GridView.IsFindPanelVisible)
                    browserMyCompanies.GridView.HideFindPanel();
                else
                    browserMyCompanies.GridView.ShowFindPanel();
            }
            if(SaleManager.Helper.CurrentSection== Section.Product)
            {
                if (browserProducts.GridView.IsFindPanelVisible)
                    browserProducts.GridView.HideFindPanel();
                else
                    browserProducts.GridView.ShowFindPanel();
            }
            else if (SaleManager.Helper.CurrentSection == Section.Clients)
            {
                if (browserClients.GridView.IsFindPanelVisible)
                    browserClients.GridView.HideFindPanel();
                else
                    browserClients.GridView.ShowFindPanel();
            }
            if (SaleManager.Helper.CurrentSection == Section.Store)
            {
                if (browserMyStores.GridView.IsFindPanelVisible)
                    browserMyStores.GridView.HideFindPanel();
                else
                    browserMyStores.GridView.ShowFindPanel();
            }
            if (SaleManager.Helper.CurrentSection == Section.Supplyer)
            {
                if (browserSupplyers.GridView.IsFindPanelVisible)
                    browserSupplyers.GridView.HideFindPanel();
                else
                    browserSupplyers.GridView.ShowFindPanel();
            }
            if (SaleManager.Helper.CurrentSection == Section.Worker)
            {
                if (browserMyWorker.GridView.IsFindPanelVisible)
                    browserMyWorker.GridView.HideFindPanel();
                else
                    browserMyWorker.GridView.ShowFindPanel();
            }
            if (SaleManager.Helper.CurrentSection == Section.DocMoneyIn ||
                SaleManager.Helper.CurrentSection == Section.DocMoneyOut ||
                SaleManager.Helper.CurrentSection == Section.DocPrice ||
                SaleManager.Helper.CurrentSection == Section.DocProductIn ||
                SaleManager.Helper.CurrentSection == Section.DocProductOut)
            {
                if (ViewDocuments.IsFindPanelVisible)
                    ViewDocuments.HideFindPanel();
                else
                    ViewDocuments.ShowFindPanel();
            }
        }
        protected void InvokeDocumentDelete()
        {
            if (_documentsBindingSource.Current == null) return;
            int[] rows = ViewDocuments.GetSelectedRows();

            if (rows == null) return;
            int res = Extentions.ShowMessageChoice(SaleManager.Helper.WA,
                                                   SaleManager.Helper.WA.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                                   "Удаление",
                                                   "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                   BusinessObjects.Windows.Properties.Resources.STR_CHOICE_DEL, new int[] { 1 });
            if (rows.Length == 1)
            {
                bool docIsRowView = false;
                DataRowView rv = null;
                int i = rows[0];
                Document op = ViewDocuments.GetRow(rows[0]) as Document;

                if (op == null)
                {
                    rv = ViewDocuments.GetRow(i) as DataRowView;
                    if (rv != null)
                    {
                        int docid = (int)rv[GlobalPropertyNames.Id];
                        op = SaleManager.Helper.WA.GetObject<Document>(docid);
                        docIsRowView = true;
                    }
                }
                if (op == null) return;

                if (res == 0)
                {
                    try
                    {
                        op.Remove();
                        if (!docIsRowView)
                            _documentsBindingSource.Remove(op);
                        else
                            _documentsBindingSource.Remove(rv);
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(SaleManager.Helper.WA,
                                                               SaleManager.Helper.WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                                                   SaleManager.Helper.WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (res == 1)
                {
                    try
                    {
                        op.Delete();
                        if (!docIsRowView)
                            _documentsBindingSource.Remove(op);
                        else
                            _documentsBindingSource.Remove(rv);

                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(SaleManager.Helper.WA,
                                                               SaleManager.Helper.WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                                                   SaleManager.Helper.WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                List<DataRowView> removedDataRows = new List<DataRowView>();
                List<Document> removedDocuments = new List<Document>();
                List<Document> documenttodel = new List<Document>();
                for (int j = rows.Length - 1; j >= 0; j--)
                {

                    bool docIsRowView = false;
                    DataRowView rv = null;
                    int i = rows[j];
                    Document op = ViewDocuments.GetRow(i) as Document;
                    if (op != null)
                    {
                        removedDocuments.Add(op);
                        documenttodel.Add(op);
                    }
                    if (op == null)
                    {
                        rv = ViewDocuments.GetRow(i) as DataRowView;
                        if (rv != null)
                        {
                            int docid = (int)rv[GlobalPropertyNames.Id];
                            op = SaleManager.Helper.WA.GetObject<Document>(docid);
                            removedDataRows.Add(rv);
                            documenttodel.Add(op);
                            docIsRowView = true;
                        }
                    }
                }
                _documentsBindingSource.SuspendBinding();
                try
                {
                    if (res == 0)
                    {
                        foreach (Document opdel in documenttodel)
                        {
                            opdel.Remove();
                        }
                    }
                    else if (res == 1)
                    {
                        
                        SaleManager.Helper.WA.Empty<Document>().DeleteList(documenttodel);
                    }

                    foreach (DataRowView removedDataRow in removedDataRows)
                    {
                        _documentsBindingSource.Remove(removedDataRow);
                    }
                    foreach (Document removedDocument in removedDocuments)
                    {
                        _documentsBindingSource.Remove(removedDocument);
                    }

                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(SaleManager.Helper.WA,
                                                           SaleManager.Helper.WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                           "Ошибка удаления!", dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message,
                                        SaleManager.Helper.WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _documentsBindingSource.ResumeBinding();
                }
            }
        }
        private void SetCurrentInfo(Section kind = Section.Default)
        {
            if (kind == Section.Default)
                kind = SaleManager.Helper.CurrentSection;
            string info;
            switch (kind)
            {
                case Section.Clients:
                    info = "Клиенты";
                    break;
                case Section.DocMoneyIn:
                    info = "Входящие платежи";
                    break;
                case Section.DocMoneyOut:
                    info = "Исходящие платежи";
                    break;
                case Section.DocNotDone:
                    info = "Черновики";
                    break;
                case Section.DocPrice:
                    info = "Прайсы";
                    break;
                case Section.DocProductIn:
                    info = "Приход товара";
                    break;
                case Section.DocProductOut:
                    info = "Расходные накладные";
                    break;
                case Section.DocTrash:
                    info = "Корзина документов";
                    break;
                case Section.Help:
                    info = "Помощь";
                    break;
                case Section.MyCompany:
                    info = "Мои предприятия";
                    break;
                case Section.Product:
                    info = "Товары";
                    break;
                case Section.Store:
                    info = "Склады";
                    break;
                case Section.Supplyer:
                    info = "Поставщики";
                    break;
                case Section.Worker:
                    info = "Сотрудники";
                    break;
                case Section.Default:
                    info = "";
                    break;
                default:
                    info = "";
                    break;
            }
            siInfo.Caption = "Текущий раздел: " + info;
        }
    }
}