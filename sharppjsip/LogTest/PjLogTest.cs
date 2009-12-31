using PJLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LogTest
{
    
    
    /// <summary>
    ///This is a test class for PjLogTest and is intended
    ///to contain all PjLogTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PjLogTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for LogLevel
        ///</summary>
        [TestMethod()]
        public void LogLevelTest()
        {
            PjLog_Accessor target = new PjLog_Accessor(); // TODO: Initialize to an appropriate value
            PjLogLevel expected = new PjLogLevel(); // TODO: Initialize to an appropriate value
            PjLogLevel actual;
            target.LogLevel = expected;
            actual = target.LogLevel;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LogDecoration
        ///</summary>
        [TestMethod()]
        public void LogDecorationTest()
        {
            PjLog_Accessor target = new PjLog_Accessor(); // TODO: Initialize to an appropriate value
            PjLogDecoration expected = new PjLogDecoration(); // TODO: Initialize to an appropriate value
            PjLogDecoration actual;
            target.LogDecoration = expected;
            actual = target.LogDecoration;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for write
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PJLib.dll")]
        public void writeTest()
        {
            PjLog_Accessor target = new PjLog_Accessor(); // TODO: Initialize to an appropriate value
            string logged = string.Empty; // TODO: Initialize to an appropriate value
            PjLogLevel level = new PjLogLevel(); // TODO: Initialize to an appropriate value
            target.write(logged, level);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for log
        ///</summary>
        [TestMethod()]
        public void logTest1()
        {
            PjLog_Accessor target = new PjLog_Accessor(); // TODO: Initialize to an appropriate value
            object caller = null; // TODO: Initialize to an appropriate value
            PjLogLevel level = new PjLogLevel(); // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.log(caller, level, message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for log
        ///</summary>
        [TestMethod()]
        public void logTest()
        {
            PjLog_Accessor target = new PjLog_Accessor(); // TODO: Initialize to an appropriate value
            object caller = null; // TODO: Initialize to an appropriate value
            string message = string.Empty; // TODO: Initialize to an appropriate value
            target.log(caller, message);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for PjLog Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PJLib.dll")]
        public void PjLogConstructorTest1()
        {
            string file = string.Empty; // TODO: Initialize to an appropriate value
            PjLog_Accessor target = new PjLog_Accessor(file);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for PjLog Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("PJLib.dll")]
        public void PjLogConstructorTest()
        {
            PjLog_Accessor target = new PjLog_Accessor();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
