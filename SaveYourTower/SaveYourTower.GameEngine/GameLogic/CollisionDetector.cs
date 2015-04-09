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
        public static void FindCollision(Field gameField)
        {
            var enemies = gameField.GameObjects.FindAll( o => { return (o is Enemy); } );
            var tower = gameField.GameObjects.Find( o => { return (o is Tower); } );

            gameField.GameObjects.ForEach( obj =>
                {
                    if ((obj is CannonBall) && obj.Visible)
                    {
                        foreach (var enemie in enemies)
                        {
                            if (Collision(obj, enemie))
                            {
                                if (HitDamage(obj, enemie))
                                {
                                    gameField.GameScore.AddPoint();
                                    enemie.Visible = false;
                                }

                                obj.Visible = false;
                            }
                        }
                    }
                    else if (obj is Enemy)
                    {
                        if (Collision(obj, tower))
                        {
                            HitDamage(obj, tower);
                            obj.Visible = false;
                        }
                    }
                }
            );

            gameField.GameObjects.RemoveAll(obj => { return !obj.Visible; } );
        }

        public static bool HitDamage(GameObject striker, GameObject receiver)
        {
            receiver.ReceiveDamage(striker.Damage);

            return receiver.LifePoints <= 0;
        }

        public static bool Collision(GameObject striker, GameObject receiver)
        {
             return ((Distance(striker.Position, receiver.Position)
                 - (striker.ColliderRadius + receiver.ColliderRadius)) <= 0);
        }

        public static double Distance(Point left, Point right)
        { 
            return Math.Sqrt(Math.Pow((left.X - right.X), 2) 
                + Math.Pow((left.Y - right.Y), 2));
        }
    }
}
