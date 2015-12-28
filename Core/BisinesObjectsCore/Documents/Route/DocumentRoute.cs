using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{
    internal struct BaseStructDocumentRoute
    {
        /// <summary>Идентификатор устройства слежения</summary>
        public int DeviceId;
        /// <summary>Идентификатор менеджера</summary>
        public int ManagerId;
        /// <summary>Идентификатор старшего менеджера</summary>
        public int SupervisorId;
        /// <summary>Идентификатор объекта слежения</summary>
        public int RouteMemberId;
        /// <summary>Идентификатор статуса документа</summary>
        public int StatusId;
        /// <summary>Плановая дата документа</summary>
        public DateTime? PlanDate;
        /// <summary>Коэффициент сложности маршрута</summary>
        public decimal Multiplier;

        /// <summary>Понедельник</summary>
        public bool Monday;
        /// <summary>Вторник</summary>
        public bool Tuesday;
        /// <summary>Среда</summary>
        public bool Wednesday;
        /// <summary>Четверг</summary>
        public bool Thursday;
        /// <summary>Пятница</summary>
        public bool Friday;
        /// <summary>Суббота</summary>
        public bool Saturday;
        /// <summary>Воскресенье</summary>
        public bool Sunday;
    }

    /// <summary>
    /// Документ "Маршрут торгового агента"
    /// </summary>
    public class DocumentRoute : DocumentBase, IEditableObject, IChainsAdvancedList<Document, FileData>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Плановый маршрут, соответствует значению 1</summary>
        public const int KINDVALUE_PLAN = 1;
        /// <summary>Фактический маршрут, соответствует значению 2</summary>
        public const int KINDVALUE_FACT = 2;
        /// <summary>Планово-фактический маршрут, соответствует значению 3</summary>
        public const int KINDVALUE_PLANFACT = 3;
        /// <summary>Настройки раздела, соответствует значению 100</summary>
        public const int KINDVALUE_CONFIG = 100;

        /// <summary>Расход, соответствует значению 983041</summary>
        public const int KINDID_PLAN = 983041;
        /// <summary>Приход, соответствует значению 983042</summary>
        public const int KINDID_FACT = 983042;
        /// <summary>Заказ поставщику, соответствует значению 983043</summary>
        public const int KINDID_PLANFACT = 983043;
        /// <summary>Настройки раздела, соответствует значению 983140</summary>
        public const int KINDID_CONFIG = 983140;
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentRoute(): base()
        {
            EntityId = 15;
            _details = new List<DocumentDetailRoute>();
            _analitics = new List<DocumentAnalitic>();
        }

        #region Свойства

        private List<DocumentDetailRoute> _details;
        /// <summary>
        /// Детализация документа
        /// </summary>
        public List<DocumentDetailRoute> Details
        {
            get { return _details; }
            set { _details = value; }
        }

        private List<DocumentAnalitic> _analitics;
        /// <summary>
        /// Детализация документа на уровне аналитик
        /// </summary>
        public List<DocumentAnalitic> Analitics
        {
            get { return _analitics; }
            set { _analitics = value; }
        }

        private int _statusId;
        /// <summary>
        /// Статус документа
        /// </summary>
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

        private int _deviceId;
        /// <summary>
        /// Идентификатор устройства слежения
        /// </summary>
        public int DeviceId
        {
            get { return _deviceId; }
            set
            {
                if (value == _deviceId) return;
                OnPropertyChanging(GlobalPropertyNames.DeviceId);
                _deviceId = value;
                OnPropertyChanged(GlobalPropertyNames.DeviceId);
            }
        }

        private Device _device;
        /// <summary>
        /// Устройство слежения
        /// </summary>
        public Device Device
        {
            get
            {
                if (_deviceId == 0)
                    return null;
                if (_device == null)
                    _device = Workarea.Cashe.GetCasheData<Device>().Item(_deviceId);
                else if (_device.Id != _deviceId)
                    _device = Workarea.Cashe.GetCasheData<Device>().Item(_deviceId);
                return _device;
            }
            set
            {
                if (_device == value) return;
                OnPropertyChanging(GlobalPropertyNames.Device);
                _device = value;
                _deviceId = _device == null ? 0 : _device.Id;
                OnPropertyChanged(GlobalPropertyNames.Device);
            }
        }

        private int _routeMemberId;
        /// <summary>
        /// Идентификатор объекта слежения
        /// </summary>
        public int RouteMemberId
        {
            get { return _routeMemberId; }
            set
            {
                if (value == _routeMemberId) return;
                OnPropertyChanging(GlobalPropertyNames.RouteMemberId);
                _routeMemberId = value;
                OnPropertyChanged(GlobalPropertyNames.RouteMemberId);
            }
        }

        private RouteMember _routeMember;
        /// <summary>
        /// Объект слежения
        /// </summary>
        public RouteMember RouteMember
        {
            get
            {
                if (_routeMemberId == 0)
                    return null;
                if (_routeMember == null)
                    _routeMember = Workarea.Cashe.GetCasheData<RouteMember>().Item(_routeMemberId);
                else if (_routeMember.Id != _routeMemberId)
                    _routeMember = Workarea.Cashe.GetCasheData<RouteMember>().Item(_routeMemberId);
                return _routeMember;
            }
            set
            {
                if (_routeMember == value) return;
                OnPropertyChanging(GlobalPropertyNames.RouteMember);
                _routeMember = value;
                _routeMemberId = _routeMember == null ? 0 : _routeMember.Id;
                OnPropertyChanged(GlobalPropertyNames.RouteMember);
            }
        }

        private DateTime? _planDate;
        /// <summary>
        /// 
        /// </summary>
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

        private int _supervisorId;
        /// <summary>
        /// Идентификатор старшого менеджера
        /// </summary>
        public int SupervisorId
        {
            get { return _supervisorId; }
            set
            {
                if (value == _supervisorId) return;
                OnPropertyChanging(GlobalPropertyNames.SupervisorId);
                _supervisorId = value;
                OnPropertyChanged(GlobalPropertyNames.SupervisorId);
            }
        }

        private Agent _supervisor;
        /// <summary>
        /// Старший менеджер
        /// </summary>
        public Agent Supervisor
        {
            get
            {
                if (_supervisorId == 0)
                    return null;
                if (_supervisor == null)
                    _supervisor = Workarea.Cashe.GetCasheData<Agent>().Item(_supervisorId);
                else if (_supervisor.Id != _supervisorId)
                    _supervisor = Workarea.Cashe.GetCasheData<Agent>().Item(_supervisorId);
                return _supervisor;
            }
            set
            {
                if (_supervisor == value) return;
                OnPropertyChanging(GlobalPropertyNames.Supervisor);
                _supervisor = value;
                _supervisorId = _supervisor == null ? 0 : _supervisor.Id;
                OnPropertyChanged(GlobalPropertyNames.Supervisor);
            }
        }

        private int _managerId;
        /// <summary>
        /// Идентификатор менеджера
        /// </summary>
        public int ManagerId
        {
            get { return _managerId; }
            set
            {
                if (value == _managerId) return;
                OnPropertyChanging(GlobalPropertyNames.ManagerId);
                _managerId = value;
                OnPropertyChanged(GlobalPropertyNames.ManagerId);
            }
        }

        private Agent _manager;
        /// <summary>
        /// Менеджер
        /// </summary>
        public Agent Manager
        {
            get
            {
                if (_managerId == 0)
                    return null;
                if (_manager == null)
                    _manager = Workarea.Cashe.GetCasheData<Agent>().Item(_managerId);
                else if (_manager.Id != _managerId)
                    _manager = Workarea.Cashe.GetCasheData<Agent>().Item(_managerId);
                return _manager;
            }
            set
            {
                if (_manager == value) return;
                OnPropertyChanging(GlobalPropertyNames.Manager);
                _manager = value;
                _managerId = _manager == null ? 0 : _manager.Id;
                OnPropertyChanged(GlobalPropertyNames.Manager);
            }
        }

        private decimal _multiplier;
        /// <summary>
        /// Коэффициент сложности маршрута
        /// </summary>
        public decimal Multiplier
        {
            get { return _multiplier; }
            set
            {
                if (value == _multiplier) return;
                OnPropertyChanging(GlobalPropertyNames.Multiplier);
                _multiplier = value;
                OnPropertyChanged(GlobalPropertyNames.Multiplier);
            }
        }

        public bool _monday;
        /// <summary>Понедельник</summary>
        public bool Monday
        {
            get { return _monday; }
            set
            {
                if (value == _monday) return;
                OnPropertyChanging(GlobalPropertyNames.Monday);
                _monday = value;
                OnPropertyChanged(GlobalPropertyNames.Monday);
            }
        }
        
        public bool _tuesday;
        /// <summary>Вторник</summary>
        public bool Tuesday
        {
            get { return _tuesday; }
            set
            {
                if (value == _tuesday) return;
                OnPropertyChanging(GlobalPropertyNames.Thursday);
                _tuesday = value;
                OnPropertyChanged(GlobalPropertyNames.Thursday);
            }
        }

        
        public bool _wednesday;
        /// <summary>Среда</summary>
        public bool Wednesday
        {
            get { return _wednesday; }
            set
            {
                if (value == _wednesday) return;
                OnPropertyChanging(GlobalPropertyNames.Wednesday);
                _wednesday = value;
                OnPropertyChanged(GlobalPropertyNames.Wednesday);
            }
        }

        public bool _thursday;
        /// <summary>Четверг</summary>
        public bool Thursday
        {
            get { return _thursday; }
            set
            {
                if (value == _thursday) return;
                OnPropertyChanging(GlobalPropertyNames.Thursday);
                _thursday = value;
                OnPropertyChanged(GlobalPropertyNames.Thursday);
            }
        }

        public bool _friday;
        /// <summary>Пятница</summary>
        public bool Friday
        {
            get { return _friday; }
            set
            {
                if (value == _friday) return;
                OnPropertyChanging(GlobalPropertyNames.Friday);
                _friday = value;
                OnPropertyChanged(GlobalPropertyNames.Friday);
            }
        }

        public bool _saturday;
        /// <summary>Суббота</summary>
        public bool Saturday
        {
            get { return _saturday; }
            set
            {
                if (value == _saturday) return;
                OnPropertyChanging(GlobalPropertyNames.Saturday);
                _saturday = value;
                OnPropertyChanged(GlobalPropertyNames.Saturday);
            }
        }

        public bool _sunday;
        /// <summary>Воскресенье</summary>
        public bool Sunday
        {
            get { return _sunday; }
            set
            {
                if (value == _sunday) return;
                OnPropertyChanging(GlobalPropertyNames.Sunday);
                _sunday = value;
                OnPropertyChanged(GlobalPropertyNames.Sunday);
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

            if (_supervisorId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SupervisorId, XmlConvert.ToString(_supervisorId));
            if (_managerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ManagerId, XmlConvert.ToString(_managerId));
            if (_deviceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.DeviceId, XmlConvert.ToString(_deviceId));
            if (_routeMemberId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RouteMemberId, XmlConvert.ToString(_routeMemberId));
            if (_statusId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.StatusId, XmlConvert.ToString(_statusId));
            if (_planDate != null)
                writer.WriteAttributeString(GlobalPropertyNames.PlanDate, XmlConvert.ToString((DateTime)_planDate));
            if (_multiplier != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Multiplier, XmlConvert.ToString(_multiplier));
            writer.WriteAttributeString(GlobalPropertyNames.Monday, XmlConvert.ToString(_monday));
            writer.WriteAttributeString(GlobalPropertyNames.Tuesday, XmlConvert.ToString(_tuesday));
            writer.WriteAttributeString(GlobalPropertyNames.Wednesday, XmlConvert.ToString(_wednesday));
            writer.WriteAttributeString(GlobalPropertyNames.Thursday, XmlConvert.ToString(_thursday));
            writer.WriteAttributeString(GlobalPropertyNames.Friday, XmlConvert.ToString(_friday));
            writer.WriteAttributeString(GlobalPropertyNames.Saturday, XmlConvert.ToString(_saturday));
            writer.WriteAttributeString(GlobalPropertyNames.Sunday, XmlConvert.ToString(_sunday));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.SupervisorId) != null)
                _supervisorId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SupervisorId));
            if (reader.GetAttribute(GlobalPropertyNames.ManagerId) != null)
                _managerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ManagerId));
            if (reader.GetAttribute(GlobalPropertyNames.DeviceId) != null)
                _deviceId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.DeviceId));
            if (reader.GetAttribute(GlobalPropertyNames.RouteMemberId) != null)
                _routeMemberId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RouteMemberId));
            if (reader.GetAttribute(GlobalPropertyNames.StatusId) != null)
                _statusId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.StatusId));
            if (reader.GetAttribute(GlobalPropertyNames.PlanDate) != null)
                _planDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.PlanDate));
            if (reader.GetAttribute(GlobalPropertyNames.Multiplier) != null)
                _multiplier = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Multiplier));

            if (reader.GetAttribute(GlobalPropertyNames.Monday) != null)
                _monday = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Monday));
            if (reader.GetAttribute(GlobalPropertyNames.Tuesday) != null)
                _tuesday = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Tuesday));
            if (reader.GetAttribute(GlobalPropertyNames.Wednesday) != null)
                _wednesday = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Wednesday));
            if (reader.GetAttribute(GlobalPropertyNames.Thursday) != null)
                _thursday = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Thursday));
            if (reader.GetAttribute(GlobalPropertyNames.Friday) != null)
                _friday = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Friday));
            if (reader.GetAttribute(GlobalPropertyNames.Saturday) != null)
                _saturday = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Saturday));
            if (reader.GetAttribute(GlobalPropertyNames.Sunday) != null)
                _sunday = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.Sunday));
        }

        #endregion

        #region Состояния

        BaseStructDocumentRoute _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentRoute
                {
                    DeviceId = _deviceId,
                    ManagerId = _managerId,
                    Multiplier = _multiplier,
                    PlanDate = _planDate,
                    RouteMemberId = _routeMemberId,
                    StatusId = _statusId,
                    SupervisorId = _supervisorId,
                    Monday = _monday,
                    Tuesday = _tuesday,
                    Wednesday= _wednesday,
                    Thursday = _thursday,
                    Friday = _friday,
                    Saturday = _saturday,
                    Sunday = _sunday
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
            _deviceId = _baseStruct.DeviceId;
            _managerId = _baseStruct.ManagerId;
            _multiplier = _baseStruct.Multiplier;
            _planDate = _baseStruct.PlanDate;
            _routeMemberId = _baseStruct.RouteMemberId;
            _statusId = _baseStruct.StatusId;
            _supervisorId = _baseStruct.SupervisorId;
            _monday = _baseStruct.Monday;
            _tuesday = _baseStruct.Thursday;
            _wednesday = _baseStruct.Wednesday;
            _thursday = _baseStruct.Thursday;
            _friday = _baseStruct.Friday;
            _saturday = _baseStruct.Saturday;
            _sunday = _baseStruct.Sunday;
            IsChanged = false;
        }

        #endregion

        #region Дополнительные методы

        /// <summary>
        /// Добавить новую строку детализации
        /// </summary>
        /// <returns></returns>
        public DocumentDetailRoute NewRow()
        {
            DocumentDetailRoute row = new DocumentDetailRoute
            {
                Workarea = Workarea,
                Document = this,
                StateId = State.STATEACTIVE,
                Date = Date,
                Kind = Kind,
                OwnerId = Id
            };
            Details.Add(row);
            return row;
        }

        /// <summary>
        /// Новая строка аналитики
        /// </summary>
        /// <returns></returns>
        public DocumentAnalitic NewAnaliticRow()
        {
            DocumentAnalitic row = new DocumentAnalitic
            {
                Workarea = Workarea,
                Document = this.Document,
                StateId = State.STATEACTIVE,
                Kind = Kind,
                OwnerId = Id
            };
            Analitics.Add(row);
            return row;
        }

        /// <summary>
        /// Обновить данные из базы данных о аналитических данных строк документа
        /// </summary>
        public void RefreshDetailAnalitic()
        {
            foreach (DocumentDetailRoute row in Details)
            {
                row.RefreshAnalitics();
            }
        }

        #endregion

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            Document.StateId = StateId;
            Document.FlagsValue = FlagsValue;
            if (_multiplier == 0)
                _multiplier = 1;
            if (Workarea.IsWebApplication)
                Document.UserName = UserName;
            Document.Validate();
            Date = Document.Date;
            base.Validate();
            foreach (DocumentDetailRoute row in _details)
            {
                if (Workarea.IsWebApplication)
                    row.UserName = UserName;
                row.Validate();
            }
            if (_collDocumentContractor != null)
            {
                foreach (DocumentContractor row in _collDocumentContractor)
                {
                    row.OwnId = this.Id;
                    row.Validate();
                }
            }
            if (Document._signs != null)
            {
                foreach (DocumentSign s in Document._signs)
                {
                    s.Validate();
                }
            }

        }

        #region Подписи документа
        /// <summary>Коллекция подписей документа</summary>
        public List<DocumentSign> Signs()
        {
            if (Document._signs == null)
                Document.LoadSigns();
            return Document._signs;
        }
        #endregion
        /// <summary>
        /// Собственный обработчик обновления
        /// </summary>
        protected override void OnUpdated()
        {
            base.OnUpdated();

            foreach (DocumentDetailRoute row in _details)
            {
                // сохраняем данные о аналитике строки данных
                if (row.Analitics.Count > 0)
                {
                    row.SaveAnalitics();
                }
            }
            this.Document.InternalOnUpdated();
        }

        /// <summary>
        /// Собственный обработчик создания
        /// </summary>
        protected override void OnCreated()
        {
            base.OnCreated();

            foreach (DocumentDetailRoute row in _details)
            {
                // сохраняем данные о аналитике строки данных
                if (row.Analitics.Count > 0)
                {
                    row.SaveAnalitics();
                }
            }
            this.Document.InternalOnCreated();
        }

        #region Дополнительные корреспонденты документа
        internal List<DocumentContractor> _collDocumentContractor;
        /// <summary>Загрузить подписи документа их базы данных</summary>
        internal void LoadContractors()
        {
            _collDocumentContractor = new List<DocumentContractor>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = Workarea.Empty<DocumentContractor>().Entity.FindMethod(GlobalMethodAlias.LoadByOwnerId).FullName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                    cmd.Parameters.Add(prm);

                    prm = cmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int);
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
                                DocumentContractor item = new DocumentContractor
                                {
                                    Workarea = Workarea,
                                    Owner = Document
                                };
                                item.Load(reader);
                                _collDocumentContractor.Add(item);
                            }
                            reader.Close();
                        }
                        // TODO: Проверка ....
                        //object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        //if (retval == null)
                        //    throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        //if ((Int32)retval != 0)
                        //    throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

                    }
                    finally
                    {
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }

                }
            }
        }
        /// <summary>Коллекция дополнительных корреспондентов документа</summary>
        public List<DocumentContractor> Contractors()
        {
            if (_collDocumentContractor == null)
                LoadContractors();
            return _collDocumentContractor;
        }
        /// <summary>
        /// Обновить подписи
        /// </summary>
        public void RefreshContractors()
        {
            LoadContractors();
        }
        #endregion
        #region База данных
        
        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        /// <param name="versionControl">Использовать контроль версии объекта</param>
        protected override void Update(string procedureName, bool versionControl)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    SqlTransaction transaction = cnn.BeginTransaction("DocumentSaveTransaction");
                    #region Основной документ
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.Transaction = transaction;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        SetParametersToInsertUpdate(sqlCmd, false, versionControl);

                        if (sqlCmd.Connection.State != ConnectionState.Open)
                            sqlCmd.Connection.Open();
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {
                                if (Document == null)
                                    Document = new Document { Workarea = Workarea };
                                Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        Load(reader);
                                }
                                _details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailRoute docRow = new DocumentDetailRoute { Workarea = Workarea, Document = this };
                                            docRow.Load(reader);
                                            _details.Add(docRow);
                                        }
                                    }
                                }
                                _analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = Workarea, Document = Document };
                                            docRow.Load(reader);
                                            _analitics.Add(docRow);
                                        }
                                    }
                                }
                            }
                            // NOTE: Перед проверкой Return параметра - закрыть Reader!!!
                            reader.Close();
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                    #endregion
                    #region Дополнительные корреспонденты документа
                    if (_collDocumentContractor != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.ContractorsInsertUpdateAll";
                            SetParametersToContracts(sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                _collDocumentContractor.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentContractor docRow = new DocumentContractor { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        _collDocumentContractor.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    #region Подписи документа
                    if (Document._signs != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.SignatureInsertUpdateAll";
                            SetParametersToSigns(Document._signs, sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                Document._signs.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentSign docRow = new DocumentSign { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        Document._signs.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    transaction.Commit();
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="procedureName">Хранимая процедура</param>
        protected override void Create(string procedureName)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;
                try
                {
                    SqlTransaction transaction = cnn.BeginTransaction("DocumentSaveTransaction");
                    #region Основной документ
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.Transaction = transaction;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        SetParametersToInsertUpdate(sqlCmd, true, false);

                        if (sqlCmd.Connection.State != ConnectionState.Open)
                            sqlCmd.Connection.Open();
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {
                                if (Document == null)
                                    Document = new Document { Workarea = Workarea };
                                Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        Load(reader);
                                }
                                _details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailRoute docRow = new DocumentDetailRoute { Workarea = Workarea, Document = this };
                                            docRow.Load(reader);
                                            _details.Add(docRow);
                                        }
                                    }
                                }
                                _analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = Workarea, Document = Document };
                                            docRow.Load(reader);
                                            _analitics.Add(docRow);
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                    #endregion
                    #region Дополнительные корреспонденты документа
                    if (_collDocumentContractor != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.ContractorsInsertUpdateAll";
                            SetParametersToContracts(sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                _collDocumentContractor.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentContractor docRow = new DocumentContractor { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        _collDocumentContractor.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    #region Подписи документа
                    if (Document._signs != null)
                    {
                        using (SqlCommand sqlCmd = cnn.CreateCommand())
                        {
                            sqlCmd.Transaction = transaction;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "Document.SignatureInsertUpdateAll";
                            SetParametersToSigns(Document._signs, sqlCmd, false);

                            if (sqlCmd.Connection.State != ConnectionState.Open)
                                sqlCmd.Connection.Open();
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                Document._signs.Clear();
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentSign docRow = new DocumentSign { Workarea = Workarea, OwnId = Id };
                                        docRow.Load(reader);
                                        docRow.Owner = Document;
                                        Document._signs.Add(docRow);
                                    }
                                }
                                reader.Close();

                                object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                                if (retval == null)
                                {
                                    throw new SqlReturnException(Workarea.Cashe.ResourceString(
                                        "EX_MSG_DBUNCNOWNRESULTS", 1049));
                                }

                                if ((int)retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                                }
                            }
                        }
                    }

                    #endregion
                    transaction.Commit();
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }
        
        /// <summary>
        /// Создание копии
        /// </summary>
        /// <param name="value">Документ, копию которго необходимо сделать</param>
        /// <returns>Копия документа</returns>
        public static DocumentRoute CreateCopy(DocumentRoute value)
        {
            DocumentRoute routeDoc = new DocumentRoute { Workarea = value.Workarea };
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        sqlCmd.CommandText = value.FindProcedure(GlobalMethodAlias.Copy); //"Core.CustomViewListCopy";
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {
                                routeDoc.Document = new Document { Workarea = value.Workarea };
                                routeDoc.Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        routeDoc.Load(reader);
                                }
                                routeDoc._details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailRoute docRow = new DocumentDetailRoute { Workarea = value.Workarea, Document = routeDoc };
                                            docRow.Load(reader);
                                            routeDoc._details.Add(docRow);
                                        }
                                    }
                                }
                                routeDoc._analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = value.Workarea, Document = routeDoc.Document };
                                            docRow.Load(reader);
                                            routeDoc._analitics.Add(docRow);
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(value.Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(value.Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return routeDoc;
        }

        /// <summary>
        /// Загрузить данные из базы данных
        /// </summary>
        /// <param name="value">Идентификатор</param>
        /// <param name="procedureName">Хранимая процедура</param>
        protected override void Load(int value, string procedureName)
        {
            if (value == 0)
                return;
            OnBeginInit();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = procedureName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value;
                        SqlDataReader reader = sqlCmd.ExecuteReader();

                        if (reader.Read() && reader.HasRows)
                        {
                            if (Document == null)
                                Document = new Document { Workarea = Workarea };
                            Document.Load(reader);
                            if (reader.NextResult())
                            {
                                if (reader.Read() && reader.HasRows)
                                    Load(reader);
                            }
                            _details.Clear();
                            if (reader.NextResult())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentDetailRoute docRow = new DocumentDetailRoute { Workarea = Workarea, Document = this };
                                        docRow.Load(reader);
                                        _details.Add(docRow);
                                    }
                                }
                            }
                            _analitics.Clear();
                            if (reader.NextResult())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        DocumentAnalitic docRow = new DocumentAnalitic { Workarea = Workarea, Document = Document };
                                        docRow.Load(reader);
                                        _analitics.Add(docRow);
                                    }
                                }
                            }
                        }

                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                    OnEndInit();
                }
            }
        }

        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                Kind = reader.GetInt32(10);
                _statusId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                _deviceId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                _routeMemberId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _planDate = reader.IsDBNull(14) ? null : (DateTime?)reader.GetDateTime(14);
                _supervisorId = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                _managerId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                _multiplier = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);

                _monday = reader.IsDBNull(18) ? false : reader.GetBoolean(18);
                _tuesday = reader.IsDBNull(19) ? false : reader.GetBoolean(19);
                _wednesday = reader.IsDBNull(20) ? false : reader.GetBoolean(20);
                _thursday = reader.IsDBNull(21) ? false : reader.GetBoolean(21);
                _friday = reader.IsDBNull(22) ? false : reader.GetBoolean(22);
                _saturday = reader.IsDBNull(23) ? false : reader.GetBoolean(23);
                _sunday = reader.IsDBNull(24) ? false : reader.GetBoolean(24);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentRoute>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StatusId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DeviceId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.RouteMemberId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.PlanDate, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.SupervisorId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ManagerId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Multiplier, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Monday, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.Tuesday, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.Wednesday, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.Thursday, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.Friday, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.Saturday, SqlDbType.Bit),
                    new SqlMetaData(GlobalPropertyNames.Sunday, SqlDbType.Bit)
                );

                foreach (DocumentRoute doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }

        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentRoute doc)
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
            sdr.SetInt32(10, doc.Kind);

            if (doc.StatusId == 0)
                sdr.SetValue(11, DBNull.Value);
            else
                sdr.SetInt32(11, doc.StatusId);

            if (doc.DeviceId == 0)
                sdr.SetValue(12, DBNull.Value);
            else
                sdr.SetInt32(12, doc.DeviceId);

            if (doc.RouteMemberId == 0)
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetInt32(13, doc.RouteMemberId);

            if (!doc.PlanDate.HasValue)
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetDateTime(14, doc.PlanDate.Value);

            if (doc.SupervisorId == 0)
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetInt32(15, doc.SupervisorId);

            if (doc.ManagerId == 0)
                sdr.SetValue(16, DBNull.Value);
            else
                sdr.SetInt32(16, doc.ManagerId);

            if (doc.Multiplier == 0)
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetDecimal(17, doc.Multiplier);
            sdr.SetBoolean(18, doc.Monday);
            sdr.SetBoolean(19, doc.Tuesday);
            sdr.SetBoolean(20, doc.Wednesday);
            sdr.SetBoolean(21, doc.Thursday);
            sdr.SetBoolean(22, doc.Friday);
            sdr.SetBoolean(23, doc.Saturday);
            sdr.SetBoolean(24, doc.Sunday);
            return sdr;
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

            Document.TpvCollection coll = new Document.TpvCollection { Document };
            var headerParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Header, coll);
            headerParam.SqlDbType = SqlDbType.Structured;

            TpvCollection collTypes = new TpvCollection { this };
            var headerTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.HeaderType, collTypes);
            headerTypeParam.SqlDbType = SqlDbType.Structured;

            DocumentDetailRoute.TpvCollection collRows = new DocumentDetailRoute.TpvCollection();
            collRows.AddRange(_details);
            if (_details.Count == 0)
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Detail, null);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
            else
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Detail, collRows);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }

            DocumentAnalitic.TpvCollection collAnalitics = new DocumentAnalitic.TpvCollection();
            collAnalitics.AddRange(_analitics);
            if (_analitics.Count == 0)
            {
                var analiticTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.AnaliticDetail, null);
                analiticTypeParam.SqlDbType = SqlDbType.Structured;
            }
            else
            {
                var analiticTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.AnaliticDetail, collAnalitics);
                analiticTypeParam.SqlDbType = SqlDbType.Structured;
            }
        }
        private void SetParametersToContracts(SqlCommand sqlCmd, bool validateVersion)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = Id;

            DocumentContractor.TpvCollection collRows = new DocumentContractor.TpvCollection();
            foreach (DocumentContractor row in _collDocumentContractor)
            {
                row.OwnId = this.Id;
                row.Validate();
            }
            collRows.AddRange(_collDocumentContractor);
            if (_collDocumentContractor.Count == 0)
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Contractors, null);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
            else
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Contractors, collRows);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
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
            //_baseStruct = new BaseStructDocumentStore();
        }

        #endregion

        #region IChainsAdvancedList<Document,FileData> Members

        List<IChainAdvanced<Document, FileData>> IChainsAdvancedList<Document, FileData>.GetLinks()
        {
            return GetLinks(13);
        }

        List<ChainValueView> IChainsAdvancedList<Document, FileData>.GetChainView()
        {
            return ChainValueView.GetView<Document, FileData>(this.Document);
        }

        public List<IChainAdvanced<Document, FileData>> GetLinks(int? kind)
        {
            List<IChainAdvanced<Document, FileData>> collection = new List<IChainAdvanced<Document, FileData>>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = EntityDocument.FindMethod("LoadFiles").FullName;
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
                                ChainAdvanced<Document, FileData> item = new ChainAdvanced<Document, FileData> { Workarea = Workarea, Left = Document };
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
