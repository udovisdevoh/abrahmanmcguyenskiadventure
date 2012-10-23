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
    /// Represents hills in parallax background
    /// </summary>
    internal class HillSet
    {
        #region Static and const
        /// <summary>
        /// Minimum brightness
        /// </summary>
        public const double minBrightness = 0.5;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Column's surface
        /// </summary>
        private Surface surface;
        #endregion

        #region Constructor
        /// <summary>
        /// Build hill set
        /// </summary>
        /// <param name="seed">seed for random generator</param>
        /// <param name="colorTheme">color theme</param>
        public HillSet(int seed, ColorTheme colorTheme)
        {
            Random random = new Random(seed);

            Color color = colorTheme.GetRandomColumnColor(random);

            Texture texture = new Texture(random, color, 1.0, true, random.Next(), 0, false);

            Surface textureSurface = texture.Surface;
            int textureSurfaceHeight = textureSurface.Height;
            int textureSurfaceWidth = textureSurface.Width;

            AbstractWave hillWave = BuildHillWave(random);
            

            int surfaceHeight = (int)((double)Program.screenHeight * 1.23);
            int surfaceWidth = Program.screenWidth/* * 2*/;


            int shadeSizeSurfaceSize = surfaceHeight / 2;

            /*Surface hillShadesSurface = new Surface("./assets/rendered/HillShades.png");
            hillShadesSurface = hillShadesSurface.CreateScaledSurface((double)shadeSizeSurfaceSize / (double)hillShadesSurface.Width);
            hillShadesSurface.Transparent = true;*/

            surface = new Surface(surfaceWidth, surfaceHeight, Program.bitDepth, false);
            surface.Transparent = true;


            for (int x = 0; x < surfaceWidth; x++)
            {
                
                int y = GetY(hillWave, x, surfaceWidth, surfaceHeight);
                int xFromSource = x % textureSurfaceWidth;

                int yWithOffset = y;
                while (yWithOffset < surfaceHeight)
                {
                    surface.Blit(textureSurface, new Point(x, yWithOffset), new Rectangle(xFromSource, 0, 1, textureSurfaceHeight));
                    yWithOffset += textureSurfaceHeight;
                }

                int shadeXOffset = GetShadeXOffset(shadeSizeSurfaceSize, hillWave[GetXPositionInWave(x, surfaceWidth)], hillWave[GetXPositionInWave(x+1, surfaceWidth)]);

                /*surface.Blit(hillShadesSurface, new Point(x, y), new Rectangle(shadeXOffset, 0, 1, shadeSizeSurfaceSize));*/
            }
        }
        #endregion

        #region Private Methods
        private double GetXPositionInWave(int x, int surfaceWidth)
        {
            return (double)(x) / (double)(surfaceWidth) * 12.0;
        }

        private int GetY(AbstractWave hillWave, int x, int surfaceWidth, int surfaceHeight)
        {
            return (int)((hillWave[GetXPositionInWave(x, surfaceWidth)] + 1.0) / 4.0 * (double)surfaceHeight);
        }

        private int GetShadeXOffset(int shadeSizeSurfaceSize, double y, double previousY)
        {
            int xOffset = shadeSizeSurfaceSize / 2;

            /*if (y > previousY)
                return 0;
            else if (y < previousY)
                return 359;
            else
                return 180;*/

            int difference = (int)Math.Round((y - previousY) * (double)shadeSizeSurfaceSize * 20);
            if (difference > shadeSizeSurfaceSize / 2 - 1)
                difference = shadeSizeSurfaceSize / 2 - 1;
            else if (difference < -(shadeSizeSurfaceSize / 2))
                difference = -(shadeSizeSurfaceSize / 2);

            xOffset += difference;

            return xOffset;
        }

        /// <summary>
        /// Build the hillset's wave
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>hill's wave</returns>
        private AbstractWave BuildHillWave(Random random)
        {
            int waveCount = random.Next(1, 6);

            WavePack wavePack = new WavePack();


            for (int i = 0; i < waveCount; i++)
            {
                double waveLength = 12.0 / (double)(random.Next(1, 13));
                double amplitude = random.NextDouble() * waveLength;
                double phase = random.NextDouble() * 2.0 - 1.0;

                WaveFunction waveFunction = WaveFunctions.GetRandomWaveFunction(random, random.Next(0, 2) == 0, random.Next(0, 2) == 0);

                wavePack.Add(new Wave(amplitude, waveLength, phase, waveFunction));
            }

            wavePack.Normalize(1.0, true, 0.01, 12.0);

            return wavePack;
        }
        #endregion

        #region Properties
        public Surface Surface
        {
            get { return surface; }
        }
        #endregion
    }
}
