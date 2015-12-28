using System;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    
    /// <summary>Описание свойства</summary>
    public class CustomPropertyDescriptor : BaseCore<CustomPropertyDescriptor>
    {
        // TODO: Реализация!!!
        /// <summary>Конструктор</summary>
        public CustomPropertyDescriptor(): base()
        {
            
        }

        #region Свойства
        private Int16 _dbTypeSource;
        public Int16 DbTypeSource
        {
            get { return _dbTypeSource; }
            set
            {
                if (_dbTypeSource == value) return;
                OnPropertyChanging(GlobalPropertyNames.DbTypeSource);
                _dbTypeSource = value;
                OnPropertyChanged(GlobalPropertyNames.DbTypeSource);
            }
        }
	
        /// <summary>Тип</summary>
        public Type Type { get; set; }
        private string _dataField;
        /// <summary>Наименование столбца в базе данных</summary>
        public string DataField
        {
            get { return _dataField; }
            set
            {
                if (_dataField == value) return;
                OnPropertyChanging(GlobalPropertyNames.DataField);
                _dataField = value;
                OnPropertyChanged(GlobalPropertyNames.DataField);
            }
        }
        private string _dataBaseType;
        /// <summary>Тип в базе данных</summary>
        public string DatabaseType
        {
            get { return _dataBaseType; }
            set
            {
                if (_dataBaseType == value) return;
                OnPropertyChanging(GlobalPropertyNames.DatabaseType);
                _dataBaseType = value;
                OnPropertyChanged(GlobalPropertyNames.DatabaseType);
            }
        }
        private int _size;
        /// <summary>Максимальный размер для строковых данных</summary>
        public int Size
        {
            get { return _size; }
            set
            {
                if (_size == value) return;
                OnPropertyChanging(GlobalPropertyNames.Size);
                _size = value;
                OnPropertyChanged(GlobalPropertyNames.Size);
            }
        }
        private bool _allowNull;
        /// <summary>Разрешено ли принимать значение NULL</summary>
        public bool AllowNull
        {
            get { return _allowNull; }
            set
            {
                if (_allowNull == value) return;
                OnPropertyChanging(GlobalPropertyNames.AllowNull);
                _allowNull = value;
                OnPropertyChanged(GlobalPropertyNames.AllowNull);
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

            if (!string.IsNullOrEmpty(_dataField))
                writer.WriteAttributeString(GlobalPropertyNames.DataField, _dataField);
            if (!string.IsNullOrEmpty(_dataBaseType))
                writer.WriteAttributeString(GlobalPropertyNames.DatabaseType, _dataBaseType);
            if (_size != 0)
                writer.WriteAttributeString(GlobalPropertyNames.Size, XmlConvert.ToString(_size));
            if (_allowNull)
                writer.WriteAttributeString(GlobalPropertyNames.AllowNull, XmlConvert.ToString(_allowNull));
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.DataField) != null)
                _dataField = reader.GetAttribute(GlobalPropertyNames.DataField);
            if (reader.GetAttribute(GlobalPropertyNames.DatabaseType) != null)
                _dataBaseType = reader.GetAttribute(GlobalPropertyNames.DatabaseType);
            if (reader.GetAttribute(GlobalPropertyNames.Size) != null)
                _size = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.Size));
            if (reader.GetAttribute(GlobalPropertyNames.AllowNull) != null)
                _allowNull = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.AllowNull));
        }
        #endregion

        /// <summary>Загрузить экземпляр из базы данных по его идентификатору</summary>
        /// <remarks>Метод использует хранимую процедуру указанную в методах объекта по ключу "Load"</remarks>
        /// <param name="value">Идентификатор</param>
        public override void Load(int value)
        {
            // TODO: Срочно !!! Добавить реализацию
            throw new NotImplementedException();
        }

        /// <summary>Загрузить экземпляр из базы данных</summary>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _dataField = reader.GetString(17);
                _dataBaseType = reader.GetString(18);
                _size = reader.GetInt32(19);
                _allowNull = reader.GetBoolean(20);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
    }
}