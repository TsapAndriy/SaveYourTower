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

            Mine mine = new Mine(field, new Point(1, 1));

            field.AddGameObject(mine);

            Assert.AreEqual(1, field.GameObjects.Count);
            Assert.AreSame(field.GameObjects.Find(obj => { return (obj is Mine); }), mine);
        }

        [TestMethod]
        public void TestRemoveGameObject()
        {
            Field field = new Field(new Point(10, 15), 1);
            Mine mine = new Mine(field, new Point(1, 1));

            field.AddGameObject(mine);
            
            Assert.AreEqual(1, field.GameObjects.Count);
            Assert.AreSame(field.GameObjects.Find(obj => { return (obj is Mine); }), mine);
            
            field.RemoveGameObject(mine);

            Assert.AreEqual(0, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestClean()
        {
            Field field = new Field(new Point(10, 15), 1);
            Mine mine = new Mine(field, new Point(1, 1));

            field.AddGameObject(mine);
            field.Clean();

            Assert.AreEqual(0, field.GameObjects.Count);
        }

    }

     //#region Properties

     //   public Point Size { get; private set; }
     //   public Score GameScore { get; private set; }
     //   public List<GameObject> GameObjects { get; private set; }
 
     //   #endregion

     //   #region Constructors

     //   public Field(Point size)
     //   {
     //       Size = size;
     //       GameScore = new Score();
     //       GameObjects = new List<GameObject>();
     //   } 

     //   #endregion

     //   #region Methods

     //   public void AddGameObject(GameObject gameObject)
     //   {
     //       GameObjects.Add(gameObject);
     //   }

     //   public void RemoveGameObject(GameObject gameObject)
     //   {
     //       if (gameObject != null)
     //       {
     //           GameObjects.Remove(gameObject);
     //       }
     //   } 

     //   #endregion
}
