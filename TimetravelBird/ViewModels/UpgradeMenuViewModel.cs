// <copyright file="UpgradeMenuViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using TimetravelBird.Model;
    using TimetravelBird.Repo;

    /// <summary>
    /// A class for the upgrade menu view model.
    /// </summary>
    public class UpgradeMenuViewModel : ViewModelBase
    {
        private double currentmoney;
        private string upgradename;
        private string upgradedesc;
        private double upgradecost;
        private double progressbar;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeMenuViewModel"/> class.
        /// </summary>
        /// <param name="e">timetravelbirdmodel interface.</param>
        public UpgradeMenuViewModel(ITimetravelBirdModel e)
        {
            this.LaunchAgain = new RelayCommand(() => this.LaunchAgain_Click());
            this.QuitToMainMenuClick = new RelayCommand(() => this.QuitToMainMenu_Click());
            this.UpgradeClick = new RelayCommand(() => this.UpgradeClick_Click());
            this.SaveGameClick = new RelayCommand(() => this.SaveGameClick_Click());
            this.CurrentMoney = e.Money;

            // this.model = new ObservableCollection<TimetravelBirdModel>();
            this.Model = e;
            this.BoostyClick = new RelayCommand(() => this.BoostyClick_Click());
            this.LightlyClick = new RelayCommand(() => this.LightlyClick_Click());
            this.ResistancyClick = new RelayCommand(() => this.ResistancyClick_Click());
            this.MoneymultiplyClick = new RelayCommand(() => this.MoneymultiplyClick_Click());
            this.FuelyClick = new RelayCommand(() => this.FuelyClick_Click());
            this.BouncyClick = new RelayCommand(() => this.BouncyClick_Click());
            this.LaunchyClick = new RelayCommand(() => this.LaunchyClick_Click());
            this.GravityClick = new RelayCommand(() => this.GravityClick_Click());
        }

        /// <summary>
        /// Gets or sets the value of the currentmoney.
        /// </summary>
        public double CurrentMoney
        {
            get { return this.currentmoney; }
            set { this.Set(ref this.currentmoney, value); }
        }

        /// <summary>
        /// Gets or sets the name of the selected upgrade item.
        /// </summary>
        public string UpgradeName
        {
            get { return this.upgradename; }
            set { this.Set(ref this.upgradename, value); }
        }

        /// <summary>
        /// Gets or sets the description of the selected upgrade item.
        /// </summary>
        public string UpgradeDesc
        {
            get { return this.upgradedesc; }
            set { this.Set(ref this.upgradedesc, value); }
        }

        /// <summary>
        /// Gets or sets the value of the selected upgrade item.
        /// </summary>
        public double UpgradeCost
        {
            get { return this.upgradecost; }
            set { this.Set(ref this.upgradecost, value); }
        }

        /// <summary>
        /// Gets or sets the value of the progressbar based on the selected upgrade item.
        /// </summary>
        public double ProgressBar
        {
            get { return this.progressbar; }
            set { this.Set(ref this.progressbar, value); }
        }

        // public ObservableCollection<TimetravelBirdModel> model;

        /// <summary>
        /// Gets or sets the bird model.
        /// </summary>
        public ITimetravelBirdModel Model { get; set; }

        /// <summary>
        /// Gets the window's datacontext to ingame.
        /// </summary>
        public ICommand LaunchAgain { get; private set; }

        /// <summary>
        /// Gets the window's datacontext to mainmenuviewmodel.
        /// </summary>
        public ICommand QuitToMainMenuClick { get; private set; }

        /// <summary>
        /// Gets the current game saved.
        /// </summary>
        public ICommand SaveGameClick { get; private set; }

        /// <summary>
        /// Gets the selected upgradeitem next level if the player has enough money.
        /// </summary>
        public ICommand UpgradeClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem1 informations.
        /// </summary>
        public ICommand BoostyClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem2 informations.
        /// </summary>
        public ICommand LightlyClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem3 informations.
        /// </summary>
        public ICommand ResistancyClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem4 informations.
        /// </summary>
        public ICommand MoneymultiplyClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem5 informations.
        /// </summary>
        public ICommand FuelyClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem6 informations.
        /// </summary>
        public ICommand BouncyClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem7 informations.
        /// </summary>
        public ICommand LaunchyClick { get; private set; }

        /// <summary>
        /// Gets the upgradeitem8 informations.
        /// </summary>
        public ICommand GravityClick { get; private set; }

        private void LaunchAgain_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MainWindow).Dir["InGameMenu"];
        }

        private void QuitToMainMenu_Click()
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).DataContext = new MainMenuViewModel();
        }

        private void SaveGameClick_Click()
        {
            GameIO.SaveGame(this.Model);
        }

        private void UpgradeClick_Click()
        {
            switch (this.UpgradeName)
            {
                case Config.UpgradeItem1Name:
                    if (this.Model.Money >= this.Model.UpgradeItem1.Cost && this.Model.UpgradeItem1.Level < 7)
                    {
                        this.Model.UpgradeItem1.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem1.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem1.Cost = Convert.ToInt32(this.Model.UpgradeItem1.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = this.Model.UpgradeItem1.Cost;
                        this.ProgressBar = this.Model.UpgradeItem1.Level * 10;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem1.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                case Config.UpgradeItem2Name:
                    if (this.Model.Money >= this.Model.UpgradeItem2.Cost && this.Model.UpgradeItem2.Level < 7)
                    {
                        this.Model.UpgradeItem2.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem2.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem2.Cost = Convert.ToInt32(this.Model.UpgradeItem2.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = Convert.ToInt32(this.Model.UpgradeItem2.Cost);
                        this.ProgressBar = this.Model.UpgradeItem2.Level * 10;
                        this.Model.Bird.Weight -= 0.5;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem2.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                case Config.UpgradeItem3Name:
                    if (this.Model.Money >= this.Model.UpgradeItem3.Cost && this.Model.UpgradeItem3.Level < 7)
                    {
                        this.Model.UpgradeItem3.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem3.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem3.Cost = Convert.ToInt32(this.Model.UpgradeItem3.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = this.Model.UpgradeItem3.Cost;
                        this.ProgressBar = this.Model.UpgradeItem3.Level * 10;
                        this.Model.Bird.WeatherResistance += 5;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem3.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                case Config.UpgradeItem4Name:
                    if (this.Model.Money >= this.Model.UpgradeItem4.Cost && this.Model.UpgradeItem4.Level < 7)
                    {
                        this.Model.UpgradeItem4.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem4.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem4.Cost = Convert.ToInt32(this.Model.UpgradeItem4.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = this.Model.UpgradeItem4.Cost;
                        this.ProgressBar = this.Model.UpgradeItem4.Level * 10;
                        this.Model.Bird.MoneyBonusModifier += 3;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem4.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                case Config.UpgradeItem5Name:
                    if (this.Model.Money >= this.Model.UpgradeItem5.Cost && this.Model.UpgradeItem5.Level < 7)
                    {
                        this.Model.UpgradeItem5.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem5.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem5.Cost = Convert.ToInt32(this.Model.UpgradeItem5.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = this.Model.UpgradeItem5.Cost;
                        this.ProgressBar = this.Model.UpgradeItem5.Level * 10;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem5.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                case Config.UpgradeItem6Name:
                    if (this.Model.Money >= this.Model.UpgradeItem6.Cost && this.Model.UpgradeItem6.Level < 7)
                    {
                        this.Model.UpgradeItem6.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem6.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem6.Cost = Convert.ToInt32(this.Model.UpgradeItem6.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = this.Model.UpgradeItem6.Cost;
                        this.ProgressBar = this.Model.UpgradeItem6.Level * 10;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem6.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                case Config.UpgradeItem7Name:
                    if (this.Model.Money >= this.Model.UpgradeItem7.Cost && this.Model.UpgradeItem7.Level < 7)
                    {
                        this.Model.UpgradeItem7.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem7.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem7.Cost = Convert.ToInt32(this.Model.UpgradeItem7.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = this.Model.UpgradeItem7.Cost;
                        this.ProgressBar = this.Model.UpgradeItem7.Level * 10;
                        this.Model.Launcher.Power += 5;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem7.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                case Config.UpgradeItem8Name:
                    if (this.Model.Money >= this.Model.UpgradeItem8.Cost && this.Model.UpgradeItem8.Level < 7)
                    {
                        this.Model.UpgradeItem8.Level++;
                        this.Model.Money = this.Model.Money - this.Model.UpgradeItem8.Cost;
                        this.CurrentMoney = Convert.ToInt32(this.Model.Money);
                        this.Model.UpgradeItem8.Cost = Convert.ToInt32(this.Model.UpgradeItem8.Cost * this.Model.ItemUpgradeMultiplier);
                        this.UpgradeCost = this.Model.UpgradeItem8.Cost;
                        this.ProgressBar = this.Model.UpgradeItem8.Level * 10;
                    }
                    else if (this.Model.Money < this.Model.UpgradeItem8.Cost)
                    {
                        this.UpgradeDesc = "You don't have enough \nmoney to buy this item!";
                    }
                    else
                    {
                        this.UpgradeDesc = "You have reached the max \nlevel of this item!";
                    }

                    break;
                default:
                    this.ProgressBar = 0;
                    this.UpgradeDesc = "You can't upgrade nothing!";
                    break;
            }
        }

        private void BoostyClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem1Name;
            this.UpgradeDesc = "Increases the effectiveness\nof the bird's boost.";
            this.UpgradeCost = this.Model.UpgradeItem1.Cost;
            this.ProgressBar = this.Model.UpgradeItem1.Level * 10;
        }

        private void LightlyClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem2Name;
            this.UpgradeDesc = "Decreases the weight of the\nbird making it's fall slower.";
            this.UpgradeCost = this.Model.UpgradeItem2.Cost;
            this.ProgressBar = this.Model.UpgradeItem2.Level * 10;
        }

        private void ResistancyClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem3Name;
            this.UpgradeDesc = "Makes the bird even more\nresistant to the strength \nof the weather.";
            this.UpgradeCost = this.Model.UpgradeItem3.Cost;
            this.ProgressBar = this.Model.UpgradeItem3.Level * 10;
        }

        private void MoneymultiplyClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem4Name;
            this.UpgradeDesc = "Increases the amount of \nmoney after each game.";
            this.UpgradeCost = this.Model.UpgradeItem4.Cost;
            this.ProgressBar = this.Model.UpgradeItem4.Level * 10;
        }

        private void FuelyClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem5Name;
            this.UpgradeDesc = "Increases the amount of fuel\nfor the boost, making the\nboost last longer.";
            this.UpgradeCost = this.Model.UpgradeItem5.Cost;
            this.ProgressBar = this.Model.UpgradeItem5.Level * 10;
        }

        private void BouncyClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem6Name;
            this.UpgradeDesc = "Decreases the loss of the \nspeed after each bounce.";
            this.UpgradeCost = this.Model.UpgradeItem6.Cost;
            this.ProgressBar = this.Model.UpgradeItem6.Level * 10;
        }

        private void LaunchyClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem7Name;
            this.UpgradeDesc = "Increases the effectiveness\nof the launcher.";
            this.UpgradeCost = this.Model.UpgradeItem7.Cost;
            this.ProgressBar = this.Model.UpgradeItem7.Level * 10;
        }

        private void GravityClick_Click()
        {
            this.UpgradeName = Config.UpgradeItem8Name;
            this.UpgradeDesc = "Even we dont know how,\nbut this upgrade somehow\ndecreases the gravity of\nEarth.";
            this.UpgradeCost = this.Model.UpgradeItem8.Cost;
            this.ProgressBar = this.Model.UpgradeItem8.Level * 10;
        }
    }
}
