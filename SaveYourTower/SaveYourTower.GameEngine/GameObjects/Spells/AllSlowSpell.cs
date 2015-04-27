using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.Spells.Interfaces;

using System.Windows;

namespace SaveYourTower.GameEngine.GameObjects.Spells
{
    public class AllSlowSpell : GameObject, ISpell, ILive
    {

        #region Properties

        public bool IsUsed { get; private set; }
        public int ReloadingTime { get; private set; } 

        #endregion

        #region Constructors

        public AllSlowSpell(
           Field gameField,
           int reloadingTime,
           int cost = int.MaxValue)
            : base(
                gameField,
                new Point((gameField.Size.X / 2), (gameField.Size.Y / 2)),
                colliderRaius: 0,
                cost: cost)
        {
            this.ReloadingTime = reloadingTime;
        } 

        #endregion

        #region Methods

        public void Cast()
        {
            double divisor = double.Parse(ConfigurationManager.AppSettings["AllSlowSpellRatio"]);
            GameObject.VelositiDivisor += divisor;
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
                    double divisor = double.Parse(ConfigurationManager.AppSettings["AllSlowSpellRatio"]);
                    GameObject.VelositiDivisor -= divisor;
                    this.IsAlive = false;
                }
            }
        } 

        #endregion
    }
}
