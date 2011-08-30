using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite viewed on map
    /// </summary>
    internal abstract class MapSprite : AbstractSprite
    {
        #region Fields and parts
        /// <summary>
        /// X Position
        /// </summary>
        private double xPosition;

        /// <summary>
        /// Y Position
        /// </summary>
        private double yPosition;
        #endregion

        #region Properties
        public double XPosition
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        public double YPosition
        {
            get { return yPosition; }
            set { yPosition = value; }
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Get surface
        /// </summary>
        /// <returns>surface</returns>
        internal abstract Surface GetSurface();
        #endregion
    }
}