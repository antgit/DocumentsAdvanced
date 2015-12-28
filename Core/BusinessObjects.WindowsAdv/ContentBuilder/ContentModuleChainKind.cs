using System;
using System.Linq;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������� ������ "���� ������"
    /// </summary>
    public sealed class ContentModuleChainKind : ContentModuleCore<ChainKind>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "���� ������".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleChainKind()
        {
            Caption = "���� ������";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.CHAIN_X32);
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<ChainKind> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<ChainKind> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<ChainKind> _saveChainKind;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(ChainKind value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                // TODO:
                //value.Created += delegate
                //{
                //    int position = browserBaseObjects.BindingSource.Add(value);
                //    browserBaseObjects.BindingSource.Position = position;
                //};
            }
        }
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ��������� � ���� �����
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowPropTreeList(ChainKind value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                // TODO:
                //value.Created += delegate
                //{
                //    int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                //    treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                //};
            }
        }
        /// <summary>
        /// ���������� ������ ���������� �������
        /// </summary>
        /// <param name="value">������ ��� ����������</param>
        void OnSaveObject(ChainKind value)
        {
            value.Save();
        }
        
        /// <summary>
        /// ���������� ������� ����������� ������
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            if (browserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<ChainKind>(OnShowProp);
                    browserBaseObjects.ShowProperty += _showProp;
                    if ((Owner is RibbonForm))
                    {
                        RibbonForm form = Owner as RibbonForm;
                        RibbonPage page = form.Ribbon.SelectedPage;
                        RibbonPageGroup gp = page.Groups["CHAINKIND_ACTIONLIST"];
                        BarButtonItem btnCreate = (BarButtonItem)gp.ItemLinks[0].Item;

                        PopupMenu mnuTemplates = new PopupMenu {Ribbon = form.Ribbon};
                        BarButtonItem btn = new BarButtonItem {Caption = "�����"};
                        mnuTemplates.AddItem(btn);
                        btn.ItemClick += delegate
                        {
                            ChainKind ck = new ChainKind { Workarea = Workarea };
                            ck.Id = Workarea.CollectionChainKinds.Max(f => f.Id) + 1;
                            ck.ShowProperty().FormClosed += delegate(object s, FormClosedEventArgs ev)
                            {
                                if (s != null)
                                {
                                    Form f = s as Form;
                                    if (f.DialogResult == DialogResult.OK)
                                    {
                                        int index = browserBaseObjects.BindingSource.Add(ck);
                                        browserBaseObjects.BindingSource.Position = index;
                                    }
                                }
                            };
                        };
                        btnCreate.DropDownControl = mnuTemplates;
                    }
                    
                }
            }
            // TODO:
            //if (treeListBrowser != null)
            //{
            //    if (ShowPropTreeList == null)
            //    {
            //        ShowPropTreeList = new Action<ChainKind>(OnShowPropTreeList);
            //        treeListBrowser.ListBrowserBaseObjects.ShowProperty += ShowPropTreeList;
            //    }
            //    if (SaveChainKind == null)
            //    {
            //        SaveChainKind = new Action<ChainKind>(OnSaveObject);
            //        treeListBrowser.ListBrowserBaseObjects.Save += SaveChainKind;
            //    }
            //}
        }
       
    }
}