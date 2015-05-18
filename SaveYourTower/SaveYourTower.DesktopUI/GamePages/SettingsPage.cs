using System;
using System.Media;
using System.Windows.Forms;


namespace SaveYourTower.DesktopUI.GamePages
{
    public partial class SettingsPage : UserControl, ILoadable
    {
        #region Events

        public event EventHandler<PageEventArgs> PageEventHandler;
        
        #endregion

        #region Constructors

        public SettingsPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            lblFullScreen.Text = Properties.Settings.Default.FullScreen.ToString();
        }
        
        #endregion

        #region Methods

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
                ((SaveYourTowerForm)this.Parent).WindowState =
                    FormWindowState.Normal;
            }
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void lblFullScreen_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void lblNextDifficulty_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void lblPrevDifficulty_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        } 

        #endregion
    }
}
