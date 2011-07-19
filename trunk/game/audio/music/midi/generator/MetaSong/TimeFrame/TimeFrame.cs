using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// List of bar blocks
    /// </summary>
    public class TimeFrame : IList<BarBlock>
    {
        #region Fields
        /// <summary>
        /// List of bar blocks
        /// </summary>
        private List<BarBlock> internalList = new List<BarBlock>();
        #endregion

        #region IList<BarBlock> Members
        /// <summary>
        /// Index of bar block
        /// </summary>
        /// <param name="item">bar block</param>
        /// <returns>Index of bar block</returns>
        public int IndexOf(BarBlock item)
        {
            return internalList.IndexOf(item);
        }

        /// <summary>
        /// Insert bar block at index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">bar block</param>
        public void Insert(int index, BarBlock item)
        {
            internalList.Insert(index, item);
        }

        /// <summary>
        /// Remove bar block at index
        /// </summary>
        /// <param name="index">index</param>
        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
        }

        /// <summary>
        /// Get bar block at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>bar block at index</returns>
        public BarBlock this[int index]
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
        /// Add bar block
        /// </summary>
        /// <param name="item">bar block</param>
        public void Add(BarBlock item)
        {
            internalList.Add(item);
        }

        /// <summary>
        /// Add list of bar blocks
        /// </summary>
        /// <param name="list">list of bar blocks</param>
        public void AddRange(List<BarBlock> list)
        {
            internalList.AddRange(list);
        }

        /// <summary>
        /// Clear bar blocks
        /// </summary>
        public void Clear()
        {
            internalList.Clear();
        }

        /// <summary>
        /// Whether contains bar block
        /// </summary>
        /// <param name="item">bar block</param>
        /// <returns>Whether contains bar block</returns>
        public bool Contains(BarBlock item)
        {
            return internalList.Contains(item);
        }

        /// <summary>
        /// Copy to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(BarBlock[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Count bar blocks
        /// </summary>
        public int Count
        {
            get { return internalList.Count; }
        }

        /// <summary>
        /// Whether time frame is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove bar block
        /// </summary>
        /// <param name="item">bar block</param>
        /// <returns>whether removal worked</returns>
        public bool Remove(BarBlock item)
        {
            return internalList.Remove(item);
        }

        /// <summary>
        /// Bar block enumerator
        /// </summary>
        /// <returns>Bar block enumerator</returns>
        public IEnumerator<BarBlock> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Bar block enumerator
        /// </summary>
        /// <returns>Bar block enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Whether riff is allowed in time range
        /// </summary>
        /// <param name="currentPosition">current position</param>
        /// <param name="currentRiffId">riff id</param>
        /// <returns>Whether riff is allowed in time range</returns>
        public bool IsAllowTime(double currentPosition, int currentRiffId)
        {
            int position = (int)Math.Floor(currentPosition);
            return internalList[position].Contains(currentRiffId);
        }

        /// <summary>
        /// Trim
        /// </summary>
        public void Trim()
        {
            List<BarBlock> newList = new List<BarBlock>();

            foreach (BarBlock barBlock in internalList)
                if (barBlock.Count > 0)
                    newList.Add(barBlock);

            this.internalList = newList;
        }
        #endregion
    }
}
