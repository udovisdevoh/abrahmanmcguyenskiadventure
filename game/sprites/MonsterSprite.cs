﻿using System;
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

        private Cycle kickedHelmetCycle;
        
        private double jumpProbability;

        private bool isCanJump;

        private bool isFleeWhenAttacked;

        private bool isAiEnabled;

        private bool isNoAiDefaultDirectionWalkingRight;

        private bool isAvoidFall;

        private bool isFullSpeedAfterBounceNoAi;

        private bool isWalkEnabled;

        private bool isToggleWalkWhenJumpedOn;

        private bool isInstantKickConvertedSprite;
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
            isWalkEnabled = true;
            kickedHelmetCycle = new Cycle(16.0,false);
            defaultUndefinedSurface = new Surface((int)(this.Width * Program.tileSize), (int)(this.Height * Program.tileSize), Program.bitDepth);
            defaultUndefinedSurface.Fill(Color.Red);
            isCanJump = BuildIsCanJump(random);
            jumpProbability = BuildJumpProbability();
            isFleeWhenAttacked = BuildIsFleeWhenAttacked(random);
            isAiEnabled = BuildIsAiEnabled();
            isAvoidFall = BuildIsAvoidFall(random);
            isNoAiDefaultDirectionWalkingRight = random.Next(0, 2) == 1;
            isFullSpeedAfterBounceNoAi = BuildIsFullSpeedAfterBounceNoAi();
            isToggleWalkWhenJumpedOn = BuildIsToggleWalkWhenJumpedOn();
            isInstantKickConvertedSprite = BuildIsInstantKickConvertedSprite();
        }
        #endregion

        #region Abstract methods
        protected abstract bool BuildIsAiEnabled();

        protected abstract bool BuildIsCanJump(Random random);

        protected abstract bool BuildIsAvoidFall(Random random);

        protected abstract bool BuildIsToggleWalkWhenJumpedOn();

        protected abstract double BuildJumpProbability();

        /// <summary>
        /// Whether sprites will flee just after getting attacked
        /// </summary>
        /// <returns>Whether sprites will flee just after getting attacked</returns>
        protected abstract bool BuildIsFleeWhenAttacked(Random random);

        protected abstract bool BuildIsFullSpeedAfterBounceNoAi();

        protected abstract bool BuildIsInstantKickConvertedSprite();

        /// <summary>
        /// Sprite when converted to another sprite (default: null)
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Sprite when converted to another sprite (default: null)</returns>
        public abstract AbstractSprite GetConverstionSprite(Random random);
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

        public bool IsAvoidFall
        {
            get { return isAvoidFall; }
        }

        public bool IsFullSpeedAfterBounceNoAi
        {
            get { return isFullSpeedAfterBounceNoAi; }
        }

        public bool IsWalkEnabled
        {
            get { return isWalkEnabled; }
            set { isWalkEnabled = value; }
        }

        public bool IsToggleWalkWhenJumpedOn
        {
            get { return isToggleWalkWhenJumpedOn; }
        }

        public bool IsInstantKickConvertedSprite
        {
            get { return isInstantKickConvertedSprite; }
        }

        public Cycle KickedHelmetCycle
        {
            get { return kickedHelmetCycle; }
        }
        #endregion
    }
}
