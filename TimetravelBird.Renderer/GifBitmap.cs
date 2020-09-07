// <copyright file="GifBitmap.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Game.Renderer.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    /// <summary>
    /// Class to handle the gif images.
    /// </summary>
    public class GifBitmap
    {
        private int currentFrame;
        private int frameCount;
        private Bitmap gifBitmap;
        private Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="GifBitmap"/> class.
        /// </summary>
        /// <param name="gif">Name or the file path of the gif.</param>
        /// <param name="dispatcher">Dispatcher.</param>
        /// <param name="isResource">Whether the gif is resource ot not.</param>
        public GifBitmap(string gif, Dispatcher dispatcher, bool isResource)
        {
            this.currentFrame = 0;
            this.StartFrame = -1;
            if (isResource)
            {
                var stream = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream(gif);
                this.gifBitmap = new Bitmap(stream);
            }
            else
            {
                this.gifBitmap = new Bitmap(gif);
            }

            this.frameCount = this.gifBitmap.GetFrameCount(new FrameDimension(this.gifBitmap.FrameDimensionsList[0]));
        }

        /// <summary>
        /// Frame changed event handler.
        /// </summary>
        public event EventHandler FrameChanged;

        /// <summary>
        /// Gets or sets the starting frame of the animation.
        /// </summary>
        public int StartFrame { get; set; }

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public int CurrentFrame
        {
            get { return this.currentFrame; }
        }

        /// <summary>
        /// Gets the framecount.
        /// </summary>
        public int FrameCount
        {
            get { return this.frameCount; }
        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="hObject">Pointer to the object.</param>
        /// <returns>Returns a bool value.</returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        /// <returns>Returns a BitmapSource object.</returns>
        public BitmapSource GetFrame()
        {
            IntPtr hBitmap = this.gifBitmap.GetHbitmap();
            try
            {
                System.Drawing.ImageAnimator.UpdateFrames(this.gifBitmap);
                return Imaging.CreateBitmapSourceFromHBitmap(
                                           hBitmap,
                                           IntPtr.Zero,
                                           Int32Rect.Empty,
                                           BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
                this.currentFrame++;
            }
        }

        /// <summary>
        /// Returns whether the gif is at it's last frame.
        /// </summary>
        /// <returns>True if the frame is the gif's last frame.</returns>
        public bool IsLastFrame()
        {
            if ((this.currentFrame - this.StartFrame + 1) == this.frameCount - 1)
            {
                this.StartFrame = -1;
                return true;
            }

            return false;
        }

        /*private void OnFrameChanged(object o, EventArgs e)
        {
            this.dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => this.FrameChanged?.Invoke(this, EventArgs.Empty)));
        }*/

        /// <summary>
        /// Builds a gif into bitmapsources.
        /// </summary>
        /// <returns>A list of the gifs every frame in bitmapsource.</returns>
        public List<BitmapSource> BuildGif()
        {
            List<BitmapSource> src = new List<BitmapSource>();
            ImageAnimator.UpdateFrames();
            Bitmap[] bitmap = this.ParseFrames(this.gifBitmap);
            for (int i = 0; i < this.frameCount; i++)
            {
                IntPtr hBitmap = bitmap[i].GetHbitmap();
                try
                {
                    System.Drawing.ImageAnimator.UpdateFrames(this.gifBitmap);
                    src.Add(Imaging.CreateBitmapSourceFromHBitmap(
                                               hBitmap,
                                               IntPtr.Zero,
                                               Int32Rect.Empty,
                                               BitmapSizeOptions.FromEmptyOptions()));
                }
                finally
                {
                    DeleteObject(hBitmap);
                    this.currentFrame++;
                }
            }

            return src;
        }

        /// <summary>
        /// Turn the animation into bitmaps.
        /// </summary>
        /// <param name="animation">The gif to turn into images.</param>
        /// <returns>Bitmaps of the image.</returns>
        private Bitmap[] ParseFrames(Bitmap animation)
        {
            int length = animation.GetFrameCount(FrameDimension.Time);
            Bitmap[] frames = new Bitmap[length];
            for (int index = 0; index < length; index++)
            {
                animation.SelectActiveFrame(FrameDimension.Time, index);
                frames[index] = new Bitmap(animation.Size.Width, animation.Size.Height);
                Graphics.FromImage(frames[index]).DrawImage(animation, new System.Drawing.Point(0, 0));
            }

            return frames;
        }
    }
}
