using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Ценовой диапазон"</summary>
    internal struct PriceRegionStruct
    {
        /// <summary>Идентификатор вида цены</summary>
        public int PriceNameId;
        /// <summary>Идентификатор товара</summary>
        public int ProductId;
        /// <summary>Значение</summary>
        public decimal Value;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Минимальное значение</summary>
        public decimal ValueMin;
        /// <summary>Максимальное значение</summary>
        public decimal ValueMax;
        /// <summary>Начало</summary>
        public DateTime DateStart;
        /// <summary>Дата окончания</summary>
        public DateTime DateEnd;
    }

    /// <summary>
    /// Ценовой диапазон
    /// </summary>
    public class PriceRegion : BaseCoreObject, IComparable, IComparable<PriceRegion>
    {
        /// <summary>Конструктор</summary>
        public PriceRegion()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.PriceRegion;
        }
        public int CompareTo(object obj)
        {
            PriceRegion otherValue = (PriceRegion)obj;
            return Id.CompareTo(otherValue.Id);
        }

        public int CompareTo(PriceRegion other)
        {
            return Id.CompareTo(other.Id);
        }

        #region Свойства


        private int _priceNameId;
        /// <summary>
        /// Идентификатор цены
        /// </summary>
        public int PriceNameId
        {
            get { return _priceNameId; }
            set
            {
                if (value == _priceNameId) return;
                OnPropertyChanging(GlobalPropertyNames.PriceNameId);
                _priceNameId = value;
                OnPropertyChanged(GlobalPropertyNames.PriceNameId);
            }
        }


        private PriceName _priceName;
        /// <summary>
        /// Вид цены
        /// </summary>
        public PriceName PriceName
        {
            get
            {
                if (_priceNameId == 0)
                    return null;
                if (_priceName == null)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_priceNameId);
                else if (_priceName.Id != _priceNameId)
                    _priceName = Workarea.Cashe.GetCasheData<PriceName>().Item(_priceNameId);
                return _priceName;
            }
            set
            {
                if (_priceName == value) return;
                OnPropertyChanging(GlobalPropertyNames.PriceName);
                _priceName = value;
                _priceNameId = _priceName == null ? 0 : _priceName.Id;
                OnPropertyChanged(GlobalPropertyNames.PriceName);
            }
        }


        private int _productId;
        /// <summary>
        /// Идентификатор цены
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


        private decimal _value;
        /// <summary>
        /// Значение
        /// </summary>
        public decimal Value
        {
            get { return _value; }
            set
            {
                if (value == _value) return;
                OnPropertyChanging(GlobalPropertyNames.Value);
                _value = value;
                OnPropertyChanged(GlobalPropertyNames.Value);
            }
        }


        private decimal _valueMin;
        /// <summary>
        /// Минимальное значение
        /// </summary>
        public decimal ValueMin
        {
            get { return _valueMin; }
            set
            {
                if (value == _valueMin) return;
                OnPropertyChanging(GlobalPropertyNames.ValueMin);
                _valueMin = value;
                OnPropertyChanged(GlobalPropertyNames.ValueMin);
            }
        }

        private decimal _valueMax;
        /// <summary>
        /// Максимальное значение
        /// </summary>
        public decimal ValueMax
        {
            get { return _valueMax; }
            set
            {
                if (value == _valueMax) return;
                OnPropertyChanging(GlobalPropertyNames.ValueMax);
                _valueMax = value;
                OnPropertyChanged(GlobalPropertyNames.ValueMax);
            }
        }


        private DateTime _dateStart;
        /// <summary>Начало</summary>
        public DateTime DateStart
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


        private DateTime _dateEnd;
        /// <summary>Дата окончания</summary>
        public DateTime DateEnd
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
        
        
        private string _memo;
        /// <summary>Примечание</summary>
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

            if (_priceNameId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PriceNameId, XmlConvert.ToString(_priceNameId));
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
            if (_value != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Value, XmlConvert.ToString(_value));
            if (_valueMin != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ValueMin, XmlConvert.ToString(_valueMin));
            if (_valueMax != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ValueMax, XmlConvert.ToString(_valueMax));
            writer.WriteAttributeString(GlobalPropertyNames.DateStart, XmlConvert.ToString(_dateStart,  XmlDateTimeSerializationMode.Local));
            writer.WriteAttributeString(GlobalPropertyNames.DateEnd, XmlConvert.ToString(_dateEnd, XmlDateTimeSerializationMode.Local));
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

            if (reader.GetAttribute(GlobalPropertyNames.PriceNameId) != null)
                _priceNameId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PriceNameId));
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
            if (reader.GetAttribute(GlobalPropertyNames.Value) != null)
                _value = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.Value));
            if (reader.GetAttribute(GlobalPropertyNames.ValueMin) != null)
                _valueMin = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.ValueMin));
            if (reader.GetAttribute(GlobalPropertyNames.ValueMax) != null)
                _valueMax = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.ValueMax));
            if (reader.GetAttribute(GlobalPropertyNames.DateStart) != null)
                _dateStart = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateStart),  XmlDateTimeSerializationMode.Local);
            if (reader.GetAttribute(GlobalPropertyNames.DateEnd) != null)
                _dateEnd = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.DateEnd), XmlDateTimeSerializationMode.Local);
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        public override void Validate()
        {
            if (_value == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PRICEREGIONVALUE", 1049));
            if (_valueMin == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PRICEREGIONVALUEMIN", 1049));
            if (_valueMax == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PRICEREGIONVALUEMAX", 1049));
            if (_priceNameId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PRICEREGIONPRICEID", 1049));
            if (_productId == 0)
                throw new ValidateException(Workarea.Cashe.ResourceString("EX_MSG_PRICEREGIONPODUCTID", 1049));
            if (DatabaseId == 0)
                DatabaseId = Workarea.MyBranche.Id;
        }
        #region Состояние
        PriceRegionStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new PriceRegionStruct
                                  {
                                      PriceNameId = _priceNameId,
                                      ProductId = _productId,
                                      Memo = _memo,
                                      Value = _value,
                                      ValueMin = _valueMin,
                                      ValueMax = _valueMax,
                                      DateStart = _dateStart,
                                      DateEnd = _dateEnd
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
            PriceNameId = _baseStruct.PriceNameId;
            Memo = _baseStruct.Memo;
            ProductId = _baseStruct.ProductId;
            Value = _baseStruct.Value;
            ValueMin = _baseStruct.ValueMin;
            ValueMax = _baseStruct.ValueMax;
            DateEnd = _baseStruct.DateEnd;
            DateStart = _baseStruct.DateStart;
            IsChanged = false;
        }
        #endregion
        #region База данных
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _priceNameId = reader.GetInt32(9);
                _productId = reader.GetInt32(10);
                _value = reader.GetDecimal(11);
                _valueMin = reader.GetDecimal(12);
                _valueMax = reader.GetDecimal(13);
                _dateStart = reader.GetDateTime(14);
                _dateEnd = reader.GetDateTime(15);
                _memo = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
            }
            catch (Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией обновления</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.PriceNameId, SqlDbType.Int) { IsNullable = true, Value = _priceNameId};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ProductId, SqlDbType.Int) { IsNullable = true, Value = _productId };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.Value, SqlDbType.Money) { IsNullable = true, Value = _value };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ValueMin, SqlDbType.Money) { IsNullable = true, Value = _valueMin };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ValueMax, SqlDbType.Money) { IsNullable = true, Value = _valueMax };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.DateStart, SqlDbType.Date) { IsNullable = true, Value = _dateStart };
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.DateEnd, SqlDbType.Date) { IsNullable = true, Value = _dateEnd };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar, -1) { IsNullable = true };
            if (string.IsNullOrEmpty(_memo))
                prm.Value = DBNull.Value;
            else
                prm.Value = _memo;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        /// <summary>
        /// Представление сущности в виде форматированной строки
        /// </summary>
        /// <param name="mask">Маска</param>
        /// <returns></returns>
        public virtual string ToString(string mask)
        {
            if (string.IsNullOrEmpty(mask))
            {
                return ToString();
            }
            string res = base.ToString(mask);
            // Макроподстановка для Value
            res = res.Replace("%value%", Value.ToString());
            // Макроподстановка для ProductId
            res = res.Replace("%productId%", ProductId.ToString());
            // Макроподстановка для ValueMin
            res = res.Replace("%valuemin%", ValueMin.ToString());
            // Макроподстановка для ValueMax
            res = res.Replace("%valuemax%", ValueMax.ToString());
            // Макроподстановка для PriceNameId
            res = res.Replace("%pricenameid%", PriceNameId.ToString());
            // Макроподстановка для DateEnd
            res = res.Replace("%dateend%", DateEnd.ToString());
            // Макроподстановка для DateStart
            res = res.Replace("%datestart%", DateStart.ToString());
            return res;
        }
        /// <summary>Представление объекта в виде строки</summary>
        public override string ToString()
        {
            return string.Format("Значение {0}; мин: {1}; макс: {2}", _value, _valueMin, _valueMax);
        }
    }
}