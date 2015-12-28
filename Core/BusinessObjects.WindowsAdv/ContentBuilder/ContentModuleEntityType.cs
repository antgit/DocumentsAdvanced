using System;
using System.Linq;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "��������� �������"
    /// </summary>
    public sealed class ContentModuleEntityType : ContentModuleBase<EntityType>
    {
        /// <summary>
        /// ����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "��������� �������".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleEntityType()
        {
            
            Caption = "��������� �������";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.CLASS_X32);
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<EntityType> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<EntityType> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<EntityType> _saveEntityType;
        Action<EntityType, EntityType> OnCreateDelegate;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(EntityType value)
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
        void OnShowPropTreeList(EntityType value)
        {
            Form frm = value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                {
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
        void OnSaveObject(EntityType value)
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
                    _showProp = new Action<EntityType>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<EntityType>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveEntityType == null)
                {
                    _saveEntityType = new Action<EntityType>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveEntityType;
                }
                if (OnCreateDelegate==null)
                {
                    OnCreateDelegate = new Action<EntityType, EntityType>(OnObjectsCreateNew);
                    TreeListBrowser.ListBrowserBaseObjects.CreateNew += OnCreateDelegate;
                }
            }
        }

        void OnObjectsCreateNew(EntityType arg1, EntityType arg2)
        {
            arg1.Id = Workarea.CollectionEntities.Max(f => f.Id)+1;
        }
    }
}