using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaveYourTower.DesktopUI.GamePages;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Spells;
using GamePoint = SaveYourTower.GameEngine.DataContainers.Point;

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

        private CursorStatus _cursorStatus = CursorStatus.Fire;
        public event Action<Type> PageEventHandler;
        private static Game _game;
        private static object _sync = new object();
        private Cursor defaultCursor;

        public PlaingPage()
        {
            InitializeComponent();
            RunGame();
            defaultCursor = this.Cursor;
            this.Dock = DockStyle.Fill;
        }

        private async void RunGame()
        {
            _game = new Game(new GamePoint(this.Width, this.Height), 1);
            _game.Output += Output;
            _game.Input += Input;
            Task task = new Task(_game.Run);
            task.Start();
            await task;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(MainPage));
            this.Dispose();
        }


        private void btnLose_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(LosePage));
            this.Dispose();
        }

        private void btnWin_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(WinPage));
            this.Dispose();
        }

        private void btnWinLevel_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(PlaingPage));
            this.Dispose();
        }

        #region MyRegion
        public void Input(Game game)
        {
            lock (_sync)
            {
                if (_game.GameStatus == Status.IsReadyToStart)
                {
                    game.Start();
                }
            }
        }

        public void Output(Game game)
        {
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
                    this.Invoke((Action<Type>) PageEventHandler, typeof (WinPage));
                }
                this.Dispose();
            }
            else if (game.GameStatus == Status.IsExit)
            {
                lock (_sync)
                {
                    this.Invoke((Action<Type>)PageEventHandler, typeof(LosePage)); 
                }
                this.Dispose();
            }
        }
        #endregion


        private void WinLevelOutput(Field gameField)
        {
            bntNextLevel.Visible = true;

            Bitmap result = new Bitmap(pFieldView.Size.Width, pFieldView.Size.Height);

            DrawText(_game.GetScore().ToString(), result, new System.Drawing.Point(100, 0));

            pFieldView.Image = result;
        }

        private void DrawGameObjects(Field gameField)
        {
            Bitmap result = new Bitmap(pFieldView.Size.Width, pFieldView.Size.Height);

            foreach (var obj in gameField.GameObjects)
            {
                if (obj is Tower)
                {
                    Graphics g = Graphics.FromImage(result);
                    //Image image = Properties.Resources.Tower;

                    GamePoint lookPoint = new GamePoint(0, 0);
                    lookPoint.X = pFieldView.PointToClient(MousePosition).X;
                    lookPoint.Y = pFieldView.PointToClient(MousePosition).Y;

                    ((Tower)obj).LookAt(lookPoint);

                    Image image = RotateImage(Properties.Resources.Tower, RadianToDegree((float)obj.Direction.Angle) + 90);


                    g.DrawImage(image, ((int)obj.Position.X) - image.Width / 2, ((int)obj.Position.Y) - image.Height / 2);
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
                    Image image = RotateImage(Properties.Resources.CannonBAll3, RadianToDegree((float)obj.Direction.Angle) + 90);

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

            DrawText(_game.GetScore().ToString(), result, new System.Drawing.Point(0, 0));

            var towerLife = _game.GameField.GameObjects.Find(obj => obj is Tower).LifePoints;
            DrawText(towerLife.ToString(), result, new System.Drawing.Point(this.Size.Width / 2, 0));

            pFieldView.Image = result;
        }

        private void DrawText(string drawString, Image result, System.Drawing.Point drawPoint)
        {
            Graphics grafiGraphics = Graphics.FromImage(result);

            // Create font and brush.
            Font drawFont = new Font("MV Boli", 25);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

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
                graphics.TranslateTransform((float)image.Width / 2f, (float)image.Height / 2f);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-(float)image.Width / 2f, -(float)image.Height / 2f);
                graphics.DrawImage(image, 0, 0);
            }
            return result;
        }

        private void pFieldView_Click_1(object sender, EventArgs e)
        {
            if (_cursorStatus == CursorStatus.Fire)
            {
                Tower tower = (Tower)_game.GameField.GameObjects.Find(obj => obj is Tower);
                GamePoint lookPoint = new GamePoint(0, 0);
                lookPoint.X = pFieldView.PointToClient(MousePosition).X;
                lookPoint.Y = pFieldView.PointToClient(MousePosition).Y;
                tower.LookAt(lookPoint);

                _game.Fire(Properties.Resources.CannonBAll3); 
            }
            else if (_cursorStatus == CursorStatus.Turret)
            {
                GamePoint turretPosision = new GamePoint(0, 0);
                turretPosision.X = pFieldView.PointToClient(MousePosition).X;
                turretPosision.Y = pFieldView.PointToClient(MousePosition).Y;

                Image view = Properties.Resources.Turret;
                _game.BuyGameObject(new Turret(_game.GameField, turretPosision, 17, 500, 30, 50, cost: 1));
                _cursorStatus = CursorStatus.Fire;
            }

            this.Cursor = new Cursor((Properties.Resources.Mark).GetHicon());
        }


      

        private void pFieldView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void bntNextLevel_Click(object sender, EventArgs e)
        {
            if (_game.GameStatus == Status.IsWinnedLevel)
            {
                _game.NextLevel();
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

    }
}
