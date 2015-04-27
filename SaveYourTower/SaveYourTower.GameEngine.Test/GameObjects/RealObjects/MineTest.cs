using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.Test.GameObjects
{
    [TestClass]
    public class MineTest
    {
        [TestMethod]
        public void TestConstructor()
        { 
            Field field = new Field(new Point(10, 10), 1);
            Point position = new Point(1, 1);

            Mine mine = new Mine(field, position);

            Assert.IsNotNull(mine);
            Assert.AreSame(field, mine.GameField);
            Assert.AreSame(position, mine.Position);
            Assert.IsNotNull(mine.Colliders);
        }

        [TestMethod]
        public void TestExplode()
        {
            Field field = new Field(new Point(10, 10), 1);
            Point position = new Point(1, 1);

            Mine mine = new Mine(field, position);

            mine.Explode();

            Assert.IsTrue(mine.IsExplose);
        }

        [TestMethod]
        public void TestLive()
        {
            Field field = new Field(new Point(10, 10), 1);
            Point position = new Point(1, 1);

            Mine mine = new Mine(field, position);
            
            Assert.IsTrue(mine.IsAlive);
            mine.Explode();
            mine.Live();
            Assert.IsFalse(mine.IsAlive);
        }

        [TestMethod]
        public void TestOnCollison()
        {

            Field field = new Field(new Point(10, 10), 1);
            Mine mine = new Mine(field, new Point(1, 1), 2, 2);

            GameObject enemy = new Enemy(field, new Point(1, 2), 2, 1, 1, 10);

            field.AddGameObject(mine);
            field.AddGameObject(enemy);

            CollisionDetector collisionDetector = new CollisionDetector();

            collisionDetector.FindCollisions(field);
            mine.Live();

            Assert.IsFalse(mine.IsAlive);
        }
    }
}
