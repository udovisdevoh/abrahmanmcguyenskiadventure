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
        private const int screenColumnCount = 3;

        private const int screenRowCount = 3;
        #endregion

        #region Fields and parts
        private Surface surface;

        private int hue;

        private int saturation;

        private int lightness;
        
        private static int skyHeight = Program.screenHeight * screenRowCount;
        
        private static int skyWidth = Program.screenWidth * screenColumnCount;
        #endregion

        #region Constructor
        public Sky(Random random)
        {
            hue = random.Next(0, 256);
            saturation = random.Next(24, 224);
            lightness = random.Next(32, 256);

            IWave horizontalWaveHue = BuildWave(random);
            IWave horizontalWaveSaturation = BuildWave(random);
            IWave horizontalWaveLightness = BuildWave(random);
            IWave verticalWave = BuildWave(random);

            surface = new Surface(skyWidth,skyHeight,16);
            
            Surface column = null;
            
            for (int x = 0; x < skyWidth; x++)
            {
            	double verticalWaveOffset = verticalWave[x];
            	
            	if (column == null)
            	{
            		column = new Surface(1, skyHeight,16);
	            	for (int y = 0; y < skyHeight; y++)
	            	{
	            		double currentHue = hue;
	            		double currentSaturation = saturation;
	            		double currentLightness = lightness;
	            		
           		
	            		currentHue += horizontalWaveHue[(double)y];
	            		currentSaturation += horizontalWaveSaturation[(double)y];
	            		currentLightness += horizontalWaveLightness[(double)y];
	            		
	            		Color color = ColorTheme.ColorFromHSV(currentHue, currentSaturation / 256.0, currentLightness / 256.0);
	            		column.Fill(new Rectangle(0,y,1,1), color);
            		}
            	}
            	surface.Blit(column,new Point(x,(int)verticalWaveOffset));
            }
        }
        #endregion

        #region Private Methods
        private IWave BuildWave(Random random)
        {
            WavePack wavePack = new WavePack();
            for (int i = 1; i < 5; i++)
            {
                double amplitude = random.NextDouble() * 7.0 + 1.0;
                double waveLength = (double)i;
                double phase = random.NextDouble() * 2.0 - 1.0;
                wavePack.Add(new Wave(amplitude, waveLength, phase, WaveFunctions.Sine));
            }
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
