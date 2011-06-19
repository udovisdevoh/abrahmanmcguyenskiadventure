﻿using System;
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
        private Dictionary<int, Surface> internalDictionary = new Dictionary<int, Surface>();

        /// <summary>
        /// Queue of cached zone indexes
        /// </summary>
        private Queue<int> internalQueue = new Queue<int>();
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
            return internalDictionary.TryGetValue((indexX * Program.totalHeightTileCount) + indexY, out surface);
        }

        /// <summary>
        /// Add zone to cache
        /// </summary>
        /// <param name="indexX">x index</param>
        /// <param name="indexY">y index</param>
        /// <param name="surface">surface</param>
        public void Add(int indexX, int indexY, Surface surface)
        {
            int index = (indexX * Program.totalHeightTileCount) + indexY;
            if (!internalDictionary.ContainsKey(index))
                internalDictionary.Add(index, surface);
        }
        #endregion
    }
}
