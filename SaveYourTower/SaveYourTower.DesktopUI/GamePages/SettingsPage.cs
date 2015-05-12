using System;
using System.Windows.Forms;


namespace SaveYourTower.DesktopUI.GamePages
{
    public partial class SettingsPage : UserControl, ILoadable
    {
        public event EventHandler<PageEventArgs> PageEventHandler;

        public SettingsPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            lblFullScreen.Text = Properties.Settings.Default.FullScreen.ToString();
            lblDifficulty.Text = Properties.Settings.Default.Difficulty.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(MainPage)));
            this.Dispose();
        }

        private void lblFullScreen_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FullScreen = 
                !Properties.Settings.Default.FullScreen;

            lblFullScreen.Text = Properties.Settings.Default.FullScreen.ToString();

            if (Properties.Settings.Default.FullScreen)
            {
                ((SaveYourTowerForm)this.Parent).FormBorderStyle = 
                    System.Windows.Forms.FormBorderStyle.None;

                ((SaveYourTowerForm)this.Parent).WindowState = 
                    FormWindowState.Maximized;
            }
            else
            {
                ((SaveYourTowerForm)this.Parent).FormBorderStyle = 
                    System.Windows.Forms.FormBorderStyle.FixedDialog;

                ((SaveYourTowerForm) this.Parent).WindowState = 
                    FormWindowState.Normal;
            }
        }

        private void lblNextDifficulty_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Difficulty = 
                Properties.Settings.Default.Difficulty < 10
                ? Properties.Settings.Default.Difficulty + 1
                : 0;

            lblDifficulty.Text = Properties.Settings.Default.Difficulty.ToString();
        }

        private void lblPrevDifficulty_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Difficulty =
                Properties.Settings.Default.Difficulty > 0
                ? Properties.Settings.Default.Difficulty - 1
                : 10;

            lblDifficulty.Text = Properties.Settings.Default.Difficulty.ToString();
        }
    }
}
