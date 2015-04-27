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
    }

     //public double X { get; set; }
     //   public double Y { get; set; }

     //   public Point(double x, double y)
     //   {
     //       SetPostition(x, y);
     //   }

     //   public void SetPostition(double x, double y)
     //   {
     //       X = x;
     //       Y = y;
     //   }

     //   public static Point operator + (Point left, Point right)
     //   {
     //       return new Point((left.X + right.X), (left.X + right.Y));
     //   }

     //   public static Point operator - (Point left, Point right)
     //   {
     //       return new Point((left.X - right.X), (left.X - right.Y));
     //   }

     //   public Point Clone()
     //   {
     //       return new Point(X, Y);
     //   }

     //   public override bool Equals(Object obj)
     //   {
     //       Point point = obj as Point;
     //       if (point == null)
     //           return false;
     //       else
     //           return ((this.X == point.X) && (this.Y == point.Y));
     //   }
}


