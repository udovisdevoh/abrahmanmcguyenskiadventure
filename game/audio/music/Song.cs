using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Instrument types
    /// </summary>
    internal enum InstrumentType { Soprano, Alto, Tenor, Bass, Pad, ChromaticPercussion, Drum }

    /// <summary>
    /// Represents a music theme with high level guide lines
    /// </summary>
    internal class Song
    {
        #region Fields
        /// <summary>
        /// Whether we allow ternary time
        /// </summary>
        private bool isAllowedTernary;

        /// <summary>
        /// Tempo
        /// </summary>
        private int tempo;

        /// <summary>
        /// Instrument tracks
        /// </summary>
        private List<InstrumentTrack> listInstrumentTrack;
        #endregion

        #region Constructor
        /// <summary>
        /// Create music mood
        /// </summary>
        /// <param name="random">random number generator</param>
        public Song(Random random)
        {
            tempo = random.Next(80, 160);

            isAllowedTernary = random.Next(0, 2) == 1;

            listInstrumentTrack = new List<InstrumentTrack>();

            listInstrumentTrack.Add(new InstrumentTrack(InstrumentType.Soprano, isAllowedTernary, random));
            listInstrumentTrack.Add(new InstrumentTrack(InstrumentType.Alto, isAllowedTernary, random));
            listInstrumentTrack.Add(new InstrumentTrack(InstrumentType.Tenor, isAllowedTernary, random));
            listInstrumentTrack.Add(new InstrumentTrack(InstrumentType.Bass, isAllowedTernary, random));
            listInstrumentTrack.Add(new InstrumentTrack(InstrumentType.Pad, isAllowedTernary, random));
            listInstrumentTrack.Add(new InstrumentTrack(InstrumentType.ChromaticPercussion, isAllowedTernary, random));
            listInstrumentTrack.Add(new InstrumentTrack(InstrumentType.Drum, isAllowedTernary, random));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether we allow ternary time
        /// </summary>
        public bool IsAllowedTernary
        {
            get { return isAllowedTernary; }
        }
        #endregion
    }
}
