// <copyright file="TimetravelBirdModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// The model for the main game.
    /// </summary>
    public class TimetravelBirdModel : ITimetravelBirdModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimetravelBirdModel"/> class.
        /// </summary>
        /// <param name="id">The birds id.</param>
        public TimetravelBirdModel(int id = 0)
        {
            this.GameWidth = Config.GameWidth;
            this.GameHeight = Config.GameHeight;
            this.Money = 0;
            this.Meter = 0;
            this.HighestMeter = 0;
            this.Height = 0;
            this.ItemUpgradeMultiplier = 1.35;
            this.UpgradeItem1 = new Item(Config.UpgradeItem1Name, 120);
            this.UpgradeItem2 = new Item(Config.UpgradeItem2Name, 130);
            this.UpgradeItem3 = new Item(Config.UpgradeItem3Name, 150);
            this.UpgradeItem4 = new Item(Config.UpgradeItem4Name, 250);
            this.UpgradeItem5 = new Item(Config.UpgradeItem5Name, 200);
            this.UpgradeItem6 = new Item(Config.UpgradeItem6Name, 175);
            this.UpgradeItem7 = new Item(Config.UpgradeItem7Name, 300);
            this.UpgradeItem8 = new Item(Config.UpgradeItem8Name, 1000);
            this.Weather = new Weather(Config.GameHeight, Config.GameWidth); // fix this with real values later
            this.Wind = new Wind(Config.GameWidth);

            switch (id)
            {
                case 1: this.Bird = Config.Pinky; break;
                case 2: this.Bird = Config.Blue; break;
                case 3: this.Bird = Config.Yellow; break;
                case 4: this.Bird = Config.Red; break;
                case 5: this.Bird = Config.Gray; break;
                default: this.Bird = Config.DefaultBird; break;
            }

            this.Backgrounds = new List<OneBackground>();
            this.Backgrounds.Add(new OneBackground(0, 0));
            this.Backgrounds.Add(new OneBackground(this.GameWidth, 0));
            this.Backgrounds.Add(new OneBackground(this.GameWidth * 2, 0));
            this.CurrentLevel = 0;
            this.Launcher = new Launcher(100, 20);
            this.LoadedGameName = string.Empty;
            this.GameStartingTime = DateTime.Now;
            this.Launches = 1;
        }

        /// <summary>
        /// Gets or sets the amount of money player can spend.
        /// </summary>
        public double Money { get; set; }

        /// <summary>
        /// Gets or sets he real height the bird is currently at.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets how far the bird is from start.
        /// </summary>
        public double Meter { get; set; }

        /// <summary>
        /// Gets or sets the highest meter the bird achieved.
        /// </summary>
        public double HighestMeter { get; set; }

        /// <summary>
        /// Gets or sets the game width.
        /// </summary>
        public double GameWidth { get; set; }

        /// <summary>
        /// Gets or sets the the amount of launches.
        /// </summary>
        public int Launches { get; set; }

        /// <summary>
        /// Gets or sets the game height.
        /// </summary>
        public double GameHeight { get; set; }

        /// <summary>
        /// Gets or sets the Bird class for the game.
        /// </summary>
        public Bird Bird { get; set; }

        /// <summary>
        /// Gets or sets the launcher class for the game.
        /// </summary>
        public Launcher Launcher { get; set; }

        /// <summary>
        /// Gets or sets the weather class for the game.
        /// </summary>
        public Weather Weather { get; set; }

        /// <summary>
        /// Gets or sets the weather class for the game.
        /// </summary>
        public Wind Wind { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem1 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem2 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem3 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem4 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem5 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem6 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem7 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        public Item UpgradeItem8 { get; set; }

        /// <summary>
        /// Gets or sets the multiplier how much more it costs after each upgrade of the item.
        /// </summary>
        public double ItemUpgradeMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the backgrounds for the game.
        /// </summary>
        public List<OneBackground> Backgrounds { get; set; }

        /// <summary>
        /// Gets or sets the level the player is currently at.
        /// </summary>
        public int CurrentLevel { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected save.
        /// </summary>
        public string LoadedGameName { get; set; }

        /// <summary>
        /// Gets or sets time the game started.
        /// </summary>
        public DateTime GameStartingTime { get; set; }
    }
}