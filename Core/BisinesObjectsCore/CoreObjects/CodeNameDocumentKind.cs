using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace BusinessObjects
{
    /// <summary>
    /// Допустимый подтип документов для кодов
    /// </summary>
    public sealed class CodeNameDocumentKind
        : BaseCoreObject
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CodeNameDocumentKind()
            : base()
        {
            EntityId = (short)WhellKnownDbEntity.CodeNameDocumentKind;
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

        /// <summary>
        /// Частичная запись XML данных
        /// </summary>
        /// <param name="writer">Объект записи XML данных</param>
        protected override void WritePartialXml(XmlWriter writer)
        {
            base.WritePartialXml(writer);

            if (_elementId!=0)
                writer.WriteAttributeString(GlobalPropertyNames.ElementId, XmlConvert.ToString(_elementId));
            if (_entityKindId != 0)
                writer.WriteAttributeString(GlobalPropertyNames.EntityKindId, XmlConvert.ToString(_entityKindId));
            if (!string.IsNullOrEmpty(Name))
                writer.WriteAttributeString(GlobalPropertyNames.Name, Name);
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
            if (reader.GetAttribute(GlobalPropertyNames.EntityKindId) != null)
                _entityKindId = XmlConvert.ToInt32(reader.GetAttribute(GlobalPropertyNames.EntityKindId));
            if (reader.GetAttribute(GlobalPropertyNames.Name) != null)
                Name = reader[GlobalPropertyNames.Name];
        }
        #endregion

        /// <summary>Загрузка данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        /// <param name="endInit">Признак окончания загрузки</param>
        public override void Load(SqlDataReader reader, bool endInit = true)
        {
            base.Load(reader, false);
            try
            {
                _elementId = reader.GetInt32(9);
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
            SqlParameter prm = new SqlParameter(GlobalSqlParamNames.ElementId, SqlDbType.Int) { IsNullable = false, Value = _elementId };
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
        public static List<CodeNameDocumentKind> GetCollection(Workarea wa, int columnId)
        {
            CodeNameDocumentKind item;
            List<CodeNameDocumentKind> collection = new List<CodeNameDocumentKind>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = columnId;
                        cmd.CommandText = wa.Empty<CodeName>().Entity.FindMethod("CodeNameDocumentKindLoadByElementId").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new CodeNameDocumentKind { Workarea = wa };
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