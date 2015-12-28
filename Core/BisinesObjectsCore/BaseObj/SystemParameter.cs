using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using BusinessObjects.Security;

namespace BusinessObjects
{
    internal struct SystemParameterStruct
    {
        /// <summary>Двоичное значение</summary>
        public byte[] ValueBinary;
        /// <summary>Вещественное значение</summary>
        public float? ValueFloat;
        /// <summary>Значение глобального идентификатора</summary>
        public Guid? ValueGuid;
        /// <summary>Числовое значение</summary>
        public int? ValueInt;
        /// <summary>Денежное значение</summary>
        public decimal? ValueMoney;
        /// <summary>Строковое значение</summary>
        public string ValueString;
    }
    // TODO: Полностью пересмотреть...
    /// <summary>Системный параметр</summary>
    public sealed class SystemParameter : BaseCore<SystemParameter>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Целое значение, соответствует значению 1</summary>
        public const int KINDVALUE_INT = 1;
        /// <summary>Строковое значение, соответствует значению 2</summary>
        public const int KINDVALUE_STRING = 2;
        /// <summary>Составное - число и строка, соответствует значению 3</summary>
        public const int KINDVALUE_COMPOUND = 3;
        /// <summary>Денежное значение, соответствует значению 4</summary>
        public const int KINDVALUE_MONEY = 4;
        /// <summary>Вещественное значение, соответствует значению 8</summary>
        public const int KINDVALUE_REAL = 8;
        /// <summary>Двочное значение, соответствует значению 16</summary>
        public const int KINDVALUE_BIN = 16;
        /// <summary>Глобальный идентификатор, соответствует значению 32</summary>
        public const int KINDVALUE_GUID = 32;
        /// <summary>Ссылка на список, соответствует значению 64</summary>
        public const int KINDVALUE_LINK = 64;

        /// <summary>Целое значение, соответствует значению 1638401</summary>
        public const int KINDID_INT = 1638401;
        /// <summary>Строковое значение, соответствует значению 1638402</summary>
        public const int KINDID_STRING = 1638402;
        /// <summary>Составное - число и строка, соответствует значению 1638403</summary>
        public const int KINDID_COMPOUND = 1638403;
        /// <summary>Денежное значение, соответствует значению 1638404</summary>
        public const int KINDID_MONEY = 1638404;
        /// <summary>Вещественное значение, соответствует значению 1638408</summary>
        public const int KINDID_REAL = 1638408;
        /// <summary>Двочное значение, соответствует значению 1638416</summary>
        public const int KINDID_BIN = 1638416;
        /// <summary>Глобальный идентификатор, соответствует значению 1638432</summary>
        public const int KINDID_GUID = 1638432;
        /// <summary>Ссылка на список, соответствует значению 1638464</summary>
        public const int KINDID_LINK = 1638464;
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>
        /// Разрешить свободную регистрацию предприятий
        /// </summary>
        public const string WEBALLOWCOMPANYREGISTER = "SYSTEMPARAMETER_WEBALLOWCOMPANYREGISTER";
        /// <summary>
        /// Корневой каталог Web приложения
        /// </summary>
        public const string WEBROOTSERVER = "WEBROOTSERVER";
        
        /// <summary>Конструктор</summary>
        public SystemParameter(): base()
        {
            EntityId = 25;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override SystemParameter Clone(bool endInit)
        {
            SystemParameter obj = base.Clone(false);
            obj.ValueBinary = ValueBinary;
            obj.ValueFloat = ValueFloat;
            obj.ValueGuid = ValueGuid;
            obj.ValueInt = ValueInt;
            obj.ValueMoney = ValueMoney;
            obj.ValueString = ValueString;
            
            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства

        private int _entityReferenceId;
        /// <summary>
        /// Идентификатор системного типа ссылки
        /// </summary>
        public int EntityReferenceId
        {
            get { return _entityReferenceId; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.EntityReferenceId);
                _entityReferenceId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityReferenceId);
            }
        }
        private EntityType _entityReference;
        /// <summary>
        /// Системный тип ссылки
        /// </summary>
        public EntityType EntityReference
        {
            get
            {
                if (_entityReferenceId == 0)
                    return null;
                if (_entityReference == null)
                    _entityReference = Workarea.CollectionEntities.Find(f => f.Id == _entityReferenceId);
                else if (_entityReference.Id != _entityReferenceId)
                    _entityReference = Workarea.CollectionEntities.Find(f => f.Id == _entityReferenceId);
                return _entityReference;
            }
            set
            {
                if (_entityReference == value) return;
                OnPropertyChanging(GlobalPropertyNames.EntityReference);
                _entityReference = value;
                _entityReferenceId = _entityReference == null ? 0 : _entityReference.Id;
                OnPropertyChanged(GlobalPropertyNames.EntityReference);
            }
        }

