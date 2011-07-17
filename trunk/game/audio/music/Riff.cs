using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Represents a riff
    /// </summary>
    internal class Riff
    {
        #region Fields and parts
        private AbstractWave pitchWave;

        private RythmPattern rythmPattern;
        #endregion

        #region Constructor
        public Riff(Random random, double length, bool isAllowedTernary, InstrumentType instrumentType)
        {
            pitchWave = MusicWaveBuilder.BuildMusicWave(random);
            rythmPattern = RythmPatternBuilder.Build(random, length, isAllowedTernary, instrumentType);
        }
        #endregion
    }
}
