using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;

using SaveYourTower.DesktopUI.GamePages;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Spells;
using GamePoint = SaveYourTower.GameEngine.DataContainers.Point;
using SaveYourTower.DesktopUI.VisualEffects;
using SaveYourTower.GameEngine.GameObjects.Base;

namespace SaveYourTower.DesktopUI
{
    public partial class PlaingPage : UserControl, ILoadable
    {

        #region Constants

        private const int BackgroundStartCount = 200;
        private const int EnemyBossLookLife = 100;
        private const int StrongCannonballLookDamage = 10;
        private const int SpellCost = 10;
        private const int TimeOfSpellCostViewing = 2000;
        private const int SpellDuration = 100;
        private const int TurretFindingCollider = 150;
        private const int TurretLifePoints = 30;
        private const int TurretFireRateDivisor = 10; 

        #endregion

        #region Enums

        private enum CursorStatus
        {
            Fire,
            Turret,
            Mine
        }
        
        #endregion

        #region Events

        public event EventHandler<PageEventArgs> PageEventHandler; 

        #endregion

        #region Fields

        private static object _sync = new object();

        private List<object> _effects = new List<object>();
        private Cursor _defaultCursor;
        private CursorStatus _cursorStatus = CursorStatus.Fire;
        private Game _game;
        private Level[] _levels;
        private List<Rectangle> _stars = new List<Rectangle>();
        private bool _showSpellCost = false;

        #endregion

        #region Constructors

        public PlaingPage()
        {
            InitializeComponent();
            _defaultCursor = this.Cursor;

            this.Dock = DockStyle.Fill;
            btnTurret.Enabled = false;
            btnHitAll.Enabled = false;
            btnSlowAll.Enabled = false;
            this.Cursor = new Cursor((Properties.Resources.Mark).GetHicon());
            _levels = LoadLevels();
            bntPause.Parent = tblMainGameView;
        } 

        #endregion

        #region Methods
        private void RunGame()
        {
            _game = new Game(new GamePoint(this.Width, this.Height), _levels);
            _game.OutputEventHandler += OnOutputEventHandler;
            _game.InputEventHandler += OnInputEventHandler;
            _game.DieEventHandler += DieEffects;
            Task task = new Task(_game.Run);
            task.Start();
        }

        #region Game input/output functions

        public void OnInputEventHandler(object sender, EventArgs e)
        {
            Game game = sender as Game;
            if (game != null)
            {
                lock (_sync)
                {
                    if (_game.GameStatus == Status.IsReadyToStart)
                    {
                        game.Start();
                    }
                }
            }
        }

        public void OnOutputEventHandler(object sender, EventArgs e)
        {
            if (PageEventHandler != null)
            {
                if (_game.GameStatus == Status.IsStarted)
                {
                    lock (_sync)
                    {
                        Invoke((Action<Field>)Draw, _game.GameField);
                    }
                }
                else if (_game.GameStatus == Status.IsWonLevel)
                {
                    lock (_sync)
                    {
                        Invoke((Action<Field>)LevelWinOutput, _game.GameField);
                    }
                }
                else if (_game.GameStatus == Status.IsWon)
                {
                    lock (_sync)
                    {
                        _game.Stop();
                        BeginInvoke(PageEventHandler,
                            this,
                            new PageEventArgs(typeof(WinPage)));

                        Invoke((MethodInvoker)delegate { this.Dispose(); });
                    }
                }
                else if (_game.GameStatus == Status.IsExit)
                {
                    lock (_sync)
                    {
                        BeginInvoke(PageEventHandler,
                            this,
                            new PageEventArgs(typeof(LosePage)));

                        Invoke((MethodInvoker)delegate { this.Dispose(); });
                    }
                }
            }
        }

        #endregion

        #region GameObjectsDrawing

