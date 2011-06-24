using System;
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
        public RiotControlSprite(double xPosition, double yPosition, Random random)
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
        public RiotControlSprite(double xPosition, double yPosition, Random random, bool isAvoidFall)
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
        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 5;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.3;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.17;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.55;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return 1.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 1.3;
        }

        protected override double BuildMaxHealth()
        {
            return 1.0;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
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

        protected override double BuildJumpProbability()
        {
            return 0.2;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
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

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
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
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0.24;
            if (!IsAlive)
            {
                if (IsAvoidFall)
                    return GetDeadSurface();
                else
                    return GetDeadSurface2();
            }

            if (CurrentJumpAcceleration != 0)
            {
                yOffset = 0.4;
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
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                if (cycleDivision == 1 || cycleDivision == 3)
                {
                    yOffset = 0.4;
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
                    yOffset = 0.24;
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
                yOffset = 0.24;
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