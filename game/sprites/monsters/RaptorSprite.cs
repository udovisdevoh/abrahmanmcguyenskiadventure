﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class RaptorSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface walking1LeftSurface;

        private static Surface walking1RightSurface;

        private static Surface walking2LeftSurface;

        private static Surface walking2RightSurface;

        private static Surface standingLeftSurface;

        private static Surface standingRightSurface;

        private static Surface hitLeftSurface;

        private static Surface hitRightSurface;

        private static Surface deadSurface;

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Beware of the rapture!";
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public RaptorSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            GetWalking1RightSurface();
            GetWalking1LeftSurface();
            GetWalking2RightSurface();
            GetWalking2LeftSurface();
            GetStandingRightSurface();
            GetStandingLeftSurface();
            GetHitRightSurface();
            GetHitLeftSurface();
            GetDeadSurface();
            IsUseBottomHitCollisionDeadZone = true;
            IsUseBottomHitCollisionDeadZoneExceptionRadius = true;
            BottomHitCollisionDeadZoneHeight = 1.18;
            BottomHitCollisionDeadZoneExceptionRadius = 0.25;
        }
        #endregion

        #region Private Methods
        private Surface GetWalking1RightSurface()
        {
            if (walking1RightSurface == null)
            {
                if (Program.screenHeight > 720)
                {
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/1080/raptor/walk1.png");
                }
                else if (Program.screenHeight > 480)
                {
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/720/raptor/walk1.png");
                }
                else
                {
                    walking1RightSurface = BuildSpriteSurface("./assets/rendered/480/raptor/walk1.png");
                }
            }
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
            {
                if (Program.screenHeight > 720)
                {
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/1080/raptor/walk2.png");
                }
                else if (Program.screenHeight > 480)
                {
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/720/raptor/walk2.png");
                }
                else
                {
                    walking2RightSurface = BuildSpriteSurface("./assets/rendered/480/raptor/walk2.png");
                }
            }
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
            {
                if (Program.screenHeight > 720)
                {
                    standingRightSurface = BuildSpriteSurface("./assets/rendered/1080/raptor/stand.png");
                }
                else if (Program.screenHeight > 480)
                {
                    standingRightSurface = BuildSpriteSurface("./assets/rendered/720/raptor/stand.png");
                }
                else
                {
                    standingRightSurface = BuildSpriteSurface("./assets/rendered/480/raptor/stand.png");
                }
            }
            return standingRightSurface;
        }

        private Surface GetHitRightSurface()
        {
            if (hitRightSurface == null)
            {
                if (Program.screenHeight > 720)
                {
                    hitRightSurface = BuildSpriteSurface("./assets/rendered/1080/raptor/hit.png");
                }
                else if (Program.screenHeight > 480)
                {
                    hitRightSurface = BuildSpriteSurface("./assets/rendered/720/raptor/hit.png");
                }
                else
                {
                    hitRightSurface = BuildSpriteSurface("./assets/rendered/480/raptor/hit.png");
                }
            }
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
            return 0.011;
            //return 0.004;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.45;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.75;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 28.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildWidth(Random random)
        {
            return 4.0;
        }

        protected override double BuildHeight(Random random)
        {
            return 2.5;
        }

        protected override double BuildMaxHealth()
        {
            return 1.5;
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

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 0.25;
        }

        protected override string BuildTutorialComment()
        {
            return tutorialComment;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
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

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return random.Next(0, 3) == 1;
        }

        protected override bool BuildIsAiEnabled()
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return true;
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

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;

            if (!IsAlive)
                return GetDeadSurface();
            
            if (CurrentJumpAcceleration != 0)
            {
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return GetHitRightSurface();
                    else
                        return GetHitLeftSurface();
                }

                if (IsTryingToWalkRight)
                    return GetStandingRightSurface();
                else
                    return GetStandingLeftSurface();
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                if (cycleDivision == 1)
                {
                    if (IsTryingToWalkRight)
                        return GetWalking1RightSurface();
                    else
                        return GetWalking1LeftSurface();
                }
                else if (cycleDivision == 3)
                {
                    if (IsTryingToWalkRight)
                        return GetWalking2RightSurface();
                    else
                        return GetWalking2LeftSurface();
                }
                else
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return GetHitRightSurface();
                        else
                            return GetHitLeftSurface();
                    }

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