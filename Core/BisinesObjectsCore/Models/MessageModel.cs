using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель сообщения пользователя
    /// </summary>
    public class MessageModel : BaseModel<Message>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public MessageModel()
        {   
        }
        /// <summary>
        /// Получение данных
        /// </summary>
        /// <param name="value"></param>
        public override void GetData(Message value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            UserOwnerId = value.UserOwnerId;
            UserOwnerName = value.UserOwnerId != 0 ? value.UserOwner.Name : string.Empty;
            PriorityId = value.PriorityId;
            PriorityName = value.PriorityName;
            UserId = value.UserId;
            UserRecipientName = value.UserId != 0 ? value.User.Name : string.Empty;
            HasRead = value.HasRead;
            ReadDone = value.ReadDone;
            ReadDate = value.ReadDate;
            ReadTime = value.ReadTime;
            IsSend = value.IsSend;
            SendDate = value.SendDate;
            SendTime = value.SendTime;
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            MarkedOwner = value.MarkedOwner;
            MarkScoreOwner = value.MarkScoreOwner;
            MarkedRecipient = value.MarkedRecipient;
            MarkScoreRecipient = value.MarkScoreRecipient;
            AgentOwnerName = value.AgentOwnerName;
            AgentToName = value.AgentToName;
            SendTimeAsDate = Period.TimeSpan2DateTime(value.SendTime);
            ReadTimeAsDate = Period.TimeSpan2DateTime(value.ReadTime);
        }
        

        /// <summary>Идентификатор пользователя владельца</summary>
        public int UserOwnerId {get; set; }
        /// <summary>Имя пользователя владельца</summary>
        public string UserOwnerName { get; set; }
        /// <summary>Идентификатор приоритета</summary>
        public int PriorityId { get; set; }
        /// <summary>Наименование приоритета</summary>
        public string PriorityName { get; set; }
        /// <summary>Идентификатор пользователя получателя</summary>
        public int UserId { get; set; }
        /// <summary>Имя пользователя получателя</summary>
        public string UserRecipientName { get; set; }
        /// <summary>Требовать уведомления о прочтении</summary>
        public bool HasRead { get; set; }
        /// <summary>Флаг "Собщение прочитано", обработано</summary>
        public bool ReadDone { get; set; }
        /// <summary>Дата прочтения</summary>
        public DateTime? ReadDate { get; set; }
        /// <summary>Время прочтения</summary>
        public TimeSpan? ReadTime { get; set; }
        /// <summary>Время прочтения в виде даты</summary>
        public DateTime? ReadTimeAsDate { get; set; }
        /// <summary>Отправлено</summary>
        public bool IsSend { get; set; }
        /// <summary>Дата отправки</summary>
        public DateTime? SendDate { get; set; }
        /// <summary>Время отправки</summary>
        public TimeSpan? SendTime { get; set; }
        /// <summary>Время отправки в виде строки</summary>
        public string SendTimeString
        {
            get
            {
                if (SendTime.HasValue)
                    Period.TimeSpan2DateTime(SendTime);
                return string.Empty;
            }
        }
        public DateTime? SendTimeAsDate { get; set; }
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }    
        /// <summary>Отметка отправителя</summary>
        public bool MarkedOwner { get; set; }
        /// <summary>Уровень отметки отправителя</summary>
        public int MarkScoreOwner { get; set; }
        /// <summary>Отметка получателя</summary>
        public bool MarkedRecipient { get; set; }
        /// <summary>Уровень отметки получателя</summary>
        public int MarkScoreRecipient { get; set; }

        public string AgentToName { get; set; }
        public string AgentOwnerName { get; set; }
    }
}
