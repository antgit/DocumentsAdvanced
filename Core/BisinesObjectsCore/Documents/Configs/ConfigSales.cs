using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BusinessObjects.Documents.Person;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// ��������� ������� "���������� ���������"
    /// </summary>
    /// <remarks>������������ � Web ����������</remarks>
    public class ConfigSales
    {
        /// <summary>
        /// �����������
        /// </summary>
        public ConfigSales()
        {
            Out = new ConfigDocumentBase();
            In = new ConfigDocumentBase();
            AccountOut = new ConfigDocumentBase();
            AccountIn = new ConfigDocumentBase();
            OrderOut = new ConfigDocumentBase();
            OrderIn = new ConfigDocumentBase();
            AssortOut = new ConfigDocumentBase();
            AssortIn = new ConfigDocumentBase();
            Move = new ConfigDocumentBase();
            Inventory = new ConfigDocumentBase();
            ReturnOut = new ConfigDocumentBase();
            ReturnIn = new ConfigDocumentBase();
        }
        /// <summary>
        /// ����� �������� �� �������� �� ���������
        /// </summary>
        public void Reset()
        {
            Out.Reset();
            In.Reset();
            AccountIn.Reset();
            AccountOut.Reset();
            OrderIn.Reset();
            OrderOut.Reset();

            AssortIn.Reset();
            AssortOut.Reset();
            Move.Reset();
            Inventory.Reset();
            ReturnIn.Reset();
            ReturnOut.Reset();
        }
        /// <summary>
        /// ��������� ��������� "��������� ���������"
        /// </summary>
        public ConfigDocumentBase Out { get; set; }

        /// <summary>
        /// ��������� ��������� "��������� ���������"
        /// </summary>
        public ConfigDocumentBase In { get; set; }

        /// <summary>
        /// ��������� ��������� "���� ���������"
        /// </summary>
        public ConfigDocumentBase AccountOut { get; set; }

        /// <summary>
        /// ��������� ��������� "���� ��������"
        /// </summary>
        public ConfigDocumentBase AccountIn { get; set; }

        /// <summary>
        /// ��������� ��������� "����� ���������"
        /// </summary>
        public ConfigDocumentBase OrderOut { get; set; }

        /// <summary>
        /// ��������� ��������� "����� ��������"
        /// </summary>
        public ConfigDocumentBase OrderIn { get; set; }

        /// <summary>
        /// ��������� ��������� "�������������� ���� ���������"
        /// </summary>
        public ConfigDocumentBase AssortOut { get; set; }

        /// <summary>
        /// ��������� ��������� "�������������� ���� ��������"
        /// </summary>
        public ConfigDocumentBase AssortIn { get; set; }

        /// <summary>
        /// ��������� ��������� "������� ����������"
        /// </summary>
        public ConfigDocumentBase ReturnOut { get; set; }

        /// <summary>
        /// ��������� ��������� "������� ����������"
        /// </summary>
        public ConfigDocumentBase ReturnIn { get; set; }

        /// <summary>
        /// ��������� ��������� "�����������"
        /// </summary>
        public ConfigDocumentBase Move { get; set; }

        /// <summary>
        /// ��������� ��������� "��������������"
        /// </summary>
        public ConfigDocumentBase Inventory { get; set; }

        #region ������������
        ///<summary>
        /// ������������
        ///</summary>
        ///<returns>�������������� Xml</returns>
        public string Save()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigSales));
                serializer.Serialize(sw, this);
                return sw.ToString();
            }

        }

        ///<summary>
        /// ��������������
        ///</summary>
        ///<param name="xml">�������� Xml</param>
        ///<returns>����������������� ������</returns>
        static public ConfigSales Load(string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof (ConfigSales));
                    return serializer.Deserialize(sr) as ConfigSales;
                }
            }
            catch
            {
                ConfigSales data = new ConfigSales();
                return data;
            }
        }
        #endregion

        /// <summary>
        /// ��������� ������� "���������� ���������" ��� ��������
        /// </summary>
        /// <param name="wa">������� �������</param>
        /// <param name="companyId">������������� ��������</param>
        /// <returns></returns>
        public static ConfigSales GetConfig(Workarea wa, int companyId)
        {
            ConfigSales currentConfig = new ConfigSales();
            currentConfig.Reset();
            if(companyId!=0)
            {
                DocumentSalesConfig df = wa.Cashe.GetCasheData<DocumentSalesConfig>().Dictionary.Values.FirstOrDefault(f =>
                    f.Document != null
                    && f.Kind == DocumentSales.KINDID_CONFIG
                    && f.Document.AgentDepartmentFromId == companyId);
                if (df != null)
                    return df.Config;
                //foreach (DocumentSalesConfig v in wa.Cashe.GetCasheData<DocumentSalesConfig>().Dictionary.Values)
                //{
                //    if (v.Document != null && v.Kind == DocumentSales.KINDID_CONFIG &&
                //        v.Document.AgentDepartmentFromId == companyId)
                //    {
                //        return v.Config;
                //    }
                //}
                // ������� ������ � ����
                List<Document> docs = Document.GetCollectionDocumentByAgent(wa, companyId, DocumentSales.KINDID_CONFIG, System.Data.SqlTypes.SqlDateTime.MinValue.Value, System.Data.SqlTypes.SqlDateTime.MaxValue.Value);
                Document d = docs.FirstOrDefault(f => f.AgentDepartmentFromId == companyId);
                if (d != null)
                {
                    DocumentSalesConfig salesDoc = wa.Cashe.GetCasheData<DocumentSalesConfig>().Item(d.Id);
                    return salesDoc.Config;
                }
            }
            //if(companyId!=0)
            //{
            //    foreach (DocumentSales v in wa.Cashe.GetCasheData<DocumentSales>().Dictionary.Values)
            //    {
            //        if (v.Document != null && v.Kind == DocumentSales.KINDID_CONFIG &&
            //            v.Document.AgentDepartmentFromId == companyId)
            //        {
            //            List<DocumentXml> coll = v.Document.GetXmlData();
            //            if(coll.Count>0)
            //            {
            //                currentConfig = Load(coll[0].Xml);
            //                return currentConfig;
            //            }
            //        }
            //    }

            //    // ������� ������ � ����
            //    List<Document> docs = Document.GetCollectionDocumentByAgent(wa, companyId, DocumentSales.KINDID_CONFIG, System.Data.SqlTypes.SqlDateTime.MinValue.Value, System.Data.SqlTypes.SqlDateTime.MaxValue.Value);
            //    Document d = docs.FirstOrDefault(f => f.AgentDepartmentFromId == companyId);
            //    if (d != null)
            //    {
            //        DocumentSales salesDoc = wa.Cashe.GetCasheData<DocumentSales>().Item(d.Id);
            //        List<DocumentXml> coll = salesDoc.Document.GetXmlData();
            //        if (coll.Count > 0)
            //        {
            //            currentConfig = Load(coll[0].Xml);
            //            return currentConfig;
            //        }
            //    }
            //}
            
            return currentConfig;
        }
        
    }
}