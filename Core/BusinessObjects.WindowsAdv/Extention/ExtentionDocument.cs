using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using BusinessObjects.Windows.Controls;
using System.Drawing;
using BusinessObjects.Documents;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства документа
        /// </summary>
        /// <param name="item">Документ</param>
        /// <returns></returns>
        public static Form ShowProperty(this Document item)
        {
            InternalShowPropertyBase<Document> showPropertyBase = new InternalShowPropertyBase<Document>
            {
                SelectedItem = item,
                ControlBuilder =
                    new BuildControlDocument()
                    {
                        SelectedItem = item
                    }
            };
            return showPropertyBase.ShowDialog();
        }
        #endregion
        /// <summary>
        /// Свойства документа
        /// </summary>
        /// <param name="value">Документ</param>
        /// <param name="parentId">Идентификатор родителя</param>
        /// <param name="parentForm">Родительское представление</param>
        public static void ShowDocument(this IDocument value, int parentId=0, IDocumentView parentForm=null)
        {
            if (value == null) return;
            Document op = value.Document;

            if (op == null) return;
            if (op.ProjectItemId == 0) return;
            Library lib = op.ProjectItem;
            int referenceLibId = Library.GetLibraryIdByContent(value.Workarea, lib.LibraryTypeId);
            Library referenceLib = value.Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
            LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

            Assembly ass = Library.GetAssemblyFromGac(referenceLib);
            if (ass == null)
            {
                string assFile = Path.Combine(Application.StartupPath,
                                              referenceLib.AssemblyDll.NameFull);
                if (!File.Exists(assFile))
                {
                    using (
                        FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                    {
                        stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                     referenceLib.AssemblyDll.StreamData.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                }
                ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                      Assembly.LoadFile(assFile);
            }
            Type type = ass.GetType(cnt.FullTypeName);
            if (type == null) return;
            object objectContentModule = Activator.CreateInstance(type);
            IDocumentView formModule = objectContentModule as IDocumentView;
            formModule.Load(value);
            formModule.SourceView = parentForm;
            formModule.Show(value.Workarea, null, op.Id, op.TemplateId, parentId);
        }
        /// <summary>
        /// Отображает диалог свойств документа
        /// </summary>
        /// <param name="doc">Документ</param>
        public static Form ShowProperties(this Document doc)
        {
            InternalShowPropertyBase<Document> showPropertyBase = new InternalShowPropertyBase<Document>
                                                                      {
                                                                          SelectedItem = doc,
                                                                          ControlBuilder =
                                                                              new BuildControlDocument
                                                                                  {
                                                                                      SelectedItem = doc
                                                                                  }
                                                                      };
            return showPropertyBase.ShowDialog();
        }

        /// <summary>
        /// Отображает диалог дополнительных кодов документа
        /// </summary>
        /// <param name="doc">Документ</param>
        public static Form ShowCodes(this Document doc, bool modal=true)
        {
            InternalShowPropertyBase<Document> showPropertyBase = new InternalShowPropertyBase<Document>
            {
                SelectedItem = doc,
                ControlBuilder =
                    new BuildControlDocumentCodes
                    {
                        SelectedItem = doc
                    }
            };
            return showPropertyBase.ShowDialog(true);
        }

        /// <summary>
        /// Отображает диалог связанных статей базы знаний для документа
        /// </summary>
        /// <param name="doc">Документ</param>
        public static Form ShowKnowledges(this Document doc, bool modal = true)
        {
            InternalShowPropertyBase<Document> showPropertyBase = new InternalShowPropertyBase<Document>
            {
                SelectedItem = doc,
                ControlBuilder =
                    new BuildControlDocumentKnowledge
                    {
                        SelectedItem = doc
                    }
            };
            showPropertyBase.Show += delegate
                                         {
                                             showPropertyBase.Form.btnPrint.Visibility = BarItemVisibility.Never;
                                             showPropertyBase.Form.btnSave.Visibility = BarItemVisibility.Never;
                                             showPropertyBase.Form.btnSaveClose.Visibility = BarItemVisibility.Never;
                                         };
            return showPropertyBase.ShowDialog(true);
        }
        /// <summary>
        /// Отображает диалог дополнительных примечаний документа
        /// </summary>
        /// <param name="doc">Документ</param>
        public static Form ShowNotes(this Document doc, bool modal = true)
        {
            InternalShowPropertyBase<Document> showPropertyBase = new InternalShowPropertyBase<Document>
            {
                SelectedItem = doc,
                ControlBuilder =
                    new BuildControlDocumentNotes
                    {
                        SelectedItem = doc
                    }
            };
            return showPropertyBase.ShowDialog(true);
        }
        /// <summary>
        /// Отображает диалог списка процессов документа
        /// </summary>
        /// <param name="doc">Документ</param>
        public static Form ShowWorkflowList(this Document doc, bool modal = true)
        {
            InternalShowPropertyBase<Document> showPropertyBase = new InternalShowPropertyBase<Document>
            {
                SelectedItem = doc,
                ControlBuilder =
                    new BuildControlDocumentWorkFlowView
                    {
                        SelectedItem = doc
                    }
            };
            return showPropertyBase.ShowDialog(true);
        }

        /// <summary>
        /// Отображает протокол изменений документа
        /// </summary>
        /// <param name="doc">Документ</param>
        /// TODO: Реализовать протокол изменений документа
        public static void ShowChangesProtocol(this Document doc)
        {
        }

        /// <summary>
        /// Отображает диалог с деревом связанных документов
        /// </summary>
        /// <param name="doc">Документ</param>
        public static void ShowChainDocsTree(this Document doc)
        {
            if (doc != null)
            {
                Image img = ResourceImage.GetByCode(doc.Workarea, ResourceImage.DOCUMENTDONE_X16);
                FormProperties frm = new FormProperties
                                         {
                                             Width = 650,
                                             Height = 450,
                                             Text = "Дерево связанных документов",
                                             btnSave = {Visibility = BarItemVisibility.Never},
                                             btnRefresh = {Visibility = BarItemVisibility.Always}
                                         };

                #region Источник/Назначение
                BarButtonItem buttonDocLinksView = new BarButtonItem
                {
                    Caption = "Вид\n[Источник]",
                    Tag = 0,
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(doc.Workarea, ResourceImage.ARROWDOWNBLUE_X32)
                };
                buttonDocLinksView.SuperTip = CreateSuperToolTip(buttonDocLinksView.Glyph, "Вид отображения связей",
                "Задает вид отображения связей. Существует два вида связей - источник и назначение.\nИсточник - список документов зависящих от текущего документа.\nНазачение - список документов от которых зависит текущий документ.");
                frm.Ribbon.SelectedPage.Groups[0].ItemLinks.Add(buttonDocLinksView);
                buttonDocLinksView.ItemClick += delegate
                {
                    if ((int)buttonDocLinksView.Tag == 0)
                    {
                        buttonDocLinksView.Caption = "Вид\n[Назначение]";
                        buttonDocLinksView.Glyph = ResourceImage.GetByCode(doc.Workarea, ResourceImage.ARROWUPBLUE_X32);
                        buttonDocLinksView.Tag = 1;
                    }
                    else
                    {
                        buttonDocLinksView.Caption = "Вид\n[Источник]";
                        buttonDocLinksView.Glyph = ResourceImage.GetByCode(doc.Workarea, ResourceImage.ARROWDOWNBLUE_X32);
                        buttonDocLinksView.Tag = 0;
                    }
                    frm.Ribbon.Refresh();
                    frm.btnRefresh.PerformClick();
                };
                #endregion

                ControlTree treeChains = new ControlTree();
                frm.clientPanel.Controls.Add(treeChains);
                treeChains.Dock = DockStyle.Fill;
                treeChains.ImageCollection.AddImage(img);

                TreeListColumn colId = new TreeListColumn { Caption = "Ид", FieldName = GlobalPropertyNames.Id, Visible = false };

                TreeListColumn colDate = new TreeListColumn
                                             {
                                                 Caption = "Дата",
                                                 FieldName = "Date",
                                                 VisibleIndex = 0,
                                                 Width = 100
                                             };

                TreeListColumn colName = new TreeListColumn
                                             {
                                                 Caption = "Наименование",
                                                 FieldName = "Name",
                                                 VisibleIndex = 0,
                                                 Width = 150
                                             };

                TreeListColumn colNumber = new TreeListColumn
                                               {
                                                   Caption = "Номер",
                                                   FieldName = "Number",
                                                   VisibleIndex = 1,
                                                   Width = 50
                                               };

                treeChains.Tree.Columns.AddRange(new [] { colId, colDate, colName, colNumber });
                frm.btnRefresh.ItemClick += delegate
                {
                    treeChains.Tree.ClearNodes();
                    TreeListNode node = treeChains.Tree.AppendNode(new object[] { doc.Id, doc.Date, doc.Name, doc.Number }, 0);
                    node.ImageIndex = 0;
                    node.StateImageIndex = 0;
                    node.SelectImageIndex = 0;
                    treeChains.Tree.AppendNode(new object[] { 0, DateTime.Now, "Empty", "000" }, node.Id);
                    treeChains.Tree.BeforeExpand += delegate(object sender, BeforeExpandEventArgs e)
                    {
                        e.Node.Nodes.Clear();
                        List<Document> list = null;
                        list = (int)buttonDocLinksView.Tag == 0 ? Document.GetChainSourceList(doc.Workarea, (int)e.Node.GetValue(GlobalPropertyNames.Id), 20) : Document.GetChainDestinationList(doc.Workarea, (int)e.Node.GetValue(GlobalPropertyNames.Id), 20);
                        foreach (Document d in list)
                        {
                            TreeListNode _new = treeChains.Tree.AppendNode(new object[] { d.Id, d.Date, d.Name, d.Number }, e.Node.Id);
                            _new.ImageIndex = 0;
                            _new.StateImageIndex = 0;
                            _new.SelectImageIndex = 0;
                            treeChains.Tree.AppendNode(new object[] { 0, DateTime.Now, "Empty", "000" }, _new.Id);
                        }
                    };
                    node.Expanded = true;
                };
                frm.btnRefresh.PerformClick();
                frm.Show();
            }
        }

        public static List<Document> BrowseList(this Document item, Predicate<Document> filter, List<Document> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<Document> BrowseMultyList(this Document item, Workarea wa, Predicate<Document> filter, List<Document> sourceCollection, bool allowMultySelect)
        {
            List<Document> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<Document>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());

            ListBrowserBaseObjects<Document> browserBaseObjects = new ListBrowserBaseObjects<Document>(wa, sourceCollection, filter, item, true, false, false, true);
            browserBaseObjects.Owner = frm;
            browserBaseObjects.Build();

            new FormStateMaintainer(frm, String.Format("Browse{0}", item.GetType().Name));
            frm.clientPanel.Controls.Add(browserBaseObjects.ListControl);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Always;
            frm.btnCreate.Visibility = BarItemVisibility.Always;
            frm.btnDelete.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;

            frm.btnDelete.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32);
            frm.btnSelect.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32);

            frm.btnProp.ItemClick += delegate
            {
                browserBaseObjects.InvokeProperties();
            };
            frm.btnDelete.ItemClick += delegate
            {
                browserBaseObjects.InvokeDelete();
            };
            browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            browserBaseObjects.ShowProperty += delegate(Document obj)
            {
                obj.ShowProperty();
                if (obj.IsNew)
                {
                    obj.Created += delegate
                    {
                        int position = browserBaseObjects.BindingSource.Add(obj);
                        browserBaseObjects.BindingSource.Position = position;
                    };
                }
            };

            browserBaseObjects.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Alt)
                {
                    returnValue = browserBaseObjects.SelectedValues;
                    frm.Close();
                }
            };
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browserBaseObjects.SelectedValues;
                frm.Close();
            };
            frm.btnClose.ItemClick += delegate
            {
                returnValue = null;
                frm.Close();

            };
            frm.ShowDialog();

            return returnValue;
        }

        //public static List<Document> BrowseContent(this Document item, Workarea wa = null)
        //{
        //    ContentModuleDocuments module = new ContentModuleDocuments();
        //    module.Workarea = item != null ? item.Workarea : wa;
        //    return module.ShowDialog(true);
        //}
    }
}