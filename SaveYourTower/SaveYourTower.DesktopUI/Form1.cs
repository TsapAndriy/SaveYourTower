using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.PowerPacks;
using SaveYourTower.DesktopUI.GamePages;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Spells;
using SaveYourTower.GameEngine.Spells;

using GamePoint = SaveYourTower.GameEngine.DataContainers.Point;

namespace SaveYourTower.DesktopUI
{
    public partial class Form1 : Form
    {
        private int _angle = 0;
        private static Game _game;
        private static object sync = new object();
        private PictureBox qwe;



        enum PlayerAction
        {
            Nothing,
            Start,
            Fire,
            PlaceTower,
            PlaceMine,
            UseHitSpell,
            UseSlowSpell,
        }

        private PlayerAction _playerAction = PlayerAction.Nothing;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPage(typeof(MainPage));

            //_game = new Game(new GamePoint(this.Width, this.Height), 1);
            //_game.Output += Output;
            //_game.Input += Input;
            //Task task = new Task(_game.Run);
            //task.Start();
        }

        #region MyRegion
        //public void Input(Game game)
        //{
        //    //lock (sync)
        //    //{
        //    //    if (_game.GameStatus == Status.IsReadyToStart)
        //    //    {
        //    //        game.Start();
        //    //    }
        //    //}
        //}

        //public void Output(Game game)
        //{
        //    //if (game.GameStatus == Status.IsReadyToStart)
        //    //{
        //    //    StartOutput(game.GameField);
        //    //}
        //    //else 
        //    if (game.GameStatus == Status.IsStarted)
        //    {
        //        lock (sync)
        //        {
        //            this.Invoke((Action<Field>)DrawGameObjects, _game.GameField);
        //        }
        //    }

        //    //DrawGameObjects(_game.GameField);

        //    else if (game.GameStatus == Status.IsWinnedLevel)
        //    {
        //        lock (sync)
        //        {
        //            this.Invoke((Action<Field>)WinLevelOutput, _game.GameField);
        //        }
        //    }
        //    //else if (game.GameStatus == Status.IsWinned)
        //    //{
        //    //    WinOutput(game.GameField);
        //    //}
        //}
        #endregion


        public void LoadPage(Type pageType)
        {
            if (typeof(ILoadable).IsAssignableFrom(pageType))
            {
                ILoadable page = (ILoadable)Activator.CreateInstance(pageType);
                page.PageEventHandler += LoadPage;
                pFieldView.Controls.Add((UserControl)page);
            }
        }

      
       
        private void WinLevelOutput(Field gameField)
        {
            Bitmap result = new Bitmap(pFieldView.Size.Width, pFieldView.Size.Height);

            Graphics g = Graphics.FromImage(result);

            // Create string to draw.
            String drawString = "Sample Text";

            // Create font and brush.
            Font drawFont = new Font("MV Boli", 25);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(150.0F, 150.0F);

            // Draw string to screen.
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);

            g.Dispose();

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
                    Image image = Properties.Resources.Tower;
                    g.DrawImage(image,  ((int)obj.Position.X) - image.Width / 2,  ((int)obj.Position.Y) - image.Height/2);
                    g.Dispose();
                }
                else if (obj is Enemy)
                {
                    Graphics g = Graphics.FromImage(result);

                    Image image = RotateImage(Properties.Resources.top, RadianToDegree((float)obj.Direction.Angle) + 90);
                    g.DrawImage(image,  ((int)obj.Position.X) - image.Width / 2, ((int)obj.Position.Y) - image.Height / 2);
                    g.Dispose();
                }
                else if (obj is CannonBall)
                {
                    Graphics g = Graphics.FromImage(result);

                    Image image = Properties.Resources.ENRGA0;
                    g.DrawImage(image,  ((int)obj.Position.X) - image.Width / 2,  ((int)obj.Position.Y) - image.Height / 2);
                    g.Dispose();
                }
            }

            pFieldView.Image = result;
        }

        private float RadianToDegree(float angle)
        {
            return angle * (180.0F / (float)Math.PI);
        }


        private void Player_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Up)
            //{
            //    Player.Location = new Point(Player.Location.X, Player.Location.Y - 10);
            //}
            //else if (e.KeyCode == Keys.Down)
            //{
            //    Player.Location = new Point(Player.Location.X, Player.Location.Y + 10);
            //}
            //else if (e.KeyCode == Keys.Right)
            //{
            //    Player.Location = new Point(Player.Location.X + 10, Player.Location.Y);
            //}
            //else if (e.KeyCode == Keys.Left)
            //{
            //    Player.Location = new Point(Player.Location.X - 10, Player.Location.Y);
            //}
        }


        private Image RotateImage(Image image, float angle)
        {
            Image result = new Bitmap(image.Width, image.Height, image.PixelFormat);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.TranslateTransform((float)image.Width / 2f, (float)image.Height / 2f);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-(float)image.Width / 2f, -(float)image.Height / 2f);
                graphics.DrawImage(Properties.Resources.top, 0, 0);
            }
            return result;
        }

        private void pFieldView_Click(object sender, EventArgs e)
        {
            Tower tower = (Tower)_game.GameField.GameObjects.Find(obj => obj is Tower);
            GamePoint lookPoint = new GamePoint(0, 0);
            lookPoint.X = pFieldView.PointToClient(MousePosition).X;
            lookPoint.Y = pFieldView.PointToClient(MousePosition).Y;
            tower.LookAt(lookPoint);

            _game.Fire();
        }


    }
}
