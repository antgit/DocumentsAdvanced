using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct FactNameStruct
    {
        /// <summary>������������� ���������� ������� �������� ����������� ����</summary>
        public short ToEntityId;
    }
    /// <summary>������������ �����</summary>
    public sealed class FactName : BaseCore<FactName>
    {
        #region ��������� ����� � ��������
        // ReSharper disable InconsistentNaming
        /// <summary>������������ �����, ������������� �������� 1</summary>
        public const int KINDVALUE_FACT = 1;
        /// <summary>������������ ��������, ������������� �������� 2</summary>
        public const int KINDVALUE_PROPERTY = 2;

        /// <summary>������������ �����, ������������� �������� 1114113</summary>
        public const int KINDID_FACT = 1114113;
        /// <summary>������������ ��������, ������������� �������� 1114114</summary>
        public const int KINDID_PROPERTY = 1114114;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>�����������</summary>
        public FactName():base()
        {
            EntityId = (short)WhellKnownDbEntity.FactName;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override FactName Clone(bool endInit)
        {
            FactName obj = base.Clone(endInit);
            obj.ToEntityId = ToEntityId;

            if (endInit)
                OnEndInit();
            return obj;
        }
        #region ��������
        private short _toEntityId;
        /// <summary>������������� ���������� ������� �������� ����������� ����</summary>
        public short ToEntityId
        {
            get { return _toEntityId; }
            set
            {
                if (value == _toEntityId) return;
                OnPropertyChanging(GlobalPropertyNames.ToEntityId);
                _toEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ToEntityId);
            }
        }
        private EntityType _toEntity;
        /// <summary>��� ��������</summary>
        public EntityType ToEntity
        {
            get
            {
                if (_toEntityId == 0)
                    return null;
                if (_toEntity == null)
                    _toEntity = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _toEntityId);
                else if (_toEntity.Id != _toEntityId)
                    _toEntity = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _toEntityId);
                return _toEntity;
            }
        } 
        #endregion

        #region ������������
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_toEntityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ToEntityId, XmlConvert.ToString(_toEntityId));
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ToEntityId) != null)
                _toEntityId = XmlConvert.ToInt16(reader.GetAttribute(GlobalPropertyNames.ToEntityId));
        }
        #endregion

        #region ���������
        FactNameStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new FactNameStruct
                {
                    ToEntityId = _toEntityId,
                };
                return true;
            }
            return false;
        }
        public override void RestoreState()
        {
            base.RestoreState();
            ToEntityId = _baseStruct.ToEntityId;

            IsChanged = false;
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
                _toEntityId = reader.GetInt16(17);
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
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt) { IsNullable = false, Value = _toEntityId };
            sqlCmd.Parameters.Add(prm);
        }       
        #endregion

        #region �������������� ������
        //
        public bool HasFactNames(short toEntityId)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return false;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<FactName>().Entity.FindMethod("FactNamesExistsToEntityId").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt).Value = toEntityId;
                        object obj = cmd.ExecuteScalar();
                        return (bool) obj;
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        /// <summary>
        /// ��������� ������� �����
        /// </summary>
        /// <returns></returns>
        public List<FactColumn> GetCollectionFactColumns()
        {
            FactColumn item;
            List<FactColumn> collection = new List<FactColumn>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(GlobalSqlParamNames.FactNameId, SqlDbType.Int).Value = Id;
                        cmd.CommandText = Workarea.Empty<FactColumn>().Entity.FindMethod("FactColumnsLoadByFactName").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FactColumn { Workarea = Workarea };
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

        /// <summary>
        /// �������� ����� �� ����
        /// </summary>
        /// <param name="actualDate">����</param>
        /// <param name="columnId">������������� �������</param>
        /// <param name="valueId">������������� ��������</param>
        /// <param name="entityId">��� �������</param>
        /// <returns></returns>
        public List<FactValue> GetFactValues(DateTime actualDate, int columnId, int valueId, int entityId)
        {
            FactValue item;
            List<FactValue> collection = null;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    collection = new List<FactValue>();
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.ActualDate, SqlDbType.DateTime).Value = actualDate;
                        cmd.Parameters.Add(GlobalSqlParamNames.ColumnId, SqlDbType.Int).Value = columnId;
                        cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = valueId;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = entityId;
                        cmd.CommandText =
                            Workarea.Empty<FactName>().Entity.FindMethod("FactValuesLoadForDateByEntryIdColumnId").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FactValue { Workarea = Workarea };
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
        /// <summary>
        /// ��������� �������� ��� ����� ��� �������
        /// </summary>
        /// <param name="entityId">������������� �������</param>
        /// <param name="kindType">������������� ���������� �������</param>
        /// <param name="columnId">������������� ������� �����</param>
        /// <param name="ds">���� ������</param>
        /// <param name="de">���� ���������</param>
        /// <returns></returns>
        public List<FactDate> GetCollectionFactDates(int entityId, int kindType, int columnId, DateTime ds, DateTime de)
        {
            FactDate item;
            List<FactDate> collection = null;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    collection = new List<FactDate>();
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.ColumnId, SqlDbType.Int).Value = columnId;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = entityId;
                        cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindType;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = ds;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = de;
                        cmd.CommandText = Workarea.Empty<FactName>().Entity.FindMethod("FactDateGetRange").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FactDate { Workarea = Workarea };
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

        /// <summary>
        /// ������������ �������� �����
        /// </summary>
        /// <param name="entityId">������������� �������</param>
        /// <param name="kindType">������������� ���������� �������</param>
        /// <param name="columnId">������������� �������</param>
        /// <returns></returns>
        public List<FactDate> GetCollectionFactDatesMax(int entityId, int kindType, int columnId)
        {
            FactDate item;
            List<FactDate> collection = null;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    collection = new List<FactDate>();
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.ColumnId, SqlDbType.Int).Value = columnId;
                        cmd.Parameters.Add(GlobalSqlParamNames.KindId, SqlDbType.Int).Value = kindType;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.Int).Value = entityId;
                        cmd.Parameters.Add(GlobalSqlParamNames.ActualDate, SqlDbType.DateTime).Value = DBNull.Value;
                        cmd.CommandText =
                            Workarea.Empty<FactName>().Entity.FindMethod("FactDatesGetMaxForDate").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();


                        while (reader.Read())
                        {
                            item = new FactDate { Workarea = Workarea };
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
        #endregion
    }
}
