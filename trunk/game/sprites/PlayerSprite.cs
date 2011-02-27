using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a player
    /// </summary>
    internal class PlayerSprite : AbstractSprite
    {
        #region Constructors
        /// <summary>
        /// Create a player's sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public PlayerSprite(double xPosition, double yPosition)
            : base(xPosition, yield)
        {
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Build width
        /// </summary>
        /// <returns>width</returns>
        protected override double BuildWidth()
        {
            return 0.5;
        }

        /// <summary>
        /// Build height
        /// </summary>
        /// <returns>height</returns>
        protected override double BuildHeight()
        {
            return 1.0;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        protected override double BuildMass()
        {
            return 1.0;
        }
        #endregion
    }
}
