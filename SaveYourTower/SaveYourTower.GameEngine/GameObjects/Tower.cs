using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Tower : GameObject, ITower
    {
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

        }

        public void Fire()
        {
            //Instantine(new CannonBall(GameField, Position.Clone(), new UnitVector2(Direction.Angle), 1, 2, 10));
            GameField.AddGameObject(new CannonBall(GameField, Position.Clone(), new UnitVector2(Direction.Angle), 1, 2, 10));
        }

        public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.ReceiveDamage(gameObject.Damage);
            }
        }
    }
}
