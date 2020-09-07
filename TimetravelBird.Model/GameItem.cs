// <copyright file="GameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// Main class for the gameitems.
    /// </summary>
    public abstract class GameItem : IGameItem
    {
        /// <summary>
        /// Area of the gameitems.
        /// </summary>
        private protected Geometry area;

        /// <summary>
        /// Gets or sets the rotdegree to rad.
        /// </summary>
        public double Rad
        {
            get
            {
                return Math.PI * this.RotDegree / 180;
            }

            set
            {
                this.RotDegree = 180 * value / Math.PI;
            }
        }

        /// <summary>
        /// Gets and rotates the area.
        /// </summary>
        public Geometry RealArea
        {
            get
            {
                TransformGroup tg = new TransformGroup();
                tg.Children.Add(new TranslateTransform(this.ItemCoordX, this.ItemCoordY));
                tg.Children.Add(new RotateTransform(this.RotDegree, this.ItemCoordX, this.ItemCoordY));
                this.area.Transform = tg;
                return this.area.GetFlattenedPathGeometry();
            }
        }

        /// <summary>
        /// Gets or sets center x position.
        /// </summary>
        public double ItemCoordX { get; set; }

        /// <summary>
        /// Gets or sets center y position.
        /// </summary>
        public double ItemCoordY { get; set; }

        /// <summary>
        /// Gets or sets degree of the bird.
        /// </summary>
        protected double RotDegree { get; set; }

        /// <summary>
        /// Returns when 2 gameitems collide.
        /// </summary>
        /// <param name="other">the other gameitem.</param>
        /// <returns>a bool.</returns>
        public bool IsCollision(GameItem other)
        {
            return Geometry.Combine(this.RealArea, other.RealArea, GeometryCombineMode.Intersect, null)
                .GetArea() > 0;
        }
    }
}
