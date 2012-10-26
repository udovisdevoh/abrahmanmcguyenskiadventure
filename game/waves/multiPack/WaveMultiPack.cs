using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a group of wave packs which changes between wave packs periodically
    /// </summary>
    public class WaveMultiPack : AbstractWave, IList<WavePack>
    {
        #region Fields and parts
        /// <summary>
        /// List of component wave packs
        /// </summary>
        private List<WavePack> wavePackList = new List<WavePack>();

        /// <summary>
        /// Cached values
        /// </summary>
        private Dictionary<int, double> waveValueCache = new Dictionary<int, double>();

        /// <summary>
        /// Represents information about where wave packs are segmented and what are the X offsets for wavepack
        /// </summary>
        private WaveMultiPackSegmentationData waveMultiPackSegmentationData = new WaveMultiPackSegmentationData();

        /// <summary>
        /// Normalization multiplicator
        /// </summary>
        private double normalizationMultiplicator = 1.0;
        #endregion

        #region AbstractWave Overrides
        /// <summary>
        /// Normalize the wave pack set
        /// </summary>
        public override void Normalize()
        {
            Normalize(1.0);
        }

        /// <summary>
        /// Normalize the wave pack set
        /// </summary>
        /// <param name="maxValue">maximum value for wave</param>
        public override void Normalize(double maxValue)
        {
            Normalize(maxValue, true);
        }

        /// <summary>
        /// Normalize the wave pack
        /// </summary>
        public override void Normalize(double maxValue, bool isIncreaseToo)
        {
            Normalize(maxValue, isIncreaseToo, 1.0, 10024.0);
        }

        /// <summary>
        /// Normalize wave pack
        /// </summary>
        public override void Normalize(double maxValue, bool isIncreaseToo, double incrementation, double range)
        {
            double oldNormalizationMultiplicator = normalizationMultiplicator;

            normalizationMultiplicator = 1.0;
            double y;

            double minimumX = range * -1;

            double maxY = double.NegativeInfinity;
            double minY = double.PositiveInfinity;
            for (double x = minimumX; x < range; x += incrementation)
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

        /// <summary>
        /// Whether wave or wavepack equals the other
        /// </summary>
        /// <param name="other">other wave</param>
        /// <returns>Whether wave or wavepack equals the other</returns>
        public override bool Equals(AbstractWave other)
        {
            return other == this;
        }

        /// <summary>
        /// Get cached value at x
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>cached value at x</returns>
        public override double GetCachedValue(double x)
        {
            double value;
            int key = (int)(x * Program.tileSize);
            if (!waveValueCache.TryGetValue(key, out value))
            {
                value = this[x];
                waveValueCache.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// Get amplitude at position/time x
        /// </summary>
        /// <param name="x">x</param>
        /// <returns>amplitude at position/time x</returns>
        public override double this[double x]
        {
            get
            {
                double xOffset;
                double yOffset;
                WavePack currentWavePack = waveMultiPackSegmentationData.GetSelectedWavePackAt(x, out xOffset, out yOffset);
                return (currentWavePack.GetCachedValue(x + xOffset) + yOffset) * normalizationMultiplicator;
            }
        }
        #endregion

        #region IList<AbstractWave> Members
        /// <summary>
        /// Returns the index of selected wave component
        /// </summary>
        /// <param name="item">selected wave component</param>
        /// <returns>index of selected wave component</returns>
        public int IndexOf(WavePack item)
        {
            int index = 0;
            foreach (WavePack wavePack in wavePackList)
            {
                if (item.Equals(wavePack))
                    return index;
                index++;
            }

            return -1;
        }

        /// <summary>
        /// Insert component wave at selected index
        /// </summary>
        /// <param name="index">selected index</param>
        public void Insert(int index, WavePack item)
        {
            wavePackList.Insert(index, item);
        }

        /// <summary>
        /// Removes component wave at selected index
        /// </summary>
        /// <param name="index">selected index</param>
        public void RemoveAt(int index)
        {
            wavePackList.RemoveAt(index);
        }

        public WavePack this[int index]
        {
            get
            {
                return wavePackList[index];
            }
            set
            {
                wavePackList[index] = value;
            }
        }

        /// <summary>
        /// Add wave pack to multipack
        /// </summary>
        /// <param name="item">wave pack</param>
        public void Add(WavePack item)
        {
            wavePackList.Add(item);
            waveMultiPackSegmentationData.Add(item);
        }

        public void Clear()
        {
            wavePackList.Clear();
            waveValueCache.Clear();
            waveMultiPackSegmentationData.Clear();
        }

        public bool Contains(WavePack item)
        {
            foreach (WavePack child in wavePackList)
                if (child.Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(WavePack[] array, int arrayIndex)
        {
            wavePackList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return wavePackList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(WavePack item)
        {
            foreach (WavePack wavePack in wavePackList)
                if (wavePack.Equals(item))
                    return wavePackList.Remove(wavePack);
            return false;
        }

        public IEnumerator<WavePack> GetEnumerator()
        {
            return wavePackList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return wavePackList.GetEnumerator();
        }
        #endregion
    }
}