using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.Test.DataContainers
{
    [TestClass]
    public  class CollisionEventArgsTest
    {
        [TestMethod]
        public void TestConstructor()
        { 
            Collider collider1 = new Collider(new Point(1,1), 1, "Body");
            Collider collider2 = new Collider(new Point(3,3), 2, "Finding");
            CollisionEventArgs collisionEventArgs = new CollisionEventArgs(collider1, collider2);

            Assert.AreSame(collisionEventArgs.MyCollider, collider1);
            Assert.AreSame(collisionEventArgs.OtherCollider, collider2);
        }
    }
}
