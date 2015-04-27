using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;

namespace SaveYourTower.GameEngine.GameLogic
{
    public class Collider
    {
        #region Events

        public event Action<GameObject, CollisionEventArgs> CollisionEventHandler;

        #endregion

        #region Properties

        public Point Position { get; private set; }
        public double Radius { get; private set; }
        public string Tag { get; private set; } 

        #endregion

        #region Methods

        public Collider(Point position, double radius, string tag)
        {
            Position = position;
            Radius = radius;
            Tag = tag;
        }

        public void DoCollision(GameObject gameObject, CollisionEventArgs collisionEventArgs)
        {
            CollisionEventHandler(gameObject, collisionEventArgs);
        } 

        #endregion
    }
}
