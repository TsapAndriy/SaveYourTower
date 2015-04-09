using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Score
    {
        public int Value { get; private set; }

        public void AddPoint()
        {
            Value++;
        }

        public void Clean()
        {
            Value = 0;
        }
    }
}
