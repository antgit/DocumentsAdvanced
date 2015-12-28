using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessObjects
{
    public sealed class FinderAgent: FinderObject<Agent>
    {
        public FinderAgent(): base()
        {
            SupportCriteria.Add(new SearchField {Name = "Name", Caption = "Наименование"});
            SupportCriteria.Add(new SearchField { Name = "KindId", Caption = "Тип", DataType = SqlDbType.Int });
            SupportCriteria.Add(new SearchField { Name = "Code", Caption = "Код" });
            SupportCriteria.Add(new SearchField { Name = "Memo", Caption = "Примечание" });
            SupportCriteria.Add(new SearchField { Name = "FlagString", Caption = "Пользовательский флаг" });
            SupportCriteria.Add(new SearchField { Name = "CodeFind", Caption = "Код поиска" });
            SupportCriteria.Add(new SearchField { Name = "CodeTax", Caption = "Налоговый код" });
            SupportCriteria.Add(new SearchField { Name = "InternationalName", Caption = "Международное наименование" });
            SupportCriteria.Add(new SearchField { Name = "Okpo", Caption = "Окпо" });
            SupportCriteria.Add(new SearchField { Name = "FirstName", Caption = "Фамилия" });
            SupportCriteria.Add(new SearchField { Name = "LastName", Caption = "Имя" });
            SupportCriteria.Add(new SearchField { Name = "MidleName", Caption = "Отчество" });
            SupportCriteria.Add(new SearchField { Name = "TabNo", Caption = "Табельный номер" });
            SupportCriteria.Add(new SearchField { Name = "ActivityBrancheName", Caption = "Вид деятельности" });
            SupportCriteria.Add(new SearchField { Name = "ActivityEconomicName", Caption = "Экономическая деятельность" });
            SupportCriteria.Add(new SearchField { Name = "IndustryName", Caption = "Отрасль" });
            SupportCriteria.Add(new SearchField { Name = "RegNumber", Caption = "Номер свидетельсва о регистрации" });
            SupportCriteria.Add(new SearchField { Name = "TypeOutletName", Caption = "Тип торговой точки" });
            SupportCriteria.Add(new SearchField { Name = "MetricAreaName", Caption = "Метраж" });
            SupportCriteria.Add(new SearchField { Name = "CategoryName", Caption = "Категория" });
            SupportCriteria.Add(new SearchField { Name = "SalesRepresentativeName", Caption = "Торговый представитель" });
            SupportCriteria.Add(new SearchField { Name = "Generic", Caption = "Везде" });
        }

        private List<Agent> _result;
        public override List<Agent> Result()
        {
            if(_result == null)
                Convert();
            return _result;
        }

        protected override void Convert()
        {
            _result = new List<Agent>();
            if (!IsSearhed || !HasResult) return;
            foreach (Agent ag in from DataRow row in DataResult.Rows
                                 select Workarea.Cashe.GetCasheData<Agent>().Item(System.Convert.ToInt32(row[GlobalPropertyNames.Id]))
                                 into ag where ag != null select ag)
            {
                _result.Add(ag);
            }
        }

        public override void Search()
        {
            _result = null;
            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // TODO: Использовать маппинг ХП
                    cmd.CommandText = "[Contractor].[SearchAgents]";
                    #region Параметры
                    foreach (SearchField criterion in Criteria)
                    {
                        SqlParameter prm = cmd.Parameters.Add(criterion.SqlParamName, criterion.DataType);
                        if (criterion.DataType == SqlDbType.NVarChar)
                        {
                            if (string.IsNullOrEmpty(criterion.Value))
                                prm.Value = DBNull.Value;
                            else
                            {
                                prm.Size = 255;
                                switch (criterion.Kind)
                                {
                                    case SearchFieldKind.Default:
                                        prm.Value = '%' + criterion.Value + '%';
                                        break;
                                    case SearchFieldKind.EndWith:
                                        prm.Value = '%' + criterion.Value;
                                        break;
                                    case SearchFieldKind.StartWith:
                                        prm.Value = criterion.Value + '%';
                                        break;
                                    case SearchFieldKind.Strong:
                                        prm.Value = criterion.Value;
                                        break;
                                }
                            }
                        }
                        if (criterion.DataType != SqlDbType.Int) continue;
                        if (string.IsNullOrEmpty(criterion.Value))
                            prm.Value = DBNull.Value;
                        else
                            prm.Value = Int32.Parse(criterion.Value);
                    }
                    cmd.Parameters.Add(GlobalSqlParamNames.SearchKind, SqlDbType.Int).Value = (int)SearchType;
                    #endregion

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataResult = new DataTable();
                    da.Fill(DataResult);
                    IsSearhed = true;
                    HasResult = DataResult.Rows.Count > 0;
                }
            }
        }
    }
}