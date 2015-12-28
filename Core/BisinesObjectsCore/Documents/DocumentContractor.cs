using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Дополнительные корреспонденты документа
    /// </summary>
    public sealed class DocumentContractor: BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentContractor()
        {
            EntityId = (short)WhellKnownDbEntity.DocumentContractor;   
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

        private int _agentId;
        /// <summary>
        /// Корреспондент
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
        

        private string _memo;
        /// <summary>
        /// Примечание
        /// </summary>
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

        #region База данных
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
                _kind = reader.GetInt32(11);
                _memo = reader.IsDBNull(12)? string.Empty: reader.GetString(12);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
        }


        #endregion

        /// <summary>
        /// Проверка соответствия объекта системным требованиям
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (_ownId == 0)
                throw new ValidateException("Не указан документ");
            if (_agentId == 0)
                throw new ValidateException("Не указан сотрудник");
            
        }
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

            prm = sqlCmd.Parameters.Add(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, -1);
            prm.Direction = ParameterDirection.Input;
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _memo;
        }

        internal class TpvCollection : List<DocumentContractor>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.Kind, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, -1)
                );

                foreach (DocumentContractor doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentContractor doc)
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
            sdr.SetInt32(11, doc.Kind);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(12, DBNull.Value);
            else
                sdr.SetString(12, doc.Memo);

            
            return sdr;
        }
    }
}