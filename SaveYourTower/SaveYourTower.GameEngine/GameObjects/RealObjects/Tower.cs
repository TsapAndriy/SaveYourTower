using System.Configuration;

using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects.RealObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.RealObjects
{
    public class Tower : GameObject, ITower
    {
        #region Fields

        private bool _isFiring;

        #endregion

        #region Constructors

        public Tower(
            Field gameField,
            Point position,
            UnitVector2 direction,
            int colliderRaius,
            int lifePoints)
            : base(
                gameField,
                position,
                direction,
                colliderRaius,
                0,
                lifePoints: lifePoints)
        {
            Collider bodyCollider = new Collider(position, colliderRaius, "BodyCollider");
            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);
        } 

        #endregion

        #region Methods

        public void Fire()
        {
            _isFiring = !_isFiring;
        }

        public void OnCollision(object sender, CollisionEventArgs e)
        {
            GameObject gameObject = sender as GameObject;
            if ((gameObject is Enemy) 
                && (e.OtherCollider.Tag == "BodyCollider") 
                && (e.MyCollider.Tag == "BodyCollider"))
            {
                 ReceiveDamage(gameObject.Damage);

                if (LifePoints <= 0)
                {
                    IsAlive = false;
                }
            }
        }

        public void Live()
        {
            if (_isFiring)
            {
                GameField.AddGameObject(new CannonBall(
                    GameField,
                    (Point) Position.Clone(),
                    new UnitVector2(Direction.Angle),
                    3,
                    10,
                    int.Parse(ConfigurationManager.AppSettings["TowerCannonDamage"]),
                    int.Parse(ConfigurationManager.AppSettings["TowerCannonBallLifeTime"])
                    ));
            }
            _isFiring = false;
        }
        
        #endregion
    }
}
