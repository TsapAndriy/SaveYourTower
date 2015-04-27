using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;

namespace SaveYourTower.GameEngine.Test.DataContainers
{
    [TestClass]
    public class UnitVector2Test
    {
        [TestMethod]
        public void TestAngle()
        {
            UnitVector2 vector = new UnitVector2(0, 1);

            vector.Angle = 0;
            Assert.IsTrue(Math.Round(new UnitVector2(1, 0).Angle, 3) == Math.Round(vector.Angle, 3));

            vector.Angle = (90d / 180d * Math.PI);
            Assert.IsTrue(Math.Round(new UnitVector2(0, 1).Angle, 3) == Math.Round(vector.Angle, 3));

            vector.Angle = (180d / 180d * Math.PI);
            Assert.IsTrue(Math.Round(new UnitVector2(-1, 0).Angle, 3) == Math.Round(vector.Angle, 3));

            vector.Angle = (270d / 180d * Math.PI);
            Assert.IsTrue(Math.Round(new UnitVector2(0, -1).Angle, 3) == Math.Round(vector.Angle, 3));
        }

        [TestMethod]
        public void TestNormalize()
        {
            // Normalize used in constructor.
            UnitVector2 vector = new UnitVector2(5, 10);

            double magnitude = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
            magnitude = Math.Round(magnitude, 3);

            Assert.IsTrue(magnitude == 1);
            Assert.IsTrue(Math.Round((vector.X / vector.Y), 3) == Math.Round((5d / 10d), 3));         
        }

        [TestMethod]
        public void TestConstructorWithXY()
        {
            UnitVector2 vector = new UnitVector2(0, 1);
            Assert.IsTrue((vector.X == 0) && (vector.Y == 1));
        }

        [TestMethod]
        public void TestConctructorWithAngle()
        {
            UnitVector2 vector = new UnitVector2((90d / 180d * Math.PI));
            Assert.IsTrue((Math.Round(vector.X, 3) == 0) && (Math.Round(vector.Y, 3) == 1));
        }

        [TestMethod]
        public void TestClone()
        {
            UnitVector2 vector1 = new UnitVector2(1);
            UnitVector2 vector2 = (UnitVector2)vector1.Clone();

            Assert.AreNotSame(vector1, vector2);
            Assert.IsTrue(vector1.Equals(vector2));
        }
    }
}
