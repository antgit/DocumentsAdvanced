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
    internal struct BaseStructDocumentDetailPerson
    {
        /// <summary>Идентификатор типа строки данных</summary>
        public Int32 Kind;
        /// <summary>Идентификатор товара</summary>
        public int EmployerId;
        /// <summary>Идентификатор единицы измерения</summary>
        public int UnitId;
        /// <summary>Сумма</summary>
        public decimal Summa;
        /// <summary>Примечание</summary>
        public string Memo;
    }
    /// <summary>
    /// Детализация финансового документа
    /// </summary>
    public class DocumentDetailPerson : DocumentBaseDetail, IEditableObject,
        IChainsAdvancedList<DocumentDetailPerson, FileData>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailPerson()
            : base()
        {
            _entityId = 13;

        }
        #region Свойства

        /// <summary>
        /// Документ
        /// </summary>
        public DocumentPerson Document { get; set; }

        //private int _kind;
        ///// <summary>
        ///// Тип товарной операции
        ///// </summary>
        ///// <remarks>Текущие типы товарной операции: 
        ///// 0-приход, 
        ///// 1-Расход
        ///// </remarks>
        //public int Kind
        //{
        //    get { return _kind; }
        //    set
        //    {
        //        if (value != _kind)
        //        {
        //            OnPropertyChanging("KindValue");
        //            _kind = value;
        //            OnPropertyChanged("KindValue");
        //        }
        //    }
        //}

        private int _employerId;
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int EmployerId
        {
            get { return _employerId; }
            set
            {
                if (value == _employerId) return;
                OnPropertyChanging(GlobalPropertyNames.EmployerId);
                _employerId = value;
                OnPropertyChanged(GlobalPropertyNames.EmployerId);
            }
        }

        private Agent _employer;
        /// <summary>
        /// Товар
        /// </summary>
        public Agent Employer
        {
            get
            {
                if (_employerId == 0)
                    return null;
                if (_employer == null)
                    _employer = Workarea.Cashe.GetCasheData<Agent>().Item(_employerId);
                else if (_employer.Id != _employerId)
                    _employer = Workarea.Cashe.GetCasheData<Agent>().Item(_employerId);
                return _employer;
            }
            set
            {
                if (_employer == value) return;
                OnPropertyChanging(GlobalPropertyNames.Employer);
                _employer = value;
                _employerId = _employer == null ? 0 : _employer.Id;
                OnPropertyChanged(GlobalPropertyNames.Employer);
            }
        }

        private decimal _summa;
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Summa
        {
            get { return _summa; }
            set
            {
                if (value == _summa) return;
                OnPropertyChanging(GlobalPropertyNames.Summa);
                _summa = value;
                OnPropertyChanged(GlobalPropertyNames.Summa);
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

        #endregion

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_employerId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.EmployerId, XmlConvert.ToString(_employerId));
            if (_summa != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Summa, XmlConvert.ToString(_summa));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.EmployerId) != null)
                _employerId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.EmployerId));
            if (reader.GetAttribute(GlobalPropertyNames.Summa) != null)
                _summa = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Summa));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
        }
        #endregion

        #region Состояния
        BaseStructDocumentDetailPerson _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailPerson
                {
                    Kind = Kind,
                    Memo = _memo,
                    Summa = _summa,
                    EmployerId = _employerId,
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
            Kind = _baseStruct.Kind;
            _memo = _baseStruct.Memo;
            _summa = _baseStruct.Summa;
            _employerId = _baseStruct.EmployerId;
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
            if (_employerId == 0)
                throw new ValidateException("Не указан сотрудник");

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
                _employerId = reader.GetInt32(12);
                _summa = reader.GetDecimal(13);
                _memo = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                _mGuid = reader.IsDBNull(15) ? Guid.Empty : reader.GetGuid(15);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailPerson>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.FlagsValue, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StateId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Date, SqlDbType.Date),
                    new SqlMetaData(GlobalPropertyNames.OwnId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.EmployerId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                );

                foreach (DocumentDetailPerson doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailPerson doc)
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
            sdr.SetInt32(12, doc.EmployerId);
            sdr.SetDecimal(13, doc.Summa);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(14, DBNull.Value);
            else
                sdr.SetString(14, doc.Memo);
            sdr.SetGuid(15, doc.MGuid);
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
            _baseStruct = new BaseStructDocumentDetailPerson();
        }

        #endregion

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailPerson,FileData> Members

        List<IChainAdvanced<DocumentDetailPerson, FileData>> IChainsAdvancedList<DocumentDetailPerson, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailPerson, FileData>)this).GetLinks(55);
        }

        List<IChainAdvanced<DocumentDetailPerson, FileData>> IChainsAdvancedList<DocumentDetailPerson, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DocumentDetailPerson, FileData>.GetChainView()
        {
            // TODO: 
            return null; //ChainValueView.GetView<Agent, FileData>(this);
        }
        public List<IChainAdvanced<DocumentDetailPerson, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailPerson, FileData>> collection = new List<IChainAdvanced<DocumentDetailPerson, FileData>>();
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
                                ChainAdvanced<DocumentDetailPerson, FileData> item = new ChainAdvanced<DocumentDetailPerson, FileData> { Workarea = Workarea, Left = this };
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
