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

        #region Internal Methods
        /// <summary>
        /// Build a wave pack
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="isOnlyContinuous">true: no saw wave nor square wave</param>
        /// <returns>wave pack</returns>
        internal static WavePack BuildWavePack(Random random, bool isOnlyContinuous, bool isCurvyWaveOnly)
        {
            WavePack wavePack = new WavePack();
            //Mountains
            do
            {
                wavePack.Add(BuildIndividualWave(32, 512, 16, 48, random, true || isOnlyContinuous, true, isCurvyWaveOnly));
            } while (random.Next(0, 3) != 0);

            //Platforms
            do
            {
                bool isAllowSawWave = random.Next(0, 2) == 1;
                wavePack.Add(BuildIndividualWave(4, 64, 1, 6, random, false || isOnlyContinuous, isAllowSawWave, isCurvyWaveOnly));
            } while (random.Next(0, 3) != 0);

            return wavePack;
        }

        /// <summary>
        /// Build a wave tree
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave tree</returns>
        internal static WaveTree BuildWaveTree(Random random)
        {
            return BuildWaveTree(random, defaultHowManyLeaf);
        }

        /// <summary>
        /// Build a wave tree
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="howManyLeaf">how many wave leaf (must be a power of 2)</param>
        /// <returns>wave tree</returns>
        internal static WaveTree BuildWaveTree(Random random, int howManyLeaf)
        {
            if (howManyLeaf == 1)
            {
                bool isOnlyContinuous = random.Next(0, 2) == 0;
                bool isAllowSawWave = random.Next(0, 2) == 0;

                double minWaveLength;
                double maxWaveLength;
                double minAmplitude;
                double maxAmplitude;

                if (random.Next(0, 2) == 0)
                {
                    minWaveLength = 4.0;
                    maxWaveLength = 64.0;
                    minAmplitude = 1.0;
                    maxAmplitude = 6.0;
                }
                else
                {
                    minWaveLength = 32.0;
                    maxWaveLength = 512.0;
                    minAmplitude = 16.0;
                    maxAmplitude = 32.0;
                    isOnlyContinuous = true;
                    isAllowSawWave = false;
                }

                return new WaveTree(BuildIndividualWave(minWaveLength, maxWaveLength, minAmplitude, maxAmplitude, random, isOnlyContinuous, isAllowSawWave, false));
            }

            bool isMultNotAdd = random.Next(0, 20) == 0;

            return new WaveTree(BuildWaveTree(random, howManyLeaf / 2), isMultNotAdd, BuildWaveTree(random, howManyLeaf / 2));
        }

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
        internal static Wave BuildIndividualWave(double minWaveLength, double maxWaveLength, double minAmplitude, double maxAmplitude, Random random, bool isOnlyContinuous, bool isAllowSawWave, bool isCurvyWaveOnly)
        {
            double waveLength = minWaveLength + random.NextDouble() * (maxWaveLength - minWaveLength);
            double amplitude = minAmplitude + random.NextDouble() * (maxAmplitude - minAmplitude);
            double phase = random.NextDouble() * 2.0 - 1.0;

            WaveFunction waveFunction;
            if (isCurvyWaveOnly)
                waveFunction = WaveFunctions.Sine;
            else
                waveFunction = WaveFunctions.GetRandomWaveFunction(random, isOnlyContinuous, isAllowSawWave);

            if (waveFunction == WaveFunctions.AbsSin)
                amplitude /= 2.0;

            return new Wave(amplitude, waveLength, phase, waveFunction);
        }
        #endregion
    }
}
