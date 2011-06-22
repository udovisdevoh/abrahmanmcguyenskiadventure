using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Common interface for ground and sprites (stuff that can be walked on)
    /// </summary>
    interface IGround
    {
        /// <summary>
        /// Ground height
        /// </summary>
        /// <param name="xPosition">x Position</param>
        /// <returns>Ground height</returns>
        float this[float xPosition]
        {
            get;
        }
    }
}
