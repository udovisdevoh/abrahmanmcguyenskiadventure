using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Create key modulators
    /// </summary>
    class ModulatorBuilder
    {
        #region Public Method
        /// <summary>
        /// Create  modulator
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="modulationStrength">0: none 1: full</param>
        /// <returns>New key modulator</returns>
        public Modulator Build(Random random, double modulationStrength)
        {
            double phase1 = random.NextDouble();
            double phase2 = random.NextDouble();
            double phase3 = random.NextDouble();

            if (random.Next(0, 2) == 1)
                phase1 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase2 *= -1.0;
            if (random.Next(0, 2) == 1)
                phase3 *= -1.0;

            WaveFunction waveFunction1 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction2 = WaveFunctions.GetRandomWaveFunction(random);
            WaveFunction waveFunction3 = WaveFunctions.GetRandomWaveFunction(random);

            double modulationWaveLengthMultiplicator = 2.0;//0.125;

            WavePack wavePack = new WavePack();
            wavePack.Add(new Wave(random.NextDouble(), 1 * modulationWaveLengthMultiplicator, phase1, waveFunction1));
            wavePack.Add(new Wave(random.NextDouble(), 2 * modulationWaveLengthMultiplicator, phase2, waveFunction2));
            wavePack.Add(new Wave(random.NextDouble(), 4 * modulationWaveLengthMultiplicator, phase3, waveFunction3));
            wavePack.Normalize(1.0, true, 0.001, 2.0);

            return new Modulator(wavePack, modulationStrength);
        }
        #endregion
    }
}