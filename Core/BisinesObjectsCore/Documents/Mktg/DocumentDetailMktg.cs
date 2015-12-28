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
    internal struct BaseStructDocumentDetailMktg
    {
        /// <summary>Идентификатор типа строки данных</summary>
        public Int32 Kind;
        /// <summary>Идентификатор товара</summary>
        public int ProductId;
        /// <summary>Идентификатор единицы измерения</summary>
        public int UnitId;
        /// <summary>Цена</summary>
        public decimal Price;
        /// <summary>Количество</summary>
        public decimal Qty;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Идентификатор производной единицы измерения</summary>
        public int FUnitId;
        /// <summary>Количество в производной единице измерения</summary>
        public decimal FQty;
        /// <summary>Идентификатор партии товара</summary>
        public int TaxAnaliticId;
        /// <summary>Сумма НДС</summary>
        public decimal SummaTax;
        /// <summary>Сумма списания</summary>
        public decimal SummaExp;
        /// <summary>Декларативная сумма списания</summary>
        public decimal SummaIn;
        /// <summary>Сумма списания по данным последнего прихода</summary>
        public decimal SummaLastIn;
        /// <summary>Скидка в % отношении</summary>
        public decimal Discount;
        /// <summary>Фактическое количество</summary>
        public decimal QtyFact;
        /// <summary>Фактическая сумма</summary>
        public decimal SummaFact;
        /// <summary>Количество в базовой единице измерения, при использовании производной единицы</summary>
        public decimal QtyBase;

    }
    /// <summary>
    /// Детализация документа в разделе "Управление товарными запасами"
    /// </summary>
    public class DocumentDetailMktg : DocumentBaseDetail, IEditableObject, IChainsAdvancedList<DocumentDetailMktg, FileData>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailMktg()
            : base()
        {
            _entityId = 14;

        }
        #region Свойства
        /// <summary>
        /// Основной документ
        /// </summary>
        public DocumentMktg Document { get; set; }


        private decimal _qtyBase;
        /// <summary>
        /// Количество в базовой единице измерения, при использовании производной единицы
        /// </summary>
        public decimal QtyBase
        {
            get { return _qtyBase; }
            set
            {
                if (value == _qtyBase) return;
                OnPropertyChanging(GlobalPropertyNames.QtyBase);
                _qtyBase = value;
                OnPropertyChanged(GlobalPropertyNames.QtyBase);
            }
        }

        private decimal _summaFact;
        /// <summary>
        /// Фактическая сумма
        /// </summary>
        /// <remarks>Используется в документах инвентаризации</remarks>
        public decimal SummaFact
        {
            get { return _summaFact; }
            set
            {
                if (value == _summaFact) return;
                OnPropertyChanging(GlobalPropertyNames.SummaFact);
                _summaFact = value;
                OnPropertyChanged(GlobalPropertyNames.SummaFact);
            }
        }
        
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

        private decimal _fQty;
        /// <summary>
        /// Количество в производной единице измерения
        /// </summary>
        public decimal FQty
        {
            get { return _fQty; }
            set
            {
                if (value == _fQty) return;
                OnPropertyChanging(GlobalPropertyNames.FQty);
                _fQty = value;
                OnPropertyChanged(GlobalPropertyNames.FQty);
            }
        }

        private int _fUnitId;
        /// <summary>
        /// Идентификатор производной единицы измерения
        /// </summary>
        public int FUnitId
        {
            get { return _fUnitId; }
            set
            {
                if (value == _fUnitId) return;
                OnPropertyChanging(GlobalPropertyNames.FUnitId);
                _fUnitId = value;
                OnPropertyChanged(GlobalPropertyNames.FUnitId);
            }
        }

        private decimal _discount;
        /// <summary>
        /// Скидка в % отношении
        /// </summary>
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                if (value == _discount) return;
                OnPropertyChanging(GlobalPropertyNames.Discount);
                _discount = value;
                OnPropertyChanged(GlobalPropertyNames.Discount);
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
            if (_price != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Price, XmlConvert.ToString(_price));
            if (_qty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Qty, XmlConvert.ToString(_qty));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_fUnitId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FUnitId, XmlConvert.ToString(_fUnitId));
            if (_fQty != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FQty, XmlConvert.ToString(_fQty));
            
            if (_summaFact != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SummaFact, XmlConvert.ToString(_summaFact));
            if (_qtyBase != 0)
                writer.WriteAttributeString(GlobalPropertyNames.QtyBase, XmlConvert.ToString(_qtyBase));
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
            if (reader.GetAttribute(GlobalPropertyNames.Price) != null)
                _price = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Price));
            if (reader.GetAttribute(GlobalPropertyNames.Qty) != null)
                _qty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Qty));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo =reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.FUnitId) != null)
                _fUnitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FUnitId));
            if (reader.GetAttribute(GlobalPropertyNames.FQty) != null)
                _fQty = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.FQty));
            
            if (reader.GetAttribute(GlobalPropertyNames.Discount) != null)
                _discount = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Discount));
            
            if (reader.GetAttribute(GlobalPropertyNames.SummaFact) != null)
                _summaFact = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.SummaFact));
            if (reader.GetAttribute(GlobalPropertyNames.QtyBase) != null)
                _qtyBase = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.QtyBase));
        }
        #endregion

        #region Состояния
        BaseStructDocumentDetailMktg _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailMktg
                {
                    Kind = Kind,
                    Memo = _memo,
                    Price = _summa,
                    ProductId = _productId,
                    Qty = _qty,
                    UnitId = _unitId,
                    FQty = _fQty,
                    FUnitId = _fUnitId,
                    Discount = _discount,
                    SummaFact = _summaFact,
                    QtyBase = _qtyBase

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
            _summa = _baseStruct.Price;
            _productId = _baseStruct.ProductId;
            _qty = _baseStruct.Qty;
            _unitId = _baseStruct.UnitId;
            _fQty = _baseStruct.FQty;
            _fUnitId = _baseStruct.FUnitId;
            _discount = _baseStruct.Discount;
            _summaFact = _baseStruct.SummaFact;
            _qtyBase = _baseStruct.QtyBase;
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
            if(StateId!= State.STATEDELETED)
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
            if (FUnitId == 0)
                QtyBase = 0;
            
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
                _productId = reader.GetInt32(12);
                _unitId = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                _qty = reader.GetDecimal(14);
                _price = reader.GetDecimal(15);
                _summa = reader.GetDecimal(16);
                _memo = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _fUnitId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
                _fQty = reader.GetDecimal(19);
                _discount = reader.GetDecimal(20);
                _summaFact = reader.GetDecimal(21);
                _qtyBase = reader.GetDecimal(22);
                _mGuid = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailMktg>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.OwnId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.UnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Qty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.FUnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.FQty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Discount, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.SummaFact, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.QtyBase, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                );

                foreach (DocumentDetailMktg doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailMktg doc)
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
            
            if (doc.UnitId == 0)
                sdr.SetValue(13, DBNull.Value);
            else
                sdr.SetInt32(13, doc.UnitId);

            
            sdr.SetDecimal(14, doc.Qty);
            sdr.SetDecimal(15, doc.Price);
            sdr.SetDecimal(16, doc.Summa);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetString(17, doc.Memo);

            if (doc.FUnitId == 0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.FUnitId);
            sdr.SetDecimal(19, doc.FQty);

            sdr.SetDecimal(20, doc.Discount);
            sdr.SetDecimal(21, doc.SummaFact);
            sdr.SetDecimal(22, doc.QtyBase);
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
            _baseStruct = new BaseStructDocumentDetailMktg();
        }

        #endregion

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailMktg,FileData> Members

        List<IChainAdvanced<DocumentDetailMktg, FileData>> IChainsAdvancedList<DocumentDetailMktg, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailMktg, FileData>)this).GetLinks(50);
        }

        List<IChainAdvanced<DocumentDetailMktg, FileData>> IChainsAdvancedList<DocumentDetailMktg, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DocumentDetailMktg, FileData>.GetChainView()
        {
            return null; //ChainValueView.GetView<Agent, FileData>(this);
        }
        public List<IChainAdvanced<DocumentDetailMktg, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailMktg, FileData>> collection = new List<IChainAdvanced<DocumentDetailMktg, FileData>>();
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
                                ChainAdvanced<DocumentDetailMktg, FileData> item = new ChainAdvanced<DocumentDetailMktg, FileData> { Workarea = Workarea, Left = this };
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