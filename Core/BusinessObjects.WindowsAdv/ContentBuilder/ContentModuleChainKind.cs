using System;
using System.Linq;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейстный модуль "Типы связей"
    /// </summary>
    public sealed class ContentModuleChainKind : ContentModuleCore<ChainKind>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Виды связей".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleChainKind()
        {
            Caption = "Виды связей";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.CHAIN_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<ChainKind> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<ChainKind> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<ChainKind> _saveChainKind;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(ChainKind value)
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
        void OnShowPropTreeList(ChainKind value)
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
        void OnSaveObject(ChainKind value)
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
                    _showProp = new Action<ChainKind>(OnShowProp);
                    browserBaseObjects.ShowProperty += _showProp;
                    if ((Owner is RibbonForm))
                    {
                        RibbonForm form = Owner as RibbonForm;
                        RibbonPage page = form.Ribbon.SelectedPage;
                        RibbonPageGroup gp = page.Groups["CHAINKIND_ACTIONLIST"];
                        BarButtonItem btnCreate = (BarButtonItem)gp.ItemLinks[0].Item;

                        PopupMenu mnuTemplates = new PopupMenu {Ribbon = form.Ribbon};
                        BarButtonItem btn = new BarButtonItem {Caption = "Связь"};
                        mnuTemplates.AddItem(btn);
                        btn.ItemClick += delegate
                        {
                            ChainKind ck = new ChainKind { Workarea = Workarea };
                            ck.Id = Workarea.CollectionChainKinds.Max(f => f.Id) + 1;
                            ck.ShowProperty().FormClosed += delegate(object s, FormClosedEventArgs ev)
                            {
                                if (s != null)
                                {
                                    Form f = s as Form;
                                    if (f.DialogResult == DialogResult.OK)
                                    {
                                        int index = browserBaseObjects.BindingSource.Add(ck);
                                        browserBaseObjects.BindingSource.Position = index;
                                    }
                                }
                            };
                        };
                        btnCreate.DropDownControl = mnuTemplates;
                    }
                    
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