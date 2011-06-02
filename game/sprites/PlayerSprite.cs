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

        private static Surface walking1LeftSurfaceDoped;

        private static Surface walking1RightSurfaceDoped;

        private static Surface walking2LeftSurfaceDoped;

        private static Surface walking2RightSurfaceDoped;

        private static Surface standingLeftSurfaceDoped;

        private static Surface standingRightSurfaceDoped;

        private static Surface hitLeftSurfaceDoped;

        private static Surface hitRightSurfaceDoped;

        private static Surface crouchedRightSurfaceDoped;

        private static Surface crouchedLeftSurfaceDoped;

        private static Surface crouchedHitRightSurfaceDoped;

        private static Surface crouchedHitLeftSurfaceDoped;

        private static Surface attackFrame1RightSurfaceDoped;

        private static Surface attackFrame2RightSurfaceDoped;

        private static Surface kickFrame1RightSurfaceDoped;

        private static Surface kickFrame2RightSurfaceDoped;

        private static Surface crouchedAttackFrame1RightSurfaceDoped;

        private static Surface crouchedAttackFrame2RightSurfaceDoped;

        private static Surface attackFrame1LeftSurfaceDoped;

        private static Surface attackFrame2LeftSurfaceDoped;

        private static Surface kickFrame1LeftSurfaceDoped;

        private static Surface kickFrame2LeftSurfaceDoped;

        private static Surface crouchedAttackFrame1LeftSurfaceDoped;

        private static Surface crouchedAttackFrame2LeftSurfaceDoped;

        private static Surface kickFrame1RightSurfaceTiny;

        private static Surface kickFrame2RightSurfaceTiny;

        private static Surface kickFrame1LeftSurfaceTiny;

        private static Surface kickFrame2LeftSurfaceTiny;

        private static Surface standingRightSurfaceTiny;

        private static Surface standingLeftSurfaceTiny;

        private static Surface attackFrame1RightSurfaceTiny;

        private static Surface attackFrame2RightSurfaceTiny;

        private static Surface attackFrame1LeftSurfaceTiny;

        private static Surface attackFrame2LeftSurfaceTiny;

        private Cycle powerUpAnimationCycle;

        /// <summary>
        /// Whether sprite can throw fire balls
        /// </summary>
        private bool isDoped = false;

        /// <summary>
        /// Default health
        /// </summary>
        private double defaultHealth = 0.5;
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
            IsTiny = true;
            Health = defaultHealth;
            powerUpAnimationCycle = new Cycle(30, false);
        }
        #endregion

        #region Private Methods
        private Surface GetWalking1RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetWalking1RightSurfaceDoped();

            if (walking1RightSurface == null)
                walking1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/walk1.png");
            return walking1RightSurface;
        }

        private Surface GetWalking1LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetWalking1LeftSurfaceDoped();

            if (walking1LeftSurface == null)
                walking1LeftSurface = GetWalking1RightSurface(false).CreateFlippedHorizontalSurface();

            return walking1LeftSurface;
        }

        private Surface GetWalking2LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetWalking2LeftSurfaceDoped();

            if (walking2LeftSurface == null)
                walking2LeftSurface = GetWalking2RightSurface(false).CreateFlippedHorizontalSurface();

            return walking2LeftSurface;
        }

        private Surface GetWalking2RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetWalking2RightSurfaceDoped();

            if (walking2RightSurface == null)
                walking2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/walk2.png");

            return walking2RightSurface;
        }

        private Surface GetStandingLeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetStandingLeftSurfaceDoped();

            if (standingLeftSurface == null)
                standingLeftSurface = GetStandingRightSurface(false).CreateFlippedHorizontalSurface();

            return standingLeftSurface;
        }

        private Surface GetCrouchedRightSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedRightSurfaceDoped();

            if (crouchedRightSurface == null)
                crouchedRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouched.png");

            return crouchedRightSurface;
        }

        private Surface GetCrouchedLeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedLeftSurfaceDoped();

            if (crouchedLeftSurface == null)
                crouchedLeftSurface = GetCrouchedRightSurface(false).CreateFlippedHorizontalSurface();

            return crouchedLeftSurface;
        }

        private Surface GetCrouchedHitRightSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedHitRightSurfaceDoped();

            if (crouchedHitRightSurface == null)
                crouchedHitRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouchedHit.png");

            return crouchedHitRightSurface;
        }

        private Surface GetCrouchedHitLeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedHitLeftSurfaceDoped();

            if (crouchedHitLeftSurface == null)
                crouchedHitLeftSurface = GetCrouchedHitRightSurface(false).CreateFlippedHorizontalSurface();

            return crouchedHitLeftSurface;
        }

        private Surface GetHitRightSurface(bool isDoped)
        {
            if (isDoped)
                return GetHitRightSurfaceDoped();

            if (hitRightSurface == null)
                hitRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/hit.png");

            return hitRightSurface;
        }

        private Surface GetHitLeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetHitLeftSurfaceDoped();

            if (hitLeftSurface == null)
                hitLeftSurface = GetHitRightSurface(false).CreateFlippedHorizontalSurface();

            return hitLeftSurface;
        }

        private Surface GetStandingRightSurface(bool isDoped)
        {
            if (isDoped)
                return GetStandingRightSurfaceDoped();

            if (standingRightSurface == null)
                standingRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/stand.png");

            return standingRightSurface;
        }

        private Surface GetAttackFrame2RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetAttackFrame2RightSurfaceDoped();

            if (attackFrame2RightSurface == null)
                attackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/punch2.png");

            return attackFrame2RightSurface;
        }

        private Surface GetAttackFrame1RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetAttackFrame1RightSurfaceDoped();

            if (attackFrame1RightSurface == null)
                attackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/punch1.png");

            return attackFrame1RightSurface;
        }

        private Surface GetCrouchedAttackFrame1RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedAttackFrame1RightSurfaceDoped();

            if (crouchedAttackFrame1RightSurface == null)
                crouchedAttackFrame1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouchedPunch1.png");

            return crouchedAttackFrame1RightSurface;
        }

        private Surface GetCrouchedAttackFrame2RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedAttackFrame2RightSurfaceDoped();

            if (crouchedAttackFrame2RightSurface == null)
                crouchedAttackFrame2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouchedPunch2.png");

            return crouchedAttackFrame2RightSurface;
        }

        private Surface GetAttackFrame2LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetAttackFrame2LeftSurfaceDoped();

            if (attackFrame2LeftSurface == null)
                attackFrame2LeftSurface = GetAttackFrame2RightSurface(false).CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurface;
        }

        private Surface GetAttackFrame1LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetAttackFrame1LeftSurfaceDoped();

            if (attackFrame1LeftSurface == null)
                attackFrame1LeftSurface = GetAttackFrame1RightSurface(false).CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurface;
        }

        private Surface GetCrouchedAttackFrame2LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedAttackFrame2LeftSurfaceDoped();

            if (crouchedAttackFrame2LeftSurface == null)
                crouchedAttackFrame2LeftSurface = GetCrouchedAttackFrame2RightSurface(false).CreateFlippedHorizontalSurface();

            return crouchedAttackFrame2LeftSurface;
        }

        private Surface GetCrouchedAttackFrame1LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetCrouchedAttackFrame1LeftSurfaceDoped();

            if (crouchedAttackFrame1LeftSurface == null)
                crouchedAttackFrame1LeftSurface = GetCrouchedAttackFrame1RightSurface(false).CreateFlippedHorizontalSurface();

            return crouchedAttackFrame1LeftSurface;
        }

        private Surface GetKickFrame2LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetKickFrame2LeftSurfaceDoped();

            if (kickFrame2LeftSurface == null)
                kickFrame2LeftSurface = GetKickFrame2RightSurface(false).CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurface;
        }

        private Surface GetKickFrame2RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetKickFrame2RightSurfaceDoped();

            if (kickFrame2RightSurface == null)
                kickFrame2RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/kick2.png");

            return kickFrame2RightSurface;
        }

        private Surface GetKickFrame1LeftSurface(bool isDoped)
        {
            if (isDoped)
                return GetKickFrame1LeftSurfaceDoped();

            if (kickFrame1LeftSurface == null)
                kickFrame1LeftSurface = GetKickFrame1RightSurface(false).CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurface;
        }

        private Surface GetKickFrame1RightSurface(bool isDoped)
        {
            if (isDoped)
                return GetKickFrame1RightSurfaceDoped();

            if (kickFrame1RightSurface == null)
                kickFrame1RightSurface = BuildSpriteSurface("./assets/rendered/abrahman/kick1.png");

            return kickFrame1RightSurface;
        }

        private Surface GetWalking1RightSurfaceDoped()
        {
            if (walking1RightSurfaceDoped == null)
                walking1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/walk1doped.png");
            return walking1RightSurfaceDoped;
        }

        private Surface GetWalking1LeftSurfaceDoped()
        {
            if (walking1LeftSurfaceDoped == null)
                walking1LeftSurfaceDoped = GetWalking1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return walking1LeftSurfaceDoped;
        }

        private Surface GetWalking2LeftSurfaceDoped()
        {
            if (walking2LeftSurfaceDoped == null)
                walking2LeftSurfaceDoped = GetWalking2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return walking2LeftSurfaceDoped;
        }

        private Surface GetWalking2RightSurfaceDoped()
        {
            if (walking2RightSurfaceDoped == null)
                walking2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/walk2doped.png");

            return walking2RightSurfaceDoped;
        }

        private Surface GetStandingLeftSurfaceDoped()
        {
            if (standingLeftSurfaceDoped == null)
                standingLeftSurfaceDoped = GetStandingRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return standingLeftSurfaceDoped;
        }

        private Surface GetCrouchedRightSurfaceDoped()
        {
            if (crouchedRightSurfaceDoped == null)
                crouchedRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/croucheddoped.png");

            return crouchedRightSurfaceDoped;
        }

        private Surface GetCrouchedLeftSurfaceDoped()
        {
            if (crouchedLeftSurfaceDoped == null)
                crouchedLeftSurfaceDoped = GetCrouchedRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedLeftSurfaceDoped;
        }

        private Surface GetCrouchedHitRightSurfaceDoped()
        {
            if (crouchedHitRightSurfaceDoped == null)
                crouchedHitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/crouchedHitdoped.png");

            return crouchedHitRightSurfaceDoped;
        }

        private Surface GetCrouchedHitLeftSurfaceDoped()
        {
            if (crouchedHitLeftSurfaceDoped == null)
                crouchedHitLeftSurfaceDoped = GetCrouchedHitRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedHitLeftSurfaceDoped;
        }

        private Surface GetHitRightSurfaceDoped()
        {
            if (hitRightSurfaceDoped == null)
                hitRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/hitdoped.png");

            return hitRightSurfaceDoped;
        }

        private Surface GetHitLeftSurfaceDoped()
        {
            if (hitLeftSurfaceDoped == null)
                hitLeftSurfaceDoped = GetHitRightSurfaceDoped().CreateFlippedHorizontalSurface();

            return hitLeftSurfaceDoped;
        }

        private Surface GetStandingRightSurfaceDoped()
        {
            if (standingRightSurfaceDoped == null)
                standingRightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/standdoped.png");

            return standingRightSurfaceDoped;
        }

        private Surface GetAttackFrame2RightSurfaceDoped()
        {
            if (attackFrame2RightSurfaceDoped == null)
                attackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/punch2doped.png");

            return attackFrame2RightSurfaceDoped;
        }

        private Surface GetAttackFrame1RightSurfaceDoped()
        {
            if (attackFrame1RightSurfaceDoped == null)
                attackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/punch1doped.png");

            return attackFrame1RightSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame1RightSurfaceDoped()
        {
            if (crouchedAttackFrame1RightSurfaceDoped == null)
                crouchedAttackFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/crouchedPunch1doped.png");

            return crouchedAttackFrame1RightSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame2RightSurfaceDoped()
        {
            if (crouchedAttackFrame2RightSurfaceDoped == null)
                crouchedAttackFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/crouchedPunch2doped.png");

            return crouchedAttackFrame2RightSurfaceDoped;
        }

        private Surface GetAttackFrame2LeftSurfaceDoped()
        {
            if (attackFrame2LeftSurfaceDoped == null)
                attackFrame2LeftSurfaceDoped = GetAttackFrame2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurfaceDoped;
        }

        private Surface GetAttackFrame1LeftSurfaceDoped()
        {
            if (attackFrame1LeftSurfaceDoped == null)
                attackFrame1LeftSurfaceDoped = GetAttackFrame1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame2LeftSurfaceDoped()
        {
            if (crouchedAttackFrame2LeftSurfaceDoped == null)
                crouchedAttackFrame2LeftSurfaceDoped = GetCrouchedAttackFrame2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame2LeftSurfaceDoped;
        }

        private Surface GetCrouchedAttackFrame1LeftSurfaceDoped()
        {
            if (crouchedAttackFrame1LeftSurfaceDoped == null)
                crouchedAttackFrame1LeftSurfaceDoped = GetCrouchedAttackFrame1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return crouchedAttackFrame1LeftSurfaceDoped;
        }

        private Surface GetKickFrame2LeftSurfaceDoped()
        {
            if (kickFrame2LeftSurfaceDoped == null)
                kickFrame2LeftSurfaceDoped = GetKickFrame2RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurfaceDoped;
        }

        private Surface GetKickFrame2RightSurfaceDoped()
        {
            if (kickFrame2RightSurfaceDoped == null)
                kickFrame2RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/kick2doped.png");

            return kickFrame2RightSurfaceDoped;
        }

        private Surface GetKickFrame1LeftSurfaceDoped()
        {
            if (kickFrame1LeftSurfaceDoped == null)
                kickFrame1LeftSurfaceDoped = GetKickFrame1RightSurfaceDoped().CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurfaceDoped;
        }

        private Surface GetKickFrame1RightSurfaceDoped()
        {
            if (kickFrame1RightSurfaceDoped == null)
                kickFrame1RightSurfaceDoped = BuildSpriteSurface("./assets/rendered/abrahman/kick1doped.png");

            return kickFrame1RightSurfaceDoped;
        }

        private Surface GetKickFrame1LeftSurfaceTiny()
        {
            if (kickFrame1LeftSurfaceTiny == null)
                kickFrame1LeftSurfaceTiny = GetKickFrame1RightSurfaceTiny().CreateFlippedHorizontalSurface();

            return kickFrame1LeftSurfaceTiny;
        }

        private Surface GetKickFrame2LeftSurfaceTiny()
        {
            if (kickFrame2LeftSurfaceTiny == null)
                kickFrame2LeftSurfaceTiny = GetKickFrame2RightSurfaceTiny().CreateFlippedHorizontalSurface();

            return kickFrame2LeftSurfaceTiny;
        }

        private Surface GetStandingRightSurfaceTiny()
        {
            if (standingRightSurfaceTiny == null)
                standingRightSurfaceTiny = BuildSpriteSurface("./assets/rendered/abrahman/tinyStand.png");

            return standingRightSurfaceTiny;
        }

        private Surface GetStandingLeftSurfaceTiny()
        {
            if (standingLeftSurfaceTiny == null)
                standingLeftSurfaceTiny = GetStandingRightSurfaceTiny().CreateFlippedHorizontalSurface();

            return standingLeftSurfaceTiny;
        }

        private Surface GetKickFrame1RightSurfaceTiny()
        {
            if (kickFrame1RightSurfaceTiny == null)
                kickFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/abrahman/tinyKick1.png");

            return kickFrame1RightSurfaceTiny;
        }

        private Surface GetKickFrame2RightSurfaceTiny()
        {
            if (kickFrame2RightSurfaceTiny == null)
                kickFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/abrahman/tinyKick2.png");

            return kickFrame2RightSurfaceTiny;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetStandingRightSurfaceTiny().CreateFlippedVerticalSurface();

            return deadSurface;
        }

        private Surface GetAttackFrame1LeftSurfaceTiny()
        {
            if (attackFrame1LeftSurfaceTiny == null)
                attackFrame1LeftSurfaceTiny = GetAttackFrame1RightSurfaceTiny().CreateFlippedHorizontalSurface();

            return attackFrame1LeftSurfaceTiny;
        }

        private Surface GetAttackFrame2LeftSurfaceTiny()
        {
            if (attackFrame2LeftSurfaceTiny == null)
                attackFrame2LeftSurfaceTiny = GetAttackFrame2RightSurfaceTiny().CreateFlippedHorizontalSurface();

            return attackFrame2LeftSurfaceTiny;
        }

        private Surface GetAttackFrame1RightSurfaceTiny()
        {
            if (attackFrame1RightSurfaceTiny == null)
                attackFrame1RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/abrahman/tinyPunch1.png");

            return attackFrame1RightSurfaceTiny;
        }

        private Surface GetAttackFrame2RightSurfaceTiny()
        {
            if (attackFrame2RightSurfaceTiny == null)
                attackFrame2RightSurfaceTiny = BuildSpriteSurface("./assets/rendered/abrahman/tinyPunch2.png");

            return attackFrame2RightSurfaceTiny;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Whether sprite is affected by gravity
        /// </summary>
        /// <returns>Whether sprite is affected by gravity</returns>
        protected override bool BuildIsAffectedByGravity()
        {
            return true;
        }

        protected override bool BuildIsImpassable()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

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

        protected override double BuildBounciness()
        {
            return 1.0;
        }

        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            //If currently attacking
            int attackCycleDivision = AttackingCycle.GetCycleDivision(8);

            if (!IsAlive)
                return GetDeadSurface();

            bool isShowDopedColor;
            if (powerUpAnimationCycle.IsFired)
            {
                isShowDopedColor = ((int)(powerUpAnimationCycle.CurrentValue) % 4 >= 2);
            }
            else
            {
                isShowDopedColor = isDoped;
            }

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
                            return GetCrouchedAttackFrame2RightSurface(isShowDopedColor);
                        }
                        else
                        {
                            xOffset = 0.2;
                            return GetCrouchedAttackFrame1RightSurface(isShowDopedColor);
                        }
                    }
                    else
                    {
                        if (attackCycleDivision >= 4)
                        {
                            xOffset = -0.6;
                            return GetCrouchedAttackFrame2LeftSurface(isShowDopedColor);
                        }
                        else
                        {
                            xOffset = -0.2;
                            return GetCrouchedAttackFrame1LeftSurface(isShowDopedColor);
                        }
                    }
                    #endregion
                }
                else if (IGround == null)
                {
                    #region In air
                    if (IsTiny)
                    {
                        #region Tiny
                        if (IsTryingToWalkRight)
                        {
                            if (attackCycleDivision < 4)
                            {
                                xOffset = 0.35;
                                yOffset = 0.1;
                                return GetKickFrame2RightSurfaceTiny();
                            }
                            else
                            {
                                xOffset = -0.2;
                                yOffset = 0.0;
                                return GetKickFrame1RightSurfaceTiny();
                            }
                        }
                        else
                        {
                            if (attackCycleDivision < 4)
                            {
                                xOffset = -0.35;
                                yOffset = 0.1;
                                return GetKickFrame2LeftSurfaceTiny();
                            }
                            else
                            {
                                xOffset = 0.2;
                                yOffset = 0.0;
                                return GetKickFrame1LeftSurfaceTiny();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Not tiny
                        if (IsTryingToWalkRight)
                        {
                            if (attackCycleDivision < 4)
                            {
                                xOffset = 0.35;
                                yOffset = 0.1;
                                return GetKickFrame2RightSurface(isShowDopedColor);
                            }
                            else
                            {
                                xOffset = -0.2;
                                yOffset = 0.0;
                                return GetKickFrame1RightSurface(isShowDopedColor);
                            }
                        }
                        else
                        {
                            if (attackCycleDivision < 4)
                            {
                                xOffset = -0.35;
                                yOffset = 0.1;
                                return GetKickFrame2LeftSurface(isShowDopedColor);
                            }
                            else
                            {
                                xOffset = 0.2;
                                yOffset = 0.0;
                                return GetKickFrame1LeftSurface(isShowDopedColor);
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    if (IsTiny)
                    {
                        #region Tiny
                        if (IsTryingToWalkRight)
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = 0.6;
                                return GetAttackFrame2RightSurfaceTiny();
                            }
                            else
                            {
                                xOffset = 0.2;
                                return GetAttackFrame1RightSurfaceTiny();
                            }
                        }
                        else
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = -0.6;
                                return GetAttackFrame2LeftSurfaceTiny();
                            }
                            else
                            {
                                xOffset = -0.2;
                                return GetAttackFrame1LeftSurfaceTiny();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Not tiny
                        if (IsTryingToWalkRight)
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = 0.6;
                                return GetAttackFrame2RightSurface(isShowDopedColor);
                            }
                            else
                            {
                                xOffset = 0.2;
                                return GetAttackFrame1RightSurface(isShowDopedColor);
                            }
                        }
                        else
                        {
                            if (attackCycleDivision >= 4)
                            {
                                xOffset = -0.6;
                                return GetAttackFrame2LeftSurface(isShowDopedColor);
                            }
                            else
                            {
                                xOffset = -0.2;
                                return GetAttackFrame1LeftSurface(isShowDopedColor);
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion

            #region Crouched and not attacking
            if (IsCrouch)
            {
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return GetCrouchedHitRightSurface(isShowDopedColor);
                    else
                        return GetCrouchedHitLeftSurface(isShowDopedColor);
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetCrouchedRightSurface(isShowDopedColor);
                    else
                        return GetCrouchedLeftSurface(isShowDopedColor);
                }
            }
            #endregion

            if (CurrentJumpAcceleration != 0)
            {
                #region Jumping or falling while being hit
                if (HitCycle.IsFired)
                {
                    if (IsTryingToWalkRight)
                        return GetHitRightSurface(isShowDopedColor);
                    else
                        return GetHitLeftSurface(isShowDopedColor);
                }
                #endregion

                #region Jumping or falling
                if (IsTryingToWalkRight)
                    return GetWalking1RightSurface(isShowDopedColor);
                else
                    return GetWalking1LeftSurface(isShowDopedColor);
                #endregion
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                #region Walking
                if (cycleDivision == 1)
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return GetHitRightSurface(isShowDopedColor);
                        else
                            return GetHitLeftSurface(isShowDopedColor);
                    }

                    if (IsTryingToWalkRight)
                        return GetWalking1RightSurface(isShowDopedColor);
                    else
                        return GetWalking1LeftSurface(isShowDopedColor);
                }
                else if (cycleDivision == 3)
                {
                    if (HitCycle.IsFired)
                    {
                        if (IsTryingToWalkRight)
                            return GetHitRightSurface(isShowDopedColor);
                        else
                            return GetHitLeftSurface(isShowDopedColor);
                    }

                    if (IsTryingToWalkRight)
                        return GetWalking2RightSurface(isShowDopedColor);
                    else
                        return GetWalking2LeftSurface(isShowDopedColor);
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetStandingRightSurface(isShowDopedColor);
                    else
                        return GetStandingLeftSurface(isShowDopedColor);
                }
                #endregion
            }
            else
            {
                #region Standing
                if (IsTiny)
                {
                    if (IsTryingToWalkRight)
                        return GetStandingRightSurfaceTiny();
                    else
                        return GetStandingLeftSurfaceTiny();
                }
                else
                {
                    if (IsTryingToWalkRight)
                        return GetStandingRightSurface(isShowDopedColor);
                    else
                        return GetStandingLeftSurface(isShowDopedColor);
                }
                #endregion
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Whether sprite can throw fire balls
        /// </summary>
        public bool IsDoped
        {
            get { return isDoped; }
            set { isDoped = value; }
        }

        /// <summary>
        /// Health when starting a level or after death
        /// </summary>
        public double DefaultHealth
        {
            get { return defaultHealth; }
        }

        /// <summary>
        /// Power up animation cycle
        /// </summary>
        public Cycle PowerUpAnimationCycle
        {
            get { return powerUpAnimationCycle; }
        }
        #endregion
    }
}
