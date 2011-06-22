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
        public StaticSprite(float xPosition, float yPosition, Random random)
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
        public StaticSprite(float xPosition, float yPosition, Random random, bool isDestructible)
            : base(xPosition, yPosition, random)
        {
            this.isDestructible = isDestructible;
        }
        #endregion

        #region Overrides
        protected override bool BuildIsCrossGrounds()
        {
            return false;
        }

        protected override float BuildJumpingTime()
        {
            return 0f;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 0f;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 0f;
        }

        protected override float BuildAttackingTime()
        {
            return 0f;
        }

        protected override float BuildHitTime()
        {
            return 16f;
        }

        protected override float BuildMaxFallingSpeed()
        {
            return float.PositiveInfinity;
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
