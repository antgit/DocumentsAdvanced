using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ ����������� "������ �����"
    /// </summary>
    public sealed class ContentModuleTimePeriod : ContentModuleBase<TimePeriod>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "������ �����".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleTimePeriod()
        {
            Caption = "������ �����";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.CALENDAR_X32);
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<TimePeriod> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<TimePeriod> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<TimePeriod> _saveAnalitic;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(TimePeriod value)
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
        void OnShowPropTreeList(TimePeriod value)
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
        void OnSaveObject(TimePeriod value)
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
                    _showProp = new Action<TimePeriod>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                TreeListBrowser.ListBrowserBaseObjects.GridView.OptionsView.ColumnAutoWidth = false;
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<TimePeriod>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<TimePeriod>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
            }
        }


    }
}