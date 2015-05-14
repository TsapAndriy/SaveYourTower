using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveYourTower.GameEngine;
using SaveYourTower.GameEngine.DataContainers;
using SaveYourTower.GameEngine.GameObjects;
using SaveYourTower.GameEngine.GameObjects.RealObjects;
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
            Level[] levels = { new Level() };
            Game game = new Game(size, levels);

            Assert.IsNotNull(game.GameField);
            Assert.AreSame(size, game.GameField.Size);
            Assert.AreEqual(Status.IsReadyToRun, game.GameStatus);
            Assert.IsTrue(game.GameField.GameObjects.Exists(obj => obj is Tower));
        }

        [TestMethod]
        public void TestGetScore()
        {
            Level[] levels = { new Level() };
            var game = new Game(new Point(10, 10), levels);

            game.GameField.GameScore.AddPoint(10);

            Assert.IsTrue(game.GetScore() == game.GameField.GameScore.Value);
        }

        [TestMethod]
        public void TestRotate()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            game.Rotate(10d * Math.PI / 180);

            Tower tower = (Tower)game.GameField.GameObjects.Find(obj =>
                obj is Tower);

            Assert.AreEqual(Math.Round(10d * Math.PI / 180), 
                Math.Round(tower.Direction.Angle));
        }

        [TestMethod]
        public void TestFire()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            game.Fire();

            ILive tower = (ILive)game.GameField.GameObjects.Find(obj => 
                obj is Tower);

            tower.Live();

            Assert.AreEqual(2, game.GameField.GameObjects.Count);
            Assert.IsTrue(game.GameField.GameObjects.Exists(obj => (obj is CannonBall)));
        }

        [TestMethod]
        public void TestRemoveOutOfFieldObjects()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            PrivateObject testGame = new PrivateObject(game);
            testGame.Invoke("RemoveOutOfFieldObjects");
        }

        [TestMethod]
        public void TestBuyGameObject()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);
            Turret turret = new Turret(game.GameField, new Point(1, 1), 1, 1, 1, 1, 20);

            game.GameField.GameScore.AddPoint(1000);

            BuingStatus buingStatus = game.BuyGameObject(turret);

            Assert.AreEqual(980, game.GetScore());
            Assert.AreEqual(BuingStatus.Success, buingStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestBuyGameObjectObjectExists()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);
            Turret turret = new Turret(game.GameField, new Point(1, 1), 1, 1, 1, 1, 20);

            game.GameField.GameScore.AddPoint(1000);

            game.BuyGameObject(turret);
            game.BuyGameObject(turret);
        }

        [TestMethod]
        public void TestBuyGameObjectPlaceIsBusy()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);
            Turret turret = new Turret(game.GameField, new Point(4, 4), 1, 1, 1, 1, 20);

            game.GameField.GameScore.AddPoint(1000);

            BuingStatus buingStatus = game.BuyGameObject(turret);

            Assert.AreEqual(1000, game.GetScore());
            Assert.AreEqual(BuingStatus.PlaceIsBusy, buingStatus);
        }

        [TestMethod]
        public void TestBuyGameObjectNeedMorePoints()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);
            Turret turret = new Turret(game.GameField, new Point(1, 1), 1, 1, 1, 1, 20);

            BuingStatus buingStatus = game.BuyGameObject(turret);

            Assert.AreEqual(0, game.GetScore());
            Assert.AreEqual(BuingStatus.NeedMorePoints, buingStatus);
        }

        [TestMethod]
        public void TestDoLevelWin()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            PrivateObject testGame = new PrivateObject(game);
            testGame.SetProperty("GameStatus", Status.IsWinnedLevel);

            Assert.AreEqual(Status.IsWinnedLevel, game.GameStatus);
        }

        [TestMethod]
        public void TestCheckWin()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

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
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);
            PrivateObject privateGame = new PrivateObject(game);

            game.WinLevelEventHandler += ((sender, e) => eventHappend = true);

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
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);
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
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

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
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            PrivateObject testGame = new PrivateObject(game);
            testGame.SetField("_exitEvent", new ManualResetEvent(true));

            game.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWrongStatus2()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            PrivateObject testGame = new PrivateObject(game);
            testGame.SetField("_exitEvent", new ManualResetEvent(true));

            game.Stop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWrongStatus3()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            PrivateObject testGame = new PrivateObject(game);
            testGame.SetField("_exitEvent", new ManualResetEvent(true));

            game.Pause();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWrongStatus4()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            PrivateObject testGame = new PrivateObject(game);
            testGame.SetField("_exitEvent", new ManualResetEvent(true));

            game.Restore();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestWrongStatus5()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            PrivateObject testGame = new PrivateObject(game);
            testGame.SetField("_exitEvent", new ManualResetEvent(true));

            game.Run();
            game.Run();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestNextLevelWrongStatus5()
        {
            Level[] levels = { new Level() };
            Game game = new Game(new Point(10, 10), levels);

            game.NextLevel();
        }

        [TestMethod]
        public void TestNextLevel()
        {
            Level[] levels = { new Level(maxLevel: 3), new Level(maxLevel: 3), new Level(maxLevel: 3) };
            Game game = new Game(new Point(10, 10), levels);

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
            Level[] levels = { new Level(number:1, maxLevel: 3), 
                new Level(number:2, maxLevel: 3), 
                new Level(number: 3, maxLevel: 3) };

            Game game = new Game(new Point(10, 10), levels);

            PrivateObject privateGame = new PrivateObject(game);
            privateGame.SetProperty("GameStatus", Status.IsWinnedLevel);

            game.GameField.CurrenGameLevel = levels[game.GameField.CurrenGameLevel.MaxLevel - 1];

            game.NextLevel();

            Assert.AreEqual(Status.IsWinned, game.GameStatus);
        }

        [TestMethod]
        public void TestUpdate_Live()
        {
            Level[] levels = { new Level(number: 1, maxLevel: 3),
                new Level(number: 2, maxLevel: 3), 
                new Level(number: 3, maxLevel: 3) };

            Game game = new Game(new Point(10, 10), levels);

            PrivateObject privateGame = new PrivateObject(game);
            privateGame.SetProperty("GameStatus", Status.IsStarted);

            game.GameField.CurrenGameLevel = levels[game.GameField.CurrenGameLevel.MaxLevel - 1];

            GameObject enemy = new Enemy(game.GameField, new Point(3, 3));

            game.GameField.AddGameObject(enemy);

            game.Update(null, null);

            Assert.IsTrue(game.GameField.GameObjects.Contains(enemy));

            game.Update(null, null);

            Assert.IsFalse(game.GameField.GameObjects.Contains(enemy));

            game.Update(null, null);
        }

        [TestMethod]
        public void TestUpdate_TowerLose()
        {
            Level[] levels = { new Level(number: 1, maxLevel: 3),
                new Level(number: 2, maxLevel: 3), 
                new Level(number: 3, maxLevel: 3) };

            Game game = new Game(new Point(10, 10), levels);

            PrivateObject privateGame = new PrivateObject(game);
            privateGame.SetProperty("GameStatus", Status.IsStarted);

            game.GameField.CurrenGameLevel = levels[game.GameField.CurrenGameLevel.MaxLevel - 1];

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

            Level[] levels = { new Level(number: 1, maxLevel: 3),
                new Level(number: 2, maxLevel: 3), 
                new Level(number: 3, maxLevel: 3) };

            Game game = new Game(new Point(10, 10), levels);

            game.InputEventHandler += ((sender, e) => isInput = true);
            game.OutputEventHandler += ((sender, e) => isOutput = true);

            PrivateObject privateGame = new PrivateObject(game);
            privateGame.SetProperty("GameStatus", Status.IsStarted);

            game.Update(null, null);

            Assert.IsTrue(isInput);
            Assert.IsTrue(isOutput);
        }

        [TestMethod]
        public void TestDie_Event()
        {
            bool isDie = false;

            Level[] levels = { new Level(number: 1, maxLevel: 3),
                new Level(number: 2, maxLevel: 3), 
                new Level(number: 3, maxLevel: 3) };

            Game game = new Game(new Point(10, 10), levels);
            game.GameField.AddGameObject(new Enemy(game.GameField, new Point(1, 1), lifePoints: -10));

            game.DieEventHandler += ((sender, e) => isDie = true);

            PrivateObject privateGame = new PrivateObject(game);
            privateGame.SetProperty("GameStatus", Status.IsStarted);

            game.Update(null, null);
            game.Update(null, null);

            Assert.IsTrue(isDie);
        }
    }
}
