using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Rasta hat (so player can fly)
    /// </summary>
    internal class RastaHatSprite : AbstractSprite, IGrowable, IFloatable
    {
        #region Fields and parts
        /// <summary>
        /// Surface
        /// </summary>
        private static Surface surface;

        /// <summary>
        /// Cycle of growth
        /// </summary>
        private Cycle growthCycle;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public RastaHatSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (surface == null)
                surface = BuildSpriteSurface("./assets/rendered/powerups/rastaHat.png");
        }
        #endregion

        #region Overrides
        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override double BuildMaxHealth()
        {
            return 100.0;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 10;
        }

        protected override double BuildAttackingTime()
        {
            return 0.0;
        }

        protected override double BuildHitTime()
        {
            return 0.0;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.0;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.0;
        }

        public Cycle GrowthCycle
        {
            get { return growthCycle; }
        }

        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 10;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.016;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.20;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.20;
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAffectedByGravity()
        {
            return false;
        }

        protected override double BuildBounciness()
        {
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0.0;
            return surface;
        }
        #endregion
    }
}
