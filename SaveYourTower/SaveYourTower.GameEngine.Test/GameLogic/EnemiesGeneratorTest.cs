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
            Level level = new Level(enemyGenerationLanency: 0, enemyCount: 1000);
            Field field = new Field(new Point(10, 10), level);

            EnemiesGenerator enemiesGenerator = new EnemiesGenerator(level);

            for (int i = 0; !enemiesGenerator.EnemiesAreEnded; i++)
            {
                enemiesGenerator.Generate(field);
            }

            Assert.AreEqual(1000, field.GameObjects.Count);
        }
    }
}
