using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.RealObjects;

namespace SaveYourTower.GameEngine.Test.DataContainers
{
    [TestClass]
    public class DieEventArgsTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            var gameObject = new Enemy(null, null);
            DieEventArgs dieEventArgs = new DieEventArgs(gameObject);

            Assert.AreSame(gameObject, dieEventArgs.DeadGameObject);
        }
    }
}
