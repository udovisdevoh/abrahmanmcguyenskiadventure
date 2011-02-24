using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    internal class WaveBuilder
    {
        #region Constants
        private const int defaultHowManyLeaf = 8;
        #endregion

        public WavePack BuildWavePack(Random random)
        {
            WavePack wavePack = new WavePack();
            //Mountains
            do
            {
                wavePack.Add(this.BuildIndividualWave(32, 512, 16, 48, random, true, true));
            } while (random.Next(0, 3) != 0);

            //Platforms
            do
            {
                bool isAllowSawWave = random.Next(0, 2) == 1;
                wavePack.Add(this.BuildIndividualWave(4, 64, 1, 6, random, false, isAllowSawWave));
            } while (random.Next(0, 3) != 0);

            //Soil
            /*do
            {
                wavePack.Add(this.BuildIndividualWave(0.2, 4, 0.1, 0.4, random, false));
            } while (random.Next(0, 2) != 0);*/

            //wavePack.Normalize(random.Next(8,64));

            return wavePack;
        }

        /// <summary>
        /// Build a wave tree
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>wave tree</returns>
        public WaveTree BuildWaveTree(Random random)
        {
            /*List<WaveTree> leafList = new List<WaveTree>();
            for (int i = 0; i < 8; i++)
                leafList.Add(new WaveTree(BuildWave(random)));

            for (int i = 0; i < 8; i+=2)
                firstBranchList.Add(new WaveTree(leafList[));*/

            return BuildWaveTree(random, defaultHowManyLeaf);
        }

        /// <summary>
        /// Build a wave tree
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="howManyLeaf">how many wave leaf (must be a power of 2)</param>
        /// <returns>wave tree</returns>
        public WaveTree BuildWaveTree(Random random, int howManyLeaf)
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
                    maxAmplitude = 48.0;
                    isOnlyContinuous = true;
                    isAllowSawWave = false;
                }

                return new WaveTree(BuildIndividualWave(minWaveLength, maxWaveLength, minAmplitude, maxAmplitude, random, isOnlyContinuous, isAllowSawWave));
            }

            bool isMultNotAdd = random.Next(0, 20) == 0;

            return new WaveTree(BuildWaveTree(random, howManyLeaf / 2), isMultNotAdd, BuildWaveTree(random, howManyLeaf / 2));
        }

        private Wave BuildIndividualWave(double minWaveLength, double maxWaveLength, double minAmplitude, double maxAmplitude, Random random, bool isOnlyContinuous, bool isAllowSawWave)
        {
            double waveLength = minWaveLength + random.NextDouble() * (maxWaveLength - minWaveLength);
            double amplitude = minAmplitude + random.NextDouble() * (maxAmplitude - minAmplitude);
            double phase = random.NextDouble() * 2.0 - 1.0;

            return new Wave(amplitude, waveLength, phase, WaveFunctions.GetRandomWaveFunction(random, isOnlyContinuous, isAllowSawWave));
        }
    }
}
