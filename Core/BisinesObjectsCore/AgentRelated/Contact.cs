using System;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct ContactStruct
    {
        /// <summary>Идентификатор корреспондента</summary>
        public int OwnId;
        /// <summary>Номер в группе</summary>
        public int OrderNo;
    }
    /// <summary>Контактная информация</summary>
    public class Contact : BaseCore<Contact>, IRelationMany
    {
        /// <summary>Текущее расположение, соответствует значению 1</summary>
        public const int KINDVALUE_LOCATIONCURRENT = 1;
        /// <summary>Фактическое расположение, соответствует значению 2</summary>
        public const int KINDVALUE_LOCATIONFACT = 2;
        /// <summary>WWW, соответствует значению 3</summary>
        public const int KINDVALUE_WWW = 3;
        /// <summary>EMAIL, соответствует значению 4</summary>
        public const int KINDVALUE_EMAIL = 4;
        /// <summary>Рабочий телефон, соответствует значению 5</summary>
        public const int KINDVALUE_PHONEWORK = 5;
        /// <summary>Мобильный, соответствует значению 6</summary>
        public const int KINDVALUE_PHONEMOBILE = 6;
        /// <summary>Домашний телефон, соответствует значению 7</summary>
        public const int KINDVALUE_PHONEHOME = 7;
        /// <summary>Телекс, соответствует значению 8</summary>
        public const int KINDVALUE_TELEX = 8;
        /// <summary>
        /// Конструктор
        /// </summary>
        public Contact(): base()
        {
            EntityId = (short)WhellKnownDbEntity.Contact;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override Contact Clone(bool endInit)
        {
            Contact obj = base.Clone(endInit);
            obj.OrderNo = OrderNo;
            obj.OwnId = OwnId;
            
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private int _ownId;
        /// <summary>
        /// Идентификатор корреспондента
        /// </summary>
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                if (_ownId == value) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
            }
        }

        private int _orderNo;
        /// <summary>
        /// Номер в группе
        /// </summary>
        public int OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (value == _orderNo) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            }
        }
        
        
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
        }
        #endregion

        #region Состояние
        ContactStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new ContactStruct
                {
                    OwnId = _ownId,
                    OrderNo = _orderNo
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            OwnId = _baseStruct.OwnId;
            OrderNo = _baseStruct.OrderNo;
            IsChanged = false;
        }
        #endregion
        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _ownId = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
                _orderNo = reader.GetInt32(18);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (endInit)
                OnEndInit();
        }
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.OwnId, System.Data.SqlDbType.Int) { Value = _ownId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, System.Data.SqlDbType.Int) { Value = _orderNo };
            sqlCmd.Parameters.Add(prm);
        }

        #region IRelationSingle Members

        string IRelationSingle.Schema
        {
            get { return GlobalSchemaNames.Contractor; }
        }

        #endregion
    }
}
