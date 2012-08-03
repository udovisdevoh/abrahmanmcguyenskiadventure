using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a pendulum
    /// </summary>
    #warning Eventually remove abstract keyword
    internal abstract class Pendulum : AbstractLinkage
    {
        #region Override
        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override double BuildBounciness()
        {
            throw new NotImplementedException();
        }

        protected override string BuildTutorialComment()
        {
            return "Play with that pendulum, it's fun! Be careful when there are flail balls attached to it.";
        }

        public override double BuildSupportHeight()
        {
            return 0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Do not use that constructor
        /// </summary>
        public Pendulum()
        {
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public Pendulum(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {   
        }

        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isAffectedByGravity">whether wheel is affected by gravity (default: false)</param>
        /// <param name="supportHeight">support's height (default: 0)</param>
        public Pendulum(double xPosition, double yPosition, Random random, bool isAffectedByGravity, double supportHeight)
            : this(xPosition, yPosition, random)
        {
            this.IsAffectedByGravity = isAffectedByGravity;
            this.SupportHeight = supportHeight;
        }
        #endregion
    }
}
