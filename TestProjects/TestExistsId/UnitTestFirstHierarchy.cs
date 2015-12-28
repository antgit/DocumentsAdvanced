using System;
using System.Collections.Generic;
using BusinessObjects;
using BusinessObjects.Documents.Person;
using BusinessObjects.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    
    [TestClass]
    public class DocumentTestStringData
    {
        private Workarea WA;

        public DocumentTestStringData()
        {
            Uid user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            WA = new Workarea
            {
                ConnectionString =
                    "Data Source=.;Initial Catalog=Documents2012;Integrated Security=True;Application Name=Documents System 2012;Current Language=Russian"

                //ConnectionString =
                //    "Data Source=srv-devdoc;Initial Catalog=Documents2011DMPZ;Integrated Security=False;User ID=LocalAdmin;Password=12345;Application Name=Documents System 2012 DEV;Current Language=Russian"
                //<add name="Document2011" connectionString="Server=srv-devdoc;Database=Documents2011DMPZ;User ID=LocalAdmin;Password=12345; Application Name=&quot;Documents System 2012 DEV&quot;" providerName="System.Data.SqlClient" />
            };
            WA.LogOn(user.Name);
        }
        [TestMethod]
        public void TestDocumentStringData()
        {
            int id = 1208024;
            BusinessObjects.Documents.DocumentSales obj = WA.GetObject<BusinessObjects.Documents.DocumentSales>(id);

            System.Diagnostics.Debug.WriteLine(obj.Document.Name);

            var strData = obj.Document.GetStringData();
            strData.Value1 = "ssss1234g56789";
            obj.Save();
            //strData.Save();
            
            System.Diagnostics.Debug.WriteLine("End State2 Analitic");
        }

        [TestMethod]
        public void TestDocumentXmlData()
        {
            int id = 1207030;
            BusinessObjects.Documents.DocumentSales obj = WA.GetObject<BusinessObjects.Documents.DocumentSales>(id);

            System.Diagnostics.Debug.WriteLine(obj.Document.Name);

            var tst = PersonXml.GetValue(obj.Document);
            
            //strData.Save();
            
            System.Diagnostics.Debug.WriteLine("End State2 Analitic");
        }
        

    }

    [TestClass]
    public class UnitTestFirstHierarchy
    {
        private Workarea WA;

        public UnitTestFirstHierarchy()
        {
            Uid user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            WA = new Workarea
                     {
                         //ConnectionString =
                         //    "Data Source=.;Initial Catalog=Documents2012;Integrated Security=True;Application Name=Documents System 2012;Current Language=Russian"

                         ConnectionString =
                             "Data Source=srv-devdoc;Initial Catalog=Documents2011DMPZ;Integrated Security=False;User ID=LocalAdmin;Password=12345;Application Name=Documents System 2012 DEV;Current Language=Russian"
                         //<add name="Document2011" connectionString="Server=srv-devdoc;Database=Documents2011DMPZ;User ID=LocalAdmin;Password=12345; Application Name=&quot;Documents System 2012 DEV&quot;" providerName="System.Data.SqlClient" />
                     };
            WA.LogOn(user.Name);
        }
        [TestMethod]
        public void AnaliticFirstHierarchy()
        {
            int id = 992;
            Analitic obj = WA.GetObject<Analitic>(id);

            System.Diagnostics.Debug.WriteLine(obj.Name);
            obj.FirstHierarchy();
            System.Diagnostics.Debug.WriteLine("End Analitic");
            obj.FirstHierarchy();
            System.Diagnostics.Debug.WriteLine("End State2 Analitic");
        }

    }

    [TestClass]
    public class UnitTestTaskProperties
    {
        private Workarea WA;

        public UnitTestTaskProperties()
        {
            Uid user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            WA = new Workarea
            {
                //ConnectionString =
                //    "Data Source=.;Initial Catalog=Documents2012;Integrated Security=True;Application Name=Documents System 2012;Current Language=Russian"

                ConnectionString =
                    "Data Source=srv-devdoc;Initial Catalog=Documents2011DMPZ;Integrated Security=False;User ID=LocalAdmin;Password=12345;Application Name=Documents System 2012 DEV;Current Language=Russian"
                //<add name="Document2011" connectionString="Server=srv-devdoc;Database=Documents2011DMPZ;User ID=LocalAdmin;Password=12345; Application Name=&quot;Documents System 2012 DEV&quot;" providerName="System.Data.SqlClient" />
            };
            WA.LogOn(user.Name);
        }
        [TestMethod]
        public void TestTaskProperties()
        {
            List<Task> coll = WA.GetCollection<Task>();

            foreach (Task task in coll)
            {
                System.Diagnostics.Debug.WriteLine(task.AgentOwnerName);    
            }
            
            
            
            
            
        }

    }
}