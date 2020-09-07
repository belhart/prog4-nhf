// <copyright file="Wind.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Wind class for another obstacle.
    /// </summary>
    public class Wind : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Wind"/> class.
        /// </summary>
        /// <param name="cx">itemcoordx.</param>
        public Wind(double cx)
        {
            this.ItemCoordX = cx;

            RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, 150, Config.GameHeight / 3));

            this.area = rect;
        }
    }
}
