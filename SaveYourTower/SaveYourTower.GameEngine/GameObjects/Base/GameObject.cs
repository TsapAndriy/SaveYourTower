using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.DataContainers;

using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.Base
{
    public abstract class GameObject
    {
        #region Fields

        private double _velosity;

        #endregion

        #region StaticProperties

        public static double VelositiDivisor { get; set; }

        #endregion

        #region Properties

        public int Cost { get; private set; }        
        public int LifePoints { get; private set; }
        public int Damage { get; private set; }
        public List<Collider> Colliders { get; private set; }
        public Point Position { get; private set; }
        public Field GameField { get; private set; }

        public bool IsAlive { get; set; }
        public UnitVector2 Direction { get; set; }

        public double Velosity
        {
            get { return (_velosity / VelositiDivisor); }
            set { _velosity = value; }
        }

        #endregion

        #region Constructors

        static GameObject()
        {
            VelositiDivisor = 1;
        }

        public GameObject(
            Field gameField, 
            Point position,
            UnitVector2 direction = null,
            int colliderRaius = 1,
            double velosity = 0,
            int damage = 1, 
            int lifePoints = 1,
            int cost = int.MaxValue)
        {
            GameField = gameField;
            Position = position;
            Direction = (direction == null) ? new UnitVector2(0) : direction;
            Velosity = velosity;
            Damage = damage;
            LifePoints = lifePoints;
            Cost = cost;

            Colliders = new List<Collider>();
            
            IsAlive = true;
        }

        #endregion

        #region Methods

        // Move game object using direction and velosity.
        public void MoveOnVelosity()
        {
            if (Velosity != 0)
            {
                Position.X += (Direction.X * Velosity);
                Position.Y += (Direction.Y * Velosity);
            }
        }

        public void ReceiveDamage(int damage)
        {
            this.LifePoints -= damage;
        }

        // Set game object direction to the specific point.
        public void LookAt(Point point)
        {
            double newX = point.X - Position.X;
            double newY = point.Y - Position.Y;

            if ((newX == 0) && (newY == 0))
            {
                Velosity = 0;
                return;
            }

            Direction = new UnitVector2(newX, newY);
        }

        public void Rotate(double angle)
        {
            this.Direction.Angle += angle;
        }

        #endregion
    }
}
