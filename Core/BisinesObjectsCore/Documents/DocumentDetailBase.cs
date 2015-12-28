using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Documents
{
    internal struct  DocumentBaseDetailStruct
    {
        /// <summary>Идентификатор типа строки данных</summary>
        public int Kind;
        /// <summary>Идентификатор документа</summary>
        public int OwnerId;
        /// <summary>Дата</summary>
        public DateTime Date;
    }
    /// <summary>
    /// Базовый класс детализации документов
    /// </summary>
    /// <remarks>Данный класс представляет собой строку документа</remarks>
    public abstract class DocumentBaseDetail : BaseCoreObject, IDocumentDetail
    {
        /// <summary>Конструктор</summary>
        protected DocumentBaseDetail()
            : base()
        {
        }
        #region Свойства

        private int _ownerId;
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public int OwnerId
        {
            get { return _ownerId; }
            set
            {
                if (value == _ownerId) return;
                OnPropertyChanging(GlobalPropertyNames.OwnerId);
                _ownerId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnerId);
            }
        }


        protected Guid _mGuid;
        /// <summary>
        /// Служебный глобальный идентификатор, не предназначен для прямого использования!
        /// </summary>
        public Guid MGuid
        {
            get { return _mGuid; }
            set
            {
                if (value == _mGuid) return;
                OnPropertyChanging(GlobalPropertyNames.MGuid);
                _mGuid = value;
                OnPropertyChanged(GlobalPropertyNames.MGuid);
            }
        }
        
        //private Document _document;
        ///// <summary>
        ///// Основная операция
        ///// </summary>
        //public Document Document
        //{
        //    get { return _document; }
        //    set { _document = value; }
        //}

        protected Int16 _entityId;

        public Int16 EntityId
        {
            get { return _entityId; }
            set { _entityId = value; }
        }
        private EntityDocument _entityDocument;
        /// <summary>
        /// Тип документа
        /// </summary>
        public EntityDocument EntityDocument
        {
            get
            {
                if (_entityId == 0 | Workarea == null)
                    return null;
                if (_entityDocument == null)
                {
                    _entityDocument = new EntityDocument{Workarea=Workarea};
                    _entityDocument.Load(_entityId);
                }
                else if (_entityDocument.Id != _entityId)
                {
                    _entityDocument = new EntityDocument { Workarea = Workarea };
                    _entityDocument.Load(_entityId);
                }
                return _entityDocument;
            }
        }

        private DateTime _date;
        /// <summary>
        /// Дата
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

        private int _kind;
        /// <summary>
        /// Идентификатор типа строки данных
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

        private List<DocumentDetailAnalitic> _analitics;
        /// <summary>
        /// Детализация строки документа на уровне аналитики
        /// </summary>
        /// <remarks>Данные о аналитике на уровне строк не загружаются автоматически! Используйте метод RefreshAnalitic данног класа или 
        /// RefreshDetailAnalitic() родительского класса.
        ///  </remarks>
        public List<DocumentDetailAnalitic> Analitics
        {
            get
            {
                if (_analitics == null)
                {
                    _analitics = new List<DocumentDetailAnalitic>();
                    //RefreshDocumentDetailAnalitic();
                }
                return _analitics;
            }
            set { _analitics = value; }
        }
        #endregion
        /// <summary>
        /// Обновить данные о дополнительной алаитике строки
        /// </summary>
        public void RefreshAnalitics()
        {
            if (Id == 0)
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
                        sqlCmd.CommandText = EntityDocument.Methods.Find(f => f.Method == "DocumentDetailAnaliticsLoadByOwnId").FullName;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.OwnId, SqlDbType.Int).Value = Id;

                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        sqlCmd.Parameters.Add(prm);

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        _analitics.Clear();

                        while (reader.Read())
                        {
                            DocumentDetailAnalitic docRow = new DocumentDetailAnalitic
                                                                {Workarea = Workarea, Document = this};
                            docRow.Load(reader);
                            _analitics.Add(docRow);
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
        /// <summary>
        /// Сохранение данных о аналитических данных строки документа
        /// </summary>
        public void SaveAnalitics()
        {
            using (SqlConnection cnn = (this as ICoreObject).Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = EntityDocument.Methods.Find(f => f.Method == "DocumentDetailAnaliticsInsert").FullName; ;

                        DocumentDetailAnalitic.TpvCollection collAnalitics = new DocumentDetailAnalitic.TpvCollection();
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
                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
                        sqlCmd.Parameters.Add(prm);

                        _analitics.Clear();
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DocumentDetailAnalitic docRow = new DocumentDetailAnalitic { Workarea = Workarea, Document = this };
                                docRow.Load(reader);
                                _analitics.Add(docRow);
                            }
                        }
                        reader.Close();

                        object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));
                        if ((Int32)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (Int32)retval);
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            
            
        }
        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_kind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Kind, XmlConvert.ToString(_kind));
            if (_ownerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnerId, XmlConvert.ToString(_ownerId));
            //if (_date != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Kind) != null)
                _kind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Kind));
            if (reader.GetAttribute(GlobalPropertyNames.OwnerId) != null)
                _ownerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnerId));
            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
        }
        #endregion

        #region Состояния

        private DocumentBaseDetailStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentBaseDetailStruct
                                  {
                                      Date = Date,
                                      OwnerId = OwnerId,
                                      Kind = Kind
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
            Date = _baseStruct.Date;
            OwnerId = _baseStruct.OwnerId;
            Kind = _baseStruct.Kind;
            _entityId = EntityDocumentKind.ExtractEntityKind(Kind);
            IsChanged = false;
        }
        #endregion

        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _date = reader.GetDateTime(9);
                _ownerId = reader.GetInt32(10);
                _kind = reader.GetInt32(11);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
        }


        #endregion
    }
}