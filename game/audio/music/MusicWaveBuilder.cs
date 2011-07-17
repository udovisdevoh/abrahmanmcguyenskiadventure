using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Builds music waves
    /// </summary>
    internal static class MusicWaveBuilder
    {
        #region Internal Methods
        /// <summary>
        /// Build a music wave
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        internal static AbstractWave BuildMusicWave(Random random)
        {
            WavePack wavePack = new WavePack();

            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();
            double phase4 = random.NextDouble();
            double phase5 = random.NextDouble();

            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase4 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase5 *= -1.0;

            WaveFunction waveFunction1 = WaveFunctions.GetRandomWaveFunction(random, false, true);
            WaveFunction waveFunction2 = WaveFunctions.GetRandomWaveFunction(random, false, true);
            WaveFunction waveFunction3 = WaveFunctions.GetRandomWaveFunction(random, false, true);
            WaveFunction waveFunction4 = WaveFunctions.GetRandomWaveFunction(random, false, true);
            WaveFunction waveFunction5 = WaveFunctions.GetRandomWaveFunction(random, false, true);

            wavePack.Add(new Wave(random.NextDouble() * 0.45, 2 * random.Next(1, 3), phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 3 * random.Next(1, 3), phase2, waveFunction2));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 4 * random.Next(1, 3), phase3, waveFunction3));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 8 * random.Next(1, 3), phase4, waveFunction4));
            wavePack.Add(new Wave(random.NextDouble() * 0.45, 16 * random.Next(1, 3), phase4, waveFunction5));

            wavePack.Normalize(1.0, true, 0.1, 16.0);

            return wavePack;
        }
        #endregion
    }
}
