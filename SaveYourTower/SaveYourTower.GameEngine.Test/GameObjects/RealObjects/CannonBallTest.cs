using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.Test.GameObjects
{
    [TestClass]
    public class CannonBallTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Field field = new Field(new Point(10, 10), null);

            CannonBall cannonBall = new CannonBall(field, new Point(1, 1), new UnitVector2(90), 1, 1, 1, 10);

            Assert.AreSame(cannonBall.GameField, field);
            Assert.IsTrue(cannonBall.Position.Equals(new Point(1, 1)));
            Assert.IsTrue(cannonBall.Direction.Equals(new UnitVector2(90)));
            Assert.IsNotNull(cannonBall.Colliders);
            Assert.IsTrue(cannonBall.Velosity == 1);
            Assert.IsTrue(cannonBall.Damage == 1);

            Assert.IsTrue(cannonBall.IsAlive);
        }

        [TestMethod]
        public void TestOnCollision()
        {

            Field field = new Field(new Point(10, 10), null);
            CannonBall cannonBall = new CannonBall(field, new Point(1, 1), new UnitVector2(90), 2, 1, 1, 10);

            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 10);

            field.AddGameObject(cannonBall);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            collisionDetector.FindCollisions(field);

            Assert.IsFalse(cannonBall.IsAlive);

        }

        [TestMethod]
        public void TestLive()
        {
            CannonBall cannonBall = new CannonBall(null, null, null, 0, 0, 0, 1);
            cannonBall.Live();
            Assert.IsFalse(cannonBall.IsAlive);
        }

        [TestMethod]
        public void TestMoveOnVelosity()
        {
            Field field = new Field(new Point(10, 10), null);
            CannonBall cannonBall = new CannonBall(field, new Point(1, 1), new UnitVector2(90), 2, 1, 1, 10);
            cannonBall.MoveOnVelosity();

            Assert.AreEqual(1, Math.Round(cannonBall.Position.X));
            Assert.AreEqual(2, Math.Round(cannonBall.Position.Y));
        }
    }
}
