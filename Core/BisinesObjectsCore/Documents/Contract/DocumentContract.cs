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
    internal struct BaseStructDocumentContract
    {
        /// <summary>Иденгтификатор регистратора документа</summary>
        public int RegistratorId;
        /// <summary>Идентификатор типа договора</summary>
        public int ContractKindId;
        /// <summary>Идентификатор текущего состояния договора</summary>
        public int StateCurrentId;
        /// <summary>Идентификатор важности договора</summary>
        public int ImportanceId;
        /// <summary>Дата начала договора</summary>
        public DateTime? DateStart;
        /// <summary>Дата окончания договора</summary>
        public DateTime? DateEnd;
        /// <summary>Исходящий номер</summary>
        public string NumberOut;
    }

    /// <summary>
    /// Документ учета документов
    /// </summary>
    public class DocumentContract : DocumentBase, IEditableObject
    {/// <summary>Конструктор</summary>
        public DocumentContract()
            : base()
        {
            EntityId = 4;
            _details = new List<DocumentDetailContract>();
            _analitics = new List<DocumentAnalitic>();
        }
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Договор, соответствует значению 1</summary>
        public const int KINDVALUE_DOGOVOR = 1;
        /// <summary>Акт ревизии, соответствует значению 2</summary>
        public const int KINDVALUE_REVISION = 2;
        /// <summary>Акт сверки клиента, соответствует значению 3</summary>
        public const int KINDVALUE_VERIFICATION = 3;
        /// <summary>Счета-заявки, соответствует значению 4</summary>
        public const int KINDVALUE_ACCOUNTIN = 4;
        /// <summary>Договор поставки по видам продукции, соответствует значению 5</summary>
        public const int KINDVALUE_SALEOUT = 5;
        /// <summary>Письмо входящее, соответствует значению 6</summary>
        public const int KINDVALUE_LETTERIN = 6;
        /// <summary>Письмо входящее, соответствует значению 7</summary>
        public const int KINDVALUE_LETTEROUT = 7;
        /// <summary>Распоряжение, соответствует значению 8</summary>
        public const int KINDVALUE_INSTRUCTION = 8;
        /// <summary>Приказ, соответствует значению 9</summary>
        public const int KINDVALUE_COMMAND = 9;
        /// <summary>Учет компьютеров, соответствует значению, 10</summary>
        public const int KINDVALUE_COMPUTER = 10;
        /// <summary>Учет принтеров, соответствует значению 11</summary>
        public const int KINDVALUE_PRINTER = 11;
        /// <summary>Служебная записка, соответствует значению 12</summary>
        public const int KINDVALUE_OFFICIALNOTE = 11;
        /// <summary>Настройки раздела, соответствует значению 100</summary>
        public const int KINDVALUE_CONFIG = 100;

        /// <summary>Договор, соответствует значению 262145</summary>
        public const int KINDID_DOGOVOR = 262145;
        /// <summary>Акт ревизии, соответствует значению 262146</summary>
        public const int KINDID_REVISION = 262146;
        /// <summary>Акт сверки клиента, соответствует значению 262147</summary>
        public const int KINDID_VERIFICATION = 262147;
        /// <summary>Счета-заявки, соответствует значению 262148</summary>
        public const int KINDID_ACCOUNTIN = 262148;
        /// <summary>Договор поставки по видам продукции, соответствует значению 262149</summary>
        public const int KINDID_SALEOUT = 262149;
        /// <summary>Письмо входящее 262150</summary>
        public const int KINDID_LETTERIN = 262150;
        /// <summary>Письмо входящее 262151</summary>
        public const int KINDID_LETTEROUT = 262151;
        /// <summary>Распоряжение 262152</summary>
        public const int KINDID_INSTRUCTION = 262152;
        /// <summary>Приказ 262153</summary>
        public const int KINDID_COMMAND = 262153;
        /// <summary>Учет компьютеров 262154</summary>
        public const int KINDID_COMPUTER = 262154;
        /// <summary>Учет принтеров 262155</summary>
        public const int KINDID_PRINTER = 262155;
        /// <summary>Служебная записка, соответствует значению 262156</summary>
        public const int KINDID_OFFICIALNOTE = 262156;

        /// <summary>Настройки раздела, соответствует значению 262244</summary>
        public const int KINDID_CONFIG = 262244;
        // ReSharper restore InconsistentNaming
        #endregion
        #region Свойства
        private List<DocumentDetailContract> _details;
        /// <summary>
        /// Детализация документа учета документов
        /// </summary>
        public List<DocumentDetailContract> Details
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
        private int _registratorId;
        /// <summary>
        /// Иденгтификатор регистратора документа
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

        private int _contractKindId;
        /// <summary>
        /// Идентификатор типа договора
        /// </summary>
        public int ContractKindId
        {
            get { return _contractKindId; }
            set
            {
                if (value == _contractKindId) return;
                OnPropertyChanging(GlobalPropertyNames.ContractKindId);
                _contractKindId = value;
                OnPropertyChanged(GlobalPropertyNames.ContractKindId);
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
        /// Дата начала договора
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
        /// Дата окончания договора
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


        private string _numberOut;
        /// <summary>
        /// Исходящий номер
        /// </summary>
        public string NumberOut
        {
            get { return _numberOut; }
            set
            {
                if (value == _numberOut) return;
                OnPropertyChanging(GlobalPropertyNames.NumberOut);
                _numberOut = value;
                OnPropertyChanged(GlobalPropertyNames.NumberOut);
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
            if (_contractKindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ContractKindId, XmlConvert.ToString(_contractKindId));
            if (_stateCurrentId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.StateCurrentId, XmlConvert.ToString(_stateCurrentId));
            if (_importanceId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ImportanceId, XmlConvert.ToString(_importanceId));
            if (_dateStart.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(_dateStart.Value));
            if (_dateEnd.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(_dateEnd.Value));
            if (!string.IsNullOrEmpty(_numberOut))
                writer.WriteAttributeString(GlobalPropertyNames.NumberOut, _numberOut);
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
                _contractKindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ContractKindId));
            if (reader.GetAttribute(GlobalPropertyNames.StateCurrentId) != null)
                _stateCurrentId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.StateCurrentId));
            if (reader.GetAttribute(GlobalPropertyNames.ImportanceId) != null)
                _importanceId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ImportanceId));
            if (reader.GetAttribute(GlobalPropertyNames.DateStart) != null)
                _dateStart = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart));
            if (reader.GetAttribute(GlobalPropertyNames.DateEnd) != null)
                _dateEnd = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateEnd));
            if (reader.GetAttribute(GlobalPropertyNames.NumberOut) != null)
                _numberOut = reader.GetAttribute(GlobalPropertyNames.NumberOut);
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
        public DocumentDetailContract NewRow()
        {
            DocumentDetailContract row = new DocumentDetailContract
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
        BaseStructDocumentContract _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentContract
                  {
                      ContractKindId = _contractKindId,
                      DateEnd = _dateEnd,
                      DateStart= _dateStart,
                      ImportanceId = _importanceId,
                      NumberOut = _numberOut,
                      RegistratorId = _registratorId,
                      StateCurrentId = _stateCurrentId
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
            _contractKindId = _baseStruct.ContractKindId;
            _dateEnd = _baseStruct.DateEnd;
            _dateStart = _baseStruct.DateStart;
            _importanceId = _baseStruct.ImportanceId;
            _numberOut = _baseStruct.NumberOut;
            _registratorId = _baseStruct.RegistratorId;
            _stateCurrentId = _baseStruct.StateCurrentId;
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
            foreach (DocumentDetailContract row in _details)
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
                throw new ValidateException("Не указан отправитель");
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
                                            DocumentDetailContract docRow = new DocumentDetailContract { Workarea = Workarea,Document = this };
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
                                            DocumentDetailContract docRow = new DocumentDetailContract { Workarea = Workarea, Document = this };
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

        public static DocumentContract CreateCopy(DocumentContract value)
        {

            DocumentContract doc = new DocumentContract { Workarea = value.Workarea };
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
                                            DocumentDetailContract docRow = new DocumentDetailContract { Workarea = value.Workarea, Document = doc };
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
                                        DocumentDetailContract docRow = new DocumentDetailContract {Workarea = Workarea,Document = this};
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
                _contractKindId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                _registratorId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                _stateCurrentId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _numberOut = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                _dateStart = reader.IsDBNull(15) ? (DateTime?) null : reader.GetDateTime(15);
                _dateEnd = reader.IsDBNull(16) ? (DateTime?) null : reader.GetDateTime(16);
                _importanceId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }
        internal class TpvCollection : List<DocumentContract>, IEnumerable<SqlDataRecord>
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
                new SqlMetaData(GlobalPropertyNames.ContractKind, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.Registrator, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.StateCurrent, SqlDbType.Int),
                new SqlMetaData(GlobalPropertyNames.NumberOut, SqlDbType.NVarChar, 50),
                new SqlMetaData(GlobalPropertyNames.DateStart, SqlDbType.Date),
                new SqlMetaData(GlobalPropertyNames.DateEnd, SqlDbType.Date),
                new SqlMetaData(GlobalPropertyNames.Importance, SqlDbType.Int)
                );

                foreach (DocumentContract doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentContract doc)
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

            if (doc.ContractKindId == 0)
                sdr.SetValue(11, DBNull.Value);
            else
                sdr.SetInt32(11, doc.ContractKindId);
            if (doc.RegistratorId == 0)
                sdr.SetValue(12, DBNull.Value);
            else
                sdr.SetInt32(12, doc.RegistratorId);

            if (doc.StateCurrentId == 0)
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetInt32(13, doc.StateCurrentId);
            if (string.IsNullOrEmpty(doc.NumberOut))
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetString(14, doc.NumberOut);

            if (!doc.DateStart.HasValue)
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetDateTime(15, doc.DateStart.Value);
            if (!doc.DateEnd.HasValue)
                sdr.SetValue(16, DBNull.Value);
            else
                sdr.SetDateTime(16, doc.DateEnd.Value);

            if (doc.ImportanceId == 0)
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetInt32(17, doc.ImportanceId);
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

            DocumentDetailContract.TpvCollection collRows = new DocumentDetailContract.TpvCollection();
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
    }
}
