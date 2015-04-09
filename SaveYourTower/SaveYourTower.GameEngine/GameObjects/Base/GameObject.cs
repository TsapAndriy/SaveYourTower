using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.DataContainers;


namespace SaveYourTower.GameEngine.GameObjects.Base
{
    public class GameObject
    {
        #region Fields
        // Give tower posibility to create game objects.
        public InstantineDelegate Instantine;

        public bool Visible { get; set; }
        public int LifePoints { get; private set; }
        public int Damage { get; private set; }
        public int ColliderRadius { get; private set; }
        public int Velosity { get; set; }
        public UnitVector2 Direction { get; set; }
        public Point Position { get; private set; }

        #endregion

        #region Constructors

        public GameObject(Point position, UnitVector2 direction,
            int colliderRaius, int velosity, int damage = 1, int lifePoints = 1)
        {
            Visible = true;
            Velosity = velosity;
            Direction = direction;
            Position = position;
            ColliderRadius = colliderRaius;
            Damage = damage;
            LifePoints = lifePoints;
        }

        #endregion

        #region Methods

        // Move game object using direction and velosity fields.
        public void MoveOnVelosity()
        {
            if (Velosity > 0)
            {
                Point newPosition = new Point((int)(Direction.X * Velosity), (int)(Direction.Y * Velosity));

                // Do one forced simplest move when velosity is to low.
                if ((newPosition.X == 0) && (newPosition.Y == 0))
                {
                    newPosition.X = Position.X + Math.Sign(Direction.X);
                    newPosition.Y = Position.Y + Math.Sign(Direction.Y);
                }
                else
                {
                    newPosition.X += Position.X;
                    newPosition.Y += Position.Y;
                }

                Position = newPosition;
            }
        }

        #region Change direction

        public void ReceiveDamage(int damage)
        {
            this.LifePoints -= damage;
        }

        // Set game object direction to the specific point.
        public void LookAt(Point point)
        {
            int newX = point.X - Position.X;
            int newY = point.Y - Position.Y;

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

        #endregion
    }
}
