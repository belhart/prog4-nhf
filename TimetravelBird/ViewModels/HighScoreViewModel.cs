// <copyright file="HighScoreViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.ViewModels
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;

    /// <summary>
    /// A class for the highscore viewmodel.
    /// </summary>
    public class HighScoreViewModel : ViewModelBase
    {
        private string highscore;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreViewModel"/> class.
        /// </summary>
        public HighScoreViewModel()
        {
            this.CloseClick = new RelayCommand(() => this.CloseClick_Click());
            this.BackToMainMenu = new RelayCommand(() => this.MainMenu_Click());
            this.DataRead();
        }

        /// <summary>
        /// Gets or sets the values for the highscore table.
        /// </summary>
        public string HighScore
        {
            get { return this.highscore; }
            set { this.Set(ref this.highscore, value); }
        }

        /// <summary>
        /// Gets the window to close.
        /// </summary>
        public ICommand CloseClick { get; private set; }

        /// <summary>
        /// Gets or sets the datacontext to mainmenu.
        /// </summary>
        public ICommand BackToMainMenu { get; set; }

        private void CloseClick_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
        }

        private void MainMenu_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuViewModel();
        }

        private void DataRead()
        {
            if (File.Exists("highscore.txt"))
            {
                var lines = File.ReadAllLines("highscore.txt");
                if (lines.Length == 0)
                {
                    this.HighScore = "Unfortunately, there\naren't any highscores:(";
                }
                else
                {
                    int idx;
                    _ = lines.Length < 11 ? idx = lines.Length : idx = 10;
                    for (int i = 0; i < idx; i++)
                    {
                        var parsedLine = lines[i].Split('|');
                        int pontszam = Convert.ToInt32((1000 / Convert.ToInt32(parsedLine[1])) * 100);
                        this.HighScore += i + 1 + ". \t" + pontszam + "\t" + parsedLine[0] + "\n";
                    }
                }
            }
            else
            {
                this.HighScore = "Unfortunately, the\nfile doesnt exist yet:(";
            }
        }
    }
}
