using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct FactColumnStruct
    {
        /// <summary>Идентификатор наименования</summary>
        public int FactNameId;
        /// <summary>Порядок сортировки</summary>
        public int OrderNo;
        /// <summary>Тип первой ссылки</summary>
        public int? ReferenceType;
        /// <summary>Тип второй ссылки</summary>
        public int? ReferenceType2;
        /// <summary>Тип третьей ссылки</summary>
        public int? ReferenceType3;
        /// <summary>Идентификатор корня первой ссылки</summary>
        public int? RootId;
        /// <summary>Идентификатор корня второй ссылки</summary>
        public int? RootId2;
        /// <summary>Идентификатор корня третьей ссылки</summary>
        public int? RootId3;
    }
    /// <summary>Колонка факта</summary>
    public class FactColumn : BaseCore<FactColumn>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Целое значение, соответствует значению 1</summary>
        public const int KINDVALUE_INT = 1;
        /// <summary>Строковое значение, соответствует значению 2</summary>
        public const int KINDVALUE_STRING = 2;
        /// <summary>Составное число и строка, соответствует значению 3</summary>
        public const int KINDVALUE_COMPOSITE = 3;
        /// <summary>Денежное значение, соответствует значению 4</summary>
        public const int KINDVALUE_MONEY = 4;
        /// <summary>Дата - время, соответствует значению 8</summary>
        public const int KINDVALUE_DATETIME = 8;
        /// <summary>Логика, соответствует значению 16</summary>
        public const int KINDVALUE_LOGIC = 16;
        /// <summary>XML данные, соответствует значению 32</summary>
        public const int KINDVALUE_XML = 32;
        /// <summary>Глобальный идентификатор, соответствует значению 64</summary>
        public const int KINDVALUE_GUID = 64;
        /// <summary>Вещественное значение, соответствует значению 128</summary>
        public const int KINDVALUE_REAL = 128;
        /// <summary>Двоичное значение, соответствует значению 256</summary>
        public const int KINDVALUE_BIN = 256;
        /// <summary>Первая ссылка, соответствует значению 512</summary>
        public const int KINDVALUE_FIRSTLINK = 512;
        /// <summary>Вторая ссылка, соответствует значению 1024</summary>
        public const int KINDVALUE_SECONDLINK = 1024;
        /// <summary>Третья ссылка, соответствует значению 2048</summary>
        public const int KINDVALUE_THIRDLING = 2048;

        /// <summary>Целое значение, соответствует значению 1179649</summary>
        public const int KINDID_INT = 1179649;
        /// <summary>Строковое значение, соответствует значению 1179650</summary>
        public const int KINDID_STRING = 1179650;
        /// <summary>Составное число и строка, соответствует значению 1179651</summary>
        public const int KINDID_COMPOSITE = 1179651;
        /// <summary>Денежное значение, соответствует значению 1179652</summary>
        public const int KINDID_MONEY = 1179652;
        /// <summary>Дата - время, соответствует значению 1179653</summary>
        public const int KINDID_DATETIME = 1179653;
        /// <summary>Логика, соответствует значению 1179664</summary>
        public const int KINDID_LOGIC = 1179664;
        /// <summary>XML данные, соответствует значению 1179660</summary>
        public const int KINDID_XML = 1179660;
        /// <summary>Глобальный идентификатор, соответствует значению 1179712</summary>
        public const int KINDID_GUID = 1179712;
        /// <summary>Вещественное значение, соответствует значению 1179776</summary>
        public const int KINDID_REAL = 1179776;
        /// <summary>Двоичное значение, соответствует значению 1179904</summary>
        public const int KINDID_BIN = 1179904;
        /// <summary>Первая ссылка, соответствует значению 1180160</summary>
        public const int KINDID_FIRSTLINK = 1180160;
        /// <summary>Вторая ссылка, соответствует значению 1180672</summary>
        public const int KINDID_SECONDLINK = 1180672;
        /// <summary>Третья ссылка, соответствует значению 1181696</summary>
        public const int KINDID_THIRDLING = 1181696;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>Конструктор</summary>
        public FactColumn(): base()
        {
            EntityId = (int)WhellKnownDbEntity.FactColumn;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override FactColumn Clone(bool endInit)
        {
            FactColumn obj = base.Clone(endInit);
            obj.FactNameId = FactNameId;
            obj.OrderNo = OrderNo;
            obj.ReferenceType = ReferenceType;
            obj.ReferenceType2 = ReferenceType2;
            obj.ReferenceType3 = ReferenceType3;
            obj.RootId = RootId;
            obj.RootId2 = RootId2;
            obj.RootId3 = RootId3;

            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private int _factNameId;
        /// <summary>Идентификатор наименования</summary>
        public int FactNameId
        {
            get { return _factNameId; }
            set 
            {
                if (value == _factNameId) return;
                OnPropertyChanging(GlobalPropertyNames.FactNameId);
                _factNameId = value;
                OnPropertyChanged(GlobalPropertyNames.FactNameId);
            }
        }

        private FactName _factName;
        /// <summary>Наименование факта</summary>
        public FactName FactName
        {
            get
            {
                if (_factNameId == 0)
                    return null;
                if (_factName == null)
                    _factName = Workarea.Cashe.GetCasheData<FactName>().Item(_factNameId);
                else if (_factName.Id != _factNameId)
                    _factName = Workarea.Cashe.GetCasheData<FactName>().Item(_factNameId);
                return _factName;
            }
            set
            {
                if (_factName == value) return;
                OnPropertyChanging(GlobalPropertyNames.FactName);
                _factName = value;
                _factNameId = _factName == null ? 0 : _factName.Id;
                OnPropertyChanged(GlobalPropertyNames.FactName);
            }
        } 

        private int _orderNo;
        /// <summary>Порядок сортировки</summary>
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
        private int? _referenceType;
        /// <summary>Тип первой ссылки</summary>
        public int? ReferenceType
        {
            get { return _referenceType; }
            set 
            {
                if (value == _referenceType) return;
                OnPropertyChanging(GlobalPropertyNames.ReferenceType);
                _referenceType = value;
                OnPropertyChanged(GlobalPropertyNames.ReferenceType);
            }
        }

        private EntityType _reference;
        /// <summary>Тип первой ссылки</summary>
        public EntityType Reference
        {
            get
            {
                if (!_referenceType.HasValue || _referenceType == 0)
                    return null;
                // ReSharper disable PossibleInvalidOperationException
                if (_reference == null)
                    _reference = Workarea.CollectionEntities.Find(f => f.Id == _referenceType.Value);
                else if (_reference.Id != _referenceType)
                    _reference = Workarea.CollectionEntities.Find(f => f.Id == _referenceType.Value);
                // ReSharper restore PossibleInvalidOperationException
                return _reference;
            }
            set
            {
                if (_reference == value) return;
                OnPropertyChanging(GlobalPropertyNames.Reference);
                _reference = value;
                _referenceType = _reference == null ? 0 : _reference.Id;
                OnPropertyChanged(GlobalPropertyNames.Reference);
            }
        }

        private int? _referenceType2;
        /// <summary>Тип второй ссылки</summary>
        public int? ReferenceType2
        {
            get { return _referenceType2; }
            set
            {
                if (value == _referenceType2) return;
                OnPropertyChanging(GlobalPropertyNames.ReferenceType2);
                _referenceType2 = value;
                OnPropertyChanged(GlobalPropertyNames.ReferenceType2);
            }
        }

        private EntityType _reference2;
        /// <summary>Тип первой ссылки</summary>
        public EntityType Reference2
        {
            get
            {
                if (!_referenceType2.HasValue || _referenceType2 == 0)
                    return null;
                if (_reference2 == null)
// ReSharper disable PossibleInvalidOperationException
                    _reference2 = Workarea.CollectionEntities.Find(f=>f.Id==_referenceType2.Value);
                else if (_reference2.Id != _referenceType2)
                    _reference2 = Workarea.CollectionEntities.Find(f => f.Id == _referenceType2.Value);
                // ReSharper restore PossibleInvalidOperationException
                return _reference2;
            }
            set
            {
                if (_reference2 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Reference2);
                _reference2 = value;
                _referenceType2 = _reference2 == null ? 0 : _reference2.Id;
                OnPropertyChanged(GlobalPropertyNames.Reference2);
            }
        }

        private int? _referenceType3;
        /// <summary>Тип третьей ссылки</summary>
        public int? ReferenceType3
        {
            get { return _referenceType3; }
            set
            {
                if (value == _referenceType3) return;
                OnPropertyChanging(GlobalPropertyNames.ReferenceType3);
                _referenceType3 = value;
                OnPropertyChanged(GlobalPropertyNames.ReferenceType3);
            }
        }

        private EntityType _reference3;
        /// <summary>Тип первой ссылки</summary>
        public EntityType Reference3
        {
            get
            {
                if (!_referenceType3.HasValue || _referenceType3 == 0)
                    return null;
                if (_reference3 == null)
// ReSharper disable PossibleInvalidOperationException
                    _reference3 = Workarea.CollectionEntities.Find(f=>f.Id==_referenceType3.Value);
                else if (_reference3.Id != _referenceType3)
                    _reference3 = Workarea.CollectionEntities.Find(f => f.Id == _referenceType3.Value);
// ReSharper restore PossibleInvalidOperationException
                return _reference3;
            }
            set
            {
                if (_reference3 == value) return;
                OnPropertyChanging(GlobalPropertyNames.Reference3);
                _reference3 = value;
                _referenceType3 = _reference3 == null ? 0 : _reference3.Id;
                OnPropertyChanged(GlobalPropertyNames.Reference3);
            }
        }
        private int? _rootId;
        /// <summary>Идентификатор корня первой ссылки</summary>
        public int? RootId
        {
            get { return _rootId; }
            set 
            {
                if (value == _rootId) return;
                OnPropertyChanging(GlobalPropertyNames.RootId);
                _rootId = value;
                OnPropertyChanged(GlobalPropertyNames.RootId);
            }
        }
        private int? _rootId2;
        /// <summary>Идентификатор корня второй ссылки</summary>
        public int? RootId2
        {
            get { return _rootId2; }
            set 
            {
                if (value == _rootId2) return;
                OnPropertyChanging(GlobalPropertyNames.RootId2);
                _rootId2 = value;
                OnPropertyChanged(GlobalPropertyNames.RootId2);
            }
        }

        private int? _rootId3;
        /// <summary>Идентификатор корня третьей ссылки</summary>
        public int? RootId3
        {
            get { return _rootId3; }
            set
            {
                if (value == _rootId3) return;
                OnPropertyChanging(GlobalPropertyNames.RootId3);
                _rootId3 = value;
                OnPropertyChanged(GlobalPropertyNames.RootId3);
            }
        }
        /// <summary>Разрешена ли третья ссылка</summary>
        public bool AllowValueRef3
        {
            get { return (KindId & 2048) == 2048; }
        }

        /// <summary>Разрешена ли вторая ссылка</summary>
        public bool AllowValueRef2
        {
            get { return (KindId & 1024) == 1024; }
        }

        /// <summary>Разрешена ли первая ссылка</summary>
        public bool AllowValueRef1
        {
            get { return (KindId & 512) == 512; }
        }
        /// <summary>Разрешено ли двоичное значение</summary>
        public bool AllowValueBinary
        {
            get { return (KindId & 256) == 256; }
        }
        /// <summary>Разрешено ли десятичное значение</summary>
        public bool AllowValueDecimal
        {
            get { return (KindId & 128) == 128; }
        }
        /// <summary>Разрешено ли значение глобального идентификатора</summary>
        public bool AllowValueGuid
        {
            get { return (KindId & 64) == 64; }
        }
        /// <summary>Разрешено ли значение XML</summary>
        public bool AllowValueXml
        {
            get { return (KindId & 32) == 32; }
        }
        /// <summary>Разрешено ли логическое значение</summary>
        public bool AllowValueBit
        {
            get { return (KindId & 16) == 16; }
        }
        /// <summary>Разрешено ли значение даты/времени</summary>
        public bool AllowValueDate
        {
            get { return (KindId & 8) == 8; }
        }
        /// <summary>Разрешено ли денежное значение</summary>
        public bool AllowValueMoney
        {
            get { return (KindId & 4) == 4; }
        }
        /// <summary>Разрешено ли строковое значение</summary>
        public bool AllowValueString
        {
            get { return (KindId & 2) == 2; }
        }
        /// <summary>Разрешено ли целое значение</summary>
        public bool AllowValueInt
        {
            get { return (KindId & 1) == 1; }
        }
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_factNameId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.FactNameId, XmlConvert.ToString(_factNameId));
            if (_orderNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
            if (_referenceType.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ReferenceType, XmlConvert.ToString(_referenceType.HasValue));
            if (_referenceType2.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ReferenceType2, XmlConvert.ToString(_referenceType2.HasValue));
            if (_referenceType3.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ReferenceType3, XmlConvert.ToString(_referenceType3.HasValue));
            if (_rootId.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.RootId, XmlConvert.ToString(_rootId.HasValue));
            if (_rootId2.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.RootId2, XmlConvert.ToString(_rootId2.HasValue));
            if (_rootId3.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.RootId3, XmlConvert.ToString(_rootId3.HasValue));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.FactNameId) != null)
                _factNameId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FactNameId));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OrderNo));
            if (reader.GetAttribute(GlobalPropertyNames.ReferenceType) != null)
                _referenceType = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ReferenceType));
            if (reader.GetAttribute(GlobalPropertyNames.ReferenceType2) != null)
                _referenceType2 = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ReferenceType2));
            if (reader.GetAttribute(GlobalPropertyNames.ReferenceType3) != null)
                _referenceType3 = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ReferenceType3));
            if (reader.GetAttribute(GlobalPropertyNames.RootId) != null)
                _rootId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RootId));
            if (reader.GetAttribute(GlobalPropertyNames.RootId2) != null)
                _rootId2 = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RootId2));
            if (reader.GetAttribute(GlobalPropertyNames.RootId3) != null)
                _rootId3 = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.RootId3));
        }
        #endregion

        #region База данных
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _factNameId = reader.GetInt32(17);
                _orderNo = reader.GetInt32(18);
                _referenceType = reader.IsDBNull(19) ? (int?) null : reader.GetInt32(19);
                _referenceType2 = reader.IsDBNull(20) ? (int?) null : reader.GetInt32(20);
                _referenceType3 = reader.IsDBNull(21) ? (int?) null : reader.GetInt32(21);
                _rootId = reader.IsDBNull(22) ? (int?) null : reader.GetInt32(22);
                _rootId2 = reader.IsDBNull(22) ? (int?) null : reader.GetInt32(22);
                _rootId3 = reader.IsDBNull(23) ? (int?) null : reader.GetInt32(23);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>
        /// Установить значения параметров для комманды создания
        /// </summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand"></param>
        /// <param name="validateVersion"></param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.FactNameId, SqlDbType.Int) {IsNullable = false, Value = _factNameId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.Int) {IsNullable = false, Value = _orderNo};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ReferenceType, SqlDbType.Int) {IsNullable = true};
            if (!_referenceType.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _referenceType;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ReferenceType2, SqlDbType.Int) {IsNullable = true};
            if (!_referenceType2.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _referenceType2;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ReferenceType3, SqlDbType.Int) {IsNullable = true};
            if (!_referenceType3.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _referenceType3;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.RootId, SqlDbType.Int) {IsNullable = true};
            if (!_rootId.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _rootId;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.RootId2, SqlDbType.Int) {IsNullable = true};
            if (!_rootId2.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _rootId2;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.RootId3, SqlDbType.Int) {IsNullable = true};
            if (!_rootId3.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _rootId3;
            sqlCmd.Parameters.Add(prm);
        }        
        #endregion

        #region Состояние
        FactColumnStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FactColumnStruct
                                  {
                                      FactNameId = _factNameId,
                                      ReferenceType = _referenceType,
                                      ReferenceType2 = _referenceType2,
                                      ReferenceType3 = _referenceType3,
                                      RootId = _rootId,
                                      RootId2 = _rootId2,
                                      RootId3 = _rootId3,
                                      OrderNo = _orderNo
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
            _factNameId = _baseStruct.FactNameId;
            _referenceType = _baseStruct.ReferenceType;
            _referenceType2 = _baseStruct.ReferenceType2;
            _referenceType3 = _baseStruct.ReferenceType3;
            _rootId = _baseStruct.RootId;
            _rootId2 = _baseStruct.RootId2;
            _rootId3 = _baseStruct.RootId3;
            _orderNo = _baseStruct.OrderNo;
            IsChanged = false;
        }
        #endregion
    }
}
