using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// To re-use rendered zone (view segments)
    /// </summary>
    internal class LevelViewerCacheColumnBased
    {
        #region Fields and parts
        /// <summary>
        /// Right most index
        /// </summary>
        private int rightMostIndex = 0;

        /// <summary>
        /// Left most index
        /// </summary>
        private int leftMostIndex = 0;

        /// <summary>
        /// Internal cached zone surface list
        /// </summary>
        private Dictionary<int, Surface> internalDictionary = new Dictionary<int, Surface>();

        /// <summary>
        /// Queue of cached zone indexes
        /// </summary>
        private Queue<int> internalQueue = new Queue<int>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Get surface from index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="surface">surface</param>
        /// <returns>whether could find surface at index</returns>
        internal bool TryGetValue(int index, out Surface surface)
        {
            return internalDictionary.TryGetValue(index, out surface);
        }

        /// <summary>
        /// Add surface at index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="surface">surface</param>
        internal void Add(int index, Surface surface)
        {
            if (index > rightMostIndex)
                rightMostIndex = index;
            else if (index < leftMostIndex)
                leftMostIndex = index;

            internalDictionary.Add(index, surface);
            internalQueue.Enqueue(index);
        }

        /// <summary>
        /// Only keep maximum surface count, remove other cached surfaces
        /// </summary>
        /// <param name="maxCachedColumnCount">maximum surface count</param>
        internal void Trim(int maxCachedColumnCount)
        {
            while (internalDictionary.Count > maxCachedColumnCount)
            {
                int index = internalQueue.Dequeue();
                if (index == leftMostIndex)
                    leftMostIndex++;
                else if (index == rightMostIndex)
                    rightMostIndex--;
                if (internalDictionary.ContainsKey(index))
                    internalDictionary.Remove(index);
            }
        }

        /// <summary>
        /// Clear level viewer cache
        /// </summary>
        internal void Clear()
        {
            leftMostIndex = 0;
            rightMostIndex = 0;
            internalDictionary.Clear();
            internalQueue.Clear();
        }

        /// <summary>
        /// Get index of next unrendered zone
        /// </summary>
        /// <param name="isPlayerWalkingRight">is player walking right</param>
        /// <returns>index of next unrendered zone</returns>
        internal int GetNextUnrenderedZoneIndex(bool isPlayerWalkingRight)
        {
            if (isPlayerWalkingRight)
                return rightMostIndex + 1;
            else
                return leftMostIndex - 1;
        }

        /// <summary>
        /// Clear cache at provided range
        /// </summary>
        /// <param name="minX">left bound (inclusive)</param>
        /// <param name="maxX">right bound (inclusive)</param>
        internal void ClearCacheAtRange(int minX, int maxX)
        {
            for (int x = minX; x <= maxX; x++)
                if (internalDictionary.ContainsKey(x))
                    internalDictionary.Remove(x);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether level viewer cache is full
        /// </summary>
        public bool IsFull
        {
        	get {return internalDictionary.Count >= Program.maxCachedColumnCount;}
        }
        #endregion
    }
}
