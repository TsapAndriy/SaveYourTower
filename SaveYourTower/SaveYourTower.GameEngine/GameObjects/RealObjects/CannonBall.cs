using System;
using System.Collections.Generic;
using System.Configuration;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class CannonBall : GameObject, ICannonBall, ILive
    {
        #region Fields

        private int _timeToLive;
 
        #endregion

        #region Constructors

        public CannonBall(
           Field gameField,
           Point position,
           UnitVector2 direction,
           int colliderRaius,
           double velosity,
           int damage,
           int timeToLive)
            : base(
                gameField,
                position,
                direction,
                colliderRaius,
                velosity,
                damage : damage,
                lifePoints: 1)
        {
            _timeToLive = timeToLive;

            Collider bodyCollider = new Collider(position, colliderRaius, "BodyCollider");
            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);
        } 

        #endregion

        #region Methods

        public void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.IsAlive = false;
            }
        }

        public void Live()
        {
            _timeToLive--;
            if (_timeToLive <= 0)
            {
                this.IsAlive = false;
            }
            MoveOnVelosity();
        } 

        #endregion
    }
}
