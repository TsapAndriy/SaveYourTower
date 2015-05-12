using System;
using System.Windows.Forms;

namespace SaveYourTower.DesktopUI.GamePages
{
    public partial class WinPage : UserControl, ILoadable
    {
        public event EventHandler<PageEventArgs> PageEventHandler;

        public WinPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

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
    }
}
