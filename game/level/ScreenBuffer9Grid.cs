using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.level
{
    /* Represents a set of 9 screens (the current is in the center) */
    internal class ScreenBuffer9Grid
    {
        private Surface[,] internalArray;

        public ScreenBuffer9Grid()
        {
            internalArray = new Surface[3,3];
        }

        public Surface this[int x, int y]
        {
            get
            {
                return internalArray[x, y];
            }
        }
    }
}
