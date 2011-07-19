using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Rythm pattern
    /// </summary>
    public class RythmPattern : IEnumerable<double>
    {
        #region Fields
        /// <summary>
        /// List of beat times
        /// </summary>
        private List<double> beatTimeList = new List<double>();

        /// <summary>
        /// Current position
        /// </summary>
        private int position = 0;
        #endregion

        #region Public Methods
        /// <summary>
        /// Add beat time
        /// </summary>
        /// <param name="beatTime">beat time</param>
        public void Add(double beatTime)
        {
            beatTimeList.Add(beatTime);
        }

        /// <summary>
        /// Reset position
        /// </summary>
        public void ResetPosition()
        {
            position = 0;
        }

        /// <summary>
        /// Get next time length
        /// </summary>
        /// <returns>next time length</returns>
        public double GetNextLength()
        {
            while (position >= beatTimeList.Count)
                position -= beatTimeList.Count;

            double currentTime = beatTimeList[position];

            position++;

            return currentTime;
        }

        /// <summary>
        /// Shortest time length in rythm pattern
        /// </summary>
        /// <returns>Shortest time length in rythm pattern</returns>
        public double Min()
        {
            return beatTimeList.Min();
        }

        /// <summary>
        /// Clone rythm pattern
        /// </summary>
        /// <returns>identical rythm pattern</returns>
        public RythmPattern Clone()
        {
            RythmPattern cloned = new RythmPattern();
            cloned.beatTimeList = new List<double>(this.beatTimeList);
            return cloned;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Build rythm pattern
        /// </summary>
        public RythmPattern()
        {
        }

        /// <summary>
        /// Build rythm pattern by joining two rythm patterns (one after one other)
        /// </summary>
        /// <param name="rythmPattern1">first</param>
        /// <param name="rythmPattern2">second</param>
        public RythmPattern(RythmPattern rythmPattern1, RythmPattern rythmPattern2)
        {
            beatTimeList = new List<double>(rythmPattern1);
            beatTimeList.AddRange(rythmPattern2.beatTimeList);
        }
        #endregion

        #region IEnumerable<double> Members
        /// <summary>
        /// Time length enumerator
        /// </summary>
        /// <returns>Time length enumerator</returns>
        public IEnumerator<double> GetEnumerator()
        {
            return beatTimeList.GetEnumerator();
        }

        /// <summary>
        /// Time length enumerator
        /// </summary>
        /// <returns>Time length enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return beatTimeList.GetEnumerator();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Sum of time lenght of all beat times
        /// </summary>
        public double Sum
        {
            get
            {
                return beatTimeList.Sum();
            }
        }

        /// <summary>
        /// How many beat times
        /// </summary>
        public int Count
        {
            get { return beatTimeList.Count; }
        }

        /// <summary>
        /// Get beat time at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>beat time at index</returns>
        public double this[int index]
        {
            get { return beatTimeList[index]; }
            set { beatTimeList[index] = value; }
        }
        #endregion
    }
}
