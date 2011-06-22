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
        public RastaHatSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            MaxFallingSpeed = 0.025f;
            IsCrossGrounds = true;
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (surface == null)
                surface = BuildSpriteSurface("./assets/rendered/powerups/rastaHat.png");
            ChangeDirectionNoAiCycle.CurrentValue = (float)random.NextDouble() * ChangeDirectionNoAiCycle.TotalTimeLength;
        }
        #endregion

        #region Overrides
        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override float BuildMaxHealth()
        {
            return 100f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 30f;
        }

        protected override float BuildAttackingTime()
        {
            return 0f;
        }

        protected override float BuildHitTime()
        {
            return 0f;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return -0.5f;
        }

        protected override float BuildWidth(Random random)
        {
            return 1f;
        }

        protected override float BuildHeight(Random random)
        {
            return 1f;
        }

        public Cycle GrowthCycle
        {
            get { return growthCycle; }
        }

        protected override float BuildJumpingTime()
        {
            return 10f;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 10f;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.016f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.20f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.20f;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0f;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = yOffset = 0f;
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
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

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 38f;
        }

        protected override float BuildJumpProbability()
        {
            return 0f;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }
        #endregion
    }
}
