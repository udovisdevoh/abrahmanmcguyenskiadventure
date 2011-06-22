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

        private AbstractWave horizontalHueWave;

        private AbstractWave horizontalSaturationWave;

        private AbstractWave horizontalLightnessWave;

        private AbstractWave verticalHueWave;

        private AbstractWave verticalSaturationWave;

        private AbstractWave verticalLightnessWave;

        private AbstractWave horizontalThicknessWave;

        private AbstractWave xOffsetInputWave;

        private AbstractWave yOffsetInputWave;

        private bool isHueMultiply;

        private bool isSaturationMultiply;

        private bool isLightnessMultiply;

        private bool isUseTopTextureThicknessScaling;

        private bool isUseXOffsetInputWave;

        private bool isUseYOffsetInputWave;

        private bool isWaveHeightMultiplicator;

        private bool isBumpMapLightness;

        private Dictionary<int, Surface> scalingCache = new Dictionary<int, Surface>();
        #endregion

        #region Constructor
        /// <summary>
        /// Create a random texture
        /// </summary>
        /// <param name="random">random number generator</param>
        public Texture(Random random, int seed, int groundId)
            : this(random, Color.Empty, 1.0f, seed, groundId, true)
        {
        }

        /// <summary>
        /// Create a random texture
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="color">main color</param>
        public Texture(Random random, Color color, float waveStrengthMultiplicator, int seed, int groundId, bool isTop)
            : this(random, color, -1, waveStrengthMultiplicator, seed, groundId, isTop)
        {
        }

        /// <summary>
        /// Create a random texture
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="color">main color</param>
        /// <param name="defaultHeight">default height (how manu tiles)</param>
        public Texture(Random random, Color color, int defaultHeight, float waveStrengthMultiplicator, int seed, int groundId, bool isTop)
        {
            if (color == Color.Empty)
                color = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

            this.color = color;

            isUseTopTextureThicknessScaling = random.Next(0, 3) == 0;
            isUseXOffsetInputWave = random.Next(0, 3) == 0;
            isUseYOffsetInputWave = random.Next(0, 3) == 0;
            isWaveHeightMultiplicator = random.Next(0, 2) == 0;


            if (Program.isUseTopTextureThicknessScaling && isUseTopTextureThicknessScaling)
                this.horizontalThicknessWave = BuildThicknessWave(random);

            isHueMultiply = random.Next(0, 3) == 0;
            isSaturationMultiply = random.Next(0, 3) == 0;
            isLightnessMultiply = random.Next(0, 3) == 0;
            
            if (!isLightnessMultiply && Program.isUseBumpMapLightness)
                isBumpMapLightness = random.Next(0, 3) == 0;
            else
                isBumpMapLightness = false;

            int surfaceWidth = Program.tileSize * 8;
            int surfaceHeight;

            if (defaultHeight == -1)
                surfaceHeight = (int)((double)Program.tileSize * random.NextDouble() * 3.5 + (0.5 * Program.tileSize));
            else
                surfaceHeight = Program.tileSize * defaultHeight;


            int waveHeightMultiplicator = 1;
            if (isWaveHeightMultiplicator)
                waveHeightMultiplicator = surfaceHeight / Program.tileSize;

            horizontalHueWave = BuildWave(random, 1);
            horizontalSaturationWave = BuildWave(random, 1);
            horizontalLightnessWave = BuildWave(random, 1);
            verticalHueWave = BuildWave(random, waveHeightMultiplicator);
            verticalSaturationWave = BuildWave(random, waveHeightMultiplicator);
            verticalLightnessWave = BuildWave(random, waveHeightMultiplicator);

            if (isUseXOffsetInputWave)
            {
                xOffsetInputWave = BuildWave(random, 1);
                xOffsetInputWave.Normalize(surfaceWidth / 16.0f * (float)random.Next(1, 5));
            }

            if (isUseYOffsetInputWave)
            {
                yOffsetInputWave = BuildWave(random,1);
                yOffsetInputWave.Normalize(surfaceWidth / 16.0f * (float)random.Next(1, 5));
            }

            if (!Program.isUseTextureCache || !TextureCache.TryGetCachedSurface(seed, groundId, isTop, Program.screenWidth, Program.screenHeight, out surface))
            {
                if (isBumpMapLightness)
                {
                    horizontalLightnessWave.NormalizeTangent(1.0f, -1.0f, 1.0f, 0f);
                    verticalLightnessWave.NormalizeTangent(1.0f, -1.0f, 1.0f, 0f);
                }


                surface = new Surface(surfaceWidth, surfaceHeight, Program.bitDepth);
                surface.Transparent = false;

                float originalHue = color.GetHue();
                float originalSaturation = color.GetSaturation() * 256.0f;
                float originalLightness = color.GetBrightness() * 256.0f;

                for (int x = 0; x < surfaceWidth; x++)
                {
                    for (int y = 0; y < surfaceHeight; y++)
                    {
                        float relativeY = y;
                        float relativeX = x;

                        if (isUseXOffsetInputWave)
                            relativeX += (Program.isUseWaveValueCache) ? xOffsetInputWave.GetCachedValue(y) : xOffsetInputWave[y];

                        if (isUseYOffsetInputWave)
                            relativeY += (Program.isUseWaveValueCache) ? yOffsetInputWave.GetCachedValue(x) : yOffsetInputWave[x];

                        float currentHue = originalHue;
                        float currentSaturation = originalSaturation;
                        float currentLightness = originalLightness;

                        float verticalHueWaveRelativeY = Program.isUseWaveValueCache ? verticalHueWave.GetCachedValue(relativeY) : verticalHueWave[relativeY];
                        float verticalHueContribution = verticalHueWaveRelativeY * waveStrengthMultiplicator;

                        float horizontalHueWaveRelativeX = Program.isUseWaveValueCache ? horizontalHueWave.GetCachedValue(relativeX) : horizontalHueWave[relativeX];
                        float horizontalHueContribution = horizontalHueWaveRelativeX * waveStrengthMultiplicator;

                        float horizontalSaturationWaveRelativeX = Program.isUseWaveValueCache ? horizontalSaturationWave.GetCachedValue(relativeX) : horizontalSaturationWave[relativeX];
                        float horizontalSaturationContribution = horizontalSaturationWaveRelativeX * waveStrengthMultiplicator;

                        float horizontalSaturationWaveRelativeY = Program.isUseWaveValueCache ? horizontalSaturationWave.GetCachedValue(relativeY) : horizontalSaturationWave[relativeY];
                        float verticalSaturationContribution = horizontalSaturationWaveRelativeY * waveStrengthMultiplicator;

                        float horizontalLightnessContribution;
                        float verticalLightnessContribution;

                        if (isBumpMapLightness)
                        {
                            horizontalLightnessContribution = horizontalLightnessWave.GetTangentValue(relativeX, 1.0f) * waveStrengthMultiplicator;
                            verticalLightnessContribution = verticalLightnessWave.GetTangentValue(relativeY, 1.0f) * waveStrengthMultiplicator;
                        }
                        else
                        {
                            horizontalLightnessContribution = horizontalLightnessWave[relativeX] * waveStrengthMultiplicator;
                            verticalLightnessContribution = verticalLightnessWave[relativeY] * waveStrengthMultiplicator;
                        }


                        if (isHueMultiply)
                            currentHue += (horizontalHueContribution * verticalHueContribution) * 10.0f;
                        else
                            currentHue += ((horizontalHueContribution + verticalHueContribution) * 10.0f);

                        if (isSaturationMultiply)
                            currentSaturation += (horizontalSaturationContribution * verticalSaturationContribution) * 30f;
                        else
                            currentSaturation += ((horizontalSaturationContribution + verticalSaturationContribution) * 30.0f);

                        if (isLightnessMultiply)
                            currentLightness += (horizontalLightnessContribution * verticalLightnessContribution) * 30f;
                        else
                            currentLightness += ((horizontalLightnessContribution + verticalLightnessContribution) * 30.0f);


                        /*while (currentHue < 0.0)
                            currentHue += 256.0;

                        while (currentHue > 255.0)
                            currentHue -= 256.0;*/

                        if (isTop)
                        {
                            if (y == 0)
                                currentLightness += 75;
                            else if (y == 1)
                                currentLightness += 25;
                        }

                        currentSaturation = Math.Max(0.0f, currentSaturation);
                        currentLightness = Math.Max(1f, currentLightness);
                        currentHue = Math.Max(0.0f, currentHue);
                        currentSaturation = Math.Min(255.0f, currentSaturation);
                        currentLightness = Math.Min(255.0f, currentLightness);
                        currentHue = Math.Min(255.0f, currentHue);

                        Color currentColor = ColorTheme.ColorFromHSV(currentHue, currentSaturation / 256.0f, currentLightness / 256.0f);

                        surface.Fill(new Rectangle(x, y, 1, 1), currentColor);
                    }
                }

                if (Program.isUseTextureCache)
                    TextureCache.AddSurfaceToCache(seed, groundId, isTop, Program.screenWidth, Program.screenHeight, surface);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build wave
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="waveLengthMultiplicator">if -1, 0 or 1: ignored.</param>
        /// <returns>wave</returns>
        private AbstractWave BuildWave(Random random, int waveLengthMultiplicator)
        {
            WavePack wavePack = new WavePack();

            int waveCount = random.Next(2, 24);

            for (int i = 1; i < 5; i++)
            {
                float waveLength = (float)Program.tileSize / ((float)random.Next(1, 5)) * random.Next(1, 3) / random.Next(1, 3);
                float amplitude = (float)random.NextDouble();
                float phase = (float)random.NextDouble() * 2.0f - 1.0f;

                if (waveLengthMultiplicator > 1)
                    waveLength *= waveLengthMultiplicator;

                wavePack.Add(new Wave(amplitude, waveLength, phase, WaveFunctions.GetRandomWaveFunction(random, random.Next(0, 2) == 0, random.Next(0, 2) == 0)));
            }

            wavePack.Normalize((float)random.NextDouble() + 0.5f);

            return wavePack;
        }

        private AbstractWave BuildThicknessWave(Random random)
        {
            WavePack wavePack = new WavePack();

            int waveCount = random.Next(2, 24);

            for (int i = 1; i < 5; i++)
            {
                float waveLength = (float)Program.tileSize * (float)random.Next(1, 5);
                float amplitude = (float)random.NextDouble();
                float phase = (float)random.NextDouble() * 2.0f - 1.0f;

                wavePack.Add(new Wave(amplitude, waveLength, phase, WaveFunctions.GetRandomWaveFunction(random, random.Next(0, 2) == 0, random.Next(0, 2) == 0)));
            }

            wavePack.Normalize((float)random.NextDouble() * 0.75f + 0.15f);

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
            set { surface = value; }
        }

        public AbstractWave HorizontalThicknessWave
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
