using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// ������������� ��������������� ��� ������ � Web ����������
    /// </summary>
    public sealed class PeopleWebSearchView
    {
        /// <summary>������������� ��������������</summary>
        public int Id { get; set; }
        /// <summary>������� ����� ������</summary>
        public int FlagsValue { get; set; }
        /// <summary>������������</summary>
        public string Name { get; set; }
        /// <summary>��������� �����</summary>
        public string TaxNumber { get; set; }
        /// <summary>���������� �� 100 ��������</summary>
        public string DisplayMemo { get; set; }
        /// <summary>���������� �����</summary>
        public string Phone { get; set; }
        /// <summary>������������� �������� ���������</summary>
        public int MyCompanyId { get; set; }
        /// <summary>������������ �������� ���������</summary>
        public string MyCompanyName { get; set; }
        /// <summary>�������</summary>
        public string FirstName { get; set; }
        /// <summary>���</summary>
        public string LastName { get; set; }
        /// <summary>��������</summary>
        public string MidleName { get; set; }
        /// <summary>������ ��������</summary>
        public string FirstHierarchy { get; set; }
        /// <summary>������������� ��������</summary>
        public int FirstHierarchyId { get; set; }
        /// <summary>������������� ������</summary>
        public int DepatmentId { get; set; }
        /// <summary>�����</summary>
        public string DepatmentName { get; set; }
        /// <summary>������������� ������������</summary>
        public int UidId { get; set; }
        /// <summary>��� ������������</summary>
        public string UidName { get; set; }

        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
        }

        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
            TaxNumber = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            DisplayMemo = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            Phone = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            MyCompanyId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
            MyCompanyName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            FirstName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            LastName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
            MidleName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
            FirstHierarchy = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
            FirstHierarchyId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
            FlagsValue = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
            DepatmentName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
            DepatmentId = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
            UidId = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
            UidName = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
        }

        /// <summary>
        /// ������ ����������� � ������ 
        /// </summary>
        /// <param name="h">�������� � ������� ���������� ����������� �����, ������������ ��������, �� ������ ���� ����� null</param>
        /// <param name="nested">������ �� ���� ��������� ���������</param>
        /// <param name="myCompanyId">������������� �������� ������� ����������� ���������</param>
        /// <param name="onlyUsers">������ ������ ����������� ��������������� � �������������� �������</param>
        /// <returns></returns>
        public static List<PeopleWebSearchView> GetView(Hierarchy h, bool nested = false, int myCompanyId = 0, bool onlyUsers=true)
        {
            List<PeopleWebSearchView> collection = new List<PeopleWebSearchView>();
            PeopleWebSearchView item = new PeopleWebSearchView();
            using (SqlConnection cnn = h.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Contractor].[ViewWebPeoplesSearch]";
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = h.Id;
                        if (nested)
                            cmd.Parameters.Add(GlobalSqlParamNames.Nested, SqlDbType.Bit).Value = true;

                        if (myCompanyId != 0)
                            cmd.Parameters.Add(GlobalSqlParamNames.MyCompanyId, SqlDbType.Int).Value = myCompanyId;
                        
                        cmd.Parameters.Add(GlobalSqlParamNames.OnlyUsers, SqlDbType.Int).Value = onlyUsers;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new PeopleWebSearchView { _workarea = h.Workarea };
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
                return collection;
            }
        }
    }
}