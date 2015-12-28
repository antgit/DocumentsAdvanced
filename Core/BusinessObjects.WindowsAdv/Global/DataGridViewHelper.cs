using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;

namespace BusinessObjects.Windows
{
    
    internal static class DataGridViewHelper
    {
        public static void GenerateGridColumns(IWorkarea wa, DevExpress.XtraGrid.Views.Grid.GridView view, string sysCode)
        {
            SystemParameter prm = wa.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>(sysCode);
            if (prm != null && prm.ReferenceId!=0)
            {
                GenerateGridColumns(wa, view, prm.ReferenceId);
            }
            else
            {
                CustomViewList list = wa.Cashe.GetCasheData<CustomViewList>().ItemCode<CustomViewList>(sysCode);
                if(list!=null)
                {
                    GenerateGridColumns(wa, view, list.Id);    
                }
            }
        }

        
        public static void GenerateGridColumns(IWorkarea wa, DevExpress.XtraGrid.Views.Grid.GridView view, int viewId)
        {
            view.Columns.Clear();
            CustomViewList list = wa.Cashe.GetCasheData<CustomViewList>().Item(viewId);
            view.OptionsView.ShowAutoFilterRow = list.AutoFilterVisible;
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowGroupPanel = list.GroupPanelVisible;
            view.OptionsView.ShowIndicator = list.ShowIndicator;
            
            List<CustomViewColumn> columns = list.Columns.Where(s=>s.StateId== State.STATEACTIVE).ToList();

            foreach (CustomViewColumn c in columns)
            {
                if (c.Code == "colImage" || c.Code == "colStateImage" || c.Code == "colImageRight")
                {
                    DevExpress.XtraGrid.Columns.GridColumn colImage = new DevExpress.XtraGrid.Columns.GridColumn
                                                                          {
                                                                              Name = c.Code,
                                                                              Caption = c.Name,
                                                                              FieldName = c.DataProperty,
                                                                              Width = 17,
                                                                              Visible = c.Visible
                                                                          };
                    if(c.Visible)
                        colImage.VisibleIndex = c.OrderNo;
                    RepositoryItemPictureEdit repositoryItemPictureEdit1 = new RepositoryItemPictureEdit();
                    view.GridControl.RepositoryItems.AddRange(new RepositoryItem[] {repositoryItemPictureEdit1});
                    colImage.ColumnEdit = repositoryItemPictureEdit1;
                    colImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                    colImage.OptionsColumn.AllowSize = false;
                    colImage.OptionsColumn.ShowCaption = c.DisplayHeader;
                    colImage.OptionsColumn.FixedWidth = true;
                    colImage.OptionsColumn.AllowSort= DevExpress.Utils.DefaultBoolean.False;
                    colImage.OptionsFilter.AllowFilter = false;
                    view.Columns.Add(colImage);
                }
                else if(c.DataType=="Image")
                {
                    DevExpress.XtraGrid.Columns.GridColumn colImage = new DevExpress.XtraGrid.Columns.GridColumn
                                                                          {
                                                                              Name = c.Code,
                                                                              Caption = c.Name,
                                                                              FieldName = c.DataProperty,
                                                                              Width = c.With,
                                                                              Visible = c.Visible
                                                                          };
                    if (c.Visible)
                        colImage.VisibleIndex = c.OrderNo;
                    RepositoryItemPictureEdit repositoryItemPictureEdit1 = new RepositoryItemPictureEdit();
                    view.GridControl.RepositoryItems.AddRange(new RepositoryItem[] { repositoryItemPictureEdit1 });
                    colImage.ColumnEdit = repositoryItemPictureEdit1;
                    colImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                    colImage.OptionsColumn.AllowSize = true;
                    colImage.OptionsColumn.ShowCaption = c.DisplayHeader;
                    colImage.OptionsColumn.FixedWidth = false;
                    colImage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    colImage.OptionsFilter.AllowFilter = false;
                    view.Columns.Add(colImage);
                    colImage.GroupIndex = c.GroupIndex;
                }
                else if(c.DataType=="System.Boolean")
                {
                    DevExpress.XtraGrid.Columns.GridColumn colBoolean = new DevExpress.XtraGrid.Columns.GridColumn
                                                                            {
                                                                                Name = c.Code,
                                                                                Caption = c.Name,
                                                                                FieldName = c.DataProperty,
                                                                                Width = c.With,
                                                                                Visible = c.Visible
                                                                            };
                    if (c.Visible)
                        colBoolean.VisibleIndex = c.OrderNo;
                    RepositoryItemCheckEdit repositoryItemBool = new RepositoryItemCheckEdit();
                    view.GridControl.RepositoryItems.AddRange(new RepositoryItem[] {repositoryItemBool});
                    colBoolean.ColumnEdit = repositoryItemBool;
                    colBoolean.OptionsColumn.AllowSize = true;
                    colBoolean.OptionsColumn.ShowCaption = c.DisplayHeader;
                    view.Columns.Add(colBoolean);
                    colBoolean.GroupIndex = c.GroupIndex;
                }
                else
                {
                    DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn
                                                                     {
                                                                         Name = c.Code,
                                                                         Caption = (c.DisplayHeader ? c.Name : string.Empty),
                                                                         Width = c.With,
                                                                         VisibleIndex = c.OrderNo,
                                                                         Visible = c.Visible,
                                                                         FieldName = c.DataProperty
                                                                     };
                    if (!string.IsNullOrEmpty(c.Format))
                    {
                        if (c.DataType == "System.DateTime")
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        if (c.DataType == "System.TimeSpan")
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        if (c.DataType == "System.Decimal")
                            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.DisplayFormat.FormatString = c.Format;
                    }
                    if (c.KindValue == CustomViewColumn.KINDVALUE_COMPUTED)
                    {
                        col.ShowUnboundExpressionMenu = true;
                        col.UnboundExpression = c.Formula;
                        col.OptionsColumn.AllowEdit = false;
                        if (c.DataType == "System.DateTime")
                            col.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
                        if (c.DataType == "System.Decimal")
                            col.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                        if (c.DataType == "System.Int32")
                            col.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                        if (c.DataType == "System.String")
                            col.UnboundType = DevExpress.Data.UnboundColumnType.String;
                        if (c.DataType == "System.Object")
                            col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                    }
                    view.Columns.Add(col);
                    col.GroupIndex = c.GroupIndex;
                }
                
            }
        }

        


