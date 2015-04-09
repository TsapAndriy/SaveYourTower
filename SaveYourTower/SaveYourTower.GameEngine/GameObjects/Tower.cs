using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Tower : GameObject, ITower
    {
        public Tower(Point position, UnitVector2 direction, int colliderRaius, int velosity)
            : base(position, direction, colliderRaius, velosity, lifePoints : 10)
        {

        }

        public void Fire()
        {
            Instantine(new CannonBall(Position, new UnitVector2(Direction.Angle), 1, 2));
        }
    }
}
