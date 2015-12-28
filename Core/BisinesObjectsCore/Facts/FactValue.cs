using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;

namespace BusinessObjects
{
    internal struct FactValueStruct
    {
        /// <summary>Дата актуальности</summary>
        public DateTime ActualDate;
        /// <summary>Признак</summary>
        public string Code;
        /// <summary>Идентификатор даты актуальности</summary>
        public int FactDateId;
        /// <summary>Примечание</summary>
        public string Memo;
        /// <summary>Двоичное значение</summary>
        public byte[] ValueBinary;
        /// <summary>Логическое значение</summary>
        public bool? ValueBit;
        /// <summary>Значение даты/времени</summary>
        public DateTime? ValueDate;
        /// <summary>Десятичное значение</summary>
        public float? ValueDecimal;
        /// <summary>Значение глобального идентификатора</summary>
        public Guid? ValueGuid;
        /// <summary>Числовое значение</summary>
        public int? ValueInt;
        /// <summary>Денежное значение</summary>
        public decimal? ValueMoney;
        /// <summary>Ссылка 1</summary>
        public int? ValueRef1;
        /// <summary>Ссылка 2</summary>
        public int? ValueRef2;
        /// <summary>Ссылка 3</summary>
        public int? ValueRef3;
        /// <summary>Строковое значение</summary>
        public string ValueString;
        /// <summary>Значение XML</summary>
        public string ValueXml;
    }
    /// <summary>Значение факта</summary>
    public sealed class FactValue : BaseCoreObject//BaseCore<FactValue>
    {
        /// <summary>Конструктор</summary>
        public FactValue():base()
        {
            EntityId = (short)WhellKnownDbEntity.FactValue;
        }
        #region Свойства

        private string _code;

