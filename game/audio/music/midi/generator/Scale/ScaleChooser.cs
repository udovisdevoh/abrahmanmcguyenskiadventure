using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Used to choose among available music scales
    /// </summary>
    public class ScaleChooser
    {
        #region Fields
        /// <summary>
        /// Internal list of music scales
        /// </summary>
        private List<Scale> scaleList = new List<Scale>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a scale from available scales
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>a scale from available scales</returns>
        public Scale BuildPreferedScale(Random random)
        {
            if (scaleList.Count < 1)
                throw new ScaleChooserException("Must have at least one scale");

            return scaleList[random.Next(0, scaleList.Count)];
        }

        /// <summary>
        /// Add musical scale to scale list
        /// </summary>
        /// <param name="scale">music scale</param>
        public void Add(Scale scale)
        {
            scaleList.Add(scale);
        }
        #endregion
    }
}
