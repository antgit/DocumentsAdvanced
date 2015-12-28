using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects.Developer
{
    /// <summary>��������� ������� "�������� ������ ������� ���� ������"</summary>
    internal struct DbObjectChildStruct
    {
        /// <summary> ��������� Null ��������</summary>
        public bool AllowNull;
        /// <summary>�������� </summary>
        public string Description;
        /// <summary>������</summary>
        public short GroupNo;
        /// <summary>�������� ����������� ��������</summary>
        public bool IsFormula;
        /// <summary>����� �������</summary>
        public short OrderNo;
        /// <summary>������������� �������</summary>
        public int OwnId;
        /// <summary>����� ����</summary>
        public string TypeLen;
        /// <summary>��� .Net</summary>
        public string TypeNameNet;
        /// <summary>��� Sql</summary>
        public string TypeNameSql;
    }
    /// <summary>
    /// �������� ������ ������� ���� ������
    /// </summary>
    /// <remarks>������ ������ ������������ ����� ������������ ������� �������, �������� �������� ��������� � �.�.</remarks>
    public class DbObjectChild : BaseCore<DbObjectChild>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>������� �������, ������������� �������� 1</summary>
        public const int KINDVALUE_COLUMN = 1;
        /// <summary>��� ��������, ������������� �������� 2</summary>
        public const int KINDVALUE_RETURNCODE = 2;
        /// <summary>��������, ������������� �������� 3</summary>
        public const int KINDVALUE_PARAMETER = 3;

        /// <summary>������� �������, ������������� �������� 2752513</summary>
        public const int KINDID_COLUMN = 2752513;
        /// <summary>��� ��������, ������������� �������� 2752514</summary>
        public const int KINDID_RETURNCODE = 2752514;
        /// <summary>��������, ������������� �������� 2752515</summary>
        public const int KINDID_PARAMETER = 2752515;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>
        /// �����������
        /// </summary>
        public DbObjectChild():base((short)WhellKnownDbEntity.DbObjectChild)
        {
            
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override DbObjectChild Clone(bool endInit)
        {
            DbObjectChild obj = base.Clone(endInit);
            obj.AllowNull = AllowNull;
            obj.Description = Description;
            obj.GroupNo = GroupNo;
            obj.IsFormula = IsFormula;
            obj.OrderNo = OrderNo;
            obj.OwnId = OwnId;
            obj.TypeLen = TypeLen;
            obj.TypeNameSql = TypeNameSql;
            obj.TypeNameNet = TypeNameNet;

            if (endInit)
                OnEndInit();
            return obj;
        }
        #region ��������
       
        private short _groupNo;
        /// <summary>
        /// ������
        /// </summary>
        public short GroupNo 
        { 
            get{ return _groupNo; } 
            set
            {
                if (value == _groupNo) return;
                OnPropertyChanging(GlobalPropertyNames.GroupNo);
                _groupNo = value;
                OnPropertyChanged(GlobalPropertyNames.GroupNo);
            } 
        }
        
        private short _orderNo; 
        /// <summary>
        /// ����� �������
        /// </summary>
        public short OrderNo 
        { 
            get{ return _orderNo; } 
            set
            {
                if (value == _orderNo) return;
                OnPropertyChanging(GlobalPropertyNames.OrderNo);
                _orderNo = value;
                OnPropertyChanged(GlobalPropertyNames.OrderNo);
            } 
        }
        
        
        private int _ownId;
        /// <summary>
        /// ������������� �������
        /// </summary>
        public int OwnId
        {
            get { return _ownId; }
            set
            {
                if (value == _ownId) return;
                OnPropertyChanging(GlobalPropertyNames.OwnId);
                _ownId = value;
                OnPropertyChanged(GlobalPropertyNames.OwnId);
                
            }
        }

        private DbObject _owner;
        /// <summary>
        /// �������
        /// </summary>
        public DbObject Owner
        {
            get
            {
                if (_ownId == 0)
                    return null;
                if (_owner == null)
                    _owner = Workarea.Cashe.GetCasheData<DbObject>().Item(_ownId);
                else if (_owner.Id != _ownId)
                    _owner = Workarea.Cashe.GetCasheData<DbObject>().Item(_ownId);
                return _owner;
            }
            set
            {
                if (_owner == value) return;
                OnPropertyChanging(GlobalPropertyNames.Owner);
                _owner = value;
                _ownId = _owner == null ? 0 : _owner.Id;
                OnPropertyChanged(GlobalPropertyNames.Owner);
            }
        }

        private string _description;
        /// <summary>
        /// ��������
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                OnPropertyChanging(GlobalPropertyNames.Description);
                _description = value;
                OnPropertyChanged(GlobalPropertyNames.Description);
            }
        }
        private bool _isFormula; 
        /// <summary>
        /// �������� ����������� ��������
        /// </summary>
        public bool IsFormula 
        { 
            get{ return _isFormula; } 
            set
            {
                if (value == _isFormula) return;
                OnPropertyChanging(GlobalPropertyNames.IsFormula);
                _isFormula = value;
                OnPropertyChanged(GlobalPropertyNames.IsFormula);
            } 
        }
        private string _typeNameSql;
        /// <summary>
        /// ��� Sql
        /// </summary>
        public string TypeNameSql
        {
            get { return _typeNameSql; }
            set
            {
                if (value == _typeNameSql) return;
                OnPropertyChanging(GlobalPropertyNames.TypeNameSql);
                _typeNameSql = value;
                OnPropertyChanged(GlobalPropertyNames.TypeNameSql);
            }
        }
        private string _typeNameNet;
        /// <summary>
        /// ��� .Net
        /// </summary>
        public string TypeNameNet
        {
            get { return _typeNameNet; }
            set
            {
                if (value == _typeNameNet) return;
                OnPropertyChanging(GlobalPropertyNames.TypeNameNet);
                _typeNameNet = value;
                OnPropertyChanged(GlobalPropertyNames.TypeNameNet);
            }
        }
        private string _typeLen;
        /// <summary>
        /// ����� ����
        /// </summary>
        public string TypeLen
        {
            get { return _typeLen; }
            set
            {
                if (value == _typeLen) return;
                OnPropertyChanging(GlobalPropertyNames.TypeLen);
                _typeLen = value;
                OnPropertyChanged(GlobalPropertyNames.TypeLen);
            }
        }
        private bool _allowNull;
        /// <summary>
        /// ��������� Null ��������
        /// </summary>
        public bool AllowNull
        {
            get { return _allowNull; }
            set
            {
                if (value == _allowNull) return;
                OnPropertyChanging(GlobalPropertyNames.AllowNull);
                _allowNull = value;
                OnPropertyChanged(GlobalPropertyNames.AllowNull);
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

            if (_allowNull)
                writer.WriteAttributeString(GlobalPropertyNames.AllowNull, XmlConvert.ToString(_allowNull));
            if (!string.IsNullOrEmpty(_description))
                writer.WriteAttributeString(GlobalPropertyNames.Description, _description);
            if (_groupNo != 0)
                writer.WriteAttributeString(GlobalPropertyNames.GroupNo, XmlConvert.ToString(_groupNo));
            if (_isFormula)
                writer.WriteAttributeString(GlobalPropertyNames.IsFormula, XmlConvert.ToString(_isFormula));
            if (_orderNo !=0 )
                writer.WriteAttributeString(GlobalPropertyNames.OrderNo, XmlConvert.ToString(_orderNo));
            if (_ownId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.OwnId, XmlConvert.ToString(_ownId));
            if (!string.IsNullOrEmpty(_typeLen))
                writer.WriteAttributeString(GlobalPropertyNames.TypeLen, _typeLen);
            if (!string.IsNullOrEmpty(_typeNameNet))
                writer.WriteAttributeString(GlobalPropertyNames.TypeNameNet, _typeNameNet);
            if (!string.IsNullOrEmpty(_typeNameSql))
                writer.WriteAttributeString(GlobalPropertyNames.TypeNameSql, _typeNameSql);
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.AllowNull) != null)
                _allowNull = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.AllowNull));
            if (reader.GetAttribute(GlobalPropertyNames.Description) != null)
                _description = (reader.GetAttribute(GlobalPropertyNames.Description));
            if (reader.GetAttribute(GlobalPropertyNames.GroupNo) != null)
                _groupNo = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.GroupNo));
            if (reader.GetAttribute(GlobalPropertyNames.IsFormula) != null)
                _isFormula = XmlConvert.ToBoolean(reader.GetAttribute(GlobalPropertyNames.IsFormula));
            if (reader.GetAttribute(GlobalPropertyNames.OrderNo) != null)
                _orderNo = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.OrderNo));
            if (reader.GetAttribute(GlobalPropertyNames.OwnId) != null)
                _ownId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.OwnId));
            if (reader.GetAttribute(GlobalPropertyNames.TypeLen) != null)
                _typeLen = reader.GetAttribute(GlobalPropertyNames.TypeLen);
            if (reader.GetAttribute(GlobalPropertyNames.TypeNameNet) != null)
                _typeNameNet = reader.GetAttribute(GlobalPropertyNames.TypeNameNet);
            if (reader.GetAttribute(GlobalPropertyNames.TypeNameSql) != null)
                _typeNameSql = reader.GetAttribute(GlobalPropertyNames.TypeNameSql);
        }
        #endregion

        #region ���������
        DbObjectChildStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new DbObjectChildStruct
                {
                    AllowNull = _allowNull,
                    Description = _description,
                    GroupNo = _groupNo,
                    IsFormula = _isFormula,
                    OrderNo = _orderNo,
                    OwnId = _ownId,
                    TypeLen = _typeLen,
                    TypeNameNet = _typeNameNet,
                    TypeNameSql = _typeNameSql,
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            AllowNull = _baseStruct.AllowNull;
            Description = _baseStruct.Description;
            GroupNo = _baseStruct.GroupNo;
            IsFormula = _baseStruct.IsFormula;
            OrderNo = _baseStruct.OrderNo;
            OwnId = _baseStruct.OwnId;
            TypeLen = _baseStruct.TypeLen;
            TypeNameNet = _baseStruct.TypeNameNet;
            TypeNameSql = _baseStruct.TypeNameSql;

            IsChanged = false;
        }
        #endregion

        #region ���� ������

        /// <summary>�������� ������</summary>
        /// <param name="reader">������ ������ ������</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _groupNo = reader.GetInt16(17);
                _orderNo = reader.GetInt16(18);
                _description = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                _isFormula = reader.GetBoolean(20);
                _ownId = reader.GetInt32(21);
                _typeNameSql = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                _typeNameNet = reader.IsDBNull(23) ? string.Empty : reader.GetString(23);
                _typeLen = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                _allowNull = reader.IsDBNull(25) ? false : reader.GetBoolean(25);
            }
            catch(Exception ex)
            {
                throw new ObjectReaderException("������ ������ ������� �� ���� ������", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>���������� �������� ���������� ��� �������� ��������</summary>
        /// <param name="sqlCmd">�������� ��������</param>
        /// <param name="insertCommand">�������� �� �������� ��������� ����������</param>
        /// <param name="validateVersion">��������� �� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);

            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.GroupNo, SqlDbType.SmallInt)
            {
                IsNullable = false,
                Value = _groupNo
            };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.SmallInt)
            {
                IsNullable = false,
                Value = _orderNo
            };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.IsFormula, SqlDbType.Bit)
            {
                IsNullable = false,
                Value = _isFormula
            };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.Description, SqlDbType.NVarChar) { IsNullable = true };
            if (string.IsNullOrEmpty(_description))
                prm.Value = DBNull.Value;
            else
            {
                prm.Size = _description.Length;
                prm.Value = _description;
            }
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OwnId, SqlDbType.Int)
            {
                IsNullable = false,
                Value = _ownId
            };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TypeNameSql, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_typeNameSql))
                prm.Value = DBNull.Value;
            else
                prm.Value = _typeNameSql;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TypeNameNet, SqlDbType.NVarChar, 128) { IsNullable = true };
            if (string.IsNullOrEmpty(_typeNameNet))
                prm.Value = DBNull.Value;
            else
                prm.Value = _typeNameNet;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.TypeLen, SqlDbType.NVarChar, 16) { IsNullable = true };
            if (string.IsNullOrEmpty(_typeLen))
                prm.Value = DBNull.Value;
            else
                prm.Value = _typeLen;
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.AllowNull, SqlDbType.Bit) {IsNullable = false, Value = _allowNull};
            sqlCmd.Parameters.Add(prm);
        }  
        #endregion
        /// <summary>
        /// ��������� ������� � ��������� ������� � �����
        /// </summary>
        /// <param name="workarea">������� �������</param>
        /// <param name="schema">����� ������</param>
        /// <param name="table">������������ �������</param>
        /// <returns></returns>
        public static List<DbObjectChild> GetCollection(Workarea workarea, string schema, string table)
        {
            List<DbObjectChild> collection = new List<DbObjectChild>();
            using (SqlConnection cnn = workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = workarea.Empty<DbObject>().Entity.FindMethod("DbObjectsLoadByOwner").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 128).Value = table;
                        cmd.Parameters.Add(GlobalSqlParamNames.Schema, SqlDbType.NVarChar, 128).Value = schema;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DbObjectChild item = new DbObjectChild { Workarea = workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }
    }
}