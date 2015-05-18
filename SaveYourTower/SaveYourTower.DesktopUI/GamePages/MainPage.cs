using System;
using System.Media;
using System.Windows.Forms;

using SaveYourTower.DesktopUI.GamePages;

namespace SaveYourTower.DesktopUI
{

    public partial class MainPage : UserControl, ILoadable
    {
        #region Events

        public event EventHandler<PageEventArgs> PageEventHandler;

        #endregion

        #region Constructors

        public MainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
        
        #endregion

        #region Methods

        private void button1_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(PlaingPage)));
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(HelpPage)));
            this.Dispose();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(SettingsPage)));
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

        private void btnHelp_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void btnSettings_MouseEnter(object sender, EventArgs e)
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
