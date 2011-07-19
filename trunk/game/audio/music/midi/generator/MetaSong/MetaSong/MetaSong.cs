using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Create random songs according to creation settings
    /// </summary>
    class MetaSong
    {
        #region Private Fields
        /// <summary>
        /// List of riff pack builders (to create song 
        /// </summary>
        private RiffPackBuilder riffPackBuilder;

        /// <summary>
        /// Key modulator
        /// </summary>
        private Modulator modulator;

        /// <summary>
        /// Time frame to turn on or off riffs in song
        /// </summary>
        private TimeFrame timeFrame;
        #endregion

        #region Constructor
        /// <summary>
        /// Create metaSong (normally called by MetaSongBuilder)
        /// </summary>
        /// <param name="riffPackBuilder">riffPackBuilder (create metaRiffs deterministically)</param>
        /// <param name="modulator">key modulator</param>
        /// <param name="timeFrame">time frame</param>
        public MetaSong(RiffPackBuilder riffPackBuilder, Modulator modulator, TimeFrame timeFrame)
        {
            this.riffPackBuilder = riffPackBuilder;
            this.modulator = modulator;
            this.timeFrame = timeFrame;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build a song that can be played
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>song that can be played</returns>
        public RiffPack BuildSong(Random random)
        {
            return riffPackBuilder.Build(random, modulator, timeFrame);
        }
        #endregion
    }
}
