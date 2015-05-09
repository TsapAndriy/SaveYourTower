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
        }

        private  void RunGame()
        {
            _game = new Game(new GamePoint(this.Width, this.Height), 1);
            _game.OutputEventHandler += OnOutputEventHandler;
            _game.InputEventHandler += OnInputEventHandler;
            _game.DieEventHandler += DieEffects;
            Task task = new Task(_game.Run);
            task.Start();
        }

        private void DieEffects(object sender, EventArgs e)
        {
            playSound(Properties.Resources.Explosion);
            Enemy enemy = sender as Enemy;
            if (enemy != null)
            {
                _effects.Add(new Boom(enemy.Position));
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(MainPage)));
            this.Dispose();
        }

        private void btnLose_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(LosePage)));
            this.Dispose();
        }

        private void btnWin_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(WinPage)));
            this.Dispose();
        }

        private void btnWinLevel_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(PlaingPage)));
            this.Dispose();
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
                    this.Invoke((Action<Field>)DrawGameObjects, _game.GameField);
                }
            }
            else if (game.GameStatus == Status.IsWinnedLevel)
            {
                lock (_sync)
                {
                    this.Invoke((Action<Field>)WinLevelOutput, _game.GameField);
                }
            }
            else if (game.GameStatus == Status.IsWinned)
            {
                lock (_sync)
                {
                    this.BeginInvoke(PageEventHandler,this, new PageEventArgs(typeof(WinPage)));
                    this.BeginInvoke((MethodInvoker)delegate { this.Dispose(); });
                }
            }
            else if (game.GameStatus == Status.IsExit)
            {
                lock (_sync)
                {
                    this.BeginInvoke(PageEventHandler, this, new PageEventArgs(typeof(LosePage)));
                    this.BeginInvoke((MethodInvoker)delegate { this.Dispose(); });
                }
            } 
        }
        #endregion

        private void WinLevelOutput(Field gameField)
        {
            bntNextLevel.Visible = true;

            Bitmap result = new Bitmap(tblMainGameView.Size.Width, tblMainGameView.Size.Height);

            DrawText(_game.GetScore().ToString(), result, new Point(100, 0));

            tblMainGameView.BackgroundImage = result;
        }

        private void DrawGameObjects(Field gameField)
        {
            Bitmap result = new Bitmap(tblMainGameView.Size.Width, tblMainGameView.Size.Height);
            foreach (var obj in gameField.GameObjects)
            {
                if (obj is Tower)
                {
                    Graphics g = Graphics.FromImage(result);

                    GamePoint lookPoint = new GamePoint(0, 0);
                    lookPoint.X = tblMainGameView.PointToClient(MousePosition).X;
                    lookPoint.Y = tblMainGameView.PointToClient(MousePosition).Y;

                    ((Tower)obj).LookAt(lookPoint);

                    Image image = RotateImage(Properties.Resources.Tower, RadianToDegree((float)obj.Direction.Angle) + 90);
                    
                    g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2, ((int)obj.Position.Y) - image.Height / 2);

                    DrawText("Health : ", result, new System.Drawing.Point(this.Size.Width / 2 - 70, 0));
                    DrawText(((Tower)obj).LifePoints.ToString(), result, new Point(this.Size.Width / 2 + 70, 0));
                    g.Dispose();
                }
                else if (obj is Enemy)
                {
                    Graphics g = Graphics.FromImage(result);

                    Image image = RotateImage(Properties.Resources.Enemy, RadianToDegree((float)obj.Direction.Angle) + 90);
                    g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2, ((int)obj.Position.Y) - image.Height / 2);
                    g.Dispose();
                }
                else if (obj is CannonBall)
                {
                    Graphics g = Graphics.FromImage(result);
                    Image image;
                    if (obj.Damage >= 10)
                    {
                        image = RotateImage(Properties.Resources.CannonBAll1,
                            RadianToDegree((float) obj.Direction.Angle) + 90);
                    }
                    else
                    {
                        image = RotateImage(Properties.Resources.CannonBAll3,
                            RadianToDegree((float)obj.Direction.Angle) + 90);
                    }
                      
                    g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2, ((int)obj.Position.Y) - image.Height / 2);
                    g.Dispose();
                }
                else if (obj is Turret)
                {
                    Graphics g = Graphics.FromImage(result);

                    Image image = RotateImage(Properties.Resources.Turret, RadianToDegree((float)obj.Direction.Angle) + 90);

                    g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2, ((int)obj.Position.Y) - image.Height / 2);
                    g.Dispose();
                }
            }

            DrawText("Score: ", result, new System.Drawing.Point(0, 0));
            DrawText(_game.GetScore().ToString(), result, new Point(100, 0));
            DrawEffects(result);

            tblMainGameView.BackgroundImage = result;
        }

        private void DrawEffects(Bitmap result)
        {
            _effects.ForEach(obj =>
            {
                Boom boom = obj as Boom;
                if (boom != null)
                {
                    Graphics g = Graphics.FromImage(result);
                    Image image = RotateImage(Properties.Resources.Boom,
                        RadianToDegree((float) boom.Angle));

                    g.DrawImage(image, ((int)boom.Position.X) - image.Width / 2, ((int)boom.Position.Y) - image.Height / 2);
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
            if (_game.GameStatus == Status.IsWinnedLevel)
            {
                _effects.Clear();
                _game.NextLevel();

                if (_game.GameField.CurrenGameLevel == 2)
                {
                    btnTurret.Enabled = true;
                }
                if (_game.GameField.CurrenGameLevel == 3)
                {
                    btnHitAll.Enabled = true;
                    btnSlowAll.Enabled = true;
                }
            }

            bntNextLevel.Visible = false;
        }

        private void btnHitAll_Click(object sender, EventArgs e)
        {
            AllHilSpell allHilSpell = new AllHilSpell(_game.GameField, 100, 1);
            if (_game.BuyGameObject(allHilSpell) == BuingStatus.Success)
            {
                allHilSpell.Cast();
            }
        }

        private void btnSlowAll_Click(object sender, EventArgs e)
        {
            AllSlowSpell allSlowSpell = new AllSlowSpell(_game.GameField, 100, 1);
            if (_game.BuyGameObject(allSlowSpell) == BuingStatus.Success)
            {
                allSlowSpell.Cast();
            }
        }

        private void btnTurret_Click(object sender, EventArgs e)
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
        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_game != null && _game.GameStatus == Status.IsStarted)
            {
                if (_cursorStatus == CursorStatus.Fire)
                {
                    Tower tower = (Tower) _game.GameField.GameObjects.Find(obj => obj is Tower);
                    GamePoint lookPoint = new GamePoint(0, 0);
                    lookPoint.X = tblMainGameView.PointToClient(MousePosition).X;
                    lookPoint.Y = tblMainGameView.PointToClient(MousePosition).Y;
                    tower.LookAt(lookPoint);
                    _game.Fire();
                    playSound(Properties.Resources.Beem);
                }
                else if (_cursorStatus == CursorStatus.Turret)
                {
                    GamePoint turretPosision = new GamePoint(0, 0);
                    turretPosision.X = tblMainGameView.PointToClient(MousePosition).X;
                    turretPosision.Y = tblMainGameView.PointToClient(MousePosition).Y;

                    Turret turret = new Turret(_game.GameField, turretPosision, 17, 500, 30, 10, cost: 1);
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

        private void OnTurretFire(object sender, EventArgs e)
        {
            playSound(Properties.Resources.Beem);
        }

        private void playSound(Stream sound)
        {
            SoundPlayer player = new SoundPlayer(sound);
            player.Play();
        }

        private void tblMainGameView_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
