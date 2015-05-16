using System;
using System.Configuration;

using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects.RealObjects;

namespace SaveYourTower.GameEngine.GameLogic
{
    public class EnemiesGenerator
    {
        #region Fields
        private Level _level;
        private int _iterationCounter;
        private int _enemiesCounter;
        Random _rand = new Random();

        #endregion

        #region Properties

        public bool EnemiesAreEnded { get; private set; }

        #endregion

        #region Constructors

        public EnemiesGenerator(Level level)
        {
            _level = level;
        }

        #endregion

        #region Methods

        public void Generate(Field gameField)
        {
            if ((_iterationCounter >= _level.EnemyGenerationLanency)
                && !EnemiesAreEnded)
            {
                _iterationCounter = 0;
                _enemiesCounter++;

                gameField.AddGameObject(new Enemy(
                    gameField,
                    StickToTheSide(gameField),
                    _level.EnemyColliderRadius,
                    _level.EnemyVelocity,
                    _level.EnemyDamage,
                    _level.EnemyLife
                    ));
            }

            if (_enemiesCounter >= _level.EnemyCount)
            {
                EnemiesAreEnded = true;
            }

           _iterationCounter++;
        }

        private Point StickToTheSide(Field gameField)
        {
            Point sidePosition = null; 

            switch (_rand.Next(0, 4))
            {
                case 0:
                    sidePosition = UpSide(gameField);
                    break;
                case 1:
                    sidePosition = RightSide(gameField);
                    break;
                case 2:
                    sidePosition = DownSide(gameField);
                    break;
                case 3:
                    sidePosition = LeftSide(gameField);
                    break;
            }

            return sidePosition;
        }

        private Point UpSide(Field gameField)
        {
           return new Point(1, _rand.Next(0, (int)(gameField.Size.Y)));
        }

        private Point RightSide(Field gameField)
        {
            return new Point(_rand.Next(0, (int)(gameField.Size.X)), 1);
        }

        private Point DownSide(Field gameField)
        {
            return new Point((gameField.Size.X - 1),
                _rand.Next(1, (int)(gameField.Size.Y)));
        }

        private Point LeftSide(Field gameField)
        {
            return new Point(_rand.Next(1, (int)(gameField.Size.X)),
                (gameField.Size.Y - 1));
        }

        #endregion
    }
}
