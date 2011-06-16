using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class SnakeSprite : MonsterSprite
    {
        #region Fields and parts
        private static Surface right1Surface;

        private static Surface left1Surface;

        private static Surface right2Surface;

        private static Surface left2Surface;

        private static Surface deadSurface;
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public SnakeSprite(double xPosition, double yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            GetLeft1Surface();
            GetLeft2Surface();
            GetRight1Surface();
            GetRight2Surface();
            GetDeadSurface();
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
            return 0.01;
        }

        protected override double BuildMaxWalkingSpeed()
        {
            return 0.30;
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
            return 1.0;
        }

        protected override double BuildMaxHealth()
        {
            return 0.5;
        }

        protected override double BuildSafeDistanceAi()
        {
            return 0.0;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override bool BuildIsCanJump(Random random)
        {
            return true;
        }

        protected override bool BuildIsVulnerableToInvincibility()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageToPlayerWhenTouched()
        {
            return true;
        }

        protected override double BuildJumpProbability()
        {
            return 1.2;
        }

        protected override double BuildHitTime()
        {
            return 16;
        }

        protected override double BuildAttackStrengthCollision()
        {
            return 0.5;
        }

        protected override double BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsNoAiChangeDirectionByCycle()
        {
            return false;
        }

        protected override bool BuildIsAiEnabled()
        {
            return true;
        }

        protected override bool BuildIsAvoidFall(Random random)
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
        }

        public override AbstractSprite GetConverstionSprite(Random random)
        {
            return null;
        }

        protected override bool BuildIsNoAiChangeDirectionWhenStucked()
        {
            return true;
        }

        protected override bool BuildIsNoAiDieWhenStucked()
        {
            return false;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
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

        protected override bool BuildIsInstantKickConvertedSprite()
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

        protected override bool BuildIsNoAiAlwaysBounce()
        {
            return false;
        }

        protected override bool BuildIsMakeSoundWhenTouchGround()
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
            int cycleDivision = WalkingCycle.GetCycleDivision(2.0);

            if (!IsAlive)
                return GetDeadSurface();

            if (cycleDivision == 1)
            {
                if (IsTryingToWalkRight)
                {
                    return GetRight1Surface();
                }
                else
                {
                    return GetLeft1Surface();
                }
            }
            else
            {
                if (IsTryingToWalkRight)
                {
                    return GetRight2Surface();
                }
                else
                {
                    return GetLeft2Surface();
                }
            }
        }
        #endregion

        #region Private Method
        private Surface GetLeft1Surface()
        {
            if (left1Surface == null)
                left1Surface = GetRight1Surface().CreateFlippedHorizontalSurface();

            return left1Surface;
        }

        private Surface GetRight1Surface()
        {
            if (right1Surface == null)
                right1Surface = BuildSpriteSurface("./assets/rendered/snake/snake1.png");

            return right1Surface;
        }

        private Surface GetLeft2Surface()
        {
            if (left2Surface == null)
                left2Surface = GetRight2Surface().CreateFlippedHorizontalSurface();

            return left2Surface;
        }

        private Surface GetRight2Surface()
        {
            if (right2Surface == null)
                right2Surface = BuildSpriteSurface("./assets/rendered/snake/snake2.png");

            return right2Surface;
        }

        private Surface GetDeadSurface()
        {
            if (deadSurface == null)
                deadSurface = GetRight1Surface().CreateFlippedVerticalSurface();

            return deadSurface;
        }
        #endregion
    }
}