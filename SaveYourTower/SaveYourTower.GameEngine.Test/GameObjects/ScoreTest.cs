using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;

namespace SaveYourTower.GameEngine.Test.GameObjects
{
    [TestClass]
    public class ScoreTest
    {
        [TestMethod]
        public void TestAddPoint()
        {
            Score testScore = new Score();
            testScore.AddPoint(10);

            Assert.AreEqual(10, testScore.Value);
        }

        [TestMethod]
        public void TestSpendPoints()
        {
            Score testScore = new Score();
            testScore.AddPoint(10);
            Assert.IsTrue(testScore.SpendPoints(5));
            Assert.AreEqual(5, testScore.Value);
            Assert.IsFalse(testScore.SpendPoints(1000));
        }

        [TestMethod]
        public void TestClean()
        {

            Score testScore = new Score();
            testScore.AddPoint(10);
            testScore.Clean();

            Assert.AreEqual(0, testScore.Value);

        }
    }

     //#region Properties

     //   public int Value { get; private set; } 

     //   #endregion

     //   #region Methods

     //   public void AddPoint(int value)
     //   {
     //       Value += value;
     //   }

     //   public bool SpendPoints(int value)
     //   {
     //       if (value <= Value)
     //       {
     //           Value -= value;
     //           return true;
     //       }

     //       return false;
     //   }

     //   public void Clean()
     //   {
     //       Value = 0;
     //   } 

     //   #endregion
}
