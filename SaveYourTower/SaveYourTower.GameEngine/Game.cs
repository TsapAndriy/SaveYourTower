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
    public delegate void InstantineDelegate(GameObject gameObject);

    public class Game
    {
        #region Fields

        InstantineDelegate Instantine;

        Field GameField { get; set; }

        public event Action<Game> Input;
        public event Action<Field> Output;
        public bool IsExit { get; set; }
        #endregion
        


        #region Constructors

        public Game()
        {
            Instantine = InstantineGameObject;
            GameField = new Field(new Point(50, 50));
            GameField.GameScore.Clean();
        }

        #endregion



        #region Methods

        public void Start()
        {          
            IsExit = false;

            GameField.AddGameObject(new Tower(new Point((GameField.Size.X / 2), (GameField.Size.Y / 2)), new UnitVector2(10, 10), 1, 0) { Instantine = Instantine });
            GameField.AddGameObject(new Enemy(new Point(1, 1), new UnitVector2(1, 0), 1, 2));

            while (!IsExit)
            {
                Update();
                Thread.Sleep(100);
            }

            GameField.GameObjects.Clear();
        }

        public void Update()
        {
            Tower tower = GameField.GameObjects.Find((o) => { return o is Tower; }) as Tower;

            if (tower.LifePoints <= 0)
            {
                this.IsExit = true;
                return;
            }

            // send -> GameLogic
            if (DateTime.Now.Millisecond % 500 < 100)
                GameField.AddGameObject(new Enemy(new Point(1, 1), new UnitVector2(1, 0), 1, 2));

            if (Input != null)
            {
                Input(this);
            }

            var enemies = GameField.GameObjects.FindAll(obj => { return (obj is Enemy); });
            // Set direction of enemies moving to the tower.
            enemies.ForEach(obj => obj.LookAt(tower.Position));

            GameField.GameObjects.ForEach(obj => obj.MoveOnVelosity());
            
            RemoveOutOfFieldObjects();

            CollisionDetector.FindCollision(GameField);

            if (Output != null)
            {
                Output(GameField);
            }
        }

        void RemoveOutOfFieldObjects()
        {
            GameField.GameObjects.RemoveAll(OutOfRange);
        }

        bool OutOfRange(GameObject gameObject)
        {
            return (gameObject.Position.X <= 0)
                || (gameObject.Position.Y <= 0)
                || (gameObject.Position.X >= GameField.Size.X)
                || (gameObject.Position.Y >= GameField.Size.Y);
        }

        void InstantineGameObject(GameObject gameObject)
        {
            GameField.AddGameObject(gameObject);
        }

        void KillGameObject(GameObject gameObject)
        {
            GameField.RemoveGameObject(gameObject);
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

        public int GetScore()
        {
            return GameField.GameScore.Value;
        }

        #endregion
    }
}