        /// <summary>
        /// Признак
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                OnPropertyChanging(GlobalPropertyNames.Code);
                _code = value;
                OnPropertyChanged(GlobalPropertyNames.Code);
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
                if (_memo == value) return;
                OnPropertyChanging(GlobalPropertyNames.Memo);
                _memo = value;
                OnPropertyChanged(GlobalPropertyNames.Memo);
            }
        }


        private int _columnId;
        public int ColumnId
        {
            get
            {
                if (Id!=0)
                {
                    _columnId = FactDate.ColumnId;
                }
                return _columnId;
            }
            internal set
            {
                if (value == _columnId) return;
                OnPropertyChanging(GlobalPropertyNames.ColumnId);
                _columnId = value;
                OnPropertyChanged(GlobalPropertyNames.ColumnId);
            }
        }
        private FactColumn _factColumn;
        /// <summary>Колонка факта</summary>
        public FactColumn FactColumn
        {
            get
            {
                if (ColumnId == 0)
                    return null;
                if (_factColumn == null)
                    _factColumn = Workarea.Cashe.GetCasheData<FactColumn>().Item(ColumnId);
                else if (_factColumn.Id != ColumnId)
                    _factColumn = Workarea.Cashe.GetCasheData<FactColumn>().Item(ColumnId);
                return _factColumn;
            }
            internal set
            {
                if (_factColumn == value) return;
                OnPropertyChanging(GlobalPropertyNames.FactColumn);
                _factColumn = value;
                _columnId = _factColumn == null ? 0 : _factColumn.Id;
                OnPropertyChanged(GlobalPropertyNames.FactColumn);
            }
        }

        private int _elementId;
        /// <summary>Идентификатор сущности</summary>
        public int ElementId
        {
            get
            {
                if (Id != 0)
                    _elementId = FactDate.ElementId;
                return _elementId;
            }
            internal set
            {
                if (value == _elementId) return;
                OnPropertyChanging(GlobalPropertyNames.ElementId);
                _elementId = value;
                OnPropertyChanged(GlobalPropertyNames.ElementId);
            }
        }

        private int _toEntityId;
        /// <summary>Тип сущности</summary>
        public int ToEntityId
        {
            get
            {
                if (Id != 0)
                    _toEntityId = FactDate.ToEntityId;
                return _toEntityId;
            }
            internal set
            {
                if (value == _toEntityId) return;
                OnPropertyChanging(GlobalPropertyNames.ToEntityId);
                _toEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ToEntityId);
            }
        }

        private FactDate _factDate;
        /// <summary>Дата факта</summary>
        public FactDate FactDate
        {
            get
            {
                if (IsNew)
                    return _factDate;
                if (_factDateId == 0)
                    return null;
                if (_factDate == null)
                {
                    _factDate = new FactDate{Workarea = Workarea};
                    _factDate.Load(_factDateId);
                }
                else if (_factDate.Id != _factDateId)
                {
                    _factDate = new FactDate { Workarea = Workarea };
                    _factDate.Load(_factDateId);
                }
                    
                return _factDate;
            }
            set
            {
                if (_factDate == value) return;
                OnPropertyChanging(GlobalPropertyNames.FactDate);
                _factDate = value;
                _factDateId = _factDate == null ? 0 : _factDate.Id;
                OnPropertyChanged(GlobalPropertyNames.FactDate);
            }
        }
        /// <summary>Дата актуальности</summary>
        public DateTime ActualDate
        {
            get
            {
                return FactDate != null ? _factDate.ActualDate : DateTime.MinValue;
            }
        }
	
        private int _factDateId;
        /// <summary>Идентификатор даты актуальности</summary>
        public int FactDateId
        {
            get { return _factDateId; }
            set 
            {
                if (value == _factDateId) return;
                OnPropertyChanging(GlobalPropertyNames.FactDateId);
                _factDateId = value;
                OnPropertyChanged(GlobalPropertyNames.FactDateId);
            }
        }

        private int? _valueInt;
        /// <summary>Числовое значение</summary>
        public int? ValueInt
        {
            get { return _valueInt; }
            set 
            {
                if (value == _valueInt) return;
                OnPropertyChanging(GlobalPropertyNames.ValueInt);
                _valueInt = value;
                OnPropertyChanged(GlobalPropertyNames.ValueInt);
            }
        }

        private int? _valueRef1;
        /// <summary>Ссылка 1</summary>
        public int? ValueRef1
        {
            get { return _valueRef1; }
            set
            {
                if (value == _valueRef1) return;
                OnPropertyChanging(GlobalPropertyNames.ValueRef1);
                _valueRef1 = value;
                OnPropertyChanged(GlobalPropertyNames.ValueRef1);
            }
        }
        private int? _valueRef2;
        /// <summary>Ссылка 2</summary>
        public int? ValueRef2
        {
            get { return _valueRef2; }
            set
            {
                if (value == _valueRef2) return;
                OnPropertyChanging(GlobalPropertyNames.ValueRef2);
                _valueRef2 = value;
                OnPropertyChanged(GlobalPropertyNames.ValueRef2);
            }
        }
        private int? _valueRef3;
        /// <summary>Ссылка 3</summary>
        public int? ValueRef3
        {
            get { return _valueRef3; }
            set
            {
                if (value == _valueRef3) return;
                OnPropertyChanging(GlobalPropertyNames.ValueRef3);
                _valueRef3 = value;
                OnPropertyChanged(GlobalPropertyNames.ValueRef3);
            }
        }
        private string _valueString;
        /// <summary>Строковое значение</summary>
        public string ValueString
        {
            get { return _valueString; }
            set 
            {
                if (value == _valueString) return;
                OnPropertyChanging(GlobalPropertyNames.ValueString);
                _valueString = value;
                OnPropertyChanged(GlobalPropertyNames.ValueString);
            }
        }

        private decimal? _valueMoney;
        /// <summary>Денежное значение</summary>
        public decimal? ValueMoney
        {
            get { return _valueMoney; }
            set 
            {
                if (value == _valueMoney) return;
                OnPropertyChanging(GlobalPropertyNames.ValueMoney);
                _valueMoney = value;
                OnPropertyChanged(GlobalPropertyNames.ValueMoney);
            }
        }

        private DateTime? _valueDate;
        /// <summary>Значение даты/времени</summary>
        public DateTime? ValueDate
        {
            get { return _valueDate; }
            set 
            {
                if (value == _valueDate) return;
                OnPropertyChanging(GlobalPropertyNames.ValueDate);
                _valueDate = value;
                OnPropertyChanged(GlobalPropertyNames.ValueDate);
            }
        }

        private bool? _valueBit;
        /// <summary>Логическое значение</summary>
        public bool? ValueBit
        {
            get { return _valueBit; }
            set 
            {
                if (value == _valueBit) return;
                OnPropertyChanging(GlobalPropertyNames.ValueBit);
                _valueBit = value;
                OnPropertyChanged(GlobalPropertyNames.ValueBit);
            }
        }

        private string _valueXml;
        /// <summary>Значение XML</summary>
        public string ValueXml
        {
            get { return _valueXml; }
            set 
            {
                if (value == _valueXml) return;
                OnPropertyChanging(GlobalPropertyNames.ValueXml);
                _valueXml = value;
                OnPropertyChanged(GlobalPropertyNames.ValueXml);
            }
        }

        private Guid? _valueGuid;
        /// <summary>Значение глобального идентификатора</summary>
        public Guid? ValueGuid
        {
            get { return _valueGuid; }
            set 
            {
                if (value == _valueGuid) return;
                OnPropertyChanging(GlobalPropertyNames.ValueGuid);
                _valueGuid = value;
                OnPropertyChanged(GlobalPropertyNames.ValueGuid);
            }
        }

        private float? _valueDecimal;
        /// <summary>Десятичное значение</summary>
        public float? ValueDecimal
        {
            get { return _valueDecimal; }
            set 
            {
                if (value == _valueDecimal) return;
                OnPropertyChanging(GlobalPropertyNames.ValueDecimal);
                _valueDecimal = value;
                OnPropertyChanged(GlobalPropertyNames.ValueDecimal);
            }
        }

        private byte[] _valueBinary;
        /// <summary>Двоичное значение</summary>
        public byte[] ValueBinary
        {
            get { return _valueBinary; }
            set 
            {
                OnPropertyChanging(GlobalPropertyNames.ValueBinary);
                _valueBinary = value;
                OnPropertyChanged(GlobalPropertyNames.ValueBinary);
            }
        }
        /// <summary>Строковое представление</summary>
        /// <returns></returns>
        public override string ToString()
        {
            string value = string.Empty;
            if (_factDate != null && _factDate.FactColumn!=null)
            {
                if (_factDate.FactColumn.AllowValueBit)
                    value = string.Format("Логика: {0}",_valueBit);
                if (_factDate.FactColumn.AllowValueInt)
                {
                    if (value.Length > 0)
                        value = value + ";";
                    value = string.Format("Целое: {0}", _valueInt);
                }
                if (_factDate.FactColumn.AllowValueString)
                {
                    if (value.Length > 0)
                        value = value + ";";
                    value = string.Format("Строка: {0}", _valueString);
                }
            }
            return value;
        }
	
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            //if (ActualDate)
                writer.WriteAttributeString(GlobalPropertyNames.ActualDate, XmlConvert.ToString(ActualDate));
            if (!string.IsNullOrEmpty(_code))
                writer.WriteAttributeString(GlobalPropertyNames.Code, _code);
            if (_factDateId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.FactDateId, XmlConvert.ToString(_factDateId));
            if (!string.IsNullOrEmpty(_memo))
                writer.WriteAttributeString(GlobalPropertyNames.Memo, _memo);
            if (_valueBinary != null)
                writer.WriteAttributeString(GlobalPropertyNames.ValueBinary, Convert.ToBase64String(_valueBinary));
            if (_valueBit.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueBit, XmlConvert.ToString(_valueBit.Value));
            if (_valueDate.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueDate, XmlConvert.ToString(_valueDate.Value));
            if (_valueDecimal.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueDecimal, XmlConvert.ToString(_valueDecimal.Value));
            if (_valueGuid.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueGuid, XmlConvert.ToString(_valueGuid.Value));
            if (_valueInt.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueInt, XmlConvert.ToString(_valueInt.Value));
            if (_valueMoney.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueMoney, XmlConvert.ToString(_valueMoney.Value));
            if (_valueRef1.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueRef1, XmlConvert.ToString(_valueRef1.Value));
            if (_valueRef2.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueRef2, XmlConvert.ToString(_valueRef2.Value));
            if (_valueRef3.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueRef3, XmlConvert.ToString(_valueRef3.Value));
            if (!string.IsNullOrEmpty(_valueString))
                writer.WriteAttributeString(GlobalPropertyNames.ValueString, _valueString);
            if (!string.IsNullOrEmpty(_valueXml))
                writer.WriteAttributeString(GlobalPropertyNames.ValueXml, _valueXml);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            /*if (reader.GetAttribute(GlobalPropertyNames.ActualDate) != null)
                ActualDate = reader.GetAttribute(GlobalPropertyNames.ActualDate);*/
            if (reader.GetAttribute(GlobalPropertyNames.Code) != null)
                _code = reader.GetAttribute(GlobalPropertyNames.Code);
            if (reader.GetAttribute(GlobalPropertyNames.FactDateId) != null)
                _factDateId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.FactDateId));
            if (reader.GetAttribute(GlobalPropertyNames.Memo) != null)
                _memo = reader.GetAttribute(GlobalPropertyNames.Memo);
            if (reader.GetAttribute(GlobalPropertyNames.ValueBinary) != null)
                _valueBinary =Convert.FromBase64String(reader.GetAttribute(GlobalPropertyNames.ValueBinary));
            if (reader.GetAttribute(GlobalPropertyNames.ValueBit) != null)
                _valueBit =Convert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.ValueBit));
            if (reader.GetAttribute(GlobalPropertyNames.ValueDate) != null)
                _valueDate = XmlConvert.ToDateTime(reader.GetAttribute(GlobalPropertyNames.ValueDate));
            if (reader.GetAttribute(GlobalPropertyNames.ValueDecimal) != null)
                _valueDecimal =float.Parse(reader.GetAttribute(GlobalPropertyNames.ValueDecimal));
            if (reader.GetAttribute(GlobalPropertyNames.ValueGuid) != null)
                _valueGuid = XmlConvert.ToGuid(reader.GetAttribute(GlobalPropertyNames.ValueGuid));
            if (reader.GetAttribute(GlobalPropertyNames.ValueInt) != null)
                _valueInt = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ValueInt));
            if (reader.GetAttribute(GlobalPropertyNames.ValueMoney) != null)
                _valueMoney = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.ValueMoney));
            if (reader.GetAttribute(GlobalPropertyNames.ValueRef1) != null)
                _valueRef1 = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ValueRef1));
            if (reader.GetAttribute(GlobalPropertyNames.ValueRef2) != null)
                _valueRef2 = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ValueRef2));
            if (reader.GetAttribute(GlobalPropertyNames.ValueRef3) != null)
                _valueRef3 = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ValueRef3));
            if (reader.GetAttribute(GlobalPropertyNames.ValueString) != null)
                _valueString = reader.GetAttribute(GlobalPropertyNames.ValueString);
            if (reader.GetAttribute(GlobalPropertyNames.ValueXml) != null)
                _valueXml = reader.GetAttribute(GlobalPropertyNames.ValueXml);
        }
        #endregion

        #region Состояние
        FactValueStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FactValueStruct
                {
                    ActualDate = ActualDate,
                    Code = _code,
                    FactDateId = _factDateId,
                    Memo = _memo,
                    ValueBinary = _valueBinary,
                    ValueBit = _valueBit,
                    ValueDate = _valueDate,
                    ValueDecimal = _valueDecimal,
                    ValueGuid = _valueGuid,
                    ValueInt = _valueInt,
                    ValueMoney = _valueMoney,
                    ValueRef1 = _valueRef1,
                    ValueRef2 = _valueRef2,
                    ValueRef3 = _valueRef3,
                    ValueString = _valueString,
                    ValueXml = _valueXml,
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            _factDate.ActualDate = _baseStruct.ActualDate;
            Code = _baseStruct.Code;
            FactDateId = _baseStruct.FactDateId;
            Memo = _baseStruct.Memo;
            ValueBinary = _baseStruct.ValueBinary;
            ValueBit = _baseStruct.ValueBit;
            ValueDate = _baseStruct.ValueDate;
            ValueDecimal = _baseStruct.ValueDecimal;
            ValueGuid = _baseStruct.ValueGuid;
            ValueInt = _baseStruct.ValueInt;
            ValueMoney = _baseStruct.ValueMoney;
            ValueRef1 = _baseStruct.ValueRef1;
            ValueRef2 = _baseStruct.ValueRef2;
            ValueRef3 = _baseStruct.ValueRef3;
            ValueString = _baseStruct.ValueString;
            ValueXml = _baseStruct.ValueXml;

            IsChanged = false;
        }
        #endregion
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        public override void Validate()
        {
            if (_factDate != null && _factDate.Id != _factDateId)
                _factDateId = _factDate.Id;
            base.Validate();
            if (!string.IsNullOrEmpty(Code) && Code.Length > 50)
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_NAMELENTH", 1049));
        }
        #region База данных
        //public void Save()
        //{
        //    Validate();
        //    if (IsNew)
        //        Create(Workarea.Empty<FactName>().Entity.FindMethod("FactValueInsert").FullName);
        //    else
        //        Update(Workarea.Empty<FactName>().Entity.FindMethod("FactValueUpdate").FullName, true);
        //}

        //public void Delete()
        //{
        //    throw new NotImplementedException();
        //}
        //public void Remove()
        //{
        //    // TODO: Срочной!!! добавить реализацию
        //    throw new NotImplementedException();
        //}
        //public override void Load(int value)
        //{
        //    Load(value, "[Fact].[FactValueLoad]");
        //}
        /// <summary>Загрузка</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                Code = reader.IsDBNull(9) ? null : reader.GetString(9);
                Memo = reader.IsDBNull(10) ? null : reader.GetString(10);
                _factDateId = reader.GetInt32(11);
                _valueInt = reader.IsDBNull(12) ? (int?) null : reader.GetInt32(12);
                _valueString = reader.IsDBNull(13) ? null : reader.GetString(13);
                _valueMoney = reader.IsDBNull(14) ? (decimal?) null : reader.GetDecimal(14);
                _valueDate = reader.IsDBNull(15) ? (DateTime?) null : reader.GetDateTime(15);
                _valueBit = reader.IsDBNull(16) ? (bool?) null : reader.GetBoolean(16);
                _valueXml = reader.IsDBNull(17) ? null : reader.GetString(17);
                _valueGuid = reader.IsDBNull(18) ? (Guid?) null : reader.GetGuid(18);
                _valueDecimal = reader.IsDBNull(19) ? (float?) null : reader.GetFloat(19);
                _valueBinary = !reader.IsDBNull(20) ? reader.GetSqlBinary(20).Value : null;
                _valueRef1 = reader.IsDBNull(21) ? (int?)null : reader.GetInt32(21);
                _valueRef2 = reader.IsDBNull(22) ? (int?)null : reader.GetInt32(22);
                _valueRef3 = reader.IsDBNull(23) ? (int?)null : reader.GetInt32(23);
            }
            catch(Exception ex)
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.Code, SqlDbType.NVarChar, 100) {IsNullable = true};
            if (string.IsNullOrEmpty(Code))
                prm.Value = DBNull.Value;
            else
                prm.Value = Code;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Memo, SqlDbType.NVarChar) {IsNullable = true};
            if (string.IsNullOrEmpty(Memo))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = Memo.Length;
                prm.Value = Memo;
            }

            prm = new SqlParameter(GlobalSqlParamNames.FactDateId, SqlDbType.Int) {IsNullable = false, Value = _factDateId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ValueInt, SqlDbType.Int) { IsNullable = true };
            if (!_valueInt.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueInt;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueString, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_valueString))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _valueString.Length;
                prm.Value = _valueString;
            }
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueMoney, SqlDbType.Money) { IsNullable = true };
            if (!_valueMoney.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueMoney;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueDate, SqlDbType.DateTime) {IsNullable = true};
            if (!_valueDate.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueDate;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueBit, SqlDbType.Bit) {IsNullable = true};
            if (!_valueBit.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueBit;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueXml, SqlDbType.Xml) {IsNullable = true};
            if (string.IsNullOrEmpty(_valueXml))
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueXml;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ValueBinary, SqlDbType.VarBinary) { IsNullable = true };
            if(_valueBinary != null)
            {
                if(_valueBinary.All(v => v == 0))
                    prm.Value = DBNull.Value;
                else
                    prm.Value = _valueBinary;
            }
            else
                prm.Value = DBNull.Value;                
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ValueRef1, SqlDbType.Int) { IsNullable = true };
            // TODO:!_valueRef1.HasValue || _valueRef1.Value==0
            if (!_valueRef1.HasValue || (_valueRef1.HasValue && _valueRef1.Value==0))
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueRef1;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ValueRef2, SqlDbType.Int) { IsNullable = true };
            if (!_valueRef2.HasValue || (_valueRef2.HasValue && _valueRef2.Value == 0))
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueRef2;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ValueRef3, SqlDbType.Int) { IsNullable = true };
            if (!_valueRef3.HasValue || (_valueRef3.HasValue && _valueRef3.Value == 0))
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueRef3;
            sqlCmd.Parameters.Add(prm);
        }

        
        #endregion
    }
}
