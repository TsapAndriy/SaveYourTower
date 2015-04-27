using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.Base;
using SaveYourTower.GameEngine.GameObjects.Interfaces;
using Timer = System.Timers.Timer;

namespace SaveYourTower.GameEngine.Test
{
     [TestClass]
     public class GameTest
     {
         [TestMethod]
         public void TestConstructor()
         {
            Point size = new Point(10, 10);
            Game game = new Game(size, 1);

            Assert.IsNotNull(game.GameField);
            Assert.AreSame(size, game.GameField.Size);
            Assert.AreEqual(Status.IsReadyToRun, game.GameStatus);
            Assert.IsTrue(game.GameField.GameObjects.Exists(obj => obj is Tower));
         }

         [TestMethod]
         public void TestGetScore()
         {
             var game = new Game(new Point(10, 10), 1);

             game.GameField.GameScore.AddPoint(10);

             Assert.IsTrue(game.GetScore() == game.GameField.GameScore.Value);
         }

         [TestMethod]
         public void TestRotate()
         {
             Game game = new Game(new Point(10, 10), 1);
             game.Rotate(10d * Math.PI / 180);

             Tower tower = (Tower)game.GameField.GameObjects.Find( obj => { return (obj is Tower); });
             Assert.AreEqual(Math.Round(10d * Math.PI / 180), Math.Round(tower.Direction.Angle));
         }

         [TestMethod]
         public void TestFire()
         {
             Game game = new Game(new Point(10, 10), 1);
             game.Fire();
             ILive tower = (ILive)game.GameField.GameObjects.Find(obj => obj is Tower);
             tower.Live();
             Assert.AreEqual(2, game.GameField.GameObjects.Count);
             Assert.IsTrue(game.GameField.GameObjects.Exists( obj => (obj is CannonBall)));
         }

         [TestMethod]
         public void TestRemoveOutOfFieldObjects()
         {
             Game game = new Game(new Point(10, 10), 1);
             Mine mine1 = new Mine(game.GameField, new Point (-4, 2));
             Mine mine2 = new Mine(game.GameField, new Point(2, -4));
             Mine mine3 = new Mine(game.GameField, new Point(11, 2));
             Mine mine4 = new Mine(game.GameField, new Point(2, 11));

             game.GameField.AddGameObject(mine1);
             game.GameField.AddGameObject(mine2);
             game.GameField.AddGameObject(mine3);
             game.GameField.AddGameObject(mine4);

             Assert.IsNotNull(game.GameField.GameObjects.Find(obj => { return (obj is Mine); }));

             PrivateObject testGame = new PrivateObject(game);
             testGame.Invoke("RemoveOutOfFieldObjects");
             Assert.IsNull(game.GameField.GameObjects.Find(obj => { return (obj is Mine); }));
         }

         [TestMethod]
         public void TestSaleGameObject()
         {
             Game game = new Game(new Point(10, 10), 1);
             Mine mine = new Mine(game.GameField, new Point(-4, 2), 1, 2, 10);
             Turret turret = new Turret(game.GameField, new Point(-4, 2), 1, 1, 1, 1, 20);

             game.GameField.AddGameObject(mine);
             game.GameField.AddGameObject(turret);

             Assert.AreEqual(0, game.GetScore());
             game.SaleGameObject(mine);
             Assert.AreEqual(10, game.GetScore());
             game.SaleGameObject(turret);
             Assert.AreEqual(30, game.GetScore());
         }

         [TestMethod]
         [ExpectedException(typeof(InvalidOperationException))]
         public void TestBuyGameObject()
         {
             Game game = new Game(new Point(10, 10), 1);
             Mine mine1 = new Mine(game.GameField, new Point(4, 2), 1, 2, 10);
             Mine mine2 = new Mine(game.GameField, new Point(4, 2), 1, 2, 10);
             Turret turret = new Turret(game.GameField, new Point(5, 5), 1, 1, 1, 1, 20);

             Assert.AreEqual(BuingStatus.NeedMorePoints, game.BuyGameObject(mine1));
             game.GameField.GameScore.AddPoint(1000);
             Assert.AreEqual(BuingStatus.Success, game.BuyGameObject(mine1));
             Assert.AreEqual(BuingStatus.PlaceIsBusy, game.BuyGameObject(mine2));

             game.BuyGameObject(mine1);
         }

