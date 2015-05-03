using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaveYourTower.DesktopUI.GamePages;
using SaveYourTower.GameEngine;

namespace SaveYourTower.DesktopUI
{
    public partial class PlaingPage : UserControl, ILoadable
    {
        public event Action<Type> PageEventHandler;
  
        public PlaingPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(MainPage));
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnLose_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(LosePage));
            this.Dispose();
        }

        private void btnWin_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(WinPage));
            this.Dispose();
        }

        private void btnWinLevel_Click(object sender, EventArgs e)
        {
            PageEventHandler(typeof(PlaingPage));
            this.Dispose();
        }


    }
}
