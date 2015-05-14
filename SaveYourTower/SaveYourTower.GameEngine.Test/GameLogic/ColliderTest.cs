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
    public class ColliderTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Point position = new Point(1, 1);
            string tag = "Body";
            Collider collider = new Collider(position, 2, tag);

            Assert.AreSame(position, collider.Position);
            Assert.AreEqual(2, collider.Radius);
            Assert.AreSame(tag, collider.Tag);
        }

        [TestMethod]
        public void TestDoCollision()
        {
            CannonBall cannonBall = new CannonBall(null, null, null, 1, 1, 1, 10);
            Enemy enemy = new Enemy(null, null, 1, 1, 1, 1);
            CollisionEventArgs e = new CollisionEventArgs(cannonBall.Colliders.ToArray()[0], enemy.Colliders.ToArray()[0]);

            cannonBall.Colliders.ForEach(obj => obj.RaiseCollisionEvent(enemy, e));

            Assert.IsFalse(cannonBall.IsAlive);
        }
    }
}
