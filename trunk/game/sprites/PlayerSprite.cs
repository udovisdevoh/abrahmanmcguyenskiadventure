using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a player
    /// </summary>
    internal class PlayerSprite : AbstractSprite
    {
        #region Fields and parts
        private Surface defaultSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a player's sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public PlayerSprite(double xPosition, double yPosition)
            : base(xPosition, yPosition)
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
            return 1.5;
        }

        /// <summary>
        /// Build height
        /// </summary>
        /// <returns>height</returns>
        protected override double BuildHeight()
        {
            return 2.0;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        protected override double BuildMass()
        {
            return 1.0;
        }

        public override Surface GetCurrentSurface()
        {
            if (defaultSurface == null)
            {
                defaultSurface = new Surface("./assets/rendered/abrahman/walk1.png");
                double zoom = height * Program.tileSize / defaultSurface.Height;
                defaultSurface = defaultSurface.CreateScaledSurface(zoom);
            }
            return defaultSurface;
        }
        #endregion
    }
}
