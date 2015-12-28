using System;

namespace BusinessObjects.Print
{
    /// <summary>
    /// Класс печати заголовка документа
    /// </summary>
    public class PrintDataDocumentHeaderPrice
    {
        public string CompanyFromName { get; set; }
        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        /// <summary>Дата начала действия</summary>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocNo { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Наименование корреспондента "Кто"
        /// </summary>
        public string AgFromName { get; set; }

        /// <summary>
        /// Наименование корреспондента "Кому"
        /// </summary>
        public string AgToName { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Summa { get; set; }

        /// <summary>
        /// Расчетный счет корреспондента "Кто"
        /// </summary>
        public string AgentFromAcount { get; set; }

        /// <summary>
        /// Расчетный счет корреспондента "Кому"
        /// </summary>
        public string AgentToAcount { get; set; }

        /// <summary>
        /// Наименование банка корреспондента "Кто"
        /// </summary>
        public string AgentFromBank { get; set; }

        /// <summary>
        /// Наименование банка корреспондента "Кому"
        /// </summary>
        public string AgentToBank { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Телефон корреспондента "Кто"
        /// </summary>
        public string AgentFromPhone { get; set; }
        /// <summary>
        /// Телефон корреспондента "Кому"
        /// </summary>
        public string AgentToPhone { get; set; }
        /// <summary>
        /// Номер свидетельства корреспондента "Кто"
        /// </summary>
        public string AgentFromReg { get; set; }
        /// <summary>
        /// Номер свидетельства корреспондента "Кому"
        /// </summary>
        public string AgentToReg { get; set; }
        /// <summary>
        /// Инн корреспондента "Кому"
        /// </summary>
        public string AgentToInn { get; set; }
        /// <summary>
        /// Инн корреспондента "Кто"
        /// </summary>
        public string AgentFromInn { get; set; }
        /// <summary>
        /// Адрес корреспондента "Кто"
        /// </summary>
        public string AgentFromAddres { get; set; }
        /// <summary>
        /// Адрес корреспондента "Кому"
        /// </summary>
        public string AgentToAddres { get; set; }
        /// <summary>
        /// Окпо корреспондента "Кто" 
        /// </summary>
        public string AgentFromOkpo { get; set; }
        /// <summary>
        /// Окпо корреспондента "Кому" 
        /// </summary>
        public string AgentToOkpo { get; set; }
        /// <summary>
        /// Мфо банка корреспондента "Кто" 
        /// </summary>
        public string AgentFromBankMfo { get; set; }
        /// <summary>
        /// Мфо банка корреспондента "Кому" 
        /// </summary>
        public string AgentToBankMfo { get; set; }

    }
}