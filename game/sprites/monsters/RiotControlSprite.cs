﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class RiotControlSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface walkingLeftSurface;

        private static Surface walkingRightSurface;

        private static Surface walking2LeftSurface;

        private static Surface walking2RightSurface;

        private static Surface standingLeftSurface;

        private static Surface standingRightSurface;

        private static Surface standing2LeftSurface;

        private static Surface standing2RightSurface;

        private static Surface deadSurface;

        private static Surface dead2Surface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public RiotControlSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            GetWalkingRightSurface();
            GetWalking2RightSurface();
            GetWalkingLeftSurface();
            GetWalking2LeftSurface();
            GetStandingRightSurface();
            GetStandingLeftSurface();
            GetStanding2RightSurface();
            GetStanding2LeftSurface();
            GetDeadSurface();
            GetDeadSurface2();
        }

        /// <summary>
        /// Create sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isAvoidFall">is avoid fall</param>
        public RiotControlSprite(float xPosition, float yPosition, Random random, bool isAvoidFall)
            : base(xPosition, yPosition, random)
        {
            this.IsAvoidFall = isAvoidFall;
        }
        #endregion

        #region Private Methods
        private Surface GetWalkingRightSurface()
        {
            if (walkingRightSurface == null)
                walkingRightSurface = BuildSpriteSurface("./assets/rendered/riotControl/walk.png");
            return walkingRightSurface;
        }

        private Surface GetWalkingLeftSurface()
        {
            if (walkingLeftSurface == null)
                walkingLeftSurface = GetWalkingRightSurface().CreateFlippedHorizontalSurface();

            return walkingLeftSurface;
        }

        private Surface GetWalking2RightSurface()
        {
            if (walking2RightSurface == null)
                walking2RightSurface = BuildSpriteSurface("./assets/rendered/riotControl/walk2.png");
            return walking2RightSurface;
        }

        private Surface GetWalking2LeftSurface()
        {
            if (walking2LeftSurface == null)
                walking2LeftSurface = GetWalking2RightSurface().CreateFlippedHorizontalSurface();

            return walking2LeftSurface;
        }

        private Surface GetStandingLeftSurface()
        {
            if (standingLeftSurface == null)
                standingLeftSurface = GetStandingRightSurface().CreateFlippedHorizontalSurface();

            return standingLeftSurface;
        }

        private Surface GetStanding2LeftSurface()
        {
            if (standing2LeftSurface == null)
                standing2LeftSurface = GetStanding2RightSurface().CreateFlippedHorizontalSurface();

            return standing2LeftSurface;
        }

        private Surface GetStandingRightSurface()
        {
            if (standingRightSurface == null)
                standingRightSurface = BuildSpriteSurface("./assets/rendered/riotControl/stand.png");

            return standingRightSurface;
        }

        private Surface GetStanding2RightSurface()
        {
            if (standing2RightSurface == null)
                standing2RightSurface = BuildSpriteSurface("./assets/rendered/riotControl/stand2.png");

            return standing2RightSurface;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetStandingRightSurface().CreateFlippedVerticalSurface();

            return deadSurface;
        }

        private Surface GetDeadSurface2()
        {
            if (dead2Surface == null)
                dead2Surface = GetStanding2RightSurface().CreateFlippedVerticalSurface();

            return dead2Surface;
        }
        #endregion

        #region Override Methods
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
            return 0.3f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.17f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.55f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            return 25.0f;
        }

        protected override float BuildAttackingTime()
        {
            return 4;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0f;
        }

        protected override float BuildHeight(Random random)
        {
            return 1.3f;
        }

        protected override float BuildMaxHealth()
        {
            return 1.0f;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0.0f;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return random.Next(0, 2) == 1;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override float BuildJumpProbability()
        {
            return 0.2f;
        }

        protected override float BuildHitTime()
        {
            return 32;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.5f;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return random.Next(0, 2) == 1;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
        {
            return false;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return false;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return false;
        }

        protected override bool BuildIsEnableSpontaneousConversion()
        {
            return false;
        }

        protected override bool BuildIsEnableJumpOnConversion()
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

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return new HelmetSprite(XPosition, YPosition, random, IsAvoidFall);
        }

        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = 0;
            yOffset = 0.24f;
            if (!IsAlive)
            {
                if (IsAvoidFall)
                    return GetDeadSurface();
                else
                    return GetDeadSurface2();
            }

            if (CurrentJumpAcceleration != 0)
            {
                yOffset = 0.4f;
                if (IsAvoidFall)
                {
                    if (IsTryingToWalkRight)
                        return GetWalkingRightSurface();
                    else
                        return GetWalkingLeftSurface();
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetWalking2RightSurface();
                    else
                        return GetWalking2LeftSurface();
                }
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0f);

                if (cycleDivision == 1 || cycleDivision == 3)
                {
                    yOffset = 0.4f;
                    if (IsAvoidFall)
                    {
                        if (IsTryingToWalkRight)
                            return GetWalkingRightSurface();
                        else
                            return GetWalkingLeftSurface();
                    }
                    else
                    {
                        if (IsTryingToWalkRight)
                            return GetWalking2RightSurface();
                        else
                            return GetWalking2LeftSurface();
                    }
                }
                else
                {
                    yOffset = 0.24f;
                    if (IsAvoidFall)
                    {
                        if (IsTryingToWalkRight)
                            return GetStandingRightSurface();
                        else
                            return GetStandingLeftSurface();
                    }
                    else
                    {
                        if (IsTryingToWalkRight)
                            return GetStanding2RightSurface();
                        else
                            return GetStanding2LeftSurface();
                    }
                }
            }
            else
            {
                yOffset = 0.24f;
                if (IsAvoidFall)
                {
                    if (IsTryingToWalkRight)
                        return GetStandingRightSurface();
                    else
                        return GetStandingLeftSurface();
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetStanding2RightSurface();
                    else
                        return GetStanding2LeftSurface();
                }
            }
        }
        #endregion
    }
}