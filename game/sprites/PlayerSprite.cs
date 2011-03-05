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
        private Surface walking1LeftSurface;

        private Surface walking1RightSurface;

        private Surface walking2LeftSurface;

        private Surface walking2RightSurface;

        private Surface standingLeftSurface;

        private Surface standingRightSurface;

        private Surface crouchedRightSurface;

        private Surface crouchedLeftSurface;

        private Surface attackRightSurface;

        private Surface kickRightSurface;

        private Surface crouchedAttackRightSurface;

        private Surface attackLeftSurface;

        private Surface kickLeftSurface;

        private Surface crouchedAttackLeftSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a player's sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        public PlayerSprite(double xPosition, double yPosition)
            : base(xPosition, yPosition)
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

        private Surface GetStandingRightSurface()
        {
            if (standingRightSurface == null)
                standingRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/stand.png");

            return standingRightSurface;
        }

        private Surface GetAttackRightSurface()
        {
            if (attackRightSurface == null)
                attackRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/punch.png");

            return attackRightSurface;
        }

        private Surface GetCrouchedAttackRightSurface()
        {
            if (crouchedAttackRightSurface == null)
                crouchedAttackRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/crouchedPunch.png");

            return crouchedAttackRightSurface;
        }

        private Surface GetAttackLeftSurface()
        {
            if (attackLeftSurface == null)
                attackLeftSurface = GetAttackRightSurface().CreateFlippedHorizontalSurface();

            return attackLeftSurface;
        }

        private Surface GetCrouchedAttackLeftSurface()
        {
            if (crouchedAttackLeftSurface == null)
                crouchedAttackLeftSurface = GetCrouchedAttackRightSurface().CreateFlippedHorizontalSurface();

            return crouchedAttackLeftSurface;
        }

        private Surface GetKickLeftSurface()
        {
            if (kickLeftSurface == null)
                kickLeftSurface = GetKickRightSurface().CreateFlippedHorizontalSurface();

            return kickLeftSurface;
        }

        private Surface GetKickRightSurface()
        {
            if (kickRightSurface == null)
                kickRightSurface = BuildSpriteSurface("./assets/rendered/abrahman/kick.png");

            return kickRightSurface;
        }

        private Surface BuildSpriteSurface(string fileName)
        {
            Surface spriteSurface = new Surface(fileName);

            if (Program.screenHeight != 480)
            {
                double zoom = (double)Program.screenHeight / 480.0;
                spriteSurface = spriteSurface.CreateScaledSurface(zoom);
            }

            return spriteSurface;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Build width
        /// </summary>
        /// <returns>width</returns>
        protected override double BuildWidth()
        {
            return 1.2;
        }

        /// <summary>
        /// Build height
        /// </summary>
        /// <returns>height</returns>
        protected override double BuildHeight()
        {
            return 2.0;
        }

        /// <summary>
        /// Build mass
        /// </summary>
        /// <returns>mass</returns>
        protected override double BuildMass()
        {
            return 1.0;
        }

        public override Surface GetCurrentSurface()
        {
            //If currently attacking
            if (AttackingCycle.GetCycleDivision(8) >= 1)
            {
                if (IsCrouch)
                {
                    if (IsTryingToWalkRight)
                    {
                        return GetCrouchedAttackRightSurface();
                    }
                    else
                    {
                        return GetCrouchedAttackLeftSurface();
                    }
                }
                else if (Ground == null)
                {
                    if (IsTryingToWalkRight)
                    {
                        return GetKickRightSurface();
                    }
                    else
                    {
                        return GetKickLeftSurface();
                    }
                }
                else
                {
                    if (IsTryingToWalkRight)
                    {
                        return GetAttackRightSurface();
                    }
                    else
                    {
                        return GetAttackLeftSurface();
                    }
                }
            }

            if (IsCrouch)
            {
                if (IsTryingToWalkRight)
                    return GetCrouchedRightSurface();
                else
                    return GetCrouchedLeftSurface();
            }

            if (CurrentJumpAcceleration != 0)
            {
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
            return 0.02;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.75;
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
        #endregion
    }
}
