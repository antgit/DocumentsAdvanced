using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>��������� ����������� �������� ��� ����������/������������� ���������</summary>
    internal struct HierarchyContentStruct
    {
        /// <summary>������������� ��������</summary>
        public int ElementId;
        /// <summary>������������� ��������</summary>
        public int HierarchyId;
        /// <summary>������� ����������</summary>
        public int SortOrder;
    }
    /// <summary>���������� ��������</summary>
    /// <remarks>
    /// �������� <see cref="BusinessObjects.BaseCore<T>.SubKindId"/> ������������� ��������� ���� �����������.
    /// �������� <see cref="BusinessObjects.BaseCore<T>.EntityId"/> ������������� ���������-�������� ���� ����������� ��������.
    /// </remarks>
    public sealed class HierarchyContent : BaseCore<HierarchyContent>, IEquatable<HierarchyContent>
    {
        bool IEquatable<HierarchyContent>.Equals(HierarchyContent other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>�����������</summary>
        public HierarchyContent(): base()
        {
            EntityId = (short)WhellKnownDbEntity.HierarchyContent;
            base.StateId = 1;
        }
        /// <summary>������������ �������</summary>
        /// <param name="endInit">��������� �������������</param>
        /// <returns></returns>
        protected override HierarchyContent Clone(bool endInit)
        {
            HierarchyContent obj = base.Clone(false);
            obj.ElementId = ElementId;
            obj.HierarchyId = HierarchyId;
            obj.SortOrder = SortOrder;
            obj.ToEntityId = ToEntityId;
            if (endInit)
                OnEndInit();
            return obj;
        }
        /// <summary>������������� ���������</summary>
        public override int StateId
        {
            get
            {
                return base.StateId;
            }
            set
            {

            }
        }

        #region ��������
        private short _toEntityId;
        /// <summary>������������� ���������� ���� �����������</summary>
        public short ToEntityId
        {
            get { return _toEntityId; }
            set
            {
                if (_toEntityId == value) return;
                OnPropertyChanging(GlobalPropertyNames.ToEntityId);
                _toEntityId = value;
                OnPropertyChanged(GlobalPropertyNames.ToEntityId);
            }
        }

        private EntityType _toEntity;
        /// <summary>��������� ��� �����������</summary>
        public EntityType ToEntity
        {
            get
            {
                if (_toEntityId == 0)
                    return null;
                if (_toEntity == null)
                    _toEntity = Workarea.CollectionEntities.Find(f => f.Id == _toEntityId);
                else if (_toEntity.Id != _toEntityId)
                    _toEntity = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _toEntityId);
                return _toEntity;
            }
            
        }
	
        private int _hierarchyId;
        /// <summary>������������� ��������</summary>
        public int HierarchyId
        {
            get { return _hierarchyId; }
            set
            {
                if (value == _hierarchyId) return;
                OnPropertyChanging(GlobalPropertyNames.HierarchyId);
                _hierarchyId = value;
                OnPropertyChanged(GlobalPropertyNames.HierarchyId);
            }
        }

        private int _elementId;
        /// <summary>������������� ��������</summary>
        public int ElementId
        {
            get { return _elementId; }
            set
            {
                if (value == _elementId) return;
                OnPropertyChanging(GlobalPropertyNames.ElementId);
                _elementId = value;
                OnPropertyChanged(GlobalPropertyNames.ElementId);
            }
        }

        private int _sortOrder;
        /// <summary>����������</summary>
        public int SortOrder
        {
            get { return _sortOrder; }
            set
            {
                if (value == _sortOrder) return;
                OnPropertyChanging(GlobalPropertyNames.SortOrder);
                _sortOrder = value;
                OnPropertyChanged(GlobalPropertyNames.SortOrder);
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

            if (_elementId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ElementId, XmlConvert.ToString(_elementId));
            if (_hierarchyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.HierarchyId, XmlConvert.ToString(_hierarchyId));
            if (_sortOrder != 0)
                writer.WriteAttributeString(GlobalPropertyNames.SortOrder, XmlConvert.ToString(_sortOrder));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ElementId) != null)
                _elementId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ElementId));
            if (reader.GetAttribute(GlobalPropertyNames.HierarchyId) != null)
                _hierarchyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.HierarchyId));
            if (reader.GetAttribute(GlobalPropertyNames.SortOrder) != null)
                _sortOrder = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.SortOrder));
        }
        #endregion

        #region ���������
        HierarchyContentStruct _baseStruct;
        /// <summary>��������� ������� ��������� �������</summary>
        /// <param name="overwrite">��������� ����������</param>
        public override bool SaveState(bool overwrite)
        {

            if (base.SaveState(overwrite))
            {
                _baseStruct = new HierarchyContentStruct
                                  {
                                      ElementId = _elementId,
                                      HierarchyId = _hierarchyId,
                                      SortOrder = _sortOrder
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
            ElementId = _baseStruct.ElementId;
            HierarchyId = _baseStruct.HierarchyId;
            SortOrder = _baseStruct.SortOrder;
            IsChanged = false;
        }
        #endregion

        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (e.Cancel)
                return;
            if (_elementId == 0)
                throw new ValidateException("�� ����� �������");
            if (_hierarchyId == 0)
                throw new ValidateException("�� ������� ��������");
            if (DatabaseId == 0)
                DatabaseId = (this as ICoreObject).Workarea.MyBranche.Id;
            OnValidated();
        }
        /// <summary>�������������� ����������� �������(������) � �������� ������</summary>
        /// <typeparam name="T">��� �������</typeparam>
        /// <returns></returns>
        public T ConvertToTypedObject<T>() where T : class, ICoreObject, new()
        {
            T val = Workarea.Cashe.GetCasheData<T>().Item(ElementId);
            return val;
        }
        #region ���� ������
        /// <summary>��������� ��������� �� ���� ������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">������� ��������� ��������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _hierarchyId = reader.GetInt32(17);
                _elementId = reader.GetInt32(18);
                _toEntityId = reader.GetInt16(19);
                _sortOrder = reader.GetInt32(20);
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
        /// <param name="insertCommand">�������� �� �������� ��������� ��������</param>
        /// <param name="validateVersion">��������� �������� ������</param>
        protected override void SetParametersToInsertUpdate(SqlCommand sqlCmd, bool insertCommand, bool validateVersion = true)
        {
            base.SetParametersToInsertUpdate(sqlCmd, insertCommand, validateVersion);
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.HierarchyId, SqlDbType.Int) {IsNullable = false, Value = _hierarchyId};
            sqlCmd.Parameters.Add(prm);
            
            prm = new SqlParameter(GlobalSqlParamNames.ElementId, SqlDbType.Int) {IsNullable = false, Value = _elementId};
            sqlCmd.Parameters.Add(prm);
            prm = new SqlParameter(GlobalSqlParamNames.ElementKind, SqlDbType.SmallInt) {IsNullable = false, Value = _toEntityId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.OrderNo, SqlDbType.Int) {IsNullable = false, Value = _sortOrder};
            sqlCmd.Parameters.Add(prm);
        }
        #endregion

        /// <summary>����������� �������� � ������ ��������</summary>
        /// <param name="hierarchyId">������������� ��������</param>
        /// <param name="toDbEntityId">������������� ���������� ����, ����������� ������ ��� ���������� � ������������� �������� 20</param>
        public List<HierarchyContent> Copy(int hierarchyId, int? toDbEntityId=null)
        {
            List<HierarchyContent> collection = new List<HierarchyContent>();
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Workarea.Empty<Hierarchy>().Entity.FindMethod("HierarchyContent.Copy").FullName;  //"[Hierarchy].[HierarchyContentCopy]";

                        cmd.Parameters.Add(GlobalSqlParamNames.ContentId, SqlDbType.Int).Value = Id;                        
                        cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if(toDbEntityId.HasValue && toDbEntityId.Value==20)
                            cmd.Parameters.Add("@ToDbEntityId", SqlDbType.Int).Value = toDbEntityId.Value;
                        if (toDbEntityId.HasValue && toDbEntityId.Value != 20)
                            cmd.Parameters.Add("@ToDbEntityId", SqlDbType.Int).Value = toDbEntityId.Value;
                        SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                        prm.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HierarchyContent item = new HierarchyContent { Workarea = Workarea };
                            item.Load(reader);
                            collection.Add(item);
                        }
                        reader.Close();

                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return collection;
        }

        /// <summary>����������� �������� � ������ ��������</summary>
        /// <param name="hierarchyId">������������� �������� � ������� ���������� ������ ������</param>
        /// <param name="toDbEntityId">������������� ���������� ����, ����������� ������ ��� ���������� � ������������� �������� 20</param>
        public void Move(int hierarchyId, int? toDbEntityId = null)
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText =
                            Workarea.Empty<Hierarchy>().Entity.FindMethod("HierarchyContent.Move").FullName;

                        cmd.Parameters.Add(GlobalSqlParamNames.ContentId, SqlDbType.Int).Value = Id;                       
                        cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = hierarchyId;
                        if (toDbEntityId.HasValue && toDbEntityId.Value == 20)
                            cmd.Parameters.Add("@ToDbEntityId", SqlDbType.Int).Value = toDbEntityId.Value;
                        else
                        {
                            if (toDbEntityId.HasValue && toDbEntityId.Value != 20)
                            {
                                cmd.Parameters.Add("@ToDbEntityId", SqlDbType.Int).Value = toDbEntityId.Value;
                            }
                        }
                        SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
                        prm.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();
                        object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
                        if (retval == null)
                            throw new SqlReturnException(Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                        if ((int)retval != 0)
                            throw new DatabaseException(Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);

                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }

        ///// <summary>
        ///// ����� �������� �� ����
        ///// </summary>
        ///// <returns></returns>
        //internal static int FindByContentId(Workarea wa, int entityId, int elementId)
        //{
        //    using (SqlConnection cnn = wa.GetDatabaseConnection())
        //    {
        //        if (cnn == null) return 0;

        //        try
        //        {
        //            using (SqlCommand cmd = cnn.CreateCommand())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText =
        //                    wa.Empty<Hierarchy>().Entity.FindMethod("HierarchyContent.FindByContentId").FullName;

        //                cmd.Parameters.Add(GlobalSqlParamNames.ToEntityId, SqlDbType.Int).Value = entityId;
        //                cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = elementId;
        //                SqlParameter prm = cmd.Parameters.Add(GlobalSqlParamNames.Return, SqlDbType.Int);
        //                prm.Direction = ParameterDirection.ReturnValue;
        //                cmd.ExecuteNonQuery();
        //                object retval = cmd.Parameters[GlobalSqlParamNames.Return].Value;
        //                if (retval == null)
        //                    throw new SqlReturnException(wa.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

        //                if ((int)retval != 0)
        //                    throw new DatabaseException(wa.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);

        //            }
        //        }
        //        finally
        //        {
        //            cnn.Close();
        //        }
        //    }
        //}
    }


}
