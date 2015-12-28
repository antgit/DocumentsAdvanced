using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Documents;
using BusinessObjects.Documents.Person;

namespace BusinessObjects.Print
{
    /// <summary>
    /// Подготовка печатного документа в разделе "Управление финансами"
    /// </summary>
    public class PreparePrintPerson
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PreparePrintPerson()
        {

        }

        private DocumentPerson _sourceDocument;

        /// <summary>
        /// Документ для печати
        /// </summary>
        public DocumentPerson SourceDocument
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
        public PrintDataDocumentPersonHeader PrintHeader { get; private set; }
        ///// <summary>
        ///// Товарная детализация документа
        ///// </summary>
        //public List<PrintDataDocumentProductDetail> PrintData { get; private set; }
        /// <summary>
        /// Запуск подготовки документа
        /// </summary>
        public virtual void Refresh()
        {
            if (SourceDocument == null)
                throw new ApplicationException("Не указан документ для подготовки печати!");
            #region Подготовка данных
            PrintHeader = new PrintDataDocumentPersonHeader
            {
                DocDate = SourceDocument.Document.Date,
                DocNo = SourceDocument.Document.Number,
                Summa = SourceDocument.Document.Summa,
                Memo = SourceDocument.Document.Memo
            };

            //PrintHeader.DateStart = SourceDocument.
            //PrintHeader.DateEnd = SourceDocument.
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

            if (SourceDocument.EmployerId!=0)
            {
                PrintHeader.EmployerName = SourceDocument.Employer.Name;
                PrintHeader.EmployerFirstName = SourceDocument.Employer.People.FirstName;
                PrintHeader.EmployerMidleName = SourceDocument.Employer.People.MidleName;
                PrintHeader.EmployerLastName = SourceDocument.Employer.People.LastName;
            }
            PersonXml personXml = PersonXml.GetValue(SourceDocument.Document);
            if(personXml!=null)
            {
                PrintHeader.DateStart = personXml.DateStart;
                PrintHeader.DateEnd = personXml.DateEnd;
                PrintHeader.DepatmentFromName = personXml.DepatmentFromId == 0 ? string.Empty : SourceDocument.Workarea.Cashe.GetCasheData<Depatment>().Item(personXml.DepatmentFromId).Name;
                PrintHeader.DepatmentToName = personXml.DepatmentToId == 0 ? string.Empty : SourceDocument.Workarea.Cashe.GetCasheData<Depatment>().Item(personXml.DepatmentToId).Name;
            }

            
            #endregion

        }
    }
}