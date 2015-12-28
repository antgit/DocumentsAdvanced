using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraExport;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Export;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства библиотеки
        /// </summary>
        /// <param name="item">Библиотека</param>
        /// <returns></returns>
        public static Form ShowProperty(this Library item)
        {
            InternalShowPropertyBase<Library> showPropertyBase = new InternalShowPropertyBase<Library>
                                                                     {
                                                                         SelectedItem = item,
                                                                         ControlBuilder =
                                                                             new BuildControlLibrary
                                                                                 {
                                                                                     SelectedItem = item
                                                                                 }
                                                                     };
            return showPropertyBase.ShowDialog();
        }
        #endregion

        /// <summary>
        /// Список библиотек
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Library BrowseList(this Library item)
        {
            List<Library> col = BrowseMultyList(item, item.Workarea, null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список библиотек
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<Library> BrowseList(this Library item, Predicate<Library> filter, List<Library> sourceCollection)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true);
        }
        internal static List<Library> BrowseMultyList(this Library item, Workarea wa, Predicate<Library> filter, List<Library> sourceCollection, bool allowMultySelect)
        {
            List<Library> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = wa.Empty<Library>().Entity.GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ListBrowserBaseObjects<Library> browserBaseObjects = new ListBrowserBaseObjects<Library>(wa, sourceCollection, filter, item, true, false, false, true)
                                                                     {Owner = frm};
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
            browserBaseObjects.ShowProperty += delegate(Library obj)
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
        private static void ShowReportTable(this Library item, ICoreObject toValue = null)
        {
            FormProperties frm = new FormProperties();
            CreateOpenWindowsButton(frm.Ribbon, item.Workarea);
            frm.Text = item.Name;
            frm.Ribbon.ApplicationIcon = ResourceImage.GetByCode(item.Workarea, ResourceImage.REPORT_X16);
            ControlList ctl = new ControlList();
            frm.clientPanel.Controls.Add(ctl);
            frm.MinimumSize = new Size(800, 600);
            ctl.Dock = DockStyle.Fill;

            //var res = item.Workarea.GetCollection<Product>().Select(val => new
            //{
            //    Номенклатура = val.Nomenclature,
            //    Код = val.Code,
            //    Наименование = val.Name,
            //    Группа = val.TradeMarkId == 0 ? null : val.TradeMark.Name,
            //    Бренд = val.BrandId == 0 ? null : val.Brand.Name
            //});

            if (item.ListView == null) return;
            DataSet ds = new DataSet(item.Name.Replace(" ", "_"));
            DataGridViewHelper.GenerateGridColumns(item.Workarea, ctl.View, item.ListId);
            LibraryReportParams libParams = LibraryReportParams.GetLibraryParams(item);
            if(toValue!=null)
            {
                
                using (SqlConnection cnn = item.Workarea.GetDatabaseConnection())
                {
                    try
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = item.ListView.SystemName;
                            if (toValue is Documents.DocumentSales || toValue is Documents.Document)
                            {
                                int docId = 0;
                                Documents.DocumentSales saleDoc = toValue as Documents.DocumentSales;
                                if(saleDoc!=null)
                                {
                                    docId = saleDoc.Id;
                                }
                                else if(saleDoc==null)
                                {
                                    Documents.Document doc = toValue as Documents.Document;
                                    if (doc != null)
                                        docId = doc.Id;
                                }
                                if (libParams.Params.Exists(f => f.Alias == "DocumentId"))
                                {
                                    cmd.Parameters.Add(GlobalSqlParamNames.DocumentId , SqlDbType.Int).Value = docId;
                                }
                            }
                            if(toValue is Agent)
                            {
                                int agentId = 0;
                                Agent agent = toValue as Agent;

                                if (agent != null)
                                {
                                    agentId = agent.Id;
                                }
                                
                                if (libParams.Params.Exists(f => f.Alias == "AgentId"))
                                {
                                    cmd.Parameters.Add(GlobalSqlParamNames.AgentId, SqlDbType.Int).Value = agentId;
                                }
                                if (libParams.Params.Exists(f => f.Alias == "Id"))
                                {
                                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = agentId;
                                }
                            }
                            if (toValue is Product)
                            {
                                int productId = 0;
                                Product product = toValue as Product;

                                if (product != null)
                                {
                                    productId = product.Id;
                                }

                                if (libParams.Params.Exists(f => f.Alias == "ProductId"))
                                {
                                    cmd.Parameters.Add(GlobalSqlParamNames.ProductId, SqlDbType.Int).Value = productId;
                                }
                                if (libParams.Params.Exists(f => f.Alias == "Id"))
                                {
                                    cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = productId;
                                }
                            }
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                        }
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }

                
                
            }
            //item.ListView.SystemName;


            ctl.Grid.DataSource = ds.Tables[0]; //res.ToList();
            ctl.View.OptionsMenu.ShowGroupSummaryEditorItem = true;
            ctl.View.OptionsView.ColumnAutoWidth = false;
            ctl.View.OptionsView.ShowFooter = true;
            ctl.View.OptionsView.ShowGroupedColumns = true;
            ctl.View.OptionsView.ShowGroupPanel = true;
            ctl.View.OptionsSelection.MultiSelect = true;
            BarButtonItem btnExportExcel = new BarButtonItem
            {
                Caption = "Экспорт Excel",
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.EXCEL_X32)
            };
            btnExportExcel.SuperTip = CreateSuperToolTip(btnExportExcel.Glyph, "Экспорт данных", "Экспорт данных в Microfost Excel");
            frm.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnExportExcel);
            btnExportExcel.ItemClick += delegate
            {
                //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //DevExpress.XtraGrid.Export.GridViewExportLink link = null;
                //DevExpress.XtraExport.ExportXlsxProvider provider = null; ;
                //saveFileDialog1.Filter = "Excel file|*.xlsx";
                //saveFileDialog1.RestoreDirectory = true;
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    provider = new DevExpress.XtraExport.ExportXlsxProvider(saveFileDialog1.FileName);
                //    link = ctl.View.CreateExportLink(provider) as DevExpress.XtraGrid.Export.GridViewExportLink;
                //    link.ExportCellsAsDisplayText = true;
                //    link.ExportTo(true);
                //    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                //}

                GridViewExportLink link = null;
                ExportXlsxProvider provider = null;
                string filepath = Path.GetTempFileName();
                filepath = filepath.Remove(filepath.LastIndexOf('.') + 1);
                filepath = String.Concat(filepath, "xlsx");
                provider = new ExportXlsxProvider(filepath);
                //link = ctl.View.CreateExportLink(provider);
                link = ctl.View.CreateExportLink(provider) as GridViewExportLink;
                link.ExportCellsAsDisplayText = true;
                link.Progress += delegate(object sender, ProgressEventArgs e)
                {
                    if (e.Phase == ExportPhase.Link)
                    {
                        frm.barEditItemProgress.Refresh();
                        frm.Ribbon.Update();
                    }
                };
                frm.barEditItemProgress.Visibility = BarItemVisibility.Always;
                frm.Ribbon.Update();

                Application.DoEvents();
                link.ExportTo(true);
                ProcessStartInfo startInfo =
                    new ProcessStartInfo("Excel.exe", String.Format("/r \"{0}\"", filepath));
                Process.Start(startInfo);
                frm.barEditItemProgress.Visibility = BarItemVisibility.Never;
            };

            BarButtonItem btnExportHtm = new BarButtonItem
            {
                Caption = "Экспорт Html",
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.FILEHTML_X32),
                
            };
            btnExportHtm.SuperTip = CreateSuperToolTip(btnExportHtm.Glyph, "Экспорт данных", "Экспорт данных в Html");
            //btnExportHtm.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.EXCEL32);
            frm.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnExportHtm);
            btnExportHtm.ItemClick += delegate
            {

                GridViewExportLink link = null;
                ExportHtmlProvider provider = null;
                string filepath = Path.GetTempFileName();
                filepath = filepath.Remove(filepath.LastIndexOf('.') + 1);
                filepath = String.Concat(filepath, "html");
                provider = new ExportHtmlProvider(filepath);
                link = ctl.View.CreateExportLink(provider) as GridViewExportLink;
                link.ExportCellsAsDisplayText = true;
                link.ExportTo(true);
                Process.Start(filepath);

                //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //DevExpress.XtraGrid.Export.GridViewExportLink link = null;
                //DevExpress.XtraExport.ExportHtmlProvider provider = null; ;
                //saveFileDialog1.Filter = "Html|*.html";
                //saveFileDialog1.RestoreDirectory = true;
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{
                //    provider = new DevExpress.XtraExport.ExportHtmlProvider(saveFileDialog1.FileName);
                //    link = ctl.View.CreateExportLink(provider) as DevExpress.XtraGrid.Export.GridViewExportLink;
                //    link.ExportCellsAsDisplayText = true;
                //    link.ExportTo(true);
                //    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                //}
            };
            ctl.View.ExpandAllGroups();
            frm.Show();
        }

        public static void ShowReportPrint(this Library item, ICoreObject toValue=null)
        {
            Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(item.GetAssembly());
            int idx = report.Dictionary.Databases.IndexOf("MainCnn");
            if (idx > -1)
                report.Dictionary.Databases.RemoveAt(idx);
            report.RegData("MainCnn", item.Workarea.GetDatabaseConnectionAdmin());

            DateTime? v = (DateTime?) item.Workarea.Period.Start;

            
            report["UserName"] = item.Workarea.CurrentUser.Name;
            report["ds"] = item.Workarea.Period.Start;
            report["de"] = item.Workarea.Period.End;
            report["UserId"] = item.Workarea.CurrentUser.Id;
            if(toValue!=null)
                report["Id"] = toValue.Id;
            report.Dictionary.Synchronize();
            report.Render();

            report.Show();
        }
        public static void ShowReportWeb(this Library item, ICoreObject toValue = null)
        {
            SystemParameter prm =
                item.Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("REPORTSTIMULSOFTPATH");
            SystemParameter prmFlash =
                item.Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("REPORTSTIMULSOFTFLASH");
            
            string navReportString = string.Format("{0}{1}{2}", prm.ValueString, "WebViewer.aspx?repId=", item.Id);
            if (prmFlash.ValueInt.HasValue && prmFlash.ValueInt.Value==1)
                navReportString = string.Format("{0}{1}{2}", prm.ValueString, "WebViewerFx.aspx?repId=", item.Id);
            Process.Start(navReportString);
        }
        //private static void ShowReportTable(this Library item, ICoreObject toValue = null)
        //{
        //    FormProperties frm = new FormProperties();
        //    CreateOpenWindowsButton(frm.Ribbon, item.Workarea);
        //    frm.Text = item.Name;
        //    frm.Ribbon.ApplicationIcon = ResourceImage.GetByCode(item.Workarea, ResourceImage.REPORT_X16);
        //    ControlList ctl = new ControlList();
        //    frm.clientPanel.Controls.Add(ctl);
        //    frm.MinimumSize = new Size(800, 600);
        //    ctl.Dock = DockStyle.Fill;
                
        //    var res = item.Workarea.GetCollection<Product>().Select(val => new { Номенклатура = val.Nomenclature, Код = val.Code, Наименование = val.Name, 
        //        Группа = val.TradeMarkId==0? null: val.TradeMark.Name, 
        //        Бренд = val.BrandId==0? null: val.Brand.Name });


        //    ctl.Grid.DataSource = res.ToList();
        //    ctl.View.OptionsMenu.ShowGroupSummaryEditorItem = true;
        //    ctl.View.OptionsView.ColumnAutoWidth = false;
        //    ctl.View.OptionsView.ShowFooter = true;
        //    ctl.View.OptionsView.ShowGroupedColumns = true;
        //    ctl.View.OptionsView.ShowGroupPanel = true;
        //    ctl.View.OptionsSelection.MultiSelect = true;
        //    BarButtonItem btnStructure = new BarButtonItem
        //                                     {
        //                                         Caption = "Экспорт Excel",
        //                                         RibbonStyle = RibbonItemStyles.Large,
        //                                         Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.EXCEL32)
        //                                     };
        //    btnStructure.SuperTip = CreateSuperToolTip(btnStructure.Glyph, "Экспорт данных",
        //        "Экспорт данных");
        //    frm.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnStructure);
        //    btnStructure.ItemClick += delegate
        //    {
        //        //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        //        //DevExpress.XtraGrid.Export.GridViewExportLink link = null;
        //        //DevExpress.XtraExport.ExportXlsxProvider provider = null; ;
        //        //saveFileDialog1.Filter = "Excel file|*.xlsx";
        //        //saveFileDialog1.RestoreDirectory = true;
        //        //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //        //{
        //        //    provider = new DevExpress.XtraExport.ExportXlsxProvider(saveFileDialog1.FileName);
        //        //    link = ctl.View.CreateExportLink(provider) as DevExpress.XtraGrid.Export.GridViewExportLink;
        //        //    link.ExportCellsAsDisplayText = true;
        //        //    link.ExportTo(true);
        //        //    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
        //        //}

        //        GridViewExportLink link = null;
        //        ExportXlsxProvider provider = null; 
        //        string filepath = Path.GetTempFileName();
        //        filepath = filepath.Remove(filepath.LastIndexOf('.') + 1);
        //        filepath = String.Concat(filepath, "xlsx");
        //        provider = new ExportXlsxProvider(filepath);
        //        //link = ctl.View.CreateExportLink(provider);
        //        link = ctl.View.CreateExportLink(provider) as GridViewExportLink;
        //        link.ExportCellsAsDisplayText = true;
        //        link.Progress += delegate(object sender, ProgressEventArgs e)
        //                             {
        //                                 if (e.Phase == ExportPhase.Link)
        //                                 {
        //                                     frm.barEditItemProgress.Refresh();
        //                                     frm.Ribbon.Update();
        //                                 }
        //                             };
        //        frm.barEditItemProgress.Visibility = BarItemVisibility.Always;
        //        frm.Ribbon.Update();

        //        Application.DoEvents();
        //        link.ExportTo(true);
        //        ProcessStartInfo startInfo =
        //            new ProcessStartInfo("Excel.exe", String.Format("/r \"{0}\"", filepath));
        //        Process.Start(startInfo);
        //        frm.barEditItemProgress.Visibility = BarItemVisibility.Never;
        //    };

        //    BarButtonItem btnExportHtm = new BarButtonItem
        //                                     {
        //                                         Caption = "Экспорт Html",
        //                                         RibbonStyle = RibbonItemStyles.Large,
        //                                         SuperTip =
        //                                             CreateSuperToolTip(btnStructure.Glyph, "Экспорт данных Html",
        //                                                                "Экспорт данных в Html")
        //                                     };
        //    //btnExportHtm.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.EXCEL32);
        //    frm.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnExportHtm);
        //    btnExportHtm.ItemClick += delegate
        //    {
                
        //        GridViewExportLink link = null;
        //        ExportHtmlProvider provider = null;
        //        string filepath = Path.GetTempFileName();
        //        filepath = filepath.Remove(filepath.LastIndexOf('.') + 1);
        //        filepath = String.Concat(filepath, "html");
        //        provider = new ExportHtmlProvider(filepath);
        //        link = ctl.View.CreateExportLink(provider) as GridViewExportLink;
        //        link.ExportCellsAsDisplayText = true;
        //        link.ExportTo(true);
        //        Process.Start(filepath);

        /// <summary>
        /// Показать отчет в SQL Reporting Service
        /// </summary>
        /// <remarks>
        /// Построение отчетов в службе SQL Reporting Service проводится в текщем браузере
        /// интернета или внутреннем окне просмотра. Следует отметить что построение отчета
        /// выполняется в асинхронном режиме. Для передачи параметров для отчета
        /// используются возможности службы SQL Reporting Service URL доступа. Передача
        /// параметров для службы отчетов задается в библиотеке отчетов. Наименования
        /// параметров для сушности является жестко заданным. 
        /// <para>Глобальные параметры</para>
        /// <para> </para>
        /// <list type="table">
        /// <listheader>
        /// <term>Ключ</term>
        /// <description>Значение</description></listheader>
        /// <item>
        /// <term>ds</term>
        /// <description>начало рабочего перода</description></item>
        /// <item>
        /// <term>de</term>
        /// <description>конец рабочего периода</description></item>
        /// <item>
        /// <term>UserName</term>
        /// <description>Имя текущего пользователя</description></item>
        /// <item>
        /// <term>UserId</term>
        /// <description>Идентификатор текущего пользователя</description></item></list> 
        /// <para>Параметры ICoreObject</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Ключ</term>
        /// <description>Наименование</description></listheader>
        /// <item>
        /// <term>id</term>
        /// <description>Идентификатор</description></item>
        /// <item>
        /// <term>StateId</term>
        /// <description>Идентификатор состояния </description></item>
        /// <item>
        /// <term>Guid</term>
        /// <description>Глобальный идентификатор </description></item>
        /// <item>
        /// <term>DatabaseId</term>
        /// <description>Идентификатор базы данных </description></item>
        /// <item>
        /// <term>EntityId</term>
        /// <description>Идентификатор типа </description></item></list>
        /// </remarks>
        /// <param name="item"></param>
        /// <param name="srv">Текущий сервер для построения отчетов</param>
        /// <param name="toValue">Сущность от которой запускается отчет</param>
        private static void ShowReportSqlReporting(this Library item, string srv, ICoreObject toValue = null)
        {
            //http://localhost/Reports/Pages/Report.aspx?ItemPath=
            //srv = 
            SystemParameter prm = item.Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>("REPORTSERVER_EXTERNALBROWSER");
            string navstring = string.Empty;
            if (toValue != null && !string.IsNullOrWhiteSpace(item.Code))
            {
                navstring = HttpUtility.UrlEncode(item.TypeUrl) + item.Code.Replace("{id}", toValue.Id.ToString());
            }
            else
                navstring = HttpUtility.UrlEncode(item.TypeUrl);
            if(srv.EndsWith("reportserver"))
                navstring = srv + "?" + navstring;
            else
                navstring = srv + "/Pages/Report.aspx?ItemPath=" + navstring;
            // установка параметров
            //&EmployeeID=1234
            LibraryReportParams libParams = LibraryReportParams.GetLibraryParams(item);
            if(libParams!=null)
            {
                // Глобальные параметры
                if (libParams.Params.Exists(f => f.Alias == "ds"))
                {
                    navstring = navstring + "&ds=" + item.Workarea.Period.Start;
                }
                if (libParams.Params.Exists(f => f.Alias == "de"))
                {
                    navstring = navstring + "&de=" + item.Workarea.Period.End;
                }
                // Параметры ICoreValue
                if(toValue !=null)
                {
                    if (libParams.Params.Exists(f => f.Alias == "Id"))
                    {
                        navstring = navstring + "&Id=" + toValue.Id;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "Guid"))
                    {
                        navstring = navstring + "&Guid=" + toValue.Guid;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "StateId"))
                    {
                        navstring = navstring + "&StateId=" + toValue.StateId;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "EntityId"))
                    {
                        navstring = navstring + "&EntityId=" + toValue.EntityId;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "Name"))
                    {
                        navstring = navstring + "&Name=" + toValue;
                    }
                }
                if(toValue!=null && toValue is Agent)
                {
                    if (libParams.Params.Exists(f => f.Alias == "AgentToId"))
                    {
                        navstring = navstring + "&AgentToId=" + toValue.Id;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "ClientId"))
                    {
                        navstring = navstring + "&ClientId=" + toValue.Id;
                    }   
                }
                if (toValue != null && toValue is Folder)
                {
                    if (libParams.Params.Exists(f => f.Alias == "FolderId"))
                    {
                        navstring = navstring + "&FolderId=" + toValue.Id;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "FolderCodeFind"))
                    {
                        navstring = navstring + "&FolderCodeFind=" + (toValue as Folder).CodeFind;
                    }
                }
                if (toValue != null && toValue is IBase)
                {
                    IBase valueIBase = toValue as IBase;
                    if (libParams.Params.Exists(f => f.Alias == "Code"))
                    {
                        navstring = navstring + "&Code=" + valueIBase.Code;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "KindId"))
                    {
                        navstring = navstring + "&KindId=" + valueIBase.KindId;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "KindValue"))
                    {
                        navstring = navstring + "&KindValue=" + valueIBase.KindValue;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "Memo"))
                    {
                        navstring = navstring + "&Memo=" + valueIBase.Memo;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "TemplateId"))
                    {
                        navstring = navstring + "&TemplateId=" + valueIBase.TemplateId;
                    }
                }
                if(toValue is Documents.Document)
                {
                    Documents.Document commonDoc = toValue as Documents.Document;
                    if (libParams.Params.Exists(f => f.Alias == "FolderId"))
                    {
                        navstring = navstring + "&FolderId=" + commonDoc.FolderId;
                    }
                }
                if(toValue is Documents.DocumentSales)
                {
                    Documents.DocumentSales saleDoc = toValue as Documents.DocumentSales;
                    if (libParams.Params.Exists(f => f.Alias == "DocumentId"))
                    {
                        navstring = navstring + "&DocumentId=" + saleDoc.Id;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "AgentToId"))
                    {
                        navstring = navstring + "&AgentToId=" + saleDoc.Document.AgentToId;
                    }
                    if (libParams.Params.Exists(f => f.Alias == "ClientId"))
                    {
                        navstring = navstring + "&ClientId=" + saleDoc.Document.AgentToId;
                    }
                    
                }
            }

            if (prm.ValueInt == 0)
            {
                FormWebViewer frm = new FormWebViewer();
                Bitmap img = ResourceImage.GetByCode(item.Workarea, ResourceImage.REPORT_X16);
                frm.Icon = Icon.FromHandle(img.GetHicon());
                CreateOpenWindowsButton(frm.Ribbon, item.Workarea);
                frm.Text = item.Name;
                frm.btnClose.Glyph = ResourceImage.GetSystemImage(ResourceImage.EXIT_X32);
                frm.btnNext.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.FORWARDGREEN_X32);
                frm.btnBack.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.BACKGREEN_X32);
                frm.Ribbon.ApplicationIcon = img;
                //string navstring = System.Web.HttpUtility.UrlEncode("/Документы2010/Управление продажами/Продажи за период");
                //navstring = "http://srv-devel/ReportServer?"+navstring;
                
                //navstring = "http://srv-devel/ReportServer?" + navstring;
                frm.Shown += delegate
                                 {
                                     frm.Browser.Navigate(navstring);
                                 };
                frm.Show();
            }
            else
            {
                System.Diagnostics.Process.Start(navstring);
            }
        }
        /// <summary>
        /// Инициализировать едактирование серверного отчета в построителе
        /// </summary>
        /// <param name="item">Библиотека</param>
        /// <param name="srv">URL срока сервера отчетов</param>
        public static void EditSqlReport(this Library item, string srv)
        {
            // для автоматического редактирования отчета
            //http://localhost/ReportServer/reportbuilder/reportbuilder_2_0_0_0.application?http://localhost:8080/reportserver/testfromUrl
            // для запуска построителя отчетов    
            //http://localhost/ReportServer/reportbuilder/reportbuilder_2_0_0_0.application

            string navstring = srv + HttpUtility.UrlEncode(item.TypeUrl);
            navstring = srv + "/reportbuilder/reportbuilder_3_0_0_0.application?"+ navstring;

            System.Diagnostics.Process.Start(navstring);
        }
        /// <summary>
        /// Показать отчет
        /// </summary>
        /// <param name="item">Библиотека</param>
        /// <param name="toValue">Объект для которого отображается отчет</param>
        /// <param name="srv">URL сервера отчетов</param>
        public static void ShowReport(this Library item, ICoreObject toValue = null, string srv=null)
        {
            if (item.KindValue == Library.KINDVALUE_REPSQL)
                item.ShowReportSqlReporting(srv, toValue);
            else if (item.KindValue == Library.KINDVALUE_REPTBL)
                item.ShowReportTable(toValue);
            else if (item.KindValue == Library.KINDVALUE_REPPRINT)
                item.ShowReportPrint(toValue);
            else if (item.KindValue == Library.KINDVALUE_WEBREPORT)
                item.ShowReportWeb(toValue);
        }
        // для автоматического редактирования отчета
        //http://localhost:8080/ReportServer/reportbuilder/reportbuilder_2_0_0_0.application?http://localhost:8080/reportserver/testfromUrl
        // для запуска построителя отчетов

        public static List<Library> BrowseContent(this Library item, Workarea wa = null)
        {
            ContentModuleLibrary module = new ContentModuleLibrary();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }
    }
}
