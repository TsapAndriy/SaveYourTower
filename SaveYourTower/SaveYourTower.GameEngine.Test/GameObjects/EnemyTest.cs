using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.GameObjects;
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
            Field field = new Field(new Point(10, 10));

            Point position = new Point(1,1);

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

            Field field = new Field(new Point(10, 10));
            CannonBall cannonBall = new CannonBall(field, new Point(1, 1), new UnitVector2(90), 2, 1, 1, 10);

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

            Field field = new Field(new Point(10, 10));
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

            Field field = new Field(new Point(10, 10));

            Turret turret = new Turret(field, new Point(1, 1), 1, 5, 10);

            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

            field.AddGameObject(turret);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            collisionDetector.FindCollisions(field);

            Assert.IsFalse(enemy.IsAlive);
        }

        [TestMethod]
        public void TestOnCollisionWithMine()
        {

            Field field = new Field(new Point(10, 10));

            Mine mine = new Mine(field, new Point(1, 1), 2, 2);

            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

            field.AddGameObject(mine);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            collisionDetector.FindCollisions(field);

            Assert.IsFalse(enemy.IsAlive);
        }

        [TestMethod]
        public void TestLookAtPoint()
        {
            GameObject enemy = new Enemy(null, new Point(1, 1), 1, 1, 1, 1);

            enemy.LookAt(new Point(1, 2));
            Assert.AreEqual(Math.Round((90d * Math.PI / 180), 3), Math.Round(enemy.Direction.Angle, 3));

            enemy.LookAt(new Point(1, 1));
            Assert.AreEqual(0, enemy.Velosity);
        }
    }



     //   #endregion

     //   #region Methods

     //   public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
     //   {
     //       if ((gameObject is CannonBall) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
     //       {
     //           this.ReceiveDamage(gameObject.Damage);

     //           if (this.LifePoints <= 0)
     //           {
     //               this.IsAlive = false;
     //               GameField.GameScore.AddPoint(1);
     //           }
     //       }
     //       else if ((gameObject is Tower) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
     //       {
     //           this.IsAlive = false;
     //       }
     //       else if ((gameObject is Turret) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
     //       {
     //           this.IsAlive = false;
     //       }
     //       else if (gameObject is Mine)
     //       {
     //           if ((collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
     //           {
     //               this.IsAlive = false;
     //           }
     //           else if ((collisionEventArgs.OtherCollider.Tag == "ExplosionCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
     //           {
     //               if ((gameObject as Mine).IsExplose)
     //               {
     //                   this.IsAlive = false;
     //               }
     //           }
     //       }
     //   } 

     //   #endregion
}
