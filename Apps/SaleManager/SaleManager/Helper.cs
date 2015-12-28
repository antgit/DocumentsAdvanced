using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessObjects;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using BusinessObjects.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;

namespace SaleManager
{
    public enum Section
    {
        Default,
        Product,
        Clients,
        Supplyer,
        MyCompany,
        Store,
        Worker,

        DocProductIn,
        DocProductOut,
        DocMoneyIn,
        DocMoneyOut,
        DocPrice,
        DocNotDone,
        DocTrash,
        Help,
        Banks
    }

    public static class Helper
    {
        static Helper()
        {
            WebPages.Add("http://atlan.com.ua/documentsmini.aspx");
            WebPages.Add("http://atlan.com.ua/docmproducts.aspx");
            WebPages.Add("http://atlan.com.ua/docmagents.aspx");
            WebPages.Add("http://atlan.com.ua/docmlistuse.aspx");
            WebPages.Add("http://atlan.com.ua/docmsettings.aspx");
        }
        public const string FLD_SALES_IN_NDS = "FLD_SALES_IN_NDS"; //202
        public const string FLD_SALES_OUT_NDS = "FLD_SALES_OUT_NDS"; //201
        public const string FLD_SALES_OUT_RETURN_NDS = "FLD_SALES_OUT_RETURN_NDS"; //211
        public const string FLD_SALES_IN_RETURN_NDS = "FLD_SALES_IN_RETURN_NDS"; //212
        public const string FLD_PRICE = "FLD_PRICE"; //501
        public const string FLD_FINANCE_IN = "FLD_FINANCE_IN"; // 601
        public const string FLD_FINANCE_OUT = "FLD_FINANCE_OUT"; // 603

        public static Dictionary<string, string> FolderCodes;
        public static Dictionary<string, Folder> FolderValue;

        public static List<string> WebPages = new List<string>();

        public static int StoreChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для печатных форм
        /// </summary>
        public static int PrintFormChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для подразделений
        /// </summary>
        public static int DepatmentChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для сотрудников
        /// </summary>
        public static int WorkresChainId { get; set; }
        /// <summary>
        /// Идентификатор вида связи для торговых представителей
        /// </summary>
        public static int TradersChainId { get; set; }

