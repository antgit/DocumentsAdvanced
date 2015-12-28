using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Security;
using System;
using System.Linq;
namespace BusinessObjects.Windows
{
    internal class InternalShowPropertyCore<T> where T: class, ICoreObject, new()
    {
        protected virtual void OnShow(EventArgs e)
        {
            if (_showHandlers != null)
                _showHandlers.Invoke(this, e);
        }
        protected virtual void OnShow()
        {
            OnShow(new EventArgs());
        }
        [NonSerialized]
        private EventHandler _showHandlers;
        /// <summary>
        /// Событие перед отображением окна свойств
        /// </summary>
        public event EventHandler Show
        {
            add
            {
                _showHandlers = (EventHandler)
                      Delegate.Combine(_showHandlers, value);
            }
            remove
            {
                _showHandlers = (EventHandler)
                      Delegate.Remove(_showHandlers, value);
            }
        }

        public T SelectedItem { get; set; }
        public IBasePropertyControlICore<T> ControlBuilder { get; set; }
        public bool Modal { get; set; }
        protected FormProperties frm;
        internal FormProperties Form { get { return frm; } }
        public virtual Form ShowDialog(bool modal=false)
        {
            Modal = modal;
            frm = new FormProperties
                                     {
                                         ribbon = { ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.PROPERTIES_X16) },
                                         btnSaveClose = {Visibility = DevExpress.XtraBars.BarItemVisibility.Always}
                                     };
            new FormStateMaintainer(frm, string.Format("Property{0}", SelectedItem.GetType().Name));
            frm.Text = "Свойства";
            ControlBuilder.Owner = frm;
            ControlBuilder.Build();
            frm.clientPanel.Controls.Add(ControlBuilder.Control);
            ControlBuilder.Control.BringToFront();
            ControlBuilder.Control.Dock = DockStyle.Fill;