         [TestMethod]
         public void TestDoLevelWin()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.SetProperty("GameStatus", Status.IsWinnedLevel);

             Assert.AreEqual(Status.IsWinnedLevel, game.GameStatus);          
         }

         [TestMethod]
         public void TestCheckWin()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.Invoke("CheckLevelWin");
             Assert.AreNotEqual(Status.IsWinnedLevel, game.GameStatus);

             for (int i = 0; !game.GameEmeniesGenerator.EnemiesAreEnded; i++)
             {
                 game.GameEmeniesGenerator.Generate(game.GameField);
             }

             game.GameField.GameObjects.RemoveAll(obj => obj is Enemy);

             testGame.Invoke("CheckLevelWin");
             Assert.AreEqual(Status.IsWinnedLevel, game.GameStatus);
         }

         [TestMethod]
         public void TestCheckWinEvent()
         {
             bool eventHappend = false;
             Game game = new Game(new Point(10, 10), 1);
             PrivateObject privateGame = new PrivateObject(game);

             game.WinLevel += (() => eventHappend = true);

             for (int i = 0; !game.GameEmeniesGenerator.EnemiesAreEnded; i++)
             {
                 game.GameEmeniesGenerator.Generate(game.GameField);
             }
             game.GameField.GameObjects.RemoveAll(obj => obj is Enemy);

             privateGame.Invoke("CheckLevelWin");
             Assert.AreEqual(Status.IsWinnedLevel, game.GameStatus);
             Assert.IsTrue(eventHappend);
         }

         [TestMethod]
         public void TestCheckTowerLose()
         {
             Game game = new Game(new Point(10, 10), 1);
             Assert.AreNotEqual(Status.IsExit, game.GameStatus);

             PrivateObject testGame = new PrivateObject(game);
             Timer timer = (Timer)testGame.GetField("_gameTimer");
             timer.Start();             

             // Tower still alive.
             testGame.Invoke("CheckTowerLose");

             Assert.IsTrue(timer.Enabled);
             Assert.AreNotEqual(Status.IsExit, game.GameStatus);

             // Kill tower.
             game.GameField.GameObjects.RemoveAll(obj => obj is Tower);

             // Tower is dead.
             testGame.Invoke("CheckTowerLose");
             Assert.IsFalse(timer.Enabled);
             Assert.AreEqual(Status.IsExit, game.GameStatus);
         }

         [TestMethod]
         public void TestLifeCircle()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.SetField("_exitEvent", new ManualResetEvent(true));
          
             Assert.AreEqual(Status.IsReadyToRun, game.GameStatus);
             game.Run();
             Assert.AreEqual(Status.IsReadyToStart, game.GameStatus);
             game.Start();
             Assert.AreEqual(Status.IsStarted, game.GameStatus);
             game.Pause();
             Assert.AreEqual(Status.IsPaused, game.GameStatus);
             game.Restore();
             Assert.AreEqual(Status.IsStarted, game.GameStatus);
             game.Stop();
             Assert.AreEqual(Status.IsExit, game.GameStatus);
         }

         [TestMethod]
         [ExpectedException(typeof(InvalidOperationException))]
         public void TestWrongStatus1()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.SetField("_exitEvent", new ManualResetEvent(true));

             game.Start();
         }

         [TestMethod]
         [ExpectedException(typeof(InvalidOperationException))]
         public void TestWrongStatus2()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.SetField("_exitEvent", new ManualResetEvent(true));

             game.Stop();
         }

         [TestMethod]
         [ExpectedException(typeof(InvalidOperationException))]
         public void TestWrongStatus3()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.SetField("_exitEvent", new ManualResetEvent(true));

             game.Pause();
         }

         [TestMethod]
         [ExpectedException(typeof(InvalidOperationException))]
         public void TestWrongStatus4()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.SetField("_exitEvent", new ManualResetEvent(true));

             game.Restore();
         }

         [TestMethod]
         [ExpectedException(typeof(InvalidOperationException))]
         public void TestWrongStatus5()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject testGame = new PrivateObject(game);
             testGame.SetField("_exitEvent", new ManualResetEvent(true));

             game.Run();
             game.Run();
         }

         [TestMethod]
         [ExpectedException(typeof(InvalidOperationException))]
         public void TestNextLevelWrongStatus5()
         {
             Game game = new Game(new Point(10, 10), 1);

             game.NextLevel();
         }

         [TestMethod]
         public void TestNextLevel()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject privateGame = new PrivateObject(game);
             privateGame.SetProperty("GameStatus", Status.IsWinnedLevel);

             game.NextLevel();

             // Are there anything except enemy, turret or tower.
             Assert.IsFalse(game.GameField.GameObjects.Exists(obj =>
             {
                 return !((obj is Enemy) || (obj is Turret) || (obj is Tower));
             }));

             Assert.IsFalse(game.GameEmeniesGenerator.EnemiesAreEnded);
             Assert.AreEqual(Status.IsStarted, game.GameStatus);
         }

         [TestMethod]
         public void TestNextLevelAtWinnedGame()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject privateGame = new PrivateObject(game);
             privateGame.SetProperty("GameStatus", Status.IsWinnedLevel);
             
             int maxLevel = int.Parse(ConfigurationManager.AppSettings["MaxLevel"]);
             game.GameField.CurrenGameLevel = maxLevel;

             game.NextLevel();

             Assert.AreEqual(Status.IsWinned, game.GameStatus);
         }

         [TestMethod]
         public void TestUpdate_Live()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject privateGame = new PrivateObject(game);
             privateGame.SetProperty("GameStatus", Status.IsStarted);

             int maxLevel = int.Parse(ConfigurationManager.AppSettings["MaxLevel"]);
             game.GameField.CurrenGameLevel = maxLevel;

             GameObject outOfField = new Mine(game.GameField, new Point(-1, -2));
             GameObject mine1 = new Mine(game.GameField, new Point(2, 3));
             GameObject mine2 = new Mine(game.GameField, new Point(7, 7));
             GameObject enemy = new Enemy(game.GameField, new Point(3, 3));

             game.GameField.AddGameObject(outOfField);
             game.GameField.AddGameObject(mine1);
             game.GameField.AddGameObject(mine2);
             game.GameField.AddGameObject(enemy);

             game.Update(null, null);

             Assert.IsFalse(game.GameField.GameObjects.Contains(outOfField));
             Assert.IsTrue(game.GameField.GameObjects.Contains(mine1));
             Assert.IsTrue(game.GameField.GameObjects.Contains(mine2));
             Assert.IsTrue(game.GameField.GameObjects.Contains(enemy));

             game.Update(null, null);

             Assert.IsTrue(game.GameField.GameObjects.Contains(mine1));
             Assert.IsTrue(game.GameField.GameObjects.Contains(mine2));
             Assert.IsFalse(game.GameField.GameObjects.Contains(enemy)); 
             
             game.Update(null, null);

             Assert.IsFalse(game.GameField.GameObjects.Contains(mine1));
             Assert.IsTrue(game.GameField.GameObjects.Contains(mine2));
         }

         [TestMethod]
         public void TestUpdate_TowerLose()
         {
             Game game = new Game(new Point(10, 10), 1);

             PrivateObject privateGame = new PrivateObject(game);
             privateGame.SetProperty("GameStatus", Status.IsStarted);

             int maxLevel = int.Parse(ConfigurationManager.AppSettings["MaxLevel"]);
             game.GameField.CurrenGameLevel = maxLevel;

             GameObject tower = game.GameField.GameObjects.Find(obj => obj is Tower);

             tower.IsAlive = false;

             game.Update(null, null);

             Assert.AreEqual(Status.IsExit, game.GameStatus);
         }

         [TestMethod]
         public void TestUpdate_Events()
         {
             bool isInput = false;
             bool isOutput = false;

             Game game = new Game(new Point(10, 10), 1);

             game.Input += (obj => isInput = true);
             game.Output += (obj => isOutput = true);

             PrivateObject privateGame = new PrivateObject(game);
             privateGame.SetProperty("GameStatus", Status.IsStarted);

             game.Update(null, null);

             Assert.IsTrue(isInput);
             Assert.IsTrue(isOutput);
         }
     }
}
