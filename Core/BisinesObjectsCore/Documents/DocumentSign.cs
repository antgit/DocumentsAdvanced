using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{

    internal struct DocumentSignStruct
    {
        /// <summary>Идентификатор документа</summary>
        public int OwnId;
        /// <summary>Идентификатор корреспондента</summary>
        public int AgentId;
        /// <summary>Номер в списке</summary>
        public int OrderNo;
        /// <summary>Тип подписи</summary>
        public int Kind;
        /// <summary>Дата подписи</summary>
        public DateTime Date;
        /// <summary>Идентификатор резолюции</summary>
        public int ResolutionId;
        /// <summary>Описание</summary>
        public string Memo;
        /// <summary>Дата фактической подписи</summary>
        public DateTime? DateSign;
        /// <summary>Необходимость формировать задачу</summary>
        public bool TaskNeed;
        /// <summary>Необходимость формировать сообщение</summary>
        public bool MessageNeed;
        /// <summary>Идентификатор сообщения</summary>
        public int MessageId;
        /// <summary>Идентификатор задачи</summary>
        public int TaskId;
        /// <summary>Тип подписания ИЛИ/И</summary>
        public bool SignKind;
        /// <summary>Является ли главным подписантом</summary>
        public bool IsMain;
        /// <summary>Заместитель</summary>
        public int AgentSubId;
        /// <summary>Идентификатор сотрудника фактически выполнившего подписание</summary>
        public int AgentSignId;
        /// <summary>Приритет</summary>
        public int PriorityId;
        /// <summary>Номер группы</summary>
        public int GroupNo;
        /// <summary>Идентификатор отдела</summary>
        public int DepatmentId;
        /// <summary>Идентификатор уровня группы</summary>
        public int GroupLevelId;
    }
    /// <summary>
    /// Подпись документа
    /// </summary>
    public sealed class DocumentSign : BaseCoreObject, ICopyValue<DocumentSign>,
        IEquatable<DocumentSign>,
        IComparable
    {
        /// <summary>
        /// Список документов которые возможно использовать как шаблоны для подписания
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="kind">Идентификатор типа документа</param>
        /// <returns></returns>
        public static List<Document> GetCollectionDocumentSignTemplates(Workarea wa, int kind)
        {
            List<Document> collection = new List<Document>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = wa.Empty<Document>().Entity.FindMethod("Document.GetSignaturesDocuments").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int).Value = kind;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Document item = new Document { Workarea = wa };
                            item.Load(reader);
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

        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Первая сторона, соответствует значению 1</summary>
        public const int KINDVALUE_FIRST = 1;
        /// <summary>Вторая сторона, соответствует значению 2</summary>
        public const int KINDVALUE_SECOND = 2;
        /// <summary>Третья сторона, соответствует значению 3</summary>
        public const int KINDVALUE_THIRD = 3;

        /// <summary>Первая сторона, соответствует значению 3407873</summary>
        public const int KINDID_FIRST = 3407873;
        /// <summary>Вторая сторона, соответствует значению 3407874</summary>
        public const int KINDID_SECOND = 3407874;
        /// <summary>Третья сторона, соответствует значению 3407875</summary>
        public const int KINDID_THIRD = 3407875;

        /// <summary>
        /// Код аналитики группы подписания "Утверждение"
        /// </summary>
        public const string SIGN_LEVEL_SIGNING = "SIGN_LEVEL_SIGNING";
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentSign():base()
        {
            EntityId = (short) WhellKnownDbEntity.DocumentSign;
        }
        void ICopyValue<DocumentSign>.CopyValue(DocumentSign template)
        {
            CopyValue(template);
        }
        public void CopyValue(DocumentSign template)
        {

            Kind = template.Kind;
            Date = DateTime.Now;
            OwnId = template.OwnId;
            Workarea = Workarea;
            StateId = State.STATEACTIVE;
            AgentId = template.AgentId;
            AgentSignId = template.AgentSignId;
            AgentSubId = template.AgentSubId;
            AgentToId = template.AgentToId;
            AgentToSubId = template.AgentToSubId;
            DepatmentId = template.DepatmentId;
            GroupLevelId = template.GroupLevelId;
            GroupNo = template.GroupNo;
            ResolutionId = template.ResolutionId;
            DatabaseId = Workarea.MyBranche.Id;
        }
        bool IEquatable<DocumentSign>.Equals(DocumentSign other)
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
            DocumentSign otherObj = (DocumentSign)obj;
            return Id.CompareTo(otherObj.Id);
        }
        /// <summary>
        /// Сравнение двух аналитик по идентификатору
        /// </summary>
        /// <param name="other">Объект аналитики</param>
        /// <returns></returns>
        public int CompareTo(DocumentSign other)
        {
            return Id.CompareTo(other.Id);
        }
        #region Свойства
        private int _ownId;
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                if (value == _ownId) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
            }
        }
        private Document _own;
        /// <summary>
        /// Документ
        /// </summary>
        public Document Owner
        {
            get
            {
                if (_ownId == 0)
                    return default(Document);
                if (_own == null)
                    _own = Workarea.Cashe.GetCasheData<Document>().Item(_ownId);
                else if (_own.Id != _ownId)
                    _own = Workarea.Cashe.GetCasheData<Document>().Item(_ownId);
                return _own;
            }
            set
            {
                if (_own == value) return;
                OnPropertyChanging(GlobalPropertyNames.Owner);
                _own = value;
                _ownId = _own == null ? 0 : _own.Id;
                OnPropertyChanged(GlobalPropertyNames.Owner);
            }
        }
        private int _agentId;
        /// <summary>
        /// Идентификатор корреспондента инициатора подписания
        /// </summary>
        public int AgentId
        {
            get { return _agentId; }
            set
            {
                if (value == _agentId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentId);
                _agentId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentId);
            }
        }
        private Agent _agent;
        /// <summary>
        /// Корреспондент
        /// </summary>
        public Agent Agent
        {
            get
            {
                if (_agentId == 0)
                    return default(Agent);
                if (_agent == null)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                else if (_agent.Id != _agentId)
                    _agent = Workarea.Cashe.GetCasheData<Agent>().Item(_agentId);
                return _agent;
            }
            set
            {
                if (_agent == value) return;
                OnPropertyChanging(GlobalPropertyNames.Agent);
                _agent = value;
                _agentId = _agent == null ? 0 : Agent.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent);
            }
        }
        private int _orderNo;
        /// <summary>
        /// Номер в списке
        /// </summary>
        public int OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (value == _orderNo) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            }
        }

        private int _kind;
        /// <summary>
        /// Тип подписи
        /// </summary>
        public int Kind
        {
            get { return _kind; }
            set
            {
                if (value == _kind) return;
                OnPropertyChanging(GlobalPropertyNames.Kind);
                _kind = value;
                OnPropertyChanged(GlobalPropertyNames.Kind);
            }
        }

        /// <summary>Имя вида</summary>
        public string KindName
        {
            get
            {
                EntityKind obj = Workarea.CollectionEntityKinds.FirstOrDefault(f => f.Id == Kind);
                return obj != null ? obj.Name : "Неизвестный тип";
            }
        }
        private DateTime _date;
        /// <summary>
        /// Дата подписи (подписать до)
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (value == _date) return;
                OnPropertyChanging(GlobalPropertyNames.Date);
                _date = value;
                OnPropertyChanged(GlobalPropertyNames.Date);
            }
        }

        private int _resolutionId;
        /// <summary>
        /// Идентификатор резолюции
        /// </summary>
        public int ResolutionId
        {
            get { return _resolutionId; }
            set
            {
                if (value == _resolutionId) return;
                OnPropertyChanging(GlobalPropertyNames.ResolutionId);
                _resolutionId = value;
                OnPropertyChanged(GlobalPropertyNames.ResolutionId);
            }
        }

        private Analitic _resolution;
        /// <summary>
        /// Резолюция
        /// </summary>
        public Analitic Resolution
        {
            get
            {
                if (_resolutionId == 0)
                    return default(Analitic);
                if (_resolution == null)
                    _resolution = Workarea.Cashe.GetCasheData<Analitic>().Item(_resolutionId);
                else if (_resolution.Id != _resolutionId)
                    _resolution = Workarea.Cashe.GetCasheData<Analitic>().Item(_resolutionId);
                return _resolution;
            }
            set
            {
                if (_resolution == value) return;
                OnPropertyChanging(GlobalPropertyNames.Resolution);
                _resolution = value;
                _resolutionId = _resolution == null ? 0 : _resolution.Id;
                OnPropertyChanged(GlobalPropertyNames.Resolution);
            }
        }

        private string _memo;
        /// <summary>Описание</summary>
        public string Memo
        {
            get { return _memo; }
            set
            {
                if (value == _memo) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }


        private DateTime? _dateSign;
        /// <summary>Дата фактической подписи</summary>
        public DateTime? DateSign
        {
            get { return _dateSign; }
            set
            {
                if (value == _dateSign) return;
                OnPropertyChanging(GlobalPropertyNames.DateSign);
                _dateSign = value;
                OnPropertyChanged(GlobalPropertyNames.DateSign);
            }
        }


        private bool _taskNeed;
        /// <summary>Необходимость формировать задачу</summary>
        public bool TaskNeed
        {
            get { return _taskNeed; }
            set
            {
                if (value == _taskNeed) return;
                OnPropertyChanging(GlobalPropertyNames.TaskNeed);
                _taskNeed = value;
                OnPropertyChanged(GlobalPropertyNames.TaskNeed);
            }
        }


        private bool _messageNeed;
        /// <summary>Необходимость формировать сообщение</summary>
        public bool MessageNeed
        {
            get { return _messageNeed; }
            set
            {
                if (value == _messageNeed) return;
                OnPropertyChanging(GlobalPropertyNames.MessageNeed);
                _messageNeed = value;
                OnPropertyChanged(GlobalPropertyNames.MessageNeed);
            }
        }


        private int _messageId;
        /// <summary>Идентификатор сообщения</summary>
        public int MessageId
        {
            get { return _messageId; }
            set
            {
                if (value == _messageId) return;
                OnPropertyChanging(GlobalPropertyNames.MessageId);
                _messageId = value;
                OnPropertyChanged(GlobalPropertyNames.MessageId);
            }
        }


        private int _taskId;
        /// <summary>Идентификатор задачи</summary>
        public int TaskId
        {
            get { return _taskId; }
            set
            {
                if (value == _taskId) return;
                OnPropertyChanging(GlobalPropertyNames.TaskId);
                _taskId = value;
                OnPropertyChanged(GlobalPropertyNames.TaskId);
            }
        }


        private bool _signKind;
        /// <summary>Тип подписания ИЛИ/И</summary>
        public bool SignKind
        {
            get { return _signKind; }
            set
            {
                if (value == _signKind) return;
                OnPropertyChanging(GlobalPropertyNames.SignKind);
                _signKind = value;
                OnPropertyChanged(GlobalPropertyNames.SignKind);
            }
        }


        private bool _isMain;
        /// <summary>Является ли главным подписантом</summary>
        public bool IsMain
        {
            get { return _isMain; }
            set
            {
                if (value == _isMain) return;
                OnPropertyChanging(GlobalPropertyNames.IsMain);
                _isMain = value;
                OnPropertyChanged(GlobalPropertyNames.IsMain);
            }
        }


        private int _agentSubId;
        /// <summary>Заместитель</summary>
        public int AgentSubId
        {
            get { return _agentSubId; }
            set
            {
                if (value == _agentSubId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentSubId);
                _agentSubId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentSubId);
            }
        }


        private Agent _agentSub;
        /// <summary>
        /// Сотрудник-заместитель инициатора подписания
        /// </summary>
        public Agent AgentSub
        {
            get
            {
                if (_agentSubId == 0)
                    return null;
                if (_agentSub == null)
                    _agentSub = Workarea.Cashe.GetCasheData<Agent>().Item(_agentSubId);
                else if (_agentSub.Id != _agentSubId)
                    _agentSub = Workarea.Cashe.GetCasheData<Agent>().Item(_agentSubId);
                return _agentSub;
            }
            set
            {
                if (_agentSub == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentSub);
                _agentSub = value;
                _agentSubId = _agentSub == null ? 0 : _agentSub.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentSub);
            }
        }


        private int _agentSignId;
        /// <summary>Идентификатор сотрудника фактически выполнившего подписание</summary>
        public int AgentSignId
        {
            get { return _agentSignId; }
            set
            {
                if (value == _agentSignId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentSignId);
                _agentSignId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentSignId);
            }
        }


        private Agent _agentSign;
        /// <summary>Сотудник фактически выполнивший подписания</summary>
        public Agent AgentSign
        {
            get
            {
                if (_agentSignId == 0)
                    return null;
                if (_agentSign == null)
                    _agentSign = Workarea.Cashe.GetCasheData<Agent>().Item(_agentSignId);
                else if (_agentSign.Id != _agentSignId)
                    _agentSign = Workarea.Cashe.GetCasheData<Agent>().Item(_agentSignId);
                return _agentSign;
            }
            set
            {
                if (_agentSign == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentSign);
                _agentSign = value;
                _agentSignId = _agentSign == null ? 0 : _agentSign.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentSign);
            }
        }
        
        
        private int _agentToId; 
        /// <summary>
        /// Идентификатор сотрудника которому необходимо рассмотреть и подписать документ
        /// </summary>
        public int AgentToId 
        { 
            get{ return _agentToId; } 
            set
            {
               if (value == _agentToId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentToId);
                _agentToId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentToId);
            } 
        }
        
        private Agent _agentTo; 
        /// <summary>
        /// Cотрудник которому необходимо рассмотреть и подписать документ
        /// </summary>
        public Agent AgentTo 
        { 
            get
            {
                if (_agentToId == 0)
                    return null;
                if (_agentTo == null)
                    _agentTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToId);
                else if (_agentTo.Id != _agentToId)
                    _agentTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToId);
                return _agentTo;
            }
            set
            {
                if (_agentTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentTo);
                _agentTo = value;
                _agentToId = _agentTo == null ? 0 : _agentTo.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentTo);
            }
        }
        
        private int _agentToSubId; 
        /// <summary>
        /// Идентификатор сотрудника-заместителя которому необходимо подписать документ
        /// </summary>
        public int AgentToSubId 
        { 
            get{ return _agentToSubId; } 
            set
            {
               if (value == _agentToSubId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentToSubId);
                _agentToSubId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentToSubId);
            } 
        }
        
        private Agent _agentToSub; 
        /// <summary>
        /// Cотрудник-заместителm которому необходимо подписать документ
        /// </summary>
        public Agent AgentToSub 
        { 
            get
            {
                if (_agentToSubId == 0)
                    return null;
                if (_agentToSub == null)
                    _agentToSub = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToSubId);
                else if (_agentToSub.Id != _agentToSubId)
                    _agentToSub = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToSubId);
                return _agentToSub;
            }
            set
            {
                if (_agentToSub == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentToSub);
                _agentToSub = value;
                _agentToSubId = _agentToSub == null ? 0 : _agentToSub.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentToSub);
            }
        }
        
        private int _priorityId; 
        /// <summary>
        /// Идентификатор приритета
        /// </summary>
        public int PriorityId 
        { 
            get{ return _priorityId; } 
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
        /// Приоритет
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
                OnPropertyChanging(GlobalPropertyNames.Priority);
                _priority = value;
                _priorityId = _priority == null ? 0 : _priority.Id;
                OnPropertyChanged(GlobalPropertyNames.Priority);
            }
        }
        
        private int _groupNo; 
        /// <summary>
        /// Номер группы
        /// </summary>
        public int GroupNo 
        { 
            get{ return _groupNo; } 
            set
            {
               if (value == _groupNo) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo);
                _groupNo = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo);
            } 
        }
        
        private int _depatmentId; 
        /// <summary>
        /// Идентификатор отдела
        /// </summary>
        public int DepatmentId
        { 
            get{ return _depatmentId; } 
            set
            {
               if (value == _depatmentId) return;
                OnPropertyChanging(GlobalPropertyNames.DepatmentId);
                _depatmentId = value;
                OnPropertyChanged(GlobalPropertyNames.DepatmentId);
            } 
        }
        
        private Depatment _depatment;
        /// <summary>
        /// Отдел
        /// </summary>
        public Depatment Depatment
        {
            get
            {
                if (_depatmentId == 0)
                    return null;
                if (_depatment == null)
                    _depatment = Workarea.Cashe.GetCasheData<Depatment>().Item(_depatmentId);
                else if (_depatment.Id != _depatmentId)
                    _depatment = Workarea.Cashe.GetCasheData<Depatment>().Item(_depatmentId);
                return _depatment;
            }
            set
            {
                if (_depatment == value) return;
                OnPropertyChanging(GlobalPropertyNames.Depatment);
                _depatment = value;
                _depatmentId = _depatment == null ? 0 : _depatment.Id;
                OnPropertyChanged(GlobalPropertyNames.Depatment);
            }
        }
        
        private int _groupLevelId; 
        /// <summary>
        /// Идентификатор уровня группы
        /// </summary>
        public int GroupLevelId 
        { 
            get{ return _groupLevelId; } 
            set
            {
               if (value == _groupLevelId) return;
                OnPropertyChanging(GlobalPropertyNames.GroupLevelId);
                _groupLevelId = value;
                OnPropertyChanged(GlobalPropertyNames.GroupLevelId);
            } 
        }

        private Analitic _groupLevel;
        /// <summary>
        /// Уровень группы
        /// </summary>
        public Analitic GroupLevel
        {
            get
            {
                if (_groupLevelId == 0)
                    return null;
                if (_groupLevel == null)
                    _groupLevel = Workarea.Cashe.GetCasheData<Analitic>().Item(_groupLevelId);
                else if (_groupLevel.Id != _groupLevelId)
                    _groupLevel = Workarea.Cashe.GetCasheData<Analitic>().Item(_groupLevelId);
                return _groupLevel;
            }
            set
            {
                if (_groupLevel == value) return;
                OnPropertyChanging(GlobalPropertyNames.GroupLevel);
                _groupLevel = value;
                _groupLevelId = _groupLevel == null ? 0 : _groupLevel.Id;
                OnPropertyChanged(GlobalPropertyNames.GroupLevel);
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

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (_agentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentId, XmlConvert.ToString(_agentId));
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
            if (_kind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Kind, XmlConvert.ToString(_kind));
            //if (_date != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
            if (_resolutionId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ResolutionId, XmlConvert.ToString(_resolutionId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_dateSign.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateSign, XmlConvert.ToString(_dateSign.Value));
            if (_taskNeed)
                writer.WriteAttributeString(GlobalPropertyNames.TaskNeed, XmlConvert.ToString(_taskNeed));
            if (_messageNeed)
                writer.WriteAttributeString(GlobalPropertyNames.MessageNeed, XmlConvert.ToString(_messageNeed));
            if (_messageId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.MessageId, XmlConvert.ToString(_messageId));
            if (_taskId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TaskId, XmlConvert.ToString(_taskId));
            if (_signKind)
                writer.WriteAttributeString(GlobalPropertyNames.SignKind, XmlConvert.ToString(_signKind));
            if (_isMain)
                writer.WriteAttributeString(GlobalPropertyNames.IsMain, XmlConvert.ToString(_isMain));
            if (_agentSubId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentSubId, XmlConvert.ToString(_agentSubId));
            if (_agentSignId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentSignId, XmlConvert.ToString(_agentSignId));

            if (_priorityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PriorityId, XmlConvert.ToString(_priorityId));
            if (_groupNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.GroupNo, XmlConvert.ToString(_groupNo));
            if (_depatmentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DepatmentId, XmlConvert.ToString(_depatmentId));
            if (_groupLevelId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.GroupLevelId, XmlConvert.ToString(_groupLevelId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentId) != null)
                _agentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentId));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
            if (reader.GetAttribute(GlobalPropertyNames.Kind) != null)
                _kind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Kind));
            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
            if (reader.GetAttribute(GlobalPropertyNames.ResolutionId) != null)
                _resolutionId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ResolutionId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.DateSign) != null)
                _dateSign = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateSign));
            if (reader.GetAttribute(GlobalPropertyNames.TaskNeed) != null)
                _taskNeed = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.TaskNeed));
            if (reader.GetAttribute(GlobalPropertyNames.MessageNeed) != null)
                _messageNeed = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.MessageNeed));
            if (reader.GetAttribute(GlobalPropertyNames.MessageId) != null)
                _messageId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.MessageId));
            if (reader.GetAttribute(GlobalPropertyNames.TaskId) != null)
                _taskId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TaskId));
            if (reader.GetAttribute(GlobalPropertyNames.SignKind) != null)
                _signKind = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.SignKind));
            if (reader.GetAttribute(GlobalPropertyNames.IsMain) != null)
                _isMain = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.IsMain));
            if (reader.GetAttribute(GlobalPropertyNames.AgentSubId) != null)
                _agentSubId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentSubId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentSignId) != null)
                _agentSignId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentSignId));

            if (reader.GetAttribute(GlobalPropertyNames.PriorityId) != null)
                _priorityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PriorityId));
            if (reader.GetAttribute(GlobalPropertyNames.GroupNo) != null)
                _groupNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.GroupNo));	
            if (reader.GetAttribute(GlobalPropertyNames.DepatmentId) != null)
                _depatmentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DepatmentId));
            if (reader.GetAttribute(GlobalPropertyNames.GroupLevelId) != null)
                _groupLevelId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.GroupLevelId));	
        }
        #endregion

        #region Методы
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (Id!=0 && _ownId == 0)
                throw new ValidateException("Не указан документ");
            if (_agentId == 0)
                throw new ValidateException("Не указан ответственный сотрудник (инициатор)");
            if (_date < SqlDateTime.MinValue || _date > SqlDateTime.MaxValue)
                throw new ValidateException("Не указана дата");
            if (_resolutionId == 0)
                throw new ValidateException("Не указана резолюция ");
            if(_agentToId==0 )
                throw new ValidateException("Не указана сотрудник подписания (подписант)");
        }
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.GetInt32(9);
                _agentId = reader.GetInt32(10);
                _orderNo = reader.GetInt32(11);
                _kind = reader.GetInt32(12);
                _date = reader.GetDateTime(13);
                _resolutionId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _memo = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);

                _agentSubId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                _isMain = reader.GetBoolean(17);
                _signKind = reader.GetBoolean(18);
                _taskId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _messageId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _messageNeed = reader.GetBoolean(21);
                _taskNeed = reader.GetBoolean(22);
                _dateSign = reader.IsDBNull(23) ? (DateTime?)null : reader.GetDateTime(23);
                _agentSignId = reader.IsDBNull(24) ? 0 : reader.GetInt32(24);

                _priorityId = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                _groupNo = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                _depatmentId = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                _groupLevelId = reader.IsDBNull(28) ? 0 : reader.GetInt32(28);
                _agentToId = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);
                _agentToSubId = reader.IsDBNull(30) ? 0 : reader.GetInt32(30);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _ownId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.AgentId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _agentId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Kind, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _kind;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.OrderNo, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _orderNo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Date, SqlDbType.Date);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _date;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.ResolutionId, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = _resolutionId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, 255);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _memo;


            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.AgentSubId, SqlDbType.Int);
            if(_agentSubId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentSubId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.IsMain, SqlDbType.Bit);
            prm.Value = _isMain;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.SignKind, SqlDbType.Bit);
            prm.Value = _signKind;
            
            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.TaskId, SqlDbType.Int);
            if(_taskId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _taskId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.MessageId, SqlDbType.Int);
            if(_messageId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _messageId;
	
            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.MessageNeed, SqlDbType.Bit);
            prm.Value = _messageNeed;


            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.TaskNeed, SqlDbType.Bit);
            prm.Value = _taskNeed;


            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.DateSign, SqlDbType.Date);
            if (_dateSign.HasValue)
                prm.Value = _dateSign;
            else
                prm.Value = DBNull.Value;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.AgentSignId, SqlDbType.Int);
            if (_agentSignId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentSignId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.PriorityId, SqlDbType.Int);
            if (_priorityId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _priorityId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.GroupNo, SqlDbType.Int);
            if (_groupNo == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _groupNo;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.DepatmentId, SqlDbType.Int);
            if (_depatmentId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _depatmentId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.GroupLevelId, SqlDbType.Int);
            if (_groupLevelId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _groupLevelId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.AgentToId, SqlDbType.Int);
            if (_agentToId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentToId;

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.AgentToSubId, SqlDbType.Int);
            if (_agentToSubId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _agentToSubId;
        }

        ///// <summary>
        ///// Удаление объекта из базы данных
        ///// </summary>
        //public void Delete()
        //{
        //    Workarea.DeleteById(Id, Workarea.Empty<Document>().Entity.FindMethod("SignatureDelete").FullName);    
        //}
        ///// <summary>
        ///// Загрузить данные объекта из базы данных по идентификатору
        ///// </summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.Empty<Document>().Entity.FindMethod("SignatureLoad").FullName);
        //}
        ///// <summary>
        ///// Загрузить текущий объект
        ///// </summary>
        ///// <remarks>Загрузка возможна только для объекта существующего в базе данных, чей идентификатор не равен 0</remarks>
        //public void Load()
        //{
        //    Load(Id);
        //}
        #endregion

        private DocumentSignStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentSignStruct
                                  {
                                      AgentId = AgentId,
                                      Date = Date,
                                      OwnId = OwnId,
                                      Kind = Kind,
                                      OrderNo = OrderNo,
                                      ResolutionId = ResolutionId,
                                      Memo = Memo,
                                      AgentSignId = AgentSignId,
                                      AgentSubId = AgentSubId,
                                      DateSign = DateSign,
                                      DepatmentId = DepatmentId,
                                      GroupLevelId = GroupLevelId,
                                      GroupNo = GroupNo,
                                      IsMain = IsMain,
                                      MessageId = MessageId,
                                      MessageNeed = MessageNeed,
                                      PriorityId = PriorityId,
                                      SignKind = SignKind,
                                      TaskId = TaskId,
                                      TaskNeed = TaskNeed
                                  };
                return true;
            }
            return false;
        }
        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            AgentId = _baseStruct.AgentId;
            Date = _baseStruct.Date;
            OwnId = _baseStruct.OwnId;
            Kind = _baseStruct.Kind;
            OrderNo = _baseStruct.OrderNo;
            ResolutionId = _baseStruct.ResolutionId;
            Memo = _baseStruct.Memo;
            AgentSignId = _baseStruct.AgentSignId;
            AgentSubId = _baseStruct.AgentSubId;
            DateSign = _baseStruct.DateSign;
            DepatmentId = _baseStruct.DepatmentId;
            GroupLevelId = _baseStruct.GroupLevelId;
            GroupNo = _baseStruct.GroupNo;
            IsMain = _baseStruct.IsMain;
            MessageId = _baseStruct.MessageId;
            MessageNeed = _baseStruct.MessageNeed;
            PriorityId = _baseStruct.PriorityId;
            SignKind = _baseStruct.SignKind;
            TaskId = _baseStruct.TaskId;
            TaskNeed = _baseStruct.TaskNeed;
            IsChanged = false;
        }

        internal class TpvCollection : List<DocumentSign>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                var sdr = new SqlDataRecord
                (
                    new SqlMetaData(GlobalPropertyNames.Id, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Guid, SqlDbType.UniqueIdentifier),
                    new SqlMetaData(GlobalPropertyNames.DatabaseId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DbSourceId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Version, SqlDbType.Binary, 8),
                    new SqlMetaData(GlobalPropertyNames.UserName, SqlDbType.NVarChar, 50),
                    new SqlMetaData(GlobalPropertyNames.DateModified, SqlDbType.DateTime),
                    new SqlMetaData(GlobalPropertyNames.Flags, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StateId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.OwnId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.OrderNo, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Kind, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Date, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.ResolutionId, SqlDbType.Int),
	                new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, -1),
                    new SqlMetaData(GlobalPropertyNames.AgentSubId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.IsMain, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.SignKind, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.TaskId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.MessageId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.MessageNeed, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.TaskNeed, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.DateSign, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.AgentSignId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.PriorityId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.GroupNo, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DepatmentId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.GroupLevelId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentToId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentToSubId, SqlDbType.Int)
                );

                foreach (DocumentSign doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentSign doc)
        {
            sdr.SetInt32(0, doc.Id);
            sdr.SetGuid(1, doc.Guid);
            sdr.SetInt32(2, doc.DatabaseId);
            if (doc.DbSourceId == 0)
                sdr.SetValue(3, DBNull.Value);
            else
                sdr.SetInt32(3, doc.DbSourceId);
            if (doc.ObjectVersion == null || doc.ObjectVersion.All(v => v == 0))
                sdr.SetValue(4, DBNull.Value);
            else
                sdr.SetValue(4, doc.ObjectVersion);

            if (string.IsNullOrEmpty(doc.UserName))
                sdr.SetValue(5, DBNull.Value);
            else
                sdr.SetString(5, doc.UserName);

            if (doc.DateModified.HasValue)
                sdr.SetDateTime(6, doc.DateModified.Value);
            else
                sdr.SetValue(6, DBNull.Value);

            sdr.SetInt32(7, doc.FlagsValue);
            sdr.SetInt32(8, doc.StateId);
            sdr.SetInt32(9, doc.OwnId);
            sdr.SetInt32(10, doc.AgentId);
            sdr.SetInt32(11, doc.OrderNo);
            sdr.SetInt32(12, doc.Kind);
            sdr.SetDateTime(13, doc.Date);

            if (doc.ResolutionId!=0)
                sdr.SetInt32(14, doc.ResolutionId);
            else
                sdr.SetValue(14, DBNull.Value);

            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetString(15, doc.Memo);
	
            if (doc.AgentSubId!=0)
                sdr.SetInt32(16, doc.AgentSubId);
            else
                sdr.SetValue(16, DBNull.Value);
	
            sdr.SetBoolean(17, doc.IsMain);
            sdr.SetBoolean(18, doc.SignKind);
	
            if (doc.TaskId!=0)
                sdr.SetInt32(19, doc.TaskId);
            else
                sdr.SetValue(19, DBNull.Value);
            
            if (doc.MessageId!=0)
                sdr.SetInt32(20, doc.MessageId);
            else
                sdr.SetValue(20, DBNull.Value);
	
            sdr.SetBoolean(21, doc.MessageNeed);
            sdr.SetBoolean(22, doc.TaskNeed);
	
            if (doc.DateSign.HasValue)
                sdr.SetDateTime(23, doc.DateSign.Value);
            else
                sdr.SetValue(23, DBNull.Value);

            if (doc.AgentSignId!=0)
                sdr.SetInt32(24, doc.AgentSignId);
            else
                sdr.SetValue(24, DBNull.Value);
	
            if (doc.PriorityId!=0)
                sdr.SetInt32(25, doc.PriorityId);
            else
                sdr.SetValue(25, DBNull.Value);
	
            if (doc.GroupNo!=0)
                sdr.SetInt32(26, doc.GroupNo);
            else
                sdr.SetValue(26, DBNull.Value);

            if (doc.DepatmentId!=0)
                sdr.SetInt32(27, doc.DepatmentId);
            else
                sdr.SetValue(27, DBNull.Value);
	
            if (doc.GroupLevelId!=0)
                sdr.SetInt32(28, doc.GroupLevelId);
            else
                sdr.SetValue(28, DBNull.Value);

            if (doc.AgentToId != 0)
                sdr.SetInt32(29, doc.AgentToId);
            else
                sdr.SetValue(29, DBNull.Value);

            if (doc.AgentToSubId!=0)
                sdr.SetInt32(30, doc.AgentToSubId);
            else
                sdr.SetValue(30, DBNull.Value);
            
            return sdr;
        }
    }
}
