using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Server;
using BusinessObjects.Security;

namespace BusinessObjects.Documents
{
    internal struct BaseStructDocumentPlan
    {
        /// <summary>
        /// Идентификатор периода планирования
        /// </summary>
        public int DateregionId;
        /// <summary>Иденгтификатор регистратора документа</summary>
        public int RegistratorId;
        /// <summary>Идентификатор типа договора</summary>
        public int PlanKindId;
        /// <summary>Идентификатор текущего состояния договора</summary>
        public int StateCurrentId;
        /// <summary>Идентификатор заключения</summary>
        public int StateResultId;
        /// <summary>Идентификатор важности договора</summary>
        public int ImportanceId;
        /// <summary>Дата начала договора</summary>
        public DateTime? DateStart;
        /// <summary>Дата окончания договора</summary>
        public DateTime? DateEnd;
        /// <summary>Идентификатор отдела</summary>
        public int DepatmentFromId;
        /// <summary>Идентификатор отдела</summary>
        public int DepatmentToId;
        /// <summary>Идентификатор сотрудника</summary>
        public int WorkerFromId;
        /// <summary>Идентификатор сотрудника</summary>
        public int WorkerToId;
    }

    /// <summary>
    /// Документ учета документов
    /// </summary>
    public class DocumentPlan : DocumentBase, IEditableObject
    {/// <summary>Конструктор</summary>
        public DocumentPlan()
            : base()
        {
            EntityId = 12;
            _details = new List<DocumentDetailPlan>();
            _analitics = new List<DocumentAnalitic>();
        }
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Договор, соответствует значению 1</summary>
        public const int KINDVALUE_ORDER = 1;
        /// <summary>Акт ревизии, соответствует значению 2</summary>
        public const int KINDVALUE_ORDERTOTAL = 2;
        /// <summary>Акт сверки клиента, соответствует значению 3</summary>
        public const int KINDVALUE_ORDERFINAL = 3;
        /// <summary>Настройки раздела, соответствует значению 100</summary>
        public const int KINDVALUE_CONFIG = 100;

        /// <summary>Договор, соответствует значению 786433</summary>
        public const int KINDID_ORDER = 786433;
        /// <summary>Акт ревизии, соответствует значению 786434</summary>
        public const int KINDID_ORDERTOTAL = 786434;
        /// <summary>Акт сверки клиента, соответствует значению 786435</summary>
        public const int KINDID_ORDERFINAL = 786435;
        /// <summary>Настройки раздела, соответствует значению 786532</summary>
        public const int KINDID_CONFIG = 786532;
        // ReSharper restore InconsistentNaming
        #endregion
        #region Свойства
        private List<DocumentDetailPlan> _details;
        /// <summary>
        /// Детализация документа учета документов
        /// </summary>
        public List<DocumentDetailPlan> Details
        {
            get { return _details; }
            set { _details = value; }
        }

        private List<DocumentAnalitic> _analitics;
        /// <summary>
        /// Детализация документа на уровне аналитики
        /// </summary>
        public List<DocumentAnalitic> Analitics
        {
            get { return _analitics; }
            set { _analitics = value; }
        }


        private int _dateregionId;
        /// <summary>
        /// Идентификатор периода планирования
        /// </summary>
        public int DateregionId
        {
            get { return _dateregionId; }
            set
            {
                if (value == _dateregionId) return;
                OnPropertyChanging(GlobalPropertyNames.DateregionId);
                _dateregionId = value;
                OnPropertyChanged(GlobalPropertyNames.DateregionId);
            }
        }

        private DateRegion _dateregion;
        /// <summary>
        /// Период планирования
        /// </summary>
        public DateRegion Dateregion
        {
            get
            {
                if (_dateregionId == 0)
                    return null;
                if (_dateregion == null)
                    _dateregion = Workarea.Cashe.GetCasheData<DateRegion>().Item(_dateregionId);
                else if (_dateregion.Id != _dateregionId)
                    _dateregion = Workarea.Cashe.GetCasheData<DateRegion>().Item(_dateregionId);
                return _dateregion;
            }
            set
            {
                if (_dateregion == value) return;
                OnPropertyChanging(GlobalPropertyNames.Dateregion);
                _dateregion = value;
                _dateregionId = _dateregion == null ? 0 : _dateregion.Id;
                OnPropertyChanged(GlobalPropertyNames.Dateregion);
            }
        }
        
