using System;
using System.Collections.Generic;
using System.Threading;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;



namespace SaveYourTower.GameEngine
{
    public class Game
    {
        #region Fields

        public bool IsPaused { get; set; }
        public bool IsExit { get; set; }

        //public InstantineDelegate Instantine { get; set; }

        public event Action<Game> Input;
        public event Action<Field> Output;

        public Field GameField { get; set; }
        public EnemiesGenerator GameEmeniesGenerator { get; private set; }
 

        #endregion

        #region Constructors

        public Game()
        {
            IsPaused = false;
            //Instantine = InstantineGameObject;
            GameField = new Field(new Point(25, 50));
            GameField.GameScore.Clean();
            GameEmeniesGenerator = new EnemiesGenerator();
        }

        #endregion

        #region Methods

        public void Start()
        {          
            IsExit = false;
            GameField.AddGameObject(new Tower(GameField, new Point((GameField.Size.X / 2), (GameField.Size.Y / 2)), new UnitVector2(0, 0), 1, 100));

            while (!IsExit)
            {
                Update();
                Thread.Sleep(100);
            }

            GameField.GameObjects.Clear();
        }

        public void Update()
        {

            if (Input != null)
            {
                Input(this);
            }

            if (!IsPaused)
            {
                // Remove dead game objects.
                GameField.GameObjects.RemoveAll(obj => { return !obj.IsAlive; });

                Tower tower = GameField.GameObjects.Find((o) => { return o is Tower; }) as Tower;

                GameField.GameObjects.ForEach(o => o.Live());
                // Turrets fire.
                List<GameObject> turrets = GameField.GameObjects.FindAll((obj) => { return (obj is Turret); });
                foreach (Turret turret in turrets)
                {
                    turret.Fire();
                }

                if (tower.LifePoints <= 0)
                {
                    this.IsExit = true;
                    return;
                }

                GameEmeniesGenerator.Ganerate(GameField);
                var enemies = GameField.GameObjects.FindAll(obj => { return (obj is Enemy); });

                // Set direction of enemies moving to the tower.
                enemies.ForEach(obj => obj.LookAt(tower.Position));

                GameField.GameObjects.ForEach(obj => obj.MoveOnVelosity());

                RemoveOutOfFieldObjects();

                CollisionDetector.FindCollisions(GameField);
            }

            if (Output != null)
            {
                Output(GameField);
            }
        }

        void RemoveOutOfFieldObjects()
        {
            GameField.GameObjects.RemoveAll(OutOfFieldRange);
        }

        bool OutOfFieldRange(GameObject gameObject)
        {
            return (gameObject.Position.X <= 0)
                || (gameObject.Position.Y <= 0)
                || (gameObject.Position.X >= GameField.Size.X)
                || (gameObject.Position.Y >= GameField.Size.Y);
        }

        public void Instantine(GameObject gameObject)
        {
            GameField.AddGameObject(gameObject);
        }

        public void KillGameObject(GameObject gameObject)
        {
            GameField.RemoveGameObject(gameObject);
        }

        public void SaleGameObject(GameObject gameObject)
        { 
            if ((gameObject is Turret) || (gameObject is Mine))
            {
                GameField.GameScore.AddPoint(gameObject.Cost);
                KillGameObject(gameObject);
            }
        }

        public void Fire()
        {
            Tower tower = GameField.GameObjects.Find(o => { return o is Tower; }) as Tower;
            tower.Fire();
        }

        public void Rotate(double angle)
        {
            Tower tower = GameField.GameObjects.Find(o => { return o is Tower; }) as Tower;
            tower.Rotate(angle);
        }

        //public bool BuyGameObject(GameObject)
        //{

        //    return false;
        //}



        //public bool 
        public string BuyGameObject(GameObject gameObject)
        {
            if (gameObject.GameField.GameObjects.Find(obj => { return obj.Position.Equals(gameObject.Position); }) != null)
            {
                return "Can`t place object, the place is busy.";
            }
            else if (gameObject.Cost > GameField.GameScore.Value)
            {
                return "You need more points.";
            }
            else
            {
                GameField.GameScore.SpendPoints(gameObject.Cost);
                GameField.AddGameObject(gameObject);
            }
           
            return "";
        }

        public int GetScore()
        {
            return GameField.GameScore.Value;
        }

        #endregion
    }
}
