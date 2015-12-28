using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Событие"</summary>
    internal struct EventStruct
    {
        /// <summary>Дата начала</summary>
        public DateTime? StartOn;
        /// <summary>Дата окончания</summary>
        public DateTime? EndOn;
        /// <summary>Ежедневное</summary>
        public bool IsRecurcive;
        /// <summary>Идентификатор статуса</summary>
        public int StatusId;
        /// <summary>Время начала</summary>
        public TimeSpan? StartOnTime;
        /// <summary>Время окончания</summary>
        public TimeSpan? EndOnTime;
        /// <summary>Идентификатор процесса для запуска</summary>
        public int WfToStartId;
        /// <summary>Идентификатор процесса владельца</summary>
        public int WfOwnerId;
        /// <summary>Идентификатор типа рекурсии</summary>
        public int RecursiveId;
        /// <summary>Дополнительные параметры для запуска процесса</summary>
        public string XmlData;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
    }
    /// <summary>Пользовательское событие</summary>
    public sealed class Event : BaseCore<Event>, IEquatable<Event>,
                                IComparable, IComparable<Event>, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Телефонный звонок, соответствует значению 1</summary>
        public const int KINDVALUE_PHONE = 1;
        /// <summary>Личная встреча, соответствует значению 2</summary>
        public const int KINDVALUE_MEETING = 2;
        /// <summary>Электронное письмо, соответствует значению 3</summary>
        public const int KINDVALUE_EMAIL = 3;
        /// <summary>День рождения, соответствует значению 4</summary>
        public const int KINDVALUE_BIRTHDAY = 4;
        /// <summary>Напоминание, соответствует значению 5</summary>
        public const int KINDVALUE_REMINDER = 5;
        /// <summary>Сообщение, соответствует значению 6</summary>
        public const int KINDVALUE_MESSAGE = 6;
        /// <summary>Запуск процесса, соответствует значению 7</summary>
        public const int KINDVALUE_PROCESS = 7;
        /// <summary>Уведомление, соответствует значению 8</summary>
        public const int KINDVALUE_LOGDATA = 8;    
        
        /// <summary>Системное сообщение, соответствует значению 4390913</summary>
        public const int KINDID_PHONE = 4390913;
        /// <summary>Пользовательское сообщение, соответствует значению 4390914</summary>
        public const int KINDID_MEETING = 4390914;
        /// <summary>Электронное письмо, соответствует значению 4390915</summary>
        public const int KINDID_EMAIL = 4390915;
        /// <summary>День рождения, соответствует значению 4390916</summary>
        public const int KINDID_BIRTHDAY = 4390916;
        /// <summary>Напоминание, соответствует значению 4390917</summary>
        public const int KINDID_REMINDER = 4390917;
        /// <summary>Сообщение, соответствует значению 4390918</summary>
        public const int KINDID_MESSAGE = 4390918;
        /// <summary>Запуск процесса, соответствует значению 4390919</summary>
        public const int KINDID_PROCESS = 4390919;
        /// <summary>Уведомление, соответствует значению 4390920</summary>
        public const int KINDID_LOGDATA = 4390920;


        /// <summary>Статус события "Выполняется"</summary>
        public const string SYSTEM_EVENT_PROCESS = "SYSTEM_EVENT_PROCESS";
        /// <summary>Статус события "Завершено"</summary>
        public const string SYSTEM_EVENT_END = "SYSTEM_EVENT_END";
        /// <summary>Статус события "Отменено"</summary>
        public const string SYSTEM_EVENT_CANCEL = "SYSTEM_EVENT_CANCEL";
        /// <summary>Статус события "Запланировано"</summary>
        public const string SYSTEM_EVENT_PLAN = "SYSTEM_EVENT_PLAN";   

        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<Event>.Equals(Event other)
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
            Event otherPerson = (Event)obj;
            return Id.CompareTo(otherPerson.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Event other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public Event()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Event;
        }
        protected override void CopyValue(Event template)
        {
            base.CopyValue(template);
            StartPlanDate = template.StartPlanDate;
            StartPlanTime = template.StartPlanTime;
            StartOn = template.StartOn;
            EndOn = template.EndOn;
            IsRecurcive = template.IsRecurcive;
            StatusId = template.StatusId;
            WfOwnerId = template.WfOwnerId;
            WfToStartId = template.WfToStartId;
            RecursiveId = template.RecursiveId;
            XmlData = template.XmlData;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Event Clone(bool endInit)
        {
            Event obj = base.Clone(false);
            obj.StartOn = StartOn;
            obj.EndOn = EndOn;
            obj.IsRecurcive = IsRecurcive;
            obj.StatusId = StatusId;
            obj.RecursiveId = RecursiveId;
            obj.XmlData = XmlData;

            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private DateTime? _startOn;
        /// <summary>Дата начала</summary>
        public DateTime? StartOn
        {
            get { return _startOn; }
            set
            {
                if (value == _startOn) return;
                OnPropertyChanging(GlobalPropertyNames.StartOn);
                _startOn = value;
                OnPropertyChanged(GlobalPropertyNames.StartOn);
            }
        }


        private TimeSpan? _startOnTime;
        /// <summary>
        /// Время начала
        /// </summary>
        public TimeSpan? StartOnTime
        {
            get { return _startOnTime; }
            set
            {
                if (value == _startOnTime) return;
                OnPropertyChanging(GlobalPropertyNames.StartOnTime);
                _startOnTime = value;
                OnPropertyChanged(GlobalPropertyNames.StartOnTime);
            }
        }


        private DateTime? _endOn;
        /// <summary>Дата окончания</summary>
        public DateTime? EndOn
        {
            get { return _endOn; }
            set
            {
                if (value == _endOn) return;
                OnPropertyChanging(GlobalPropertyNames.EndOn);
                _endOn = value;
                OnPropertyChanged(GlobalPropertyNames.EndOn);
            }
        }


        private TimeSpan? _endOnTime;
        /// <summary>Время окончания</summary>
        public TimeSpan? EndOnTime
        {
            get { return _endOnTime; }
            set
            {
                if (value == _endOnTime) return;
                OnPropertyChanging(GlobalPropertyNames.EndOnTime);
                _endOnTime = value;
                OnPropertyChanged(GlobalPropertyNames.EndOnTime);
            }
        }

        private bool _isRecurcive;
        /// <summary>Ежедневное</summary>
        public bool IsRecurcive
        {
            get { return _isRecurcive; }
            set
            {
                if (value == _isRecurcive) return;
                OnPropertyChanging(GlobalPropertyNames.IsRecurcive);
                _isRecurcive = value;
                OnPropertyChanged(GlobalPropertyNames.IsRecurcive);
            }
        }


        private int _recursiveId;
        /// <summary>
        /// Идентификатор типа рекурсии
        /// </summary>
        public int RecursiveId
        {
            get { return _recursiveId; }
            set
            {
                if (value == _recursiveId) return;
                OnPropertyChanging(GlobalPropertyNames.RecursiveId);
                _recursiveId = value;
                OnPropertyChanged(GlobalPropertyNames.RecursiveId);
            }
        }


        private Analitic _recursive;
        /// <summary>
        /// Тип рекурсии
        /// </summary>
        public Analitic Recursive
        {
            get
            {
                if (_recursiveId == 0)
                    return null;
                if (_recursive == null)
                    _recursive = Workarea.Cashe.GetCasheData<Analitic>().Item(_recursiveId);
                else if (_recursive.Id != _recursiveId)
                    _recursive = Workarea.Cashe.GetCasheData<Analitic>().Item(_recursiveId);
                return _recursive;
            }
            set
            {
                if (_recursive == value) return;
                OnPropertyChanging(GlobalPropertyNames.Recursive);
                _recursive = value;
                _recursiveId = _recursive == null ? 0 : _recursive.Id;
                OnPropertyChanged(GlobalPropertyNames.Recursive);
            }
        }
        

        private int _statusId;
        /// <summary>Идентификатор статуса</summary>
        public int StatusId
        {
            get { return _statusId; }
            set
            {
                if (value == _statusId) return;
                OnPropertyChanging(GlobalPropertyNames.StatusId);
                _statusId = value;
                OnPropertyChanged(GlobalPropertyNames.StatusId);
            }
        }

        private Analitic _status;
        /// <summary>
        /// Статус
        /// </summary>
        public Analitic Status
        {
            get
            {
                if (_statusId == 0)
                    return null;
                if (_status == null)
                    _status = Workarea.Cashe.GetCasheData<Analitic>().Item(_statusId);
                else if (_status.Id != _statusId)
                    _status = Workarea.Cashe.GetCasheData<Analitic>().Item(_statusId);
                return _status;
            }
            set
            {
                if (_status == value) return;
                OnPropertyChanging("Status");
                _status = value;
                _statusId = _status == null ? 0 : _status.Id;
                OnPropertyChanged("Status");
            }
        }


        private int _wfToStartId;
        /// <summary>
        /// Идентификатор процесса для запуска
        /// </summary>
        /// <remarks>Идентификатор процесса который необходимо инициализировать и запустить на исполнение при
        /// наступлении даты и времени старта</remarks>
        public int WfToStartId
        {
            get { return _wfToStartId; }
            set
            {
                if (value == _wfToStartId) return;
                OnPropertyChanging(GlobalPropertyNames.WfToStartId);
                _wfToStartId = value;
                OnPropertyChanged(GlobalPropertyNames.WfToStartId);
            }
        }


        private Ruleset _wfToStart;
        /// <summary>
        /// Процесс для запуска
        /// </summary>
        public Ruleset WfToStart
        {
            get
            {
                if (_wfToStartId == 0)
                    return null;
                if (_wfToStart == null)
                    _wfToStart = Workarea.Cashe.GetCasheData<Ruleset>().Item(_wfToStartId);
                else if (_wfToStart.Id != _wfToStartId)
                    _wfToStart = Workarea.Cashe.GetCasheData<Ruleset>().Item(_wfToStartId);
                return _wfToStart;
            }
            set
            {
                if (_wfToStart == value) return;
                OnPropertyChanging(GlobalPropertyNames.WfToStart);
                _wfToStart = value;
                _wfToStartId = _wfToStart == null ? 0 : _wfToStart.Id;
                OnPropertyChanged(GlobalPropertyNames.WfToStart);
            }
        }

        private int _wfOwnerId;
        /// <summary>
        /// Идентификатор процесса владельца
        /// </summary>
        /// <remarks>Идентификатор процесса являющегося инициализатором события</remarks>
        public int WfOwnerId
        {
            get { return _wfOwnerId; }
            set
            {
                if (value == _wfOwnerId) return;
                OnPropertyChanging(GlobalPropertyNames.WfOwnerId);
                _wfOwnerId = value;
                OnPropertyChanged(GlobalPropertyNames.WfOwnerId);
            }
        }


        private Ruleset _wfOwner;
        /// <summary>
        /// Процесс владелец
        /// </summary>
        public Ruleset WfOwner
        {
            get
            {
                if (_wfOwnerId == 0)
                    return null;
                if (_wfOwner == null)
                    _wfOwner = Workarea.Cashe.GetCasheData<Ruleset>().Item(_wfOwnerId);
                else if (_wfOwner.Id != _wfOwnerId)
                    _wfOwner = Workarea.Cashe.GetCasheData<Ruleset>().Item(_wfOwnerId);
                return _wfOwner;
            }
            set
            {
                if (_wfOwner == value) return;
                OnPropertyChanging(GlobalPropertyNames.WfOwner);
                _wfOwner = value;
                _wfOwnerId = _wfOwner == null ? 0 : _wfOwner.Id;
                OnPropertyChanged(GlobalPropertyNames.WfOwner);
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
                OnPropertyChanging(GlobalPropertyNames.UserOwner);
                _userOwner = value;
                _userOwnerId = _userOwner == null ? 0 : _userOwner.Id;
                OnPropertyChanged(GlobalPropertyNames.UserOwner);
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

        
        private DateTime _startPlanDate;
        /// <summary>Дата планового запуска</summary>
        public DateTime StartPlanDate
        { 
            get{ return _startPlanDate; } 
            set
            {
               if (value == _startPlanDate) return;
                OnPropertyChanging(GlobalPropertyNames.StartPlanDate);
                _startPlanDate = value;
                OnPropertyChanged(GlobalPropertyNames.StartPlanDate);
            } 
        }
 
        
        private TimeSpan _startPlanTime;
        /// <summary>Время планового запуска</summary>
        public TimeSpan StartPlanTime 
        { 
            get{ return _startPlanTime; } 
            set
            {
               if (value == _startPlanTime) return;
                OnPropertyChanging(GlobalPropertyNames.StartPlanTime);
                _startPlanTime = value;
                OnPropertyChanged(GlobalPropertyNames.StartPlanTime);
            } 
        }


        private string _xmlData;
        /// <summary>
        /// Дополнительные параметры для запуска процесса
        /// </summary>
        public string XmlData
        {
            get { return _xmlData; }
            set
            {
                if (value == _xmlData) return;
                OnPropertyChanging(GlobalPropertyNames.XmlData);
                _xmlData = value;
                OnPropertyChanged(GlobalPropertyNames.XmlData);
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

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            //if (_startOn!=DateTime.MinValue)
                //writer.WriteAttributeString(GlobalPropertyNames.StartOn, XmlConvert.ToString(_startOn));
            //if (_endOn!=DateTime.MinValue)
                //writer.WriteAttributeString(GlobalPropertyNames.EndOn, XmlConvert.ToString(_endOn));
            if (_isRecurcive)
                writer.WriteAttributeString(GlobalPropertyNames.IsRecurcive, XmlConvert.ToString(_isRecurcive));
            if (_statusId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.StatusId, XmlConvert.ToString(_statusId));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.StartOn) != null)
                _startOn = XmlConvert.ToDateTime(reader[GlobalPropertyNames.StartOn]);
            if (reader.GetAttribute(GlobalPropertyNames.EndOn) != null)
                _endOn = XmlConvert.ToDateTime(reader[GlobalPropertyNames.EndOn]);
            if (reader.GetAttribute(GlobalPropertyNames.IsRecurcive) != null)
                _isRecurcive = XmlConvert.ToBoolean(reader[GlobalPropertyNames.IsRecurcive]);
            if (reader.GetAttribute(GlobalPropertyNames.StatusId) != null)
                _statusId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.StatusId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
        }
        #endregion

        #region Состояние
        EventStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new EventStruct { EndOn = _endOn, StartOn = _startOn, IsRecurcive = _isRecurcive, RecursiveId=_recursiveId, StatusId = _statusId, XmlData=_xmlData, MyCompanyId = _myCompanyId};
                return true;
            }
            return false;
        }
        /// <summary>
        /// Восстановить состояние
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            StartOn = _baseStruct.StartOn;
            EndOn = _baseStruct.EndOn;
            StatusId = _baseStruct.StatusId;
            IsRecurcive = _baseStruct.IsRecurcive;
            RecursiveId = _baseStruct.RecursiveId;
            XmlData = _baseStruct.XmlData;
            MyCompanyId = _baseStruct.MyCompanyId;
            IsChanged = false;
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (_statusId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_EVENT_STATUSID"));
            if (_userOwnerId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_EVENT_USEROWNERID"));
            if (_userToId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_EVENT_USERTOID"));
        }

        #region База данных
        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <param name="reader">Объект <see cref="SqlDataReader"/> чтения данных</param>
        /// <param name="endInit">Закончить инициализацию</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _startOn = reader.IsDBNull(17) ? (DateTime?)null : reader.GetDateTime(17);
                _startOnTime = reader.IsDBNull(18) ? (TimeSpan?)null : reader.GetTimeSpan(18);
                _endOn = reader.IsDBNull(19) ? (DateTime?)null : reader.GetDateTime(19);
                _endOnTime = reader.IsDBNull(20) ? (TimeSpan?)null : reader.GetTimeSpan(20);
                _isRecurcive = reader.GetBoolean(21);
                _statusId = reader.GetInt32(22);
                _wfToStartId = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
                _wfOwnerId = reader.IsDBNull(24) ? 0 : reader.GetInt32(24);
                _userOwnerId = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                _userToId = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                _startPlanDate = reader.GetDateTime(27);
                _startPlanTime = reader.GetTimeSpan(28);
                _xmlData = reader.IsDBNull(29) ? string.Empty : reader.GetString(29);
                _recursiveId = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
                _myCompanyId = reader.IsDBNull(31) ? 0 : reader.GetInt32(31);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.StartOn, SqlDbType.Date) { IsNullable = true };
            if (_startOn.HasValue) 
                prm.Value = _startOn;
            else 
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StartOnTime, SqlDbType.Time) { IsNullable = true};
            if (_startOnTime.HasValue)
                prm.Value = _startOnTime;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EndOn, SqlDbType.Date) { IsNullable = true };
            prm.Value = _endOn.HasValue ? (object) _endOn : DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EndOnTime, SqlDbType.Time) { IsNullable = true};
            prm.Value = _endOnTime.HasValue ? (object)_endOnTime : DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StatusId, SqlDbType.Int) { IsNullable = false, Value = _statusId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EveryDay, SqlDbType.Bit) { IsNullable = false, Value = _isRecurcive };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WfToStartId, SqlDbType.Int) { IsNullable = true };
            if (_wfToStartId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _wfToStartId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.WfOwnerId, SqlDbType.Int) { IsNullable = true };
            if (_wfOwnerId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _wfOwnerId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _userOwnerId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserToId, SqlDbType.Int) { IsNullable = false };
            prm.Value = _userToId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StartPlanDate, SqlDbType.Date) { IsNullable = false };
            prm.Value = _startPlanDate;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.StartPlanTime, SqlDbType.Time) { IsNullable = true };
            prm.Value = _startPlanTime;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.RecursiveId, SqlDbType.Int) { IsNullable = true };
            if (_recursiveId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _recursiveId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.XmlData, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_xmlData))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _xmlData.Length;
                prm.Value = _xmlData;
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

        public override string ToString(string mask)
        {
            string res = base.ToString(mask);

            // Макроподстановка для названия
            res = res.Replace("%startplantime%", DateTime.Parse(StartPlanTime.ToString()).ToString("hh:mm tt"));
            return res;
        }
        //Return Format(DateTime.Parse(StartTime.ToString(), "hh:mm tt")

    }
}