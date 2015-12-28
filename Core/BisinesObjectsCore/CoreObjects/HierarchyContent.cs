using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>Структура содержимого иерархии для сохранения/востановления состояния</summary>
    internal struct HierarchyContentStruct
    {
        /// <summary>Идентификатор сущности</summary>
        public int ElementId;
        /// <summary>Идентификатор иерархии</summary>
        public int HierarchyId;
        /// <summary>Признак сортировки</summary>
        public int SortOrder;
    }
    /// <summary>Содержимое иерархии</summary>
    /// <remarks>
    /// Свойство <see cref="BusinessObjects.BaseCore<T>.SubKindId"/> соответствует основному типу содержимого.
    /// Свойство <see cref="BusinessObjects.BaseCore<T>.EntityId"/> соответствует константе-значению типа содержимого иерархии.
    /// </remarks>
    public sealed class HierarchyContent : BaseCore<HierarchyContent>, IEquatable<HierarchyContent>
    {
        bool IEquatable<HierarchyContent>.Equals(HierarchyContent other)
        {
            return Workarea == other.Workarea & Id == other.Id
                   & DbSourceId == other.DbSourceId
                   & Entity == other.Entity;
        }
        /// <summary>Конструктор</summary>
        public HierarchyContent(): base()
        {
            EntityId = (short)WhellKnownDbEntity.HierarchyContent;
            base.StateId = 1;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
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
        /// <summary>Идентификатор состояния</summary>
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

        #region Свойства
        private short _toEntityId;
        /// <summary>Идентификатор системного типа содержимого</summary>
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
        /// <summary>Системный тип содержимого</summary>
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
        /// <summary>Идентификатор иерархии</summary>
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
        /// <summary>Идентификатор элемента</summary>
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
        /// <summary>Сортировка</summary>
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

        #region Сериализация

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
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
        /// Частичное чтение XML данных
        /// </summary>
        /// <param name="reader">Объект чтения XML данных</param>
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

        #region Состояние
        HierarchyContentStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
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

        /// <summary>Востановить текущее состояние объекта</summary>
        /// <remarks>Востановление состояние возможно только после выполнения сосхранения состояния</remarks>
        public override void RestoreState()
        {
            base.RestoreState();
            ElementId = _baseStruct.ElementId;
            HierarchyId = _baseStruct.HierarchyId;
            SortOrder = _baseStruct.SortOrder;
            IsChanged = false;
        }
        #endregion

        /// <summary>Проверка соответствия объекта системным требованиям</summary>
        /// <returns><c>true</c> если проверка прошла успешно, <c>false</c> в противном случае</returns>
        public override void Validate()
        {
            CancelEventArgs e = new CancelEventArgs();
            if (e.Cancel)
                return;
            if (_elementId == 0)
                throw new ValidateException("Не задан элемент");
            if (_hierarchyId == 0)
                throw new ValidateException("Не указана иерархия");
            if (DatabaseId == 0)
                DatabaseId = (this as ICoreObject).Workarea.MyBranche.Id;
            OnValidated();
        }
        /// <summary>Преобразование содержимого иеархии(группы) в основной объект</summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <returns></returns>
        public T ConvertToTypedObject<T>() where T : class, ICoreObject, new()
        {
            T val = Workarea.Cashe.GetCasheData<T>().Item(ElementId);
            return val;
        }
        #region База данных
        /// <summary>Загрузить экземпляр из базы данных</summary>
        /// <param name="reader">Объект SqlDataReader</param>
        /// <param name="endInit">Признак окончания загрузки</param>
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
                throw new ObjectReaderException("Ошибка чтения объекта из базы данных", ex);
            }
            if (!endInit) return;
            OnEndInit();
        }
        /// <summary>Установить значения параметров для комманды создания</summary>
        /// <param name="sqlCmd">Комманда создания</param>
        /// <param name="insertCommand">Является ли комманда операцией создания</param>
        /// <param name="validateVersion">Выполнять проверку версии</param>
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

        /// <summary>Копирование элемента в другую иерархию</summary>
        /// <param name="hierarchyId">Идентификатор иерархии</param>
        /// <param name="toDbEntityId">Идентификатор системного типа, указывается только для документов и соответствует значению 20</param>
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

        /// <summary>Перемещение элемента в другую иерархию</summary>
        /// <param name="hierarchyId">Идентификатор иерархии в которую помешается данный объект</param>
        /// <param name="toDbEntityId">Идентификатор системного типа, указывается только для документов и соответствует значению 20</param>
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
        ///// Поиск иерархии по типу
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
