using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;

namespace SaveYourTower.GameEngine.GameObjects.Interfaces
{
    interface ITurret : ICollisional
    {
        void Fire();
    }
}
