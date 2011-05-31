using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Sprite not moving, not affected by gravity
    /// </summary>
    abstract class StaticSprite : AbstractSprite
    {
        #region Fields and parts
        /// <summary>
        /// Whether sprite can be destroyed (default:false)
        /// </summary>
        private bool isDestructible;
        #endregion

        #region Constructor
        /// <summary>
        /// Build static sprite
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <param name="yPosition">Y Position</param>
        /// <param name="random">Random number generator</param>
        public StaticSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            isDestructible = false;
        }

        /// <summary>
        /// Build static sprite
        /// </summary>
        /// <param name="xPosition">X Position</param>
        /// <param name="yPosition">Y Position</param>
        /// <param name="random">Random number generator</param>
        /// <param name="isDestructible">whether sprite can be destroyed (default: false)</param>
        public StaticSprite(double xPosition, double yPosition, Random random, bool isDestructible)
            : base(xPosition, yPosition, random)
        {
            this.isDestructible = isDestructible;
        }
        #endregion

        #region Overrides
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
            return 0;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0;
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
            return 16;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether sprite can be destroyed (default:false)
        /// </summary>
        public bool IsDestructible
        {
            get { return isDestructible; }
            set { isDestructible = value; }
        }
        #endregion
    }
}
