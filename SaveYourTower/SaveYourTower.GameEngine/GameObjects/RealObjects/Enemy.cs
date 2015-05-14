using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects.RealObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects.RealObjects
{
    public class Enemy : GameObject, IEnemy
    {
        #region Properties

        public Tower LookingTower { get; private set; }

        #endregion

        #region Constructors

        public Enemy(
           Field gameField,
           Point position,
           int colliderRaius = 1,
           double velosity = 1,
           int damage = 1,
           int lifePoints = 1)
            : base(
                gameField,
                position,
                colliderRaius: colliderRaius,
                damage : damage,
                velosity: velosity,
                lifePoints: lifePoints)
        {
            Collider bodyCollider = 
                new Collider(position, colliderRaius, "BodyCollider");

            bodyCollider.CollisionEventHandler += OnCollision;
            Colliders.Add(bodyCollider);
            if (gameField != null)
            {
                LookingTower = 
                    (Tower)GameField.GameObjects.Find(obj => obj is Tower);
            }
        } 

        #endregion

        #region Methods

        public void OnCollision(object sender, CollisionEventArgs e)
        {
            GameObject gameObject = sender as GameObject;
            
            if ((gameObject is CannonBall) 
                && (e.OtherCollider.Tag == "BodyCollider") 
                && (e.MyCollider.Tag == "BodyCollider"))
            {
                ReceiveDamage(gameObject.Damage);

                if ((IsAlive) && (LifePoints <= 0))
                {
                    IsAlive = false;
                    GameField.GameScore.AddPoint(1);
                }
            }
            else if ((gameObject is Tower) 
                && (e.OtherCollider.Tag == "BodyCollider") 
                && (e.MyCollider.Tag == "BodyCollider"))
            {
                IsAlive = false;
            }
        }

        public void Live()
        {
            if (LifePoints <= 0)
            {
                 IsAlive = false;
            }

            if (LookingTower != null)
            {
                LookAt(LookingTower.Position);
            }

            MoveOnVelosity();
        }

        #endregion
    }
}
