using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Привязка типов объектов и типов связей
    /// </summary>
    public sealed class ChainKindContentType
        : BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ChainKindContentType()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.ChainKindContentType;
        }

        #region Свойства
        private int _elementId;
        /// <summary>
        /// Идентификатор элемента
        /// </summary>
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

        private int _entityKindId;
        /// <summary>
        /// Идентификатор подтипа системного объекта
        /// </summary>
        public int EntityKindId
        {
            get { return _entityKindId; }
            set
            {
                if (value == _entityKindId) return;
                OnPropertyChanging(GlobalPropertyNames.EntityKindId);
                _entityKindId = value;
                OnPropertyChanged(GlobalPropertyNames.EntityKindId);
            }
        }


        private int _entityKindIdFrom;
        /// <summary>
        /// Идентификатор подтипа объекта источника
        /// </summary>
        public int EntityKindIdFrom
        {
            get { return _entityKindIdFrom; }
            set
            {
                if (value == _entityKindIdFrom) return;
                OnPropertyChanging(GlobalPropertyNames.EntityKindIdFrom);
                _entityKindIdFrom = value;
                OnPropertyChanged(GlobalPropertyNames.EntityKindIdFrom);
            }
        }
        
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        // ReSharper disable MemberCanBePrivate.Global
        /// <summary>
        /// Наименование
        /// </summary>
        /// <value>Значение соответствует наименованию подтипа системного объекта</value>
        public string Name { get; private set; }
        /// <summary>
        /// Наименование
        /// </summary>
        /// <value>Значение соответствует наименованию главного подтипа системного объекта</value>
        public string NameFrom { get; private set; }
        
        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_elementId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.ElementId, XmlConvert.ToString(_elementId));
            if (_entityKindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.EntityKindId, XmlConvert.ToString(_entityKindId));
            if (!string.IsNullOrEmpty(Name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, Name);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ElementId) != null)
                _elementId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ElementId));
            if (reader.GetAttribute(GlobalPropertyNames.EntityKindId) != null)
                _entityKindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.EntityKindId));
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                Name = reader[GlobalPropertyNames.Name];
        }
        #endregion

        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _elementId = reader.GetInt32(9);
                _entityKindId = reader.GetInt32(10);
                Name = reader.GetString(11);
                _entityKindIdFrom = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                NameFrom = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
            }
            catch (Exception ex)
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ElementId, SqlDbType.Int) { IsNullable = false, Value = _elementId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityKindId, SqlDbType.Int) { IsNullable = false, Value = _entityKindId };
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityKindIdFrom, SqlDbType.Int) { IsNullable = true};
            if(_entityKindIdFrom==0)
            {
                prm.Value = DBNull.Value;
            }
            else
            {
                prm.Value = _entityKindIdFrom;
            }
            sqlCmd.Parameters.Add(prm);
        }
        /// <summary>
        /// Коллекция системных подтипов для колонки
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="columnId">Идентификатор колонки</param>
        /// <returns></returns>
        public static List<ChainKindContentType> GetCollection(Workarea wa, int columnId)
        {
            ChainKindContentType item;
            List<ChainKindContentType> collection = new List<ChainKindContentType>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = columnId;
                        cmd.CommandText = wa.Empty<ChainKind>().Entity.FindMethod("ChainKindContentTypesLoadByElementId").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new ChainKindContentType { Workarea = wa };
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