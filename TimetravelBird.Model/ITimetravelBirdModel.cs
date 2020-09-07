// <copyright file="ITimetravelBirdModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for the model.
    /// </summary>
    public interface ITimetravelBirdModel
    {
        /// <summary>
        /// Gets or sets the amount of money player can spend.
        /// </summary>
        double Money { get; set; }

        /// <summary>
        /// Gets or sets he real height the bird is currently at.
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// Gets or sets how far the bird is from start.
        /// </summary>
        double Meter { get; set; }

        /// <summary>
        /// Gets or sets the highest meter the bird achieved.
        /// </summary>
        double HighestMeter { get; set; }

        /// <summary>
        /// Gets or sets the game width.
        /// </summary>
        double GameWidth { get; set; }

        /// <summary>
        /// Gets or sets the game height.
        /// </summary>
        double GameHeight { get; set; }

        /// <summary>
        /// Gets or sets the Bird class for the game.
        /// </summary>
        Bird Bird { get; set; }

        /// <summary>
        /// Gets or sets the launcher class for the game.
        /// </summary>
        Launcher Launcher { get; set; }

        /// <summary>
        /// Gets or sets the weather class for the game.
        /// </summary>
        Weather Weather { get; set; }

        /// <summary>
        /// Gets or sets the weather class for the game.
        /// </summary>
        Wind Wind { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem1 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem2 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem3 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem4 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem5 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem6 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem7 { get; set; }

        /// <summary>
        /// Gets or sets the upgrade iteam class for the game.
        /// </summary>
        Item UpgradeItem8 { get; set; }

        /// <summary>
        /// Gets or sets the multiplier how much more it costs after each upgrade of the item.
        /// </summary>
        double ItemUpgradeMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the backgrounds for the game.
        /// </summary>
        List<OneBackground> Backgrounds { get; set; }

        /// <summary>
        /// Gets or sets the level the player is currently at.
        /// </summary>
        int CurrentLevel { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected save.
        /// </summary>
        string LoadedGameName { get; set; }

        /// <summary>
        /// Gets or sets the the amount of launches.
        /// </summary>
        int Launches { get; set; }

        /// <summary>
        /// Gets or sets time the game started.
        /// </summary>
        DateTime GameStartingTime { get; set; }
    }
}
