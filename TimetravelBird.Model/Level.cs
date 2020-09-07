// <copyright file="Level.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Model
{
    /// <summary>
    /// A class to store the levels images and distance.
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        /// <param name="dist">distance between levels.</param>
        /// <param name="cityFName">image name of the lowermap.</param>
        /// <param name="skyFName">image name of the uppermap.</param>
        public Level(double dist, string cityFName, string skyFName)
        {
            this.DistanceToSwitchLevel = dist;
            this.FileNameToCityIMG = cityFName;
            this.FileNameToSkyIMG = skyFName;
        }

        /// <summary>
        /// Gets or sets the distance from the launch when this level is activated.
        /// </summary>
        public double DistanceToSwitchLevel { get; set; }

        /// <summary>
        /// Gets or sets the name of the file for the city image on this level.
        /// </summary>
        public string FileNameToCityIMG { get; set; }

        /// <summary>
        /// Gets or sets he name of the file for the sky image on this level.
        /// </summary>
        public string FileNameToSkyIMG { get; set; }
    }
}
