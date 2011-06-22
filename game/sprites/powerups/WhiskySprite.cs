using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Makes player invincible
    /// </summary>
    class WhiskySprite : MonsterSprite, IGrowable
    {
        #region Fields and parts
        private static Surface scotch1;

        private static Surface scotch2;

        private static Surface scotch3;

        private static Surface scotch4;

        private static Surface scotch5;

        private static Surface scotch6;

        private static Surface scotch7;

        private static Surface scotch8;

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
        public WhiskySprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (scotch1 == null)
            {
                scotch1 = BuildSpriteSurface("./assets/rendered/powerups/scotch1.png");
                scotch2 = BuildSpriteSurface("./assets/rendered/powerups/scotch2.png");
                scotch3 = BuildSpriteSurface("./assets/rendered/powerups/scotch3.png");
                scotch4 = scotch2.CreateFlippedVerticalSurface();
                scotch5 = scotch1.CreateFlippedVerticalSurface();
                scotch6 = scotch4.CreateFlippedHorizontalSurface();
                scotch7 = scotch3.CreateFlippedHorizontalSurface();
                scotch8 = scotch2.CreateFlippedHorizontalSurface();
            }
        }
        #endregion

        #region Overrides
        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
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

        protected override bool BuildIsJumpableOn()
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

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override float BuildJumpProbability()
        {
            return 0.0f;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0.0f;
        }

        protected override float BuildMaxHealth()
        {
            return 100;
        }

        protected override float BuildJumpingTime()
        {
            return 10.0f;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 3;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.01f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.20f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.40f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 30.0f;
        }

        protected override float BuildAttackingTime()
        {
            return 4;
        }

        protected override float BuildHitTime()
        {
            return 32;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 1.0f;
        }

        protected override float BuildWidth(Random random)
        {
            return 0.25f;
        }

        protected override float BuildHeight(Random random)
        {
            return 0.25f;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = 0f;
            yOffset = 0f;

            int cycleDivision = WalkingCycle.GetCycleDivision(8.0f);

            if (!IsNoAiDefaultDirectionWalkingRight)
                cycleDivision = cycleDivision * -1 + 7;

            switch (cycleDivision)
            {
                case 1:
                    return scotch1;
                case 2:
                    return scotch2;
                case 3:
                    return scotch3;
                case 4:
                    return scotch4;
                case 5:
                    return scotch5;
                case 6:
                    return scotch6;
                case 7:
                    return scotch7;
                default:
                    return scotch8;
            }
        }
        #endregion

        #region IGrowable Members
        public Cycle GrowthCycle
        {
            get { return growthCycle; }
        }
        #endregion
    }
}
