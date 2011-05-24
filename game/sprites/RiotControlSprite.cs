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
        public RiotControlSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
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

        private Surface GetStandingLeftSurface()
        {
            if (standingLeftSurface == null)
                standingLeftSurface = GetStandingRightSurface().CreateFlippedHorizontalSurface();

            return standingLeftSurface;
        }

        private Surface GetStandingRightSurface()
        {
            if (standingRightSurface == null)
                standingRightSurface = BuildSpriteSurface("./assets/rendered/riotControl/stand.png");

            return standingRightSurface;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetStandingRightSurface().CreateFlippedVerticalSurface();

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
            return 0.3;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.25;
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
            return 2.0;
        }

        protected override double BuildMass(Random random)
        {
            return 1.0;
        }

        protected override double BuildMaxHealth()
        {
            return 0.8;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return random.Next(0, 2) == 1;
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

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
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
                return GetDeadSurface();
            }

            if (CurrentJumpAcceleration != 0)
            {
                yOffset = 0.4;
                if (IsTryingToWalkRight)
                    return GetWalkingRightSurface();
                else
                    return GetWalkingLeftSurface();
            }
            else if (CurrentWalkingSpeed != 0)
            {
                int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

                if (cycleDivision == 1)
                {
                    yOffset = 0.4;
                    if (IsTryingToWalkRight)
                        return GetWalkingRightSurface();
                    else
                        return GetWalkingLeftSurface();
                }
                else if (cycleDivision == 3)
                {
                    yOffset = 0.4;
                    if (IsTryingToWalkRight)
                        return GetWalkingRightSurface();
                    else
                        return GetWalkingLeftSurface();
                }
                else
                {
                    yOffset = 0.24;
                    if (IsTryingToWalkRight)
                        return GetStandingRightSurface();
                    else
                        return GetStandingLeftSurface();
                }
            }
            else
            {
                yOffset = 0.24;
                if (IsTryingToWalkRight)
                    return GetStandingRightSurface();
                else
                    return GetStandingLeftSurface();
            }
        }
        #endregion
    }
}