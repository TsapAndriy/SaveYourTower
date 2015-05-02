using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Enemy : GameObject, IEnemy
    {
        #region Properties

        public Tower LookingTower { get; private set; }

        #endregion

        #region Constructors

        public Enemy(
           Field gameField,
           Point position,
           int colliderRaius = 1,
           double velosity = 1,
           int damage = 1,
           int lifePoints = 1)
            : base(
                gameField,
                position,
                colliderRaius: colliderRaius,
                damage : damage,
                velosity: velosity,
                lifePoints: lifePoints)
        {
            Collider bodyCollider = new Collider(position, colliderRaius, "BodyCollider");
            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);
            if (gameField != null)
            {
                LookingTower = (Tower)GameField.GameObjects.Find(obj => obj is Tower);
            }
        } 

        #endregion

        #region Methods

        public void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is CannonBall) 
                && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") 
                && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.ReceiveDamage(gameObject.Damage);

                if ((this.IsAlive) && (LifePoints <= 0))
                {
                    this.IsAlive = false;
                    GameField.GameScore.AddPoint(1);
                }
            }
            else if ((gameObject is Tower) 
                && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") 
                && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.IsAlive = false;
            }
            else if ((gameObject is Turret) 
                && (collisionEventArgs.OtherCollider.Tag == "BodyCollider")
                && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.IsAlive = false;
            }
            else if (gameObject is Mine)
            {
                if ((collisionEventArgs.OtherCollider.Tag == "BodyCollider") 
                    && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
                {
                    this.IsAlive = false;
                }
                else 
                    if ((collisionEventArgs.OtherCollider.Tag == "ExplosionCollider") 
                    && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
                {
                    if ((gameObject as Mine).IsExplose)
                    {
                        this.IsAlive = false;
                    }
                }
            }
        }

        public void Live()
        {
            LookAt(LookingTower.Position);

            MoveOnVelosity();
        }

        #endregion
    }
}
