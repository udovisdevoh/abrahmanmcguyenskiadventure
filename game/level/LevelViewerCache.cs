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
    internal class LevelViewerCache
    {
        #region Fields and parts
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
                internalDictionary.Remove(internalQueue.Dequeue());
            }
        }

        /// <summary>
        /// Clear level viewer cache
        /// </summary>
        internal void Clear()
        {
            internalDictionary.Clear();
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
