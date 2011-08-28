using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprites that can grow out of anarchy blocks
    /// </summary>
    interface IGrowable
    {
        /// <summary>
        /// Cycle of growth
        /// </summary>
        Cycle GrowthCycle
        {
            get;
        }
    }
}
