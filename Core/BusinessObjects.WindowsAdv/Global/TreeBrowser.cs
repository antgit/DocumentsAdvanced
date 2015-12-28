using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Правило формирования дерева на основе связей
    /// </summary>
    public sealed class ChainRule
    {
        private ChainKind _ChainKind;
        /// <summary>
        /// Тип связи
        /// </summary>
        public ChainKind ChainKind
        {
            get { return _ChainKind; }
        }

        private bool _IsActive;
        /// <summary>
        /// Используется или не используется при постраении дерева
        /// </summary>
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        public ChainRule(ChainKind ChainKind, bool IsActive)
        {
            _ChainKind = ChainKind;
            _IsActive = IsActive;
        }

        public ChainRule(ChainKind ChainKind)
        {
            _ChainKind = ChainKind;
            _IsActive = true;
        }
    }
    
    /// <summary>
    /// Дополнительный класс для просмотра объектов в виде дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeBrowser<T> where T : class, IBase, new()
    {

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        public TreeBrowser(Workarea wa)
        {
            _workarea = wa;
            _allowDragDrop = true;
            CheckedRightNodes = new List<TreeListNode>();
            CheckedLeftNodes = new List<TreeListNode>();
            _ChainsRules = new List<ChainRule>();
            
            ActiveHasChild = false;
        }
       
        #region Свойства

        private List<string> _excludeHierarchies;

        /// <summary>
        /// Список иерархий исключающихся при построении дерева
        /// </summary>
        public List<string> ExcludeHierarchies
        {
            get
            {
                if (_excludeHierarchies == null)
                    _excludeHierarchies = new List<string>();
                return _excludeHierarchies;
            }
        }

        /// <summary>
        /// Отображать колонку "Отметить текущий"
        /// </summary>
        public bool ShowCheckSingle { get; set; }
        /// <summary>
        /// Отображать колонку "Отметить все вложенные"
        /// </summary>
        public bool ShowCheckAll { get; set; }
        /// <summary>
        /// Список идентификаторов отмеченных(выбранных) нодов-элементов слева, "отмеченные текущие, без учета вложенных"
        /// </summary>
        public List<int> CheckedLeftNodeId
        {
            get
            {
                return new List<int>();
            }
            set 
            {
                
            }
        }
        /// <summary>
        /// Список идентификаторов отмеченных(выбранных) нодов-элементов справа, "отмеченные с учетом вложенных"
        /// </summary>
        public List<int> CheckedRightNodeId
        {
            get
            {
                return new List<int>();
            }
            set 
            {
                
            }
        }
        /// <summary>
        /// Список идентификаторов отмеченных(выбранных) нодов иерархий слева, "отмеченные текущие, без учета вложенных"
        /// </summary>
        public List<int> CheckedLeftNodeHierarchyId
        {
            get
            {
                string hierarchyEntityId=Workarea.Empty<Hierarchy>().EntityId.ToString();
                return CheckedLeftNodes.Where(s => (Workarea.Cashe.GetCasheData<Hierarchy>().Item(s.Id).EntityId.ToString() == hierarchyEntityId)).Select(s => (int)s.GetValue(GlobalPropertyNames.Id)).ToList();
            }
            set
            {
                if (value != null)
                {
                    foreach (int i in value)
                    {
                        Hierarchy hierarchy = Workarea.Cashe.GetCasheData<Hierarchy>().Item(i);
                        JumpOnHierachy(hierarchy);
                        if (ControlTree.Tree.Selection.Count > 0)
                        {
                            //TreeListNode Node = ControlTree.Tree.Selection[0];
                            TreeListNode node = ControlTree.Tree.FocusedNode;
                            node.SetValue("CheckSingle", true);
                            CheckedLeftNodes.Add(node);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Список идентификаторов отмеченных(выбранных) нодов иерархий справа, "отмеченные с учетом вложенных"
        /// </summary>
        public List<int> CheckedRightNodeHierarchyId
        {
            get
            {
                string hierarchyEntityId = Workarea.Empty<Hierarchy>().EntityId.ToString();
                return CheckedRightNodes.Where(s => (Workarea.Cashe.GetCasheData<Hierarchy>().Item(s.Id).EntityId.ToString() == hierarchyEntityId)).Select(s => (int)s.GetValue(GlobalPropertyNames.Id)).ToList();
            }
            set
            {
                if (value != null)
                {
                    foreach (int i in value)
                    {
                        Hierarchy hierarchy = Workarea.Cashe.GetCasheData<Hierarchy>().Item(i);
                        JumpOnHierachy(hierarchy);
                        if (ControlTree.Tree.Selection.Count > 0)
                        {
                            //TreeListNode Node = ControlTree.Tree.Selection[0];
                            TreeListNode node = ControlTree.Tree.FocusedNode;
                            RecursivelySetNodeValue(node, "CheckAll", true, false);
                            RecursivelySetNodeValue(node, "CheckSingle", null, true);
                            CheckedRightNodes.Add(node);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Коллекция отмеченных нодов по столбцу с учетом вложенных
        /// </summary>
        public List<TreeListNode> CheckedRightNodes { get; set; }
        /// <summary>
        /// Коллекция отмеченных нодов по столбцу "Текущий"
        /// </summary>
        public List<TreeListNode> CheckedLeftNodes { get; set; }
        /// <summary>
        /// Корень с которого необходимо заполнить дерево
        /// </summary>
        public string RootCode{ get; set; }
        private bool _allowDragDrop;

        /// <summary>
        /// Разрешить операции Drag&Drop
        /// </summary>
        public bool AllowDragDrop
        {
            get { return _allowDragDrop; }
            set
            {
                if (value != _allowDragDrop)
                {
                    _allowDragDrop = value;
                    if (_allowDragDrop)
                        InitDragDropList();
                    else
                        RemoveDragDrop();
                }

            }
        }

        /// <summary>
        /// Отображать содержимое в дереве
        /// </summary>
        /// <remarks>Если указана данная опция в дереве отображается содержимое иерархии, если не указана - дерево содержит только 
        /// папки иерархий. </remarks>
        public bool ShowContentTree { get; set; }

        /// <summary>
        /// Устанавливает значение HasChild у элементов контента в дереве
        /// </summary>
        public bool ActiveHasChild { get; set; }

        /// <summary>
        /// Значение объекта на который необходимо установить фокус при открытии диалога
        /// </summary>
        public T StartValue { get; set; }

        /// <summary>
        /// Элемент управления "Дерево"
        /// </summary>
        internal ControlTree ControlTree { get; set; }
        /// <summary>
        /// Элемент управления "Дерево"
        /// </summary>
        public Control Control { get { return ControlTree; } }
        /// <summary>
        /// Идентификатор элемента "Состав иерархии" в дереве
        /// </summary>
        public int SelectedHierarchyContentId
        {
            get
            {
                if (ControlTree.Tree.FocusedNode == null) return 0;
                return (Int32)ControlTree.Tree.FocusedNode.GetValue("ContentEntityId");
            }
        }
        /// <summary>
        /// Идентификатор выделенного элемента в дереве
        /// </summary>
        public int SelectedElementId
        {
            get
            {
                if (ControlTree.Tree.FocusedNode == null) return 0;
                return (Int32)ControlTree.Tree.FocusedNode.GetValue("ElementId");
            }
        }
        /// <summary>
        /// Идентификатор выделенной иерархии 
        /// </summary>
        public int SelectedHierarchyId
        {
            get { return (Int32)ControlTree.Tree.Selection[0].GetValue(GlobalPropertyNames.Id); }
        }
        /// <summary>
        /// Является ли текщая ветка в дереве иерархией
        /// </summary>
        public bool SelectedTreeIsHierarchy
        {
            get
            {
                if (ControlTree.Tree.FocusedNode != null)
                    return ((short)ControlTree.Tree.FocusedNode.GetValue("EntityId"))==28;
                return false;
            }
        }
        /// <summary>
        /// Текущая иерархия
        /// </summary>
        public Hierarchy SelectedHierarchy
        {
            get
            {
                Hierarchy currentHierarchy = null;
                if (ControlTree.Tree.Selection != null && ControlTree.Tree.Selection.Count>0)
                {

                    int id;
                    if (!SelectedTreeIsHierarchy)
                        id = (Int32)ControlTree.Tree.Selection[0].ParentNode.GetValue(GlobalPropertyNames.Id);
                    else
                        id = SelectedHierarchyId;
                    if (id != 0)
                        currentHierarchy = Workarea.Cashe.Hierarhies.Item(id);
                    else
                        currentHierarchy = ControlTree.Tree.Selection[0].GetValue("Data") as Hierarchy;
                }
                return currentHierarchy;
            }
        }
        private readonly Workarea _workarea;
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea
        {
            get { return _workarea; }
        }
        private List<ChainRule> _ChainsRules = null;
        /// <summary>
        /// Набор правил формирования дерева на основе связей
        /// </summary>
        public List<ChainRule> ChainsRules
        {
            get { return _ChainsRules; }
        }
        public void RefreshChainRules()
        {
            foreach (ChainKind item in Workarea.CollectionChainKinds)
            {
                if (Workarea.Empty<T>().Entity == item.FromEntity && item.FromEntityId == item.ToEntityId)
                {
                    ChainRule rule = new ChainRule(item);
                    _ChainsRules.Add(rule);
                }
            }
        }
        #endregion


        Security.ElementRightView _secure;
        /// <summary>
        /// Перепрыгнуть на заданную иерархию
        /// </summary>
        /// <param name="targetHierarchy">Иерархия - цель</param>
        public void JumpOnHierachy(Hierarchy targetHierarchy)
        {
            Hierarchy cursor = targetHierarchy;
            List<Hierarchy> chainSteps = new List<Hierarchy>();
            while (cursor.Parent != null)
            {
                chainSteps.Add(cursor);
                cursor = cursor.Parent;
            }
            chainSteps.Add(cursor);
            ProcessJumpTree(ControlTree.Tree.Nodes, chainSteps, chainSteps.Count - 1);
        }

        /// <summary>
        /// Рекурсивный поиск иерархии в дереве
        /// </summary>
        /// <param name="nodes">Текущая коллекция узлов</param>
        /// <param name="steps">Коллекция шагов поиска</param>
        /// <param name="currLevel">Текущий номер шага поиска</param>
        private void ProcessJumpTree(TreeListNodes nodes, List<Hierarchy> steps, int currLevel)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.GetValue(0) != null)
                {
                    int id = (int)node.GetValue(0);
                    if (id == steps[currLevel].Id)
                    {
                        node.Selected = true;
                        if (currLevel > 0)
                        {
                            node.Expanded = true;
                            ProcessJumpTree(node.Nodes, steps, currLevel - 1);
                        }
                        else
                        {
                            ControlTree.Tree.Selection.Clear();
                            ControlTree.Tree.Selection.Add(node);
                            ControlTree.Tree.SetFocusedNode(node);
                        }
                        break;
                    }
                }
            }
        }

        public bool ExternalTree { get; set; }
        /// <summary>
        /// Устанавливает значение ноды и всех вложенных
        /// </summary>
        /// <param name="node">Нода</param>
        /// <param name="column">Устанавлеваемый столбец</param>
        /// <param name="value">Значение</param>
        /// <param name="verification">false-простая установка, true-если столбец установлен в true, то не менять его значение</param>
        private void RecursivelySetNodeValue(TreeListNode node, string column, object value, bool verification)
        {
            if (!(verification && (node.GetValue(column) != null) && (bool)node.GetValue(column)))
            {
                    node.SetValue(column, value);
            }

            foreach (TreeListNode childNode in node.Nodes)
            {
                //Удаляем рекурсивно только те флажки, которых нет в списке выбранных
                if ((value!=null)&&((bool)value == false))
                {
                    if ((column == "CheckAll") && (CheckedRightNodes.Contains(childNode)))
                        continue;
                    if ((column == "CheckSingle") && (CheckedLeftNodes.Contains(childNode)))
                        continue;
                }
                
                RecursivelySetNodeValue(childNode, column, value, verification);
            }
        }

        /// <summary>
        /// Подготовить элемент упраления
        /// </summary>
        public void Build()
        {
            if (!ExternalTree)
            {
                if (ControlTree == null)
                {
                    ControlTree = new ControlTree
                                      {
                                          Dock = DockStyle.Fill
                                      };
                    BuildImageList();

                    #region Колонки дерева

                    DevExpress.XtraTreeList.Columns.TreeListColumn colId = new DevExpress.XtraTreeList.Columns.
                        TreeListColumn
                                                                               {
                                                                                   Caption = "Ид",
                                                                                   FieldName = GlobalPropertyNames.Id,
                                                                                   Visible = false
                                                                               };

                    DevExpress.XtraTreeList.Columns.TreeListColumn colName = new DevExpress.XtraTreeList.Columns.
                        TreeListColumn
                                                                                 {
                                                                                     Caption = "Наименование",
                                                                                     FieldName = "Name",
                                                                                     VisibleIndex = 0,
                                                                                     Width = 150
                                                                                 };

                    DevExpress.XtraTreeList.Columns.TreeListColumn colEntityId = new DevExpress.XtraTreeList.Columns.
                        TreeListColumn
                                                                                     {
                                                                                         Caption = "EntityId",
                                                                                         FieldName = "EntityId"
                                                                                     };

                    DevExpress.XtraTreeList.Columns.TreeListColumn colContentEntityId = new DevExpress.XtraTreeList.
                        Columns.TreeListColumn
                                                                                            {
                                                                                                Caption =
                                                                                                    "ContentEntityId",
                                                                                                FieldName =
                                                                                                    "ContentEntityId"
                                                                                            };

                    DevExpress.XtraTreeList.Columns.TreeListColumn colElementId = new DevExpress.XtraTreeList.Columns.
                        TreeListColumn
                                                                                      {
                                                                                          Caption = "Ид элемента",
                                                                                          FieldName = "ElementId"
                                                                                      };

                    DevExpress.XtraTreeList.Columns.TreeListColumn colCheckSingle = new DevExpress.XtraTreeList.Columns.
                        TreeListColumn
                                                                                        {
                                                                                            Caption = "Текущий",
                                                                                            FieldName = "CheckSingle",
                                                                                            Visible = ShowCheckSingle
                                                                                        };
                    if (ShowCheckSingle)
                        colCheckSingle.VisibleIndex = 1;
                    colCheckSingle.Width = 10;
                    RepositoryItemCheckEdit checkSingleEditor = new RepositoryItemCheckEdit();
                    colCheckSingle.ColumnEdit = checkSingleEditor;

                    DevExpress.XtraTreeList.Columns.TreeListColumn colCheckAll = new DevExpress.XtraTreeList.Columns.
                        TreeListColumn
                                                                                     {
                                                                                         Caption = "Все",
                                                                                         FieldName = "CheckAll",
                                                                                         Visible = ShowCheckAll,
                                                                                         Width = 10
                                                                                     };
                    if (ShowCheckAll)
                        colCheckAll.VisibleIndex = 2;
                    colCheckAll.ColumnEdit = new RepositoryItemCheckEdit();
                    colCheckAll.OptionsColumn.AllowEdit = true;

                    DevExpress.XtraTreeList.Columns.TreeListColumn colData =
                        new DevExpress.XtraTreeList.Columns.TreeListColumn {Caption = "", FieldName = "Data"};

                    ControlTree.Tree.Columns.AddRange(new[]
                                                          {
                                                              colId, colName, colEntityId, colContentEntityId, colElementId
                                                              , colCheckSingle, colCheckAll, colData
                                                          });

                    #endregion

                    #region Клик на колонке

                    ControlTree.Tree.MouseClick += delegate(object sender, MouseEventArgs e)
                                                       {
                                                           Point p = new Point(e.X, e.Y);
                                                           TreeListHitInfo hit = ControlTree.Tree.CalcHitInfo(p);

                                                           if (hit.HitInfoType == HitInfoType.Cell)
                                                           {
                                                               if (hit.Column.FieldName == "CheckSingle")
                                                               {
                                                                   //Изменение состояния текущего чекбокса
                                                                   object currentVal = hit.Node.GetValue("CheckSingle");
                                                                   if (currentVal == null)
                                                                   {
                                                                       hit.Node.SetValue("CheckSingle", true);
                                                                       CheckedLeftNodes.Add(hit.Node);
                                                                   }
                                                                   else
                                                                   {
                                                                       //В соответствии с итоговым значением чекбокса добавляем или удаляем запись в список
                                                                       if (!(bool) currentVal)
                                                                       {
                                                                           CheckedLeftNodes.Add(hit.Node);
                                                                       }
                                                                       else
                                                                       {
                                                                           CheckedLeftNodes.Remove(hit.Node);
                                                                       }
                                                                       //Если установлен флаг "CheckAll"
                                                                       if ((bool) hit.Node.GetValue("CheckAll"))
                                                                           hit.Node.SetValue("CheckSingle", null);
                                                                       else
                                                                           //Иначе просто инвертируем
                                                                           hit.Node.SetValue("CheckSingle",
                                                                                             !(bool) currentVal);
                                                                   }
                                                               }

                                                               if (hit.Column.FieldName == "CheckAll")
                                                               {
                                                                   //Изменение состояния текущего чекбокса
                                                                   object currentVal = hit.Node.GetValue("CheckAll");
                                                                   if (currentVal == null)
                                                                   {
                                                                       hit.Node.SetValue("CheckAll", true);
                                                                       CheckedLeftNodes.Add(hit.Node);
                                                                   }
                                                                   else
                                                                   {
                                                                       //В соответствии с итоговым значением чекбокса добавляем или удаляем запись в список
                                                                       if (!(bool) currentVal)
                                                                       {
                                                                           CheckedRightNodes.Add(hit.Node);
                                                                       }
                                                                       else
                                                                       {
                                                                           CheckedRightNodes.Remove(hit.Node);
                                                                       }

                                                                       RecursivelySetNodeValue(hit.Node, "CheckAll",
                                                                                               !(bool) currentVal, false);
                                                                       if (!(bool) currentVal)
                                                                           RecursivelySetNodeValue(hit.Node,
                                                                                                   "CheckSingle", null,
                                                                                                   true);
                                                                       else
                                                                           RecursivelySetNodeValue(hit.Node,
                                                                                                   "CheckSingle", false,
                                                                                                   true);
                                                                   }
                                                               }
                                                           }
                                                       };

                    #endregion

                    _secure = _workarea.Access.ElementRightView(28);

                    #region Дерево - раскрытие узла

                    ControlTree.Tree.BeforeExpand +=
                        delegate(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
                            {
                                if (e.Node.FirstNode == null)
                                {
                                    Cursor currentCursor = Cursor.Current;
                                    Cursor.Current = Cursors.WaitCursor;
                                    if ((short) e.Node.GetValue("EntityId") == 28)
                                    {
                                        object dataValue = e.Node.GetValue("Data");
                                        if(dataValue !=null)
                                        {
                                            Hierarchy h = dataValue as Hierarchy;
                                            if(h!=null && h.KindValue==2)
                                            {
                                                FillTreeHierarchy(e.Node, h, ShowContentTree);
                                            }
                                            else
                                            {
                                                int id = (int)e.Node.GetValue(GlobalPropertyNames.Id);
                                                Hierarchy currentHierarchy = Workarea.Cashe.Hierarhies.Item(id);
                                                FillTreeHierarchy(e.Node, currentHierarchy, ShowContentTree);
                                            }
                                        }
                                    }
                                    else
                                        FillTreeContent(e.Node);
                                    //e.Node.Tag = null;
                                    Cursor.Current = currentCursor;
                                }

                                //Если в раскрываемой ноде установлен флажек "CheckAll", установить в дочерних "ChechSingle" в третье состояние
                                if (Convert.ToBoolean(e.Node.GetValue("CheckAll")))
                                {
                                    foreach (TreeListNode node in e.Node.Nodes)
                                    {
                                        node.SetValue("CheckSingle", null);
                                        node.SetValue("CheckAll", true);
                                    }
                                }
                            };

                    #endregion

                    if (StartValue == null)
                        StartValue = Workarea.Empty<T>();
                    if (AllowDragDrop)
                        InitDragDropList();
                }
                else
                    ControlTree.Tree.Nodes.Clear();
            }
            else
            {
                BuildImageList();

                #region Колонки дерева

                DevExpress.XtraTreeList.Columns.TreeListColumn colId = new DevExpress.XtraTreeList.Columns.
                    TreeListColumn
                {
                    Caption = "Ид",
                    FieldName = GlobalPropertyNames.Id,
                    Visible = false
                };

                DevExpress.XtraTreeList.Columns.TreeListColumn colName = new DevExpress.XtraTreeList.Columns.
                    TreeListColumn
                {
                    Caption = "Наименование",
                    FieldName = "Name",
                    VisibleIndex = 0,
                    Width = 150
                };

                DevExpress.XtraTreeList.Columns.TreeListColumn colEntityId = new DevExpress.XtraTreeList.Columns.
                    TreeListColumn
                {
                    Caption = "EntityId",
                    FieldName = "EntityId"
                };

                DevExpress.XtraTreeList.Columns.TreeListColumn colContentEntityId = new DevExpress.XtraTreeList.
                    Columns.TreeListColumn
                {
                    Caption =
                        "ContentEntityId",
                    FieldName =
                        "ContentEntityId"
                };

                DevExpress.XtraTreeList.Columns.TreeListColumn colElementId = new DevExpress.XtraTreeList.Columns.
                    TreeListColumn
                {
                    Caption = "Ид элемента",
                    FieldName = "ElementId"
                };

                DevExpress.XtraTreeList.Columns.TreeListColumn colCheckSingle = new DevExpress.XtraTreeList.Columns.
                    TreeListColumn
                {
                    Caption = "Текущий",
                    FieldName = "CheckSingle",
                    Visible = ShowCheckSingle
                };
                if (ShowCheckSingle)
                    colCheckSingle.VisibleIndex = 1;
                colCheckSingle.Width = 10;
                RepositoryItemCheckEdit checkSingleEditor = new RepositoryItemCheckEdit();
                colCheckSingle.ColumnEdit = checkSingleEditor;

                DevExpress.XtraTreeList.Columns.TreeListColumn colCheckAll = new DevExpress.XtraTreeList.Columns.
                    TreeListColumn
                {
                    Caption = "Все",
                    FieldName = "CheckAll",
                    Visible = ShowCheckAll,
                    Width = 10
                };
                if (ShowCheckAll)
                    colCheckAll.VisibleIndex = 2;
                colCheckAll.ColumnEdit = new RepositoryItemCheckEdit();
                colCheckAll.OptionsColumn.AllowEdit = true;

                DevExpress.XtraTreeList.Columns.TreeListColumn colData =
                    new DevExpress.XtraTreeList.Columns.TreeListColumn { Caption = "", FieldName = "Data" };

                ControlTree.Tree.Columns.AddRange(new[]
                                                          {
                                                              colId, colName, colEntityId, colContentEntityId, colElementId
                                                              , colCheckSingle, colCheckAll, colData
                                                          });

                #endregion

                #region Клик на колонке

                ControlTree.Tree.MouseClick += delegate(object sender, MouseEventArgs e)
                {
                    Point p = new Point(e.X, e.Y);
                    TreeListHitInfo hit = ControlTree.Tree.CalcHitInfo(p);

                    if (hit.HitInfoType == HitInfoType.Cell)
                    {
                        if (hit.Column.FieldName == "CheckSingle")
                        {
                            //Изменение состояния текущего чекбокса
                            object currentVal = hit.Node.GetValue("CheckSingle");
                            if (currentVal == null)
                            {
                                hit.Node.SetValue("CheckSingle", true);
                                CheckedLeftNodes.Add(hit.Node);
                            }
                            else
                            {
                                //В соответствии с итоговым значением чекбокса добавляем или удаляем запись в список
                                if (!(bool)currentVal)
                                {
                                    CheckedLeftNodes.Add(hit.Node);
                                }
                                else
                                {
                                    CheckedLeftNodes.Remove(hit.Node);
                                }
                                //Если установлен флаг "CheckAll"
                                if ((bool)hit.Node.GetValue("CheckAll"))
                                    hit.Node.SetValue("CheckSingle", null);
                                else
                                    //Иначе просто инвертируем
                                    hit.Node.SetValue("CheckSingle",
                                                      !(bool)currentVal);
                            }
                        }

                        if (hit.Column.FieldName == "CheckAll")
                        {
                            //Изменение состояния текущего чекбокса
                            object currentVal = hit.Node.GetValue("CheckAll");
                            if (currentVal == null)
                            {
                                hit.Node.SetValue("CheckAll", true);
                                CheckedLeftNodes.Add(hit.Node);
                            }
                            else
                            {
                                //В соответствии с итоговым значением чекбокса добавляем или удаляем запись в список
                                if (!(bool)currentVal)
                                {
                                    CheckedRightNodes.Add(hit.Node);
                                }
                                else
                                {
                                    CheckedRightNodes.Remove(hit.Node);
                                }

                                RecursivelySetNodeValue(hit.Node, "CheckAll",
                                                        !(bool)currentVal, false);
                                if (!(bool)currentVal)
                                    RecursivelySetNodeValue(hit.Node,
                                                            "CheckSingle", null,
                                                            true);
                                else
                                    RecursivelySetNodeValue(hit.Node,
                                                            "CheckSingle", false,
                                                            true);
                            }
                        }
                    }
                };

                #endregion

                _secure = _workarea.Access.ElementRightView(28);

                #region Дерево - раскрытие узла

                ControlTree.Tree.BeforeExpand +=
                    delegate(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
                    {
                        if (e.Node.FirstNode == null)
                        {
                            Cursor currentCursor = Cursor.Current;
                            Cursor.Current = Cursors.WaitCursor;
                            if ((short)e.Node.GetValue("EntityId") == 28)
                            {
                                int id = (int)e.Node.GetValue(GlobalPropertyNames.Id);
                                Hierarchy currentHierarchy = Workarea.Cashe.Hierarhies.Item(id);
                                FillTreeHierarchy(e.Node, currentHierarchy, ShowContentTree);
                            }
                            else
                                FillTreeContent(e.Node);
                            //e.Node.Tag = null;
                            Cursor.Current = currentCursor;
                        }

                        //Если в раскрываемой ноде установлен флажек "CheckAll", установить в дочерних "ChechSingle" в третье состояние
                        if (Convert.ToBoolean(e.Node.GetValue("CheckAll")))
                        {
                            foreach (TreeListNode node in e.Node.Nodes)
                            {
                                node.SetValue("CheckSingle", null);
                                node.SetValue("CheckAll", true);
                            }
                        }
                    };

                #endregion

                if (StartValue == null)
                    StartValue = Workarea.Empty<T>();
                if (AllowDragDrop)
                    InitDragDropList();
            }

            List<Hierarchy> collection; 
            if(string.IsNullOrEmpty(RootCode))
                collection = Workarea.Empty<Hierarchy>().GetCollectionHierarchy(StartValue.EntityId);
            else
            {
                collection = Hierarchy.FindByCode(Workarea, RootCode, StartValue.EntityId);
            }
            Hierarchy hValue = null;
            if (collection.Count > 0)
            {
                int? fvalue = Hierarchy.FirstHierarchy(StartValue);
                if (fvalue.HasValue && fvalue != 0)
                {
                    hValue = new Hierarchy { Workarea = Workarea };
                    hValue.Load(fvalue.Value);
                }
            }

            foreach (Hierarchy item in collection)
            {
                if (_secure.IsAllow("VIEW", item.Id))
                {

                    TreeListNode node = ControlTree.Tree.AppendNode(new object[] { item.Id, item.Name, item.EntityId, item.ContentEntityId, item.Id, false, false,item}, null);
                    node.ImageIndex = GetImageIndexHierarchy(item);
                    node.SelectImageIndex = node.ImageIndex;
                    //node.StateImageIndex = node.ImageIndex;
                    //node.Image = item.GetImage();
                    if (item.HasChildren)
                    {
                        node.HasChildren = true;
                        node.Expanded = true;
                    }
                }
            }
            if (!Workarea.Access.RightCommon.AdminEnterprize)
            {
                //BusinessObjects.Security.EntityRightView secureEntity = Workarea.Access.DbentityRightView();

                // установка разрешений для кнопок действий...
                //ControlTree.btnNew.Enabled = secureEntity.IsAllow("CREATE", Workarea.Empty<Hierarchy>().EntityId);
                //ControlTree.btnDelete.Enabled = secureEntity.IsAllow("TRASH", Workarea.Empty<Hierarchy>().EntityId);
                //ControlTree.btnProperties.Enabled = secureEntity.IsAllow("SHOWPROP", Workarea.Empty<Hierarchy>().EntityId);


                //ControlTree.btnDelete.Visible = ControlTree.btnDelete.Enabled;
                //ControlTree.btnNew.Visible = ControlTree.btnNew.Enabled;
                //ControlTree.btnProperties.Visible = ControlTree.btnProperties.Enabled;
                //ControlTree.btnAcl.Visible = false;
            }
            #region Обрабокта кнопок панели управления
            //if (ShowTreeToolBar)
            //{

            //    #region свойства, в зависимости от текущего элемента "иерархии" или элемента
            //    ControlTree.btnProperties.Click += delegate
            //    {
            //        this.InvokeProperties();
            //    };
            //    #endregion

            //    #region диалог удаления элемента, исключения из иерархии, помещения в корзину
            //    ControlTree.btnDelete.Click += delegate
            //    {
            //        InvokeDelete();
            //    };
            //    #endregion

            //    #region диалог разрешений
            //    ControlTree.btnAcl.Click += delegate
            //    {
            //        DoShowAcl();
            //    };
            //    #endregion

            //    #region обновить текущую ветку, все элементы текущий элемент
            //    ControlTree.btnRefresh.Click += delegate
            //    {
            //        Refresh();
            //    };
            //    #endregion
            //    #region новая иерархия, добавления существующего из списка
            //    ControlTree.btnNew.Click += delegate
            //    {
            //        if (newItems == null)
            //        {
            //            newItems = new ContextMenuStrip();
            //            if (ShowContentTree)
            //            {
            //                ToolStripItem mnuCreateAdd = newItems.Items.Add("Добавить существующий...");
            //                mnuCreateAdd.Click += delegate
            //                {
            //                    AddExistsObject();
            //                };
            //            }
            //            ToolStripItem mnuCreateHierarchy = newItems.Items.Add("Новая иерархия...");
            //            mnuCreateHierarchy.Click += delegate
            //            {
            //                CreateHierarchy();

            //            };
            //        }
            //        System.Drawing.Point p = ControlTree.ToolStrip.PointToScreen(ControlTree.ToolStrip.Location);
            //        p.Offset(0, ControlTree.ToolStrip.Height);
            //        newItems.Show(p, ToolStripDropDownDirection.BelowRight);

            //    };
            //    #endregion
            //}
            #endregion
            //ControlTree.mniContents.Visible = false;
            //ControlTree.mniTrash.Visible = false;

            //ControlTree.mniDelete.Click += delegate
            //{
            //    InvokeDelete();
            //};
            //ControlTree.mniProp.Click += delegate
            //{
            //    this.InvokeProperties();
            //};
            //ControlTree.mniCreate.Click += delegate
            //{
            //    ControlTree.btnNew.PerformClick();
            //};
            //ControlTree.mniRefresh.Click += delegate
            //{
            //    Refresh();
            //};
            //ControlTree.mniAcl.Click += delegate
            //{
            //    DoShowAcl();
            //};
            #region DragDrop
            //ControlTree.View.AllowDrop = true;
            //System.Drawing.Rectangle mMinDistanceRectangle = System.Drawing.Rectangle.Empty;
            //int rowIndexFromMouseDown = 0;
            //ControlTree.View.MouseMove += delegate(object sender, MouseEventArgs e)
            //{
            //    if (e.Button == MouseButtons.Left)
            //    {
            //        ControlTree.View.DoDragDrop(ControlTree.View.CurrentNode, (DragDropEffects.Move | DragDropEffects.Copy));
            //    }
            //};
            //ControlTree.View.DragOver += delegate(object sender, DragEventArgs e)
            //{
            //    e.Effect = e.KeyState == 9 ? DragDropEffects.Copy : DragDropEffects.Move;
            //};
            //ControlTree.View.DragEnter += delegate(object sender, DragEventArgs e)
            //{
            //    e.Effect = e.KeyState == 9 ? DragDropEffects.Copy : DragDropEffects.Move;
            //};
            //ControlTree.View.DragDrop += delegate(object sender, System.Windows.Forms.DragEventArgs e)
            //{
            //    // Ensure that the controlList item index is contained in the data.
            //    if (e.Data.GetDataPresent(typeof(BusinessObjects.Controls.AdvancedDataGridView.TreeGridNode)))
            //    {

            //        BusinessObjects.Controls.AdvancedDataGridView.TreeGridNode item = (BusinessObjects.Controls.AdvancedDataGridView.TreeGridNode)e.Data.GetData(typeof(BusinessObjects.Controls.AdvancedDataGridView.TreeGridNode));
            //        // Perform drag-and-drop, depending upon the effect.
            //        if (e.Effect == DragDropEffects.Copy ||
            //            e.Effect == DragDropEffects.Move)
            //        {
            //            System.Drawing.Point clientPoint = ControlTree.View.PointToClient(new System.Drawing.Point(e.X, e.Y));

            //            // Get the row index of the item the mouse is below. 
            //            int rowIndexOfItemUnderMouseToDrop = ControlTree.View.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            //            BusinessObjects.Controls.AdvancedDataGridView.TreeGridNode nodeTo = ControlTree.View.GetNodeForRow(rowIndexOfItemUnderMouseToDrop);
            //            if (nodeTo.Cells[2].Value.ToString() == item.Cells[2].Value.ToString())
            //            {
            //                if (nodeTo.Cells[1].Value.ToString() != item.Cells[1].Value.ToString())
            //                {
            //                    int hierarchyId = (Int32)item.Cells[1].Value;
            //                    Hierarchy currentHierarchy = Workarea.GetObject<Hierarchy>(hierarchyId);
            //                    if (currentHierarchy.ParentId != (Int32)nodeTo.Cells[1].Value)
            //                    {
            //                        currentHierarchy.ParentId = (Int32)nodeTo.Cells[1].Value;
            //                        currentHierarchy.Save();

            //                        //controlTree.View.Nodes.Remove(item);
            //                        ControlTree.View.Rows.RemoveAt(rowIndexFromMouseDown);

            //                        // обновить вложенные с текщей ветки
            //                        nodeTo.Nodes.Clear();
            //                        int pid = (Int32)nodeTo.Cells[1].Value;
            //                        List<Hierarchy> nodes = Workarea.GetHierarchyChild(pid);
            //                        foreach (Hierarchy n in nodes)
            //                        {
            //                            BusinessObjects.Controls.AdvancedDataGridView.TreeGridNode node = nodeTo.Nodes.Add(n.Name, n.Id, n.ContentEntityId, true);
            //                            node.Image = n.GetImage();
            //                            if (item.HasChildren)
            //                            {
            //                                node.Nodes.Add("EMPTY", 0, n.EntityId);
            //                            }
            //                        }

            //                    }
            //                }
            //            }
            //            else
            //            {
            //                BusinessObjects.Controls.cTaskDialog.MessageBox(Properties.Resources.MSG_CAPATTENTION, Properties.Resources.MSG_EX_MOVE_HIERARCYKINDS, string.Empty, BusinessObjects.Controls.eTaskDialogButtons.OK, BusinessObjects.Controls.eSysIcons.Warning);
            //            }
            //        }
            //    }

            //};
            //ControlTree.View.MouseDown += delegate(object sender, MouseEventArgs e)
            //{
            //    // Get the index of the item the mouse is below.
            //    DataGridView.HitTestInfo hInfo = ControlTree.View.HitTest(e.X, e.Y);
            //    rowIndexFromMouseDown = hInfo.RowIndex;
            //    // Will just handle if hitted a cell. You can test whatever kind of intersection you like here
            //    if (hInfo.Type == DataGridViewHitTestType.Cell)
            //    {
            //        // The DragSize indicates the size that the mouse must move to consider a drag operation
            //        System.Drawing.Size dragSize = SystemInformation.DragSize;

            //        // Create a rectangle using the DragSize, with the mouse position being
            //        // at the center of the rectangle.
            //        mMinDistanceRectangle = new System.Drawing.Rectangle(
            //        new System.Drawing.Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            //    }
            //    else
            //    {
            //        // Reset the rectangle if the mouse is not over an item in the ListBox. 
            //        mMinDistanceRectangle = System.Drawing.Rectangle.Empty;
            //    }
            //};
            #endregion
            ControlTree.Focus();

            #region Фокусировка
            //if (hValue != null)
            //{
            //    string a = hValue.Path;
            //    a = a.Replace("0", string.Empty);
            //    string[] b = a.Split('.');
            //    TreeGridNodeCollection startNodes = ControlTree.View.Nodes;
            //    foreach (string p in b)
            //    {
            //        foreach (TreeGridNode item in startNodes)
            //        {
            //            if (item.Cells[1].Value.ToString() == p)
            //            {
            //                item.Expand();
            //                item.Selected = true;
            //                startNodes = item.Nodes;
            //                break;
            //            }
            //        }
            //    }
            //    foreach (TreeGridNode item in startNodes)
            //    {
            //        if (item.Cells[1].Value.ToString() == StartValue.Id.ToString())
            //        {
            //            item.Selected = true;
            //            break;
            //        }
            //    }
            //}
            #endregion
        }
        

        private TreeListNode _moveNode;
        private Hierarchy _sourceHierarchy;
        //private TreeListHitInfo hitInfo;
        //private bool _beginDrag;
        void InitDragDropList()
        {
            ControlTree.Tree.OptionsBehavior.CanCloneNodesOnDrop = false;

            ControlTree.Tree.OptionsBehavior.DragNodes = true;
            ControlTree.Tree.OptionsBehavior.AutoChangeParent = false;
            ControlTree.Tree.AllowDrop = true;

            ControlTree.Tree.MouseDown += GridControl1MouseDown;
            ControlTree.Tree.MouseUp += delegate
                                            {
                                                //_beginDrag = false;
                                            };
            ControlTree.Tree.DragOver += TreeDragOver;
            ControlTree.Tree.MouseMove += GridControl1MouseMove;
            ControlTree.Tree.DragEnter += ListBoxControl1DragEnter;
            ControlTree.Tree.DragDrop += ListBoxControl1DragDrop;
            ControlTree.Tree.QueryContinueDrag += ListBoxControl1QueryContinueDrag;
            ControlTree.Tree.BeforeDragNode += Tree_BeforeDragNode;
            ControlTree.Tree.FocusedNodeChanged += delegate { 
                _sourceHierarchy = SelectedHierarchy;
            };
        }

        void Tree_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            e.CanDrag = false;
        }


        //private DragDropEffects GetDragDropEffect(TreeList tl, TreeListNode dragNode)
        //{
        //    TreeListNode targetNode;
        //    Point p = tl.PointToClient(Control.MousePosition);
        //    targetNode = tl.CalcHitInfo(p).Node;

        //    if (dragNode != null && targetNode != null
        //        && dragNode != targetNode
        //        && dragNode.ParentNode == targetNode.ParentNode)
        //        return DragDropEffects.Move;
        //    else
        //        return DragDropEffects.None;
        //}

        private void RemoveDragDrop()
        {
            if (ControlTree != null)
            {
                ControlTree.Tree.MouseDown -= GridControl1MouseDown;
                ControlTree.Tree.MouseMove -= GridControl1MouseMove;
                ControlTree.Tree.DragEnter -= ListBoxControl1DragEnter;
                ControlTree.Tree.DragDrop -= ListBoxControl1DragDrop;
                ControlTree.Tree.QueryContinueDrag -= ListBoxControl1QueryContinueDrag;

                ControlTree.Tree.OptionsBehavior.DragNodes = false;
                ControlTree.Tree.AllowDrop = false;
            }
        }

        void TreeDragOver(object sender, DragEventArgs e)
        {
            //true если перетаскиваемый объект - иерархия
            bool isHierarchy = e.Data.GetDataPresent(typeof (Hierarchy));
            //if (!isHierarchy)
            //{
            //    e.Effect = DragDropEffects.None;
            //}
            Point p = ControlTree.Tree.PointToClient(new Point(e.X, e.Y));
            TreeListHitInfo hitInfo = ControlTree.Tree.CalcHitInfo(p);
            if (hitInfo.Node != null)
            {
                Hierarchy toHierarchy = hitInfo.Node.GetValue("Data") as Hierarchy;
                if (toHierarchy==null)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                if (toHierarchy.IsVirtual)
                    e.Effect = DragDropEffects.None;
                else
                {
                    if (_sourceHierarchy.Id == toHierarchy.Id || isHierarchy && (_sourceHierarchy.ParentId == toHierarchy.Id || toHierarchy.IsChildTo(_sourceHierarchy))
                        || _sourceHierarchy.IsVirtualRoot & toHierarchy.IsVirtualRoot)
                        e.Effect = DragDropEffects.None;
                    else
                        e.Effect = DragDropEffects.Move;
                }

            }
            else
                e.Effect = DragDropEffects.None;
        }
        private void GridControl1MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //hitInfo = ControlTree.Tree.CalcHitInfo(new Point(e.X, e.Y));
            //_beginDrag = true;
        }
        private void GridControl1MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                _moveNode = ControlTree.Tree.FocusedNode;
                _sourceHierarchy = SelectedHierarchy;
                if (_sourceHierarchy == null) return;
                if (_sourceHierarchy.IsVirtual) return;
                ControlTree.Tree.DoDragDrop(_sourceHierarchy, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void ListBoxControl1DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //if (_beginDrag)
            //{
            //    if (SelectedHierarchy.IsVirtual) return;
            //    e.Effect = DragDropEffects.Move;
            //    _moveNode = ControlTree.Tree.FocusedNode;
            //    _sourceHierarchy = SelectedHierarchy;
            //    e.Data.SetData(_sourceHierarchy);
            //}
        }

        private void ListBoxControl1DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Hierarchy)))
            {
                //Point p = ControlTree.Tree.PointToClient(new Point(e.X, e.Y));
                //DevExpress.XtraTreeList.TreeListHitInfo hitTree = ControlTree.Tree.CalcHitInfo(p);
                //if (hitTree.Node == null) return;
                //if (hitTree.Node == _moveNode) return;
                //if (_sourceHierarchy == null) return;
                //Hierarchy hierarchyTo = hitTree.Node.GetValue("Data") as Hierarchy;
                //Hierarchy hierarchySource = e.Data.GetData(typeof(Hierarchy)) as Hierarchy;
                //hierarchySource.ParentId = hierarchyTo.Id;
                ////_sourceHierarchy.Save();
                //ControlTree.Tree.MoveNode(_moveNode, hitTree.Node);


                //_sourceHierarchy.ParentId = hierarchyTo.Id;
                //int id = (int)hitTree.Node.GetValue(GlobalPropertyNames.Id);
                //Hierarchy hierarchyTo = Workarea.Cashe.GetCasheData<Hierarchy>().Item(id);
                //_sourceHierarchy.ParentId = hierarchyTo.Id;
                //_sourceHierarchy.Save();
                //ControlTree.Tree.MoveNode(_moveNode, hitTree.Node);
            }
        }

        private ContextMenuStrip _dragMenu;
        private ToolStripMenuItem _miMove;
        private ToolStripMenuItem _miCancel;
        private bool _isCancel = true;

        private void ListBoxControl1QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (_sourceHierarchy == null || _sourceHierarchy.IsVirtual)
            {
                e.Action = DragAction.Cancel;
                return;
            }
            
            if(e.Action== DragAction.Drop)
            {
                if (!ControlTree.Tree.AllowDrop)
                {
                    e.Action = DragAction.Cancel;
                    return;
                }
                Point p = ControlTree.Tree.PointToClient(Cursor.Position);
                DevExpress.XtraTreeList.TreeListHitInfo hitTree = ControlTree.Tree.CalcHitInfo(p);
                if (hitTree.Node == null)
                    return;
                Hierarchy hierarchyTo = hitTree.Node.GetValue("Data") as Hierarchy;
                if (hierarchyTo == null)
                    return;
                if(hierarchyTo.IsVirtual)
                {
                    e.Action = DragAction.Cancel;
                    return;
                }
                if (_sourceHierarchy.Id == hierarchyTo.Id || _sourceHierarchy.ParentId == hierarchyTo.Id || hierarchyTo.IsChildTo(_sourceHierarchy))
                {
                    e.Action = DragAction.Cancel;
                    return;
                }
                if(_sourceHierarchy.IsVirtualRoot & hierarchyTo.IsVirtualRoot)
                {
                    e.Action = DragAction.Cancel;
                    return;
                }
                _dragMenu = new ContextMenuStrip();
                _miMove = new ToolStripMenuItem("Переместить");
                _miCancel = new ToolStripMenuItem("Отменить");
                _dragMenu.Items.Add(_miMove);
                _dragMenu.Items.Add(_miCancel);
                _miMove.Click += delegate
                                     {
                                         _sourceHierarchy.ParentId = hierarchyTo.Id;
                                         _sourceHierarchy.Save();
                                         ControlTree.Tree.MoveNode(_moveNode, hitTree.Node);
                                         e.Action = DragAction.Cancel;
                                     };
                _miCancel.Click += delegate
                                       {
                                           e.Action = DragAction.Cancel;
                                       };
                _dragMenu.Capture = true;
                _dragMenu.Show(Cursor.Position);
            }
            ////Cursor c = new Cursor(
            //if (e.Action == DragAction.Drop)
            //{
            //    if (_dragMenu == null)
            //    {
            //        _dragMenu = new ContextMenuStrip();
            //        _miMove = new ToolStripMenuItem("Переместить");
            //        _dragMenu.Items.Add(_miMove);

            //        _miMove.Click += delegate
            //        {
            //            _isCancel = false;
            //        };
            //    }

            //    _dragMenu.Show(Cursor.Position);
            //    _dragMenu.Capture = true;
            //    while (_dragMenu.Visible)
            //        Application.DoEvents();
            //    if (_isCancel)
            //        e.Action = DragAction.Cancel;
            //    else
            //        _isCancel = true;
            //}
        }

        // TODO: вынести в дополнительный класс
        private void BuildImageList()
        {
            
                int kind = Workarea.Empty<T>().EntityId;
                ControlTree.ImageCollection.AddImage(ExtentionsImage.GetImageHierarchy(Workarea, kind, false), "hierarchy");
                ControlTree.ImageCollection.AddImage(ExtentionsImage.GetImageHierarchy(Workarea, kind, true), "hierarchy_find");

                EntityType type = Workarea.Cashe.GetCasheData<EntityType>().Item(kind);
                foreach (EntityKind value in type.EntityKinds)
                {
                    ControlTree.ImageCollection.AddImage(value.GetImage(), value.SubKind.ToString());
                }

        }
        private int GetImageIndexHierarchy(Hierarchy value)
        {
            if (value.ViewListDocumentsId != 0) return 1;
            if(value.ViewListId==0)
            {
                return 0;
            }
            else if (value.ViewListId != 0 && value.ViewList!=null && value.ViewList.KindValue != CustomViewList.KINDVALUE_LIST)
            {
                return 1;
            }
            return 0;
        }

        private int GetImageIndexValue(HierarchyContent value)
        {
            if(ControlTree.ImageCollection.Images.Count==3)
                return 2;
            return ControlTree.ImageCollection.Images.Keys.IndexOf(value.KindValue.ToString());
        }

        private void DoShowAcl()
        {
            //Hierarchy currentObject = SelectedHierarchy;
            //if (currentObject != null)
            //{
            //    currentObject.BrowseRightsElement();
            //}
        }
        #region Private void

        public void InvokeDelete()
        {
            if (SelectedTreeIsHierarchy)
            {
                #region Иерархии
                int res = Extentions.ShowMessageChoice(Workarea,
                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), 
                    "Удаление корреспондента",
                                                         "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                         Properties.Resources.STR_CHOICE_DEL);

                // удаление в корзину
                if (res == 0)
                {
                    try
                    {
                        SelectedHierarchy.Remove();
                        ControlTree.Tree.FocusedNode.ParentNode.Nodes.Remove(ControlTree.Tree.FocusedNode);
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // удаление
                else if (res == 1)
                {
                    try
                    {
                        SelectedHierarchy.Delete();
                        ControlTree.Tree.FocusedNode.ParentNode.Nodes.Remove(ControlTree.Tree.FocusedNode);
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, 
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
            }

            else
            {
                #region MyRegion
                int res = Extentions.ShowMessageChoice(Workarea, 
                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), 
                    "Удаление корреспондента",
                                                         "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                         "Запретить использование|Исключить из иерархии|Удалить объект в корзину|Удалить объект навсегда");

                // запретить использование
                if (res == 0)
                {
                    try
                    {
                        HierarchyContent cnt = this.Workarea.GetObject<HierarchyContent>(SelectedHierarchyContentId);
                        cnt.Remove();
                        // TODO: 
                        //ControlTree.Tree.FocusedNode.ImageIndex = cnt.GetImage();
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, 
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // удалить из иерархии
                else if (res == 1)
                {
                    try
                    {
                        HierarchyContent cnt = this.Workarea.GetObject<HierarchyContent>(SelectedHierarchyContentId);
                        cnt.Delete();
                        ControlTree.Tree.FocusedNode.ParentNode.Nodes.Remove(ControlTree.Tree.FocusedNode);
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, 
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Удалить объект в корзину
                else if (res == 2)
                {
                    try
                    {
                        HierarchyContent cnt = this.Workarea.GetObject<HierarchyContent>(SelectedHierarchyContentId);
                        T obj = cnt.ConvertToTypedObject<T>();
                        obj.Remove();
                        ControlTree.Tree.FocusedNode.ParentNode.Nodes.Remove(ControlTree.Tree.FocusedNode);
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, 
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Удалить объект навсегда
                else if (res == 3)
                {
                    try
                    {
                        HierarchyContent cnt = this.Workarea.GetObject<HierarchyContent>(SelectedHierarchyContentId);
                        T obj = cnt.ConvertToTypedObject<T>();
                        obj.Delete();
                        ControlTree.Tree.FocusedNode.ParentNode.Nodes.Remove(ControlTree.Tree.FocusedNode);
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                         "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, 
                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion
            }
        }
        public void InvokeRefresh()
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!ShowContentTree)
                {
                    /*Старый refresh списка*/
                    // TODO: проверить правильность построения если указан корень построения
                    if (!SelectedTreeIsHierarchy && ControlTree.Tree.FocusedNode == null)
                        return;
                    TreeListNode hNode = SelectedTreeIsHierarchy ? ControlTree.Tree.FocusedNode : ControlTree.Tree.FocusedNode.ParentNode;
                    int id = (Int32)hNode.GetValue(GlobalPropertyNames.Id);
                    hNode.Expanded = false;
                    hNode.Nodes.Clear();
                    Hierarchy currentHierarchy = Workarea.Cashe.Hierarhies.Item(id);
                    currentHierarchy.Refresh();
                    FillTreeHierarchy(hNode, currentHierarchy, ShowContentTree);
                    hNode.Expanded = true;
                    /*--------------------*/
                }
                else
                {
                    /*Новый refresh списка*/
                    Build();
                    if (ControlTree.Tree.Nodes.Count > 0)
                        ControlTree.Tree.Nodes[0].Expanded = true;
                    /*--------------------*/
                }
            }
            finally
            {
                Cursor.Current = currentCursor;
            }
        }
        private void AddExistsObject()
        {
            //List<T> addItem = Extentions.BrowseMultyList<T>(this.Workarea, StartValue.EntityId);
            //if (addItem != null)
            //{
            //    foreach (T sourceItem in addItem)
            //    {
            //        HierarchyContent cnt = new HierarchyContent
            //        {
            //            Workarea = this.Workarea,
            //            HierarchyId = SelectedHierarchy.Id,
            //            ElementId = sourceItem.Id,
            //            Name = sourceItem.Name,
            //            Code = sourceItem.Code,
            //            ContentDbEntityId = sourceItem.EntityId
            //        };
            //        cnt.Save();
            //        BusinessObjects.Controls.AdvancedDataGridView.TreeGridNode nodeEnt = ControlTree.View.CurrentNode.Nodes.Add(cnt.Name, cnt.ElementId, cnt.EntityId, false);
            //        nodeEnt.Image = cnt.GetImage();
            //    }
            //}
        }
        private void CreateHierarchy()
        {
            //bool isHierarchy = SelectedTreeIsHierarchy;
            //TreeGridNode hNode = isHierarchy ? ControlTree.View.CurrentNode : ControlTree.View.CurrentNode.Parent;

            //if (!hNode.IsNodeExpanded)
            //    hNode.Expand();

            //Hierarchy newHierarchy = new Hierarchy
            //{
            //    Workarea = this.Workarea,
            //    ContentEntityId = SelectedHierarchy.ContentEntityId,
            //    ParentId = SelectedHierarchy.Id,
            //    KindId = SelectedHierarchy.KindId
            //};
            //Form frmProp = newHierarchy.ShowProperty();
            //frmProp.FormClosed += delegate
            //{
            //    if (frmProp.DialogResult == DialogResult.OK)
            //    {
            //        TreeGridNode newnode = hNode.Nodes.Add(newHierarchy.Name, newHierarchy.Id, newHierarchy.ContentEntityId, true);
            //        newnode.Image = newHierarchy.GetImage();
            //    }
            //};
        }
        public void InvokeProperties()
        {
            if (ControlTree.Tree.FocusedNode != null)
            {
                if (SelectedTreeIsHierarchy)
                {

                    Form frmProp = SelectedHierarchy.ShowProperty();
                    frmProp.Closed += delegate
                    {
                        if (frmProp.DialogResult == DialogResult.OK)
                        {
                            ControlTree.Tree.FocusedNode.SetValue("Name", SelectedHierarchy.Name);
                        }
                    };
                }
                else
                {
                    if ((int)ControlTree.Tree.FocusedNode.GetValue("ElementId") != 0)
                    {
                        int dbEntityId = (int)ControlTree.Tree.FocusedNode.GetValue(GlobalPropertyNames.Id);
                        T selectedItem = Workarea.GetObject<T>(dbEntityId);
                        selectedItem.ShowPropertyType();
                    }
                }
            }
        }
        bool IsSecureAllowView(Hierarchy item)
        {
            if (Workarea.Access.RightCommon.AdminEnterprize)
                return true;
            if (item.ParentId == 0)
                return false;
            if (_secure.IsSet("VIEW", item.Id))
                return _secure.IsAllow("VIEW", item.Id);
            return IsSecureAllowView(item.Parent);
        }
        private void FillTreeHierarchy(TreeListNode toNode, Hierarchy from, bool includeContents)
        {
            ControlTree.Tree.BeginUnboundLoad();
            if(from.KindValue==2)
            {
                if(from.VirtualBuildId!=0)
                {
                    InvokeFillFromVirtualBuild(toNode, from);
                }
                else
                {
                    InvokeFillFromVirtualBuild(toNode, from);
                }
            }
            else if (from.KindValue == 1)
            {
                List<Hierarchy> nodes = from.Children; //Workarea.GetHierarchyChild(from.Id);

                foreach (Hierarchy item in nodes)
                {
                    Workarea.Cashe.Hierarhies.Add(item);
                    
                    if (!ExcludeHierarchies.Contains(item.Code))
                    {
                        if (IsSecureAllowView(item))
                        {
                            TreeListNode node =
                                ControlTree.Tree.AppendNode(
                                    new object[]
                                        {
                                            item.Id, item.Name, item.EntityId, item.ContentEntityId, item.Id, false, false,
                                            item
                                        }, toNode);
                            node.ImageIndex = GetImageIndexHierarchy(item);
                            node.SelectImageIndex = node.ImageIndex;
                            if (item.HasChildren)
                            {
                                node.HasChildren = true;
                            }
                            else if (includeContents && item.HasContents)
                            {
                                node.HasChildren = true;
                            }
                            if (item.VirtualBuildId != 0)
                            {
                                node.HasChildren = true;
                            }
                        }
                    }
                }
                if (includeContents && from.HasContents)
                {
                    foreach (HierarchyContent cnt in from.Contents)
                    {
                        TreeListNode node = ControlTree.Tree.AppendNode(new object[] {cnt.Id, cnt.Name, cnt.EntityId, 0, cnt.ElementId}, toNode);
                        node.ImageIndex = GetImageIndexValue(cnt);
                        node.SelectImageIndex = node.ImageIndex;
                        node.HasChildren = ActiveHasChild;
                    }
                }
            }
            ControlTree.Tree.EndUnboundLoad();
        }

        private void InvokeFillFromVirtualBuild(TreeListNode toNode,Hierarchy from)
        {
            IVirtualGroupBuilder<T> builder= null;
            if (from.VirtualBuildId != 0)
            {
                Library lib = from.Workarea.Cashe.GetCasheData<Library>().Item(from.VirtualBuildId);
                int referenceLibId = Library.GetLibraryIdByContent(Workarea, lib.LibraryTypeId);
                Library referenceLib = Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
                LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

                Assembly ass = Library.GetAssemblyFromGac(referenceLib);
                if (ass == null)
                {
                    string assFile = System.IO.Path.Combine(Application.StartupPath,
                                                            referenceLib.AssemblyDll.NameFull);
                    if (!System.IO.File.Exists(assFile))
                    {
                        using (
                            System.IO.FileStream stream = System.IO.File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                        {
                            stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                         referenceLib.AssemblyDll.StreamData.Length);
                            stream.Close();
                            stream.Dispose();
                        }
                    }
                    ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                          System.Reflection.Assembly.LoadFile(assFile);
                }
                Type type = ass.GetType(cnt.FullTypeName);
                if (type == null) return;
                object objectContentModule = Activator.CreateInstance(type);
                builder = objectContentModule as IVirtualGroupBuilder<T>;
                if (builder == null) return;
                builder.Hierarchy = from;
            }
            else
            {
                builder = (toNode.Tag as IVirtualGroup<T>).Owner;
            }
            if (builder == null) return;
            if (from.VirtualBuildId != 0)
            {
                foreach (IVirtualGroup<T> vg in builder.Roots())
                {

                    TreeListNode node =
                        ControlTree.Tree.AppendNode(
                            new object[] {vg.Value.Id, vg.Value.Name, vg.Value.EntityId, vg.Value.ContentEntityId, vg.Value.Id, false, false,vg.Value}, toNode);
                    node.ImageIndex = GetImageIndexHierarchy(vg.Value);
                    node.SelectImageIndex = node.ImageIndex;
                    node.Tag = vg;
                    if (vg.Children != null)
                        if (vg.Children.Count > 0)
                            node.HasChildren = true;
                }
            }
            else
            {
                IVirtualGroup<T> group = toNode.Tag as IVirtualGroup<T>;
                foreach (IVirtualGroup<T> vg in group.Children)
                {

                    TreeListNode node =
                        ControlTree.Tree.AppendNode(
                            new object[] { vg.Value.Id, vg.Value.Name, vg.Value.EntityId, vg.Value.ContentEntityId, vg.Value.Id, false, false,vg.Value }, toNode);
                    node.ImageIndex = GetImageIndexHierarchy(vg.Value);
                    node.SelectImageIndex = node.ImageIndex;
                    node.Tag = vg;
                }
            }
        }

        private void FillTreeContent(TreeListNode toNode)
        {
            int id = (int)toNode.GetValue("ElementId");
            if (id == 0) return;
            T obj = new T() { Workarea = Workarea };
            obj.Load(id);
            List<IChain<T>> collection = (obj as IChains<T>).GetLinks();
            Dictionary<string, TreeListNode> dic = new Dictionary<string, TreeListNode>();

            ControlTree.Tree.BeginUnboundLoad();
            foreach (IChain<T> item in collection)
            {
                if (ValidChain(item))
                {
                    if (item.Kind.Code == "TREE")
                    {
                        TreeListNode node = ControlTree.Tree.AppendNode(new object[] { item.Right.Id, item.Right.Name, item.Right.EntityId, 0, item.Right.Id, false, false }, toNode);
                        node.ImageIndex = ControlTree.ImageCollection.Images.Keys.IndexOf(item.Right.KindValue.ToString());
                        node.SelectImageIndex = node.ImageIndex;
                        node.HasChildren = ActiveHasChild;
                    }
                    else
                    {
                        if (!dic.ContainsKey(item.Kind.Code))
                        {
                            TreeListNode nodeDir = ControlTree.Tree.AppendNode(new object[] { 0, item.Kind.Name, item.Right.EntityId, 0, 0, false, false }, toNode);
                            nodeDir.ImageIndex = 0;
                            nodeDir.SelectImageIndex = nodeDir.ImageIndex;
                            nodeDir.HasChildren = true;
                            dic.Add(item.Kind.Code, nodeDir);
                        }
                        TreeListNode node = ControlTree.Tree.AppendNode(new object[] { item.Right.Id, item.Right.Name, item.Right.EntityId, 0, item.Right.Id, false, false }, dic[item.Kind.Code]);
                        node.ImageIndex = ControlTree.ImageCollection.Images.Keys.IndexOf(item.Right.KindValue.ToString());
                        node.SelectImageIndex = node.ImageIndex;
                        node.HasChildren = ActiveHasChild;
                    }
                }
            }
            ControlTree.Tree.EndUnboundLoad();
        }

        private bool ValidChain(IChain<T> item)
        {
            foreach (ChainRule rule in ChainsRules)
            {
                if (rule.ChainKind.Id == item.KindId && rule.IsActive)
                    return true;
            }
            return false;
        }

        #endregion
    }
}
