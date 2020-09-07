// <copyright file="GameIO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Repo
{
    using System;
    using Newtonsoft.Json;
    using TimetravelBird.Model;

    /// <summary>
    /// Implenets the ICurrentGame interface and its methods.
    /// </summary>
    public static class GameIO // : //ICurrentGame
    {
        /// <summary>
        /// Deletes a saved game file.
        /// </summary>
        /// <param name="fname">Name to the json file.</param>
        public static void DeleteSavedGame(string fname)
        {
            FileHandler.DeleteFile(fname);
        }

        /// <summary>
        /// Loads an already saved game from a json file.
        /// </summary>
        /// <param name="fname">Name to the json file.</param>
        /// <returns>Returns an already functioning TimetravelBird model.</returns>
        public static ITimetravelBirdModel LoadSavedGame(string fname)
        {
            string json = FileHandler.ReadSavedFile(fname);
            ITimetravelBirdModel model = JsonConvert.DeserializeObject<TimetravelBirdModel>(json);
            return model;
        }

        /// <summary>
        /// Saves a TimetraverBird model to json.
        /// </summary>
        /// <param name="model">The model the player wants to save.</param>
        public static void SaveGame(ITimetravelBirdModel model)
        {
            string modelToJson = JsonConvert.SerializeObject(model, Formatting.Indented);
            FileHandler.SaveGameToFile(model.LoadedGameName, modelToJson);
        }

        /// <summary>
        /// Checks if the game already exists.
        /// </summary>
        /// <param name="fname">Name to the json file.</param>
        /// <returns>True or false based on what FileHandler returns.</returns>
        public static bool GameExists(string fname)
        {
            return FileHandler.CheckFile(fname);
        }

        /// <summary>
        /// Saves the info into the high score file.
        /// </summary>
        /// <param name="time">Time the player played.</param>
        /// <param name="launches">The amount of launches.</param>
        public static void SaveHighScore(string time, int launches)
        {
            string stringToWrite = DateTime.Now + "|" + launches.ToString() + "|" + time + "\n";
            FileHandler.SaveHighScore(stringToWrite);
        }
    }
}
