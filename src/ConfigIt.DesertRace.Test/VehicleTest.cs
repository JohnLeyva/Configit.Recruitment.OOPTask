using ConfigIt.DesertRace.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ConfigIt.DesertRace.Test
{
    
    
    /// <summary>
    ///This is a test class for VehicleTest and is intended
    ///to contain all VehicleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VehicleTest
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

        private ArenaRectangular<GameEntity> GenArena()
        {
            var container = new ArenaRectangular<GameEntity>();
            container.MaxLocation = new Location(10, 20);
            return container;
        }

        private Vehicle GenVehicle(string config)
        {
            var container = GenArena();
            Vehicle target = new Vehicle(container, Util.SeparedParams(config));
            return target;
        }


        /// <summary>
        ///A test for Vehicle Constructor
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Creation")]
        public void VehicleConstructorTest1()
        {
            var target = GenVehicle("V1 5 4 N");
            Assert.AreEqual(target.Position.X,5);
            Assert.AreEqual(target.Position.Y,4);
            Assert.AreEqual(Direction.North, target.Direction);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Direction")]
        public void ExecCommand_Left_N()
        {
            var target = GenVehicle("V1 5 4 N");
            target.ExecCommand(VehicleCommands.Left);
            Assert.AreEqual(Direction.West, target.Direction);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Direction")]
        public void ExecCommand_Left_E()
        {
            var target = GenVehicle("V1 5 4 E");
            target.ExecCommand(VehicleCommands.Left);
            Assert.AreEqual(Direction.North, target.Direction);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Direction")]
        public void ExecCommand_Right_N()
        {
            var target = GenVehicle("V1 5 4 N");
            target.ExecCommand(VehicleCommands.Right);
            Assert.AreEqual(Direction.East, target.Direction);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Direction")]
        public void ExecCommand_Right_E()
        {
            var target = GenVehicle("V1 5 4 E");
            target.ExecCommand(VehicleCommands.Right);
            Assert.AreEqual(Direction.South, target.Direction);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement")]
        public void ExecCommand_Move_N()
        {
            var target = GenVehicle("V1 5 4 N");
            target.ExecCommand(VehicleCommands.Move);
            Assert.AreEqual(5,target.Position.Y);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement")]
        public void ExecCommand_Move_S()
        {
            var target = GenVehicle("V1 5 4 S");
            target.ExecCommand(VehicleCommands.Move);
            Assert.AreEqual(3,target.Position.Y);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement")]
        public void ExecCommand_Move_W()
        {
            var target = GenVehicle("V1 5 4 W");
            target.ExecCommand(VehicleCommands.Move);
            Assert.AreEqual(4,target.Position.X);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement")]
        public void ExecCommand_Move_E()
        {
            var target = GenVehicle("V1 5 4 E");
            target.ExecCommand(VehicleCommands.Move);
            Assert.AreEqual(6,target.Position.X);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement")]
        public void ExecCommand_Move_T1()
        {
            var target = GenVehicle("V1 4 4 E");
            target.ExecCommand(VehicleCommands.Right);
            Assert.AreEqual(Direction.South, target.Direction);
        }

        
        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement"),ExpectedException(typeof(GameException))]
        public void ExecCommand_Move_E_Onvalid()
        {
            var target = GenVehicle("V1 10 2 E");
            target.ExecCommand(VehicleCommands.Move);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement"), ExpectedException(typeof(GameException))]
        public void ExecCommand_Move_N_Onvalid()
        {
            var target = GenVehicle("V1 5 20 N");
            target.ExecCommand(VehicleCommands.Move);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement"), ExpectedException(typeof(GameException))]
        public void ExecCommand_Move_S_Onvalid()
        {
            var target = GenVehicle("V1 5 0 S");
            target.ExecCommand(VehicleCommands.Move);
        }

        /// <summary>
        ///A test for ExecCommand
        ///</summary>
        [TestMethod(), TestCategory("Vehicle Movement"), ExpectedException(typeof(GameException))]
        public void ExecCommand_Move_W_Onvalid()
        {
            var target = GenVehicle("V1 0 2 W");
            target.ExecCommand(VehicleCommands.Move);
        }
    
    }
}
