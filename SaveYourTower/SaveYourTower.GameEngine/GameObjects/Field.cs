using System.Collections.Generic;

using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Field
    {
        #region Properties

        public Point Size { get; private set; }
        public Score GameScore { get; private set; }
        public List<GameObject> GameObjects { get; private set; }
        public Level CurrenGameLevel { get; set; }
        public double VelositiDivisor { get; set; }

        #endregion

        #region Constructors

        public Field(Point size, Level gameLevel)
        {
            Size = size;
            GameScore = new Score();
            GameObjects = new List<GameObject>();
            CurrenGameLevel = gameLevel;
            VelositiDivisor = 1;
        } 

        #endregion

        #region Methods

        public void AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject != null)
            {
                GameObjects.Remove(gameObject);
            }
        }

        public void Clean()
        {
            GameObjects.Clear();
        }

        #endregion
    }
}
