using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Просмотр объектов в виде списка по группам
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete]
    public class BrowseTreeList<T> where T : class, IBase, new()
    {
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea { get; set; }
        private TreeListBrowser<T> TreeListBrowser { get; set; }
        // Форма отображения
        private FormProperties _form;
        /// <summary>
        /// Показать диалог выбора 
        /// </summary>
        public void ShowDialog()
        {
            _form = new FormProperties();
            new FormStateMaintainer(_form, string.Format("Property{0}", typeof(T).Name));
            _form.btnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            _form.btnSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            _form.btnSelect.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32);
            TreeListBrowser = new TreeListBrowser<T> {ShowCheckSingle = true, ShowCheckAll = true, Workarea = Workarea};

            TreeListBrowser.Build();
            _form.clientPanel.Controls.Add(TreeListBrowser.Control);
            TreeListBrowser.Control.Dock = DockStyle.Fill;
            _form.btnSelect.ItemClick += BtnSelectItemClick;
            _form.ShowDialog();
        }

        void BtnSelectItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _selectedvalue = TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;
            _selectedValues = TreeListBrowser.ListBrowserBaseObjects.SelectedValues;
            _form.Close();
        }

        private List<T> _selectedValues;
        /// <summary>
        /// Коллекция выбранных объектов
        /// </summary>
        public List<T> SelectedValues
        {
            get{ return _selectedValues;}
        }
        private T _selectedvalue;
        /// <summary>
        /// Первый выбранный элемент списка
        /// </summary>
        public T SelectedValue
        {
            get { return _selectedvalue; }
        }

        public List<int> CheckedLeftNodeId { get { return TreeListBrowser.CheckedLeftNodeId; } }
        public List<int> CheckedRightNodeId { get { return TreeListBrowser.CheckedRightNodeId; } }
        public List<int> CheckedLeftNodeHierarchyId { get { return TreeListBrowser.CheckedLeftNodeHierarchyId; } }
        public List<int> CheckedRightNodeHierarchyId { get { return TreeListBrowser.CheckedRightNodeHierarchyId; } }
    }
}