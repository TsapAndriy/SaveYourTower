using System;
using System.Text;

using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Spells;

namespace SaveYourTower.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var levels = LoadLevels();

            Game game = new Game(new Point(25, 50), levels);
            game.OutputEventHandler += OutputEventHandler;
            game.InputEventHandler += InputEventHandler;
            game.Run();
            
            if (game.GameStatus == Status.IsExit)
            {
                Console.Clear();
                Console.WriteLine("Thanks for playing. \n\n\n \t \t Your score: {0}", game.GetScore());
            }
        }

        static private Level[] LoadLevels()
        {
            Level[] levels = new Level[LevelsSettings.Default.MaxLevel];

            levels[0] = new Level(
                    1,
                    LevelsSettings.Default.IterationLatency,
                    LevelsSettings.Default.EnemyGenerationLanency,
                    LevelsSettings.Default.MaxLevel,
                    LevelsSettings.Default.Level1EnemyCount,
                    LevelsSettings.Default.Level1EnemyDamage,
                    LevelsSettings.Default.Level1EnemyVelocity,
                    LevelsSettings.Default.Level1EnemyLife,
                    LevelsSettings.Default.Level1EnemyPowerRising,
                    LevelsSettings.Default.TowerLife,
                    LevelsSettings.Default.TowerCannonBallLifeTime,
                    LevelsSettings.Default.TurretCannonBallLifeTime,
                    LevelsSettings.Default.TowerCannonDamage,
                    LevelsSettings.Default.TurretCannonDamage,
                    LevelsSettings.Default.AllHitSpellDamage,
                    LevelsSettings.Default.AllSlowSpellRatio,
                    LevelsSettings.Default.AllSlowSpellDuration,
                    LevelsSettings.Default.EnemyCollierRadius,
                    LevelsSettings.Default.TowerColliderRadius,
                    LevelsSettings.Default.TurretColliderRadius,
                    LevelsSettings.Default.CannonBallColliderRadius,
                    LevelsSettings.Default.TowerCannonBallVelosity,
                    LevelsSettings.Default.TurretCannonBallVelosity);

            levels[1] = new Level(
                    2,
                    LevelsSettings.Default.IterationLatency,
                    LevelsSettings.Default.EnemyGenerationLanency,
                    LevelsSettings.Default.MaxLevel,
                    LevelsSettings.Default.Level2EnemyCount,
                    LevelsSettings.Default.Level2EnemyDamage,
                    LevelsSettings.Default.Level2EnemyVelocity,
                    LevelsSettings.Default.Level2EnemyLife,
                    LevelsSettings.Default.Level2EnemyPowerRising,
                    LevelsSettings.Default.TowerLife,
                    LevelsSettings.Default.TowerCannonBallLifeTime,
                    LevelsSettings.Default.TurretCannonBallLifeTime,
                    LevelsSettings.Default.TowerCannonDamage,
                    LevelsSettings.Default.TurretCannonDamage,
                    LevelsSettings.Default.AllHitSpellDamage,
                    LevelsSettings.Default.AllSlowSpellRatio,
                    LevelsSettings.Default.AllSlowSpellDuration,
                    LevelsSettings.Default.EnemyCollierRadius,
                    LevelsSettings.Default.TowerColliderRadius,
                    LevelsSettings.Default.TurretColliderRadius,
                    LevelsSettings.Default.CannonBallColliderRadius,
                    LevelsSettings.Default.TowerCannonBallVelosity,
                    LevelsSettings.Default.TurretCannonBallVelosity);

            levels[2] = new Level(
                    3,
                    LevelsSettings.Default.IterationLatency,
                    LevelsSettings.Default.EnemyGenerationLanency,
                    LevelsSettings.Default.MaxLevel,
                    LevelsSettings.Default.Level3EnemyCount,
                    LevelsSettings.Default.Level3EnemyDamage,
                    LevelsSettings.Default.Level3EnemyVelocity,
                    LevelsSettings.Default.Level3EnemyLife,
                    LevelsSettings.Default.Level3EnemyPowerRising,
                    LevelsSettings.Default.TowerLife,
                    LevelsSettings.Default.TowerCannonBallLifeTime,
                    LevelsSettings.Default.TurretCannonBallLifeTime,
                    LevelsSettings.Default.TowerCannonDamage,
                    LevelsSettings.Default.TurretCannonDamage,
                    LevelsSettings.Default.AllHitSpellDamage,
                    LevelsSettings.Default.AllSlowSpellRatio,
                    LevelsSettings.Default.AllSlowSpellDuration,
                    LevelsSettings.Default.EnemyCollierRadius,
                    LevelsSettings.Default.TowerColliderRadius,
                    LevelsSettings.Default.TurretColliderRadius,
                    LevelsSettings.Default.CannonBallColliderRadius,
                    LevelsSettings.Default.TowerCannonBallVelosity,
                    LevelsSettings.Default.TurretCannonBallVelosity);

            return levels;
        }

        public static void InputEventHandler(object sender, EventArgs e)
        {
            Game game = sender as Game;
            
            if (game.GameStatus == Status.IsReadyToStart)
            {
                StartInput(game);
            }
            else if (game.GameStatus == Status.IsStarted)
            {
                PlayingInput(game, new EventArgs());
            }
            else if (game.GameStatus == Status.IsPaused)
            {
                PlayingInput(game, new EventArgs());
            }
            else if (game.GameStatus == Status.IsWinnedLevel)
            {
                WinLevelInput(game);
            }
            else if (game.GameStatus == Status.IsWinned)
            {
                WinInput(game);
            }
        }

        public static void StartInput(Game game)
        {
            if (Console.KeyAvailable)
            {
                ClearInputKeysBuffer();
                game.Start();
            }
        }

        public static void PlayingInput(object sender, EventArgs e)
        {
            Game game = sender as Game;
            if (game == null)
            {
                throw new InvalidOperationException("No sender of output event");
            }

            switch (ListenKey())
            {
                case ConsoleKey.Escape:
                    game.Stop();
                    break;
                case ConsoleKey.P:
                    if (game.GameStatus == Status.IsStarted)
                    {
                        game.Pause();
                    }
                    else if (game.GameStatus == Status.IsPaused)
                    {
                        game.Restore();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    game.Rotate(10d / 180d * Math.PI);
                    break;
                case ConsoleKey.RightArrow:
                    game.Rotate(-10d / 180d * Math.PI);
                    break;
                case ConsoleKey.Spacebar:
                    game.Fire();
                    break;
                case ConsoleKey.D1:
                    game.BuyGameObject(new Turret(game.GameField, AskPosition(game.GameField), 1, 5, 30, cost: 1));
                    break;
                case ConsoleKey.D3:
                    AllHilSpell allHilSpell = new AllHilSpell(game.GameField, 100, 10);
                    if (game.BuyGameObject(allHilSpell) == BuingStatus.Success)
                    {
                        allHilSpell.Cast();
                    }
                    break;
                case ConsoleKey.D4:
                    AllSlowSpell allSlowSpell = new AllSlowSpell(game.GameField, 100, 10);
                    if (game.BuyGameObject(allSlowSpell) == BuingStatus.Success)
                    {
                        allSlowSpell.Cast();
                    }
                    break;
                case ConsoleKey.D:
                    Point objectPosition = AskPosition(game.GameField);
                    GameObject gameObject = game.GameField.GameObjects.Find(obj =>
                    {
                        return (obj.Position.Equals(objectPosition));
                    });
                    game.SaleGameObject(gameObject);
                    break;
            }
        }

        public static void WinLevelInput(Game game)
        {
            switch (ListenKey())
            {
                case ConsoleKey.Escape:
                    game.Stop();
                    break;
                case ConsoleKey.Enter:
                    game.NextLevel();
                    break;
            }
        }

        public static void WinInput(Game game)
        {
            switch (ListenKey())
            {
                case ConsoleKey.Escape:
                    game.Stop();
                    break;
            }
        }

        public static void OutputEventHandler(object sender, EventArgs e)
        {
            Game game = sender as Game;
            if (game == null)
            {
                throw new InvalidOperationException("No sender of output event");
            }

            if (game.GameStatus == Status.IsReadyToStart)
            {
                StartOutput(game.GameField);
            }
            else if (game.GameStatus == Status.IsStarted)
            {
                PlayingOutput(game.GameField);
            }
            else if (game.GameStatus == Status.IsWinnedLevel)
            {
                WinLevelOutput(game.GameField);
            }
            else if (game.GameStatus == Status.IsWinned)
            {
                WinOutput(game.GameField);
            }
        }

        public static void StartOutput(Field gameField)
        {
            Console.Clear();
            Console.WriteLine("Save Your Tower\n\n\n\n\tPress any key to start");
        }

        public static void PlayingOutput(Field gameField)
        {
            int playerC = 0;
            Console.Clear();
            StringBuilder output = new StringBuilder();

            for (var i = 0; i < gameField.Size.X; i++)
            {
                for (var j = 0; j < gameField.Size.Y; j++)
                {
                    foreach (var obj in gameField.GameObjects)
                    {
                        if ((int)obj.Position.X == i && (int)obj.Position.Y == j)
                        {
                            if (obj is Tower)
                            {
                                output.Append('0');
                            }
                            else if (obj is Enemy)
                            {
                                output.Append('*');
                            }
                            else if (obj is CannonBall)
                            {
                                output.Append('.');
                            }
                            else if (obj is Turret)
                            {
                                output.Append('o');
                            }
                            playerC++;
                            break;
                        }
                    }

                    if (playerC <= 0)
                    {
                        if ((i == 0) || (j == 0) || (i == (int)gameField.Size.X - 1) || (j == (int)gameField.Size.Y - 1))
                            output.Append('▓');
                        else
                            output.Append(" ");
                    }
                    playerC -= playerC <= 0 ? 0 : 1;
                }
                output.Append('\n');
            }

            var tower = gameField.GameObjects.Find(obj => { return (obj is Tower); });
            Console.WriteLine("Score : {0} \t\tAngle:{1:00}\t\t LifePoints: {2}", gameField.GameScore.Value, tower.Direction.Angle * 180 / 3.14, tower.LifePoints);

            PrintHelp(ref output);

            Console.Write(output);
        }

        public static void WinLevelOutput(Field gameField)
        {
            Console.Clear();
            StringBuilder output = new StringBuilder();
            output.AppendFormat(
                "You win lever : {0}, \n Score : {1} \nPress Enter to load next level, or ESK to exit",
                gameField.CurrenGameLevel.Number,
                gameField.GameScore.Value
                );
            Console.Write(output);
        }

        public static void WinOutput(Field gameField)
        {
            Console.Clear();
            StringBuilder output = new StringBuilder();
            output.AppendFormat(
                "Congratulations. You win!!! \n your score : {0} \nPress ESK to exit",
                gameField.GameScore.Value
                );
            Console.Write(output);
        }

        public static void PrintHelp(ref StringBuilder str)
        {
            str.Append("Basic control :\n");

            str.Append("\tleft/right arrows : rotate\n");
            str.Append("\tspacebar : fire\n");
            str.Append("\tD : sell object\n");

            str.Append("\nPosibilities :\n");
            str.Append("\t1 : turret\n");
            str.Append("\t2 : mine\n");
            str.Append("\t3 : hitting spell\n");
            str.Append("\t4 : slowing spell\n");


            str.Append("\nGeneral control :\n");
            str.Append("\tP : pause\n");
            str.Append("\tESC : exit\n");
        }

        public static ConsoleKey ListenKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                ClearInputKeysBuffer();
                return key;
            }
            return 0;
        }

        public static void ClearInputKeysBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        public static Point AskPosition(Field gameField)
        {
            Console.CursorSize = 100;
            Point position = new Point((gameField.Size.X / 2), (gameField.Size.Y / 2));

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        position.X -= ((position.X > 2) ? 1 : 0);
                        break;
                    case ConsoleKey.DownArrow:
                        position.X += ((position.X < (gameField.Size.X - 2)) ? 1 : 0);
                        break;
                    case ConsoleKey.LeftArrow:
                        position.Y -= ((position.Y > 1) ? 1 : 0);
                        break;
                    case ConsoleKey.RightArrow:
                        position.Y += ((position.Y < (gameField.Size.Y - 2)) ? 1 : 0);
                        break;
                    case ConsoleKey.Enter:
                        return position;
                    case ConsoleKey.Escape:
                        return null;
                }
                //OutputEventHandler(gameField);
                Console.SetCursorPosition((int)position.Y, (int)position.X + 1);
            }
        }
    }
}
