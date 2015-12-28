using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// ����� ���������� ������� ��� �������� �������������� ��������� IBase
    /// </summary>
    /// <typeparam name="T">��� �������</typeparam>
    public class ListBrowserBaseObjects<T> : ListBrowserCore<T> where T : class, IBase, new()//BaseCore<T>
    {
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="sourceCollection">��������� ��� �����������</param>
        /// <param name="filter">������</param>
        /// <param name="startValue">��������� ������</param>
        /// <param name="allowContextMenu">��������� ����������� ����</param>
        /// <param name="showToolbar">���������� ���������� ������ � �������� ����������</param>
        /// <param name="multySelect">��������� ������������� �����</param>
        /// <param name="standartAction">������������ ����������� ��������</param>
        public ListBrowserBaseObjects(IWorkarea wa, List<T> sourceCollection, Predicate<T> filter, T startValue, bool allowContextMenu, bool showToolbar, bool multySelect, bool standartAction)
            :base(wa, sourceCollection, filter, startValue, allowContextMenu, showToolbar, multySelect,standartAction)
        {
            RestrictedTemplateKinds = new List<int>();
        }
        /// <summary>
        /// ��������� �������������� ����� ����� ���������-��������
        /// </summary>
        /// <remarks>������������ ��� ����������� ������� � ���� "�������"</remarks>
        public List<int> RestrictedTemplateKinds { get; set; }

        #region ������

        /// <summary>
        /// ��������� ����������
        /// </summary>
        /// <param name="endInit"></param>
        public override void Build(bool endInit=true)
        {
            base.Build(false);
            #region ������� ����� ���������
            if (Options.StandartAction)
            {

                List<T> tmpcollectionTemplates = Workarea.GetTemplates<T>();
                List<T> collectionTemplates = tmpcollectionTemplates;
                if(RestrictedTemplateKinds!=null && RestrictedTemplateKinds.Count>0)
                {
                    collectionTemplates =
                        tmpcollectionTemplates.Where(f => RestrictedTemplateKinds.Contains(f.KindValue)).ToList();

                }

                
                //PopupMenu mnuTemplates = ListControl.CreateMenu;
                foreach (T itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem();
                    ListControl.CreateMenu.AddItem(btn);
                    btn.Caption = itemTml.Name;
                    btn.Glyph = itemTml.GetImage();
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                                     {
                                         T objectTml = (T)btn.Tag;
                                         T newObject = objectTml.Workarea.CreateNewObject(objectTml);
                                         OnCreateNew(newObject, objectTml);
                                         OnShowProperty(newObject);
                                     };

                    if (Owner != null)
                    {
                        BarButtonItem btn2 = new BarButtonItem();
                        btn2.Caption = itemTml.Name;
                        btn2.Glyph = itemTml.GetImage();
                        btn2.Tag = itemTml;
                        btn2.ItemClick += delegate
                        {
                            T objectTml = (T)btn2.Tag;
                            T newObject = objectTml.Workarea.CreateNewObject(objectTml);
                            OnCreateNew(newObject, objectTml);
                            OnShowProperty(newObject);
                        };
                        Owner.CreateMenu.AddItem(btn2);
                    }
                }
                //ListControl.btnNew.DropDown = mnuTemplates;
            }
            #endregion
            if (endInit)
                OnAfterBuild();
            //if (ListControl != null)
            //    return;

            //ListControl = new Controls.ControlList();
            ////TODO: _ListControl.Grid.MultiSelect = _MultySelect;
            //GridView.OptionsBehavior.Editable = false;
            //GridView.OptionsBehavior.AutoPopulateColumns = false;
            //GridView.OptionsBehavior.AutoSelectAllInEditor = false;
            //GridView.OptionsCustomization.AllowRowSizing = false;
            //GridView.OptionsDetail.EnableMasterViewMode = false;
            //GridView.OptionsDetail.ShowDetailTabs = false;
            //GridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            //GridView.OptionsSelection.MultiSelect = true;
            //GridView.OptionsSelection.UseIndicatorForSelection = false;
            ////GridView.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate;
            ////GridView.OptionsView.ColumnAutoWidth = false;
            //GridView.OptionsView.ShowDetailButtons = false;
            //GridView.OptionsView.ShowIndicator = false;
            //GridView.RowHeight = 0;
            //GridView.SynchronizeClones = false;
            ////_ListControl.Grid.MultiSelect = _MultySelect;
            //InvokeRefresh();
            ////
            //BindingSource = new BindingSource();
            //ListViewCode = StartValue != null
            //                      ? string.Format("DEFAULT_LISTVIEW{0}", StartValue.GetType().Name.ToUpper())
            //                      : string.Format("DEFAULT_LISTVIEW{0}", typeof(T).Name.ToUpper());
            //DataGridViewHelper.GenerateGridColumns(Workarea, GridView, ListViewCode);

            //BindingSource.DataSource = SourceCollection;
            //ListControl.Grid.DataSource = BindingSource;

            //#region ��������������
            //GridView.CustomUnboundColumnData += ViewCustomUnboundColumnData;
            //// TODO: �������� �������� �������������� ��� ����� �������
            //#endregion

            //#region ������� ����� ���������
            //if (StandartAction)
            //{
            //    List<T> collectionTemplates = Workarea.GetTemplates<T>();

            //    //PopupMenu mnuTemplates = ListControl.CreateMenu;
            //    foreach (T itemTml in collectionTemplates)
            //    {
            //        BarButtonItem btn = new BarButtonItem();
            //        ListControl.CreateMenu.AddItem(btn);
            //        btn.Caption = itemTml.Name;
            //        btn.Glyph = itemTml.GetImage();
            //        btn.Tag = itemTml;
            //        btn.ItemClick += delegate
            //                         {
            //            T objectTml = (T)btn.Tag;
            //            T newObject = objectTml.Workarea.CreateNewObject(objectTml);
            //            OnCreateNew(newObject, objectTml);
            //            OnShowProperty(newObject);
            //        };

            //        if(Owner!=null)
            //        {
            //            BarButtonItem btn2 = new BarButtonItem();
            //            btn2.Caption = itemTml.Name;
            //            btn2.Glyph = itemTml.GetImage();
            //            btn2.Tag = itemTml;
            //            btn2.ItemClick += delegate
            //            {
            //                T objectTml = (T)btn2.Tag;
            //                T newObject = objectTml.Workarea.CreateNewObject(objectTml);
            //                OnCreateNew(newObject, objectTml);
            //                OnShowProperty(newObject);
            //            };
            //            Owner.CreateMenu.AddItem(btn2);
            //        }
            //    }
            //    //ListControl.btnNew.DropDown = mnuTemplates;
            //}
            //#endregion

            //#region ��������
            //if (ShowProperiesOnDoudleClick)
            //    ListControl.Grid.DoubleClick += delegate
            //    {
            //        System.Drawing.Point p = ListControl.Grid.PointToClient(Control.MousePosition);
            //        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = GridView.CalcHitInfo(p.X, p.Y);
            //        if (hit.InRowCell)
            //        {
            //            InvokeProperties();
            //        }
            //    };
            //#endregion

            //#region ��������
            //if (StandartAction)
            //{
            //}
            //#endregion

            //#region ����������
            //if (StandartAction)
            //{
            //    //ListControl.btnAcl.ItemClick += delegate
            //    //{
            //    //    if (BindingSource != null && BindingSource.Current != null)
            //    //    {
            //    //        T currentObject = BindingSource.Current as T;
            //    //        if (currentObject != null)
            //    //        {
            //    //            // TODO: �������� ����������
            //    //           // currentObject.BrowseRightsElement();
            //    //        }
            //    //    }
            //    //};
            //}
            //#endregion

            //ListControl.Grid.MouseClick += delegate(object sender, MouseEventArgs eM)
            //{
            //    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = GridView.CalcHitInfo(eM.X, eM.Y);
            //    #region ������ ������
            //    if (eM.Button == MouseButtons.Right)
            //    {
            //    }
            //    #endregion
            //    #region ����� ������
            //    else if (eM.Button == MouseButtons.Left)
            //    {
            //        // TODO: ����������� ����
            //    }
            //    #endregion
            //};
            //#region ������� �������
            //ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            //{

            //    if (e.KeyCode == Keys.Delete)
            //    {
            //        InvokeDelete();
            //        //ListControl.btnDelete.PerformClick();
            //    }
            //    else if (e.KeyCode == Keys.Space)
            //    {
            //        InvokeProperties();
            //        //ListControl.btnProperties.PerformClick();
            //    }
            //    else if (e.KeyCode == Keys.Divide)
            //    {
            //        Cursor currentCursor = Cursor.Current;
            //        Cursor.Current = Cursors.WaitCursor;
            //        DataGridViewHelper.ExpandGridColumns(ListControl.View, -1, -1);
            //        Cursor.Current = currentCursor;
            //    }
            //    else if (e.KeyCode == Keys.Multiply)
            //    {
            //        Cursor currentCursor = Cursor.Current;
            //        Cursor.Current = Cursors.WaitCursor;
            //        DataGridViewHelper.ExpandGridColumns(ListControl.View, -1, 100);
            //        Cursor.Current = currentCursor;
            //        //ListControl.View.BestFitColumns();
            //    }
            //    else if (e.KeyCode == Keys.F || e.KeyCode == Keys.F3)
            //    {
            //        OnFind(FirstSelectedValue);
            //        e.Handled = true;
            //    }
            //};
            //#endregion

            //if (StartValue != null && StartValue.Id != 0)
            //{
            //    BindingSource.CurrencyManager.Position = SourceCollection.FindIndex(s => (s.Id == StartValue.Id));
            //}
            //LazyInitDataSource = false;
            //OnAfterBuild();
        }
        #endregion

    }
}