﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void button1_Click(object sender, EventArgs e)
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
