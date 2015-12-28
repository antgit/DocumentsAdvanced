using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Курсы валют"
    /// </summary>
    public sealed class ContentModuleRate : ContentModuleBase<Rate>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Курсы валют".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleRate()
        {
            Caption = "Курсы валют";
            Showing += ContentModuleShowing;
            Show += ContentModuleShow;
        }

        private bool _isInit;

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.RATE_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Rate> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Rate> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Rate> _saveRate;
        /// <summary>
        /// Делегат заполнения данных при создании нового объекта
        /// </summary>
        private Action<Rate, Rate> _CreateNew;
        /// <summary>
        /// Реализация метода заполнения начальных данных при создании нового объекта по шаблону 
        /// </summary>
        /// <remarks>При создании нового курса автоматически устанавливается текущая дата.</remarks>
        /// <param name="value">Значение</param>
        /// <param name="tml">Шаблон</param>
        void OnCreateNew(Rate value, Rate tml)
        {
            value.Date = DateTime.Today;
            //value.CurrencyFromId = tml.CurrencyFromId;
            //value.CurrencyToId = tml.CurrencyToId;
            //value.BankId = tml.BankId;
            //value.Multiplier = tml.Multiplier;
        }
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
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
        /// Реализация метода отображения свойств объекта при просмотре в виде групп
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
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
        /// Реализация метода сохранения объекта
        /// </summary>
        /// <param name="value">Объект для сохранения</param>
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
        /// Обработчик события отображения модуля
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