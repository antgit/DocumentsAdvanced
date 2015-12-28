using System;

namespace BusinessObjects.Models
{
    /// <summary>
    /// Модель задачи
    /// </summary>
    public class TaskModel : BaseModel<Task>
    {
        /// <summary>Конструктор</summary>
        public TaskModel()
        {
        }
        /// <summary>Заполнение данных</summary>
        public override void GetData(Task value)
        {
            base.GetData(value);
            MyCompanyId = value.MyCompanyId;
            MyCompanyName = value.MyCompanyId != 0 ? value.MyCompany.Name : string.Empty;
            DateStartPlan = value.DateStartPlan;
            DateStart = value.DateStart;
            DateEndPlan = value.DateEndPlan;
            DateEnd = value.DateEnd;
            DateStartPlanTime = value.DateStartPlanTime;
            DateStartTime = value.DateStartTime;
            DateEndPlanTime = value.DateEndPlanTime;
            DateEndTime = value.DateEndTime;
            UserOwnerId = value.UserOwnerId;
            PriorityId = value.PriorityId;
            TaskStateId = value.TaskStateId;
            TaskNumber = value.TaskNumber;
            DonePersent = value.DonePersent;
            UserToId = value.UserToId;
            InPrivate = value.InPrivate;
            MemoTxt = value.MemoTxt;
            AgentOwnerName = value.AgentOwnerName;
            AgentToName = value.AgentToName;
            PriorityName = value.PriorityId != 0 ? value.Priority.Name : string.Empty;
            TaskStateName = value.TaskStateId != 0 ? value.TaskState.Name : string.Empty;
        }
        #region Свойства
        public string AgentToName { get; set; }
        public string AgentOwnerName { get; set; }
        public string PriorityName { get; set; }
        public string TaskStateName { get; set; }

        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование предприятия, которому принадлежит объект</summary>
        public string MyCompanyName { get; set; }

        /// <summary>Дата планового старта задачи</summary>
        public DateTime? DateStartPlan { get; set; }

        /// <summary>Дата начала задачи</summary>
        public DateTime? DateStart { get; set; }

        /// <summary>Дата планового окончания задачи</summary>
        public DateTime? DateEndPlan { get; set; }

        /// <summary>Дата окончания задачи</summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>Время планового старта задачи</summary>
        public TimeSpan? DateStartPlanTime { get; set; }

        /// <summary>Время старта задачи</summary>
        public TimeSpan? DateStartTime { get; set; }

        /// <summary>Время планового окончания задачи</summary>
        public TimeSpan? DateEndPlanTime { get; set; }

        /// <summary>Время окончания задачи</summary>
        public TimeSpan? DateEndTime { get; set; }

        /// <summary>Идентификатор автора задачи, постановщика</summary>
        public int UserOwnerId { get; set; }

        /// <summary>Идентификатор приоритета</summary>
        public int PriorityId { get; set; }

        /// <summary>Идентификатор состояния задачи</summary>
        public int TaskStateId { get; set; }

        /// <summary>Номер задачи</summary>
        public int TaskNumber { get; set; }

        /// <summary>Процент выполнения</summary>
        public int DonePersent { get; set; }

        /// <summary>Идентификатор пользователя ответственного за исполнение задачи</summary>
        public int UserToId { get; set; }

        /// <summary>Признак собственного, частного задания</summary>
        public bool InPrivate { get; set; }

        /// <summary>Примечание в виде текста</summary>
        public string MemoTxt { get; set; }
        #endregion

        public DateTime? DateEndPlanTimeAsDate { get; set; }
        public DateTime? DateEndTimeAsDate { get; set; }
        public DateTime? DateStartPlanTimeAsDate { get; set; }
        public DateTime? DateStartTimeAsDate { get; set; }
    }
}