            #region OK
            frm.btnSaveClose.ItemClick += delegate
            {
                ControlBuilder.Save();
                if (ControlBuilder.CanClose)
                    frm.Close();
            };
            frm.btnSave.ItemClick += delegate
            {
                    ControlBuilder.Save();
            };
            #endregion
            #region Отмена
            frm.btnClose.ItemClick += delegate
            {
                ControlBuilder.SelectedItem.Refresh();
                frm.Close();
            };
            #endregion
            OnShow();
            if (Modal)
            {
                frm.FormClosing += delegate(object senderForm, FormClosingEventArgs e)
                                       {
                                           e.Cancel = !ControlBuilder.CanClose;
                                       };
                frm.ShowDialog();
            }
            else
                frm.Show();
            return frm;
        }

        
    }
    internal sealed class InternalShowPropertyBase<T> : InternalShowPropertyCore<T> where T : class, IBase, new()
    {
        public InternalShowPropertyBase()
        {
            
        }

        public override Form ShowDialog(bool modal=false)
        {
            frm = new FormProperties
                                     {
                                         ribbon = {ApplicationIcon = SelectedItem.GetImage()},
                                         btnSaveClose = {Visibility = DevExpress.XtraBars.BarItemVisibility.Always},
                                         btnRefresh = {Visibility = DevExpress.XtraBars.BarItemVisibility.Always}
                                     };
            
            new FormStateMaintainer(frm, string.Format("Property{0}", SelectedItem.GetType().Name));
            frm.Text = string.Format("Свойства: {0}", SelectedItem.Name);
            frm.Key = "Окно свойств - " + SelectedItem.Entity.Name;
            
            // WINDOW_AGENT_KINDVALUE1 , WINDOW_AGENT
            //string keyWindows = string.Format("WINDOW_{0}_KINDVALUE{1}", SelectedItem.GetType().Name.ToUpper(),
            //                                  SelectedItem.KindValue);
            Library libWindow = UIHelper.FindWindow<T>(SelectedItem); //SelectedItem.Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(keyWindows);
            //if(libWindow==null)
            //{
            //    keyWindows = string.Format("WINDOW_{0}", SelectedItem.GetType().Name.ToUpper());
            //    libWindow = SelectedItem.Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(keyWindows);
            //}
            if(libWindow!=null)
            {
                var chainsAdvancedList = SelectedItem as IChainsAdvancedList<T, Note>;
                if(chainsAdvancedList == null)
                {
                    if (ControlBuilder.TotalPages.ContainsKey(ExtentionString.CONTROL_NOTES))
                        ControlBuilder.TotalPages.Remove(ExtentionString.CONTROL_NOTES);
                }

                bool res = SelectedItem.Workarea.Empty<FactName>().HasFactNames(SelectedItem.EntityId);
                if (!res)
                {
                    if (ControlBuilder.TotalPages.ContainsKey(ExtentionString.CONTROL_FACT_NAME))
                        ControlBuilder.TotalPages.Remove(ExtentionString.CONTROL_FACT_NAME);
                }

                res = SelectedItem.Workarea.CollectionChainKinds.Exists(f => f.FromEntityId == SelectedItem.EntityId && f.ToEntityId == SelectedItem.EntityId);
                if (!res)
                    if (ControlBuilder.TotalPages.ContainsKey(ExtentionString.CONTROL_LINK_NAME))
                        ControlBuilder.TotalPages.Remove(ExtentionString.CONTROL_LINK_NAME);

                // определение закладок текущего окна    
                List<ChainValueView> colPagesOwn = ChainValueView.GetView<Library>(libWindow, ChainKind.PAGES_ID);

                List<ChainValueView> colWindowsDef = ChainValueView.GetView<Library>(libWindow, SelectedItem.Workarea.PageDefaultChainId());
                List<ChainValueView> colPagesDef = new List<ChainValueView>();
                if(colWindowsDef.Count>0)
                {
                    Library mainLib = SelectedItem.Workarea.Cashe.GetCasheData<Library>().Item(colWindowsDef[0].RightId);
                    colPagesDef = ChainValueView.GetView<Library>(mainLib, ChainKind.PAGES_ID);    
                }
                List<ChainValueView> colPages = colPagesOwn.Union(colPagesDef).ToList();

                ElementRightView secureLibrary = SelectedItem.Workarea.Access.ElementRightView(15);
                // проверка разрешений на закладки текущего окна
                foreach (ChainValueView libPage in colPages)
                {
                    bool canView = secureLibrary.IsAllow("VIEW", libPage.RightId);
                    if (!canView)
                    {
                        if (ControlBuilder.TotalPages.ContainsKey(libPage.RightCode))
                        {
                            ControlBuilder.TotalPages.Remove(libPage.RightCode);
                        }
                    }
                }
                if (colPages.Count > 0)
                {
                    List<string> removekeys = new List<string>();
                    foreach (string key in ControlBuilder.TotalPages.Keys)
                    {
                        ChainValueView v = colPages.FirstOrDefault(s => s.RightCode == key);
                        if (v == null)
                            removekeys.Add(key);
                        if(v!=null && v.StateId != State.STATEACTIVE)
                            removekeys.Add(key);
                    }
                    foreach (string removekey in removekeys)
                    {
                        if (ControlBuilder.TotalPages.ContainsKey(removekey))
                            ControlBuilder.TotalPages.Remove(removekey);
                    }

                    var innerJoinQuery = from page in ControlBuilder.TotalPages
                                         join c in colPages on page.Key equals c.RightCode
                                         orderby c.OrderNo ascending
                                         select new { page.Key, page.Value };

                    Dictionary<string, string> newPages = new Dictionary<string, string>();
                    foreach (var v in innerJoinQuery)
                    {
                        newPages.Add(v.Key, v.Value);
                    }
                    ControlBuilder.TotalPages = newPages;
                }

                /*
                List<Library> collPages = Chain<Library>.GetChainSourceList(libWindow, ChainKind.PAGES_ID);
                List<IChain<Library>> chain = (libWindow as IChains<Library>).GetLinks(ChainKind.PAGES_ID);
                ElementRightView secureLibrary = SelectedItem.Workarea.Access.ElementRightView(15);
                // проверка разрешений на закладки текущего окна
                foreach (Library libPage in collPages)
                {
                    bool canView = secureLibrary.IsAllow("VIEW", libPage.Id);
                    if(!canView)
                    {
                        if(ControlBuilder.TotalPages.ContainsKey(libPage.Code))
                        {
                            ControlBuilder.TotalPages.Remove(libPage.Code);
                        }
                    }
                }
                if (collPages.Count > 0)
                {
                    List<string> removekeys = new List<string>();
                    foreach (string key in ControlBuilder.TotalPages.Keys)
                    {
                        Library v = collPages.FirstOrDefault(s => s.Code == key);
                        if (v == null)
                            removekeys.Add(key);
                    }
                    foreach (string removekey in removekeys)
                    {
                        if (ControlBuilder.TotalPages.ContainsKey(removekey))
                            ControlBuilder.TotalPages.Remove(removekey);
                    }
                }

                */
                //// сортировка текщих страниц по данным настроек
                //// JOIN текущих связей и доступных страниц для отображения по ключу
                //var innerJoinQuery = from page in ControlBuilder.TotalPages
                //                     join c in chain on page.Key equals c.Right.Code
                //                     orderby c.OrderNo ascending
                //                     select new {page.Key, page.Value};

                //Dictionary<string, string> newPages = new Dictionary<string, string>();
                //foreach (var v in innerJoinQuery)
                //{
                //    newPages.Add(v.Key, v.Value);
                //}

                //ControlBuilder.TotalPages = newPages;

                //Dictionary<long, Package> dictionary = packages.ToDictionary(p => p.TrackingNumber);


                //var innerJoinQuery =
                //from category in categories
                //join prod in products on category.ID equals prod.CategoryID
                //select new { ProductName = prod.Name, Category = category.Name }; 

            }
            ControlBuilder.Owner = frm;
            ControlBuilder.Build();
            string lastPage = UIHelper.GetLastActivePageIndex(typeof(T).Name);
            ControlBuilder.ActivePage = lastPage;
            frm.clientPanel.Controls.Add(ControlBuilder.Control);
            ControlBuilder.Control.BringToFront();
            ControlBuilder.Control.Dock = DockStyle.Fill;
            frm.Workarea = ControlBuilder.SelectedItem.Workarea;
            frm.Tag = SelectedItem.KindId;
            frm.btnRefresh.ItemClick += delegate
                                            {
                                                SelectedItem.Refresh();
                                            };
            #region OK
            frm.btnSaveClose.ItemClick += delegate
            {
                UIHelper.SetLastActivePageIndex(typeof(T).Name, ControlBuilder.ActivePage);
                ControlBuilder.Save();
                if (ControlBuilder.CanClose)
                    frm.Close();
            };
            frm.btnSave.ItemClick += delegate
            {
                ControlBuilder.Save();
            };
            #endregion
            #region Отмена
            frm.btnClose.ItemClick += delegate
            {
                UIHelper.SetLastActivePageIndex(typeof (T).Name, ControlBuilder.ActivePage);
                ControlBuilder.SelectedItem.Refresh();
                frm.Close();
            };
            #endregion
            OnShow();
            if (modal)
                frm.ShowDialog();
            else
                frm.Show();
            return frm;
        }

    }
}
