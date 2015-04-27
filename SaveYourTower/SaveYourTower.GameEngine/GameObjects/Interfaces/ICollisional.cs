using System;
using System.Collections.Generic;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Base;

namespace SaveYourTower.GameEngine.GameObjects.Interfaces
{
    interface ICollisional
    {
        void OnCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs);
    }
}
