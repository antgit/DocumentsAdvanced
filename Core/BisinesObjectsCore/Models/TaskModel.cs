using System;

namespace BusinessObjects.Models
{
    /// <summary>
    /// ������ ������
    /// </summary>
    public class TaskModel : BaseModel<Task>
    {
        /// <summary>�����������</summary>
        public TaskModel()
        {
        }
        /// <summary>���������� ������</summary>
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
        #region ��������
        public string AgentToName { get; set; }
        public string AgentOwnerName { get; set; }
        public string PriorityName { get; set; }
        public string TaskStateName { get; set; }

        /// <summary>������������� �����������, �������� ����������� ������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �����������, �������� ����������� ������</summary>
        public string MyCompanyName { get; set; }

        /// <summary>���� ��������� ������ ������</summary>
        public DateTime? DateStartPlan { get; set; }

        /// <summary>���� ������ ������</summary>
        public DateTime? DateStart { get; set; }

        /// <summary>���� ��������� ��������� ������</summary>
        public DateTime? DateEndPlan { get; set; }

        /// <summary>���� ��������� ������</summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>����� ��������� ������ ������</summary>
        public TimeSpan? DateStartPlanTime { get; set; }

        /// <summary>����� ������ ������</summary>
        public TimeSpan? DateStartTime { get; set; }

        /// <summary>����� ��������� ��������� ������</summary>
        public TimeSpan? DateEndPlanTime { get; set; }

        /// <summary>����� ��������� ������</summary>
        public TimeSpan? DateEndTime { get; set; }

        /// <summary>������������� ������ ������, ������������</summary>
        public int UserOwnerId { get; set; }

        /// <summary>������������� ����������</summary>
        public int PriorityId { get; set; }

        /// <summary>������������� ��������� ������</summary>
        public int TaskStateId { get; set; }

        /// <summary>����� ������</summary>
        public int TaskNumber { get; set; }

        /// <summary>������� ����������</summary>
        public int DonePersent { get; set; }

        /// <summary>������������� ������������ �������������� �� ���������� ������</summary>
        public int UserToId { get; set; }

        /// <summary>������� ������������, �������� �������</summary>
        public bool InPrivate { get; set; }

        /// <summary>���������� � ���� ������</summary>
        public string MemoTxt { get; set; }
        #endregion

        public DateTime? DateEndPlanTimeAsDate { get; set; }
        public DateTime? DateEndTimeAsDate { get; set; }
        public DateTime? DateStartPlanTimeAsDate { get; set; }
        public DateTime? DateStartTimeAsDate { get; set; }
    }
}