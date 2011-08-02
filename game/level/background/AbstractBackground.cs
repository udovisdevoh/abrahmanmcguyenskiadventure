using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.level
{
    /// <summary>
    /// Represents a background
    /// </summary>
    internal abstract class AbstractBackground
    {
        #region Field and parts
        /// <summary>
        /// Background surface to blit
        /// </summary>
        protected Surface surface;

        /// <summary>
        /// Height of the background (in pixels)
        /// </summary>
        protected int backgroundHeight;

        /// <summary>
        /// Width of the background (in pixels)
        /// </summary>
        protected int backgroundWidth;
        #endregion

        #region Properties
        /// <summary>
        /// Get surface to blit
        /// </summary>
        public Surface Surface
        {
            get { return surface; }
        }

        public int BackgroundHeight
        {
            get { return backgroundHeight; }
        }

        public int BackgroundWidth
        {
            get { return backgroundWidth; }
        }
        #endregion
    }
}
