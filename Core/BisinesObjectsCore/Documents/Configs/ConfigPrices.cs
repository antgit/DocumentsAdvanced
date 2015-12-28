using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BusinessObjects.Documents
{
    /// <summary>
    /// Настройки раздела "Управление ценами"
    /// </summary>
    /// <remarks>Используется в Web приложении</remarks>
    public class ConfigPrices
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ConfigPrices()
        {
            PriceList= new ConfigDocumentBase();
            PriceListInd = new ConfigDocumentBase();
            PriceListSupplier = new ConfigDocumentBase();
            PriceListCompetitor = new ConfigDocumentBase();
            PriceListCompetitorInd = new ConfigDocumentBase();

            Command = new ConfigDocumentBase();
            CommandInd = new ConfigDocumentBase();
        }
        /// <summary>
        /// Сброс настроек на значения по умолчанию
        /// </summary>
        public void Reset()
        {
            Command.Reset();
            CommandInd.Reset();
            PriceList.Reset();
            PriceListInd.Reset();
            PriceListSupplier.Reset();
            PriceListCompetitor.Reset();
            PriceListCompetitorInd.Reset();

            SalePriceOutByDocDate = false;
            ServicePriceOutByDocDate = false;
            TaxPriceOutByDocDate = false;
        }
        /// <summary>
        /// Использовать механизм ценообразования по дате документа или по текущей системной дате
        /// </summary>
        public bool SalePriceOutByDocDate { get; set; }
        /// <summary>
        /// Использовать механизм ценообразования по дате документа или по текущей системной дате
        /// </summary>
        public bool ServicePriceOutByDocDate { get; set; }
        /// <summary>
        /// Использовать механизм ценообразования по дате документа или по текущей системной дате
        /// </summary>
        public bool TaxPriceOutByDocDate { get; set; }

        /// <summary>
        /// Настройки документа "Приказ на изменение цен"
        /// </summary>
        public ConfigDocumentBase Command { get; set; }

        /// <summary>
        /// Настройки документа "Приказ на изменение индивидуальныйх цен"
        /// </summary>
        public ConfigDocumentBase CommandInd { get; set; }

        /// <summary>
        /// Настройки документа "Прайс-лист"
        /// </summary>
        public ConfigDocumentBase PriceList { get; set; }


        /// <summary>
        /// Настройки документа "Прайс-лист индивидуальный"
        /// </summary>
        public ConfigDocumentBase PriceListInd { get; set; }


        /// <summary>
        /// Настройки документа "Прайс-лист поставщика"
        /// </summary>
        public ConfigDocumentBase PriceListSupplier { get; set; }

        /// <summary>
        /// Настройки документа "Прайс-лист конкурента"
        /// </summary>
        public ConfigDocumentBase PriceListCompetitor { get; set; }
        /// <summary>
        /// Настройки документа "Прайс-лист конкурента индивидуальный"
        /// </summary>
        public ConfigDocumentBase PriceListCompetitorInd { get; set; }
        #region Сериализация
        ///<summary>
        /// Сериализация
        ///</summary>
        ///<returns>Результирующий Xml</returns>
        public string Save()
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigPrices));
                serializer.Serialize(sw, this);
                return sw.ToString();
            }

        }

        ///<summary>
        /// Десериализация
        ///</summary>
        ///<param name="xml">Исходный Xml</param>
        ///<returns>Десериализованный объект</returns>
        static public ConfigPrices Load(string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigPrices));
                    return serializer.Deserialize(sr) as ConfigPrices;
                }
            }
            catch
            {
                ConfigPrices data = new ConfigPrices();
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
        public static ConfigPrices GetConfig(Workarea wa, int companyId)
        {
            ConfigPrices currentConfig = new ConfigPrices();
            currentConfig.Reset();
            if (companyId != 0)
            {
                foreach (DocumentPricesConfig v in wa.Cashe.GetCasheData<DocumentPricesConfig>().Dictionary.Values)
                {
                    if (v.Document != null && v.Kind == DocumentPrices.KINDID_CONFIG &&
                        v.Document.AgentDepartmentFromId == companyId)
                    {
                        return v.Config;
                    }
                }
                // пробуем искать в базе
                List<Document> docs = Document.GetCollectionDocumentByAgent(wa, companyId, DocumentPrices.KINDID_CONFIG, System.Data.SqlTypes.SqlDateTime.MinValue.Value, System.Data.SqlTypes.SqlDateTime.MaxValue.Value);
                Document d = docs.FirstOrDefault(f => f.AgentDepartmentFromId == companyId);
                if (d != null)
                {
                    DocumentPricesConfig salesDoc = wa.Cashe.GetCasheData<DocumentPricesConfig>().Item(d.Id);
                    return salesDoc.Config;
                }
            }

            return currentConfig;
        }
    }
}
