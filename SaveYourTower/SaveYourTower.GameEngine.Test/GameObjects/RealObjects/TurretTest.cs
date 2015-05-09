using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;


namespace SaveYourTower.GameEngine.Test.GameObjects
{
    [TestClass]
    public class TurretTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Field field = new Field(new Point(10, 10), 1);
            Point position = new Point(1, 1);
            GameObject turret = new Turret(field, position, 1, 5, 1, 2);

            Assert.IsTrue(turret.IsAlive);
            Assert.AreSame(field, turret.GameField);
            Assert.AreSame(position, turret.Position);

            Assert.IsNotNull(turret.Colliders.Find( obj => { return (obj.Radius == 1); }));
            Assert.IsNotNull(turret.Colliders.Find( obj => { return (obj.Radius == 5); }));
            Assert.AreEqual(2, turret.Colliders.Count);
            Assert.AreEqual(1, turret.LifePoints);
        }

        [TestMethod]
        public void TestOnCollision()
        {
            Field field = new Field(new Point(10, 10), 1);
            Turret turret = new Turret(field, new Point(1, 5), 1, 5, 1, 2);
            Enemy enemy = new Enemy(field, new Point(1, 2), 1, 1, 1, 1);

            field.AddGameObject(turret);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            // Test finding enemy collision.
            collisionDetector.FindCollisions(field);
            Assert.AreSame(enemy, turret.Target);

            // Test body collision.
            enemy.Position.SetPostition(1, 4);
            collisionDetector.FindCollisions(field);

            Assert.IsFalse(turret.IsAlive);
            Assert.AreEqual(0, turret.LifePoints);
        }

        [TestMethod]
        public void TestFire()
        {
            Field field = new Field(new Point(10, 10), 1);
            Turret turret = new Turret(field, new Point(1, 5), 1, 5, 1, 0);
            Enemy enemy = new Enemy(field, new Point(1, 2), 1, 1, 1, 1);

            field.AddGameObject(turret);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);

            turret.Fire();

            Assert.AreEqual(3, field.GameObjects.Count);
            Assert.IsNotNull(turret.GameField.GameObjects.Find(obj => { return (obj is CannonBall); }));

            enemy.IsAlive = false;

            turret.Fire();
            Assert.AreEqual(3, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestLive()
        {
            Field field = new Field(new Point(10, 10), 1);
            Turret turret = new Turret(field, new Point(1, 5), 1, 5, 1, 0);
            Enemy enemy = new Enemy(field, new Point(1, 2), 1, 1, 1, 1);

            field.AddGameObject(turret);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);

            turret.Live();

            Assert.AreEqual(3, field.GameObjects.Count);
            Assert.IsNotNull(turret.GameField.GameObjects.Find(obj => { return (obj is CannonBall); }));

            enemy.IsAlive = false;
            turret.Live();

            Assert.AreEqual(3, field.GameObjects.Count);
        }
    }
}
