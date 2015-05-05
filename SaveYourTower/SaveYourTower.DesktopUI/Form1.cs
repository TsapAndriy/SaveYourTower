﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.PowerPacks;
using SaveYourTower.DesktopUI.GamePages;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Spells;
using SaveYourTower.GameEngine.Spells;


namespace SaveYourTower.DesktopUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPage(typeof(MainPage));
        }

        public void LoadPage(Type pageType)
        {
            if (typeof(ILoadable).IsAssignableFrom(pageType))
            {
                ILoadable page = (ILoadable)Activator.CreateInstance(pageType);
                page.PageEventHandler += LoadPage;
                pFieldView.Controls.Add(((UserControl) page));
            }
        }
    }
}
