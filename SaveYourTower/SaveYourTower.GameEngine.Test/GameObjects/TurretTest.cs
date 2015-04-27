using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.GameObjects;
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
            Field field = new Field(new Point(10, 10));
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
            Field field = new Field(new Point(10, 10));
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
            Field field = new Field(new Point(10, 10));

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
    }

     //#region Fields

     //   private int _fireCounter = 0;
     //   private bool _hasTarget = false;

     //   #endregion

     //   #region Properties

     //   public int FindingColliderRadius { get; private set; }
     //   public int FireSpeedDivisor { get; private set; }
     //   public GameObject Target { get; private set; }

     //   #endregion

     //   #region Constructors

     //   public Turret(
     //       Field gameField,
     //       Point position,
     //       int colliderRaius,
     //       int lifePoints,
     //       int fireSpeedDivisor = 2,
     //       int cost = int.MaxValue)
     //       : base(
     //           gameField,
     //           position,
     //           colliderRaius: colliderRaius,
     //           lifePoints: lifePoints,
     //           cost: cost)
     //   {
     //       Collider findingCollider = new Collider(position, 5, "FindingCollider");
     //       findingCollider.CollisionEventHandler += OnCollision;
     //       Colliders.Add(findingCollider);

     //       FireSpeedDivisor = fireSpeedDivisor;
     //   } 

     //   #endregion

     //   #region Methods

     //   public void Fire()
     //   {
     //       if ((Target != null) && (Target.IsAlive) && (_fireCounter == FireSpeedDivisor))
     //       {
     //           LookAt(Target.Position);
     //           GameField.AddGameObject(new CannonBall(GameField, Position.Clone(), new UnitVector2(Direction.Angle), 1, 1, 1, 10));
     //       }
     //       else
     //       {
     //           _hasTarget = false;
     //       }
     //       _fireCounter = (_fireCounter < FireSpeedDivisor ? (_fireCounter + 1) : 0);
     //   }

     //   public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
     //   {
     //       if (!_hasTarget && (gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "FindingCollider"))
     //       {
     //           Target = gameObject;
     //           _hasTarget = true;
     //       }
     //       else if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
     //       {
     //           this.ReceiveDamage(gameObject.Damage);

     //           if (this.LifePoints <= 0)
     //           {
     //               this.IsAlive = false;
     //           }
     //       }
     //   }
}
