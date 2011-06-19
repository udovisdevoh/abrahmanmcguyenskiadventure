using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// To view level
    /// </summary>
    interface ILevelViewer
    {
        /// <summary>
        /// Clear the viewer's cache
        /// </summary>
        void ClearCache();

        /// <summary>
        /// View current state
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="colorTheme">color theme</param>
        /// <param name="sky">sky</param>
        /// <param name="viewOffsetX">view offset X</param>
        /// <param name="viewOffsetY">view offset Y</param>
        void Update(Level level, ColorTheme colorTheme, Sky sky, double viewOffsetX, double viewOffsetY);
    }
}
