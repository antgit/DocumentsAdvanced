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
    internal struct BaseStructDocumentDetailPrice
    {
        /// <summary>Идентификатор типа строки данных</summary>
        public Int32 Kind;
        /// <summary>Идентификатор товара</summary>
        public int ProductId;
        /// <summary>Идентификатор ценовой политики</summary>
        public int PrcNameId;
        /// <summary>Цена</summary>
        public decimal Value;
        /// <summary>Примечание</summary>
        public string Memo;
    }
    /// <summary>
    /// Детализация документа "Прайс-лист"
    /// </summary>
    public class DocumentDetailPrice : DocumentBaseDetail, IEditableObject, IChainsAdvancedList<DocumentDetailPrice, FileData>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailPrice()
            : base()
        {
            _entityId = 2;

        }
        #region Свойства
        /// <summary>
        /// Основной документ
        /// </summary>
        public DocumentPrices Document { get; set; }

        //private int _kind;
        ///// <summary>
        ///// Тип товарной операции
        ///// </summary>
        ///// <remarks>Текущие типы товарной операции: 
        ///// 0-приход, 
        ///// 1-Расход, 
        ///// 2-перемещение, 
        ///// 3-возврат товара поставщику,
        ///// 4-возврат товара от покупателя</remarks>
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

        private int _prcNameId;
        /// <summary>
        /// Идентификатор ценовой политики
        /// </summary>
        public int PrcNameId
        {
            get { return _prcNameId; }
            set
            {
                if (value == _prcNameId) return;
                OnPropertyChanging(GlobalPropertyNames.PrcNameId);
                _prcNameId = value;
                OnPropertyChanged(GlobalPropertyNames.PrcNameId);
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
                if (_prcNameId == 0)
                    return null;
                if (_priceName == null)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_prcNameId);
                else if (_priceName.Id != _prcNameId)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_prcNameId);
                return _priceName;
            }
            set
            {
                if (_priceName == value) return;
                OnPropertyChanging(GlobalPropertyNames.PriceName);
                _priceName = value;
                _prcNameId = _priceName == null ? 0 : _priceName.Id;
                OnPropertyChanged(GlobalPropertyNames.PriceName);
            }
        }

        private decimal _value;
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
            }
        }

        private decimal _valueOld;
        /// <summary>
        /// Предыдущая цена
        /// </summary>
        public decimal ValueOld
        {
            get { return _valueOld; }
            set
            {
                if (_valueOld == value) return;
                OnPropertyChanging(GlobalPropertyNames.ValueOld);
                _valueOld = value;
                OnPropertyChanged(GlobalPropertyNames.ValueOld);
            }
        }

        private decimal _discount;
        /// <summary>
        /// Коэффициент скидки
        /// </summary>
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                if (_discount == value) return;
                OnPropertyChanging(GlobalPropertyNames.Discount);
                _discount = value;
                OnPropertyChanged(GlobalPropertyNames.Discount);
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

            /*if (_kind != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Kind, XmlConvert.ToString(_kind));*/
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            if (_prcNameId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PrcNameId, XmlConvert.ToString(_prcNameId));
            if (_value != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Value, XmlConvert.ToString(_value));
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

            /*if (reader.GetAttribute(GlobalPropertyNames.Kind) != null)
                _kind = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Kind));*/
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            if (reader.GetAttribute(GlobalPropertyNames.PrcNameId) != null)
                _prcNameId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PrcNameId));
            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Value));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
        }
        #endregion

        #region Состояния
        BaseStructDocumentDetailPrice _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailPrice
                {
                    Kind = Kind,
                    Memo = _memo,
                    Value = _value,
                    ProductId = _productId,
                    PrcNameId = _prcNameId,
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
            _value = _baseStruct.Value;
            _productId = _baseStruct.ProductId;
            _prcNameId = _baseStruct.PrcNameId;
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
            PrcNameId = Document.PrcNameId;
            if (Kind == 0)
            {
                throw new ValidateException("Не указан тип строки документа");
            }
            if (_productId == 0)
                throw new ValidateException("Не указан товар");

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
                _prcNameId = reader.GetInt32(12);
                _productId = reader.GetInt32(13);
                _value = reader.GetDecimal(14);
                _valueOld = reader.GetDecimal(15);
                _discount = reader.GetDecimal(16);
                _memo = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _mGuid = reader.IsDBNull(18) ? Guid.Empty : reader.GetGuid(18);
                
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
            //try
            //{
            //    Id = reader.GetInt32(0);
            //    Guid = reader.GetGuid(1);
            //    DatabaseId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
            //    DbSourceId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
            //    ObjectVersion = reader.GetSqlBinary(4).Value;
            //    UserName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            //    if (reader.IsDBNull(6))
            //        DateModified = null;
            //    else
            //        DateModified = reader.GetDateTime(6);
            //    FlagsValue = reader.GetInt32(7);
            //    StateId = reader.GetInt32(8);
            //    OwnerId = reader.GetInt32(10);
            //    Date = reader.GetDateTime(9);
            //    _prcNameId = reader.GetInt32(11);
            //    _productId = reader.GetInt32(12);
            //    _value = reader.GetDecimal(13);
            //    _valueOld = reader.GetDecimal(14);
            //    _discount = reader.GetDecimal(15);
            //    _memo = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
            //}
            //catch(Exception ex)
            //{
            //    throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            //}
            //OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailPrice>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.PrcContentId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Value, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.ValueOld, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Discount, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                );

                foreach (DocumentDetailPrice doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailPrice doc)
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
            sdr.SetInt32(12, doc.PrcNameId);
            sdr.SetInt32(13, doc.ProductId);
            sdr.SetDecimal(14, doc.Value);
            sdr.SetDecimal(15, doc.ValueOld);
            sdr.SetDecimal(16, doc.Discount);
            
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetString(17, doc.Memo);
            sdr.SetGuid(18, doc.MGuid);
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
            _baseStruct = new BaseStructDocumentDetailPrice();
        }

        #endregion

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailPrice,FileData> Members

        List<IChainAdvanced<DocumentDetailPrice, FileData>> IChainsAdvancedList<DocumentDetailPrice, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailPrice, FileData>)this).GetLinks(54);
        }

        List<IChainAdvanced<DocumentDetailPrice, FileData>> IChainsAdvancedList<DocumentDetailPrice, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DocumentDetailPrice, FileData>.GetChainView()
        {
            return null;//ChainValueView.GetView<Agent, FileData>(this);
        }
        public List<IChainAdvanced<DocumentDetailPrice, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailPrice, FileData>> collection = new List<IChainAdvanced<DocumentDetailPrice, FileData>>();
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
                                ChainAdvanced<DocumentDetailPrice, FileData> item = new ChainAdvanced<DocumentDetailPrice, FileData> { Workarea = Workarea, Left = this };
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
