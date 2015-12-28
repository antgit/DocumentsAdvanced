using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects
{
    /// <summary>
    /// Представление текщих значений фактов
    /// </summary>
    public sealed class FactView
    {
        /// <summary>Идентификатор наименофания факта</summary>
        public int FactNameId { get; private set; }
        /// <summary>Наименование факта</summary>
        public string FactName { get; private set; }
        /// <summary>Код наименования факта</summary>
        public string FactNameCode { get; private set; }
        /// <summary>Идентификатор колонки факта</summary>
        public int ColumnId { get; private set; }
        /// <summary>Наименование колонки факта</summary>
        public string ColumnName { get; private set; }
        /// <summary>Код колонки</summary>
        public string ColumnCode { get; private set; }
        /// <summary>Тип колонки</summary>
        public int ColumnKind { get; private set; }
        /// <summary>Идентификатор даты</summary>
        public int? DateId { get; private set; }
        public DateTime? ActualDate { get; private set; }
        /// <summary>Идентификатор значения</summary>
        public int ValueId { get; private set; }
        /// <summary>Значение "Строка"</summary>
        public string ValueString { get; private set; }
        /// <summary>Значение "Целое"</summary>
        public int? ValueInt { get; private set; }
        /// <summary>Значение "Денежное"</summary>
        public decimal? ValueMoney { get; private set; }
        /// <summary>Значение "Вещественное"</summary>
        public decimal? ValueDecimal { get; private set; }
        /// <summary>Значение "Дата"</summary>
        public DateTime? ValueDate { get; private set; }
        /// <summary>Значение "Логика"</summary>
        public bool? ValueBit { get; private set; }
        /// <summary>Значение "Гуид"</summary>
        public Guid? ValueGuid { get; private set; }
        /// <summary>Строковое представление значения</summary>
        public string Value { get; private set; }
        /// <summary>Ссылочное значение 1</summary>
        public int? ValueRef1 { get; private set; }
        /// <summary>Ссылочное значение 2</summary>
        public int? ValueRef2 { get; private set; }
        /// <summary>Ссылочное значение 3</summary>
        public int? ValueRef3 { get; private set; }
        /// <summary>Тип факта 1 - факт, 2 - свойство</summary>
        public int FactNameKindValue { get; private set; }
        

        public void Load(SqlDataReader reader)
        {
            FactNameId = reader.GetInt32(0);
            FactName = reader.GetString(1);
            FactNameCode = reader.IsDBNull(2)? string.Empty: reader.GetString(2);
            ColumnId = reader.GetInt32(3);
            ColumnName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            ColumnCode = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            ColumnKind = reader.GetInt32(6);
            if(!reader.IsDBNull(7))
            {
                DateId = reader.GetInt32(7);
            }
            if (!reader.IsDBNull(8))
            {
                ActualDate = reader.GetDateTime(8);
            }
            ValueId = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
            if (!reader.IsDBNull(10))
            {
                ValueString = reader.GetString(10);
            }
            if (!reader.IsDBNull(11))
            {
                ValueInt = reader.GetInt32(11);
            }
            if (!reader.IsDBNull(12))
            {
                ValueMoney = reader.GetDecimal(12);
            }
            if (!reader.IsDBNull(13))
            {
                ValueDate = reader.GetDateTime(13);
            }
            if (!reader.IsDBNull(14))
            {
                ValueBit = reader.GetBoolean(14);
            }
            if (!reader.IsDBNull(15))
            {
                ValueDecimal = reader.GetDecimal(15);
            }
            if (!reader.IsDBNull(16))
            {
                ValueGuid = reader.GetGuid(16);
            }
            Value = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
            ValueRef1 = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
            ValueRef2 = reader.IsDBNull(19) ? 0 : reader.GetInt32(19);
            ValueRef3 = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
            FactNameKindValue = reader.GetInt32(21);
        }
    }

    internal static class FactHelper
    {
        
        public static List<FactView> GetCollectionFactView(Workarea wa, int entityId, int kindId)
        {
            FactView item;
            List<FactView> collection = new List<FactView>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = entityId;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = kindId;
                        cmd.CommandText = "Fact.GetFactTableData";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FactView();
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
        
        public static List<FactView> GetCollectionFactView(Workarea wa, int entityId, int kindId, int entityKind)
        {
            FactView item;
            List<FactView> collection = new List<FactView>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = entityId;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityId, SqlDbType.SmallInt).Value = kindId;
                        cmd.Parameters.Add(GlobalSqlParamNames.EntityKind, SqlDbType.Int).Value = entityKind;
                        cmd.CommandText = "Fact.GetFactTableData2";
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FactView();
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
        /// Коллекция наименований фактов для системного объекта
        /// </summary>
        /// <param name="toEntityId">Идентификатор системного типа</param>
        /// <returns></returns>
        public static List<FactName> GetFactNames(Workarea wa, short toEntityId)
        {
            FactName item;
            List<FactName> collection = new List<FactName>();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;
                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(GlobalSqlParamNames.ToEntityId, SqlDbType.SmallInt).Value = toEntityId;
                        cmd.CommandText = wa.Empty<FactName>().Entity.FindMethod("FactNamesLoadByEntityId").FullName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new FactName { Workarea = wa };
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