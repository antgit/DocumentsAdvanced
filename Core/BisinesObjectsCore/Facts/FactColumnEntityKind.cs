using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Тип колонки
    /// </summary>
    public sealed class FactColumnEntityKind
        : BaseCoreObject
    {
        public FactColumnEntityKind()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.FactColumnEntityKind;
        }

        #region Свойства
        private int _columnId;
        /// <summary>
        /// Идентификатор колонки
        /// </summary>
        public int ColumnId
        {
// ReSharper disable UnusedMember.Global
            get { return _columnId; }
// ReSharper restore UnusedMember.Global
            set
            {
                if (value == _columnId) return;
                OnPropertyChanging(GlobalPropertyNames.ColumnId);
                _columnId = value;
                OnPropertyChanged(GlobalPropertyNames.ColumnId);
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
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
        /// <summary>
        /// Наименование
        /// </summary>
        /// <value>Значение соответствует наименованию подтипа системного объекта</value>
        public string Name { get; private set; }
// ReSharper restore MemberCanBePrivate.Global
// ReSharper restore UnusedAutoPropertyAccessor.Global
        
        #endregion

        #region Сериализация
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_columnId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ColumnId, XmlConvert.ToString(_columnId));
            if (_entityKindId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.EntityKindId, XmlConvert.ToString(_entityKindId));
            if (!string.IsNullOrEmpty(Name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, Name);
        }

        protected override void ReadPartialXml(XmlReader reader)
        {
            base.ReadPartialXml(reader);

            if (reader.GetAttribute(GlobalPropertyNames.ColumnId) != null)
                _columnId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.ColumnId));
            if (reader.GetAttribute(GlobalPropertyNames.EntityKindId) != null)
                _entityKindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.EntityKindId));
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                Name = reader.GetAttribute(GlobalPropertyNames.Name);
        }
        #endregion

        public override void Load(SqlDataReader reader, bool endInit=true)
        {
            base.Load(reader, false);
            try
            {
                _columnId = reader.GetInt32(9);
                _entityKindId = reader.GetInt32(10);
                Name = reader.GetString(11);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ColumnId, SqlDbType.Int) {IsNullable = false, Value = _columnId};
            sqlCmd.Parameters.Add(prm);

            prm = new SqlParameter(GlobalSqlParamNames.EntityKindId, SqlDbType.Int) { IsNullable = false, Value = _entityKindId };
            sqlCmd.Parameters.Add(prm);
        }
        /// <summary>
        /// Коллекция системных подтипов для колонки
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="columnId">Идентификатор колонки</param>
        /// <returns></returns>
        public static List<FactColumnEntityKind> GetCollection(Workarea wa, int columnId)
        {
            FactColumnEntityKind item;
            List<FactColumnEntityKind> collection = new List<FactColumnEntityKind>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = columnId;
                        cmd.CommandText = wa.Empty<FactColumn>().Entity.FindMethod("FactColumnEntityKindsLoadByColumn").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FactColumnEntityKind { Workarea = wa };
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