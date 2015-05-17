using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.RealObjects.Interfaces;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.GameObjects.RealObjects
{
    public class Turret : GameObject, ITurret
    {
        #region Fields

        private int _fireCounter;
        private bool _hasTarget;

        #endregion

        #region Properties

        public int FindingColliderRadius { get; private set; }
        public int FireSpeedDivisor { get; private set; }
        public GameObject Target { get; private set; }
        private List<GameObject> _targets = new List<GameObject>();
        Random _rand = new Random();

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
            Collider findingCollider = 
                new Collider(position, findingColliderRadius, "FindingCollider");

            findingCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(findingCollider);

            Collider bodyCollider = 
                new Collider(position, bodyColliderRaius, "BodyCollider");

            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);

            FireSpeedDivisor = fireSpeedDivisor;

            FindingColliderRadius = findingColliderRadius;
        }

        #endregion

        #region Methods

        public void Fire()
        {
            if ((Target != null) && (Target.IsAlive))
            {
                if (_fireCounter == FireSpeedDivisor)
                {
                    LookAt(Target.Position);
                    GameField.AddGameObject(new CannonBall(
                        GameField,
                        (Point) Position.Clone(),
                        new UnitVector2(Direction.Angle),
                        GameField.CurrenGameLevel.CannonBallColliderRadius,
                        GameField.CurrenGameLevel.TurretCannonBallVelosity,
                        GameField.CurrenGameLevel.TurretCannonDamage,
                        GameField.CurrenGameLevel.TurretCannonBallLifeTime
                        ));
                }
            }
            else
            {
                _hasTarget = false;
            }

            _fireCounter = 
                (_fireCounter < FireSpeedDivisor) ? (_fireCounter + 1) : 0;
        }
        
        public void OnCollision(object sender, CollisionEventArgs e)
        {
            GameObject gameObject = sender as GameObject;

            if (gameObject is Enemy
                && (e.OtherCollider.Tag == "BodyCollider") 
                && (e.MyCollider.Tag == "FindingCollider"))
            {
                if (_targets.Find(obj => obj.Equals(gameObject)) == null)
                {
                    _targets.Add(gameObject);
                }
            }

            if (!_hasTarget 
                && (gameObject is Enemy) 
                && (e.OtherCollider.Tag == "BodyCollider") 
                && (e.MyCollider.Tag == "FindingCollider"))
            {
                RandomTarget();
                _hasTarget = true;
            }
            else if ((gameObject is Enemy) 
                && (e.OtherCollider.Tag == "BodyCollider") 
                && (e.MyCollider.Tag == "BodyCollider"))
            {
                ReceiveDamage(gameObject.Damage);

                if (LifePoints <= 0)
                {
                    IsAlive = false;
                }
            }
        }

        public void Live()
        {
            Fire();
            RemoveOutOfRangeTargets();
            _targets.RemoveAll(obj => !obj.IsAlive);
        }

        private void RemoveOutOfRangeTargets()
        {
            _targets.RemoveAll(obj => 
                FindingColliderRadius < (int)Distance(Position, obj.Position));
        }

        private void RandomTarget()
        {
            int targetId = 0;

            if (_targets.Count > 0)
            {
                targetId = _rand.Next(_targets.Count);
            }
            Target = _targets.ToArray()[targetId];
        }

        private double Distance(Point left, Point right)
        {
            return Math.Sqrt(Math.Pow((left.X - right.X), 2)
                + Math.Pow((left.Y - right.Y), 2));
        } 

        #endregion
    }
}
