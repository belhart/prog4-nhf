// <copyright file="IGameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    /// <summary>
    /// Interface for the gameitem class.
    /// </summary>
    public interface IGameItem
    {
        /// <summary>
        /// Returns when 2 gameitems collide.
        /// </summary>
        /// <param name="other">other gameitem.</param>
        /// <returns>a bool.</returns>
        bool IsCollision(GameItem other);
    }
}
