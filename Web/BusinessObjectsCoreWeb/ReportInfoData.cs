using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// ������ ��� ������� ���������� ������������ 
    /// </summary>
    public sealed class ReportInfoData
    {
        internal class Cashe
        {
            public Dictionary<int, List<ReportInfoData>> Data;
            public Cashe()
            {
                if (Data == null)
                {
                    Data = new Dictionary<int, List<ReportInfoData>>();
                    Date = DateTime.Now;
                }

            }

            
            public bool NeadUpdate
            {
                get { return (DateTime.Now.Subtract(Date).TotalMinutes > 10); }
                
            }

            public DateTime Date { get; set; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// ������ ��������� ��� �����������
        /// </summary>
        public string NavigateUrl { get; set; }
        /// <summary>
        /// ������� �� ���� �����
        /// </summary>
        /// <param name="reader"></param>
        private void Load(SqlDataReader reader)
        {
            Code = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
            Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
            Value = reader.IsDBNull(2) ? string.Empty : reader.GetValue(2);
        }

        private static Cashe cashe;
        /// <summary>
        /// �����������
        /// </summary>
        static ReportInfoData()
        {
            if (cashe== null)
                cashe = new Cashe();
        }
        /// <summary>
        /// ������ � �����������
        /// </summary>
        /// <param name="id">�� ��������������</param>
        /// <param name="wa">������� �������</param>
        /// <returns></returns>
        public static List<ReportInfoData> GetView(Workarea wa, int id)
        {
            if (cashe.NeadUpdate)
                cashe = new Cashe();
            if (cashe.Data.ContainsKey(id) && !cashe.NeadUpdate)
                return cashe.Data[id];
            
            List<ReportInfoData> collection = new List<ReportInfoData>();
            ReportInfoData item = new ReportInfoData();
            using (SqlConnection cnn = wa.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Report].[AgentMyCompanySimpleInfo]";
                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = id;
                        

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new ReportInfoData();
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
                if (cashe.Data.ContainsKey(id))
                    cashe.Data[id] = collection;
                else
                    cashe.Data.Add(id, collection);
                return collection;
            }
        }
    }
}