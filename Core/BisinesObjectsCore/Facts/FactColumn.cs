using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct FactColumnStruct
    {
        /// <summary>������������� ������������</summary>
        public int FactNameId;
        /// <summary>������� ����������</summary>
        public int OrderNo;
        /// <summary>��� ������ ������</summary>
        public int? ReferenceType;
        /// <summary>��� ������ ������</summary>
        public int? ReferenceType2;
        /// <summary>��� ������� ������</summary>
        public int? ReferenceType3;
        /// <summary>������������� ����� ������ ������</summary>
        public int? RootId;
        /// <summary>������������� ����� ������ ������</summary>
        public int? RootId2;
        /// <summary>������������� ����� ������� ������</summary>
        public int? RootId3;
    }
    /// <summary>������� �����</summary>
    public class FactColumn : BaseCore<FactColumn>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>����� ��������, ������������� �������� 1</summary>
        public const int KINDVALUE_INT = 1;
        /// <summary>��������� ��������, ������������� �������� 2</summary>
        public const int KINDVALUE_STRING = 2;
        /// <summary>��������� ����� � ������, ������������� �������� 3</summary>
        public const int KINDVALUE_COMPOSITE = 3;
        /// <summary>�������� ��������, ������������� �������� 4</summary>
        public const int KINDVALUE_MONEY = 4;
        /// <summary>���� - �����, ������������� �������� 8</summary>
        public const int KINDVALUE_DATETIME = 8;
        /// <summary>������, ������������� �������� 16</summary>
        public const int KINDVALUE_LOGIC = 16;
        /// <summary>XML ������, ������������� �������� 32</summary>
        public const int KINDVALUE_XML = 32;
        /// <summary>���������� �������������, ������������� �������� 64</summary>
        public const int KINDVALUE_GUID = 64;
        /// <summary>������������ ��������, ������������� �������� 128</summary>
        public const int KINDVALUE_REAL = 128;
        /// <summary>�������� ��������, ������������� �������� 256</summary>
        public const int KINDVALUE_BIN = 256;
        /// <summary>������ ������, ������������� �������� 512</summary>
        public const int KINDVALUE_FIRSTLINK = 512;
        /// <summary>������ ������, ������������� �������� 1024</summary>
        public const int KINDVALUE_SECONDLINK = 1024;
        /// <summary>������ ������, ������������� �������� 2048</summary>
        public const int KINDVALUE_THIRDLING = 2048;

        /// <summary>����� ��������, ������������� �������� 1179649</summary>
        public const int KINDID_INT = 1179649;
        /// <summary>��������� ��������, ������������� �������� 1179650</summary>
        public const int KINDID_STRING = 1179650;
        /// <summary>��������� ����� � ������, ������������� �������� 1179651</summary>
        public const int KINDID_COMPOSITE = 1179651;
        /// <summary>�������� ��������, ������������� �������� 1179652</summary>
        public const int KINDID_MONEY = 1179652;
        /// <summary>���� - �����, ������������� �������� 1179653</summary>
        public const int KINDID_DATETIME = 1179653;
        /// <summary>������, ������������� �������� 1179664</summary>
        public const int KINDID_LOGIC = 1179664;
        /// <summary>XML ������, ������������� �������� 1179660</summary>
        public const int KINDID_XML = 1179660;
        /// <summary>���������� �������������, ������������� �������� 1179712</summary>
        public const int KINDID_GUID = 1179712;
        /// <summary>������������ ��������, ������������� �������� 1179776</summary>
        public const int KINDID_REAL = 1179776;
        /// <summary>�������� ��������, ������������� �������� 1179904</summary>
        public const int KINDID_BIN = 1179904;
        /// <summary>������ ������, ������������� �������� 1180160</summary>
        public const int KINDID_FIRSTLINK = 1180160;
        /// <summary>������ ������, ������������� �������� 1180672</summary>
        public const int KINDID_SECONDLINK = 1180672;
        /// <summary>������ ������, ������������� �������� 1181696</summary>
        public const int KINDID_THIRDLING = 1181696;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>�����������</summary>
        public FactColumn(): base()
        {
            EntityId = (int)WhellKnownDbEntity.FactColumn;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
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
        #region ��������
        private int _factNameId;
        /// <summary>������������� ������������</summary>
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
        /// <summary>������������ �����</summary>
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
        /// <summary>������� ����������</summary>
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
        /// <summary>��� ������ ������</summary>
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
        /// <summary>��� ������ ������</summary>
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
        /// <summary>��� ������ ������</summary>
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
        /// <summary>��� ������ ������</summary>
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
        /// <summary>��� ������� ������</summary>
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
        /// <summary>��� ������ ������</summary>
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
        /// <summary>������������� ����� ������ ������</summary>
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
        /// <summary>������������� ����� ������ ������</summary>
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
        /// <summary>������������� ����� ������� ������</summary>
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
        /// <summary>��������� �� ������ ������</summary>
        public bool AllowValueRef3
        {
            get { return (KindId & 2048) == 2048; }
        }

        /// <summary>��������� �� ������ ������</summary>
        public bool AllowValueRef2
        {
            get { return (KindId & 1024) == 1024; }
        }

        /// <summary>��������� �� ������ ������</summary>
        public bool AllowValueRef1
        {
            get { return (KindId & 512) == 512; }
        }
        /// <summary>��������� �� �������� ��������</summary>
        public bool AllowValueBinary
        {
            get { return (KindId & 256) == 256; }
        }
        /// <summary>��������� �� ���������� ��������</summary>
        public bool AllowValueDecimal
        {
            get { return (KindId & 128) == 128; }
        }
        /// <summary>��������� �� �������� ����������� ��������������</summary>
        public bool AllowValueGuid
        {
            get { return (KindId & 64) == 64; }
        }
        /// <summary>��������� �� �������� XML</summary>
        public bool AllowValueXml
        {
            get { return (KindId & 32) == 32; }
        }
        /// <summary>��������� �� ���������� ��������</summary>
        public bool AllowValueBit
        {
            get { return (KindId & 16) == 16; }
        }
        /// <summary>��������� �� �������� ����/�������</summary>
        public bool AllowValueDate
        {
            get { return (KindId & 8) == 8; }
        }
        /// <summary>��������� �� �������� ��������</summary>
        public bool AllowValueMoney
        {
            get { return (KindId & 4) == 4; }
        }
        /// <summary>��������� �� ��������� ��������</summary>
        public bool AllowValueString
        {
            get { return (KindId & 2) == 2; }
        }
        /// <summary>��������� �� ����� ��������</summary>
        public bool AllowValueInt
        {
            get { return (KindId & 1) == 1; }
        }
        #endregion

        #region ������������
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

        #region ���� ������
        /// <summary>���������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">��������� ������������� �������</param>
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
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>
        /// ���������� �������� ���������� ��� �������� ��������
        /// </summary>
        /// <param name="sqlCmd">�������� ��������</param>
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

        #region ���������
        FactColumnStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
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
        /// <summary>����������� ������� ��������� �������</summary>
        /// <remarks>������������� ��������� �������� ������ ����� ���������� ����������� ���������</remarks>
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
