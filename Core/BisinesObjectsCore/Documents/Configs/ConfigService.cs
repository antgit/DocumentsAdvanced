using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// ��������� ������� "���������� ��������"
    /// </summary>
    /// <remarks>������������ � Web ����������</remarks>
    public class ConfigService
    {
        /// <summary>
        /// �����������
        /// </summary>
        public ConfigService()
        {
            Out = new ConfigDocumentBase();
            In = new ConfigDocumentBase();
            AccountOut = new ConfigDocumentBase();
            AccountIn = new ConfigDocumentBase();
            OrderOut = new ConfigDocumentBase();
            OrderIn = new ConfigDocumentBase();
            AssortOut = new ConfigDocumentBase();
            AssortIn = new ConfigDocumentBase();
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
            //Move.Reset();
            //Inventory.Reset();
            //ReturnIn.Reset();
            //ReturnOut.Reset();
        }
        /// <summary>
        /// ��������� ��������� "��� ����������� �����"
        /// </summary>
        public ConfigDocumentBase Out { get; set; }

        /// <summary>
        /// ��������� ��������� "��� ����������� ����� ��������"
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

        ///// <summary>
        ///// ��������� ��������� "������� ����������"
        ///// </summary>
        //public ConfigDocumentBase ReturnOut { get; set; }

        ///// <summary>
        ///// ��������� ��������� "������� ����������"
        ///// </summary>
        //public ConfigDocumentBase ReturnIn { get; set; }

        ///// <summary>
        ///// ��������� ��������� "�����������"
        ///// </summary>
        //public ConfigDocumentBase Move { get; set; }

        ///// <summary>
        ///// ��������� ��������� "��������������"
        ///// </summary>
        //public ConfigDocumentBase Inventory { get; set; }

        #region ������������
        ///<summary>
        /// ������������
        ///</summary>
        ///<returns>�������������� Xml</returns>
        public string Save()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigService));
                serializer.Serialize(sw, this);
                return sw.ToString();
            }

        }

        ///<summary>
        /// ��������������
        ///</summary>
        ///<param name="xml">�������� Xml</param>
        ///<returns>����������������� ������</returns>
        static public ConfigService Load(string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigService));
                    return serializer.Deserialize(sr) as ConfigService;
                }
            }
            catch
            {
                ConfigService data = new ConfigService();
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
        public static ConfigService GetConfig(Workarea wa, int companyId)
        {
            ConfigService currentConfig = new ConfigService();
            currentConfig.Reset();
            if (companyId != 0)
            {
                foreach (DocumentService v in wa.Cashe.GetCasheData<DocumentService>().Dictionary.Values)
                {
                    if (v.Document != null && v.Kind == DocumentService.KINDID_CONFIG &&
                        v.Document.AgentDepartmentFromId == companyId)
                    {
                        List<DocumentXml> coll = v.Document.GetXmlData();
                        if (coll.Count > 0)
                        {
                            currentConfig = Load(coll[0].Xml);
                            return currentConfig;
                        }
                    }
                }

                // ������� ������ � ����
                List<Document> docs = Document.GetCollectionDocumentByAgent(wa, companyId, DocumentSales.KINDID_CONFIG, System.Data.SqlTypes.SqlDateTime.MinValue.Value, System.Data.SqlTypes.SqlDateTime.MaxValue.Value);
                Document d = docs.FirstOrDefault(f => f.AgentDepartmentFromId == companyId);
                if (d != null)
                {
                    DocumentService salesDoc = wa.Cashe.GetCasheData<DocumentService>().Item(d.Id);
                    List<DocumentXml> coll = salesDoc.Document.GetXmlData();
                    if (coll.Count > 0)
                    {
                        currentConfig = Load(coll[0].Xml);
                        return currentConfig;
                    }
                }
            }

            return currentConfig;
        }

    }
}