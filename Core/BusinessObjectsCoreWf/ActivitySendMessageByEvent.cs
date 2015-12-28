using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Activities;

namespace BusinessObjects.Workflows
{
    /// <summary>
    /// Отправка сообщения пользователю
    /// </summary>
    /// <remarks>Процесс для автоматического формирования сообщения при наступлении соответсвующего события.
    /// Сообщение формируется:
    /// для пользователя-получателя указанного в событии, 
    /// в качестве наименования используется наименовние события,
    /// в качестве краткого сообщения используется полное наименование события,
    /// в качестве полного текста сообщения используется примечание события,
    /// состояние сообщения - по шаблону,
    /// флаг - только чтение.
    /// 
    /// По окончании выполнения процесса данного события флаг события соответствует "Только чтение", 
    /// статус - "Завершено".
    /// </remarks>
    [ToolboxBitmap(typeof(BusinessObjects.Workflows.ActivityTaskSetDone), "NoteHS.bmp")]
    public sealed class ActivitySendMessageByEvent : CodeActivity
    {
        public ActivitySendMessageByEvent()
            : base()
        {
            this.DisplayName = "Отправка сообщения пользователю";
        }
        // Define an activity input argument of type string
        [RequiredArgument]
        public InArgument<Event> CurrentObject { get; set; }
        // Define an activity input argument of type string
        public InArgument<string> XmlData { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Рабочая область
            Workarea wa = CurrentObject.Get(context).Workarea;
            // Текущее событие
            Event owner = CurrentObject.Get(context);

            owner.StartOn = DateTime.Today;
            owner.StartOnTime = DateTime.Now.TimeOfDay;
            owner.StatusId = wa.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>(Event.SYSTEM_EVENT_PROCESS).Id;
            owner.Save();

            Message msgTml = wa.GetTemplates<Message>().First(s => s.KindId == Message.KINDID_USER);
            Message newMessage = wa.CreateNewObject<Message>(msgTml);
            newMessage.UserOwnerId = owner.UserOwnerId;
            newMessage.UserId = owner.UserToId;
            newMessage.Name = owner.Name;
            newMessage.Memo = owner.Memo;
            newMessage.IsSend = true;
            newMessage.NameFull = owner.NameFull;
            newMessage.SetFlagReadOnly();
            newMessage.Save();


            Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_MESSAGE_USERS);
            h.ContentAdd(newMessage);

            owner.StatusId = wa.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>(Event.SYSTEM_EVENT_END).Id;
            owner.EndOn = DateTime.Today;
            owner.EndOnTime = DateTime.Now.TimeOfDay;
            owner.SetFlagReadOnly();
            
            owner.Save();

        }
    }
}
