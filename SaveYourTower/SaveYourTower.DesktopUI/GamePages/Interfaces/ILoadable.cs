using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.DesktopUI.GamePages
{
    interface ILoadable
    {
        event Action<Type> PageEventHandler;
    }
}
