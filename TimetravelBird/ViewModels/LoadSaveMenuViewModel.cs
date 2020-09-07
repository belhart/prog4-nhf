// <copyright file="LoadSaveMenuViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace TimetravelBird.ViewModels
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using TimetravelBird.Repo;

    /// <summary>
    /// A class for the load and save menu view model.
    /// </summary>
    public class LoadSaveMenuViewModel : ViewModelBase
    {
        private string selectedsaveslot;
        private string savestatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSaveMenuViewModel"/> class.
        /// </summary>
        public LoadSaveMenuViewModel()
        {
            this.SaveSlot1 = new RelayCommand(() => this.SaveSlot1_Click());
            this.SaveSlot2 = new RelayCommand(() => this.SaveSlot2_Click());
            this.SaveSlot3 = new RelayCommand(() => this.SaveSlot3_Click());
            this.BackToMainMenu = new RelayCommand(() => this.MainMenu_Click());
            this.LoadGame = new RelayCommand(() => this.LoadGame_Click());
            this.DeleteClick = new RelayCommand(() => this.DeleteClick_Click());
        }

        /// <summary>
        /// Gets or sets the name of the selected upgrade item.
        /// </summary>
        public string SelectedSaveSlot
        {
            get { return this.selectedsaveslot; }
            set { this.Set(ref this.selectedsaveslot, value); }
        }

        /// <summary>
        /// Gets or sets the name of the selected upgrade item.
        /// </summary>
        public string SaveStatus
        {
            get { return this.savestatus; }
            set { this.Set(ref this.savestatus, value); }
        }

        /// <summary>
        /// Gets or sets the save on slot 1.
        /// </summary>
        public ICommand SaveSlot1 { get; set; }

        /// <summary>
        /// Gets or sets the save on slot 2.
        /// </summary>
        public ICommand SaveSlot2 { get; set; }

        /// <summary>
        /// Gets or sets the save on slot 3.
        /// </summary>
        public ICommand SaveSlot3 { get; set; }

        /// <summary>
        /// Gets or sets the datacontext to mainmenuviewmodel.
        /// </summary>
        public ICommand BackToMainMenu { get; set; }

        /// <summary>
        /// Gets or sets the datacontext based on the selected save slot.
        /// </summary>
        public ICommand LoadGame { get; set; }

        /// <summary>
        /// Gets or sets the the selected saveslot value to null.
        /// </summary>
        public ICommand DeleteClick { get; set; }

        private void SaveSlot1_Click()
        {
            this.SelectedSaveSlot = "Save1";
            this.SaveStatus = string.Empty;
        }

        private void SaveSlot2_Click()
        {
            this.SelectedSaveSlot = "Save2";
            this.SaveStatus = string.Empty;
        }

        private void SaveSlot3_Click()
        {
            this.SelectedSaveSlot = "Save3";
            this.SaveStatus = string.Empty;
        }

        private void MainMenu_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuViewModel();
        }

        private void LoadGame_Click()
        {
            if (this.SelectedSaveSlot == "Save1" || this.SelectedSaveSlot == "Save2" || this.SelectedSaveSlot == "Save3")
            {
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Title = "maingame-" + this.SelectedSaveSlot;
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MainWindow).Dir["InGameMenu"];
            }
            else
            {
                this.SaveStatus = "Please choose a save slot!";
            }
        }

        private void DeleteClick_Click()
        {
            if (GameIO.GameExists("maingame-" + this.SelectedSaveSlot) == true)
            {
                GameIO.DeleteSavedGame("maingame-" + this.SelectedSaveSlot);
                this.SaveStatus = "Deleted successfully";
                this.SelectedSaveSlot = string.Empty;
            }
            else
            {
                this.SelectedSaveSlot = string.Empty;
                this.SaveStatus = "File doesn't exists,\ncan't be removed.";
            }
        }
    }
}
