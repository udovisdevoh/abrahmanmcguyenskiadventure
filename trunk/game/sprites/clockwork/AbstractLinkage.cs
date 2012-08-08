using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a mechanical component. It could be linkage on a wheel, a pendulum, a seesaw, an aerial lift, a platform or a liana
    /// Platforms or other linkages are attached to it. (platforms and flail balls are linkages)
    /// Linkages can be attached to "rope-grounds", elevator's vertical line, or can be held in the void, or can be affected by gravity and use the platforms as legs
    /// </summary>
    internal abstract class AbstractLinkage : AbstractSprite
    {
        #region Fields and parts
        /// <summary>
        /// Height of the support (can be 0 in many cases)
        /// </summary>
        private double supportHeight;

        /// <summary>
        /// Parent node of current linkage (can be null)
        /// </summary>
        private AbstractBearing parentNode = null;
        #endregion

        #region Protected Override
        protected override bool BuildIsCrossGrounds()
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToPunch()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 100;
        }

        protected override double BuildJumpingTime()
        {
            return 0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 0;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.1;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.3;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.3;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 0;
        }

        protected override double BuildAttackingTime()
        {
            return 0;
        }

        protected override double BuildHitTime()
        {
            return 0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0;
        }

        protected override int BuildZIndex()
        {
            return 0;
        }

        protected override double BuildMaxFallingSpeed()
        {
            return double.PositiveInfinity;
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Height of the support (can be 0 in many cases)
        /// </summary>
        /// <returns>Height of the support (can be 0 in many cases)</returns>
        public abstract double BuildSupportHeight();
        #endregion

        #region Constructor
        /// <summary>
        /// Build an abstract linkage (wheel, pendulum, seesaw, lift, platform, liana)
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <param name="random"></param>
        public AbstractLinkage(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            supportHeight = BuildSupportHeight();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Height of the support (can be 0 in many cases)
        /// </summary>
        public double SupportHeight
        {
            get { return supportHeight; }
            set { supportHeight = value; }
        }

        /// <summary>
        /// Parent node of current linkage (can be null)
        /// </summary>
        public AbstractBearing ParentNode
        {
            get { return parentNode; }
        }

        /// <summary>
        /// Parent node of current linkage. Don't use directly
        /// </summary>
        public AbstractBearing _ParentNode
        {
            set { parentNode = value; }
        }
        #endregion
    }
}