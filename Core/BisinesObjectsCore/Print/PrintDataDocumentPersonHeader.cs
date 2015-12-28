using System;

namespace BusinessObjects.Print
{
    /// <summary>
    /// Класс печати заголовка документа раздела "Персонал"
    /// </summary>
    public class PrintDataDocumentPersonHeader
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocNo { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Наименование корреспондента "Кому"
        /// </summary>
        public string AgToName { get; set; }

        /// <summary>
        /// Окпо корреспондента "Кому" 
        /// </summary>
        public string AgentToOkpo { get; set; }

        /// <summary>
        /// Адрес корреспондента "Кому"
        /// </summary>
        public string AgentToAddres { get; set; }

        /// <summary>
        /// Мфо банка корреспондента "Кому" 
        /// </summary>
        public string AgentToBankMfo { get; set; }

        /// <summary>
        /// Наименование банка корреспондента "Кому"
        /// </summary>
        public string AgentToBank { get; set; }

        /// <summary>
        /// Расчетный счет корреспондента "Кому"
        /// </summary>
        public string AgentToAcount { get; set; }

        /// <summary>
        /// Бухгалтер корреспондента "Кому" 
        /// </summary>
        public string AgentToBuh { get; set; }
        /// <summary>
        /// Директор корреспондента "Кому" 
        /// </summary>
        public string AgentToDirector { get; set; }

        /// <summary>
        /// Кассир корреспондента "Кто" 
        /// </summary>
        public string AgentToCashier { get; set; }
        /// <summary>
        /// Телефон корреспондента "Кому"
        /// </summary>
        public string AgentToPhone { get; set; }

        /// <summary>
        /// Наименование корреспондента "Кому"
        /// </summary>
        public string DepatmentToName { get; set; }

        /// <summary>
        /// Наименование корреспондента "Кому"
        /// </summary>
        public string DepatmentFromName { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Summa { get; set; }

        /// <summary>
        /// Наименование сотрудника
        /// </summary>
        public string EmployerName { get; set; }

        /// <summary>
        /// Наименование сотрудника
        /// </summary>
        public string EmployerFirstName { get; set; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string EmployerLastName { get; set; }

        /// <summary>
        /// Отчество  сотрудника
        /// </summary>
        public string EmployerMidleName { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string WorkPost { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Memo { get; set; }
    }
}