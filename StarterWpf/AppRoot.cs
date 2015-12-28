using System.Collections.Generic;
using System.Data.SqlClient;

namespace StarterWpf
{
    /// <summary>Корень приложения</summary>
    internal class AppRoot
    {
        /// <summary>Идентификатор иеархии</summary>
        public int HierarchyId { get; set; }
        /// <summary>Идентификатор основного приложения</summary>
        public int Id { get; set; }
        /// <summary>Наименование приложения</summary>
        public string Name { get; set; }
        /// <summary>Загрузка данных о приложении
        /// </summary>
        /// <param name="owner">Владелец</param>
        /// <param name="cnn">Соединение</param>
        /// <returns></returns>
        public static List<AppRoot> Load(AplicationLoader owner, SqlConnection cnn)
        {
            List<AppRoot> coll = new List<AppRoot>();
            SqlDataReader reader;
            System.Data.ConnectionState previousConnectionState = cnn.State;

            try
            {
                if (cnn.State == System.Data.ConnectionState.Closed)
                {
                    cnn.Open();
                }
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "[Core].[LibrariesGetAppNames]";
                reader = cmd.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        owner.SendInfo("Загрузка корневых приложений....");
                        AppRoot item = new AppRoot {Id = reader.GetInt32(2), Name = reader.GetString(1), HierarchyId = reader.GetInt32(0)};
                        coll.Add(item);
                    }
                    reader.Close();
                }
            }
            finally
            {
                if (previousConnectionState == System.Data.ConnectionState.Closed)
                {
                    cnn.Close();
                }
            }
            return coll;
        }
    }
}