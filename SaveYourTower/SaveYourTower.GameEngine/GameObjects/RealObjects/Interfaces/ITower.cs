using System;
using System.Collections.Generic;

namespace SaveYourTower.GameEngine.GameObjects.Interfaces
{
    interface ITower : ICollisional, ILive
    {
        void Fire();
    }
}
