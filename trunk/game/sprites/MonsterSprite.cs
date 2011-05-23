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

        private bool isFleeWhenAttacked;

        private bool isAiEnabled;

        private bool isNoAiDefaultDirectionWalkingRight;
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
            isFleeWhenAttacked = BuildIsFleeWhenAttacked(random);
            isAiEnabled = BuildIsAiEnabled();
            isNoAiDefaultDirectionWalkingRight = random.Next(0, 2) == 1;
        }
        #endregion

        #region Abstract methods
        protected abstract bool BuildIsAiEnabled();

        protected abstract bool BuildIsCanJump();

        protected abstract double BuildJumpProbability();

        /// <summary>
        /// Whether sprites will flee just after getting attacked
        /// </summary>
        /// <returns></returns>
        protected abstract bool BuildIsFleeWhenAttacked(Random random);
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

        public bool IsFleeWhenAttacked
        {
            get { return isFleeWhenAttacked; }
        }

        public bool IsAiEnabled
        {
            get { return isAiEnabled; }
        }

        public bool IsNoAiDefaultDirectionWalkingRight
        {
            get { return isNoAiDefaultDirectionWalkingRight; }
            set { isNoAiDefaultDirectionWalkingRight = value; }
        }
        #endregion
    }
}
