using System;

using SaveYourTower.GameEngine.GameObjects.Base;

namespace SaveYourTower.GameEngine.DataContainers
{
    public class DieEventArgs : EventArgs
    {
        #region Properties

        public GameObject DeadGameObject { get; private set; }


        #endregion

        #region Constructors

        public DieEventArgs(GameObject gameObject)
        {
            DeadGameObject = gameObject;
        }
 
        #endregion
    }
}
