using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessObjects.Documents;

namespace BusinessObjects.Print
{
    /// <summary>
    /// Подготовка печатного документа в разделе "Управление продажами"
    /// </summary>
    public class PreparePrintSales
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PreparePrintSales()
        {
            
        }

        private DocumentSales _sourceDocument;

        /// <summary>
        /// Документ для печати
        /// </summary>
        public DocumentSales SourceDocument
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
                    if (SourceDocument.BankAccFromId != 0)
                    {
                        PrintHeader.AgentFromAcount = SourceDocument.BankAccFrom.Code;
                        if (SourceDocument.BankAccFrom.Bank != null)
                        {
                            PrintHeader.AgentFromBank = SourceDocument.BankAccFrom.Bank.Name;
                            PrintHeader.AgentFromBankMfo = SourceDocument.BankAccFrom.Bank.Company.Bank.Mfo;
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
                    if (SourceDocument.BankAccToId != 0)
                    {
                        PrintHeader.AgentToAcount = SourceDocument.BankAccTo.Code;
                        if (SourceDocument.BankAccTo.Bank != null)
                        {
                            PrintHeader.AgentToBank = SourceDocument.BankAccTo.Bank.Name;
                            PrintHeader.AgentToBankMfo = SourceDocument.BankAccTo.Bank.Company.Bank.Mfo;
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
                PrintHeader.AgentToPhone = SourceDocument.Document.AgentDepartmentTo.Phone;
            }

            if(SourceDocument.Signs().Count>0)
            {
                DocumentSign signFirst = SourceDocument.Signs().FirstOrDefault(f => f.Kind == DocumentSign.KINDID_FIRST);
                if (signFirst != null)
                    PrintHeader.WorkerSignFirst = signFirst.AgentSignId == 0 ? string.Empty : signFirst.AgentSign.Name;

                DocumentSign signSecond = SourceDocument.Signs().FirstOrDefault(f => f.Kind == DocumentSign.KINDID_SECOND);
                if (signSecond != null)
                    PrintHeader.WorkerSignSecond = signSecond.AgentSignId == 0 ? string.Empty : signSecond.AgentSign.Name;
            }

            prnName = PrintHeader.AgFromName + "; ЕДРПОУ " + PrintHeader.AgentFromOkpo +
                      "тел. " + PrintHeader.AgentFromPhone + " ; " + PrintHeader.AgentFromBank +
                      " МФО " + PrintHeader.AgentFromBankMfo + 
                      " т/с № " + PrintHeader.AgentFromAcount + 
                      "; Адрес: " + PrintHeader.AgentFromAddres;
            decimal Summa = 0;

            IEnumerable<DocumentDetailSale> items = SourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

            PrintData = items.Select(item => new PrintDataDocumentProductDetail
            {
                Discount = item.Discount,
                Price = item.Price,
                Summa = item.Summa,
                Memo = item.Memo,
                ProductCode = item.Product.Nomenclature,
                ProductName = item.Product.Name,
                Qty = item.Qty,
                UnitName = (item.UnitId != 0 ? item.Unit.Code : string.Empty)
            }).ToList();

            Summa = PrintData.Sum(f => f.Summa);
            PrintHeader.SummaNds = Math.Round(SourceDocument.Document.SummaTax);
            PrintHeader.SummaTotal = Summa + PrintHeader.SummaNds;
            #endregion
        }
    }
}
