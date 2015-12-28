using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BusinessObjects.Documents.Person;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Настройки раздела "Управление продажами"
    /// </summary>
    /// <remarks>Используется в Web приложении</remarks>
    public class ConfigSales
    {
        /// <summary>
        /// Конструктор
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
        /// Сброс настроек на значения по умолчанию
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
        /// Настройки документа "Расходная накладная"
        /// </summary>
        public ConfigDocumentBase Out { get; set; }

        /// <summary>
        /// Настройки документа "Приходная накладная"
        /// </summary>
        public ConfigDocumentBase In { get; set; }

        /// <summary>
        /// Настройки документа "Счет исходящий"
        /// </summary>
        public ConfigDocumentBase AccountOut { get; set; }

        /// <summary>
        /// Настройки документа "Счет входящий"
        /// </summary>
        public ConfigDocumentBase AccountIn { get; set; }

        /// <summary>
        /// Настройки документа "Заказ исходящий"
        /// </summary>
        public ConfigDocumentBase OrderOut { get; set; }

        /// <summary>
        /// Настройки документа "Заказ входящий"
        /// </summary>
        public ConfigDocumentBase OrderIn { get; set; }

        /// <summary>
        /// Настройки документа "Ассортиментный лист исходящий"
        /// </summary>
        public ConfigDocumentBase AssortOut { get; set; }

        /// <summary>
        /// Настройки документа "Ассортиментный лист входящий"
        /// </summary>
        public ConfigDocumentBase AssortIn { get; set; }

        /// <summary>
        /// Настройки документа "Возврат поставщику"
        /// </summary>
        public ConfigDocumentBase ReturnOut { get; set; }

        /// <summary>
        /// Настройки документа "Возврат покупателя"
        /// </summary>
        public ConfigDocumentBase ReturnIn { get; set; }

        /// <summary>
        /// Настройки документа "Перемещение"
        /// </summary>
        public ConfigDocumentBase Move { get; set; }

        /// <summary>
        /// Настройки документа "Инвентаризация"
        /// </summary>
        public ConfigDocumentBase Inventory { get; set; }

        #region Сериализация
        ///<summary>
        /// Сериализация
        ///</summary>
        ///<returns>Результирующий Xml</returns>
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
        /// Десериализация
        ///</summary>
        ///<param name="xml">Исходный Xml</param>
        ///<returns>Десериализованный объект</returns>
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
        /// Настройки раздела "Управление продажами" для компании
        /// </summary>
        /// <param name="wa">Рабочая область</param>
        /// <param name="companyId">Идентификатор компании</param>
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
                // пробуем искать в базе
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

            //    // пробуем искать в базе
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