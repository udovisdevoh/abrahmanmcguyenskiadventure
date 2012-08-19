/*using System;
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
        #region Fields and parts
        /// <summary>
        /// Column's surface
        /// </summary>
        private Surface surface;

        /// <summary>
        /// For column's shape
        /// </summary>
        private AbstractWave shapeWave;

        /// <summary>
        /// How manu columns in set
        /// </summary>
        private int columnCount;
        #endregion

        #region Constructor
        /// <summary>
        /// Column
        /// </summary>
        /// <param name="groundId">index of ground</param>
        /// <param name="seed">seed</param>
        /// <param name="random">random number generator</param>
        public ColumnSet(Random random, Color color)
        {
            columnCount = random.Next(1, 5);

            Texture texture = new Texture(random, color, 1.0, true, random.Next(), 0, false);

            double variance = random.NextDouble() * 2.6 + 0.1;

            shapeWave = BuildShapeWave(random, variance);

            double minimumThickness = (random.NextDouble() * 2 + 1);

            int height = Program.screenHeight;

            double radius = minimumThickness + variance;

            int width = (int)(Math.Round(radius * 2.0 * ((double)Program.tileSize)));

            surface = new Surface(width, height, Program.bitDepth);
            surface.Transparent = true;


            int sourceSurfaceHeight = texture.Surface.Height;
            int sourceSurfaceWidth = texture.Surface.Width;

            Surface cylinderSurface = new Surface("./assets/rendered/Cylinder.png");

            Surface scaledCylinder = cylinderSurface.CreateScaledSurface(((double)sourceSurfaceWidth / 648.0), ((double)sourceSurfaceHeight), true);

            texture.Surface.Blit(scaledCylinder, new Point(0, 0));
            

            int x;
            for (int y = 0; y < height; y++)
            {
                double yPositionInWave = (double)(y) / (double)(Program.tileSize);
                double currentRadius = shapeWave[yPositionInWave] + radius;

                int currentWidth = (int)(currentRadius * (double)Program.tileSize);
                x = (width - currentWidth) / 2;


                double zoomX = ((double)currentWidth / (double)sourceSurfaceWidth);

                Surface scatedTextureSurface = texture.Surface.CreateScaledSurface(zoomX, 1.0, true);

                int yFromSource = y;
                while (yFromSource < 0)
                    yFromSource += sourceSurfaceHeight;
                while (yFromSource >= sourceSurfaceHeight)
                    yFromSource -= sourceSurfaceHeight;

                surface.Blit(scatedTextureSurface, new Point(x, y), new Rectangle(0, yFromSource, scatedTextureSurface.Width, 1));
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
        #endregion
    }
}
*/