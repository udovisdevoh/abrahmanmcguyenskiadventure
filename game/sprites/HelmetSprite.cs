using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class HelmetSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface walkingLeftSurface;

        private static Surface walkingRightSurface;

        private static Surface deadSurface;

        private static Surface walking2LeftSurface;

        private static Surface walking2RightSurface;

        private static Surface dead2Surface;

        private bool isBlack;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        /// <param name="isBlack">is black</param>
        public HelmetSprite(double xPosition, double yPosition, Random random, bool isBlack)
            : base(xPosition, yPosition, random)
        {
            this.isBlack = isBlack;
        }
        #endregion

        #region Private Methods
        private Surface GetWalkingRightSurface()
        {
            if (walkingRightSurface == null)
                walkingRightSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmet.png");
            return walkingRightSurface;
        }

        private Surface GetWalkingLeftSurface()
        {
            if (walkingLeftSurface == null)
                walkingLeftSurface = GetWalkingRightSurface().CreateFlippedHorizontalSurface();

            return walkingLeftSurface;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetWalkingRightSurface().CreateFlippedVerticalSurface();

            return deadSurface;
        }

        private Surface GetWalking2RightSurface()
        {
            if (walking2RightSurface == null)
                walking2RightSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmet2.png");
            return walking2RightSurface;
        }

        private Surface GetWalking2LeftSurface()
        {
            if (walking2LeftSurface == null)
                walking2LeftSurface = GetWalking2RightSurface().CreateFlippedHorizontalSurface();

            return walking2LeftSurface;
        }

        private Surface GetDead2Surface()
        {
            if (dead2Surface == null)
                dead2Surface = GetWalking2RightSurface().CreateFlippedVerticalSurface();

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
            return 0.1;
            //return 0.3;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.57;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.77;
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
            return 1.0;
        }

        protected override double BuildMass(Random random)
        {
            return 0.5;
        }

        protected override double BuildMaxHealth()
        {
            return 0.4;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return false;
        }

        protected override double BuildJumpProbability()
        {
            return 0.0;
        }

        protected override double BuildHitTime()
        {
            return 32;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 1.0;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
        {
            return false;
        }

        protected override bool BuildIsAiEnabled()
        {
            return false;
        }

        protected override bool BuildIsAvoidFall(Random random)
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
            {
                if (isBlack)
                    return GetDeadSurface();
                else
                    return GetDead2Surface();
            }

            if (isBlack)
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
        #endregion
    }
}