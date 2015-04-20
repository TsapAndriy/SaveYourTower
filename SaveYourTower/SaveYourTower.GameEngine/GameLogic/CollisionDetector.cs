using System;
using System.Collections.Generic;

using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameLogic
{
    public class CollisionDetector
    {
        public void FindCollisions(Field gameField)
        {
            GameObject[] gameObjects = gameField.GameObjects.ToArray(); 

            for (int i = 0; i < gameField.GameObjects.Count; i++)
            {
                for (int j = i; j < gameField.GameObjects.Count; j++)
                {
                    CheckColliders(gameObjects[i], gameObjects[j]);
                }
            }
        }

        void CheckColliders(GameObject left, GameObject right)
        {
            foreach (Collider colliderLeft in left.Colliders)
            {
                foreach (Collider colliderRight in right.Colliders)
                {
                    if (CheckOnConllisions(colliderLeft, colliderRight))
                    {
                        colliderLeft.DoCollision(right, new CollisionEventArgs(colliderLeft, colliderRight));
                        colliderRight.DoCollision(left, new CollisionEventArgs(colliderRight, colliderLeft));
                    }
                }
            }
        }

        bool CheckOnConllisions(Collider left, Collider right)
        {
            return (Distance(left.Position, right.Position) - (left.Radius + left.Radius) <= 0);
        }
        
        double Distance(Point left, Point right)
        { 
            return Math.Sqrt(Math.Pow((left.X - right.X), 2) 
                + Math.Pow((left.Y - right.Y), 2));
        }
    }
}
