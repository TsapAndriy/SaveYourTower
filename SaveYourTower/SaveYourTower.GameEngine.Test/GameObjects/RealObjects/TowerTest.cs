using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.Test.GameObjects
{
    [TestClass]
    public class TowerTest
    {
        [TestMethod]
        public void TestConstructor()
        {

            Field field = new Field(new Point(10, 10), null);
            Point position = new Point(1, 1);
            UnitVector2 direction = new UnitVector2(0);
            Tower tower = new Tower(field, position, direction, 2, 10);

            Assert.IsNotNull(tower);
            Assert.AreSame(field, tower.GameField);
            Assert.AreSame(position, tower.Position);
            Assert.AreSame(direction, tower.Direction);
            Assert.AreEqual(10, tower.LifePoints);
        }

        [TestMethod]
        public void TestFire()
        {
            Field field = new Field(new Point(10, 10), new Level());
            Point position = new Point(1, 1);
            UnitVector2 direction = new UnitVector2(0);
            Tower tower = new Tower(field, position, direction, 2, 10);

            tower.Live();

            Assert.IsNull(tower.GameField.GameObjects.Find(obj => { return (obj is Cannonball); }));

            tower.Fire();
            tower.Live();

            Assert.IsNotNull(tower.GameField.GameObjects.Find(obj => { return (obj is Cannonball); }));
            Assert.AreEqual(1, field.GameObjects.Count);

        }

        [TestMethod]
        public void TestOnCollision()
        {
            Field field = new Field(new Point(10, 10), new Level());
            GameObject tower = new Tower(field, new Point(1, 1), null, 2, 10);
            GameObject enemy = new Enemy(field, new Point(1, 2));

            field.AddGameObject(tower);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);

            Assert.AreEqual(8, tower.LifePoints);
        }

        [TestMethod]
        public void TestOnCollisionDeath()
        {
            Field field = new Field(new Point(10, 10), new Level());
            GameObject tower = new Tower(field, new Point(1, 1), null, 2, 2);
            GameObject enemy = new Enemy(field, new Point(1, 2));

            field.AddGameObject(tower);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);

            Assert.IsFalse(tower.IsAlive);
            Assert.AreEqual(0, tower.LifePoints);
        }

        [TestMethod]
        public void TestLive()
        {
            Field field = new Field(new Point(10, 10), new Level());
            Point position = new Point(1, 1);
            UnitVector2 direction = new UnitVector2(0);
            Tower tower = new Tower(field, position, direction, 2, 10);

            tower.Live();

            Assert.IsNull(tower.GameField.GameObjects.Find(obj => { return (obj is Cannonball); }));

            tower.Fire();
            tower.Live();

            Assert.IsNotNull(tower.GameField.GameObjects.Find(obj => { return (obj is Cannonball); }));
            Assert.AreEqual(1, field.GameObjects.Count);
        }
    }
}
