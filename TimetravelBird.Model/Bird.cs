// <copyright file="Bird.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using TimetravelBird;

    /// <summary>
    /// A class to show the games bird.
    /// </summary>
    public class Bird : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bird"/> class.
        /// </summary>
        /// <param name="weight">weight stat.</param>
        /// <param name="moneybonus">moneymultiplier stat.</param>
        /// <param name="resistance">weather resistance.</param>
        /// <param name="boost">power of the boost.</param>
        /// <param name="fuel">amount of fuel for the boost.</param>
        /// <param name="cx">x coordinate.</param>
        /// <param name="cy">y coordinate.</param>
        /// <param name="fname">filename to gif file.</param>
        public Bird(double weight, double moneybonus, double resistance, int boost, double fuel, double cx, double cy, string fname)
        {
            this.DX = 15;
            this.DY = 10;
            this.Weight = weight;
            this.MoneyBonusModifier = moneybonus;
            this.WeatherResistance = resistance;
            this.Boost = boost;
            this.Fuel = fuel;
            this.ItemCoordX = cx;
            this.ItemCoordY = cy;
            this.FileNameToGif = fname;
            RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, 50, 50));
            this.area = rect;
        }

        /// <summary>
        /// Gets or sets the cost of the bird.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the speed of the bird.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Gets or sets the weight stat of the bird.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the moneybonus stat of the bird.
        /// </summary>
        public double MoneyBonusModifier { get; set; }

        /// <summary>
        /// Gets or sets the weather stat of the bird.
        /// </summary>
        public double WeatherResistance { get; set; }

        /// <summary>
        /// Gets or sets the boost of the bird.
        /// </summary>
        public int Boost { get; set; }

        /// <summary>
        /// Gets or sets the amount of fuel for the boost.
        /// </summary>
        public double Fuel { get; set; }

        /// <summary>
        /// Gets or sets movement on the x axis.
        /// </summary>
        public double DX { get; set; }

        /// <summary>
        /// Gets or sets movement on the y axis.
        /// </summary>
        public double DY { get; set; }

        /// <summary>
        /// Gets or sets the file name of the birds image.
        /// </summary>
        public string FileNameToGif { get; set; }

        /// <summary>
        /// Gets or sets the realy y distance of the bird from the ground.
        /// </summary>
        public double RealY { get; set; }

        /// <summary>
        /// Gets or sets the real x distance of the bird from the launch.
        /// </summary>
        public double RealX { get; set; }
    }
}