        private int _referenceId;
        /// <summary>
        /// Идентификатор ссылки
        /// </summary>
        public int ReferenceId
        {
            get { return _referenceId; }
            set
            {
                if (value == _referenceId) return;
                OnPropertyChanging(GlobalPropertyNames.ReferenceId);
                _referenceId = value;
                OnPropertyChanged(GlobalPropertyNames.ReferenceId);
            }
        }
        
        private int? _valueInt;
        /// <summary>Числовое значение</summary>
        public int? ValueInt
        {
            get { return _valueInt; }
            set { _valueInt = value; }
        }
        private string _valueString;
        /// <summary>Строковое значение</summary>
        public string ValueString
        {
            get { return _valueString; }
            set { _valueString = value; }
        }
        private Guid? _valueGuid;
        /// <summary>Значение глобального идентификатора</summary>
        public Guid? ValueGuid
        {
            get { return _valueGuid; }
            set { _valueGuid = value; }
        }

        private byte[] _valueBinary;
        /// <summary>Двоичное значение</summary>
        public byte[] ValueBinary
        {
            get { return _valueBinary; }
            set { _valueBinary = value; }
        }

        private decimal? _valueMoney;
        /// <summary>Денежное значение</summary>
        public decimal? ValueMoney
        {
            get { return _valueMoney; }
            set { _valueMoney = value; }
        }


        private float? _valueFloat;
        /// <summary>Вещественное значение</summary>
        public float? ValueFloat
        {
            get { return _valueFloat; }
            set { _valueFloat = value; }
        }

        private string _currentValue = string.Empty;
        public string CurrentValue
        {
            get
            {
                if(string.IsNullOrEmpty(_currentValue))
                {
                    switch (KindValue)
                    {
                        case 1:
                            _currentValue = _valueInt.ToString();
                            break;
                        case 2:
                            _currentValue = _valueString;
                            break;
                        case 3:
                            _currentValue = _valueString;
                            break;
                        case 4:
                            _currentValue = _valueMoney.HasValue ? _valueMoney.ToString() : "Нет данных"; 
                            break;
                        case 8:
                            _currentValue = _valueFloat.HasValue ? _valueFloat.ToString() : "Нет данных"; 
                            break;
                        case 16:
                            _currentValue = _valueBinary==null? "Нет данных": "Двоичные данные";
                            break;
                        case 32:
                            _currentValue = _valueGuid.HasValue ? _valueGuid.ToString() : "Нет данных";
                            break;
                        case 64:
                            if (_entityReferenceId == 21 && _referenceId != 0)
                                _currentValue = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_referenceId).Name;
                            break;
                        default:
                            _currentValue = "Неопределен";
                            break;
                    }
                }
                return _currentValue;
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

            if (_valueBinary != null)
                writer.WriteAttributeString(GlobalPropertyNames.ValueBinary, Convert.ToBase64String(_valueBinary));
            if (_valueFloat.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueFloat, XmlConvert.ToString(_valueFloat.Value));
            if (_valueGuid.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueGuid, XmlConvert.ToString(_valueGuid.Value));
            if (_valueInt.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueInt, XmlConvert.ToString(_valueInt.Value));
            if (_valueMoney.HasValue)
                writer.WriteAttributeString(GlobalPropertyNames.ValueMoney, XmlConvert.ToString(_valueMoney.Value));
            if (!string.IsNullOrEmpty(_valueString))
                writer.WriteAttributeString(GlobalPropertyNames.ValueString, _valueString);
        }

