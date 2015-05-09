using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.RealObjects.Interfaces
{
    interface IEnemy : ICollisional, ILive
    {
        Tower LookingTower { get; }
    }
}
