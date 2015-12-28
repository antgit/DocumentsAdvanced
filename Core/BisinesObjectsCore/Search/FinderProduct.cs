using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessObjects
{
    public sealed class FinderProduct : FinderObject<Product>
    {
        public FinderProduct()
            : base()
        {
            SupportCriteria.Add(new SearchField { Name = "Name", Caption = "Наименование" });
            SupportCriteria.Add(new SearchField { Name = "KindId", Caption = "Тип", DataType = SqlDbType.Int });
            SupportCriteria.Add(new SearchField { Name = "Code", Caption = "Код" });
            SupportCriteria.Add(new SearchField { Name = "Memo", Caption = "Примечание" });
            SupportCriteria.Add(new SearchField { Name = "FlagString", Caption = "Пользовательский флаг" });
            SupportCriteria.Add(new SearchField { Name = "Nomenclature", Caption = "Наменклатура" });
            SupportCriteria.Add(new SearchField { Name = "Articul", Caption = "Артикул" });
            SupportCriteria.Add(new SearchField { Name = "Cataloque", Caption = "Каталожный" });
            SupportCriteria.Add(new SearchField { Name = "Barcode", Caption = "Штрих код" });
            SupportCriteria.Add(new SearchField { Name = "Weight", Caption = "Вес", DataType = SqlDbType.Money });
            SupportCriteria.Add(new SearchField { Name = "Height", Caption = "Высота", DataType = SqlDbType.Money });
            SupportCriteria.Add(new SearchField { Name = "Width", Caption = "Ширина", DataType = SqlDbType.Money });
            SupportCriteria.Add(new SearchField { Name = "Depth", Caption = "Глубина", DataType = SqlDbType.Money });
            SupportCriteria.Add(new SearchField { Name = "Size", Caption = "Размер"});
            SupportCriteria.Add(new SearchField { Name = "StoragePeriod", Caption = "Период хранения", DataType = SqlDbType.Money });
            SupportCriteria.Add(new SearchField { Name = "UnitName", Caption = "Единица измерения" });
            SupportCriteria.Add(new SearchField { Name = "UnitShortName", Caption = "Единица измерения (краткая)" });
            SupportCriteria.Add(new SearchField { Name = "State", Caption = "Состояние" });
            SupportCriteria.Add(new SearchField { Name = "ManufacturerName", Caption = "Изготовитель" });
            SupportCriteria.Add(new SearchField { Name = "TradeMarkName", Caption = "Торговая группа" });
            SupportCriteria.Add(new SearchField { Name = "Brand", Caption = "Бренд" });
            SupportCriteria.Add(new SearchField { Name = "ProductTypeName", Caption = "Тип продукции" });
            SupportCriteria.Add(new SearchField { Name = "PakcTypeName", Caption = "Тип фасовки" });
            SupportCriteria.Add(new SearchField { Name = "HierarchyName", Caption = "Группа" });
            SupportCriteria.Add(new SearchField { Name = "HierarchyCode", Caption = "Код группы" });

            SupportCriteria.Add(new SearchField { Name = "Generic", Caption = "Везде" });
        }

        private List<Product> _result;
        public override List<Product> Result()
        {
            if (_result == null)
                Convert();
            return _result;
        }

        protected override void Convert()
        {
            _result = new List<Product>();
            if (!IsSearhed || !HasResult) return;
            foreach (Product ag in from DataRow row in DataResult.Rows
                                   select Workarea.Cashe.GetCasheData<Product>().Item(System.Convert.ToInt32(row[GlobalPropertyNames.Id]))
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
                    cmd.CommandText = "[Product].[SearchProducts]";
                    #region Параметры
                    foreach (SearchField criterion in Criteria)
                    {
                        SqlParameter prm = cmd.Parameters.Add(criterion.SqlParamName, criterion.DataType);
                        switch (criterion.DataType)
                        {
                            case SqlDbType.NVarChar:
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
                                break;
                            case SqlDbType.Int:
                                if (string.IsNullOrEmpty(criterion.Value))
                                    prm.Value = DBNull.Value;
                                else
                                    prm.Value = Int32.Parse(criterion.Value);
                                break;
                            case SqlDbType.Money:
                                if (string.IsNullOrEmpty(criterion.Value))
                                    prm.Value = DBNull.Value;
                                else
                                    prm.Value = Decimal.Parse(criterion.Value);
                                break;
                        }
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