using SaveYourTower.GameEngine.DataContainers;

namespace SaveYourTower.GameEngine.GameObjects.Interfaces
{
    interface ICollisional
    {
        void OnCollision(object sender, CollisionEventArgs e);
    }
}
