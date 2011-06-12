using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio
{
    /// <summary>
    /// Chromatic scale
    /// </summary>
    internal class ChromaticScale
    {
        #region Fields and parts
        /// <summary>
        /// Intervals
        /// </summary>
        private List<int> intervals;

        /// <summary>
        /// List of major intervals
        /// </summary>
        private List<int> majorChord1;

        /// <summary>
        /// List of major intervals
        /// </summary>
        private List<int> majorChord2;

        /// <summary>
        /// List of major intervals
        /// </summary>
        private List<int> majorChord3;

        /// <summary>
        /// List of minor intervals
        /// </summary>
        private List<int> minorChord1;
        
        /// <summary>
        /// List of minor intervals
        /// </summary>
        private List<int> minorChord2;

        /// <summary>
        /// List of minor intervals
        /// </summary>
        private List<int> minorChord3;

        /// <summary>
        /// List of dissonant intervals
        /// </summary>
        private List<int> dissonantChord;
        #endregion

        #region Constructor
        /// <summary>
        /// Chromatic scale
        /// </summary>
        public ChromaticScale()
        {
            intervals = new List<int>();
            intervals.Add(0);
            intervals.Add(2);
            intervals.Add(4);
            intervals.Add(5);
            intervals.Add(7);
            intervals.Add(9);
            intervals.Add(11);

            majorChord1 = new List<int>();
            majorChord1.Add(0);
            majorChord1.Add(4);
            majorChord1.Add(7);
            majorChord1.Add(12);

            majorChord2 = new List<int>();
            majorChord2.Add(5);
            majorChord2.Add(9);
            majorChord2.Add(12);
            majorChord2.Add(17);

            majorChord3 = new List<int>();
            majorChord3.Add(7);
            majorChord3.Add(11);
            majorChord3.Add(14);
            majorChord3.Add(19);

            minorChord1 = new List<int>();
            minorChord1.Add(2);
            minorChord1.Add(5);
            minorChord1.Add(9);
            minorChord1.Add(14);

            minorChord2 = new List<int>();
            minorChord2.Add(4);
            minorChord2.Add(7);
            minorChord2.Add(11);
            minorChord2.Add(16);

            minorChord3 = new List<int>();
            minorChord3.Add(9);
            minorChord3.Add(12);
            minorChord3.Add(16);
            minorChord3.Add(21);

            dissonantChord = new List<int>();
            dissonantChord.Add(2);
            dissonantChord.Add(5);
            dissonantChord.Add(11);
            dissonantChord.Add(14);
        }
        #endregion
    }
}
