using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Documents;

namespace BusinessObjects.Print
{
    /// <summary>
    /// Подготовка печатного документа "Отчет о копировальной технике" в разделе "Договора"
    /// </summary>
    public class PreparePrintContractPrinter
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PreparePrintContractPrinter()
        {

        }

        private DocumentContract _sourceDocument;

        /// <summary>
        /// Документ для печати
        /// </summary>
        public DocumentContract SourceDocument
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
        public List<PrintDataDocumentPrinterDetail> PrintData { get; private set; }
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
                    PrintHeader.AgentFromBank = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Name;
                    PrintHeader.AgentFromBankMfo = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Company.Bank.Mfo;
                    PrintHeader.AgentFromAcount = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Code;
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
                    if (SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank != null)
                    {
                        PrintHeader.AgentToBank = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Name;
                        PrintHeader.AgentToBankMfo = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Company.Bank.Mfo;
                    }
                    PrintHeader.AgentToAcount = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Code;
                }
                PrintHeader.AgentToPhone = SourceDocument.Document.AgentDepartmentTo.Phone;
            }

            prnName = PrintHeader.AgFromName + "; ЕДРПОУ " + PrintHeader.AgentFromOkpo +
                      "тел. " + PrintHeader.AgentFromPhone + " ; " + PrintHeader.AgentFromBank +
                      " МФО " + PrintHeader.AgentFromBankMfo +
                      " т/с № " + PrintHeader.AgentFromAcount +
                      "; Адрес: " + PrintHeader.AgentFromAddres;
            decimal Summa = 0;

            PrintHeader.RegistratorName = SourceDocument.RegistratorId==0? string.Empty : SourceDocument.Registrator.Name;
            PrintHeader.DateStart = SourceDocument.DateStart;
            PrintHeader.DateEnd = SourceDocument.DateEnd;

            IEnumerable<DocumentDetailContract> items = SourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

            PrintData = items.Select(item => new PrintDataDocumentPrinterDetail
                                                 {
                                                     CountMonth = item.Price,
                                                     CountTotal = item.Summa,
                                                     Location = item.StringValue2,
                                                     Memo = item.Memo,
                                                     ProductCode = item.Product.Nomenclature,
                                                     ProductName = item.Product.Name,
                                                     YearStart = (int)item.Qty,
                                                     UnitName = (item.UnitId != 0 ? item.Unit.Code : string.Empty),
                                                     EqupmentState = (item.AnaliticId !=0? item.Analitic.Name: string.Empty ),
                                                     Configuration = item.Product.Memo
                                                 }).ToList();

            //Summa = PrintData.Sum(f => f.Summa);
            //PrintHeader.SummaNds = Math.Round(SourceDocument.Document.SummaTax);
            //PrintHeader.SummaTotal = Summa + PrintHeader.SummaNds;
            #endregion
        }
    }
}