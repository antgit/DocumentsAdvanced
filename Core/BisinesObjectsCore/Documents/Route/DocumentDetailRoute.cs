using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Data;

namespace BusinessObjects.Documents
{
    internal struct BaseStructDocumentDetailRoute
    {
        /// <summary>Идентификатор клиента</summary>
        public int AgentId;
        /// <summary>Идентификатор адреса клиента</summary>
        public int AddressId;
        /// <summary>Идентификатор статуса посещения</summary>
        public int StatusId;
        /// <summary>Плановая дата прибытия</summary>
        public DateTime? PlanDate;
        /// <summary>Плановое время прибытия</summary>
        public TimeSpan? PlanTime;
        /// <summary>Плановое время стоянки (минут)</summary>
        public int PlanStaying;
        /// <summary>Фактическая дата прибытия</summary>
        public DateTime? FactDate;
        /// <summary>Фактическое время прибытия</summary>
        public TimeSpan? FactTime;
        /// <summary>Фактическое время стоянки (минут)</summary>
        public int FactStaying;
        /// <summary>Идентификатор состояния посещения клиента (плановое/внеплановое)</summary>
        public int StatusFactId;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Порядок следования</summary>
        public int OrderNo;
    }

    /// <summary>
    /// Детализация документа "Маршрут торгового представителя"
    /// </summary>
    public class DocumentDetailRoute : DocumentBaseDetail, IEditableObject, IChainsAdvancedList<DocumentDetailRoute, FileData>
    {
        public DocumentDetailRoute() : base()
        {
            EntityId = 15;
        }

        #region Свойства
        
        /// <summary>
        /// Основной документ
        /// </summary>
        public DocumentRoute Document { get; set; }

