using ConfigIt.DesertRace.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ConfigIt.DesertRace.Test
{
    
    
    /// <summary>
    ///This is a test class for WarGameTest and is intended
    ///to contain all WarGameTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameTest
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

        private static void PlayGameWithAssert(StringBuilder input, StringBuilder expected)
        {
            using (var results = new StringWriter())
            {
                using (var reader = new StringReader(input.ToString()))
                {
                    DesertRaceGame.Play(reader, results);
                }
                string result = results.ToString();
                Assert.AreEqual(expected.ToString(), result);
            }
        }

        /// <summary>
        ///A test for PlayGame
        ///</summary>
        [TestMethod()]
        public void PlayGameTest_Case_1()
        {
            var input = new StringBuilder();
            input.AppendLine("10 20");
            input.AppendLine("5 10");
            input.AppendLine("V1 2 4 E");
            input.AppendLine("MMR");

            var expected = new StringBuilder();
            expected.AppendLine("V1 4 4 S");

            PlayGameWithAssert(input, expected);
        }

        /// <summary>
        ///A test for PlayGame
        ///</summary>
        [TestMethod()]
        public void PlayGameTest_Case_2()
        {
            var input = new StringBuilder();
            input.AppendLine("10 20");
            input.AppendLine("5 10");
            input.AppendLine("V2 2 4 E");
            input.AppendLine("MMRMMRMRRM");
            input.AppendLine("V1 3 4 N");
            input.AppendLine("LMLMLMLMM");

            var expected = new StringBuilder();
            expected.AppendLine("V1 3 5 N");
            expected.AppendLine("V2 4 2 E");
            PlayGameWithAssert(input, expected);
        }
    }
}
