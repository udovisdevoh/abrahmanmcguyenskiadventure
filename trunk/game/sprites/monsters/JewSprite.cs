using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class JewSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface walking1LeftSurface;

        private static Surface walking1RightSurface;

        private static Surface walking2LeftSurface;

        private static Surface walking2RightSurface;

        private static Surface standingLeftSurface;

        private static Surface standingRightSurface;

        private static Surface hitRightSurface;

        private static Surface hitLeftSurface;

        private static Surface deadSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public JewSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            GetWalking1RightSurface();
            GetWalking2RightSurface();
            GetWalking1LeftSurface();
            GetWalking2LeftSurface();
            GetStandingRightSurface();
            GetStandingLeftSurface();
            GetHitRightSurface();
            GetHitLeftSurface();
            GetDeadSurface();
        }
        #endregion

        #region Private Methods
        private Surface GetWalking1RightSurface()
        {
            if (walking1RightSurface == null)
                walking1RightSurface = BuildSpriteSurface("./assets/rendered/jew/walk1.png");
            return walking1RightSurface;
        }

        private Surface GetWalking1LeftSurface()
        {
            if (walking1LeftSurface == null)
                walking1LeftSurface = GetWalking1RightSurface().CreateFlippedHorizontalSurface();

            return walking1LeftSurface;
        }

        private Surface GetWalking2LeftSurface()
        {
            if (walking2LeftSurface == null)
                walking2LeftSurface = GetWalking2RightSurface().CreateFlippedHorizontalSurface();

            return walking2LeftSurface;
        }

        private Surface GetWalking2RightSurface()
        {
            if (walking2RightSurface == null)
                walking2RightSurface = BuildSpriteSurface("./assets/rendered/jew/walk2.png");

            return walking2RightSurface;
        }

        private Surface GetStandingLeftSurface()
        {
            if (standingLeftSurface == null)
                standingLeftSurface = GetStandingRightSurface().CreateFlippedHorizontalSurface();

            return standingLeftSurface;
        }

        private Surface GetStandingRightSurface()
        {
            if (standingRightSurface == null)
                standingRightSurface = BuildSpriteSurface("./assets/rendered/jew/stand.png");

            return standingRightSurface;
        }

        private Surface GetHitRightSurface()
        {
            if (hitRightSurface == null)
                hitRightSurface = BuildSpriteSurface("./assets/rendered/jew/hit.png");

            return hitRightSurface;
        }

        private Surface GetHitLeftSurface()
        {
            if (hitLeftSurface == null)
                hitLeftSurface = GetHitRightSurface().CreateFlippedHorizontalSurface();

            return hitLeftSurface;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetHitRightSurface().CreateFlippedVerticalSurface();

            return deadSurface;
        }
        #endregion

        #region Override Methods
        protected override float BuildJumpingTime()
        {
            return 10f;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 5f;
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
            return 0.65f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 25f;
        }

        protected override float BuildAttackingTime()
        {
            return 4f;
        }

        protected override float BuildWidth(Random random)
        {
            return 1f;
        }

        protected override float BuildHeight(Random random)
        {
            return 2f;
        }

        protected override float BuildMaxHealth()
        {
            return 1f;
        }

        protected override float BuildJumpProbability()
        {
            return 0.2f;
        }

        protected override float BuildHitTime()
        {
            return 32f;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.5f;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0f;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return true;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return random.Next(0, 2) == 1;
        }

        protected override bool BuildIsAiEnabled()
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return true;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
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

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 100f;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            if (IsTryingToWalkRight)
                xOffset = 0.1f;
            else
                xOffset = -0.1f;
            yOffset = 0f;

            if (!IsAlive)
                return GetDeadSurface();

            if (CurrentJumpAcceleration != 0f)
            {
                if (IsTryingToWalkRight)
                    return GetWalking1RightSurface();
                else
                    return GetWalking1LeftSurface();
            }
            else if (CurrentWalkingSpeed != 0f)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0f);

                if (cycleDivision == 1)
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return GetHitRightSurface();
                        else
                            return GetHitLeftSurface();
                    }

                    if (IsTryingToWalkRight)
                        return GetWalking1RightSurface();
                    else
                        return GetWalking1LeftSurface();
                }
                else if (cycleDivision == 3)
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return GetHitRightSurface();
                        else
                            return GetHitLeftSurface();
                    }

                    if (IsTryingToWalkRight)
                        return GetWalking2RightSurface();
                    else
                        return GetWalking2LeftSurface();
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetStandingRightSurface();
                    else
                        return GetStandingLeftSurface();
                }
            }
            else
            {
                if (IsTryingToWalkRight)
                    return GetStandingRightSurface();
                else
                    return GetStandingLeftSurface();
            }
        }
        #endregion
    }
}