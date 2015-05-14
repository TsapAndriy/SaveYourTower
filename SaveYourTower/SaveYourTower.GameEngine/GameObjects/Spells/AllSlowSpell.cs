using System.Configuration;
using System.Timers;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.GameObjects.Spells.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.Spells
{
    public class AllSlowSpell : GameObject, ISpell
    {

        #region Properties

        public bool IsUsed { get; private set; }
        public int ReloadingTime { get; private set; } 

        #endregion

        #region Constructors

        public AllSlowSpell(
           Field gameField,
           int cost = int.MaxValue)
            : base(
                gameField,
                new Point((gameField.Size.X / 2), (gameField.Size.Y / 2)),
                colliderRaius: 0,
                cost: cost)
        {
            ReloadingTime = GameField.CurrenGameLevel.AllSlowSpellDuration; ;
        } 

        #endregion

        #region Methods

        public void Cast()
        {
            double divisor = GameField.CurrenGameLevel.AllSlowSpellRatio;
            VelositiDivisor += divisor;
            IsUsed = true;
            Timer timer = new Timer(ReloadingTime);
            timer.AutoReset = false;
            timer.Elapsed += FinishEffect;
            timer.Enabled = true;
            timer.Start();
        }

        private void FinishEffect(object source, ElapsedEventArgs e)
        {
            double divisor = GameField.CurrenGameLevel.AllSlowSpellRatio;
            VelositiDivisor -= divisor;
            IsAlive = false;
        }

        #endregion
    }
}