        public static void GenerateLookUpColumns(IWorkarea wa, DevExpress.XtraEditors.SearchLookUpEdit control, string code)
        {
            CustomViewList list = null;
            SystemParameter prm = wa.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>(code);
            if (prm != null && prm.ReferenceId != 0)
            {
                list = wa.Cashe.GetCasheData<CustomViewList>().Item(prm.ReferenceId);
            }
            else
                list = wa.Cashe.GetCasheData<CustomViewList>().ItemCode<CustomViewList>(code);

            GenerateGridColumns(wa, control.Properties.View, list.Id);   
        }
        public static void GenerateLookUpColumns(IWorkarea wa, DevExpress.XtraEditors.LookUpEdit control, string code)
        {
            CustomViewList list = null;
            SystemParameter prm = wa.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>(code);
            if (prm != null && prm.ReferenceId!=0)
            {
                list = wa.Cashe.GetCasheData<CustomViewList>().Item(prm.ReferenceId);
            }
            else
                list = wa.Cashe.GetCasheData<CustomViewList>().ItemCode<CustomViewList>(code);
            foreach (CustomViewColumn c in list.Columns.Where(f => f.Visible))
            {

                if (c.Code == "colImage" || c.Code == "colStateImage")
                {
                    // TODO:
                    //LookUpColumnInfo colImage = new LookUpColumnInfo();
                    //colImage.Caption = string.Empty;
                    //colImage.Width = 17;
                    //colImage.FieldName = c.DataProperty;
                    //control.Properties.Columns.Add(colImage);
                }
                else
                {
                    LookUpColumnInfo col = new LookUpColumnInfo {Caption = c.Name, Width = c.With, FieldName = c.DataProperty};
                    control.Properties.Columns.Add(col);
                }
            }
        }
        public static void GenerateTreeGreedColumns(IWorkarea wa, DevExpress.XtraTreeList.TreeList control, string code)
        {
            CustomViewList list = null;
            SystemParameter prm = wa.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>(code);
            if (prm != null && prm.ReferenceId != 0)
            {
                list = wa.Cashe.GetCasheData<CustomViewList>().Item(prm.ReferenceId);
            }
            else
                list = wa.Cashe.GetCasheData<CustomViewList>().ItemCode<CustomViewList>(code);
            foreach (CustomViewColumn c in list.Columns)
            {
                /*
                 DevExpress.XtraTreeList.Columns.TreeListColumn colId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                colId.Caption = "ָה";
                colId.FieldName = GlobalPropertyNames.Id;
                colId.Visible = false;
                 */
                if (c.Code == "colImage" || c.Code == "colStateImage")
                {
                    // TODO:
                    //LookUpColumnInfo colImage = new LookUpColumnInfo();
                    //colImage.Caption = string.Empty;
                    //colImage.Width = 17;
                    //colImage.FieldName = c.DataProperty;
                    //control.Properties.Columns.Add(colImage);
                }
                else
                {
                    DevExpress.XtraTreeList.Columns.TreeListColumn col = control.Columns.Add();
                    col.Caption = c.Name;
                    col.Width = c.With;
                    col.FieldName = c.DataProperty;
                    col.Visible = c.Visible;
                    if (!string.IsNullOrEmpty(c.Format))
                    {
                        if (c.DataType == "System.DateTime")
                            col.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
                        if (c.DataType == "System.Decimal")
                            col.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                        col.Format.FormatString = c.Format;
                    }
                    
                }
            }
        }
        public static void ExpandGridColumns(DevExpress.XtraGrid.Views.Grid.GridView view, int maxWith, int rowCount)
        {
            view.BeginUpdate();
            int fitMaxRowCount = view.BestFitMaxRowCount;
            view.BestFitMaxRowCount = rowCount;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
            {
                if (col.Visible)
                {
                    col.BestFit();
                    if (maxWith > -1 && col.Width > maxWith) col.Width = maxWith;
                }

            }
            view.BestFitMaxRowCount = fitMaxRowCount;
            view.EndUpdate();
        }
    }
}