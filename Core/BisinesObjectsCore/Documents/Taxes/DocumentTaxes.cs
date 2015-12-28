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
    internal struct BaseStructDocumentTax
    {
        /// <summary>Идентификатор документа торговли</summary>
        public int SalesDocId;
        /// <summary>Идентификатор усливия поставки</summary>
        public string DeliveryCondition;
        /// <summary>Идентификатор формы проведенных расчетов</summary>
        public string PaymentMethod;
        /// <summary>Номер договора</summary>
        public string DogovorNo;
        /// <summary>Дата договора</summary>
        public DateTime? DogovorDate;
    }
    /// <summary>
    /// Документ раздела "Налоговые накладные"
    /// </summary>
    public class DocumentTaxes : DocumentBase, IEditableObject, IChainsAdvancedList<Document, FileData>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Налоговая накладная исходящая, соответствует значению 1</summary>
        public const int KINDVALUE_OUT = 1;
        /// <summary>Налоговая накладная входящая, соответствует значению 2</summary>
        public const int KINDVALUE_IN = 2;
        /// <summary>Корректировочная налоговая накладная исходящая, соответствует значению 3</summary>
        public const int KINDVALUE_COROUT = 3;
        /// <summary>Корректировочная налоговая накладная входящая, соответствует значению 4</summary>
        public const int KINDVALUE_CORIN = 4;
        /// <summary>Настройки раздела, соответствует значению 100</summary>
        public const int KINDVALUE_CONFIG = 100;


        /// <summary>Налоговая накладная исходящая, соответствует значению 458753</summary>
        public const int KINDID_OUT = 458753;
        /// <summary>Налоговая накладная входящая, соответствует значению 458754</summary>
        public const int KINDID_IN = 458754;
        /// <summary>Корректировочная налоговая накладная исходящая, соответствует значению 458755</summary>
        public const int KINDID_COROUT = 458755;
        /// <summary>Корректировочная налоговая накладная входящая, соответствует значению 458756</summary>
        public const int KINDID_CORIN = 458756;
        /// <summary>Настройки раздела, соответствует значению 458852</summary>
        public const int KINDID_CONFIG = 458852;
        
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>Конструктор</summary>
        public DocumentTaxes()
            : base()
        {
            EntityId = 7;
            _details = new List<DocumentDetailTax>();
            _analitics = new List<DocumentAnalitic>();
        }

        #region Свойства

        private DateTime? _dogovorDate;
        /// <summary>
        /// Дата договора
        /// </summary>
        public DateTime? DogovorDate
        {
            get { return _dogovorDate; }
            set
            {
                if (value == _dogovorDate) return;
                OnPropertyChanging(GlobalPropertyNames.DogovorDate);
                _dogovorDate = value;
                OnPropertyChanged(GlobalPropertyNames.DogovorDate);
            }
        }

        private string _dogovorNo;
        /// <summary>
        /// Номер договора
        /// </summary>
        public string DogovorNo
        {
            get { return _dogovorNo; }
            set
            {
                if (value == _dogovorNo) return;
                OnPropertyChanging(GlobalPropertyNames.DogovorNo);
                _dogovorNo = value;
                OnPropertyChanged(GlobalPropertyNames.DogovorNo);
            }
        }
        
        
        internal List<DocumentDetailTax> _details;
        /// <summary>
        /// Детализация налогового документа
        /// </summary>
        public List<DocumentDetailTax> Details
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
        private int _salesDocId;
        /// <summary>
        /// Идентификатор документа торговли
        /// </summary>
        public int SalesDocId
        {
            get { return _salesDocId; }
            set
            {
                if (value == _salesDocId) return;
                OnPropertyChanging(GlobalPropertyNames.SalesDocId);
                _salesDocId = value;
                OnPropertyChanged(GlobalPropertyNames.SalesDocId);
            }
        }

        private string _deliveryCondition;
        /// <summary>
        /// Идентификатор усливия поставки
        /// </summary>
        public string DeliveryCondition
        {
            get { return _deliveryCondition; }
            set
            {
                if (value == _deliveryCondition) return;
                OnPropertyChanging(GlobalPropertyNames.DeliveryCondition);
                _deliveryCondition = value;
                OnPropertyChanged(GlobalPropertyNames.DeliveryCondition);
            }
        }


        private string _paymentMethod;
        /// <summary>
        /// Идентификатор формы проведенных расчетов
        /// </summary>
        public string PaymentMethod
        {
            get { return _paymentMethod; }
            set
            {
                if (value == _paymentMethod) return;
                OnPropertyChanging(GlobalPropertyNames.PaymentMethod);
                _paymentMethod = value;
                OnPropertyChanged(GlobalPropertyNames.PaymentMethod);
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

            if (_salesDocId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SalesDocId, XmlConvert.ToString(_salesDocId));
            if (!string.IsNullOrEmpty(_deliveryCondition))
                writer.WriteAttributeString(GlobalPropertyNames.DeliveryCondition, _deliveryCondition);
            if (!string.IsNullOrEmpty(_paymentMethod))
                writer.WriteAttributeString(GlobalPropertyNames.PaymentMethod, _paymentMethod);
            if (!string.IsNullOrEmpty(_dogovorNo))
                writer.WriteAttributeString(GlobalPropertyNames.DogovorNo, _dogovorNo);
            if (_dogovorDate.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DogovorDate, XmlConvert.ToString(_dogovorDate.Value));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.SalesDocId) != null)
                _salesDocId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SalesDocId));
            if (reader.GetAttribute(GlobalPropertyNames.DeliveryCondition) != null)
                _deliveryCondition = reader.GetAttribute(GlobalPropertyNames.DeliveryCondition);
            if (reader.GetAttribute(GlobalPropertyNames.PaymentMethod) != null)
                _paymentMethod = reader.GetAttribute(GlobalPropertyNames.PaymentMethod);
            if (reader.GetAttribute(GlobalPropertyNames.DogovorNo) != null)
                _dogovorNo = reader.GetAttribute(GlobalPropertyNames.DogovorNo);
            if (reader.GetAttribute(GlobalPropertyNames.DogovorDate) != null)
                _dogovorDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DogovorDate));
        }
        #endregion

        #region Состояния
        BaseStructDocumentTax _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentTax
                                  {
                                      SalesDocId = _salesDocId,
                                      DeliveryCondition = _deliveryCondition,
                                      PaymentMethod = _paymentMethod,
                                      DogovorDate = _dogovorDate,
                                      DogovorNo = _dogovorNo
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
            _salesDocId = _baseStruct.SalesDocId;
            _deliveryCondition = _baseStruct.DeliveryCondition;
            _paymentMethod = _baseStruct.PaymentMethod;
            _dogovorDate = _baseStruct.DogovorDate;
            _dogovorNo = _baseStruct.DogovorNo;
            IsChanged = false;
        }
        #endregion

        #region Дополнительные методы
        /// <summary>
        /// Добавить новую строку детализации
        /// </summary>
        /// <returns></returns>
        public DocumentDetailTax NewRow()
        {
            DocumentDetailTax row = new DocumentDetailTax
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
            if (Workarea.IsWebApplication)
                Document.UserName = UserName;
            if(string.IsNullOrEmpty(_deliveryCondition))
                throw new ValidateException("Не указаны условия поставки");
            if(string.IsNullOrEmpty(_paymentMethod))
                throw new ValidateException("Не указана форма проведенных расчетов");
            Document.Validate();
            base.Validate();
            foreach (DocumentDetailTax row in _details)
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
            if (_collDocumentContractor!=null)
                foreach (DocumentContractor row in _collDocumentContractor)
                {
                    row.OwnId = this.Id;
                    row.Validate();
                }
            if (Document._signs != null)
            {
                foreach (DocumentSign s in Document._signs)
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
                                            DocumentDetailTax docRow = new DocumentDetailTax { Workarea = Workarea, Document = this };
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
                                            DocumentDetailTax docRow = new DocumentDetailTax { Workarea = Workarea, Document = this };
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

        public static DocumentTaxes CreateCopy(DocumentTaxes value)
        {

            DocumentTaxes doc = new DocumentTaxes { Workarea = value.Workarea };
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
                                            DocumentDetailTax docRow = new DocumentDetailTax { Workarea = value.Workarea, Document = doc };
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
                                        DocumentDetailTax docRow = new DocumentDetailTax { Workarea = Workarea, Document = this };
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
                _salesDocId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                _deliveryCondition = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                _paymentMethod = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                if (reader.IsDBNull(14))
                    _dogovorDate = null;
                else
                    _dogovorDate = reader.GetDateTime(14);

                _dogovorNo = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }
        internal class TpvCollection : List<DocumentTaxes>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.SalesDocId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DeliveryCondition, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.PaymentMethod, SqlDbType.NVarChar,100),
                    new SqlMetaData(GlobalPropertyNames.DogovorDate, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.DogovorNo, SqlDbType.NVarChar,50)
                    );

                foreach (DocumentTaxes doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentTaxes doc)
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
            
            if (doc.SalesDocId == 0)
                sdr.SetValue(11, DBNull.Value);
            else
                sdr.SetInt32(11, doc.SalesDocId);

            if (string.IsNullOrEmpty(doc.DeliveryCondition))
                sdr.SetValue(12, DBNull.Value);
            else
                sdr.SetString(12, doc.DeliveryCondition);

            if (string.IsNullOrEmpty(doc.PaymentMethod))
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetString(13, doc.PaymentMethod);

            if (doc.DogovorDate.HasValue)
                sdr.SetDateTime(14, doc.DogovorDate.Value);
            else
                sdr.SetValue(14, DBNull.Value);

            if (string.IsNullOrEmpty(doc.DogovorNo))
                sdr.SetValue(15, DBNull.Value);
            else
                sdr.SetString(15, doc.DogovorNo);
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

            Document.TpvCollection coll = new Document.TpvCollection { Document };
            var headerParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.Header, coll);
            headerParam.SqlDbType = SqlDbType.Structured;

            TpvCollection collTypes = new TpvCollection { this };
            var headerTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.HeaderType, collTypes);
            headerTypeParam.SqlDbType = SqlDbType.Structured;

            DocumentDetailTax.TpvCollection collRows = new DocumentDetailTax.TpvCollection();
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