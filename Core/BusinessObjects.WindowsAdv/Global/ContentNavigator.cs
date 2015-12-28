using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    public interface IContentNavigator
    {
        string ParentKey { get; set; } 
        RibbonForm MainForm { get; set; }
        Workarea Workarea { get; set; }
        string CurrentKey { get; set; }
        Dictionary<string, IContentModule> Modules { get; set; }
        string ActiveKey { get; set; }
        event Action<IContentModule> ActiveChangin;
        event Action<IContentModule> ActiveChanged;
        event Action<string> ReguestModule;
    }

    /// <summary>
    /// Навигатор
    /// </summary>
    public class ContentNavigator : IContentNavigator
    {
        private int MaxStoredContent = 3;
        public ContentNavigator()
        {
            History = new List<string>();
            Modules = new Dictionary<string, IContentModule>();
            _activeKey = string.Empty;
            //MainForm.Ribbon.MouseClick += new MouseEventHandler(Ribbon_MouseClick);
            //
        }

        public void SafeAddModule(string key, IContentModule value)
        {
            if (Modules.ContainsKey(key))
            {
                Modules[key] = value;
            }
            else
            {
                //if(Modules.Count== MaxStoredContent)
                //{
                //    string keyRemove = Modules.First().Key;
                //    if(MainForm.Controls["clientPanel"].Controls.Contains(Modules[keyRemove].Control))
                //    {
                //        MainForm.Controls["clientPanel"].Controls.Remove(Modules[keyRemove].Control);
                //        Modules.Remove(keyRemove);
                //    }
                //}
                Modules.Add(key, value);
            }
        }

        public RibbonForm MainForm { get; set; }

        public Workarea Workarea { get; set; }
        public string CurrentKey { get; set; }
        List<string> History { get; set; }
        public Dictionary<string, IContentModule> Modules { get; set; }
        private BarButtonItem GetButtom(RibbonPage page)
        {
            string keyPage = page.Tag.ToString();

            RibbonPageGroup actionGroup = page.Groups.GetGroupByName(keyPage + "ACTIONS");
            if (actionGroup == null) return null;
            BarButtonItem btn = FindButton(actionGroup, keyPage + "_SELECT");
            return btn;
        }
        private BarButtonItem FindButton(RibbonPageGroup group, string name)
        {
            foreach (BarItemLink link in group.ItemLinks)
            {
                if (link.Item.Name == name)
                    return link.Item as BarButtonItem;
            }
            return null;
        }

        private string _parentKey;
        private string _lastParentKey;
        /// <summary>
        /// Родительский ключ
        /// </summary>
        public string ParentKey
        {
            get { return _parentKey; }
            set
            {
                if (_parentKey == null)
                    _lastParentKey = value;
                else
                    _lastParentKey = _parentKey;
                _parentKey = value;
            }
        }

        private string _activeKey;
        public string ActiveKey
        {
            get { return _activeKey; }
            set
            {
                if (_activeKey != value || _parentKey!=_lastParentKey)
                {
                    if (Modules.ContainsKey(_activeKey))
                    {
                        OnActiveChangin(Modules[_activeKey]);
                        Modules[_activeKey].PerformHide();
                        Modules[_activeKey].Control.Visible = false;
                        History.Add(_activeKey);
                    }
                    _activeKey = value;
                    if(!Modules.ContainsKey(_activeKey) || Modules[_activeKey]==null)
                    {
                        OnReguestModule(_activeKey); 
                    }
                    if (Modules.ContainsKey(_activeKey) && Modules[_activeKey] != null)
                    {
                        Modules[value].ContentNavigator = this;
                        if (Modules[value].ParentKey != null && Modules[value].ParentKey != string.Empty && MainForm != null)
                        {
                            if (MainForm.Ribbon.SelectedPage.Tag != null && MainForm.Ribbon.SelectedPage.Tag.ToString() != Modules[value].ParentKey)
                            {
                                RibbonPage page = UIHelper.GetPageByTag(MainForm, Modules[value].ParentKey);
                                if (page != null)
                                    MainForm.Ribbon.SelectedPage = page;
                            }
                        }
                        if(MainForm!=null)
                        {
                            if(!MainForm.Controls["clientPanel"].Controls.Contains(Modules[value].Control))
                            {
                                Modules[value].Owner = MainForm;
                                Modules[value].PerformShow();
                                MainForm.Controls["clientPanel"].Controls.Add(Modules[value].Control);
                            }
                            else
                            {
                                Modules[value].PerformShow();        
                            }
                        }
                        if (MainForm != null)
                        {
                            BarButtonItem btn = GetButtom(MainForm.Ribbon.SelectedPage);
                            if (btn != null)
                            {
                                btn.Glyph = Modules[value].Image32;
                                btn.Caption = Modules[value].Caption;
                                btn.Tag = value;

                                if (Workarea != null && Modules[value].SelfLibrary!=null)
                                {
                                    if (!string.IsNullOrEmpty(Modules[value].SelfLibrary.Memo))
                                    {
                                        btn.SuperTip = CreateSuperToolTip(btn.Glyph, Modules[value].Caption,
                                                                          Modules[value].SelfLibrary.Memo);
                                    }
                                    else
                                    {
                                        btn.SuperTip = null;
                                    }
                                }
                            }
                        }
                        Modules[value].Control.Visible = true;
                        Modules[value].Control.Dock = DockStyle.Fill;
                        OnActiveChanged(Modules[value]);
                    }
                }
            }
        }

        public event Action<IContentModule> ActiveChangin;
        public event Action<IContentModule> ActiveChanged;
        public event Action<string> ReguestModule;
        protected void OnReguestModule(string key)
        {
            if (ReguestModule != null)
            {
                ReguestModule(key);
            }
        }
        protected void OnActiveChangin(IContentModule args)
        {
            if (ActiveChangin != null)
            {
                ActiveChangin(args);
            }
        }
        protected void OnActiveChanged(IContentModule args)
        {
            if (ActiveChanged != null)
            {
                ActiveChanged(args);
            }

        }

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
    }
}