        /// <summary>
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ValueBinary) != null)
                _valueBinary = Convert.FromBase64String(reader.GetAttribute(GlobalPropertyNames.ValueBinary));
            if (reader.GetAttribute(GlobalPropertyNames.ValueFloat) != null)
                _valueFloat = float.Parse(reader.GetAttribute(GlobalPropertyNames.ValueFloat));
            if (reader.GetAttribute(GlobalPropertyNames.ValueGuid) != null)
                _valueGuid = XmlConvert.ToGuid(reader.GetAttribute(GlobalPropertyNames.ValueGuid));
            if (reader.GetAttribute(GlobalPropertyNames.ValueInt) != null)
                _valueInt = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ValueInt));
            if (reader.GetAttribute(GlobalPropertyNames.ValueMoney) != null)
                _valueMoney = XmlConvert.ToDecimal(reader.GetAttribute(GlobalPropertyNames.ValueMoney));
            if (reader.GetAttribute(GlobalPropertyNames.ValueString) != null)
                _valueString = reader.GetAttribute(GlobalPropertyNames.ValueString);
        }
        #endregion

        #region Состояние
        SystemParameterStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {
            
            if (base.SaveState(overwrite))
            {
                _baseStruct = new SystemParameterStruct
                                  {
                                      ValueBinary = _valueBinary,
                                      ValueFloat = _valueFloat,
                                      ValueGuid = _valueGuid,
                                      ValueInt = _valueInt,
                                      ValueMoney = _valueMoney,
                                      ValueString = _valueString
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
            ValueBinary = _baseStruct.ValueBinary;
            ValueFloat = _baseStruct.ValueFloat;
            ValueGuid = _baseStruct.ValueGuid;
            ValueInt = _baseStruct.ValueInt;
            ValueMoney = _baseStruct.ValueMoney;
            ValueString = _baseStruct.ValueString;
            IsChanged = false;
        }
        #endregion
        /// <summary>Загрузить экземпляр из базы данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _valueInt = reader.IsDBNull(17) ? (int?) null : reader.GetInt32(17);
                _valueString = reader.IsDBNull(18) ? null : reader.GetString(18);
                _valueGuid = reader.IsDBNull(19) ? (Guid?) null : reader.GetGuid(19);
                _valueBinary = reader.IsDBNull(20) ? null : reader.GetSqlBinary(20).Value;
                _valueMoney = reader.IsDBNull(21) ? (decimal?) null : reader.GetDecimal(21);
                _valueFloat = reader.IsDBNull(22) ? (float?) null : reader.GetFloat(22);
                _entityReferenceId = reader.IsDBNull(23) ? 0 : reader.GetInt16(23);
                _referenceId = reader.IsDBNull(24) ? 0 : reader.GetInt32(24);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(Code))
                throw new ValidateException(Workarea.Cashe.ResourceString("MSG_VAL_SYSPARAMCODE", 1049));
        }
        /// <summary>Установить значения параметров для комманды создания/обновления</summary>
        /// <param name="sqlCmd">Комманда создания/обновления</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ValueInt, SqlDbType.Int) {IsNullable = true};
            if (!_valueInt.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueInt;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueString, SqlDbType.NVarChar) {IsNullable = true};
            if (string.IsNullOrEmpty(_valueString))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _valueString.Length;
                prm.Value = _valueString;
            }
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueMoney, SqlDbType.Money) {IsNullable = true};
            if (!_valueMoney.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueMoney;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueGuid, SqlDbType.UniqueIdentifier) {IsNullable = true};
            if (!_valueGuid.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueGuid;
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueBinary, SqlDbType.Binary) {IsNullable = true};
            if (_valueBinary == null || _valueBinary.All(v => v == 0))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _valueBinary.Length;
                prm.Value = _valueBinary;
            }
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ValueFloat, SqlDbType.Float) {IsNullable = true};
            if (!_valueFloat.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueFloat;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityReferenceId, SqlDbType.SmallInt) { IsNullable = true };
            if (_entityReferenceId==0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _entityReferenceId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ReferenceId, SqlDbType.Int) { IsNullable = true };
            if (_referenceId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _referenceId;
            sqlCmd.Parameters.Add(prm);

            
            
        }

        private List<SystemParameterUser> listUserParams;
        /// <summary>
        /// Возвращает коллекцию пользовательских параметров
        /// </summary>
        /// <returns>Коллекция пользовательских параметров</returns>
        public List<SystemParameterUser> GetUserParams(bool refresh=false)
        {
            if (!refresh && listUserParams != null)
                return listUserParams;
            listUserParams = new List<SystemParameterUser>();
            using (SqlConnection con = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(Workarea.FindMethod("SystemParameterUserLoadByOwnId").FullName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = new SqlParameter(GlobalSqlParamNames.OwnId, Id)
                                           {Direction = ParameterDirection.Input};
                    cmd.Parameters.Add(prm);
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            SystemParameterUser spu = new SystemParameterUser(this) {Workarea = Workarea};
                            spu.Load(rd);
                            listUserParams.Add(spu);
                        }
                        rd.Close();
                    }
                }
            }
            return listUserParams;
        }

        private SystemParameterUser _spu;
        /// <summary>
        /// Возвращает пользовательский параметр для текущего пользователя.
        /// </summary>
        /// <returns>Пользовательский параметр.</returns>
        public SystemParameterUser GetParameterCurrentUser()
        {
            if (_spu != null)
                return _spu;
            using (SqlConnection con = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(Workarea.FindMethod("SystemParameterUserLoadByOwnId").FullName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter prm = cmd.Parameters.AddWithValue(GlobalSqlParamNames.OwnId, Id);
                    prm.Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue(GlobalSqlParamNames.UserId, Workarea.CurrentUser.Id);
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            _spu = new SystemParameterUser(this) {Workarea = Workarea};
                            _spu.Load(rd);
                            return _spu;
                        }
                        rd.Close();
                    }
                }
            }
            _spu = new SystemParameterUser(this, Workarea.CurrentUser.Id) { Workarea = Workarea };
            return _spu;
        }
    }

    /// <summary>
    /// Системный параметр пользователя
    /// </summary>
    public sealed class SystemParameterUser : BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SystemParameterUser()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.SystemParameterUser;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="owner">Владелец</param>
        public SystemParameterUser(SystemParameter owner)
            : base()
        {
            Owner = owner;
            EntityId = (short) WhellKnownDbEntity.SystemParameterUser;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="owner">Владелец</param>
        /// <param name="userId">Идентификатор пользователя</param>
        public SystemParameterUser(SystemParameter owner, int userId)
            : base()
        {
            Owner = owner;
            UserId = userId;
            EntityId = (short)WhellKnownDbEntity.SystemParameterUser;
        }

        #region Свойства

        public string Name
        {
            get
            {
                Uid user = Workarea.Cashe.GetCasheData<Uid>().Item(UserId); //new Uid {Workarea = Workarea};
                //user.Load(UserId);
                return user.Id != 0 ? user.Name : string.Empty;
            }
        }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.UserId);
                _userId = value;
                OnPropertyChanged(GlobalPropertyNames.UserId);
            }
        }

        /// <summary>
        /// Базовый системный параметр
        /// </summary>
        private string _currentValue = string.Empty;
        public string CurrentValue
        {
            get
            {
                if (string.IsNullOrEmpty(_currentValue))
                {
                    switch (_owner.KindValue)
                    {
                        case 1:
                            _currentValue = _valueInt.ToString();
                            break;
                        case 2:
                            _currentValue = _valueString;
                            break;
                        case 3:
                            _currentValue = _valueString;
                            break;
                        case 4:
                            _currentValue = _valueMoney.HasValue ? _valueMoney.ToString() : "Нет данных";
                            break;
                        case 8:
                            _currentValue = _valueFloat.HasValue ? _valueFloat.ToString() : "Нет данных";
                            break;
                        case 16:
                            _currentValue = _valueBinary == null ? "Нет данных" : "Двоичные данные";
                            break;
                        case 32:
                            _currentValue = _valueGuid.HasValue ? _valueGuid.ToString() : "Нет данных";
                            break;
                        case 64:
                            if (_entityReferenceId == 21 && _referenceId != 0)
                                _currentValue = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_referenceId).Name;
                            break;
                        default:
                            _currentValue = "Неопределен";
                            break;
                    }
                }
                return _currentValue;
            }
        }

        private SystemParameter _owner;
        public SystemParameter Owner
        {
            get { return _owner; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.Owner);
                _owner = value;
                _ownId = _owner != null ? _owner.Id : 0;
                OnPropertyChanged(GlobalPropertyNames.Owner);
            }
        }

        /// <summary>
        /// Идентификатор владельца
        /// </summary>
        private int _ownId;
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                if (_ownId != 0)
                {
                    _owner = Workarea.Cashe.GetCasheData<SystemParameter>().Item(_ownId);
                    _owner.Load(_ownId);
                    _ownId = _owner.Id;
                }
                OnPropertyChanged(GlobalPropertyNames.OwnId);
            }
        }

        /// <summary>
        /// Идентификатор системного типа ссылки
        /// </summary>
        private int _entityReferenceId;
        public int EntityReferenceId
        {
            get { return _entityReferenceId; }
            set
            {
                OnPropertyChanging(GlobalPropertyNames.EntityReferenceId);
                _entityReferenceId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityReferenceId);
            }
        }

        /// <summary>
        /// Системный тип ссылки
        /// </summary>
        private EntityType _entityReference;
        public EntityType EntityReference
        {
            get
            {
                if (_entityReferenceId == 0)
                    return null;
                if (_entityReference == null)
                    _entityReference = Workarea.CollectionEntities.Find(f => f.Id == _entityReferenceId);
                else if (_entityReference.Id != _entityReferenceId)
                    _entityReference = Workarea.CollectionEntities.Find(f => f.Id == _entityReferenceId);
                return _entityReference;
            }
            set
            {
                if (_entityReference == value) return;
                OnPropertyChanging(GlobalPropertyNames.EntityReference);
                _entityReference = value;
                _entityReferenceId = _entityReference == null ? 0 : _entityReference.Id;
                OnPropertyChanged(GlobalPropertyNames.EntityReference);
            }
        }

        /// <summary>
        /// Идентификатор ссылки
        /// </summary>
        private int _referenceId;
        public int ReferenceId
        {
            get { return _referenceId; }
            set
            {
                if (value == _referenceId) return;
                OnPropertyChanging(GlobalPropertyNames.ReferenceId);
                _referenceId = value;
                OnPropertyChanged(GlobalPropertyNames.ReferenceId);
            }
        }

        /// <summary>Числовое значение</summary>
        private int? _valueInt;
        public int? ValueInt
        {
            get { return _valueInt; }
            set { _valueInt = value; }
        }

        /// <summary>Строковое значение</summary>
        private string _valueString;
        public string ValueString
        {
            get { return _valueString; }
            set { _valueString = value; }
        }

        /// <summary>Значение глобального идентификатора</summary>
        private Guid? _valueGuid;
        public Guid? ValueGuid
        {
            get { return _valueGuid; }
            set { _valueGuid = value; }
        }

        /// <summary>Двоичное значение</summary>
        private byte[] _valueBinary;
        public byte[] ValueBinary
        {
            get { return _valueBinary; }
            set { _valueBinary = value; }
        }

        /// <summary>Денежное значение</summary>
        private decimal? _valueMoney;
        public decimal? ValueMoney
        {
            get { return _valueMoney; }
            set { _valueMoney = value; }
        }

        /// <summary>Вещественное значение</summary>
        private float? _valueFloat;
        public float? ValueFloat
        {
            get { return _valueFloat; }
            set { _valueFloat = value; }
        }

        #endregion

        #region Состояние
        SystemParameterStruct _baseStruct;
        
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new SystemParameterStruct
                {
                    ValueBinary = _valueBinary,
                    ValueFloat = _valueFloat,
                    ValueGuid = _valueGuid,
                    ValueInt = _valueInt,
                    ValueMoney = _valueMoney,
                    ValueString = _valueString
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
            ValueBinary = _baseStruct.ValueBinary;
            ValueFloat = _baseStruct.ValueFloat;
            ValueGuid = _baseStruct.ValueGuid;
            ValueInt = _baseStruct.ValueInt;
            ValueMoney = _baseStruct.ValueMoney;
            ValueString = _baseStruct.ValueString;
            IsChanged = false;
        }
        #endregion

        #region Protected Методы

        /// <summary>Загрузить экземпляр из базы данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            _ownId = reader.GetInt32(9);
            _userId = reader.GetInt32(10);
            _valueInt = reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11);
            _valueString = reader.IsDBNull(12) ? null : reader.GetString(12);
            _valueGuid = reader.IsDBNull(13) ? (Guid?)null : reader.GetGuid(13);
            _valueBinary = reader.IsDBNull(14) ? null : reader.GetSqlBinary(14).Value;
            _valueMoney = reader.IsDBNull(15) ? (decimal?)null : reader.GetDecimal(15);
            _valueFloat = reader.IsDBNull(16) ? (float?)null : reader.GetFloat(16);
            _entityReferenceId = reader.IsDBNull(17) ? 0 : reader.GetInt16(17);
            _referenceId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания/обновления</summary>
        /// <param name="sqlCmd">Комманда создания/обновления</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion=true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.OwnId, SqlDbType.Int) {IsNullable = false, Value = _ownId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.UserId, SqlDbType.Int) {IsNullable = false, Value = _userId};
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

            prm = new SqlParameter(GlobalSqlParamNames.ValueGuid, SqlDbType.UniqueIdentifier) { IsNullable = true };
            if (!_valueGuid.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueGuid;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ValueBinary, SqlDbType.Binary) { IsNullable = true };
            if (_valueBinary == null || _valueBinary.All(v => v == 0))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _valueBinary.Length;
                prm.Value = _valueBinary;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ValueFloat, SqlDbType.Float) { IsNullable = true };
            if (!_valueFloat.HasValue)
                prm.Value = DBNull.Value;
            else
                prm.Value = _valueFloat;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityReferenceId, SqlDbType.SmallInt) { IsNullable = true };
            if (_entityReferenceId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _entityReferenceId;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.ReferenceId, SqlDbType.Int) { IsNullable = true };
            if (_referenceId == 0)
                prm.Value = DBNull.Value;
            else
                prm.Value = _referenceId;
            sqlCmd.Parameters.Add(prm);
        }

        #endregion

        #region Public Методы
        
        ///// <summary>
        ///// Загрузка по идентификатору
        ///// </summary>
        ///// <param name="value">Идентификатор</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.FindMethod("SystemParameterUserLoad").FullName);
        //}

        ///// <summary>
        ///// Сохраняет объект в базу
        ///// </summary>
        //public void Save()
        //{
        //    // Запрет на сохранение пользовательского параметра, если в системном параметре не проставлен флаг 16
        //    if ((_owner.FlagsValue & 16) != 16)
        //        return;
        //    Validate();
        //    if (Id == 0)
        //        Create(Workarea.FindMethod("SystemParameterUserInsert").FullName);
        //    else
        //        Update(Workarea.FindMethod("SystemParameterUserUpdate").FullName, true);
        //}

        /// <summary>
        /// Проверка обыекта на соответствие системные требованиям
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (_owner == null)
            {
                _owner = new SystemParameter {Workarea = Workarea};
                _owner.Load(OwnId);
            }
            if (_owner.Id == 0)
                throw new Exception("OwnId задан не корректно");
            Uid u = Workarea.Cashe.GetCasheData<Uid>().Item(UserId);
            //Uid u = new Uid {Workarea = Workarea};
            //u.Load(UserId);
            if (u.Id == 0)
                throw new Exception("UserId задан не верно");
        }

        #endregion
    }
}
