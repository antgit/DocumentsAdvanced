using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{
    internal struct BaseStructDocumentService
    {
        /// <summary>Идентификатор старшего менеджера</summary>
        public int SupervisorId;
        /// <summary>Идентификатор менеджера</summary>
        public int ManagerId;
        /// <summary>Идентификатор ценовой политики</summary>
        public int PriceId;
        /// <summary>Идентификатор документа "Налоговая накладная"</summary>
        public int TaxDocId;
        /// <summary>Идентификатор расчетного счета получателя</summary>
        public int BankAccToId;
        /// <summary>Идентификатор расчетного счета отправителя</summary>
        public int BankAccFromId;
    }
    /// <summary>
    /// Документ раздела "Услуги" 
    /// </summary>
    public class DocumentService : DocumentBase, IEditableObject, IChainsAdvancedList<Document, FileData>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Акт выполненых работ исходящий, соответствует значению 1</summary>
        public const int KINDVALUE_OUT = 1;
        /// <summary>Акт выполненых работ входящий, соответствует значению 2</summary>
        public const int KINDVALUE_IN = 2;
        /// <summary>Заказ поставщику, соответствует значению 3</summary>
        public const int KINDVALUE_ORDEROUT = 3;
        /// <summary>Заказ покупателя, соответствует значению 4</summary>
        public const int KINDVALUE_ORDERIN = 4;
        /// <summary>Счет исходящий, соответствует значению 7</summary>
        public const int KINDVALUE_ACCOUNTOUT = 7;
        /// <summary>Счет входящий, соответствует значению 8</summary>
        public const int KINDVALUE_ACCOUNTIN = 8;
        /// <summary>Настройки раздела, соответствует значению 100</summary>
        public const int KINDVALUE_CONFIG = 100;


        /// <summary>Акт выполненых работ исходящий, соответствует значению 589825</summary>
        public const int KINDID_OUT = 589825;
        /// <summary>Акт выполненых работ входящий, соответствует значению 589826</summary>
        public const int KINDID_IN = 589826;
        /// <summary>Заказ поставщику, соответствует значению 589827</summary>
        public const int KINDID_ORDEROUT = 589827;
        /// <summary>Заказ покупателя, соответствует значению 589828</summary>
        public const int KINDID_ORDERIN = 589828;
        /// <summary>Счет исходящий, соответствует значению 589831</summary>
        public const int KINDID_ACCOUNTOUT = 589831;
        /// <summary>Счет входящий, соответствует значению 589832</summary>
        public const int KINDID_ACCOUNTIN = 589832;
        /// <summary>Настройки раздела, соответствует значению 589924</summary>
        public const int KINDID_CONFIG = 589924;
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>Конструктор</summary>
        public DocumentService()
            : base()
        {
            EntityId = 9;
            _details = new List<DocumentDetailService>();
            _analitics = new List<DocumentAnalitic>();
        }
        #region Свойства

        internal List<DocumentDetailService> _details;
        /// <summary>
        /// Детализация документа на уровне строк
        /// </summary>
        public List<DocumentDetailService> Details
        {
            get { return _details; }
            set { _details = value; }
        }

        internal List<DocumentAnalitic> _analitics;
        /// <summary>
        /// Детализация документа на уровне аналитики
        /// </summary>
        public List<DocumentAnalitic> Analitics
        {
            get { return _analitics; }
            set { _analitics = value; }
        }

        private int _supervisorId;
        /// <summary>
        /// Идентификатор старшего менеджера
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

        private int _priceId;
        /// <summary>
        /// Идентификатор ценовой политики
        /// </summary>
        public int PriceId
        {
            get { return _priceId; }
            set
            {
                if (value == _priceId) return;
                OnPropertyChanging(GlobalPropertyNames.PriceId);
                _priceId = value;
                OnPropertyChanged(GlobalPropertyNames.PriceId);
            }
        }

        private PriceName _priceName;
        /// <summary>
        /// Ценовая политика 
        /// </summary>
        public PriceName PriceName
        {
            get
            {
                if (_priceId == 0)
                    return null;
                if (_priceName == null)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_priceId);
                else if (_priceName.Id != _priceId)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_priceId);
                return _priceName;
            }
            set
            {
                if (_priceName == value) return;
                OnPropertyChanging(GlobalPropertyNames.PriceName);
                _priceName = value;
                _priceId = _priceName == null ? 0 : _priceName.Id;
                OnPropertyChanged(GlobalPropertyNames.PriceName);
            }
        }
        private int _taxDocId;
        /// <summary>
        /// Идентификатор документа "Налоговая накладная"
        /// </summary>
        public int TaxDocId
        {
            get { return _taxDocId; }
            set
            {
                if (value == _taxDocId) return;
                OnPropertyChanging(GlobalPropertyNames.TaxDocId);
                _taxDocId = value;
                OnPropertyChanged(GlobalPropertyNames.TaxDocId);
            }
        }

        private DateTime? _dateShip;
        /// <summary>
        /// Дата "Фактическая дата отгрузки"
        /// </summary>
        public DateTime? DateShip
        {
            get { return _dateShip; }
            set
            {
                if (value == _dateShip) return;
                OnPropertyChanging(GlobalPropertyNames.DateShip);
                _dateShip = value;
                OnPropertyChanged(GlobalPropertyNames.DateShip);
            }
        }

        private DateTime? _datePay;
        /// <summary>
        /// Дата "Оплатить до"
        /// </summary>
        public DateTime? DatePay
        {
            get { return _datePay; }
            set
            {
                if (value == _datePay) return;
                OnPropertyChanging(GlobalPropertyNames.DatePay);
                _datePay = value;
                OnPropertyChanged(GlobalPropertyNames.DatePay);
            }
        }

        private int _bankAccFromId;
        /// <summary>
        /// Идентификатор расчетного счета отправителя
        /// </summary>
        public int BankAccFromId
        {
            get { return _bankAccFromId; }
            set
            {
                if (value == _bankAccFromId) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccFromId);
                _bankAccFromId = value;
                OnPropertyChanged(GlobalPropertyNames.BankAccFromId);
            }
        }


        private AgentBankAccount _bankAccFrom;
        /// <summary>
        /// Расчетный счет отправителя
        /// </summary>
        public AgentBankAccount BankAccFrom
        {
            get
            {
                if (_bankAccFromId == 0)
                    return null;
                if (_bankAccFrom == null)
                    _bankAccFrom = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccFromId);
                else if (_bankAccFrom.Id != _bankAccFromId)
                    _bankAccFrom = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccFromId);
                return _bankAccFrom;
            }
            set
            {
                if (_bankAccFrom == value) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccFrom);
                _bankAccFrom = value;
                _bankAccFromId = _bankAccFrom == null ? 0 : _bankAccFrom.Id;
                OnPropertyChanged(GlobalPropertyNames.BankAccFrom);
            }
        }


        private int _bankAccToId;
        /// <summary>
        /// Идентификатор расчетного счета получателя
        /// </summary>
        public int BankAccToId
        {
            get { return _bankAccToId; }
            set
            {
                if (value == _bankAccToId) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccToId);
                _bankAccToId = value;
                OnPropertyChanged(GlobalPropertyNames.BankAccToId);
            }
        }


        private AgentBankAccount _bankAccTo;
        /// <summary>
        /// Расчетный счет получателя
        /// </summary>
        public AgentBankAccount BankAccTo
        {
            get
            {
                if (_bankAccToId == 0)
                    return null;
                if (_bankAccTo == null)
                    _bankAccTo = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccToId);
                else if (_bankAccTo.Id != _bankAccToId)
                    _bankAccTo = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(_bankAccToId);
                return _bankAccTo;
            }
            set
            {
                if (_bankAccTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.BankAccTo);
                _bankAccTo = value;
                _bankAccToId = _bankAccTo == null ? 0 : _bankAccTo.Id;
                OnPropertyChanged(GlobalPropertyNames.BankAccTo);
            }
        }

        #endregion
        /// <summary>
        /// Собственный обработчик обновления
        /// </summary>
        protected override void OnUpdated()
        {
            base.OnUpdated();

            foreach (DocumentDetailService row in _details)
            {
                // сохраняем данные о аналитике строки данных
                if (row.Analitics.Count > 0)
                {
                    row.SaveAnalitics();
                }
            }
            this.Document.InternalOnUpdated();
        }

        protected override void OnCreated()
        {
            base.OnCreated();

            foreach (DocumentDetailService row in _details)
            {
                // сохраняем данные о аналитике строки данных
                if (row.Analitics.Count > 0)
                {
                    row.SaveAnalitics();
                }
            }
            if(this.Document._signs!=null)
            {
                foreach (DocumentSign s in Document._signs)
                {
                    if (s.OwnId == 0)
                        s.OwnId = Document.Id;
                }
            }
            this.Document.InternalOnCreated();
        }
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
            if (_priceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PriceId, XmlConvert.ToString(_priceId));
            if (_taxDocId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.TaxDocId, XmlConvert.ToString(_taxDocId));
            if (_bankAccFromId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BankAccFromId, XmlConvert.ToString(_bankAccFromId));
            if (_bankAccToId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.BankAccToId, XmlConvert.ToString(_bankAccToId));
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
            if (reader.GetAttribute(GlobalPropertyNames.PriceId) != null)
                _priceId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PriceId));
            if (reader.GetAttribute(GlobalPropertyNames.TaxDocId) != null)
                _taxDocId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.TaxDocId));
            if (reader.GetAttribute(GlobalPropertyNames.BankAccFromId) != null)
                _bankAccFromId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BankAccFromId));
            if (reader.GetAttribute(GlobalPropertyNames.BankAccToId) != null)
                _bankAccToId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.BankAccToId));
        }
        #endregion

        #region Состояния
        BaseStructDocumentService _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentService
                {
                    ManagerId = _managerId,
                    PriceId = _priceId,
                    SupervisorId = _supervisorId,
                    TaxDocId = _taxDocId,
                    BankAccFromId = _bankAccFromId,
                    BankAccToId = _bankAccToId
                };
                return true;
            }
            return false;
        }
        /// <summary>
        /// Востановить состояние
        /// </summary>
        public override void RestoreState()
        {
            base.RestoreState();
            _managerId = _baseStruct.ManagerId;
            _priceId = _baseStruct.PriceId;
            _supervisorId = _baseStruct.SupervisorId;
            _taxDocId = _baseStruct.TaxDocId;
            _bankAccToId = _baseStruct.BankAccToId;
            _bankAccFromId = _baseStruct.BankAccFromId;
            IsChanged = false;
        }
        #endregion

        #region Дополнительные методы
        /// <summary>
        /// Добавить новую строку детализации
        /// </summary>
        /// <returns></returns>
        public DocumentDetailService NewRow()
        {
            DocumentDetailService row = new DocumentDetailService
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

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            Document.StateId = StateId;
            Document.FlagsValue = FlagsValue;
            if (Workarea.IsWebApplication)
                Document.UserName = UserName;
            Document.Validate();
            Date = Document.Date;
            base.Validate();
            foreach (DocumentDetailService row in _details)
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
            if (_collDocumentContractor != null)
            {
                foreach (DocumentContractor row in _collDocumentContractor)
                {
                    row.OwnId = this.Id;
                    row.Validate();
                }
            }
            if(Document._signs!=null)
            {
                foreach(DocumentSign s in Document._signs)
                {
                    s.Validate();
                }
            }
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
        #region Подписи документа
        /// <summary>Коллекция подписей документа</summary>
        public List<DocumentSign> Signs()
        {
            if (Document._signs == null)
                Document.LoadSigns();
            return Document._signs;
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
                                            DocumentDetailService docRow = new DocumentDetailService { Workarea = Workarea, Document = this };
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
                                            DocumentDetailService docRow = new DocumentDetailService { Workarea = Workarea, Document = this };
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

        public static DocumentService CreateCopy(DocumentService value)
        {

            DocumentService doc = new DocumentService { Workarea = value.Workarea };
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
                                            DocumentDetailService docRow = new DocumentDetailService { Workarea = value.Workarea, Document = doc };
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
                            if(Document==null)
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
                                        DocumentDetailService docRow = new DocumentDetailService { Workarea = Workarea, Document = this };
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
                _supervisorId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                _managerId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                _priceId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _taxDocId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                if (reader.IsDBNull(15))
                    _dateShip = null;
                else
                    _dateShip = reader.GetDateTime(15);
                if (reader.IsDBNull(16))
                    _datePay = null;
                else
                    _datePay = reader.GetDateTime(16);

                _bankAccFromId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _bankAccToId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }
        internal class TpvCollection : List<DocumentService>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.SupervisorId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ManagerId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.PriceId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.TaxDocId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DateShip, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.DatePay, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.BankAccFromId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.BankAccToId, SqlDbType.Int)
                );

                foreach (DocumentService doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentService doc)
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

            if (doc.SupervisorId == 0)
                sdr.SetValue(11, DBNull.Value);
            else
                sdr.SetInt32(11, doc.SupervisorId);

            if (doc.ManagerId == 0)
                sdr.SetValue(12, DBNull.Value);
            else
                sdr.SetInt32(12, doc.ManagerId);

            if (doc.PriceId == 0)
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetInt32(13, doc.PriceId);

            if (doc.TaxDocId == 0)
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetInt32(14, doc.TaxDocId);

            if (doc.DateShip.HasValue)
                sdr.SetDateTime(15, doc.DateShip.Value);
            else
                sdr.SetValue(15, DBNull.Value);

            if (doc.DatePay.HasValue)
                sdr.SetDateTime(16, doc.DatePay.Value);
            else
                sdr.SetValue(16, DBNull.Value);

            if (doc.BankAccFromId == 0)
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetInt32(17, doc.BankAccFromId);

            if (doc.BankAccToId == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.BankAccToId);
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

            DocumentDetailService.TpvCollection collRows = new DocumentDetailService.TpvCollection();
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
        //public List<IChain<DocumentContract>> GetLinks()
        //{
        //    List<IChain<DocumentContract>> collection = new List<IChain<DocumentContract>>();
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
        //                            DocumentChain<DocumentContract> item = new DocumentChain<DocumentContract> { Workarea = Workarea, Left = this };
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

        #region IChainsAdvancedList<DocumentContract,FileData> Members
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