using System;
using System.Media;
using System.Windows.Forms;

namespace SaveYourTower.DesktopUI.GamePages
{
    public partial class LosePage : UserControl, ILoadable
    {
        public event EventHandler<PageEventArgs> PageEventHandler;

        public LosePage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

        }

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
    }
}
