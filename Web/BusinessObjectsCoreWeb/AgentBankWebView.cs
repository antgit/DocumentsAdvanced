using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Web.Core
{
    /// <summary>
    /// Представление банков для Web интерфейса
    /// </summary>
    public sealed class AgentBankWebView
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }
        /// <summary>Тип</summary>
        public int KindId { get; set; }
        /// <summary>Ид состояния</summary>
        public int StateId { get; set; }
        /// <summary>Флаг</summary>
        public int FlagsValue { get; set; }
        /// <summary>Наименование</summary>
        public string Name { get; set; }
        /// <summary>Печатное наименование</summary>
        public string NameFull { get; set; }
        /// <summary>Код</summary>
        public string Code { get; set; }
        /// <summary>Налоговый номер</summary>
        public string TaxNumber { get; set; }
        /// <summary>Окпо</summary>
        public string Okpo { get; set; }
        /// <summary>Примечание</summary>
        public string Memo { get; set; }
        /// <summary>Примечание до 100 символов</summary>
        public string DisplayMemo { get; set; }
        /// <summary>Состояние</summary>
        public string StateName { get; set; }
        /// <summary>Форма собственности (сокращение)</summary>
        public string OwnerShip { get; set; }
        /// <summary>Идентификатор формы собственности</summary>
        public int OwnerShipId { get; set; }
        /// <summary>Имя пользователя</summary>
        public string UserName { get; set; }
        /// <summary>Телефонный номер</summary>
        public string Phone { get; set; }
        /// <summary>Плательщик НДС</summary>
        public bool NdsPayer { get; set; }
        /// <summary>Номер свидетельства о регистрации</summary>
        public string RegNumber;
        /// <summary>Наименование компании владельца</summary>
        public int MyCompanyId { get; set; }
        /// <summary>Идентификатор компании владельца</summary>
        public string MyCompanyName { get; set; }
        /// <summary>Идентификатор аналитики категория торговой точки</summary>
        public int CategoryId { get; set; }
        /// <summary>Наименование аналитики категория торговой точки</summary>
        public string CategoryName { get; set; }
        /// <summary>Идентификатор метража</summary>
        public int MetricAreaId { get; set; }
        /// <summary>Наименование метража</summary>
        public string MetricAreaName { get; set; }
        /// <summary>Код формы собственности</summary>
        public string OwnershipCode { get; set; }
        /// <summary>Идентификатор аналитики тип торговой точки</summary>
        public int TypeOutletId { get; set; }
        /// <summary>Код аналитики тип торговой точки</summary>
        public string TypeOutletCode { get; set; }
        /// <summary>Наименование аналитики тип торговой точки</summary>
        public string TypeOutletName { get; set; }
        /// <summary>Фамилия</summary>
        public string FirstName { get; set; }
        /// <summary>Имя</summary>
        public string LastName { get; set; }
        /// <summary>Отчество</summary>
        public string MidleName { get; set; }
        /// <summary>Первая иерархия</summary>
        public string FirstHierarchy { get; set; }
        /// <summary>Id первой иерархии</summary>
        public int FirstHierarchyId { get; set; }

        public string Mfo { get; set; }
        public string SertificateNo { get; set; }
        public string LicenseNo { get; set; }
        public string Swift { get; set; }
        public string CorrBankAccount { get; set; }
        public DateTime? SertificateDate { get; set; }
        public DateTime? LicenseDate { get; set; }

        private Workarea _workarea;
        /// <summary>Рабочая область</summary>
        public Workarea Workarea
        {
            get { return _workarea; }
        }
        /// <summary>Загрузка данных из объекта чтения данных</summary>
        /// <param name="reader">Объект чтения данных</param>
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            KindId = reader.GetInt32(1);
            Name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            NameFull = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
            Code = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
            TaxNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
            Okpo = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
            Memo = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
            DisplayMemo = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
            StateName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
            OwnerShip = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
            OwnerShipId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
            UserName = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
            Phone = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
            if (reader.IsDBNull(14))
                NdsPayer = false;
            else
                NdsPayer = reader.GetBoolean(14);
            RegNumber = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
            MyCompanyId = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
            MyCompanyName = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);

            CategoryId = reader.IsDBNull(18) ? 0 : reader.GetInt32(18);
            CategoryName = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
            MetricAreaId = reader.IsDBNull(20) ? 0 : reader.GetInt32(20);
            MetricAreaName = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
            OwnershipCode = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
            TypeOutletId = reader.IsDBNull(23) ? 0 : reader.GetInt32(23);
            TypeOutletCode = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
            TypeOutletName = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);

            StateId = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
            FlagsValue = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
            FirstHierarchy = reader.IsDBNull(28) ? string.Empty : reader.GetString(28);
            FirstHierarchyId = reader.IsDBNull(29) ? 0 : reader.GetInt32(29);

            Mfo = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
            SertificateNo = reader.IsDBNull(31) ? string.Empty : reader.GetString(31);
            SertificateDate = reader.IsDBNull(32) ? (DateTime?)null: reader.GetDateTime(32);
            LicenseNo = reader.IsDBNull(33) ? string.Empty : reader.GetString(33);
            LicenseDate = reader.IsDBNull(34) ? (DateTime?)null: reader.GetDateTime(34);
            Swift = reader.IsDBNull(35) ? string.Empty : reader.GetString(35);
            CorrBankAccount = reader.IsDBNull(36) ? string.Empty : reader.GetString(36);
        }
        /// <summary>
        /// Получить представление
        /// </summary>
        /// <param name="h">Иерархия</param>
        /// <param name="nested">С учетом вложенных</param>
        /// <returns></returns>
        public static List<AgentBankWebView> GetView(Hierarchy h, bool nested = false)
        {
            List<AgentBankWebView> collection = new List<AgentBankWebView>();
            AgentBankWebView item = new AgentBankWebView();
            using (SqlConnection cnn = h.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return collection;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Contractor].[ViewWebAgentBanks]";
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
                            item = new AgentBankWebView { _workarea = h.Workarea };
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
        /// <summary>
        /// Представление для одного объекта
        /// </summary>
        /// <param name="value">Корреспондент</param>
        /// <returns></returns>
        private static AgentBankWebView GetView(Agent value)
        {
            AgentBankWebView item = new AgentBankWebView();
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return item;

                try
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string procedureName = "[Contractor].[ViewWebAgents]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = procedureName;
                        cmd.Parameters.Add(GlobalSqlParamNames.ElementId, SqlDbType.Int).Value = value.Id;


                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            item = new AgentBankWebView { _workarea = value.Workarea };
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
        /// <summary>
        /// Преобразование представления в объект
        /// </summary>
        /// <param name="value">Представление</param>
        /// <returns></returns>
        public static implicit operator Agent(AgentBankWebView value)
        {
            Agent ag = value.Workarea.Cashe.GetCasheData<Agent>().Item(value.Id);
            ag.Name = value.Name;
            ag.NameFull = value.NameFull;
            ag.Code = value.Code;
            ag.CodeTax = value.TaxNumber;
            ag.Memo = value.Memo;
            ag.UserName = value.UserName;
            ag.Phone = value.Phone;
            ag.Company.Okpo = value.Okpo;
            ag.Company.OwnershipId = value.OwnerShipId;
            ag.Company.MetricAreaId = value.MetricAreaId;
            ag.Company.CategoryId = value.CategoryId;
            ag.Company.TypeOutletId = value.TypeOutletId;
            ag.Company.NdsPayer = value.NdsPayer;
            ag.Company.RegNumber = value.RegNumber;
            return ag;
        }
        /// <summary>
        /// Преобразование объекта в представление
        /// </summary>
        /// <param name="value">Корреспондент</param>
        /// <returns></returns>
        public static implicit operator AgentBankWebView(Agent value)
        {
            return GetView(value);
        }

    }
}