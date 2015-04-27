using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using SaveYourTower.GameEngine.GameObjects;

namespace SaveYourTower.GameEngine.Spells.Interfaces
{
    public interface ISpell
    {
        bool IsUsed { get; }
        Field GameField { get; }
        int ReloadingTime { get; }

        void Cast();
    }
}
