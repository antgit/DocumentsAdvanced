using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects.Workflows.Web
{
    /// <summary>
    /// Отправить сообщение при запросе подписания документа 
    /// </summary>
    public class ActivitySendMessageOnDocumentSignRequest: CodeActivity
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ActivitySendMessageOnDocumentSignRequest()
            : base()
        {
            this.DisplayName = "Отправить сообщение при запросе подписания документа";
        }
        /// <summary>
        /// Рабочая область
        /// </summary>
        [RequiredArgument]
        public InArgument<Workarea> CurrentWorkarea { get; set; }
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        [RequiredArgument]
        public InArgument<int> DocumentId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [RequiredArgument]
        public InArgument<int> AgentId { get; set; }

        /// <summary>
        /// Идентификатор пользователя отправителя
        /// </summary>
        [RequiredArgument]
        public InArgument<int> UserFromId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            // сформировать сообщение для пользователя

            Workarea wa = CurrentWorkarea.Get(context);
            int agentId = AgentId.Get(context);
            int userFromId = UserFromId.Get(context);
            int documentId = DocumentId.Get(context);
            Document doc = wa.GetObject<Document>(documentId);
            // поиск пользователей для которых формируется сообщение
            IEnumerable<Uid> users = wa.Access.GetAllUsers().Where(f => f.AgentId == agentId);
            Message templateMessage = wa.GetTemplates<Message>().First(f => f.KindId == Message.KINDID_USER);
            foreach (Uid user in users)
            {
                Message msg = wa.CreateNewObject(templateMessage);
                msg.UserId = user.Id;
                msg.UserOwnerId = userFromId;
                msg.SendDate = DateTime.Now;
                msg.SendTime = DateTime.Now.TimeOfDay;
                msg.IsSend = true;
                msg.HasRead = true;
                msg.PriorityId = wa.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>(Analitic.SYSTEM_PRIORITY_NORMAL).Id;
                msg.MyCompanyId = user.MyCompanyId;
                msg.Memo = string.Format("Требуется Ваша подпись в документе {0} от {1} номер {2}", doc.Name, doc.Date, doc.Number);
                msg.Save();
            }
        }


    }
}