using System;
using System.Collections.Generic;
//using System.Threading;
using System.Timers;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;



namespace SaveYourTower.GameEngine
{
    public enum Status
    {
        IsReadyToRun,
        IsRuning,
        IsPaused,
        IsExit
    }

    public enum BuingStatus
    {
        PlaceIsBusy,
        NeedMorePoints,
        Success
    }

    public class Game
    {

        #region Fileds

        private CollisionDetector _collisionDetector;
        private Timer _gameTimer;
        private System.Threading.ManualResetEvent _exitEvent;

        #endregion

        #region Properties
        public event Action<Game> Input;
        public event Action<Field> Output;

        public Status GameStatus { get; private set; }
        public EnemiesGenerator GameEmeniesGenerator { get; private set; }
        public Field GameField { get; set; }

        #endregion

        #region Constructors

        public Game(Point filedSize)
        {
            _exitEvent = new System.Threading.ManualResetEvent(false);
            GameStatus = Status.IsReadyToRun;
            GameField = new Field(filedSize);
            _collisionDetector = new CollisionDetector();
            _gameTimer = new Timer(100);
            _gameTimer.Elapsed += Update;

            GameEmeniesGenerator = new EnemiesGenerator();
            GameField.AddGameObject(new Tower(GameField, new Point((GameField.Size.X / 2), (GameField.Size.Y / 2)), new UnitVector2(0, 0), 1, 100));
        }

        #endregion

        #region Methods

        public void Start()
        {
            #region Validation

            if (GameStatus != Status.IsReadyToRun)
            {
                throw new InvalidOperationException("Only game with status 'IsReadyToRu' can be started");
            }

            #endregion

            GameStatus = Status.IsRuning;

            _gameTimer.Start();

            _exitEvent.WaitOne();
        }

        public void Update(object source, ElapsedEventArgs e)
        {
            if (Input != null)
            {
                Input(this);
            }

            if (GameStatus != Status.IsPaused)
            {
                // Remove dead game objects.
                GameField.GameObjects.RemoveAll(obj => { return !obj.IsAlive; });

                Tower tower = GameField.GameObjects.Find((obj) => { return obj is Tower; }) as Tower;

                // Do life cikle step
                GameField.GameObjects.ForEach(obj => obj.Live());

                // Turrets fire.
                List<GameObject> turrets = GameField.GameObjects.FindAll((obj) => { return (obj is Turret); });
                foreach (Turret turret in turrets)
                {
                    turret.Fire();
                }

                // Check tower status
                if (tower.LifePoints <= 0)
                {
                    GameStatus = Status.IsExit;
                    return;
                }

                // Generate enemies
                if (DateTime.Now.Millisecond % 500 < 100)
                {
                    GameEmeniesGenerator.Generate(GameField);
                }

                var enemies = GameField.GameObjects.FindAll(obj => { return (obj is Enemy); });

                // Set enemies direction to the tower.
                enemies.ForEach(obj => obj.LookAt(tower.Position));

                GameField.GameObjects.ForEach(obj => obj.MoveOnVelosity());

                RemoveOutOfFieldObjects();

                _collisionDetector.FindCollisions(GameField);
            }

            if (Output != null)
            {
                Output(GameField);
            }
        }

        public void Pause()
        {
            #region Validation

            if (GameStatus != Status.IsRuning)
            {
                throw new InvalidOperationException("Only game with status 'IsRunning' can be paused");
            }

            #endregion

            GameStatus = Status.IsPaused;
        }

        public void Restore()
        {
            #region Validation

            if (GameStatus != Status.IsPaused)
            {
                throw new InvalidOperationException("Only game with status 'IsPaused' can be restored");
            }

            #endregion

            GameStatus = Status.IsRuning;
        }

        public void Stop()
        {
            #region Validation

            if ((GameStatus != Status.IsRuning) && (GameStatus != Status.IsPaused))
            {
                throw new InvalidOperationException("Only game with status 'IsRuning' or 'IsPaused' can be stoped");
            }
            #endregion

            GameStatus = Status.IsExit;
            _exitEvent.Set();
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

        public void RemoveOutOfFieldObjects()
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

        public void SaleGameObject(GameObject gameObject)
        { 
            if ((gameObject is Turret) || (gameObject is Mine))
            {
                GameField.GameScore.AddPoint(gameObject.Cost);
                GameField.RemoveGameObject(gameObject);
            }
        }

        public BuingStatus BuyGameObject(GameObject gameObject)
        {
            if (IsObejectOnTheField(gameObject))
            {
                throw new InvalidOperationException("Object already exist.");// "Object already exist";
            }
            else if (gameObject.GameField.GameObjects.Find(obj => { return obj.Position.Equals(gameObject.Position); }) != null)
            {
                return BuingStatus.PlaceIsBusy;// "Can`t place object, the place is busy.";
            }
            else if (gameObject.Cost > GameField.GameScore.Value)
            {
                return BuingStatus.NeedMorePoints;// "You need more points.";
            }
            else
            {
                GameField.GameScore.SpendPoints(gameObject.Cost);
                GameField.AddGameObject(gameObject);
                return BuingStatus.Success;
            }
        }

        bool IsObejectOnTheField(GameObject gameObject)
        {
            return gameObject.GameField.GameObjects.Exists(
                obj => { return ReferenceEquals(obj, gameObject); }
                );
        }

        public int GetScore()
        {
            return GameField.GameScore.Value;
        }

        #endregion
    }
}
