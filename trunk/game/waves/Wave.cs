using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a wave
    /// </summary>
    public class Wave : AbstractWave
    {
        #region Fields
        /// <summary>
        /// Amplitude (from 0 to 1)
        /// </summary>
        private double amplitude;

        /// <summary>
        /// Amount of wave cycle per common position/time span
        /// </summary>
        private double frequency;

        private Dictionary<int, double> waveValueCache = new Dictionary<int, double>();

        /// <summary>
        /// Phase, from -1 to 1
        /// </summary>
        private double phase;

        /// <summary>
        /// Wave's function
        /// </summary>
        private WaveFunction waveFunction;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a wave
        /// </summary>
        /// <param name="amplitude">Amplitude (from 0 to 1)</param>
        /// <param name="waveLength">Length of the wave</param>
        /// <param name="phase">Phase, from -1 to 1</param>
        public Wave(double amplitude, double waveLength, double phase)
            : this(amplitude, waveLength, phase, null)
        {
        }

        /// <summary>
        /// Create a wave
        /// </summary>
        /// <param name="amplitude">Amplitude (from 0 to 1)</param>
        /// <param name="waveLength">Length of the wave</param>
        /// <param name="phase">Phase, from -1 to 1</param>
        /// <param name="waveFunction">wave function (default: Math.sin)</param>
        public Wave(double amplitude, double waveLength, double phase, WaveFunction waveFunction)
        {
            if (waveFunction == null)
                waveFunction = Math.Sin;

            this.amplitude = amplitude;
            this.frequency = 1.0 / waveLength;
            this.phase = phase;
            this.waveFunction = waveFunction;
        }
        #endregion

        #region Operator Overloads
        /// <summary>
        /// Add two waves
        /// </summary>
        /// <param name="wave1">wave 1</param>
        /// <param name="wave2">wave 2</param>
        /// <returns>wave pack</returns>
        public static AbstractWave operator +(Wave wave1, AbstractWave wave2)
        {
            WavePack wavePack = new WavePack();
            wavePack.Add(wave1);
            wavePack.Add(wave2);
            return wavePack;
        }
        #endregion

        #region AbstractWave Members
        /// <summary>
        /// Get amplitude at position/time x
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>amplitude at position/time x</returns>
        public override double this[double x]
        {
            get
            {
                double value = 0.0;

                x += (phase / frequency);
                value = waveFunction(Math.PI * x * frequency) * amplitude;

                return value;
            }
        }

        public override double GetCachedValue(double x)
        {
            double value;
            int key = (int)(x * 32);
            if (!waveValueCache.TryGetValue(key, out value))
            {
                value = this[x];
                waveValueCache.Add(key, value);
            }
            return value;
        }

        public override void Normalize()
        {
            Normalize(1.0);
        }

        /// <summary>
        /// Normalize the wave to amplitude 1
        /// </summary>
        /// <param name="maxValue">max value</param>
        public override void Normalize(double maxValue)
        {
            Normalize(maxValue, true);
        }

        /// <summary>
        /// Normalize the wave to amplitude 1
        /// </summary>
        /// <param name="maxValue">max value</param>
        /// <param name="isIncreaseToo">true: we can increase amplitude, false: decrease only</param>
        public override void Normalize(double maxValue, bool isIncreaseToo)
        {
            if (isIncreaseToo)
                amplitude = maxValue;
            else
                amplitude = Math.Min(amplitude, maxValue);
        }

        /// <summary>
        /// Whether waves are identical
        /// </summary>
        /// <param name="other">other wave</param>
        /// <returns>Whether waves are identical</returns>
        public override bool Equals(AbstractWave other)
        {
            if (other is Wave)
            {
                Wave otherWave = (Wave)other;
                return phase == otherWave.phase && frequency == otherWave.frequency && amplitude == otherWave.amplitude && waveFunction == otherWave.waveFunction;
            }
            else if (other is WavePack)
            {
                return ((WavePack)other)[0].Equals(this);
            }
            return false;
        }
        #endregion
    }
}
