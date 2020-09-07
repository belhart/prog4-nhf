// <copyright file="Item.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System.Windows.Media;

    /// <summary>
    /// A class to show the items.
    /// </summary>
    public class Item : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <param name="cost">The cost of the item.</param>
        public Item(string name, double cost)
        {
            this.Name = name;
            this.Cost = cost;
            this.Level = 1;
            this.IsMaxLevel = false;
            this.area = new RectangleGeometry(new System.Windows.Rect(this.ItemCoordX, this.ItemCoordY, 20, 20));
        }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the cost of the item.
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Gets or sets the current level of the item.
        /// </summary>
        public double Level { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is at max level.
        /// </summary>
        public bool IsMaxLevel { get; set; }
    }
}