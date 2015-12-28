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
    internal struct BaseStructDocumentDetailBookKeep
    {
        /// <summary>Идентификатор типа строки данных</summary>
        public Int32 Kind;
        /// <summary>/// Идентификатор товара</summary>
        public int ProductId;
        /// <summary>Идентификатор единицы измерения</summary>
        public int UnitId;
        /// <summary>Сумма</summary>
        public decimal Summa;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Группа проводок</summary>
        public int GroupNo;
        /// <summary>Идентификатор счета по дебету</summary>
        public int AccountDbId;
        /// <summary>Идентификатор счета по кредиту</summary>
        public int AccountCrId;
        /// <summary>Идентификатор корреспондента "Кто"</summary>
        public int AgentFromId;
        /// <summary>Идентификатор корреспондента "Кому"</summary>
        public int AgentToId;
    }
    /// <summary>
    /// Детализация бухгалтеского документа
    /// </summary>
    public class DocumentDetailBookKeep : DocumentBaseDetail, IEditableObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailBookKeep()
            : base()
        {
            _entityId = 3;

        }
        #region Свойства

        /// <summary>
        /// Документ
        /// </summary>
        public DocumentBookKeep Document { get; set; }

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

        private int _accountDbId;
        /// <summary>
        /// Идентификатор счета по дебету
        /// </summary>
        public int AccountDbId
        {
            get { return _accountDbId; }
            set
            {
                if (value != _accountDbId)
                {
                    OnPropertyChanging(GlobalPropertyNames.AccountDbId);
                    _accountDbId = value;
                    OnPropertyChanged(GlobalPropertyNames.AccountDbId);
                }
            }
        }
        
        private Account _accDb;
        /// <summary>
        /// Счет по дебету
        /// </summary>
        public Account AccountDb
        {
            get
            {
                if (_accountDbId == 0)
                    return null;
                if (_accDb == null)
                    _accDb = Workarea.Cashe.GetCasheData<Account>().Item(_accountDbId);
                else if (_accDb.Id != _accountDbId)
                    _accDb = Workarea.Cashe.GetCasheData<Account>().Item(_accountDbId);
                return _accDb;
            }
            set
            {
                if (_accDb == value) return;
                OnPropertyChanging(GlobalPropertyNames.AccountDb);
                _accDb = value;
                _accountDbId = _accDb == null ? 0 : _accDb.Id;
                OnPropertyChanged(GlobalPropertyNames.AccountDb);
            }
        }
        
        private int _accountCrId;
        /// <summary>
        /// Идентификатор счета по кредиту
        /// </summary>
        public int AccountCrId
        {
            get { return _accountCrId; }
            set
            {
                if (value != _accountCrId)
                {
                    OnPropertyChanging(GlobalPropertyNames.AccountCrId);
                    _accountCrId = value;
                    OnPropertyChanged(GlobalPropertyNames.AccountCrId);
                }
            }
        }
        
        private Account _accCr;
        /// <summary>
        /// Счет по кредиту
        /// </summary>
        public Account AccountCr
        {
            get
            {
                if (_accountCrId == 0)
                    return null;
                if (_accCr == null)
                    _accCr = Workarea.Cashe.GetCasheData<Account>().Item(_accountCrId);
                else if (_accCr.Id != _accountCrId)
                    _accCr = Workarea.Cashe.GetCasheData<Account>().Item(_accountCrId);
                return _accCr;
            }
            set
            {
                if (_accCr == value) return;
                OnPropertyChanging(GlobalPropertyNames.AccountCr);
                _accCr = value;
                _accountCrId = _accCr == null ? 0 : _accCr.Id;
                OnPropertyChanged(GlobalPropertyNames.AccountCr);
            }
        }
        
        private int _productId;
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int ProductId
        {
            get { return _productId; }
            set
            {
                if (value == _productId) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId);
                _productId = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId);
            }
        }

        private Product _product;
        /// <summary>
        /// Товар
        /// </summary>
        public Product Product
        {
            get
            {
                if (_productId == 0)
                    return null;
                if (_product == null)
                    _product = Workarea.Cashe.GetCasheData<Product>().Item(_productId);
                else if (_product.Id != _productId)
                    _product = Workarea.Cashe.GetCasheData<Product>().Item(_productId);
                return _product;
            }
            set
            {
                if (_product == value) return;
                OnPropertyChanging(GlobalPropertyNames.Product);
                _product = value;
                _productId = _product == null ? 0 : _product.Id;
                OnPropertyChanged(GlobalPropertyNames.Product);
            }
        }

        private int _unitId;
        /// <summary>
        /// Идентификатор единицы измерения
        /// </summary>
        public int UnitId
        {
            get { return _unitId; }
            set
            {
                if (value == _unitId) return;
                OnPropertyChanging(GlobalPropertyNames.UnitId);
                _unitId = value;
                OnPropertyChanged(GlobalPropertyNames.UnitId);
            }
        }

        private Unit _unit;
        /// <summary>
        /// Единица измерения 
        /// </summary>
        public Unit Unit
        {
            get
            {
                if (_unitId == 0)
                    return null;
                if (_unit == null)
                    _unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
                else if (_unit.Id != _unitId)
                    _unit = Workarea.Cashe.GetCasheData<Unit>().Item(_unitId);
                return _unit;
            }
            set
            {
                if (_unit == value) return;
                OnPropertyChanging(GlobalPropertyNames.Unit);
                _unit = value;
                _unitId = _unit == null ? 0 : _unit.Id;
                OnPropertyChanged(GlobalPropertyNames.Unit);
            }
        }

        private decimal _price;
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price == value) return;
                OnPropertyChanging(GlobalPropertyNames.Price);
                _price = value;
                OnPropertyChanged(GlobalPropertyNames.Price);
            }
        }
        private decimal _qty;
        /// <summary>
        /// Количество
        /// </summary>
        public decimal Qty
        {
            get { return _qty; }
            set
            {
                if (value == _qty) return;
                OnPropertyChanging(GlobalPropertyNames.Qty);
                _qty = value;
                OnPropertyChanged(GlobalPropertyNames.Qty);
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


        private Agent _agFrom;
        /// <summary>
        /// Корреспондент "Кто" 
        /// </summary>
        public Agent AgentFrom
        {
            get
            {
                if (_agentFromId == 0)
                    return null;
                if (_agFrom == null)
                    _agFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_agentFromId);
                else if (_agFrom.Id != _agentFromId)
                    _agFrom = Workarea.Cashe.GetCasheData<Agent>().Item(_agentFromId);
                return _agFrom;
            }
            set
            {
                if (_agFrom == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFrom);
                _agFrom = value;
                _agentFromId = _agFrom == null ? 0 : _agFrom.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentFrom);
            }
        }
        
        private int _agentFromId;
        /// <summary>
        /// Идентификатор корреспондента "Кто"
        /// </summary>
        public int AgentFromId
        {
            get { return _agentFromId; }
            set
            {
                if (value == _agentFromId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentFromId);
                _agentFromId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentFromId);
            }
        }


        private Agent _agTo;
        /// <summary>
        /// Корреспондент "Кому" 
        /// </summary>
        public Agent AgentTo
        {
            get
            {
                if (_agentToId == 0)
                    return null;
                if (_agTo == null)
                    _agTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToId);
                else if (_agTo.Id != _agentToId)
                    _agTo = Workarea.Cashe.GetCasheData<Agent>().Item(_agentToId);
                return _agTo;
            }
            set
            {
                if (_agTo == value) return;
                OnPropertyChanging(GlobalPropertyNames.AgentTo);
                _agTo = value;
                _agentToId = _agTo == null ? 0 : _agTo.Id;
                OnPropertyChanged(GlobalPropertyNames.AgentTo);
            }
        }
        

        private int _agentToId;
        /// <summary>
        /// Идентификатор корреспондента "Кому"
        /// </summary>
        public int AgentToId
        {
            get { return _agentToId; }
            set
            {
                if (value == _agentToId) return;
                OnPropertyChanging(GlobalPropertyNames.AgentToId);
                _agentToId = value;
                OnPropertyChanged(GlobalPropertyNames.AgentToId);
            }
        }
        
        private int _groupNo;
        /// <summary>
        /// Группа проводок
        /// </summary>
        public int GroupNo
        {
            get { return _groupNo; }
            set
            {
                if (value == _groupNo) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo);
                _groupNo = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo);
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

            /*if (_kind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Kind, XmlConvert.ToString(_kind));*/
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            if (_unitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.UnitId, XmlConvert.ToString(_unitId));
            if (_summa != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Summa, XmlConvert.ToString(_summa));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_groupNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.GroupNo, XmlConvert.ToString(_groupNo));
            if (_accountDbId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AccountDbId, XmlConvert.ToString(_accountDbId));
            if (_accountCrId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AccountCrId, XmlConvert.ToString(_accountCrId));
            if (_agentFromId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentFromId, XmlConvert.ToString(_agentFromId));
            if (_agentToId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.AgentToId, XmlConvert.ToString(_agentToId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            /*if (reader.GetAttribute(GlobalPropertyNames.Kind) != null)
                _kind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Kind));*/
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            if (reader.GetAttribute(GlobalPropertyNames.UnitId) != null)
                _unitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.UnitId));
            if (reader.GetAttribute(GlobalPropertyNames.Summa) != null)
                _summa = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Summa));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.GroupNo) != null)
                _groupNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.GroupNo));
            if (reader.GetAttribute(GlobalPropertyNames.AccountDbId) != null)
                _accountDbId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AccountDbId));
            if (reader.GetAttribute(GlobalPropertyNames.AccountCrId) != null)
                _accountCrId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AccountCrId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentFromId) != null)
                _agentFromId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentFromId));
            if (reader.GetAttribute(GlobalPropertyNames.AgentToId) != null)
                _agentToId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.AgentToId));
        }
        #endregion

        #region Состояния
        BaseStructDocumentDetailBookKeep _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailBookKeep
                {
                    Kind = Kind,
                    Memo = _memo,
                    Summa = _summa,
                    ProductId = _productId,
                    UnitId = _unitId,
                    GroupNo = _groupNo,
                    AccountCrId = _accountCrId,
                    AccountDbId = _accountDbId,
                    AgentFromId = _agentFromId,
                    AgentToId = _agentToId
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
            _productId = _baseStruct.ProductId;
            _unitId = _baseStruct.UnitId;
            _groupNo = _baseStruct.GroupNo;
            _accountCrId = _baseStruct.AccountCrId;
            _accountDbId = _baseStruct.AccountDbId;
            _agentFromId = _baseStruct.AgentFromId;
            _agentToId = _baseStruct.AgentToId;
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
            if (_accountCrId == 0)
                throw new ValidateException("Не указан счет по кредиту");
            if (_accountDbId == 0)
                throw new ValidateException("Не указан счет по дебету");
            if (_agentFromId == 0)
                throw new ValidateException("Не указан первый корреспондент");
            if (_agentToId == 0)
                throw new ValidateException("Не указан второй корреспондент");
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
                _productId = reader.GetInt32(12);
                _unitId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _summa = reader.GetDecimal(14);
                _memo = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                _accountDbId = reader.GetInt32(16);
                _accountCrId = reader.GetInt32(17);
                _agentFromId = reader.GetInt32(18);
                _agentToId = reader.GetInt32(19);
                _groupNo = reader.GetInt32(20);
                _mGuid = reader.IsDBNull(21) ? Guid.Empty : reader.GetGuid(21);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailBookKeep>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.ProductId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.UnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Qty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.AccountDbId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AccountCrId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentFromId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AgentToId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.GroupNo, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                );

                foreach (DocumentDetailBookKeep doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailBookKeep doc)
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
            sdr.SetInt32(12, doc.ProductId);
            sdr.SetInt32(13, doc.UnitId);
            sdr.SetDecimal(14, doc.Qty);
            sdr.SetDecimal(15, doc.Price);
            sdr.SetDecimal(16, doc.Summa);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetString(17, doc.Memo);

            sdr.SetInt32(18, doc.AccountDbId);
            sdr.SetInt32(19, doc.AccountCrId);
            sdr.SetInt32(20, doc.AgentFromId);
            sdr.SetInt32(21, doc.AgentToId);
            sdr.SetInt32(22, doc.GroupNo);
            sdr.SetGuid(23, doc.MGuid);

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
            _baseStruct = new BaseStructDocumentDetailBookKeep();
        }

        #endregion

        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }
    }
}