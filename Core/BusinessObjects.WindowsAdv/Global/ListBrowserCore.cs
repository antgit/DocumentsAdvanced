using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Делегат просмотра объектов в виде списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public delegate T Browse<T>(T obj) where T : class, ICoreObject;

    public delegate List<T> BrowseChainObject<T>(T value, Predicate<T> filter, List<T> sourceCollection) where T : class, IBase;
    /// <summary>
    /// Настройки просмотра объектов в виде списков
    /// </summary>
    public class BrowserOptions
    {
        public bool ShowProperiesOnDoudleClick { get; set; }
        /// <summary>
        /// Разрешить контекстное меню в списке
        /// </summary>
        public bool AllowContextMenu { get; set; }
        /// <summary>
        /// Стандарные действия по созданию, удалению и пр.
        /// </summary>
        /// <remarks>Если значение свойства установлено в <c>true</c> 
        /// регестрируются обработчики событий для действий удаления, создания, поиска.
        /// </remarks>
        public bool StandartAction { get; set; }
        /// <summary>
        /// Разрешить контестное меню заголовка столбцов
        /// </summary>
        public bool AllowHeaderMenu { get; set; }
        /// <summary>
        /// Разрешить выбор нескольких объектов
        /// </summary>
        public bool MultySelect { get; set; }
        private bool _showToolBar;

        /// <summary>
        /// Показывать статусную строку
        /// </summary>
        public bool ShowStatusBar
        {
            get;
            set;
            
        }
        /// <summary>
        /// Отображать панель инструментов
        /// </summary>
        public bool ShowToolBar
        {
            get { return _showToolBar; }
            set
            {
                if (value != _showToolBar)
                {
                    _showToolBar = value;
                }
            }
        }
        /// <summary>
        /// Отложенная инициализация
        /// </summary>
        public bool LazyInitDataSource { get; set; }
    }
    /// <summary>
    /// Просмотр объектов в виде списка
    /// </summary>
    /// <typeparam name="T">Тип объекта</typeparam>
    public class ListBrowserCore<T> where T : class, ICoreObject, new()//BaseCore<T>
    {
        public ListBrowserCore()
        {
            Options = new BrowserOptions {ShowProperiesOnDoudleClick = true};
        }
        /// <summary>
        /// Настройки просмотра объектов в виде списков
        /// </summary>
        public BrowserOptions Options { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="startValue">Стартовый объект</param>
        /// <param name="allowContextMenu"></param>
        /// <param name="showToolbar"></param>
        /// <param name="multySelect">Разрешить выбор нескольких объектов</param>
        /// <param name="standartAction">Использовать стандартную обработку действий</param>
        public ListBrowserCore(IWorkarea wa, List<T> sourceCollection, Predicate<T> filter, T startValue, bool allowContextMenu, bool showToolbar, bool multySelect, bool standartAction): this()
        {
            Workarea = wa;
            StartValue = startValue;
            Filter = filter;
            SourceCollection = sourceCollection;
            InitCollectionIsNull = sourceCollection == null;
            Options.AllowContextMenu = allowContextMenu;
            Options.AllowHeaderMenu = false;
            Options.MultySelect = multySelect;
            Options.ShowStatusBar = showToolbar;
            Options.ShowToolBar = showToolbar;
            Options.StandartAction = standartAction;
        }
        #region Свойства
        private IWorkarea _workarea;
        /// <summary>Рабочая область</summary>
        public IWorkarea Workarea
        {
            get { return _workarea; }
            protected set { _workarea = value; }
        }



        /// <summary>
        /// Значение объекта на который необходимо установить фокус при открытии диалога
        /// </summary>
        /// <remarks>Фокус устанавливается только тогда, когда идентификатор объекта не равен 0.</remarks>
        public T StartValue { get; set; }

        /// <summary>
        /// Первый выделенный объект
        /// </summary>
        public virtual T FirstSelectedValue
        {
            get
            {
                T currentObject = BindingSource.Current as T;
                if (currentObject != null)
                    return currentObject;

                DataRowView rv = BindingSource.Current as DataRowView;
                if (rv != null)
                {
                    int id = (int)rv[GlobalPropertyNames.Id];
                    T item = Workarea.Cashe.GetCasheData<T>().Item(id);
                    return item;
                }
                return null;
            }
        }



        /// <summary>
        /// Исходная коллекция для отображения в списке
        /// </summary>
        public List<T> SourceCollection { get; set; }

        private Predicate<T> _filter;
        /// <summary>
        /// Фильтр коллекции
        /// </summary>
        public Predicate<T> Filter
        {
            get { return _filter; }
            set
            {

                _filter = value;
                OnFilterChanged();
            }
        }

        /// <summary>
        /// Связанная коллекция
        /// </summary>
        public BindingSource BindingSource { get; set; }

        /// <summary>
        /// Корневая иерархия
        /// </summary>
        public string RootCode { get; set; }

        #endregion
        public virtual void OnFilterChanged()
        {

        }
        /// <summary>
        /// Событие сохранения
        /// </summary>
        public event Action<T> Save = delegate { };
        /// <summary>
        /// Сохранение объекта
        /// </summary>
        /// <param name="value">Объект</param>
        protected virtual void OnSave(T value)
        {
            if (Save != null)
                Save.Invoke(value);
        }
        /// <summary>
        /// Выполнить сохранение объекта
        /// </summary>
        /// <param name="value">Объект</param>
        public virtual void InvokeSave(T value)
        {
            OnSave(value);
        }
        /// <summary>
        /// Событие удаления
        /// </summary>
        public event Action<T> Delete = delegate { };
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="value">Объект</param>
        protected virtual void OnDelete(T value)
        {
            if (Delete != null)
                Delete.Invoke(value);
        }
        /// <summary>
        /// Событие запроса на удаление единичного объекта в списке
        /// </summary>
        public event System.ComponentModel.CancelEventHandler RequestDelete = delegate { };
        /// <summary>
        /// Запрос на удаление объекта
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool OnRequestDelete(T value)
        {
            if (RequestDelete != null)
            {
                System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
                RequestDelete.Invoke(value, e);
                return e.Cancel;
            }
            return false;
        }

        /// <summary>
        /// Событие запроса на удаление множества объекта в списке
        /// </summary>
        public event System.ComponentModel.CancelEventHandler RequestDeleteMany = delegate { };
        /// <summary>
        /// Запрос на удаление объекта
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual bool OnRequestDeleteMany(List<T> value)
        {
            if (RequestDeleteMany != null)
            {
                System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
                RequestDeleteMany.Invoke(value, e);
                return e.Cancel;
            }
            return false;
        }

        /// <summary>
        /// Событие "Показать свойства"
        /// </summary>
        public event Action<T> ShowProperty = delegate { };
        /// <summary>
        /// Событие "Создание нового объекта"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public event Action<T, T> CreateNew = delegate { };
        /// <summary>
        /// Событие "Поиск"
        /// </summary>
        public event Action<T> Find = delegate { };
        /// <summary>
        /// Событие "После построения"
        /// </summary>
        public event EventHandler AfterBuild;
        /// <summary>
        /// Вызов события "После постоения"
        /// </summary>
        protected virtual void OnAfterBuild()
        {
            if (AfterBuild != null)
                AfterBuild.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Вызов события "Показать свойства"
        /// </summary>
        /// <param name="value"></param>
        protected virtual void OnShowProperty(T value)
        {
            if (ShowProperty != null)
                ShowProperty.Invoke(value);
            // TODO: Проблема с дублированием элементов в справочниках при создании
            /*if (collectionBind.DataSource != null && collectionBind.DataSource.GetType().Equals(typeof(List<T>)))
            {
                if (!collectionBind.Contains(value))
                {
                    int position = collectionBind.Add(value);
                    collectionBind.Position = position;
                }
            }*/
        }

        /// <summary>
        /// Вызов события "Создание нового"
        /// </summary>
        /// <param name="value"></param>
        /// <param name="templateValue"></param>
        protected virtual void OnCreateNew(T value, T templateValue)
        {
            if (CreateNew != null)
                CreateNew.Invoke(value, templateValue);
        }
        /// <summary>
        /// Вызов события "Поиск"
        /// </summary>
        /// <param name="value"></param>
        protected virtual void OnFind(T value)
        {
            if (Find != null)
                Find.Invoke(value);
        }
        
        #region Свойства
        /// <summary>Отображать свойства по двойному клику</summary>
        public bool ShowProperiesOnDoudleClick { get; set; }
        /// <summary>Коллекция выделенных объектов</summary>
        public virtual List<T> SelectedValues
        {
            get
            {
                List<T> returnValue = new List<T>();
                DevExpress.XtraGrid.Views.Grid.GridView view = ListControl.Grid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView;
                int[] rowsHandle = view.GetSelectedRows();
                foreach (int rowHandle in rowsHandle)
                {
                    returnValue.Add(view.GetRow(rowHandle) as T);
                }
                return returnValue;
            }
        }
        /// <summary>
        /// Представление по умолчанию
        /// </summary>
        public DevExpress.XtraGrid.Views.Grid.GridView GridView
        {
            get { return ListControl.Grid.DefaultView as DevExpress.XtraGrid.Views.Grid.GridView; }
        }

        /// <summary>Собственно элемент управления отображения списка</summary>
        internal ControlList ListControl { get; set; }

        public bool ExternalControl { get; set; }
        /// <summary>Собственно элемент управления отображения списка</summary>
        /// <remarks>В зависимости от реализации в качестве данного контрола может выступать список, 
        /// таблица или любой другой элемент поддерживающий отображение в виде списка</remarks>
        protected virtual Control BrowseControl
        {
            get { return ListControl; }
        }

        private string _listCode;
        /// <summary>
        /// Код списка используемого для таблицы
        /// </summary>
        public string ListViewCode
        {
            get { return _listCode; }
            set { _listCode = value; }
        }
        #endregion

        #region Методы
        
        protected bool InitCollectionIsNull;
        public virtual void Build(bool endInit=true)
        {
            if (!ExternalControl && ListControl != null)
                return;
            if (!ExternalControl)
            {
                ListControl = new ControlList();
                //TODO: _ListControl.Grid.MultiSelect = _MultySelect;
                GridView.OptionsBehavior.Editable = false;
                GridView.OptionsBehavior.AutoPopulateColumns = false;
                GridView.OptionsBehavior.AutoSelectAllInEditor = false;
                GridView.OptionsCustomization.AllowRowSizing = false;
                GridView.OptionsDetail.EnableMasterViewMode = false;
                GridView.OptionsDetail.ShowDetailTabs = false;
                GridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                GridView.OptionsSelection.MultiSelect = true;
                GridView.OptionsSelection.UseIndicatorForSelection = false;
                //GridView.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
                //GridView.OptionsView.ColumnAutoWidth = false;
                GridView.OptionsView.ShowDetailButtons = false;
                GridView.OptionsView.ShowIndicator = false;
                GridView.RowHeight = 0;
                GridView.SynchronizeClones = false;

                ListControl.barTools.Visible = Options.ShowToolBar;
                if (Options.ShowToolBar)
                {
                    ListControl.btnEdit.ItemClick += delegate
                                                         {
                                                             InvokeProperties();
                                                         };
                    ListControl.btnDelete.ItemClick += delegate
                    {
                        InvokeDelete();
                    };
                    ListControl.btnCreate.ItemClick += delegate
                    {
                        OnCreateNew(null, null);
                    };
                }
                InvokeRefresh();
            }
            else
            {
                GridView.OptionsBehavior.Editable = false;
                GridView.OptionsBehavior.AutoPopulateColumns = false;
                GridView.OptionsBehavior.AutoSelectAllInEditor = false;
                GridView.OptionsCustomization.AllowRowSizing = false;
                GridView.OptionsDetail.EnableMasterViewMode = false;
                GridView.OptionsDetail.ShowDetailTabs = false;
                GridView.OptionsSelection.EnableAppearanceFocusedCell = false;
                GridView.OptionsSelection.MultiSelect = true;
                GridView.OptionsSelection.UseIndicatorForSelection = false;
                //GridView.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
                //GridView.OptionsView.ColumnAutoWidth = false;
                GridView.OptionsView.ShowDetailButtons = false;
                GridView.OptionsView.ShowIndicator = false;
                GridView.RowHeight = 0;
                GridView.SynchronizeClones = false;

                InvokeRefresh();
            }
            //_ListControl.Grid.MultiSelect = _MultySelect;
            //if (SourceCollection == null)
            //    SourceCollection = Filter == null ? Workarea.GetCollection<T>() : Workarea.GetCollection<T>().FindAll(Filter);
            //else
            //{
            //if (SourceCollection!=null && Filter != null)
            //    SourceCollection = SourceCollection.FindAll(Filter);
            //}
            // + фильтр безопастности
            if (!Workarea.Access.RightCommon.AdminEnterprize)
            {
                //BusinessObjects.Security.EntityRightView secureDbEntity = Workarea.Access.DbentityRightView();
                //if (secureDbEntity.IsAllow("VIEW", Workarea.Empty<T>().EntityId))
                //{
                //    BusinessObjects.Security.ElementRightView secure = Workarea.Access.ElementRightView(Workarea.Empty<T>().EntityId);
                //    List<int> denied = secure.GetDeny("VIEW");
                //    if (denied.Count > 0)
                //        SourceCollection = SourceCollection.Where(f => !denied.Contains(f.Id)).ToList();
                //}
                //else
                //    SourceCollection.Clear();
            }
            BindingSource = new BindingSource();
            if(string.IsNullOrEmpty(_listCode))
                _listCode = StartValue != null
                                  ? string.Format("DEFAULT_LISTVIEW{0}", StartValue.GetType().Name.ToUpper())
                                  : string.Format("DEFAULT_LISTVIEW{0}", typeof(T).Name.ToUpper());
            DataGridViewHelper.GenerateGridColumns(Workarea, GridView, _listCode);

            BindingSource.DataSource = SourceCollection;
            ListControl.Grid.DataSource = BindingSource;

            #region Форматирование
            GridView.CustomUnboundColumnData += ViewCustomUnboundColumnData;
            #endregion

            #region Свойства
            if (ShowProperiesOnDoudleClick)
            ListControl.Grid.DoubleClick += delegate
                                                {
                                                    
                                                    System.Drawing.Point p = ListControl.Grid.PointToClient(Control.MousePosition);
                                                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = GridView.CalcHitInfo(p.X, p.Y);
                                                    if (hit.InRowCell && ListControl.View.FocusedRowHandle > -1)
                                                    {
                                                        InvokeProperties();
                                                    }
                                                    //if (hit.InRow && ListControl.View.FocusedRowHandle>-1)
                                                    //{
                                                    //    InvokeProperties();
                                                    //}
                                                };
            #endregion

            #region Удаление
            if (Options.StandartAction)
            {
            }
            #endregion

            #region Разрешения
            if (Options.StandartAction)
            {
                //ListControl.btnAcl.ItemClick += delegate
                //                                    {
                //                                        if (BindingSource != null && BindingSource.Current != null)
                //                                        {
                //                                            T currentObject = BindingSource.Current as T;
                //                                            if (currentObject != null)
                //                                            {
                //                                                // TODO: Просмотр разрешений
                //                                                // currentObject.BrowseRightsElement();
                //                                            }
                //                                        }
                //                                    };
            }
            #endregion

            #region Горячие клавиши
            ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
                                            {

                                                if (e.KeyCode == Keys.Delete)
                                                {
                                                    InvokeDelete();
                                                }
                                                else if (e.KeyCode == Keys.Space)
                                                {
                                                    InvokeProperties();
                                                }
                                                else if (e.KeyCode == Keys.Divide)
                                                {
                                                    //ListControl.Grid.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders);
                                                    Cursor currentCursor = Cursor.Current;
                                                    Cursor.Current = Cursors.WaitCursor;
                                                    DataGridViewHelper.ExpandGridColumns(ListControl.View, -1, -1);
                                                    Cursor.Current = currentCursor;
                                                }
                                                else if (e.KeyCode == Keys.Multiply)
                                                {
                                                    //ListControl.Grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader);
                                                    Cursor currentCursor = Cursor.Current;
                                                    Cursor.Current = Cursors.WaitCursor;
                                                    DataGridViewHelper.ExpandGridColumns(ListControl.View, -1, 100);
                                                    Cursor.Current = currentCursor;
                                                }
                                                else if (e.KeyCode == Keys.F || e.KeyCode == Keys.F3)
                                                {
                                                    OnFind(FirstSelectedValue);
                                                    e.Handled = true;
                                                }
                                            };
            #endregion


            ListControl.Grid.MouseClick += delegate(object sender, MouseEventArgs eM)
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = GridView.CalcHitInfo(eM.X, eM.Y);
                #region Правая кнопка
                if (eM.Button == MouseButtons.Right)
                {
                }
                #endregion
                #region Левая кнопка
                else if (eM.Button == MouseButtons.Left)
                {
                    // TODO: Контекстное меню
                }
                #endregion
            };
            if (StartValue != null && StartValue.Id != 0)
            {
                BindingSource.CurrencyManager.Position = SourceCollection.FindIndex(s => (s.Id == StartValue.Id));
            }
            Options.LazyInitDataSource = false;
            if (endInit)
                OnAfterBuild();
        }
        /// <summary>
        /// Вызов обработки обновления источника данных
        /// </summary>
        public void InvokeRefresh()
        {
            if (string.IsNullOrEmpty(RootCode))
            {
                if (InitCollectionIsNull)
                {
                    if (!Options.LazyInitDataSource)
                        SourceCollection = Filter == null
                                               ? Workarea.GetCollection<T>(true)
                                               : Workarea.GetCollection<T>(true).FindAll(Filter);
                    else
                        SourceCollection = new List<T>();
                }

                if (SourceCollection == null)
                {
                    if (!Options.LazyInitDataSource)
                        SourceCollection = Filter == null
                                               ? Workarea.GetCollection<T>(true)
                                               : Workarea.GetCollection<T>(true).FindAll(Filter);
                    else
                        SourceCollection = new List<T>();
                }
                else
                {
                    if (Filter != null)
                        SourceCollection = SourceCollection.FindAll(Filter);
                }
            }
            else
            {
                Hierarchy rootHierarchy = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(RootCode);

                if (rootHierarchy == null)
                    throw new Exception(string.Format("Не найдена иерархия {0}", rootHierarchy));

                if (InitCollectionIsNull)
                {
                    if (!Options.LazyInitDataSource)
                        SourceCollection = Filter == null
                                               ? rootHierarchy.GetTypeContents<T>()
                                               : rootHierarchy.GetTypeContents<T>().FindAll(Filter);
                    else
                        SourceCollection = new List<T>();
                }

                if (SourceCollection == null)
                {
                    if (!Options.LazyInitDataSource)
                        SourceCollection = Filter == null
                                               ? rootHierarchy.GetTypeContents<T>()
                                               : rootHierarchy.GetTypeContents<T>().FindAll(Filter);
                    else
                        SourceCollection = new List<T>();
                }
                else
                {
                    if (Filter != null)
                        SourceCollection = SourceCollection.FindAll(Filter);
                }
            }

            // + фильтр безопастности
            if (!Workarea.Access.RightCommon.AdminEnterprize)
            {
                
                if (SourceCollection.Count > 0)
                {
                    T item = SourceCollection[0];
                    Security.ElementRightView secure = Workarea.Access.ElementRightView(item.EntityId);
                    List<int> denyedObject = secure.GetDeny("VIEW");
                    if (denyedObject.Count > 0)
                    {
                        SourceCollection = SourceCollection.Where(p => !denyedObject.Contains(p.Id)).ToList();
                    }
                }
            //    BusinessObjects.Security.EntityRightView secureDbEntity = Workarea.Access.DbentityRightView();
            //    if (secureDbEntity.IsAllow("VIEW", Workarea.Empty<T>().EntityId))
            //    {
            //        BusinessObjects.Security.ElementRightView secure = Workarea.Access.ElementRightView(Workarea.Empty<T>().EntityId);
            //        List<int> denied = secure.GetDeny("VIEW");
            //        if (denied.Count > 0)
            //            SourceCollection = SourceCollection.Where(f => !denied.Contains(f.Id)).ToList();
            //        //List<int> allowed = secure.GetAllowed("VIEW");
            //        //_SourceCollection = SourceCollection.Where(f => allowed.Contains(f.Id)).ToList();
            //    }
            //    else
            //        SourceCollection.Clear();
            }
            if (BindingSource != null)
            {
                BindingSource.DataSource = SourceCollection;
                ListControl.Grid.DataSource = BindingSource;
            }
            if (ListControl.Grid.DataSource != null)
            {
                ListControl.Grid.Refresh();
            }
        }

        /// <summary>
        /// Вызов обработки удаления объекта.  Вызывается событие OnDelete для удаления объекта.
        /// </summary>
        public virtual void InvokeDelete(int[] disableItems = null)
        {
            if (BindingSource.Current == null) return;
            int[] rows = ListControl.View.GetSelectedRows();

            if (rows == null) return;
            if (rows.Length == 1)
            {
                T currentObject = BindingSource.Current as T;
                // при использовании списка из данных хранимой процедуры
                if (currentObject == null)
                {
                    DataRowView rv = BindingSource.Current as DataRowView;
                    if (rv != null)
                    {
                        int id = (int)rv[GlobalPropertyNames.Id];
                        currentObject = Workarea.GetObject<T>(id);
                    }
                }
                if (currentObject != null)
                {
                    if (OnRequestDelete(currentObject)) return;
                    int res = Extentions.ShowMessageChoice(Workarea,
                                                           Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                                           "Удаление объекта",
                                                           "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                           Properties.Resources.STR_CHOICE_DEL, disableItems);
                    if (res == 0)
                    {
                        try
                        {
                            currentObject.Remove();
                            BindingSource.Remove(currentObject);
                        }
                        catch (DatabaseException dbe)
                        {
                            Extentions.ShowMessageDatabaseExeption(Workarea, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                   "Ошибка удаления объекта!", dbe.Message, dbe.Id);
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (res == 1)
                    {
                        try
                        {
                            currentObject.Delete();
                            BindingSource.Remove(currentObject);
                        }
                        catch (DatabaseException dbe)
                        {
                            Extentions.ShowMessageDatabaseExeption(Workarea,
                                                                   Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                   "Ошибка удаления объекта!", dbe.Message, dbe.Id);
                        }
                        catch (Exception ex)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                
                List<DataRowView> removedDataRows = new List<DataRowView>();
                List<T> removedDocuments = new List<T>();
                List<T> documenttodel = new List<T>();

                // Анализ текущих типов выделенных в списке
                T dataObject = ListControl.View.GetRow(0) as T;
                if (dataObject != null) // если в списке объекты...
                {
                    for (int j = rows.Length - 1; j >= 0; j--)
                    {
                        int i = rows[j];
                        T op = ListControl.View.GetRow(i) as T;
                        if (op != null)
                        {
                            removedDocuments.Add(op);
                            documenttodel.Add(op);
                        }
                    }
                }
                else
                {
                    for (int j = rows.Length - 1; j >= 0; j--)
                    {
                        bool docIsRowView = false;
                        DataRowView rv = null;
                        int i = rows[j];
                        rv = ListControl.View.GetRow(i) as DataRowView;
                        if (rv != null)
                        {
                            int docid = (int)rv[GlobalPropertyNames.Id];
                            T op = Workarea.GetObject<T>(docid);
                            removedDataRows.Add(rv);
                            documenttodel.Add(op);
                            docIsRowView = true;
                        }
                    }
                }


                //for (int j = rows.Length - 1; j >= 0; j--)
                //{

                //    bool docIsRowView = false;
                //    DataRowView rv = null;
                //    int i = rows[j];
                //    T op = ListControl.View.GetRow(i) as T;
                //    if (op != null)
                //    {
                //        removedDocuments.Add(op);
                //        documenttodel.Add(op);
                //    }
                //    if (op == null)
                //    {
                //        rv = ListControl.View.GetRow(i) as DataRowView;
                //        if (rv != null)
                //        {
                //            int docid = (int)rv[GlobalPropertyNames.Id];
                //            op = Workarea.GetObject<T>(docid);
                //            removedDataRows.Add(rv);
                //            documenttodel.Add(op);
                //            docIsRowView = true;
                //        }
                //    }
                //}
                if (OnRequestDeleteMany(documenttodel)) return;
                BindingSource.SuspendBinding();
                try
                {
                    int res = Extentions.ShowMessageChoice(Workarea,
                                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                       "Удаление объекта",
                                       "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                       Properties.Resources.STR_CHOICE_DEL);
                    if (res == 0)
                    {
                        ObjectGroupAction<T> actions = new ObjectGroupAction<T> { Workarea = Workarea };
                        actions.FillFromCollection(documenttodel);

                        actions.Action = delegate(T value) { value.Remove(); };
                        actions.ShowDialog();

                        foreach (GroupActionObject v in actions.SourceCollection)
                        {
                            if (!v.Status)
                            {
                                T notDone = documenttodel.Find(f => f.Id == v.Id);
                                removedDocuments.Remove(notDone);
                            }
                        }
                    }
                    else if (res == 1)
                    {
                        ObjectGroupAction<T> actions = new ObjectGroupAction<T> {Workarea = Workarea};
                        actions.FillFromCollection(documenttodel);

                        actions.Action = delegate(T value) { value.Delete(); };
                        actions.ShowDialog();

                        foreach (GroupActionObject v in actions.SourceCollection)
                        {
                            if(!v.Status)
                            {
                                T notDone = documenttodel.Find(f => f.Id == v.Id);
                                removedDocuments.Remove(notDone);
                            }
                        }
                        // TODO:
                        //Workarea.Empty<T>().DeleteList(documenttodel);
                    }

                    bool priorChatty = BindingSource.RaiseListChangedEvents;
                    BindingSource.RaiseListChangedEvents = false;
                    BindingSource.ResetBindings(false);
                    foreach (DataRowView removedDataRow in removedDataRows)
                    {
                        BindingSource.Remove(removedDataRow);
                    }
                    for (int i = removedDocuments.Count-1; i >=0 ; i--)
                    {
                        int idx = BindingSource.IndexOf(removedDocuments[i]);

                        BindingSource.RemoveAt(idx);
                    }
                    BindingSource.RaiseListChangedEvents = priorChatty;

                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(Workarea,
                                                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                            "Ошибка удаления объекта!", dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    BindingSource.ResumeBinding();
                    ListControl.Grid.DataSource = null;
                    ListControl.Grid.DataSource = BindingSource;
                }
            }
        }
        //public virtual void InvokeDelete()
        //{
        //    if (BindingSource.Current == null) return;
        //    int[] rows = ListControl.View.GetSelectedRows();

        //    if (rows == null) return;
        //    if (rows.Length == 1)
        //    {
        //        T currentObject = (T) BindingSource.Current;
        //        if (currentObject != null)
        //        {
        //            if (OnRequestDelete(currentObject)) return;
        //            DialogResult res = DevExpress.XtraEditors.XtraMessageBox.Show("Удалить данные?",
        //                                                                          Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
        //                                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (res == DialogResult.Yes)
        //            {
        //                try
        //                {

        //                    OnDelete(currentObject);
        //                    BindingSource.Remove(currentObject);
        //                }
        //                catch (DatabaseException dbe)
        //                {
        //                    Extentions.ShowMessageDatabaseExeption(Workarea,
        //                                                           Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                           "Ошибка удаления!", dbe.Message, dbe.Id);
        //                }
        //                catch (Exception ex)
        //                {
        //                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
        //                                                               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                               MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        List<DataRowView> removedDataRows = new List<DataRowView>();
        //        List<T> removedDocuments = new List<T>();
        //        List<T> documenttodel = new List<T>();
        //        for (int j = rows.Length - 1; j >= 0; j--)
        //        {

        //            bool docIsRowView = false;
        //            DataRowView rv = null;
        //            int i = rows[j];
        //            T op = ListControl.View.GetRow(i) as T;
        //            if (op != null)
        //            {
        //                removedDocuments.Add(op);
        //                documenttodel.Add(op);
        //            }
        //            if (op == null)
        //            {
        //                rv = ListControl.View.GetRow(i) as DataRowView;
        //                if (rv != null)
        //                {
        //                    int docid = (int)rv[GlobalPropertyNames.Id];
        //                    op = Workarea.GetObject<T>(docid);
        //                    removedDataRows.Add(rv);
        //                    documenttodel.Add(op);
        //                    docIsRowView = true;
        //                }
        //            }
        //        }
        //        BindingSource.SuspendBinding();
        //        try
        //        {
        //            foreach (T opdel in documenttodel)
        //            {
        //                opdel.Delete();
        //            }
        //            // TODO:
        //            //Workarea.Empty<T>().DeleteList(documenttodel);
                    

        //            foreach (DataRowView removedDataRow in removedDataRows)
        //            {
        //                BindingSource.Remove(removedDataRow);
        //            }
        //            foreach (T removedDocument in removedDocuments)
        //            {
        //                BindingSource.Remove(removedDocument);
        //            }

        //        }
        //        catch (DatabaseException dbe)
        //        {
        //            Extentions.ShowMessageDatabaseExeption(Workarea,
        //                                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                    "Ошибка удаления!", dbe.Message, dbe.Id);
        //        }
        //        catch (Exception ex)
        //        {
        //            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
        //                                                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        finally
        //        {
        //            BindingSource.ResumeBinding();
        //        }
        //    }

        //}
        /// <summary>
        /// Вызов обработки отображения свойств объекта.  Вызывается событие OnShowProperty для свойств объекта.
        /// </summary>
        public virtual void InvokeProperties()
        {
            T currentObject = BindingSource.Current as T;
            if (currentObject != null)
            {
                OnShowProperty(currentObject);
            }
            else
            {
                DataRowView rv = BindingSource.Current as DataRowView;
                if (rv != null)
                {
                    int id = (int)rv[GlobalPropertyNames.Id];
                    T item = Workarea.GetObject<T>(id);
                    OnShowProperty(item);
                }
            }
        }
        void ViewCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                T imageItem = BindingSource[e.ListSourceRowIndex] as T;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
                else
                {
                    DataRowView rv = BindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId) && rv.DataView.Table.Columns.Contains("KindId"))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        int KindId = (int)rv["KindId"];
                        e.Value = ExtentionsImage.GetImage(Workarea, KindId, stId);
                    }
                    // возможно это EntityType ???
                    else if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.Id) && typeof(T) == typeof(EntityType))
                    {
                        int id = (int)rv[GlobalPropertyNames.Id];
                        EntityType entype = Workarea.CollectionEntities.FirstOrDefault(f => f.Id == id);
                        e.Value = entype.GetImage();
                    }
                    // возможно это EntityType ???
                    else if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.Id) && typeof(T) == typeof(Document))
                    {
                        EntityType entype = Workarea.CollectionEntities.FirstOrDefault(f => f.Id == 20);
                        e.Value = entype.GetImage();
                    }
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                T imageItem = BindingSource[e.ListSourceRowIndex] as T;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
                else
                {
                    DataRowView rv = BindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        e.Value = ExtentionsImage.GetImageState(Workarea, stId);
                    }
                }
            }
        }
        //void GridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    if (e.Column.Name == "colStateImage")
        //    {
        //        // TODO:
        //        //System.Drawing.Rectangle r = e.Bounds;
        //        //T currentValue = e.Column.View.GetRow(e.RowHandle) as T;
        //        //System.Drawing.Image img = currentValue.State.GetImage();
        //        //if (img != null)
        //        //    e.Graphics.DrawImageUnscaled(img, r);
        //        //e.Handled = true;
        //    }
        //    if (e.Column.Name == "colImage")
        //    {

        //        // TODO:
        //        //System.Drawing.Rectangle r = e.Bounds;
        //        //T currentValue = e.Column.View.GetRow(e.RowHandle) as T;
        //        //System.Drawing.Image img = currentValue.GetImage();
        //        //if (img != null)
        //        //    e.Graphics.DrawImageUnscaled(img, r);
        //        //e.Handled = true;
        //    }

        //}
        #endregion
        /// <summary>
        /// Форма-владелец
        /// </summary>
        internal FormProperties Owner { get; set; }
        
    }
}