using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BusinessObjects.Developer;

namespace BusinessObjects.Exchange
{
    /// <summary>����� �������</summary>
    [Serializable]
    public class ExportImportData
    {
        public DataSet MemoryDataSet;

        public ExportImportData()
        {
            SelectedRow = new List<Row>();
            //User = Environment.UserName;
        }
        /// <summary>������� �������</summary>
        public Workarea Workarea { get; set; }

        /// <summary>��������� ������ ������� �������</summary>
        /// <param name="value">�������</param>
        /// <returns>������</returns>
        internal DataTable GetDataTable(DbObject value)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(string.Format("SELECT * FROM [{0}].[{1}]", value.Schema, value.Name), cnn))
                {
                    da.Fill(dt);
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return dt;

        }

        /// <summary>������ ��������� ����� �������</summary>
        public List<Row> SelectedRow { get; set; }

        /// <summary>�������������� ������ �� �����</summary>
        /// <param name="filename">��� �����</param>
        public static ExportImportData LoadSelection(string filename)
        {
            TextReader r = new StreamReader(filename);
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(ExportImportData));
                ExportImportData _exportImportData = (ExportImportData)s.Deserialize(r);
                return _exportImportData;
            }
            finally
            {
                r.Close();
            }
        }

        /// <summary>������������ ������� � xml ����</summary>
        /// <param name="filename">��� �����</param>
        /// <param name="workarea">������� �������</param>
        public static void SaveSelection(string filename, ExportImportData workarea)
        {
            TextWriter w = null;
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(ExportImportData));
                w = new StreamWriter(filename);
                s.Serialize(w, workarea);
            }

            finally
            {
                w.Close();
            }
        }

        /// <summary>������ DataSet �� XML ����� � DataSet</summary>
        /// <param name="filename">��� �����</param>
        public DataSet ImportXmlData(string filename, SqlConnection connection)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(filename);
            ImportDataSet(ds, connection);
            return ds;
        }

        /// <summary>������ ��������� ������ �� �� � XML ���� �� �����</summary>
        /// <param name="filename">��� �����</param>
        public void ExportXmlData(string filename, SqlConnection cnn )
        {
            DataSet ds = ExportDataToDataset(cnn);
            ds.WriteXml(filename, XmlWriteMode.WriteSchema);
        }

        /// <summary>
        /// ���������� �������� ��������� ��������/�������
        /// </summary>
        /// <param name="exchangeSettings"></param>
        /// <param name="cnn">���������� � ��</param>
        /// <returns>true - ������, false-������</returns>
        public bool RunExportSettings(ExchangeSettings exchangeSettings, SqlConnection cnn)
        {
            if (exchangeSettings == null)
                return false;

            switch (exchangeSettings.Code)
            {
                case "IMPORT":
                    switch (exchangeSettings.ExportTo)
                    {
                        case 0://������
                            if (MemoryDataSet == null)
                                return false;
                            break;
                        case 1://����
                            MemoryDataSet = new DataSet();
                            MemoryDataSet.ReadXml(Path.Combine(exchangeSettings.Path, exchangeSettings.FileName));
                            break;
                        default:
                            return false;
                    }

                    ImportDataSet(MemoryDataSet, cnn);
                    break;

                case "EXPORT":
                    switch(exchangeSettings.Kind)
                    {
                        case "LIBRARIES":
                            MemoryDataSet = ExportFromStoredProcedure("Export.GetLibraries", cnn);
                            break;
                        case "TABLES":
                            MemoryDataSet = ExportFromStoredProcedure("Export.GetTables", cnn);
                            break;
                        default:
                            MemoryDataSet = ExportByKind(exchangeSettings.Kind, cnn);
                            break;
                    }

                    switch (exchangeSettings.ExportTo)
                    {
                        case 0://������
                            break;
                        case 1://����
                            string fileName = exchangeSettings.FileName == string.Empty ? string.Format("{0}_{1}_{2}.xml", exchangeSettings.Source.Replace("_", ""), exchangeSettings.Destination.Replace("_", ""), DateTime.Now.ToString("yyyyddmm_hhmmss")) : exchangeSettings.FileName;
                            string filePath = exchangeSettings.Path == string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : exchangeSettings.Path;
                            if (!Directory.Exists(filePath))
                                throw new System.Exception("����� �������� ����");
                            MemoryDataSet.WriteXml(Path.Combine(filePath, fileName), XmlWriteMode.WriteSchema);
                            break;
                        default:
                            return false;
                    }
                    break;

                default:
                    return false;
            }

            return true;
        }
        /// <summary>
        /// ������� ������ �� ��
        /// </summary>
        /// <param name="SPname">��� ��</param>
        /// <param name="cnn">���������� � ��</param>
        /// <returns>������</returns>
        private static DataSet ExportFromStoredProcedure(string SPname, SqlConnection cnn)
        {
            DataSet ds = new DataSet();

            using (cnn)
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = SPname;
                    SqlDataAdapter da = new SqlDataAdapter(cmd)
                                            {
                                                MissingSchemaAction = MissingSchemaAction.AddWithKey
                                            };
                    da.Fill(ds);
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            ProcessDataSet(ds);
            RemoveEmptyTables(ds);
            return ds;
        }
        /// <summary>
        /// ������� ������ � ����������� �� ��������������� ���� ���������
        /// </summary>
        /// <param name="Kind">�������������� ��� ��������</param>
        /// <param name="cnn">���������� � ��</param>
        /// <returns>������</returns>
        private DataSet ExportByKind(string Kind,SqlConnection cnn)
        {
            DataSet ds = new DataSet();

            using (cnn)
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                //��������� ������ �������������� ���� ��������� ������� �������
                DataSet systemObjectsId = new DataSet();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    switch(Kind)
                    {
                        case "SYSTEM":
                            cmd.CommandText = "Export.GetSystemObjectsId";
                            break;
                        case "TEMPLATES":
                            cmd.CommandText = "Export.GetTemplatesId";
                            break;
                        default:
                            throw new Exception("��� ��������� �������� ��� �������� ���� �������������� ���������");
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd)
                                            {
                                                MissingSchemaAction = MissingSchemaAction.AddWithKey
                                            };
                    da.Fill(systemObjectsId);
                }

                //��������� ������� ������ �������
                int i = 0;
                List<DbObject> TableInfoCollection = Workarea.GetCollection<DbObject>();

                //�������, ��� ������� ������� ����� ��������� � ����������
                string[] tablesWithDependence = { "Core.CustomViewLists", "Core.DocumentTypes", "Developer.DbObjects", "Fact.FactNames" };
                foreach (DataTable dt in systemObjectsId.Tables)
                {
                    if (dt.TableName == "Table") continue;
                    string tableName = systemObjectsId.Tables[0].Rows[i]["Name"].ToString();
                    DbObject table = TableInfoCollection.FirstOrDefault(s => s.Schema + "." + s.Name == tableName);

                    if (table == null)
                    {
                        throw new Exception("�� ������� ������� '" + tableName + "'");
                        //continue;
                    }
                    if (table.ProcedureExport == string.Empty)
                    {
                        throw new Exception("��� ������� '" + tableName + "' ��� ��������� ��������");
                        //continue;
                    }

                    int onlyMainData = tablesWithDependence.Contains(tableName) ? 0 : 1;

                    //��������� ������� ��� ������ �������� ��������� ������� �������
                    DataSet newDataSet = new DataSet();
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = table.ProcedureExport;
                        cmd.Parameters.AddWithValue(GlobalSqlParamNames.TVP, dt);
                        cmd.Parameters.AddWithValue(GlobalSqlParamNames.OnlyMainData, onlyMainData);
                        SqlDataAdapter da = new SqlDataAdapter(cmd)
                                                {
                                                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                                                };
                        da.Fill(newDataSet);
                    }

                    //���� �������� OnlyMainData ����� 0, �� � ���������� ������� ������ �������� ���� ������ ���� ������
                    if (onlyMainData == 0)
                    {
                        ProcessDataSet(newDataSet);
                    }
                    else
                    {
                        //��������� ������������ ������� � �������
                        newDataSet.Tables[0].TableName = tableName;
                        newDataSet.Tables[0].PrimaryKey = new[] { newDataSet.Tables[0].Columns[GlobalPropertyNames.Id] };
                    }

                    ds.Merge(newDataSet);
                    i++;
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            //�������� ������ ������
            RemoveEmptyTables(ds);

            return ds;
        }
        /// <summary>
        /// ����� ���������� ����� �������� � ������������ � ������ � ������� �
        /// </summary>
        /// <param name="ds">DataSet</param>
        private static void ProcessDataSet(DataSet ds)
        {
            int i = 0;
            foreach (DataTable table in ds.Tables.Cast<DataTable>().Where(table => table.TableName != "Table"))
            {
                table.PrimaryKey = new[] { table.Columns[GlobalPropertyNames.Id] };
                table.TableName = ds.Tables[0].Rows[i]["Name"].ToString();
                i++;
            }
            ds.Tables.Remove("Table");
        }
        /// <summary>
        /// �������� ������ ������
        /// </summary>
        /// <param name="ds">DataSet</param>
        private static void RemoveEmptyTables(DataSet ds)
        {
            for (int k = ds.Tables.Count - 1; k >= 0; k--)
                if (ds.Tables[k].Rows.Count == 0)
                    ds.Tables.Remove(ds.Tables[k]);
        }

        /// <summary>
        /// ������� ��������� ����� ������� � DataSet
        /// </summary>
        /// <param name="cnn">���������� � ��</param>
        /// <returns>DataSet</returns>
        private DataSet ExportDataToDataset(SqlConnection cnn)
        {
            DataSet ds = new DataSet();
            using (cnn)
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                foreach (Row row in SelectedRow)
                {
                    //�������� ��������-��������� �������, ���������� �������������� �������� �����
                    DataTable IdTable = new DataTable();
                    IdTable.Columns.Add(GlobalPropertyNames.Id, Type.GetType("System.Int32"));

                    foreach (int v in row.Id)
                    {
                        IdTable.Rows.Add(v);
                    }

                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //����� ����� �������� ��������� � ����������� �� ����� �������
                        if(row.Table.ProcedureExport==string.Empty)
                            throw new NullReferenceException("��� �������� ��������� �������� ��� �������� �������!");

                        //���������� ���������� ������ ���������
                        cmd.CommandText = row.Table.ProcedureExport;
                        cmd.Parameters.AddWithValue(GlobalSqlParamNames.TVP, IdTable);
                        cmd.Parameters.AddWithValue(GlobalSqlParamNames.OnlyMainData, 0);
                        if ((row.Table.Schema == "Hierarchy") && (row.Table.Name == "Hierarchies"))
                        {
                            cmd.Parameters.AddWithValue(GlobalSqlParamNames.WithContent, 1);
                        }

                        //��������� ������
                        SqlDataAdapter da = new SqlDataAdapter(cmd)
                                                {
                                                    MissingSchemaAction = MissingSchemaAction.AddWithKey
                                                };
                        DataSet newDataSet = new DataSet();
                        da.Fill(newDataSet);

                        //���������� ��������� ����������� � ����������� DataSet
                        int i = 0;
                        foreach (DataTable newDataTable in
                            from DataTable dt in newDataSet.Tables where dt.TableName != "Table" select dt.Copy())
                        {
                            newDataTable.TableName = newDataSet.Tables[0].Rows[i]["Name"].ToString();
                            newDataTable.PrimaryKey = new[] { newDataTable.Columns[GlobalPropertyNames.Id] };
                            i++;
                            ds.Merge(newDataTable);
                        }
                    }
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            RemoveEmptyTables(ds);
            return ds;
        }

        /// <summary>
        /// ������������ ��� ���� .NET � TSQL
        /// </summary>
        /// <param name="name">��� ���� .NET</param>
        /// <returns>��� ���� .TSQL</returns>
        private static string ConvertDataTypeToSQL(DataColumn dc)
        {
            switch (dc.DataType.Name)
            {
                case "DateTime":
                    return "[DateTime]";
                case "Boolean":
                    return "[bit]";
                case "Int16":
                    return "[smallint]";
                case "Int32":
                    return "[int]";
                /*case "Int64":
                    return "[bigint]";*/
                case "Guid":
                    return "[uniqueidentifier]";
                case "String":
                    return (dc.MaxLength > 10000) || (dc.MaxLength == -1)
                               ? "[nvarchar](MAX)"
                               : string.Format("[nvarchar]({0})", dc.MaxLength);
                case "Byte[]":
                    return dc.ColumnName == "Version" ? "[binary](8)" : "[varbinary](MAX)";
                case "Decimal":
                    return "[money]";
                case "Double":
                    return "[float]";
                case "TimeSpan":
                    return "[time](7)";
                default:
                    throw new System.Exception("����������� ������������� ���� ��� ������ " + dc.DataType.Name+" (������� "+dc.ColumnName+")");
            }
        }

        /// <summary>
        /// �������� ������ ������� �� �������� �������, � ������� �����, ���������� ��������
        /// </summary>
        /// <param name="dt">�������� �������</param>
        /// <param name="TmpTableName">��� �������� ������� �� �������</param>
        /// <returns>������</returns>
        private static string FormCreateQuery(DataTable dt,string TmpTableName)
        {
            string Create = string.Empty;
            foreach (DataColumn dc in dt.Columns)
            {
                if (Create.Length > 0)
                    Create += ", ";

                Create += string.Format("[{0}] {1}", dc.ColumnName, ConvertDataTypeToSQL(dc));
            }
            return "CREATE TABLE "+TmpTableName+"("+Create+")";
        }

        /// <summary>
        /// ������������ ����� ��������� �������
        /// </summary>
        /// <param name="TableName">��� �������� �������</param>
        /// <returns>��� ��������� �������</returns>
        public string GetTmpTableName(string TableName)
        {
            string Schema = TableName.Substring(0, TableName.IndexOf('.'));
            string Name = TableName.Substring(TableName.IndexOf('.') + 1);
            return (Schema != "Documents" && (Name == "Documents" || Name == "DocumentDetails"))
                        ? "#" + Schema + TableName
                        : "#" + Name;
        }

        /// <summary>
        /// ����������� ������� ������� �� ��������� �� ������
        /// </summary>
        /// <param name="dt">������� � �������</param>
        /// 
        /// <param name="cnn">���������� � ��</param>
        public void CopyTableToServer(DataTable dt, SqlConnection cnn)
        {
            string TmpTableName = GetTmpTableName(dt.TableName);
            string query = FormCreateQuery(dt, TmpTableName);

            using(SqlCommand cmd=new SqlCommand(query,cnn))
            {
                cmd.ExecuteNonQuery();
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(cnn))
            {
                bulkCopy.DestinationTableName = TmpTableName;
                bulkCopy.WriteToServer(dt);
            }

        }

        /// <summary>
        /// ������ �������
        /// </summary>
        /// <param name="dt">������� ��� �������</param>
        /// <param name="flagAction">���� �������� 1-���������, 2-���������, 4-�������,</param>
        /// <param name="searchColumnList">������� ���������</param>
        /// <param name="operand">�������� ��������� ��� ��������������� �������� (OR ��� AND)</param>
        /// <param name="cnn">���������� � ��</param>
        public void ImportDataTable(DataTable dt, MergeAction flagAction, string searchColumnList, bool operand, SqlConnection cnn)
        {
            DbObject table=Workarea.GetCollection<DbObject>().FirstOrDefault(s => (s.Schema + "." + s.Name == dt.TableName));

            if (table == null)
                throw new Exception(string.Format("������� '{0}' �� �������!", dt.TableName));
            if (table.ProcedureImport == string.Empty)
                throw new Exception(string.Format("��� �������� ��������� ������� ��� ������� '{0}'!", dt.TableName));

            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = table.ProcedureImport;
                cmd.Parameters.AddWithValue(GlobalSqlParamNames.Action, flagAction);
                // add a new parameter, with any name we want - its for our own use only 
                SqlParameter sqlParam = cmd.Parameters.Add(GlobalSqlParamNames.ReturnValue, SqlDbType.Int);
                // set the direction flag so that it will be filled with the return value 
                sqlParam.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);

                if ((int)cmd.Parameters["@ReturnValue"].Value == -1)
                {
                    throw new System.Exception("������ ���������� �������� ���������! ����������� ������� �� ���� ��������� �� ������.");
                }
            }
        }

        /// <summary>
        /// ������ ������ �� DataSet �� ��������� ������� � ����������� ������
        /// </summary>
        /// <param name="ds">�������� DataSet</param>
        /// <param name="cnn">����������</param>
        /// <returns>���������� ���������� �������</returns>
        public void ImportDataSet(DataSet ds, SqlConnection cnn)
        {
            using (cnn)
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                //�������� ���� ������ �� ������
                foreach (DataTable dt in ds.Tables)
                {
                    CopyTableToServer(dt, cnn);
                }

                #region ����� ������
                //�������, ��� ������ ����� ���������������. ������� �����.     
                string[] TableNames = 
                {
                    "Analitic.Analitics", "BookKeep.Accounts", "Core.Branches",
                    "Core.CustomViewLists", "Core.Libraries",                   /*Core.CustomViewLists*/
                    "Core.Folders",                                             /*Core.Libraries, Core.CustomViewLists*/
                    "Core.DocumentTypes", "Document.DocumentTypeView",
                    "Core.Files",
                    "Contractor.Agents", "Secure.Users",                        /*Contractor.Agents*/
                    "Contractor.BankAccounts",                                  /*Contractor.Agents*/
                    "Core.DatabaseLogs",
                    "Core.ErrorLogs",
                    "Core.ChainKinds", "Core.Culture",
                    "Core.Entities",                                            /*Core.Culture*/
                    "Core.SystemParameters",                                    /*Core.Entities*/
                    "Core.Currencies", "Core.Rates",                            /*Core.Currencies*/
                    "Core.XmlStorages",
                    "Document.Documents",                                       /*Contractor.Agents, Core.Currencies, Core.Files*/
                    "Core.FlagValues",
                    "Core.ResourceImages", "Core.ResourceStrings", "Core.RuleSets",
                    "Core.States", "Developer.DbObjects", "Fact.FactNames",
                    "Core.Units", "Product.Products",                           /*Core.Units, Contractor.Agents, Analitic.Analitics*/
                    "BookKeep.Documents","Contracts.Documents","Finance.Documents","Price.Documents","Sales.Documents","Store.Documents",/*Contractor.BankAccounts, Contractor.Agents, Document.Documents, Core.Units, Product.Products*/
                    "Price.PriceNames",
                    "Product.StorageCells",                                     /*Core.Units*/
                    "Report.ReportsAccounts", "Report.ReportsAnalitics", "Report.ReportsContactors", "Report.ReportsFolders", "Report.ReportsHierarchies", "Report.ReportsProducts",
                    "Secure.Rights",
                    "Territory.Countries", "Territory.Territories", "Territory.Towns",
                    "Hierarchy.Hierarchies"                                     /*Core.CustomViewLists, Core.Libraries...*/
                };

                //�������, ������ ������� ������������ ������ � ���������� ���������. ������� �� �����.
                string[] DependentTableNames =
                {
                    "BookKeep.DocumentDetails",
                    "Contractor.Banks",
                    "Contractor.Companies",
                    "Contractor.Employers",
                    "Contractor.Peoples",
                    "Contractor.Agents",
                    "Contracts.DocumentDetails",
                    "Core.DocumentTypeMaps",
                    "Core.EntityKinds",
                    "Core.EntityMaps",
                    "Core.EntityProperties",
                    "Core.EntityPropertyGroups",
                    "Core.EntityPropertyNames",
                    "Core.CustomViewColumns",
                    "Document.Files",
                    "Document.Taxes",
                    "Document.Signatures",
                    "Fact.FactColumns",
                    "Finance.DocumentDetails",
                    "Developer.DbObjectChilds",
                    "Hierarchy.HierarchyContents",
                    "Product.ProductRecipes",
                    "Product.Recipes",
                    "Sales.DocumentDetails",
                    "Store.DocumentDetails",
                    "Store.Rests",
                    "Store.Series",

                    //Chains:
                    "Analitic.AnaliticChains",
                    "BookKeep.AccountChains",
                    //"BookKeep.DetailSalesChains",
                    //"BookKeep.DetailStoreChains",
                    //"BookKeep.SaleChains",
                    //"BookKeep.StoreChains",
                    "Contractor.AgentChains",
                    "Core.BrancheChains",
                    "Core.CurrencyChains",
                    //"Core.CustomViewListChains",
                    "Core.FileChains",
                    "Core.FolderChains",
                    "Core.LibraryChains",
                    "Core.UnitChains",
                    "Core.XmlStorageChains",
                    "Document.DocumentChains",
                    "Hierarchy.HierarchyChains",
                    //"Price.PriceListChains",
                    "Price.PriceNameChains",
                    //"Price.PriceValueChains",
                    "Product.ProductChains",
                    //"Sales.DocumentChains",
                    //"Sales.DocumentDetailChains",
                    "Secure.RightChains",
                    "Store.DetailSalesChains",
                    "Store.DocumentChains",
                    "Store.DocumentDetailChains",
                    "Territory.CountryChains",
                    "Territory.TerritoryChains"
                };
                #endregion

                //��� �� ������� ����� ����������?
                foreach (DataTable dt in ds.Tables)
                {
                    if (!TableNames.Contains(dt.TableName) && !DependentTableNames.Contains(dt.TableName))
                        throw new System.Exception("������� '" + dt.TableName + "' �� ��������� � ������� ������.");
                }

                //������ ������
                foreach (string name in TableNames)
                {
                    if (ds.Tables.Contains(name))
                        ImportDataTable(ds.Tables[name], MergeAction.Update | MergeAction.Create, "Guid", false, cnn);
                }

                if (cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
        }
    }

    [Flags]
    public enum MergeAction
    {
        None =0,
        Create=1,
        Update=2,
        Delete=4
    }
}