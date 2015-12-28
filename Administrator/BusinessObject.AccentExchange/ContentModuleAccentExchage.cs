using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects.AccentExchange.Controls;
using BusinessObjects;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using BusinessObjects.Exchange;

namespace BusinessObjects.AccentExchange
{
    /// <summary>
    /// Модуль обмена данными с программой "Акцент 7" 
    /// </summary>
    /// <remarks>
    /// Значение ключа модуля "ACCENT7EXCHANGE_MODULE".
    /// Значение ключа отображаемой иконки "ACCENT32"
    /// Модуль использует наборs колонок: "DEFAULT_LISTVIEWACCENTAGENTS" - для представления списка данных о кореспондентах;
    /// "DEFAULT_LISTVIEWACCENTPRODUCTS" - список колонок о товарах 
    /// </remarks>
    public class ContentModuleAccentExchage : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        private const string TYPENAME = "ACCENT7EXCHANGE";
        //ControlImportAgent
        private Controls.ControlAccent7Exchange _controlMain;
        private RibbonPageGroup _groupLinksAction,_groupLinksCheck;
        BindingSource _source;
        private readonly Dictionary<string, string> _dataCollection;
        public ContentModuleAccentExchage()
        {
            Caption = "Обмен данными Акцент7";
            Key = TYPENAME + "_MODULE";
            _dataCollection = new Dictionary<string, string>
                                 {
                                     {"IMPORTAGENT", "Импорт корреспондентов"},
                                     {"IMPORTAGENTGROUPS", "Импорт групп корреспондентов"},
                                     {"IMPORTPRODUCT", "Импорт товаров"},
                                     {"IMPORTPRODUCTGROUPS", "Импорт групп товаров"},
                                     {"IMPORTANALITICBRAND", "Импорт брендов"},
                                     {"IMPORTANALITICPRODUCTTYPE", "Импорт видов продукции"},
                                     {"IMPORTANALITICPACKTYPE", "Импорт типов упаковки"},
                                     {"IMPORTANALITICTRADEMARK", "Импорт торговых групп"},
                                     {"IMPORTANALITICMETRICAREA", "Импорт аналитики метраж"},
                                     {"IMPORTANALITICTypeOutlet", "Импорт аналитики тип торговой точки"}
                                 };
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
        // Изображение
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea
        {
            get { return _workarea; }
            set
            {
                _workarea = value;
                Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.ACCENT_X32);
            }
        }

        public string Key { get; set; }
        /// <summary>
        /// Надпись
        /// </summary>
        public string Caption { get; set; }

        public Control Control
        {
            get
            {
                return _controlMain;
            }
        }

        void CreateControls()
        {
            _controlMain = new Controls.ControlAccent7Exchange();
            _source = new BindingSource();
            _source.DataSource = _dataCollection;
            _controlMain.Grid.DataSource = _source;
            _source.PositionChanged += new EventHandler(_source_PositionChanged);
            //DataGridViewHelper.GenerateGridColumns(Workarea, _controlMain.View, "DEFAULT_LISTVIEWACCENTAGENTS");
            _controlMain.Grid.DoubleClick += Grid_DoubleClick;
            //_controlMain.View.CustomUnboundColumnData += View_CustomUnboundColumnData;
            //_controlMain.View.OptionsView.RowAutoHeight = true;
        }
        void HideControls(string key)
        {
            foreach (Control c in _controlMain.splitContainer.Panel2.Controls)
            {
                if (c.Name != key) c.Visible = false;
            }
        }

        private string KeyActive;
        void _source_PositionChanged(object sender, EventArgs e)
        {
            if(_source.Position!=-1)
            {
                System.Collections.Generic.KeyValuePair<string, string> val = (System.Collections.Generic.KeyValuePair<string, string>)_source.Current;
                HideButtons(KeyActive);
                HideControls(val.Key);
                KeyActive = val.Key;
                switch(val.Key)
                {
                    case "IMPORTAGENT":
                        BuildControlImportAgent();
                        break;
                    case "IMPORTAGENTGROUPS":
                        BuildControlImportAgentGroups();
                        break;
                    case "IMPORTPRODUCT":
                        BuildControlImportProduct();
                        break;
                    case "IMPORTPRODUCTGROUPS":
                        BuildControlImportProductGroups();
                        break;
                    case "IMPORTANALITICBRAND":
                        BuildControlImportAnaliticBrands();
                        break;
                    case "IMPORTANALITICPRODUCTTYPE":
                        BuildControlImportProductType();
                        break;
                    case "IMPORTANALITICPACKTYPE":
                        BuildControlImportPackType();
                        break;
                    case "IMPORTANALITICTRADEMARK":
                        BuildControlImportTradeMark();
                        break;
                    case "IMPORTANALITICMETRICAREA":
                        BuildControlImportAnaliticMetricArea();
                        break;
                    case "IMPORTANALITICTypeOutlet":
                        BuildControlImportAnaliticTypeOutlet();
                        break;
                    default:
                        KeyActive = string.Empty;
                        HideControls(string.Empty);
                        break;
                }
            }
            else
            {
                KeyActive = string.Empty;
                HideControls(string.Empty);
            }
        }

        private void HideButtons(string key)
        {
            switch (key)
            {
                case "IMPORTAGENT":
                    btnImpAgGetData.Visibility = BarItemVisibility.Never;
                    btnImpAgUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTAGENTGROUPS":
                    btnImpAgentGroupsGetData.Visibility = BarItemVisibility.Never;
                    btnImpAgentGroupsUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTPRODUCT":
                    btnImpProdGetData.Visibility = BarItemVisibility.Never;
                    btnImpProdUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTPRODUCTGROUPS":
                    btnImpProdGroupsGetData.Visibility = BarItemVisibility.Never;
                    btnImpProdGroupsUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTANALITICBRAND":
                    btnImpAnaliticBrandsGetData.Visibility = BarItemVisibility.Never;
                    btnImpAnaliticBrandsUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTANALITICPRODUCTTYPE":
                    btnImpProdTypeGetData.Visibility = BarItemVisibility.Never;
                    btnImpProdTypeUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTANALITICPACKTYPE":
                    btnImpPackTypeGetData.Visibility = BarItemVisibility.Never;
                    btnImpPackTypeUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTANALITICTRADEMARK":
                    btnImpTradeMarkGetData.Visibility = BarItemVisibility.Never;
                    btnImpTradeMarkUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTANALITICMETRICAREA":
                    btnImpAnaliticMetricAreaGetData.Visibility = BarItemVisibility.Never;
                    btnImpAnaliticMetricAreaUpdateData.Visibility = BarItemVisibility.Never;
                    break;
                case "IMPORTANALITICTypeOutlet":
                    btnImpAnaliticTypeOutletGetData.Visibility = BarItemVisibility.Never;
                    btnImpAnaliticTypeOutletUpdateData.Visibility = BarItemVisibility.Never;
                    break;
            }
        }

