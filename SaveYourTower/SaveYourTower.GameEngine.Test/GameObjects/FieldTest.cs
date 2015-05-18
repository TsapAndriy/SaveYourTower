using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.RealObjects;


namespace SaveYourTower.GameEngine.Test.GameObjects
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Field field = new Field(new Point(10, 15), new Level());

            Assert.AreEqual(10, field.Size.X);
            Assert.AreEqual(15, field.Size.Y);
            Assert.IsNotNull(field);
            Assert.IsNotNull(field.GameScore);
            Assert.IsNotNull(field.GameObjects);
            Assert.IsNotNull(field.CurrentGameLevel);
            Assert.AreEqual(0, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestAddGameObject()
        {
            Field field = new Field(new Point(10, 15), new Level());
            field.AddGameObject(new Enemy(null, null));

            Assert.AreEqual(1, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestRemoveGameObject()
        {
            Field field = new Field(new Point(10, 15), new Level());
            GameObject gameObject = new Enemy(null, null);

            field.AddGameObject(gameObject);
            Assert.AreEqual(1, field.GameObjects.Count);

            field.RemoveGameObject(gameObject);
            Assert.AreEqual(0, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestClean()
        {
            Field field = new Field(new Point(10, 15), null);

            field.Clean();

            Assert.AreEqual(0, field.GameObjects.Count);
        }

    }
}
