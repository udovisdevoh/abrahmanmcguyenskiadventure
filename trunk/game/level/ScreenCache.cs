using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.level
{
    internal class ScreenCache
    {
        private Surface[,] internalArray;

        public ScreenCache(int columnCount, int rowCount)
        {
            internalArray = new Surface[columnCount, rowCount];
        }
    }
}
