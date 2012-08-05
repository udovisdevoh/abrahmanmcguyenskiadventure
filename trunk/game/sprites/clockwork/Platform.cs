using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a platform attached to a wheel or any mechanical component
    /// </summary>
    #warning Eventually remove abstract keyword
    internal class Platform : AbstractLinkage
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
            return 3.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 0.5;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        protected override bool BuildIsImpassable()
        {
            return true;
        }

        protected override string BuildTutorialComment()
        {
            return "You can jump on moving platforms.";
        }

        public override double BuildSupportHeight()
        {
            return 0;
        }

        protected override int BuildZIndex()
        {
            return 1;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0;
            return surface;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Do not use that constructor
        /// </summary>
        public Platform()
        {
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public Platform(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (surface == null)
            {
                if (Program.screenHeight > 720)
                    surface = BuildSpriteSurface("./assets/rendered/1080/clockwork/Platform.png");
                else if (Program.screenHeight > 480)
                    surface = BuildSpriteSurface("./assets/rendered/720/clockwork/Platform.png");
                else
                    surface = BuildSpriteSurface("./assets/rendered/480/clockwork/Platform.png");
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
        public Platform(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight)
            : this(xPosition, yPosition, random)
        {
            this.IsAffectedByGravity = isAffectedByGravity;
            this.IsCrossGrounds = !isAffectedByGravity;
            this.SupportHeight = supportHeight;
        }
        #endregion
    }
}
