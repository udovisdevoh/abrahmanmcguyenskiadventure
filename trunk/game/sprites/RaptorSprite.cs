using System;
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
        /// <summary>
        /// Sprite's math mesh
        /// </summary>
        private Surface defaultSurface;

        private Surface walking1LeftSurface;

        private Surface walking1RightSurface;

        private Surface walking2LeftSurface;

        private Surface walking2RightSurface;

        private Surface standingLeftSurface;

        private Surface standingRightSurface;
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
        }
        #endregion

        #region Private Methods
        private Surface GetWalking1RightSurface()
        {
            if (walking1RightSurface == null)
                walking1RightSurface = BuildSpriteSurface("./assets/rendered/raptor/walk1.png");
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
                walking2RightSurface = BuildSpriteSurface("./assets/rendered/raptor/walk2.png");

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
                standingRightSurface = BuildSpriteSurface("./assets/rendered/raptor/stand.png");

            return standingRightSurface;
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
            return 0.004;
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

        protected override double BuildMass(Random random)
        {
            return 6.0;
        }

        protected override double BuildMaxHealth()
        {
            return 1.0;
        }

        protected override bool BuildIsCanJump()
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

        /// <summary>
        /// Get the sprite's current surface
        /// </summary>
        /// <returns>sprite's current surface</returns>
        public override Surface GetCurrentSurface(out double xOffset, out double yOffset)
        {
            xOffset = 0;
            yOffset = 0;

            if (CurrentJumpAcceleration != 0)
            {
                if (IsTryingToWalkRight)
                    return GetWalking1RightSurface();
                else
                    return GetWalking1LeftSurface();
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