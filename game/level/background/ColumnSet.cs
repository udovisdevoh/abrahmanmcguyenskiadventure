using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a column in background
    /// </summary>
    internal class ColumnSet
    {
        #region Static and const
        public const double minBrightness = 0.5;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Column's surface
        /// </summary>
        private Surface surface;

        /// <summary>
        /// How many columns in set
        /// </summary>
        private int columnCount;

        private bool isHorizontal;
        #endregion

        #region Constructor
        public ColumnSet(int seed, ColorTheme colorTheme):this(seed,colorTheme,false)
        {
        }

        /// <summary>
        /// Column
        /// </summary>
        /// <param name="groundId">index of ground</param>
        /// <param name="seed">seed for random number generator</param>
        /// <param name="isHorizontal">whether beam is horizontal</param>
        public ColumnSet(int seed, ColorTheme colorTheme, bool isHorizontal)
        {
            this.isHorizontal = isHorizontal;
            Random random = new Random(seed);

            Color color = colorTheme.GetRandomColumnColor(random);

            if (isHorizontal)
                columnCount = random.Next(1, 3);
            else
                columnCount = random.Next(1, 5);

            Texture texture = new Texture(random, color, 1.0, true, random.Next(), 0, false);

            double variance = random.NextDouble() * 2.6 + 0.1;

            AbstractWave shapeWave = BuildShapeWave(random, variance);

            double minimumThickness = (random.NextDouble() * 2 + 1);

            int height = Program.screenHeight;

            if (isHorizontal)
                height = Program.screenWidth;

            double radius = minimumThickness + variance;

            int width = (int)(Math.Round(radius * 2.0 * ((double)Program.tileSize)));

            surface = new Surface(width, height, Program.bitDepth, false);
            surface.Transparent = true;


            int sourceSurfaceHeight = texture.Surface.Height;
            int sourceSurfaceWidth = texture.Surface.Width;


            Surface cylinderSurface = new Surface("./assets/rendered/Cylinder.png");
            Surface scaledCylinder = cylinderSurface.CreateScaledSurface(((double)sourceSurfaceWidth / 648.0), ((double)sourceSurfaceHeight / 648.0), true);
            texture.Surface.Blit(scaledCylinder, new Point(0, 0));


            int x;
            for (int y = 0; y < height; y++)
            {
                double yPositionInWave = (double)(y) / (double)(Program.tileSize);
                double currentRadius = shapeWave[yPositionInWave] + radius;

                int currentWidth = (int)(currentRadius * (double)Program.tileSize);
                x = (width - currentWidth) / 2;


                double zoomX = ((double)currentWidth / (double)sourceSurfaceWidth);

                Surface scaledTextureSurface = texture.Surface.CreateScaledSurface(zoomX, 1.0, true);

                int yFromSource = y;
                while (yFromSource < 0)
                    yFromSource += sourceSurfaceHeight;
                while (yFromSource >= sourceSurfaceHeight)
                    yFromSource -= sourceSurfaceHeight;

                surface.Blit(scaledTextureSurface, new Point(x, y), new Rectangle(0, yFromSource, scaledTextureSurface.Width, 1));
            }

            #region We make the surface twice the height (mirror in the middle
            Surface flippedSurface = surface.CreateFlippedVerticalSurface();
            flippedSurface.Transparent = true;

            Surface compositeSurface = new Surface(width, height * 2, Program.bitDepth);
            compositeSurface.Transparent = true;

            compositeSurface.Blit(surface, new Point(0, 0));
            compositeSurface.Blit(flippedSurface, new Point(0, surface.Height));
            surface = compositeSurface;
            #endregion

            if (isHorizontal)
            {
                surface = surface.CreateRotatedSurface(90, false);
                surface.Transparent = true;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build shape's wave
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="variance">variance</param>
        /// <returns>shape's wave</returns>
        private AbstractWave BuildShapeWave(Random random, double variance)
        {
            int waveCount = random.Next(1, 6);

            WavePack wavePack = new WavePack();

            for (int i = 0; i < waveCount; i++)
            {
                double waveLength = (double)random.Next(1, 8);
                double amplitude = random.NextDouble();
                double phase = random.NextDouble() * 2.0 - 1.0;

                WaveFunction waveFunction = WaveFunctions.GetRandomWaveFunction(random, random.Next(0, 2) == 0, random.Next(0, 2) == 0);

                wavePack.Add(new Wave(amplitude, waveLength, phase, waveFunction));
            }

            wavePack.Normalize(variance);

            return wavePack;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Column's surface
        /// </summary>
        public Surface Surface
        {
            get { return surface; }
        }

        /// <summary>
        /// How many columns in set
        /// </summary>
        public int ColumnCount
        {
            get { return columnCount; }
            set { columnCount = value; }
        }

        public bool IsHorizontal
        {
            get { return isHorizontal; }
        }
        #endregion
    }
}