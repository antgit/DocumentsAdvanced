using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "��������� ���������"
    /// </summary>
    public sealed class ContentModuleDateRegion : ContentModuleBase<DateRegion>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "��������� �������".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleDateRegion()
        {
            Caption = "��������� �������";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.PERIOD_X32);
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<DateRegion> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<DateRegion> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<DateRegion> _saveAnalitic;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(DateRegion value)
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
        void OnShowPropTreeList(DateRegion value)
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
        void OnSaveObject(DateRegion value)
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
                    _showProp = new Action<DateRegion>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<DateRegion>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<DateRegion>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
            }
        }


    }
}