using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.GameEngine.DataContainers
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            SetPostition(x, y);
        }

        public void SetPostition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
