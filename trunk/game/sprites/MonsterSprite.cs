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

        private bool isCanJump;

        private double jumpProbability;
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
            isCanJump = BuildIsCanJump();
            jumpProbability = BuildJumpProbability();
        }
        #endregion

        #region Abstract methods
        protected abstract bool BuildIsCanJump();

        protected abstract double BuildJumpProbability();
        #endregion

        #region Override methods
        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            return defaultUndefinedSurface;
        }
        #endregion

        #region Properties
        public bool IsCanJump
        {
            get { return isCanJump; }
        }

        public double JumpProbability
        {
            get { return jumpProbability; }
        }
        #endregion
    }
}
