using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

using SaveYourTower.DesktopUI.GamePages;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Spells;
using GamePoint = SaveYourTower.GameEngine.DataContainers.Point;
using SaveYourTower.DesktopUI.VisualEffects;

namespace SaveYourTower.DesktopUI
{
    public partial class PlaingPage : UserControl, ILoadable
    {
        private enum CursorStatus
        {
            Fire,
            Turret,
            Mine
        }

        private List<object> _effects = new List<object>();
        private static Game _game;
        private static object _sync = new object();
        private Cursor defaultCursor;
        private CursorStatus _cursorStatus = CursorStatus.Fire;
        private Level[] _levels;
        private List<Rectangle> _stars = new List<Rectangle>(); 

        public event EventHandler<PageEventArgs> PageEventHandler;
        
        public PlaingPage()
        {
            InitializeComponent();
            defaultCursor = this.Cursor;

            this.Dock = DockStyle.Fill;
            btnTurret.Enabled = false;
            btnHitAll.Enabled = false;
            btnSlowAll.Enabled = false; 
            this.Cursor = new Cursor((Properties.Resources.Mark).GetHicon());
            _levels = LoadLevels();

        }

        private  void RunGame()
        {
            _game = new Game(new GamePoint(this.Width, this.Height), _levels);
            _game.OutputEventHandler += OnOutputEventHandler;
            _game.InputEventHandler += OnInputEventHandler;
            _game.DieEventHandler += DieEffects;
            Task task = new Task(_game.Run);
            task.Start();
        }

        private void DieEffects(object sender, EventArgs e)
        {
            Enemy enemy = sender as Enemy;
            if (sender is Enemy)
            {
                _effects.Add(new Boom(((Enemy)sender).Position, Properties.Resources.Boom, 200));
                playSound(Properties.Resources.TurretExplosionSound);
            }
            else if (sender is CannonBall)
            {
                _effects.Add(new Boom(((CannonBall)sender).Position, Properties.Resources.CannonBallBoom, 100));
            }
            else if (sender is Turret)
            {
                _effects.Add(new Boom(((Turret)sender).Position, Properties.Resources.Boom, 2000));
                playSound(Properties.Resources.EnemyExplosionSound);
            }
        }
        
        #region MyRegion
        public void OnInputEventHandler(object sender, EventArgs e)
        {
            Game game = sender as Game;
            lock (_sync)
            {
                if (_game.GameStatus == Status.IsReadyToStart)
                {
                    game.Start();
                }
            }
        }

        public void OnOutputEventHandler(object sender, EventArgs e)
        {
            Game game = sender as Game;
            
            if (game.GameStatus == Status.IsStarted)
            {
                lock (_sync)
                {
                    Invoke((Action<Field>)DrawGameObjects, _game.GameField);
                }
            }
            else if (game.GameStatus == Status.IsWinnedLevel)
            {
                lock (_sync)
                {
                    Invoke((Action<Field>)WinLevelOutput, _game.GameField);
                }
            }
            else if (game.GameStatus == Status.IsWinned)
            {
                lock (_sync)
                {

                    BeginInvoke(PageEventHandler,
                    this,
                    new PageEventArgs(typeof(WinPage)));

                    Invoke((MethodInvoker)delegate { this.Dispose(); });
                }
            }
            else if (game.GameStatus == Status.IsExit)
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
        #endregion

        private void WinLevelOutput(Field gameField)
        {
            bntNextLevel.Visible = true;

            Bitmap result = new Bitmap(tblMainGameView.Size.Width,
                tblMainGameView.Size.Height);

            DrawText(_game.GetScore().ToString(), result, new Point(100, 0));

            tblMainGameView.BackgroundImage = result;
        }
        private void DrawGameObjects(Field gameField)
        {
            Bitmap result = new Bitmap(tblMainGameView.Size.Width,
                tblMainGameView.Size.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                Pen blackPen = new Pen(Color.Aqua, 2);
                _stars.ForEach(obj => g.DrawEllipse(blackPen, obj));
            }

            foreach (var obj in gameField.GameObjects)
            {
                if (obj is Tower)
                {
                    Graphics g = Graphics.FromImage(result);

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

                    g.Dispose();
                }
                else if (obj is Enemy)
                {
                    Graphics g = Graphics.FromImage(result);
                    Image image;
                    if (obj.LifePoints > 100)
                    {
                        image = RotateImage(Properties.Resources.EnemyBoss,
                            RadianToDegree((float) obj.Direction.Angle) + 90);
                    }
                    else
                    {
                        image = RotateImage(Properties.Resources.Enemy,
                          RadianToDegree((float)obj.Direction.Angle) + 90);
                    }

                    g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2,
                        ((int)obj.Position.Y) - image.Height / 2);

                    g.Dispose();
                }
                else if (obj is CannonBall)
                {
                    Graphics g = Graphics.FromImage(result);
                    Image image;
                    if (obj.Damage >= 10)
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
                    g.Dispose();
                }
                else if (obj is Turret)
                {
                    Graphics g = Graphics.FromImage(result);

                    Image image = RotateImage(Properties.Resources.Turret,
                        RadianToDegree((float)obj.Direction.Angle) + 90);

                    g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2,
                        ((int)obj.Position.Y) - image.Height / 2);

                    g.Dispose();
                }
            }

