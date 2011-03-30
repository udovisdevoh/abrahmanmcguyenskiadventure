using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using System.Drawing;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a monster
    /// </summary>
    abstract class MonsterSprite : AbstractSprite
    {
        #region Fields and parts
        private Surface defaultUndefinedSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create monster sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public MonsterSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            defaultUndefinedSurface = new Surface((int)(this.Width * Program.tileSize), (int)(this.Height * Program.tileSize), Program.bitDepth);
            defaultUndefinedSurface.Fill(Color.Red);
        }
        #endregion

        #region Override methods
        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface()
        {
            return defaultUndefinedSurface;
        }
        #endregion
    }
}
