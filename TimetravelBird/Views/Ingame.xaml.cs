// <copyright file="Ingame.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Views
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using TimetravelBird.ViewModels;

    /// <summary>
    /// Interaction logic for Ingame.xaml.
    /// </summary>
    public partial class Ingame : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ingame"/> class.
        /// </summary>
        public Ingame()
        {
            this.InitializeComponent();
        }

        private void TimetravelBirdControl_GameEnded(object sender, Model.ITimetravelBirdModel e)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new UpgradeMenuViewModel(e);
        }

        private void TimetravelBirdControl_GameFinished(object sender, System.EventArgs e)
        {
            // Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = (Window.GetWindow(this) as MainWindow).dir["PostGame"];
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new PostGame();
        }
    }
}
