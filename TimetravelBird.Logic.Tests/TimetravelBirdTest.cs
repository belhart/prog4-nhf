// <copyright file="TimetravelBirdTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit;
    using NUnit.Framework;
    using TimetravelBird.Logic;
    using TimetravelBird.Model;

    /// <summary>
    /// Basic class for the logic tests.
    /// </summary>
    [TestFixture]
    class TimetravelBirdTest
    {
        private TimeTravelBirdLogic logic;

        private Mock<ITimetravelBirdModel> modelMock;

        /// <summary>
        /// Setup the mock.
        /// </summary>
        [SetUp]
        public void Init()
        {
            List<OneBackground> backgrounds = new List<OneBackground>()
            {
                new OneBackground(0, 0),
                new OneBackground(50, 0),
                new OneBackground(200, 0),
                new OneBackground(1000, 0),
            };
            this.modelMock = new Mock<ITimetravelBirdModel>();
            /*TimetravelBirdModel modelteszt = new TimetravelBirdModel(-1)
            {
                GameWidth = 1240,
                GameHeight = 720,
                Money = 0,
                Meter = 0,
                HighestMeter = 0,
                Height = 0,
                UpgradeItem1 = new Item(Config.UpgradeItem1Name, 100, null),
                UpgradeItem2 = new Item(Config.UpgradeItem2Name, 100, null),
                UpgradeItem3 = new Item(Config.UpgradeItem3Name, 100, null),
                UpgradeItem4 = new Item(Config.UpgradeItem4Name, 250, null),
                UpgradeItem5 = new Item(Config.UpgradeItem5Name, 110, null),
                UpgradeItem6 = new Item(Config.UpgradeItem6Name, 200, null),
                UpgradeItem7 = new Item(Config.UpgradeItem7Name, 150, null),
                UpgradeItem8 = new Item(Config.UpgradeItem8Name, 1000, null),
                weather = new Weather(30, 30),
                Bird = Config.DefaultBird,
                CurrentLevel = 0,
                Launcher = new Launcher(100, 20),
                backgrounds = backgrounds,
            };*/
            Bird bird = new Bird(10, 10, 10, 5, 30, 10, 560, @"TimetravelBird.Renderer\assets\birds\pinkyv2.gif");
            this.modelMock.SetupProperty(x => x.CurrentLevel, 0);
            this.modelMock.SetupProperty(x => x.Bird, bird);
            this.modelMock.SetupProperty(x => x.Backgrounds, backgrounds);
            this.modelMock.SetupProperty(x => x.Height, 0);
            this.modelMock.SetupProperty(x => x.Launcher, new Launcher(300, 30));
            this.modelMock.SetupProperty(x => x.Weather, new Weather(30, 30));
            this.modelMock.SetupProperty(x => x.HighestMeter, 0);
            this.modelMock.SetupProperty(x => x.UpgradeItem1, new Item("Item 1", 100));
            this.modelMock.SetupProperty(x => x.UpgradeItem2, new Item("Item 2", 100));
            this.modelMock.SetupProperty(x => x.UpgradeItem3, new Item("Item 3", 100));
            this.modelMock.SetupProperty(x => x.UpgradeItem4, new Item("Item 4", 100));
            this.modelMock.SetupProperty(x => x.UpgradeItem5, new Item("Item 5", 100));
            this.modelMock.SetupProperty(x => x.UpgradeItem6, new Item("Item 6", 100));
            this.modelMock.SetupProperty(x => x.UpgradeItem7, new Item("Item 7", 100));
            this.modelMock.SetupProperty(x => x.UpgradeItem8, new Item("Item 8", 100));
            this.modelMock.SetupProperty(x => x.Launches, 1);
            this.modelMock.SetupProperty(x => x.GameStartingTime, DateTime.Now);
            this.modelMock.SetupProperty(x => x.LoadedGameName, "testsave");
            this.modelMock.SetupProperty(x => x.Wind, new Wind(1280));
            this.logic = new TimeTravelBirdLogic(this.modelMock.Object);
        }

        /// <summary>
        /// Tests if the bird is in the sky.
        /// </summary>
        [Test]
        public void LogicInitInSkyTest()
        {
            bool logic = this.logic.InSky;
            Assert.IsFalse(logic);
        }

        /// <summary>
        /// Tests if the launcher changes the parameters after launch.
        /// </summary>
        [Test]
        public void LogicInitLaunchesTest()
        {
            var bird = this.modelMock.Object.Bird;
            this.logic.LauncherShoot();
            Assert.AreEqual(bird.DX, 45);
            Assert.AreEqual(bird.DY, 13);
            Assert.AreEqual(this.modelMock.Object.Bird.Fuel, 18);
        }

        /// <summary>
        /// Checks if the bird resets correctly.
        /// </summary>
        [Test]
        public void LogicBirdReset()
        {
            var bird = this.modelMock.Object.Bird;
            this.logic.LauncherShoot();
            this.logic.OneTick();
            this.logic.BirdReset();
            Assert.AreEqual(bird.DX, 15);
            Assert.AreEqual(bird.DY, 10);
            Assert.AreEqual(this.modelMock.Object.Launches, 2);
        }

        /// <summary>
        /// Checks if the bird parameters changes correctly after birdtick.
        /// </summary>
        [Test]
        public void LogicBirdTickTest()
        {
            var bird = this.modelMock.Object.Bird;
            this.logic.BirdTick();
            this.logic.BirdTick();
            var realx = this.modelMock.Object.Bird.RealX;
            var rad = this.modelMock.Object.Bird.Rad;
            Assert.AreEqual(realx, 30);
            Assert.AreEqual(rad, Math.PI / 1800);
        }

        /// <summary>
        /// Checks if the bird's rotates after collision.
        /// </summary>
        [Test]
        public void LogicOneTickTest()
        {
            var bird = this.modelMock.Object.Bird;
            this.logic.OneTick();
            this.modelMock.Object.Bird.IsCollision(this.modelMock.Object.Wind);
            var rad = this.modelMock.Object.Bird.Rad;
            var rad2 = (Math.PI / 60) * (1 - (this.modelMock.Object.Bird.WeatherResistance / 100));
            Assert.Greater(rad, rad2);
        }

        /// <summary>
        /// Checks if the bird's sets up correctly.
        /// </summary>
        [Test]
        public void ModelBirdSetupTest()
        {
            var bird = this.modelMock.Object.Bird;
            Assert.AreEqual(Config.DefaultBird.FileNameToGif, bird.FileNameToGif);
        }

        /// <summary>
        /// Checks bird's dy parameter after boost.
        /// </summary>
        [Test]
        public void LogicBoostMethodDYTest()
        {
            var bird = this.modelMock.Object.Bird;
            this.logic.Launch = false;
            this.logic.BirdBoost();
            Assert.AreEqual(9.5, bird.DY);
        }

        /// <summary>
        /// Checks bird's dx parameter after boost.
        /// </summary>
        [Test]
        public void LogicBoostMethodDXTest()
        {
            var bird = this.modelMock.Object.Bird;
            this.logic.Launch = false;
            this.logic.BirdBoost();
            Assert.AreEqual(28, bird.Fuel);
            Assert.AreEqual(bird.DX, 15.3);

            // this.modelMock.Verify(x => x.Bird.DX);
            this.modelMock.Verify(x => x.Bird, Times.AtLeastOnce());
        }

        /// <summary>
        /// Checks when the bird is in the sky and when isn't.
        /// </summary>
        [Test]
        public void LogicInSkyTest()
        {
            bool inSky = this.logic.InSky;
            Assert.IsFalse(inSky);
            this.modelMock.Object.Bird.RealY = 550;
            this.logic.OneTick();
            inSky = this.logic.InSky;
            Assert.IsTrue(inSky);
        }

        [Test]
        public void LogicInCityTest()
        {
            var insky = this.logic.InSky;
            Assert.IsFalse(insky);
            this.modelMock.Object.Bird.RealY = 490;
            this.logic.OneTick();
            Assert.IsFalse(insky);
            this.modelMock.Verify(x => x.Bird, Times.AtLeast(4));
        }

        /// <summary>
        /// Tests if the highest meter the bird has achieved, changes.
        /// </summary>
        [Test]
        public void HighestMeterChangingTest()
        {
            this.modelMock.Object.Bird.RealX = 200;
            this.modelMock.Object.Bird.RealY = -31;
            this.logic.OneTick();
            var meter = this.modelMock.Object.HighestMeter;
            Assert.Greater(meter, 0);
            this.modelMock.Verify(x => x.Bird, Times.AtLeast(4));
        }

        /// <summary>
        /// Tests if the highest meter the bird has achieved, not changes.
        /// </summary>
        [Test]
        public void HighestMeterNotChangingTest()
        {
            this.modelMock.Object.HighestMeter = 300;
            this.modelMock.Object.Bird.RealX = 200;
            this.modelMock.Object.Bird.RealY = -31;
            this.logic.OneTick();
            var meter = this.modelMock.Object.HighestMeter;
            Assert.AreEqual(meter, 300);
            this.modelMock.Verify(x => x.Bird, Times.AtLeast(4));
        }

        /// <summary>
        /// Tests if the game ends when the bird's dx is not null.
        /// </summary>
        [Test]
        public void GameNotEndedDXTest()
        {
            this.modelMock.Object.Bird.DX = 10;
            var let = this.logic.IsGameEnded();
            Assert.IsFalse(let);
            this.modelMock.Verify(x => x.Bird, Times.Exactly(2));
        }

        /// <summary>
        /// Tests if the game ends in the sky.
        /// </summary>
        [Test]
        public void GameNotEndedInSkyTest()
        {
            this.modelMock.Object.Bird.DX = 0;
            this.modelMock.Object.Bird.ItemCoordY = 610;
            this.logic.InSky = true;
            var let = this.logic.IsGameEnded();
            Assert.IsFalse(let);
            this.modelMock.Verify(x => x.Bird, Times.AtLeast(4));
        }

        /// <summary>
        /// Tests if the game not ends in the city.
        /// </summary>
        [Test]
        public void GameNotEndedInCityTest()
        {
            this.modelMock.Object.Bird.DX = 0;
            this.modelMock.Object.Bird.ItemCoordY = 500;
            this.logic.InSky = false;
            var let = this.logic.IsGameEnded();
            Assert.IsFalse(let);
            this.modelMock.Verify(x => x.Bird, Times.AtLeast(4));
        }

        /// <summary>
        /// Tests if the game ends in the city.
        /// </summary>
        [Test]
        public void GameEndedInCityTest()
        {
            this.modelMock.Object.Bird.DX = 0;
            this.modelMock.Object.Bird.ItemCoordY = 601;
            this.logic.InSky = false;
            var let = this.logic.IsGameEnded();
            Assert.IsTrue(let);
            this.modelMock.Verify(x => x.Bird, Times.AtLeast(4));
        }

        /// <summary>
        /// Tests if the endgameanimation plays after finish.
        /// </summary>
        [Test]
        public void GameFinishedTest()
        {
            this.modelMock.Object.Bird.RealX = 200000;
            this.logic.InTimeTravel = true;
            this.logic.EndGameAnimation = true;
            var let = this.logic.IsGameFinished();
            Assert.IsTrue(let);
            this.modelMock.Verify(x => x.Bird, Times.Exactly(2));
        }

        /// <summary>
        /// Tests if the endgameanimation plays after not finishing the game.
        /// </summary>
        [Test]
        public void GameNotFinishedTest()
        {
            this.modelMock.Object.Bird.RealX = 2000;
            this.logic.InTimeTravel = true;
            this.logic.EndGameAnimation = true;
            var let = this.logic.IsGameFinished();
            Assert.IsFalse(let);
            this.modelMock.Verify(x => x.Bird, Times.Exactly(2));
        }
    }
}
