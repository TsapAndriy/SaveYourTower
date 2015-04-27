using System;
using System.Collections.Generic;

using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Mine : GameObject, IMine
    {
        #region Properties

        public bool IsExplose { get; private set; }
        public int ExlosionRadius { get; private set; } 

        #endregion

        #region Constructors

        public Mine(
            Field gameField,
            Point position,
            int colliderRaius = 1,
            int explosionRadius = 2,
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
        } 

        #endregion

        #region Methods

        public void Explode()
        {
            IsExplose = true;
        }

        public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                Explode();
            }
        }

        public override void Live()
        {
            if (IsExplose)
            {
                this.IsAlive = false;
            }
        } 

        #endregion
    }
}
