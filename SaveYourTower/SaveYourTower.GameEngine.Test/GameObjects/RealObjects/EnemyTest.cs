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
    public class EnemyTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Field field = new Field(new Point(10, 10), null);

            Point position = new Point(1, 1);

            Enemy enemy = new Enemy(field, position, 1, 2, 3, 4);

            Assert.IsNotNull(enemy);
            Assert.AreSame(field, enemy.GameField);
            Assert.AreSame(position, enemy.Position);
            Assert.AreEqual(2, enemy.Velosity);
            Assert.AreEqual(3, enemy.Damage);
            Assert.AreEqual(4, enemy.LifePoints);
        }

        [TestMethod]
        public void TestOnCollisionWithCannonBall()
        {

            Field field = new Field(new Point(10, 10), null);
            Cannonball cannonBall = new Cannonball(field, new Point(1, 1), new UnitVector2(90), 2, 1, 1, 10);

            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

            field.AddGameObject(cannonBall);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            collisionDetector.FindCollisions(field);

            Assert.IsFalse(enemy.IsAlive);
        }

        [TestMethod]
        public void TestOnCollisionWithTower()
        {

            Field field = new Field(new Point(10, 10), null);
            Tower tower = new Tower(field, new Point(1, 1), null, 2, 10);
            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

            field.AddGameObject(tower);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);

            Assert.IsFalse(enemy.IsAlive);
        }

        [TestMethod]
        public void TestOnCollisionWithTurret()
        {

            Field field = new Field(new Point(10, 10), null);

            Tower tower = new Tower(field, new Point(9, 9), null, 1, 5);
            field.AddGameObject(tower);

            Turret turret = new Turret(field, new Point(1, 1), 1, 5, 10);
            field.AddGameObject(turret);

            Enemy enemy = new Enemy(field, new Point(1, 2), damage: 1000);
            field.AddGameObject(enemy);


            CollisionDetector collisionDetector = new CollisionDetector();
            collisionDetector.FindCollisions(field);

            turret.Live();
            enemy.Live();

            Assert.IsFalse(turret.IsAlive);
            Assert.IsTrue(enemy.IsAlive);
        }

        [TestMethod]
        public void TestLookAtPoint()
        {
            Field field = new Field(new Point(10, 10), null);
            Enemy enemy = new Enemy(field, new Point(1, 1), 1, 1, 1, 1);

            enemy.LookAt(new Point(1, 2));
            Assert.AreEqual(Math.Round((90d * Math.PI / 180), 3), Math.Round(enemy.Direction.Angle, 3));

            enemy.LookAt(new Point(1, 1));
            Assert.AreEqual(0, enemy.Velosity);
        }

        [TestMethod]
        public void TestLive()
        {
            Level[] level = {new Level()};
            Game game = new Game(new Point(10, 10), level);

            Point position = new Point(1, 1);
            Enemy enemy = new Enemy(game.GameField, position, 1, 1, 1, 1);
            Point oldPosition = (Point)enemy.Position.Clone();
            UnitVector2 oldDirection = (UnitVector2)enemy.Direction.Clone();

            Assert.IsTrue(position.Equals(oldPosition));
            Assert.IsTrue(enemy.Direction.Equals(oldDirection));

            enemy.Live();

            Assert.IsFalse(position.Equals(oldPosition));
            Assert.IsFalse(enemy.Direction.Equals(oldDirection));
        }

        [TestMethod]
        public void TestLiveDIe()
        {
            Field gameField = new Field(new Point(10, 10), new Level());
            Enemy enemy = new Enemy(gameField, new Point(1, 1));

            enemy.ReceiveDamage(100000);
            enemy.Live();

            Assert.IsFalse(enemy.IsAlive);
        }
    }
}
