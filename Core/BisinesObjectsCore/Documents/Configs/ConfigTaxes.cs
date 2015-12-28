using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Настройки раздела "Управление налоговыми"
    /// </summary>
    /// <remarks>Используется в Web приложении</remarks>
    public class ConfigTaxes
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigTaxes()
        {
            Out = new ConfigDocumentBase();
            In = new ConfigDocumentBase();
            CorOut = new ConfigDocumentBase();
            CorIn = new ConfigDocumentBase();
            
        }
        /// <summary>
        /// Сброс настроек на значения по умолчанию
        /// </summary>
        public void Reset()
        {
            Out.Reset();
            In.Reset();
            CorIn.Reset();
            CorOut.Reset();
        }
        /// <summary>
        /// Настройки документа "Налоговая накладная"
        /// </summary>
        public ConfigDocumentBase Out { get; set; }

        /// <summary>
        /// Настройки документа "Входящая налоговая накладная "
        /// </summary>
        public ConfigDocumentBase In { get; set; }

        /// <summary>
        /// Настройки документа "Корректировочная налоговая накладная"
        /// </summary>
        public ConfigDocumentBase CorOut { get; set; }

        /// <summary>
        /// Настройки документа "Входящая корректировочная налоговая накладная"
        /// </summary>
        public ConfigDocumentBase CorIn { get; set; }

        
        #region Сериализация
        ///<summary>
        /// Сериализация
        ///</summary>
        ///<returns>Результирующий Xml</returns>
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
        /// Десериализация
        ///</summary>
        ///<param name="xml">Исходный Xml</param>
        ///<returns>Десериализованный объект</returns>
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
        /// Настройки раздела "Управление продажами" для компании
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="companyId">Идентификатор компании</param>
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
               
                // пробуем искать в базе
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