        private int _registratorId;
        /// <summary>
        /// Идентификатор корреспондента-регистратора документа
        /// </summary>
        public int RegistratorId
        {
            get { return _registratorId; }
            set
            {
                if (value == _registratorId) return;
                OnPropertyChanging(GlobalPropertyNames.RegistratorId);
                _registratorId = value;
                OnPropertyChanged(GlobalPropertyNames.RegistratorId);
            }
        }
        private Agent _registrator;
        /// <summary>
        /// Регистратор
        /// </summary>
        public Agent Registrator
        {
            get
            {
                if (_registratorId == 0)
                    return null;
                if (_registrator == null)
                    _registrator = Workarea.Cashe.GetCasheData<Agent>().Item(_registratorId);
                else if (_registrator.Id != _registratorId)
                    _registrator = Workarea.Cashe.GetCasheData<Agent>().Item(_registratorId);
                return _registrator;
            }
            set
            {
                if (_registrator == value) return;
                OnPropertyChanging(GlobalPropertyNames.Registrator);
                _registrator = value;
                _registratorId = _registrator == null ? 0 : _registrator.Id;
                OnPropertyChanged(GlobalPropertyNames.Registrator);
            }
        }

        private int _planKindId;
        /// <summary>
        /// Идентификатор типа планирования
        /// </summary>
        public int PlanKindId
        {
            get { return _planKindId; }
            set
            {
                if (value == _planKindId) return;
                OnPropertyChanging(GlobalPropertyNames.PlanKindId);
                _planKindId = value;
                OnPropertyChanged(GlobalPropertyNames.PlanKindId);
            }
        }
        private int _stateCurrentId;
        /// <summary>
        /// Идентификатор текущего состояния договора
        /// </summary>
        public int StateCurrentId
        {
            get { return _stateCurrentId; }
            set
            {
                if (value == _stateCurrentId) return;
                OnPropertyChanging(GlobalPropertyNames.StateCurrentId);
                _stateCurrentId = value;
                OnPropertyChanged(GlobalPropertyNames.StateCurrentId);
            }
        }

        private int _stateResultId;
        /// <summary>
        /// Идентификатор заключения
        /// </summary>
        public int StateResultId
        {
            get { return _stateResultId; }
            set
            {
                if (value == _stateResultId) return;
                OnPropertyChanging(GlobalPropertyNames.StateResultId);
                _stateResultId = value;
                OnPropertyChanged(GlobalPropertyNames.StateResultId);
            }
        }
        
        private int _importanceId;
        /// <summary>
        /// Идентификатор важности договора
        /// </summary>
        public int ImportanceId
        {
            get { return _importanceId; }
            set
            {
                if (value == _importanceId) return;
                OnPropertyChanging(GlobalPropertyNames.ImportanceId);
                _importanceId = value;
                OnPropertyChanged(GlobalPropertyNames.ImportanceId);
            }
        }
        
        private DateTime? _dateStart;
        /// <summary>
        /// Дата начала 
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
        private DateTime? _dateEnd;
        /// <summary>
        /// Дата окончания 
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


        private int _depatmentFromId;
        /// <summary>
        /// Идентификатор отдела
        /// </summary>
        public int DepatmentFromId
        {
            get { return _depatmentFromId; }
            set
            {
                if (value == _depatmentFromId) return;
                OnPropertyChanging(GlobalPropertyNames.DepatmentFromId);
                _depatmentFromId = value;
                OnPropertyChanged(GlobalPropertyNames.DepatmentFromId);
            }
        }


        private int _depatmentToId;
        /// <summary>
        /// Идентификатор отдела
        /// </summary>
        public int DepatmentToId
        {
            get { return _depatmentToId; }
            set
            {
                if (value == _depatmentToId) return;
                OnPropertyChanging(GlobalPropertyNames.DepatmentToId);
                _depatmentToId = value;
                OnPropertyChanged(GlobalPropertyNames.DepatmentToId);
            }
        }


