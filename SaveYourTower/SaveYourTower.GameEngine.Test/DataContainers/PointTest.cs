using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;


namespace SaveYourTower.GameEngine.Test.DataContainers
{
    [TestClass]
    public class PointTest
    {

        [TestMethod]
        public void TestConscructor()
        {
            Point point = new Point(10, 20);
            Assert.IsTrue((point.X == 10) && (point.Y == 20));
        }

        [TestMethod]
        public void TestSetPosition()
        {
            Point point = new Point(0, 0);
            point.SetPostition(3, 4);
            Assert.IsTrue(point.Equals(new Point(3, 4)));
        }

        [TestMethod]
        public void TestOperanorPlus()
        {
            Point point1 = new Point(1, 1);
            Point point2 = (Point)point1.Clone();

            Assert.IsTrue((point1 + point2).Equals(new Point(2, 2)));
        }

        [TestMethod]
        public void TestOperanorMinus()
        {
            Point point1 = new Point(1, 1);
            Point point2 = (Point)point1.Clone();

            Assert.IsTrue((point1 - point2).Equals(new Point(0, 0)));
        }


        [TestMethod]
        public void TestClone()
        {
            Point point1 = new Point(1, 1);
            Point point2 = (Point)point1.Clone();

            Assert.AreNotSame(point1, point2);
            Assert.IsTrue(point1.Equals(point2));
        }

        [TestMethod]
        public void TestEquals()
        {
            Point point1 = new Point(10, 13);
            Point point2 = new Point(10, 13);

            Assert.IsTrue(point1.Equals(point2));

            point1 = new Point(0, 0);
            point2 = new Point(10, 13);

            Assert.IsFalse(point1.Equals(point2));
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            Point point1 = new Point(10, 13);
            Point point2 = new Point(10, 13);

            Assert.IsTrue(point1.GetHashCode() == point2.GetHashCode());

            point1 = new Point(0, 0);
            point2 = new Point(10, 13);

            Assert.IsFalse(point1.GetHashCode() == point2.GetHashCode());

        }
    }
}


