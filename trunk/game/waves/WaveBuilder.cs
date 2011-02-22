using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.waves
{
    internal class WaveBuilder
    {
        public WavePack Build(Random random)
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

        private IWave BuildIndividualWave(double minWaveLength, double maxWaveLength, double minAmplitude, double maxAmplitude, Random random, bool isOnlyContinuous, bool isAllowSawWave)
        {
            double waveLength = minWaveLength + random.NextDouble() * (maxWaveLength - minWaveLength);
            double amplitude = minAmplitude + random.NextDouble() * (maxAmplitude - minAmplitude);
            double phase = random.NextDouble() * 2.0 - 1.0;

            return new Wave(amplitude, waveLength, phase, WaveFunctions.GetRandomWaveFunction(random, isOnlyContinuous, isAllowSawWave));
        }
    }
}
