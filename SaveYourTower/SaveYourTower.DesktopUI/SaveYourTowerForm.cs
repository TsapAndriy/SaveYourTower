using System;
using System.Windows.Forms;

using SaveYourTower.DesktopUI.GamePages;


namespace SaveYourTower.DesktopUI
{
    public partial class SaveYourTowerForm : Form
    {
        public SaveYourTowerForm()
        {
            InitializeComponent();

            if (Properties.Settings.Default.FullScreen)
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnLoadPage(this, new PageEventArgs(typeof(MainPage)));
        }

        public void OnLoadPage(object sender, PageEventArgs e)
        {
            if (typeof(ILoadable).IsAssignableFrom(e.PageType))
            {
                ILoadable page = (ILoadable)Activator.CreateInstance(e.PageType);
                page.PageEventHandler += OnLoadPage;
                UserControl control = (UserControl) page;
                control.Parent = this;
                this.Controls.Add(control);
            }
        }
    }
}
