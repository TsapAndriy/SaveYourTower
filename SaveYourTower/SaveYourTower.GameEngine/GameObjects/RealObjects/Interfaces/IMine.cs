using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.GameEngine.GameObjects.Interfaces
{
    interface IMine : ICollisional, ILive 
    {
        void Explode();
    }
}
