// <copyright file="MainMenuViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace TimetravelBird.ViewModels
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.CommandWpf;

    /// <summary>
    /// A class for the main menu view model.
    /// </summary>
    public class MainMenuViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuViewModel"/> class.
        /// </summary>
        public MainMenuViewModel()
        {
            this.PlayClick = new RelayCommand(() => this.PlayClick_Click());
            this.CloseClick = new RelayCommand(() => this.CloseClick_Click());
            this.HighScoreClick = new RelayCommand(() => this.HighScoreClick_Click());
        }

        /// <summary>
        ///  Gets the datacontext to loadsavemenuviewmodel.
        /// </summary>
        public ICommand PlayClick { get; private set; }

        /// <summary>
        /// Gets the window to close.
        /// </summary>
        public ICommand CloseClick { get; private set; }

        /// <summary>
        /// Gets the window's datacontext to highscoremenu.
        /// </summary>
        public ICommand HighScoreClick { get; private set; }

        private void PlayClick_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new LoadSaveMenuViewModel();
        }

        private void CloseClick_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
        }

        private void HighScoreClick_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new HighScoreViewModel();
        }
    }
}
