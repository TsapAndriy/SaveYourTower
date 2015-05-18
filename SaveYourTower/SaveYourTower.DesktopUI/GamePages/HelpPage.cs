using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace SaveYourTower.DesktopUI.GamePages
{
    public partial class HelpPage : UserControl, ILoadable
    {
        #region Events

        public event EventHandler<PageEventArgs> PageEventHandler;
        
        #endregion

        #region Fields

        private Array _helpImages;
        private int _helpIndex = 0;
        
        #endregion

        #region Constructors

        public HelpPage()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            List<Image> imageList = new List<Image>();

            imageList.Add(Properties.Resources.Help1);
            imageList.Add(Properties.Resources.Help2);
            imageList.Add(Properties.Resources.Help3);
            imageList.Add(Properties.Resources.Help4);
            imageList.Add(Properties.Resources.Help5);
            imageList.Add(Properties.Resources.Help6);

            _helpImages = imageList.ToArray();
            pbHelpView.Image = (Image)_helpImages.GetValue(_helpIndex);
        }
        
        #endregion

        #region Methods

        private void btnBack_Click(object sender, EventArgs e)
        {
            PageEventHandler(this, new PageEventArgs(typeof(MainPage)));
            this.Dispose();
        }

        private void btnNextHelpPage_Click(object sender, EventArgs e)
        {
            if (_helpImages != null)
            {
                _helpIndex =
                    _helpIndex < _helpImages.Length - 1
                    ? _helpIndex + 1
                    : 0;

                pbHelpView.Image = (Image)_helpImages.GetValue(_helpIndex);
            }
        }

        private void btnPrevHelpPage_Click(object sender, EventArgs e)
        {

            if (_helpImages != null)
            {
                _helpIndex =
                    _helpIndex > 0
                    ? _helpIndex - 1
                    : _helpImages.Length - 1;

                pbHelpView.Image = (Image)_helpImages.GetValue(_helpIndex);
            }
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void btnPrevHelpPage_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        }

        private void btnNextHelpPage_MouseEnter(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SelectSound);
            sound.Play();
        } 
        #endregion
    }
}
