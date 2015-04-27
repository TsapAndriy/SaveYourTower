using System;
using System.Configuration;
using System.Collections.Generic;
//using System.Threading;
using System.Timers;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.Spells;
using SaveYourTower.GameEngine.Spells.Interfaces;


namespace SaveYourTower.GameEngine
{
    public enum Status
    {
        IsReadyToRun,
        IsReadyToStart,
        IsStarted,
        IsPaused,
        IsWinnedLevel,
        IsWinned,
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
        private int _maxLevel;
        private int _gameIterationLatency;

        #endregion

        #region Properties

        public event Action<Game> Input;
        public event Action<Game> Output;
        public event Action WinLevel;

        public Status GameStatus { get; private set; }
        public EnemiesGenerator GameEmeniesGenerator { get; private set; }
        public Field GameField { get; set; }

        #endregion

        #region Constructors

        public Game(Point filedSize, int gameLevel)
        {
            GameStatus = Status.IsReadyToRun;
            GameField = new Field(filedSize, gameLevel);

            _exitEvent = new System.Threading.ManualResetEvent(false);
            _collisionDetector = new CollisionDetector();
            _maxLevel = int.Parse(ConfigurationManager.AppSettings["MaxLevel"]);

            int gameIteraionLatency = int.Parse(ConfigurationManager.AppSettings["IterationLatency"]);
            _gameTimer = new Timer(gameIteraionLatency);
            _gameTimer.Elapsed += new ElapsedEventHandler(Update);

            GameEmeniesGenerator = new EnemiesGenerator();

            GameField.AddGameObject(
                new Tower(GameField, 
                    new Point((GameField.Size.X / 2), 
                    (GameField.Size.Y / 2)), 
                    new UnitVector2(0, 0), 
                    1,
                    int.Parse(ConfigurationManager.AppSettings["TowerLife"])));
        }

        #endregion

        #region Methods

        #region GameControllMethods

        public void Run()
        {
            #region Validation

            if (GameStatus != Status.IsReadyToRun)
            {
                throw new InvalidOperationException("Only game with status 'IsReadyToRu' can be started");
            }

            #endregion

            GameStatus = Status.IsReadyToStart;
            _gameTimer.Start();
            _exitEvent.WaitOne();
        }

        public void Start()
        {
            #region Validation

            if (GameStatus != Status.IsReadyToStart)
            {
                throw new InvalidOperationException("Only game with status 'IsReadyToRu' can be started");
            }

            #endregion

            GameStatus = Status.IsStarted;
        }

        public void Update(object source, ElapsedEventArgs e)
        {
            if (GameStatus == Status.IsStarted)
            {
                CheckLevelWin();
            }
            
            if (Output != null)
            {
                Output(this);
            }
            
            if (Input != null)
            {
                Input(this);
            }

            if (GameStatus == Status.IsStarted)
            {
                // Remove dead game objects.
                GameField.GameObjects.RemoveAll(obj => (!obj.IsAlive));

                // Do life cikle step.
                List<GameObject> liveList = GameField.GameObjects.FindAll(obj => obj is ILive);
                foreach (ILive liveObject in liveList)
                {
                    liveObject.Live();
                }

                CheckTowerLose();
                GameEmeniesGenerator.Generate(GameField);
                RemoveOutOfFieldObjects();
                _collisionDetector.FindCollisions(GameField);
            }
        }

        public void Pause()
        {
            #region Validation

            if (GameStatus != Status.IsStarted)
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

            GameStatus = Status.IsStarted;
        }

        public void Stop()
        {
            #region Validation

            if ((GameStatus != Status.IsStarted) 
                && (GameStatus != Status.IsPaused)
                && (GameStatus != Status.IsWinned)
                && (GameStatus != Status.IsWinnedLevel))
            {
                throw new InvalidOperationException("Only game with status 'IsRuning' or 'IsPaused' can be stoped");
            }

            #endregion

            GameStatus = Status.IsExit;
            _gameTimer.Stop();
            _exitEvent.Set();
        }

        #endregion

        #region TowerControllMethos

        public void Fire()
        {
            Tower tower = (Tower)GameField.GameObjects.Find(obj => obj is Tower);
            tower.Fire();
        }

        public void Rotate(double angle)
        {
            Tower tower = (Tower)GameField.GameObjects.Find(obj => obj is Tower);
            tower.Rotate(angle);
        }

        #endregion

        #region OperationsWithGameobjects

        public int GetScore()
        {
            return GameField.GameScore.Value;
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
            else if (IsPlaceBusy(gameObject) && !(gameObject is ISpell))
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

        private void RemoveOutOfFieldObjects()
        {
            GameField.GameObjects.RemoveAll(OutOfFieldRange);
        }

        private bool OutOfFieldRange(GameObject gameObject)
        {
            return (gameObject.Position.X <= 0)
                || (gameObject.Position.Y <= 0)
                || (gameObject.Position.X >= GameField.Size.X)
                || (gameObject.Position.Y >= GameField.Size.Y);
        }

        private bool IsPlaceBusy(GameObject gameObject)
        {
            return (gameObject.GameField.GameObjects.Find(obj =>
            {
                return obj.Position.Equals(gameObject.Position);
            }) != null);
        }

        private bool IsObejectOnTheField(GameObject gameObject)
        {
            return gameObject.GameField.GameObjects.Exists(obj =>
            {
                return ReferenceEquals(obj, gameObject);
            });
        }

        private void CheckTowerLose()
        {
            Tower tower = (Tower)GameField.GameObjects.Find(obj => obj is Tower);
            if (tower == null)
            {
                GameStatus = Status.IsExit;
                _exitEvent.Set();
                _gameTimer.Stop();
            }
        }
        

        #endregion

        #region LevelContorlMethods

        public void NextLevel()
        {
            #region Validation

            if (GameStatus != Status.IsWinnedLevel)
            {
                throw new InvalidOperationException("Only game with status 'IsWinned' can load next level");
            }

            #endregion

            if (GameField.CurrenGameLevel == _maxLevel)
            {
                GameStatus = Status.IsWinned;
                return;
            }

            GameStatus = Status.IsReadyToStart;
            PrepareFieldToNextLevel(GameField);
            GameEmeniesGenerator = new EnemiesGenerator();

            Start();
        }

        private void PrepareFieldToNextLevel(Field gameField)
        {
            // Remove all except enemies and turrets.
            gameField.GameObjects.RemoveAll(obj =>
            {
                return !((obj is Enemy) || (obj is Turret));
            });

            GameField.AddGameObject(new Tower(
                GameField,
                new Point((GameField.Size.X / 2), (GameField.Size.Y / 2)),
                new UnitVector2(0, 0),
                1,
                int.Parse(ConfigurationManager.AppSettings["TowerLife"]))
                );

            GameField.CurrenGameLevel++;
        }

        private void CheckLevelWin()
        {
            bool enemiesNotExist = !this.GameField.GameObjects.Exists(obj => obj is Enemy);
            bool towerLive = this.GameField.GameObjects.Exists(obj => obj is Tower);

            if (towerLive && enemiesNotExist && GameEmeniesGenerator.EnemiesAreEnded)
            {
                GameStatus = Status.IsWinnedLevel;

                if (WinLevel != null)
                {
                    WinLevel();
                }
            }
        }

        #endregion

        #endregion
    }
}
