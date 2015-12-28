using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// Представление списка документов цен для Web интерфейса
    /// </summary>
    public sealed class PricesDocumentsWebView
    {
        static PricesDocumentsWebView()
        {
            CasheData = new Dictionary<string, DataTable>();
        }
        public static void Refresh()
        {
            CasheData = new Dictionary<string, DataTable>();
        }
        private static Dictionary<string, DataTable> CasheData;
        public static DataTable GetView(Workarea wa, int kindId, string folderCodeFind, string userName, DateTime ds, DateTime de, int? stateId, int? count = null, bool refresh = false)
        {
            Folder f = wa.GetFolderByCodeFind(folderCodeFind);
            string keyData = string.Format("{0}{1}{2}{3}{4}{5}{6}", kindId, folderCodeFind, userName, ds, de, count, stateId);
            if (!refresh && CasheData.ContainsKey(keyData))
            {
                return CasheData[keyData];
            }

            DataTable tblData = new DataTable();
            if (f == null)
                return tblData;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return null;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Price].[DocumentsLoadByKind]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = kindId;
                        cmd.Parameters.Add(GlobalSqlParamNames.FolderId, SqlDbType.Int).Value = f.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.Date).Value = ds;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.Date).Value = de;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;
                        if (stateId.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.StateId, SqlDbType.Int).Value = stateId.Value;
                        if (count.HasValue)
                            cmd.Parameters.Add(GlobalSqlParamNames.Count, SqlDbType.Int).Value = count.Value;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tblData);


                    }
                }
                finally
                {
                    cnn.Close();
                }
                if (CasheData.ContainsKey(keyData))
                    CasheData[keyData] = tblData;
                else
                    CasheData.Add(keyData, tblData);
                return CasheData[keyData];
            }
        }

    }
}