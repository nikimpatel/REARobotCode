using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot_Simulator;

namespace RobotSimulator.Tests
{
    [TestClass]
    public class RobotDriverTests
    {
        [TestMethod]
        public void RobotDriver_InitialisedRobotDriver_ControlsRobot()
        {
            var driver = new RobotDriver(new Robot());
            Assert.IsNotNull(driver.Robot);
        }

        [TestMethod]
        public void RobotDriver_EmptyCommand_ReportsInvalid()
        {
            var driver = new RobotDriver(new Robot());
            var response = driver.executecommand("");
            Assert.AreEqual("Invalid command.", response);
        }

        [TestMethod]
        public void RobotDriver_UnrecognisedCommand_ReportsInvalid()
        {
            var driver = new RobotDriver(new Robot());
            var response = driver.executecommand("XXXX");
            Assert.AreEqual("Invalid command.", response);
        }

        [TestMethod]
        public void RobotDriver_RecognisedCommand_ReportsValid()
        {
            var driver = new RobotDriver(new Robot());
            var response = driver.executecommand("MOVE");
            Assert.AreEqual("Robot cannot move until it has been placed on the table.", response);
        }

        [TestMethod]
        public void RobotDriver_PlaceCommandWithNoArguments_ReportsInvalid()
        {
            var driver = new RobotDriver(new Robot());
            var response = driver.executecommand("PLACE");
            Assert.AreEqual("Invalid command.", response);
        }

        [TestMethod]
        public void RobotDriver_PlaceCommandWithInvalidArguments_ReportsInvalid()
        {
            var driver = new RobotDriver(new Robot());
            var response = driver.executecommand("PLACE XXX");
            Assert.AreEqual("Invalid command.", response);
            response = driver.executecommand("PLACE 1,X,NORTH");
            Assert.AreEqual("Invalid command.", response);
            response = driver.executecommand("PLACE X,1,NORTH");
            Assert.AreEqual("Invalid command.", response);
            response = driver.executecommand("PLACE 1,1,XXX");
            Assert.AreEqual("Invalid command.", response);
        }

        [TestMethod]
        public void RobotDriver_PlacedAndTurnedLeft_ReportsCorrectPosition()
        {
            var driver = new RobotDriver(new Robot());
            driver.executecommand("PLACE 1,1,NORTH");
            driver.executecommand("LEFT");
            Assert.AreEqual("1,1,WEST", driver.executecommand("REPORT"));
        }

        [TestMethod]
        public void RobotDriver_PlacedAndTurnedRight_ReportsCorrectPosition()
        {
            var driver = new RobotDriver(new Robot());
            driver.executecommand("PLACE 1,1,NORTH");
            driver.executecommand("RIGHT");
            Assert.AreEqual("1,1,EAST", driver.executecommand("REPORT"));
        }

        [TestMethod]
        public void RobotDriver_PlacedAndMovedOffTable_CannotBeMoved()
        {
            var driver = new RobotDriver(new Robot());
            driver.executecommand("PLACE 5,5,NORTH");
            driver.executecommand("MOVE");
            Assert.AreEqual("5,5,NORTH", driver.executecommand("REPORT"));
        }

        [TestMethod]
        public void RobotDriver_PlacedAndMoved_ReportsCorrectPosition()
        {
            var driver = new RobotDriver(new Robot());
            driver.executecommand("PLACE 1,1,NORTH");
            driver.executecommand("MOVE");
            Assert.AreEqual("1,2,NORTH", driver.executecommand("REPORT"));
        }

        public void RobotDriver_PlacedMovedAndTurned_ReportsCorrectPosition()
        {
            var driver = new RobotDriver(new Robot());
            driver.executecommand("PLACE 1,2,EAST");
            driver.executecommand("MOVE");
            driver.executecommand("MOVE");
            driver.executecommand("LEFT");
            driver.executecommand("MOVE");
            Assert.AreEqual("3,3,NORTH", driver.executecommand("REPORT"));
        }
    }
}
