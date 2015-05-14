using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Level
    {
        #region Properties

        public int Number { get; private set; }
        public int IterationLatency { get; private set; }
        public int EnemyGenerationLanency { get; private set; }
        public int MaxLevel { get; private set; }

        public int EnemyCount { get; private set; }
        public int EnemyDamage { get; private set; }
        public double EnemyVelocity { get; private set; }
        public int EnemyLife { get; private set; }
        public double EnemyPowerRising { get; private set; }

        public int TowerLife { get; private set; }

        public int TowerCannonBallLifeTime { get; private set; }
        public int TurretCannonBallLifeTime { get; private set; }

        public int TowerCannonDamage { get; private set; }
        public int TurretCannonDamage { get; private set; }

        public int AllHitSpellDamage { get; private set; }
        public int AllSlowSpellRatio { get; private set; }
        public int AllSlowSpellDuration { get; private set; }

        public int EnemyColliderRadius { get; private set; }
        public int TowerColliderRadius { get; private set; }
        public int TurretColliderRadius { get; private set; }
        public int CannonBallColliderRadius { get; private set; }
        public double TowerCannonBallVelosity { get; private set; }
        public double TurretCannonBallVelosity { get; private set; } 

        #endregion

        #region Constructors

        public Level(
        int number = 1,
        int iterationLatency = 20,
        int enemyGenerationLanency = 0,
        int maxLevel = 1,
        int enemyCount = 1,
        int enemyDamage = 1,
        double enemyVelocity = 1,
        int enemyLife = 1,
        double enemyPowerRising = 0,
        int towerLife = 1,
        int towerCannonBallLifeTime = 1,
        int turretCannonBallLifeTime = 1,
        int towerCannonDamage = 1,
        int turretCannonDamage = 1,
        int allHitSpellDamage = 1,
        int allSlowSpellRatio = 1,
        int allSlowSpellDuration = 1,
        int enemyColliderRadius = 1,
        int towerColliderRadius = 1,
        int turretColliderRadius = 1,
        int cannonBallColliderRadius = 1,
        double towerCannonBallVelosity = 10,
        double turretCannonBallVelosity = 10)
        {
            Number = number;
            IterationLatency = iterationLatency;
            EnemyGenerationLanency = enemyGenerationLanency;
            MaxLevel = maxLevel;
            EnemyCount = enemyCount;
            EnemyDamage = enemyDamage;
            EnemyVelocity = enemyVelocity;
            EnemyLife = enemyLife;
            EnemyPowerRising = enemyPowerRising;
            TowerLife = towerLife;
            TowerCannonBallLifeTime = towerCannonBallLifeTime;
            TurretCannonBallLifeTime = turretCannonBallLifeTime;
            TowerCannonDamage = towerCannonDamage;
            TurretCannonDamage = turretCannonDamage;
            AllHitSpellDamage = allHitSpellDamage;
            AllSlowSpellRatio = allSlowSpellRatio;
            AllSlowSpellDuration = allSlowSpellDuration;
            EnemyColliderRadius = enemyColliderRadius;
            TowerColliderRadius = towerColliderRadius;
            TurretColliderRadius = turretColliderRadius;
            CannonBallColliderRadius = cannonBallColliderRadius;
            TowerCannonBallVelosity = towerCannonBallVelosity;
            TurretCannonBallVelosity = turretCannonBallVelosity;
        } 

        #endregion
    }
}
