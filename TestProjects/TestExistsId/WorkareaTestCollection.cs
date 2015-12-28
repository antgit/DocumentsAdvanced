using System;
using System.Collections.Generic;
using System.Diagnostics;
using BusinessObjects;
using BusinessObjects.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    [TestClass]
    public class WorkareaTestCollection
    {
        private Workarea WA;

        public WorkareaTestCollection()
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
        public void TestGetCoolectionTimePeriod()
        {
            System.Diagnostics.Debug.WriteLine("Test Start TestGetCoolectionTimePeriod");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            List<TimePeriod> coll = WA.GetCollection<TimePeriod>();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            System.Diagnostics.Debug.WriteLine("RunTime " + elapsedTime);

            System.Diagnostics.Debug.WriteLine("Test step2");
            stopWatch.Start();
            coll = WA.GetCollection<TimePeriod>();
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts2.Hours, ts2.Minutes, ts2.Seconds,
                ts2.Milliseconds / 10);
            System.Diagnostics.Debug.WriteLine("RunTime " + elapsedTime2);

            System.Diagnostics.Debug.WriteLine("Test step3");
            stopWatch.Start();
            coll = WA.GetCollection<TimePeriod>(true);
            stopWatch.Stop();
            TimeSpan ts3 = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime3 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts3.Hours, ts3.Minutes, ts3.Seconds,
                ts3.Milliseconds / 10);
            System.Diagnostics.Debug.WriteLine("RunTime " + elapsedTime3);
            System.Diagnostics.Debug.WriteLine("Test End TestGetCoolectionTimePeriod");
        }

    }
}