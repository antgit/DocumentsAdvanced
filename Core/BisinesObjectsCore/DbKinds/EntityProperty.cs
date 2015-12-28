using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Свойство объекта
    /// </summary>
    /// <remarks>
    /// В соответствии с методом <see
    /// cref="M:BusinessObjects.EntityProperty.Validate">проверки объекта</see>
    /// обязательнымы являются свойства:
    /// <list type="bullet">
    /// <item>
    /// <description>Идентификатор группы</description></item>
    /// <item>
    /// <description>Идентификатор наименования свойства</description></item>
    /// <item>
    /// <description>Идентификатор языка</description></item></list>
    /// </remarks>
    public sealed class EntityProperty: BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public EntityProperty():base()
        {
            
        }
        #region Свойства
        private int _groupId;
        /// <summary>
        /// Идентификатор группы свойств
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.Group">Группа</seealso>
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
        /// Группа
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.GroupId">Идентификатор группы
        /// свойств</seealso>
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
        /// Идентификатор наименования свойства
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.Property">Свойство</seealso>
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
        /// Наименование свойства
        /// </summary>
        /// <seealso cref="P:BusinessObjects.EntityProperty.PropertyId">Идентификатор
        /// наименования свойства</seealso>
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
        /// Идентификатор системного объекта 
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
        /// <summary>Тип элемента</summary>
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

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
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
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
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

        /// <summary>Сохранить объект в базе данных</summary>
        /// <remarks>В зависимости от состояния объекта <see cref="EntityKind.IsNew"/> 
        /// выполняется создание или обновление объекта</remarks>
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
        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            base.Validate();
            if (_groupId==0)
                throw new ValidateException("Не указана группа свойства");
            if (_propertyId == 0)
                throw new ValidateException("Не указан идентификатор свойства");
            if (_entityId == 0)
                throw new ValidateException("Не указан системный объект");
        }
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt) { IsNullable = false, Value = _entityId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.PropertyId, SqlDbType.Int) { IsNullable = false, Value = _propertyId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.GroupId, SqlDbType.Int) {IsNullable = false, Value = _groupId};
            sqlCmd.Parameters.Add(prm);
        }

    }
}