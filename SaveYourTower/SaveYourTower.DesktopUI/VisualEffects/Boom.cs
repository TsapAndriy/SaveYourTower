using System;
using System.Drawing;
using System.Timers;
using Point = SaveYourTower.GameEngine.DataContainers.Point;

namespace SaveYourTower.DesktopUI.VisualEffects
{
    public class Boom
    {
        #region Fields

        private double _angle = 0.01;
        private Random _rand = new Random();
        
        #endregion


        #region Properties
        public Image Look { get; private set; }
        public bool IsAlive { get; private set; }
        public Point Position { get; private set; }
        public double Angle
        {
            get
            {
                _angle += (double)_rand.Next(-10, 10) / 100;
                return _angle;
            }
        }

        #endregion

        #region Constructors

        public Boom(Point position, Image look, int lifeTime)
        {
            IsAlive = true;
            Position = (Point)position.Clone();
            Look = look;

            Timer lifeTimer = new Timer(lifeTime);
            lifeTimer.Elapsed += Die;
            lifeTimer.AutoReset = false;
            lifeTimer.Start();
        } 

        #endregion

        #region Methods

        private void Die(object sender, ElapsedEventArgs e)
        {
            this.IsAlive = false;
        } 

        #endregion
    }
}
