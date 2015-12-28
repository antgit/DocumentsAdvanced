using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StarterWpf
{
    internal class AppicationData
    {
        /// <summary>Идентификатор библиотеки</summary>
        public int Id { get; set; }
        /// <summary>Дата изменения</summary>
        public DateTime? DateModified { get; set; }
        /// <summary>Тип</summary>
        public int KindId { get; set; }
        /// <summary>Код</summary>
        public string Code { get; set; }
        /// <summary>Библиотека</summary>
        public byte[] AssemblyDll { get; set; }
        /// <summary>Исходники</summary>
        public byte[] AssemblySource { get; set; }
        /// <summary>Имя файла</summary>
        public string FileName { get; set; }
        /// <summary>Версия</summary>
        public string AssemblyVersion { get; set; }
        /// <summary>Родитель</summary>
        public int ParentId { get; set; }
        /// <summary>Уровень</summary>
        public int Level { get; set; }
        /// <summary>Папка</summary>
        public string Folder { get; set; }

        public static List<AppicationData> Load(AplicationLoader owner, SqlConnection cnn, int rootId)
        {
            List<AppicationData> coll = new List<AppicationData>();
            SqlDataReader reader;
            ConnectionState previousConnectionState = cnn.State;

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[Core].[LibrariesGetApplication]";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = rootId;
                    reader = cmd.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            AppicationData item = new AppicationData();
                            item.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            item.DateModified = reader.IsDBNull(reader.GetOrdinal("DateModified"))
                                                    ? (DateTime?) null
                                                    : reader.GetDateTime(reader.GetOrdinal("DateModified"));
                            item.KindId = reader.IsDBNull(reader.GetOrdinal("KindId")) ? 0 : reader.GetInt32(reader.GetOrdinal("KindId"));
                            item.Code = reader.IsDBNull(reader.GetOrdinal("Code")) ? string.Empty : reader.GetString(reader.GetOrdinal("Code"));
                            item.FileName = reader.IsDBNull(reader.GetOrdinal("FileName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FileName"));
                            item.AssemblyVersion = reader.IsDBNull(reader.GetOrdinal("AssemblyVersion"))
                                                       ? string.Empty
                                                       : reader.GetString(reader.GetOrdinal("AssemblyVersion"));
                            item.AssemblyDll = !reader.IsDBNull(reader.GetOrdinal("AssemblyDll"))
                                                   ? reader.GetSqlBinary(reader.GetOrdinal("AssemblyDll")).Value
                                                   : null;
                            item.AssemblySource = reader.IsDBNull(reader.GetOrdinal("AssemblySource"))
                                                      ? null
                                                      : reader.GetSqlBinary(reader.GetOrdinal("AssemblySource")).Value;
                            item.Folder = reader.IsDBNull(reader.GetOrdinal("FolderName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FolderName"));
                            item.Level = reader.IsDBNull(reader.GetOrdinal("Lv")) ? 0 : reader.GetInt32(reader.GetOrdinal("Lv"));
                            item.ParentId = reader.IsDBNull(reader.GetOrdinal("ParentId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ParentId"));
                            coll.Add(item);
                            owner.SendInfo(string.Format("Загрузка библиотеки {0}...", item.FileName));
                        }
                        reader.Close();
                    }
                }
            }
            finally
            {
                if (previousConnectionState == ConnectionState.Closed)
                {
                    cnn.Close();
                }

            }
            owner.SendInfo("Загрузка библиотек завершена...");
            return coll;
        }
    }
}