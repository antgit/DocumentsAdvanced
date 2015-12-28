using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// Представление списка документов для Web интерфейса
    /// </summary>
    public sealed class DocumentsWebView
    {
        static DocumentsWebView()
        {
            CasheData = new Dictionary<string, DataTable>();
        }
        public static void Refresh()
        {
            CasheData = new Dictionary<string, DataTable>();
        }
        private static Dictionary<string, DataTable> CasheData;
        public static DataTable GetView(Workarea wa, int kindId, string folderCodeFind, string userName, DateTime ds, DateTime de, bool refresh = false)
        {
            Folder f = wa.GetFolderByCodeFind(folderCodeFind);
            string keyData = string.Format("{0}{1}{2}{3}{4}", kindId, folderCodeFind, userName, ds, de);
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
                        string procedureName = "[Document].[ViewWebDocumentsLoadByFolder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = kindId;
                        cmd.Parameters.Add(GlobalSqlParamNames.FolderId, SqlDbType.Int).Value = f.Id;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.Date).Value = ds;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.Date).Value = de;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;

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
        public static DataTable GetView(Workarea wa, int kindId, int folderId, string userName, DateTime ds, DateTime de, bool refresh = false)
        {
            string folderCodeFind = "FLD_NONEXISTS";
            if(folderId!=0)
            {
                Folder f = wa.Cashe.GetCasheData<Folder>().Item(folderId);
                folderCodeFind = f.CodeFind;
            }
            string keyData = string.Format("{0}{1}{2}{3}{4}", kindId, folderCodeFind, userName, ds, de);
            if (!refresh && CasheData.ContainsKey(keyData))
            {
                return CasheData[keyData];
            }

            DataTable tblData = new DataTable();
            if (folderCodeFind == "FLD_NONEXISTS")
                return tblData;
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return null;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Document].[ViewWebDocumentsLoadByFolder]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = kindId;
                        cmd.Parameters.Add(GlobalSqlParamNames.FolderId, SqlDbType.Int).Value = folderId;
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.Date).Value = ds;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.Date).Value = de;
                        cmd.Parameters.Add(GlobalSqlParamNames.UserName, SqlDbType.NVarChar, 128).Value = userName;

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