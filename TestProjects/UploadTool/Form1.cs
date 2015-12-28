using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadTool.WebOrdersServiceReference;

namespace UploadTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private string completeMessage = string.Empty;

        private Dictionary<int, string> messages = new Dictionary<int, string>() {{-1, "Ошибка"}, {0, "Обновлено"}, {1, "Закачано"}};

        private const string USERNAME = "WebAdmin";
        private const string PASSWORD = "123456";

        private DataTable LoadAnaliticByCode(string code)
        {
            string cnnStr = ConfigurationManager.ConnectionStrings["AccentDB"].ConnectionString;
            
            using(SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();

                using(SqlCommand cmd=cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("SELECT m.MSC_GUID AS [Guid], m.MSC_NAME AS [Name]" +
                                                    "FROM dbo.MISC m WHERE m.MSC_NO = (SELECT TOP 1 MSC_NO from dbo.MISC WHERE MSC_CODE='{0}' AND MSC_TYPE=-1)" +
                                                    "AND m.MSC_TYPE=1", code);

                    using(SqlDataAdapter adapter= new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable=new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        private void UploadAnaliticByCode(string code, DataTable dt)
        {
            using (WebOrdersServiceClient client = new WebOrdersServiceClient())
            {
                client.Open();

                if (!client.Login(USERNAME, PASSWORD))
                {
                    completeMessage = "Ошибка авторизации";
                    return;
                }

                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Guid guid = (Guid)row[0];
                    string name = (string)row[1];
                    int res=-1;

                    switch (code)
                    {
                        case "BRAND":
                            res = client.CreateBrand(guid, name);
                            break;
                        case "PRODUCTTYPE":
                            res = client.CreateProductType(guid, name);
                            break;
                        case "TRADEMARK":
                            res = client.CreateTradeMark(guid, name);
                            break;
                    }

                    TextOut(string.Format("{0}: '{1}'({2})",messages[res], name, guid));
                    //progressBar1.Value++;
                    i++;
                    backgroundWorker1.ReportProgress(i * 100 / dt.Rows.Count);
                }

                completeMessage = "Оперция успешно завершена";
            }
        }

        private DataTable LoadProducts()
        {
            string cnnStr = ConfigurationManager.ConnectionStrings["AccentDB"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                cnn.Open();

                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "dbo.[ExportProductsFromAccent]";
                    cmd.Parameters.AddWithValue("@ds", DateTime.Now);
                    cmd.Parameters.AddWithValue("@BrandParamName", ConfigurationManager.AppSettings["BrandParamName"]);
                    cmd.Parameters.AddWithValue("@ProductTypeName", ConfigurationManager.AppSettings["ProductTypeName"]);
                    cmd.Parameters.AddWithValue("@TradeMarkParamName", ConfigurationManager.AppSettings["TradeMarkParamName"]);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        private void TextOut(string message)
        {
            textBox1.Invoke((MethodInvoker) delegate
                                                {
                                                    textBox1.AppendText(string.Format(message));
                                                    textBox1.AppendText(Environment.NewLine);
                                                });
        }

        private void UploadProducts(DataTable dt)
        {
            using (WebOrdersServiceClient client = new WebOrdersServiceClient())
            {
                client.Open();

                if (!client.Login(USERNAME, PASSWORD))
                {
                    completeMessage = "Ошибка авторизации";
                    return;
                }

                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    Guid guid = (Guid)row["Guid"];
                    string name = (string)row["Name"];
                    string nomenclature = Convert.IsDBNull(row["Nomenclature"]) ? "" : (string)row["Nomenclature"];
                    decimal price = (decimal) row["Price"];
                    string brandName = Convert.IsDBNull(row["BrandName"]) ? "" : (string)row["BrandName"];
                    string tradeMarkName = Convert.IsDBNull(row["TradeMarkName"]) ? "" : (string) row["TradeMarkName"];
                    string productTypeName = Convert.IsDBNull(row["ProductTypeName"]) ? "" : (string)row["ProductTypeName"];

                    int res = client.CreateProduct(guid, name, nomenclature, price, brandName, tradeMarkName, productTypeName);

                    textBox1.Invoke((MethodInvoker)delegate
                    {
                        TextOut(string.Format("{0}: '{1}'({2})", messages[res], name, guid));
                    });

                    //progressBar1.Value++;
                    i++;
                    backgroundWorker1.ReportProgress(i * 100 / dt.Rows.Count);
                }

                TextOut("===Применение цен===");
                client.CreateProductEnd();

                completeMessage = "Оперция успешно завершена";
            }
        }

        private void btnUploadAnalitics_Click(object sender, EventArgs e)
        {
            string[] analiticCodes = { "BRAND", "PRODUCTTYPE", "TRADEMARK" };
            string[] analiticMessages = { "Загрузка брендов", "Загрузка видов продукции", "Загрузка торговых групп" };

            textBox1.ResetText();
            progressBar1.Maximum = 100; //dt.Rows.Count;


            backgroundWorker1 = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
            backgroundWorker1.DoWork += delegate
                                            {
                                                for (int index = 0; index < analiticCodes.Length; index++)
                                                {
                                                    var code = analiticCodes[index];
                                                    TextOut("===" + analiticMessages[index] + " ===");
                                                    DataTable dt = LoadAnaliticByCode(code);
                                                    //progressBar1.Value = 0;
                                                    backgroundWorker1.ReportProgress(0);
                                                    UploadAnaliticByCode(code, dt);
                                                }
                                            };
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += delegate { MessageBox.Show(completeMessage); };
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnUploadProducts_Click(object sender, EventArgs e)
        {
            DataTable dt = LoadProducts();
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            textBox1.ResetText();

            backgroundWorker1 = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            backgroundWorker1.DoWork += delegate { UploadProducts(dt); };
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += delegate { MessageBox.Show(completeMessage); };
            backgroundWorker1.RunWorkerAsync();
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}
