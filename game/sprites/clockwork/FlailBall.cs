﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a flail ball attached to a linkage
    /// </summary>
    internal class FlailBall : AbstractLinkage, IEvilSprite
    {
        #region Private members
        private static Surface surface;
        #endregion

        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.69;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.69;
        }

        protected override double BuildBounciness()
        {
            return 0;
        }

        protected override string BuildTutorialComment()
        {
            return "Beware the flail!";
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        public override double BuildSupportHeight()
        {
            return 0;
        }

        protected override int BuildZIndex()
        {
            return 1;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0.0655;
            yOffset = 0.655;
            return surface;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public FlailBall(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (surface == null)
            {
                if (Program.screenHeight > 720)
                    surface = BuildSpriteSurface("./assets/rendered/1080/clockwork/FlailBall.png");
                else if (Program.screenHeight > 480)
                    surface = BuildSpriteSurface("./assets/rendered/720/clockwork/FlailBall.png");
                else
                    surface = BuildSpriteSurface("./assets/rendered/480/clockwork/FlailBall.png");
            }
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isAffectedByGravity">whether wheel is affected by gravity (default: false)</param>
        /// <param name="supportHeight">support's height (default: 0)</param>
        public FlailBall(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight)
            : this(xPosition, yPosition, random)
        {
            this.IsAffectedByGravity = isAffectedByGravity;
            this.IsCrossGrounds = !isAffectedByGravity;
            this.SupportHeight = supportHeight;
        }
        #endregion
    }
}
