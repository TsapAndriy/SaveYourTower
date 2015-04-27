using System;
using System.Collections.Generic;

using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Mine : GameObject, IMine, ILive
    {
        #region Properties

        public int _fireTimer;
        public bool IsExplose { get; private set; }
        public int ExlosionRadius { get; private set; } 

        #endregion

        #region Constructors

        public Mine(
            Field gameField,
            Point position,
            int colliderRaius = 1,
            int explosionRadius = 20,
            int cost = int.MaxValue)
            : base(
                gameField,
                position,
                colliderRaius: colliderRaius,
                cost: cost)
        {
            Collider mineExplosionCollider = new Collider(position, explosionRadius, "ExplosionCollider");
            mineExplosionCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(mineExplosionCollider);

            Collider bodyCollider = new Collider(position, colliderRaius, "BodyCollider");
            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);
        } 

        #endregion

        #region Methods

        public void Explode()
        {
            IsExplose = true;
        }

        public void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is Enemy) 
                && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") 
                && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                Explode();
            }
        }

        public void Live()
        {
            if (IsExplose)
            {
                this.IsAlive = false; 
            }
        } 

        #endregion
    }
}