        private static Workarea _wa;
        public static Section CurrentSection { get; set; }
        public static Workarea WA
        {
            get
            {
                if(_wa==null)
                {
                    Uid user = new Uid();
                    _wa = OpenDataBase(out user);
                    _wa.Period.Start = new DateTime(2010, 1, 1);
                    _wa.Period.End = new DateTime(2015, 1, 1);
                    FolderCodes = new Dictionary<string, string>();
                    FolderValue = new Dictionary<string, Folder>();
                    FolderCodes.Add(FLD_SALES_IN_NDS, "202");
                    FolderCodes.Add(FLD_SALES_OUT_NDS, "201");
                    FolderCodes.Add(FLD_SALES_OUT_RETURN_NDS, "211");

                    FolderCodes.Add(FLD_SALES_IN_RETURN_NDS, "212");
                    FolderCodes.Add(FLD_PRICE, "501");
                    FolderCodes.Add(FLD_FINANCE_OUT, "603");
                    FolderCodes.Add(FLD_FINANCE_IN, "601");

                    foreach (string key in FolderCodes.Keys)
                    {
                        string code = FolderCodes[key];
                        Folder v =  WA.Cashe.GetCasheData<Folder>().ItemCode<Folder>(code);
                        FolderValue.Add(key, v);
                    }

                    DepatmentChainId = _wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TREE && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                    PrintFormChainId = _wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.PRINTFORM && s.FromEntityId == (int)WhellKnownDbEntity.Library).Id;
                    WorkresChainId = _wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.WORKERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
                    TradersChainId = _wa.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TRADERS && s.FromEntityId == (int)WhellKnownDbEntity.Agent).Id;
            


                }
                return _wa;
            }
            set { _wa = value; }
        }

        public static Workarea OpenDataBase(out Uid user)
        {
            // Пользователь
            user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            // Соединяемся под интегрированной аутентификацией
            builder.IntegratedSecurity = true;
            // Текущий сервер - локальный, если необходимо меняем
            //builder.DataSource = ".";
            builder.DataSource = ".";
            // Имя базы данных
            builder.InitialCatalog = "Documents2011";
            builder.UserID = user.Name;
            builder.Password = user.Password;
            builder.IntegratedSecurity = user.AuthenticateKind == 1;

            Workarea WA = new Workarea();
            WA.ConnectionString = builder.ConnectionString;
            try
            {
                if (WA.LogOn(user.Name))
                {
                    return WA;
                }
            }
            catch (SqlException sqlEx)
            {
                // спец коды для определения необходимости замены пароля
                if ((sqlEx.Number == 18487) || (sqlEx.Number == 18488))
                {
                    // Получить новый пароль вызвав диалог ввода пароля с дополнительным сообщением...
                    int idx = Extentions.ShowMessageChoice(WA,
                                                           "Внимание", "Изменение пароля",
                                                           "Ваш пароль требуется изменить в соответсвии с требованиями учетной политики",
                                                           "Выполнить изменение парля самостоятельно|Я отказываюсь изменить свой пароль (работа с программой будет закончена)");
                    if (idx == 0)
                    {
                        string cnnString = builder.ConnectionString;
                        user = BusinessObjects.Windows.Extentions.ShowLogin(builder.UserID, builder.Password,
                                                                            builder.IntegratedSecurity);
                        // Установить новый пароль путем вызова SqlConnection.Open()
                        builder.Password = user.Password;
                        WA.ConnectionString = builder.ConnectionString;
                        System.Data.SqlClient.SqlConnection.ChangePassword(cnnString, builder.Password);
                    }
                }
                else
                    DevExpress.XtraEditors.XtraMessageBox.Show(sqlEx.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public static FormDocumentView GenerateDocView(IDocument value, Folder fld)
        {
            FormDocumentView frm = new FormDocumentView();
            frm.Ribbon.ApplicationButtonClick += delegate
                                                     {
                                                         frm.DialogResult = DialogResult.Cancel;
                                                         frm.Close();
                                                     };
            frm.Ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.DOCUMENTDONE_X16);
            
            frm.btnPrint.LargeGlyph = ResourceImage.GetSystemImage(ResourceImage.PRINT_X32);
            frm.btnPrint.SuperTip = UIHelper.CreateSuperToolTip(ResourceImage.GetByCode(WA, ResourceImage.PRINT_X32),
                                                                WA.Cashe.ResourceString(ResourceString.STR_DOC_PRINT,
                                                                                        1049),
                                                                WA.Cashe.ResourceString(
                                                                    ResourceString.STR_DOC_PRINT_TIP, 1049));

            frm.btnClose.LargeGlyph = ResourceImage.GetSystemImage(ResourceImage.EXIT_X32);
            frm.btnClose.SuperTip = UIHelper.CreateSuperToolTip(frm.btnClose.Glyph, WA.Cashe.ResourceString(ResourceString.STR_DOC_CLOSE_INFO, 1049),
                WA.Cashe.ResourceString(ResourceString.STR_DOC_CLOSE_TIP, 1049));
            frm.btnClose.ItemClick += delegate
                                          {
                                              frm.DialogResult = DialogResult.Cancel;
                                              frm.Close();
                                          };

            frm.btnSave.LargeGlyph = ResourceImage.GetSystemImage(ResourceImage.SAVE_X32);
            frm.btnSave.SuperTip = UIHelper.CreateSuperToolTip(frm.btnSave.Glyph, WA.Cashe.ResourceString(ResourceString.STR_DOC_SAVE_INFO, 1049),
                WA.Cashe.ResourceString(ResourceString.STR_DOC_SAVE_TIP, 1049));

            frm.btnDone.Glyph = ResourceImage.GetByCode(WA, ResourceImage.APPROVEGREEN_X32);
            frm.btnDone.SuperTip =
                UIHelper.CreateSuperToolTip(ResourceImage.GetByCode(WA, ResourceImage.APPROVEGREEN_X32),
                                            WA.Cashe.ResourceString(ResourceString.STR_DOC_SETDONE, 1049),
                                            WA.Cashe.ResourceString(ResourceString.STR_DOC_SETDONE_TIP, 1049));

            frm.btnNotDone.Glyph = ResourceImage.GetByCode(WA, ResourceImage.ROLLBACKRED_X32);
            
            frm.btnNotDone.SuperTip =
                UIHelper.CreateSuperToolTip(
                    ResourceImage.GetByCode(WA, ResourceImage.ROLLBACKRED_X32),
                    WA.Cashe.ResourceString(ResourceString.STR_DOC_SETNOTDONE, 1049),
                    WA.Cashe.ResourceString(ResourceString.STR_DOC_SETNOTDONE_TIP, 1049));

            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.ViewFrom, "DEFAULT_LOOKUPAGENT");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.ViewTo, "DEFAULT_LOOKUPAGENT");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.ViewSuperviser, "DEFAULT_LOOKUPAGENT");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.ViewTrader, "DEFAULT_LOOKUPAGENT");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.ViewPrice, "DEFAULT_LOOKUP_NAME");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.ViewBankFrom, "DEFAULT_LISTVIEWAGENTBANKACCOUNT");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.ViewBankTo, "DEFAULT_LISTVIEWAGENTBANKACCOUNT");


            Document template = WA.Cashe.GetCasheData<Document>().Item(fld.DocumentId);

            if (value.Id == 0)
            {
                value.Document = new Document
                {
                    Workarea = WA,
                    TemplateId = template.Id,
                    FolderId = template.FolderId,
                    ProjectItemId = template.ProjectItemId,
                    StateId = template.StateId,
                    Name = template.Name,
                    KindId = template.KindId,
                    AgentFromId = template.AgentFromId,
                    AgentDepartmentFromId = template.AgentDepartmentFromId,
                    AgentToId = template.AgentToId,
                    AgentDepartmentToId = template.AgentDepartmentToId,
                    CurrencyId = template.CurrencyId,
                    MyCompanyId = template.MyCompanyId
                };
                List<Taxe> tmlTaxes = template.Taxes();
                if (tmlTaxes.Count > 0)
                {
                    if (value.Document.Taxes().Count == 0)
                    {
                        foreach (Taxe tmlTaxesValue in tmlTaxes)
                        {
                            Taxe docTax = new Taxe
                            {
                                Workarea = WA,
                                DocumentId = value.Id,
                                TaxId = tmlTaxesValue.TaxId
                            };
                            value.Document.Taxes().Add(docTax);
                        }
                    }
                }
            }

            frm.ControlMain.dtDate.EditValue = value.Document.Date;
            frm.ControlMain.txtNumber.Text = value.Document.Number;
            frm.ControlMain.txtMemo.Text = value.Document.Memo;
            frm.Text = value.Document.Name;
            return frm;
        }

        /// <summary>
        /// Подготовка входящего документа
        /// </summary>
        /// <returns></returns>
        private static void DocumentIn(FormDocumentView frm, IDocument value, Folder fld)
        {
            frm.ControlMain.CollectionAgentTo = Agent.GetChainSourceList(WA, (int)value.Document.AgentToId, DepatmentChainId);
            frm.ControlMain.bindAgentTo.DataSource = frm.ControlMain.CollectionAgentTo;
            if (value.Document.AgentDepartmentToId != 0)
            {
                frm.ControlMain.bindAgentTo.Add(WA.Cashe.GetCasheData<Agent>().Item(value.Document.AgentDepartmentToId));
            }
            frm.ControlMain.cmbTo.EditValue = value.Document.AgentDepartmentToId;
            frm.ControlMain.cmbTo.ButtonClick +=
                delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 1)
                    {
                        int idAgTo = (int)frm.ControlMain.cmbTo.EditValue;
                        if (idAgTo != 0)
                        {
                            WA.Cashe.GetCasheData<Agent>().Item(idAgTo).ShowWizard();
                        }
                    }
                };

            if (value.Document.AgentFromId != 0)
            {
                frm.ControlMain.bindAgentFrom.Add(WA.Cashe.GetCasheData<Agent>().Item(value.Document.AgentFromId));
            }

            frm.ControlMain.cmbFrom.QueryPopUp += delegate
            {
                if (frm.ControlMain.bindAgentFrom.Count < 2)
                {
                    frm.ControlMain.CollectionAgentFrom =
                        WA.GetCollection<Agent>().Where(
                            s =>
                            (s.KindValue & Agent.KINDVALUE_MYCOMPANY) !=
                            Agent.KINDVALUE_MYCOMPANY &&
                            s.IsCompany).ToList();
                    frm.ControlMain.bindAgentFrom.DataSource =
                        frm.ControlMain.CollectionAgentFrom;
                }
            };

            frm.ControlMain.cmbFrom.EditValue = value.Document.AgentFromId;

            frm.ControlMain.cmbFrom.ButtonClick +=
                delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 1)
                    {
                        int idAgFrom = (int)frm.ControlMain.cmbFrom.EditValue;
                        if (idAgFrom != 0)
                        {
                            WA.Cashe.GetCasheData<Agent>().Item(idAgFrom).ShowWizard();
                        }
                    }
                };

            
        }
        /// <summary>
        /// Подготовка исходящего документа
        /// </summary>
        /// <returns></returns>
        private static void DocumentOut(FormDocumentView frm, IDocument value, Folder fld)
        {
            frm.ControlMain.CollectionAgentFrom = Agent.GetChainSourceList(WA, (int)value.Document.AgentFromId, DepatmentChainId);
            frm.ControlMain.bindAgentFrom.DataSource = frm.ControlMain.CollectionAgentFrom;
            if (value.Document.AgentDepartmentFromId != 0)
            {
                if (!frm.ControlMain.CollectionAgentFrom.Exists(s => s.Id == value.Document.AgentDepartmentFromId))
                    frm.ControlMain.bindAgentFrom.Add(WA.Cashe.GetCasheData<Agent>().Item(value.Document.AgentDepartmentFromId));
            }
            frm.ControlMain.cmbFrom.EditValue = value.Document.AgentDepartmentFromId;
            frm.ControlMain.cmbFrom.ButtonClick +=
                delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                    {
                        if(e.Button.Index==1)
                        {
                            int idAgFrom = (int) frm.ControlMain.cmbFrom.EditValue;
                            if(idAgFrom!=0)
                            {
                                WA.Cashe.GetCasheData<Agent>().Item(idAgFrom).ShowWizard();
                            }
                        }
                    };

            if (value.Document.AgentToId != 0)
            {
                frm.ControlMain.bindAgentTo.Add(WA.Cashe.GetCasheData<Agent>().Item(value.Document.AgentToId));
            }

            frm.ControlMain.cmbTo.QueryPopUp += delegate
                                                      {
                                                          if (frm.ControlMain.bindAgentTo.Count < 2)
                                                          {
                                                              frm.ControlMain.CollectionAgentTo =
                                                                  WA.GetCollection<Agent>().Where(
                                                                      s =>
                                                                      (s.KindValue & Agent.KINDVALUE_MYCOMPANY) !=
                                                                      Agent.KINDVALUE_MYCOMPANY &&
                                                                      s.IsCompany).ToList();
                                                              frm.ControlMain.bindAgentTo.DataSource =
                                                                  frm.ControlMain.CollectionAgentTo;
                                                          }
                                                      };

            frm.ControlMain.cmbTo.EditValue = value.Document.AgentToId;

            frm.ControlMain.cmbTo.ButtonClick +=
                delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    if (e.Button.Index == 1)
                    {
                        int idAgTo = (int)frm.ControlMain.cmbTo.EditValue;
                        if (idAgTo != 0)
                        {
                            WA.Cashe.GetCasheData<Agent>().Item(idAgTo).ShowWizard();
                        }
                    }
                };

            
            
        }

        

        /// <summary>
        /// Показать документ "Приход товара"
        /// </summary>
        /// <returns></returns>
        public static FormDocumentView ShowDocumentIn(DocumentSales value, Folder fld)
        {
            FormDocumentView frm = GenerateDocView(value, fld);

            if (value.Id == 0)
            {
                DocumentSales salesTemplate = WA.Cashe.GetCasheData<DocumentSales>().Item(fld.DocumentId);

                if (salesTemplate != null)
                {
                    // Установить вид цены
                    if (value.PrcNameId == 0)
                        value.PrcNameId = salesTemplate.PrcNameId;
                    if (value.ManagerId == 0)
                        value.ManagerId = salesTemplate.ManagerId;
                    if (value.SupervisorId == 0)
                        value.SupervisorId = salesTemplate.SupervisorId;
                    if (value.StoreToId == 0)
                        value.StoreToId = salesTemplate.StoreToId;
                    if (value.StoreFromId == 0)
                        value.StoreFromId = salesTemplate.StoreFromId;
                    if (value.ReturnReasonId == 0)
                        value.ReturnReasonId = salesTemplate.ReturnReasonId;
                    if (value.DeliveryId == 0)
                        value.DeliveryId = salesTemplate.DeliveryId;

                    if (salesTemplate.Details.Count > 0 && value.Details.Count == 0)
                    {
                        foreach (DocumentDetailSale jrnTml in salesTemplate.Details)
                        {
                            DocumentDetailSale r = value.NewRow();
                            r.ProductId = jrnTml.ProductId;
                            r.Price = jrnTml.Price;
                            r.Qty = jrnTml.Qty;
                            r.UnitId = jrnTml.UnitId;
                            r.FUnitId = jrnTml.FUnitId;
                            r.FQty = jrnTml.FQty;
                        }
                    }
                }
            }

            DocumentIn(frm, value, fld);

            if (value.PrcNameId != 0)
            {
                frm.ControlMain.CollectionPrice.Add(WA.Cashe.GetCasheData<PriceName>().Item(value.PrcNameId));
            }
            frm.ControlMain.cmbPrice.QueryPopUp += delegate
            {
                if (frm.ControlMain.bindPrice.Count < 2)
                {
                    frm.ControlMain.CollectionPrice =
                        WA.GetCollection<PriceName>().Where(
                            s => s.KindId == PriceName.KINDID_PRICENAME).
                            ToList();
                    frm.ControlMain.bindPrice.DataSource =
                        frm.ControlMain.CollectionPrice;
                }
            };

            if (value.ManagerId != 0)
            {
                frm.ControlMain.CollectionTrader.Add(value.Manager);
            }
            frm.ControlMain.cmbTrader.EditValue = value.ManagerId;
            frm.ControlMain.cmbTrader.QueryPopUp += delegate
            {
                if (frm.ControlMain.bindTrader.Count < 2)
                {
                    frm.ControlMain.CollectionTrader =
                        Agent.GetChainSourceList(WA,
                                                 (int)
                                                 frm.ControlMain.cmbTo.
                                                     EditValue,
                                                 TradersChainId);
                    frm.ControlMain.bindTrader.DataSource =
                        frm.ControlMain.CollectionTrader;
                }
            };

            if (value.SupervisorId != 0)
            {
                frm.ControlMain.CollectionSuperviser.Add(value.Supervisor);
            }
            frm.ControlMain.cmbSuperviser.EditValue = value.SupervisorId;
            frm.ControlMain.cmbSuperviser.QueryPopUp += delegate
            {
                if (frm.ControlMain.bindSuperviser.Count < 2)
                {
                    frm.ControlMain.CollectionSuperviser = Agent.GetChainSourceList(WA,
                                                                                    (int)
                                                                                    frm.ControlMain.cmbTo.EditValue,
                                                                                    TradersChainId);

                    frm.ControlMain.bindSuperviser.DataSource = frm.ControlMain.CollectionSuperviser;
                }
            };
            frm.Show();
            return frm;
        }
        /// <summary>
        /// Показать документ "Расход товара"
        /// </summary>
        /// <returns></returns>
        public static FormDocumentView ShowDocumentOut(DocumentSales value, Folder fld)
        {
            FormDocumentView frm = GenerateDocView(value, fld);

            #region Если это новый документ
            if (value.Id == 0)
            {
                DocumentSales salesTemplate = WA.Cashe.GetCasheData<DocumentSales>().Item(fld.DocumentId);

                if (salesTemplate != null)
                {
                    // Установить вид цены
                    if (value.PrcNameId == 0)
                        value.PrcNameId = salesTemplate.PrcNameId;
                    if (value.ManagerId == 0)
                        value.ManagerId = salesTemplate.ManagerId;
                    if (value.SupervisorId == 0)
                        value.SupervisorId = salesTemplate.SupervisorId;
                    if (value.StoreToId == 0)
                        value.StoreToId = salesTemplate.StoreToId;
                    if (value.StoreFromId == 0)
                        value.StoreFromId = salesTemplate.StoreFromId;
                    if (value.ReturnReasonId == 0)
                        value.ReturnReasonId = salesTemplate.ReturnReasonId;
                    if (value.DeliveryId == 0)
                        value.DeliveryId = salesTemplate.DeliveryId;

                    value.Kind = salesTemplate.Kind;
                    if (salesTemplate.Details.Count > 0 && value.Details.Count == 0)
                    {
                        foreach (DocumentDetailSale jrnTml in salesTemplate.Details)
                        {
                            DocumentDetailSale r = value.NewRow();
                            r.ProductId = jrnTml.ProductId;
                            r.Price = jrnTml.Price;
                            r.Qty = jrnTml.Qty;
                            r.UnitId = jrnTml.UnitId;
                            r.FUnitId = jrnTml.FUnitId;
                            r.FQty = jrnTml.FQty;
                        }
                    }
                }
            } 
            #endregion

            DocumentOut(frm, value, fld);

            if (value.PrcNameId != 0)
            {
                frm.ControlMain.bindPrice.Add(WA.Cashe.GetCasheData<PriceName>().Item(value.PrcNameId));
            }
            frm.ControlMain.cmbPrice.QueryPopUp += delegate
                                                       {
                                                           if (frm.ControlMain.bindPrice.Count < 2)
                                                           {
                                                               frm.ControlMain.CollectionPrice =
                                                                   WA.GetCollection<PriceName>().Where(
                                                                       s => s.KindId == PriceName.KINDID_PRICENAME).
                                                                       ToList();
                                                               frm.ControlMain.bindPrice.DataSource =
                                                                   frm.ControlMain.CollectionPrice;
                                                           }
                                                       };

            frm.ControlMain.cmbPrice.EditValue = value.PrcNameId;
            if (value.ManagerId != 0)
            {
                frm.ControlMain.bindTrader.Add(value.Manager);
            }
            frm.ControlMain.cmbTrader.EditValue = value.ManagerId;
            frm.ControlMain.cmbTrader.QueryPopUp += delegate
                                                       {
                                                           if (frm.ControlMain.bindTrader.Count < 2)
                                                           {
                                                               frm.ControlMain.CollectionTrader =
                                                                   Agent.GetChainSourceList(WA,
                                                                                            (int)
                                                                                            frm.ControlMain.cmbFrom.
                                                                                                EditValue,
                                                                                            TradersChainId);
                                                               frm.ControlMain.bindTrader.DataSource =
                                                                   frm.ControlMain.CollectionTrader;
                                                           }
                                                       };

            if (value.SupervisorId != 0)
            {
                frm.ControlMain.bindSuperviser.Add(value.Supervisor);
            }
            frm.ControlMain.cmbSuperviser.EditValue = value.SupervisorId;
            frm.ControlMain.cmbSuperviser.QueryPopUp += delegate
            {
                if (frm.ControlMain.bindSuperviser.Count < 2)
                {
                    frm.ControlMain.CollectionSuperviser = Agent.GetChainSourceList(WA,
                                                                                    (int)
                                                                                    frm.ControlMain.cmbFrom.EditValue,
                                                                                    TradersChainId);

                    frm.ControlMain.bindSuperviser.DataSource = frm.ControlMain.CollectionSuperviser;
                }
            };

            frm.ControlMain.BindSourceDetails.DataSource = value.Details;
            
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.editNom.View, "DEFAULT_LISTVIEWPRODUCT");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.editName.View, "DEFAULT_LISTVIEWPRODUCT");

            List<Product> collProducts;
            collProducts = WA.GetCollection<Product>().Where(s => s.KindValue == Product.KINDVALUE_PRODUCT).ToList();
            frm.ControlMain.bindProduct.DataSource = collProducts;

            frm.ControlMain.editNom.DataSource = frm.ControlMain.bindProduct;
            frm.ControlMain.editName.DataSource = frm.ControlMain.bindProduct;

            frm.ControlMain.editName.ButtonClick += delegate(object sender, ButtonPressedEventArgs e)
                                                        {
                                                            if(e.Button.Index==1)
                                                            {
                                                                int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                                                                if ((frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Product != null)
                                                                {
                                                                    (frm.ControlMain.ViewDetail.GetRow(index) as
                                                                     DocumentDetailSale).Product.ShowWizard();
                                                                }

                                                            }
                                                        };

            frm.ControlMain.BindSourceDetails.AddingNew += (sender, eNew) =>
            {
                if (eNew.NewObject == null)
                    eNew.NewObject = new DocumentDetailSale { Workarea = WA, Document = value, StateId = State.STATEACTIVE };
            };

            frm.ControlMain.ViewDetail.CustomRowFilter += delegate(object sender, RowFilterEventArgs e)
            {
                if ((frm.ControlMain.BindSourceDetails.List[e.ListSourceRow] as DocumentDetailSale).StateId == State.STATEDELETED)
                {
                    e.Visible = false;
                    e.Handled = true;
                }
            };
            frm.ControlMain.ViewDetail.KeyDown += delegate(object sender, KeyEventArgs eKey)
            {
                if (eKey.KeyCode == Keys.Delete &&
                    (value.Document.FlagsValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
                {
                    try
                    {
                        int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                        IDocumentDetail docRow = frm.ControlMain.ViewDetail.GetRow(index) as IDocumentDetail;
                        if (docRow != null)
                        {
                            if (docRow.Id == 0)
                                frm.ControlMain.ViewDetail.DeleteRow(index);
                            else
                            {
                                docRow.StateId = State.STATEDELETED;
                                frm.ControlMain.ViewDetail.RefreshData();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            frm.ControlMain.editNom.ProcessNewValue += delegate(object sender, ProcessNewValueEventArgs eNv)
            {
                RepositoryItemGridLookUpEdit edit = ((GridLookUpEdit)sender).Properties;

                if (eNv.DisplayValue == null || edit.NullText.Equals(eNv.DisplayValue) || string.Empty.Equals(eNv.DisplayValue))
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    DocumentDetailSale docRow = frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale;
                    if (docRow != null && docRow.Id == 0)
                    {
                        frm.ControlMain.ViewDetail.DeleteRow(index);
                    }
                }
                else
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if ((frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Product != null)
                        (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Unit =
                            (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Product.Unit;
                }
            };
            frm.ControlMain.ViewDetail.ValidatingEditor += delegate(object sender, BaseContainerValidateEditorEventArgs eEd)
            {
                if (frm.ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnName" | frm.ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnNom")
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                        eEd.Value != null)
                    {
                        int id = Convert.ToInt32(eEd.Value);
                        Product prod = WA.Cashe.GetCasheData<Product>().Item(id);
                        if (prod != null)
                        {
                            (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Unit = prod.Unit;
                        }
                    }
                }
                if (frm.ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnQty")
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                        eEd.Value != null)
                    {
                        decimal val = Convert.ToDecimal(eEd.Value);
                        (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Summa = val * (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Price;

                    }
                }
                if (frm.ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnPrice")
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                        eEd.Value != null)
                    {
                        decimal val = Convert.ToDecimal(eEd.Value);
                        (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Summa = val * (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Qty;

                    }
                }
                if (frm.ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnSumm")
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                        eEd.Value != null)
                    {
                        decimal val = Convert.ToDecimal(eEd.Value);
                        if ((frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Qty != 0)
                            (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Price = val / (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Qty;

                    }
                }
            };
            //frm.ControlMain.gridColumnDiscount.VisibleIndex = 5;
            //frm.ControlMain.gridColumnSummDiscount.VisibleIndex = 7;
            frm.ControlMain.ViewDetail.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs eNew)
            {
                if (eNew.Column.Name == "gridColumnName" || eNew.Column.Name == "gridColumnNom")
                    if (eNew.Value != null && eNew.Value is int)
                    {
                        
                        decimal newPrice = PriceValue.GetActualPrice(WA, frm.ControlMain.cmbPrice.EditValue==null? 0: (int)frm.ControlMain.cmbPrice.EditValue, (int)eNew.Value,
                                      (int)frm.ControlMain.cmbFrom.EditValue, frm.ControlMain.dtDate.DateTime);
                        decimal qty = (frm.ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Qty;
                        decimal summ = newPrice * qty;
                        decimal discount = (frm.ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Discount;
                        frm.ControlMain.ViewDetail.SetRowCellValue(eNew.RowHandle, "Price", newPrice);
                        (frm.ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Summa = summ;
                    }
            };
            frm.ControlMain.ViewDetail.RefreshData();

            List<Hierarchy> hList = WA.Empty<Hierarchy>().GetCollectionHierarchy(4)[0].Children.Where(h => h.Code != null && h.Code.ToUpper() == "TAXES").ToList();
            if (hList.Count > 0)
            {
                List<Analitic> anList = hList[0].GetTypeContents<Analitic>();
                BindingSource bindingTaxes = new BindingSource { DataSource = anList };
                frm.ControlMain.cmbTaxes.Properties.DataSource = bindingTaxes;
                frm.ControlMain.cmbTaxes.Properties.DisplayMember = GlobalPropertyNames.Name;
                frm.ControlMain.cmbTaxes.Properties.ValueMember = GlobalPropertyNames.Id;
                frm.ControlMain.cmbTaxes.Properties.GetItems();
                List<Taxe> taxes = value.Document.Taxes();
                foreach (Taxe dt in taxes)
                {
                    for (int i = 0; i < frm.ControlMain.cmbTaxes.Properties.Items.Count; i++)
                    {
                        int id = (int)frm.ControlMain.cmbTaxes.Properties.Items[i].Value;
                        if (id == dt.TaxId)
                            frm.ControlMain.cmbTaxes.Properties.Items[i].CheckState = CheckState.Checked;
                    }
                }
            }

            #region Сохранить

            frm.btnSave.ItemClick += delegate
                                         {
                                             value.Document.Number = frm.ControlMain.txtNumber.Text;
                                             value.Document.Date = frm.ControlMain.dtDate.DateTime;
                                             value.Document.Memo = frm.ControlMain.txtMemo.Text;

                                             value.Document.AgentToId = (int)frm.ControlMain.cmbTo.EditValue;
                                             value.Document.AgentDepartmentFromId = (int)frm.ControlMain.cmbFrom.EditValue;
                                             value.Document.AgentDepartmentToId = (int)frm.ControlMain.cmbTo.EditValue;
                                             value.PrcNameId = (int)frm.ControlMain.cmbPrice.EditValue;
                                             value.ManagerId = (int)frm.ControlMain.cmbTrader.EditValue;
                                             value.SupervisorId = (int)frm.ControlMain.cmbSuperviser.EditValue;

                                             value.Document.MyCompanyId = value.Document.AgentDepartmentFromId;
                                             value.Document.ClientId = value.Document.AgentDepartmentToId;

                                             value.Document.AgentFromName = value.Document.AgentFromId == 0 ? string.Empty : value.Document.AgentFrom.Name;
                                             value.Document.AgentDepartmentFromName = value.Document.AgentDepartmentFromId == 0 ? string.Empty : value.Document.AgentDepartmentFrom.Name;
                                             value.Document.AgentDepartmentToName = value.Document.AgentDepartmentToId == 0 ? string.Empty : value.Document.AgentDepartmentTo.Name;
                                             value.Document.AgentToName = value.Document.AgentToId == 0 ? string.Empty : value.Document.AgentTo.Name;

                                             List<Taxe> taxes = value.Document.Taxes();
                                             for (int i = 0; i < frm.ControlMain.cmbTaxes.Properties.Items.Count; i++)
                                             {
                                                 int id = (int)frm.ControlMain.cmbTaxes.Properties.Items[i].Value;
                                                 if (frm.ControlMain.cmbTaxes.Properties.Items[i].CheckState == CheckState.Checked)
                                                 {
                                                     if (taxes.Where(dt => dt.TaxId == id).Count() == 0)
                                                     {
                                                         Taxe docTax = new Taxe
                                                         {
                                                             Workarea = WA,
                                                             DocumentId = value.Id,
                                                             TaxId = id
                                                         };
                                                         docTax.Save();
                                                         value.Document.Taxes().Add(docTax);
                                                     }
                                                 }
                                                 else
                                                 {
                                                     Taxe dt = taxes.FirstOrDefault(f => f.TaxId == id);
                                                     if (dt != null)
                                                     {
                                                         dt.Delete();

                                                         value.Document.Taxes().Remove(dt);
                                                     }
                                                     //foreach (DocumentTax dt in taxes.Where(dt => dt.TaxId == id))
                                                     //{
                                                     //    dt.Delete();
                                                     //    SourceDocument.Document.Taxes().Remove(dt);
                                                     //}
                                                 }
                                             }

                                             value.Validate();
                                             if (value.IsNew)
                                             {
                                                 if (string.IsNullOrEmpty(value.Document.Number))
                                                 {
                                                     Autonum autonum = Autonum.GetAutonumByDocumentKind(WA,
                                                                                                        value.Document.
                                                                                                            KindId);
                                                     autonum.Number++;
                                                     value.Document.Number = autonum.Number.ToString();
                                                     autonum.Save();
                                                     frm.ControlMain.txtNumber.Text = value.Document.Number;
                                                 }
                                             }
                                             try
                                             {
                                                 value.Save();
                                             }
                                             catch (DatabaseException dbe)
                                             {
                                                 Extentions.ShowMessageDatabaseExeption(WA, WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                          WA.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                                             }
                                             catch (Exception ex)
                                             {
                                                 XtraMessageBox.Show(WA.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
                                                     WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                             }
                                         };


            #endregion

            frm.Show();
            return frm;
        }

        
        /// <summary>
        /// Показать документ "Прайс"
        /// </summary>
        /// <returns></returns>
        public static FormDocumentView ShowDocumentPrice(DocumentPrices value, Folder fld)
        {
            FormDocumentView frm = GenerateDocView(value, fld);
            DocumentOut(frm, value, fld);
            return frm;
        }

        /// <summary>
        /// Показать документ "Приход товара"
        /// </summary>
        /// <returns></returns>
        public static FormDocumentView ShowDocumentMoneyIn(DocumentFinance value, Folder fld)
        {
            FormDocumentView frm = GenerateDocView(value, fld);
            DocumentIn(frm, value, fld);
            return frm;
        }
        /// <summary>
        /// Показать документ "Расход денег"
        /// </summary>
        /// <returns></returns>
        public static FormDocumentView ShowDocumentMoneyOut(DocumentFinance value, Folder fld)
        {
            FormDocumentView frm = GenerateDocView(value, fld);

            frm.ControlMain.layoutControlItemPrice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            frm.ControlMain.layoutControlItemTrader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            frm.ControlMain.layoutControlItemSuperviser.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            frm.ControlMain.layoutControlItemPayDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            frm.ControlMain.gridColumnPrice.Visible = false;
            frm.ControlMain.gridColumnQty.Visible = false;

            #region Если это новый документ
            if (value.Id == 0)
            {
                DocumentFinance salesTemplate = WA.Cashe.GetCasheData<DocumentFinance>().Item(fld.DocumentId);

                if (salesTemplate != null)
                {
                    // Установить вид цены
                    if (value.AgFromBankAccId == 0)
                        value.AgFromBankAccId = salesTemplate.AgFromBankAccId;
                    if (value.AgToBankAccId == 0)
                        value.AgToBankAccId = salesTemplate.AgToBankAccId;
                    

                    value.Kind = salesTemplate.Kind;
                    if (salesTemplate.Details.Count > 0 && value.Details.Count == 0)
                    {
                        foreach (DocumentDetailFinance jrnTml in salesTemplate.Details)
                        {
                            DocumentDetailFinance r = value.NewRow();
                            r.ProductId = jrnTml.ProductId;
                            r.Summa = jrnTml.Summa;
                            r.UnitId = jrnTml.UnitId;
                            
                        }
                    }
                }
            }
            #endregion

            DocumentOut(frm, value, fld);

            if (value.AgFromBankAccId != 0)
            {
                frm.ControlMain.bindBankFrom.Add(WA.Cashe.GetCasheData<AgentBankAccount>().Item(value.AgFromBankAccId));
            }
            //frm.ControlMain.cmb.QueryPopUp += delegate
            //{
            //    if (frm.ControlMain.bindPrice.Count < 2)
            //    {
            //        frm.ControlMain.CollectionPrice =
            //            WA.GetCollection<PriceName>().Where(
            //                s => s.KindId == PriceName.KINDID_PRICENAME).
            //                ToList();
            //        frm.ControlMain.bindPrice.DataSource =
            //            frm.ControlMain.CollectionPrice;
            //    }
            //};

            //frm.ControlMain.cmbPrice.EditValue = value.PrcNameId;
            //if (value.ManagerId != 0)
            //{
            //    frm.ControlMain.bindTrader.Add(value.Manager);
            //}
            //frm.ControlMain.cmbTrader.EditValue = value.ManagerId;
            //frm.ControlMain.cmbTrader.QueryPopUp += delegate
            //{
            //    if (frm.ControlMain.bindTrader.Count < 2)
            //    {
            //        frm.ControlMain.CollectionTrader =
            //            Agent.GetChainSourceList(WA,
            //                                     (int)
            //                                     frm.ControlMain.cmbFrom.
            //                                         EditValue,
            //                                     TradersChainId);
            //        frm.ControlMain.bindTrader.DataSource =
            //            frm.ControlMain.CollectionTrader;
            //    }
            //};

            //if (value.SupervisorId != 0)
            //{
            //    frm.ControlMain.bindSuperviser.Add(value.Supervisor);
            //}
            //frm.ControlMain.cmbSuperviser.EditValue = value.SupervisorId;
            //frm.ControlMain.cmbSuperviser.QueryPopUp += delegate
            //{
            //    if (frm.ControlMain.bindSuperviser.Count < 2)
            //    {
            //        frm.ControlMain.CollectionSuperviser = Agent.GetChainSourceList(WA,
            //                                                                        (int)
            //                                                                        frm.ControlMain.cmbFrom.EditValue,
            //                                                                        TradersChainId);

            //        frm.ControlMain.bindSuperviser.DataSource = frm.ControlMain.CollectionSuperviser;
            //    }
            //};

            frm.ControlMain.BindSourceDetails.DataSource = value.Details;

            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.editNom.View, "DEFAULT_LISTVIEWPRODUCT");
            DataGridViewHelper.GenerateGridColumns(WA, frm.ControlMain.editName.View, "DEFAULT_LISTVIEWPRODUCT");

            List<Product> collProducts;
            collProducts = WA.GetCollection<Product>().Where(s => s.KindValue == Product.KINDVALUE_PRODUCT).ToList();
            frm.ControlMain.bindProduct.DataSource = collProducts;

            frm.ControlMain.editNom.DataSource = frm.ControlMain.bindProduct;
            frm.ControlMain.editName.DataSource = frm.ControlMain.bindProduct;

            frm.ControlMain.editName.ButtonClick += delegate(object sender, ButtonPressedEventArgs e)
            {
                if (e.Button.Index == 1)
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if ((frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance).Product != null)
                    {
                        (frm.ControlMain.ViewDetail.GetRow(index) as
                         DocumentDetailFinance).Product.ShowWizard();
                    }

                }
            };

            frm.ControlMain.BindSourceDetails.AddingNew += (sender, eNew) =>
            {
                if (eNew.NewObject == null)
                    eNew.NewObject = new DocumentDetailFinance { Workarea = WA, Document = value, StateId = State.STATEACTIVE };
            };

            frm.ControlMain.ViewDetail.CustomRowFilter += delegate(object sender, RowFilterEventArgs e)
            {
                if ((frm.ControlMain.BindSourceDetails.List[e.ListSourceRow] as DocumentDetailFinance).StateId == State.STATEDELETED)
                {
                    e.Visible = false;
                    e.Handled = true;
                }
            };
            frm.ControlMain.ViewDetail.KeyDown += delegate(object sender, KeyEventArgs eKey)
            {
                if (eKey.KeyCode == Keys.Delete &&
                    (value.Document.FlagsValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
                {
                    try
                    {
                        int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                        IDocumentDetail docRow = frm.ControlMain.ViewDetail.GetRow(index) as IDocumentDetail;
                        if (docRow != null)
                        {
                            if (docRow.Id == 0)
                                frm.ControlMain.ViewDetail.DeleteRow(index);
                            else
                            {
                                docRow.StateId = State.STATEDELETED;
                                frm.ControlMain.ViewDetail.RefreshData();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            frm.ControlMain.editNom.ProcessNewValue += delegate(object sender, ProcessNewValueEventArgs eNv)
            {
                RepositoryItemGridLookUpEdit edit = ((GridLookUpEdit)sender).Properties;

                if (eNv.DisplayValue == null || edit.NullText.Equals(eNv.DisplayValue) || string.Empty.Equals(eNv.DisplayValue))
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    DocumentDetailFinance docRow = frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance;
                    if (docRow != null && docRow.Id == 0)
                    {
                        frm.ControlMain.ViewDetail.DeleteRow(index);
                    }
                }
                else
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if ((frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance).Product != null)
                        (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance).Unit =
                            (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance).Product.Unit;
                }
            };
            frm.ControlMain.ViewDetail.ValidatingEditor += delegate(object sender, BaseContainerValidateEditorEventArgs eEd)
            {
                if (frm.ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnName" | frm.ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnNom")
                {
                    int index = frm.ControlMain.ViewDetail.FocusedRowHandle;
                    if (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance != null &&
                        eEd.Value != null)
                    {
                        int id = Convert.ToInt32(eEd.Value);
                        Product prod = WA.Cashe.GetCasheData<Product>().Item(id);
                        if (prod != null)
                        {
                            (frm.ControlMain.ViewDetail.GetRow(index) as DocumentDetailFinance).Unit = prod.Unit;
                        }
                    }
                }
            };
            //frm.ControlMain.gridColumnDiscount.VisibleIndex = 5;
            //frm.ControlMain.gridColumnSummDiscount.VisibleIndex = 7;
            frm.ControlMain.ViewDetail.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs eNew)
            {
                if (eNew.Column.Name == "gridColumnName" || eNew.Column.Name == "gridColumnNom")
                    if (eNew.Value != null && eNew.Value is int)
                    {

                        //decimal newPrice = PriceValue.GetActualPrice(WA, frm.ControlMain.cmbPrice.EditValue == null ? 0 : (int)frm.ControlMain.cmbPrice.EditValue, (int)eNew.Value,
                        //              (int)frm.ControlMain.cmbFrom.EditValue, frm.ControlMain.dtDate.DateTime);
                        //decimal qty = (frm.ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Qty;
                        //decimal summ = newPrice * qty;
                        //decimal discount = (frm.ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Discount;
                        //frm.ControlMain.ViewDetail.SetRowCellValue(eNew.RowHandle, "Price", newPrice);
                        //(frm.ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Summa = summ;
                    }
            };
            frm.ControlMain.ViewDetail.RefreshData();

            List<Hierarchy> hList = WA.Empty<Hierarchy>().GetCollectionHierarchy(4)[0].Children.Where(h => h.Code != null && h.Code.ToUpper() == "TAXES").ToList();
            if (hList.Count > 0)
            {
                List<Analitic> anList = hList[0].GetTypeContents<Analitic>();
                BindingSource bindingTaxes = new BindingSource { DataSource = anList };
                frm.ControlMain.cmbTaxes.Properties.DataSource = bindingTaxes;
                frm.ControlMain.cmbTaxes.Properties.DisplayMember = GlobalPropertyNames.Name;
                frm.ControlMain.cmbTaxes.Properties.ValueMember = GlobalPropertyNames.Id;
                frm.ControlMain.cmbTaxes.Properties.GetItems();
                List<Taxe> taxes = value.Document.Taxes();
                foreach (Taxe dt in taxes)
                {
                    for (int i = 0; i < frm.ControlMain.cmbTaxes.Properties.Items.Count; i++)
                    {
                        int id = (int)frm.ControlMain.cmbTaxes.Properties.Items[i].Value;
                        if (id == dt.TaxId)
                            frm.ControlMain.cmbTaxes.Properties.Items[i].CheckState = CheckState.Checked;
                    }
                }
            }

            #region Сохранить

            frm.btnSave.ItemClick += delegate
            {
                value.Document.Number = frm.ControlMain.txtNumber.Text;
                value.Document.Date = frm.ControlMain.dtDate.DateTime;
                value.Document.Memo = frm.ControlMain.txtMemo.Text;

                value.Document.AgentToId = (int)frm.ControlMain.cmbTo.EditValue;
                value.Document.AgentDepartmentFromId = (int)frm.ControlMain.cmbFrom.EditValue;
                value.Document.AgentDepartmentToId = (int)frm.ControlMain.cmbTo.EditValue;
                //value.PrcNameId = (int)frm.ControlMain.cmbPrice.EditValue;
                //value.ManagerId = (int)frm.ControlMain.cmbTrader.EditValue;
                //value.SupervisorId = (int)frm.ControlMain.cmbSuperviser.EditValue;

                value.Document.MyCompanyId = value.Document.AgentDepartmentFromId;
                value.Document.ClientId = value.Document.AgentDepartmentToId;

                value.Document.AgentFromName = value.Document.AgentFromId == 0 ? string.Empty : value.Document.AgentFrom.Name;
                value.Document.AgentDepartmentFromName = value.Document.AgentDepartmentFromId == 0 ? string.Empty : value.Document.AgentDepartmentFrom.Name;
                value.Document.AgentDepartmentToName = value.Document.AgentDepartmentToId == 0 ? string.Empty : value.Document.AgentDepartmentTo.Name;
                value.Document.AgentToName = value.Document.AgentToId == 0 ? string.Empty : value.Document.AgentTo.Name;

                List<Taxe> taxes = value.Document.Taxes();
                for (int i = 0; i < frm.ControlMain.cmbTaxes.Properties.Items.Count; i++)
                {
                    int id = (int)frm.ControlMain.cmbTaxes.Properties.Items[i].Value;
                    if (frm.ControlMain.cmbTaxes.Properties.Items[i].CheckState == CheckState.Checked)
                    {
                        if (taxes.Where(dt => dt.TaxId == id).Count() == 0)
                        {
                            Taxe docTax = new Taxe
                            {
                                Workarea = WA,
                                DocumentId = value.Id,
                                TaxId = id
                            };
                            docTax.Save();
                            value.Document.Taxes().Add(docTax);
                        }
                    }
                    else
                    {
                        Taxe dt = taxes.FirstOrDefault(f => f.TaxId == id);
                        if (dt != null)
                        {
                            dt.Delete();

                            value.Document.Taxes().Remove(dt);
                        }
                        //foreach (DocumentTax dt in taxes.Where(dt => dt.TaxId == id))
                        //{
                        //    dt.Delete();
                        //    SourceDocument.Document.Taxes().Remove(dt);
                        //}
                    }
                }

                value.Validate();
                if (value.IsNew)
                {
                    if (string.IsNullOrEmpty(value.Document.Number))
                    {
                        Autonum autonum = Autonum.GetAutonumByDocumentKind(WA,
                                                                           value.Document.
                                                                               KindId);
                        autonum.Number++;
                        value.Document.Number = autonum.Number.ToString();
                        autonum.Save();
                        frm.ControlMain.txtNumber.Text = value.Document.Number;
                    }
                }
                try
                {
                    value.Save();
                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(WA, WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                             WA.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(WA.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
                        WA.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };


            #endregion

            frm.Show();
            return frm;
        }



        
    }
}
