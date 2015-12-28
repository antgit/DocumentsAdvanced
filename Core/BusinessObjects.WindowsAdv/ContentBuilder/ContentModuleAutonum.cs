using System;
using BusinessObjects.Documents;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль справочника "Автонумерация"
    /// </summary>
    public sealed class ContentModuleAutonum : ContentModuleCore<Autonum>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Автонумерация документов".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleAutonum()
        {
            Caption = "Автонумерация документов";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.AUTONUM_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Autonum> _showProp;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Autonum value)
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
        /// Реализация метода сохранения объекта
        /// </summary>
        /// <param name="value">Объект для сохранения</param>
        void OnSaveObject(Autonum value)
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
                    _showProp = new Action<Autonum>(OnShowProp);
                    browserBaseObjects.ShowProperty += _showProp;
                }
            }
            // TODO:
            //groupLinksView.Visible = false;
            //if (BrowserBaseObjects != null)
            //{
            //    if (_showProp == null)
            //    {
            //        _showProp = new Action<Autonum>(OnShowProp);
            //        BrowserBaseObjects.ShowProperty += _showProp;
            //    }
            //}
            //if (TreeListBrowser != null)
            //{
            //    if (_showPropTreeList == null)
            //    {
            //        _showPropTreeList = new Action<Autonum>(OnShowPropTreeList);
            //        TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
            //    }
            //    if (_saveRate == null)
            //    {
            //        _saveRate = new Action<Autonum>(OnSaveObject);
            //        TreeListBrowser.ListBrowserBaseObjects.Save += _saveRate;
            //    }
            //}
        }
    }
}
