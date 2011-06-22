using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a mushroom
    /// </summary>
    class MushroomSprite : MonsterSprite, IGrowable
    {
        #region Fields and parts
        private static Surface surface;

        /// <summary>
        /// Cycle of growth
        /// </summary>
        private Cycle growthCycle;
        #endregion

        #region Constructor
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public MushroomSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (surface == null)
                surface = BuildSpriteSurface("./assets/rendered/powerups/mushroom.png");
        }
        #endregion

        #region Override
        protected override float BuildMaxHealth()
        {
            return 100;
        }

        protected override float BuildJumpingTime()
        {
            return 0;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 0;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.016;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.17;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.17;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 20;
        }

        protected override float BuildAttackingTime()
        {
            return 0;
        }

        protected override float BuildHitTime()
        {
            return 0;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return -0.5;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override float BuildHeight(Random random)
        {
            return 1.0;
        }

        protected override float BuildBounciness()
        {
            return 0;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return false;
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
            return true;
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

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return true;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override float BuildJumpProbability()
        {
            return 0;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = yOffset = 0;
            return surface;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Cycle of growth
        /// </summary>
        public Cycle GrowthCycle
        {
            get { return growthCycle; }
        }
        #endregion
    }
}
