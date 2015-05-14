using System;

using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;

namespace SaveYourTower.GameEngine.GameLogic
{
    
    public class CollisionDetector
    {
        #region Methods

        public void FindCollisions(Field gameField)
        {
            GameObject[] gameObjects = gameField.GameObjects.ToArray();

            for (int i = 0; i < gameObjects.Length; i++)
            {
                for (int j = 0; j < gameObjects.Length; j++)
                {
                    if (i != j)
                    {
                        CheckColliders(gameObjects[i], gameObjects[j]);
                    }
                }
            }
        }

        private void CheckColliders(GameObject left, GameObject right)
        {
            foreach (Collider colliderLeft in left.Colliders)
            {
                foreach (Collider colliderRight in right.Colliders)
                {
                    if (CheckOnConllisions(colliderLeft, colliderRight))
                    {
                        colliderLeft.RaiseCollisionEvent(right, 
                            new CollisionEventArgs(colliderLeft, colliderRight));

                        colliderRight.RaiseCollisionEvent(left, 
                            new CollisionEventArgs(colliderRight, colliderLeft));
                    }
                }
            }
        }

        private bool CheckOnConllisions(Collider left, Collider right)
        {
            double distance = Distance(left.Position, right.Position) 
                - (left.Radius + left.Radius);

            return (distance <= 0);
        }

        private double Distance(Point left, Point right)
        {
            return Math.Sqrt(Math.Pow((left.X - right.X), 2)
                + Math.Pow((left.Y - right.Y), 2));
        } 

    #endregion
    }
}
