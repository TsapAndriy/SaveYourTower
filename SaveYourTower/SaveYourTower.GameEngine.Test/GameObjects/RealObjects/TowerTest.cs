using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.GameObjects;
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
            
            Field field = new Field(new Point(10, 10), 1);
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
            Field field = new Field(new Point(10, 10), 1);
            Point position = new Point(1, 1);
            UnitVector2 direction = new UnitVector2(0);

            Tower tower = new Tower(field, position, direction, 2, 10);

            tower.Live();

            Assert.IsNull(tower.GameField.GameObjects.Find(obj => { return (obj is CannonBall); }));

            tower.Fire();
            tower.Live();

            Assert.IsNotNull(tower.GameField.GameObjects.Find(obj => { return (obj is CannonBall); }));
            Assert.AreEqual(1, field.GameObjects.Count);

        }

        [TestMethod]
        public void TestOnCollision()
        {
            Field field = new Field(new Point(10, 10), 1);
            GameObject tower = new Tower(field, new Point(1, 1), null, 2, 10);
            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

            field.AddGameObject(tower);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            collisionDetector.FindCollisions(field);

            Assert.AreEqual(9, tower.LifePoints);
        }

        [TestMethod]
        public void TestOnCollisionDeath()
        {
            Field field = new Field(new Point(10, 10), 1);
            GameObject tower = new Tower(field, new Point(1, 1), null, 2, 1);
            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

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
            Tower tower = new Tower(null, null, null, 0, 0);
            //tower.Live();
            Assert.IsNull(tower.GameField);
            Assert.IsNull(tower.Position);
            Assert.AreEqual(0, tower.Colliders.ToArray()[0].Radius);
            Assert.AreEqual(0, tower.Velosity);
            Assert.IsTrue(tower.IsAlive);
        }
    }

        // #region Constructors

        //public Tower(
        //    Field gameField,
        //    Point position,
        //    UnitVector2 direction,
        //    int colliderRaius,
        //    int lifePoints)
        //    : base(
        //        gameField,
        //        position,
        //        direction,
        //        colliderRaius,
        //        0,
        //        lifePoints: lifePoints)
        //{

        //} 

        //#endregion

        //#region Methods

        //public void Fire()
        //{
        //    GameField.AddGameObject(new CannonBall(GameField, Position.Clone(), new UnitVector2(Direction.Angle), 1, 2, 1, 10));
        //}

        //public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        //{
        //    if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
        //    {
        //        this.ReceiveDamage(gameObject.Damage);
        //    }
        //} 

        //#endregion
}
