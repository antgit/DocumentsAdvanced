using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

namespace BusinessObjects
{
    /// <summary>
    /// Вспомогательный класс работы с базой данных
    /// </summary>
    public static class DatabaseHelper
    {
        /// <summary>
        /// Добавление TVP параметра для типа "KeyListId"
        /// </summary>
        /// <param name="cmd">Sql комманда</param>
        /// <param name="values">Значения</param>
        /// <param name="prName">Наименование параметра, значение по умолчанию "@Values"</param>
        public static void AddTvpParamKeyListId(SqlCommand cmd, IEnumerable<int> values, string prName = "@Values")
        {
            List<SqlDataRecord> records = new List<SqlDataRecord>();
            SqlMetaData[] tvpDefinition = { new SqlMetaData(GlobalPropertyNames.Id, SqlDbType.Int) };
            foreach (int prodid in values)
            {
                SqlDataRecord rec = new SqlDataRecord(tvpDefinition);
                rec.SetInt32(0, prodid);
                records.Add(rec);
            }
            cmd.Parameters.Add(prName, SqlDbType.Structured);
            cmd.Parameters[prName].Direction = ParameterDirection.Input;
            cmd.Parameters[prName].TypeName = "KeyListId";
            cmd.Parameters[prName].Value = records;
        }

        /// <summary>
        /// Добавление TVP параметра для типа "KeyListId"
        /// </summary>
        /// <param name="cmd">Sql комманда</param>
        /// <param name="values">Значения</param>
        /// <param name="prName">Наименование параметра, значение по умолчанию "@Values"</param>
        public static void AddTvpParamKeyListGuid(SqlCommand cmd, IEnumerable<Guid> values, string prName = "@Values")
        {
            List<SqlDataRecord> records = new List<SqlDataRecord>();
            SqlMetaData[] tvpDefinition = { new SqlMetaData("Guid", SqlDbType.UniqueIdentifier) };
            foreach (Guid prodid in values)
            {
                SqlDataRecord rec = new SqlDataRecord(tvpDefinition);
                rec.SetGuid(0, prodid);
                records.Add(rec);
            }
            cmd.Parameters.Add(prName, SqlDbType.Structured);
            cmd.Parameters[prName].Direction = ParameterDirection.Input;
            cmd.Parameters[prName].TypeName = "KeyListGuid";
            cmd.Parameters[prName].Value = records;
        }
        /// <summary>
        /// Наименование рабочей станции
        /// </summary>
        /// <remarks>Данные соответствуют запросу SELECT HOST_NAME()</remarks>
        /// <param name="wa">Рабочая область</param>
        /// <returns></returns>
        public static string GetHostName(Workarea wa)
        {
            string res = string.Empty;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT HOST_NAME()";
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
                    res = cmd.ExecuteScalar().ToString();
                    cmd.Connection.Close();
                }
            }
            return res;
        }
    }
}
