namespace SaveYourTower.GameEngine.GameObjects.Spells.Interfaces
{
    public interface ISpell
    {
        bool IsUsed { get; }
        Field GameField { get; }
        int ReloadingTime { get; }

        void Cast();
    }
}
