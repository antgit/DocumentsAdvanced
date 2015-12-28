using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows.Controls
{
    internal sealed partial class ControlSqlServerConnection : XtraUserControl
    {
        public ControlSqlServerConnection()
        {
            InitializeComponent();
        }
        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
        private string _ConnectionString;

        public string ConnectionString
        {
            get
            {
                builder.DataSource = this.cmbServer.Text;
                builder.InitialCatalog = this.cmbDataBase.Text;
                builder.IntegratedSecurity = this.chkWindows.Checked;
                this.chkSql.Checked = !this.chkWindows.Checked;
                if (!builder.IntegratedSecurity)
                {
                    builder.UserID = this.txtUserId.Text;
                    builder.Password = this.txtPassword.Text;
                }
                if (builder.ApplicationName == ".Net SqlClient Data Provider" || builder.ApplicationName.Length == 0)
                    builder.ApplicationName = "Documents System";
                builder.CurrentLanguage = "Russian";
                _ConnectionString = builder.ConnectionString;
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
                try
                {
                    builder.ConnectionString = _ConnectionString;
                    this.cmbServer.Text = builder.DataSource;
                    this.cmbDataBase.Text = builder.InitialCatalog;
                    if (builder.IntegratedSecurity)
                        this.chkWindows.Checked = true;
                    else
                    {
                        this.chkSql.Checked = true;
                        this.txtUserId.Text = builder.UserID;
                        this.txtPassword.Text = builder.Password;
                    }
                    if (!this.chkWindows.Checked && !this.chkSql.Checked)
                        this.chkWindows.Checked = true;
                }
                // TODO: 
                catch
                {

                }
            }
        }

        private void cmbServer_Popup(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (this.cmbServer.Properties.Items.Count == 0)
                {
                    System.Data.Sql.SqlDataSourceEnumerator servers = System.Data.Sql.SqlDataSourceEnumerator.Instance;
                    DataTable tbl = servers.GetDataSources();
                    foreach (DataRow row in tbl.Rows)
                    {
                        if (row.IsNull("InstanceName"))
                            this.cmbServer.Properties.Items.Add(row["ServerName"]);
                        else
                            this.cmbServer.Properties.Items.Add(row["ServerName"].ToString() + @"\" + row["InstanceName"].ToString());
                    }
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cmbDataBase_Popup(object sender, EventArgs e)
        {
            if (cmbDataBase.Properties.Items.Count == 0 && ConnectionString.Length > 0)
            {
                cmbDataBase.Text = string.Empty;
                try
                {
                    using (System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection(ConnectionString))
                    {
                        cnn.Open();
                        //cnn.ServerVersion
                        System.Data.SqlClient.SqlCommand cmd = cnn.CreateCommand();
                        if (cnn.ServerVersion.StartsWith("09"))
                            cmd.CommandText = "select name from sys.databases " +
                            "where state_desc<>'OFFLINE' " +
                            "and name not in ('master','tempdb', 'model', 'msdb')";
                        else
                            cmd.CommandText = "select name from sysdatabases " +
                            "where name not in ('master','tempdb', 'model', 'msdb')";

                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string dbname = reader.GetString(0);
                                builder.InitialCatalog = dbname;
                                try
                                {
                                    using (System.Data.SqlClient.SqlConnection cnn2 = new System.Data.SqlClient.SqlConnection(builder.ConnectionString))
                                    {
                                        cnn2.Open();
                                        System.Data.SqlClient.SqlCommand cmd2 = cnn2.CreateCommand();
                                        cmd2.CommandText = "if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'SystemParameters') select 1 else select 0";
                                        object val = cmd2.ExecuteScalar();
                                        if (val != null && val.ToString() == "1")
                                            cmbDataBase.Properties.Items.Add(dbname);
                                    }
                                }
                                // TODO:
                                catch (Exception)
                                {
                                }
                                finally
                                {
                                }

                            }
                        }
                    }

                }
                catch (System.Data.SqlClient.SqlException sqlex)
                {
                    MessageBox.Show(sqlex.Message);
                }
            }
        }

        private void chkSql_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSql.Checked)
                chkWindows.Checked = false;
        }

        private void chkWindows_CheckedChanged(object sender, EventArgs e)
        {
            if(chkWindows.Checked)
                chkSql.Checked = false;
        }
    }
}
