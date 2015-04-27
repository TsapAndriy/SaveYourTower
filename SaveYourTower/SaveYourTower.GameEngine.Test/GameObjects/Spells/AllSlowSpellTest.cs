using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Field gameField = new Field(new Point(10, 10), 1);
            AllSlowSpell spell = new AllSlowSpell(gameField, 5, 1);

            Assert.IsNotNull(spell);
            Assert.AreEqual(5, spell.ReloadingTime);
            Assert.IsFalse(spell.IsUsed);
        }

        [TestMethod]
        public void TestCast()
        {
            Field gameField = new Field(new Point(10, 10), 1);
            AllSlowSpell spell = new AllSlowSpell(gameField, 5, 1);

            double oldSlowRatio = GameObject.VelositiDivisor;

            spell.Cast();

            double slowRatio = double.Parse(ConfigurationManager.AppSettings["AllSlowSpellRatio"]);

            Assert.IsTrue(spell.IsUsed);
            Assert.AreEqual((slowRatio + oldSlowRatio), GameObject.VelositiDivisor);
        }

        [TestMethod]
        public void Live()
        {
            Field gameField = new Field(new Point(10, 10), 1);
            AllSlowSpell spell = new AllSlowSpell(gameField, 1, 1);

            spell.Live();
            Assert.IsTrue(spell.IsAlive);
            Assert.IsFalse(spell.IsUsed);
            Assert.AreEqual(1, spell.ReloadingTime);

            spell.Cast();

            double oldSlowRatio = GameObject.VelositiDivisor;
            double slowRatio = double.Parse(ConfigurationManager.AppSettings["AllSlowSpellRatio"]);

            spell.Live();
            Assert.IsTrue(spell.IsAlive);
            Assert.IsTrue(spell.IsUsed);
            Assert.AreEqual(0, spell.ReloadingTime);

            spell.Live();
            Assert.IsFalse(spell.IsAlive);
            Assert.IsTrue(spell.IsUsed);

            Assert.AreEqual(oldSlowRatio - slowRatio, GameObject.VelositiDivisor);
        }

    }
}
