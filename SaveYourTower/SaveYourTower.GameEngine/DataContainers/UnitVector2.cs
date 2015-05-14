using System;

namespace SaveYourTower.GameEngine.DataContainers
{
    public class UnitVector2 : ICloneable
    {
        #region Properties

        public double X { get; private set; }
        public double Y { get; private set; } 

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

        // Convert random vector to unit vector.
        public void Normalize()
        {
            double magnitude = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            magnitude = (magnitude.Equals(0) ? (-1 * double.MinValue) : magnitude);
            X = X / magnitude;
            Y = Y / magnitude;
        }

        public override bool Equals(object obj)
        {
            UnitVector2 vector = obj as UnitVector2;
            bool equals = false;

            if (vector != null)
            {
                equals = X.Equals(vector.X) && Y.Equals(vector.Y);
            }

            return equals;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        } 

        #endregion
    }
}
