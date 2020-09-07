// <copyright file="TimetravelBirdControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TimetravelBird
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Game.Renderer.Classes;
    using TimetravelBird.Logic;
    using TimetravelBird.Model;
    using TimetravelBird.Renderer;

    /// <summary>
    /// The control class to handle the main games window methods.
    /// </summary>
    public class TimetravelBirdControl : FrameworkElement
    {
        private static Random random = new Random();
        private static string activeDirectory = Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 24, 24);
        private static string[] birds = new string[5];
        private TimetravelBirdRenderer renderer;
        private ITimeTravelBirdLogic logic;
        private Stopwatch watch;
        private DispatcherTimer mainTimer;
        private bool isLoaded = false;
        private GifBitmap gif;
        private int birdGifFrame;
        private int birdMaxFrame;
        private int weatherFrame;
        private int windFrame;
        private int timeTravelFrame;
        private string currentBird;
        private Dictionary<string, List<BitmapSource>> gifDir;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimetravelBirdControl"/> class.
        /// </summary>
        public TimetravelBirdControl()
        {
            this.Loaded += this.TimetravelBirdControl_Loaded;
        }

        /// <summary>
        /// Handles the gameended event.
        /// </summary>
        public event EventHandler<ITimetravelBirdModel> GameEnded;

        /// <summary>
        /// Handles the gamefinished event.
        /// </summary>
        public event EventHandler GameFinished;

        /// <summary>
        /// If the window is open then it renderers the objects for it.
        /// </summary>
        /// <param name="drawingContext">The objects drawingcontext to render to.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null)
            {
                if (!this.logic.InTimeTravel)
                {
                    drawingContext.DrawDrawing(this.renderer.BuildGameDrawing());
                    if (!this.logic.EndGameAnimation)
                    {
                        this.renderer.DrawWindEffect(drawingContext, this.gifDir["wind"][this.windFrame]);
                        this.renderer.BuildText(drawingContext);
                        this.renderer.DrawWeather(drawingContext, this.gifDir["weather"][this.weatherFrame]);
                    }

                    if (this.logic.Launch == false)
                    {
                        this.renderer.DrawBird(drawingContext, this.gifDir[this.currentBird][this.birdGifFrame]);
                    }

                    _ = this.birdGifFrame == this.birdMaxFrame - 1 ? this.birdGifFrame = 0 : this.birdGifFrame++;
                    _ = this.windFrame == 4 ? this.windFrame = 0 : this.windFrame++;
                    _ = this.weatherFrame == 10 ? this.weatherFrame = 0 : this.weatherFrame++;
                }
                else
                {
                    if (!this.logic.EndGameAnimation || !this.logic.InTimeTravel)
                    {
                        this.renderer.DrawTimeTravel(drawingContext, this.gifDir["travel"][this.timeTravelFrame++]);
                        if (this.timeTravelFrame == 48)
                        {
                            this.timeTravelFrame = 0;
                            this.logic.InTimeTravel = false;
                        }
                    }
                    else
                    {
                        this.mainTimer.Stop();
                        this.renderer.Reset();
                        this.logic.BirdReset();
                    }
                }
            }
        }

        private void TimetravelBirdControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.isLoaded == false)
            {
                this.logic = TimeTravelBirdLogic.GameCreate(Window.GetWindow(this).Title);
                this.renderer = new TimetravelBirdRenderer(this.logic.Model);
                this.watch = new Stopwatch();
                this.birdGifFrame = 0;
                this.weatherFrame = 0;
                this.windFrame = 0;
                this.timeTravelFrame = 0;

                Window win = Window.GetWindow(this);
                if (win != null)
                {
                    win.KeyDown += this.Win_KeyDown;
                }

                this.InitTimer();
                this.InvalidateVisual();
                this.watch.Start();
                this.isLoaded = true;
                this.gifDir = new Dictionary<string, List<BitmapSource>>();
                string[] birdName = { "pinky", "blue", "gray", "red", "yellow" };
                this.currentBird = birdName[random.Next(0, 5)];
                this.gif = new GifBitmap(activeDirectory + @"TimetravelBird.Renderer\assets\birds\" + this.currentBird + "v2.gif", this.Dispatcher, false);
                List<BitmapSource> src = this.gif.BuildGif();
                this.gifDir.Add(this.currentBird, src);
                this.birdMaxFrame = this.gif.FrameCount;
                this.gif = new GifBitmap(activeDirectory + @"TimetravelBird.Renderer\assets\backgrounds\tornado.gif", this.Dispatcher, false);
                src = this.gif.BuildGif();
                this.gifDir.Add("weather", src);
                this.gif = new GifBitmap(activeDirectory + @"TimetravelBird.Renderer\assets\backgrounds\wind2.gif", this.Dispatcher, false);
                src = this.gif.BuildGif();
                this.gifDir.Add("wind", src);
                this.gif = new GifBitmap(activeDirectory + @"TimetravelBird.Renderer\assets\backgrounds\timetravel.gif", this.Dispatcher, false);
                src = this.gif.BuildGif();
                this.gifDir.Add("travel", src);
            }

            this.logic.GameEnded += this.Logic_GameEnded;
            this.logic.GameFinished += this.Logic_GameFinished;
        }

        private void Logic_GameFinished(object sender, EventArgs e)
        {
            this.GameFinished?.Invoke(this, e);
        }

        private void Logic_GameEnded(object sender, ITimetravelBirdModel e)
        {
            this.GameEnded?.Invoke(this, e);
        }

        private void InitTimer()
        {
            if (this.mainTimer == null)
            {
                this.mainTimer = new DispatcherTimer();
                this.mainTimer.Interval = TimeSpan.FromMilliseconds(25);
                this.mainTimer.Tick += this.MainTimer_Tick;
            }
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (this.logic.IsGameEnded() && !this.logic.GameIsOver)
            {
                this.logic.InTimeTravel = false;
                this.mainTimer.Stop();
                this.renderer.Reset();
                this.logic.BirdReset();
                this.logic.GameEndedEvent();
            }
            else if (!this.logic.GameIsOver)
            {
                if (!this.logic.InTimeTravel)
                {
                    this.logic.OneTick();
                    this.InvalidateVisual();
                }
                else
                {
                    this.InvalidateVisual();
                }
            }
            else
            {
                this.renderer = null;
                this.isLoaded = false;
            }
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
           double time = this.watch.ElapsedTicks;
           switch (e.Key)
            {
                case Key.Space:
                    this.logic.BirdBoost();
                    break;
                case Key.P: this.mainTimer.IsEnabled = !this.mainTimer.IsEnabled; break;
                case Key.S: this.logic.LauncherShoot(); this.mainTimer.Start(); break;
                case Key.A: this.logic.RotateBird(e); break;
                case Key.D: this.logic.RotateBird(e); break;
            }
        }
    }
}
