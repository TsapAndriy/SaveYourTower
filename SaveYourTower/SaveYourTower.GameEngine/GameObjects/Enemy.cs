using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Enemy : GameObject, IEnemy
    {
        public Enemy(Point position, UnitVector2 direction, int colliderRaius, int velosity)
            : base(position, direction, colliderRaius, velosity)
        {

        }
    }
}
