using System;
using System.Collections.Generic;

using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Turret : GameObject, ITurret
    {
        #region Fields

        private int _fireCounter = 0;
        private bool _hasTarget = false;

        #endregion

        #region Properties

        public int FindingColliderRadius { get; private set; }
        public int FireSpeedDivisor { get; private set; }
        public GameObject Target { get; private set; }

        #endregion

        #region Constructors

        public Turret(
            Field gameField,
            Point position,
            int bodyColliderRaius,
            int findingColliderRadius,
            int lifePoints,
            int fireSpeedDivisor = 2,
            int cost = int.MaxValue)
            : base(
                gameField,
                position,
                colliderRaius: bodyColliderRaius,
                lifePoints: lifePoints,
                cost: cost)
        {
            Collider findingCollider = new Collider(position, findingColliderRadius, "FindingCollider");
            findingCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(findingCollider);

            FireSpeedDivisor = fireSpeedDivisor;
        } 

        #endregion

        #region Methods

        public void Fire()
        {
            if ((Target != null) && (Target.IsAlive) && (_fireCounter == FireSpeedDivisor))
            {
                LookAt(Target.Position);
                GameField.AddGameObject(new CannonBall(GameField, Position.Clone(), new UnitVector2(Direction.Angle), 1, 1, 1, 10));
            }
            else
            {
                _hasTarget = false;
            }
            _fireCounter = (_fireCounter < FireSpeedDivisor ? (_fireCounter + 1) : 0);
        }

        public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if (!_hasTarget && (gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "FindingCollider"))
            {
                Target = gameObject;
                _hasTarget = true;
            }
            else if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.ReceiveDamage(gameObject.Damage);

                if (this.LifePoints <= 0)
                {
                    this.IsAlive = false;
                }
            }
        }

        
        // TO DO If another object is on the fire line, get new target.

        #endregion
    }
}
