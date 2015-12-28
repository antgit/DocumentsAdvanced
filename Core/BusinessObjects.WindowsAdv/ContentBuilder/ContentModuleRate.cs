using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ������������ ������ "����� �����"
    /// </summary>
    public sealed class ContentModuleRate : ContentModuleBase<Rate>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <remarks>
        /// � ������������ �������������� ��������� � ������������ ������� � ������� 
        /// ����������� ������.
        /// ��������� ������ �� ��������� - "����� �����".
        /// � ��������� ������� ����������� �������������� ������ �������� ��� 
        /// ����������, ����������� ������� ������� � ���� ������ � �����.
        /// </remarks>
        public ContentModuleRate()
        {
            Caption = "����� �����";
            Showing += ContentModuleShowing;
            Show += ContentModuleShow;
        }

        private bool _isInit;

        /// <summary>
        /// ����� ����������� ��������������� ����������� �������� Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.RATE_X32);
        }
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        Action<Rate> _showProp;
        /// <summary>
        /// ������� ����������� ������� ������� ��� ����������� � ���� �����
        /// </summary>
        Action<Rate> _showPropTreeList;
        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        Action<Rate> _saveRate;
        /// <summary>
        /// ������� ���������� ������ ��� �������� ������ �������
        /// </summary>
        private Action<Rate, Rate> _CreateNew;
        /// <summary>
        /// ���������� ������ ���������� ��������� ������ ��� �������� ������ ������� �� ������� 
        /// </summary>
        /// <remarks>��� �������� ������ ����� ������������� ��������������� ������� ����.</remarks>
        /// <param name="value">��������</param>
        /// <param name="tml">������</param>
        void OnCreateNew(Rate value, Rate tml)
        {
            value.Date = DateTime.Today;
            //value.CurrencyFromId = tml.CurrencyFromId;
            //value.CurrencyToId = tml.CurrencyToId;
            //value.BankId = tml.BankId;
            //value.Multiplier = tml.Multiplier;
        }
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ����������� � ���� ������
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowProp(Rate value)
        {
            if (value.IsNew)
            {
                value.Creating += delegate
                {
                    if (value.CurrencyFromId != 0 && value.CurrencyToId != 0)
                        value.Name = string.Format("{0} {1}", value.CurrencyFrom.Code,
                                                   value.CurrencyTo.Code);
                };
                value.Created += delegate
                {
                    int position = BrowserBaseObjects.BindingSource.Add(value);
                    BrowserBaseObjects.BindingSource.Position = position;
                };
            }
            value.ShowProperty();
            
        }
        /// <summary>
        /// ���������� ������ ����������� ������� ������� ��� ��������� � ���� �����
        /// </summary>
        /// <remarks>����� ����������� ������� ������� �� ����������� � ������� ������.</remarks>
        /// <param name="value">������ ��� �����������</param>
        void OnShowPropTreeList(Rate value)
        {

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
            value.ShowProperty();
        }
        /// <summary>
        /// ���������� ������ ���������� �������
        /// </summary>
        /// <param name="value">������ ��� ����������</param>
        void OnSaveObject(Rate value)
        {
            value.Save();
        }
        void ContentModuleShowing(object sender, EventArgs e)
        {
            if (!_isInit)
            {
                _isInit = true;
                ActiveView = "LIST";
                //((BarCheckItem)(((BarCheckItemLink)this.groupLinksView.ItemLinks[0]).Item)).PerformClick();
            }
        }
        /// <summary>
        /// ���������� ������� ����������� ������
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            groupLinksView.Visible = false;
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<Rate>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
                if(_CreateNew ==null)
                {
                    _CreateNew = new Action<Rate, Rate>(OnCreateNew);
                    BrowserBaseObjects.CreateNew += _CreateNew;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Rate>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveRate == null)
                {
                    _saveRate = new Action<Rate>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveRate;
                }
            }
        }

        
    }
}