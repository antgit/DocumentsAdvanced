using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "���� ���������"
    /// </summary>
    public sealed class ContentModuleEntityDocument : ContentModuleBase<EntityDocument>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "���� ����������".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleEntityDocument()
        {
            
            Caption = "���� ����������";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.NEW_X32); 
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<EntityDocument> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<EntityDocument> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<EntityDocument> _saveEntityType;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(EntityDocument value)
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
        void OnShowPropTreeList(EntityDocument value)
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
                TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);
                if (!TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                {
                    int position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                }
                TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position =
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(value);
            }
        }
        /// <summary>
        /// ���������� ������ ���������� �������
        /// </summary>
        /// <param name="value">������ ��� ����������</param>
        void OnSaveObject(EntityDocument value)
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
                    _showProp = new Action<EntityDocument>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<EntityDocument>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveEntityType == null)
                {
                    _saveEntityType = new Action<EntityDocument>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveEntityType;
                }
            }
        }
    }
}