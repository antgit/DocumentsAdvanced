using System;
using System.Collections.Generic;
using BusinessObjects;
using BusinessObjects.Documents.Person;
using BusinessObjects.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    [TestClass]
    public class TestChains
    {
        private Workarea WA;

        public TestChains()
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
        public void TestChain_GetChainSourceList()
        {
            System.Diagnostics.Debug.WriteLine("Test Start - TestChain_GetChainSourceList");
            Agent ag = WA.GetObject<Agent>(5);

            List<Agent> coll = Chain<Agent>.GetChainSourceList(ag, 1, State.STATEACTIVE, false);
            System.Diagnostics.Debug.WriteLine("Test - 1");

            coll = Chain<Agent>.GetChainSourceList(ag, 1, State.STATEACTIVE, false);
            System.Diagnostics.Debug.WriteLine("Test - 2");

            coll = Chain<Agent>.GetChainSourceList(ag, 1, State.STATEACTIVE, true);
            System.Diagnostics.Debug.WriteLine("Test - 3");

            System.Diagnostics.Debug.WriteLine("Test End - TestChain_GetChainSourceList");
        }
    }
}