        void View_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            /*
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                ResourceImage imageItem = _source[e.ListSourceRowIndex] as ResourceImage;
                if (imageItem != null)
                {
                    //e.Value = imageItem.GetImage();
                }
                else
                {
                    System.Data.DataRowView rv = _source[e.ListSourceRowIndex] as System.Data.DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId) && rv.DataView.Table.Columns.Contains("KindId"))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        int kindId = (int)rv["KindId"];
                        e.Value = ExtentionsImage.GetImage(Workarea, kindId, stId);
                    }
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                ResourceImage imageItem = _source[e.ListSourceRowIndex] as ResourceImage;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
                else
                {
                    System.Data.DataRowView rv = _source[e.ListSourceRowIndex] as System.Data.DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        e.Value = ExtentionsImage.GetImageState(Workarea, stId);
                    }
                }
            }
            else if (e.Column.Name == "colValue" && e.IsGetData)
            {
                ResourceImage imageItem = _source[e.ListSourceRowIndex] as ResourceImage;
                e.Value = imageItem.Value;
                //_controlMain.View.Ro e.RowHandle
                //if (_controlMain.View.RowHeight < imageItem.Value.Size.Height)
                //    _controlMain.View.RowHeight = imageItem.Value.Size.Height;
            }
            */

        }
        void Grid_DoubleClick(object sender, System.EventArgs e)
        {
            //System.Drawing.Point p = _controlMain.Grid.PointToClient(Control.MousePosition);
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlMain.View.CalcHitInfo(p.X, p.Y);
            //if (hit.InRowCell)
            //{
            //    if (_source.Current == null) return;
            //    (_source.Current as ResourceImage).ShowProperty();
            //}
        }
        private BindingSource _bindServers;
        private RepositoryItemGridLookUpEdit _cmbReportserver;
        private BarEditItem searchContainer;
        public void PerformShow()
        {
            if (_controlMain == null)
            {
                CreateControls();
            }
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = true;
            if (_groupLinksCheck != null)
                _groupLinksCheck.Visible = true;

            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksAction = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
            if (_groupLinksAction == null)
            {
                _groupLinksAction = new RibbonPageGroup { Name = TYPENAME + "_ACTIONLIST", Text = Workarea.Cashe.ResourceString("LB_RIBBON_ACTION", 1049) };

                BarStaticItem barStaticItem1 = new BarStaticItem { Caption = "База данных Акцент7:" };
                _groupLinksAction.ItemLinks.Add(barStaticItem1);



                _cmbReportserver = new RepositoryItemGridLookUpEdit
                                       {
                                           TextEditStyle =
                                               DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor,
                                           View = {OptionsView = {ShowIndicator = false}}
                                       };
                searchContainer = new BarEditItem();

                DataGridViewHelper.GenerateGridColumns(Workarea, _cmbReportserver.View, "DEFAULT_LOOKUP_NAME");
                //cmbReportserver.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Наименование"));
                searchContainer.Width = 150;
                searchContainer.Edit = _cmbReportserver;
                _cmbReportserver.Buttons.Add(
                    new DevExpress.XtraEditors.Controls.EditorButton(
                        DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph));
                _cmbReportserver.Buttons[1].Image = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X16);
                _cmbReportserver.ButtonClick += CmbReportserverButtonClick;
                _groupLinksAction.ItemLinks.Add(searchContainer);

                Hierarchy rootServers = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("ACCENT7");
                if (rootServers != null)
                {
                    List<Branche> collBranches = rootServers.GetTypeContents<Branche>();
                    _bindServers = new BindingSource {DataSource = collBranches};
                    _cmbReportserver.DataSource = _bindServers;
                    if (_bindServers.Count > 0)
                    {
                        _bindServers.Position = 0;
                        searchContainer.EditValue = _bindServers[0];
                    }
                }
                page.Groups.Add(_groupLinksAction);
                
            }

            _groupLinksCheck = page.GetGroupByName(TYPENAME + "_CHECKLIST");
            if (_groupLinksCheck == null)
            {
                _groupLinksCheck = new RibbonPageGroup { Name = TYPENAME + "_CHECKLIST", Text = "Выборка" };

                BarButtonItem btnCheckAll = new BarButtonItem
                {
                    Caption = "Выбрать всё",
                    Name = "CheckAll",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32)
                };
                btnCheckAll.ItemClick += delegate
                                            {
                                                foreach (Control ctrl in _controlMain.splitContainer.Panel2.Controls)
                                                {
                                                    if (ctrl.Visible)
                                                    {
                                                        DataTable tbl = ((ControlImportGrid)ctrl).Grid.DataSource as DataTable;
                                                        if (tbl == null)
                                                            break;
                                                        foreach (DataRow row in tbl.Rows)
                                                        {
                                                            row["Imp"] = true;
                                                        }
                                                        break;
                                                    }
                                                }
                                            };
                _groupLinksCheck.ItemLinks.Add(btnCheckAll);

                BarButtonItem btnUncheckAll = new BarButtonItem
                {
                    Caption = "Снять выбор",
                    Name = "UncheckAll",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                };
                btnUncheckAll.ItemClick += delegate
                                            {
                                                foreach (Control ctrl in _controlMain.splitContainer.Panel2.Controls)
                                                {
                                                    if (ctrl.Visible)
                                                    {
                                                        DataTable tbl = ((ControlImportGrid)ctrl).Grid.DataSource as DataTable;
                                                        if (tbl == null)
                                                            break;
                                                        foreach (DataRow row in tbl.Rows)
                                                        {
                                                            row["Imp"] = false;
                                                        }
                                                        //((ControlImportGrid)ctrl).Grid.RefreshDataSource();
                                                        break;
                                                    }
                                                }
                                            };
                _groupLinksCheck.ItemLinks.Add(btnUncheckAll);

                page.Groups.Add(_groupLinksCheck);
            }

            _source_PositionChanged(null, null);
            
        }

        void CmbReportserverButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index ==1 )
            {
                Hierarchy rootServers = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("ACCENT7");
                if (rootServers != null)
                {
                    List<Branche> collBranches = rootServers.GetTypeContents<Branche>();
                    _bindServers.DataSource = collBranches;
                    _cmbReportserver.DataSource = _bindServers;
                    if (_bindServers.Count > 0)
                    {
                        _bindServers.Position = 0;
                        searchContainer.EditValue = _bindServers[0];
                    }
                }
            }
        }

        public void PerformHide()
        {
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = false;
            if (_groupLinksCheck != null)
                _groupLinksCheck.Visible = false;
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
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.DATABASE_X16);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            navigator.SafeAddModule(Key, this);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;

            frm.Show();
        }
        #endregion

        /// <summary>
        /// Конструирование запроса на обновление на основе заданной таблицы и его выполнение
        /// </summary>
        /// <param name="tbl">Импортируемая таблица</param>
        /// <param name="code">Код иерархии, в которую добавляются элементы, если он не null</param>
        /// <param name="entType">Тип добавляемых элементов. Не имеет значения, если код =null</param>
        private void ImportData(DataTable tbl, string code = null, WhellKnownDbEntity entType = WhellKnownDbEntity.Analitic)
        {
            if (tbl == null)
                return;

            string[] excludedColumns = {
                                            GlobalPropertyNames.Id, "KindName", "Imp", "Exist",
                                            "UnitShot", "UnitGuid", "PakcTypeGuid", "TradeMarkGuid", "BrandGuid","PakcTypeGuid"
                                       };

            ExportImportData exportImportData = new ExportImportData();
            exportImportData.Workarea = Workarea;

            DataTable res = new DataTable();

            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if(cnn.State == ConnectionState.Closed)
                    cnn.Open();

                DataRow[] rows = tbl.Select("Imp = true");
                if (rows.Count() == 0)
                    return;
                DataTable tbl2 = rows.CopyToDataTable();
                tbl2.TableName = tbl.TableName;
                exportImportData.CopyTableToServer(tbl2, cnn);

                //Формирование инструкции UPDATE
                string sql = "MERGE " + tbl.TableName + " trg USING " + exportImportData.GetTmpTableName(tbl.TableName) + " src ON trg.Guid=src.Guid \n" +
                             "WHEN MATCHED \n" +
                             "      THEN UPDATE SET ";

                int i = 0;
                foreach (DataColumn column in tbl.Columns)
                {
                    if (excludedColumns.Contains(column.ColumnName))
                        continue;

                    if (i > 0)
                        sql += ", ";

                    if ((i % 4 == 0) && (i > 0))
                        sql += " \n                      ";

                    sql += "trg." + column.ColumnName + " = src." + column.ColumnName;
                    i++;
                }

                //ФОрмирование инструкции INSERT
                sql += "\n" +
                       "WHEN NOT MATCHED BY TARGET \n" +
                       "        THEN INSERT(";
                i = 0;
                foreach (DataColumn column in tbl.Columns)
                {
                    if (excludedColumns.Contains(column.ColumnName))
                        continue;

                    if (i > 0)
                        sql += ", ";
                    sql += column.ColumnName;
                    i++;
                }

                sql += ") \n" +
                       "             VALUES(";
                i = 0;
                foreach (DataColumn column in tbl.Columns)
                {
                    if (excludedColumns.Contains(column.ColumnName))
                        continue;

                    if (i > 0)
                        sql += ", ";
                    sql += column.ColumnName;
                    i++;
                }
                sql += ")\n"+
                       "OUTPUT $action, Inserted.Id;";


                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                SqlDataAdapter dataAdapter=new SqlDataAdapter(cmd);
                dataAdapter.Fill(res);

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            if (code != null)
            {
                Hierarchy h = Hierarchy.FindByCode(Workarea, code, (short) entType)[0];
                if(h==null)
                {
                    throw new System.Exception("Иерархия не найдена!");
                }
                foreach (DataRow row in res.Select("$action = 'INSERT'"))
                {
                    Analitic a = Workarea.GetObject<Analitic>((int)row[GlobalPropertyNames.Id]);
                    h.ContentAdd(a);
                }
            }
        }

        /// <summary>
        /// Импорт иерархий
        /// </summary>
        /// <param name="kind">Тип иерархии</param>
        /// <param name="tbl">Импортируемая таблица</param>
        private void ImportHierarchy(WhellKnownDbEntity kind, DataTable tbl)
        {
            List<Hierarchy> coll = Workarea.GetCollection<Hierarchy>();
            List<Hierarchy> collRoot = Workarea.Empty<Hierarchy>().GetCollectionHierarchy((int)kind);

            int maxLevel = 0;
            foreach (DataRow row in tbl.Rows.Cast<DataRow>().Where(row => (int)row["lv"] > maxLevel))
            {
                maxLevel = (int)row["lv"] + 1;
            }

            //Импортируем по очереди каждый уровнь иерархии
            for (int i = 0; i < maxLevel; i++)
            {
                foreach (DataRow row in tbl.Select("lv = " + i + " AND Imp = true"))
                {
                    Hierarchy h = coll.FirstOrDefault(s => ((ICoreObject)s).Guid == (Guid)row["Guid"]);
                    //int id = row["NewPK]
                    //h = Workarea.Cashe.GetCasheData<Hierarchy>().Item(id);
                    if (h == null)
                    {
                        h = new Hierarchy
                        {
                            Workarea = Workarea,
                            ContentEntityId = (short)kind,
                            //ParentId = (int)row["ParentId"],
                            KindId = 1835009,
                        };
                        ((ICoreObject)h).Guid = (Guid)row["Guid"];

                    }

                    if (h.IsNew)
                    {
                        if (i == 0)
                            h.ParentId = collRoot[0].Id;
                        else
                        {
                            //Поиск Guid родителя в импортируемой таблице
                            Guid parentGuid = (Guid)row["ParentGuid"];
                            //Поиск импортированого родителя в БД по Guid
                            h.ParentId = h.ExistsGuids(parentGuid);
                        }
                    }

                    h.Name = row["Name"] as string;
                    h.Code = row["Code"] as string;
                    h.Memo = row["Memo"] as string;
                    h.Save();
                }
            }
        }

        /// <summary>
        /// Получение данных аналитики из таблицы MISC по заданному значению поля MSC_TAG
        /// </summary>
        /// <param name="tag">Значение поля MSC_TAG</param>
        /// <returns>Данные</returns>
        private DataTable RefreshAnaliticByTag(string tag, int kindId = 262145)
        {
            if (_bindServers.Position != -1)
            {
                Branche currentServer = searchContainer.EditValue as Branche;
                //Branche currentServer = _bindServers.Current as Branche;
                //string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;", currentServer.ServerName, currentServer.DatabaseName);
                try
                {
                    using (SqlConnection cnn = currentServer.GetDatabaseConnection())
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            //
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT m.MSC_GUID AS Guid, m.MSC_ID AS DbSourceId, 0 as DatabaseId, m.MSC_NAME AS Name, m.MSC_MEMO as Memo, m.MSC_TAG AS Code, " + kindId + " AS KindId, 2 as StateId, 0 AS Flags  \n"
                                              + "  FROM dbo.MISC m WHERE m.MSC_NO =  \n"
                                              + "(SELECT MIN(msc_no) FROM dbo.MISC WHERE MSC_TAG = '" + tag + "') \n"
                                              + "AND m.MSC_TYPE=1";
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable tbl = new DataTable("Analitic.Analitics");
                            da.Fill(tbl);
                            DataColumn colChecked = new DataColumn("Imp", Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colChecked);
                            /*foreach (DataRow row in tbl.Rows)
                            {
                                row["Imp"] = true;
                            }*/
                            DataColumn colExists = new DataColumn("Exist", Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colExists);

                            // соединение с текущей базой и проверка глобальных идентификаторов
                            List<Guid> ListGuid = (from DataRow row in tbl.Rows select (Guid) row["Guid"]).ToList();

                            Dictionary<Guid, int> res = Workarea.Empty<Analitic>().ExistsGuids(ListGuid);

                            foreach (DataRow row in tbl.Rows)
                            {
                                bool exist = res[(Guid) row["Guid"]] != 0;
                                row["Exist"] = exist;
                                row["Imp"] = !exist;
                                row["DatabaseId"] = currentServer.Id;
                            }
                            return tbl;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        #region Импорт данных "Корреспонденты"
        private BarButtonItem btnImpAgGetData;
        private BarButtonItem btnImpAgUpdateData;
        private Controls.ControlImportGrid _ctlImpAgents;
        private void BuildControlImportAgent()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTAGENT"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTAGENT"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTAGENT"].BringToFront();
                btnImpAgGetData.Visibility = BarItemVisibility.Always;
                btnImpAgUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpAgents = new ControlImportGrid();
                _ctlImpAgents.Name = "IMPORTAGENT";
                _ctlImpAgents.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpAgents.View, "DEFAULT_LISTVIEWACCENTAGENTS");
                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpAgents);
                _ctlImpAgents.Dock = DockStyle.Fill;

                btnImpAgGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "AgImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAgGetData);
                btnImpAgGetData.ItemClick += delegate
                {
                    RefreshImpAgentAccentData();
                };

                btnImpAgUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "AgImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAgUpdateData);
                btnImpAgUpdateData.ItemClick += delegate
                {
                    UpdateImpAgentAccentData();
                };
            }
        }
        private void UpdateImpAgentAccentData()
        {
            ImportData(_ctlImpAgents.Grid.DataSource as DataTable);
        }
        private void RefreshImpAgentAccentData()
        {
            if (_bindServers.Position != -1)
            {
                Branche currentServer = searchContainer.EditValue as Branche;
                //Branche currentServer = _bindServers.Current as Branche;
                //string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;", currentServer.ServerName, currentServer.DatabaseName);
                try
                {
                    using (SqlConnection cnn = currentServer.GetDatabaseConnection())
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText ="SELECT a.AG_ID as Id, a.AG_GUID AS Guid, \n"
            + "1 AS DatabaseId, \n"
            + "a.AG_ID AS DbSourceId, \n"
            + "0 AS Flags, \n"
            + "2 AS StateId, \n"
            + "a.AG_NAME AS Name, \n"
            + "       cast(case when a.AG_TYPE = 5 THEN 196612 \n"
            + "            WHEN a.AG_TYPE = 1 THEN 196609 \n"
            + "            WHEN a.AG_TYPE = 2 THEN 196624 \n"
            + "            WHEN a.AG_TYPE = 3 OR a.AG_TYPE = 4 THEN 196610      \n"
            + "            else a.AG_TYPE END AS INT) AS KindId,  \n"
            + "CASE WHEN a.AG_TYPE = 1 THEN 'Предприятие'     \n"
            + "     WHEN a.AG_TYPE = 5 THEN  'Наше предприятие'    \n"
            + "     WHEN a.AG_TYPE = 2 THEN  'Склад'    \n"
            + "     WHEN a.AG_TYPE = 3 OR a.AG_TYPE = 4 THEN  'Сотрудник'    \n"
            + "     ELSE Cast(a.AG_TYPE AS NVARCHAR(50)) END AS KindName,    \n"
            + "a.AG_TAG AS Code, \n"
            + "a.AG_MEMO AS Memo,  \n"
            + "NULL AS CodeTax, \n"
            + "NULL AS NameFull, \n"
            + "AG_ADDRESS AS AddressLegal, \n"
            + "AG_ADDRESS AS AddressPhysical \n"

           
            //+ "       a.AG_VATNO AS VatNo, a.AG_REGNO AS RegNo,a.AG_CODE AS Okpo  \n"
            + "FROM dbo.AGENTS a WHERE a.AG_TYPE>0 ";
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable tbl = new DataTable("Contractor.Agents");
                            da.Fill(tbl);
                            DataColumn colChecked = new DataColumn("Imp", System.Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colChecked);
                            /*foreach (DataRow row in tbl.Rows)
                            {
                                row["Imp"] = true;
                            }*/
                            DataColumn colExists = new DataColumn("Exist", System.Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colExists);
                            
                            // соединение с текущей базой и проверка глобальных идентификаторов
                            List<Guid> ListGuid= (from DataRow row in tbl.Rows select (Guid) row["Guid"]).ToList();

                            Dictionary<Guid,int> res=Workarea.Empty<Agent>().ExistsGuids(ListGuid);

                            foreach (DataRow row in tbl.Rows)
                            {
                                bool exist = res[(Guid)row["Guid"]] != 0;
                                row["Exist"] = exist;
                                row["Imp"] = !exist;
                            }
                            _ctlImpAgents.Grid.DataSource = tbl;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        #endregion

        #region Импорт данных "Группы корреспондентов"
        private BarButtonItem btnImpAgentGroupsGetData;
        private BarButtonItem btnImpAgentGroupsUpdateData;
        private Controls.ControlImportGrid _ctlImpAgentGroups;
        private void BuildControlImportAgentGroups()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTAGENTGROUPS"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTAGENTGROUPS"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTAGENTGROUPS"].BringToFront();
                btnImpAgentGroupsGetData.Visibility = BarItemVisibility.Always;
                btnImpAgentGroupsUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpAgentGroups = new ControlImportGrid();
                _ctlImpAgentGroups.Name = "IMPORTAGENTGROUPS";
                _ctlImpAgentGroups.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpAgentGroups.View, "DEFAULT_LISTVIEWACCENTAGENTGROUPS");
                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpAgentGroups);
                _ctlImpAgentGroups.Dock = DockStyle.Fill;

                btnImpAgentGroupsGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "AgImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAgentGroupsGetData);
                btnImpAgentGroupsGetData.ItemClick += delegate
                {
                    RefreshImpAgentGroupsAccentData();
                };

                btnImpAgentGroupsUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "AgImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAgentGroupsUpdateData);
                btnImpAgentGroupsUpdateData.ItemClick += delegate
                {
                    UpdateImpAgentGroupsAccentData();
                };
            }
        }
        private void UpdateImpAgentGroupsAccentData()
        {
            ImportHierarchy(WhellKnownDbEntity.Agent, _ctlImpAgentGroups.Grid.DataSource as DataTable);
        }
        private void RefreshImpAgentGroupsAccentData()
        {
            if (_bindServers.Position != -1)
            {
                //_cmbReportserver.

                Branche currentServer = searchContainer.EditValue as Branche; //_bindServers.Current as Branche;
                //string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;", currentServer.ServerName, currentServer.DatabaseName);
                try
                {
                    using (SqlConnection cnn = currentServer.GetDatabaseConnection())
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT   a.AG_ID AS Id, \n"+
                                                       "a.AG_GUID AS [Guid], \n"+
                                                       "a.AG_NAME AS Name, \n"+
                                                       "a.AG_CODE AS Code,  \n"+
                                                       "a.AG_MEMO AS Memo, \n"+
                                                       "t.P0 AS ParentId, \n"+
                                                       "a2.AG_GUID AS ParentGuid, \n"+
                                                       "a2.AG_NAME AS ParentName, \n" +
                                                       "lv = CASE \n"+
                                                       "          WHEN t.P0 = 0 THEN 0 \n"+
                                                       "          WHEN t.P1 = 0 THEN 1 \n"+
                                                       "          WHEN t.P2 = 0 THEN 2 \n"+
                                                       "          WHEN t.P3 = 0 THEN 3 \n"+
                                                       "          WHEN t.P4 = 0 THEN 4 \n"+
                                                       "          WHEN t.P5 = 0 THEN 5 \n"+
                                                       "          WHEN t.P6 = 0 THEN 6 \n"+
                                                       "          WHEN t.P7 = 0 THEN 7 \n"+
                                                       "     END \n"+
                                                "FROM   dbo.AGENTS a INNER JOIN dbo.AG_TREE t ON  a.AG_ID = t.ID \n" +
                                                "      INNER JOIN dbo.AGENTS a2 ON t.p0=a2.AG_ID " +
                                                "WHERE a.AG_TYPE = 0 AND t.SHORTCUT=0";
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable tbl = new DataTable("Hierarchy.Hierarchies");
                            da.Fill(tbl);

                            DataColumn colChecked = new DataColumn("Imp", System.Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colChecked);
                            DataColumn colExists = new DataColumn("Exist", System.Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colExists);

                            //Соединение с текущей базой и проверка глобальных идентификаторов
                            List<Guid> ListGuid = (from DataRow row in tbl.Rows select (Guid)row["Guid"]).ToList();
                            Dictionary<Guid, int> res = Workarea.Empty<Hierarchy>().ExistsGuids(ListGuid);

                            //List<Hierarchy> coll = Workarea.GetCollection<Hierarchy>();

                            foreach (DataRow row in tbl.Rows)
                            {
                                bool exist = res[(Guid)row["Guid"]] != 0;
                                //bool exist = coll.Exists(s => ((ICoreObject)s).Guid == (Guid)row["Guid"]);
                                row["Exist"] = exist;
                                row["Imp"] = !exist;
                            }
                            _ctlImpAgentGroups.Grid.DataSource = tbl;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        #endregion

        #region Импорт данных "Товары"
        private BarButtonItem btnImpProdGetData;
        private BarButtonItem btnImpProdUpdateData;
        private Controls.ControlImportGrid _ctlImpProd;
        private void BuildControlImportProduct()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTPRODUCT"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTPRODUCT"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTPRODUCT"].BringToFront();
                btnImpProdGetData.Visibility = BarItemVisibility.Always;
                btnImpProdUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpProd = new ControlImportGrid();
                _ctlImpProd.Name = "IMPORTPRODUCT";
                _ctlImpProd.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpProd.View, "DEFAULT_LISTVIEWACCENTPRODUCT");

                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpProd);
                _ctlImpProd.Dock = DockStyle.Fill;

                btnImpProdGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "ProdImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpProdGetData);
                btnImpProdGetData.ItemClick += delegate
                {
                    RefreshImpProdAccentData();
                };

                btnImpProdUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "ProdImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpProdUpdateData);
                btnImpProdUpdateData.ItemClick += delegate
                {
                    UpdateImpProdAccentData();
                    RefreshImpProdAccentData();
                };
            }
        }
        private void UpdateImpProdAccentData()
        {
            ImportData(_ctlImpProd.Grid.DataSource as DataTable);
        }
        private void RefreshImpProdAccentData()
        {
            if (_bindServers.Position != -1)
            {
                Branche currentServer = searchContainer.EditValue as Branche;
                //Branche currentServer = _bindServers.Current as Branche;
                //string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;", currentServer.ServerName, currentServer.DatabaseName);
                try
                {
                    using (SqlConnection cnn = currentServer.GetDatabaseConnection())
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            //
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = " SELECT v.ENT_ID AS Id, v.ENT_GUID AS Guid, \n"
                               + " 1 AS DatabaseId,     \n"
                               + " v.ENT_ID AS DbSourceId, \n"
                               + " 0 AS Flags,     \n"
                               + " 2 AS StateId,     \n"
                               + " v.ENT_NAME AS Name,     \n"
                               + " cast(CASE WHEN v.ENT_TYPE = 1004 OR v.ENT_TYPE = 1003 OR v.ENT_TYPE = 1015 OR v.ENT_TYPE = 1016 OR v.ENT_TYPE = 1017 THEN 65537  \n"
                               + "           WHEN v.ENT_TYPE = 1001 THEN 65538  \n"
                               + "           when v.ENT_TYPE = 1014 then 65552  \n"
                               + "           WHEN v.ENT_TYPE = 1002 THEN 65540  \n"
                               + "           WHEN v.ENT_TYPE = 1005 THEN 65600  \n"
                               + "           WHEN v.ENT_TYPE = 1006 or v.ENT_TYPE = 1007 or v.ENT_TYPE = 1009 or v.ENT_TYPE = 1011 or v.ENT_TYPE = 1012 or v.ENT_TYPE = 1013 THEN 65544     \n"
                               + "           WHEN v.ENT_TYPE = 1008 THEN 65568  \n"
                               + "      ELSE v.ENT_TYPE END AS INT) AS KindId,  \n"
                               + " CASE WHEN v.ENT_TYPE = 1004 OR v.ENT_TYPE = 1003 OR v.ENT_TYPE = 1015 OR v.ENT_TYPE = 1016 OR v.ENT_TYPE = 1017 THEN 'Товар' \n"
                               + "           WHEN v.ENT_TYPE = 1001 THEN  'Деньги'   \n"
                               + "           when v.ENT_TYPE = 1014 then  'Тара'     \n"
                               + "           WHEN v.ENT_TYPE = 1002 THEN  'Услуги'   \n"
                               + "           WHEN v.ENT_TYPE = 1005 THEN  'МБП' \n"
                               + "           WHEN v.ENT_TYPE = 1006 or v.ENT_TYPE = 1007 or v.ENT_TYPE = 1009 or v.ENT_TYPE = 1011 or v.ENT_TYPE = 1012 or v.ENT_TYPE = 1013 THEN  'Основные средства'     \n"
                               + "           WHEN v.ENT_TYPE = 1008 THEN  'Автомобиль'     \n"
                               + "      ELSE Cast(v.ENT_TYPE AS NVARCHAR(50)) END AS KindName,     \n"
                               + " v.ENT_TAG AS Code,     \n"
                               + " v.ENT_MEMO AS Memo,     \n"
                               + " v.ENT_NOM AS Nomenclature,     \n"
                               + " v.ENT_ART AS Articul,     \n"
                               + " v.ENT_CAT AS Cataloque,     \n"
                               + " v.ENT_BAR AS Barcode,     \n"
                               + " u.UN_SHORT AS UnitShot, u.UN_GUID AS UnitGuid,     \n"
                               + " cast(null AS int) AS UnitId,  \n"
                               + " w.PRM_CY AS Weight,  \n"
                               + " TradeMark.PRM_LONG AS TradeMarkId, \n"
                               + " TradeMark.MSC_GUID AS TradeMarkGuid,     \n"
                               + " Brand.PRM_LONG AS BrandId, \n"
                               + " Brand.MSC_GUID AS BrandGuid,     \n"
                               //+ " ProductType.PRM_LONG AS ProductTypeId,     \n"
                               //+ " ProductType.MSC_GUID AS ProductTypeGuid, \n"
                               + " Pakc.PRM_LONG AS PakcTypeId,    \n"
                               + " Pakc.MSC_GUID AS PakcTypeGuid  \n"
                               + "   FROM dbo.ENTITIES v \n"
                               + "   LEFT JOIN dbo.UNITS u ON v.UN_ID = u.UN_ID     \n"
                               + "   LEFT JOIN   \n"
                               + "   (SELECT p.ENT_ID, p.PRM_CY FROM \n"
                               + "		  dbo.ENT_PARAMS p INNER JOIN dbo.ENT_PARAM_NAMES epn ON epn.PRM_ID = p.PRM_ID AND epn.PRM_NAME = 'Опт: Вес, кг') AS w ON v.ENT_ID = w.ENT_ID \n"
                               + "	LEFT JOIN  \n"
                               //ТТГ Опт: Товарная группа TradeMark 
                               + "   (SELECT p.ENT_ID, p.PRM_LONG, m.MSC_GUID from   \n"
                               + "          dbo.ENT_PARAMS p INNER JOIN dbo.ENT_PARAM_NAMES epn   \n"
                               + "              ON epn.PRM_ID = p.PRM_ID AND epn.PRM_NAME = 'Опт: Товарная группа'  \n"
                               + "          INNER JOIN dbo.MISC m ON p.PRM_LONG = m.MSC_ID) AS TradeMark ON v.ENT_ID = TradeMark.ENT_ID \n"
                               + "   LEFT JOIN  \n"
                               // ТТМ Опт: Торговая марка (бренд) Brend
                               + "   (SELECT p.ENT_ID, p.PRM_LONG, m.MSC_GUID from   \n"
                               + "          dbo.ENT_PARAMS p INNER JOIN dbo.ENT_PARAM_NAMES epn   \n"
                               + "              ON epn.PRM_ID = p.PRM_ID AND epn.PRM_NAME = 'Опт: Торговая марка (бренд)'  \n"
                               + "          INNER JOIN dbo.MISC m ON p.PRM_LONG = m.MSC_ID) AS Brand ON v.ENT_ID = Brand.ENT_ID			    \n"
                               + "   LEFT JOIN  \n"
                               //SalesType -- Удалить
                               //+ "   (SELECT p.ENT_ID, p.PRM_LONG, m.MSC_GUID from   \n"
                               //+ "          dbo.ENT_PARAMS p INNER JOIN dbo.ENT_PARAM_NAMES epn   \n"
                               //+ "              ON epn.PRM_ID = p.PRM_ID AND epn.PRM_NAME = '????'  \n"
                               //+ "          INNER JOIN dbo.MISC m ON p.PRM_LONG = m.MSC_ID) AS ProductType ON v.ENT_ID = ProductType.ENT_ID			    \n"
                               //+ "   LEFT JOIN  \n"
                               //ТВУ Опт: Вид упаковки
                               + "   (SELECT p.ENT_ID, p.PRM_LONG, m.MSC_GUID from   \n"
                               + "          dbo.ENT_PARAMS p INNER JOIN dbo.ENT_PARAM_NAMES epn   \n"
                               + "              ON epn.PRM_ID = p.PRM_ID AND epn.PRM_NAME = 'Опт: Вид упаковки'  \n"
                               + "          INNER JOIN dbo.MISC m ON p.PRM_LONG = m.MSC_ID) AS Pakc ON v.ENT_ID = Pakc.ENT_ID  \n"
                               + " WHERE v.ENT_TYPE>1";
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable tbl = new DataTable("Product.Products");
                            da.Fill(tbl);
                            DataColumn colChecked = new DataColumn("Imp", Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colChecked);
                            /*foreach (DataRow row in tbl.Rows)
                            {
                                row["Imp"] = true;
                            }*/
                            DataColumn colExists = new DataColumn("Exist", Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colExists);

                            // соединение с текущей базой и проверка глобальных идентификаторов
                            List<Guid> ListGuid = (from DataRow row in tbl.Rows select (Guid) row["Guid"]).ToList();

                            Dictionary<Guid, int> res = Workarea.Empty<Product>().ExistsGuids(ListGuid);
                            // проверить и установить правильный идентификатор единицы измерения по Guid
                            DataTable tblUnitGuids = tbl.DefaultView.ToTable(true, "UnitGuid");
                            for (int i = tblUnitGuids.Rows.Count-1; i >-1 ; i--)
                            {
                                if(tblUnitGuids.Rows[i].IsNull("UnitGuid"))
                                    tblUnitGuids.Rows.RemoveAt(i);
                            }
                            tblUnitGuids.Columns[0].ColumnName = "Guid";
                            Dictionary<Guid, int> resUnits = Workarea.Empty<Unit>().ExistsGuids(tblUnitGuids);
                            // проверить и установить правильный идентификатор единицы измерения по краткому наименованию
                            DataTable tblUnitCodes = tbl.DefaultView.ToTable(true, "UnitShot");
                            for (int i = tblUnitCodes.Rows.Count - 1; i > -1; i--)
                            {
                                if (tblUnitCodes.Rows[i].IsNull("UnitShot"))
                                    tblUnitCodes.Rows.RemoveAt(i);
                            }
                            tblUnitGuids.Columns[0].ColumnName = "Name";
                            string sqlFindUnitCodes = "SELECT Name, MIN(Id) AS Id from \n"
                                                   + "(SELECT v.Name,  CASE WHEN a.Id IS NOT NULL THEN a.Id ELSE 0 END AS Id \n"
                                                   + "from @Values v left join Core.Units a ON v.Name = a.Code) AS s \n"
                                                   + "GROUP BY Name ";

                            SqlCommand cmdFindUnitCodes = new SqlCommand(sqlFindUnitCodes, Workarea.GetDatabaseConnection());

                            cmdFindUnitCodes.Parameters.AddWithValue(GlobalSqlParamNames.Values, tblUnitCodes);
                            cmdFindUnitCodes.Parameters[0].SqlDbType = SqlDbType.Structured;
                            cmdFindUnitCodes.Parameters[0].TypeName = "dbo.KeyListName";

                            if (cmdFindUnitCodes.Connection.State != ConnectionState.Open)
                                cmdFindUnitCodes.Connection.Open();
                            SqlDataReader reader = cmdFindUnitCodes.ExecuteReader();
                            Dictionary<string, int> unitCodeHash = new Dictionary<string, int>();
                            while (reader.Read())
                            {
                                unitCodeHash.Add(reader.GetString(0), reader.GetInt32(1));
                            }

                            // TODO: проверить и установить правильный идентификатор "Тип упаковки" по Guid
                            // TODO: проверить и установить правильный идентификатор "Вид продукции"
                            // TODO: проверить и установить правильный идентификатор "Торговая марка"
                            // TODO: проверить и установить правильный идентификатор "Бренд"
                            
                            foreach (DataRow row in tbl.Rows)
                            {
                                bool exist = res[(Guid)row["Guid"]] != 0;
                                row["Exist"] = exist;
                                row["Imp"] = !exist;
                                row["DatabaseId"] = currentServer.Id; 
                                if(!row.IsNull("UnitGuid"))
                                {
                                    if (resUnits[(Guid) row["UnitGuid"]] != 0)
                                    {
                                        row["UnitId"] = resUnits[(Guid) row["UnitGuid"]];
                                    }
                                    else if (unitCodeHash[row["UnitShot"].ToString()] != 0)
                                    {
                                        row["UnitId"] = unitCodeHash[row["UnitShot"].ToString()];
                                    }
                                }
                            }
                            _ctlImpProd.Grid.DataSource = tbl;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        #endregion

        #region Импорт данных "Группы товаров"
        private BarButtonItem btnImpProdGroupsGetData;
        private BarButtonItem btnImpProdGroupsUpdateData;
        private Controls.ControlImportGrid _ctlImpProdGroups;
        private void BuildControlImportProductGroups()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTPRODUCTGROUPS"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTPRODUCTGROUPS"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTPRODUCTGROUPS"].BringToFront();
                btnImpProdGetData.Visibility = BarItemVisibility.Always;
                btnImpProdUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpProdGroups = new ControlImportGrid();
                _ctlImpProdGroups.Name = "IMPORTPRODUCTGROUPS";
                _ctlImpProdGroups.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpProdGroups.View, "DEFAULT_LISTVIEWACCENTPRODUCTGROUPS");

                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpProdGroups);
                _ctlImpProdGroups.Dock = DockStyle.Fill;

                btnImpProdGroupsGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "ProdGroupsImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpProdGroupsGetData);
                btnImpProdGroupsGetData.ItemClick += delegate
                {
                    RefreshImpProdGroupsAccentData();
                };

                btnImpProdGroupsUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "ProdGroupsImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpProdGroupsUpdateData);
                btnImpProdGroupsUpdateData.ItemClick += delegate
                {
                    UpdateImpProdGroupsAccentData();
                    RefreshImpProdGroupsAccentData();
                };
            }
        }
        private void UpdateImpProdGroupsAccentData()
        {
            ImportHierarchy(WhellKnownDbEntity.Product, _ctlImpProdGroups.Grid.DataSource as DataTable);           
        }
        private void RefreshImpProdGroupsAccentData()
        {
            if (_bindServers.Position != -1)
            {
                Branche currentServer = searchContainer.EditValue as Branche;
                //Branche currentServer = _bindServers.Current as Branche;
                //string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;", currentServer.ServerName, currentServer.DatabaseName);
                try
                {
                    using (SqlConnection cnn = currentServer.GetDatabaseConnection())
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT e.ENT_ID AS Id, \n"
                                           + "       e.ENT_GUID AS [Guid], \n"
                                           + "       e.ENT_NAME AS Name, \n"
                                           + "       e.ENT_CODE AS Code, \n"
                                           + "       e.ENT_MEMO AS Memo, \n"
                                           + "       t.P0 AS ParentId, \n"
                                           + "       e2.ENT_GUID AS ParentGuid, \n"
                                           + "       e2.ENT_NAME AS ParentName, \n"
                                           + "       lv = CASE  \n"
                                           + "                 WHEN t.P0 = 0 THEN 0 \n"
                                           + "                 WHEN t.P1 = 0 THEN 1 \n"
                                           + "                 WHEN t.P2 = 0 THEN 2 \n"
                                           + "                 WHEN t.P3 = 0 THEN 3 \n"
                                           + "                 WHEN t.P4 = 0 THEN 4 \n"
                                           + "                 WHEN t.P5 = 0 THEN 5 \n"
                                           + "                 WHEN t.P6 = 0 THEN 6 \n"
                                           + "                 WHEN t.P7 = 0 THEN 7 \n"
                                           + "            END \n"
                                           + "FROM   dbo.ENTITIES e INNER JOIN dbo.ENT_TREE t ON  e.ENT_ID = t.ID \n"
                                           + "       INNER JOIN dbo.ENTITIES e2 ON t.p0=e2.ENT_ID \n"
                                           + "WHERE  e.ENT_TYPE < 2 AND t.SHORTCUT = 0 and t.p0=7125\n";
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable tbl = new DataTable("Hierarchy.Hierarchies");
                            da.Fill(tbl);

                            DataColumn colChecked = new DataColumn("Imp", Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colChecked);
                            DataColumn colExists = new DataColumn("Exist", Type.GetType("System.Boolean"));
                            tbl.Columns.Add(colExists);

                            //Соединение с текущей базой и проверка глобальных идентификаторов
                            List<Guid> ListGuid = (from DataRow row in tbl.Rows select (Guid)row["Guid"]).ToList();
                            Dictionary<Guid, int> res = Workarea.Empty<Hierarchy>().ExistsGuids(ListGuid);

                            //List<Hierarchy> coll = Workarea.GetCollection<Hierarchy>();

                            foreach (DataRow row in tbl.Rows)
                            {
                                bool exist = res[(Guid)row["Guid"]] != 0;
                                //bool exist = coll.Exists(s => ((ICoreObject)s).Guid == (Guid)row["Guid"]);
                                row["Exist"] = exist;
                                row["Imp"] = !exist;
                            }
                            _ctlImpProdGroups.Grid.DataSource = tbl;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        #endregion

        #region Импорт данных "Бренды"
        private BarButtonItem btnImpAnaliticBrandsGetData;
        private BarButtonItem btnImpAnaliticBrandsUpdateData;
        private Controls.ControlImportGrid _ctlImpAnaliticBrands;
        private void BuildControlImportAnaliticBrands()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTANALITICBRAND"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICBRAND"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICBRAND"].BringToFront();
                btnImpAnaliticBrandsGetData.Visibility = BarItemVisibility.Always;
                btnImpAnaliticBrandsUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpAnaliticBrands = new ControlImportGrid();
                _ctlImpAnaliticBrands.Name = "IMPORTANALITICBRAND";
                _ctlImpAnaliticBrands.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpAnaliticBrands.View, "DEFAULT_LISTVIEWACCENTPRODUCTGROUPS");

                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpAnaliticBrands);
                _ctlImpAnaliticBrands.Dock = DockStyle.Fill;

                btnImpAnaliticBrandsGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "AnaliticBrandsImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAnaliticBrandsGetData);
                btnImpAnaliticBrandsGetData.ItemClick += delegate
                {
                    RefreshImpAnaliticBrandsAccentData();
                };

                btnImpAnaliticBrandsUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "AnaliticBrandsImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAnaliticBrandsUpdateData);
                btnImpAnaliticBrandsUpdateData.ItemClick += delegate
                {
                    UpdateImpAnaliticBrandsAccentData();
                    RefreshImpAnaliticBrandsAccentData();
                };
            }
        }
        private void UpdateImpAnaliticBrandsAccentData()
        {
            ImportData(_ctlImpAnaliticBrands.Grid.DataSource as DataTable, "BRAND", WhellKnownDbEntity.Analitic);
        }
        private void RefreshImpAnaliticBrandsAccentData()
        {
            _ctlImpAnaliticBrands.Grid.DataSource = RefreshAnaliticByTag("ТТМ", 262148);
        }

        #endregion

        #region Импорт данных "Вид продукции"
        private BarButtonItem btnImpProdTypeGetData;
        private BarButtonItem btnImpProdTypeUpdateData;
        private Controls.ControlImportGrid _ctlImpProdType;
        private void BuildControlImportProductType()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTANALITICPRODUCTTYPE"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICPRODUCTTYPE"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICPRODUCTTYPE"].BringToFront();
                btnImpProdTypeGetData.Visibility = BarItemVisibility.Always;
                btnImpProdTypeUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpProdType = new ControlImportGrid();
                _ctlImpProdType.Name = "IMPORTANALITICPRODUCTTYPE";
                _ctlImpProdType.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpProdType.View, "DEFAULT_LISTVIEWACCENTPRODUCTGROUPS");
                
                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpProdType);
                _ctlImpProdType.Dock = DockStyle.Fill;

                btnImpProdTypeGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "ProdTypeImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpProdTypeGetData);
                btnImpProdTypeGetData.ItemClick += delegate
                {
                    RefreshImpProdTypeAccentData();
                };

                btnImpProdTypeUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "ProdTypeImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpProdTypeUpdateData);
                btnImpProdTypeUpdateData.ItemClick += delegate
                {
                    UpdateImpProdTypeAccentData();
                    RefreshImpProdTypeAccentData();
                };
            }
        }
        private void UpdateImpProdTypeAccentData()
        {
            //DataTable tbl = _ctlImpProdType.Grid.DataSource as DataTable;
            //if (tbl == null)
            //    return;
            //DataTable tblSource = tbl.Select("Imp = true").CopyToDataTable();
            //tblSource.TableName = "Analitic.Analitics";
            //ExportImportData exportImportData = new ExportImportData { Workarea = Workarea };

            //using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            //{
            //    exportImportData.CopyTableToServer(tblSource, cnn);
            //    string sql = "MERGE Analitic.Analitics T USING #Analitics S ON T.Guid = S.Guid \n"
            //               + "WHEN MATCHED \n"
            //               + "    THEN UPDATE SET T.UserName = suser_sname(), T.DateModified = GETDATE(), T.[Name]=S.Name, \n"
            //               + "                    T.Memo = S.Memo, T.Code = S.Code \n"
            //               + "WHEN NOT MATCHED BY TARGET \n"
            //               + "    THEN INSERT(Guid,DatabaseId,DbSourceId,UserName,DateModified,Flags,StateId,Name,KindId,Code,Memo) \n"
            //               + "         VALUES(Guid,DatabaseId,DbSourceId,suser_sname(),GETDATE(),Flags,StateId,Name,KindId,Code,Memo);";
            //    SqlCommand cmd = cnn.CreateCommand();
            //    cmd.CommandText = sql;
            //    cmd.ExecuteNonQuery();
            //}
            ImportData(_ctlImpProdType.Grid.DataSource as DataTable, "PRODUCTTYPE", WhellKnownDbEntity.Analitic);
        }
        private void RefreshImpProdTypeAccentData()
        {
            _ctlImpProdType.Grid.DataSource = RefreshAnaliticByTag("SalesType");            
        }
        #endregion

        #region Импорт данных "Тип упаковки"
        private BarButtonItem btnImpPackTypeGetData;
        private BarButtonItem btnImpPackTypeUpdateData;
        private Controls.ControlImportGrid _ctlImpPackType;
        private void BuildControlImportPackType()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTANALITICPACKTYPE"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICPACKTYPE"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICPACKTYPE"].BringToFront();
                btnImpPackTypeGetData.Visibility = BarItemVisibility.Always;
                btnImpPackTypeUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpPackType = new ControlImportGrid();
                _ctlImpPackType.Name = "IMPORTANALITICPACKTYPE";
                _ctlImpPackType.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpPackType.View, "DEFAULT_LISTVIEWACCENTPRODUCTGROUPS");

                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpPackType);
                _ctlImpPackType.Dock = DockStyle.Fill;

                btnImpPackTypeGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "PackTypeImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpPackTypeGetData);
                btnImpPackTypeGetData.ItemClick += delegate
                {
                    RefreshImpPackTypeAccentData();
                };

                btnImpPackTypeUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "PackTypeImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpPackTypeUpdateData);
                btnImpPackTypeUpdateData.ItemClick += delegate
                {
                    UpdateImpPackTypeAccentData();
                    RefreshImpPackTypeAccentData();
                };
            }
        }
        private void UpdateImpPackTypeAccentData()
        {
            ImportData(_ctlImpPackType.Grid.DataSource as DataTable, "PACKTYPE", WhellKnownDbEntity.Analitic);
        }
        private void RefreshImpPackTypeAccentData()
        {
            _ctlImpPackType.Grid.DataSource = RefreshAnaliticByTag("ТВУ");
        }
        #endregion

        #region Импорт данных "Торговая группа"
        private BarButtonItem btnImpTradeMarkGetData;
        private BarButtonItem btnImpTradeMarkUpdateData;
        private Controls.ControlImportGrid _ctlImpTradeMark;
        private void BuildControlImportTradeMark()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTANALITICTRADEMARK"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICTRADEMARK"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICTRADEMARK"].BringToFront();
                btnImpTradeMarkGetData.Visibility = BarItemVisibility.Always;
                btnImpTradeMarkUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpTradeMark = new ControlImportGrid();
                _ctlImpTradeMark.Name = "IMPORTANALITICTRADEMARK";
                _ctlImpTradeMark.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpTradeMark.View, "DEFAULT_LISTVIEWACCENTPRODUCTGROUPS");

                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpTradeMark);
                _ctlImpTradeMark.Dock = DockStyle.Fill;

                btnImpTradeMarkGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "TradeMarkImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpTradeMarkGetData);
                btnImpTradeMarkGetData.ItemClick += delegate
                {
                    RefreshImpTradeMarkAccentData();
                };

                btnImpTradeMarkUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "TradeMarkImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpTradeMarkUpdateData);
                btnImpTradeMarkUpdateData.ItemClick += delegate
                {
                    UpdateImpTradeMarkAccentData();
                    RefreshImpTradeMarkAccentData();
                };
            }
        }
        private void UpdateImpTradeMarkAccentData()
        {
            ImportData(_ctlImpTradeMark.Grid.DataSource as DataTable, "TRADEMARK", WhellKnownDbEntity.Analitic);
        }
        private void RefreshImpTradeMarkAccentData()
        {
            _ctlImpTradeMark.Grid.DataSource = RefreshAnaliticByTag("ТТГ");
        }
        #endregion

        #region Импорт данных "Аналитика метраж"
        private BarButtonItem btnImpAnaliticMetricAreaGetData;
        private BarButtonItem btnImpAnaliticMetricAreaUpdateData;
        private Controls.ControlImportGrid _ctlImpAnaliticMetricArea;
        private void BuildControlImportAnaliticMetricArea()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTANALITICMETRICAREA"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICMETRICAREA"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICMETRICAREA"].BringToFront();
                btnImpAnaliticMetricAreaGetData.Visibility = BarItemVisibility.Always;
                btnImpAnaliticMetricAreaUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpAnaliticMetricArea = new ControlImportGrid();
                _ctlImpAnaliticMetricArea.Name = "IMPORTANALITICMETRICAREA";
                _ctlImpAnaliticMetricArea.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpAnaliticMetricArea.View, "DEFAULT_LISTVIEWACCENTPRODUCTGROUPS");

                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpAnaliticMetricArea);
                _ctlImpAnaliticMetricArea.Dock = DockStyle.Fill;

                btnImpAnaliticMetricAreaGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "AnaliticMetricAreaImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAnaliticMetricAreaGetData);
                btnImpAnaliticMetricAreaGetData.ItemClick += delegate
                {
                    RefreshImpAnaliticMetricAreaAccentData();
                };

                btnImpAnaliticMetricAreaUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "AnaliticMetricAreaImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAnaliticMetricAreaUpdateData);
                btnImpAnaliticMetricAreaUpdateData.ItemClick += delegate
                {
                    UpdateImpAnaliticMetricAreaAccentData();
                    RefreshImpAnaliticMetricAreaAccentData();
                };
            }
        }
        private void UpdateImpAnaliticMetricAreaAccentData()
        {
            ImportData(_ctlImpAnaliticMetricArea.Grid.DataSource as DataTable, "METRICAREA", WhellKnownDbEntity.Analitic);
        }
        private void RefreshImpAnaliticMetricAreaAccentData()
        {
            _ctlImpAnaliticMetricArea.Grid.DataSource = RefreshAnaliticByTag("КТМ");
        }
        #endregion

        #region Импорт данных "ТТ: Тип магазина"
        private BarButtonItem btnImpAnaliticTypeOutletGetData;
        private BarButtonItem btnImpAnaliticTypeOutletUpdateData;
        private Controls.ControlImportGrid _ctlImpAnaliticTypeOutlet;
        private void BuildControlImportAnaliticTypeOutlet()
        {
            if (_controlMain.splitContainer.Controls.ContainsKey("IMPORTANALITICTypeOutlet"))
            {
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICTypeOutlet"].Visible = true;
                _controlMain.splitContainer.Panel2.Controls["IMPORTANALITICTypeOutlet"].BringToFront();
                btnImpAnaliticTypeOutletGetData.Visibility = BarItemVisibility.Always;
                btnImpAnaliticTypeOutletUpdateData.Visibility = BarItemVisibility.Always;
            }
            else
            {
                _ctlImpAnaliticTypeOutlet = new ControlImportGrid();
                _ctlImpAnaliticTypeOutlet.Name = "IMPORTANALITICTypeOutlet";
                _ctlImpAnaliticTypeOutlet.View.OptionsView.ShowGroupedColumns = true;
                DataGridViewHelper.GenerateGridColumns(Workarea, _ctlImpAnaliticTypeOutlet.View, "DEFAULT_LISTVIEWACCENTPRODUCTGROUPS");

                _controlMain.splitContainer.Panel2.Controls.Add(_ctlImpAnaliticTypeOutlet);
                _ctlImpAnaliticTypeOutlet.Dock = DockStyle.Fill;

                btnImpAnaliticTypeOutletGetData = new BarButtonItem
                {
                    Caption = "Получение данных",
                    Name = "AnaliticTypeOutletImportGetData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAnaliticTypeOutletGetData);
                btnImpAnaliticTypeOutletGetData.ItemClick += delegate
                {
                    RefreshImpAnaliticTypeOutletAccentData();
                };

                btnImpAnaliticTypeOutletUpdateData = new BarButtonItem
                {
                    Caption = "Старт",
                    Name = "AnaliticTypeOutletImportUpdateData",
                    //Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.FORWARDGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnImpAnaliticTypeOutletUpdateData);
                btnImpAnaliticTypeOutletUpdateData.ItemClick += delegate
                {
                    UpdateImpAnaliticTypeOutletAccentData();
                    RefreshImpAnaliticTypeOutletAccentData();
                };
            }
        }
        private void UpdateImpAnaliticTypeOutletAccentData()
        {
            ImportData(_ctlImpAnaliticTypeOutlet.Grid.DataSource as DataTable, "SYSTEM_AGENTTYPEOUTLET", WhellKnownDbEntity.Analitic);
        }
        private void RefreshImpAnaliticTypeOutletAccentData()
        {
            _ctlImpAnaliticTypeOutlet.Grid.DataSource = RefreshAnaliticByTag("КТТ");
        }
        #endregion
    }
}
