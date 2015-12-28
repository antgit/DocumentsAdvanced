using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Microsoft.SqlServer.ReportingServices2010;

namespace BusinessObjects.ReportingService
{
    public class Reports
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Reports()
        {
            
        }

        /// <summary>
        /// Заполнение отчетов
        /// </summary>
        /// <param name="servicePath">Путь к службе отчетов</param>
        /// <param name="hierarchy">Иерархия для создания</param>
        /// <param name="rootFolder">Корневая папка, обычно "Документы2011" или "/" для корневой папки</param>
        public void FillFromSourceServer(string servicePath, Hierarchy hierarchy, string rootFolder = "/Документы2011")
        {
            ReportingService2010 rs = new ReportingService2010();
            rs.Url = servicePath; //"http://<Server Name>/reportserver/reportexecution2005.asmx?wsdl"
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //http://localhost/ReportServer/ReportService2010.asmx?wsdl

            // проверка подлинности
            //ReportingService service = new ReportingService();service.Credentials = new System.Net.NetworkCredential("username", "password", "domain");
            //ReportingService rs = new ReportingService();rs.Credentials = System.Net.CredentialCache.DefaultCredentials;


            // чтение каталога
            //searchFolder = HttpUtility.UrlEncode(searchFolder); 
            List<Library> collLibrary = hierarchy.GetTypeContents<Library>();
            
            List<Library> fullCollectionLib = hierarchy.Workarea.GetCollection<Library>();
            CatalogItem[] items = rs.ListChildren(rootFolder, true);
            Library repTml= hierarchy.Workarea.GetTemplates<Library>().FirstOrDefault(s => s.KindId == Library.KINDID_REPSQL);
            foreach (CatalogItem item in items.Where(s=>s.TypeName=="Report"))
            {
                if(!item.Hidden)
                {
                    Console.WriteLine(item.ID);
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.Path);
                    Library findItem = collLibrary.FirstOrDefault(s => s.TypeUrl == item.Path);
                    // поиск по всем библиотекам
                    if(findItem==null)
                    {
                        findItem = fullCollectionLib.FirstOrDefault(s => s.TypeUrl == item.Path);
                    }
                    if(findItem!=null)
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                            findItem.Memo = item.Description;

                        
                        if(findItem.IsChanged)
                            findItem.Save();

                        HierarchyContent cnt = hierarchy.Contents.FirstOrDefault(s => s.ElementId == findItem.Id);
                        if(cnt==null)
                            hierarchy.ContentAdd<Library>(findItem);
                        else if(cnt.StateId!= State.STATEACTIVE)
                        {
                            cnt.StateId = State.STATEACTIVE;
                            cnt.Save();
                        }
                    }
                    else // создание новой библиотеки-отчета
                    {
                        findItem = hierarchy.Workarea.CreateNewObject<Library>(repTml);
                        findItem.Name = item.Name;
                        findItem.Memo = item.Description;
                        findItem.TypeUrl = item.Path;
                        findItem.Save();
                        hierarchy.ContentAdd<Library>(findItem);
                    }
                }
            }
        }

    }

    public class RSSerializedData
    {
        private List<RSObject> _Content = new List<RSObject>();

        [XmlElement]
        public DateTime ExportDate;
        [XmlElement]
        public string Host;
        [XmlArray]
        [XmlArrayItem(typeof(RSObject))]
        public List<RSObject> Content
        {
            get { return _Content; }
        }

        public RSSerializedData()
        { }
    }

    public class RSObject
    {
        [XmlAttribute]
        public string Path;
        [XmlAttribute]
        public string TypeName;
        [XmlAttribute]
        public string Description;
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public DateTime DateModified;
        [XmlAttribute]
        public string Parent;

        [XmlAttribute]
        public string Id;

        [XmlAttribute]
        public bool Hidden;

        [XmlAttribute]
        public string CreatedBy;
        
        public RSObject()
        { }
    }
}
