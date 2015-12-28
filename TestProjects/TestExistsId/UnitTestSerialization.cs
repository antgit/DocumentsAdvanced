using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestExistsId
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTestSerialization
    {
        private Workarea WA;
        public UnitTestSerialization()
        {
            WA = new Workarea
                     {
                         ConnectionString =
                             "Data Source=NEWSERVER;Initial Catalog=Documents2010;Integrated Security=True;Application Name=Documents System 2010;Current Language=Russian"
                     };
            WA.LogOn(Environment.UserName);
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
        public void SerialiszationEntityKind()
        {
            EntityKind classEntityKind = WA.CollectionEntityKinds[0];

            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(EntityKind));
            serializer.Serialize(stream, classEntityKind);

            XmlSerializer serializer2 = new XmlSerializer(typeof(EntityKind));

            StringBuilder sb = new StringBuilder();

            StringWriter writer = new StringWriter(sb);
            serializer2.Serialize(writer, classEntityKind);

            Debug.WriteLine(sb.ToString());

            XmlSerializer serializerColl = new XmlSerializer(typeof(List<EntityKind>));

            StringBuilder sb3 = new StringBuilder();

            StringWriter writer3 = new StringWriter(sb3);
            serializerColl.Serialize(writer3, WA.CollectionEntityKinds);

            Debug.WriteLine(sb3.ToString());
        }
        
    }
}