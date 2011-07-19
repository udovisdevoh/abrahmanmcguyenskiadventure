﻿using System;
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
    internal class Song : IEnumerable<InstrumentTrack>
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

        /// <summary>
        /// Chord progression
        /// </summary>
        private ChordProgression chordProgression;

        /// <summary>
        /// Song's length
        /// </summary>
        private double length;
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

            length = InstrumentTrack.GetMaxLength(listInstrumentTrack);

            chordProgression = new ChordProgression(random);
        }
        #endregion

        #region IEnumerable<InstrumentTrack> Members
        public IEnumerator<InstrumentTrack> GetEnumerator()
        {
            return listInstrumentTrack.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return listInstrumentTrack.GetEnumerator();
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

        /// <summary>
        /// Tempo
        /// </summary>
        public int Tempo
        {
            get { return tempo; }
        }

        /// <summary>
        /// Chord progression
        /// </summary>
        public ChordProgression ChordProgression
        {
            get { return chordProgression; }
        }

        /// <summary>
        /// Song's length
        /// </summary>
        public double Length
        {
            get { return length; }
        }
        #endregion
    }
}
