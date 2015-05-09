using System.Configuration;
using System.Timers;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.GameObjects.Spells.Interfaces;

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
            ReloadingTime = reloadingTime;
        } 

        #endregion

        #region Methods

        public void Cast()
        {
            double divisor = double.Parse(ConfigurationManager.AppSettings["AllSlowSpellRatio"]);
            VelositiDivisor += divisor;
            IsUsed = true;
            int interval = int.Parse(ConfigurationManager.AppSettings["AllSlowSpellDuration"]);
            Timer timer = new Timer(interval);
            timer.AutoReset = false;
            timer.Elapsed += FinishEffect;
            timer.Enabled = true;
            timer.Start();
        }

        public void FinishEffect(object source, ElapsedEventArgs e)
        {
            double divisor = double.Parse(ConfigurationManager.AppSettings["AllSlowSpellRatio"]);
            VelositiDivisor -= divisor;
            IsAlive = false;
        }

        public void Live()
        {
            //if ((this.IsAlive) && (this.IsUsed))
            //{
            //    if (ReloadingTime > 0)
            //    {
            //        //ReloadingTime--;
            //    }
            //    else
            //    {
            //        //double divisor = double.Parse(ConfigurationManager.AppSettings["AllSlowSpellRatio"]);
            //        //GameObject.VelositiDivisor -= divisor;
            //        //this.IsAlive = false;
            //    }
            //}
        } 

        #endregion
    }
}
