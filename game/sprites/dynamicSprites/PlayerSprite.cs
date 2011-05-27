using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    /// <summary>
    /// Represents a player
    /// </summary>
    internal class PlayerSprite : AbstractSprite
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

        private static Surface crouchedRightSurface;

        private static Surface crouchedLeftSurface;

        private static Surface crouchedHitRightSurface;

        private static Surface crouchedHitLeftSurface;

        private static Surface attackFrame1RightSurface;

        private static Surface attackFrame2RightSurface;

        private static Surface kickFrame1RightSurface;

        private static Surface kickFrame2RightSurface;

        private static Surface crouchedAttackFrame1RightSurface;

        private static Surface crouchedAttackFrame2RightSurface;

        private static Surface attackFrame1LeftSurface;

        private static Surface attackFrame2LeftSurface;

        private static Surface kickFrame1LeftSurface;

        private static Surface kickFrame2LeftSurface;

        private static Surface crouchedAttackFrame1LeftSurface;

        private static Surface crouchedAttackFrame2LeftSurface;

        private static Surface deadSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a player's sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public PlayerSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
        }
        #endregion

        #region Private Methods
        private Surface GetWalking1RightSurface()
        {
            if (walking1RightSurface == null)
                walking1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/walk1.png");
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
                walking2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/walk2.png");

            return walking2RightSurface;
        }

        private Surface GetStandingLeftSurface()
        {
            if (standingLeftSurface == null)
                standingLeftSurface = GetStandingRightSurface().CreateFlippedHorizontalSurface();

            return standingLeftSurface;
        }

        private Surface GetCrouchedRightSurface()
        {
            if (crouchedRightSurface == null)
                crouchedRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouched.png");

            return crouchedRightSurface;
        }

        private Surface GetCrouchedLeftSurface()
        {
            if (crouchedLeftSurface == null)
                crouchedLeftSurface = GetCrouchedRightSurface().CreateFlippedHorizontalSurface();

            return crouchedLeftSurface;
        }

        private Surface GetCrouchedHitRightSurface()
        {
            if (crouchedHitRightSurface == null)
                crouchedHitRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouchedHit.png");

            return crouchedHitRightSurface;
        }

        private Surface GetCrouchedHitLeftSurface()
        {
            if (crouchedHitLeftSurface == null)
                crouchedHitLeftSurface = GetCrouchedHitRightSurface().CreateFlippedHorizontalSurface();

            return crouchedHitLeftSurface;
        }

        private Surface GetHitRightSurface()
        {
            if (hitRightSurface == null)
                hitRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/hit.png");

            return hitRightSurface;
        }

        private Surface GetHitLeftSurface()
        {
            if (hitLeftSurface == null)
                hitLeftSurface = GetHitRightSurface().CreateFlippedHorizontalSurface();

            return hitLeftSurface;
        }

        private Surface GetStandingRightSurface()
        {
            if (standingRightSurface == null)
                standingRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/stand.png");

            return standingRightSurface;
        }

        private Surface GetAttackFrame2RightSurface()
        {
            if (attackFrame2RightSurface == null)
                attackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/punch2.png");

            return attackFrame2RightSurface;
        }

        private Surface GetAttackFrame1RightSurface()
        {
            if (attackFrame1RightSurface == null)
                attackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/punch1.png");

            return attackFrame1RightSurface;
        }

        private Surface GetCrouchedAttackFrame1RightSurface()
        {
            if (crouchedAttackFrame1RightSurface == null)
                crouchedAttackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouchedPunch1.png");

            return crouchedAttackFrame1RightSurface;
        }

        private Surface GetCrouchedAttackFrame2RightSurface()
        {
            if (crouchedAttackFrame2RightSurface == null)
                crouchedAttackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouchedPunch2.png");

            return crouchedAttackFrame2RightSurface;
        }

        private Surface GetAttackFrame2LeftSurface()
        {
            if (attackFrame2LeftSurface == null)
                attackFrame2LeftSurface = GetAttackFrame2RightSurface().CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurface;
        }

        private Surface GetAttackFrame1LeftSurface()
        {
            if (attackFrame1LeftSurface == null)
                attackFrame1LeftSurface = GetAttackFrame1RightSurface().CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurface;
        }

        private Surface GetCrouchedAttackFrame2LeftSurface()
        {
            if (crouchedAttackFrame2LeftSurface == null)
                crouchedAttackFrame2LeftSurface = GetCrouchedAttackFrame2RightSurface().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame2LeftSurface;
        }

        private Surface GetCrouchedAttackFrame1LeftSurface()
        {
            if (crouchedAttackFrame1LeftSurface == null)
                crouchedAttackFrame1LeftSurface = GetCrouchedAttackFrame1RightSurface().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame1LeftSurface;
        }

        private Surface GetKickFrame2LeftSurface()
        {
            if (kickFrame2LeftSurface == null)
                kickFrame2LeftSurface = GetKickFrame2RightSurface().CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurface;
        }

        private Surface GetKickFrame2RightSurface()
        {
            if (kickFrame2RightSurface == null)
                kickFrame2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/kick2.png");

            return kickFrame2RightSurface;
        }

        private Surface GetKickFrame1LeftSurface()
        {
            if (kickFrame1LeftSurface == null)
                kickFrame1LeftSurface = GetKickFrame1RightSurface().CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurface;
        }

        private Surface GetKickFrame1RightSurface()
        {
            if (kickFrame1RightSurface == null)
                kickFrame1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/kick1.png");

            return kickFrame1RightSurface;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetStandingRightSurface().CreateFlippedVerticalSurface();

            return deadSurface;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Build width
        /// </summary>
        /// <returns>width</returns>
        protected override double BuildWidth(Random random)
        {
            return 1.2;
        }

        /// <summary>
        /// Build height
        /// </summary>
        /// <returns>height</returns>
        protected override double BuildHeight(Random random)
        {
            return 2.0;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        protected override double BuildMass(Random random)
        {
            return 1.0;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            return 25.0;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.45;
        }

        protected override double BuildWalkingAcceleration()
        {
            return 0.016;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.60;
        }

        protected override double BuildWalkingCycleLength()
        {
            return 10;
        }

        protected override double BuildJumpingTime()
        {
            return 10.0;
        }

        protected override double BuildAttackingTime()
        {
            return 4;
        }

        protected override double BuildMaxHealth()
        {
            return 1.0;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            //If currently attacking
            int attackCycleDivision = AttackingCycle.GetCycleDivision(8);

            if (!IsAlive)
                return GetDeadSurface();

            #region Attacking
            if (AttackingCycle.IsFired)
            {
                if (IsCrouch)
                {
                    #region Crouched
                    if (IsTryingToWalkRight)
                    {
                        if (attackCycleDivision >= 4)
                        {
                            xOffset = 0.6;
                            return GetCrouchedAttackFrame2RightSurface();
                        }
                        else
                        {
                            xOffset = 0.2;
                            return GetCrouchedAttackFrame1RightSurface();
                        }
                    }
                    else
                    {
                        if (attackCycleDivision >= 4)
                        {
                            xOffset = -0.6;
                            return GetCrouchedAttackFrame2LeftSurface();
                        }
                        else
                        {
                            xOffset = -0.2;
                            return GetCrouchedAttackFrame1LeftSurface();
                        }
                    }
                    #endregion
                }
                else if (Ground == null)
                {
                    #region In air
                    if (IsTryingToWalkRight)
                    {
                        if (attackCycleDivision < 4)
                        {
                            xOffset = 0.35;
                            yOffset = 0.1;
                            return GetKickFrame2RightSurface();
                        }
                        else
                        {
                            xOffset = -0.2;
                            yOffset = 0.0;
                            return GetKickFrame1RightSurface();
                        }
                    }
                    else
                    {
                        if (attackCycleDivision < 4)
                        {
                            xOffset = -0.35;
                            yOffset = 0.1;
                            return GetKickFrame2LeftSurface();
                        }
                        else
                        {
                            xOffset = 0.2;
                            yOffset = 0.0;
                            return GetKickFrame1LeftSurface();
                        }
                    }
                    #endregion
                }
                else
                {
                    if (IsTryingToWalkRight)
                    {
                        if (attackCycleDivision >= 4)
                        {
                            xOffset = 0.6;
                            return GetAttackFrame2RightSurface();
                        }
                        else
                        {
                            xOffset = 0.2;
                            return GetAttackFrame1RightSurface();
                        }
                    }
                    else
                    {
                        if (attackCycleDivision >= 4)
                        {
                            xOffset = -0.6;
                            return GetAttackFrame2LeftSurface();
                        }
                        else
                        {
                            xOffset = -0.2;
                            return GetAttackFrame1LeftSurface();
                        }
                    }
                }
            }
            #endregion

            if (IsCrouch)
            {
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return GetCrouchedHitRightSurface();
                    else
                        return GetCrouchedHitLeftSurface();
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetCrouchedRightSurface();
                    else
                        return GetCrouchedLeftSurface();
                }
            }

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
                    return GetWalking1RightSurface();
                else
                    return GetWalking1LeftSurface();
            }
            else if (/*IsTryingToWalk || */CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

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
