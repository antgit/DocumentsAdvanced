using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using BusinessObjects.Documents;
using BusinessObjects.Security;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Сообщение"</summary>
    internal struct MessageStruct
    {
        /// <summary>Идентификатор пользователя владельца</summary>
        public int UserOwnerId;
        /// <summary>Идентификатор приоритета</summary>
        public int PriorityId;
        /// <summary>Идентификатор пользователя получателя</summary>
        public int UserId;
        /// <summary>Требовать уведомления о прочтении</summary>
        public bool HasRead;
        /// <summary>Флаг "Собщение прочитано", обработано</summary>
        public bool ReadDone;
        /// <summary>Дата прочтения</summary>
        public DateTime? ReadDate;
        /// <summary>Время прочтения</summary>
        public TimeSpan? ReadTime;
        /// <summary>Отправлено</summary>
        public bool IsSend;
        /// <summary>Дата отправки</summary>
        public DateTime? SendDate;
        /// <summary>Время отправки</summary>
        public TimeSpan? SendTime;
        /// <summary>Идентификатор предприятия, которому принадлежит объект</summary>
        public int MyCompanyId;
        /// <summary>Отметка отправителя</summary>
        public bool MarkedOwner;
        /// <summary>Уровень отметки отправителя</summary>
        public int MarkScoreOwner;
        /// <summary>Отметка получателя</summary>
        public bool MarkedRecipient;
        /// <summary>Уровень отметки получателя</summary>
        public int MarkScoreRecipient;
	
    }
    /// <summary>Сообщение</summary>
    public sealed class Message : BaseCore<Message>, IChains<Message>, IEquatable<Message>,
                                  IComparable, IComparable<Message>,IFacts<Message>,
                                  ICodes<Message>,
        IChainsAdvancedList<Message, Document>,
        IChainsAdvancedList<Message, FileData>,
        IHierarchySupport, ICompanyOwner
    {
        #region Константы значений типов и подтипов
        // ReSharper disable InconsistentNaming

        /// <summary>Системное сообщение, соответствует значению 1</summary>
        public const int KINDVALUE_SYSTEM = 1;
        /// <summary>Пользовательское сообщение, соответствует значению 2</summary>
        public const int KINDVALUE_USER = 2;
        /// <summary>Протокол службы обмена файлами, соответствует значению 3</summary>
        public const int KINDVALUE_FILESERVICE = 3;
        /// <summary>Протокол службы экспорта данных, соответствует значению 4</summary>
        public const int KINDVALUE_EXPORTSERVICE = 4;
        /// <summary>Протокол службы импорта данных, соответствует значению 5</summary>
        public const int KINDVALUE_IMPORTSERVICE = 5;
        /// <summary>Уведомление о прочтении, соответствует значению 6</summary>
        public const int KINDVALUE_REPLY = 6;
        
        /// <summary>Системное сообщение, соответствует значению 4980737</summary>
        public const int KINDID_SYSTEM = 4980737;
        /// <summary>Пользовательское сообщение, соответствует значению 4980738</summary>
        public const int KINDID_USER = 4980738;
        /// <summary>Протокол службы обмена файлами, соответствует значению 4980739</summary>
        public const int KINDID_FILESERVICE = 4980739;
        /// <summary>Протокол службы экспорта данных, соответствует значению 4980740</summary>
        public const int KINDID_EXPORTSERVICE = 4980740;
        /// <summary>Протокол службы импорта данных, соответствует значению 4980741</summary>
        public const int KINDID_IMPORTSERVICE = 4980741;
        /// <summary>Уведомление о прочтении, соответствует значению 4980742</summary>
        public const int KINDID_REPLY = 4980742;

        /// <summary>
        /// Код системного сообщения-шаблона о постановке задачи
        /// </summary>
        public const string MESSAGE_TEMPLATE_TASKCREATE = "MESSAGE_TEMPLATE_TASKCREATE";
        /// <summary>
        /// Код системного сообщения-шаблона о обновлении задачи
        /// </summary>
        public const string MESSAGE_TEMPLATE_TASKUPDATE = "MESSAGE_TEMPLATE_TASKUPDATE";

        /// <summary>
        /// Код системного сообщения-шаблона о постановке документа на расмотрение в разделе "Финансовое планирование"
        /// </summary>
        public const string MESSAGE_TEMPLATE_DOCUMENTPLANREATE = "MESSAGE_TEMPLATE_DOCUMENTPLANREATE";
        // ReSharper restore InconsistentNaming

        #endregion
        bool IEquatable<Message>.Equals(Message other)
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
            Message otherPerson = (Message)obj;
            return Id.CompareTo(otherPerson.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(Message other)
        {
            return Id.CompareTo(other.Id);
        }

        /// <summary>Конструктор</summary>
        public Message()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Message;
        }
        protected override void CopyValue(Message template)
        {
            base.CopyValue(template);
            UserOwnerId = template.UserOwnerId;
            PriorityId = template.PriorityId;
            UserId = template.UserId;
            HasRead = template.HasRead;
            ReadDone = template.ReadDone;
            ReadDate = template.ReadDate;
            ReadTime = template.ReadTime;
            MyCompanyId = template.MyCompanyId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Message Clone(bool endInit)
        {
            Message obj = base.Clone(false);
            obj.UserOwnerId = UserOwnerId;
            obj.PriorityId = PriorityId;
            obj.UserId = UserId;
            obj.HasRead = HasRead;
            obj.ReadDone = ReadDone;
            obj.ReadDate = ReadDate;
            obj.ReadTime = ReadTime;
            obj.MyCompanyId = MyCompanyId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private int _userOwnerId;
        /// <summary>
        /// Идентификатор пользователя владельца
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
        /// Пользователь владелец сообщения
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


        private int _priorityId;
        /// <summary>Идентификатор приоритета</summary>
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
        /// <summary>Приоритет</summary>
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
                OnPropertyChanging(GlobalPropertyNames.Priority);
                _priority = value;
                _priorityId = _priority == null ? 0 : _priority.Id;
                OnPropertyChanged(GlobalPropertyNames.Priority);
            }
        }

        /// <summary>
        /// Наименование приоритета
        /// </summary>
        public string PriorityName
        {
            get
            {
                if (_priorityId != 0)
                {
                    return Priority.Name;
                }
                return string.Empty;
            }
        }

        private int _userId;
        /// <summary>
        /// Идентификатор пользователя получателя
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (value == _userId) return;
                OnPropertyChanging(GlobalPropertyNames.UserId);
                _userId = value;
                OnPropertyChanged(GlobalPropertyNames.UserId);
            }
        }


        private Uid _user;
        /// <summary>
        /// Пользователь получатель
        /// </summary>
        public Uid User
        {
            get
            {
                if (_userId == 0)
                    return null;
                if (_user == null)
                    _user = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                else if (_user.Id != _userId)
                    _user = Workarea.Cashe.GetCasheData<Uid>().Item(_userId);
                return _user;
            }
            set
            {
                if (_user == value) return;
                OnPropertyChanging(GlobalPropertyNames.User);
                _user = value;
                _userId = _user == null ? 0 : _user.Id;
                OnPropertyChanged(GlobalPropertyNames.User);
            }
        }

        private bool _hasRead;
        /// <summary>
        /// Требовать уведомления о прочтении
        /// </summary>
        public bool HasRead
        {
            get { return _hasRead; }
            set
            {
                if (value == _hasRead) return;
                OnPropertyChanging(GlobalPropertyNames.HasRead);
                _hasRead = value;
                OnPropertyChanged(GlobalPropertyNames.HasRead);
            }
        }

        private bool _readDone;
        /// <summary>
        /// Флаг "Собщение прочитано", обработано.
        /// </summary>
        public bool ReadDone
        {
            get { return _readDone; }
            set
            {
                if (value == _readDone) return;
                OnPropertyChanging(GlobalPropertyNames.ReadDone);
                _readDone = value;
                OnPropertyChanged(GlobalPropertyNames.ReadDone);
            }
        }
        
        private DateTime? _readDate; 
        /// <summary>
        /// Дата прочтения
        /// </summary>
        public DateTime? ReadDate 
        { 
            get{ return _readDate; } 
            set
            {
               if (value == _readDate) return;
                OnPropertyChanging(GlobalPropertyNames.ReadDate);
                _readDate = value;
                OnPropertyChanged(GlobalPropertyNames.ReadDate);
            } 
        }

        private TimeSpan? _readTime;
        /// <summary>
        /// Время прочтения
        /// </summary>
        public TimeSpan? ReadTime
        {
            get { return _readTime; }
            set
            {
                if (value == _readTime) return;
                OnPropertyChanging(GlobalPropertyNames.ReadTime);
                _readTime = value;
                OnPropertyChanged(GlobalPropertyNames.ReadTime);
            }
        }

        private bool _isSend;
        /// <summary>Отправлено</summary>
        public bool IsSend
        {
            get { return _isSend; }
            set
            {
                if (value == _isSend) return;
                OnPropertyChanging(GlobalPropertyNames.IsSend);
                _isSend = value;
                OnPropertyChanged(GlobalPropertyNames.IsSend);
            }
        }

        private DateTime? _sendDate;
        /// <summary>Дата отправки</summary>
        public DateTime? SendDate
        {
            get { return _sendDate; }
            set
            {
                if (value == _sendDate) return;
                OnPropertyChanging(GlobalPropertyNames.SendDate);
                _sendDate = value;
                OnPropertyChanged(GlobalPropertyNames.SendDate);
            }
        }

        private TimeSpan? _sendTime;
        /// <summary>Время отправки</summary>
        public TimeSpan? SendTime
        {
            get { return _sendTime; }
            set
            {
                if (value == _sendTime) return;
                OnPropertyChanging(GlobalPropertyNames.SendTime);
                _sendTime = value;
                OnPropertyChanged(GlobalPropertyNames.SendTime);
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


        private bool _markedOwner;
        /// <summary>Отметка отправителя</summary>
        public bool MarkedOwner
        {
            get { return _markedOwner; }
            set
            {
                if (value == _markedOwner) return;
                OnPropertyChanging(GlobalPropertyNames.MarkedOwner);
                _markedOwner = value;
                OnPropertyChanged(GlobalPropertyNames.MarkedOwner);
            }
        }


        private int _markScoreOwner;
        /// <summary>Уровень отметки отправителя</summary>
        public int MarkScoreOwner
        {
            get { return _markScoreOwner; }
            set
            {
                if (value == _markScoreOwner) return;
                OnPropertyChanging(GlobalPropertyNames.MarkScoreOwner);
                _markScoreOwner = value;
                OnPropertyChanged(GlobalPropertyNames.MarkScoreOwner);
            }
        }

        private bool _markedRecipient;
        /// <summary>Отметка получателя</summary>
        public bool MarkedRecipient
        {
            get { return _markedRecipient; }
            set
            {
                if (value == _markedRecipient) return;
                OnPropertyChanging(GlobalPropertyNames.MarkedRecipient);
                _markedRecipient = value;
                OnPropertyChanged(GlobalPropertyNames.MarkedRecipient);
            }
        }
        
        private int _markScoreRecipient;
        /// <summary>Уровень отметки получателя</summary>
        public int MarkScoreRecipient 
        { 
            get{ return _markScoreRecipient; } 
            set
            {
               if (value == _markScoreRecipient) return;
                OnPropertyChanging(GlobalPropertyNames.MarkScoreRecipient);
                _markScoreRecipient = value;
                OnPropertyChanged(GlobalPropertyNames.MarkScoreRecipient);
            } 
        }


        /// <summary>
        /// Наименование сотрудника получателя (по данным о пользователе, являющегося получателем)
        /// </summary>
        public string AgentToName
        {
            get
            {
                if (_userId != 0)
                {
                    return User.AgentId == 0 ? string.Empty : _user.Agent.Name;
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// Наименование сотрудника отправителя (по данным о пользователе, являющегося автором)
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
        
        #endregion

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);
            if (_userOwnerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UserOwnerId, XmlConvert.ToString(_userOwnerId));
            if (_priorityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PriorityId, XmlConvert.ToString(_priorityId));
            if (_myCompanyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_myCompanyId));
            if(_markedOwner)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_markedOwner));
            if(_markScoreOwner!=0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_markScoreOwner));
            if(_markedRecipient)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_markedRecipient));
            if(_markScoreRecipient!=0)
                writer.WriteAttributeString(GlobalPropertyNames.MyCompanyId, XmlConvert.ToString(_markScoreRecipient));        
        
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.UserOwnerId) != null)
                _userOwnerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UserOwnerId));
            if (reader.GetAttribute(GlobalPropertyNames.PriorityId) != null)
                _priorityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PriorityId));
            if (reader.GetAttribute(GlobalPropertyNames.MyCompanyId) != null)
                _myCompanyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MyCompanyId));
            if (reader.GetAttribute(GlobalPropertyNames.MarkedOwner) != null)
                _markedOwner = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.MarkedOwner));
            if (reader.GetAttribute(GlobalPropertyNames.MarkScoreOwner) != null)
                _markScoreOwner = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MarkScoreOwner));
            if (reader.GetAttribute(GlobalPropertyNames.MarkedRecipient) != null)
                _markedRecipient = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.MarkedRecipient));
            if (reader.GetAttribute(GlobalPropertyNames.MarkScoreRecipient) != null)
                _markScoreRecipient = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MarkScoreRecipient));
        }
        #endregion

        #region Состояние
        MessageStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new MessageStruct
                                  {
                                      UserOwnerId = _userOwnerId, 
                                      PriorityId = _priorityId, 
                                      UserId = _userId, 
                                      HasRead = _hasRead, 
                                      ReadDone = _readDone, 
                                      ReadDate = _readDate, 
                                      ReadTime = _readTime, 
                                      IsSend = _isSend, 
                                      SendDate = _sendDate, 
                                      SendTime = _sendTime, 
                                      MyCompanyId=_myCompanyId,
                                      MarkedOwner = _markedOwner,
                                      MarkedRecipient = _markedRecipient,
                                      MarkScoreOwner = _markScoreOwner,
                                      MarkScoreRecipient = _markScoreRecipient
                                  };
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
            UserOwnerId = _baseStruct.UserOwnerId;
            PriorityId = _baseStruct.PriorityId;
            UserId = _baseStruct.UserId;
            HasRead = _baseStruct.HasRead;
            ReadDone = _baseStruct.ReadDone;
            ReadDate = _baseStruct.ReadDate;
            ReadTime = _baseStruct.ReadTime;
            IsSend = _baseStruct.IsSend;
            SendDate = _baseStruct.SendDate;
            SendTime = _baseStruct.SendTime;
            MyCompanyId = _baseStruct.MyCompanyId;
            MarkedOwner = _baseStruct.MarkedOwner;
            MarkedRecipient = _baseStruct.MarkedRecipient;
            MarkScoreOwner = _baseStruct.MarkScoreOwner;
            MarkScoreRecipient = _baseStruct.MarkScoreRecipient;
            IsChanged = false;
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();

            if (_userOwnerId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_MESSAGE_USEROWNERID"));
            if (_priorityId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_MESSAGE_PRIORITY"));
            if (_userId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_MESSAGE_USERID"));
            if (string.IsNullOrEmpty(Memo))
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_MESSAGE_MEMO"));

            if(_isSend)
            {
                // проверить и установить дату и время отправки
                if (!_sendDate.HasValue)
                {
                    _sendDate = DateTime.Today;
                    _sendTime = DateTime.Now.TimeOfDay;
                }
                
            }
            else
            {
                _sendDate = null;
                _sendTime = null;
            }
            if(_readDone)
            {
                if(!_readDate.HasValue)
                {
                    _readDate = DateTime.Today;
                    _readTime = DateTime.Now.TimeOfDay;
                }
            }
            else
            {
                _readDate = null;
                _readTime = null;
            }
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
                _userOwnerId = reader.GetSqlInt32(17).Value;
                _priorityId = reader.GetSqlInt32(18).Value;
                _userId = reader.GetSqlInt32(19).Value;
                _hasRead = reader.GetSqlBoolean(20).Value;
                _readDone = reader.GetSqlBoolean(21).Value;
                _readDate = reader.IsDBNull(22) ? (DateTime?)null : reader.GetDateTime(22);
                _readTime = reader.IsDBNull(23) ? TimeSpan.Zero : reader.GetTimeSpan(23);
                _isSend = reader.GetSqlBoolean(24).Value;
                _sendDate = reader.IsDBNull(25) ? (DateTime?)null : reader.GetDateTime(25);
                _sendTime = reader.IsDBNull(26) ? TimeSpan.Zero : reader.GetTimeSpan(26);
                _myCompanyId = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                _markedOwner = reader.IsDBNull(28) ? false : reader.GetBoolean(28);
                _markScoreOwner = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
                _markedRecipient = reader.IsDBNull(30) ? false : reader.GetBoolean(30);
                _markScoreRecipient = reader.IsDBNull(31) ? 0 : reader.GetInt32(31);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.UserOwnerId, SqlDbType.Int) { IsNullable = false, Value = _userOwnerId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PriorityId, SqlDbType.Int) { IsNullable = false, Value = _priorityId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserId, SqlDbType.Int) { IsNullable = false, Value = _userId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.HasRead, SqlDbType.Bit) { IsNullable = false, Value = _hasRead };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ReadDone, SqlDbType.Bit) { IsNullable = false, Value = _readDone };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ReadDate, SqlDbType.Date) { IsNullable = true};
            if (_readDate.HasValue)
                prm.Value = _readDate;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ReadTime, SqlDbType.Time) { IsNullable = true };
            if (_readTime.HasValue)
                prm.Value = _readTime;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.IsSend, SqlDbType.Bit) { IsNullable = false, Value = _isSend };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SendDate, SqlDbType.Date) { IsNullable = true };
            if (_sendDate.HasValue)
                prm.Value = _sendDate;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.SendTime, SqlDbType.Time) { IsNullable = true };
            if (_sendTime.HasValue)
                prm.Value = _sendTime;
            else
                prm.Value = DBNull.Value;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int) { IsNullable = true };
            if (_myCompanyId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _myCompanyId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MarkedOwner, SqlDbType.Bit) { IsNullable = false };
            prm.Value = _markedOwner;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MarkScoreOwner, SqlDbType.Int) { IsNullable = false };
            prm.Value = _markScoreOwner;
            sqlCmd.Parameters.Add(prm);
    
            prm = new SqlParameter(GlobalSqlParamNames.MarkedRecipient, SqlDbType.Bit) { IsNullable = false };
            prm.Value = _markedRecipient;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.MarkScoreRecipient, SqlDbType.Int) { IsNullable = false };
            prm.Value = _markScoreRecipient;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        #region ILinks<Message> Members
        /// <summary>Связи аналитики</summary>
        /// <returns></returns>
        public List<IChain<Message>> GetLinks()
        {
            return Workarea.CollectionChainSources(this, null);
        }
        /// <summary>Связи аналитики</summary>
        /// <param name="kind">Тип связи</param>
        /// <returns></returns>
        public List<IChain<Message>> GetLinks(int kind)
        {
            return Workarea.CollectionChainSources(this, kind);
        }
        List<Message> IChains<Message>.SourceList(int chainKindId)
        {
            return Chain<Message>.GetChainSourceList(this, chainKindId);
        }
        List<Message> IChains<Message>.DestinationList(int chainKindId)
        {
            return Chain<Message>.DestinationList(this, chainKindId);
        }
        #endregion

        #region ICodes
        public List<CodeValue<Message>> GetValues(bool allKinds)
        {
            return CodeHelper<Message>.GetValues(this, true);
        }
        public List<CodeValueView> GetView(bool allKinds)
        {
            return CodeHelper<Message>.GetView(this, true);
        }
        #endregion

        /// <summary>
        /// Первая группа в которую входит объект
        /// </summary>
        /// <returns></returns>
        public Hierarchy FirstHierarchy()
        {
            int? id = Hierarchy.FirstHierarchy<Message>(this);
            if (!id.HasValue) return null;
            return Workarea.Cashe.GetCasheData<Hierarchy>().Item(id.Value);
        }

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

        #region IChainsAdvancedList<Message,Document> Members

        List<IChainAdvanced<Message, Document>> IChainsAdvancedList<Message, Document>.GetLinks()
        {
            return ChainAdvanced<Message, Document>.CollectionSource(this);
        }

        List<IChainAdvanced<Message, Document>> IChainsAdvancedList<Message, Document>.GetLinks(int? kind)
        {
            return ChainAdvanced<Message, Document>.CollectionSource(this, kind);
        }
        public List<IChainAdvanced<Message, Document>> GetLinkedDocuments(int? kind = null)
        {
            return ChainAdvanced<Message, Document>.CollectionSource(this, kind);
        }
        List<ChainValueView> IChainsAdvancedList<Message, Document>.GetChainView()
        {
            return ChainValueView.GetView<Message, Document>(this);
        }
        #endregion

        #region IChainsAdvancedList<Message,FileData> Members

        List<IChainAdvanced<Message, FileData>> IChainsAdvancedList<Message, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<Message, FileData>)this).GetLinks(77);
        }

        List<IChainAdvanced<Message, FileData>> IChainsAdvancedList<Message, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<Message, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Message, FileData>(this);
        }
        public List<IChainAdvanced<Message, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<Message, FileData>> collection = new List<IChainAdvanced<Message, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Message>().Entity.FindMethod("LoadFiles").FullName;
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
                                ChainAdvanced<Message, FileData> item = new ChainAdvanced<Message, FileData> { Workarea = Workarea, Left = this };
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
        /// <summary>
        /// Поиск данных
        /// </summary>
        /// <param name="hierarchyId">Идентификатор группы</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="flags">Флаг</param>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="name">Наименование</param>
        /// <param name="kindId">Идентификатор типа</param>
        /// <param name="code">Код</param>
        /// <param name="memo">Примечание</param>
        /// <param name="flagString">Пользовательский флаг</param>
        /// <param name="templateId">Идентификатор шаблона</param>
        /// <param name="count">Количество</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="useAndFilter">Использовать фильтр И</param>
        /// <returns></returns>
        public List<Message> FindBy(int? hierarchyId = null, string userName = null, int? flags = null,
            int? stateId = null, string name = null, int kindId = 0, string code = null,
            string memo = null, string flagString = null, int templateId = 0,
            int count = 100, Predicate<Message> filter = null, int? myCompanyId = null,
            bool useAndFilter = false)
        {
            Message item = new Message { Workarea = Workarea };
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", GlobalMethodAlias.LoadList));
            }
            List<Message> collection = new List<Message>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindProcedure(GlobalMethodAlias.FindBy);
                        cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count;
                        if (hierarchyId != null && hierarchyId.Value != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (userName != null && !string.IsNullOrEmpty(userName))
                            cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if (flags.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Flags, SqlDbType.Int).Value = flags;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId;
                        if (!string.IsNullOrWhiteSpace(name))
                            cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255).Value = name;
                        if (kindId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindId;
                        if (!string.IsNullOrWhiteSpace(code))
                            cmd.Parameters.Add(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100).Value = code;
                        if (!string.IsNullOrWhiteSpace(memo))
                            cmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255).Value = memo;
                        if (!string.IsNullOrWhiteSpace(flagString))
                            cmd.Parameters.Add(GlobalSqlParamNames.FlagString, SqlDbType.NVarChar, 50).Value = flagString;
                        if (templateId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.TemplateId, SqlDbType.Int).Value = templateId;
                        if (myCompanyId.HasValue && myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId.Value;
                        if (useAndFilter)
                            cmd.Parameters.Add(GlobalSqlParamNames.UseAndFilter, SqlDbType.Bit).Value = true;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Message { Workarea = Workarea };
                            item.Load(reader);
                            Workarea.Cashe.SetCasheData(item);
                            if (filter != null && filter.Invoke(item))
                                collection.Add(item);
                            else if (filter == null)
                                collection.Add(item);

                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        /// <summary>
        /// Количество входящих сообщений
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public static int MessageIncommingCount(Uid user)
        {
            int val = 0;
            using (SqlConnection cnn = user.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return 0;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = user.Workarea.Empty<Message>().FindProcedure("MessageIncommingCount");
                        cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = user.Id;
                        object res = cmd.ExecuteScalar();

                        val = res != null?(int)res : 0;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return val;
        }
        /// <summary>
        /// Список непрочтенных сообщений для пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Message> MessageIncommingNotRead(Uid user)
        {
            Message item = user.Workarea.Empty<Message>();
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", "MessageIncommingNotRead"));
            }
            List<Message> collection = new List<Message>();
            using (SqlConnection cnn = user.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = item.FindProcedure("MessageIncommingNotRead");
                        cmd.Parameters.Add(GlobalSqlParamNames.UserId, SqlDbType.Int).Value = user.Id;
                        
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Message { Workarea = user.Workarea };
                            item.Load(reader);
                            user.Workarea.Cashe.SetCasheData(item);
                            collection.Add(item);
                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }

        /// <summary>
        /// Список последних пяти новостей
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Message> MessageNewsLastFive(Uid user)
        {
            Message item = user.Workarea.Empty<Message>();
            if (item.EntityId == 0)
            {
                throw new MethodFindException(string.Format("Не найден метод {0}, потому что данный объект не зарегестрирован", "MessageIncommingNotRead"));
            }
            List<Message> collection = new List<Message>();
            using (SqlConnection cnn = user.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = item.FindProcedure("MessageNewsLastFive");
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = user.Name;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            item = new Message { Workarea = user.Workarea };
                            item.Load(reader);
                            user.Workarea.Cashe.SetCasheData(item);
                            collection.Add(item);
                        }
                        reader.Close();

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
        public bool CreateMessageReply()
        {
            if (this.UserOwnerId == UserId)
                return false;
            // шаблон сообщения
            Message tmlMessage = Workarea.GetTemplates<Message>().FirstOrDefault(s => s.KindId == Message.KINDID_REPLY);
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
                    newMessage.NameFull = "Ваше сообщение прочитано!";
                }
                else
                {
                    SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>(SystemParameter.WEBROOTSERVER);
                    string webLink = string.Format("{0}/UserPersonal/UserMessage/Edit/{1}", prm.ValueString, Id);

                    newMessage.NameFull = newMessage.NameFull.Replace("{AGENTOWNERNAME}", this.AgentOwnerName).Replace("{AGENTTONAME}", AgentToName).Replace("{WEBLINK}", webLink);
                    newMessage.UserId = this.UserOwnerId;
                    newMessage.UserOwnerId = this.UserId;
                    newMessage.HasRead = false;
                    newMessage.Save();

                    Chain<Message> chain = new Chain<Message>(this);
                    chain.KindId = chainKind.Id;
                    chain.Right = newMessage;
                    chain.StateId = State.STATEACTIVE;
                    chain.Save();
                }
            }
            return true;
        }
        
    }
}