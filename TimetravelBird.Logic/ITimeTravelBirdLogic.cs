// <copyright file="ITimeTravelBirdLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Logic
{
    using System;
    using System.Windows.Input;
    using TimetravelBird.Model;
    using TimetravelBird.Renderer;

    /// <summary>
    /// Interface for logic.
    /// </summary>
    public interface ITimeTravelBirdLogic
    {
        /// <summary>
        /// .
        /// </summary>
        event EventHandler<ITimetravelBirdModel> GameEnded;

        /// <summary>
        /// .
        /// </summary>
        event EventHandler GameFinished;

        /// <summary>
        /// Gets or sets a value indicating whether the bird has been launched.
        /// </summary>
        bool Launch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bird is in the sky.
        /// </summary>
        bool InSky { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bird is approaching to a new level.
        /// </summary>
        bool InTimeTravel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game is over and the post game animation is playing.
        /// </summary>
        bool GameIsOver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game is over.
        /// </summary>
        bool EndGameAnimation { get; set; }

        /// <summary>
        /// Gets or sets a value of the model.
        /// </summary>
        ITimetravelBirdModel Model { get; set; }

        /// <summary>
        /// When the game ends it resets the position of the objects.
        /// </summary>
        /// <param name="fname">resets the current save.</param>
        void BirdReset();

        /// <summary>
        /// Event to call when the player completed the whole game.
        /// </summary>
        void GameEndedEvent();

        /// <summary>
        /// Monitors the bird's speed and location, return true when the current game has ended.
        /// </summary>
        /// <returns>true/false.</returns>
        bool IsGameEnded();

        /// <summary>
        /// Moves the backgrounds, handles next map animation.
        /// </summary>
        void BackgroundTick();

        /// <summary>
        /// Modifies the speed with each use, making the bird more controlable.
        /// </summary>
        void BirdBoost();

        /// <summary>
        /// With each tick changes the speed of the bird considering the angle.
        /// </summary>
        void BirdTick();

        /// <summary>
        /// The bird tick, if the bird is in the upper stage of the game.
        /// </summary>
        void BirdCityTick();

        /// <summary>
        /// The bird tick, if the bird is in the lower stage of the game.
        /// </summary>
        void BirdSkyTick();

        /// <summary>
        /// Handling all the method in each tick.
        /// </summary>
        void OneTick();

        /// <summary>
        /// Disappears the launcher after shoot.
        /// </summary>
        void LauncherTick();

        /// <summary>
        /// Increases the base speed of the bird.
        /// </summary>
        void LauncherShoot();

        /// <summary>
        /// Allows the user to change the angle.
        /// </summary>
        /// <param name="e">key argument, A or D.</param>
        void RotateBird(KeyEventArgs e);
    }
}
