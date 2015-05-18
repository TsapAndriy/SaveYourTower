using System;

namespace SaveYourTower.DesktopUI.GamePages
{
    public class PageEventArgs : EventArgs
    {
        #region Properties

        public Type PageType { get; private set; }
        
        #endregion

        #region Constructors

        public PageEventArgs(Type pageType)
        {
            PageType = pageType;
        } 

        #endregion
    }
}
