using System;
using System.Collections.Generic;
using BusinessObjects;
using BusinessObjects.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{


    [TestClass]
    public class TestAgents
    {
        private Workarea WA;

        public TestAgents()
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
        public void Test_GetAgentHolding()
        {
            System.Diagnostics.Debug.WriteLine("Test Start - Test_GetAgentHolding");
            Agent ag = WA.GetObject<Agent>(15554);

            Agent agHolding = ag.GetAgentHolding();
            

            System.Diagnostics.Debug.WriteLine("Test End - Test_GetAgentHolding");
        }
    }
}