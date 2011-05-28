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
        /// <summary>
        /// Default undefined surface
        /// </summary>
        private Surface defaultUndefinedSurface;

        /// <summary>
        /// Cycle of kicked sprite (for instance, helmet)
        /// When cycle is fired, no damage is given from touching this sprite
        /// </summary>
        private Cycle kickedHelmetCycle;

        /// <summary>
        /// At the end of the cycle, the sprite will start shaking (if fired)
        /// At the very end, the sprite will be transformed into another sprite (if fired)
        /// </summary>
        private Cycle spontaneousTransformationCycle;
        
        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        private double jumpProbability;

        /// <summary>
        /// Whether monster can jump
        /// </summary>
        private bool isCanJump;

        /// <summary>
        /// Whether monster flees when getting attacked
        /// </summary>
        private bool isFleeWhenAttacked;

        /// <summary>
        /// Whether monster is chasing player
        /// </summary>
        private bool isAiEnabled;

        /// <summary>
        /// Whether monster is walking right, not left, when monster is not chasing player
        /// </summary>
        private bool isNoAiDefaultDirectionWalkingRight;

        /// <summary>
        /// Whether monster will try to avoid falling in holes
        /// </summary>
        private bool isAvoidFall;

        /// <summary>
        /// Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)
        /// </summary>
        private bool isFullSpeedAfterBounceNoAi;

        /// <summary>
        /// Whether monster can walk, or is stoped (i.e. stopped helmets)
        /// </summary>
        private bool isWalkEnabled;

        /// <summary>
        /// Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)
        /// </summary>
        private bool isToggleWalkWhenJumpedOn;

        /// <summary>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </summary>
        private bool isInstantKickConvertedSprite;

        /// <summary>
        /// Whether this sprite can be spontaneously transformed into another one when not walking for too long
        /// </summary>
        private bool isEnableSpontaneousConversion;

        /// <summary>
        /// Whether sprite can be converted to another sprite when getting jumped on
        /// </summary>
        private bool isEnableJumpOnConversion;
        #endregion

        #region Constructors
        /// <summary>
        /// Create monster sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MonsterSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            isWalkEnabled = true;
            kickedHelmetCycle = new Cycle(16.0,false);
            spontaneousTransformationCycle = new Cycle(256, false);
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
            isEnableSpontaneousConversion = BuildIsEnableSpontaneousConversion();
            isEnableJumpOnConversion = BuildIsEnableJumpOnConversion();
        }
        #endregion

        #region Abstract methods
        /// <summary>
        /// Whether monster is chasing player
        /// </summary>
        /// <returns>Whether monster is chasing player</returns>
        protected abstract bool BuildIsAiEnabled();

        /// <summary>
        /// Whether monster can jump
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Whether monster can jump</returns>
        protected abstract bool BuildIsCanJump(Random random);

        /// <summary>
        /// Whether monster will try to avoid falling in holes
        /// </summary>
        /// <param name="random">random number generator</param>
        /// <returns>Whether monster will try to avoid falling in holes</returns>
        protected abstract bool BuildIsAvoidFall(Random random);

        /// <summary>
        /// Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)
        /// </summary>
        /// <returns>Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)</returns>
        protected abstract bool BuildIsToggleWalkWhenJumpedOn();

        /// <summary>
        /// Whether sprite will flee just after getting attacked
        /// </summary>
        /// <returns>Whether sprite will flee just after getting attacked</returns>
        protected abstract bool BuildIsFleeWhenAttacked(Random random);

        /// <summary>
        /// Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)
        /// </summary>
        /// <returns>Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)</returns>
        protected abstract bool BuildIsFullSpeedAfterBounceNoAi();

        /// <summary>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </summary>
        /// <returns>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </returns>
        protected abstract bool BuildIsInstantKickConvertedSprite();

        /// <summary>
        /// Whether this sprite can be spontaneously transformed into another one when not walking for too long
        /// </summary>
        /// <returns>Whether this sprite can be spontaneously transformed into another one when not walking for too long</returns>
        protected abstract bool BuildIsEnableSpontaneousConversion();

        /// <summary>
        /// Whether sprite can be converted to another sprite when getting jumped on
        /// </summary>
        /// <returns>Whether sprite can be converted to another sprite when getting jumped on</returns>
        protected abstract bool BuildIsEnableJumpOnConversion();

        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        /// <returns>Probability of jumping (from 0 to 1)</returns>
        protected abstract double BuildJumpProbability();

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
        /// <param name="xOffset">X Offset</param>
        /// <param name="yOffset">Y Offset</param>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            return defaultUndefinedSurface;
        }

        protected override bool BuildIsAffectedByGravity()
        {
            return true;
        }

        protected override double BuildBounciness()
        {
            return 1.0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Cycle of kicked sprite (for instance, helmet)
        /// When cycle is fired, no damage is given from touching this sprite
        /// </summary>
        public Cycle KickedHelmetCycle
        {
            get { return kickedHelmetCycle; }
        }

        /// <summary>
        /// At the end of the cycle, the sprite will start shaking (if fired)
        /// At the very end, the sprite will be transformed into another sprite (if fired)
        /// </summary>
        public Cycle SpontaneousTransformationCycle
        {
            get { return spontaneousTransformationCycle; }
        }

        /// <summary>
        /// Whether monster can jump
        /// </summary>
        public bool IsCanJump
        {
            get { return isCanJump; }
        }

        /// <summary>
        /// Whether sprite will flee just after getting attacked
        /// </summary>
        public bool IsFleeWhenAttacked
        {
            get { return isFleeWhenAttacked; }
        }

        /// <summary>
        /// Whether monster is chasing player
        /// </summary>
        public bool IsAiEnabled
        {
            get { return isAiEnabled; }
        }

        /// <summary>
        /// Whether monster is walking right, not left, when monster is not chasing player
        /// </summary>
        public bool IsNoAiDefaultDirectionWalkingRight
        {
            get { return isNoAiDefaultDirectionWalkingRight; }
            set { isNoAiDefaultDirectionWalkingRight = value; }
        }

        /// <summary>
        /// Whether monster will try to avoid falling in holes
        /// </summary>
        public bool IsAvoidFall
        {
            get { return isAvoidFall; }
            set { isAvoidFall = value; }
        }

        /// <summary>
        /// Whether monster will be at full speed after bouncing on a wall (i.e. bouncing helmets)
        /// </summary>
        public bool IsFullSpeedAfterBounceNoAi
        {
            get { return isFullSpeedAfterBounceNoAi; }
        }

        /// <summary>
        /// Whether monster can currently walk, or is stoped (i.e. stopped helmets)
        /// </summary>
        public bool IsWalkEnabled
        {
            get { return isWalkEnabled; }
            set { isWalkEnabled = value; }
        }

        /// <summary>
        /// Whether monster moves (if doesn't move) or stop moving (if moves) when getting jumped on (i.e. moving helmets)
        /// </summary>
        public bool IsToggleWalkWhenJumpedOn
        {
            get { return isToggleWalkWhenJumpedOn; }
        }

        /// <summary>
        /// True: when jumping on this sprite, will be converted to something that moves already
        /// False: when jumping on this sprite, will be converted to something that doesn't move yet, but waits to be touched or jumped on
        /// </summary>
        public bool IsInstantKickConvertedSprite
        {
            get { return isInstantKickConvertedSprite; }
        }

        /// <summary>
        /// Whether this sprite can be spontaneously transformed into another one when not walking for too long
        /// </summary>
        public bool IsEnableSpontaneousConversion
        {
            get { return isEnableSpontaneousConversion; }
        }

        /// <summary>
        /// Whether sprite can be converted to another sprite when getting jumped on
        /// </summary>
        public bool IsEnableJumpOnConversion
        {
            get { return isEnableJumpOnConversion; }
        }

        /// <summary>
        /// Probability of jumping (from 0 to 1)
        /// </summary>
        public double JumpProbability
        {
            get { return jumpProbability; }
        }
        #endregion
    }
}