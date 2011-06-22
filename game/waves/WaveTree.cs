using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a binary tree of waves
    /// </summary>
    internal class WaveTree : AbstractWave
    {
        #region Fields and parts
        /// <summary>
        /// Left child (if has no atomic wave)
        /// </summary>
        private WaveTree leftChild;

        /// <summary>
        /// Right child (if has no atomic wave)
        /// </summary>
        private WaveTree rightChild;

        /// <summary>
        /// Wave (if has no child)
        /// </summary>
        private Wave atomicWave;

        private Dictionary<int, float> waveValueCache = new Dictionary<int, float>();

        /// <summary>
        /// Whether the operator is a * instead of + (to join left and right child)
        /// </summary>
        private bool isMultNotAdd;

        /// <summary>
        /// Normalization multiplicator
        /// </summary>
        private float normalizationMultiplicator = 1.0f;
        #endregion

        #region Constructor
        /// <summary>
        /// Create wave tree from wave
        /// </summary>
        /// <param name="atomicWave">atomic wave</param>
        public WaveTree(Wave atomicWave)
        {
            this.atomicWave = atomicWave;
        }

        /// <summary>
        /// Create wave tree from two child wave tree
        /// </summary>
        /// <param name="leftChild">left child</param>
        /// <param name="isMultNotAdd">true: *, false: -</param>
        /// <param name="rightChild">right child</param>
        public WaveTree(WaveTree leftChild, bool isMultNotAdd, WaveTree rightChild)
        {
            this.leftChild = leftChild;
            this.isMultNotAdd = isMultNotAdd;
            this.rightChild = rightChild;
        }
        #endregion

        #region AbstractWave Members
        public override void Normalize()
        {
            Normalize(1.0f);
        }

        /// <summary>
        /// Normalize the wave pack
        /// </summary>
        /// <returns></returns>
        public override void Normalize(float maxValue)
        {
            Normalize(maxValue, true);
        }

        /// <summary>
        /// Normalize the wave pack
        /// </summary>
        /// <returns></returns>
        public override void Normalize(float maxValue, bool isIncreaseToo)
        {
            float oldNormalizationMultiplicator = normalizationMultiplicator;

            normalizationMultiplicator = 1.0f;
            float y;

            float maxY = float.NegativeInfinity;
            float minY = float.PositiveInfinity;
            for (float x = -10024.0f; x < 10024.0f; x += 16f)
            {
                y = this[x];
                if (y > maxY)
                    maxY = y;

                if (y < minY)
                    minY = y;
            }

            maxY = Math.Max(maxY, minY * -1.0f);

            normalizationMultiplicator = 1.0f / maxY * maxValue;

            if (!isIncreaseToo)
                normalizationMultiplicator = Math.Min(oldNormalizationMultiplicator, normalizationMultiplicator);
        }

        public override float this[float x]
        {
            get
            {
                /*if (x > -10 && x < 10)
                    return Program.totalHeightTileCount / -2;
                else if (x >= 10 && x < 30)
                    return 0;
                else if (x >= 30 && x < 50)
                    return Program.totalHeightTileCount / 2;*/
            	
                if (atomicWave != null)
                {
                    return atomicWave[x];
                }
                else
                {
                    if (isMultNotAdd)
                    {
                        return (leftChild[x] * leftChild[x]) * normalizationMultiplicator;
                    }
                    else
                    {
                        return (leftChild[x] + leftChild[x]) * normalizationMultiplicator;
                    }
                }
            }
        }

        public override float GetCachedValue(float x)
        {
            float value;
            int key = (int)(x * Program.tileSize);
            if (!waveValueCache.TryGetValue(key, out value))
            {
                value = this[x];
                waveValueCache.Add(key, value);
            }
            return value;
        }

        public override bool Equals(AbstractWave other)
        {
            if (atomicWave != null && other is Wave)
            {
                return atomicWave.Equals(((Wave)other));
            }
            else if (other is WaveTree)
            {
                WaveTree otherWaveTree = (WaveTree)other;

                if (isMultNotAdd == otherWaveTree.isMultNotAdd)
                    return false;

                if (!leftChild.Equals(otherWaveTree.leftChild) || rightChild.Equals(otherWaveTree.rightChild))
                    return false;

                if (!leftChild.Equals(otherWaveTree.rightChild) || rightChild.Equals(otherWaveTree.leftChild))
                    return false;

                return true;
            }

            return false;
        }
        #endregion
    }
}
