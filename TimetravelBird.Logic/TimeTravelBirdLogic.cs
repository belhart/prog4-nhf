// <copyright file="TimeTravelBirdLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Logic
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using TimetravelBird.Model;
    using TimetravelBird.Repo;

    /// <summary>
    /// Main logic of the game.
    /// </summary>
    public class TimeTravelBirdLogic : ITimeTravelBirdLogic
    {
        private static Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTravelBirdLogic"/> class.
        /// </summary>
        /// <param name="model">model parameter.</param>
        public TimeTravelBirdLogic(ITimetravelBirdModel model)
        {
            this.Model = model;
            this.Launch = true;
            this.InSky = false;
            this.WeatherTickCounter = 0;
            this.WindTickCounter = 0;
            this.IsThereWeather = false;
            this.IsThereWind = false;
            this.InTimeTravel = false;
            this.EndGameAnimation = false;
        }

        /// <summary>
        /// Handles an event which raises when the bird has fallen to the ground with zero speed.
        /// </summary>
        public event EventHandler<ITimetravelBirdModel> GameEnded;

        /// <summary>
        /// Handles an event which raises when the player finished the game.
        /// </summary>
        public event EventHandler GameFinished;

        /// <summary>
        /// Gets or sets a value indicating whether the weather has been spawned.
        /// </summary>
        public bool IsThereWeather { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the weather has been spawned.
        /// </summary>
        public bool IsThereWind { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bird has been launched.
        /// </summary>
        public bool Launch { get; set; }

        /// <summary>
        /// Gets or sets a value of the model.
        /// </summary>
        public ITimetravelBirdModel Model { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bird is in the sky.
        /// </summary>
        public bool InSky { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game is over and the post game animation is playing.
        /// </summary>
        public bool GameIsOver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bird is at the end of the game.
        /// </summary>
        public bool EndGameAnimation { get; set; }

        /// <summary>
        /// Gets or sets the weather when the counter reaches a certain amount.
        /// </summary>
        public int WeatherTickCounter { get; set; }

        /// <summary>
        /// Gets or sets the wind when the counter reaches a certain amount.
        /// </summary>
        public int WindTickCounter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bird is approaching to a new level.
        /// </summary>
        public bool InTimeTravel { get; set; }

        /*public bool GameEnded()
        {
            bool ended = false;
            if (this.model.Bird.DX <= 0 && this.model.Bird.ItemCoordY >= 560)
            {
                ended = true;
            }

            return ended;
        }*/

        /// <summary>
        /// Loads the selected save.
        /// </summary>
        /// <param name="fname">selected save.</param>
        /// <returns>a model.</returns>
        public static ITimetravelBirdModel LoadGame(string fname)
        {
            return GameIO.LoadSavedGame(fname);
        }

        /// <summary>
        /// Static method to make or read the games model.
        /// </summary>
        /// <param name="fname">Name to the file.</param>
        /// <returns>A logic for the game.</returns>
        public static TimeTravelBirdLogic GameCreate(string fname)
        {
            var model = GameExists(fname)
                    ? LoadGame(fname)
                    : new TimetravelBirdModel(random.Next(1, 6));
            model.LoadedGameName = fname;
            var logic = new TimeTravelBirdLogic(model);
            return logic;
        }

        /// <summary>
        /// Scans for an existing save.
        /// </summary>
        /// <param name="fname">current save.</param>
        /// <returns>a bool.</returns>
        public static bool GameExists(string fname)
        {
            return GameIO.GameExists(fname);
        }

        /// <summary>
        /// When the game ends it resets the position of the objects.
        /// </summary>
        /// <param name="fname">name of the save which needs to be reseted.</param>
        public void BirdReset()
        {
            this.Model.Money += Convert.ToInt32((this.Model.Bird.RealX / 100) * (1 + (this.Model.Bird.MoneyBonusModifier / 100)));
            this.Model.Bird.RealY = 0;
            this.Model.Bird.RealX = 0;
            this.Model.Bird.DY = 10;
            this.Model.Bird.DX = 15;
            this.Model.Bird.ItemCoordY = 600;
            this.Model.Bird.ItemCoordX = 40;
            this.Model.Bird.Rad = 0;
            this.Model.Launcher.ItemCoordX = 0;
            this.Launch = true;
            this.Model.CurrentLevel = 0;
            this.Model.Weather.ItemCoordX = Config.GameWidth;
            this.Model.Wind.ItemCoordX = Config.GameWidth;
            this.Model.Launches++;

            // GameIO.SaveGame(this.model, fname);
        }

        /// <summary>
        /// Modifies the speed with each use, making the bird more controlable.
        /// </summary>
        public void BirdBoost()
        {
            if (this.Model.Bird.Fuel > 0 && !this.Launch)
            {
                if (this.Model.Bird.Rad < 0)
                {
                    this.Model.Bird.DY += (1.2 * this.Model.UpgradeItem1.Level) + (0.2 * (-this.Model.Bird.Rad));
                }
                else
                {
                    this.Model.Bird.DY -= (0.5 * this.Model.UpgradeItem1.Level) + (0.1 * this.Model.Bird.Rad);
                }

                this.Model.Bird.DX += 0.3 * this.Model.UpgradeItem1.Level;
                this.Model.Bird.Fuel -= 2;
            }
        }

        /// <summary>
        /// With each tick changes the speed of the bird considering the angle.
        /// </summary>
        public void BirdTick()
        {
            if (this.Model.Bird.Rad >= 0)
            {
                this.Model.Bird.DY = (this.Model.Bird.DY - ((0.01 * this.Model.Bird.Weight) + (0.1 * this.Model.Bird.Rad))) * (1 - (0.02 * this.Model.UpgradeItem8.Level)); // lefele néz akkor gyorsabban zuhan
            }
            else if (this.Model.Bird.Rad < 0)
            {
                this.Model.Bird.DY = (this.Model.Bird.DY - ((0.01 * this.Model.Bird.Weight) + (0.08 * (-this.Model.Bird.Rad)))) * (1 - (0.02 * this.Model.UpgradeItem8.Level)); // lassabban zuhan
            }

            // this.model.Bird.DX = this.model.Bird.DX - 0.25;
            // this.model.Bird.Speed = Math.Abs(this.model.Bird.DY * this.model.Bird.DX) / 2;
            this.Model.Bird.RealY += this.Model.Bird.DY;
            this.Model.Bird.RealX += this.Model.Bird.DX;
            this.Model.Bird.Rad += Math.PI / 3600;

            // this.model.Bird.Rad = Math.Atan2(this.model.Bird.DY, this.model.Bird.DX);
            if (this.Model.Bird.RealX >= Config.GameDistance)
            {
                this.EndGameAnimation = true;
            }
        }

        /// <summary>
        /// The bird tick, if the bird is in the lower stage of the game.
        /// </summary>
        public void BirdCityTick()
        {
            if (this.InSky)
            {
                this.Model.Bird.ItemCoordY = 100;
                this.InSky = false;
            }

            this.BirdTick();
            this.Model.Bird.ItemCoordY -= this.Model.Bird.DY;
            if (this.Model.Bird.ItemCoordY > 600)
            {
                this.Model.Bird.DY = Math.Abs(this.Model.Bird.DY) - (Math.Abs(this.Model.Bird.DY) / 3) + (0.2 * this.Model.UpgradeItem6.Level);
                this.Model.Bird.DX = this.Model.Bird.DX - (Convert.ToInt32(Math.Abs(this.Model.Bird.DY)) + 2);
            }
        }

        /// <summary>
        /// The bird tick, if the bird is in the upper stage of the game.
        /// </summary>
        public void BirdSkyTick()
        {
            if (this.InSky == false)
            {
                this.Model.Bird.ItemCoordY = Config.GameHeight - 101;
                this.InSky = true;
                if (this.IsThereWeather)
                {
                    this.IsThereWeather = false;
                    this.Model.Weather.ItemCoordX = Config.GameWidth;
                }
            }

            if (this.Model.Bird.ItemCoordY < 100)
            {
                this.Model.Bird.ItemCoordY = 100;
            }
            else
            {
                if (this.Model.Bird.DY <= 0 && this.Model.Bird.ItemCoordY > Config.GameHeight - 100)
                {
                    this.Model.Bird.ItemCoordY = Config.GameHeight - 100;
                }
                else
                {
                    this.Model.Bird.ItemCoordY -= this.Model.Bird.DY;
                }
            }

            this.BirdTick();
        }

        /// <summary>
        /// Event to call when the player completed the whole game.
        /// </summary>
        public void GameEndedEvent()
        {
            this.GameEnded?.Invoke(this, this.Model);
        }

        /// <summary>
        /// Handling all the method in each tick.
        /// </summary>
        public void OneTick()
        {
            if (!this.EndGameAnimation)
            {
                if (this.Model.Bird.RealY < 500)
                {
                    this.BirdCityTick();
                }
                else
                {
                    this.BirdSkyTick();
                }

                if (this.Model.Bird.RealX > Config.Levels[this.Model.CurrentLevel + 1].DistanceToSwitchLevel)
                {
                    this.Model.Bird.RealX = Config.Levels[this.Model.CurrentLevel + 1].DistanceToSwitchLevel + 1;
                    this.Model.CurrentLevel++;
                    this.InTimeTravel = true;
                    return;
                }

                this.BackgroundTick();

                // this.LauncherTick();
                if (this.Model.Bird.IsCollision(this.Model.Weather))
                {
                    this.Model.Bird.DX += (random.NextDouble() * random.Next(-6, 2)) * (1 - (this.Model.Bird.WeatherResistance / 100));
                    if (this.Model.Bird.Rad < Math.PI / 3)
                    {
                        this.Model.Bird.Rad += (Math.PI / 60) * (1 - (this.Model.Bird.WeatherResistance / 100));
                    }
                }

                if (this.Model.Bird.IsCollision(this.Model.Wind))
                {
                    this.Model.Bird.DX += (random.NextDouble() * random.Next(-4, 0)) * (1 - (this.Model.Bird.WeatherResistance / 100));
                    if (this.Model.Bird.Rad < Math.PI / 3)
                    {
                        this.Model.Bird.Rad += (Math.PI / 90) * (1 - (this.Model.Bird.WeatherResistance / 100));
                    }
                }

                if (this.Model.Bird.RealY > -30)
                {
                    if (this.Model.HighestMeter < this.Model.Bird.RealX)
                    {
                        this.Model.HighestMeter = this.Model.Bird.RealX;
                    }
                }

                if (!this.IsThereWeather)
                {
                    if (this.WeatherTickCounter < 10)
                    {
                        this.WeatherTickCounter++;
                    }
                    else
                    {
                        this.WeatherTickCounter = 0;
                        this.WeatherGenerateTick();
                    }
                }
                else
                {
                    this.WeatherTick();
                }

                if (!this.IsThereWind)
                {
                    if (this.WindTickCounter < 10)
                    {
                        this.WindTickCounter++;
                    }
                    else
                    {
                        this.WindTickCounter = 0;
                        this.WindGenerateTick();
                    }
                }
                else
                {
                    this.WindTick();
                }
            }
            else
            {
                this.Model.Bird.ItemCoordX += 5;
                if (this.Model.Bird.ItemCoordX > 1280)
                {
                    this.InTimeTravel = true;
                }
            }

            if (this.IsGameFinished())
            {
                this.InTimeTravel = false;
                this.EndGameAnimation = false;
                this.GameIsOver = true;
                this.BirdReset();
                DateTime now = DateTime.Now;
                TimeSpan played = now.Subtract(this.Model.GameStartingTime);
                string playedInString = $"{played.Days}days {played.Hours}:{played.Minutes}:{played.Seconds}";
                GameIO.SaveHighScore(playedInString, this.Model.Launches);
                GameIO.DeleteSavedGame(this.Model.LoadedGameName);
                this.GameFinished?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Despawns the weather.
        /// </summary>
        public void WeatherTick()
        {
            this.Model.Weather.ItemCoordX -= 15;
            if (this.Model.Weather.ItemCoordX < -300)
            {
                this.IsThereWeather = false;
            }
        }

        /// <summary>
        /// Despawns the wind.
        /// </summary>
        public void WindTick()
        {
            this.Model.Wind.ItemCoordX -= 15;
            if (this.Model.Wind.ItemCoordX < -300)
            {
                this.IsThereWind = false;
            }
        }

        /// <summary>
        /// Generates and spawns the weather.
        /// </summary>
        public void WeatherGenerateTick()
        {
            int weatherRand = random.Next(0, 1000);
            int weathChance = 20;
            if (weatherRand < weathChance && !this.InSky)
            {
                this.IsThereWeather = true;
                this.Model.Weather.ItemCoordX = Config.GameWidth;
            }
        }

        /// <summary>
        /// Generates and spawns the wind.
        /// </summary>
        public void WindGenerateTick()
        {
            int windRand = random.Next(0, 1000);
            int windChance = 100;
            if (windRand < windChance /*&& this.inSky==true*/)
            {
                this.IsThereWind = true;
                this.Model.Wind.ItemCoordX = Config.GameWidth;
                this.Model.Wind.ItemCoordY = random.Next(100, (int)Config.GameHeight - 250);
            }
        }

        /// <summary>
        /// Moves the backgrounds, handles next map animation.
        /// </summary>
        public void BackgroundTick()
        {
            foreach (OneBackground background in this.Model.Backgrounds)
            {
                background.ItemCoordX -= Math.Abs(this.Model.Bird.Speed);
                background.ItemCoordX -= Convert.ToInt32(Math.Abs(this.Model.Bird.DX) / 2);

                if (background.ItemCoordX + Config.GameWidth <= 0)
                {
                    double farRightXCord = this.Model.Backgrounds.Max(x => x.ItemCoordX);
                    background.ItemCoordX = farRightXCord + Config.GameWidth - Convert.ToInt32(Math.Abs(this.Model.Bird.DX) / 2);
                }
            }
        }

        /// <summary>
        /// Disappears the launcher after shoot.
        /// </summary>
        public void LauncherTick()
        {
            this.Model.Launcher.ItemCoordX -= Math.Abs(this.Model.Bird.Speed);
        }

        /// <summary>
        /// Increases the base speed of the bird.
        /// </summary>
        public void LauncherShoot()
        {
            if (this.Launch)
            {
                this.Model.Bird.DX += this.Model.Launcher.Power;
                this.Model.Bird.DY += this.Model.Launcher.Power / 10;
                this.Launch = false;
                this.Model.Bird.Fuel = 10 + (8 * this.Model.UpgradeItem5.Level);
            }
        }

        /// <summary>
        /// Allows the user to change the angle.
        /// </summary>
        /// <param name="e">key argument, A or D.</param>
        public void RotateBird(KeyEventArgs e)
        {
            double rotate = (this.Model.Bird.Rad / Math.PI) * 180;
            if (rotate < 60 && rotate > -60)
            {
                switch (e.Key)
                {
                    case Key.A: this.Model.Bird.Rad -= (3.0 / 180.0) * Math.PI; break;
                    case Key.D: this.Model.Bird.Rad += (3.0 / 180.0) * Math.PI; break;
                }
            }
            else if (rotate >= 60)
            {
                this.Model.Bird.Rad = 19 * Math.PI / 60;
                switch (e.Key)
                {
                    case Key.A: this.Model.Bird.Rad -= 3.0 / 180.0 * Math.PI; break;
                    case Key.D: this.Model.Bird.Rad += 3.0 / 180.0 * Math.PI; break;
                }
            }
            else
            {
                this.Model.Bird.Rad = -19 * Math.PI / 60;
                switch (e.Key)
                {
                    case Key.A: this.Model.Bird.Rad -= 3.0 / 180.0 * Math.PI; break;
                    case Key.D: this.Model.Bird.Rad += 3.0 / 180.0 * Math.PI; break;
                }
            }
        }

        /// <summary>
        /// Monitors the bird's speed and location, return true when the current game has ended.
        /// </summary>
        /// <returns>true/false.</returns>
        public bool IsGameEnded()
        {
            if (this.Model.Bird.DX <= 0 && this.Model.Bird.ItemCoordY >= 600 && !this.InSky)
            {
                this.BirdReset();
                GameIO.SaveGame(this.Model);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Observes if the player has reached the endgame.
        /// </summary>
        /// <returns>true/false.</returns>
        public bool IsGameFinished()
        {
            if ((this.Model.Bird.RealX > Config.GameDistance) && this.InTimeTravel && this.EndGameAnimation)
            {
                return true;
            }

            return false;
        }
    }
}
