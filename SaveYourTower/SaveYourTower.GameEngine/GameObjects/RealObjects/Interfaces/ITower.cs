using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.RealObjects.Interfaces
{
    interface ITower : ICollisional, ILive
    {
        void Fire();
    }
}
