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

        private Surface surface;

        private IWave horizontalHueWave;

        private IWave horizontalSaturationWave;

        private IWave horizontalLightnessWave;

        private IWave verticalHueWave;

        private IWave verticalSaturationWave;

        private IWave verticalLightnessWave;

        private IWave horizontalThicknessWave;

        private IWave xOffsetInputWave;

        private bool isHueMultiply;

        private bool isSaturationMultiply;

        private bool isLightnessMultiply;

        private bool isUseTopTextureThicknessScaling;

        private bool isUseOffsetInputWave;

        private Dictionary<int, Surface> scalingCache = new Dictionary<int, Surface>();
        #endregion

        #region Constructor
        /// <summary>
        /// Create a random texture
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="color">main color</param>
        public Texture(Random random, Color color, double waveStrengthMultiplicator) : this(random,color,-1, waveStrengthMultiplicator)
        {
        }

        /// <summary>
        /// Create a random texture
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="color">main color</param>
        /// <param name="defaultHeight">default height (how manu tiles)</param>
        public Texture(Random random, Color color, int defaultHeight, double waveStrengthMultiplicator)
        {
            this.color = color;
            horizontalHueWave = BuildWave(random);
            horizontalSaturationWave = BuildWave(random);
            horizontalLightnessWave = BuildWave(random);
            verticalHueWave = BuildWave(random);
            verticalSaturationWave = BuildWave(random);
            verticalLightnessWave = BuildWave(random);

            isUseTopTextureThicknessScaling = random.Next(0, 3) == 0;
            isUseOffsetInputWave = random.Next(0, 3) == 0;
            


            if (Program.isUseTopTextureThicknessScaling && isUseTopTextureThicknessScaling)
                this.horizontalThicknessWave = BuildThicknessWave(random);

            isHueMultiply = random.Next(0, 3) == 0;
            isSaturationMultiply = random.Next(0, 3) == 0;
            isLightnessMultiply = random.Next(0, 3) == 0;
            

            int surfaceWidth = Program.tileSize * 8;
            int surfaceHeight;

            if (defaultHeight == -1)
                surfaceHeight = (int)((double)Program.tileSize * random.NextDouble() * 3.5 + (0.5 * Program.tileSize));
            else
                surfaceHeight = Program.tileSize * defaultHeight;


            if (isUseOffsetInputWave)
            {
                xOffsetInputWave = BuildWave(random);
                xOffsetInputWave.Normalize(surfaceWidth / 16.0 * (double)random.Next(1,5));
            }


            surface = new Surface(surfaceWidth, surfaceHeight, Program.bitDepth);
            surface.Transparent=false;


            double originalHue = color.GetHue();
            double originalSaturation = color.GetSaturation() * 256.0;
            double originalLightness = color.GetBrightness() * 256.0;
            
            for (int x = 0; x < surfaceWidth; x++)
            {
                for (int y = 0; y < surfaceHeight; y++)
                {
                    double relativeY = (double)y / (double)surfaceHeight * 24.0;
                    double relativeX = x;
                        
                    if (isUseOffsetInputWave)
                        relativeX += xOffsetInputWave[y];

                    double currentHue = originalHue;
                    double currentSaturation = originalSaturation;
                    double currentLightness = originalLightness;

                    double verticalHueContribution = verticalHueWave[y] * waveStrengthMultiplicator;
                    double horizontalHueContribution = horizontalHueWave[relativeX] * waveStrengthMultiplicator;

                    double horizontalSaturationContribution = horizontalSaturationWave[relativeX] * waveStrengthMultiplicator;
                    double verticalSaturationContribution = horizontalSaturationWave[y] * waveStrengthMultiplicator;

                    double horizontalLightnessContribution = horizontalLightnessWave[relativeX] * waveStrengthMultiplicator;
                    double verticalLightnessContribution = verticalLightnessWave[y] * waveStrengthMultiplicator;


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

                    if (y == 0)
                        currentLightness += 75;
                    else if (y == 1)
                        currentLightness += 25;

                    currentSaturation = Math.Max(0.0, currentSaturation);
                    currentLightness = Math.Max(1, currentLightness);
                    currentHue = Math.Max(0.0, currentHue);
                    currentSaturation = Math.Min(255.0, currentSaturation);
                    currentLightness = Math.Min(255.0, currentLightness);
                    currentHue = Math.Min(255.0, currentHue);

                    Color currentColor = ColorTheme.ColorFromHSV(currentHue, currentSaturation / 256.0, currentLightness / 256.0);
                    
                    surface.Fill(new Rectangle(x,y,1,1),currentColor);
                }
            }
            
            short circleSize = (short)(surfaceWidth/2);
            //surface.Draw(new Circle(circleSize,circleSize,circleSize),Color.Red);
        }
        #endregion

        #region Private Methods
        private IWave BuildWave(Random random)
        {
            WavePack wavePack = new WavePack();

            int waveCount = random.Next(2, 24);

            for (int i = 1; i < 5; i++)
            {
                double waveLength = (double)Program.tileSize / ((double)random.Next(1, 5)) * random.Next(1, 3) / random.Next(1,3);
                double amplitude = random.NextDouble();
                double phase = random.NextDouble() * 2.0 - 1.0;

                wavePack.Add(new Wave(amplitude, waveLength, phase, WaveFunctions.GetRandomWaveFunction(random, random.Next(0, 2) == 0, random.Next(0, 2) == 0)));
            }

            wavePack.Normalize(random.NextDouble() + 0.5);

            return wavePack;
        }

        private IWave BuildThicknessWave(Random random)
        {
            WavePack wavePack = new WavePack();

            int waveCount = random.Next(2, 24);

            for (int i = 1; i < 5; i++)
            {
                double waveLength = (double)Program.tileSize * (double)random.Next(1, 5);
                double amplitude = random.NextDouble();
                double phase = random.NextDouble() * 2.0 - 1.0;

                wavePack.Add(new Wave(amplitude, waveLength, phase, WaveFunctions.GetRandomWaveFunction(random, random.Next(0, 2) == 0, random.Next(0, 2) == 0)));
            }

            wavePack.Normalize(random.NextDouble() * 0.75 + 0.15);

            return wavePack;
        }

        private int GetRoundedScaling(double scaling)
        {
            return (int)(scaling * 20.0);
        }
        #endregion

        #region Public Methods
        internal Surface GetCachedScaledSurface(double scaling)
        {
            Surface scaledSurface;
            if (scalingCache.TryGetValue(GetRoundedScaling(scaling), out scaledSurface))
                return scaledSurface;
            return null;
        }

        internal void SetCachedScaledSurface(Surface scaledSurface, double scaling)
        {
            scalingCache.Add(GetRoundedScaling(scaling), scaledSurface);
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

        public IWave HorizontalThicknessWave
        {
            get { return horizontalThicknessWave; }
        }

        public bool IsUseTopTextureThicknessScaling
        {
            get { return isUseTopTextureThicknessScaling; }
        }
        #endregion
    }
}
