using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Core;

namespace AbrahmanAdventure.sprites
{
    class BlobSprite : MonsterSprite
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
        public BlobSprite(float xPosition, float yPosition, Random random)
            : base(xPosition, yPosition, random)
        {
            GetLeft1Surface();
            GetRight1Surface();
            GetLeft2Surface();
            GetRight2Surface();
            GetDeadSurface();
        }
        #endregion

        #region Override Methods
        protected override float BuildJumpingTime()
        {
            return 10.0f;
        }

        protected override float BuildWalkingCycleLength()
        {
            return 5f;
        }

        protected override float BuildWalkingAcceleration()
        {
            return 0.01f;
        }

        protected override float BuildMaxWalkingSpeed()
        {
            return 0.25f;
        }

        protected override float BuildMaxRunningSpeed()
        {
            return 0.55f;
        }

        protected override float BuildStartingJumpAcceleration()
        {
            //return 25.0;
            return 5.0f;
        }

        protected override float BuildAttackingTime()
        {
            return 4f;
        }

        protected override float BuildWidth(Random random)
        {
            return 1.0f;
        }

        protected override float BuildHeight(Random random)
        {
            return 1.0f;
        }

        protected override float BuildMaxHealth()
        {
            return 0.5f;
        }

        protected override float BuildSafeDistanceAi()
        {
            return 0.0f;
        }

        protected override bool BuildIsDieOnTouchGround()
        {
            return false;
        }

        protected override bool BuildIsAnnihilateOnExitScreen()
        {
            return false;
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

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override float BuildJumpProbability()
        {
            return 1.0f;
        }

        protected override float BuildHitTime()
        {
            return 16;
        }

        protected override float BuildAttackStrengthCollision()
        {
            return 0.5f;
        }

        protected override float BuildChangeDirectionNoAiCycleLength()
        {
            return 100;
        }

        protected override bool BuildIsFleeWhenAttacked(Random random)
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

        protected override bool BuildIsMakeSoundWhenTouchGround()
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
        public override Surface GetCurrentSurface(out float xOffset, out float yOffset)
        {
            xOffset = 0;
            yOffset = 0;
            int cycleDivision = WalkingCycle.GetCycleDivision(2.0f);

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
                right1Surface = BuildSpriteSurface("./assets/rendered/blob/blob1.png");

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
                right2Surface = BuildSpriteSurface("./assets/rendered/blob/blob2.png");

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
