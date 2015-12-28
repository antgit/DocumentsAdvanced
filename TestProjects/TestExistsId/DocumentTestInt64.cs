using System;
using BusinessObjects;
using BusinessObjects.Documents.Person;
using BusinessObjects.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    [TestClass]
    public class DocumentTestInt64
    {
        private Workarea WA;

        public DocumentTestInt64()
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
        public void TestDocumentTestInt64()
        {
            //System.Diagnostics.Debug.WriteLine("Test Start");
            //int a = 1;
            //int b = 2;
            //Int64 res = Chain<Analitic>.ChainCasheData.CreateId(a, b);
            //System.Diagnostics.Debug.WriteLine("Test 1");
            //Int32 res2 = Chain<Analitic>.ChainCasheData.ExtractValueKind(res);
            //System.Diagnostics.Debug.WriteLine("Test 2");
            //Int32 res3 = Chain<Analitic>.ChainCasheData.ExtractValueId(res);
            //System.Diagnostics.Debug.WriteLine("Test 3");

            //System.Diagnostics.Debug.WriteLine("Test End");
        }

        [TestMethod]
        public void TestDecimalConvert()
        {
            decimal a = 10.2568m;
            System.Diagnostics.Debug.WriteLine(a.ToString("0.0000"));
            System.Diagnostics.Debug.WriteLine(a.ToString("0.00"));
        }

    }
}