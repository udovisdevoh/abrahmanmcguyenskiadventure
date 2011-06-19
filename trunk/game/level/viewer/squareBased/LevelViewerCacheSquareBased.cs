using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Level viewer cache (segments are square shaped)
    /// </summary>
    internal class LevelViewerCacheSquareBased
    {
        #region Fields and parts
        /// <summary>
        /// Internal cached zone surface list
        /// </summary>
        private Dictionary<long, Surface> internalDictionary = new Dictionary<long, Surface>();

        /// <summary>
        /// Queue of cached zone indexes
        /// </summary>
        private Queue<long> internalQueue = new Queue<long>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Clear the cache
        /// </summary>
        public void Clear()
        {
            internalDictionary.Clear();
        }

        /// <summary>
        /// Try get surface from cache
        /// </summary>
        /// <param name="indexX">index X</param>
        /// <param name="indexY">index Y</param>
        /// <param name="surface">surface</param>
        /// <returns>Whether could get surface from cache</returns>
        public bool TryGetValue(int indexX, int indexY, out Surface surface)
        {
            long index = indexX * 10000 + indexY;
            return internalDictionary.TryGetValue(index, out surface);
        }

        /// <summary>
        /// Add zone to cache
        /// </summary>
        /// <param name="indexX">x index</param>
        /// <param name="indexY">y index</param>
        /// <param name="surface">surface</param>
        public void Add(int indexX, int indexY, Surface surface)
        {
            long index = indexX * 10000 + indexY;
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
                long index = internalQueue.Dequeue();
                internalDictionary.Remove(index);
            }
        }
        #endregion
    }
}
