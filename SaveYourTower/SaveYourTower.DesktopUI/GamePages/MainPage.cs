using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaveYourTower.DesktopUI.GamePages;

namespace SaveYourTower.DesktopUI
{

    public partial class MainPage : UserControl, ILoadable
    {
        public event Action<Type> PageEventHandler;

        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(PlaingPage));
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(HelpPage));
            this.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
