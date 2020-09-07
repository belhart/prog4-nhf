// <copyright file="Weather.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// An obstacle along the way which randomly spawns on the map and causes the bird to lose speed.
    /// </summary>
    public class Weather : GameItem
    {
        /// <summary>
        /// Width of the weather obstacle.
        /// </summary>
        public const int Width = 300;

        /// <summary>
        /// Strength of the wind.
        /// </summary>
        public const int Windstrength = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Weather"/> class.
        /// Basik constructor of the obstacle.
        /// </summary>
        /// <param name="h">height of the obstacle.</param>
        /// <param name="cx">spawn location of the obstacle.</param>
        public Weather(double h, double cx)
        {
            Random r = new Random();
            this.ItemCoordX = cx;
            RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, 200, Config.GameHeight));
            this.area = rect;
        }
    }
}
