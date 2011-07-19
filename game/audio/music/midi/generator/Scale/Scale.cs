using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// List of intervals
    /// </summary>
    public class Scale : IEnumerable<int>
    {
        #region Fields
        /// <summary>
        /// List of intervals
        /// </summary>
        private List<int> intervalList;
        #endregion

        #region Constructor
        /// <summary>
        /// Build scale
        /// </summary>
        public Scale()
        {
            intervalList = new List<int>();
            intervalList.Add(0);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add interval to scale
        /// </summary>
        /// <param name="interval">interval</param>
        public void Add(int interval)
        {
            intervalList.Add(intervalList.Max() + interval);
        }

        /// <summary>
        /// Get rounded note from scalar
        /// </summary>
        /// <param name="scalar">scalar</param>
        /// <param name="radius">radius</param>
        /// <returns>rounded note from scalar</returns>
        public int GetRoundValue(double scalar, int radius)
        {
            int ceilValue = GetCeilValue(scalar,radius);
            int floorValue = GetFloorValue(scalar, radius);

            if (((double)(ceilValue)) - scalar < scalar - ((double)(floorValue)))
                return ceilValue;
            else
                return floorValue;
        }

        /// <summary>
        /// Get rounded bottom note from scalar
        /// </summary>
        /// <param name="scalar">scalar</param>
        /// <param name="radius">radius</param>
        /// <returns>rounded bottom note from scalar</returns>
        public int GetFloorValue(double scalar, int radius)
        {
            scalar *= ((double)(radius));

            int offset = 0;
            while (scalar > 12)
            {
                scalar -= 12;
                offset += 12;
            }
            while (scalar < 0)
            {
                scalar += 12;
                offset -= 12;
            }

            int relativeInterval = 0;
            int previousInterval = 0;

            foreach (int interval in intervalList)
            {
                if (((double)(interval)) > scalar)
                {
                    relativeInterval = previousInterval;
                    break;
                }
                previousInterval = interval;
            }

            relativeInterval += offset;
            return relativeInterval;
        }

        /// <summary>
        /// Get rounded top note from scalar
        /// </summary>
        /// <param name="scalar">scalar</param>
        /// <param name="radius">radius</param>
        /// <returns>rounded top note from scalar</returns>
        public int GetCeilValue(double scalar, int radius)
        {
            scalar *= ((double)(radius));

            int offset = 0;
            while (scalar > 12)
            {
                scalar -= 12;
                offset += 12;
            }
            while (scalar < 0)
            {
                scalar += 12;
                offset -= 12;
            }

            int relativeInterval = 0;

            foreach (int interval in intervalList)
            {
                if (((double)(interval)) >= scalar)
                {
                    relativeInterval = interval;
                    break;
                }
            }

            relativeInterval += offset;
            return relativeInterval;
        }
        #endregion

        #region IEnumerable<int> Members
        /// <summary>
        /// Note interval enumerator
        /// </summary>
        /// <returns>Note interval enumerator</returns>
        public IEnumerator<int> GetEnumerator()
        {
            return intervalList.GetEnumerator();
        }

        /// <summary>
        /// Note interval enumerator
        /// </summary>
        /// <returns>Note interval enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return intervalList.GetEnumerator();
        }
        #endregion

        #region Properites
        /// <summary>
        /// How many interval in scale
        /// </summary>
        public int Count
        {
            get { return intervalList.Count; }
        }
        #endregion
    }
}