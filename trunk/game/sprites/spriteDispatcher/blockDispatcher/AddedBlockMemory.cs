using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    internal class AddedBlockMemory
    {
        #region Fields and parts
        private HashSet<int> internalHash;
        #endregion

        #region Constructor
        public AddedBlockMemory()
        {
            internalHash = new HashSet<int>();
        }
        #endregion

        #region Internal Methods
        internal bool Contains(int x, int y)
        {
            return internalHash.Contains(GetKey(x, y));
        }

        internal bool Add(int x, int y)
        {
            return internalHash.Add(GetKey(x, y));
        }
        #endregion

        #region Private Methods
        private int GetKey(int x, int y)
        {
            return (int)x * 4000 + (int)y;
        }
        #endregion
    }
}
