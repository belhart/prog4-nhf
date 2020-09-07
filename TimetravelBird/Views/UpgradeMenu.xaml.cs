// <copyright file="UpgradeMenu.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Views
{
    using System.Windows.Controls;
    using TimetravelBird.Model;

    /// <summary>
    /// Interaction logic for UpgradeMenu.xaml.
    /// </summary>
    public partial class UpgradeMenu : UserControl
    {
        // private ITimetravelBirdModel Model;
        // public UpgradeMenuViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeMenu"/> class.
        /// </summary>
        /// <param name="model">timetravelmodel interface.</param>
        public UpgradeMenu(ITimetravelBirdModel model)
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeMenu"/> class.
        /// </summary>
        public UpgradeMenu()
        {
            this.InitializeComponent();
        }
    }
}