            DrawText("Score: ", result, new System.Drawing.Point(0, 0));
            DrawText(_game.GetScore().ToString(), result, new Point(100, 0));
            DrawEffects(result);

            tblMainGameView.BackgroundImage = result;


        }

        private void CreateStars()
        {
            Random rand = new Random();
            for (int i = 0; i < 200; i++)
            {
                int size = rand.Next(0, 7);
                Rectangle rect = new Rectangle(rand.Next(0, tblMainGameView.Width), rand.Next(0, tblMainGameView.Height), size, size);
                _stars.Add(rect);
            }
        }



        private void DrawEffects(Bitmap result)
        {
            _effects.ForEach(obj =>
            {
                Boom boom = obj as Boom;
                if (boom != null)
                {
                    Graphics g = Graphics.FromImage(result);
                    Image image = RotateImage(boom.Look,
                        RadianToDegree((float) boom.Angle));

                    g.DrawImage(image, ((int)boom.Position.X) - image.Width / 2,
                        ((int)boom.Position.Y) - image.Height / 2);

                    g.Dispose();
                }
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

        private void DrawText(string drawString, Image result, Point drawPoint)
        {
            Graphics grafiGraphics = Graphics.FromImage(result);

            // Create font and brush.
            Font drawFont = new Font("MV Boli", 25);
            SolidBrush drawBrush = new SolidBrush(Color.Chartreuse);

            // Draw string to screen.
            grafiGraphics.DrawString(drawString, drawFont, drawBrush, drawPoint);

            grafiGraphics.Dispose();
        }

        private float RadianToDegree(float angle)
        {
            return angle * (180.0F / (float)Math.PI);
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

        private void bntNextLevel_Click(object sender, EventArgs e)
        {
            CreateStars();
            bntNextLevel.Visible = false;

            if (_game.GameStatus == Status.IsWinnedLevel)
            {
                _effects.Clear();
                _game.NextLevel();

                if (_game.GameField.CurrenGameLevel.Number == 2)
                {
                    btnTurret.Enabled = true;
                }
                if (_game.GameField.CurrenGameLevel.Number == 3)
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

        private static void HitAll()
        {
            AllHilSpell allHilSpell = new AllHilSpell(_game.GameField, 100, 10);
            if (_game.BuyGameObject(allHilSpell) == BuingStatus.Success)
            {
                SoundPlayer sound = new SoundPlayer(Properties.Resources.HitAllSound);
                sound.Play();

                allHilSpell.Cast();
            }
        }

        private void btnSlowAll_Click(object sender, EventArgs e)
        {
            SlowAll();
        }

        private static void SlowAll()
        {
            AllSlowSpell allSlowSpell = new AllSlowSpell(_game.GameField, 10);
            if (_game.BuyGameObject(allSlowSpell) == BuingStatus.Success)
            {
                SoundPlayer sound = new SoundPlayer(Properties.Resources.SlowAllSound);
                sound.Play();

                allSlowSpell.Cast();
            }
        }

        private void btnTurret_Click(object sender, EventArgs e)
        {
            BuyTurret();
        }

        private void BuyTurret()
        {
            _cursorStatus = CursorStatus.Turret;

            this.Cursor = new Cursor((Properties.Resources.Turret).GetHicon());
        }

        private void btnStartGame_Click(object sender, System.EventArgs e)
        {
            RunGame();
            btnStartGame.Visible = false;
            btnStartGame.Enabled= false;
            bntPause.Visible = true;
            bntPause.Enabled = true;

            CreateStars();
        }

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
                        _game.GameField.CurrenGameLevel.TurretColliderRadius, 
                        150, 
                        30, 
                        10, 
                        cost: 10);
                    _game.BuyGameObject(turret);
                    this.Cursor = new Cursor((Properties.Resources.Mark).GetHicon());

                    _cursorStatus = CursorStatus.Fire;
                }
            }
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

        #region select button sound
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


    }
}
