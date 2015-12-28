using System;
using System.Activities;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using BusinessObjects.Security;
using BusinessObjects.Workflows;
using BusinessObjects.Workflows.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    [TestClass]
    public class UnitTestRegisterNewCompany
    {
        private Workarea WA;

        public UnitTestRegisterNewCompany()
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
        public void TestRegisterNewCompany()
        {
            ActivityRegisterNewCompany activity = new ActivityRegisterNewCompany();
            #region MyRegion
            // Create a dynamic object     dynamic input = new WorkflowArguments();       
            // The property names have to match the workflow argument names     
            //input.Person = new Person { Name = "Ron", Age = 46 };       
            // pass it to the activity no need to cast it     
            // You can do the same on the output     
            //var output =  WorkflowArguments.FromDictionary(WorkflowInvoker.Invoke(new SayHello(), input


            //activity.CompanyName = "TestCompany";
            //activity.CurrentWorkarea = WA;
            //activity.Email = "test@mail.ru";
            //activity.UserLogin = "testCompanyUser";
            //activity.UserName = "Сотрудник новой компании"; 
            #endregion

            Dictionary<string, object> prms = new Dictionary<string, object>
                                                  {
                                                      {"CompanyName", "TestCompany"},
                                                      {"CurrentWorkarea", WA},
                                                      {"Email", "levkin77@mail.ru"},
                                                      {"UserLogin", "testCompanyUser"},
                                                      {"UserName", "Сотрудник новой компании"}
                                                  };

            
            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(activity, prms);



            
        }

        //[TestMethod]
        //public void TestRegisterNewCompanyV2()
        //{
        //    ActivityRegisterNewCompany activity = new ActivityRegisterNewCompany();
            
        //    Dictionary<string, object> prms = new Dictionary<string, object>
        //                                          {
        //                                              {"CompanyName", "TestCompany"},
        //                                              {"CurrentWorkarea", WA},
        //                                              {"Email", "test@mail.ru"},
        //                                              {"UserLogin", "testCompanyUser"},
        //                                              {"UserName", "Сотрудник новой компании"}
        //                                          };

        //    //WorkflowManager.GetWorkflowManagerWa(WA).ExecuteWorkflow(activity, prms);
        //    WorkflowManager mgr = new WorkflowManager();
        //    mgr.ExecuteWorkflow(activity, prms);

        //}

        
        
    }
}
