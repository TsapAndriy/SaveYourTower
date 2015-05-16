using System;

namespace SaveYourTower.DesktopUI.GamePages
{
    public class PageEventArgs : EventArgs
    {
        public Type PageType { get; private set; }

        public PageEventArgs(Type pageType)
        {
            PageType = pageType;
        }
    }
}
