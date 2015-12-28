using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Documents;

namespace BusinessObjects.Print
{
    /// <summary>
    /// Подготовка печатного документа в разделе "Управление финансами"
    /// </summary>
    public class PreparePrintFinance
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PreparePrintFinance()
        {

        }

        private DocumentFinance _sourceDocument;

        /// <summary>
        /// Документ для печати
        /// </summary>
        public DocumentFinance SourceDocument
        {
            get { return _sourceDocument; }
            set
            {
                _sourceDocument = value;
                Refresh();
            }
        }

        /// <summary>
        /// Шапка документа
        /// </summary>
        public PrintDataDocumentHeader PrintHeader { get; private set; }
        /// <summary>
        /// Товарная детализация документа
        /// </summary>
        public List<PrintDataDocumentProductDetail> PrintData { get; private set; }
        /// <summary>
        /// Запуск подготовки документа
        /// </summary>
        public virtual void Refresh()
        {
            if (SourceDocument == null)
                throw new ApplicationException("Не указан документ для подготовки печати!");
            #region Подготовка данных
            PrintHeader = new PrintDataDocumentHeader
            {
                DocDate = SourceDocument.Document.Date,
                DocNo = SourceDocument.Document.Number,
                Summa = SourceDocument.Document.Summa,
                Memo = SourceDocument.Document.Memo
            };
            string prnName = string.Empty;
            if (SourceDocument.Document.AgentDepartmentFromId != 0)
            {
                PrintHeader.AgFromName = string.IsNullOrEmpty(SourceDocument.Document.AgentDepartmentFrom.NameFull)
                                        ? SourceDocument.Document.AgentDepartmentFromName
                                        : SourceDocument.Document.AgentDepartmentFrom.NameFull;
                PrintHeader.AgentFromOkpo = SourceDocument.Document.AgentDepartmentFrom.Company.Okpo;
                PrintHeader.AgentFromAddres = SourceDocument.Document.AgentDepartmentFrom.AddressLegal;
                if (SourceDocument.Document.AgentDepartmentFrom.BankAccounts.Count > 0)
                {
                    if (SourceDocument.AgFromBankAccId != 0)
                    {
                        PrintHeader.AgentFromAcount = SourceDocument.AgFromBankAcc.Code;
                        if (SourceDocument.AgFromBankAcc.Bank != null)
                        {
                            PrintHeader.AgentFromBank = SourceDocument.AgFromBankAcc.Bank.Name;
                            PrintHeader.AgentFromBankMfo = SourceDocument.AgFromBankAcc.Bank.Company.Bank.Mfo;
                        }
                    }
                    else
                    {
                        PrintHeader.AgentFromAcount = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Code;
                        if (SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank != null)
                        {
                            PrintHeader.AgentFromBank = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Name;
                            PrintHeader.AgentFromBankMfo = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Company.Bank.Mfo;
                        }
                    }
                }
                if (SourceDocument.Document.AgentDepartmentFrom.Company != null)
                {
                    if (SourceDocument.Document.AgentDepartmentFrom.Company.DirectorId != 0)
                        PrintHeader.AgentFromDirector = SourceDocument.Document.AgentDepartmentFrom.Company.Director.Name;
                    if (SourceDocument.Document.AgentDepartmentFrom.Company.BuhId != 0)
                        PrintHeader.AgentFromBuh = SourceDocument.Document.AgentDepartmentFrom.Company.Buh.Name;
                    if (SourceDocument.Document.AgentDepartmentFrom.Company.CashierId != 0)
                        PrintHeader.AgentFromCashier = SourceDocument.Document.AgentDepartmentFrom.Company.Cashier.Name;
                }
                PrintHeader.AgentFromPhone = SourceDocument.Document.AgentDepartmentFrom.Phone;
            }
            if (SourceDocument.Document.AgentDepartmentToId != 0)
            {
                PrintHeader.AgToName = string.IsNullOrEmpty(SourceDocument.Document.AgentDepartmentTo.NameFull)
                                        ? SourceDocument.Document.AgentDepartmentToName
                                        : SourceDocument.Document.AgentDepartmentTo.NameFull;

                PrintHeader.AgentToOkpo = SourceDocument.Document.AgentDepartmentTo.Company.Okpo;
                PrintHeader.AgentToAddres = SourceDocument.Document.AgentDepartmentTo.AddressLegal;
                if (SourceDocument.Document.AgentDepartmentTo.BankAccounts.Count > 0)
                {
                    if (SourceDocument.AgToBankAccId != 0)
                    {
                        PrintHeader.AgentToAcount = SourceDocument.AgToBankAcc.Code;
                        if (SourceDocument.AgToBankAcc.Bank != null)
                        {
                            PrintHeader.AgentToBank = SourceDocument.AgToBankAcc.Bank.Name;
                            PrintHeader.AgentToBankMfo = SourceDocument.AgToBankAcc.Bank.Company.Bank.Mfo;
                        }
                    }
                    else
                    {
                        if (SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank != null)
                        {
                            PrintHeader.AgentToBank = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Name;
                            PrintHeader.AgentToBankMfo = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Company.Bank.Mfo;
                        }
                        PrintHeader.AgentToAcount = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Code;
                    }
                }
                if (SourceDocument.Document.AgentDepartmentTo.Company != null)
                {
                    if (SourceDocument.Document.AgentDepartmentTo.Company.DirectorId != 0)
                        PrintHeader.AgentToDirector = SourceDocument.Document.AgentDepartmentTo.Company.Director.Name;
                    if (SourceDocument.Document.AgentDepartmentTo.Company.BuhId != 0)
                        PrintHeader.AgentToBuh = SourceDocument.Document.AgentDepartmentTo.Company.Buh.Name;
                    if (SourceDocument.Document.AgentDepartmentTo.Company.CashierId != 0)
                        PrintHeader.AgentToCashier = SourceDocument.Document.AgentDepartmentTo.Company.Cashier.Name;
                }
                PrintHeader.AgentToPhone = SourceDocument.Document.AgentDepartmentTo.Phone;
            }

            decimal Summa = 0;

            IEnumerable<DocumentDetailFinance> items = SourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

            List<PrintDataDocumentProductDetail> collection = items.Select(item => new PrintDataDocumentProductDetail
            {
                Summa = item.Summa,
                Memo = item.Memo,
                ProductCode = item.Product.Nomenclature,
                ProductName = item.Product.Name,
                UnitName = (item.UnitId != 0 ? item.Unit.Code : string.Empty)
            }).ToList();

            Summa = collection.Sum(f => f.Summa);
            PrintHeader.SummaNds = Math.Round(SourceDocument.Document.SummaTax);
            PrintHeader.SummaTotal = Summa + PrintHeader.SummaNds;
            #endregion

        }
    }
}