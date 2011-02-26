using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a wave pack
    /// </summary>
    public class WavePack : IWave, IList<IWave>
    {
        #region Const
        /// <summary>
        /// Junction type for when we add two waves
        /// </summary>
        public const int JunctionAdd = 0;

        /// <summary>
        /// Junction type for when we multiply two waves
        /// </summary>
        public const int JunctionMultiply = 1;
        #endregion

        #region Fields
        /// <summary>
        /// Internal list of waves
        /// </summary>
        private List<IWave> waveList = new List<IWave>();

        /// <summary>
        /// Normalization multiplicator
        /// </summary>
        private double normalizationMultiplicator = 1.0;

        /// <summary>
        /// Current junction type (to add or multiply waves)
        /// </summary>
        private int junctionType = JunctionAdd;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a wave pack with selected junction type (add or multiply) default: add
        /// </summary>
        /// <param name="junctionType">junction type (add or multiply) default: add</param>
        public WavePack(int junctionType)
        {
            this.junctionType = junctionType;
        }

        /// <summary>
        /// Create a wave pack for which waves will be added
        /// </summary>
        public WavePack()
        {
        }

        /// <summary>
        /// Create a wave pack from existing wave or wave pack
        /// </summary>
        /// <param name="wave">existing wave or wave pack</param>
        public WavePack(IWave wave)
        {
            Add(wave);
        }
        #endregion

        #region Operator Overloads
        /// <summary>
        /// Add two wave or wave packs
        /// </summary>
        /// <param name="wavePack1">wave[pack] 1</param>
        /// <param name="wave2">wave 2</param>
        /// <returns>joined wave pack</returns>
        public static IWave operator +(WavePack wavePack1, IWave wave2)
        {
            WavePack summ = new WavePack();
            summ.Add(wavePack1);
            summ.Add(wave2);
            return summ;
        }
        #endregion

        #region IList<IWave> Members
        /// <summary>
        /// Add component wave to pack
        /// </summary>
        /// <param name="item">component wave</param>
        public void Add(IWave item)
        {
            if (item is Wave)
                waveList.Add(item);
            else if (item is WavePack)
            {
                foreach (IWave iWave in ((WavePack)item))
                {
                    if (!this.Contains(iWave))
                    {
                        this.Add(iWave);
                    }
                }
            }
        }

        /// <summary>
        /// Remove all component waves
        /// </summary>
        public void Clear()
        {
            waveList.Clear();
        }

        /// <summary>
        /// Whether wave pack contains specified component wave
        /// </summary>
        /// <param name="item">specified component wave</param>
        /// <returns>Whether wave pack contains specified component wave</returns>
        public bool Contains(IWave item)
        {
            foreach (IWave child in waveList)
                if (child.Equals(item))
                    return true;
            return false;
        }

        /// <summary>
        /// Copy to an array of waves
        /// </summary>
        /// <param name="array">array of waves</param>
        /// <param name="arrayIndex">array index</param>
        public void CopyTo(IWave[] array, int arrayIndex)
        {
            waveList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Count how many component waves
        /// </summary>
        public int Count
        {
            get { return waveList.Count; }
        }

        /// <summary>
        /// Whether wave pack is read only
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove component wave
        /// </summary>
        /// <param name="item">component wave</param>
        /// <returns>if removal succeeded</returns>
        public bool Remove(IWave item)
        {
            foreach (IWave iWave in waveList)
                if (iWave.Equals(item))
                    return waveList.Remove(iWave);
            return false;
        }

        /// <summary>
        /// Needed to do foreach iteration
        /// </summary>
        /// <returns>Wave iterator</returns>
        public IEnumerator<IWave> GetEnumerator()
        {
            return waveList.GetEnumerator();
        }

        /// <summary>
        /// Needed to do foreach iteration
        /// </summary>
        /// <returns>Wave iterator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return waveList.GetEnumerator();
        }

        /// <summary>
        /// Returns the index of selected wave component
        /// </summary>
        /// <param name="item">selected wave component</param>
        /// <returns>index of selected wave component</returns>
        public int IndexOf(IWave item)
        {
            int index = 0;
            foreach (IWave iWave in waveList)
            {
                if (item.Equals(iWave))
                    return index;
                index++;
            }

            return -1;
        }

        /// <summary>
        /// Insert component wave at selected index
        /// </summary>
        /// <param name="index">selected index</param>
        /// <param name="item">component wave[pack]</param>
        public void Insert(int index, IWave item)
        {
            waveList.Insert(index, item);
        }

        /// <summary>
        /// Removes component wave at selected index
        /// </summary>
        /// <param name="index">selected index</param>
        public void RemoveAt(int index)
        {
            waveList.RemoveAt(index);
        }

        /// <summary>
        /// Returns component wave at index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>component wave at index</returns>
        public IWave this[int index]
        {
            get
            {
                return waveList[index];
            }
            set
            {
                waveList[index] = value;
            }
        }
        #endregion

        #region IEquatable<IWave> Members
        /// <summary>
        /// Whether wave or wavepack equals the other
        /// </summary>
        /// <param name="other">other wave</param>
        /// <returns>Whether wave or wavepack equals the other</returns>
        public bool Equals(IWave other)
        {
            if (other is WavePack)
            {
                WavePack otherWavePack = (WavePack)other;

                foreach (IWave iWave in otherWavePack)
                    if (this.Contains(iWave))
                        return false;

                foreach (IWave iWave in this)
                    if (otherWavePack.Contains(iWave))
                        return false;

                return true;
            }
            else if (Count == 1 && other is Wave && this[0] is Wave)
            {
                return this[0].Equals(other);
            }
            return false;
        }
        #endregion

        #region IWave Members
        /// <summary>
        /// Get amplitude at position/time x
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>amplitude at position/time x</returns>
        public double this[double x]
        {
            get
            {
                double value = 0.0;

                if (junctionType == JunctionMultiply)
                    value = 1.0;
                else if (junctionType == JunctionAdd)
                    value = 0.0;


                foreach (IWave iWave in waveList)
                {
                    if (junctionType == JunctionMultiply)
                        value *= iWave[x];
                    else if (junctionType == JunctionAdd)
                        value += iWave[x];
                }


                value *= normalizationMultiplicator;

                return value;
            }
        }

        /// <summary>
        /// Normalize the wave pack
        /// </summary>
        /// <returns></returns>
        public void Normalize()
        {
            Normalize(1.0);
        }

        /// <summary>
        /// Normalize the wave pack
        /// </summary>
        /// <returns></returns>
        public void Normalize(double maxValue)
        {
            Normalize(maxValue, true);
        }

        /// <summary>
        /// Normalize the wave pack
        /// </summary>
        /// <returns></returns>
        public void Normalize(double maxValue, bool isIncreaseToo)
        {
            double oldNormalizationMultiplicator = normalizationMultiplicator;

            normalizationMultiplicator = 1.0;
            double y;

            double maxY = double.NegativeInfinity;
            double minY = double.PositiveInfinity;
            for (double x = -10024.0; x < 10024.0; x += 1)
            {
                y = this[x];
                if (y > maxY)
                    maxY = y;

                if (y < minY)
                    minY = y;
            }

            maxY = Math.Max(maxY, minY * -1.0);

            normalizationMultiplicator = 1.0 / maxY * maxValue;

            if (!isIncreaseToo)
                normalizationMultiplicator = Math.Min(oldNormalizationMultiplicator, normalizationMultiplicator);
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Return a random wave junction type (add or multiply)
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>random wave junction type (add or multiply)</returns>
        public static int GetRandomJunctionType(Random random)
        {
            return random.Next(0, 2);
        }
        #endregion
    }
}