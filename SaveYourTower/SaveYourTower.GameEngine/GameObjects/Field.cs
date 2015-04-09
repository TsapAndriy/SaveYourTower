using System;
using System.Collections.Generic;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.Interfaces;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Field
    {
        public Point Size { get; private set; }
        public Score GameScore { get; private set; }
		public List<GameObject> GameObjects { get; private set; }
		
		public Field(Point size)
		{
			Size = size;
            GameScore = new Score();
			GameObjects = new List<GameObject>();
		}
		
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
    }
}
