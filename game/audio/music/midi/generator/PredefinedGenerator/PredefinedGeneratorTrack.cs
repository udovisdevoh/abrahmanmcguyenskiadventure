using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Predefined generator track
    /// </summary>
    public class PredefinedGeneratorTrack : IEnumerable<bool>
    {
        #region Fields
        /// <summary>
        /// MetaRiffPack's name
        /// </summary>
        private string metaRiffPackName;

        /// <summary>
        /// Internal list of checkbox bools
        /// </summary>
        private List<bool> internalList = new List<bool>();
        #endregion

        #region Constructor
        /// <summary>
        /// Build predefined generator track
        /// </summary>
        /// <param name="metaRiffPackName">meta riff pack name</param>
        /// <param name="barCount">bar count</param>
        public PredefinedGeneratorTrack(string metaRiffPackName, int barCount)
        {
            this.metaRiffPackName = metaRiffPackName;
            internalList = new List<bool>();

            for (int i = 0; i < barCount; i++)
                internalList.Add(false);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get check box's bool at index
        /// </summary>
        /// <param name="index">index (bar id)</param>
        /// <returns>check box's bool at index</returns>
        public bool this[int index]
        {
            get
            {
                while (internalList.Count - 1 < index)
                    internalList.Add(false);

                return internalList[index];
            }

            set
            {
                while (internalList.Count - 1 < index)
                    internalList.Add(false);

                internalList[index] = value;
            }
        }

        /// <summary>
        /// MetaRiffPack's name
        /// </summary>
        public string MetaRiffPackName
        {
            get { return metaRiffPackName; }
            set { metaRiffPackName = value; }
        }
        #endregion

        #region IEnumerable<bool> Members
        /// <summary>
        /// Checkbox bool enumerator
        /// </summary>
        /// <returns>Checkbox bool enumerator</returns>
        public IEnumerator<bool> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Checkbox bool enumerator
        /// </summary>
        /// <returns>Checkbox bool enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
        #endregion
    }
}
