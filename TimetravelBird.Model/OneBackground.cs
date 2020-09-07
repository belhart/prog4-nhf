// <copyright file="OneBackground.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A class to show the games background.
    /// </summary>
    public class OneBackground : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneBackground"/> class.
        /// </summary>
        /// <param name="cx">The x coord where the background should be.</param>
        /// <param name="cy">The y coord where the background should be.</param>
        public OneBackground(double cx, double cy)
        {
            this.ItemCoordX = cx;
            this.ItemCoordY = cy;
            this.area = new RectangleGeometry(new Rect(this.ItemCoordX, this.ItemCoordY, Config.GameHeight, Config.GameWidth));
        }
    }
}
