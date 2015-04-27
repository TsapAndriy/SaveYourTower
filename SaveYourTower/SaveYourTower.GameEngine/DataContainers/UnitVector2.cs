using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.GameEngine.DataContainers
{
    public class UnitVector2 : ICloneable
    {
        #region Properties

        public double X { get; private set; }
        public double Y { get; private set; } 

		
		// Angle is radian.
		public double Angle
		{
			get
			{
				return Math.Atan2(Y, X);
			}
			set
			{
				X = Math.Cos(value);
				Y = Math.Sin(value);
			}
		}

        #endregion

        #region Constructors

        public UnitVector2(double angle)
        {
            Angle = angle;
        }

        public UnitVector2(double x, double y)
        {
            X = x;
            Y = y;

            Normalize();
        }

        #endregion

        #region Methods

        public object Clone()
        {
            return new UnitVector2(X, Y);
        }

        public override bool Equals(object obj)
        {
            return ((this.X == ((UnitVector2)obj).X)
                && (this.Y == ((UnitVector2)obj).Y));
        }

        // Convert random vector to unit vector.
        public void Normalize()
        {
            double magnitude = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            magnitude = (magnitude == 0 ? (-1 * double.MinValue) : magnitude);
            X = X / magnitude;
            Y = Y / magnitude;
        } 

        #endregion
    }
}
