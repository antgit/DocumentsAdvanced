using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// �������� �������
    /// </summary>
    /// <remarks>
    /// � ������������ � ������� <see
    /// cref="M:BusinessObjects.EntityProperty.Validate">�������� �������</see>
    /// ������������� �������� ��������:
    /// <list type="bullet">
    /// <item>
    /// <description>������������� ������</description></item>
    /// <item>
    /// <description>������������� ������������ ��������</description></item>
    /// <item>
    /// <description>������������� �����</description></item></list>
    /// </remarks>
    public sealed class EntityProperty: BaseCoreObject
    {
        /// <summary>
        /// �����������
        /// </summary>
        public EntityProperty():base()
        {
            
        }
        #region ��������
        private int _groupId;
        /// <summary>
        /// ������������� ������ �������
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.Group">������</seealso>
        public int GroupId
        {
            get
            {
                return _groupId;
            }
            set
            {
                if (value == _groupId) return;
                OnPropertyChanging(GlobalPropertyNames.GroupId);
                _groupId = value;
                OnPropertyChanged(GlobalPropertyNames.GroupId);
            }
        }

        private EntityPropertyGroup _group;
        /// <summary>
        /// ������
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.GroupId">������������� ������
        /// �������</seealso>
        public EntityPropertyGroup Group
        {
            get
            {
                if (_groupId == 0)
                    return null;
                if (_group == null)
                    _group = Workarea.Cashe.GetCasheData<EntityPropertyGroup>().Item(_groupId);
                else if (_group.Id != _groupId)
                    _group = Workarea.Cashe.GetCasheData<EntityPropertyGroup>().Item(_groupId);
                return _group;
            }
            set
            {
                if (_group == value) return;
                OnPropertyChanging(GlobalPropertyNames.Group);
                _group = value;
                _groupId = _group == null ? 0 : _group.Id;
                OnPropertyChanged(GlobalPropertyNames.Group);
            }
        } 

        private int _propertyId;
        /// <summary>
        /// ������������� ������������ ��������
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.Property">��������</seealso>
        public int PropertyId
        {
            get { return _propertyId; }
            set
            {
                if (value == _propertyId) return;
                OnPropertyChanging(GlobalPropertyNames.PropertyId);
                _propertyId = value;
                OnPropertyChanged(GlobalPropertyNames.PropertyId);
            }
        }

        private EntityPropertyName _property;
        /// <summary>
        /// ������������ ��������
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.PropertyId">�������������
        /// ������������ ��������</seealso>
        public EntityPropertyName Property
        {
            get
            {
                if (_propertyId == 0)
                    return null;
                if (_property == null)
                    _property = Workarea.Cashe.CollectionEntityPropertyNames().Find(f=>f.Id==_propertyId);
                else if (_group.Id != _groupId)
                    _property = Workarea.Cashe.CollectionEntityPropertyNames().Find(f => f.Id == _propertyId);
                return _property;
            }
            set
            {
                if (_property == value) return;
                OnPropertyChanging(GlobalPropertyNames.Property);
                _property = value;
                _propertyId = _property == null ? 0 : _property.Id;
                OnPropertyChanged(GlobalPropertyNames.Property);
            }
        }

        private int _entityId;
        /// <summary>
        /// ������������� ���������� ������� 
        /// </summary>
        public int EntityId
        {
            get { return _entityId; }
            set
            {
                if (value == _entityId) return;
                OnPropertyChanging(GlobalPropertyNames.EntityId);
                _entityId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityId);
            }
        }
        private EntityType _entity;
        /// <summary>��� ��������</summary>
        public EntityType Entity
        {
            get
            {
                if (_entityId == 0)
                    return null;
                if (_entity == null)
                    _entity = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _entityId);
                else if (_entity.Id != _entityId)
                    _entity = (this as ICoreObject).Workarea.CollectionEntities.Find(f => f.Id == _entityId);
                return _entity;
            }
            set
            {
                if (_entity == value) return;
                OnPropertyChanging(GlobalPropertyNames.Entity);
                _entity = value;
                _entityId = _entity == null ? 0 : _entity.Id;
                OnPropertyChanged(GlobalPropertyNames.Entity);
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

            if (_groupId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.GroupId, Convert.ToString(_groupId));
            if (_propertyId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.PropertyId, Convert.ToString(_propertyId));
            if (_entityId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.EntityId, Convert.ToString(_entityId));
        }

        /// <summary>
        /// ��������� ������ XML ������
        /// </summary>
        /// <param name="reader">������ ������ XML ������</param>
        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.GroupId) != null)
                _groupId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.GroupId));
            if (reader.GetAttribute(GlobalPropertyNames.PropertyId) != null)
                _propertyId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.PropertyId));
            if (reader.GetAttribute(GlobalPropertyNames.EntityId) != null)
                _entityId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.EntityId));
        }
        #endregion

        /// <summary>��������� ������ � ���� ������</summary>
        /// <remarks>� ����������� �� ��������� ������� <see cref="EntityKind.IsNew"/> 
        /// ����������� �������� ��� ���������� �������</remarks>
        public void Save()
        {
            Validate();
            if (IsNew)
                Create(Workarea.FindMethod("EntityPropertyInsertUpdate").FullName);
            else
                Update(Workarea.FindMethod("EntityPropertyInsertUpdate").FullName, true);
        }

        public override void Load(int value)
        {
            Load(value, Workarea.FindMethod("EntityPropertyLoad").FullName);
        }
        /// <summary>�������� ������������ ������� ��������� �����������</summary>
        /// <returns><c>true</c> ���� �������� ������ �������, <c>false</c> � ��������� ������</returns>
        public override void Validate()
        {
            base.Validate();
            if (_groupId==0)
                throw new ValidateException("�� ������� ������ ��������");
            if (_propertyId == 0)
                throw new ValidateException("�� ������ ������������� ��������");
            if (_entityId == 0)
                throw new ValidateException("�� ������ ��������� ������");
        }
        /// <summary>���������</summary>
        /// <param name="reader">������ SqlDataReader</param>
        /// <param name="endInit">��������� ������������� �������</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _groupId = reader.GetInt32(9);
                _propertyId = reader.GetInt32(10);
                _entityId = reader.GetInt16(11);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt) { IsNullable = false, Value = _entityId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PropertyId, SqlDbType.Int) { IsNullable = false, Value = _propertyId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.GroupId, SqlDbType.Int) {IsNullable = false, Value = _groupId};
            sqlCmd.Parameters.Add(prm);
        }

    }
}