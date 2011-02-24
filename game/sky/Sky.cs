﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

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

            throw new NotImplementedException();
            //surface = BuildSurface(verticalWave, horizontalWave, Program.screenWidth * screenColumnCount, Program.screenHeight * screenRowCount);
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
