using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveYourTower.GameEngine.GameObjects
{
    public class Level
    {
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

        public Level(
                int number,
                int iterationLatency,
                int enemyGenerationLanency,
                int maxLevel,
                int enemyCount,
                int enemyDamage,
                double enemyVelocity,
                int enemyLife,
                double enemyPowerRising,
                int towerLife,
                int towerCannonBallLifeTime,
                int turretCannonBallLifeTime,
                int towerCannonDamage,
                int turretCannonDamage,
                int allHitSpellDamage,
                int allSlowSpellRatio,
                int allSlowSpellDuration,
                int enemyColliderRadius,
                int towerColliderRadius,
                int turretColliderRadius,
                int cannonBallColliderRadius,
                double towerCannonBallVelosity,
                double turretCannonBallVelosity)
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
















    }
}
