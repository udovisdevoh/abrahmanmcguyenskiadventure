using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// A beaver (similar behavior to Yoshi)
    /// </summary>
    internal class BeaverSprite : MonsterSprite, IGrowable
    {
        #region Fields and parts
        private static Surface standRight;

        private static Surface standLeft;

        private static Surface walkRight;

        private static Surface walkLeft;

        private static Surface hitRight;

        private static Surface hitLeft;

        private static Surface dead;

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
        public BeaverSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            growthCycle = new Cycle(Program.powerUpGrowthTime, false);
            if (standLeft == null)
            {
                standRight = BuildSpriteSurface("./assets/rendered/beaver/BeaverStand.png");
                standLeft = standRight.CreateFlippedHorizontalSurface();

                walkRight = BuildSpriteSurface("./assets/rendered/beaver/BeaverWalk.png");
                walkLeft = walkRight.CreateFlippedHorizontalSurface();

                hitRight = BuildSpriteSurface("./assets/rendered/beaver/BeaverHit.png");
                hitLeft = hitRight.CreateFlippedHorizontalSurface();

                dead = hitRight.CreateFlippedVerticalSurface();
            }
        }
        #endregion

        #region Override
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
            return 5;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.01f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.35f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.60f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 5.0f;
        }

        protected override float BuildAttackingTime()
        {
            return 4;
        }

        protected override float BuildHitTime()
        {
            return 0;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.5f;
        }

        protected override float BuildHeight(Random random)
        {
            return 0.9f;
        }

        protected override float BuildBounciness()
        {
            return 1.0f;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0.0f;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return false;
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

        protected override bool BuildIsVulnerableToInvincibility()
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
            return 0.1f;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = yOffset = 0;

            int cycleDivision = WalkingCycle.GetCycleDivision(4.0f);

            if (!IsAlive)
                return dead;

            if (cycleDivision == 1 || cycleDivision == 3)
            {
                if (IsTryingToWalkRight)
                    return walkRight;
                else
                    return walkLeft;
            }
            else
            {
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return hitRight;
                    else
                        return hitLeft;
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return standRight;
                    else
                        return standLeft;
                }
            }
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
