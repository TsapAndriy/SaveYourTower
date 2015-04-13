using System;
using System.Collections.Generic;

using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameLogic
{
    static class CollisionDetector
    {
        public static void FindCollisions(Field gameField)
        {
            GameObject[] gameObjects = gameField.GameObjects.ToArray(); 

            for (int i = 0; i < gameField.GameObjects.Count; i++)
            {
                for (int j = (gameField.GameObjects.Count - (gameField.GameObjects.Count - i)); j < gameField.GameObjects.Count; j++)
                {
                    foreach (Collider colliderLeft in gameObjects[i].Colliders)
                    {
                        foreach (Collider colliderRight in gameObjects[j].Colliders)
                        {
                            if (CheckOnConllisions(colliderLeft, colliderRight))
                            {
                                colliderLeft.DoCollision(gameObjects[j], new CollisionEventArgs(colliderLeft, colliderRight));
                                colliderRight.DoCollision(gameObjects[i], new CollisionEventArgs(colliderRight, colliderLeft));
                            }
                        }
                    }
                }
            }
        }

        public static bool CheckOnConllisions(Collider left, Collider right)
        {
            return (Distance(left.Position, right.Position) - (left.Radius + left.Radius) <= 0);
        }
        
        public static double Distance(Point left, Point right)
        { 
            return Math.Sqrt(Math.Pow((left.X - right.X), 2) 
                + Math.Pow((left.Y - right.Y), 2));
        }
    }
}
