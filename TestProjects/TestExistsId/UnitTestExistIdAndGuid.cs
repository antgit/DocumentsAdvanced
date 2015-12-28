using System;

using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects;
using BusinessObjects.Developer;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using BusinessObjects.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTestExistIdAndGuid
    {
        private Workarea WA;
        public UnitTestExistIdAndGuid()
        {
            Uid user = new Uid { Name = Environment.UserName, Password = string.Empty, AuthenticateKind = 1 };
            WA = new Workarea
                     {
                         ConnectionString =
                             "Data Source=.;Initial Catalog=Documents2012;Integrated Security=True;Application Name=Documents System 2012;Current Language=Russian"
                     };
            WA.LogOn(user.Name);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ResaveTaskData()
        {
            List<Task> taskColl = WA.GetCollection<Task>();
            foreach (Task task in taskColl)
            {
                 InternalShowPropertyBase<Task> showPropertyBase = new InternalShowPropertyBase<Task>();
                 showPropertyBase.SelectedItem = task;
                BuildControlTask ctlData = new BuildControlTask { SelectedItem = task };
                showPropertyBase.ControlBuilder = ctlData;
                Form frm = showPropertyBase.ShowDialog();

                ctlData.Save();

                frm.Close();
            }
            
        }

        [TestMethod]
        public void EmptyByWhellKnownDbEntity()
        {
            foreach (var val in Enum.GetValues(typeof(WhellKnownDbEntity)))
            {
                Debug.WriteLine(val.ToString());
                IBase obj = WA.Empty((WhellKnownDbEntity) val);
            }
        }
        [TestMethod]
        public void FindIdByGuidAccount()
        {
            Account obj = WA.Empty<Account>();
            obj.ExistsGuids(Guid.Empty);

        }
        [TestMethod]
        public void FindIdByGuidAgent()
        {
            Agent obj = (Agent)WA.Empty(WhellKnownDbEntity.Agent);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidAnalitic()
        {
            Analitic obj = (Analitic)WA.Empty(WhellKnownDbEntity.Analitic);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidBranche()
        {
            Branche obj = (Branche)WA.Empty(WhellKnownDbEntity.Branche);
            obj.ExistsGuids(Guid.Empty);
        }

        [TestMethod]
        public void FindIdByGuidCustomViewColumn()
        {
            CustomViewColumn obj = (CustomViewColumn)WA.Empty(WhellKnownDbEntity.Column);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidAgentContact()
        {
            Contact obj = (Contact)WA.Empty(WhellKnownDbEntity.Contact);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidCountry()
        {
            Country obj = (Country)WA.Empty(WhellKnownDbEntity.Country);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidCurrency()
        {
            Currency obj = (Currency)WA.Empty(WhellKnownDbEntity.Currency);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidCustomViewList()
        {
            CustomViewList obj = (CustomViewList)WA.Empty(WhellKnownDbEntity.CustomViewList);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidTableInfo()
        {
            DbObject obj = (DbObject)WA.Empty(WhellKnownDbEntity.DbObject);
            obj.ExistsGuids(Guid.Empty);
        }

        [TestMethod]
        public void FindIdByGuidDocument()
        {
            Document obj = (Document)WA.Empty(WhellKnownDbEntity.Document);
            obj.ExistsGuids(Guid.Empty);
        }

        [TestMethod]
        public void FindIdByGuidFactColumn()
        {
            FactColumn obj = (FactColumn)WA.Empty(WhellKnownDbEntity.FactColumn);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidFactName()
        {
            FactName obj = (FactName)WA.Empty(WhellKnownDbEntity.FactName);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidFileData()
        {
            FileData obj = (FileData)WA.Empty(WhellKnownDbEntity.FileData);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidFolder()
        {
            Folder obj = (Folder)WA.Empty(WhellKnownDbEntity.Folder);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidHierarchy()
        {
            Hierarchy obj = (Hierarchy)WA.Empty(WhellKnownDbEntity.Hierarchy);
            obj.ExistsGuids(Guid.Empty);
        }
        [TestMethod]
        public void FindIdByGuidLibrary()
        {
            Library obj = (Library)WA.Empty(WhellKnownDbEntity.Library);
            obj.ExistsGuids(Guid.Empty);
        }
    }


}
