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
    /// Represents a sky (in the back layer)
    /// </summary>
    internal class Sky
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
        /// Sky surface to blit
        /// </summary>
        private Surface surface;
      
        /// <summary>
        /// Height of the sky (in pixels)
        /// </summary>
        public static int skyHeight = Program.screenHeight * screenRowCount;

        /// <summary>
        /// Width of the sky (in pixels)
        /// </summary>
        private static int skyWidth = Program.screenWidth * screenColumnCount;
        #endregion

        #region Constructor
        /// <summary>
        /// Build new sky
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="colorHsl">HSL color</param>
        public Sky(Random random, ColorHsl colorHsl)
        {
            AbstractWave horizontalWaveHue = BuildWave(random);
            AbstractWave horizontalWaveSaturation = BuildWave(random);
            AbstractWave horizontalWaveLightness = BuildWave(random);
            AbstractWave verticalWave = BuildWave(random);


            surface = new Surface(skyWidth,skyHeight,Program.bitDepth);
            
            Surface column = null;
            
            for (int x = 0; x < skyWidth; x++)
            {
                float relativeX = (float)x / (float)Program.screenWidth * 640.0f;
                float verticalWaveOffset = verticalWave[relativeX] / 4.0f;
            	
            	if (column == null)
            	{
            		column = new Surface(1, skyHeight,Program.bitDepth);
	            	for (int y = 0; y < skyHeight; y++)
	            	{
                        float currentHue = colorHsl.Hue;
                        float currentSaturation = colorHsl.Saturation;
                        float currentLightness = colorHsl.Lightness;
                        float relativeY = (float)y / (float)Program.screenHeight * 480.0f;
           		
	            		currentHue += horizontalWaveHue[relativeY];
	            		currentSaturation += horizontalWaveSaturation[relativeY];
	            		currentLightness += horizontalWaveLightness[relativeY];
	            		
	            		currentHue = Math.Max(0, currentHue);
	            		currentSaturation = Math.Max(0, currentSaturation);
	            		currentLightness = Math.Max(0, currentLightness);
	            		
	            		currentHue = Math.Min(255, currentHue);
	            		currentSaturation = Math.Min(255, currentSaturation);
	            		currentLightness = Math.Min(255, currentLightness);
	            		
	            		Color color = ColorTheme.ColorFromHSV(currentHue, currentSaturation / 256.0f, currentLightness / 256.0f);
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
                float amplitude = (float)random.NextDouble() * 7.0f + 1.0f;
                float waveLength = (float)i * 60f;
                float phase = (float)random.NextDouble() * 2.0f - 1.0f;
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
