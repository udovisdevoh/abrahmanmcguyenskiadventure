using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// To build wave packs and wave trees
    /// </summary>
    internal static class WaveBuilder
    {
        #region Constants
        /// <summary>
        /// How many wave leaf by default in wave trees
        /// </summary>
        private const int defaultHowManyLeaf = 8;
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a wave pack
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave pack</returns>
        public static WavePack BuildWavePack(Random random)
        {
            WavePack wavePack = new WavePack();
            //Mountains
            do
            {
                wavePack.Add(BuildIndividualWave(32, 512, 16, 48, random, true, true));
            } while (random.Next(0, 3) != 0);

            //Platforms
            do
            {
                bool isAllowSawWave = random.Next(0, 2) == 1;
                wavePack.Add(BuildIndividualWave(4, 64, 1, 6, random, false, isAllowSawWave));
            } while (random.Next(0, 3) != 0);

            return wavePack;
        }

        /// <summary>
        /// Build a wave tree
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave tree</returns>
        public static WaveTree BuildWaveTree(Random random)
        {
            return BuildWaveTree(random, defaultHowManyLeaf);
        }

        /// <summary>
        /// Build a wave tree
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="howManyLeaf">how many wave leaf (must be a power of 2)</param>
        /// <returns>wave tree</returns>
        public static WaveTree BuildWaveTree(Random random, int howManyLeaf)
        {
            if (howManyLeaf == 1)
            {
                bool isOnlyContinuous = random.Next(0, 2) == 0;
                bool isAllowSawWave = random.Next(0, 2) == 0;

                float minWaveLength;
                float maxWaveLength;
                float minAmplitude;
                float maxAmplitude;

                if (random.Next(0, 2) == 0)
                {
                    minWaveLength = 4.0f;
                    maxWaveLength = 64.0f;
                    minAmplitude = 1.0f;
                    maxAmplitude = 6.0f;
                }
                else
                {
                    minWaveLength = 32.0f;
                    maxWaveLength = 512.0f;
                    minAmplitude = 16.0f;
                    maxAmplitude = 32.0f;
                    isOnlyContinuous = true;
                    isAllowSawWave = false;
                }

                return new WaveTree(BuildIndividualWave(minWaveLength, maxWaveLength, minAmplitude, maxAmplitude, random, isOnlyContinuous, isAllowSawWave));
            }

            bool isMultNotAdd = random.Next(0, 20) == 0;

            return new WaveTree(BuildWaveTree(random, howManyLeaf / 2), isMultNotAdd, BuildWaveTree(random, howManyLeaf / 2));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Build individual waves
        /// </summary>
        /// <param name="minWaveLength">minimum wave length</param>
        /// <param name="maxWaveLength">maximum wave length</param>
        /// <param name="minAmplitude">minimum amplitude</param>
        /// <param name="maxAmplitude">maximum amplitude</param>
        /// <param name="random">random number generator</param>
        /// <param name="isOnlyContinuous">whether wave doesn't break (only sine and triangle etc)</param>
        /// <param name="isAllowSawWave">whether we allow sawtooth wave</param>
        /// <returns></returns>
        private static Wave BuildIndividualWave(float minWaveLength, float maxWaveLength, float minAmplitude, float maxAmplitude, Random random, bool isOnlyContinuous, bool isAllowSawWave)
        {
            float waveLength = minWaveLength + (float)random.NextDouble() * (maxWaveLength - minWaveLength);
            float amplitude = minAmplitude + (float)random.NextDouble() * (maxAmplitude - minAmplitude);
            float phase = (float)random.NextDouble() * 2.0f - 1.0f;

            WaveFunction waveFunction = WaveFunctions.GetRandomWaveFunction(random, isOnlyContinuous, isAllowSawWave);

            if (waveFunction == WaveFunctions.AbsSin)
                amplitude /= 2.0f;

            return new Wave(amplitude, waveLength, phase, waveFunction);
        }
        #endregion
    }
}
