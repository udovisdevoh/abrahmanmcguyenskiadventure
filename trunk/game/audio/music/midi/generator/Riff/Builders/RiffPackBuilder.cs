using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Build songs
    /// </summary>
    public class RiffPackBuilder : IList<RiffBuilder>
    {
        #region Fields
        /// <summary>
        /// Internal riffBuilder list
        /// </summary>
        private List<RiffBuilder> internalList = new List<RiffBuilder>();
        #endregion

        #region IList<RiffBuilder> Members
        /// <summary>
        /// Index of riff builder
        /// </summary>
        /// <param name="item">riff builder</param>
        /// <returns>Index of riff builder</returns>
        public int IndexOf(RiffBuilder item)
        {
            return internalList.IndexOf(item);
        }

        /// <summary>
        /// Insert riff builder at index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">riff builder</param>
        public void Insert(int index, RiffBuilder item)
        {
            internalList.Insert(index, item);
        }

        /// <summary>
        /// Remove riff builder at specified index
        /// </summary>
        /// <param name="index">index</param>
        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
        }

        /// <summary>
        /// Get riff builder at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>riff builder at index</returns>
        public RiffBuilder this[int index]
        {
            get
            {
                return internalList[index];
            }
            set
            {
                internalList[index] = value;
            }
        }

        /// <summary>
        /// Add riff builder
        /// </summary>
        /// <param name="item">riff builder</param>
        public void Add(RiffBuilder item)
        {
            internalList.Add(item);
        }

        /// <summary>
        /// Remove all riff builders
        /// </summary>
        public void Clear()
        {
            internalList.Clear();
        }

        /// <summary>
        /// Whether it contains riff builder
        /// </summary>
        /// <param name="item">riff builder</param>
        /// <returns>Whether it contains riff builder</returns>
        public bool Contains(RiffBuilder item)
        {
            return internalList.Contains(item);
        }

        /// <summary>
        /// Copy to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(RiffBuilder[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// How many riff builder
        /// </summary>
        public int Count
        {
            get { return internalList.Count; }
        }

        /// <summary>
        /// Whether riff pack builder is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove riff builder
        /// </summary>
        /// <param name="item">riff builder</param>
        /// <returns>whether removal worked</returns>
        public bool Remove(RiffBuilder item)
        {
            return internalList.Remove(item);
        }

        /// <summary>
        /// Riff builder enumerator
        /// </summary>
        /// <returns>Riff builder enumerator</returns>
        public IEnumerator<RiffBuilder> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Riff builder enumerator
        /// </summary>
        /// <returns>Riff builder enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Build song
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>New Song</returns>
        public RiffPack Build(Random random)
        {
            return Build(random, -1);
        }

        /// <summary>
        /// Build song
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="desiredLength">desired length</param>
        /// <returns>New Song</returns>
        public RiffPack Build(Random random, int desiredLength)
        {
            return Build(random, null, null, desiredLength);
        }

        /// <summary>
        /// Build song
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="modulator">key modulator</param>
        /// <param name="timeFrame">time frame for riffs to switch on and off</param>
        /// <returns>New Song</returns>
        public RiffPack Build(Random random, Modulator modulator, TimeFrame timeFrame)
        {
            return Build(random, modulator, timeFrame, -1);
        }

        /// <summary>
        /// Build song
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <param name="modulator">key modulator</param>
        /// <param name="timeFrame">time frame for riffs to switch on and off</param>
        /// <param name="desiredLength">desired length (how many bar)</param>
        /// <returns>New Song</returns>
        public RiffPack Build(Random random, Modulator modulator, TimeFrame timeFrame, int desiredLength)
        {
            int defaultKey = random.Next(-6, 7);
            RiffPack riffPack = new RiffPack();

            int count = 0;
            foreach (RiffBuilder riffBuilder in internalList)
            {
                if (desiredLength != -1)
                    riffBuilder.DesiredRiffLength = desiredLength;

                if (timeFrame != null)
                    riffBuilder.DesiredRiffLength = timeFrame.Count;
                
                if (modulator != null)
                    riffBuilder.Modulator = modulator;

                if (timeFrame != null)
                    riffBuilder.TimeFrame = timeFrame;

                riffBuilder.DefaultKey = defaultKey;

                riffPack.Add(riffBuilder.Build(count));
                count++;
            }

            return riffPack;
        }
        #endregion
    }
}
