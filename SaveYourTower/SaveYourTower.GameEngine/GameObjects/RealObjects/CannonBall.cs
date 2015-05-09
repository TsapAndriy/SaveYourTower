using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.RealObjects.Interfaces;

using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.GameObjects.RealObjects
{
    public class CannonBall : GameObject, ICannonBall
    {
        #region Fields

        private int _timeToLive;
 
        #endregion

        #region Constructors

        public CannonBall(
           Field gameField,
           Point position,
           UnitVector2 direction,
           int colliderRaius,
           double velosity,
           int damage,
           int timeToLive)
            : base(
                gameField,
                position,
                direction,
                colliderRaius,
                velosity,
                damage : damage,
                lifePoints: 1)
        {
            _timeToLive = timeToLive;

            Collider bodyCollider = new Collider(position, colliderRaius, "BodyCollider");
            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);
        } 

        #endregion

        #region Methods

        public void OnCollision(object sender, CollisionEventArgs e)
        {
            GameObject gameObject = sender as GameObject;

            if ((gameObject is Enemy) && (e.OtherCollider.Tag == "BodyCollider") && (e.MyCollider.Tag == "BodyCollider"))
            {
                IsAlive = false;
            }
        }

        public void Live()
        {
            _timeToLive--;
            if (_timeToLive <= 0)
            {
                IsAlive = false;
            }
            MoveOnVelosity();
        } 

        #endregion
    }
}
