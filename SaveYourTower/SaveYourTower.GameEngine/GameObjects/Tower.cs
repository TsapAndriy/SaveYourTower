using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Tower : GameObject, ITower
    {
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

        } 

        #endregion

        #region Methods

        public void Fire()
        {
            GameField.AddGameObject(new CannonBall(GameField, Position.Clone(), new UnitVector2(Direction.Angle), 1, 2, 1, 10));
        }

        public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.ReceiveDamage(gameObject.Damage);
            }
        }

        
        #endregion
    }
}
