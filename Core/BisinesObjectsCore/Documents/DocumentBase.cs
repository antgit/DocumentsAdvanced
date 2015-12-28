using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects.Documents
{
    internal struct DocumentBaseStruct
    {
        /// <summary>Числовое представление типа документа (раздела)</summary>
        public Int16 EntityId;
        /// <summary>Числовое представление документа</summary>
        public int Kind;
        /// <summary>Дата документа или проводки</summary>
        public DateTime Date;
        /// <summary>Идентификатор состояния</summary>
        public int StateId;
        /// <summary>Набор флагов</summary>
        public int Flags;
    }
    /// <summary>
    /// Дополнительные данные документа, соответствует расшифровке заголовка документа
    /// </summary>
    public abstract class DocumentBase: BaseCoreObject, IDocument
    {
        #region Конструктор
        /// <summary>Конструктор</summary>
        protected DocumentBase()
        {
        }
        #endregion

        #region Свойства

        private Document _document;
        /// <summary>
        /// Основная операция
        /// </summary>
        public Document Document
        {
            get { return _document; }
            set { _document = value; }
        }

        private Int16 _entityId;
        /// <summary>
        /// Числовое представление типа документа (раздела)
        /// </summary>
        public Int16 EntityId
        {
            get { return _entityId; }
            set
            {
                if (value == _entityId) return;
                OnPropertyChanging(GlobalPropertyNames.EntityId);
                _entityId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityId);
            }
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
                    _entityDocument = Workarea.Cashe.CollectionDocumentTypes().FirstOrDefault(f => f.Id == _entityId);
                    //_entityDocument = new EntityDocument { Workarea = Workarea };
                    //_entityDocument.Load(_entityId);
                }
                else if (_entityDocument.Id != _entityId)
                {
                    //_entityDocument = new EntityDocument { Workarea = Workarea };
                    //_entityDocument.Load(_entityId);
                    _entityDocument = Workarea.Cashe.CollectionDocumentTypes().FirstOrDefault(f => f.Id == _entityId);
                }
                return _entityDocument;
            }
        }

        private int _kind;
        /// <summary>
        /// Числовое представление документа
        /// </summary>
        public int Kind
        {
            get { return _kind; }
            set 
            {
                if (value == _kind) return;
                OnPropertyChanging(GlobalPropertyNames.KindValue);
                _kind = value;
                OnPropertyChanged(GlobalPropertyNames.KindValue);
            }
        }

        private DateTime _date;
        /// <summary>
        /// Дата документа или проводки
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


        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_entityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.EntityId, XmlConvert.ToString(_entityId));
            if (_kind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Kind, XmlConvert.ToString(_kind));
            //if (_date != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Date, XmlConvert.ToString(_date));
            /*if (string.IsNullOrEmpty(_stateId))
                writer.WriteAttributeString(GlobalPropertyNames.StateId, _stateId);
            if (string.IsNullOrEmpty(_flags))
                writer.WriteAttributeString(GlobalPropertyNames.Flags, _flags);*/
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.EntityId) != null)
                _entityId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.EntityId));
            if (reader.GetAttribute(GlobalPropertyNames.Kind) != null)
                _kind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Kind));
            if (reader.GetAttribute(GlobalPropertyNames.Date) != null)
                _date = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.Date));
            /*if (reader.GetAttribute(GlobalPropertyNames.StateId) != null)
                _stateId = reader.GetAttribute(GlobalPropertyNames.StateId);
            if (reader.GetAttribute(GlobalPropertyNames.Flags) != null)
                _flags = reader.GetAttribute(GlobalPropertyNames.Flags);*/
        }
        #endregion

        #region Состояния

        private DocumentBaseStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new DocumentBaseStruct
                                  {
                                      Date = Date,
                                      EntityId = EntityId,
                                      Flags = FlagsValue,
                                      Kind = Kind,
                                      StateId = StateId
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
            EntityId = _baseStruct.EntityId;
            FlagsValue = _baseStruct.Flags;
            Kind = _baseStruct.Kind;
            StateId = _baseStruct.StateId;
            IsChanged = false;
        } 
        #endregion

        protected internal override string FindProcedure(string metodAliasName)
        {
            return EntityDocument.FindMethod(metodAliasName).FullName;
        }
        private void Load(Document op)
        {
            _document = op;
            if (!op.IsNew)
            {
                Load(op.Id);
            }
        }
        void IDocument.Load(Document op)
        {
            Load(op);
        }
        #region База данных
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, endInit);
            try
            {
                _date = reader.GetDateTime(9);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>
        /// Загрузка данных по идентификатору
        /// </summary>
        /// <param name="value"></param>
        public override void Load(int value)
        {
            Load(value, FindProcedure("Load"));
        }
        /// <summary>
        /// Удалить дополнительные данные документа, соответствует расшифровке заголовка документа, нельзя без удаления самого документа, поэтому  удаляем сам документ.
        /// </summary>
        /// <returns></returns>
        public override void Delete(bool checkVersion = true)
        {
            if (_document != null)
                _document.Delete();
            //Workarea.DeleteById(Id, FindProcedure("Delete"));
        }

        /// <summary>Установить значения параметров для комманды создания или обновления</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.StateId, SqlDbType.Int) { IsNullable = false, Value = StateId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Date, SqlDbType.DateTime) { IsNullable = false, Value = Date };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Flags, SqlDbType.Int) { IsNullable = false, Value = FlagsValue };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Kind, SqlDbType.Int) { IsNullable = false, Value = _kind };
            sqlCmd.Parameters.Add(prm);

            sqlCmd.Parameters[GlobalSqlParamNames.Id].Value = Id;

        }

        protected void SetParametersToSigns(List<DocumentSign> coll, SqlCommand sqlCmd, bool validateVersion)
        {
            if (coll == null) coll = new List<DocumentSign>();
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Return, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            sqlCmd.Parameters.Add(prm);

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int);
            prm.Direction = ParameterDirection.Input;
            prm.Value = Id;

            DocumentSign.TpvCollection collRows = new DocumentSign.TpvCollection();
            
            foreach (DocumentSign row in Document._signs)
            {
                row.OwnId = this.Id;
                row.Validate();
            }
            collRows.AddRange(coll);
            if (coll.Count == 0)
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.DocumentSings, null);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
            else
            {
                var detailsTypeParam = sqlCmd.Parameters.AddWithValue(GlobalSqlParamNames.DocumentSings, collRows);
                detailsTypeParam.SqlDbType = SqlDbType.Structured;
            }
        }
        #endregion
    }
}
