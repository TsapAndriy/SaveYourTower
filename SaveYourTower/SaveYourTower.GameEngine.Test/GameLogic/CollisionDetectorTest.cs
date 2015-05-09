using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;

namespace SaveYourTower.GameEngine.Test.GameLogic
{
    [TestClass]
    public class CollisionDetectorTest
    {
        [TestMethod]
        public void TestFindCollisions()
        {
            Field field = new Field(new Point(10, 10), 1);
            Enemy enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);

            Assert.IsFalse(enemy.IsAlive);
        }
    }
}
