using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using SdlDotNet.Graphics.Primitives;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a background (in the back layer, sky, wall, mountains etc)
    /// </summary>
    internal class Background
    {
        #region Constants
        /// <summary>
        /// Width of how many screens
        /// </summary>
        private const int screenColumnCount = 1;

        /// <summary>
        /// Height of how many screens
        /// </summary>
        private const int screenRowCount = 2;
        #endregion

        #region Fields and parts
        /// <summary>
        /// Background surface to blit
        /// </summary>
        private Surface surface;
      
        /// <summary>
        /// Height of the background (in pixels)
        /// </summary>
        public static int backgroundHeight = Program.screenHeight * screenRowCount;

        /// <summary>
        /// Width of the background (in pixels)
        /// </summary>
        private static int backgroundWidth = Program.screenWidth * screenColumnCount;
        #endregion

        #region Constructor
        /// <summary>
        /// Build new background
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="colorHsl">HSL color</param>
        public Background(Random random, ColorHsl colorHsl)
        {
            AbstractWave horizontalWaveHue = BuildWave(random);
            AbstractWave horizontalWaveSaturation = BuildWave(random);
            AbstractWave horizontalWaveLightness = BuildWave(random);
            AbstractWave verticalWave = BuildWave(random);


            surface = new Surface(backgroundWidth,backgroundHeight,Program.bitDepth);
            
            Surface column = null;
            
            for (int x = 0; x < backgroundWidth; x++)
            {
            	double relativeX = (double)x / (double)Program.screenWidth * 640.0;
            	double verticalWaveOffset = verticalWave[relativeX] / 4.0;
            	
            	if (column == null)
            	{
            		column = new Surface(1, backgroundHeight,Program.bitDepth);
	            	for (int y = 0; y < backgroundHeight; y++)
	            	{
                        double currentHue = colorHsl.Hue;
                        double currentSaturation = colorHsl.Saturation;
                        double currentLightness = colorHsl.Lightness;
	            		double relativeY = (double)y / (double)Program.screenHeight * 480.0;
           		
	            		currentHue += horizontalWaveHue[relativeY];
	            		currentSaturation += horizontalWaveSaturation[relativeY];
	            		currentLightness += horizontalWaveLightness[relativeY];
	            		
	            		currentHue = Math.Max(0, currentHue);
	            		currentSaturation = Math.Max(0, currentSaturation);
	            		currentLightness = Math.Max(0, currentLightness);
	            		
	            		currentHue = Math.Min(255, currentHue);
	            		currentSaturation = Math.Min(255, currentSaturation);
	            		currentLightness = Math.Min(255, currentLightness);
	            		
	            		Color color = ColorTheme.ColorFromHSV(currentHue, currentSaturation / 256.0, currentLightness / 256.0);
	            		column.Fill(new Rectangle(0,y,1,1), color);
            		}
            	}

            	surface.Blit(column,new Point(x,(int)verticalWaveOffset), column.GetRectangle());
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build wave
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave</returns>
        private AbstractWave BuildWave(Random random)
        {
            WavePack wavePack = new WavePack();
            for (int i = 1; i < 5; i++)
            {
                double amplitude = random.NextDouble() * 7.0 + 1.0;
                double waveLength = (double)i * 60;
                double phase = random.NextDouble() * 2.0 - 1.0;
                wavePack.Add(new Wave(amplitude, waveLength, phase, WaveFunctions.Sine));
            }
            return wavePack;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get surface to blit
        /// </summary>
        public Surface Surface
        {
            get { return surface; }
        }
        #endregion
    }
}
