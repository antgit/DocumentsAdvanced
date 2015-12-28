using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace BusinessObjects
{
    public partial class Workarea
    {
        /// <summary>
        /// Коллекция строковых ресурсов базы данных.
        /// </summary>
        /// <returns></returns>
        public virtual List<ResourceString> GetCollectionResourceString(int cultureId)
        {
            List<ResourceString> coll = new List<ResourceString>();

            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = FindMethod("ResourceStringLoadAll").FullName;
                        cmd.Parameters.Add(GlobalSqlParamNames.CultureId, SqlDbType.Int).Value = cultureId;
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ResourceString item = new ResourceString {Workarea = this};
                            item.Load(reader);
                            coll.Add(item);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return coll;
        }
        /// <summary>
        /// Список доступных схем в базе данных
        /// </summary>
        /// <returns></returns>
        public virtual List<string> GetCollectionSchema()
        {
            List<string> coll = new List<string>();

            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Core.SystemLoadSchema";
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            coll.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return coll;
        }
        /// <summary>
        /// Список хранимых процедур в базе данных
        /// </summary>
        /// <param name="schemaName">Наименование схемы</param>
        /// <param name="withSchemaName">Возвращать полное наименование</param>
        /// <returns>Наименование хранимой процедуры, если указан параметр withSchemaName - возвращается полное наименование со схемой </returns>
        public virtual List<string> GetCollectionProcedures(string schemaName, bool withSchemaName)
        {
            List<string> coll = new List<string>();
            if (string.IsNullOrEmpty(schemaName)) return coll;
            using (SqlConnection cnn = GetDatabaseConnection())
            {
                if (cnn == null)
                    throw new ConnectionException();

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Core.LoadProcedures";
                        cmd.Parameters.Add(GlobalSqlParamNames.Schema, SqlDbType.NVarChar, 128).Value = schemaName;
                        cmd.Parameters.Add(GlobalSqlParamNames.WithSchemaName, SqlDbType.Bit).Value = withSchemaName;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            coll.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return coll;
        }
        
    }
}