        private int _agentId;
        /// <summary>Идентификатор клиента</summary>
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
                    return null;
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
                _agentId = _agent == null ? 0 : _agent.Id;
                OnPropertyChanged(GlobalPropertyNames.Agent);
            }
        }

        private int _addressId;
        /// <summary>Идентификатор адреса клиента</summary>
        public int AddressId
        {
            get { return _addressId; }
            set
            {
                if (value == _addressId) return;
                OnPropertyChanging(GlobalPropertyNames.AddressId);
                _addressId = value;
                OnPropertyChanged(GlobalPropertyNames.AddressId);
            }
        }

        private AgentAddress _address;
        /// <summary>
        /// Адрес корреспондент
        /// </summary>
        public AgentAddress Address
        {
            get
            {
                if (_addressId == 0)
                    return null;
                if (_address == null)
                    _address = Workarea.Cashe.GetCasheData<AgentAddress>().Item(_addressId);
                else if (_agent.Id != _agentId)
                    _address = Workarea.Cashe.GetCasheData<AgentAddress>().Item(_addressId);
                return _address;
            }
            set
            {
                if (_address == value) return;
                OnPropertyChanging(GlobalPropertyNames.Address);
                _address = value;
                _addressId = _address == null ? 0 : _address.Id;
                OnPropertyChanged(GlobalPropertyNames.Address);
            }
        }

        private int _statusId;
        /// <summary>Идентификатор статуса посещения</summary>
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
        /// Статус посещения
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
                OnPropertyChanging(GlobalPropertyNames.Status);
                _status = value;
                _statusId = _status == null ? 0 : _status.Id;
                OnPropertyChanged(GlobalPropertyNames.Status);
            }
        }
        

        private DateTime? _planDate;
        /// <summary>Плановая дата прибытия</summary>
        public DateTime? PlanDate
        {
            get { return _planDate; }
            set
            {
                if (value == _planDate) return;
                OnPropertyChanging(GlobalPropertyNames.PlanDate);
                _planDate = value;
                OnPropertyChanged(GlobalPropertyNames.PlanDate);
            }
        }

        private TimeSpan? _planTime;
        /// <summary>Плановое время прибытия</summary>
        public TimeSpan? PlanTime
        {
            get { return _planTime; }
            set
            {
                if (value == _planTime) return;
                OnPropertyChanging(GlobalPropertyNames.PlanTime);
                _planTime = value;
                OnPropertyChanged(GlobalPropertyNames.PlanTime);
            }
        }

        private int _planStaying;
        /// <summary>Плановое время стоянки (минут)</summary>
        public int PlanStaying
        {
            get { return _planStaying; }
            set
            {
                if (value == _planStaying) return;
                OnPropertyChanging(GlobalPropertyNames.PlanStaying);
                _planStaying = value;
                OnPropertyChanged(GlobalPropertyNames.PlanStaying);
            }
        }

        private DateTime? _factDate;
        /// <summary>Фактическая дата прибытия</summary>
        public DateTime? FactDate
        {
            get { return _factDate; }
            set
            {
                if (value == _factDate) return;
                OnPropertyChanging(GlobalPropertyNames.FactDate);
                _factDate = value;
                OnPropertyChanged(GlobalPropertyNames.FactDate);
            }
        }

        private TimeSpan? _factTime;
        /// <summary>Фактическое время прибытия</summary>
        public TimeSpan? FactTime
        {
            get { return _factTime; }
            set
            {
                if (value == _factTime) return;
                OnPropertyChanging(GlobalPropertyNames.FactTime);
                _factTime = value;
                OnPropertyChanged(GlobalPropertyNames.FactTime);
            }
        }

        private int _factStaying;
        /// <summary>Фактическое время стоянки (минут)</summary>
        public int FactStaying
        {
            get { return _factStaying; }
            set
            {
                if (value == _factStaying) return;
                OnPropertyChanging(GlobalPropertyNames.FactStaying);
                _factStaying = value;
                OnPropertyChanged(GlobalPropertyNames.FactStaying);
            }
        }

        private int _statusFactId;
        /// <summary>Идентификатор состояния посещения клиента (плановое/внеплановое)</summary>
        public int StatusFactId
        {
            get { return _statusFactId; }
            set
            {
                if (value == _statusFactId) return;
                OnPropertyChanging(GlobalPropertyNames.StatusFactId);
                _statusFactId = value;
                OnPropertyChanged(GlobalPropertyNames.StatusFactId);
            }
        }

        private Analitic _statusFact;
        /// <summary>
        /// Фастический статус посещения клиента (посетил/непосетил/отложен)
        /// </summary>
        public Analitic StatusFact
        {
            get
            {
                if (_statusFactId == 0)
                    return null;
                if (_statusFact == null)
                    _statusFact = Workarea.Cashe.GetCasheData<Analitic>().Item(_statusFactId);
                else if (_statusFact.Id != _statusFactId)
                    _statusFact = Workarea.Cashe.GetCasheData<Analitic>().Item(_statusFactId);
                return _statusFact;
            }
            set
            {
                if (_statusFact == value) return;
                OnPropertyChanging(GlobalPropertyNames.StatusFact);
                _statusFact = value;
                _statusFactId = _statusFact == null ? 0 : _statusFact.Id;
                OnPropertyChanged(GlobalPropertyNames.StatusFact);
            }
        }
        

        private string _memo;
        /// <summary>Примечание</summary>
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

        public int _orderNo;
        /// <summary>Порядок следования</summary>
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


        #endregion

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);
            if (_agentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentId, XmlConvert.ToString(_agentId));
            if (_addressId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AddressId, XmlConvert.ToString(_addressId));
            if (_statusId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.StatusId, XmlConvert.ToString(_statusId));
            if (_planDate != null)
                writer.WriteAttributeString(GlobalPropertyNames.PlanDate, XmlConvert.ToString((DateTime)_planDate));
            if (_planTime != null)
                writer.WriteAttributeString(GlobalPropertyNames.PlanTime, XmlConvert.ToString((TimeSpan)_planTime));
            if (_planStaying != null)
                writer.WriteAttributeString(GlobalPropertyNames.PlanStaying, XmlConvert.ToString(_planStaying));
            if (_factDate != null)
                writer.WriteAttributeString(GlobalPropertyNames.FactDate, XmlConvert.ToString((DateTime)_factDate));
            if (_factTime != null)
                writer.WriteAttributeString(GlobalPropertyNames.FactTime, XmlConvert.ToString((TimeSpan)_factTime));
            if (_factStaying != null)
                writer.WriteAttributeString(GlobalPropertyNames.FactStaying, XmlConvert.ToString(_factStaying));
            if (_statusFactId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.StatusFactId, XmlConvert.ToString(_statusFactId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);
            if (reader.GetAttribute(GlobalPropertyNames.AgentId) != null)
                _agentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentId));
            if (reader.GetAttribute(GlobalPropertyNames.AddressId) != null)
                _addressId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AddressId));
            if (reader.GetAttribute(GlobalPropertyNames.StatusId) != null)
                _statusId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.StatusId));
            if (reader.GetAttribute(GlobalPropertyNames.PlanDate) != null)
                _planDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.PlanDate));
            if (reader.GetAttribute(GlobalPropertyNames.PlanTime) != null)
                _planTime = XmlConvert.ToTimeSpan(reader.GetAttribute(GlobalPropertyNames.PlanTime));
            if (reader.GetAttribute(GlobalPropertyNames.PlanStaying) != null)
                _planStaying = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PlanStaying));
            if (reader.GetAttribute(GlobalPropertyNames.FactDate) != null)
                _factDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.FactDate));
            if (reader.GetAttribute(GlobalPropertyNames.FactTime) != null)
                _factTime = XmlConvert.ToTimeSpan(reader.GetAttribute(GlobalPropertyNames.FactTime));
            if (reader.GetAttribute(GlobalPropertyNames.FactStaying) != null)
                _factStaying = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FactStaying));
            if (reader.GetAttribute(GlobalPropertyNames.StatusFactId) != null)
                _statusFactId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.StatusFactId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
        }

        #endregion

        #region Состояния

        BaseStructDocumentDetailRoute _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailRoute
                {
                    AddressId = _addressId,
                    AgentId = _agentId,
                    FactTime = _factTime,
                    FactDate = _factDate,
                    FactStaying = _factStaying,
                    Memo = _memo,
                    PlanDate = _planDate,
                    PlanStaying = _planStaying,
                    PlanTime = _planTime,
                    StatusFactId = _statusFactId,
                    StatusId = _statusId,
                    OrderNo = _orderNo
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
            _addressId = _baseStruct.AddressId;
            _agentId = _baseStruct.AgentId;
            _factTime = _baseStruct.FactTime;
            _factDate = _baseStruct.FactDate;
            _factStaying = _baseStruct.FactStaying;
            _memo = _baseStruct.Memo;
            _planDate = _baseStruct.PlanDate;
            _planTime = _baseStruct.PlanTime;
            _planStaying = _baseStruct.PlanStaying;
            _statusFactId = _baseStruct.StatusFactId;
            _statusId = _baseStruct.StatusId;
            _orderNo = _baseStruct.OrderNo;
            IsChanged = false;
        }

        #endregion

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (StateId != State.STATEDELETED)
            {
                StateId = Document.StateId;
            }
            Date = Document.Date;
            if (Kind == 0)
                Kind = Document.Kind;
            if (Kind == 0)
            {
                throw new ValidateException("Не указан тип строки документа");
            }
            if (_agentId== 0)
                throw new ValidateException("Не указан клиент");
            if (_addressId == 0)
                throw new ValidateException("Не указан адрес клиента");

            if (Id == 0)
                _mGuid = Guid.NewGuid();
            else
                _mGuid = Guid;
        }

        #region База данных

        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _agentId = reader.GetInt32(12);
                _addressId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _statusId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _planDate = reader.IsDBNull(15) ? null : (DateTime?)reader.GetDateTime(15);
                _planTime = reader.IsDBNull(16) ? null : (TimeSpan?)reader.GetTimeSpan(16);
                _planStaying = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _factDate = reader.IsDBNull(18) ? null : (DateTime?)reader.GetDateTime(18);
                _factTime = reader.IsDBNull(19) ? null : (TimeSpan?)reader.GetTimeSpan(19);
                _factStaying = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _statusFactId = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                _memo = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _orderNo = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
                _mGuid = reader.IsDBNull(24) ? Guid.Empty : reader.GetGuid(24);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailRoute>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.Date, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.OwnId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AddressId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StatusId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.PlanDate, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.PlanTime, SqlDbType.Time),
                    new SqlMetaData(GlobalPropertyNames.PlanStaying, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.FactDate, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.FactTime, SqlDbType.Time),
                    new SqlMetaData(GlobalPropertyNames.FactStaying, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StatusFactId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.VarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.OrderNo, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                );
                foreach (DocumentDetailRoute doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }

        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailRoute doc)
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
            sdr.SetDateTime(9, doc.Date);
            sdr.SetInt32(10, doc.Document.Id);
            sdr.SetInt32(11, doc.Kind);
            
            sdr.SetInt32(12, doc.AgentId);
            sdr.SetInt32(13, doc.AddressId);

            if (doc.StatusId == 0)
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetInt32(14, doc.StatusId);
            
            if (!doc.PlanDate.HasValue)
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetDateTime(15, doc.PlanDate.Value);
            
            if (!doc.PlanTime.HasValue)
                sdr.SetValue(16, DBNull.Value);
            else
                sdr.SetTimeSpan(16, doc.PlanTime.Value);

            if (doc.PlanStaying == 0)
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetInt32(17, doc.PlanStaying);

            if (!doc.FactDate.HasValue)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetDateTime(18, doc.FactDate.Value);

            if (!doc.FactTime.HasValue)
                sdr.SetValue(19, DBNull.Value);
            else
                sdr.SetTimeSpan(19, doc.FactTime.Value);

            if (doc.FactStaying == 0)
                sdr.SetValue(20, DBNull.Value);
            else
                sdr.SetInt32(20, doc.FactStaying);

            if (doc.StatusFactId == 0)
                sdr.SetValue(21, DBNull.Value);
            else
                sdr.SetInt32(21, doc.StatusFactId);

            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(22, DBNull.Value);
            else
                sdr.SetString(22, doc.Memo);
            sdr.SetInt32(23, doc.OrderNo);
            sdr.SetGuid(24, doc.MGuid);
            return sdr;
        }

        #endregion

        #region IEditableObject Members

        void IEditableObject.BeginEdit()
        {
            SaveState(false);
        }

        void IEditableObject.CancelEdit()
        {
            RestoreState();
        }

        void IEditableObject.EndEdit()
        {
            _baseStruct = new BaseStructDocumentDetailRoute();
        }

        #endregion

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailRoute,FileData> Members

        List<IChainAdvanced<DocumentDetailRoute, FileData>> IChainsAdvancedList<DocumentDetailRoute, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailRoute, FileData>)this).GetLinks(50);
        }

        List<IChainAdvanced<DocumentDetailRoute, FileData>> IChainsAdvancedList<DocumentDetailRoute, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }

        List<ChainValueView> IChainsAdvancedList<DocumentDetailRoute, FileData>.GetChainView()
        {
            return null; //ChainValueView.GetView<Agent, FileData>(this);
        }

        public List<IChainAdvanced<DocumentDetailRoute, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailRoute, FileData>> collection = new List<IChainAdvanced<DocumentDetailRoute, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<Library>().Entity.FindMethod("LoadFiles").FullName;
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
                                ChainAdvanced<DocumentDetailRoute, FileData> item = new ChainAdvanced<DocumentDetailRoute, FileData> { Workarea = Workarea, Left = this };
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
    }
}
