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
    internal class RastaHatSprite : MonsterSprite
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
            MaxFallingSpeed = 0.025;
            IsCrossGrounds = true;
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (surface == null)
                surface = BuildSpriteSurface("./assets/rendered/powerups/rastaHat.png");
            ChangeDirectionNoAiCycle.CurrentValue = random.NextDouble() * ChangeDirectionNoAiCycle.TotalTimeLength;
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
            return 30;
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
            return -0.5;
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

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = yOffset = 0.0;
            return surface;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return false;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsEnableSpontaneousConversion()
        {
            return false;
        }

        protected override bool BuildIsEnableJumpOnConversion()
        {
            return false;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return true;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 38;
        }

        protected override double BuildJumpProbability()
        {
            return 0.0;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }
        #endregion
    }
}
