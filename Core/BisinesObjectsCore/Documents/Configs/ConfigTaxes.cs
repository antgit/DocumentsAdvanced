using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// ��������� ������� "���������� ����������"
    /// </summary>
    /// <remarks>������������ � Web ����������</remarks>
    public class ConfigTaxes
    {
        /// <summary>
        /// �����������
        /// </summary>
        public ConfigTaxes()
        {
            Out = new ConfigDocumentBase();
            In = new ConfigDocumentBase();
            CorOut = new ConfigDocumentBase();
            CorIn = new ConfigDocumentBase();
            
        }
        /// <summary>
        /// ����� �������� �� �������� �� ���������
        /// </summary>
        public void Reset()
        {
            Out.Reset();
            In.Reset();
            CorIn.Reset();
            CorOut.Reset();
        }
        /// <summary>
        /// ��������� ��������� "��������� ���������"
        /// </summary>
        public ConfigDocumentBase Out { get; set; }

        /// <summary>
        /// ��������� ��������� "�������� ��������� ��������� "
        /// </summary>
        public ConfigDocumentBase In { get; set; }

        /// <summary>
        /// ��������� ��������� "���������������� ��������� ���������"
        /// </summary>
        public ConfigDocumentBase CorOut { get; set; }

        /// <summary>
        /// ��������� ��������� "�������� ���������������� ��������� ���������"
        /// </summary>
        public ConfigDocumentBase CorIn { get; set; }

        
        #region ������������
        ///<summary>
        /// ������������
        ///</summary>
        ///<returns>�������������� Xml</returns>
        public string Save()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigTaxes));
                serializer.Serialize(sw, this);
                return sw.ToString();
            }

        }

        ///<summary>
        /// ��������������
        ///</summary>
        ///<param name="xml">�������� Xml</param>
        ///<returns>����������������� ������</returns>
        static public ConfigTaxes Load(string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigTaxes));
                    return serializer.Deserialize(sr) as ConfigTaxes;
                }
            }
            catch
            {
                ConfigTaxes data = new ConfigTaxes();
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
        public static ConfigTaxes GetConfig(Workarea wa, int companyId)
        {
            ConfigTaxes currentConfig = new ConfigTaxes();
            currentConfig.Reset();
            if (companyId != 0)
            {
                DocumentTaxesConfig df = wa.Cashe.GetCasheData<DocumentTaxesConfig>().Dictionary.Values.FirstOrDefault(f =>
                                                                                                                       f.Document != null
                                                                                                                       && f.Kind == DocumentTaxes.KINDID_CONFIG
                                                                                                                       && f.Document.AgentDepartmentFromId == companyId);
                if (df != null)
                    return df.Config;
               
                // ������� ������ � ����
                List<Document> docs = Document.GetCollectionDocumentByAgent(wa, companyId, DocumentTaxes.KINDID_CONFIG, System.Data.SqlTypes.SqlDateTime.MinValue.Value, System.Data.SqlTypes.SqlDateTime.MaxValue.Value);
                Document d = docs.FirstOrDefault(f => f.AgentDepartmentFromId == companyId);
                if (d != null)
                {
                    DocumentTaxesConfig salesDoc = wa.Cashe.GetCasheData<DocumentTaxesConfig>().Item(d.Id);
                    return salesDoc.Config;
                }
            }
            

            return currentConfig;
        }

    }
}