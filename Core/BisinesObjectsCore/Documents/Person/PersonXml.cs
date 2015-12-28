using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BusinessObjects.Documents.Person
{
    /// <summary>
    /// Дополнительный класс для документов по персоналу
    /// </summary>
    public class PersonXml
    {
        public PersonXml()
        {
            
        }
        /// <summary>
        /// Дата принятия на работу
        /// </summary>
        /// <remarks>Используется в документе "Приказ о приеме на работу", "Приказ о перемещении"</remarks>
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// Дата увольнения
        /// </summary>
        /// <remarks>Используется в документе "Приказ о увольнении", "Приказ о перемещении"</remarks>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Идентификатор подразделения "Откуда"
        /// </summary>
        /// <remarks>Используется в документе "Приказ о увольнении", "Приказ о перемещении"</remarks>
        public int DepatmentFromId { get; set; }

        /// <summary>
        /// Идентификатор подразделения "Куда"
        /// </summary>
        /// <remarks>Используется в документе "Приказ о приеме на работу", "Приказ о перемещении"</remarks>
        public int DepatmentToId { get; set; }

        [OptionalField]
        private int _workPostId;

        /// <summary>
        /// Идентификатор должности
        /// </summary>
        
        public int WorkPostId
        {
            get { return _workPostId; }
            set { _workPostId = value; }
        }

        #region Сериализация
        ///<summary>
        /// Сериализация
        ///</summary>
        ///<returns>Результирующий Xml</returns>
        public string Save()
        {
            using (StringWriter sw=new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PersonXml));
                serializer.Serialize(sw, this);    
                return sw.ToString();
            }

        }

        ///<summary>
        /// Десериализация
        ///</summary>
        ///<param name="xml">Исходный Xml</param>
        ///<returns>Десериализованный объект</returns>
        static public PersonXml Load(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PersonXml));
                return serializer.Deserialize(sr) as PersonXml;
            }
        }
        #endregion


        ///<summary>
        /// Получение персональных данных для документа непосредственно из базы
        ///</summary>
        ///<param name="document">Документ</param>
        ///<returns>Персональные данные</returns>
        public static PersonXml GetValueFromDb(Document document)
        {
            List<DocumentXml> linkXmlCollection = DocumentXml.GetCollection(document);
            if(linkXmlCollection.Count==0)
                return null;
            return Load(linkXmlCollection[0].Xml);
        }
        public static PersonXml GetValue(Document document)
        {
            List<DocumentXml> linkXmlCollection = document.GetXmlData();
            if (linkXmlCollection.Count == 0)
                return null;
            return Load(linkXmlCollection[0].Xml);
        }
        ///<summary>
        /// Сохранение персональных данных для документа
        ///</summary>
        ///<param name="document">Документ</param>
        public void SetValue(Document document)
        {
            List<DocumentXml> linkXmlCollection = DocumentXml.GetCollection(document);
            DocumentXml xmlData = linkXmlCollection.Count == 0
                                                  ? new DocumentXml{Workarea= document.Workarea, Owner = document}
                                                  : linkXmlCollection[0];
            xmlData.Xml = Save();
            xmlData.Save();
        }
    }
}