using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Roff pack
    /// </summary>
    public class RiffPack : IRiff, IList<IRiff>
    {
        #region Fields
        private List<IRiff> internalList;

        private int tempo = 120;
        #endregion

        #region Constructors
        /// <summary>
        /// Create riff pack
        /// </summary>
        public RiffPack()
        {
            internalList = new List<IRiff>();
        }

        /// <summary>
        /// Create riff pack from riff
        /// </summary>
        /// <param name="iRiff">riff</param>
        public RiffPack(IRiff iRiff)
        {
            this.Tempo = iRiff.Tempo;
            if (iRiff is Riff)
            {
                internalList = new List<IRiff>();
                internalList.Add((Riff)iRiff);
            }
            else if (iRiff is RiffPack)
            {
                internalList = new List<IRiff>(((RiffPack)iRiff).internalList);
            }
        }
        #endregion

        #region Operator Overloads
        /// <summary>
        /// Add two riffs
        /// </summary>
        /// <param name="riffPack1">riff 1</param>
        /// <param name="riff2">riff 2</param>
        /// <returns>summ of two riffs as a riff pack</returns>
        public static IRiff operator +(RiffPack riffPack1, IRiff riff2)
        {
            RiffPack summ = new RiffPack();
            summ.Add(riffPack1);
            summ.Add(riff2);
            return summ;
        }
        #endregion

        #region IList<IRiff> Members
        /// <summary>
        /// Remove all riffs
        /// </summary>
        public void Clear()
        {
            internalList.Clear();
        }

        /// <summary>
        /// Riff at specified index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>Riff at specified index</returns>
        public IRiff this[int index]
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
        /// Insert riff at index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">riff</param>
        public void Insert(int index, IRiff item)
        {
            if (!Contains(item))
                internalList.Insert(index, item);
        }

        /// <summary>
        /// Remove riff at specified index
        /// </summary>
        /// <param name="index">specified index</param>
        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
        }

        /// <summary>
        /// Add riff to riff pack
        /// </summary>
        /// <param name="item">riff to add</param>
        public void Add(IRiff item)
        {
            if (internalList.Count < 1)
                this.Tempo = item.Tempo;
            //else
            //    this.Tempo = (int)((this.Tempo * (double)internalList.Count + (double)item.Tempo) / ((double)internalList.Count + 1.0));

            if (!Contains(item))
                internalList.Add(item);
        }

        /// <summary>
        /// Copy to array
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(IRiff[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Riff count
        /// </summary>
        public int Count
        {
            get { return internalList.Count; }
        }

        /// <summary>
        /// Whether riffpack is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Riff enumerator
        /// </summary>
        /// <returns>Riff enumerator</returns>
        public IEnumerator<IRiff> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Riff enumerator
        /// </summary>
        /// <returns>Riff enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <summary>
        /// Whether riffpack contains specified riff
        /// </summary>
        /// <param name="item">specified riff</param>
        /// <returns>Whether riffpack contains specified riff</returns>
        public bool Contains(IRiff item)
        {
            foreach (IRiff riff in internalList)
                if (item.Equals(riff))
                    return true;
            return false;
        }

        /// <summary>
        /// Index of specified riff item
        /// </summary>
        /// <param name="item">riff item</param>
        /// <returns>Index of specified riff item</returns>
        public int IndexOf(IRiff item)
        {
            int index = 0;
            foreach (IRiff riff in internalList)
            {
                if (item.Equals(riff))
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Remove riff from riff pack
        /// </summary>
        /// <param name="item">riff to remove</param>
        /// <returns>whether removal worked</returns>
        public bool Remove(IRiff item)
        {
            foreach (IRiff riff in internalList)
                if (item.Equals(riff))
                    internalList.Remove(riff);
            return false;
        }
        #endregion

        #region IEquatable<IRiff> Members
        /// <summary>
        /// Whether both riff packs are identical
        /// </summary>
        /// <param name="other">other</param>
        /// <returns>Whether both riff packs are identical</returns>
        public bool Equals(IRiff other)
        {
            if (other is RiffPack)
            {
                RiffPack otherRiffPack = (RiffPack)other;

                if (otherRiffPack.Count != this.Count)
                    return false;

                foreach (IRiff riff in this)
                    if (!otherRiffPack.Contains(riff))
                        return false;
                
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Tempo
        /// </summary>
        public int Tempo
        {
            get { return tempo; }
            set { tempo = value; }
        }
        #endregion
    }
}
