using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.GameObjects;

namespace SaveYourTower.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Save Your Tower\n\n\n\n\tPress any key to start");
            Console.ReadKey();

            Game game = new Game();
            game.Output += ReDraw;
            game.Input += Input;
            game.Start();

            if (game.IsExit)
            {
                Console.Clear();
                Console.WriteLine("Thanks for playing. \n\n\n \t \t Your score: {0}", game.GetScore());
            }

        }
        
        public static void Input(Game game)
        {
            switch (ListenKey())
                {
                    case ConsoleKey.Escape:
                        game.IsExit = true;
                        break;
                    case ConsoleKey.LeftArrow:
                        game.Rotate(45d / 180d * 3.14d);
                        break;
                    case ConsoleKey.RightArrow:
                        game.Rotate(-45d / 180d * 3.14d);
                        break;
                    case ConsoleKey.Spacebar:
                        game.Fire();
                        break;
                }
        }

        public static ConsoleKey ListenKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;
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

        public static void ReDraw(Field gameField)
        {
            int playerC = 0;
            Console.Clear();

            string output = "";

            for (var i = 0; i < gameField.Size.X; i++)
            {
                for (var j = 0; j < gameField.Size.Y; j++)
                {

                    foreach (var obj in gameField.GameObjects)
                    {
                        if (obj.Position.X == i && obj.Position.Y == j)
                        {
                            if (obj is Tower)
                            {
                                output += 'O';
                            }
                            if (obj is Enemy)
                                output += '*';
                            if (obj is CannonBall)
                                output += '.';
                            playerC++;

                        }
                    }

                    if (playerC <= 0)
                    {
                        if ((i == 0) || (j == 0) || (i == gameField.Size.Y - 1) || (j == gameField.Size.X - 1))
                            output += '▓';
                        else
                            output += " ";
                    }
                    playerC -= playerC <= 0 ? 0 : 1;
                }
                output += '\n';
            }

            var tower = gameField.GameObjects.Find(obj => { return (obj is Tower); } );
            Console.WriteLine("Score : {0} \t\t\t LifePoints: {1}", gameField.GameScore.Value, tower.LifePoints);
            Console.Write(output);
        }
    }
}
