using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Настройки раздела "Управление финансами"
    /// </summary>
    /// <remarks>Используется в Web приложении</remarks>
    public class ConfigFinances
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigFinances()
        {
            Out = new ConfigDocumentBase();
            In = new ConfigDocumentBase();
            ReturnOut = new ConfigDocumentBase();
            ReturnIn = new ConfigDocumentBase();

        }
        /// <summary>
        /// Сброс настроек на значения по умолчанию
        /// </summary>
        public void Reset()
        {
            Out.Reset();
            In.Reset();
            ReturnIn.Reset();
            ReturnOut.Reset();
        }
        /// <summary>
        /// Настройки документа "Исходящие оплаты"
        /// </summary>
        public ConfigDocumentBase Out { get; set; }

        /// <summary>
        /// Настройки документа "Входящие оплаты"
        /// </summary>
        public ConfigDocumentBase In { get; set; }

        /// <summary>
        /// Настройки документа "Возврат денежных средств покупателю"
        /// </summary>
        public ConfigDocumentBase ReturnOut { get; set; }

        /// <summary>
        /// Настройки документа "Возврат денежных средств от покупателя"
        /// </summary>
        public ConfigDocumentBase ReturnIn { get; set; }


        #region Сериализация
        ///<summary>
        /// Сериализация
        ///</summary>
        ///<returns>Результирующий Xml</returns>
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
        /// Десериализация
        ///</summary>
        ///<param name="xml">Исходный Xml</param>
        ///<returns>Десериализованный объект</returns>
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
        /// Настройки раздела "Управление финансами" для компании
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="companyId">Идентификатор компании</param>
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

                // пробуем искать в базе
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