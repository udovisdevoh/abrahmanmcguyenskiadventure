﻿using System;
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

        /// <summary>
        /// Tutorial's comment
        /// </summary>
        private const string tutorialComment = "Beware of the blob!";
        #endregion

        #region Constructors
        /// <summary>
        /// Create caterpillar sprite
        /// </summary>
        /// <param name="xPosition">x position</param>
        /// <param name="yPosition">y position</param>
        /// <param name="random">random number generator</param>
        public BlobSprite(double xPosition, double yPosition, Random random)
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
            return 0.25;
        }

        protected override double BuildMaxRunningSpeed()
        {
            return 0.55;
        }

        protected override double BuildStartingJumpAcceleration()
        {
            //return 25.0;
            return 5.0;
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

        protected override double BuildSubjectiveOccurenceProbability()
        {
            return 1.0;
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

        protected override bool BuildIsJumpableOnEvenByBeaver()
        {
            return true;
        }

        protected override bool BuildIsCanDoDamageWhenInFreeFall()
        {
            return true;
        }

        protected override bool BuildIsJumpableOn()
        {
            return true;
        }

        protected override double BuildJumpProbability()
        {
            return 1.0;
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
            {
                if (Program.screenHeight > 720)
                    right1Surface = BuildSpriteSurface("./assets/rendered/1080/blob/blob1.png");
                else if (Program.screenHeight > 480)
                    right1Surface = BuildSpriteSurface("./assets/rendered/720/blob/blob1.png");
                else
                    right1Surface = BuildSpriteSurface("./assets/rendered/480/blob/blob1.png");
            }

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
            {
                if (Program.screenHeight > 720)
                    right2Surface = BuildSpriteSurface("./assets/rendered/1080/blob/blob2.png");
                else if (Program.screenHeight > 480)
                    right2Surface = BuildSpriteSurface("./assets/rendered/720/blob/blob2.png");
                else
                    right2Surface = BuildSpriteSurface("./assets/rendered/480/blob/blob2.png");
            }

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
