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

    internal struct BaseStructDocumentDetailContract
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
        /// <summary>Идентификатор аналитики</summary>
        public int AnaliticId;
        /// <summary>Идентификатор аналитики №2</summary>
        public int AnaliticId2;
        /// <summary>Идентификатор аналитики №3</summary>
        public int AnaliticId3;
        /// <summary>Идентификатор аналитики №4</summary>
        public int AnaliticId4;
        /// <summary>Идентификатор аналитики №5</summary>
        public int AnaliticId5;
        /// <summary>Строковое значение №1</summary>
        public string StringValue1;
        /// <summary>Строковое значение №2</summary>
        public string StringValue2;
        /// <summary>Строковое значение №3</summary>
        public string StringValue3;
        /// <summary>Строковое значение №4</summary>
        public string StringValue4;
        /// <summary>Строковое значение №5</summary>
        public string StringValue5;
    }
    /// <summary>
    /// Детализация документа в разделе "Договора и документы"
    /// </summary>
    public class DocumentDetailContract : DocumentBaseDetail, IEditableObject, IChainsAdvancedList<DocumentDetailContract, FileData>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentDetailContract()
            : base()
        {
            _entityId = 4;

        }
        #region Свойства
        /// <summary>
        /// Основной документ
        /// </summary>
        public DocumentContract Document { get; set; }

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
                if (value == _price) return;
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

        private int _analiticId;
        /// <summary>Идентификатор аналитики</summary>
        public int AnaliticId
        {
            get { return _analiticId; }
            set
            {
                if (value == _analiticId) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId);
                _analiticId = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId);
            }
        }

        private Analitic _analitic;
        /// <summary>Аналитика</summary>
        public Analitic Analitic
        {
            get
            {
                if (_analiticId == 0)
                    return null;
                if (_analitic == null)
                    _analitic = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId);
                else if (_analitic.Id != _analiticId)
                    _analitic = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId);
                return _analitic;
            }
            set
            {
                if (_analitic == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic);
                _analitic = value;
                _analiticId = _analitic == null ? 0 : _analitic.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic);
            }
        }


        private int _analiticId2;
        /// <summary>Идентификатор аналитики №2</summary>
        public int AnaliticId2
        {
            get { return _analiticId2; }
            set
            {
                if (value == _analiticId2) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId2);
                _analiticId2 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId2);
            }
        }

        private Analitic _analitic2;
        /// <summary>Аналитика №2</summary>
        public Analitic Analitic2
        {
            get
            {
                if (_analiticId2 == 0)
                    return null;
                if (_analitic2 == null)
                    _analitic2 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId2);
                else if (_analitic2.Id != _analiticId2)
                    _analitic2 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId2);
                return _analitic2;
            }
            set
            {
                if (_analitic2 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic2);
                _analitic2 = value;
                _analiticId2 = _analitic2 == null ? 0 : _analitic2.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic2);
            }
        }

        private int _analiticId3;
        /// <summary>Идентификатор аналитики №3</summary>
        public int AnaliticId3
        {
            get { return _analiticId3; }
            set
            {
                if (value == _analiticId3) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId3);
                _analiticId3 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId3);
            }
        }


        private Analitic _analitic3;
        /// <summary>Аналитика №3</summary>
        public Analitic Analitic3
        {
            get
            {
                if (_analiticId3 == 0)
                    return null;
                if (_analitic3 == null)
                    _analitic3 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId3);
                else if (_analitic3.Id != _analiticId3)
                    _analitic3 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId3);
                return _analitic3;
            }
            set
            {
                if (_analitic3 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic3);
                _analitic3 = value;
                _analiticId3 = _analitic3 == null ? 0 : _analitic3.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic3);
            }
        }


        private int _analiticId4;
        /// <summary>Идентификатор аналитики №4</summary>
        public int AnaliticId4
        {
            get { return _analiticId4; }
            set
            {
                if (value == _analiticId4) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId4);
                _analiticId4 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId4);
            }
        }


        private Analitic _analitic4;
        /// <summary>Аналитика №4</summary>
        public Analitic Analitic4
        {
            get
            {
                if (_analiticId4 == 0)
                    return null;
                if (_analitic4 == null)
                    _analitic4 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId4);
                else if (_analitic4.Id != _analiticId4)
                    _analitic4 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId4);
                return _analitic4;
            }
            set
            {
                if (_analitic4 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic4);
                _analitic4 = value;
                _analiticId4 = _analitic4 == null ? 0 : _analitic4.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic4);
            }
        }


        private int _analiticId5;
        /// <summary>Идентификатор аналитики №5</summary>
        public int AnaliticId5
        {
            get { return _analiticId5; }
            set
            {
                if (value == _analiticId5) return;
                OnPropertyChanging(GlobalPropertyNames.AnaliticId5);
                _analiticId5 = value;
                OnPropertyChanged(GlobalPropertyNames.AnaliticId5);
            }
        }


        private Analitic _analitic5;
        /// <summary>Аналитика №5</summary>
        public Analitic Analitic5
        {
            get
            {
                if (_analiticId5 == 0)
                    return null;
                if (_analitic5 == null)
                    _analitic5 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId5);
                else if (_analitic5.Id != _analiticId5)
                    _analitic5 = Workarea.Cashe.GetCasheData<Analitic>().Item(_analiticId5);
                return _analitic5;
            }
            set
            {
                if (_analitic5 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Analitic5);
                _analitic5 = value;
                _analiticId5 = _analitic5 == null ? 0 : _analitic5.Id;
                OnPropertyChanged(GlobalPropertyNames.Analitic5);
            }
        }

        private string _stringValue1;
        /// <summary>Строковое значение №1</summary>
        public string StringValue1
        {
            get { return _stringValue1; }
            set
            {
                if (value == _stringValue1) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue1);
                _stringValue1 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue1);
            }
        }


        private string _stringValue2;
        /// <summary>Строковое значение №2</summary>
        public string StringValue2
        {
            get { return _stringValue2; }
            set
            {
                if (value == _stringValue2) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue2);
                _stringValue2 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue2);
            }
        }



        private string _stringValue3;
        /// <summary>Строковое значение №3</summary>
        public string StringValue3
        {
            get { return _stringValue3; }
            set
            {
                if (value == _stringValue3) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue3);
                _stringValue3 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue3);
            }
        }

        private string _stringValue4;
        /// <summary>Строковое значение №4</summary>
        public string StringValue4
        {
            get { return _stringValue4; }
            set
            {
                if (value == _stringValue4) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue4);
                _stringValue4 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue4);
            }
        }


        private string _stringValue5;
        /// <summary>Строковое значение №5</summary>
        public string StringValue5
        {
            get { return _stringValue5; }
            set
            {
                if (value == _stringValue5) return;
                OnPropertyChanging(GlobalPropertyNames.StringValue5);
                _stringValue5 = value;
                OnPropertyChanged(GlobalPropertyNames.StringValue5);
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
                _price = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Price));
            if (reader.GetAttribute(GlobalPropertyNames.Qty) != null)
                _qty = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Qty));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.FUnitId) != null)
                _fUnitId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FUnitId));
            if (reader.GetAttribute(GlobalPropertyNames.FQty) != null)
                _fQty = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FQty));
        }
        #endregion

        #region Состояния
        BaseStructDocumentDetailContract _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            if (base.SaveState(overwrite))
            {
                _baseStruct = new BaseStructDocumentDetailContract
                                  {
                                      Kind = Kind,
                                      Memo = _memo,
                                      Price = _summa,
                                      ProductId = _productId,
                                      Qty = _qty,
                                      UnitId = _unitId,
                                      FUnitId = _fUnitId,
                                      FQty = _fQty,
                                      AnaliticId = _analiticId,
                                      AnaliticId2 = _analiticId2,
                                      AnaliticId3 = _analiticId3,
                                      AnaliticId4 = _analiticId4,
                                      AnaliticId5 = _analiticId5,
                                      StringValue1 = _stringValue1,
                                      StringValue2 = _stringValue2,
                                      StringValue3 = _stringValue3,
                                      StringValue4 = _stringValue4,
                                      StringValue5 = _stringValue5
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
            _fUnitId = _baseStruct.FUnitId;
            _fQty = _baseStruct.FQty;
            _analiticId = _baseStruct.AnaliticId;
            _analiticId2 = _baseStruct.AnaliticId2;
            _analiticId3 = _baseStruct.AnaliticId3;
            _analiticId4 = _baseStruct.AnaliticId4;
            _analiticId5 = _baseStruct.AnaliticId5;
            _stringValue1 = _baseStruct.StringValue1;
            _stringValue2 = _baseStruct.StringValue2;
            _stringValue3 = _baseStruct.StringValue3;
            _stringValue4 = _baseStruct.StringValue4;
            _stringValue5 = _baseStruct.StringValue5;
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
                _analiticId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
                _analiticId2 = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                _analiticId3 = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                _analiticId4 = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
                _analiticId5 = reader.IsDBNull(24) ? 0 : reader.GetInt32(24);
                _stringValue1 = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                _stringValue2 = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
                _stringValue3 = reader.IsDBNull(27) ? string.Empty : reader.GetString(27);
                _stringValue4 = reader.IsDBNull(28) ? string.Empty : reader.GetString(28);
                _stringValue5 = reader.IsDBNull(29) ? string.Empty : reader.GetString(29);
                _mGuid = reader.IsDBNull(30) ? Guid.Empty : reader.GetGuid(30);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            OnEndInit();
        }

        internal class TpvCollection : List<DocumentDetailContract>, IEnumerable<SqlDataRecord>
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
                    new SqlMetaData(GlobalPropertyNames.KindId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.DocId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.ProductId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.UnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.Qty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Price, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Summa, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.Memo, SqlDbType.NVarChar, 255),
                    new SqlMetaData(GlobalPropertyNames.FUnitId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.FQty, SqlDbType.Money),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId2, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId3, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId4, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.AnaliticId5, SqlDbType.Int),
                    new SqlMetaData(GlobalPropertyNames.StringValue1, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue2, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue3, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue4, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.StringValue5, SqlDbType.NVarChar, 100),
                    new SqlMetaData(GlobalPropertyNames.MGuid, SqlDbType.UniqueIdentifier)
                    );

                foreach (DocumentDetailContract doc in this)
                {
                    FillDataRecort(sdr, doc);
                    yield return sdr;
                }
            }
        }
        internal static IDataRecord FillDataRecort(SqlDataRecord sdr, DocumentDetailContract doc)
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

            sdr.SetInt32(11, doc.Document.Id);
            sdr.SetInt32(12, doc.ProductId);
            if (doc.UnitId != 0)
                sdr.SetInt32(13, doc.UnitId);
            else
                sdr.SetValue(13, DBNull.Value);
            sdr.SetDecimal(14, doc.Qty);
            sdr.SetDecimal(15, doc.Price);
            sdr.SetDecimal(16, doc.Summa);
            if (string.IsNullOrEmpty(doc.Memo))
                sdr.SetValue(17, DBNull.Value);
            else
                sdr.SetString(17, doc.Memo);

            if (doc.FUnitId==0)
                sdr.SetValue(18, DBNull.Value);
            else
                sdr.SetInt32(18, doc.FUnitId);
            sdr.SetDecimal(19, doc.FQty);

            if (doc.AnaliticId == 0)
                sdr.SetValue(20, DBNull.Value);
            else
                sdr.SetInt32(20, doc.AnaliticId);

            if (doc.AnaliticId2 == 0)
                sdr.SetValue(21, DBNull.Value);
            else
                sdr.SetInt32(21, doc.AnaliticId2);

            if (doc.AnaliticId3 == 0)
                sdr.SetValue(22, DBNull.Value);
            else
                sdr.SetInt32(22, doc.AnaliticId3);
            if (doc.AnaliticId4 == 0)
                sdr.SetValue(23, DBNull.Value);
            else
                sdr.SetInt32(23, doc.AnaliticId4);
            if (doc.AnaliticId5 == 0)
                sdr.SetValue(24, DBNull.Value);
            else
                sdr.SetInt32(24, doc.AnaliticId5);

            if (string.IsNullOrEmpty(doc.StringValue1))
                sdr.SetValue(25, DBNull.Value);
            else
                sdr.SetString(25, doc.StringValue1);

            if (string.IsNullOrEmpty(doc.StringValue2))
                sdr.SetValue(26, DBNull.Value);
            else
                sdr.SetString(26, doc.StringValue2);

            if (string.IsNullOrEmpty(doc.StringValue3))
                sdr.SetValue(27, DBNull.Value);
            else
                sdr.SetString(27, doc.StringValue3);

            if (string.IsNullOrEmpty(doc.StringValue4))
                sdr.SetValue(28, DBNull.Value);
            else
                sdr.SetString(28, doc.StringValue4);

            if (string.IsNullOrEmpty(doc.StringValue5))
                sdr.SetValue(29, DBNull.Value);
            else
                sdr.SetString(29, doc.StringValue5);

            sdr.SetGuid(30, doc.MGuid);

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
            _baseStruct = new BaseStructDocumentDetailContract();
        }

        #endregion

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            //throw new NotImplementedException();
        }

        #region IChainsAdvancedList<DocumentDetailContract,FileData> Members

        List<IChainAdvanced<DocumentDetailContract, FileData>> IChainsAdvancedList<DocumentDetailContract, FileData>.GetLinks()
        {
            return ((IChainsAdvancedList<DocumentDetailContract, FileData>)this).GetLinks(56);
        }

        List<IChainAdvanced<DocumentDetailContract, FileData>> IChainsAdvancedList<DocumentDetailContract, FileData>.GetLinks(int? kind)
        {
            return GetLinkedFiles();
        }
        List<ChainValueView> IChainsAdvancedList<DocumentDetailContract, FileData>.GetChainView()
        {
            // TODO: 
            return null; //ChainValueView.GetView<DocumentDetailContract, FileData>(this);
        }
        public List<IChainAdvanced<DocumentDetailContract, FileData>> GetLinkedFiles()
        {
            List<IChainAdvanced<DocumentDetailContract, FileData>> collection = new List<IChainAdvanced<DocumentDetailContract, FileData>>();
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
                                ChainAdvanced<DocumentDetailContract, FileData> item = new ChainAdvanced<DocumentDetailContract, FileData> { Workarea = Workarea, Left = this };
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