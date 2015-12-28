using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Задачи"
    /// </summary>
    public sealed class ContentModuleTask : ContentModuleBase<Task>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Задачи".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleTask()
        {
            Caption = "Задачи";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModuleCreateTreeList;
        }
        private TabbedControlGroup _tabbedProperties;
        //private DevExpress.XtraEditors.MemoEdit _cAgGeneral;
        //private DevExpress.XtraEditors.RichTextEdit _cAgGeneral;
        private DevExpress.XtraRichEdit.RichEditControl _cAgGeneral;
        void ContentModuleCreateTreeList(object sender, EventArgs e)
        {
            if (btnCommon != null) btnCommon.Visibility = BarItemVisibility.Always;
            TreeListBrowser.ListBrowserBaseObjects.GridView.SelectionChanged += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue == null) return;
                if (btnCommon != null && btnCommon.Checked)
                    TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                UpdateProperties();
            };

            TreeListBrowser.ListBrowserBaseObjects.GridView.DataSourceChanged += delegate
            {
                TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                TreeListBrowser.ListBrowserBaseObjects.SelectedValues.Clear();
            };

            LayoutControlGroup GroupGeneral = new LayoutControlGroup { Text = "Общие", Name = "GroupGeneral" };
            _tabbedProperties = new TabbedControlGroup();
            _tabbedProperties.AddTabPage(GroupGeneral);
            _tabbedProperties.SelectedTabPage = GroupGeneral;

            LayoutControlGroup groupLayout = new LayoutControlGroup();
            groupLayout.Add(_tabbedProperties);
            groupLayout.TextVisible = false;
            groupLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(0);

            LayoutControl baseLayout = new LayoutControl();
            baseLayout.Dock = DockStyle.Fill;
            baseLayout.Root = groupLayout;
            TreeListBrowser._control.SplitProperyListControl.Panel2.Controls.Add(baseLayout);

            #region Подготовка общих свойств
            LayoutControlItem layGeneral = new LayoutControlItem();
            _cAgGeneral = new DevExpress.XtraRichEdit.RichEditControl(); //new DevExpress.XtraEditors.RichEditEdit(); //new DevExpress.XtraEditors.MemoEdit();
            //_cAgGeneral.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            //_cAgGeneral.Properties.Appearance.Options.UseTextOptions = true;
            _cAgGeneral.Dock = DockStyle.Fill;
            _cAgGeneral.Enabled = true;
            _cAgGeneral.ReadOnly = true;
            _cAgGeneral.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            //_cAgGeneral.Properties.ReadOnly = true;
            layGeneral.Control = _cAgGeneral;
            layGeneral.Text = " ";
            GroupGeneral.Add(layGeneral);
            #endregion
        }
        private void UpdateProperties()
        {
            Task ag = TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;

            if (ag == null) return;
            switch (_tabbedProperties.SelectedTabPage.Name)
            {
                case "GroupGeneral":
                    #region Заполнение общих свойств
                    _cAgGeneral.HtmlText = ag.Memo;
                    #endregion
                    break;
                
            }
        }
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.TASK_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Task> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Task> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Task> _saveAnalitic;
        /// <summary>
        /// Делегат заполнения данных при создании нового объекта
        /// </summary>
        private Action<Task, Task> _createNew;
        /// <summary>
        /// Реализация метода заполнения начальных данных при создании нового объекта по шаблону 
        /// </summary>
        /// <remarks>При создании новой задачи автоматически устанавливается текущий пользователь как автор 
        /// и исполнитель, дата начала задачи в текущую, дата планового запуска в текущую.</remarks>
        /// <param name="value">Значение</param>
        /// <param name="tml">Шаблон</param>
        void OnCreateNew(Task value, Task tml)
        {
            value.DateStart = DateTime.Today;
            value.DateStartTime = DateTime.Now.TimeOfDay;
            value.DateStartPlan = DateTime.Today;
            value.DateStartPlanTime = value.DateStartTime;
            value.UserOwnerId = Workarea.CurrentUser.Id;
            value.UserToId = value.UserOwnerId;
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
        void OnShowProp(Task value)
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
        /// Реализация метода отображения свойств объекта при просмотре в виде групп
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowPropTreeList(Task value)
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
        /// Реализация метода сохранения объекта
        /// </summary>
        /// <param name="value">Объект для сохранения</param>
        void OnSaveObject(Task value)
        {
            value.Save();
        }
        /// <summary>
        /// Обработчик события отображения модуля
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<Task>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Task>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<Task>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
                if (_createNew == null)
                {
                    _createNew = new Action<Task, Task>(OnCreateNew);
                    TreeListBrowser.ListBrowserBaseObjects.CreateNew += _createNew;
                }
            }
        }
    }
}