// <copyright file="PostGame.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using TimetravelBird.ViewModels;

    /// <summary>
    /// Interaction logic for PostGame.xaml.
    /// </summary>
    public partial class PostGame : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostGame"/> class.
        /// </summary>
        public PostGame()
        {
            this.InitializeComponent();
        }

        private void VideoControl_MediaEnded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).DataContext = new HighScoreViewModel();
        }
    }
}
