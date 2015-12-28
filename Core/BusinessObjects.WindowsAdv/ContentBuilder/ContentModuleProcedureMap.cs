using System;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "������� �������� ��������"
    /// </summary>
    public sealed class ContentModuleProcedureMap : ContentModuleCore<ProcedureMap>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "������� �������� ��������".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleProcedureMap()
        {
            Caption = "������� �������� ��������";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.STOREDPROC_X32);
        }
        protected override void RegisterPageAction()
        {
            base.RegisterPageAction();
            if(browserBaseObjects.ListControl.CreateMenu.ItemLinks.Count==0)
            {
                BarButtonItem item = new BarButtonItem {Caption = "������� �� ������ �������"};
                item.ItemClick += ItemItemClick;
                browserBaseObjects.ListControl.CreateMenu.AddItem(item);
            }
        }

        void ItemItemClick(object sender, ItemClickEventArgs e)
        {
            ProcedureMap map = new ProcedureMap {Workarea = Workarea, StateId = State.STATEACTIVE, TypeId = 0};
            map.ShowProperty();
        }
        public override void PerformShow()
        {
            Collection = ProcedureMap.Collection(Workarea);
            base.PerformShow();
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<ProcedureMap> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<ProcedureMap> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<ProcedureMap> _saveChainKind;
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(ProcedureMap value)
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
        void OnShowPropTreeList(ProcedureMap value)
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
        void OnSaveObject(ProcedureMap value)
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
                    _showProp = new Action<ProcedureMap>(OnShowProp);
                    browserBaseObjects.ShowProperty += _showProp;
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