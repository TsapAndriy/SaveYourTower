using System.Configuration;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.GameObjects.Spells.Interfaces;

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
            IsUsed = false;
            ReloadingTime = reloadingTime;
        } 

        #endregion

        #region Methods

        public void Cast()
        {
            GameField.GameObjects.ForEach(gameObject =>
            {
                if (gameObject is Enemy)
                {
                    int damage = GameField.CurrenGameLevel.AllHitSpellDamage;
                    gameObject.ReceiveDamage(damage);
                }
            });

            IsUsed = true;
        }

        public void Live()
        {
            if ((IsAlive) && (IsUsed))
            {
                if (ReloadingTime > 0)
                {
                    ReloadingTime--;
                }
                else
                {
                    IsAlive = false;
                }
            }
        } 

        #endregion
    }
}
