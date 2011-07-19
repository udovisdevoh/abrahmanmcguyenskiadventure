using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Represents a song generator preset that can be saved, load and used to build MetaSongs
    /// </summary>
    public class PredefinedGenerator : IEnumerable<PredefinedGeneratorTrack>
    {
        #region Const
        /// <summary>
        /// Maximum track count
        /// </summary>
        private const int maxTrackCount = 16;
        #endregion

        #region Fields
        /// <summary>
        /// Whether we override tempo
        /// </summary>
        private bool isOverrideTempo;

        /// <summary>
        /// Whether we override scale
        /// </summary>
        private bool isOverrideScale;

        /// <summary>
        /// Scale's name
        /// </summary>
        private string scaleName;

        /// <summary>
        /// Tempo
        /// </summary>
        private int tempo;

        /// <summary>
        /// How many bars
        /// </summary>
        private int barCount = 24;

        /// <summary>
        /// Modulation
        /// </summary>
        private double modulation;

        /// <summary>
        /// List of predefined generator track
        /// </summary>
        private List<PredefinedGeneratorTrack> trackList;

        /// <summary>
        /// 0: start lyrics in same time as music
        /// -1: start 1 black note before music bar
        /// -2: start 1 black note before music bar
        /// </summary>
        private double lyricsToMusicPhase = 0;

        /// <summary>
        /// Whether we override key
        /// </summary>
        private bool isOverrideKey;

        /// <summary>
        /// Works only if IsOverrideKey
        /// Override default modulation offset
        /// </summary>
        private int forcedModulationOffset;
        #endregion

        #region Constructor
        /// <summary>
        /// Build predefined generator
        /// </summary>
        public PredefinedGenerator()
        {
            barCount = 16;
            isOverrideTempo = false;
            isOverrideScale = false;
            tempo = 120;
            modulation = 0.15;
            scaleName = "minorPentatonic";
            trackList = new List<PredefinedGeneratorTrack>();
            for (int i = 0; i < maxTrackCount; i++)
                trackList.Add(new PredefinedGeneratorTrack("",barCount));
        }
        #endregion

        #region Properties
        /// <summary>
        /// How many tracks
        /// </summary>
        public int Count
        {
            get
            {
                return trackList.Count;
            }
        }

        /// <summary>
        /// Whether we override tempo
        /// </summary>
        public bool IsOverrideTempo
        {
            get { return isOverrideTempo; }
            set { isOverrideTempo = value; }
        }

        /// <summary>
        /// Whether we override scale
        /// </summary>
        public bool IsOverrideScale
        {
            get { return isOverrideScale; }
            set { isOverrideScale = value; }
        }

        /// <summary>
        /// Scale's name
        /// </summary>
        public string ScaleName
        {
            get { return scaleName; }
            set { scaleName = value; }
        }

        /// <summary>
        /// Get or set tempo
        /// </summary>
        public int Tempo
        {
            get { return tempo; }
            set { tempo = value; }
        }

        /// <summary>
        /// Get or set modulation
        /// </summary>
        public double Modulation
        {
            get { return modulation; }
            set { modulation = value; }
        }

        /// <summary>
        /// Get or set how many bars
        /// </summary>
        public int BarCount
        {
            get { return barCount; }
            set { barCount = value; }
        }
        
        /// <summary>
        /// Get predefined generator track at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>predefined generator track at index</returns>
        public PredefinedGeneratorTrack this[int index]
        {
            get
            {
                while (trackList.Count - 1 < index)
                    trackList.Add(new PredefinedGeneratorTrack("", barCount));

                return trackList[index];
            }
        }

        /// <summary>
        /// 0: start lyrics in same time as music
        /// -1: start 1 black note before music bar
        /// -2: start 1 black note before music bar
        /// </summary>
        public double LyricsToMusicPhase
        {
            get { return lyricsToMusicPhase; }
            set { lyricsToMusicPhase = value; }
        }

        /// <summary>
        /// Whether we override key
        /// </summary>
        public bool IsOverrideKey
        {
            get { return isOverrideKey; }
            set { isOverrideKey = value; }
        }

        /// <summary>
        /// Works only if IsOverrideKey
        /// Override default modulation offset
        /// </summary>
        public int ForcedModulationOffset
        {
            get { return forcedModulationOffset; }
            set { forcedModulationOffset = value; }
        }
        #endregion

        #region IEnumerable<GeneratorTrack> Members
        /// <summary>
        /// Predefined generator track enumerator
        /// </summary>
        /// <returns>Predefined generator track enumerator</returns>
        public IEnumerator<PredefinedGeneratorTrack> GetEnumerator()
        {
            return trackList.GetEnumerator();
        }

        /// <summary>
        /// Predefined generator track enumerator
        /// </summary>
        /// <returns>Predefined generator track enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return trackList.GetEnumerator();
        }
        #endregion
    }
}
