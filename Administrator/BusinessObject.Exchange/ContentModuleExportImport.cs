using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Developer;
using BusinessObjects.Exchange.Controls;
using BusinessObjects.Windows;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace BusinessObjects.Exchange
{
    public class ContentModuleExportImport : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        private const string TYPENAME = "EXPORTIMPORT";
        BindingSource _source;
        private ControlDataExportImport controlTreeList;
        private ControlList controlList;
        static DbObject CurrentTable;

        public ContentModuleExportImport()
        {
            Key = TYPENAME + "_MODULE";
            Caption = "Обмен данными";
        }
        
        #region IContentModule Members
        private Library _selfLib;
        public Library SelfLibrary
        {
            get
            {
                if (_selfLib == null && Workarea != null)
                    _selfLib = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(Key);
                return _selfLib;
            }
        }
        private string _parentKey;
        public string ParentKey
        {
            get
            {
                if (_parentKey == null && Workarea != null)
                {
                    if (SelfLibrary != null)
                    {
                        int? fHierarchyId = Hierarchy.FirstHierarchy<Library>(SelfLibrary);
                        if (fHierarchyId.HasValue && fHierarchyId.Value != 0)
                        {
                            Hierarchy h = Workarea.Cashe.GetCasheData<Hierarchy>().Item(fHierarchyId.Value);
                            _parentKey = UIHelper.FindParentHierarchy(h);

                        }

                    }

                }
                return _parentKey;
            }
            set { _parentKey = value; }
        }
        public void InvokeHelp()
        {

        }
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
            set
            {
                _workarea = value;
                Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.SERVER_X32);
            }
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        protected Control control;
        public Control Control
        {
            get
            {
                return control;
            }
        }

        private ExportImportData _exportImportData;

        private string _activeView = string.Empty;

        public string ActiveView
        {
            get { return _activeView; }
            set
            {
                _activeView = value;
                PerformShow();
            }
        }

        /// <summary>
        /// Обработка двойного клика по списку таблиц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GridTables_DoubleClick(object sender, EventArgs e)
        {
            if (_source.Current == null) return;

            Point p = controlTreeList.GridTables.PointToClient(Control.MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = controlTreeList.ViewTables.CalcHitInfo(p.X, p.Y);
            if (!hit.InRow || controlTreeList.ViewTables.FocusedRowHandle <= -1) return;
            DbObject dbObject = _source.Current as DbObject;
            dbObject.ShowProperty();
        }

        /// <summary>
        /// Обработка двойного клика по списку настроек импорта/экспорта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ControlList_DoubleClick(object sender, EventArgs e)
        {
            BindingSource source = controlList.Grid.DataSource as BindingSource;
            if (source.Current == null) return;

            Point p = controlList.Grid.PointToClient(Control.MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = controlList.View.CalcHitInfo(p.X, p.Y);
            if (hit.InRow && controlList.View.FocusedRowHandle > -1)
            {
                EditObject(source);
            }
        }

        /// <summary>
        /// Показывает диалог свойств текущего объекта в списке
        /// </summary>
        /// <param name="sourse"></param>
        void EditObject(BindingSource sourse)
        {
            XmlStorage currentObject = sourse.Current as XmlStorage;
            if (currentObject != null)
            {
                currentObject.ShowPropertyExchangeSettings(Workarea);
            }
            else
            {
                DataRowView rv = sourse.Current as DataRowView;
                if (rv != null)
                {
                    int id = (int)rv[GlobalPropertyNames.Id];
                    currentObject = Workarea.GetObject<XmlStorage>(id);
                    currentObject.ShowPropertyExchangeSettings(Workarea);
                }
            }
        }

        // Заполнить вторую таблицу данными
        void GridTablesPositionChanged(object sender, EventArgs e)
        {
            //Сохранение текущей выборки строк
            UpdateSelectedRowFromDataGridView();

            //Получение данных из текущей таблицы
            if(_source.Current ==null)
            {
                controlTreeList.GridData.DataSource = null;
                return;
            }
            DbObject currenttableName = _source.Current as DbObject;
            // TODO: добавить обработку ошибок, некоторые таблицы запрещено читать или они недоступны в текущий момент
            DataTable tbl = _exportImportData.GetDataTable(currenttableName);
            controlTreeList.GridData.DataSource = tbl;
            controlTreeList.ViewData.PopulateColumns();

            //настройка вида GrivView
            string[] HiddenColumns ={"Guid","DatabaseId","DbSourceId","Version"};

            foreach(GridColumn col in controlTreeList.ViewData.Columns)
            {
                //Спрятать ненужные колонки
                if(HiddenColumns.Contains(col.FieldName))
                {
                    col.Visible = false;
                }

                //Добавить фильтр по столбцу StateId
                if (col.FieldName == GlobalPropertyNames.StateId)
                {
                    col.FilterInfo = new ColumnFilterInfo("StateId = 1");
                }

                //Добавить количество для столбца id
                if (col.FieldName == GlobalPropertyNames.Id)
                {
                    col.SummaryItem.SummaryType= DevExpress.Data.SummaryItemType.Count; 
                }
            }

            //Выборка строк в DataGrid в соответствии с выборкой для новой таблицы
            controlTreeList.ViewData.ClearSelection();
            UpdateDataGridViewFromSelectedRow();
        }

        /// <summary>
        /// Обновить выделение полей GridView в соответствии с SelectedRow
        /// </summary>
        private void UpdateDataGridViewFromSelectedRow()
        {
            //Текущая таблица
            CurrentTable = _source.Current as DbObject;

            //Поиск элемента, соотвествующего текущей таблице
            Row row =
                _exportImportData.SelectedRow.FirstOrDefault(
                    s => (s.Table.Schema == CurrentTable.Schema && s.Table.Name == CurrentTable.Name));

            if (row == null)
            {
                return;
            }

            //Проход по всем строкам GridView
            for (int rowIndex = 0; rowIndex < controlTreeList.ViewData.RowCount; rowIndex++)
            {
                if (row.Id.Exists(s => s == (int)(controlTreeList.ViewData.GetRowCellValue(rowIndex, GlobalPropertyNames.Id))))
                {
                    controlTreeList.ViewData.SelectRow(rowIndex);
                }
                else
                {
                    controlTreeList.ViewData.UnselectRow(rowIndex);
                }
            }
        }

        /// <summary>
        /// Обновить SelectedRow в соответствии с выделением полей GridView
        /// </summary>
        private void UpdateSelectedRowFromDataGridView()
        {
            if (CurrentTable == null)
            {
                return;
            }

            //Удаляем элемент, соответствующий текущей таблице
            _exportImportData.SelectedRow.RemoveAll(s => (s.Table.Schema == CurrentTable.Schema && s.Table.Name == CurrentTable.Name));

            //Если ничего не выделено
            if (controlTreeList.ViewData.SelectedRowsCount == 0)
            {
                return;
            }

            //Создаем новый пустой
            Row row = new Row {Table = CurrentTable, Id = new List<int>()};

            //Проход по всем выделеным строкам GridView и добавляем индексы выделенных строк в список
            foreach (int rowIndex in controlTreeList.ViewData.GetSelectedRows())
            {
                if ((CurrentTable.Schema == "Core") && (CurrentTable.Name == "Entities"))
                    row.Id.Add((Int16)(controlTreeList.ViewData.GetRowCellValue(rowIndex, GlobalPropertyNames.Id)));
                else
                    row.Id.Add((int)(controlTreeList.ViewData.GetRowCellValue(rowIndex, GlobalPropertyNames.Id)));
            }

            //Добавляем новый элемент
            _exportImportData.SelectedRow.Add(row);
        }

        protected RibbonPageGroup groupLinksView;
        protected RibbonPageGroup groupLinksActionList;
        protected RibbonPageGroup groupLinksActionTreeList;

        /// <summary>
        /// Показывает заданый датасет в диалоге с двумя гридами (список таблиц и список данных)
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="ShowImportElements">Добавление в форму элементов управления для возможности последующего импорта DataSet</param>
        protected Form ShowDataSet(DataSet ds, bool ShowImportElements)
        {
            FormProperties FormImport = new FormProperties {Size = new Size(700, 480), Text = "Обмен данными"};

            if (ShowImportElements)
            {
                FormImport.btnSave.Visibility = BarItemVisibility.Never;

                BarButtonItem btnOk = FormImport.Ribbon.Items.CreateButton("Импорт");
                btnOk.RibbonStyle = RibbonItemStyles.Large;
                btnOk.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DATAINTO_X32);
                FormImport.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnOk);
                btnOk.ItemClick += delegate
                {
                    FormImport.DialogResult = DialogResult.OK;
                    FormImport.Close();
                };
            }

            ControlDataExportImport controlImport = new ControlDataExportImport();
            controlImport.ViewTables.Columns[2].Visible = false;
            controlImport.ViewTables.Columns[3].Visible = false;
            FormImport.clientPanel.Controls.Add(controlImport);
            FormImport.StartPosition = FormStartPosition.CenterParent;
            controlImport.Dock = DockStyle.Fill;

            //Получение списка таблиц
            List<DbObject> tableList = Workarea.GetCollection<DbObject>().Where(s=> ds.Tables.Contains(s.Schema+"."+s.Name)).ToList();

            BindingSource SourceImport = new BindingSource {DataSource = tableList};
            controlImport.GridTables.DataSource = SourceImport;

            #region Заполнение столбца "Действие"
            if (ShowImportElements)
            {
                using (SqlConnection cnn = Workarea.GetDatabaseConnection())
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();

                    foreach (DataTable dt in ds.Tables)
                    {
                        //Формирование параметра TVP
                        DataTable IdTable = new DataTable();
                        IdTable.Columns.Add("Guid", Type.GetType("System.Guid"));
                        foreach (DataRow row in dt.Rows)
                        {
                            IdTable.Rows.Add(row["Guid"]);
                        }

                        //Выполнение запроса
                        DataTable res = new DataTable();
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            #region Код, использующий хранимую процедуру
                            //cmd.CommandText = "Export.SearchDataByGuid";
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@TableName", dt.TableName.Split('.')[1]);
                            //cmd.Parameters.AddWithValue("@TVP", IdTable);
                            #endregion

                            #region Универсальный код
                            cmd.CommandText = "SELECT s.Guid, p.Id FROM @TVP s LEFT join " + dt.TableName + " p ON s.Guid= p.Guid";
                            var prm = cmd.Parameters.AddWithValue(GlobalSqlParamNames.TVP, IdTable);
                            prm.SqlDbType = SqlDbType.Structured;
                            prm.TypeName = "dbo.KeyListGuid";
                            #endregion

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(res);
                        }

                        //Заполнение столбца "Action"
                        dt.Columns.Add("Action");
                        foreach (DataRow row in dt.Rows)
                        {
                            DataRow dr = res.Select("Guid='" + row["Guid"] + "'")[0];  //В результате может быть только одна запись, т.к. поиск по Guid
                            if (Convert.IsDBNull(dr[GlobalPropertyNames.Id]))
                            {
                                row["Action"] = "INSERT";
                            }
                            else
                            {
                                row["Action"] = "UPDATE";
                            }
                        }
                    }
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            #endregion

            //Обработка события смены позиции
            controlImport.ViewTables.FocusedRowChanged +=
                delegate(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
                {
                    if (e.FocusedRowHandle < 0)
                    {
                        controlImport.GridData.DataSource = null;
                        controlImport.ViewData.Columns.Clear();
                    }
                    else
                    {
                        DbObject CurrentTable = SourceImport.Current as DbObject;
                        DataTable dt = ds.Tables[CurrentTable.Schema + "." + CurrentTable.Name];
                        controlImport.GridData.DataSource = dt;
                        controlImport.ViewData.PopulateColumns();
                    }
                };

            if((FormImport.ShowDialog()==DialogResult.OK)&&(ShowImportElements))
                _exportImportData.ImportDataSet(ds, Workarea.GetDatabaseConnection());

            return FormImport;
        }

        protected virtual void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;
            #region View
            groupLinksView = page.GetGroupByName(TYPENAME + "_VIEW");
            if (groupLinksView == null)
            {
                groupLinksView = new RibbonPageGroup { Name = TYPENAME + "_VIEW", Text = Workarea.Cashe.ResourceString(ResourceString.STR_CAPTION_VIEW, 1049) };

                BarCheckItem btnViewList = form.Ribbon.Items.CreateCheckItem(Workarea.Cashe.ResourceString(ResourceString.STR_CAPTION_VIEWLIST, 1049), true);
                btnViewList.ButtonStyle = BarButtonStyle.Default;
                btnViewList.GroupIndex = 1;
                btnViewList.RibbonStyle = RibbonItemStyles.Default;
                groupLinksView.ItemLinks.Add(btnViewList);
                btnViewList.ItemClick += delegate
                {
                    ActiveView = "LIST";
                };
                
                BarCheckItem btnViewTreeList = form.Ribbon.Items.CreateCheckItem("Группы", false);
                btnViewTreeList.ButtonStyle = BarButtonStyle.Default;
                btnViewTreeList.GroupIndex = 1;
                btnViewTreeList.RibbonStyle = RibbonItemStyles.Default;
                groupLinksView.ItemLinks.Add(btnViewTreeList);
                btnViewTreeList.ItemClick += delegate
                {
                    ActiveView = "TREELIST";
                };

                page.Groups.Add(groupLinksView);
            }
            #endregion

            #region TreeList
            groupLinksActionTreeList = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
            if (groupLinksActionTreeList == null)
            {
                groupLinksActionTreeList = new RibbonPageGroup {Name = TYPENAME + "_ACTIONLIST", Text = "Действия"};

                #region Импорт данных
                BarButtonItem btnImportData = new BarButtonItem
                                                  {
                                                      Caption = "Импорт данных",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph =
                                                          ResourceImage.GetByCode(Workarea, ResourceImage.DATAINTO_X32)
                                                  };
                groupLinksActionTreeList.ItemLinks.Add(btnImportData);
                btnImportData.ItemClick += delegate
                {
                    //Импорт данных 
                    try
                    {
                        OpenFileDialog openFileDialog1 = new OpenFileDialog
                                                             {
                                                                 Filter = "XML файлы|*.xml|Все файлы|*.*",
                                                                 InitialDirectory =
                                                                     Environment.GetFolderPath(
                                                                         Environment.SpecialFolder.MyDocuments)
                                                             };
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            DataSet ds = new DataSet();
                            ds.ReadXml(openFileDialog1.FileName);
                            ShowDataSet(ds, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                };
                #endregion

                #region Экспорт данных
                BarButtonItem btnExportData = new BarButtonItem
                                                  {
                                                      Caption = "Экспорт данных",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DATAOUT_X32)
                                                  };
                groupLinksActionTreeList.ItemLinks.Add(btnExportData);
                btnExportData.ItemClick += delegate
                {
                    //Экспорт данных
                    UpdateSelectedRowFromDataGridView();

                    if (_exportImportData.SelectedRow.Count == 0)
                    {
                        MessageBox.Show("Ничего не выделено");
                        return;
                    }
                    try
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog
                                                             {
                                                                 Filter = "XML файлы|*.xml|Все файлы|*.*",
                                                                 InitialDirectory =
                                                                     Environment.GetFolderPath(
                                                                         Environment.SpecialFolder.MyDocuments),
                                                                 FileName =
                                                                     "Обмен данными от " +
                                                                     DateTime.Now.ToString("yyyyddmm_hhmmss") + ".xml"
                                                             };
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            _exportImportData.ExportXmlData(saveFileDialog1.FileName, Workarea.GetDatabaseConnection());
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                };
                #endregion

                #region Очистить выборку
                BarButtonItem btnClear = new BarButtonItem
                                             {
                                                 Caption = "Очистить выборку",
                                                 RibbonStyle = RibbonItemStyles.Large,
                                                 Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                             };
                groupLinksActionTreeList.ItemLinks.Add(btnClear);
                btnClear.ItemClick += delegate
                {
                    _exportImportData.SelectedRow.Clear();
                    controlTreeList.ViewData.ClearSelection();
                };
                #endregion

                page.Groups.Add(groupLinksActionTreeList);
            }
            #endregion

            #region List
            groupLinksActionList = page.GetGroupByName(TYPENAME + "_SETTINGSLIST");
            if (groupLinksActionList != null) return;
            groupLinksActionList = new RibbonPageGroup 
            {
                Name = TYPENAME + "_SETTINGSLIST",
                Text = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_SETUP, 1049)
            };

            #region Создать
            BarButtonItem btnCreate = new BarButtonItem
                                          {
                                              Caption =
                                                  Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                                              RibbonStyle = RibbonItemStyles.Large,
                                              Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                          };
            groupLinksActionList.ItemLinks.Add(btnCreate);
            btnCreate.ItemClick += delegate
                                       {
                                           XmlStorage newObj = new XmlStorage {Workarea = Workarea, KindValue = 2};
                                           Form frm = newObj.ShowPropertyExchangeSettings(Workarea);
                                           frm.FormClosed += delegate
                                                                 {
                                                                     RefreshListData();
                                                                 };
                                       };
            #endregion

            #region Изменить
            BarButtonItem btnEdit = new BarButtonItem
                                        {
                                            Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_EDIT", 1049),
                                            RibbonStyle = RibbonItemStyles.Large,
                                            Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                        };
            groupLinksActionList.ItemLinks.Add(btnEdit);
            btnEdit.ItemClick += delegate
                                     {
                                         BindingSource source = controlList.Grid.DataSource as BindingSource;
                                         EditObject(source);
                                     };
            #endregion

            #region Удалить
            BarButtonItem btnDelete = new BarButtonItem
                                          {
                                              Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                              RibbonStyle = RibbonItemStyles.Large,
                                              Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                          };
            groupLinksActionList.ItemLinks.Add(btnDelete);
            btnDelete.ItemClick += delegate
                                       {
                                           BindingSource DocumentsBindingSource = controlList.Grid.DataSource as BindingSource;
                                           if (DocumentsBindingSource.Current == null) return;
                                           int[] rows = controlList.View.GetSelectedRows();

                                           if (rows == null) return;
                                           int res = Windows.Extentions.ShowMessageChoice(Workarea,
                                                                                                          Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                                                                                          "Удаление",
                                                                                                          "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                                                                          Windows.Properties.Resources.STR_CHOICE_DEL);
                                           for (int j = rows.Length - 1; j >= 0; j--)
                                           {
                                               bool DocIsRowView = false;
                                               DataRowView rv = null;
                                               int i = rows[j];
                                               XmlStorage op = controlList.View.GetRow(i) as XmlStorage;

                                               if (op == null)
                                               {
                                                   rv = controlList.View.GetRow(i) as DataRowView;
                                                   if (rv != null)
                                                   {
                                                       int docid = (int)rv[GlobalPropertyNames.Id];
                                                       op = Workarea.GetObject<XmlStorage>(docid);
                                                       DocIsRowView = true;
                                                   }
                                               }
                                               if (op == null) continue;
                                               switch (res)
                                               {
                                                   case 0:
                                                       try
                                                       {
                                                           op.Remove();
                                                           DocumentsBindingSource.Remove(op);
                                                       }
                                                       catch (DatabaseException dbe)
                                                       {
                                                           Windows.Extentions.ShowMessageDatabaseExeption(Workarea,
                                                                                                                          Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                                          "Ошибка удаления!", dbe.Message, dbe.Id);
                                                       }
                                                       catch (Exception ex)
                                                       {
                                                           XtraMessageBox.Show(ex.Message,
                                                                                                      Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                       }
                                                       break;
                                                   case 1:
                                                       try
                                                       {
                                                           if (!DocIsRowView)
                                                               DocumentsBindingSource.Remove(op);
                                                           else
                                                               DocumentsBindingSource.Remove(rv);
                                                           op.Delete();
                                                       }
                                                       catch (DatabaseException dbe)
                                                       {
                                                           Windows.Extentions.ShowMessageDatabaseExeption(Workarea,
                                                                                                                          Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                                          "Ошибка удаления!", dbe.Message, dbe.Id);
                                                       }
                                                       catch (Exception ex)
                                                       {
                                                           XtraMessageBox.Show(ex.Message,
                                                                                                      Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                       }
                                                       break;
                                               }
                                           }
                                       };
            #endregion

            #region Импорт
            BarButtonItem btnImportSettings = new BarButtonItem
                                                  {
                                                      Caption = "Импорт",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph =
                                                          ResourceImage.GetByCode(Workarea, ResourceImage.DATAINTO_X32)
                                                  };
            groupLinksActionList.ItemLinks.Add(btnImportSettings);
            btnImportSettings.ItemClick += delegate
                                               {
                                                   //Импорт данных 
                                                   try
                                                   {
                                                       OpenFileDialog openFileDialog1 = new OpenFileDialog
                                                                                            {
                                                                                                Filter = "XML файлы|*.xml|Все файлы|*.*",
                                                                                                InitialDirectory =
                                                                                                    Environment.GetFolderPath(
                                                                                                        Environment.SpecialFolder.MyDocuments)
                                                                                            };
                                                       if (openFileDialog1.ShowDialog() == DialogResult.OK)
                                                       {
                                                           XmlStorage newObj = new XmlStorage { Workarea = Workarea };
                                                           ExchangeSettings exchangeSettings = ExchangeSettings.LoadFromFile(openFileDialog1.FileName);
                                                           newObj.KindValue = 2;
                                                           newObj.XmlData = ExchangeSettings.SaveToString(exchangeSettings);
                                                           newObj.Name = exchangeSettings.Name;
                                                           newObj.Memo = exchangeSettings.Memo;
                                                           newObj.Code = exchangeSettings.Code;
                                                           newObj.Save();
                                                           RefreshListData();
                                                       }
                                                   }
                                                   catch (Exception ex)
                                                   {
                                                       MessageBox.Show(ex.Message, ex.GetType().ToString());
                                                   }
                                               };
            #endregion

            #region Экспорт
            BarButtonItem btnExportSettings = new BarButtonItem
                                                  {
                                                      Caption = "Экспорт",
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph =
                                                          ResourceImage.GetByCode(Workarea, ResourceImage.DATAOUT_X32)
                                                  };
            groupLinksActionList.ItemLinks.Add(btnExportSettings);
            btnExportSettings.ItemClick += delegate
                                               {
                                                   try
                                                   {
                                                       SaveFileDialog saveFileDialog1 = new SaveFileDialog
                                                                                            {
                                                                                                Filter = "XML файлы|*.xml|Все файлы|*.*",
                                                                                                InitialDirectory =
                                                                                                    Environment.GetFolderPath(
                                                                                                        Environment.SpecialFolder.MyDocuments),
                                                                                                FileName =
                                                                                                    "Настройки экспорта данных от " +
                                                                                                    DateTime.Now.ToString().Replace(":", ".") + ".xml"
                                                                                            };
                                                       if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                                                       {
                                                           BindingSource source = controlList.View.DataSource as BindingSource;
                                                           if (source.Current != null)
                                                           {
                                                               string XmlData=(source.Current as XmlStorage).XmlData;
                                                               ExchangeSettings.SaveToFile(saveFileDialog1.FileName, ExchangeSettings.LoadFromString(XmlData));
                                                           }
                                                       }
                                                   }
                                                   catch (Exception ex)
                                                   {
                                                       MessageBox.Show(ex.Message, ex.GetType().ToString());
                                                   }
                                               };
            #endregion

            #region Выполнить
            BarButtonItem btnRun = new BarButtonItem
                                       {
                                           Caption = "Выполнить",
                                           RibbonStyle = RibbonItemStyles.Large,
                                           Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.RUNROUNDGREEN_X32)
                                       };
            groupLinksActionList.ItemLinks.Add(btnRun);
            btnRun.ItemClick += delegate
                                    {
                                        BindingSource source = controlList.Grid.DataSource as BindingSource;
                                        if (source.Current == null) return;
                                        XmlStorage storage=source.Current as XmlStorage;
                                        ExchangeSettings exchangeSettings = ExchangeSettings.LoadFromString(storage.XmlData);
                                        XtraMessageBox.Show(
                                            _exportImportData.RunExportSettings(exchangeSettings, Workarea.GetDatabaseConnection())
                                                ? "Настройки обмена удачно выполенены"
                                                : "Ошибка выполения настройки обмена", Application.ProductName, MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                                    };
            #endregion

            #region Выполнить на другом сервере
            BarButtonItem btnRunAtAnotherServer = new BarButtonItem
            {
                Caption = "Выполнить на другом сервере",
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.RUNROUNDGREEN_X32)
            };
            groupLinksActionList.ItemLinks.Add(btnRunAtAnotherServer);
            btnRunAtAnotherServer.ItemClick += delegate
            {
                BindingSource source = controlList.Grid.DataSource as BindingSource;
                if (source.Current == null) return;
                XmlStorage storage = source.Current as XmlStorage;
                ExchangeSettings exchangeSettings = ExchangeSettings.LoadFromString(storage.XmlData);

                if (exchangeSettings.Code != "IMPORT")
                {
                    MessageBox.Show("На другом сервере можно выполнять только настройки импорта!",
                                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<Branche> branches = Workarea.Empty<Branche>().BrowseList(f => f.KindValue == 1, null);
                Branche branche = null;

                if (branches != null && branches.Count > 0)
                    branche = branches[0];

                if (branche != null)
                {
                    XtraMessageBox.Show(
                        _exportImportData.RunExportSettings(exchangeSettings, branche.GetDatabaseConnection())
                            ? "Настройки обмена удачно выполенены"
                            : "Ошибка выполения настройки обмена", Application.ProductName, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            };
            #endregion

            #region Последняя транзакци
            BarButtonItem btnLastTransaction = new BarButtonItem
                                                   {
                                                       Caption = "Последняя транзакция",
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph =
                                                           ResourceImage.GetByCode(Workarea, ResourceImage.SEARCH_X32)
                                                   };
            groupLinksActionList.ItemLinks.Add(btnLastTransaction);
            btnLastTransaction.ItemClick += delegate
                                                {
                                                    if (_exportImportData.MemoryDataSet == null)
                                                        XtraMessageBox.Show("Память последней транзакции пустая", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    else
                                                        ShowDataSet(_exportImportData.MemoryDataSet,false);
                                                };
            #endregion

            page.Groups.Add(groupLinksActionList);

            #endregion
        }

        public void PerformShow()
        {
            if (control == null)
            {
                control = new Control();
                _exportImportData = new ExportImportData { Workarea = Workarea };
            }
            if (ActiveView == string.Empty)
                _activeView = "LIST";
            if (ActiveView == "LIST")
                CreateList();
            if (ActiveView == "TREELIST")
                CreateTreeList();
        }

        private void MakeVisiblePageGroup()
        {
            if (groupLinksView != null)
                groupLinksView.Visible = true;

            if (ActiveView == "LIST")
                groupLinksActionList.Visible = true;
            else if (groupLinksActionList != null)
                groupLinksActionList.Visible = false;

            if (ActiveView == "TREELIST")
                groupLinksActionTreeList.Visible = true;
            else if (groupLinksActionTreeList != null)
                groupLinksActionTreeList.Visible = false;
        }
        
        public void PerformHide()
        {
            if (groupLinksView != null)
                groupLinksView.Visible = false;

            if (groupLinksActionTreeList != null)
                groupLinksActionTreeList.Visible = false;

            if (groupLinksActionList != null)
                groupLinksActionList.Visible = false;
        }

        void HideAllExclude(Control excludeControl)
        {
            foreach (Control v in from Control v in control.Controls where v != excludeControl select v)
            {
                v.Visible = false;
            }
            excludeControl.Visible = true;
        }

        public Form Owner { get; set; }
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
            // TODO: 16
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.SERVER_X32);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            navigator.SafeAddModule(Key, this);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;

            frm.Show();
        }
        /// <summary>
        /// Создания дерева с именами таблиц и списка с данными
        /// </summary>
        private void CreateTreeList()
        {
            if (controlTreeList == null)
            {
                controlTreeList = new ControlDataExportImport();
                _source = new BindingSource();

                //Получение списка таблиц
                List<DbObject> coll = Workarea.GetCollection<DbObject>();

                //Привязка источника данных для формы
                _source.DataSource = coll;
                controlTreeList.GridTables.DataSource = _source;
                _source.PositionChanged += GridTablesPositionChanged;
                controlTreeList.GridTables.DoubleClick += GridTables_DoubleClick;
                control.Controls.Add(controlTreeList);
                controlTreeList.Dock = DockStyle.Fill;
            }

            RegisterPageAction();
            MakeVisiblePageGroup();
            HideAllExclude(controlTreeList);
        }

        /// <summary>
        /// Создание списка настроек обмена
        /// </summary>
        private void CreateList()
        {
            if (controlList == null)
            {
                controlList = new ControlList {Dock = DockStyle.Fill};
                controlList.View.OptionsSelection.MultiSelect = true;
                controlList.Grid.DoubleClick += ControlList_DoubleClick;
                control.Controls.Add(controlList);
                DataGridViewHelper.GenerateGridColumns(Workarea, controlList.View, "DEFAULT_LISTVIEW");
                //Форматирование
                controlList.View.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                                                            {
                                                                e.Value = ResourceImage.GetByCode(_workarea, ResourceImage.DATABASE_X16);
                                                            };
            }
            RefreshListData();
            RegisterPageAction();
            MakeVisiblePageGroup();
            HideAllExclude(controlList);
        }

        /// <summary>
        /// Обновляет данные для списка настроек экспорта-импорта
        /// </summary>
        private void RefreshListData()
        {
            List<XmlStorage> coll = Workarea.GetCollection<XmlStorage>().Where(s => s.KindValue==2).ToList();
            BindingSource source=new BindingSource {DataSource = coll};
            controlList.Grid.DataSource = source;
        }
        #endregion
    }
}
