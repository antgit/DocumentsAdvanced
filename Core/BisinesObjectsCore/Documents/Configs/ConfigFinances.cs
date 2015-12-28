using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// ��������� ������� "���������� ���������"
    /// </summary>
    /// <remarks>������������ � Web ����������</remarks>
    public class ConfigFinances
    {
        /// <summary>
        /// �����������
        /// </summary>
        public ConfigFinances()
        {
            Out = new ConfigDocumentBase();
            In = new ConfigDocumentBase();
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
            ReturnIn.Reset();
            ReturnOut.Reset();
        }
        /// <summary>
        /// ��������� ��������� "��������� ������"
        /// </summary>
        public ConfigDocumentBase Out { get; set; }

        /// <summary>
        /// ��������� ��������� "�������� ������"
        /// </summary>
        public ConfigDocumentBase In { get; set; }

        /// <summary>
        /// ��������� ��������� "������� �������� ������� ����������"
        /// </summary>
        public ConfigDocumentBase ReturnOut { get; set; }

        /// <summary>
        /// ��������� ��������� "������� �������� ������� �� ����������"
        /// </summary>
        public ConfigDocumentBase ReturnIn { get; set; }


        #region ������������
        ///<summary>
        /// ������������
        ///</summary>
        ///<returns>�������������� Xml</returns>
        public string Save()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigFinances));
                serializer.Serialize(sw, this);
                return sw.ToString();
            }

        }

        ///<summary>
        /// ��������������
        ///</summary>
        ///<param name="xml">�������� Xml</param>
        ///<returns>����������������� ������</returns>
        static public ConfigFinances Load(string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigFinances));
                    return serializer.Deserialize(sr) as ConfigFinances;
                }
            }
            catch
            {
                ConfigFinances data = new ConfigFinances();
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
        public static ConfigFinances GetConfig(Workarea wa, int companyId)
        {
            ConfigFinances currentConfig = new ConfigFinances();
            currentConfig.Reset();
            if (companyId != 0)
            {
                DocumentFinanceConfig df = wa.Cashe.GetCasheData<DocumentFinanceConfig>().Dictionary.Values.FirstOrDefault(f =>
                                                                                                                       f.Document != null
                                                                                                                       && f.Kind == DocumentFinance.KINDID_CONFIG
                                                                                                                       && f.Document.AgentDepartmentFromId == companyId);
                if (df != null)
                    return df.Config;

                // ������� ������ � ����
                List<Document> docs = Document.GetCollectionDocumentByAgent(wa, companyId, DocumentFinance.KINDID_CONFIG, System.Data.SqlTypes.SqlDateTime.MinValue.Value, System.Data.SqlTypes.SqlDateTime.MaxValue.Value);
                Document d = docs.FirstOrDefault(f => f.AgentDepartmentFromId == companyId);
                if (d != null)
                {
                    DocumentFinanceConfig salesDoc = wa.Cashe.GetCasheData<DocumentFinanceConfig>().Item(d.Id);
                    return salesDoc.Config;
                }
            }


            return currentConfig;
        }

    }
}