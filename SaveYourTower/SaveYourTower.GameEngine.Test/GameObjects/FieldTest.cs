using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;


namespace SaveYourTower.GameEngine.Test.GameObjects
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Field field = new Field(new Point(10, 15), 1);

            Assert.AreEqual(10, field.Size.X);
            Assert.AreEqual(15, field.Size.Y);
            Assert.IsNotNull(field);
            Assert.IsNotNull(field.GameScore);
            Assert.IsNotNull(field.GameObjects);
            Assert.AreEqual(0, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestAddGameObject()
        {
            Field field = new Field(new Point(10, 15), 1);


            Assert.AreEqual(1, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestRemoveGameObject()
        {
            Field field = new Field(new Point(10, 15), 1);

            Assert.AreEqual(1, field.GameObjects.Count);
            
            Assert.AreEqual(0, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestClean()
        {
            Field field = new Field(new Point(10, 15), 1);

            field.Clean();

            Assert.AreEqual(0, field.GameObjects.Count);
        }

    }
}
