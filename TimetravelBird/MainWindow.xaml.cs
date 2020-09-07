// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using TimetravelBird.ViewModels;
    using TimetravelBird.Views;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.Dir = new Dictionary<string, UserControl>();
            this.Dir.Add("PostGame", new PostGame());

            // this.dir.Add("MainMenu", new MainMenu());
            // this.dir.Add("LoadSaveMenu", new LoadSaveMenu());
            this.Dir.Add("InGameMenu", new Ingame());
            this.DataContext = new MainMenuViewModel();
        }

        /// <summary>
        /// Gets or sets a dictionary which stores the views.
        /// </summary>
        public Dictionary<string, UserControl> Dir { get; set; }
    }
}
