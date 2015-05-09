using System;
using System.Windows.Forms;

using SaveYourTower.DesktopUI.GamePages;

namespace SaveYourTower.DesktopUI
{

    public partial class MainPage : UserControl, ILoadable
    {
        public event EventHandler<PageEventArgs> PageEventHandler;

        public MainPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

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
    }
}
