using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.Spells.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.Spells
{
    public class AllHilSpell : GameObject, ISpell, ILive
    {
        #region Properties

        public bool IsUsed { get; private set; }
        public int ReloadingTime { get; private set; } 

        #endregion

        #region Constructors

        public AllHilSpell(
           Field gameField,
           int reloadingTime = 5,
           int cost = int.MaxValue)
            : base(
                gameField,
                new Point((gameField.Size.X / 2), (gameField.Size.Y / 2)),
                colliderRaius: 0,
                cost: cost)
        {
            this.IsUsed = false;
            this.ReloadingTime = reloadingTime;
        } 

        #endregion

        #region Methods

        public void Cast()
        {
            GameField.GameObjects.ForEach(gameObject =>
            {
                if (gameObject is Enemy)
                {
                    int damage = int.Parse(ConfigurationManager.AppSettings["AllHitSpellDamage"]);
                    gameObject.ReceiveDamage(damage);
                }
            });

            this.IsUsed = true;
        }

        public void Live()
        {
            if ((this.IsAlive) && (this.IsUsed))
            {
                if (ReloadingTime > 0)
                {
                    ReloadingTime--;
                }
                else
                {
                    this.IsAlive = false;
                }
            }
        } 

        #endregion
    }
}
