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

        private int _enemyGenerationLatency;
        private int _iterationCounter;
        private int _enemiesCounter;
        Random _rand = new Random();
        private double _power;

        #endregion

        #region Properties

        public bool EnemiesAreEnded { get; private set; }

        #endregion

        #region Constructors

        public EnemiesGenerator()
        {
            //_enemyGenerationLatency = latency;
            _enemyGenerationLatency = int.Parse(ConfigurationManager.AppSettings["EnemyGenerationLanency"]);
        }

        #endregion

        #region Methods

        public void Generate(Field gameField)
        {
            if (_iterationCounter < _enemyGenerationLatency)
            {
                _iterationCounter++;
                return;
            }
            else
            {
                _iterationCounter = 0;
            }

            if (Level1(gameField))
            {
                gameField.AddGameObject(new Enemy(
                    gameField, 
                    StickToTheSide(gameField),
                    8,
                    GetDoubleFromConfig("Level1EnemyVelocity"),
                    GetIntFromConfig("Level1EnemyDamage"),
                    GetIntFromConfig("Level1EnemyLife")
                    ));

                _enemiesCounter++;
                _power += double.Parse(ConfigurationManager.AppSettings["Level1EnemyPowerRising"]);
            }
            else if (Level2(gameField))
            {
                gameField.AddGameObject(new Enemy(
                    gameField, 
                    StickToTheSide(gameField), 
                    8, 
                    GetDoubleFromConfig("Level2EnemyVelocity"),
                    GetIntFromConfig("Level2EnemyDamage"),
                    GetIntFromConfig("Level2EnemyLife")
                    ));

                _enemiesCounter++;
                _power += double.Parse(ConfigurationManager.AppSettings["Level2EnemyPowerRising"]);
            }
            else if (Level3(gameField))
            {
                gameField.AddGameObject(new Enemy(
                    gameField, 
                    StickToTheSide(gameField), 
                    8,
                    GetDoubleFromConfig("Level3EnemyVelocity"),
                    GetIntFromConfig("Level3EnemyDamage"),
                    GetIntFromConfig("Level3EnemyLife")
                    ));

                _enemiesCounter++;
                _power += double.Parse(ConfigurationManager.AppSettings["Level3EnemyPowerRising"]);
            }
            else
            {
                EnemiesAreEnded = true;
            }
        }

        private bool Level1(Field gameField)
        {
            return (gameField.CurrenGameLevel == 1)
                && (_enemiesCounter < GetIntFromConfig("Level1EnemyCount"));
        }

        private bool Level2(Field gameField)
        {
            return (gameField.CurrenGameLevel == 2)
                && (_enemiesCounter < GetIntFromConfig("Level2EnemyCount"));
        }

        private bool Level3(Field gameField)
        {
            return (gameField.CurrenGameLevel == 3)
                && (_enemiesCounter < GetIntFromConfig("Level3EnemyCount"));
        }

        private int GetIntFromConfig(string key)
        {
            return int.Parse(ConfigurationManager.AppSettings[key]);
        }

        private double GetDoubleFromConfig(string key)
        {
            return double.Parse(ConfigurationManager.AppSettings[key]);
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
