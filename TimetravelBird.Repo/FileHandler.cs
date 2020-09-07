// <copyright file="FileHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Repo
{
    using System.IO;
    using TimetravelBird.Model;

    /// <summary>
    /// This class is responsible for opening, reading and writing to files.
    /// </summary>
    public class FileHandler
    {
        /// <summary>
        /// Reads a json formatted file, converts it to model class and returns it.
        /// </summary>
        /// <param name="fname">Name to the json file.</param>
        /// <returns>TimetravelbirdModel class.</returns>
        public static string ReadSavedFile(string fname)
        {
            return File.ReadAllText(fname + ".json");
        }

        /// <summary>
        /// Converts a model to json format then saves it.
        /// </summary>
        /// <param name="fname">Name to the json file.</param>
        /// <param name="jsonmodel">The model class we want to save.</param>
        public static void SaveGameToFile(string fname, string jsonmodel)
        {
            File.WriteAllText(fname + ".json", jsonmodel);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fname">Name to the json file.</param>
        public static void DeleteFile(string fname)
        {
            File.Delete(fname + ".json");
        }

        /// <summary>
        /// Checks if the file exists or not.
        /// </summary>
        /// <param name="fname">Name to the json file.</param>
        /// <returns>True or false based if the file exists.</returns>
        public static bool CheckFile(string fname)
        {
            return File.Exists(fname + ".json");
        }

        /// <summary>
        /// Saves the high score into file.
        /// </summary>
        /// <param name="stringToWrite">The string to write into the file.</param>
        public static void SaveHighScore(string stringToWrite)
        {
            File.AppendAllText("highscore.txt", stringToWrite);
        }
    }
}
