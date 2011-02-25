﻿using System;
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
    /// Texture to be applied on a ground
    /// </summary>
    internal class Texture
    {
        #region Fields and parts
        /// <summary>
        /// Main color
        /// </summary>
        private Color color;

        private bool isAlignedToGround;

        private Surface surface;

        private IWave horizontalHueWave;

        private IWave horizontalSaturationWave;

        private IWave horizontalLightnessWave;

        private IWave verticalHueWave;

        private IWave verticalSaturationWave;

        private IWave verticalLightnessWave;

        private bool isHueMultiply;

        private bool isSaturationMultiply;

        private bool isLightnessMultiply;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a random texture
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="color">main color</param>
        public Texture(Random random, Color color)
        {
            this.color = color;
            horizontalHueWave = BuildWave(random);
            horizontalSaturationWave = BuildWave(random);
            horizontalLightnessWave = BuildWave(random);
            verticalHueWave = BuildWave(random);
            verticalSaturationWave = BuildWave(random);
            verticalLightnessWave = BuildWave(random);

            isHueMultiply = random.Next(0, 3) == 0;
            isSaturationMultiply = random.Next(0, 3) == 0;
            isLightnessMultiply = random.Next(0, 3) == 0;
            isAlignedToGround = random.Next(0, 2) == 0;

            int surfaceSize = Program.tileSize * 2;

            surface = new Surface(surfaceSize, surfaceSize, Program.bitDepth);

            
            for (int x = 0; x < surfaceSize; x++)
            {
                double relativeX = (double)x / (double)surfaceSize * 24.0;
                for (int y = 0; y < surfaceSize; y++)
                {
                    double relativeY = (double)y / (double)surfaceSize * 24.0;

                    double currentHue = 128.0;
                    double currentSaturation = 128.0;
                    double currentLightness = 128.0;

                    if (isHueMultiply)
                        currentHue += (horizontalHueWave[x] + verticalHueWave[y]) * 20.0;
                    else
                        currentHue += ((horizontalHueWave[x] * verticalHueWave[y]) * 20.0);

                    if (isSaturationMultiply)
                        currentSaturation += (horizontalSaturationWave[x] + verticalSaturationWave[y]) * 40;
                    else
                        currentSaturation += ((horizontalSaturationWave[x] * verticalSaturationWave[y]) * 40.0);

                    if (isLightnessMultiply)
                        currentLightness += (horizontalLightnessWave[x] + verticalLightnessWave[y]) * 40;
                    else
                        currentLightness += ((horizontalLightnessWave[x] * verticalLightnessWave[y]) * 40.0);


                    /*while (currentHue < 0.0)
                        currentHue += 256.0;

                    while (currentHue > 255.0)
                        currentHue -= 256.0;*/


                    currentSaturation = Math.Max(0, currentSaturation);
                    currentLightness = Math.Max(0, currentLightness);
                    currentHue = Math.Max(0, currentHue);
                    currentSaturation = Math.Min(255, currentSaturation);
                    currentLightness = Math.Min(255, currentLightness);
                    currentHue = Math.Min(255, currentHue);

                    Color currentColor = ColorTheme.ColorFromHSV(currentHue, currentSaturation / 256.0, currentLightness / 256.0);
                    surface.Fill(new Rectangle(x,y,1,1),currentColor);
                }
            }
        }
        #endregion

        #region Private Methods
        private IWave BuildWave(Random random)
        {
            WavePack wavePack = new WavePack();

            int waveCount = random.Next(2, 6);

            for (int i = 1; i < waveCount; i++)
            {
                double waveLength = (double)Program.tileSize / (double)i;
                double amplitude = random.NextDouble();
                double phase = random.NextDouble() * 2.0 - 1.0;
                wavePack.Add(new Wave(amplitude,waveLength,phase, WaveFunctions.GetRandomWaveFunction(random)));
            }

            wavePack.Normalize();

            return wavePack;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Surface
        /// </summary>
        public Surface Surface
        {
            get { return surface; }
        }

        public bool IsAlignedToGround
        {
            get { return isAlignedToGround; }
        }
        #endregion
    }
}
