using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.DesktopUI
{
    public class ViewObject : IDisposable
    {
        public double Angle { get; set; }
        public object View { get; private set; }

        public ViewObject(object view, double angle)
        {
            Angle = angle;
            View = view;
        }

        public void Dispose()
        {
            IDisposable view = View as IDisposable;
            if (view != null)
            {
                view.Dispose();
            }
        }
    }
}
