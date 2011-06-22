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
        void Update(Level level, ColorTheme colorTheme, Sky sky, float viewOffsetX, float viewOffsetY);

        /// <summary>
        /// Clear level viewer's cache within provided bounds
        /// </summary>
        /// <param name="leftBound">left bound</param>
        /// <param name="rightBound">right bound</param>
        /// <param name="topBound">top bound</param>
        /// <param name="bottomBound">bottom bound</param>
        void ClearCacheAtRange(float leftBound, float rightBound, float topBound, float bottomBound);
    }
}
