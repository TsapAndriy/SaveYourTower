using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.GameEngine.DataContainers
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            SetPostition(x, y);
        }

        public void SetPostition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Point operator + (Point left, Point right)
        {
            return new Point((left.X + right.X), (left.X + right.Y));
        }

        public static Point operator - (Point left, Point right)
        {
            return new Point((left.X - right.X), (left.X - right.Y));
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }

        public override bool Equals(Object obj)
        {
            Point point = obj as Point;
        
            return ((this.X == point.X) && (this.Y == point.Y));
        }
    }
}
