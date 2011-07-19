using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Key modulator
    /// </summary>
    public class Modulator
    {
        #region Fields
        /// <summary>
        /// Wave or wavePack
        /// </summary>
        private AbstractWave wave;

        /// <summary>
        /// Modulation strength (from 0: none to 1: full)
        /// </summary>
        private double modulationStrength;
        #endregion

        #region Constructor
        /// <summary>
        /// Create modulator
        /// </summary>
        /// <param name="wave">wave</param>
        /// <param name="modulationStrength">from 0 (none) to 1 (full)</param>
        public Modulator(AbstractWave wave, double modulationStrength)
        {
            this.wave = wave;
            this.modulationStrength = modulationStrength;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Return modulation value (from -6 to 6);
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>modulation offset</returns>
        public int GetModulationOffset(double position)
        {
            position = Math.Floor(position);

            double absoluteModulation = wave[position] * (12.0 * modulationStrength) - 6.0;
            int absoluteModulationDiscrete = (int)Math.Round(absoluteModulation);

            while (absoluteModulationDiscrete > 6)
                absoluteModulationDiscrete -= 12;
            while (absoluteModulationDiscrete < 6)
                absoluteModulationDiscrete += 12;

            if (absoluteModulationDiscrete == 1)
                absoluteModulationDiscrete = 0;
            if (absoluteModulationDiscrete == 2)
                absoluteModulationDiscrete = 0;
            if (absoluteModulationDiscrete == 3)
                absoluteModulationDiscrete = 5;
            if (absoluteModulationDiscrete == 4)
                absoluteModulationDiscrete = 5;
            if (absoluteModulationDiscrete == 6)
                absoluteModulationDiscrete = 7;

            if (absoluteModulationDiscrete == -1)
                absoluteModulationDiscrete = 0;
            if (absoluteModulationDiscrete == -2)
                absoluteModulationDiscrete = 0;
            if (absoluteModulationDiscrete == -3)
                absoluteModulationDiscrete = -5;
            if (absoluteModulationDiscrete == -4)
                absoluteModulationDiscrete = -5;
            if (absoluteModulationDiscrete == -6)
                absoluteModulationDiscrete = -7;




            while (absoluteModulationDiscrete > 6)
                absoluteModulationDiscrete -= 12;
            while (absoluteModulationDiscrete < -6)
                absoluteModulationDiscrete += 12;

            return absoluteModulationDiscrete;
        }
        #endregion
    }
}
