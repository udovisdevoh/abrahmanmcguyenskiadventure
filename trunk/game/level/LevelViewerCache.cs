using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.level
{
    internal class LevelViewerCache
    {
        #region Fields and parts
        private Dictionary<int, Surface> internalDictionary = new Dictionary<int, Surface>();

        private Queue<int> internalQueue = new Queue<int>();
        #endregion

        internal bool TryGetValue(int index, out Surface currentSurface)
        {
            return internalDictionary.TryGetValue(index, out currentSurface);
        }

        internal void Add(int index, Surface currentSurface)
        {
            internalDictionary.Add(index, currentSurface);
            internalQueue.Enqueue(index);
        }

        internal void Trim(int maxCachedColumnCount)
        {
            while (internalDictionary.Count > maxCachedColumnCount)
            {
                internalDictionary.Remove(internalQueue.Dequeue());
            }
        }
        
        public bool IsFull
        {
        	get {return internalDictionary.Count >= Program.maxCachedColumnCount;}
        }
    }
}
