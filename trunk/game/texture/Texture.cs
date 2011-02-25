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


            double originalHue = color.GetHue();
            double originalSaturation = color.GetSaturation() * 256.0;
            double originalLightness = color.GetBrightness() * 256.0;
            
            for (int x = 0; x < surfaceSize; x++)
            {
                double relativeX = (double)x / (double)surfaceSize * 24.0;
                for (int y = 0; y < surfaceSize; y++)
                {
                    double relativeY = (double)y / (double)surfaceSize * 24.0;

                    double currentHue = originalHue;
                    double currentSaturation = originalSaturation;
                    double currentLightness = originalLightness;

                    double verticalHueContribution = verticalHueWave[y];
                    double horizontalHueContribution = horizontalHueWave[x];

                    double horizontalSaturationContribution = horizontalSaturationWave[x];
                    double verticalSaturationContribution = horizontalSaturationWave[y];

                    double horizontalLightnessContribution = horizontalLightnessWave[x];
                    double verticalLightnessContribution = verticalLightnessWave[y];


                    if (isHueMultiply)
                        currentHue += (horizontalHueContribution * verticalHueContribution) * 10.0;
                    else
                        currentHue += ((horizontalHueContribution + verticalHueContribution) * 10.0);

                    if (isSaturationMultiply)
                        currentSaturation += (horizontalSaturationContribution * verticalSaturationContribution) * 30;
                    else
                        currentSaturation += ((horizontalSaturationContribution + verticalSaturationContribution) * 30.0);

                    if (isLightnessMultiply)
                        currentLightness += (horizontalLightnessContribution * verticalLightnessContribution) * 30;
                    else
                        currentLightness += ((horizontalLightnessContribution + verticalLightnessContribution) * 30.0);


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

            for (int i = 1; i < 5; i++)
            {
                double waveLength = (double)Program.tileSize / ((double)random.Next(1, 5) * random.Next(1,3));
                double amplitude = random.NextDouble();
                double phase = random.NextDouble() * 2.0 - 1.0;

                wavePack.Add(new Wave(amplitude, waveLength, phase, WaveFunctions.GetRandomWaveFunction(random, random.Next(0, 2) == 0, random.Next(0, 2) == 0)));
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
