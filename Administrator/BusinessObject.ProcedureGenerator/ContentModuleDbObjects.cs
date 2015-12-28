using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Developer;
using BusinessObjects.ProcedureGenerator.Controls;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace BusinessObjects.ProcedureGenerator
{
    public class ContentModuleDbObjects : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        private string TYPENAME;
        BindingSource _source;
        private RibbonPageGroup _groupLinksAction;
        private ControlDbObjectsModule _controlMain;

        public ContentModuleDbObjects()
        {
            TYPENAME = "DBOBJECTS";
            Key = TYPENAME + "_MODULE";
            Caption = "Объекты базы данных";
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
                Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.DATABASE_X32);
            }
        }

        public string Key { get; set; }

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
            _controlMain = new ControlDbObjectsModule();
            _source = new BindingSource();
            List<DbObject> coll = Workarea.GetCollection<DbObject>();
            _source.DataSource = coll;
            _controlMain.View.OptionsDetail.EnableMasterViewMode = true;
            _controlMain.View.RowClick += ViewRowClick;
            _controlMain.ViewDetail.RowClick += ViewRowClick;
            _controlMain.View.KeyDown += ViewKeyDown;
            _controlMain.ViewDetail.KeyDown += ViewKeyDown;
            _controlMain.Grid.DataSource = _source;
        }

        void ViewKeyDown(object sender, KeyEventArgs e)
        {
            if(((GridView)_controlMain.Grid.FocusedView).FocusedRowHandle<0) return;
            if(e.KeyCode == Keys.Space || e.KeyCode== Keys.Enter)
                InvokeShowProperty();
        }

        void ViewRowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2 && ((GridView)_controlMain.Grid.FocusedView).FocusedRowHandle>-1)
                InvokeShowProperty();
        }

        private int ShowRemoveDialog<T>() where T :  BaseCoreObject, IBase, new()
        {
            if (_source.Current == null) return -1;
            int[] rows = ((GridView)_controlMain.Grid.FocusedView).GetSelectedRows();

            if (rows == null) return -1;
            int res = Extentions.ShowMessageChoice(Workarea,
                                                                           Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                                                           "Удаление",
                                                                           "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                                           Windows.Properties.Resources.STR_CHOICE_DEL);
            for (int j = rows.Length - 1; j >= 0; j--)
            {
                bool DocIsRowView = false;
                DataRowView rv = null;
                int i = rows[j];
                T op = _controlMain.Grid.FocusedView.GetRow(i) as T;

                if (op == null)
                {
                    rv = _controlMain.Grid.FocusedView.GetRow(i) as DataRowView;
                    if (rv != null)
                    {
                        int docid = (int)rv[GlobalPropertyNames.Id];
                        op = Workarea.GetObject<T>(docid);
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
                            _source.Remove(op);
                        }
                        catch (DatabaseException dbe)
                        {
                            Extentions.ShowMessageDatabaseExeption(Workarea,
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
                                _source.Remove(op);
                            else
                                _source.Remove(rv);
                            op.Delete();
                        }
                        catch (DatabaseException dbe)
                        {
                            Extentions.ShowMessageDatabaseExeption(Workarea,
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
            return res;
        }

        public void PerformShow()
        {
            if (_controlMain == null)
            {
                CreateControls();
            }
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = true;

            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksAction = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
            if (_groupLinksAction == null)
            {
                _groupLinksAction = new RibbonPageGroup {Name = TYPENAME + "_ACTIONLIST", Text = "Действия"};

                #region Создать

                BarButtonItem btnCreate = new BarButtonItem
                                              {
                                                  Caption = "Создать",
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  ButtonStyle = BarButtonStyle.DropDown,
                                                  ActAsDropDown = true,
                                                  Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                                                  DropDownEnabled = true
                                              };
                PopupMenu mnuCreate = new PopupMenu();
                mnuCreate.Ribbon = form.Ribbon;
                BarButtonItem btnCreateTable = new BarButtonItem();
                btnCreateTable.Caption = "Таблицу";
                btnCreateTable.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TABLE_X16);
                mnuCreate.AddItem(btnCreateTable);
                btnCreateTable.ItemClick += delegate
                                                {
                                                    DbObject table = new DbObject()
                                                                          {
                                                                              Workarea = Workarea,
                                                                              Schema = _source.Current == null ? string.Empty : ((DbObject) _source.Current).Schema
                                                                          };
                                                    table.ShowProperty();
                                                };
                BarButtonItem btnCreateColumn = new BarButtonItem();
                btnCreateColumn.Caption = "Столбец";
                btnCreateColumn.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.COLUMN_X16);
                mnuCreate.AddItem(btnCreateColumn);
                btnCreateColumn.ItemClick += delegate
                                                 {
                                                    if(_controlMain.View.FocusedRowHandle<0)
                                                    {
                                                        XtraMessageBox.Show("Выберите таблицу", Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                                                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        return;
                                                    }
                                                    DbObjectChild column = new DbObjectChild() { Workarea = Workarea,Owner = (DbObject)_source.Current};
                                                    column.ShowProperty();
                                                };

                btnCreate.DropDownControl = mnuCreate;

                _groupLinksAction.ItemLinks.Add(btnCreate);
                //btnCreate.ItemClick += delegate
                //{
                //    //CaptureTable tbl = _source.Current as CaptureTable;
                //    //if (string.IsNullOrEmpty(tbl.Instans))
                //    //    _dc.EnableTable(tbl);
                //};
                #endregion

                #region Изменить
                BarButtonItem btnEdit = new BarButtonItem
                                            {
                                                Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                _groupLinksAction.ItemLinks.Add(btnEdit);
                btnEdit.ItemClick += delegate
                                         {
                                             InvokeShowProperty();
                                         };
                #endregion

                #region Генератор
                BarButtonItem btnGenerate = new BarButtonItem
                                                {
                                                    Caption = "Генератор",
                                                    RibbonStyle = RibbonItemStyles.Large,
                                                    Glyph =
                                                        ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32)
                                                };
                _groupLinksAction.ItemLinks.Add(btnGenerate);
                btnGenerate.ItemClick += delegate
                {
                    ShowGenerator();
                };
                #endregion

                #region Удалить
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                              };
                _groupLinksAction.ItemLinks.Add(btnDelete);
                btnDelete.ItemClick += delegate
                                           {
                                               switch (_controlMain.Grid.FocusedView.Name)
                                               {
                                                   case "View":
                                                       ShowRemoveDialog<DbObject>();
                                                       break;
                                                   case "ViewDetail":
                                                       if(ShowRemoveDialog<DbObjectChild>()>0)
                                                           RefreshGrid();
                                                       break;
                                               } 
                                           }; 
                #endregion

                #region Обновить
                BarButtonItem btnRefresh = new BarButtonItem
                                               {
                                                   Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph =
                                                       ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                                               };
                _groupLinksAction.ItemLinks.Add(btnRefresh);
                btnRefresh.ItemClick += delegate
                                        {
                                            RefreshGrid();
                                        };
                #endregion

                #region Сравнить таблицы
                BarButtonItem btnCompareTables = new BarButtonItem
                                                    {
                                                        Caption = "Сравнить таблицы",
                                                        RibbonStyle = RibbonItemStyles.Large,
                                                        Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TABLE_X32)
                                                    };
                _groupLinksAction.ItemLinks.Add(btnCompareTables);
                btnCompareTables.ItemClick += delegate
                                                    {
                                                        ShowCompareDialog(0);
                                                        RefreshGrid();
                                                    };
                #endregion

                #region Сравнить столбцы
                BarButtonItem btnCompareColumns = new BarButtonItem
                                                    {
                                                        Caption = "Сравнить столбцы",
                                                        RibbonStyle = RibbonItemStyles.Large,
                                                        Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TABLE_X32)
                                                    };
                _groupLinksAction.ItemLinks.Add(btnCompareColumns);
                btnCompareColumns.ItemClick += delegate
                                                    {
                                                        ShowCompareDialog(1);
                                                        RefreshGrid();
                                                    };
                #endregion

                page.Groups.Add(_groupLinksAction);
            }
        }

        private void InvokeShowProperty()
        {
            if (_source.Current == null) return;
            switch (_controlMain.Grid.FocusedView.Name)
            {
                case "View":
                    {
                        DbObject dbObject = _source.Current as DbObject;
                        dbObject.ShowProperty();
                    }
                    break;
                case "ViewDetail":
                    {
                        DbObjectChild obj = ((GridView)_controlMain.Grid.FocusedView).GetFocusedRow() as DbObjectChild;
                        if (obj == null) return;
                        obj.ShowProperty(); 
                    }
                    break;
            }
        }

        /// <summary>
        /// Обновление Grid в соответствии с данными коллекции таблиц
        /// </summary>
        private void RefreshGrid()
        {
            List<DbObject> coll = Workarea.GetCollection<DbObject>();
            _source.DataSource = coll;
            _controlMain.Grid.DataSource = _source;
        }

        /// <summary>
        /// Показать диалог для сравнения таблиц/столбцов
        /// </summary>
        /// <param name="type">0-сравнение таблиц, 1-столбцов</param>
        private void ShowCompareDialog(int type)
        {
            FormProperties frm = new FormProperties
                                     {
                                         StartPosition = FormStartPosition.CenterParent,
                                         Width = 700,
                                         Height = 700,
                                         Text = type == 0 ? "Сравнение таблиц" : "Сравнение столбцов",
                                         btnSave =
                                             {
                                                 Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.RUNROUNDGREEN_X32),
                                                 Caption = "Синхронизация"
                                             },
                                     };
            frm.MinimumSize = frm.Size;

            ControlCompareGrid control = new ControlCompareGrid { Dock = DockStyle.Fill };
            frm.clientPanel.Controls.Add(control);

            //Подключение к БД и получение данных
            DataTable tbl = new DataTable();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                tbl = RefreshCompareGrid(type, control, cnn);

                #region Добавление кнопок
                RibbonPageGroup pageGroup=new RibbonPageGroup("Выборка");
                frm.ribbon.Pages[0].Groups.Add(pageGroup);

                BarButtonItem btnRefresh = new BarButtonItem
                {
                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                pageGroup.ItemLinks.Add(btnRefresh);
                btnRefresh.ItemClick += delegate
                                            {
                                                tbl = RefreshCompareGrid(type, control, cnn);
                                            };

                BarButtonItem btnCheckAll = new BarButtonItem
                {
                    Caption = "Выделить всё",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32)
                };
                pageGroup.ItemLinks.Add(btnCheckAll);
                btnCheckAll.ItemClick += delegate
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        row["Imp"] = true;
                    }
                };

                BarButtonItem btnUnCheck = new BarButtonItem
                {
                    Caption = "Снять выделение",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea,ResourceImage.DELETE_X32)
                };
                pageGroup.ItemLinks.Add(btnUnCheck);
                btnUnCheck.ItemClick += delegate
                {
                    foreach (DataRow row in tbl.Rows)
                    {
                        row["Imp"] = false;
                    }
                };
                #endregion

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //Для валидации
                    control.View.FocusedRowHandle--;
                    control.View.FocusedRowHandle++;

                    if (type == 1)
                    {
                        //Если выбран стобец какой-либо таблицы, то должны быть обновлены и остальные столбцы этой таблицы
                        //т.к. у них мог измениться порядковый номер
                        foreach (DataRow row in tbl.Select("Imp = true"))
                        {
                            foreach (DataRow row2 in tbl.Select(string.Format("Schema='{0}' AND Name='{1}'", row["Schema"], row["Name"])))
                            {
                                row2["Imp"] = true;
                            }
                        }
                    }

                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        DataTable value = new DataTable();
                        value.Columns.Add("Name", Type.GetType("System.String"));
                        foreach (DataRow row in tbl.Select("Imp=true"))
                        {
                            if(type==0)
                                value.Rows.Add(row["Schema"] + "." + row["Name"]);
                            else
                                value.Rows.Add(row["Schema"] + "." + row["Name"]+"."+row["ColumnName"]);
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = type == 0 ? "[Developer].[DbObjectsMerge]" : "[Developer].[DbObjectChildsMerge]";
                        cmd.Parameters.AddWithValue(GlobalSqlParamNames.Names, value);
                        //cmd.ExecuteNonQuery();

                        SqlDataAdapter adapter=new SqlDataAdapter(cmd);
                        DataTable res=new DataTable();
                        adapter.Fill(res);
                    }
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }

        /// <summary>
        /// Обновление грида диалога сравнения таблиц/столбцов
        /// </summary>
        /// <param name="type">>0-сравнение таблиц, 1-столбцов</param>
        /// <param name="control">Контрол грида</param>
        /// <param name="cnn">Соединение с БД</param>
        /// <returns></returns>
        private DataTable RefreshCompareGrid(int type, ControlCompareGrid control, SqlConnection cnn)
        {
            DataTable tbl = new DataTable();
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = type == 0 ? "[Developer].[DbObjectsCompare]" : "[Developer].[DbObjectChildsCompare]";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(tbl);
            }

            DataColumn colChecked = new DataColumn("Imp", System.Type.GetType("System.Boolean"));
            tbl.Columns.Add(colChecked);

            DataColumn colSepar = new DataColumn("Separ", System.Type.GetType("System.String"));
            //colSepar.Expression = "CASE Presence WHEN 0 THEN 'Присутствует' WHEN 1 THEN 'Только в схеме' WHEN 2 THEN 'Только в таблице' END";
            tbl.Columns.Add(colSepar);

            foreach (DataRow row in tbl.Rows)
            {
                row["Imp"] = (int)row["Presence"] != 0;

                switch((int)row["Presence"])
                {
                    case 0:
                        row["Separ"] = "Идентичные объекты";
                        break;
                    case 1:
                        row["Separ"] = "Только в схеме";
                        break;
                    case 2:
                        row["Separ"] = "Только в таблице";
                        break;
                }
            }

            tbl.Columns["Presence"].ReadOnly = true;
            control.Grid.DataSource = tbl;
            //control.View.OptionsView.ShowGroupedColumns = true;
            DataGridViewHelper.GenerateGridColumns(Workarea, control.View, type == 0 ? "DEFAULT_LISTVIEWCOMPARETABLES" : "DEFAULT_LISTVIEWCOMPARETABLECOLUMNS");

            return tbl;
        }

        public void PerformHide()
        {
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = false;
        }

        public Form Owner { get; set; }
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
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

        private void ShowGenerator()
        {
            FormProperties frm = new FormProperties {Size = new Size(700, 700), btnSaveClose = {Visibility = BarItemVisibility.Never}, StartPosition = FormStartPosition.CenterParent };

            ControlGenerator ctl = new ControlGenerator();
            ctl.checkedListBoxControl.Items.Add("Delete");
            ctl.checkedListBoxControl.Items.Add("Insert");
            ctl.checkedListBoxControl.Items.Add("Update");
            ctl.checkedListBoxControl.Items.Add("InsertUpdate");
            ctl.checkedListBoxControl.Items.Add("Load");
            ctl.checkedListBoxControl.Items.Add("LoadAll");
            ctl.checkedListBoxControl.Items.Add("FindIdByGuid");
            ctl.checkedListBoxControl.Items.Add("ExistsGuids");
            ctl.checkedListBoxControl.Items.Add("LoadList");
            ctl.checkedListBoxControl.Items.Add("FindBy");
            ctl.checkedListBoxControl.Items.Add("LoadByHierarchies");
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;

            #region Сохранить
            frm.btnSave.Visibility = BarItemVisibility.Always;
            
            frm.btnSave.ItemClick += delegate
                                        {
                                            frm.DialogResult = DialogResult.None;
                                            SaveFileDialog dialog = new SaveFileDialog
                                                                        {
                                                                            Filter = "SQL файлы (*.sql)|*.sql|Все файлы (*.*)|*.*",
                                                                            DefaultExt = "sql"
                                                                        };
                                            if(dialog.ShowDialog()== DialogResult.OK)
                                                System.IO.File.WriteAllText(dialog.FileName, ctl.richEditControl.Text);
                                        };
            #endregion
            
            #region Старт
            BarButtonItem btnStart = new BarButtonItem
                                         {
                                             Caption = "Старт",
                                             RibbonStyle = RibbonItemStyles.Large,
                                             Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.RUNROUNDGREEN_X32)
                                         };
            frm.ribbon.Pages[0].Groups[0].ItemLinks.Add(btnStart);
           
            btnStart.ItemClick += delegate
            {
                int[] selectedRows = _controlMain.View.GetSelectedRows();
                List<DbObject> selected = selectedRows.Select(i => _controlMain.View.GetDataSourceRowIndex(i)).Select(idx => _source[idx] as DbObject).ToList();
                if (selected.Count == 0) return;
                GeneratorDeleteById generatorDel = null;
                GeneratorProcedureInsert generatorInset = null;
                GeneratorProcedureUpdate generatorUpdate = null;
                GeneratorProcedureInsertUpdate generatorInsertUpdate = null;
                GeneratorProcedureLoad generatorLoad = null;
                GeneratorProcedureLoadAll generatorLoadAll = null;
                GeneratorFindIdByGuid generatorFindIdByGuid = null;
                GeneratorProcedureExistsGuid generatorProcedureExistsGuid = null;
                GeneratorProcedureLoadList generatorProcedureLoadList = null;
                GeneratorProcedureFindBy generatorProcedureFindBy = null;
                GeneratorProcedureLoadByHierarchies generatorProcedureLoadByHierarchies = null;
                // Удаление
                if (ctl.checkedListBoxControl.Items[0].CheckState == CheckState.Checked)
                    generatorDel = new GeneratorDeleteById { OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[1].CheckState == CheckState.Checked)
                    generatorInset = new GeneratorProcedureInsert { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[2].CheckState == CheckState.Checked)
                    generatorUpdate = new GeneratorProcedureUpdate { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[3].CheckState == CheckState.Checked)
                    generatorInsertUpdate = new GeneratorProcedureInsertUpdate { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[4].CheckState == CheckState.Checked)
                    generatorLoad = new GeneratorProcedureLoad { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[5].CheckState == CheckState.Checked)
                    generatorLoadAll = new GeneratorProcedureLoadAll { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[6].CheckState == CheckState.Checked)
                    generatorFindIdByGuid = new GeneratorFindIdByGuid { OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[7].CheckState == CheckState.Checked)
                    generatorProcedureExistsGuid = new GeneratorProcedureExistsGuid() { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[8].CheckState == CheckState.Checked)
                    generatorProcedureLoadList = new GeneratorProcedureLoadList() { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[9].CheckState == CheckState.Checked)
                    generatorProcedureFindBy = new GeneratorProcedureFindBy() { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };
                if (ctl.checkedListBoxControl.Items[10].CheckState == CheckState.Checked)
                    generatorProcedureLoadByHierarchies = new GeneratorProcedureLoadByHierarchies() { Workarea = Workarea, OptionCreate = ctl.chkCreate.Checked, OptionDrop = ctl.chkDrop.Checked };

                ctl.richEditControl.Text = string.Empty;
                const string blokSeparator = "\n--------------------------------------------------------\n";
                foreach (DbObject info in selected)
                {
                    
                    if (generatorDel != null)
                    {
                        generatorDel.TableName = info.Name;
                        generatorDel.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorDel.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorInset != null)
                    {
                        generatorInset.TableName = info.Name;
                        generatorInset.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorInset.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorUpdate != null)
                    {
                        generatorUpdate.TableName = info.Name;
                        generatorUpdate.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorUpdate.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorInsertUpdate != null)
                    {
                        generatorInsertUpdate.TableName = info.Name;
                        generatorInsertUpdate.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorInsertUpdate.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorLoad != null)
                    {
                        generatorLoad.TableName = info.Name;
                        generatorLoad.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorLoad.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorLoadAll != null)
                    {
                        generatorLoadAll.TableName = info.Name;
                        generatorLoadAll.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorLoadAll.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorFindIdByGuid != null)
                    {
                        generatorFindIdByGuid.TableName = info.Name;
                        generatorFindIdByGuid.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorFindIdByGuid.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorProcedureExistsGuid != null)
                    {
                        generatorProcedureExistsGuid.TableName = info.Name;
                        generatorProcedureExistsGuid.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorProcedureExistsGuid.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorProcedureLoadList != null)
                    {
                        generatorProcedureLoadList.TableName = info.Name;
                        generatorProcedureLoadList.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorProcedureLoadList.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorProcedureFindBy != null)
                    {
                        generatorProcedureFindBy.TableName = info.Name;
                        generatorProcedureFindBy.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorProcedureFindBy.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                    if (generatorProcedureLoadByHierarchies != null)
                    {
                        generatorProcedureLoadByHierarchies.TableName = info.Name;
                        generatorProcedureLoadByHierarchies.Schema = info.Schema;
                        ctl.richEditControl.Text += generatorProcedureLoadByHierarchies.Generate();
                        ctl.richEditControl.Text += blokSeparator;
                    }
                }


            };
            #endregion

            #region Выполнить
            BarButtonItem btnRun = new BarButtonItem
                                       {
                                           Caption = "SQL Server Management Studio",
                                           RibbonStyle = RibbonItemStyles.Large,
                                           Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.MANAGEMENTSTUDIO_X32)
                                       };
            frm.ribbon.Pages[0].Groups[0].ItemLinks.Add(btnRun);

            btnRun.ItemClick += delegate
                                {
                                    string filename = System.IO.Path.GetTempPath()+"tmp.sql";
                                    System.IO.File.WriteAllText(filename, ctl.richEditControl.Text);
                                    System.Diagnostics.Process.Start(filename);
                                };
            #endregion

            frm.ShowDialog();
        }
    }
}
