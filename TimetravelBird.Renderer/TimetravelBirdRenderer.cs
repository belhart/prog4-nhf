// <copyright file="TimetravelBirdRenderer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TimetravelBird.Model;

    /// <summary>
    /// A renderer to display the main game.
    /// </summary>
    public class TimetravelBirdRenderer
    {
        private readonly ITimetravelBirdModel model;
        private readonly Dictionary<string, Brush> myBrushes = new Dictionary<string, Brush>();
        private readonly Typeface font = new Typeface("Arial");
        private readonly Point heightPoint = new Point(Config.GameWidth - 250, 20);
        private readonly Point distancePoint = new Point(Config.GameWidth - 250, Config.GameHeight - 100);
        private readonly Point fuelPoint = new Point(20, 20);
        private readonly Point radPoint = new Point(20, 40);
        private readonly Point dyPoint = new Point(100, 20);

        // private Drawing oldBackground;
        private Drawing oldBird;
        private Drawing oldLauncher;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimetravelBirdRenderer"/> class.
        /// </summary>
        /// <param name="model">A TimetravelBirdModel model to display.</param>
        public TimetravelBirdRenderer(ITimetravelBirdModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets the brush object for the current Bird.
        /// </summary>
        public Brush BirdBrush
        {
            get
            {
                return this.GetBrush(this.model.Bird.FileNameToGif);
            }
        }

        /// <summary>
        /// Gets the brush object for the weather.
        /// </summary>
        public Brush WeatherBrush
        {
            get
            {
                return this.GetBrush(Config.WeatherImageName);
            }
        }

        /// <summary>
        /// Gets the brush object for the timetravel.
        /// </summary>
        public Brush TimeTravelAnimationBrush
        {
            get
            {
                return this.GetBrush(Config.TimeTravelFileName);
            }
        }

        /// <summary>
        /// Gets the brush object for the current city background.
        /// </summary>
        public Brush CityBackgroundBrush
        {
            get
            {
                return this.GetBrush(Config.Levels[this.model.CurrentLevel].FileNameToCityIMG);
            }
        }

        /// <summary>
        /// Gets the brush object for the current Launcher.
        /// </summary>
        public Brush LauncherBrush
        {
            get
            {
                return this.GetBrush("TimetravelBird.Renderer.assets.backgrounds.cannonv2.png");
            }
        }

        /// <summary>
        /// Gets the brush object for the current sky background.
        /// </summary>
        public Brush SkyBackgroundBrush
        {
            get
            {
                return this.GetBrush(Config.Levels[this.model.CurrentLevel].FileNameToSkyIMG);
            }
        }

        /// <summary>
        /// Call this function when a new launch is clicked or when the game begins.
        /// </summary>
        public void Reset()
        {
            this.myBrushes.Clear();
        }

        /// <summary>
        /// Gets the bursh from the exe file if the brush is not in the dictionary already.
        /// </summary>
        /// <param name="fileName">Filename to the picture.</param>
        /// <returns>A brush object with the picture in it already.</returns>
        public Brush GetBrush(string fileName)
        {
            if (!this.myBrushes.ContainsKey(fileName))
            {
                BitmapImage bImage = new BitmapImage();
                bImage.BeginInit();
                bImage.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);
                bImage.EndInit();
                ImageBrush imageBrush = new ImageBrush(bImage);
                this.myBrushes[fileName] = imageBrush;
            }

            return this.myBrushes[fileName];
        }

        /// <summary>
        /// Build all the brushet the main game needs and returns it with a drawinggroup.
        /// </summary>
        /// <returns>A drawing containing all the burshes.</returns>
        public Drawing BuildGameDrawing()
        {
            DrawingGroup gameDrawingGroup = new DrawingGroup();
            this.GetBackground(gameDrawingGroup);
            if (this.model.Bird.RealX == 0)
            {
                gameDrawingGroup.Children.Add(this.GetLauncher());
            }

            return gameDrawingGroup;
        }

        /// <summary>
        /// Gets the background brush for the background.
        /// </summary>
        /// <param name="gameDrawingGroup">The drawinggroup to place the background pictures into.</param>
        public void GetBackground(DrawingGroup gameDrawingGroup)
        {
            if (this.model.Bird.RealY <= 500)
            {
                foreach (OneBackground background in this.model.Backgrounds)
                {
                    Geometry backgroundGeometry = new RectangleGeometry(new Rect(background.ItemCoordX, background.ItemCoordY, Config.GameWidth, Config.GameHeight));

                    // this.oldBackground = new GeometryDrawing(this.BackgroundBrush, null, backgroundGeometry);background.ItemCoordY + (Config.GameHeight*1.1)
                    // gameDrawingGroup.Children.Add(this.oldBackground);
                    gameDrawingGroup.Children.Add(new GeometryDrawing(this.CityBackgroundBrush, null, backgroundGeometry));
                }
            }
            else
            {
                foreach (OneBackground background in this.model.Backgrounds)
                {
                    Geometry backgroundGeometry = new RectangleGeometry(new Rect(background.ItemCoordX, background.ItemCoordY, Config.GameWidth, Config.GameHeight));

                    // this.oldBackground = new GeometryDrawing(this.BackgroundBrush, null, backgroundGeometry);background.ItemCoordY + (Config.GameHeight*1.1)
                    // gameDrawingGroup.Children.Add(this.oldBackground);
                    gameDrawingGroup.Children.Add(new GeometryDrawing(this.SkyBackgroundBrush, null, backgroundGeometry));
                }
            }
        }

        /// <summary>
        /// Gets the bird brush for the bird.
        /// </summary>
        /// <returns>A geometry with the bird picture.</returns>
        public Drawing GetBird()
        {
            // check if y changed
            Geometry birdGeometry = new RectangleGeometry(new Rect(this.model.Bird.ItemCoordX, this.model.Bird.ItemCoordY, Config.BirdObjectWidth, Config.BirdObjectHeight));
            this.oldBird = new GeometryDrawing(this.BirdBrush, null, this.model.Bird.RealArea);
            return this.oldBird;
        }

        /// <summary>
        /// Uses the context and build the 2 text.
        /// </summary>
        /// <param name="ctx">The drawingContext to build into.</param>
        public void BuildText(DrawingContext ctx)
        {
            int heightInInt = Convert.ToInt32(this.model.Bird.RealY) / 10;
            int distanceInInt = Convert.ToInt32(this.model.Bird.RealX) / 10;
            int fuelInt = Convert.ToInt32(this.model.Bird.Fuel);
            double radInt = (this.model.Bird.Rad / Math.PI) * 180;
            double dy = this.model.Bird.DY;
            FormattedText formattedText = new FormattedText(
                "Height: " + heightInInt.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                22,
                Brushes.Black);
            ctx.DrawText(formattedText, this.heightPoint);
            formattedText = new FormattedText(
                "Distance: " + distanceInInt.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                22,
                Brushes.Black);
            ctx.DrawText(formattedText, this.distancePoint);
            formattedText = new FormattedText(
                "Fuel: " + fuelInt.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                this.font,
                20,
                Brushes.OrangeRed);
            ctx.DrawText(formattedText, this.fuelPoint);
        }

        /// <summary>
        /// Gets the launcher brush for the launcher.
        /// </summary>
        /// <returns>A geometry with the launcher picture.</returns>
        public Drawing GetLauncher()
        {
            Geometry launchergeometry = new RectangleGeometry(new Rect(this.model.Launcher.ItemCoordX, this.model.Launcher.ItemCoordY, 60, 60));
            this.oldLauncher = new GeometryDrawing(this.LauncherBrush, null, launchergeometry);
            return this.oldLauncher;
        }

        /// <summary>
        /// Draws the bird's gif on screen.
        /// </summary>
        /// <param name="ctx">context where it will be displayed.</param>
        /// <param name="gif">gif which will be displayed.</param>
        public void DrawBird(DrawingContext ctx, BitmapSource gif)
        {
            ctx.DrawGeometry(new ImageBrush(gif), null, this.model.Bird.RealArea);
        }

        /// <summary>
        /// Draws the weather's gif on screen.
        /// </summary>
        /// <param name="ctx">context where it will be displayed.</param>
        /// <param name="gif">gif which will be displayed.</param>
        public void DrawWeather(DrawingContext ctx, BitmapSource gif)
        {
            ctx.DrawGeometry(new ImageBrush(gif), null, this.model.Weather.RealArea);
            ctx.PushTransform(new RotateTransform((this.model.Bird.Rad / Math.PI) * 180, this.model.Bird.ItemCoordX, this.model.Bird.ItemCoordY));
        }

        /// <summary>
        /// Plays the animation between maps.
        /// </summary>
        /// <param name="ctx">context where it will be displayed.</param>
        /// <param name="gif">gif which will be displayed.</param>
        public void DrawTimeTravel(DrawingContext ctx, BitmapSource gif)
        {
            ctx.DrawGeometry(new ImageBrush(gif), null, new RectangleGeometry(new Rect(0, 0, Config.GameWidth, Config.GameHeight)));
        }

        /// <summary>
        /// Draws the wind's gif on screen.
        /// </summary>
        /// <param name="ctx">context where it will be displayed.</param>
        /// <param name="gif">gif which will be displayed.</param>
        public void DrawWindEffect(DrawingContext ctx, BitmapSource gif)
        {
            ctx.DrawGeometry(new ImageBrush(gif), null, this.model.Wind.RealArea);
        }
    }
}