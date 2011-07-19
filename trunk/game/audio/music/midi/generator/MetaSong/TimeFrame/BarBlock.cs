using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Bar block
    /// </summary>
    public class BarBlock : IList<int>
    {
        #region Fields
        /// <summary>
        /// List of number
        /// </summary>
        private List<int> internalList = new List<int>();
        #endregion

        #region Constructors
        /// <summary>
        /// Create bar block
        /// </summary>
        public BarBlock()
        {
        }

        /// <summary>
        /// Create bar block
        /// </summary>
        /// <param name="valueToAdd">value to add</param>
        public BarBlock(int valueToAdd)
        {
            internalList.Add(valueToAdd);
        }

        /// <summary>
        /// Create bar block
        /// </summary>
        /// <param name="toClone">bar block to clone</param>
        public BarBlock(BarBlock toClone)
        {
            internalList = new List<int>(toClone.internalList);
        }
        #endregion

        #region IList<int> Members
        /// <summary>
        /// Index of item
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>Index of item</returns>
        public int IndexOf(int item)
        {
            return internalList.IndexOf(item);
        }

        /// <summary>
        /// Insert number at index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">number at index</param>
        public void Insert(int index, int item)
        {
            internalList.Insert(index, item);
        }

        /// <summary>
        /// Remove number at index
        /// </summary>
        /// <param name="index">index</param>
        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
        }

        /// <summary>
        /// Number at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>Number at index</returns>
        public int this[int index]
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
        /// Add number
        /// </summary>
        /// <param name="item">number</param>
        public void Add(int item)
        {
            internalList.Add(item);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            internalList.Clear();
        }

        /// <summary>
        /// Whether contains number
        /// </summary>
        /// <param name="item">number</param>
        /// <returns>contains number</returns>
        public bool Contains(int item)
        {
            return internalList.Contains(item);
        }

        /// <summary>
        /// Copy to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(int[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// How many bar
        /// </summary>
        public int Count
        {
            get { return internalList.Count; }
        }

        /// <summary>
        /// Whether bar block is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove number
        /// </summary>
        /// <param name="item">number</param>
        /// <returns>whether removal worked</returns>
        public bool Remove(int item)
        {
            return internalList.Remove(item);
        }

        /// <summary>
        /// Number enumerator
        /// </summary>
        /// <returns>Number enumerator</returns>
        public IEnumerator<int> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Number enumerator
        /// </summary>
        /// <returns>Number enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
        #endregion
    }
}
