using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Enemy : GameObject, IEnemy
    {
        public Enemy(
            Field gameField,
            Point position, 
            int colliderRaius,
            double velosity,
            int lifePoints)
            : base(
                gameField,
                position,
                colliderRaius : colliderRaius, 
                velosity : velosity,
                lifePoints : lifePoints)
        {

        }

        public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is CannonBall) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.ReceiveDamage(gameObject.Damage);

                if(this.LifePoints <= 0)
                {
                    this.IsAlive = false;
                    GameField.GameScore.AddPoint(1);
                }
            }
            else if ((gameObject is Tower) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.IsAlive = false;
            }
            else if ((gameObject is Turret) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.IsAlive = false;
            }
            else if (gameObject is Mine)
            {
                if ((collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
                {
                    this.IsAlive = false;
                }
                else if ((collisionEventArgs.OtherCollider.Tag == "ExplosionCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
                {
                    if ((gameObject as Mine).IsExplose)
                    {
                        this.IsAlive = false;
                    }
                }
            }

        }
    }
}
