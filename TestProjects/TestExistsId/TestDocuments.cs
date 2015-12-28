using System;
using BusinessObjects;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDocuments
{
    [TestClass]
    public class TestDocuments
    {
        private Workarea WA;

        public TestDocuments()
        {
            Uid user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            WA = new Workarea
                     {
                         //ConnectionString =
                         //    "Data Source=.;Initial Catalog=Documents2012;Integrated Security=True;Application Name=Documents System 2012;Current Language=Russian"

                         ConnectionString =
                             "Data Source=srv-devdoc;Initial Catalog=Documents2011DMPZ;Integrated Security=False;User ID=LocalAdmin;Password=12345;Application Name=Documents System 2012 DEV;Current Language=Russian"
                     };
            WA.LogOn(user.Name);
        }
        [TestMethod]
        public void Test_ConfigSales()
        {
            System.Diagnostics.Debug.WriteLine("Test Start - Test_ConfigSales");

            ConfigSales config = ConfigSales.GetConfig(WA, 148245);
            System.Diagnostics.Debug.WriteLine("Test End - Test_ConfigSales - TestCasheData");
            
            config = ConfigSales.GetConfig(WA, 148245);

            System.Diagnostics.Debug.WriteLine("Test End - Test_ConfigSales");
        }
    }
}