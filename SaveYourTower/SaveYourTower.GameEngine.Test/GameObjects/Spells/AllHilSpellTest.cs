using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Spells;

namespace SaveYourTower.GameEngine.Test.GameObjects.Spells
{
    [TestClass]
    public class AllHilSpellTest
    {
        [TestMethod]
        public void TestConsturtor()
        {
            Field gameField = new Field(new Point(10, 10), null);
            AllHilSpell spell = new AllHilSpell(gameField, 5, 1);

            Assert.IsNotNull(spell);
            Assert.AreEqual(5, spell.ReloadingTime);
            Assert.IsFalse(spell.IsUsed);
        }

        [TestMethod]
        public void TestCast()
        {
            Level level = new Level();
            Field gameField = new Field(new Point(10, 10), level);
            AllHilSpell spell = new AllHilSpell(gameField, 5, 1);

            gameField.GameObjects.Add(new Enemy(null, null));

            spell.Cast();

            Enemy enemy = (Enemy)gameField.GameObjects.Find(obj => obj is Enemy);
            int damageResult = 1 - level.AllHitSpellDamage;


            Assert.IsTrue(spell.IsUsed);
            Assert.AreEqual(damageResult, enemy.LifePoints);
        }

        [TestMethod]
        public void Live()
        {
            Field gameField = new Field(new Point(10, 10), new Level());
            AllHilSpell spell = new AllHilSpell(gameField, 1, 1);

            spell.Cast();

            spell.Live();
            Assert.IsTrue(spell.IsAlive);
            Assert.AreEqual(0, spell.ReloadingTime);

            spell.Live();
            Assert.IsFalse(spell.IsAlive);
            Assert.AreEqual(0, spell.ReloadingTime);
        }

    }
}
