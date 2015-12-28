using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Print
{
    /// <summary>
    /// Класс печати заголовка документа
    /// </summary>
    public class PrintDataDocumentHeader
    {
        /// <summary>
        /// Номер договора
        /// </summary>
        public string DogovorNo { get; set; }
        /// <summary>
        /// Дата договора
        /// </summary>
        public DateTime? DogovorDate { get; set; }
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
        /// Сумма НДС
        /// </summary>
        public decimal SummaNds { get; set; }

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
        /// Сумма всего
        /// </summary>
        public decimal SummaTotal { get; set; }

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
        /// Условие поставки
        /// </summary>
        public string DeliveryCondition { get; set; }
        /// <summary>
        /// Форма проведённых расчётов 
        /// </summary>
        public string PaymentMethod { get; set; }
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
        /// <summary>
        /// Бухгалтер корреспондента "Кому" 
        /// </summary>
        public string AgentToBuh { get; set; }
        /// <summary>
        /// Бухгалтер корреспондента "Кто" 
        /// </summary>
        public string AgentFromBuh { get; set; }

        /// <summary>
        /// Директор корреспондента "Кто" 
        /// </summary>
        public string AgentFromDirector { get; set; }
        /// <summary>
        /// Директор корреспондента "Кому" 
        /// </summary>
        public string AgentToDirector { get; set; }
        /// <summary>
        /// Кассир корреспондента "Кому" 
        /// </summary>
        public string AgentFromCashier { get; set; }
        /// <summary>
        /// Кассир корреспондента "Кто" 
        /// </summary>
        public string AgentToCashier { get; set; }
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        

        /// <summary>
        /// Регистратор документа
        /// </summary>
        public string RegistratorName { get; set; }

        /// <summary>
        /// Имя пользователя подписавшего докуменент с первой стороны
        /// </summary>
        public string WorkerSignFirst { get; set; }

        /// <summary>
        /// Имя пользователя подписавшего докуменент со второй стороны
        /// </summary>
        public string WorkerSignSecond { get; set; }



    }
}
