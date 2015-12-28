using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Специальный документ настройки раздела "Управление финансами"
    /// </summary>
    public class DocumentFinanceConfig:DocumentFinance
    {
        
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentFinanceConfig():base()
        {
            /*
             currentConfig = new ConfigFinances();
                        currentConfig.Reset();
             */
        }
        /// <summary>
        /// Создание копии документа
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DocumentFinanceConfig CreateCopy(DocumentFinanceConfig value)
        {

            DocumentFinanceConfig salesDoc = new DocumentFinanceConfig { Workarea = value.Workarea };
            using (SqlConnection cnn = value.Workarea.GetDatabaseConnection())
            {
                if (cnn == null) return null;
                try
                {
                    using (SqlCommand sqlCmd = cnn.CreateCommand())
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = value.Id;
                        sqlCmd.CommandText = value.FindProcedure(GlobalMethodAlias.Copy); //"Core.CustomViewListCopy";
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {


                                salesDoc.Document = new Document { Workarea = value.Workarea };
                                salesDoc.Document.Load(reader);
                                if (reader.NextResult())
                                {
                                    if (reader.Read() && reader.HasRows)
                                        salesDoc.Load(reader);
                                }
                                salesDoc._details.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentDetailFinance docRow = new DocumentDetailFinance { Workarea = value.Workarea, Document = salesDoc };
                                            docRow.Load(reader);
                                            salesDoc._details.Add(docRow);
                                        }
                                    }
                                }
                                salesDoc._analitics.Clear();
                                if (reader.NextResult())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            DocumentAnalitic docRow = new DocumentAnalitic { Workarea = value.Workarea, Document = salesDoc.Document };
                                            docRow.Load(reader);
                                            salesDoc._analitics.Add(docRow);
                                        }
                                    }
                                }
                            }
                            reader.Close();
                            object retval = sqlCmd.Parameters[GlobalSqlParamNames.Return].Value;
                            if (retval == null)
                                throw new SqlReturnException(value.Workarea.Cashe.ResourceString("EX_MSG_DBUNCNOWNRESULTS", 1049));

                            if ((int)retval != 0)
                                throw new DatabaseException(value.Workarea.Cashe.ResourceString("EX_MSG_DBERRDATA", 1049), (int)retval);
                        }
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
            return salesDoc;
        }

        private ConfigFinances _config;
        /// <summary>
        /// Настройки раздела
        /// </summary>
        public ConfigFinances Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigFinances();
                    _config.Reset();
                    if (Id != 0)
                    {
                        _config = new ConfigFinances();
                        List<DocumentXml> coll = Document.GetXmlData();
                        if (coll != null && coll.Count > 0)
                        {
                            _config = ConfigFinances.Load(coll[0].Xml);
                        }
                    }
                }
                return _config;
            }
            set { _config = value; }
        }
        protected override void OnCreated()
        {
            base.OnCreated();
            SaveConfig();
        }

        private void SaveConfig()
        {
            List<DocumentXml> coll = Document.GetXmlData();
            if (coll.Count > 0)
            {
                coll[0].Xml = _config.Save();
                coll[0].Save();
            }
            else
            {
                DocumentXml newXmlData = Document.NewXmlDataRow();
                newXmlData.Xml = _config.Save();
                newXmlData.Save();
            }
        }

        protected override void OnUpdated()
        {
            base.OnUpdated();
            SaveConfig();
        }
    }
}