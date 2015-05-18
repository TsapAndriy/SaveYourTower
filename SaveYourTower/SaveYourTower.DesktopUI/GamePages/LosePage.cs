using System;
using System.Media;
using System.Windows.Forms;

namespace SaveYourTower.DesktopUI.GamePages
{
    public partial class LosePage : UserControl, ILoadable
    {
        #region Events

        public event EventHandler<PageEventArgs> PageEventHandler;
        
        #endregion

        #region Constructors

        public LosePage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
        
        #endregion

        #region Methds

        private void button1_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(PlaingPage)));
            this.Dispose();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        } 

        #endregion
    }
}
