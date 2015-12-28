using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Windows
{
    public sealed class AnalizeModuleUsed
    {
        public AnalizeModuleUsed()
        {
            
        }

        private Workarea _workarea;

        /// <summary>
        /// Рабоча область
        /// </summary>
        public Workarea Workarea
        {
            get { return _workarea; }
            set { _workarea = value; }
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public int Count { get; set; }

        public void Save()
        {
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return;

                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "Core.AnalysisModulesUsedInsert";

                        SqlParameter prm = new SqlParameter(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128) { IsNullable = true };
                        prm.Value = Workarea.CurrentUser.Name;
                        sqlCmd.Parameters.Add(prm);

                        prm = new SqlParameter(GlobalSqlParamNames.Name, SqlDbType.NVarChar, 255) { IsNullable = true };
                        prm.Value = Name;
                        sqlCmd.Parameters.Add(prm);
                        
                        sqlCmd.ExecuteNonQuery();
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
        }

        public static List<AnalizeModuleUsed> GetMostUsed(Workarea wa)
        {
            List<AnalizeModuleUsed> coll = new List<AnalizeModuleUsed>();
            AnalizeModuleUsed item = null;
            if (wa == null)
                return coll;

            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "Core.AnalysisModulesUsedGet";
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value =
                            wa.CurrentUser.Name;
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                item = new AnalizeModuleUsed {Workarea = wa};
                                item.Count = reader.GetInt32(0);
                                item.Name = reader.GetString(1);
                                coll.Add(item);
                            }
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    if (cnn.State != ConnectionState.Closed)
                        cnn.Close();
                }
            }
            return coll;
        }

    }
}
