using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура объекта "Партия товара"</summary>
    internal struct SeriesStruct
    {
        /// <summary>Номер партии</summary>
        public string Number;
        /// <summary> Дата определяющая срок хранения "Годен до" </summary>
        public DateTime? DateOut;
        /// <summary>Дата изготовления</summary>
        public DateTime? DateOn;
        /// <summary>Дата прихода</summary>
        public DateTime DateIn;
        /// <summary>Идентификатор документа в котором была создана данная партия товара</summary>
        public int OwnId;
        /// <summary>Идентификатор товара</summary>
        public int ProductId;
    }

    /// <summary>Партия товара</summary>
    /// <remarks>Партии товара напрямую связаны с приходными документами в разделе "Склад".
    /// Важно: свойство Id данного класса соответствует идентификатору строки детализации приходного документа.
    /// </remarks>
    public sealed class Series : BaseCore<Series>, IEquatable<Series>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Производная единица, соответствует значению 1</summary>
        public const int KINDVALUE_SERIES = 1;

        /// <summary>Производная единица, соответствует значению 1966081</summary>
        public const int KINDID_SERIES = 1966081;
        // ReSharper restore InconsistentNaming
        #endregion

        bool IEquatable<Series>.Equals(Series other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public Series()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.Series;
        }
        protected override void CopyValue(Series template)
        {
            base.CopyValue(template);
            DateIn = template.DateIn;
            DateOut = template.DateOut;
            Number = template.Number;
            OwnId = template.OwnId;
            ProductId = template.ProductId;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Series Clone(bool endInit)
        {
            Series obj = base.Clone(false);
            obj.DateIn = DateIn;
            obj.DateOut = DateOut;
            obj.Number = Number;
            obj.OwnId = OwnId;
            obj.ProductId = ProductId;

            if (endInit)
                OnEndInit();
            return obj;
        }

        #region Свойства
        private string _number;
        /// <summary>Номер партии</summary>
        public string Number
        {
            get { return _number; }
            set
            {
                if (value == _number) return;
                OnPropertyChanging(GlobalPropertyNames.Number);
                _number = value;
                OnPropertyChanged(GlobalPropertyNames.Number);
            }
        }

        private DateTime? _dateOut;
        /// <summary> Дата определяющая срок хранения "Годен до" </summary>
        public DateTime? DateOut
        {
            get { return _dateOut; }
            set
            {
                if (value == _dateOut) return;
                OnPropertyChanging(GlobalPropertyNames.DateOut);
                _dateOut = value;
                OnPropertyChanged(GlobalPropertyNames.DateOut);
            }
        }


        private DateTime? _dateOn;
        /// <summary>Дата изготовления</summary>
        public DateTime? DateOn
        {
            get { return _dateOn; }
            set
            {
                if (value == _dateOn) return;
                OnPropertyChanging(GlobalPropertyNames.DateOn);
                _dateOn = value;
                OnPropertyChanged(GlobalPropertyNames.DateOn);
            }
        }

        /// <summary>Дата прихода</summary>
        public DateTime DateIn { get; private set; }

        private int _ownId;
        /// <summary>Идентификатор документа в котором была создана данная партия товара</summary>
        public int OwnId
        {
            get { return _ownId; }
            private set
            {
                if (value == _ownId) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
            }
        }

        private Documents.Document _document;
        /// <summary>
        /// Документ в котором была создана партия товара
        /// </summary>
        public Documents.Document Document
        {
            get { return _document ?? (_document = Workarea.Cashe.GetCasheData<Documents.Document>().Item(_ownId)); }
        }
        private int _productId;
        /// <summary>Идентификатор товара</summary>
        /// <remarks>Данные о товаре соответствуют данным из строки приходного документа и не могут быть изменены, 
        /// для изменения необходимо изменить приходный документ в разделе "Склад"</remarks>
        public int ProductId
        {
            get { return _productId; }
            private set
            {
                if (value == _productId) return;
                OnPropertyChanging(GlobalPropertyNames.ProductId);
                _productId = value;
                OnPropertyChanged(GlobalPropertyNames.ProductId);
            }
        }
        private Product _product;
        /// <summary>Товар</summary>
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

            if (!string.IsNullOrEmpty(_number))
                writer.WriteAttributeString(GlobalPropertyNames.Number, _number);
            if (_dateOut.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateOut, XmlConvert.ToString(_dateOut.Value));
            if (_dateOn.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.DateOn, XmlConvert.ToString(_dateOn.Value));
            //if (DateIn)
                writer.WriteAttributeString(GlobalPropertyNames.DateIn, XmlConvert.ToString(DateIn));
            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnerId, XmlConvert.ToString(_ownId));
            if (_productId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ProductId, XmlConvert.ToString(_productId));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.Number) != null)
                _number = reader[GlobalPropertyNames.Number];
            if (reader.GetAttribute(GlobalPropertyNames.DateOut) != null)
                _dateOut = XmlConvert.ToDateTime(reader[GlobalPropertyNames.DateOut]);
            if (reader.GetAttribute(GlobalPropertyNames.DateOn) != null)
                _dateOn = XmlConvert.ToDateTime(reader[GlobalPropertyNames.DateOn]);
            if (reader.GetAttribute(GlobalPropertyNames.DateIn) != null)
                DateIn = XmlConvert.ToDateTime(reader[GlobalPropertyNames.DateIn]);
            if (reader.GetAttribute(GlobalPropertyNames.OwnerId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnerId));
            if (reader.GetAttribute(GlobalPropertyNames.ProductId) != null)
                _productId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ProductId));
        }
        #endregion

        #region Состояние
        SeriesStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new SeriesStruct
                {
                    Number = _number,
                    DateOut = _dateOut,
                    DateOn = _dateOn,
                    DateIn = DateIn,
                    OwnId = _ownId,
                    ProductId = _productId
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
            Number = _baseStruct.Number;
            DateOut = _baseStruct.DateOut;
            DateOn = _baseStruct.DateOn;
            DateIn = _baseStruct.DateIn;
            OwnId = _baseStruct.OwnId;
            ProductId = _baseStruct.ProductId;

            IsChanged = false;
        }
        #endregion

        /// <summary>Проверка соответствия объекта бизнес правилам</summary>
        /// <remarks>Метод выполняет проверку наименования объекта <see cref="BaseCore{T}.Name"/> на предмет null, <see cref="string.Empty"/> и максимальную длину не более 255 символов</remarks>
        /// <returns><c>true</c> - если объект соответствует бизнес правилам, <c>false</c> в противном случае</returns>
        /// <exception cref="ValidateException">Если объект не соответствует текущим правилам</exception>
        public override void Validate()
        {
            base.Validate();

            if (_productId == 0)
                throw new ValidateException("Не указан товар");
            if (_ownId==0)
                throw new ValidateException("Не указан идентификатор документа");
            if (Id == 0)
                throw new ValidateException("Не указан идентификатор строки документа");
        }
        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _number = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                _dateOut = reader.IsDBNull(18) ? (DateTime?)null : reader.GetDateTime(18);
                _ownId = reader.GetInt32(19);
                _productId = reader.GetInt32(20);
                _dateOn = reader.IsDBNull(21) ? (DateTime?)null : reader.GetDateTime(21);
                DateIn = reader.GetDateTime(22);
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
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять ли проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Number, SqlDbType.NVarChar, 50) { IsNullable = true };
            if (string.IsNullOrWhiteSpace(_number))
                prm.Value = DBNull.Value;
            else
                prm.Value = _number;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateOut, SqlDbType.Date) { IsNullable = true };
            if (!_dateOut.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateOut;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OwnId, SqlDbType.Int) {IsNullable = false, Value = _ownId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ProductId, SqlDbType.Int) {IsNullable = false, Value = _productId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.DateOn, SqlDbType.Date) { IsNullable = true };
            if (!_dateOn.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _dateOn;
            sqlCmd.Parameters.Add(prm);
        }
        #endregion
        
    }
}