        private int _workerFromId;
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int WorkerFromId
        {
            get { return _workerFromId; }
            set
            {
                if (value == _workerFromId) return;
                OnPropertyChanging(GlobalPropertyNames.WorkerFromId);
                _workerFromId = value;
                OnPropertyChanged(GlobalPropertyNames.WorkerFromId);
            }
        }


        private Agent _workerFrom;
        /// <summary>
        /// Сотрудник "Кто"
        /// </summary>
        public Agent WorkerFrom
        {
            get
            {
                if (_workerFromId == 0)
                    return null;
                if (_workerFrom == null)
                    _workerFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_workerFromId);
                else if (_workerFrom.Id != _workerFromId)
                    _workerFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_workerFromId);
                return _workerFrom;
            }
            set
            {
                if (_workerFrom == value) return;
                OnPropertyChanging(GlobalPropertyNames.WorkerFrom);
                _workerFrom = value;
                _workerFromId = _workerFrom == null ? 0 : _workerFrom.Id;
                OnPropertyChanged(GlobalPropertyNames.WorkerFrom);
            }
        }
        

        private int _workerToId;
        /// <summary>
        /// Идентификатор сотрудника "Кому"
        /// </summary>
        public int WorkerToId
        {
            get { return _workerToId; }
            set
            {
                if (value == _workerToId) return;
                OnPropertyChanging(GlobalPropertyNames.WorkerToId);
                _workerToId = value;
                OnPropertyChanged(GlobalPropertyNames.WorkerToId);
            }
        }


        private Agent _workerTo;
        /// <summary>
        /// Сотрудник "Кому"
        /// </summary>
        public Agent WorkerTo
        {
            get
            {
                if (_workerToId == 0)
                    return null;
                if (_workerTo == null)
                    _workerTo = Workarea.Cashe.GetCasheData<Agent>().Item(_workerToId);
                else if (_workerTo.Id != _workerToId)
                    _workerTo = Workarea.Cashe.GetCasheData<Agent>().Item(_workerToId);
                return _workerTo;
            }
            set
            {
                if (_workerTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.WorkerTo);
                _workerTo = value;
                _workerToId = _workerTo == null ? 0 : _workerTo.Id;
                OnPropertyChanged(GlobalPropertyNames.WorkerTo);
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

            if (_registratorId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.RegistratorId, XmlConvert.ToString(_registratorId));
            if (_planKindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ContractKindId, XmlConvert.ToString(_planKindId));
            if (_stateCurrentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.StateCurrentId, XmlConvert.ToString(_stateCurrentId));
            if (_importanceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ImportanceId, XmlConvert.ToString(_importanceId));
            if (_dateStart.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(_dateStart.Value));
            if (_dateEnd.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(_dateEnd.Value));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.RegistratorId) != null)
                _registratorId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RegistratorId));
            if (reader.GetAttribute(GlobalPropertyNames.ContractKindId) != null)
                _planKindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ContractKindId));
            if (reader.GetAttribute(GlobalPropertyNames.StateCurrentId) != null)
                _stateCurrentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.StateCurrentId));
            if (reader.GetAttribute(GlobalPropertyNames.ImportanceId) != null)
                _importanceId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ImportanceId));
            if (reader.GetAttribute(GlobalPropertyNames.DateStart) != null)
                _dateStart = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart));
            if (reader.GetAttribute(GlobalPropertyNames.DateEnd) != null)
                _dateEnd = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateEnd));
        }
        #endregion

        #region Дополнительные методы
        /// <summary>
        /// Добавить новую строку дополнительных корреспондентов
        /// </summary>
        /// <returns></returns>
        public DocumentContractor NewContractorRow()
        {
            DocumentContractor row = new DocumentContractor
            {
                Workarea = Workarea,
                Owner = this.Document,
                StateId = State.STATEACTIVE,
                Kind = 0,
                OwnId = Id
            };
            Contractors().Add(row);
            return row;
        }
        /// <summary>
        /// Добавить новую строку детализации
        /// </summary>
        /// <returns></returns>
        public DocumentDetailPlan NewRow()
        {
            DocumentDetailPlan row = new DocumentDetailPlan
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
        #endregion
        #region Состояния
        BaseStructDocumentPlan _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentPlan
                  {
                      DepatmentFromId = _depatmentFromId,
                      DepatmentToId = _depatmentToId,
                      DateregionId = _dateregionId,
                      StateResultId = _stateResultId,
                      PlanKindId = _planKindId,
                      DateEnd = _dateEnd,
                      DateStart= _dateStart,
                      ImportanceId = _importanceId,
                      RegistratorId = _registratorId,
                      StateCurrentId = _stateCurrentId,
                      WorkerFromId = _workerFromId,
                      WorkerToId = _workerToId
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
            _planKindId = _baseStruct.PlanKindId;
            _dateEnd = _baseStruct.DateEnd;
            _dateStart = _baseStruct.DateStart;
            _importanceId = _baseStruct.ImportanceId;
            _registratorId = _baseStruct.RegistratorId;
            _stateCurrentId = _baseStruct.StateCurrentId;
            _stateResultId = _baseStruct.StateResultId;
            _depatmentFromId = _baseStruct.DepatmentFromId;
            _depatmentToId = _baseStruct.DepatmentToId;
            _dateregionId = _baseStruct.DateregionId;
            _workerFromId = _baseStruct.WorkerFromId;
            _workerToId = _baseStruct.WorkerToId;
            IsChanged = false;
        }
        #endregion
        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            Document.StateId = StateId;
            if (Workarea.IsWebApplication)
                Document.UserName = UserName;
            Document.Validate();
            base.Validate();
            foreach (DocumentDetailPlan row in _details)
            {
                if (Workarea.IsWebApplication)
                    row.UserName = UserName;
                row.Validate();
            }
            foreach (DocumentAnalitic row in _analitics)
            {
                if (Workarea.IsWebApplication)
                    row.UserName = UserName;
                row.Validate();
            }
            if (_registratorId == 0)
                throw new ValidateException("Не указан регистратор документа");
            if (_stateCurrentId == 0)
                throw new ValidateException("Не указано текщее состояние документа");
            if (_planKindId == 0)
                throw new ValidateException("Не указан период планирования документа");

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
                                    reader.Read();
                                    Load(reader);
                                }
                                _details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailPlan docRow = new DocumentDetailPlan { Workarea = Workarea,Document = this };
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
                                if (Document==null)
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
                                            DocumentDetailPlan docRow = new DocumentDetailPlan { Workarea = Workarea, Document = this };
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
                            {
                                throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                            }

                            if ((int)retval != 0)
                            {
                                throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                            }
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

                                if ((int) retval != 0)
                                {
                                    throw new DatabaseException(
                                        Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int) retval);
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

        public static DocumentPlan CreateCopy(DocumentPlan value)
        {

            DocumentPlan doc = new DocumentPlan { Workarea = value.Workarea };
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


                                doc.Document = new Document { Workarea = value.Workarea };
                                doc.Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        doc.Load(reader);
                                }
                                doc._details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailPlan docRow = new DocumentDetailPlan { Workarea = value.Workarea, Document = doc };
                                            docRow.Load(reader);
                                            doc._details.Add(docRow);
                                        }
                                    }
                                }
                                doc._analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = value.Workarea, Document = doc.Document };
                                            docRow.Load(reader);
                                            doc._analitics.Add(docRow);
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
            return doc;
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
                                        DocumentDetailPlan docRow = new DocumentDetailPlan {Workarea = Workarea,Document = this};
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
                _dateregionId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                _planKindId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                _registratorId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _stateCurrentId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                _stateResultId = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                _dateStart = reader.IsDBNull(16) ? (DateTime?) null : reader.GetDateTime(16);
                _dateEnd = reader.IsDBNull(17) ? (DateTime?) null : reader.GetDateTime(17);
                _importanceId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _depatmentFromId = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
                _depatmentToId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _workerFromId = reader.IsDBNull(20) ? 0 : reader.GetInt32(21);
                _workerToId = reader.IsDBNull(20) ? 0 : reader.GetInt32(22);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }
        internal class TpvCollection : List<DocumentPlan>, IEnumerable<SqlDataRecord>
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
                new SqlMetaData(GlobalPropertyNames.DateregionId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.PlanKindId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.Registrator, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.StateCurrent, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.StateResultId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.DateStart, SqlDbType.Date),
                new SqlMetaData(GlobalPropertyNames.DateEnd, SqlDbType.Date),
                new SqlMetaData(GlobalPropertyNames.ImportanceId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.DepatmentFromId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.DepatmentToId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.WorkerFromId, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.WorkerToId, SqlDbType.Int)
                
                );

                foreach (DocumentPlan doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentPlan doc)
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

            sdr.SetInt32(11, doc.DateregionId);
            sdr.SetInt32(12, doc.PlanKindId);
            sdr.SetInt32(13, doc.RegistratorId);
            sdr.SetInt32(14, doc.StateCurrentId);

            if (doc.StateResultId==0)
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetInt32(15, doc.StateResultId);

            if (!doc.DateStart.HasValue)
                sdr.SetValue(16, DBNull.Value);
            else
                sdr.SetDateTime(16, doc.DateStart.Value);
            if (!doc.DateEnd.HasValue)
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetDateTime(17, doc.DateEnd.Value);

            if (doc.ImportanceId == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.ImportanceId);

            if (doc.DepatmentFromId == 0)
                sdr.SetValue(19, DBNull.Value);
            else
                sdr.SetInt32(19, doc.DepatmentFromId);

            if (doc.DepatmentToId == 0)
                sdr.SetValue(20, DBNull.Value);
            else
                sdr.SetInt32(20, doc.DepatmentToId);

            if (doc.WorkerFromId == 0)
                sdr.SetValue(21, DBNull.Value);
            else
                sdr.SetInt32(21, doc.DepatmentToId);

            if (doc.WorkerToId == 0)
                sdr.SetValue(22, DBNull.Value);
            else
                sdr.SetInt32(22, doc.DepatmentToId);
            
            return sdr;            
        }
        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion)
        {
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

            Document.TpvCollection coll = new Document.TpvCollection {Document};
            var headerParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Header, coll);
            headerParam.SqlDbType = SqlDbType.Structured;

            TpvCollection collTypes = new TpvCollection {this};
            var headerTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.HeaderType, collTypes);
            headerTypeParam.SqlDbType = SqlDbType.Structured;

            DocumentDetailPlan.TpvCollection collRows = new DocumentDetailPlan.TpvCollection();
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

        #region Связи
        //public List<IChain<DocumentPlan>> GetLinks()
        //{
        //    List<IChain<DocumentPlan>> collection = new List<IChain<DocumentPlan>>();
        //    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandText = EntityDocument.FindMethod("DocumentChainLoadSource").FullName;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
        //            prm.Direction = ParameterDirection.ReturnValue;

        //            prm = cmd.Parameters.Add(GlobalSqlParamNames.DbSourceId, SqlDbType.Int);
        //            prm.Direction = ParameterDirection.Input;
        //            prm.Value = Id;

        //            try
        //            {
        //                if (cmd.Connection.State == ConnectionState.Closed)
        //                    cmd.Connection.Open();
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                if (reader != null)
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            DocumentChain<DocumentPlan> item = new DocumentChain<DocumentPlan> { Workarea = Workarea, Left = this };
        //                            item.Load(reader);
        //                            collection.Add(item);
        //                        }
        //                    }
        //                    reader.Close();
        //                }
        //                object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
        //                if (retval == null)
        //                    throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

        //                if ((Int32)retval != 0)
        //                    throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

        //            }
        //            finally
        //            {
        //                if (cmd.Connection.State == ConnectionState.Open)
        //                    cmd.Connection.Close();
        //            }

        //        }
        //    }
        //    return collection;
        //}
        #endregion


        #region IChainsAdvancedList<DocumentPlan,FileData> Members

        //List<IChainAdvanced<Document, FileData>> IChainsAdvancedList<Document, FileData>.GetLinks()
        //{
        //    return GetLinks(13);
        //}

        //public List<IChainAdvanced<Document, FileData>> GetLinks(int kind)
        //{
        //    return GetLinkedFiles();
        //}

        //private List<IChainAdvanced<Document, FileData>> GetLinkedFiles()
        //{
        //    List<IChainAdvanced<Document, FileData>> collection = new List<IChainAdvanced<Document, FileData>>();
        //    using (SqlConnection cnn = Workarea.GetDatabaseConnection())
        //    {
        //        using (SqlCommand cmd = cnn.CreateCommand())
        //        {
        //            cmd.CommandText = EntityDocument.FindMethod("LoadFiles").FullName;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
        //            prm.Direction = ParameterDirection.ReturnValue;

        //            prm = cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
        //            prm.Direction = ParameterDirection.Input;
        //            prm.Value = Id;

        //            try
        //            {
        //                if (cmd.Connection.State == ConnectionState.Closed)
        //                    cmd.Connection.Open();
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                if (reader != null)
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            ChainAdvanced<Document, FileData> item = new ChainAdvanced<Document, FileData> { Workarea = Workarea, Left = this.Document };
        //                            item.Load(reader);
        //                            collection.Add(item);
        //                        }
        //                    }
        //                    reader.Close();
        //                }
        //                object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
        //                if (retval == null)
        //                    throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

        //                if ((Int32)retval != 0)
        //                    throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);

        //            }
        //            finally
        //            {
        //                if (cmd.Connection.State == ConnectionState.Open)
        //                    cmd.Connection.Close();
        //            }

        //        }
        //    }
        //    return collection;
        //}

        #endregion

        #region Дополнительные сообщения
                /// <summary>
        /// Формирование сообщения о поставленной задаче 
        /// </summary>
        public void CreateMessageInfo()
        {
            if (this.RegistratorId == this.WorkerToId)
                return;
            // шаблон сообщения
            Message tmlMessage = Workarea.GetTemplates<Message>().FirstOrDefault(s => s.Code == Message.MESSAGE_TEMPLATE_DOCUMENTPLANREATE);
            Uid userFrom = Workarea.Access.GetAllUsers().FirstOrDefault(f => f.AgentId == RegistratorId);
            Uid userTo = Workarea.Access.GetAllUsers().FirstOrDefault(f => f.AgentId == WorkerToId);
            if (userFrom != null && userTo != null && tmlMessage!=null)
            {
                Message newMessage = Workarea.CreateNewObject<Message>(tmlMessage);
                newMessage.SendDate = DateTime.Now;
                newMessage.SendTime = DateTime.Now.TimeOfDay;
                newMessage.IsSend = true;
                newMessage.Code = string.Empty;
                if (string.IsNullOrEmpty(newMessage.NameFull))
                {
                    newMessage.NameFull = "Вам необходимо ознакомится с документом!";
                }
                
                string webControllerName = string.Empty;
                if (this.Kind == DocumentPlan.KINDID_ORDER)
                    webControllerName = "PlanOrder";
                if (this.Kind == DocumentPlan.KINDID_ORDERTOTAL)
                    webControllerName = "PlanTotalOrder";
                if (this.Kind == DocumentPlan.KINDID_ORDERFINAL)
                    webControllerName = "PlanFinalOrder";
                SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>(SystemParameter.WEBROOTSERVER);
                string webLink = string.Format("{0}/Planing/{1}/Edit/{2}", prm.ValueString, webControllerName, Id);

                newMessage.NameFull = newMessage.NameFull.Replace("{AGENTOWNERNAME}", userFrom.Agent.Name).Replace("{AGENTTONAME}", userTo.Agent.Name).Replace("{WEBLINK}", webLink).Replace("{DOCNAMEFULL}", this.Document.NameFull);
                newMessage.UserId = userTo.Id;
                newMessage.UserOwnerId = userFrom.Id;
                newMessage.HasRead = true;
                newMessage.Save();
            }
                    /*
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
                    newMessage.NameFull = "Вам подготовлен документ для изнакомления!";
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
                }
            }
            return true;
             **/
        }
        #endregion
    }
}
