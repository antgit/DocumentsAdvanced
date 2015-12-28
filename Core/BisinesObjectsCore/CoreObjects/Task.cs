using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Задача"</summary>
    internal struct TaskStruct
    {
        /// <summary>
        /// Дата планового старта задачи
        /// </summary>
        public DateTime? DateStartPlan;

        /// <summary>
        /// Дата начала задачи
        /// </summary>
        public DateTime? DateStart;

        /// <summary>
        /// Дата планового окончания задачи
        /// </summary>
        public DateTime? DateEndPlan;

        /// <summary>
        /// Дата окончания задачи
        /// </summary>
        public DateTime? DateEnd;

        /// <summary>
        /// Время планового старта задачи
        /// </summary>
        public TimeSpan? DateStartPlanTime;

        /// <summary>
        /// Время старта задачи
        /// </summary>
        public TimeSpan? DateStartTime;

        /// <summary>
        /// Время планового окончания задачи
        /// </summary>
        public TimeSpan? DateEndPlanTime;

        /// <summary>
        /// Время окончания задачи
        /// </summary>
        public TimeSpan? DateEndTime;

        /// <summary>
        /// Идентификатор автора задачи, постановщика
        /// </summary>
        public int UserOwnerId;

        /// <summary>
        /// Идентификатор приоритета
        /// </summary>
        public int PriorityId;

        /// <summary>
        /// Идентификатор состояния задачи
        /// </summary>
        public int TaskStateId;

        /// <summary>
        /// Номер задачи
        /// </summary>
        public int TaskNumber;

        /// <summary>
        /// Процент выполнения
        /// </summary>
        public int DonePersent;

        /// <summary>
        /// Идентификатор пользователя ответственного за исполнение задачи
        /// </summary>
        public int UserToId;

        /// <summary>
        /// Признак собственного, частного задания
        /// </summary>
        public bool InPrivate;

        /// <summary>
        /// Примечание в виде текста
        /// </summary>
        public string MemoTxt;

        /// <summary>
        /// Идентификатор предприятия, которому принадлежит объект
        /// </summary>
        public int MyCompanyId;
    }
    /// <summary>Задача</summary>
    public sealed class Task : BaseCore<Task>, IChains<Task>, IReportChainSupport, IEquatable<Task>,
                               IComparable, IComparable<Task>,
                               IFacts<Task>,
                               IChainsAdvancedList<Task, FileData>,
                               IChainsAdvancedList<Task, Note>,
                               IChainsAdvancedList<Task, Message>,
                               IChainsAdvancedList<Task, Event>,
                               IChainsAdvancedList<Task, Document>,
                               ICodes<Task>, IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Системная задача, соответствует значению 1</summary>
        public const int KINDVALUE_TASKSYSTEM = 1;
        /// <summary>Пользовательская задача, соответствует значению 2</summary>
        public const int KINDVALUE_TASKUSER = 2;
        /// <summary>Расширенная задача, соответствует значению 3</summary>
        public const int KINDVALUE_TASKADVANCED = 3;

        /// <summary>Системная задача, соответствует значению 6291457</summary>
        public const int KINDID_TASKSYSTEM = 6291457;
        /// <summary>Пользовательская задача, соответствует значению 6291458</summary>
        public const int KINDID_TASKUSER = 6291458;
        /// <summary>Расширенная задача, соответствует значению 6291459</summary>
        public const int KINDID_TASKADVANCED = 6291459;
        
        // ReSharper restore InconsistentNaming

        #endregion
        #region Константы состояний задач
        // ReSharper disable InconsistentNaming
        /// <summary>Отложено</summary>
        public const string TASK_STATE_DELAY = "TASK_STATE_DELAY";
        /// <summary>Выполняется</summary>
        public const string TASK_STATE_INPROGRESS = "TASK_STATE_INPROGRESS";
        /// <summary>Выполнено</summary>
        public const string TASK_STATE_DONE = "TASK_STATE_DONE";
        /// <summary>Не выполнено</summary>
        public const string TASK_STATE_NOTDONE = "TASK_STATE_NOTDONE";
        /// <summary>Рассматривается</summary>
        public const string TASK_STATE_VIEW = "TASK_STATE_VIEW";
        /// <summary>Не начата</summary>
        public const string TASK_STATE_NOTSTART = "TASK_STATE_NOTSTART";
        /// <summary>Передана</summary>
        public const string TASK_STATE_REASIGN = "TASK_STATE_REASIGN";
        // ReSharper restore InconsistentNaming
        #endregion
        bool IEquatable<Task>.Equals(Task other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Task otherObj = (Task)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Task other)
        {
            return Id.CompareTo(other.Id);
        }
        protected override void CopyValue(Task template)
        {
            base.CopyValue(template);
            DateStartPlan = template.DateStartPlan;
            DateStart = template.DateStart;
            DateEndPlan = template.DateEndPlan;
            DateEnd = template.DateEnd;
            DateStartPlanTime = template.DateStartPlanTime;
            DateStartTime = template.DateStartTime;
            DateEndPlanTime = template.DateEndPlanTime;
            DateEndTime = template.DateEndTime;
            //UserOwnerId = template.UserOwnerId;
            PriorityId = template.PriorityId;
            TaskStateId = template.TaskStateId;
            TaskNumber = template.TaskNumber;
            DonePersent = template.DonePersent;
            MyCompanyId = template.MyCompanyId;
            //UserToId = template.UserToId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit"></param>
        /// <returns></returns>
        protected override Task Clone(bool endInit)
        {
            Task obj = base.Clone(false);
            obj.DateStartPlan = DateStartPlan;
            obj.DateStart = DateStart;
            obj.DateEndPlan = DateEndPlan;
            obj.DateEnd = DateEnd;
            obj.DateStartPlanTime = DateStartPlanTime;
            obj.DateStartTime = DateStartTime;
            obj.DateEndPlanTime = DateEndPlanTime;
            obj.DateEndTime = DateEndTime;
            obj.UserOwnerId = UserOwnerId;
            obj.PriorityId = PriorityId;
            obj.TaskStateId = TaskStateId;
            obj.TaskNumber = TaskNumber;
            obj.DonePersent = DonePersent;
            obj.UserToId = UserToId;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }

        /// <summary>
        /// Сравнение объекта для службы обмена данными
        /// </summary>
        /// <returns></returns>
        public bool CompareExchange(Task value)
        {
            if (!base.CompareExchange(value))
            {
                return false;
            }

            //if (value.TimeDelay != TimeDelay)
            //    return false;
            //if (value.AmmountTrust != AmmountTrust)
            //    return false;
            //if (!StringNullCompare(value.CodeTax, CodeTax))
            //    return false;
            //if (!StringNullCompare(value.AddressLegal, AddressLegal))
            //    return false;
            //if (!StringNullCompare(value.AddressPhysical, AddressPhysical))
            //    return false;
            //if (!StringNullCompare(value.Phone, Phone))
            //    return false;


            return true;
        }
        /// <summary>Конструктор</summary>
        public Task()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Task;
        }
        #region Свойства


        private DateTime? _dateStartPlan;
        /// <summary>
        /// Дата планового старта задачи
        /// </summary>
        public DateTime? DateStartPlan
        {
            get { return _dateStartPlan; }
            set
            {
                if (value == _dateStartPlan) return;
                OnPropertyChanging(GlobalPropertyNames.DateStartPlan);
                _dateStartPlan = value;
                OnPropertyChanged(GlobalPropertyNames.DateStartPlan);
            }
        }

        private DateTime? _dateStart;
        /// <summary>
        /// Дата начала задачи
        /// </summary>
        public DateTime? DateStart
        {
            get { return _dateStart; }
            set
            {
                if (value == _dateStart) return;
                OnPropertyChanging(GlobalPropertyNames.DateStart);
                _dateStart = value;
                OnPropertyChanged(GlobalPropertyNames.DateStart);
            }
        }

        private DateTime? _dateEndPlan;
        /// <summary>
        /// Дата планового окончания задачи
        /// </summary>
        public DateTime? DateEndPlan
        {
            get { return _dateEndPlan; }
            set
            {
                if (value == _dateEndPlan) return;
                OnPropertyChanging(GlobalPropertyNames.DateEndPlan);
                _dateEndPlan = value;
                OnPropertyChanged(GlobalPropertyNames.DateEndPlan);
            }
        }

        private DateTime? _dateEnd;
        /// <summary>
        /// Дата окончания задачи
        /// </summary>
        public DateTime? DateEnd
        {
            get { return _dateEnd; }
            set
            {
                if (value == _dateEnd) return;
                OnPropertyChanging(GlobalPropertyNames.DateEnd);
                _dateEnd = value;
                OnPropertyChanged(GlobalPropertyNames.DateEnd);
            }
        }

        private TimeSpan? _dateStartPlanTime;
        /// <summary>
        /// Время планового старта задачи
        /// </summary>
        public TimeSpan? DateStartPlanTime
        {
            get { return _dateStartPlanTime; }
            set
            {
                if (value == _dateStartPlanTime) return;
                OnPropertyChanging(GlobalPropertyNames.DateStartPlanTime);
                _dateStartPlanTime = value;
                OnPropertyChanged(GlobalPropertyNames.DateStartPlanTime);
            }
        }

        private TimeSpan? _dateStartTime;
        /// <summary>
        /// Время старта задачи
        /// </summary>
        public TimeSpan? DateStartTime
        {
            get { return _dateStartTime; }
            set
            {
                if (value == _dateStartTime) return;
                OnPropertyChanging(GlobalPropertyNames.DateStartTime);
                _dateStartTime = value;
                OnPropertyChanged(GlobalPropertyNames.DateStartTime);
            }
        }
        
        private TimeSpan? _dateEndPlanTime;
        /// <summary>
        /// Время планового окончания задачи
        /// </summary>
        public TimeSpan? DateEndPlanTime
        {
            get { return _dateEndPlanTime; }
            set
            {
                if (value == _dateEndPlanTime) return;
                OnPropertyChanging(GlobalPropertyNames.DateEndPlanTime);
                _dateEndPlanTime = value;
                OnPropertyChanged(GlobalPropertyNames.DateEndPlanTime);
            }
        }
        
        private TimeSpan? _dateEndTime;
        /// <summary>
        /// Время окончания задачи
        /// </summary>
        public TimeSpan? DateEndTime
        {
            get { return _dateEndTime; }
            set
            {
                if (value == _dateEndTime) return;
                OnPropertyChanging(GlobalPropertyNames.DateEndTime);
                _dateEndTime = value;
                OnPropertyChanged(GlobalPropertyNames.DateEndTime);
            }
        }
        
        private int _userOwnerId;
        /// <summary>
        /// Идентификатор автора задачи, постановщика
        /// </summary>
        public int UserOwnerId
        {
            get { return _userOwnerId; }
            set
            {
                if (value == _userOwnerId) return;
                OnPropertyChanging(GlobalPropertyNames.UserOwnerId);
                _userOwnerId = value;
                OnPropertyChanged(GlobalPropertyNames.UserOwnerId);
            }
        }
        
        private Uid _userOwner;
        /// <summary>
        /// Автор задачи, постановщик
        /// </summary>
        public Uid UserOwner
        {
            get
            {
                if (_userOwnerId == 0)
                    return null;
                if (_userOwner == null)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                else if (_userOwner.Id != _userOwnerId)
                    _userOwner = Workarea.Cashe.GetCasheData<Uid>().Item(_userOwnerId);
                return _userOwner;
            }
            set
            {
                if (_userOwner == value) return;
                OnPropertyChanging("UserOwner");
                _userOwner = value;
                _userOwnerId = _userOwner == null ? 0 : _userOwner.Id;
                OnPropertyChanged("UserOwner");
            }
        }
        
        private int _priorityId;
        /// <summary>
        /// Идентификатор приоритета
        /// </summary>
        public int PriorityId
        {
            get { return _priorityId; }
            set
            {
                if (value == _priorityId) return;
                OnPropertyChanging(GlobalPropertyNames.PriorityId);
                _priorityId = value;
                OnPropertyChanged(GlobalPropertyNames.PriorityId);
            }
        }
        
        private Analitic _priority;
        /// <summary>
        /// Приоретет задачи
        /// </summary>
        public Analitic Priority
        {
            get
            {
                if (_priorityId == 0)
                    return null;
                if (_priority == null)
                    _priority = Workarea.Cashe.GetCasheData<Analitic>().Item(_priorityId);
                else if (_priority.Id != _priorityId)
                    _priority = Workarea.Cashe.GetCasheData<Analitic>().Item(_priorityId);
                return _priority;
            }
            set
            {
                if (_priority == value) return;
                OnPropertyChanging("Priority");
                _priority = value;
                _priorityId = _priority == null ? 0 : _priority.Id;
                OnPropertyChanged("Priority");
            }
        }
        
        private int _taskStateId;
        /// <summary>
        /// Идентификатор состояния задачи
        /// </summary>
        public int TaskStateId
        {
            get { return _taskStateId; }
            set
            {
                if (value == _taskStateId) return;
                OnPropertyChanging(GlobalPropertyNames.TaskStateId);
                _taskStateId = value;
                OnPropertyChanged(GlobalPropertyNames.TaskStateId);
            }
        }
        
        private Analitic _taskState;
        /// <summary>
        /// Состояние задачи
        /// </summary>
        public Analitic TaskState
        {
            get
            {
                if (_taskStateId == 0)
                    return null;
                if (_taskState == null)
                    _taskState = Workarea.Cashe.GetCasheData<Analitic>().Item(_taskStateId);
                else if (_taskState.Id != _taskStateId)
                    _taskState = Workarea.Cashe.GetCasheData<Analitic>().Item(_taskStateId);
                return _taskState;
            }
            set
            {
                if (_taskState == value) return;
                OnPropertyChanging("TaskState");
                _taskState = value;
                _taskStateId = _taskState == null ? 0 : _taskState.Id;
                OnPropertyChanged("TaskState");
            }
        }
        
        private int _taskNumber;
        /// <summary>
        /// Номер задачи
        /// </summary>
        public int TaskNumber
        {
            get { return _taskNumber; }
            set
            {
                if (value == _taskNumber) return;
                OnPropertyChanging(GlobalPropertyNames.TaskNumber);
                _taskNumber = value;
                OnPropertyChanged(GlobalPropertyNames.TaskNumber);
            }
        }
        
        private int _donePersent;
        /// <summary>
        /// Процент выполнения
        /// </summary>
        public int DonePersent
        {
            get { return _donePersent; }
            set
            {
                if (value == _donePersent) return;
                OnPropertyChanging(GlobalPropertyNames.DonePersent);
                _donePersent = value;
                OnPropertyChanged(GlobalPropertyNames.DonePersent);
            }
        }
        
        private int _userToId;
        /// <summary>
        /// Идентификатор пользователя ответственного за исполнение задачи
        /// </summary>
        public int UserToId
        {
            get { return _userToId; }
            set
            {
                if (value == _userToId) return;
                OnPropertyChanging(GlobalPropertyNames.UserToId);
                _userToId = value;
                OnPropertyChanged(GlobalPropertyNames.UserToId);
            }
        }
        private Uid _userTo;
        /// <summary>
        /// Пользователь исполнитель
        /// </summary>
        public Uid UserTo
        {
            get
            {
                if (_userToId == 0)
                    return null;
                if (_userTo == null)
                    _userTo = Workarea.Cashe.GetCasheData<Uid>().Item(_userToId);
                else if (_userTo.Id != _userToId)
                    _userTo = Workarea.Cashe.GetCasheData<Uid>().Item(_userToId);
                return _userTo;
            }
            set
            {
                if (_userTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.UserTo);
                _userTo = value;
                _userToId = _userTo == null ? 0 : _userTo.Id;
                OnPropertyChanged(GlobalPropertyNames.UserTo);
            }
        }

        /// <summary>
        /// Наименование сотрудника исполнителя (по данным о пользователе, являющегося исполнителем)
        /// </summary>
        public string AgentToName
        {
            get
            {
                if(_userToId!=0)
                {
                    return UserTo.AgentId == 0 ? string.Empty : _userTo.Agent.Name;
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// Наименование сотрудника владельца (по данным о пользователе, являющегося автором)
        /// </summary>
        public string AgentOwnerName
        {
            get
            {
                if (_userOwnerId != 0)
                {
                    return UserOwner.AgentId == 0 ? string.Empty : _userOwner.Agent.Name;
                }
                return string.Empty;
            }
        }

        private bool _inPrivate;
        /// <summary>
        /// Признак собственного, частного задания
        /// </summary>
        public bool InPrivate
        {
            get { return _inPrivate; }
            set
            {
                if (value == _inPrivate) return;
                OnPropertyChanging(GlobalPropertyNames.InPrivate);
                _inPrivate = value;
                OnPropertyChanged(GlobalPropertyNames.InPrivate);
            }
        }

        private string _memoTxt;
        /// <summary>
        /// Примечание в виде текста
        /// </summary>
        public string MemoTxt
        {
            get { return _memoTxt; }
            set
            {
                if (value == _memoTxt) return;
                OnPropertyChanging(GlobalPropertyNames.MemoTxt);
                _memoTxt = value;
                OnPropertyChanged(GlobalPropertyNames.MemoTxt);
            }
        }

        private int _myCompanyId;
        /// <summary>
        /// Идентификатор предприятия, которому принадлежит объект
        /// </summary>
        public int MyCompanyId
        {
            get { return _myCompanyId; }
            set
            {
                if (value == _myCompanyId) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompanyId);
                _myCompanyId = value;
                OnPropertyChanged(GlobalPropertyNames.MyCompanyId);
            }
        }

        private Agent _myCompany;
        /// <summary>
        /// Моя компания, предприятие которому принадлежит объект
        /// </summary>
        public Agent MyCompany
        {
            get
            {
                if (_myCompanyId == 0)
                    return null;
                if (_myCompany == null)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                else if (_myCompany.Id != _myCompanyId)
                    _myCompany = Workarea.Cashe.GetCasheData<Agent>().Item(_myCompanyId);
                return _myCompany;
            }
            set
            {
                if (_myCompany == value) return;
                OnPropertyChanging(GlobalPropertyNames.MyCompany);
                _myCompany = value;
                _myCompanyId = _myCompany == null ? 0 : _myCompany.Id;
                OnPropertyChanged(GlobalPropertyNames.MyCompany);
            }
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (_userOwnerId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_TASKUSEROWNER"));
            if (_priorityId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_TASKPRIORITY"));
            if (_taskStateId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_TASKSTATE"));
            if (_userToId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_TASKUSERTO"));
            if (_donePersent < 0 | _donePersent>100 )
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_TASKDONEPERSENTRANGE"));
        }

        public void SetStateDone()
        {
            Analitic an = Workarea.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>("TASK_STATE_DONE");
            if(an!=null)
            {
                TaskStateId = an.Id;
            }
        }
        public bool IsTaskStateDone()
        {
            Analitic an = Workarea.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>("TASK_STATE_DONE");
            if (an != null)
            {
                return TaskStateId == an.Id;
            }
            return false;
        }
        /// <summary>
        /// Установить состояние задачи в "Передана", создать новую задачу и связать текущую задачу и вновь созданную.
        /// </summary>
        /// <returns></returns>
        public Task Reasign()
        {
            //TEMPLATE_TASK_KINDVALUE2 
            TaskStateId = Workarea.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>(Task.TASK_STATE_REASIGN).Id;
            Save();
            
            Task newTask = Workarea.CreateNewObject<Task>(this.GetTemplate());
            newTask.UserOwnerId = UserOwnerId;
            newTask.UserToId = UserToId;
            newTask.CopyValue(this);
            newTask.TaskStateId = Workarea.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>(Task.TASK_STATE_NOTSTART).Id;
            newTask.DateStartPlan = DateTime.Now;
            newTask.DateStartPlanTime = DateTime.Now.TimeOfDay;
            newTask.DateStart = DateTime.Now;
            newTask.DateStartTime = DateTime.Now.TimeOfDay;


            newTask.Save();
            Hierarchy h = FirstHierarchy();
            if (h != null)
                h.ContentAdd<Task>(newTask);
            ChainKind kind = Workarea.CollectionChainKinds.FirstOrDefault(
                s => s.Code == ChainKind.TREE && s.FromEntityId == EntityId && s.ToEntityId == EntityId);
            Chain<Task> link = new Chain<Task>(this) { RightId = newTask.Id, KindId = kind.Id, StateId = State.STATEACTIVE };
            link.Save();
            return newTask;
        }

        /// <summary>
        /// Формирование сообщения о поставленной задаче 
        /// </summary>
        public bool CreateMessageInfo()
        {
            if (this.UserOwnerId == UserToId)
                return false;
            // шаблон сообщения
            Message tmlMessage = Workarea.GetTemplates<Message>().FirstOrDefault(s => s.Code == Message.MESSAGE_TEMPLATE_TASKCREATE);
            ChainKind chainKind = Workarea.CollectionChainKinds.FirstOrDefault(
                f => f.FromEntityId == (int) WhellKnownDbEntity.Task && f.ToEntityId == (int) WhellKnownDbEntity.Message);
            if(tmlMessage!=null && chainKind !=null)
            {
                List<Message> coll = ChainAdvanced<Task, Message>.GetChainSourceList<Task, Message>(this, chainKind.Id, State.STATEACTIVE);

                if(coll.Exists(s=>s.TemplateId==tmlMessage.Id))
                {
                    return false;
                }
                Message newMessage = Workarea.CreateNewObject<Message>(tmlMessage);
                newMessage.SendDate = DateTime.Now;
                newMessage.SendTime = DateTime.Now.TimeOfDay;
                newMessage.IsSend = true;
                newMessage.Code = string.Empty;
                if(string.IsNullOrEmpty(newMessage.NameFull))
                {
                    newMessage.NameFull = "Вам поставлена новая задача для выполнения!";
                }
                else
                {
                    SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>(SystemParameter.WEBROOTSERVER);
                    string webLink = string.Format("{0}/Kb/Task/Edit/{1}", prm.ValueString, Id);
                    
                    newMessage.NameFull = newMessage.NameFull.Replace("{AGENTOWNERNAME}", this.AgentOwnerName).Replace("{AGENTTONAME}", AgentToName).Replace("{WEBLINK}", webLink);
                    newMessage.UserId = this.UserToId;
                    newMessage.UserOwnerId = this.UserOwnerId;
                    newMessage.HasRead = true;
                    newMessage.Save();

                    ChainAdvanced<Task, Message> chain = new ChainAdvanced<Task, Message>(this);
                    chain.KindId = chainKind.Id;
                    chain.Right = newMessage;
                    chain.StateId = State.STATEACTIVE;
                    chain.Save();

                    Hierarchy h = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_MESSAGE_USERS);
                    if(h!=null)
                    {
                        h.ContentAdd(newMessage, true);
                    }
                    
                }
            }
            return true;
        }
        /// <summary>
        /// Формирование сообщения о поставленной задаче 
        /// </summary>
        public void CreateMessageUpdate()
        {
            if (this.UserOwnerId == UserToId)
                return;
            // шаблон сообщения
            Message tmlMessage = Workarea.GetTemplates<Message>().FirstOrDefault(s => s.Code == Message.MESSAGE_TEMPLATE_TASKUPDATE);
            ChainKind chainKind = Workarea.CollectionChainKinds.FirstOrDefault(
                f => f.FromEntityId == (int)WhellKnownDbEntity.Task && f.ToEntityId == (int)WhellKnownDbEntity.Message);
            if (tmlMessage != null && chainKind != null)
            {
                Message newMessage = Workarea.CreateNewObject<Message>(tmlMessage);
                newMessage.SendDate = DateTime.Now;
                newMessage.SendTime = DateTime.Now.TimeOfDay;
                newMessage.IsSend = true;
                newMessage.Code = string.Empty;
                if (string.IsNullOrEmpty(newMessage.NameFull))
                {
                    newMessage.NameFull = "Задача поставленная вам изменилась!";
                }
                else
                {
                    SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>(SystemParameter.WEBROOTSERVER);
                    string webLink = string.Format("{0}/Kb/Task/Edit/{1}", prm.ValueString, Id);

                    newMessage.NameFull = newMessage.NameFull.Replace("{AGENTOWNERNAME}", this.AgentOwnerName).Replace("{AGENTTONAME}", AgentToName).Replace("{WEBLINK}", webLink);
                    newMessage.UserId = this.UserToId;
                    newMessage.UserOwnerId = this.UserOwnerId;
                    newMessage.HasRead = true;
                    newMessage.Save();

                    ChainAdvanced<Task, Message> chain = new ChainAdvanced<Task, Message>(this);
                    chain.KindId = chainKind.Id;
                    chain.Right = newMessage;
                    chain.StateId = State.STATEACTIVE;
                    chain.Save();

                    Hierarchy h = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_MESSAGE_USERS);
                    if (h != null)
                    {
                        h.ContentAdd(newMessage, true);
                    }
                }

            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if(!CreateMessageInfo())
                CreateMessageUpdate();
        }

        #region Состояние
        TaskStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new TaskStruct
                {
                    DateEnd = _dateEnd,
                    DateEndPlan = _dateEndPlan,
                    DateEndPlanTime = _dateEndPlanTime,
                    DateEndTime = _dateEndTime,
                    DateStart = _dateStart,
                    DateStartPlan = _dateStartPlan,
                    DateStartPlanTime = _dateStartPlanTime,
                    DateStartTime = _dateStartTime,
                    DonePersent = _donePersent,
                    InPrivate = _inPrivate,
                    MemoTxt = _memoTxt,
                    MyCompanyId = _myCompanyId,
                    PriorityId = _priorityId,
                    TaskNumber = _taskNumber,
                    TaskStateId = _taskStateId,
                    UserOwnerId = _userOwnerId,
                    UserToId = _userToId
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            DateEnd = _baseStruct.DateEnd;
            DateEndPlan = _baseStruct.DateEndPlan;
            DateEndPlanTime = _baseStruct.DateEndPlanTime;
            DateEndTime = _baseStruct.DateEndTime;
            DateStart = _baseStruct.DateStart;
            DateStartPlan = _baseStruct.DateStartPlan;
            DateStartPlanTime = _baseStruct.DateStartPlanTime;
            DateStartTime = _baseStruct.DateStartTime;
            DonePersent = _baseStruct.DonePersent;
            InPrivate = _baseStruct.InPrivate;
            MemoTxt = _baseStruct.MemoTxt;
            MyCompanyId = _baseStruct.MyCompanyId;
            PriorityId = _baseStruct.PriorityId;
            TaskNumber = _baseStruct.TaskNumber;
            TaskStateId = _baseStruct.TaskStateId;
            UserOwnerId = _baseStruct.UserOwnerId;
            UserToId = _baseStruct.UserToId;
            IsChanged = false;
        }
        #endregion

        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _dateStartPlan = reader.IsDBNull(17) ? (DateTime?) null : reader.GetDateTime(17);
                _dateStart = reader.IsDBNull(18) ? (DateTime?) null : reader.GetDateTime(18);
                _dateEndPlan = reader.IsDBNull(19) ? (DateTime?) null : reader.GetDateTime(19);
                _dateEnd = reader.IsDBNull(20) ? (DateTime?) null : reader.GetDateTime(20);

                _dateStartPlanTime = reader.IsDBNull(21) ? (TimeSpan?) null : reader.GetTimeSpan(21);
                _dateStartTime = reader.IsDBNull(22) ? (TimeSpan?) null : reader.GetTimeSpan(22);
                _dateEndPlanTime = reader.IsDBNull(23) ? (TimeSpan?) null : reader.GetTimeSpan(23);
                _dateEndTime = reader.IsDBNull(24) ? (TimeSpan?) null : reader.GetTimeSpan(24);

                _userOwnerId = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                _priorityId = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                _taskStateId = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                _taskNumber = reader.IsDBNull(28) ? 0 : reader.GetInt32(28);
                _donePersent = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
                _userToId = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
                _inPrivate = reader.GetBoolean(31);
                _memoTxt = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);

                _myCompanyId = reader.IsDBNull(33) ? 0 : reader.GetInt32(33);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.DateStartPlan, SqlDbType.Date) { IsNullable = true };
            if (!_dateStartPlan.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateStartPlan;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateStart, SqlDbType.Date) { IsNullable = true };
            if (!_dateStart.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateStart;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateEndPlan, SqlDbType.Date) { IsNullable = true };
            if (!_dateEndPlan.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateEndPlan;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateEnd, SqlDbType.Date) { IsNullable = true };
            if (!_dateEnd.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateEnd;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateStartPlanTime, SqlDbType.Time) { IsNullable = true };
            if (!_dateStartPlanTime.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateStartPlanTime;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateStartTime, SqlDbType.Time) { IsNullable = true };
            if (!_dateStartTime.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateStartTime;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateEndPlanTime, SqlDbType.Time) { IsNullable = true };
            if (!_dateEndPlanTime.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateEndPlanTime;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateEndTime, SqlDbType.Time) { IsNullable = true };
            if (!_dateEndTime.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateEndTime;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _userOwnerId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PriorityId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _priorityId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TaskStateId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _taskStateId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.TaskNumber, SqlDbType.Int) { IsNullable = false };
            prm.Value = _taskNumber;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DonePersent , SqlDbType.Int) { IsNullable = false };
            prm.Value = _donePersent;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserToId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _userToId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.InPrivate, SqlDbType.Bit) { IsNullable = false };
            prm.Value = _inPrivate;
            sqlCmd.Parameters.Add(prm);


            prm = new SqlParameter(GlobalSqlParamNames.MemoTxt, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_memoTxt))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _memoTxt.Length;
                prm.Value = _memoTxt;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.NVarChar) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Task> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Task>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Task>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Task> IChains<Task>.SourceList(int chainKindId)
        {
            return Chain<Task>.GetChainSourceList(this, chainKindId);
        }
        List<Task> IChains<Task>.DestinationList(int chainKindId)
        {
            return Chain<Task>.DestinationList(this, chainKindId);

        }
        #endregion

        #region IChainsAdvancedList<Task,Note> Members

        List<IChainAdvanced<Task, Note>> IChainsAdvancedList<Task, Note>.GetLinks()
        {
            return ChainAdvanced<Task, Note>.CollectionSource(this);
        }

        List<IChainAdvanced<Task, Note>> IChainsAdvancedList<Task, Note>.GetLinks(int? kind)
        {
            return ChainAdvanced<Task, Note>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Task, Note>> GetLinkedNotes(int? kind = null)
        {
            return ChainAdvanced<Task, Note>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Task, Note>.GetChainView()
        {
            return ChainValueView.GetView<Task, Note>(this);
        }
        #endregion

        #region IChainsAdvancedList<Task,Message> Members

        List<IChainAdvanced<Task, Message>> IChainsAdvancedList<Task, Message>.GetLinks()
        {
            return ChainAdvanced<Task, Message>.CollectionSource(this);
        }

        List<IChainAdvanced<Task, Message>> IChainsAdvancedList<Task, Message>.GetLinks(int? kind)
        {
            return ChainAdvanced<Task, Message>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Task, Message>> GetLinkedMessage(int? kind = null)
        {
            return ChainAdvanced<Task, Message>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Task, Message>.GetChainView()
        {
            return ChainValueView.GetView<Task, Message>(this);
        }
        #endregion

        #region IChainsAdvancedList<Task,Event> Members

        List<IChainAdvanced<Task, Event>> IChainsAdvancedList<Task, Event>.GetLinks()
        {
            return ChainAdvanced<Task, Event>.CollectionSource(this);
        }

        List<IChainAdvanced<Task, Event>> IChainsAdvancedList<Task, Event>.GetLinks(int? kind)
        {
            return ChainAdvanced<Task, Event>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Task, Event>> GetLinkedEvents(int? kind = null)
        {
            return ChainAdvanced<Task, Event>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Task, Event>.GetChainView()
        {
            return ChainValueView.GetView<Task, Event>(this);
        }
        #endregion

        #region IChainsAdvancedList<Task,Document> Members

        List<IChainAdvanced<Task, Document>> IChainsAdvancedList<Task, Document>.GetLinks()
        {
            return ChainAdvanced<Task, Document>.CollectionSource(this);
        }

        List<IChainAdvanced<Task, Document>> IChainsAdvancedList<Task, Document>.GetLinks(int? kind)
        {
            return ChainAdvanced<Task, Document>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Task, Document>> GetLinkedDocuments(int? kind = null)
        {
            return ChainAdvanced<Task, Document>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Task, Document>.GetChainView()
        {
            return ChainValueView.GetView<Task, Document>(this);
        }
        #endregion

        #region IChainsAdvancedList<Task,FileData> Members

        List<IChainAdvanced<Task, FileData>> IChainsAdvancedList<Task, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Task, FileData>)this).GetLinks(70);
        }

        List<IChainAdvanced<Task, FileData>> IChainsAdvancedList<Task, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Task, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Task, FileData>(this);
        }
        public List<IChainAdvanced<Task, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Task, FileData>> collection = new List<IChainAdvanced<Task, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Task>().Entity.FindMethod("LoadFiles").FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
                    prm.Direction = ParameterDirection.Input;
                    prm.Value = Id;

                    try
                    {
                        if (cmd.Connection.State == ConnectionState.Closed)
                            cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChainAdvanced<Task, FileData> item = new ChainAdvanced<Task, FileData> { Workarea = Workarea, Left = this };
                                item.Load(reader);
                                collection.Add(item);
                            }
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
            return collection;
        }

        #endregion

        #region ICodes
        public List<CodeValue<Task>> GetValues(bool allKinds)
        {
            return CodeHelper<Task>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Task>.GetView(this, true);
        }
        #endregion

        #region IFacts

        private List<FactView> _factView;
        public List<FactView> GetCollectionFactView()
        {
            return _factView ?? (_factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId));
        }

        public void RefreshFaсtView()
        {
            _factView = FactHelper.GetCollectionFactView(Workarea, Id, EntityId);
        }

        public FactView GetFactViewValue(string factCode, string columnCode)
        {
            return GetCollectionFactView().FirstOrDefault(s => s.FactNameCode == factCode && s.ColumnCode == columnCode);
        }

        public List<FactName> GetFactNames()
        {
            return FactHelper.GetFactNames(Workarea, EntityId);
        }
        #endregion

        /// <summary>
        /// Первая иерархия в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Task>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }
    }
}