using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Spells;

namespace SaveYourTower.GameEngine.Test.GameObjects.Spells
{
    [TestClass]
    public class AllSlowSpellTest
    {
        [TestMethod]
        public void TestConsturtor()
        {
            Level level = new Level();
            Field gameField = new Field(new Point(10, 10), level);
            AllSlowSpell spell = new AllSlowSpell(gameField, 1);

            Assert.IsNotNull(spell);
            Assert.AreEqual(level.AllSlowSpellDuration, spell.ReloadingTime);
            Assert.IsFalse(spell.IsUsed);
        }

        [TestMethod]
        public void TestCast()
        {
            Field gameField = new Field(new Point(10, 10), new Level());
            AllSlowSpell spell = new AllSlowSpell(gameField, 1);

            double oldSlowRatio = gameField.VelositiDivisor;

            spell.Cast();

            double slowRatio = 1;

            Assert.IsTrue(spell.IsUsed);
            Assert.AreEqual((slowRatio + oldSlowRatio), gameField.VelositiDivisor);

            Thread.Sleep(20);

            Assert.IsFalse(spell.IsAlive);
            Assert.AreEqual(oldSlowRatio, gameField.VelositiDivisor);
        }
    }
}
