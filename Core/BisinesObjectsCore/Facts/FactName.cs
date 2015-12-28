using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    internal struct FactNameStruct
    {
        /// <summary>Идентификатор системного объекта которому принадлежит факт</summary>
        public short ToEntityId;
    }
    /// <summary>Наименование факта</summary>
    public sealed class FactName : BaseCore<FactName>
    {
        #region Константы типов и подтипов
        // ReSharper disable InconsistentNaming
        /// <summary>Наименование факта, соответствует значению 1</summary>
        public const int KINDVALUE_FACT = 1;
        /// <summary>Наименование свойства, соответствует значению 2</summary>
        public const int KINDVALUE_PROPERTY = 2;

        /// <summary>Наименование факта, соответствует значению 1114113</summary>
        public const int KINDID_FACT = 1114113;
        /// <summary>Наименование свойства, соответствует значению 1114114</summary>
        public const int KINDID_PROPERTY = 1114114;
        // ReSharper restore InconsistentNaming
        #endregion

        /// <summary>Конструктор</summary>
        public FactName():base()
        {
            EntityId = (short)WhellKnownDbEntity.FactName;
        }
        /// <summary>Клонирование объекта</summary>
        /// <param name="endInit">Закончить инициализацию</param>
        /// <returns></returns>
        protected override FactName Clone(bool endInit)
        {
            FactName obj = base.Clone(endInit);
            obj.ToEntityId = ToEntityId;

            if (endInit)
                OnEndInit();
            return obj;
        }
        #region Свойства
        private short _toEntityId;
        /// <summary>Идентификатор системного объекта которому принадлежит факт</summary>
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
        /// <summary>Тип элемента</summary>
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

        #region Сериализация
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

        #region Состояние
        FactNameStruct _baseStruct;
        /// <summary>Сохранить текущее состояние объекта</summary>
        /// <param name="overwrite">Выполнить перезапись</param>
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

        #region База данных
        /// <summary>Загрузить</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Закончить инициализацию объекта</param>
        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _toEntityId = reader.GetInt16(17);
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
            
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt) { IsNullable = false, Value = _toEntityId };
            sqlCmd.Parameters.Add(prm);
        }       
        #endregion

        #region Дополнительные методы
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
        /// Коллекция колонок факта
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
        /// Значение факта на дату
        /// </summary>
        /// <param name="actualDate">Дата</param>
        /// <param name="columnId">Идентификатор колонки</param>
        /// <param name="valueId">Идентификатор элемента</param>
        /// <param name="entityId">Тип объекта</param>
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
        /// Коллекция значений дат факта для объекта
        /// </summary>
        /// <param name="entityId">Идентификатор объекта</param>
        /// <param name="kindType">Идентификатор системного объекта</param>
        /// <param name="columnId">Идентификатор колонки факта</param>
        /// <param name="ds">Дата начала</param>
        /// <param name="de">Дата окончания</param>
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
        /// Максимальные значения факта
        /// </summary>
        /// <param name="entityId">Идентификатор объекта</param>
        /// <param name="kindType">Идентификатор системного объекта</param>
        /// <param name="columnId">Идентификатор колонки</param>
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
