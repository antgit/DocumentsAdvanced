using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BusinessObjects.Documents.Person
{
    /// <summary>
    /// �������������� ����� ��� ���������� �� ���������
    /// </summary>
    public class PersonXml
    {
        public PersonXml()
        {
            
        }
        /// <summary>
        /// ���� �������� �� ������
        /// </summary>
        /// <remarks>������������ � ��������� "������ � ������ �� ������", "������ � �����������"</remarks>
        public DateTime? DateStart { get; set; }
        /// <summary>
        /// ���� ����������
        /// </summary>
        /// <remarks>������������ � ��������� "������ � ����������", "������ � �����������"</remarks>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// ������������� ������������� "������"
        /// </summary>
        /// <remarks>������������ � ��������� "������ � ����������", "������ � �����������"</remarks>
        public int DepatmentFromId { get; set; }

        /// <summary>
        /// ������������� ������������� "����"
        /// </summary>
        /// <remarks>������������ � ��������� "������ � ������ �� ������", "������ � �����������"</remarks>
        public int DepatmentToId { get; set; }

        [OptionalField]
        private int _workPostId;

        /// <summary>
        /// ������������� ���������
        /// </summary>
        
        public int WorkPostId
        {
            get { return _workPostId; }
            set { _workPostId = value; }
        }

        #region ������������
        ///<summary>
        /// ������������
        ///</summary>
        ///<returns>�������������� Xml</returns>
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
        /// ��������������
        ///</summary>
        ///<param name="xml">�������� Xml</param>
        ///<returns>����������������� ������</returns>
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
        /// ��������� ������������ ������ ��� ��������� ��������������� �� ����
        ///</summary>
        ///<param name="document">��������</param>
        ///<returns>������������ ������</returns>
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
        /// ���������� ������������ ������ ��� ���������
        ///</summary>
        ///<param name="document">��������</param>
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