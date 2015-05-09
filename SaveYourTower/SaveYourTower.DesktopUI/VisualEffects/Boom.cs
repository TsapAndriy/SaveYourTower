using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using SaveYourTower.GameEngine.DataContainers;

namespace SaveYourTower.DesktopUI.VisualEffects
{
    public class Boom
    {
        public bool IsAlive { get; private set; }
        public Point Position { get; private set; }
        private double _angle = 0.01;

        Random _rand = new Random();

        public double Angle
        {
            get
            {
                _angle += (double)_rand.Next(-10, 10)/100;
                return _angle;
            }
        }

        public Boom(Point position)
        {
            IsAlive = true;
            Position = (Point)position.Clone();

            Timer lifeTimer = new Timer(200);
            lifeTimer.Elapsed += Die;
            lifeTimer.AutoReset = false;
            lifeTimer.Start();
        }

        private void Die(object sender, ElapsedEventArgs e)
        {
            this.IsAlive = false;
        }
    }
}
