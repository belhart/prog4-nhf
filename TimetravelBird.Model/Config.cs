// <copyright file="Config.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    /// <summary>
    /// Config file for the main program.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Width of the windows.
        /// </summary>
        public const double GameWidth = 1280;

        /// <summary>
        /// Height of the window.
        /// </summary>
        public const double GameHeight = 720;

        /// <summary>
        /// Width of the bird displayed in the main game.
        /// </summary>
        public const double BirdObjectWidth = 50;

        /// <summary>
        /// Height of the bird displayes in the main game.
        /// </summary>
        public const double BirdObjectHeight = 50;

        /// <summary>
        /// The distance of the game to reach the end.
        /// </summary>
        public const int GameDistance = 180100;

        /// <summary>
        /// First upgrade name.
        /// </summary>
        public const string UpgradeItem1Name = "Boost Power";

        /// <summary>
        /// Second upgrade name.
        /// </summary>
        public const string UpgradeItem2Name = "Lightweight";

        /// <summary>
        /// Third upgrade name.
        /// </summary>
        public const string UpgradeItem3Name = "Resistance";

        /// <summary>
        /// Fourth upgrade name.
        /// </summary>
        public const string UpgradeItem4Name = "Money multiplier";

        /// <summary>
        /// Fifth upgrade name.
        /// </summary>
        public const string UpgradeItem5Name = "Boost fuel";

        /// <summary>
        /// Sixth upgrade name.
        /// </summary>
        public const string UpgradeItem6Name = "Bounciness";

        /// <summary>
        /// Seventh upgrade name.
        /// </summary>
        public const string UpgradeItem7Name = "Launch Power";

        /// <summary>
        /// Eighth upgrade name.
        /// </summary>
        public const string UpgradeItem8Name = "Gravity?!?";

        /// <summary>
        /// Tornade image for the weather.
        /// </summary>
        public const string WeatherImageName = "TimetravelBird.Renderer.assets.backgrounds.tornado.gif";

        /// <summary>
        /// Animation after each level.
        /// </summary>
        public const string TimeTravelFileName = "TimetravelBird.Renderer.assets.backgrounds.timetravel.gif";

        /// <summary>
        /// Stats and names of the playable birds.
        /// </summary>
        private static Bird pinky = new Bird(15, 20, 10, 5, 30, 40, 600, @"TimetravelBird.Renderer\assets\birds\pinkyv2.gif");
        private static Bird blue = new Bird(13, 20, 70, 10, 30, 40, 600, @"TimetravelBird.Renderer\assets\birds\bluev2.gif");
        private static Bird yellow = new Bird(12, 30, 20, 10, 30, 40, 600, @"TimetravelBird.Renderer\assets\birds\yellowv2.gif");
        private static Bird red = new Bird(14, 20, 10, 50, 30, 40, 600, @"TimetravelBird.Renderer\assets\birds\redv2.gif");
        private static Bird gray = new Bird(12, 50, 20, 10, 30, 40, 600, @"TimetravelBird.Renderer\assets\birds\grayv2.gif");
        private static Level[] levels = new Level[5]
        {
            new Level(0, "TimetravelBird.Renderer.assets.backgrounds.modern.png", "TimetravelBird.Renderer.assets.backgrounds.sky.png"),
            new Level(50000, "TimetravelBird.Renderer.assets.backgrounds.old.png", "TimetravelBird.Renderer.assets.backgrounds.oldsky.png"),
            new Level(100000, "TimetravelBird.Renderer.assets.backgrounds.pyr.png", "TimetravelBird.Renderer.assets.backgrounds.pyrsky.png"),
            new Level(180000, "TimetravelBird.Renderer.assets.backgrounds.space.png", "TimetravelBird.Renderer.assets.backgrounds.space.png"),
            new Level(222222, "TimetravelBird.Renderer.assets.backgrounds.space.png", "TimetravelBird.Renderer.assets.backgrounds.space.png"),
        };

        /// <summary>
        /// Gets the standard bird.
        /// </summary>
        public static Bird DefaultBird
        {
            get { return Pinky; }
        }

        /// <summary>
        /// Gets or sets the pink bird.
        /// </summary>
        public static Bird Pinky { get => pinky; set => pinky = value; }

        /// <summary>
        /// Gets or sets the blue bird.
        /// </summary>
        public static Bird Blue { get => blue; set => blue = value; }

        /// <summary>
        /// Gets or sets the yellow bird.
        /// </summary>
        public static Bird Yellow { get => yellow; set => yellow = value; }

        /// <summary>
        /// Gets or sets the red bird.
        /// </summary>
        public static Bird Red { get => red; set => red = value; }

        /// <summary>
        /// Gets or sets the gray bird.
        /// </summary>
        public static Bird Gray { get => gray; set => gray = value; }

        /// <summary>
        /// Gets all the levels.
        /// </summary>
        public static Level[] Levels { get => levels; }
    }
}