        private void Draw(Field gameField)
        {
            Bitmap result = new Bitmap(tblMainGameView.Size.Width,
                tblMainGameView.Size.Height);

            DrawStars(result);

            foreach (var obj in gameField.GameObjects)
            {
                if (obj is Tower)
                {
                    DrawTower(result, obj);
                }
                else if (obj is Enemy)
                {
                    DrawEnemies(result, obj);
                }
                else if (obj is Cannonball)
                {
                    DrawCannonBalls(result, obj);
                }
                else if (obj is Turret)
                {
                    DrawTurrets(result, obj);
                }
            }

            DrawText("Score: ", result, new System.Drawing.Point(0, 0));
            DrawText(_game.GetScore().ToString(), result, new Point(100, 0));
            DrawEffects(result);

            tblMainGameView.BackgroundImage = result;
        }

        private void DrawTower(Bitmap result, GameObject obj)
        {
            using (Graphics g = Graphics.FromImage(result))
            {
                GamePoint lookPoint = new GamePoint(0, 0);
                lookPoint.X = tblMainGameView.PointToClient(MousePosition).X;
                lookPoint.Y = tblMainGameView.PointToClient(MousePosition).Y;

                ((Tower)obj).LookAt(lookPoint);

                Image image = RotateImage(Properties.Resources.Tower,
                    RadianToDegree((float)obj.Direction.Angle) + 90);

                g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2,
                    ((int)obj.Position.Y) - image.Height / 2);

                DrawText("Health : ", result,
                    new System.Drawing.Point(this.Size.Width / 2 - 70, 0));

                DrawText(((Tower)obj).LifePoints.ToString(),
                    result, new Point(this.Size.Width / 2 + 70, 0));

                if (_showSpellCost)
                {
                    DrawText("Need " + SpellCost.ToString() + " points.",
                        result, new Point(this.Size.Width / 2 - 100, this.Size.Height - 100));
                }
            }
        }

