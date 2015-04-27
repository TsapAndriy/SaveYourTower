using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;

namespace SaveYourTower.GameEngine.Test.GameLogic
{
    [TestClass]
    public class EnemiesGeneratorTest
    {
        [TestMethod]
        public void TestGenerateLevel1()
        {
            Field field = new Field(new Point(10, 10), 1);

            EnemiesGenerator enemiesGenerator = new EnemiesGenerator();

            for (int i = 0; !enemiesGenerator.EnemiesAreEnded; i++)
            {
                enemiesGenerator.Generate(field);
            }

            var enemiesCout = int.Parse(ConfigurationManager.AppSettings["Level1EnemyCount"]);
            Assert.AreEqual(enemiesCout, field.GameObjects.Count);
        }


        [TestMethod]
        public void TestGenerateLevel2()
        {
            Field field = new Field(new Point(10, 10), 2);

            EnemiesGenerator enemiesGenerator = new EnemiesGenerator();

            for (int i = 0; !enemiesGenerator.EnemiesAreEnded; i++)
            {
                enemiesGenerator.Generate(field);
            }

            var enemiesCout = int.Parse(ConfigurationManager.AppSettings["Level2EnemyCount"]);
            Assert.AreEqual(enemiesCout, field.GameObjects.Count);
        }

        [TestMethod]
        public void TestGenerateLevel3()
        {
            Field field = new Field(new Point(10, 10), 3);

            EnemiesGenerator enemiesGenerator = new EnemiesGenerator();

            for (int i = 0; !enemiesGenerator.EnemiesAreEnded; i++)
            {
                enemiesGenerator.Generate(field);
            }

            var enemiesCout = int.Parse(ConfigurationManager.AppSettings["Level3EnemyCount"]);
            Assert.AreEqual(enemiesCout, field.GameObjects.Count);
        }
    } 
}
