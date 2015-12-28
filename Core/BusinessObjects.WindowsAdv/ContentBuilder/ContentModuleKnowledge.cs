using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "���� ������"
    /// </summary>
    public sealed class ContentModuleKnowledge : ContentModuleBase<Knowledge>
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
        public ContentModuleKnowledge()
        {
            Caption = "���� ������";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModule_CreateTreeList;
            ShowProperiesOnDoudleClick = false;
        }

        private void OnBuildReport()
        {
            foreach (Knowledge item in TreeListBrowser.ListBrowserBaseObjects.SelectedValues)
            {
                item.ShowKnowledge();
            }
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.HELP_X32);
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<Knowledge> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<Knowledge> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<Knowledge> _saveAnalitic;
        private bool _isEventCaptureBuild;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(Knowledge value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                                     {
                                         int position = BrowserBaseObjects.BindingSource.Add(value);
                                         BrowserBaseObjects.BindingSource.Position = position;
                                     };
            }
        }
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ��������� � ���� �����
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowPropTreeList(Knowledge value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                                     {
                                         /*int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                    treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;*/
                                         TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);
                                         if (!TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                                         {
                                             int position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                                             TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                                         }
                                         TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position =
                                             TreeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(value);
                                     };
            }
        }
        /// <summary>
        /// ���������� ������ ���������� �������
        /// </summary>
        /// <param name="value">������ ��� ����������</param>
        void OnSaveObject(Knowledge value)
        {
            value.Save();
        }
        /// <summary>
        /// ���������� ������� ����������� ������
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<Knowledge>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Knowledge>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<Knowledge>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
                if (!_isEventCaptureBuild)
                {
                    _isEventCaptureBuild = true;
                    TreeListBrowser.ListBrowserBaseObjects.ListControl.Grid.DoubleClick += delegate
                    {
                        System.Drawing.Point p = TreeListBrowser.ListBrowserBaseObjects.ListControl.Grid.PointToClient(Control.MousePosition);
                        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = TreeListBrowser.ListBrowserBaseObjects.GridView.CalcHitInfo(p.X, p.Y);
                        if (hit.InRowCell)
                        {
                            OnBuildReport();
                        }
                    };
                }
            }
        }

        void ContentModule_CreateTreeList(object sender, EventArgs e)
        {
            #region ���������� ������
            
                BarButtonItem btnUpdateLibrarires = new BarButtonItem
                {
                    Caption = "�������� ������",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PREVIEW_X32)
                };
                btnUpdateLibrarires.SuperTip = UIHelper.CreateSuperToolTip(btnUpdateLibrarires.Glyph, "�������� ������ ���� ������",
                    "�������� �������� �������� ������ ���� ������ ��� ����� ������������");
                
                groupLinksActionTreeList.ItemLinks.Insert(groupLinksActionTreeList.ItemLinks[0], btnUpdateLibrarires);
                btnUpdateLibrarires.ItemClick += delegate
                {
                    if(Selected!=null && Selected.Count>0)
                    {
                        Selected[0].ShowKnowledge();
                    }
                    //XtraMessageBox.Show("���������� ���������! ������������� ��������� ���� �� ��������� ��������� �����",
                    //                                "��������!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                };
            
            #endregion
        }
    }
}