        private void DrawEnemies(Bitmap result, GameObject obj)
        {
            using (Graphics g = Graphics.FromImage(result))
            {
                Image image;
                if (obj.LifePoints > EnemyBossLookLife)
                {
                    image = RotateImage(Properties.Resources.EnemyBoss,
                        RadianToDegree((float)obj.Direction.Angle) + 90);
                }
                else
                {
                    image = RotateImage(Properties.Resources.Enemy,
                      RadianToDegree((float)obj.Direction.Angle) + 90);
                }

                g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2,
                    ((int)obj.Position.Y) - image.Height / 2);
            }
        }

        private void DrawCannonBalls(Bitmap result, GameObject obj)
        {
            using (Graphics g = Graphics.FromImage(result))
            {
                Image image;
                if (obj.Damage >= StrongCannonballLookDamage)
                {
                    image = RotateImage(Properties.Resources.CannonBAll1,
                        RadianToDegree((float)obj.Direction.Angle) + 90);
                }
                else
                {
                    image = RotateImage(Properties.Resources.CannonBAll3,
                        RadianToDegree((float)obj.Direction.Angle) + 90);
                }

                g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2,
                    ((int)obj.Position.Y) - image.Height / 2);
            }
        }

        private void DrawTurrets(Bitmap result, GameObject obj)
        {
            using (Graphics g = Graphics.FromImage(result))
            {
                Image image = RotateImage(Properties.Resources.Turret,
                    RadianToDegree((float)obj.Direction.Angle) + 90);

                g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2,
                    ((int)obj.Position.Y) - image.Height / 2);
            }
        }

        #endregion

        #region AnotherDrawing

        private void DieEffects(object sender, EventArgs e)
        {
            if (sender is Enemy)
            {
                _effects.Add(new Boom(((GameObject)sender).Position, Properties.Resources.Boom, 200));
                playSound(Properties.Resources.TurretExplosionSound);
            }
            else if (sender is Cannonball)
            {
                _effects.Add(new Boom(((GameObject)sender).Position, Properties.Resources.CannonBallBoom, 100));
            }
            else if (sender is Turret)
            {
                _effects.Add(new Boom(((GameObject)sender).Position, Properties.Resources.Boom, 2000));
                playSound(Properties.Resources.EnemyExplosionSound);
            }
        }

        private void CreateStars()
        {
            Random rand = new Random();
            for (int i = 0; i < BackgroundStartCount; i++)
            {
                int size = rand.Next(0, 7);
                Rectangle rect = new Rectangle(
                    rand.Next(0, tblMainGameView.Width),
                    rand.Next(0, tblMainGameView.Height),
                    size,
                    size);

                _stars.Add(rect);
            }
        }

        private void DrawEffects(Bitmap result)
        {
            _effects.ForEach(obj =>
            {
                DrawBooms(result, obj);
            });

            _effects.RemoveAll(obj =>
            {
                Boom boom = obj as Boom;
                if (boom != null)
                {
                    return !boom.IsAlive;
                }

                return false;
            });
        }

        private void DrawBooms(Bitmap result, object obj)
        {
            Boom boom = obj as Boom;
            if (boom != null)
            {
                using (Graphics g = Graphics.FromImage(result))
                {
                    Image image = RotateImage(boom.Look,
                                   RadianToDegree((float)boom.Angle));

                    g.DrawImage(image, ((int)boom.Position.X) - image.Width / 2,
                        ((int)boom.Position.Y) - image.Height / 2);
                }
            }
        }

        private void DrawStars(Bitmap result)
        {
            using (Graphics g = Graphics.FromImage(result))
            {
                Pen blackPen = new Pen(Color.Aqua, 2);
                _stars.ForEach(obj => g.DrawEllipse(blackPen, obj));
            }
        }

        private void ShowSpellCost()
        {
            _showSpellCost = true;

            System.Timers.Timer timer = new System.Timers.Timer(TimeOfSpellCostViewing);
            timer.AutoReset = false;
            timer.Elapsed += (sender, e) => _showSpellCost = false;
            timer.Start();
        }

        private void DrawText(string drawString, Image result, Point drawPoint)
        {
            using (Graphics grafiGraphics = Graphics.FromImage(result))
            {
                Font drawFont = new Font("MV Boli", 25);
                SolidBrush drawBrush = new SolidBrush(Color.Chartreuse);

                grafiGraphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
            }
        }

        private Image RotateImage(Image image, float angle)
        {
            Image result = new Bitmap(image.Width, image.Height, image.PixelFormat);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.TranslateTransform(image.Width / 2f, image.Height / 2f);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-image.Width / 2f, -image.Height / 2f);
                graphics.DrawImage(image, 0, 0);
            }
            return result;
        }

        private void LevelWinOutput(Field gameField)
        {
            bntNextLevel.Visible = true;

            Bitmap result = new Bitmap(tblMainGameView.Size.Width,
                tblMainGameView.Size.Height);

            DrawText(_game.GetScore().ToString(), result, new Point(100, 0));

            tblMainGameView.BackgroundImage = result;
        }

        #endregion

        #region User click functions

        private void tblMainGameView_MouseClick(object sender, MouseEventArgs e)
        {
            if (_game != null && _game.GameStatus == Status.IsStarted)
            {
                if (_cursorStatus == CursorStatus.Fire)
                {
                    Tower tower = (Tower)_game.GameField.GameObjects.Find(obj =>
                        obj is Tower);

                    GamePoint lookPoint = new GamePoint(0, 0);
                    lookPoint.X = tblMainGameView.PointToClient(MousePosition).X;
                    lookPoint.Y = tblMainGameView.PointToClient(MousePosition).Y;
                    tower.LookAt(lookPoint);
                    _game.Fire();
                    playSound(Properties.Resources.BeemSound);
                }
                else if (_cursorStatus == CursorStatus.Turret)
                {
                    GamePoint turretPosision = new GamePoint(0, 0);
                    turretPosision.X = tblMainGameView.PointToClient(MousePosition).X;
                    turretPosision.Y = tblMainGameView.PointToClient(MousePosition).Y;

                    Turret turret = new Turret(
                        _game.GameField,
                        turretPosision,
                        _game.GameField.CurrentGameLevel.TurretColliderRadius,
                        TurretFindingCollider,
                        TurretLifePoints,
                        TurretFireRateDivisor,
                        SpellCost);


                    if (_game.BuyGameObject(turret) == BuingStatus.NeedMorePoints)
                    {
                        ShowSpellCost();
                    }

                    this.Cursor = new Cursor((Properties.Resources.Mark).GetHicon());

                    _cursorStatus = CursorStatus.Fire;
                }
            }
        }

        private void bntNextLevel_Click(object sender, EventArgs e)
        {
            CreateStars();
            bntNextLevel.Visible = false;

            if (_game.GameStatus == Status.IsWonLevel)
            {
                _effects.Clear();
                _game.NextLevel();

                if (_game.GameField.CurrentGameLevel.Number == 2)
                {
                    btnTurret.Enabled = true;
                }
                if (_game.GameField.CurrentGameLevel.Number == 3)
                {
                    btnHitAll.Enabled = true;
                    btnSlowAll.Enabled = true;
                }
            }
        }

        private void btnHitAll_Click(object sender, EventArgs e)
        {
            HitAll();
        }

        private void btnSlowAll_Click(object sender, EventArgs e)
        {
            SlowAll();
        }

        private void btnTurret_Click(object sender, EventArgs e)
        {
            BuyTurret();
        }

        private void btnStartGame_Click(object sender, System.EventArgs e)
        {
            RunGame();
            btnStartGame.Visible = false;
            btnStartGame.Enabled = false;
            bntPause.Visible = true;
            bntPause.Enabled = true;

            CreateStars();
        }

        private void bntPause_Click(object sender, System.EventArgs e)
        {
            if (_game != null)
            {
                if (_game.GameStatus == Status.IsPaused)
                {
                    _game.Restore();
                    bntExit.Visible = false;
                    bntExit.Enabled = false;
                }
                else if (_game.GameStatus == Status.IsStarted)
                {
                    _game.Pause();
                    bntExit.Visible = true;
                    bntExit.Enabled = true;
                }
            }
        }

        private void bntExit_Click(object sender, System.EventArgs e)
        {
            _game.Stop();
            PageEventHandler(this, new PageEventArgs(typeof(MainPage)));
            Dispose();
        }

        private void PlaingPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                bntPause_Click(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.D1 && btnTurret.Enabled)
            {
                BuyTurret();
            }
            else if (e.KeyCode == Keys.D2 && btnHitAll.Enabled)
            {
                HitAll();
            }
            else if (e.KeyCode == Keys.D3 && btnSlowAll.Enabled)
            {
                SlowAll();
            }
        }

        #endregion

        private float RadianToDegree(float angle)
        {
            return angle * (180.0F / (float)Math.PI);
        }

        #region Active spells fuctions

        private void BuyTurret()
        {
            _cursorStatus = CursorStatus.Turret;

            this.Cursor = new Cursor((Properties.Resources.Turret).GetHicon());
        }

        private void HitAll()
        {
            AllHitSpell allHilSpell = new AllHitSpell(_game.GameField, SpellDuration, SpellCost);
            if (_game.BuyGameObject(allHilSpell) == BuingStatus.Success)
            {
                SoundPlayer sound = new SoundPlayer(Properties.Resources.HitAllSound);
                sound.Play();

                allHilSpell.Cast();
            }
            else
            {
                ShowSpellCost();
            }
        }

        private void SlowAll()
        {
            AllSlowSpell allSlowSpell = new AllSlowSpell(_game.GameField, SpellCost);
            if (_game.BuyGameObject(allSlowSpell) == BuingStatus.Success)
            {
                SoundPlayer sound = new SoundPlayer(Properties.Resources.SlowAllSound);
                sound.Play();

                allSlowSpell.Cast();
            }
            else
            {
                ShowSpellCost();
            }
        }

        #endregion

        #region Button select sounds.
        private void btnStartGame_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void bntNextLevel_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void bntExit_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void bntPause_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void btnTurret_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void btnHitAll_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void btnSlowAll_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }
        #endregion

        private void playSound(Stream sound)
        {
            SoundPlayer player = new SoundPlayer(sound);
            player.Play();
        }

        private Level[] LoadLevels()
        {
            Level[] levels = new Level[LevelsSettings.Default.MaxLevel];

            levels[0] = new Level(
                    1,
                    LevelsSettings.Default.IterationLatency,
                    LevelsSettings.Default.EnemyGenerationLanency,
                    LevelsSettings.Default.MaxLevel,
                    LevelsSettings.Default.Level1EnemyCount,
                    LevelsSettings.Default.Level1EnemyDamage,
                    LevelsSettings.Default.Level1EnemyVelocity,
                    LevelsSettings.Default.Level1EnemyLife,
                    LevelsSettings.Default.Level1EnemyPowerRising,
                    LevelsSettings.Default.TowerLife,
                    LevelsSettings.Default.TowerCannonBallLifeTime,
                    LevelsSettings.Default.TurretCannonBallLifeTime,
                    LevelsSettings.Default.TowerCannonDamage,
                    LevelsSettings.Default.TurretCannonDamage,
                    LevelsSettings.Default.AllHitSpellDamage,
                    LevelsSettings.Default.AllSlowSpellRatio,
                    LevelsSettings.Default.AllSlowSpellDuration,
                    LevelsSettings.Default.EnemyCollierRadius,
                    LevelsSettings.Default.TowerColliderRadius,
                    LevelsSettings.Default.TurretColliderRadius,
                    LevelsSettings.Default.CannonBallColliderRadius,
                    LevelsSettings.Default.TowerCannonBallVelosity,
                    LevelsSettings.Default.TurretCannonBallVelosity);

            levels[1] = new Level(
                    2,
                    LevelsSettings.Default.IterationLatency,
                    LevelsSettings.Default.EnemyGenerationLanency,
                    LevelsSettings.Default.MaxLevel,
                    LevelsSettings.Default.Level2EnemyCount,
                    LevelsSettings.Default.Level2EnemyDamage,
                    LevelsSettings.Default.Level2EnemyVelocity,
                    LevelsSettings.Default.Level2EnemyLife,
                    LevelsSettings.Default.Level2EnemyPowerRising,
                    LevelsSettings.Default.TowerLife,
                    LevelsSettings.Default.TowerCannonBallLifeTime,
                    LevelsSettings.Default.TurretCannonBallLifeTime,
                    LevelsSettings.Default.TowerCannonDamage,
                    LevelsSettings.Default.TurretCannonDamage,
                    LevelsSettings.Default.AllHitSpellDamage,
                    LevelsSettings.Default.AllSlowSpellRatio,
                    LevelsSettings.Default.AllSlowSpellDuration,
                    LevelsSettings.Default.EnemyCollierRadius,
                    LevelsSettings.Default.TowerColliderRadius,
                    LevelsSettings.Default.TurretColliderRadius,
                    LevelsSettings.Default.CannonBallColliderRadius,
                    LevelsSettings.Default.TowerCannonBallVelosity,
                    LevelsSettings.Default.TurretCannonBallVelosity);

            levels[2] = new Level(
                    3,
                    LevelsSettings.Default.IterationLatency,
                    LevelsSettings.Default.EnemyGenerationLanency,
                    LevelsSettings.Default.MaxLevel,
                    LevelsSettings.Default.Level3EnemyCount,
                    LevelsSettings.Default.Level3EnemyDamage,
                    LevelsSettings.Default.Level3EnemyVelocity,
                    LevelsSettings.Default.Level3EnemyLife,
                    LevelsSettings.Default.Level3EnemyPowerRising,
                    LevelsSettings.Default.TowerLife,
                    LevelsSettings.Default.TowerCannonBallLifeTime,
                    LevelsSettings.Default.TurretCannonBallLifeTime,
                    LevelsSettings.Default.TowerCannonDamage,
                    LevelsSettings.Default.TurretCannonDamage,
                    LevelsSettings.Default.AllHitSpellDamage,
                    LevelsSettings.Default.AllSlowSpellRatio,
                    LevelsSettings.Default.AllSlowSpellDuration,
                    LevelsSettings.Default.EnemyCollierRadius,
                    LevelsSettings.Default.TowerColliderRadius,
                    LevelsSettings.Default.TurretColliderRadius,
                    LevelsSettings.Default.CannonBallColliderRadius,
                    LevelsSettings.Default.TowerCannonBallVelosity,
                    LevelsSettings.Default.TurretCannonBallVelosity);

            return levels;
        } 
        #endregion
    }
}
