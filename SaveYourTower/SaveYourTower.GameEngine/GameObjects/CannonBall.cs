using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;



namespace SaveYourTower.GameEngine.GameObjects
{
    public class CannonBall : GameObject, ICannonBall 
    {
        private int _timeToLive;

        public CannonBall(
            Field gameField, 
            Point position, 
            UnitVector2 direction,
            int colliderRaius, 
            double velosity, 
            int timeToLive)
            : base(
                gameField,
                position, 
                direction, 
                colliderRaius,
                velosity,
                lifePoints: 1)
		{
            _timeToLive = timeToLive;
		}

        public override void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            if ((gameObject is Enemy) && (collisionEventArgs.OtherCollider.Tag == "BodyCollider") && (collisionEventArgs.MyCollider.Tag == "BodyCollider"))
            {
                this.IsAlive = false;
            }
        }

        public override void Live()
        {
            _timeToLive--;
            if (_timeToLive <= 0)
            {
                this.IsAlive = false;
            }
        }
    }


}
