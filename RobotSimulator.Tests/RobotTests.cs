using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot_Simulator;

namespace RobotSimulator.Tests
{
    [TestClass]
    public class RobotTests
    {
        [TestMethod]
        public void Robot_InitialisedButNotPlaced_CannotBeMoved()
        {
            var robot = new Robot();
            var result = robot.move();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot move until it has been placed on the table.", robot.LastError);
        }

        [TestMethod]
        public void Robot_InitialisedButNotPlaced_CannotBeTurned()
        {
            var robot = new Robot();
            var result = robot.left();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot turn until it has been placed on the table.", robot.LastError);
        }

        [TestMethod]
        public void Robot_InitialisedButNotPlaced_CannotReportItsPosition()
        {
            var robot = new Robot();
            var result = robot.Report();
            Assert.AreEqual("", result);
            Assert.AreEqual("Robot cannot report it's position until it has been placed on the table.", robot.LastError);
        }

        [TestMethod]
        public void Robot_PlacedOffTable_CannotBePlaced()
        {
            var robot = new Robot();
            var result = robot.place(-1, 0, Facing.North);
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot be placed there.", robot.LastError);

            result = robot.place(0, 6, Facing.North);
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot be placed there.", robot.LastError);
        }

        [TestMethod]
        public void Robot_Placed_CanReportItsPosition()
        {
            var robot = new Robot();
            var result = robot.place(3, 2, Facing.East);
            var position = robot.Report();
            Assert.IsTrue(result);
            Assert.AreEqual("", robot.LastError);
            Assert.AreEqual("3,2,EAST", position);
        }

        [TestMethod]
        public void Robot_PlacedAndTurnedLeft_ReportsCorrectPosition()
        {
            var robot = new Robot();
            robot.place(1, 1, Facing.North);
            robot.left();
            Assert.AreEqual("1,1,WEST", robot.Report());
        }

        [TestMethod]
        public void Robot_PlacedAndTurnedRight_ReportsCorrectPosition()
        {
            var robot = new Robot();
            robot.place(1, 1, Facing.North);
            robot.right();
            Assert.AreEqual("1,1,EAST", robot.Report());
        }

        [TestMethod]
        public void Robot_PlacedAndMovedOffTable_CannotBeMoved()
        {
            var robot = new Robot();
            robot.place(5, 5, Facing.North);
            var result = robot.move();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot cannot be moved there.", robot.LastError);
            Assert.AreEqual("5,5,NORTH", robot.Report());
        }

        [TestMethod]
        public void Robot_PlacedAndMoved_ReportsCorrectPosition()
        {
            var robot = new Robot();
            robot.place(1, 1, Facing.North);
            robot.move();
            Assert.AreEqual("1,2,NORTH", robot.Report());
        }

        [TestMethod]
        public void Robot_PlacedMovedAndTurned_ReportsCorrectPosition()
        {
            var robot = new Robot();
            robot.place(1, 2, Facing.East);
            robot.move();
            robot.move();
            robot.left();
            robot.move();
            Assert.AreEqual("3,3,NORTH", robot.Report());
        }
    }
}
