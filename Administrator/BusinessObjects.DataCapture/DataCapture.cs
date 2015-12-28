using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.DataCapture
{
    internal class DataCapture
    {
        public string ConnectionString { get; set; }

        public bool Enabled
        {
            get
            {
                using (SqlConnection cnn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {

                        try
                        {
                            cmd.CommandText = "SELECT d.[name] FROM sys.databases d WHERE d.is_cdc_enabled =1 and d.database_id= DB_ID()";
                            cmd.Connection.Open();
                            object res = cmd.ExecuteScalar();
                            return (res != null);
                        }
                        finally
                        {
                            if (cnn.State == ConnectionState.Open)
                                cnn.Close();
                        }
                    }
                }
            }
            set 
            {
                if (value && !Enabled)
                    SetEnabledCdc();
                if(!value && Enabled)
                    SetDisableCdc();
            }
        }

        private void SetDisableCdc()
        {
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sys.sp_cdc_disable_db";
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                    }
                }
            }
        }
        private void SetEnabledCdc()
        {
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sys.sp_cdc_enable_db";
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                    }
                }
            }
        }

        private List<CaptureTable> _captureTable;
        /// <summary>
        /// —писок соответстви€ таблиц базы данных и таблиц протоколировани€
        /// </summary>
        public List<CaptureTable> CaptureTable
        {
            get
            {
                if (_captureTable == null)
                {
                    RefreshCapturedTables();
                }
                return _captureTable;
            }
        }

        private void RefreshCapturedTables()
        {
            _captureTable = new List<CaptureTable>();
            if (!Enabled)
                return;
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    try
                    {
                        const string sql = "SELECT t.TABLE_SCHEMA, t.TABLE_NAME, ct.INSTANCE, ct.[FILEGROUP_NAME], ct.ROLE_NAME \n"
                                           + "  FROM INFORMATION_SCHEMA.TABLES t LEFT JOIN  Export.Change_Tables ct ON t.TABLE_SCHEMA=ct.SOURCE_SCHEMA AND t.TABLE_NAME=ct.SOURCE_NAME \n"
                                           + "WHERE t.TABLE_TYPE = 'BASE TABLE' and t.TABLE_NAME NOT LIKE '%History' AND t.TABLE_SCHEMA<>'cdc' AND t.TABLE_SCHEMA<>'dbo' \n"
                                           + "ORDER BY t.TABLE_SCHEMA,t.TABLE_NAME";
                        cmd.CommandText = sql;
                        cmd.Connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CaptureTable item = new CaptureTable
                                                    {
                                                        Schema = reader.GetString(0),
                                                        Name = reader.GetString(1),
                                                        Instans =
                                                            reader.IsDBNull(2)
                                                                ? string.Empty
                                                                : reader.GetString(2),
                                                        FileGroup =
                                                            reader.IsDBNull(3)
                                                                ? string.Empty
                                                                : reader.GetString(3),
                                                        RoleName =
                                                            reader.IsDBNull(4)
                                                                ? string.Empty
                                                                : reader.GetString(4)
                                                    };
                            _captureTable.Add(item);
                        }
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                    }
                }
            }
        }

        public void Refresh()
        {
            _captureTable = null;
            RefreshCapturedTables();
        }

        public void EnableTable(CaptureTable value)
        {
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sys.sp_cdc_enable_table";
                        cmd.Parameters.Add(GlobalSqlParamNames.source_schema, SqlDbType.NVarChar, 100).Value = value.Schema;
                        cmd.Parameters.Add(GlobalSqlParamNames.source_name, SqlDbType.NVarChar, 100).Value = value.Name;
                        cmd.Parameters.Add(GlobalSqlParamNames.role_name, SqlDbType.NVarChar, 100).Value = DBNull.Value;
                        cmd.Parameters.Add(GlobalSqlParamNames.capture_instance, SqlDbType.NVarChar, 100).Value = value.Schema + "_" + value.Name;
                        cmd.Parameters.Add(GlobalSqlParamNames.filegroup_name, SqlDbType.NVarChar, 100).Value = "HISTORY";
                        cmd.Parameters.Add(GlobalSqlParamNames.RC, SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        object res = cmd.Parameters[GlobalSqlParamNames.RC].Value;
                        if (res != null)
                        {
                            if ((int)res == 0)
                            {
                                value.Instans = value.Schema + "_" + value.Name;
                                value.FileGroup = "HISTORY";
                            }
                        }
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                    }
                }
            }
        }
        public void TableDisable(CaptureTable value)
        {
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sys.sp_cdc_disable_table";
                        cmd.Parameters.Add(GlobalSqlParamNames.source_schema, SqlDbType.NVarChar, 100).Value = value.Schema;
                        cmd.Parameters.Add(GlobalSqlParamNames.source_name, SqlDbType.NVarChar, 100).Value = value.Name;
                        cmd.Parameters.Add(GlobalSqlParamNames.capture_instance, SqlDbType.NVarChar, 100).Value = value.Instans;
                        cmd.Parameters.Add(GlobalSqlParamNames.RC, SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        object res = cmd.Parameters[GlobalSqlParamNames.RC].Value;
                        if (res != null)
                        {
                            if ((int)res == 0)
                            {
                                value.Instans = string.Empty;
                                value.FileGroup = string.Empty;
                            }
                        }
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                    }
                }
            }
        }

        
        // TODO: ѕросмотр изменений...
        public DataTable GetNetChanges(DateTime start, DateTime end, CaptureTable value)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {

                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DECLARE @from_lsn binary(10), @to_lsn binary(10); \n"
                                          + "SET @from_lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than or equal', @ds); \n"
                                          + "SET @to_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal', @de); \n"
                                          + "SELECT * FROM cdc.fn_cdc_get_net_changes_"+ value.Instans +"(@from_lsn, @to_lsn, 'all');";
                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.DateTime).Value = start;
                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.DateTime).Value = end;
                        cmd.Parameters.Add(GlobalSqlParamNames.RC, SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                        cmd.Connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(tbl);
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                    }
                }
            }
            return tbl;
        }
        /*
         --------------------------------------------------------------- 
DECLARE @from_lsn binary(10), @to_lsn binary(10)
SET @from_lsn =
   sys.fn_cdc_get_min_lsn('HR_Department')
SET @to_lsn   = sys.fn_cdc_get_max_lsn()
SELECT * FROM cdc.fn_cdc_get_all_changes_HR_Department
  (@from_lsn, @to_lsn, N'all')
GO

EXECUTE sys.sp_cdc_get_ddl_history 
    @capture_instance = 'HumanResources_Employee';

--------------------------------------------------------------
SET @begin_time = GETDATE() -1;
-- Obtain the end of the time interval.
SET @end_time = GETDATE();
-- Map the time interval to a change data capture query range.
SET @from_lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than or equal', @begin_time);
SET @to_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal', @end_time);

-- Return the net changes occurring within the query window.
SELECT * FROM cdc.fn_cdc_get_net_changes_HR_Department(@from_lsn, @to_lsn, 'all');
         */
    }
}