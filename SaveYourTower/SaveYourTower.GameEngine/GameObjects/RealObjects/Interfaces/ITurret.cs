using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.RealObjects.Interfaces
{
    interface ITurret : ICollisional, ILive
    {
        void Fire();
    }
}
