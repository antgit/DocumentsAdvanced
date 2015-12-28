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
        /// <summary>�������� ��������</summary>
        public byte[] ValueBinary;
        /// <summary>������������ ��������</summary>
        public float? ValueFloat;
        /// <summary>�������� ����������� ��������������</summary>
        public Guid? ValueGuid;
        /// <summary>�������� ��������</summary>
        public int? ValueInt;
        /// <summary>�������� ��������</summary>
        public decimal? ValueMoney;
        /// <summary>��������� ��������</summary>
        public string ValueString;
    }
    // TODO: ��������� ������������...
    /// <summary>��������� ��������</summary>
    public sealed class SystemParameter : BaseCore<SystemParameter>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>����� ��������, ������������� �������� 1</summary>
        public const int KINDVALUE_INT = 1;
        /// <summary>��������� ��������, ������������� �������� 2</summary>
        public const int KINDVALUE_STRING = 2;
        /// <summary>��������� - ����� � ������, ������������� �������� 3</summary>
        public const int KINDVALUE_COMPOUND = 3;
        /// <summary>�������� ��������, ������������� �������� 4</summary>
        public const int KINDVALUE_MONEY = 4;
        /// <summary>������������ ��������, ������������� �������� 8</summary>
        public const int KINDVALUE_REAL = 8;
        /// <summary>������� ��������, ������������� �������� 16</summary>
        public const int KINDVALUE_BIN = 16;
        /// <summary>���������� �������������, ������������� �������� 32</summary>
        public const int KINDVALUE_GUID = 32;
        /// <summary>������ �� ������, ������������� �������� 64</summary>
        public const int KINDVALUE_LINK = 64;

        /// <summary>����� ��������, ������������� �������� 1638401</summary>
        public const int KINDID_INT = 1638401;
        /// <summary>��������� ��������, ������������� �������� 1638402</summary>
        public const int KINDID_STRING = 1638402;
        /// <summary>��������� - ����� � ������, ������������� �������� 1638403</summary>
        public const int KINDID_COMPOUND = 1638403;
        /// <summary>�������� ��������, ������������� �������� 1638404</summary>
        public const int KINDID_MONEY = 1638404;
        /// <summary>������������ ��������, ������������� �������� 1638408</summary>
        public const int KINDID_REAL = 1638408;
        /// <summary>������� ��������, ������������� �������� 1638416</summary>
        public const int KINDID_BIN = 1638416;
        /// <summary>���������� �������������, ������������� �������� 1638432</summary>
        public const int KINDID_GUID = 1638432;
        /// <summary>������ �� ������, ������������� �������� 1638464</summary>
        public const int KINDID_LINK = 1638464;
        // ReSharper restore InconsistentNaming
        #endregion
        /// <summary>
        /// ��������� ��������� ����������� �����������
        /// </summary>
        public const string WEBALLOWCOMPANYREGISTER = "SYSTEMPARAMETER_WEBALLOWCOMPANYREGISTER";
        /// <summary>
        /// �������� ������� Web ����������
        /// </summary>
        public const string WEBROOTSERVER = "WEBROOTSERVER";
        
        /// <summary>�����������</summary>
        public SystemParameter(): base()
        {
            EntityId = 25;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
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
        #region ��������

        private int _entityReferenceId;
        /// <summary>
        /// ������������� ���������� ���� ������
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
        /// ��������� ��� ������
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
        /// ������������� ������
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
        /// <summary>�������� ��������</summary>
        public int? ValueInt
        {
            get { return _valueInt; }
            set { _valueInt = value; }
        }
        private string _valueString;
        /// <summary>��������� ��������</summary>
        public string ValueString
        {
            get { return _valueString; }
            set { _valueString = value; }
        }
        private Guid? _valueGuid;
        /// <summary>�������� ����������� ��������������</summary>
        public Guid? ValueGuid
        {
            get { return _valueGuid; }
            set { _valueGuid = value; }
        }

        private byte[] _valueBinary;
        /// <summary>�������� ��������</summary>
        public byte[] ValueBinary
        {
            get { return _valueBinary; }
            set { _valueBinary = value; }
        }

        private decimal? _valueMoney;
        /// <summary>�������� ��������</summary>
        public decimal? ValueMoney
        {
            get { return _valueMoney; }
            set { _valueMoney = value; }
        }


        private float? _valueFloat;
        /// <summary>������������ ��������</summary>
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
                            _currentValue = _valueMoney.HasValue ? _valueMoney.ToString() : "��� ������"; 
                            break;
                        case 8:
                            _currentValue = _valueFloat.HasValue ? _valueFloat.ToString() : "��� ������"; 
                            break;
                        case 16:
                            _currentValue = _valueBinary==null? "��� ������": "�������� ������";
                            break;
                        case 32:
                            _currentValue = _valueGuid.HasValue ? _valueGuid.ToString() : "��� ������";
                            break;
                        case 64:
                            if (_entityReferenceId == 21 && _referenceId != 0)
                                _currentValue = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_referenceId).Name;
                            break;
                        default:
                            _currentValue = "�����������";
                            break;
                    }
                }
                return _currentValue;
            }
        }
        #endregion

        #region ������������

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="writer">������ ������ XML ������</param>
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
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
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

        #region ���������
        SystemParameterStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
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
        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
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
        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">��������� ������������� �������</param>
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
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
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
        /// <summary>���������� �������� ���������� ��� �������� ��������/����������</summary>
        /// <param name="sqlCmd">�������� ��������/����������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
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
        /// ���������� ��������� ���������������� ����������
        /// </summary>
        /// <returns>��������� ���������������� ����������</returns>
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
        /// ���������� ���������������� �������� ��� �������� ������������.
        /// </summary>
        /// <returns>���������������� ��������.</returns>
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
    /// ��������� �������� ������������
    /// </summary>
    public sealed class SystemParameterUser : BaseCoreObject
    {
        /// <summary>
        /// �����������
        /// </summary>
        public SystemParameterUser()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.SystemParameterUser;
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="owner">��������</param>
        public SystemParameterUser(SystemParameter owner)
            : base()
        {
            Owner = owner;
            EntityId = (short) WhellKnownDbEntity.SystemParameterUser;
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="owner">��������</param>
        /// <param name="userId">������������� ������������</param>
        public SystemParameterUser(SystemParameter owner, int userId)
            : base()
        {
            Owner = owner;
            UserId = userId;
            EntityId = (short)WhellKnownDbEntity.SystemParameterUser;
        }

        #region ��������

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
        /// ������������� ������������
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
        /// ������� ��������� ��������
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
                            _currentValue = _valueMoney.HasValue ? _valueMoney.ToString() : "��� ������";
                            break;
                        case 8:
                            _currentValue = _valueFloat.HasValue ? _valueFloat.ToString() : "��� ������";
                            break;
                        case 16:
                            _currentValue = _valueBinary == null ? "��� ������" : "�������� ������";
                            break;
                        case 32:
                            _currentValue = _valueGuid.HasValue ? _valueGuid.ToString() : "��� ������";
                            break;
                        case 64:
                            if (_entityReferenceId == 21 && _referenceId != 0)
                                _currentValue = Workarea.Cashe.GetCasheData<CustomViewList>().Item(_referenceId).Name;
                            break;
                        default:
                            _currentValue = "�����������";
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
        /// ������������� ���������
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
        /// ������������� ���������� ���� ������
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
        /// ��������� ��� ������
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
        /// ������������� ������
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

        /// <summary>�������� ��������</summary>
        private int? _valueInt;
        public int? ValueInt
        {
            get { return _valueInt; }
            set { _valueInt = value; }
        }

        /// <summary>��������� ��������</summary>
        private string _valueString;
        public string ValueString
        {
            get { return _valueString; }
            set { _valueString = value; }
        }

        /// <summary>�������� ����������� ��������������</summary>
        private Guid? _valueGuid;
        public Guid? ValueGuid
        {
            get { return _valueGuid; }
            set { _valueGuid = value; }
        }

        /// <summary>�������� ��������</summary>
        private byte[] _valueBinary;
        public byte[] ValueBinary
        {
            get { return _valueBinary; }
            set { _valueBinary = value; }
        }

        /// <summary>�������� ��������</summary>
        private decimal? _valueMoney;
        public decimal? ValueMoney
        {
            get { return _valueMoney; }
            set { _valueMoney = value; }
        }

        /// <summary>������������ ��������</summary>
        private float? _valueFloat;
        public float? ValueFloat
        {
            get { return _valueFloat; }
            set { _valueFloat = value; }
        }

        #endregion

        #region ���������
        SystemParameterStruct _baseStruct;
        
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
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

        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
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

        #region Protected ������

        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">��������� ������������� �������</param>
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
        /// <summary>���������� �������� ���������� ��� �������� ��������/����������</summary>
        /// <param name="sqlCmd">�������� ��������/����������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
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

        #region Public ������
        
        ///// <summary>
        ///// �������� �� ��������������
        ///// </summary>
        ///// <param name="value">�������������</param>
        //public override void Load(int value)
        //{
        //    Load(value, Workarea.FindMethod("SystemParameterUserLoad").FullName);
        //}

        ///// <summary>
        ///// ��������� ������ � ����
        ///// </summary>
        //public void Save()
        //{
        //    // ������ �� ���������� ����������������� ���������, ���� � ��������� ��������� �� ���������� ���� 16
        //    if ((_owner.FlagsValue & 16) != 16)
        //        return;
        //    Validate();
        //    if (Id == 0)
        //        Create(Workarea.FindMethod("SystemParameterUserInsert").FullName);
        //    else
        //        Update(Workarea.FindMethod("SystemParameterUserUpdate").FullName, true);
        //}

        /// <summary>
        /// �������� ������� �� ������������ ��������� �����������
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
                throw new Exception("OwnId ����� �� ���������");
            Uid u = Workarea.Cashe.GetCasheData<Uid>().Item(UserId);
            //Uid u = new Uid {Workarea = Workarea};
            //u.Load(UserId);
            if (u.Id == 0)
                throw new Exception("UserId ����� �� �����");
        }

        #endregion
    }
}
