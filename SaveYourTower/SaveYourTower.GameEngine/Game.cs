using System;
using System.Timers;

using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameLogic;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using SaveYourTower.GameEngine.GameObjects.Spells.Interfaces;


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

        #endregion

        #region Events

        public event EventHandler DieEventHandler;
        public event EventHandler InputEventHandler;
        public event EventHandler OutputEventHandler;
        public event EventHandler WinLevelEventHandler;

        #endregion

        #region Properties

        public Level[] Levels { get; private set; }
        public Status GameStatus { get; private set; }
        public EnemiesGenerator GameEmeniesGenerator { get; private set; }
        public Field GameField { get; private set; }

        #endregion

        #region Constructors

        public Game(Point filedSize, Level[] levels)
        {
            Levels = levels; 
            GameStatus = Status.IsReadyToRun;
            GameField = new Field(filedSize, Levels[0]);
            
            _exitEvent = new System.Threading.ManualResetEvent(false);
            _collisionDetector = new CollisionDetector();

            _maxLevel = levels[0].MaxLevel;

            _gameTimer = new Timer(levels[0].IterationLatency);

            _gameTimer.Elapsed += Update;

            GameEmeniesGenerator = new EnemiesGenerator(GameField.CurrenGameLevel);

            GameField.AddGameObject(
                new Tower(GameField, 
                    new Point((GameField.Size.X / 2), 
                    (GameField.Size.Y / 2)), 
                    new UnitVector2(0, 0), 
                    GameField.CurrenGameLevel.TowerColliderRadius,
                    levels[0].TowerLife));
        }

        #endregion

        #region Methods

        #region GameControllMethods

        public void Run()
        {
            #region Validation

            if (GameStatus != Status.IsReadyToRun)
            {
                throw new InvalidOperationException(
                    "Only game with status 'IsReadyToRu' can be started");
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
            _gameTimer.Enabled = false;
            if (GameStatus == Status.IsStarted)
            {
                CheckLevelWin();
            }
            
            if (InputEventHandler != null)
            {
                InputEventHandler(this, new EventArgs());
            }

            if (GameStatus == Status.IsStarted)
            {
                RaiseDieEvent();

                GameField.GameObjects.RemoveAll(obj => (!obj.IsAlive));

                foreach (var gameObject in GameField.GameObjects.ToArray())
                {
                    ILive alive = gameObject as ILive;

                    if (alive != null)
                    {
                        alive.Live();
                    }
                }

                CheckTowerLose();
                GameEmeniesGenerator.Generate(GameField);
                RemoveOutOfFieldObjects();
                _collisionDetector.FindCollisions(GameField);
            }

            if (OutputEventHandler != null)
            {
                OutputEventHandler(this, new EventArgs());
            }

            if (GameStatus != Status.IsExit)
            {
                _gameTimer.Enabled = true;
            }
        }

        private void RaiseDieEvent()
        {
            if (DieEventHandler != null)
            {
                GameField.GameObjects.ForEach(obj =>
                {
                    if (!obj.IsAlive)
                    {
                        DieEventHandler(obj, null);
                    }
                });
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
                throw new InvalidOperationException(
                    "Only game with status 'IsPaused' can be restored");
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
                throw new InvalidOperationException(
                    "Only game with status 'IsRuning' or 'IsPaused' can be stoped");
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

        public BuingStatus BuyGameObject(GameObject gameObject)
        {
            if (IsObejectOnTheField(gameObject))
            {
                throw new InvalidOperationException("Object already exist.");
            }
            else if (IsPlaceBusy(gameObject) && !(gameObject is ISpell))
            {
                return BuingStatus.PlaceIsBusy;
            }
            else if (gameObject.Cost > GameField.GameScore.Value)
            {
                return BuingStatus.NeedMorePoints;
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
            bool busy = true;
            if (gameObject.Colliders != null)
            {
                busy = (gameObject.GameField.GameObjects.Exists(obj => 
                    IsCollision(obj, gameObject)));
            }

            return busy;
        }

        private bool IsCollision(GameObject leftGameObject, GameObject rightGameObject)
        {
            bool collision = false;
            Collider leftCollider = leftGameObject.Colliders.Find(col => 
                col.Tag == "BodyCollider");

            Collider rightCollider = rightGameObject.Colliders.Find(col => 
                col.Tag == "BodyCollider");

            if ((leftCollider != null) && (rightCollider != null))
            {
                collision = (Distance(leftGameObject.Position, rightGameObject.Position) 
                    <= (leftCollider.Radius + rightCollider.Radius));
            }

            return collision;
        }

        public double Distance(Point left, Point right)
        {
            return Math.Sqrt(Math.Pow((left.X - right.X), 2)
                + Math.Pow((left.Y - right.Y), 2));
        }

        private bool IsObejectOnTheField(GameObject gameObject)
        {
            return gameObject.GameField.GameObjects.Exists(obj => 
                ReferenceEquals(obj, gameObject));
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
                throw new InvalidOperationException(
                    "Only game with status 'IsWinned' can load next level");
            }

            #endregion

            if (GameField.CurrenGameLevel.Number == _maxLevel)
            {
                GameStatus = Status.IsWinned;
                return;
            }

            GameStatus = Status.IsReadyToStart;
            PrepareFieldToNextLevel();
            GameEmeniesGenerator = new EnemiesGenerator(GameField.CurrenGameLevel);

            Start();
        }

        private void PrepareFieldToNextLevel()
        {
            // Remove all except enemies and turrets.
            GameField.GameObjects.RemoveAll(obj => 
                !((obj is Enemy) || (obj is Turret)));

            GameField.AddGameObject(new Tower(
                GameField,
                new Point((GameField.Size.X / 2), (GameField.Size.Y / 2)),
                new UnitVector2(0, 0),
                GameField.CurrenGameLevel.TowerColliderRadius,
                GameField.CurrenGameLevel.TowerLife));

            GameField.CurrenGameLevel = Levels[GameField.CurrenGameLevel.Number];
        }

        private void CheckLevelWin()
        {
            bool enemiesNotExist = !GameField.GameObjects.Exists(obj => obj is Enemy);
            bool towerLive = GameField.GameObjects.Exists(obj => obj is Tower);

            if (towerLive 
                && enemiesNotExist 
                && GameEmeniesGenerator.EnemiesAreEnded 
                && GameStatus != Status.IsWinned)
            {
                GameStatus = Status.IsWinnedLevel;

                if (WinLevelEventHandler != null)
                {
                    WinLevelEventHandler(this, new EventArgs());
                }
            }
        }

        #endregion

        #endregion
    }
}
