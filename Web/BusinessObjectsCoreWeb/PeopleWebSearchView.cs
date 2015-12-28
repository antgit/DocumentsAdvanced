using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// Представление корреспондентов для поиска в Web интерфейса
    /// </summary>
    public sealed class PeopleWebSearchView
    {
        /// <summary>Идентификатор корреспондента</summary>
        public int Id { get; set; }
        /// <summary>Текущий набор флагов</summary>
        public int FlagsValue { get; set; }
        /// <summary>Наименование</summary>
        public string Name { get; set; }
        /// <summary>Налоговый номер</summary>
        public string TaxNumber { get; set; }
        /// <summary>Примечание до 100 символов</summary>
        public string DisplayMemo { get; set; }
        /// <summary>Телефонный номер</summary>
        public string Phone { get; set; }
        /// <summary>Идентификатор компании владельца</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Наименование компании владельца</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Фамилия</summary>
        public string FirstName { get; set; }
        /// <summary>Имя</summary>
        public string LastName { get; set; }
        /// <summary>Отчество</summary>
        public string MidleName { get; set; }
        /// <summary>Первая иерархия</summary>
        public string FirstHierarchy { get; set; }
        /// <summary>Идентификатор иерархии</summary>
        public int FirstHierarchyId { get; set; }
        /// <summary>Идентификатор отдела</summary>
        public int DepatmentId { get; set; }
        /// <summary>Отдел</summary>
        public string DepatmentName { get; set; }
        /// <summary>Идентификатор пользователя</summary>
        public int UidId { get; set; }
        /// <summary>Имя пользователя</summary>
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
        /// список сотрудников в группе 
        /// </summary>
        /// <param name="h">Иерархия в которой необходимо производить поиск, обязательный параметр, не должен быть равен null</param>
        /// <param name="nested">Искать во всех вложенных иерархиях</param>
        /// <param name="myCompanyId">Идентификатор компании которой принадлежит сотрудник</param>
        /// <param name="onlyUsers">Искать только сотрудников ассоциированных с пользователями системы</param>
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