using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects.Workflows.Web
{
    /// <summary>
    /// ��������� ��������� ��� ������� ���������� ��������� 
    /// </summary>
    public class ActivitySendMessageOnDocumentSignRequest: CodeActivity
    {
        /// <summary>
        /// �����������
        /// </summary>
        public ActivitySendMessageOnDocumentSignRequest()
            : base()
        {
            this.DisplayName = "��������� ��������� ��� ������� ���������� ���������";
        }
        /// <summary>
        /// ������� �������
        /// </summary>
        [RequiredArgument]
        public InArgument<Workarea> CurrentWorkarea { get; set; }
        /// <summary>
        /// ������������� ���������
        /// </summary>
        [RequiredArgument]
        public InArgument<int> DocumentId { get; set; }

        /// <summary>
        /// ������������� ����������
        /// </summary>
        [RequiredArgument]
        public InArgument<int> AgentId { get; set; }

        /// <summary>
        /// ������������� ������������ �����������
        /// </summary>
        [RequiredArgument]
        public InArgument<int> UserFromId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            // ������������ ��������� ��� ������������

            Workarea wa = CurrentWorkarea.Get(context);
            int agentId = AgentId.Get(context);
            int userFromId = UserFromId.Get(context);
            int documentId = DocumentId.Get(context);
            Document doc = wa.GetObject<Document>(documentId);
            // ����� ������������� ��� ������� ����������� ���������
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
                msg.Memo = string.Format("��������� ���� ������� � ��������� {0} �� {1} ����� {2}", doc.Name, doc.Date, doc.Number);
                msg.Save();
            }
        }


    }
}