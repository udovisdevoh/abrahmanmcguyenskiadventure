using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Catholic priest
    /// </summary>
    internal class PriestSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface deadSurface;

        private static Surface walking1RightSurface;

        private static Surface walking1LeftSurface;

        private static Surface walking2RightSurface;

        private static Surface walking2LeftSurface;

        private static Surface standingRightSurface;

        private static Surface standingLeftSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public PriestSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            if (deadSurface == null)
            {
                standingRightSurface = BuildSpriteSurface("./assets/rendered/priest/stand.png");
                standingLeftSurface = standingRightSurface.CreateFlippedHorizontalSurface();
                walking1RightSurface = BuildSpriteSurface("./assets/rendered/priest/walk1.png");
                walking2RightSurface = BuildSpriteSurface("./assets/rendered/priest/walk2.png");
                walking1LeftSurface = walking1RightSurface.CreateFlippedHorizontalSurface();
                walking2LeftSurface = walking2RightSurface.CreateFlippedHorizontalSurface();
                deadSurface = walking1RightSurface.CreateFlippedVerticalSurface();
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
            return random.Next(0, 2) == 1;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return true;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return true;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
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

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override float BuildJumpProbability()
        {
            return 0.2;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 0.0;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0.0;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override float BuildMaxHealth()
        {
            return 0.5;
        }

        protected override float BuildJumpingTime()
        {
            return 10.0;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 3;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.01;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.20;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.20;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 8.0;
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
            return 0.5;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override float BuildHeight(Random random)
        {
            return 1.9;
        }

        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = yOffset = 0;
            if (!IsAlive)
                return deadSurface;

            if (CurrentJumpAcceleration != 0)
            {
                if (IsTryingToWalkRight)
                    return walking1RightSurface;
                else
                    return walking1LeftSurface;
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                if (cycleDivision == 1)
                {
                    if (IsTryingToWalkRight)
                        return walking1RightSurface;
                    else
                        return walking1LeftSurface;
                }
                else if (cycleDivision == 3)
                {
                    if (IsTryingToWalkRight)
                        return walking2RightSurface;
                    else
                        return walking2LeftSurface;
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return standingRightSurface;
                    else
                        return standingLeftSurface;
                }
            }
            else
            {
                if (IsTryingToWalkRight)
                    return standingRightSurface;
                else
                    return standingLeftSurface;
            }
        }
        #endregion
    }
}
