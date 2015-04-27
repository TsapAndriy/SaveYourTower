using System;
using System.Collections.Generic;

using SaveYourTower.GameEngine.GameLogic;

namespace SaveYourTower.GameEngine.DataContainers
{
    public class CollisionEventArgs : EventArgs
    {
        #region Properties

        public Collider MyCollider { get; private set; }
        public Collider OtherCollider { get; private set; }

        #endregion

        #region Constructors

        public CollisionEventArgs(Collider myCollider, Collider otherCollider)
        {
            MyCollider = myCollider;
            OtherCollider = otherCollider;
        }
 
        #endregion
    }
}
