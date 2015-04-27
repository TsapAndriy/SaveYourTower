using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;

namespace SaveYourTower.GameEngine.Test.GameLogic
{
    [TestClass]
    public class CollisionDetectorTest
    {
        [TestMethod]
        public void TestFindCollisions()
        {
            Field field = new Field(new Point(10, 10), 1);

            Mine mine = new Mine(field, new Point(1, 1), 2, 2);

            Enemy enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 1);

            field.AddGameObject(mine);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            collisionDetector.FindCollisions(field);

            Assert.IsFalse(enemy.IsAlive);
        }
    }

     //public void FindCollisions(Field gameField)
     //   {
     //       GameObject[] gameObjects = gameField.GameObjects.ToArray(); 

     //       for (int i = 0; i < gameField.GameObjects.Count; i++)
     //       {
     //           for (int j = i; j < gameField.GameObjects.Count; j++)
     //           {
     //               CheckColliders(gameObjects[i], gameObjects[j]);
     //           }
     //       }
     //   }

     //   void CheckColliders(GameObject left, GameObject right)
     //   {
     //       foreach (Collider colliderLeft in left.Colliders)
     //       {
     //           foreach (Collider colliderRight in right.Colliders)
     //           {
     //               if (CheckOnConllisions(colliderLeft, colliderRight))
     //               {
     //                   colliderLeft.DoCollision(right, new CollisionEventArgs(colliderLeft, colliderRight));
     //                   colliderRight.DoCollision(left, new CollisionEventArgs(colliderRight, colliderLeft));
     //               }
     //           }
     //       }
     //   }

     //   bool CheckOnConllisions(Collider left, Collider right)
     //   {
     //       return (Distance(left.Position, right.Position) - (left.Radius + left.Radius) <= 0);
     //   }
        
     //   double Distance(Point left, Point right)
     //   { 
     //       return Math.Sqrt(Math.Pow((left.X - right.X), 2) 
     //           + Math.Pow((left.Y - right.Y), 2));
     //   }
}
