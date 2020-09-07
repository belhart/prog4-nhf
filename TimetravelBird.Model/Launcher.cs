// <copyright file="Launcher.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System.Windows.Media;

    /// <summary>
    ///  A class to show the launcher.
    /// </summary>
    public class Launcher : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Launcher"/> class.
        /// </summary>
        /// <param name="cost">The cost of the launcher.</param>
        /// <param name="power">The power of the launcher.</param>
        public Launcher(double cost, double power)
        {
            this.Cost = cost;
            this.Power = power;
            this.ItemCoordX = 0;
            this.ItemCoordY = Config.GameHeight - (Config.BirdObjectHeight + 75);
            this.area = new RectangleGeometry(new System.Windows.Rect(this.ItemCoordX, this.ItemCoordY, 20, 20));
        }

        /// <summary>
        /// Gets or sets the cost of the launcher.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the power of the launcher.
        /// </summary>
        public double Power { get; set; }
    }
}