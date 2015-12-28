using System;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Маппинг хранимых процедур"
    /// </summary>
    public sealed class ContentModuleProcedureMap : ContentModuleCore<ProcedureMap>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Маппинг хранимых процедур".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleProcedureMap()
        {
            Caption = "Маппинг хранимых процедур";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
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
                BarButtonItem item = new BarButtonItem {Caption = "Маппинг ХП бизнес объекта"};
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
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<ProcedureMap> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<ProcedureMap> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<ProcedureMap> _saveChainKind;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
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
        /// Реализация метода отображения свойств объекта при просмотре в виде групп
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
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
        /// Реализация метода сохранения объекта
        /// </summary>
        /// <param name="value">Объект для сохранения</param>
        void OnSaveObject(ProcedureMap value)
        {
            value.Save();
        }
        /// <summary>
        /// Обработчик события отображения модуля
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