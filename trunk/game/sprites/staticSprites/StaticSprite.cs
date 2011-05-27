using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite not moving, not affected by gravity
    /// </summary>
    abstract class StaticSprite : AbstractSprite
    {
        #region Constructor
        /// <summary>
        /// Build static sprite
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <param name="yPosition">Y Position</param>
        /// <param name="random">Random number generator</param>
        public StaticSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
        }
        #endregion
    }
}
