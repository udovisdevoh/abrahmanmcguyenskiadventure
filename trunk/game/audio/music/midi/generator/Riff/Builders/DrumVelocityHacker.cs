using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// To reduce volume of cymbals and stuff like that
    /// </summary>
    class DrumVelocityHacker
    {
        #region Fields
        /// <summary>
        /// Key: midi pitch, Value: multiplicator used for number hacking
        /// </summary>
        private Dictionary<int, double> internalMap;
        #endregion

        #region Constructor
        public DrumVelocityHacker()
        {
            internalMap = new Dictionary<int, double>();

            //Scratch
            internalMap.Add(29, 0.0);
            internalMap.Add(30, 0.0);

            //Bass Drum
            internalMap.Add(35, 0.0);
            internalMap.Add(36, 0.0);
            
            //Snare
            internalMap.Add(28, 0.0);
            internalMap.Add(38, 0.0);
            internalMap.Add(39, 0.0);
            internalMap.Add(40, 0.0);
            

            //Cymbal
            internalMap.Add(49, 0.55);
            internalMap.Add(52, 0.55);
            internalMap.Add(55, 0.40);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Hack velocity for current note
        /// </summary>
        /// <param name="pitch">midi pitch</param>
        /// <param name="velocity">current velocity</param>
        /// <returns>hacked velocity</returns>
        public int HackVelocity(int pitch, int velocity)
        {
            double multiplicator;
            if (internalMap.TryGetValue(pitch, out multiplicator))
                velocity = (int)Math.Round(((double)velocity) * multiplicator);

            if (velocity < 0)
                velocity = 0;
            else if (velocity > 127)
                velocity = 127;

            return velocity;
        }
        #endregion
    }
}
