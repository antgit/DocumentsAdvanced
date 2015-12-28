using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// Представление корреспондентов для Web интерфейса
    /// </summary>
    public sealed class PeopleWebView
    {
        /*
        p.FirstName, p.LastName, p.MidleName   
         */
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }
        public int StateId { get; set; }
        public int FlagsValue { get; set; }
        public int KindId { get; set; }
        /// <summary>Наименование</summary>
        public string Name { get; set; }
        /// <summary>Печатное наименование</summary>
        public string NameFull { get; set; }
        /// <summary>Код</summary>
        public string Code { get; set; }
        /// <summary>Налоговый номер</summary>
        public string TaxNumber { get; set; }
        /// <summary>Примечание</summary>
        public string Memo { get; set; }
        /// <summary>Примечание до 100 символов</summary>
        public string DisplayMemo { get; set; }
        /// <summary>Состояние</summary>
        public string StateName { get; set; }
        /// <summary>Имя пользователя</summary>
        public string UserName { get; set; }
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
        
        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
        }

        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            KindId = reader.GetInt32(1);
            Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            NameFull = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            Code = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            TaxNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            Memo = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            DisplayMemo = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            StateName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
            UserName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
            Phone = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
            MyCompanyId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
            MyCompanyName = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);

            FirstName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
            LastName = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
            MidleName = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
            StateId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
            FlagsValue = reader.IsDBNull(17) ? 0 : reader.GetInt32(17);
            FirstHierarchy = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
        }

        public static List<PeopleWebView> GetView(Hierarchy h, bool nested = false)
        {
            List<PeopleWebView> collection = new List<PeopleWebView>();
            PeopleWebView item = new PeopleWebView();
            using (SqlConnection cnn = h.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Contractor].[ViewWebPeoples]";
                        //if (item.EntityId != 0)
                        //{
                        //    ProcedureMap procedureMap = item.Entity.FindMethod(GlobalMethodAlias.LoadByHierarchies);
                        //    if (procedureMap != null)
                        //    {
                        //        procedureName = procedureMap.FullName;
                        //    }
                        //}
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.HierarchyId, SqlDbType.Int).Value = h.Id;
                        if (nested)
                            cmd.Parameters.Add(GlobalSqlParamNames.Nested, SqlDbType.Bit).Value = true;

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new PeopleWebView { _workarea = h.Workarea };
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
        private static PeopleWebView GetView(Agent value)
        {
            PeopleWebView item = new PeopleWebView();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return item;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Contractor].[ViewWebPeoples]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;


                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new PeopleWebView { _workarea = value.Workarea };
                            item.Load(reader);
                        }
                        reader.Close();
                    }
                }
                finally
                {
                    cnn.Close();
                }
                return item;
            }
        }

        public static implicit operator Agent(PeopleWebView value)
        {
            Agent ag = value.Workarea.Cashe.GetCasheData<Agent>().Item(value.Id);
            ag.Name = value.Name;
            ag.NameFull = value.NameFull;
            ag.Code = value.Code;
            ag.CodeTax = value.TaxNumber;
            ag.Memo = value.Memo;
            ag.UserName = value.UserName;
            ag.Phone = value.Phone;
            ag.People.FirstName = value.FirstName;
            ag.People.LastName = value.LastName;
            ag.People.MidleName = value.MidleName;
            return ag;
        }
        public static implicit operator PeopleWebView(Agent value)
        {
            return GetView(value);
        }

    }
}