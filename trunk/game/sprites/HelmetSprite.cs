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
        private static Surface walking1aSurface;

        private static Surface walking1bSurface;

        private static Surface walking1cSurface;

        private static Surface walking1dSurface;

        private static Surface deadSurface;

        private static Surface walking2aSurface;

        private static Surface walking2bSurface;

        private static Surface walking2cSurface;

        private static Surface walking2dSurface;

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
        private Surface GetWalking1aSurface()
        {
            if (walking1aSurface == null)
                walking1aSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmet.png");
            return walking1aSurface;
        }

        private Surface GetWalking1bSurface()
        {
            if (walking1bSurface == null)
                walking1bSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmetb.png");
            return walking1bSurface;
        }

        private Surface GetWalking1cSurface()
        {
            if (walking1cSurface == null)
                walking1cSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmetc.png");
            return walking1cSurface;
        }

        private Surface GetWalking1dSurface()
        {
            if (walking1dSurface == null)
                walking1dSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmetd.png");
            return walking1dSurface;
        }

        private Surface GetWalking2aSurface()
        {
            if (walking2aSurface == null)
                walking2aSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmet2.png");
            return walking2aSurface;
        }

        private Surface GetWalking2bSurface()
        {
            if (walking2bSurface == null)
                walking2bSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmet2b.png");
            return walking2bSurface;
        }

        private Surface GetWalking2cSurface()
        {
            if (walking2cSurface == null)
                walking2cSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmet2c.png");
            return walking2cSurface;
        }

        private Surface GetWalking2dSurface()
        {
            if (walking2dSurface == null)
                walking2dSurface = BuildSpriteSurface("./assets/rendered/riotControl/helmet2d.png");
            return walking2dSurface;
        }
        
        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetWalking1aSurface().CreateFlippedVerticalSurface();

            return deadSurface;
        }

        private Surface GetDead2Surface()
        {
            if (dead2Surface == null)
                dead2Surface = GetWalking2aSurface().CreateFlippedVerticalSurface();

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
            return 0.05;
            //return 0.1;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.60;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.60;
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
            return 0.9;
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

        protected override bool BuildIsAvoidFall(Random random)
        {
            return false;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsFullSpeedAfterBounceNoAi()
        {
            return true;
        }

        protected override bool BuildIsToggleWalkWhenJumpedOn()
        {
            return true;
        }

        protected override bool BuildIsInstantKickConvertedSprite()
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
            yOffset = 0;
            if (!IsAlive)
            {
                if (isBlack)
                    return GetDeadSurface();
                else
                    return GetDead2Surface();
            }

            int cycleDivision = WalkingCycle.GetCycleDivision(4.0);

            if (isBlack)
            {
                if (cycleDivision == 0/* || CurrentWalkingSpeed == 0*/)
                {
                    if (IsTryingToWalkRight)
                        return GetWalking1aSurface();
                    else
                        return GetWalking1cSurface();
                }
                else if (cycleDivision == 1)
                    return GetWalking1bSurface();
                else if (cycleDivision == 2)
                {
                    if (IsTryingToWalkRight)
                        return GetWalking1cSurface();
                    else
                        return GetWalking1aSurface();
                }
                else
                    return GetWalking1dSurface();
            }
            else
            {
                if (cycleDivision == 0/* || CurrentWalkingSpeed == 0*/)
                {
                    if (IsTryingToWalkRight)
                        return GetWalking2aSurface();
                    else
                        return GetWalking2cSurface();
                }
                else if (cycleDivision == 1)
                    return GetWalking2bSurface();
                else if (cycleDivision == 2)
                {
                    if (IsTryingToWalkRight)
                        return GetWalking2cSurface();
                    else
                        return GetWalking2aSurface();
                }
                else
                    return GetWalking2dSurface();
            }
        }
        #endregion
    }
}