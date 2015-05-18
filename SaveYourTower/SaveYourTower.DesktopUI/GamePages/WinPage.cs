using System;
using System.Media;
using System.Windows.Forms;

namespace SaveYourTower.DesktopUI.GamePages
{
    public partial class WinPage : UserControl, ILoadable
    {
        #region Events

        public event EventHandler<PageEventArgs> PageEventHandler;
        
        #endregion

        #region Constructors

        public WinPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
        
        #endregion

        #region Methods

        private void btnStart_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(PlaingPage)));
            this.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(MainPage)));
            this.Dispose();
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
