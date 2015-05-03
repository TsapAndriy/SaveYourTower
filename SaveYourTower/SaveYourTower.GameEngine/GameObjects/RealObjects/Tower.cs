using System;
using System.Collections.Generic;
using System.Configuration;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Tower : GameObject, ITower
    {
        #region Fields

        private bool _isFiring = false;

        #endregion

        #region Constructors

        public Tower(
            Field gameField,
            Point position,
            UnitVector2 direction,
            int colliderRaius,
            int lifePoints)
            : base(
                gameField,
                position,
                direction,
                colliderRaius,
                0,
                lifePoints: lifePoints)
        {
            Collider bodyCollider = new Collider(position, colliderRaius, "BodyCollider");
            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);
        } 

        #endregion

        #region Methods

        public void Fire()
        {
            _isFiring = !_isFiring;
        }

        public void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is Enemy) 
                && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") 
                && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.ReceiveDamage(gameObject.Damage);

                if (this.LifePoints <= 0)
                {
                    this.IsAlive = false;
                }
            }
        }

        public void Live()
        {
            if (_isFiring)
            {
                GameField.AddGameObject(new CannonBall(
                    GameField,
                    (Point) Position.Clone(),
                    new UnitVector2(Direction.Angle),
                    10,
                    10,
                    int.Parse(ConfigurationManager.AppSettings["TowerCannonDamage"]),
                    int.Parse(ConfigurationManager.AppSettings["TowerCannonBallLifeTime"])
                    ));
            }
            _isFiring = false;
        }
        
        #endregion
    }
}
