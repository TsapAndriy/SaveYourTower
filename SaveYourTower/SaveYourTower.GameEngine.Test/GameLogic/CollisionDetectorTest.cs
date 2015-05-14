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
            Field field = new Field(new Point(10, 10), new Level());
            var enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);
            var cannonBall1 = new CannonBall(field, new Point(1, 2), null, 1, 1, 10000, 10);
            var cannonBall2 = new CannonBall(field, new Point(5, 5), null, 1, 1, 10000, 10);

            field.AddGameObject(enemy);
            field.AddGameObject(cannonBall1);
            field.AddGameObject(cannonBall2);

            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);


            Assert.IsTrue(cannonBall2.IsAlive);
            Assert.IsFalse(cannonBall1.IsAlive);
            Assert.IsFalse(enemy.IsAlive);
        }
    }
}
