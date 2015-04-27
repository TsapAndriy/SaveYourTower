using System;
using System.Collections.Generic;

namespace SaveYourTower.GameEngine.GameObjects.Interfaces
{
    interface ITower : ICollisional
    {
        void Fire();
    }
}
